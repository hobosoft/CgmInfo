﻿using System;
using CgmInfo.Commands;
using CgmInfo.Commands.Delimiter;
using CgmInfo.Commands.Enums;
using CgmInfo.Commands.MetafileDescriptor;
using CgmInfo.Traversal;
using CgmInfoGui.ViewModels.Nodes;

namespace CgmInfoGui.Traversal
{
    public class ViewModelBuilderVisitor : ICommandVisitor<MetafileContext>
    {
        public void AcceptDelimiterBeginMetafile(BeginMetafile beginMetafile, MetafileContext parameter)
        {
            parameter.BeginLevel(new MetafileViewModel(beginMetafile.Name));
        }

        public void AcceptMetafileDescriptorColorIndexPrecision(ColorIndexPrecision colorIndexPrecision, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("COLOUR INDEX PRECISION: {0} bit", colorIndexPrecision.Precision);
        }

        public void AcceptMetafileDescriptorColorModel(ColorModelCommand colorModel, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("COLOUR MODEL: {0}", colorModel.ColorModel);
        }

        public void AcceptMetafileDescriptorColorPrecision(ColorPrecision colorPrecision, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("COLOUR PRECISION: {0} bit", colorPrecision.Precision);
        }

        public void AcceptMetafileDescriptorColorValueExtent(ColorValueExtent colorValueExtent, MetafileContext parameter)
        {
            var extentNode = parameter.AddMetafileDescriptorNode("COLOUR VALUE EXTENT: Color Space {0}", colorValueExtent.ColorSpace);
            if (colorValueExtent.ColorSpace == ColorSpace.CIE)
            {
                extentNode.Nodes.AddRange(new[]
                {
                    new SimpleNode(string.Format("First Component: {0}", colorValueExtent.FirstComponent)),
                    new SimpleNode(string.Format("Second Component: {0}", colorValueExtent.SecondComponent)),
                    new SimpleNode(string.Format("Third Component: {0}", colorValueExtent.ThirdComponent)),
                });
            }
            else if (colorValueExtent.ColorSpace != ColorSpace.Unknown) // RGB or CMYK
            {
                extentNode.Nodes.AddRange(new[]
                {
                    new SimpleNode(string.Format("Minimum: {0}", colorValueExtent.Minimum)),
                    new SimpleNode(string.Format("Maximum: {0}", colorValueExtent.Maximum)),
                });
            }
        }

        public void AcceptMetafileDescriptorIndexPrecision(IndexPrecision indexPrecision, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("INDEX PRECISION: {0} bit", indexPrecision.Precision);
        }

        public void AcceptMetafileDescriptorIntegerPrecision(IntegerPrecision integerPrecision, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("INTEGER PRECISION: {0} bit", integerPrecision.Precision);
        }

        public void AcceptMetafileDescriptorMaximumColorIndex(MaximumColorIndex maximumColorIndex, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("MAXIMUM COLOUR INDEX: {0}", maximumColorIndex.Index);
        }

        public void AcceptMetafileDescriptorMetafileDescription(MetafileDescription metafileDescription, MetafileContext parameter)
        {
            parameter.AddDescriptorNode(new MetafileDescriptionViewModel(metafileDescription.Description));
        }

        public void AcceptMetafileDescriptorMetafileVersion(MetafileVersion metafileVersion, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("METAFILE VERSION: {0}", metafileVersion.Version);
        }

        public void AcceptMetafileDescriptorNamePrecision(NamePrecision namePrecision, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("NAME PRECISION: {0} bit", namePrecision.Precision);
        }

        public void AcceptMetafileDescriptorRealPrecision(RealPrecision realPrecision, MetafileContext parameter)
        {
            var realNode = parameter.AddMetafileDescriptorNode("REAL PRECISION: {0}", realPrecision.RepresentationForm);
            realNode.Nodes.AddRange(new[]
            {
                new SimpleNode(string.Format("Exponent Width: {0} bit", realPrecision.ExponentWidth)),
                new SimpleNode(string.Format("Fraction Width: {0} bit", realPrecision.FractionWidth)),
            });
        }

        public void AcceptMetafileDescriptorVdcType(VdcType vdcType, MetafileContext parameter)
        {
            parameter.AddMetafileDescriptorNode("VDC TYPE: {0}", vdcType.Specification);
        }

        public void AcceptMetafileDescriptorFontList(FontList fontList, MetafileContext parameter)
        {
            parameter.AddDescriptorNode(new FontListViewModel(fontList));
        }

        public void AcceptMetafileDescriptorMaximumVdcExtent(MaximumVdcExtent maximumVdcExtent, MetafileContext parameter)
        {
            var maxVdcNode = parameter.AddMetafileDescriptorNode("MAXIMUM VDC EXTENT: {0} by {1}",
                Math.Abs(maximumVdcExtent.SecondCorner.X - maximumVdcExtent.FirstCorner.X),
                Math.Abs(maximumVdcExtent.SecondCorner.Y - maximumVdcExtent.FirstCorner.Y));
            maxVdcNode.Nodes.AddRange(new[]
            {
                new SimpleNode(string.Format("First Corner: {0}", maximumVdcExtent.FirstCorner)),
                new SimpleNode(string.Format("Second Corner: {0}", maximumVdcExtent.SecondCorner)),
            });
        }

        public void AcceptUnsupportedCommand(UnsupportedCommand unsupportedCommand, MetafileContext parameter)
        {
            // do nothing
        }
    }
}
