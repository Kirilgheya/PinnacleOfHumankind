using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TreeContainer;

namespace GameUI.UI.Utilities
{
    /// <summary>
    /// Interaction logic for TreeVisualizer.xaml
    /// </summary>
    public partial class TreeVisualizer : Window
    {
		bool fUseLogicalTree = true;
		public TreeVisualizer()
        {
            InitializeComponent();
		}

		private void tcMain_Click(object sender, RoutedEventArgs e)
		{
			Button btn = e.OriginalSource as Button;
			if (btn != null)
			{
				TreeNode tn = (TreeNode)(btn.Parent);
				tn.Collapsed = !tn.Collapsed;
			}
		}

		private void DrawComponentTree(Object o, TreeNode tnControl)
		{
			TreeNode tnSubtreeRoot;
			Button btn = new Button();
			btn.Content = o.GetType().Name;

			if (tnControl == null)
			{
				tnSubtreeRoot = tcMain.AddRoot(btn);
			}
			else
			{
				tnSubtreeRoot = tcMain.AddNode(btn, tnControl);
			}

			DependencyObject dob = o as DependencyObject;

			if (dob != null)
			{
				if (fUseLogicalTree)
				{
					foreach (object objChild in LogicalTreeHelper.GetChildren(dob))
					{
						DrawComponentTree(objChild, tnSubtreeRoot);
					}
				}
				else
				{
					for (int iChild = 0; iChild < VisualTreeHelper.GetChildrenCount(dob); iChild++)
					{
						DrawComponentTree(VisualTreeHelper.GetChild(dob, iChild), tnSubtreeRoot);
					}
				}
			}
		}

		private void DrawComponentTree()
		{
			tcMain.Clear();
			DrawComponentTree(spnlDialog, null);
		}

		private void rbVisualTree_Checked(object sender, RoutedEventArgs e)
		{
			fUseLogicalTree = false;
			DrawComponentTree();
		}

		private void rbLogicalTree_Checked(object sender, RoutedEventArgs e)
		{
			fUseLogicalTree = true;
			DrawComponentTree();
		}
	}
}
