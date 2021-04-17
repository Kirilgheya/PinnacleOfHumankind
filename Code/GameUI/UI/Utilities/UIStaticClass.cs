﻿using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
using GameUI.UI.GameEngine;
using GameUI.UI.Interfaccia;
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
        public static PlanetInfoPage pg = new PlanetInfoPage();

        private static Random random = new Random();

        //ritorna true se devi refreshare lo schermo (sennò non ricaricava i pianeti levando loro il selezionato);
        public static bool Show_body_info(object to_show)
        {


            if (to_show is Ellipse)
            {
                if ((to_show as Ellipse).Tag is Star)
                {
                    if(((to_show as Ellipse).Tag as Star).selected && pg.IsVisible)   //ctrl click con pianeta selezionato e finestra aperta chiude tutto e deseleziona
                    {
                        pg.Close();
                        GameSession.UpdateSelected((to_show as Ellipse).Tag as Star);
                    }
                    else if ((!((to_show as Ellipse).Tag as Star).selected) && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra chiusa apri
                    {
                        GameSession.UpdateSelected((to_show as Ellipse).Tag as Star);
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Ellipse).Tag as Star);
                    }
                    else if (((to_show as Ellipse).Tag as Star).selected && !pg.IsVisible)  // ctrl click con pianeta selezionato e finestra chiusa apri
                    {
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Ellipse).Tag as Star);
                    }
                    else if (((to_show as Ellipse).Tag as Star).selected && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra aperta seleziona
                    {
                        GameSession.UpdateSelected((to_show as Ellipse).Tag as Star);
                    }

                    if (((to_show as Ellipse).Tag as Star).bodyShape.StrokeThickness == 0 )
                    {
                        return true;
                    }

                }

                if ((to_show as Ellipse).Tag is Planet)
                {
                    if (((to_show as Ellipse).Tag as Planet).selected && pg.IsVisible)   //ctrl click con pianeta selezionato e finestra aperta chiude tutto e deseleziona
                    {
                        pg.Close();
                        GameSession.UpdateSelected((to_show as Ellipse).Tag as Planet);
                    }
                    else if ((!((to_show as Ellipse).Tag as Planet).selected) && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra chiusa apri e seleziona
                    {
                        GameSession.UpdateSelected((to_show as Ellipse).Tag as Planet);
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Ellipse).Tag as Planet);
                    }
                    else if (((to_show as Ellipse).Tag as Planet).selected && !pg.IsVisible)  // ctrl click con pianeta selezionato e finestra chiusa apri
                    {
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Ellipse).Tag as Planet);
                    }
                    else if (((to_show as Ellipse).Tag as Planet).selected && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra aperta seleziona
                    {
                        GameSession.UpdateSelected((to_show as Ellipse).Tag as Planet);
                    }

                    if (((to_show as Ellipse).Tag as Planet).bodyShape.StrokeThickness == 0)
                    {
                        return true;
                    }
                }
            }
            else if (to_show is Path)
            {
                if ((to_show as Path).Tag is Star)
                {
                    if (((to_show as Path).Tag as Star).selected && pg.IsVisible)   //ctrl click con pianeta selezionato e finestra aperta chiude tutto e deseleziona
                    {
                        pg.Close();
                        GameSession.UpdateSelected((to_show as Path).Tag as Star);
                    }
                    else if ((!((to_show as Path).Tag as Star).selected) && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra chiusa apri
                    {
                        GameSession.UpdateSelected((to_show as Path).Tag as Star);
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Path).Tag as Star);
                    }
                    else if (((to_show as Path).Tag as Star).selected && !pg.IsVisible)  // ctrl click con pianeta selezionato e finestra chiusa apri
                    {
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Path).Tag as Star);
                    }
                    else if (((to_show as Path).Tag as Star).selected && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra aperta seleziona
                    {
                        GameSession.UpdateSelected((to_show as Path).Tag as Star);
                    }

                    if (((to_show as Path).Tag as Star).bodyShape.StrokeThickness == 0 )
                    {
                        return true;
                    }
                }

                if ((to_show as Path).Tag is Planet)
                {
                    if (((to_show as Path).Tag as Planet).selected && pg.IsVisible)   //ctrl click con pianeta selezionato e finestra aperta chiude tutto e deseleziona
                    {
                        pg.Close();
                        GameSession.UpdateSelected((to_show as Path).Tag as Planet);
                    }
                    else if ((!((to_show as Path).Tag as Planet).selected) && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra chiusa apri e seleziona
                    {
                        GameSession.UpdateSelected((to_show as Path).Tag as Planet);
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Path).Tag as Planet);
                    }
                    else if (((to_show as Path).Tag as Planet).selected && !pg.IsVisible)  // ctrl click con pianeta selezionato e finestra chiusa apri
                    {
                        pg = new PlanetInfoPage();
                        pg.Show();
                        pg.LoadInfo((to_show as Path).Tag as Planet);
                    }
                    else if (((to_show as Path).Tag as Planet).selected && !pg.IsVisible)  // ctrl click con pianeta non selezionato e finestra aperta seleziona
                    {
                        GameSession.UpdateSelected((to_show as Path).Tag as Planet);
                    }

                    if (((to_show as Path).Tag as Planet).bodyShape.StrokeThickness == 0 )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static double GetNextOrbitAngle(IBodyTreeViewItem planet, double increment = 0)
        {

            double angle = 0;
            double oldAngle = planet.angleOnOrbit;

            if (!planet.hasMoved() && increment == 0)
            {
                angle = random.Next(0, 359);

                if (oldAngle > angle - 10 && oldAngle < angle + 10)
                {
                    angle = random.Next(0, 359);
                }
            }
            else
            {

                angle = planet.angleOnOrbit;
            }

            return angle;
        }

        public static void ScatterBodiesOnOrbit(List<IBodyTreeViewItem> _planets)
        {

            foreach(IBodyTreeViewItem planet in _planets)
            {

                planet.angleOnOrbit = UIStaticClass.GetNextOrbitAngle(planet);
            }
        }

        public static Brush ColorToBrush(string color) // color = "#E7E44D"
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }


        public static SolidColorBrush BrushFromHex(string hexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexColorString));
        }

    
        public static void getPathForClass(MainGame.StarClassification_byLum _lum)
        {

          
        }

        public static double generateOrbitForBody(Canvas _canvas, Ellipse _body,Point _center, Point _bodyCoordinates, SolidColorBrush _color = null, object body = null)
        {

            if(_color == null)
            {
                _color = Brushes.Yellow;
            }

            Path orbitPath = new Path
            {
                Stroke = _color,
                StrokeThickness = 1,
                Fill = null,
                Tag = body
            };

            

            EllipseGeometry eg = new EllipseGeometry();
            double length = Point.Subtract(_center, new Point(Canvas.GetLeft(_body),Canvas.GetTop(_body))).Length;

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

            Canvas.SetZIndex(orbitPath, 0);
            Canvas.SetZIndex(line, 0);

            return eg.RadiusX;
        }

        public static Ellipse RedrawStar( Star star, double scale, int numberOfstars, double deltaFromCenter, double timePassed)
        {


            double angolo = 360 / numberOfstars;

            //Draw new stars

            Ellipse starShape = star.drawBody(scale);

            Point origStarCoordinates = new Point();

            origStarCoordinates.X = (starShape.Width / 2 + (deltaFromCenter / scale));

            origStarCoordinates.Y = starShape.Width / 2;

            setBodyPosition(star, starShape, origStarCoordinates);

            if (!star.hasMoved())
            {
                UIStaticClass.ScatterBodiesOnOrbit(new List<IBodyTreeViewItem>() { star });
            }

            //lbl_delta.Content = lbl_delta.Content + "Star: " + selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n];


            return starShape;
        }

        public static void setBodyPosition(IBodyTreeViewItem _body, Shape _bodyShape, Point _position)
        {

            Canvas.SetLeft(_bodyShape, _position.X);
            Canvas.SetTop(_bodyShape, _position.Y);
            _body.position = _position;
        }

        public static void moveBodyOnOrbit(IBodyTreeViewItem _body, double _radiants, double _orbitradius,Point _origin, Boolean _isClockwise)
        {

            Point nextPosition;
            double x, y;
        
            if(_isClockwise)
            {
                _radiants = _radiants * -1;
            }
            x = (Math.Cos(_radiants) * _orbitradius) + _origin.X - (_body.bodyShape.Width/2);
            y = (Math.Sin(_radiants) * _orbitradius) + _origin.Y - (_body.bodyShape.Width / 2);
           
            /*
            x =  (Math.Cos(_degrees) * _body.position.X) + (Math.Sin(_degrees) * _body.position.Y);
            y = -(Math.Sin(_degrees) * _body.position.X) + (Math.Cos(_degrees) * _body.position.Y);
            */
            nextPosition = new Point(x, y);

            _body.position = nextPosition;
  

            Canvas.SetLeft(_body.bodyShape, _body.position.X);
            Canvas.SetTop(_body.bodyShape, _body.position.Y);

            

        }

        public static void moveBodyOnOrbit2(Planet _body, double _radiants, double _orbitradius, Point _origin, Boolean _isClockwise)
        {

            Point nextPosition;
            double x, y;

            if (_isClockwise)
            {
                _radiants = _radiants * -1;
            }
            x = (Math.Cos(_radiants) * _orbitradius) + _origin.X - (_body.bodyShape.Width / 2);
            y = (Math.Sin(_radiants) * _orbitradius) + _origin.Y - (_body.bodyShape.Width / 2);

            /*
            x =  (Math.Cos(_degrees) * _body.position.X) + (Math.Sin(_degrees) * _body.position.Y);
            y = -(Math.Sin(_degrees) * _body.position.X) + (Math.Cos(_degrees) * _body.position.Y);
            */
            nextPosition = new Point(x, y);

            _body.position = nextPosition;


        }

        public static double DegreeToRadiants(double _degrees = -1, double _radiants = -1)
        {

            if(_degrees == -1)
            {

                return _radiants / (Math.PI / 180);
            }
            else
            {

                return _degrees * (Math.PI / 180);
            }
        }

        public static Boolean AdvanceTimeStep(double _timestep)
        {
            
            GameSession.timeStep += _timestep;

            return true;
        }
    }
}
