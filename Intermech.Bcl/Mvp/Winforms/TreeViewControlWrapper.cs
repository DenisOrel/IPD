
// Type: Intermech.Mvp.Winforms.TreeViewControlWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    public class TreeViewControlWrapper : ITreeView
    {
      private readonly TreeView control;
      private readonly Dictionary<string, TreeNode> map;
      private int totalCount;
      private EventHandler selectionChanged;

      public TreeViewControlWrapper(TreeView control)
      {
        this.control = control != null ? control : throw new ArgumentNullException(nameof (control));
        this.control.AfterSelect += new TreeViewEventHandler(this.OnAfterSelect);
        this.map = new Dictionary<string, TreeNode>();
      }

      private void OnAfterSelect(object sender, TreeViewEventArgs e)
      {
        if (this.selectionChanged == null)
          return;
        this.selectionChanged((object) this, EventArgs.Empty);
      }

      public void AddRootNode(string key, string text)
      {
        if (string.IsNullOrEmpty(key))
          throw new ArgumentNullException(nameof (key));
        this.AddInternal(key, text, this.control.Nodes);
      }

      public void AddChildNode(string parent, string key, string text)
      {
        if (string.IsNullOrEmpty(key))
          throw new ArgumentNullException(nameof (key));
        TreeNode node = this.GetNode(parent);
        this.AddInternal(key, text, node.Nodes);
      }

      private void AddInternal(string key, string text, TreeNodeCollection nodes)
      {
        if (this.map.TryGetValue(key, out TreeNode _))
          throw new InvalidOperationException();
        TreeNode treeNode = nodes.Add(text);
        treeNode.Tag = (object) key;
        this.map.Add(key, treeNode);
        ++this.totalCount;
      }

      public void ClearNodes()
      {
        this.control.Nodes.Clear();
        this.map.Clear();
        --this.totalCount;
      }

      public bool ContainsNode(string key)
      {
        return !string.IsNullOrEmpty(key) ? this.map.ContainsKey(key) : throw new ArgumentNullException(nameof (key));
      }

      public bool IsNodeExpanded(string key) => this.GetNode(key).IsExpanded;

      public void ExpandNode(string key, bool expanded)
      {
        TreeNode node = this.GetNode(key);
        if (node.IsExpanded == expanded)
          return;
        if (expanded)
          node.Expand();
        else
          node.Collapse();
      }

      public string GetSelectedNode()
      {
        return this.control.SelectedNode == null ? (string) null : (string) this.control.SelectedNode.Tag;
      }

      public void SelectNode(string key)
      {
        if (string.IsNullOrEmpty(key))
        {
          this.control.SelectedNode = (TreeNode) null;
          if (this.selectionChanged == null)
            return;
          this.selectionChanged((object) this, EventArgs.Empty);
        }
        else
          this.control.SelectedNode = this.GetNode(key);
      }

      public string GetTopVisibleNode()
      {
        return this.control.TopNode == null ? (string) null : (string) this.control.TopNode.Tag;
      }

      public void SetTopVisibleNode(string key)
      {
        this.control.TopNode = this.GetNode(key);
        this.control.TopNode.EnsureVisible();
      }

      public bool IsNodeVisible(string key) => this.GetNode(key).IsVisible;

      public event EventHandler SelectionChanged
      {
        add => this.selectionChanged += value;
        remove => this.selectionChanged -= value;
      }

      private TreeNode GetNode(string key)
      {
        if (string.IsNullOrEmpty(key))
          throw new ArgumentNullException(nameof (key));
        TreeNode node;
        if (!this.map.TryGetValue(key, out node))
          throw new InvalidOperationException();
        return node;
      }
    }
}
