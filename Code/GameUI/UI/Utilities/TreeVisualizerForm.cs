using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI.UI.Utilities
{
    public partial class TreeVisualizerForm : Form
    {
        // The root node.
        private TreeNode<CircleNode> root =
            new TreeNode<CircleNode>(new CircleNode("Root"));

        // Make a tree.
        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode<CircleNode> a_node = new TreeNode<CircleNode>(new CircleNode("A"));
            TreeNode<CircleNode> b_node = new TreeNode<CircleNode>(new CircleNode("B"));
            TreeNode<CircleNode> c_node = new TreeNode<CircleNode>(new CircleNode("C"));
            TreeNode<CircleNode> d_node = new TreeNode<CircleNode>(new CircleNode("D"));
            TreeNode<CircleNode> e_node = new TreeNode<CircleNode>(new CircleNode("E"));
            TreeNode<CircleNode> f_node = new TreeNode<CircleNode>(new CircleNode("F"));
            TreeNode<CircleNode> g_node = new TreeNode<CircleNode>(new CircleNode("G"));
            TreeNode<CircleNode> h_node = new TreeNode<CircleNode>(new CircleNode("H"));

            root.AddChild(a_node);
            root.AddChild(b_node);
            a_node.AddChild(c_node);
            a_node.AddChild(d_node);
            b_node.AddChild(e_node);
            b_node.AddChild(f_node);
            b_node.AddChild(g_node);
            e_node.AddChild(h_node);

            // Position the tree.
            root.SetTreeDrawingParameters(5, 10, 20, 5,
             TreeNode<CircleNode>.Orientations.Horizontal);
            ArrangeTree();
        }

        // Draw the tree.
        private void picTree_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            root.DrawTree(e.Graphics);
        }

        // Center the tree on the form.
        private void picTree_Resize(object sender, EventArgs e)
        {
            ArrangeTree();
        }
        private void ArrangeTree()
        {
            using (Graphics gr = picTree.CreateGraphics())
            {
                if (root.Orientation == TreeNode<CircleNode>.Orientations.Horizontal)
                {
                    // Arrange the tree once to see how big it is.
                    float xmin = 0, ymin = 0;
                    root.Arrange(gr, ref xmin, ref ymin);

                    // Arrange the tree again to center it horizontally.
                    xmin = (picTree.ClientSize.Width - xmin) / 2;
                    ymin = 10;
                    root.Arrange(gr, ref xmin, ref ymin);
                }
                else
                {
                    // Arrange the tree.
                    float xmin = root.Indent;//@
                    float ymin = xmin;
                    root.Arrange(gr, ref xmin, ref ymin);
                }
            }

            picTree.Refresh();
        }

        // The currently selected node.
        private TreeNode<CircleNode> SelectedNode;

        // Display the text of the node under the mouse.
        private void picTree_MouseMove(object sender, MouseEventArgs e)
        {
            // Find the node under the mouse.
            FindNodeUnderMouse(e.Location);

            // If there is a node under the mouse,
            // display the node's text.
            if (SelectedNode == null)
            {
                lblNodeText.Text = "";
            }
            else
            {
                lblNodeText.Text = SelectedNode.Data.Text;
            }
        }

        // If this is a right button down and the
        // mouse is over a node, display a context menu.
        private void picTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            // Find the node under the mouse.
            FindNodeUnderMouse(e.Location);

            // If there is a node under the mouse,
            // display the context menu.
            if (SelectedNode != null)
            {
                // Don't let the user delete the root node.
                // (The TreeNode class can't do that.)
                ctxNodeDelete.Enabled = (SelectedNode != root);

                // Display the context menu.
                ctxNode.Show(this, e.Location);
            }
        }

        // Set SelectedNode to the node under the mouse.
        private void FindNodeUnderMouse(PointF pt)
        {
            using (Graphics gr = picTree.CreateGraphics())
            {
                SelectedNode = root.NodeAtPoint(gr, pt);
            }
        }

  

    }
}

