// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.FolderNode
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

#nullable disable
namespace Intermech.Imbase;

public class FolderNode : CompositeNode, IContextAware
{
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();
  protected int _objTypeID = -1;
  protected long _objID;

  public FolderNode(int objTypeID, long objID)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    List<Guid> guidList1 = (List<Guid>) null;
    if (service1 != null)
      guidList1 = service1.Rule.GetObjectTypeVisibleRelationsGuids(this._objTypeID, true);
    List<Guid> guidList2 = guidList1 ?? new List<Guid>(1);
    if (guidList2.Count == 0)
    {
      Guid relationTypeGuid = MetaDataHelper.GetDefaultRelationTypeGuid(this._objTypeID);
      if (relationTypeGuid == Guid.Empty)
        guidList2.Add(relationTypeGuid);
    }
    if (guidList2.Count > 0)
    {
      if (this.Services != null)
      {
        this.Services.GetService(typeof (IViewState));
        if (this.Services.GetService(typeof (ImbaseDisableCatalogsComposition)) is ImbaseDisableCatalogsComposition service2 && service2.Category == DisableImbaseCategory.Folder)
        {
          ConditionStructure condition = new ConditionStructure(-7, RelationalOperators.Equal, (object) Consts.ImbaseFolderTypeID, LogicalOperators.NONE, 0, false);
          foreach (Guid relTypeGuid in guidList2)
          {
            int relationTypeId = MetaDataHelper.GetRelationTypeID(relTypeGuid);
            folderSlots.Add(new PartSlot(Consts.ImbaseFolderTypeGUID, (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relationTypeId, condition, this.Services)));
          }
          return folderSlots;
        }
      }
      foreach (Guid relTypeGuid in guidList2)
      {
        int relationTypeId = MetaDataHelper.GetRelationTypeID(relTypeGuid);
        folderSlots.Add(new PartSlot(Consts.ImbaseFolderTypeGUID, (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relationTypeId, this.Services)));
      }
    }
    else
      folderSlots = base.CreateFolderSlots();
    return folderSlots;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<PartSlot> nonFolderSlots = new List<PartSlot>();
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    List<Guid> guidList1 = (List<Guid>) null;
    if (service1 != null)
      guidList1 = service1.Rule.GetObjectTypeVisibleRelationsGuids(this._objTypeID, true);
    List<Guid> guidList2 = guidList1 ?? new List<Guid>(1);
    if (guidList2.Count == 0)
    {
      Guid relationTypeGuid = MetaDataHelper.GetDefaultRelationTypeGuid(this._objTypeID);
      if (relationTypeGuid == Guid.Empty)
        guidList2.Add(relationTypeGuid);
    }
    if (guidList2.Count > 0)
    {
      if (this.Services != null)
      {
        this.Services.GetService(typeof (IViewState));
        if (this.Services.GetService(typeof (ImbaseDisableCatalogsComposition)) is ImbaseDisableCatalogsComposition service2 && service2.Category == DisableImbaseCategory.Folder)
        {
          ConditionStructure condition = new ConditionStructure(-7, RelationalOperators.Equal, (object) Consts.ImbaseFolderTypeID, LogicalOperators.NONE, 0, false);
          foreach (Guid relTypeGuid in guidList2)
          {
            int relationTypeId = MetaDataHelper.GetRelationTypeID(relTypeGuid);
            nonFolderSlots.Add(new PartSlot(Consts.ImbaseFolderTypeGUID, (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relationTypeId, condition, this.Services)));
          }
          return nonFolderSlots;
        }
      }
      foreach (Guid relTypeGuid in guidList2)
      {
        int relationTypeId = MetaDataHelper.GetRelationTypeID(relTypeGuid);
        nonFolderSlots.Add(new PartSlot(Consts.ImbaseFolderTypeGUID, (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relationTypeId, this.Services)));
      }
    }
    else
      nonFolderSlots = base.CreateFolderSlots();
    return nonFolderSlots;
  }

  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }
}
