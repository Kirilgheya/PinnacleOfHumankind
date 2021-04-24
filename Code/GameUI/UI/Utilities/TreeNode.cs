using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI.UI.Utilities
{
    class TreeNode<T> where T : IDrawable
    {
        // The data.
        public T Data;

        // Child nodes in the tree.
        public List<TreeNode<T>> Children = new List<TreeNode<T>>();

        // Drawing parameters.
        // Space to skip horizontally between siblings
        // and vertically between generations.
        public float HOffset = 5;
        public float VOffset = 10;

        // Spacing for verticaly orientation.
        public float Indent = 20;
        public float SpotRadius = 5;

        public enum Orientations
        {
            Horizontal,
            Vertical
        }
        public Orientations Orientation = Orientations.Horizontal;

        // The node's center after arranging.
        private PointF DataCenter;

        // The spot where we draw the node's circle for vertical orientation.
        private PointF SpotCenter;

        // Drawing properties.
        public Font MyFont = null;
        public Pen MyPen = Pens.Black;
        public Brush FontBrush = Brushes.Black;
        public Brush BgBrush = Brushes.White;

        // Recursively set the subtree's orientation.
        public void SetTreeDrawingParameters(float h_offset, float v_offset, float indent, float node_radius, Orientations orientation)
        {
            HOffset = h_offset;
            VOffset = v_offset;
            Indent = indent;
            SpotRadius = node_radius;
            Orientation = orientation;

            // Recursively sedt the properties for the subtree.
            foreach (TreeNode<T> child in Children)
                child.SetTreeDrawingParameters(h_offset, v_offset,
                    indent, node_radius, orientation);
        }

        // Constructor.
        public TreeNode(T new_data)
            : this(new_data, new Font("Times New Roman", 12))
        {
            Data = new_data;
        }
        public TreeNode(T new_data, Font fg_font)
        {
            Data = new_data;
            MyFont = fg_font;
        }

        // Add a TreeNode to out Children list.
        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }

        // Arrange the node and its children in the allowed area.
        // Set xmin to indicate the right edge of our subtree.
        // Set ymin to indicate the bottom edge of our subtree.
        public void Arrange(Graphics gr, ref float xmin, ref float ymin)
        {
            if (Orientation == TreeNode<T>.Orientations.Horizontal)
            {
                ArrangeHorizontally(gr, ref xmin, ref ymin);
            }
            else
            {
                ArrangeVertically(gr, xmin, ref ymin);
            }
        }

        // Arrange the subtree horizontally.
        public void ArrangeHorizontally(Graphics gr, ref float xmin, ref float ymin)
        {
            // See how big this node is.
            SizeF my_size = Data.GetSize(gr, MyFont);

            // Recursively arrange our children,
            // allowing room for this node.
            float x = xmin;
            float biggest_ymin = ymin + my_size.Height;
            float subtree_ymin = ymin + my_size.Height + VOffset;
            foreach (TreeNode<T> child in Children)
            {
                // Arrange this child's subtree.
                float child_ymin = subtree_ymin;
                child.Arrange(gr, ref x, ref child_ymin);

                // See if this increases the biggest ymin value.
                if (biggest_ymin < child_ymin) biggest_ymin = child_ymin;

                // Allow room before the next sibling.
                x += HOffset;
            }

            // Remove the spacing after the last child.
            if (Children.Count > 0) x -= HOffset;

            // See if this node is wider than the subtree under it.
            float subtree_width = x - xmin;
            if (my_size.Width > subtree_width)
            {
                // Center the subtree under this node.
                // Make the children rearrange themselves
                // moved to center their subtrees.
                x = xmin + (my_size.Width - subtree_width) / 2;
                foreach (TreeNode<T> child in Children)
                {
                    // Arrange this child's subtree.
                    child.Arrange(gr, ref x, ref subtree_ymin);

                    // Allow room before the next sibling.
                    x += HOffset;
                }

                // The subtree's width is this node's width.
                subtree_width = my_size.Width;
            }

            // Set this node's center position.
            DataCenter = new PointF(
                xmin + subtree_width / 2,
                ymin + my_size.Height / 2);

            // Increase xmin to allow room for
            // the subtree before returning.
            xmin += subtree_width;

            // Set the return value for ymin.
            ymin = biggest_ymin;
        }

        // Arrange the subtree vertically.
        public void ArrangeVertically(Graphics gr, float xmin, ref float ymin)
        {
            // See how big this node is.
            SizeF my_size = Data.GetSize(gr, MyFont);
            my_size.Width += 3 * SpotRadius;

            // Set the position of this node's spot.
            SpotCenter = new PointF(
                xmin + SpotRadius,
                ymin + (my_size.Height - 2 * SpotRadius) / 2);

            // Set the position of this node's data.
            DataCenter = new PointF(
                SpotCenter.X + SpotRadius + my_size.Width / 2,
                SpotCenter.Y);

            // Allow vertical room for this node.
            ymin += my_size.Height + VOffset;

            // Recursively arrange our children.
            foreach (TreeNode<T> child in Children)
            {
                // Arrange this child's subtree.
                child.ArrangeVertically(gr, xmin + Indent, ref ymin);
            }
        }

        // Draw the subtree rooted at this node
        // with the given upper left corner.
        public void DrawTree(Graphics gr, ref float x, float y)
        {
            // Arrange the tree.
            Arrange(gr, ref x, ref y);

            // Draw the tree.
            DrawTree(gr);
        }

        // Draw the subtree rooted at this node.
        public void DrawTree(Graphics gr)
        {
            // Draw the links.
            DrawSubtreeLinks(gr);

            // Draw the nodes.
            DrawSubtreeNodes(gr);
        }

        // Draw the links for the subtree rooted at this node.
        private void DrawSubtreeLinks(Graphics gr)
        {
            if (Orientation == TreeNode<T>.Orientations.Horizontal)
            {
                DrawSubtreeLinksHorizontal(gr);
            }
            else
            {
                DrawSubtreeLinksVertical(gr);
            }
        }

        // Draw the links for the horizontal subtree rooted at this node.
        private void DrawSubtreeLinksHorizontal(Graphics gr)
        {
            foreach (TreeNode<T> child in Children)
            {
                // Draw the link between this node this child.
                gr.DrawLine(MyPen, DataCenter, child.DataCenter);

                // Recursively make the child draw its subtree nodes.
                child.DrawSubtreeLinksHorizontal(gr);
            }
        }

        // Draw the links for the subtree rooted at this node.
        private void DrawSubtreeLinksVertical(Graphics gr)
        {
            foreach (TreeNode<T> child in Children)
            {
                // Draw the link between this node this child.
                gr.DrawLine(MyPen, SpotCenter.X, SpotCenter.Y, SpotCenter.X, child.SpotCenter.Y);
                gr.DrawLine(MyPen, SpotCenter.X, child.SpotCenter.Y, child.SpotCenter.X, child.SpotCenter.Y);

                // Recursively make the child draw its subtree nodes.
                child.DrawSubtreeLinksVertical(gr);
            }
        }

        // Draw the nodes for the subtree rooted at this node.
        private void DrawSubtreeNodes(Graphics gr)
        {
            // Draw this node.
            Data.Draw(DataCenter.X, DataCenter.Y, gr, MyPen, BgBrush, FontBrush, MyFont);

            // If oriented vertically, draw the node's spot.
            if (Orientation == TreeNode<T>.Orientations.Vertical)
            {
                RectangleF rect = new RectangleF(
                    SpotCenter.X - SpotRadius, SpotCenter.Y - SpotRadius,
                    2 * SpotRadius, 2 * SpotRadius);
                if (Children.Count > 0)
                {
                    gr.FillEllipse(Brushes.LightBlue, rect);
                }
                else
                {
                    gr.FillEllipse(Brushes.Orange, rect);
                }
                gr.DrawEllipse(MyPen, rect);
            }

            // Recursively make the child draw its subtree nodes.
            foreach (TreeNode<T> child in Children)
            {
                child.DrawSubtreeNodes(gr);
            }
        }

        // Return the TreeNode at this point (or null if there isn't one there).
        public TreeNode<T> NodeAtPoint(Graphics gr, PointF target_pt)
        {
            // See if the point is under this node.
            if (Data.IsAtPoint(gr, MyFont, DataCenter, target_pt)) return this;

            // See if the point is under a node in the subtree.
            foreach (TreeNode<T> child in Children)
            {
                TreeNode<T> hit_node = child.NodeAtPoint(gr, target_pt);
                if (hit_node != null) return hit_node;
            }

            return null;
        }

        // Delete a target node from this node's subtree.
        // Return true if we delete the node.
        public bool DeleteNode(TreeNode<T> target)
        {
            // See if the target is in our subtree.
            foreach (TreeNode<T> child in Children)
            {
                // See if it's the child.
                if (child == target)
                {
                    // Delete this child.
                    Children.Remove(child);
                    return true;
                }

                // See if it's in the child's subtree.
                if (child.DeleteNode(target)) return true;
            }

            // It's not in our subtree.
            return false;
        }
    }
}
