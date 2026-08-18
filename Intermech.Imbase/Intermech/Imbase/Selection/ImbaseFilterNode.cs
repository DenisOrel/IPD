// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Selection;

internal class ImbaseFilterNode : CompositeNode, IContextAware
{
  private int _typeID = -1;
  private long _objID;
  private DataTable _dtFilter;
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();

  public ImbaseFilterNode(int typeID, long objID)
  {
    this._typeID = typeID;
    this._objID = objID;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    List<Guid> guidList1 = (List<Guid>) null;
    if (service1 != null)
      guidList1 = service1.Rule.GetObjectTypeVisibleRelationsGuids(this._typeID, true);
    List<Guid> guidList2 = guidList1 ?? new List<Guid>(1);
    if (guidList2.Count == 0)
    {
      Guid relationTypeGuid = MetaDataHelper.GetDefaultRelationTypeGuid(this._typeID);
      if (relationTypeGuid != Guid.Empty)
        guidList2.Add(relationTypeGuid);
    }
    if (guidList2.Count > 0)
    {
      if (this.Services != null && this.Services.GetService(typeof (ImbaseDisableCatalogsComposition)) is ImbaseDisableCatalogsComposition service2 && service2.Category == DisableImbaseCategory.Folder)
      {
        ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.Equal, (object) Intermech.Imbase.Consts.ImbaseFolderTypeID, LogicalOperators.NONE, 0, false);
        foreach (Guid relTypeGuid in guidList2)
        {
          ImbaseFilterPart part = new ImbaseFilterPart(this._typeID, this._objID, RelatedObjectsRole.Composition, MetaDataHelper.GetRelationTypeID(relTypeGuid), (IServiceProvider) null);
          part.SetFilter(this._dtFilter);
          folderSlots.Add(new PartSlot(Intermech.Imbase.Consts.ImbaseFolderTypeGUID, (INodePart) part));
        }
        return folderSlots;
      }
      foreach (Guid relTypeGuid in guidList2)
      {
        ImbaseFilterPart part = new ImbaseFilterPart(this._typeID, this._objID, RelatedObjectsRole.Composition, MetaDataHelper.GetRelationTypeID(relTypeGuid), (IServiceProvider) null);
        part.SetFilter(this._dtFilter);
        folderSlots.Add(new PartSlot(Intermech.Imbase.Consts.ImbaseFolderTypeGUID, (INodePart) part));
      }
    }
    else
      folderSlots = base.CreateFolderSlots();
    return folderSlots;
  }

  public void SetFilter(DataTable dt) => this._dtFilter = dt;

  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }
}
