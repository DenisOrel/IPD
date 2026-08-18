// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.TPStructureObjectsConfigs
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

[DocumentConfigElementType(DocumentConfigElementType.TPStructureObjectsConfigs)]
public class TPStructureObjectsConfigs : DocumentConfigElement
{
  private Dictionary<int, TPStructureObjectConfig> _configs = new Dictionary<int, TPStructureObjectConfig>();

  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new TPStructureObjectsConfigs();
  }

  public override DocumentConfigElementType ElementType
  {
    get => DocumentConfigElementType.TPStructureObjectsConfigs;
  }

  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is TPStructureObjectsConfigs structureObjectsConfigs))
      return;
    foreach (int key in structureObjectsConfigs._configs.Keys)
    {
      TPStructureObjectConfig structureObjectConfig = structureObjectsConfigs._configs[key].Clone() as TPStructureObjectConfig;
      this._configs.Add(key, structureObjectConfig);
    }
  }

  public override void Clear()
  {
    base.Clear();
    this._configs.Clear();
  }

  [NotNull]
  public IEnumerable<TPStructureObjectConfig> Configs
  {
    get => (IEnumerable<TPStructureObjectConfig>) this._configs.Values;
  }

  [CanBeNull]
  public TPStructureObjectConfig this[int objTypeId]
  {
    get
    {
      TPStructureObjectConfig structureObjectConfig;
      return this._configs.TryGetValue(objTypeId, out structureObjectConfig) ? structureObjectConfig : (TPStructureObjectConfig) null;
    }
  }

  public int Count => this._configs.Count<KeyValuePair<int, TPStructureObjectConfig>>();

  [NotNull]
  public TPStructureObjectConfig Add(int objTypeId)
  {
    TPStructureObjectConfig structureObjectConfig = this[objTypeId];
    if (structureObjectConfig == null)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeId);
      if (objectType != null)
      {
        structureObjectConfig = new TPStructureObjectConfig(objectType);
        this._configs.Add(objTypeId, structureObjectConfig);
      }
    }
    return structureObjectConfig;
  }

  public TPStructureObjectConfig Add(Guid objTypeGuid)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
    if (objectType == null)
      return (TPStructureObjectConfig) null;
    TPStructureObjectConfig structureObjectConfig = this[objectType.ObjectTypeID];
    if (structureObjectConfig == null)
    {
      structureObjectConfig = new TPStructureObjectConfig(objectType);
      this._configs.Add(objectType.ObjectTypeID, structureObjectConfig);
    }
    return structureObjectConfig;
  }

  public bool Remove(int objTypeId) => this._configs.Remove(objTypeId);

  public void InitDefault()
  {
    this._configs.Clear();
    TPStructureObjectConfig structureObjectConfig1 = this.Add(TechCardConsts.ObjectTypes.TechProcBaseID);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ArticleBaseID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID, TechCardConsts.RelTypes.TechRelationID, 200);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID, TechCardConsts.RelTypes.TechRelationID, 300);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DocumentBaseID, TechCardConsts.RelTypes.TechRelationID, 500);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OsnastBaseID, TechCardConsts.RelTypes.TechRelationID, 600);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.SurfaceBaseID, TechCardConsts.RelTypes.TechRelationID, 700);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.PersonalBaseID, TechCardConsts.RelTypes.TechRelationID, 800);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CommentaryID, TechCardConsts.RelTypes.TechRelationID, 1100);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ZagotID, TechCardConsts.RelTypes.TechRelationID, 1200);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialBaseID, TechCardConsts.RelTypes.TechRelationID, 1300);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OperaciyaID, TechCardConsts.RelTypes.TechRelationID, 1400);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechProcTipovID, TechCardConsts.RelTypes.TechRelationID, 1500);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ContrParamBaseID, TechCardConsts.RelTypes.TechRelationID, 1600);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DraftBaseID, TechCardConsts.RelTypes.TechRelationID, 1700);
    structureObjectConfig1.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OborudBaseID, TechCardConsts.RelTypes.TechRelationID, 1800);
    TPStructureObjectConfig structureObjectConfig2 = this.Add(TechCardConsts.ObjectTypes.OperaciyaID);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CommentaryID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OborudBaseID, TechCardConsts.RelTypes.TechRelationID, 100);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID, TechCardConsts.RelTypes.TechRelationID, 300);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID, TechCardConsts.RelTypes.TechRelationID, 400);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.PersonalBaseID, TechCardConsts.RelTypes.TechRelationID, 500);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialBaseID, TechCardConsts.RelTypes.TechRelationID, 700);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OsnastBaseID, TechCardConsts.RelTypes.TechRelationID, 800);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.InstrumPosID, TechCardConsts.RelTypes.TechRelationID, 900);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.PerehodID, TechCardConsts.RelTypes.TechRelationID, 1000);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.RegimID, TechCardConsts.RelTypes.TechRelationID, 1100);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DocumentBaseID, TechCardConsts.RelTypes.TechRelationID, 1200);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DopPriemID, TechCardConsts.RelTypes.TechRelationID, 1300);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DraftBaseID, TechCardConsts.RelTypes.TechRelationID, 1400);
    structureObjectConfig2.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ContrParamBaseID, TechCardConsts.RelTypes.TechRelationID, 1500);
    TPStructureObjectConfig structureObjectConfig3 = this.Add(TechCardConsts.ObjectTypes.PerehodID);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CommentaryID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OborudBaseID, TechCardConsts.RelTypes.TechRelationID, 100);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.PersonalBaseID, TechCardConsts.RelTypes.TechRelationID, 200);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID, TechCardConsts.RelTypes.TechRelationID, 400);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID, TechCardConsts.RelTypes.TechRelationID, 500);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialBaseID, TechCardConsts.RelTypes.TechRelationID, 600);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OsnastBaseID, TechCardConsts.RelTypes.TechRelationID, 800);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.RegimID, TechCardConsts.RelTypes.TechRelationID, 900);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DopPriemID, TechCardConsts.RelTypes.TechRelationID, 1100);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DraftBaseID, TechCardConsts.RelTypes.TechRelationID, 1200);
    structureObjectConfig3.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ContrParamBaseID, TechCardConsts.RelTypes.TechRelationID, 1300);
    TPStructureObjectConfig structureObjectConfig4 = this.Add(TechCardConsts.ObjectTypes.OborudBaseID);
    structureObjectConfig4.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.PersonalBaseID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig4.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OsnastBaseID, TechCardConsts.RelTypes.TechRelationID, 100);
    structureObjectConfig4.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialBaseID, TechCardConsts.RelTypes.TechRelationID, 200);
    structureObjectConfig4.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CommentaryID, TechCardConsts.RelTypes.TechRelationID, 300);
    structureObjectConfig4.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID, TechCardConsts.RelTypes.TechRelationID, 500);
    structureObjectConfig4.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID, TechCardConsts.RelTypes.TechRelationID, 600);
    TPStructureObjectConfig structureObjectConfig5 = this.Add(TechCardConsts.ObjectTypes.CehRouteID);
    structureObjectConfig5.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TemplRouteBaseID, TechCardConsts.RelTypes.TechRelationID, 100);
    structureObjectConfig5.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ElemRouteID, TechCardConsts.RelTypes.TechRelationID, 200);
    TPStructureObjectConfig structureObjectConfig6 = this.Add(TechCardConsts.ObjectTypes.DopPriemID);
    structureObjectConfig6.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID, TechCardConsts.RelTypes.TechRelationID, 100);
    structureObjectConfig6.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID, TechCardConsts.RelTypes.TechRelationID, 200);
    structureObjectConfig6.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DraftBaseID, TechCardConsts.RelTypes.TechRelationID, 300);
    structureObjectConfig6.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ContrParamBaseID, TechCardConsts.RelTypes.TechRelationID, 400);
    structureObjectConfig6.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.OsnastBaseID, TechCardConsts.RelTypes.TechRelationID, 500);
    this.Add(TechCardConsts.ObjectTypes.SurfaceBaseID).ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.SurfaceParamID, TechCardConsts.RelTypes.TechRelationID);
    this.Add(TechCardConsts.ObjectTypes.TemplRouteBaseID).ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ElemRouteID, TechCardConsts.RelTypes.TechRelationID);
    TPStructureObjectConfig structureObjectConfig7 = this.Add(TechCardConsts.ObjectTypes.InstrumPosID);
    structureObjectConfig7.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CommentaryID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig7.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.PerehodID, TechCardConsts.RelTypes.TechRelationID, 100);
    TPStructureObjectConfig structureObjectConfig8 = this.Add(TechCardConsts.ObjectTypes.ArticleBaseID);
    structureObjectConfig8.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CehRouteID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig8.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.ZagotID, TechCardConsts.RelTypes.TechRelationID, 100);
    structureObjectConfig8.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialBaseID, TechCardConsts.RelTypes.TechRelationID, 200);
    this.Add(TechCardConsts.ObjectTypes.OsnastBaseID).ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.DraftBaseID, TechCardConsts.RelTypes.TechRelationID, 1700);
    this.Add(TechCardConsts.ObjectTypes.ZagotID).ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.CommentaryID, TechCardConsts.RelTypes.TechRelationID);
    TPStructureObjectConfig structureObjectConfig9 = this.Add(TechCardConsts.ObjectTypes.MaterialGroupID);
    structureObjectConfig9.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialBaseID, TechCardConsts.RelTypes.TechRelationID);
    structureObjectConfig9.ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.MaterialGroupID, TechCardConsts.RelTypes.TechRelationID, 100);
    this.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID).ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnTrebovanBaseID, TechCardConsts.RelTypes.TechRelationID);
    this.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID).ChildsOrdersConfigs.Add(TechCardConsts.ObjectTypes.TechnUslovBaseID, TechCardConsts.RelTypes.TechRelationID, 100);
  }

  public TPStructureObjectsConfigs ExpandConfigWithAllSubTypes()
  {
    TPStructureObjectsConfigs expandedConfig = this.Clone() as TPStructureObjectsConfigs;
    expandedConfig._configs.Keys.ToList<int>().ForEach((Action<int>) (objTypeId =>
    {
      TPStructureObjectConfig originConfig = this[objTypeId];
      MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeId).ForEach((Action<int>) (succesorObjId =>
      {
        if (objTypeId == succesorObjId)
          return;
        expandedConfig.Add(succesorObjId).Assign((object) originConfig);
      }));
    }));
    foreach (TPStructureObjectConfig config1 in expandedConfig.Configs)
    {
      TPStructureObjectConfig config = config1;
      config.ChildsOrdersConfigs.Configs.ToList<ObjectOrderConfig>().ForEach((Action<ObjectOrderConfig>) (originOrder => MetaDataHelper.GetObjectTypeChildrenIDRecursive(originOrder.ObjectType.ObjectTypeID).ForEach((Action<int>) (succesorObjId =>
      {
        if (originOrder.ObjectType.ObjectTypeID == succesorObjId)
          return;
        config.ChildsOrdersConfigs.Add(succesorObjId, originOrder.RelationType.RelationTypeID, originOrder.Order);
      }))));
    }
    return expandedConfig;
  }
}
