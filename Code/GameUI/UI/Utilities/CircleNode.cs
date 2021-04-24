using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI.UI.Utilities
{
    class CircleNode : IDrawable
    {
        // The string we will draw.
        public string Text;

        // Constructor.
        public CircleNode(string new_text)
        {
            Text = new_text;
        }

        // Return the size of the string plus a 10 pixel margin.
        public SizeF GetSize(Graphics gr, Font font)
        {
            return gr.MeasureString(Text, font) + new SizeF(10, 10);
        }

        // Draw the object centered at (x, y).
        void IDrawable.Draw(float x, float y, Graphics gr, Pen pen, Brush bg_brush, Brush text_brush, Font font)
        {
            // Fill and draw an ellipse at our location.
            SizeF my_size = GetSize(gr, font);
            RectangleF rect = new RectangleF(
                x - my_size.Width / 2,
                y - my_size.Height / 2,
                my_size.Width, my_size.Height);
            gr.FillEllipse(bg_brush, rect);
            gr.DrawEllipse(pen, rect);

            // Draw the text.
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;
                gr.DrawString(Text, font, text_brush, x, y, string_format);
            }
        }

        // Return true if the node is above this point.
        // Note: The equation for an ellipse with half
        // width w and half height h centered at the origin is:
        //      x*x/w/w + y*y/h/h <= 1.
        bool IDrawable.IsAtPoint(Graphics gr, Font font, PointF center_pt, PointF target_pt)
        {
            // Get our size.
            SizeF my_size = GetSize(gr, font);

            // translate so we can assume the
            // ellipse is centered at the origin.
            target_pt.X -= center_pt.X;
            target_pt.Y -= center_pt.Y;

            // Determine whether the target point is under our ellipse.
            float w = my_size.Width / 2;
            float h = my_size.Height / 2;
            return
                target_pt.X * target_pt.X / w / w +
                target_pt.Y * target_pt.Y / h / h
                <= 1;
        }
    }
}
