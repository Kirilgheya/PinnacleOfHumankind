using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameUI.Keybinds
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Exit",
                "Exit",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        public static readonly RoutedUICommand Race = new RoutedUICommand
          (
              "Race",
              "Race",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Empire = new RoutedUICommand
          (
              "Empire",
              "Empire",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Science = new RoutedUICommand
       (
           "Science",
           "Science",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
           }
       );

        public static readonly RoutedUICommand Create = new RoutedUICommand
          (
              "Create",
              "Create",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Log = new RoutedUICommand
          (
              "Log",
              "Log",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Settings = new RoutedUICommand
          (
              "Settings",
              "Settings",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Planet = new RoutedUICommand
          (
              "Planet",
              "Planet",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Project = new RoutedUICommand
          (
              "Project",
              "Project",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Market = new RoutedUICommand
          (
              "Market",
              "Market",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Diplomacy = new RoutedUICommand
          (
              "Diplomacy",
              "Diplomacy",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );
        //Define more commands here, just like the one above
    }
}
