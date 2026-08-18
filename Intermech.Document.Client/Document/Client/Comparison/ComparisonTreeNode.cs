// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Comparison.ComparisonTreeNode
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Comparison;

internal class ComparisonTreeNode : TreeNode, IEquatable<ComparisonTreeNode>
{
  private readonly DocumentTreeNode _node;
  private ComparisonTreeNode _parent;
  private List<ComparisonVerdict> _childVerdicts;

  internal ComparisonTreeNode(DocumentTreeNode node, ComparisonVerdict verdict = ComparisonVerdict.Identical)
  {
    this.Tag = (object) node;
    this._node = node;
    this.Verdict = verdict;
    string str;
    if (node == null)
      str = "Найденные различия в документах";
    else
      str = $"{this.TypeCaption} [{this.Id}] ({EnumDescConverter.GetEnumDescription((Enum) verdict)})".Replace("()", "");
    this.Text = str;
    if (this._node == null || verdict == ComparisonVerdict.Identical)
      return;
    this._node.HighlightColor = Color.Yellow;
  }

  public string Id => this._node?.Id ?? string.Empty;

  public string ClassName => this._node?.NodeClass ?? "ImDocument";

  public string TypeCaption
  {
    get
    {
      return this._node is TableData node && node.IsRow ? "Строка" : this._node?.NodeTypeCaption ?? "ImDocument";
    }
  }

  public ComparisonVerdict Verdict { get; }

  public DocumentTreeNode DocNode => this._node;

  public ComparisonTreeNode Parent
  {
    get
    {
      if (this._parent == (ComparisonTreeNode) null && this._node?.Parent != null)
      {
        this._parent = new ComparisonTreeNode(this._node.Parent);
        this._parent.Nodes.Add((TreeNode) this);
      }
      return this._parent;
    }
    set
    {
      this._parent?.Nodes.Remove((TreeNode) this);
      this._parent = value;
      value?.Nodes.Add((TreeNode) this);
    }
  }

  internal ComparisonTreeNode SuperParent
  {
    get
    {
      ComparisonTreeNode parent = this.Parent;
      if (parent == (ComparisonTreeNode) null)
        return this;
      while (parent.Parent != (ComparisonTreeNode) null)
        parent = parent.Parent;
      return parent;
    }
  }

  internal ComparisonTreeNode FindParentInTree(ComparisonTreeNode treeRoot)
  {
    if (this.Parent == (ComparisonTreeNode) null || treeRoot == (ComparisonTreeNode) null)
      return (ComparisonTreeNode) null;
    if (treeRoot == this.Parent)
      return treeRoot;
    ComparisonTreeNode parentInTree = (ComparisonTreeNode) null;
    foreach (object node in treeRoot.Nodes)
    {
      parentInTree = this.FindParentInTree(node as ComparisonTreeNode);
      if (parentInTree != (ComparisonTreeNode) null)
        break;
    }
    return parentInTree;
  }

  public int CompareTo(object obj)
  {
    if (!(obj is ComparisonTreeNode comparisonTreeNode))
      return -1;
    return !(this.Id == comparisonTreeNode.Id) || !(this.ClassName == comparisonTreeNode.ClassName) || !((this.Parent?.Id ?? "") == (comparisonTreeNode.Parent?.Id ?? "")) || !((this.Parent?.ClassName ?? "") == (comparisonTreeNode.Parent?.ClassName ?? "")) ? 1 : 0;
  }

  public override bool Equals(object obj) => this.Equals(obj as ComparisonTreeNode);

  public bool Equals(ComparisonTreeNode other)
  {
    return !(other == (ComparisonTreeNode) null) && this.Id == other.Id && this.ClassName == other.ClassName && (this.Parent?.Id ?? "") == (other.Parent?.Id ?? "") && (this.Parent?.ClassName ?? "") == (other.Parent?.ClassName ?? "");
  }

  public static bool operator ==(ComparisonTreeNode obj1, ComparisonTreeNode obj2)
  {
    if ((object) obj1 == (object) obj2)
      return true;
    return (object) obj1 != null && (object) obj2 != null && obj1.Equals(obj2);
  }

  public static bool operator !=(ComparisonTreeNode obj1, ComparisonTreeNode obj2)
  {
    return !(obj1 == obj2);
  }

  /// <summary>Получить список типов различий у всех дочерних узлов</summary>
  internal List<ComparisonVerdict> GetChildNodeVerdicts(List<ComparisonVerdict> result = null)
  {
    bool flag = false;
    if (result == null)
    {
      flag = true;
      result = new List<ComparisonVerdict>();
    }
    if (this._childVerdicts == null)
    {
      this._childVerdicts = new List<ComparisonVerdict>();
      foreach (ComparisonTreeNode node in this.Nodes)
      {
        if (node.Verdict != ComparisonVerdict.Identical)
          this._childVerdicts.Add(node.Verdict);
        this._childVerdicts = node.GetChildNodeVerdicts(this._childVerdicts);
      }
      this._childVerdicts = this._childVerdicts.Distinct<ComparisonVerdict>().ToList<ComparisonVerdict>();
    }
    result.AddRange((IEnumerable<ComparisonVerdict>) this._childVerdicts);
    return !flag ? result : result.Distinct<ComparisonVerdict>().ToList<ComparisonVerdict>();
  }
}
