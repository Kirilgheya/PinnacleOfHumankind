using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GameUI.UI.DataSource.UIItems_DS
{
    class TreeViewStarSystems : INotifyPropertyChanged
    {

        public ObservableCollection<StarSystem> StarSystems { get;  }

  
        public TreeViewStarSystems()
        {

            StarSystems = new ObservableCollection<StarSystem>();
        }

        void NotifiyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
