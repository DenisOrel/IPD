
// Type: Intermech.Client.Core.TreeViewNavigator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Класс для выполнения навигации в пределах дерева</summary>
public class TreeViewNavigator : INavigate, IDisposable
{
  /// <summary>Дерево, с которым связан класс</summary>
  private TreeView _treeView;
  /// <summary>Выполняется ли слежение</summary>
  private bool _tracking;
  /// <summary>Позиция</summary>
  private int _position;
  /// <summary>История позиций (в виде списка TreeNode)</summary>
  private ArrayList _history;
  /// <summary>Ссылка на адресную службу</summary>
  public IAddressService AddressService;

  public TreeViewNavigator(TreeView treeView)
  {
    this._treeView = treeView;
    this._position = 0;
    this._tracking = true;
    this._history = new ArrayList(32 /*0x20*/);
    this._treeView.AfterSelect += new TreeViewEventHandler(this._treeView_AfterSelect);
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public event EventHandler Changed;

  public void Back() => this.Back(1);

  public void Back(int steps)
  {
    for (int index = this._position - steps - 1; index >= 0; --index)
    {
      if (this._history[index] != null && ((TreeNode) this._history[index]).TreeView != null)
      {
        this._tracking = false;
        this._treeView.SelectedNode = (TreeNode) this._history[index];
        this._tracking = true;
        this._position = index + 1;
        this.OnChanged();
        break;
      }
    }
  }

  public void Forward() => this.Forward(1);

  public void Forward(int steps)
  {
    for (int index = this._position + steps - 1; index < this._history.Count; ++index)
    {
      if (this._history[index] != null && ((TreeNode) this._history[index]).TreeView != null)
      {
        this._tracking = false;
        this._treeView.SelectedNode = (TreeNode) this._history[index];
        this._tracking = true;
        this._position = index + 1;
        this.OnChanged();
        break;
      }
    }
  }

  public bool CanBack => this._position > 1;

  public bool CanForward
  {
    get => this._position < this._history.Count && this._history[this._position] != null;
  }

  public string BackName
  {
    get
    {
      if (!this.CanBack)
        return LocalizationHolder.rm.GetString("Client.Core_583");
      TreeNode treeNode = (TreeNode) this._history[this._position - 2];
      return treeNode != null && treeNode.TreeView != null ? LocalizationHolder.rm.GetString("Client.Core_985") + treeNode.FullPath : LocalizationHolder.rm.GetString("Client.Core_583");
    }
  }

  public string ForwardName
  {
    get
    {
      if (!this.CanForward)
        return LocalizationHolder.rm.GetString("Client.Core_986");
      TreeNode treeNode = (TreeNode) this._history[this._position];
      return treeNode != null && treeNode.TreeView != null ? LocalizationHolder.rm.GetString("Client.Core_987") + treeNode.FullPath : LocalizationHolder.rm.GetString("Client.Core_986");
    }
  }

  public string[] BackNames
  {
    get
    {
      if (!this.CanBack)
        return (string[]) null;
      int length = this._position - 1;
      string[] backNames = new string[length];
      int num = 0;
      for (; length > 0; --length)
        backNames[num++] = ((TreeNode) this._history[length - 1]).FullPath;
      return backNames;
    }
  }

  public string[] ForwardNames
  {
    get
    {
      if (!this.CanForward)
        return (string[]) null;
      int length = this._history.Count - this._position;
      string[] forwardNames = new string[length];
      int num = 0;
      int position = this._position;
      for (; length > 0; --length)
      {
        TreeNode treeNode = (TreeNode) this._history[position++];
        if (treeNode != null)
          forwardNames[num++] = treeNode.FullPath;
        else
          break;
      }
      return forwardNames;
    }
  }

  /// <summary>Найти узел с указанным адресом (рекурсивный метод)</summary>
  /// <param name="parentNode">Родительский узел (или null)</param>
  /// <param name="address">Адрес</param>
  /// <returns>Узел с указанным адресом или null</returns>
  protected TreeNode FindNode(TreeNode parentNode, string address)
  {
    if (this.AddressService == null || this._treeView == null || address == string.Empty)
      return (TreeNode) null;
    parentNode?.Expand();
    TreeNodeCollection treeNodeCollection = parentNode != null ? parentNode.Nodes : this._treeView.Nodes;
    string[] separator = new string[1]
    {
      this._treeView.PathSeparator
    };
    string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    string str = strArray == null || strArray.Length == 0 ? string.Empty : strArray[0];
    if (str == string.Empty)
      return (TreeNode) null;
    address = address.Length > str.Length ? address.Substring(str.Length + this._treeView.PathSeparator.Length, address.Length - str.Length - this._treeView.PathSeparator.Length) : string.Empty;
    TreeNode parentNode1 = (TreeNode) null;
    for (int index = 0; index < treeNodeCollection.Count; ++index)
    {
      if (treeNodeCollection[index].Text.ToLowerInvariant() == str.ToLowerInvariant())
      {
        parentNode1 = treeNodeCollection[index];
        break;
      }
    }
    if (parentNode1 == null)
      return (TreeNode) null;
    return parentNode1 != null && address == string.Empty ? parentNode1 : this.FindNode(parentNode1, address) ?? parentNode1;
  }

  /// <summary>Выполнить переход по адресу из адресной службы</summary>
  public void BrowseAddress()
  {
    if (this.AddressService == null || this._treeView == null || !(this.AddressService.Text != string.Empty))
      return;
    TreeNode node = this.FindNode((TreeNode) null, this.AddressService.Text);
    if (node == null)
      return;
    this._tracking = false;
    this._treeView.SelectedNode = node;
    this._tracking = true;
    this.Select(node);
  }

  /// <summary>Задать адрес в адресной строке</summary>
  /// <param name="address"></param>
  public void UpdateAddress(string address)
  {
    if (this.AddressService == null)
      return;
    this.AddressService.Text = address;
  }

  private void Select(TreeNode node)
  {
    if (node == null || !this._tracking || this._position > 0 && this._history[this._position - 1] == node)
      return;
    while (this._position >= this._history.Count)
      this._history.Add((object) null);
    this._history[this._position] = (object) node;
    ++this._position;
    for (int position = this._position; position < this._history.Count; ++position)
      this._history[position] = (object) null;
    this.OnChanged();
    this.UpdateAddress(node.FullPath);
  }

  private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (!this._tracking || e.Action == TreeViewAction.ByKeyboard)
      return;
    this.Select(e.Node);
  }

  public void Dispose()
  {
    this._treeView.AfterSelect -= new TreeViewEventHandler(this._treeView_AfterSelect);
    this._history.Clear();
    this._history = (ArrayList) null;
  }
}
