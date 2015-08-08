using System;
using System.IO;
using CgmInfo.Commands;
using CgmInfo.Commands.Enums;

namespace CgmInfo.Binary
{
    public class MetafileReader : IDisposable
    {
        private readonly string _fileName;
        private readonly BinaryReader _reader;
        private readonly MetafileDescriptor _descriptor = new MetafileDescriptor();

        private bool _insideMetafile;

        public MetafileDescriptor Descriptor
        {
            get { return _descriptor; }
        }

        public MetafileReader(string fileName)
        {
            _fileName = fileName;
            _reader = new BinaryReader(File.OpenRead(fileName));
        }

        public Command ReadCommand()
        {
            // stop at EOF; or when we cannot at least read another command header
            if (_reader.BaseStream.Position + 2 >= _reader.BaseStream.Length)
                return null;

            Command result;
            CommandHeader commandHeader = ReadCommandHeader();
            // ISO/IEC 8632-3 8.1, Table 2
            switch (commandHeader.ElementClass)
            {
                case 0: // delimiter
                    result = ReadDelimiterElement(commandHeader);
                    break;
                case 1: // metafile descriptor
                    result = ReadMetafileDescriptorElement(commandHeader);
                    break;
                case 2: // picture descriptor
                case 3: // control
                case 4: // graphical primitive
                case 5: // attribute
                case 6: // escape
                case 7: // external
                case 8: // segment control/segment attribute
                case 9: // application structure descriptor
                default:
                    result = ReadUnsupportedElement(commandHeader);
                    break;
            }

            if (result != null && !_insideMetafile)
            {
                if (result.ElementClass != 0)
                    throw new FormatException("Expected Element Class 0 (Delimiter) at the beginning of a Metafile");
                if (result.ElementId != 1)
                    throw new FormatException("Expected Element Id 1 (BEGIN METAFILE) at the beginning of a Metafile");
            }

            return result;
        }

        private CommandHeader ReadCommandHeader()
        {
            // commands are always word aligned [ISO/IEC 8632-3 5.4]
            if (_reader.BaseStream.Position % 2 == 1)
                _reader.BaseStream.Seek(1, SeekOrigin.Current);

            ushort commandHeader = ReadWord();
            int elementClass = (commandHeader >> 12) & 0xF;
            int elementId = (commandHeader >> 5) & 0x7F;
            int parameterListLength = commandHeader & 0x1F;

            bool isLongFormat = parameterListLength == 0x1F;
            if (isLongFormat)
            {
                ushort longFormCommandHeader = ReadWord();
                int partitionFlag = (longFormCommandHeader >> 15) & 0x1;
                bool isLastPartition = partitionFlag == 0;
                if (!isLastPartition)
                    throw new InvalidOperationException("Sorry, cannot read command headers with parameters larger than 32767 octets");
                parameterListLength = longFormCommandHeader & 0x7FFF;
            }

            bool isNoop = elementClass == 0 && elementId == 0;
            if (isNoop)
            {
                if (parameterListLength > 2)
                    // TODO: length seems to include the 2 bytes already read for the header?
                    //       spec is not exactly clear there =/ [ISO/IEC 8632-3 8.2 Table 3 add.]
                    _reader.BaseStream.Seek(parameterListLength - 2, SeekOrigin.Current);
                return ReadCommandHeader();
            }

            return new CommandHeader(elementClass, elementId, parameterListLength);
        }

        private Command ReadUnsupportedElement(CommandHeader commandHeader)
        {
            // skip the command parameter bytes we don't know
            _reader.BaseStream.Seek(commandHeader.ParameterListLength, SeekOrigin.Current);
            return new UnsupportedCommand(commandHeader.ElementClass, commandHeader.ElementId);
        }

        private Command ReadDelimiterElement(CommandHeader commandHeader)
        {
            Command result;
            // ISO/IEC 8632-3 8.2, Table 3
            switch (commandHeader.ElementId)
            {
                //case 0: // no-op; these are skipped already while reading the command header
                case 1: // BEGIN METAFILE
                    result = DelimiterElementReader.BeginMetafile(this, commandHeader);
                    _insideMetafile = result != null;
                    break;
                case 2: // END METAFILE
                case 3: // BEGIN PICTURE
                case 4: // BEGIN PICTURE BODY
                case 5: // END PICTURE
                case 6: // BEGIN SEGMENT
                case 7: // END SEGMENT
                case 8: // BEGIN FIGURE
                case 9: // END FIGURE
                case 13: // BEGIN PROTECTION REGION
                case 14: // END PROTECTION REGION
                case 15: // BEGIN COMPOUND LINE
                case 16: // END COMPOUND LINE
                case 17: // BEGIN COMPOUND TEXT PATH
                case 18: // END COMPOUND TEXT PATH
                case 19: // BEGIN TILE ARRAY
                case 20: // END TILE ARRAY
                case 21: // BEGIN APPLICATION STRUCTURE
                case 22: // BEGIN APPLICATION STRUCTURE BODY
                case 23: // END APPLICATION STRUCTURE
                default:
                    result = ReadUnsupportedElement(commandHeader);
                    break;
            }
            return result;
        }

