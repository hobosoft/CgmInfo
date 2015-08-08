using CgmInfo.Commands.Enums;
using CgmInfo.Traversal;

namespace CgmInfo.Commands.MetafileDescriptor
{
    public class VdcType : Command
    {
        public VdcType(int specification)
            : base(1, 3)
        {
            Specification = (VdcTypeSpecification)specification;
        }

        public VdcTypeSpecification Specification { get; private set; }

        public override void Accept<T>(ICommandVisitor<T> visitor, T parameter)
        {
            visitor.AcceptMetafileDescriptorVdcType(this, parameter);
        }
    }
}