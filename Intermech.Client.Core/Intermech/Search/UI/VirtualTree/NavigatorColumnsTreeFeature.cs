
// Type: Intermech.Search.UI.VirtualTree.NavigatorColumnsTreeFeature
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Search.UI.VirtualTree;

public sealed class NavigatorColumnsTreeFeature : ITreeFeature
{
  private Intermech.Search.UI.VirtualTree.VirtualTree _tree;
  private NavigatorColumnsKey _navigatorColumnsKey;
  private LazyService<INavigatorColumnsService> _navigatorColumnsService = new LazyService<INavigatorColumnsService>();

  public NavigatorColumnsTreeFeature(Intermech.Search.UI.VirtualTree.VirtualTree tree)
  {
    this._tree = tree != null ? tree : throw new ArgumentNullException(nameof (tree));
  }

  public NavigatorColumnsKey NavigatorColumnsKey
  {
    get => this._navigatorColumnsKey;
    set
    {
      if (this._navigatorColumnsKey == value)
        return;
      if (this._navigatorColumnsKey != null)
        this._tree.Columns.ListChanged -= new ListChangedEventHandler(this.TreeColumns_ListChanged);
      this._navigatorColumnsKey = value;
      if (this._navigatorColumnsKey == null)
        return;
      this._tree.Columns.ListChanged += new ListChangedEventHandler(this.TreeColumns_ListChanged);
    }
  }

  public void GetAndApplyNavigatorColumnsFromNavigatorColumnsService(bool useInheritance)
  {
    if (this._navigatorColumnsKey == null)
      throw new InvalidOperationException();
    NavigatorColumns navigatorColumns = this._navigatorColumnsService.Value.GetNavigatorColumns(this._navigatorColumnsKey.Category, this._navigatorColumnsKey.Type, this._navigatorColumnsKey.Suffix, useInheritance);
    if (navigatorColumns == null || navigatorColumns.Columns == null)
      return;
    this._tree.Columns.ListChanged -= new ListChangedEventHandler(this.TreeColumns_ListChanged);
    foreach (Column column in this._tree.Columns)
      column.Changed -= new EventHandler(this.Column_Changed);
    try
    {
      this.SetNodeColumnCollection(navigatorColumns.Columns);
    }
    finally
    {
      this._tree.Columns.ListChanged += new ListChangedEventHandler(this.TreeColumns_ListChanged);
      foreach (Column column in this._tree.Columns)
        column.Changed += new EventHandler(this.Column_Changed);
    }
  }

  public void SetNavigatorColumnsToNavigatorColumnsService()
  {
    this._navigatorColumnsService.Value.CreateNavigatorColumns(new NavigatorColumns(this._navigatorColumnsKey.Category, this._navigatorColumnsKey.Type, this._navigatorColumnsKey.Suffix)
    {
      Columns = this.GetNodeColumnCollection()
    });
  }

  public NodeColumnCollection GetNodeColumnCollection()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    foreach (Column column in this._tree.Columns)
    {
      if (column is ExtendedColumn && ((ColumnBase) column).Tag is NodeColumn tag)
        columnCollection.Add(tag);
    }
    return columnCollection;
  }

  public void SetNodeColumnCollection(NodeColumnCollection nodeColumnCollection)
  {
    if (nodeColumnCollection == null)
      throw new ArgumentNullException(nameof (nodeColumnCollection));
    if (this._navigatorColumnsKey != null)
    {
      this._tree.Columns.ListChanged -= new ListChangedEventHandler(this.TreeColumns_ListChanged);
      foreach (Column column in this._tree.Columns)
        column.Changed -= new EventHandler(this.Column_Changed);
    }
    try
    {
      this._tree.Columns.Clear();
      foreach (NodeColumn nodeColumn in (List<NodeColumn>) nodeColumnCollection)
      {
        IMSAttributeType attribute = nodeColumn.Attribute;
        if (attribute != null)
        {
          ExtendedColumn extendedColumn = new ExtendedColumn();
          extendedColumn.Caption = attribute.Name;
          extendedColumn.DataField = attribute.AttributeID.ToString();
          extendedColumn.Tag = (object) nodeColumn;
          extendedColumn.Width = nodeColumn.Width;
          this._tree.Columns.Add((Column) extendedColumn);
        }
      }
    }
    finally
    {
      if (this._navigatorColumnsKey != null)
      {
        this._tree.Columns.ListChanged += new ListChangedEventHandler(this.TreeColumns_ListChanged);
        foreach (Column column in this._tree.Columns)
          column.Changed += new EventHandler(this.Column_Changed);
        this.SetNavigatorColumnsToNavigatorColumnsService();
      }
    }
  }

  public void ShowColumnCustomizeDialog(NodeColumnCollection supportedColumns)
  {
    NodeColumnCollection columnCollection = this.GetNodeColumnCollection();
    if (AppearanceTuningForm.Execute((INode) new NavigatorColumnsTreeFeature.AppearanceTuningFormFakeNode(), ContentType.Folders, supportedColumns, columnCollection) != DialogResult.OK)
      return;
    this.SetNodeColumnCollection(columnCollection);
    this._tree.UpdateRows(true);
  }

  private void TreeColumns_ListChanged(object sender, ListChangedEventArgs e)
  {
    this.SetNavigatorColumnsToNavigatorColumnsService();
  }

  private void Column_Changed(object sender, EventArgs e)
  {
    foreach (Column column in this._tree.Columns)
    {
      if (column is ExtendedColumn && ((ColumnBase) column).Tag is NodeColumn tag)
        tag.Width = column.Width;
    }
    this.SetNavigatorColumnsToNavigatorColumnsService();
  }

  private sealed class AppearanceTuningFormFakeNode : INode, INodeItems
  {
    public NodeOptions Options
    {
      get => NodeOptions.CanContainsComposition;
      set => throw new NotImplementedException();
    }

    public INodeQuery GetQuery(ContentType content) => throw new NotImplementedException();

    public NodeColumnCollection GetDefaultColumns(ContentType content)
    {
      return Utils.DefaultColumnsObjects();
    }

    public NodeColumnCollection GetSupportedColumns(ContentType content, string ColumnSetName)
    {
      throw new NotImplementedException();
    }

    public List<string> GetSupportedColumnSetNames() => throw new NotImplementedException();

    public void Refresh() => throw new NotImplementedException();

    public ContentAttributes GetAttributesOf(INodeID nodeID) => throw new NotImplementedException();

    public INode GetChild(INodeID nodeID) => throw new NotImplementedException();

    public string GetAddress(INodeID nodeID) => throw new NotImplementedException();

    public INodeID ParseAddress(string address) => throw new NotImplementedException();

    public PersistentState Serialize(INodeID nodeID) => throw new NotImplementedException();

    public INodeID Deserialize(PersistentState persistNodeID)
    {
      throw new NotImplementedException();
    }

    public object GetData(INodeID nodeID, System.Type dataFormat)
    {
      throw new NotImplementedException();
    }

    public object[] GetData(NodeIDCollection nodeIDs, System.Type dataFormat)
    {
      throw new NotImplementedException();
    }

    public IUpdateAnalyser GetAnalyser(
      NodeViewCapabilities capabilities,
      object sender,
      NotificationEventArgs e)
    {
      throw new NotImplementedException();
    }

    public object GetService(System.Type service) => throw new NotImplementedException();
  }
}
