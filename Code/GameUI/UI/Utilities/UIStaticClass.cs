using GameUI.UI.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    }
}
