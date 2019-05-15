using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POH_UI.Utils
{
	public class DataListView : ListView
	{
		public DataListView()
		{
			View = View.Details;
		}
		public void SetDataBinding(object dataSource, string dataMember)
		{
			var cm = BindingContext[dataSource] as CurrencyManager;
			RefreshList(cm);
			if (cm.List != null)
				cm.ListChanged += (s, e) =>
					RefreshList(cm);       // note: e.ListChangedType
		}
		private void RefreshList(CurrencyManager cm)
		{
			this.Clear();
			var props = cm.GetItemProperties();
			foreach (PropertyDescriptor pd in props)
				this.Columns.Add(new ColumnHeader
				{
					Text = pd.Name,
					Width = 100,
					TextAlign = HorizontalAlignment.Left
				});
			foreach (object itm in cm.List)
			{
				var items = new string[props.Count];
				for (int i = 0; i < props.Count; i++)
					items[i] = Convert.ToString(props[i].GetValue(itm));
				this.Items.Add(new ListViewItem(items));
			}
		}
	}
}
