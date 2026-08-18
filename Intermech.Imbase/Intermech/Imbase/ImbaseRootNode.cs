// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseRootNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Cache;
using Intermech.Imbase.QuickSearch;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Interfaces.QuickSearch;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseRootNode : CompositeNode, IContextAware, IQuickSearch
{
  private static DescriptorCollection _catalogTypes;
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();
  private List<long> _catalogsIDs;
  private BaseQuickSearchProvider _qsProvider;

  public List<long> CatalogIDs => this._catalogsIDs;

  public BaseQuickSearchProvider QuickSearchProvider => this._qsProvider;

  public ImbaseRootNode() => this.BuildFolders();

  public ImbaseRootNode(List<long> catalogsIDs = null)
  {
    this._catalogsIDs = catalogsIDs;
    this._qsProvider = (BaseQuickSearchProvider) new ImbaseQuickSearchProvider();
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    INodePart part;
    if (this._catalogsIDs != null && this._catalogsIDs.Count > 0)
    {
      ConditionStructure condition = new ConditionStructure(-2, RelationalOperators.In, (object) this._catalogsIDs.ToArray(), LogicalOperators.NONE, 0, false);
      part = (INodePart) new ObjectsPart(Consts.ImbaseCatalogTypeID, condition, this.Services);
    }
    else
      part = (INodePart) new ImbaseDescriptorsPart(ImbaseRootNode._catalogTypes);
    return this.SlotsFromSinglePart(part);
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    INodePart part;
    if (this._catalogsIDs != null && this._catalogsIDs.Count > 0)
    {
      ConditionStructure condition = new ConditionStructure(-2, RelationalOperators.In, (object) this._catalogsIDs.ToArray(), LogicalOperators.NONE, 0, false);
      part = (INodePart) new ObjectsPart(Consts.ImbaseCatalogTypeID, condition, this.Services);
    }
    else
      part = (INodePart) new ImbaseExtendedObjectsPart(Consts.ImbaseCatalogTypeID, this.Services);
    return this.SlotsFromSinglePart(part);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    CatalogsNodeID catalogsNodeId = nodeID as CatalogsNodeID;
    return !(dataFormat == typeof (IDescriptor)) || catalogsNodeId == null ? (!(dataFormat == typeof (ICanOpenInNewWindow)) || catalogsNodeId == null ? base.GetData(nodeID, dataFormat) : (object) new CanOpenInNewWindow()) : (object) new CatalogsNodeDescriptor(Consts.CatalogsNodeCategoryID, catalogsNodeId.CatalogName);
  }

  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return Utils.DefaultSupportedColumnsObjects();
  }

  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return Utils.DefaultColumnsObjects();
  }

  private void BuildFolders()
  {
    if (ImbaseRootNode._catalogTypes != null)
      return;
    ImbaseRootNode._catalogTypes = new DescriptorCollection();
    foreach (string name in CatalogTypes.Names)
    {
      name.GetHashCode();
      ImbaseRootNode._catalogTypes.Add((IDescriptor) new CatalogsNodeDescriptor(name.GetHashCode(), name));
    }
  }

  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  public override ContentAttributes GetAttributesOf(INodeID nodeID)
  {
    return base.GetAttributesOf(nodeID) | ContentAttributes.Folder;
  }
}
