using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace CgmInfoGui.Visuals
{
    public class TextVisual : VisualBase
    {
        public TextVisual(string text, Point location)
        {
            Text = text;
            Location = location;
        }
        public string Text { get; set; }
        public Point Location { get; private set; }

        protected internal override void DrawTo(DrawingContext drawingContext, VisualContext visualContext)
        {
            var formattedText = new FormattedText(Text, CultureInfo.GetCultureInfo("en"), FlowDirection.LeftToRight, new Typeface(new FontFamily("Arial"), new FontStyle(), new FontWeight(), new FontStretch()), 12, Brushes.Black);
            drawingContext.DrawText(formattedText, visualContext.Correct(Location));
        }
    }
}