        private Command ReadMetafileDescriptorElement(CommandHeader commandHeader)
        {
            Command result;
            // ISO/IEC 8632-3 8.3, Table 4
            switch (commandHeader.ElementId)
            {
                case 1: // METAFILE VERSION
                    result = MetafileDescriptorReader.MetafileVersion(this, commandHeader);
                    break;
                case 2: // METAFILE DESCRIPTION
                    result = MetafileDescriptorReader.MetafileDescription(this, commandHeader);
                    break;
                case 3: // VDC TYPE
                    result = MetafileDescriptorReader.VdcType(this, commandHeader);
                    break;
                case 4: // INTEGER PRECISION
                    result = MetafileDescriptorReader.IntegerPrecision(this, commandHeader);
                    break;
                case 5: // REAL PRECISION
                    var realPrecision = MetafileDescriptorReader.RealPrecision(this, commandHeader);
                    _descriptor.RealPrecision = realPrecision.Specification;
                    result = realPrecision;
                    break;
                case 6: // INDEX PRECISION
                    result = MetafileDescriptorReader.IndexPrecision(this, commandHeader);
                    break;
                case 7: // COLOUR PRECISION
                    var colorPrecision = MetafileDescriptorReader.ColorPrecision(this, commandHeader);
                    _descriptor.ColorPrecision = colorPrecision.Precision;
                    result = colorPrecision;
                    break;
                case 8: // COLOUR INDEX PRECISION
                    result = MetafileDescriptorReader.ColorIndexPrecision(this, commandHeader);
                    break;
                case 9: // MAXIMUM COLOUR INDEX
                    result = MetafileDescriptorReader.MaximumColorIndex(this, commandHeader);
                    break;
                case 10: // COLOUR VALUE EXTENT
                    result = MetafileDescriptorReader.ColorValueExtent(this, commandHeader);
                    break;
                case 19: // COLOUR MODEL
                    var colorModel = MetafileDescriptorReader.ColorModelCommand(this, commandHeader);
                    _descriptor.ColorModel = colorModel.ColorModel;
                    result = colorModel;
                    break;
                case 11: // METAFILE ELEMENT LIST
                case 12: // METAFILE DEFAULTS REPLACEMENT
                case 13: // FONT LIST
                case 14: // CHARACTER SET LIST
                case 15: // CHARACTER CODING ANNOUNCER
                case 16: // NAME PRECISION
                case 17: // MAXIMUM VDC EXTENT
                case 18: // SEGMENT PRIORITY EXTENT
                case 20: // COLOUR CALIBRATION
                case 21: // FONT PROPERTIES
                case 22: // GLYPH MAPPING
                case 23: // SYMBOL LIBRARY LIST
                case 24: // PICTURE DIRECTORY
                default:
                    result = ReadUnsupportedElement(commandHeader);
                    break;
            }
            return result;
        }

        internal int ReadInteger(int numBytes)
        {
            if (numBytes < 1 || numBytes > 4)
                throw new ArgumentOutOfRangeException("numBytes", numBytes, "Number of bytes must be between 1 and 4");
            int ret = 0;
            while (numBytes-- > 0)
                ret = (ret << 8) | _reader.ReadByte();
            return ret;
        }

        internal int ReadColorValue()
        {
            return ReadInteger(Descriptor.ColorPrecision / 8);
        }
        private double ReadFixedPoint(int numBytes)
        {
            // ISO/IEC 8632-3 6.4
            // real value is computed as "whole + (fraction / 2**exp)"
            // exp is the width of the fraction value
            int whole = ReadInteger(numBytes / 2);
            int fraction = ReadInteger(numBytes / 2);
            // if someone wanted a 4 byte fixed point real, they get 32 bits (16 bits whole, 16 bits fraction)
            // therefore exp would be 16 here (same for 8 byte with 64 bits and 32/32 -> 32 exp)
            int exp = numBytes / 2 * 8;
            return whole + fraction / Math.Pow(2, exp);
        }
        private double ReadFloatingPoint(int numBytes)
        {
            // ISO/IEC 8632-3 6.5
            // C# float/double conform to ANSI/IEEE 754 and have the same format as the specification wants;
            // so simply using BinaryReader works out just fine.
            if (numBytes == 4)
                return _reader.ReadSingle();
            if (numBytes == 8)
                return _reader.ReadDouble();

            throw new InvalidOperationException(string.Format("Sorry, cannot read a floating point value with {0} bytes", numBytes));
        }
        internal double ReadReal()
        {
            switch (Descriptor.RealPrecision)
            {
                case RealPrecisionSpecification.FixedPoint32Bit:
                    return ReadFixedPoint(4);
                case RealPrecisionSpecification.FixedPoint64Bit:
                    return ReadFixedPoint(8);
                case RealPrecisionSpecification.FloatingPoint32Bit:
                    return ReadFloatingPoint(4);
                case RealPrecisionSpecification.FloatingPoint64Bit:
                    return ReadFloatingPoint(8);
            }
            throw new NotSupportedException("The current Real Precision is not supported");
        }
        internal ushort ReadWord()
        {
            return (ushort)((_reader.ReadByte() << 8) | _reader.ReadByte());
        }

        internal string ReadString()
        {
            return _reader.ReadString();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
