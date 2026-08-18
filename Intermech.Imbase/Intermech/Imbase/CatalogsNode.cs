// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CatalogsNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

public class CatalogsNode : CompositeNode, ICatalogsNode, IContextAware
{
  private string _catalogTypeName;
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();

  public CatalogsNode() => this.options = NodeOptions.CanContainsObjectsList;

  public string CatalogTypeName => this._catalogTypeName;

  public void Bind(string catalogTypeName) => this._catalogTypeName = catalogTypeName;

  protected override List<PartSlot> CreateFolderSlots()
  {
    if (this.Services != null)
    {
      IViewState service1 = this.Services.GetService(typeof (IViewState)) as IViewState;
      ImbaseDisableCatalogsComposition service2 = this.Services.GetService(typeof (ImbaseDisableCatalogsComposition)) as ImbaseDisableCatalogsComposition;
      if (service1 != null && (service1.ViewState & ViewStateFlags.NodeInTree) != ViewStateFlags.None && service2 != null && service2.Category == DisableImbaseCategory.Catalog)
        return (List<PartSlot>) null;
    }
    ConditionStructure condition = new ConditionStructure(Consts.CatalogTypeAttID, RelationalOperators.Equal, (object) this._catalogTypeName, LogicalOperators.NONE, 0, false);
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(Consts.ImbaseCatalogTypeID, condition, this.Services));
  }

  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }
}
