// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeCollection`1
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

[Editor("TreeNodeCollectionEditor", typeof (UITypeEditor))]
public class TreeNodeCollection<TTreeNode> : 
  IList,
  ICollection,
  IEnumerable,
  IList<TTreeNode>,
  ICollection<TTreeNode>,
  IEnumerable<TTreeNode>,
  IReadOnlyCollection<TTreeNode>
  where TTreeNode : TreeNode
{
  [NotNull]
  private readonly TreeNodeCollection _treeNodeCollection;

  public TreeNodeCollection([NotNull] TreeNodeCollection treeNodeCollection)
  {
    this._treeNodeCollection = treeNodeCollection;
  }

  IEnumerator<TTreeNode> IEnumerable<TTreeNode>.GetEnumerator()
  {
    return this._treeNodeCollection.Cast<TTreeNode>().GetEnumerator();
  }

  public IEnumerator GetEnumerator() => this._treeNodeCollection.GetEnumerator();

  public int Count => this._treeNodeCollection.Count;

  public object SyncRoot => ((ICollection) this._treeNodeCollection).SyncRoot;

  public bool IsSynchronized => ((ICollection) this._treeNodeCollection).IsSynchronized;

  public virtual void Clear() => this._treeNodeCollection.Clear();

  public virtual void RemoveAt(int index) => this._treeNodeCollection.RemoveAt(index);

  public virtual void RemoveByKey([NotNull] string key)
  {
    this._treeNodeCollection.RemoveByKey(key);
  }

  public bool IsReadOnly => this._treeNodeCollection.IsReadOnly;

  public bool IsFixedSize => ((IList) this._treeNodeCollection).IsFixedSize;

  public bool Remove([NotNull] TTreeNode item)
  {
    if (!this._treeNodeCollection.Contains((TreeNode) item))
      return false;
    this._treeNodeCollection.Remove((TreeNode) item);
    return true;
  }

  void IList.Remove([CanBeNull] object value) => ((IList) this._treeNodeCollection).Remove(value);

  public virtual int Add([NotNull] TTreeNode item) => this._treeNodeCollection.Add((TreeNode) item);

  [NotNull]
  public virtual TTreeNode Add([NotNull] string text)
  {
    return (TTreeNode) this._treeNodeCollection.Add(text);
  }

  [NotNull]
  public virtual TTreeNode Add([NotNull] string key, [NotNull] string text, int imageIndex, int selectedImageIndex)
  {
    return (TTreeNode) this._treeNodeCollection.Add(key, text, imageIndex, selectedImageIndex);
  }

  [NotNull]
  public virtual TTreeNode Add([NotNull] string key, [NotNull] string text)
  {
    return (TTreeNode) this._treeNodeCollection.Add(key, text);
  }

  [NotNull]
  public virtual TTreeNode Add([NotNull] string key, [NotNull] string text, [NotNull] string imageKey)
  {
    return (TTreeNode) this._treeNodeCollection.Add(key, text, imageKey);
  }

  [NotNull]
  public virtual TTreeNode Add([NotNull] string key, [NotNull] string text, int imageIndex)
  {
    return (TTreeNode) this._treeNodeCollection.Add(key, text, imageIndex);
  }

  [NotNull]
  public virtual TTreeNode Add([NotNull] string key, [NotNull] string text, [NotNull] string imageKey, [NotNull] string selectedImageKey)
  {
    return (TTreeNode) this._treeNodeCollection.Add(key, text, imageKey, selectedImageKey);
  }

  void ICollection<TTreeNode>.Add([NotNull] TTreeNode item)
  {
    this._treeNodeCollection.Add((TreeNode) item);
  }

  int IList.Add([NotNull] object value) => ((IList) this._treeNodeCollection).Add(value);

  public bool Contains([NotNull] TTreeNode item)
  {
    return this._treeNodeCollection.Contains((TreeNode) item);
  }

  bool IList.Contains([NotNull] object value) => ((IList) this._treeNodeCollection).Contains(value);

  public virtual bool ContainsKey([NotNull] string key)
  {
    return this._treeNodeCollection.ContainsKey(key);
  }

  public void CopyTo(TTreeNode[] array, int arrayIndex)
  {
    this._treeNodeCollection.CopyTo((Array) array, arrayIndex);
  }

  public void CopyTo(Array array, int index) => this._treeNodeCollection.CopyTo(array, index);

  [NotNull]
  [ItemNotNull]
  public TTreeNode[] Find([NotNull] string key, bool searchAllChildren)
  {
    TreeNode[] source = this._treeNodeCollection.Find(key, searchAllChildren);
    return source.Cast<TTreeNode>().ToArray<TTreeNode>(source.Length);
  }

  public int IndexOf([NotNull] TTreeNode item) => this._treeNodeCollection.IndexOf((TreeNode) item);

  int IList.IndexOf([NotNull] object value) => ((IList) this._treeNodeCollection).IndexOf(value);

  public virtual int IndexOfKey([NotNull] string key) => this._treeNodeCollection.IndexOfKey(key);

  public virtual void Insert(int index, [NotNull] TTreeNode item)
  {
    this._treeNodeCollection.Insert(index, (TreeNode) item);
  }

  [NotNull]
  public virtual TTreeNode Insert(int index, [NotNull] string key, [NotNull] string text, [NotNull] string imageKey)
  {
    return (TTreeNode) this._treeNodeCollection.Insert(index, key, text, imageKey);
  }

  [NotNull]
  public virtual TTreeNode Insert(
    int index,
    [NotNull] string key,
    [NotNull] string text,
    int imageIndex,
    int selectedImageIndex)
  {
    return (TTreeNode) this._treeNodeCollection.Insert(index, key, text, imageIndex, selectedImageIndex);
  }

  [NotNull]
  public virtual TTreeNode Insert(int index, [NotNull] string key, [NotNull] string text, int imageIndex)
  {
    return (TTreeNode) this._treeNodeCollection.Insert(index, key, text, imageIndex);
  }

  [NotNull]
  public virtual TTreeNode Insert(int index, [NotNull] string key, [NotNull] string text)
  {
    return (TTreeNode) this._treeNodeCollection.Insert(index, key, text);
  }

  [NotNull]
  public virtual TTreeNode Insert(
    int index,
    [NotNull] string key,
    [NotNull] string text,
    [NotNull] string imageKey,
    [NotNull] string selectedImageKey)
  {
    return (TTreeNode) this._treeNodeCollection.Insert(index, key, text, imageKey, selectedImageKey);
  }

  [NotNull]
  public virtual TTreeNode Insert(int index, [NotNull] string text)
  {
    return (TTreeNode) this._treeNodeCollection.Insert(index, text);
  }

  void IList.Insert(int index, [NotNull] object value)
  {
    ((IList) this._treeNodeCollection).Insert(index, value);
  }

  [NotNull]
  public virtual TTreeNode this[int index]
  {
    get => Intermech.Diagnostics.Check.Result.Is<TTreeNode>((object) this._treeNodeCollection[index]);
    set => this._treeNodeCollection[index] = (TreeNode) value;
  }

  [NotNull]
  object IList.this[int index]
  {
    get => ((IList) this._treeNodeCollection)[index];
    set => ((IList) this._treeNodeCollection)[index] = value;
  }

  [NotNull]
  public virtual TreeNode this[[NotNull] string key] => this._treeNodeCollection[key];
}
