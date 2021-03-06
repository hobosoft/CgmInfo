using CgmInfo.Traversal;
using CgmInfo.Utilities;

namespace CgmInfo.Commands.GraphicalPrimitives
{
    public class CellArray : Command
    {
        public CellArray(MetafilePoint cornerPointP, MetafilePoint cornerPointQ, MetafilePoint cornerPointR, int nx, int ny, MetafileColor[] colors)
            : base(4, 9)
        {
            CornerPointP = cornerPointP;
            CornerPointQ = cornerPointQ;
            CornerPointR = cornerPointR;
            NX = nx;
            NY = ny;
            Colors = colors;
        }

        public MetafilePoint CornerPointP { get; private set; }
        public MetafilePoint CornerPointQ { get; private set; }
        public MetafilePoint CornerPointR { get; private set; }
        public int NX { get; private set; }
        public int NY { get; private set; }
        public MetafileColor[] Colors { get; private set; }

        public override void Accept<T>(ICommandVisitor<T> visitor, T parameter)
        {
            visitor.AcceptGraphicalPrimitiveCellArray(this, parameter);
        }
    }
}
