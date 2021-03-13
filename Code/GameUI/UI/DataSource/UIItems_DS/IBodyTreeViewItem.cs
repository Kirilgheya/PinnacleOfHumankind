using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GameUI.UI.DataSource.UIItems_DS
{
    abstract class IBodyTreeViewItem
    {

        protected Ellipse Shape;

         public string Name { get; set; }
        public ObservableCollection<IBodyTreeViewItem> Children { get; set; }

        protected abstract void setName();

        protected abstract void setChildren();

        public Ellipse drawBody()
        {

            this.childrenDrawBody();

            return Shape;
        }

        protected abstract void childrenDrawBody();

    }
}
