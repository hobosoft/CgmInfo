using System.Windows;
using System.Windows.Media;

namespace CgmInfoGui.Visuals
{
    public class CircleVisual : VisualBase
    {
        public CircleVisual(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }
        public Point Center { get; private set; }
        public double Radius { get; private set; }

        protected internal override void DrawTo(DrawingContext drawingContext, VisualContext visualContext)
        {
            drawingContext.DrawEllipse(null, GetBlack(), visualContext.Correct(Center), Radius, Radius);
        }
    }
}
