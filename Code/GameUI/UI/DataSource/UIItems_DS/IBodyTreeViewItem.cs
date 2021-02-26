using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI.UI.DataSource.UIItems_DS
{
    abstract class IBodyTreeViewItem
    {
  
         public string Name { get; set; }
        public ObservableCollection<IBodyTreeViewItem> Children { get; set; }

        protected abstract void setName();

        protected abstract void setChildren();
    }
}
