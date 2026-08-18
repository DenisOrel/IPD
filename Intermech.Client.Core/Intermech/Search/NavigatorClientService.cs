
// Type: Intermech.Search.NavigatorClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class NavigatorClientService : INavigatorClientService
{
  private ICurrentUserAndRole _currentUserAndRole;
  private INavGraphicsCache _navGraphicsCache;
  private ICategoryTypeIconService _categoryTypeIconService;
  private IColumnSchemes _columnSchemes;

  public NavigatorClientService(
    ICurrentUserAndRole currentUserAndRole,
    INavGraphicsCache navGraphicsCache,
    ICategoryTypeIconService categoryTypeIconService,
    IColumnSchemes columnSchemes)
  {
    if (currentUserAndRole == null)
      throw new ArgumentNullException(nameof (currentUserAndRole));
    if (navGraphicsCache == null)
      throw new ArgumentNullException(nameof (navGraphicsCache));
    if (categoryTypeIconService == null)
      throw new ArgumentNullException(nameof (categoryTypeIconService));
    if (columnSchemes == null)
      throw new ArgumentNullException(nameof (columnSchemes));
    this._currentUserAndRole = currentUserAndRole;
    this._navGraphicsCache = navGraphicsCache;
    this._categoryTypeIconService = categoryTypeIconService;
    this._columnSchemes = columnSchemes;
  }

  public NodeColumnCollection ChangeColumns(
    NodeColumnCollection columns,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection defaultColumns = null)
  {
    if (columns == null)
      throw new ArgumentNullException(nameof (columns));
    if (supportedColumns == null)
      throw new ArgumentNullException(nameof (supportedColumns));
    NodeColumnCollection columnCollection = (NodeColumnCollection) columns.Clone();
    return AppearanceTuningForm.Execute((INode) new NavigatorClientService.ChangeColumnsNode(defaultColumns ?? Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending)), ContentType.Folders, supportedColumns, columns) == DialogResult.OK ? columns : columnCollection;
  }

  public NavGradientBrush GetCheckedOutBrush(long checkedOutBy, Rectangle rectangle)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(checkedOutBy))
      throw new ArgumentException();
    return checkedOutBy != this._currentUserAndRole.UserID ? this._navGraphicsCache.GetNavGradientBrush(this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode, rectangle, this._navGraphicsCache.CurrentColorsScheme.Gradient.HasFlag((Enum) GradientUsing.CheckedOutOther)) : this._navGraphicsCache.GetNavGradientBrush(this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode, rectangle, this._navGraphicsCache.CurrentColorsScheme.Gradient.HasFlag((Enum) GradientUsing.CheckOut));
  }

  public NavGradientBrush GetCheckedOutBrush(long checkedOutBy)
  {
    return this.GetCheckedOutBrush(checkedOutBy, Rectangle.Empty);
  }

  public object GetCellValue(object attributeValue, NodeColumn column)
  {
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    if (column.Attribute != null)
    {
      AttributeSourceTypes attributeSourceTypes = column.AttrSource;
      if (attributeSourceTypes == AttributeSourceTypes.Auto && AttributeTypeHelper.IsSystemAttributeTypeID(column.Attribute.AttributeID))
        attributeSourceTypes = ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) column.Attribute.AttributeID);
      INodeColumnTransform nodeColumnTransform = (INodeColumnTransform) null;
      switch (attributeSourceTypes)
      {
        case AttributeSourceTypes.Object:
          nodeColumnTransform = !AttributeTypeHelper.IsSystemAttributeTypeID(column.Attribute.AttributeID) ? this._columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) column.Attribute.AttributeID) : this._columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) column.Attribute.AttributeID);
          break;
        case AttributeSourceTypes.Relation:
          nodeColumnTransform = !AttributeTypeHelper.IsSystemAttributeTypeID(column.Attribute.AttributeID) ? this._columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.RelationColumnSchemeGuid, (object) column.Attribute.AttributeID) : this._columnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid, (object) column.Attribute.AttributeID);
          break;
      }
      if (nodeColumnTransform != null)
      {
        try
        {
          return nodeColumnTransform.Apply(attributeValue, column, (object) null, new object[0]);
        }
        catch
        {
        }
      }
    }
    return attributeValue;
  }

  public Tuple<ImageList, int> GetObjectTypeIcon(int objectTypeID)
  {
    return new Tuple<ImageList, int>(this._categoryTypeIconService.ImageList, this._categoryTypeIconService.IndexOf(4, objectTypeID));
  }

  public NodeColumn CreateNodeColumn(
    ObligatoryObjectAttributes obligatoryObjectAttribute)
  {
    return this._columnSchemes.CreateColumn(this.GetColumnSchemeGuid(obligatoryObjectAttribute), (object) obligatoryObjectAttribute);
  }

  private Guid GetColumnSchemeGuid(
    ObligatoryObjectAttributes obligatoryObjectAttribute)
  {
    switch (ObligatoryObjectAttributesHelper.GetAttributeSourceType(obligatoryObjectAttribute))
    {
      case AttributeSourceTypes.Object:
        return Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
      case AttributeSourceTypes.Relation:
        return Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid;
      default:
        return Guid.Empty;
    }
  }

  private sealed class ChangeColumnsNode : INode, INodeItems
  {
    private NodeColumnCollection _defaultColumns;

    public ChangeColumnsNode(NodeColumnCollection defaultColumns)
    {
      this._defaultColumns = defaultColumns ?? throw new ArgumentNullException(nameof (defaultColumns));
    }

    public NodeOptions Options
    {
      get => NodeOptions.CanContainsComposition;
      set => throw new NotImplementedException();
    }

    public INodeQuery GetQuery(ContentType content) => throw new NotImplementedException();

    public NodeColumnCollection GetDefaultColumns(ContentType content) => this._defaultColumns;

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
