using GameUI.UI.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameCore = MainGame.Applicazione.DataModel;

namespace GameUI.UI.Utilities
{
    public static class UIStaticClass
    {
        public static void Show_body_info(object to_show)
        {
            if (to_show is Ellipse)
            {
                if ((to_show as Ellipse).Tag is GameCore.Star)
                {
                    show_message(((to_show as Ellipse).Tag as GameCore.Star).ToString_Info());
                }

                if ((to_show as Ellipse).Tag is GameCore.Planet)
                {
                    show_message(((to_show as Ellipse).Tag as GameCore.Planet).ToString() +"\n" + ((to_show as Ellipse).Tag as GameCore.Planet).flavour_text());
                }
            }
            else if (to_show is Path)
            {
                if ((to_show as Path).Tag is GameCore.Star)
                {
                    show_message(((to_show as Path).Tag as GameCore.Star).ToString_Info());
                }

                if ((to_show as Path).Tag is GameCore.Planet)
                {
                    show_message(((to_show as Path).Tag as GameCore.Planet).ToString() + "\n" + ((to_show as Path).Tag as GameCore.Planet).flavour_text());
                }
            }

        }

        public static void show_message(String Message)
        {
            MessageBox.Show(Message);
        }

        public static void generateOrbitForBody(Canvas _canvas, Ellipse _body,Point _center, Point _bodyCoordinates, SolidColorBrush _color = null)
        {

            if(_color == null)
            {
                _color = Brushes.Yellow;
            }

            Path orbitPath = new Path
            {
                Stroke = _color,
                StrokeThickness = 2,
                Fill = null

            };
            EllipseGeometry eg = new EllipseGeometry();
            double length = Point.Subtract(_center, _bodyCoordinates).Length;

            eg.Center = _center;
            eg.RadiusX = length - (_body.Width / 2);
            eg.RadiusY = length - (_body.Width / 2);

            // Add all the geometries to a GeometryGroup.  
            GeometryGroup orbitGroup = new GeometryGroup();
            orbitGroup.Children.Add(eg);

            // Set Path.Data  
            orbitPath.Data = orbitGroup;


            Line line = new Line();
            line.X1 = _center.X;
            line.X2 = _bodyCoordinates.X;
            line.Y1 = _center.Y;
            line.Y2 = _bodyCoordinates.Y;

            _canvas.Children.Add(orbitPath);
            _canvas.Children.Add(line);
        }
    }
}
