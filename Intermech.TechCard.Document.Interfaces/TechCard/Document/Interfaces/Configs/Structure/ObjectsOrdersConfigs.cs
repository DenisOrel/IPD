// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.ObjectsOrdersConfigs
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public class ObjectsOrdersConfigs : IAssignable, ICloneable
{
  private Dictionary<string, ObjectOrderConfig> _configs = new Dictionary<string, ObjectOrderConfig>();

  private string GetConfigID(int objTypeId, int relTypeId)
  {
    return $"{Convert.ToString(objTypeId)}_{Convert.ToString(relTypeId)}";
  }

  [NotNull]
  public TPStructureObjectConfig ParentConfig { get; set; }

  [NotNull]
  public IEnumerable<ObjectOrderConfig> Configs
  {
    get => (IEnumerable<ObjectOrderConfig>) this._configs.Values;
  }

  [CanBeNull]
  public ObjectOrderConfig this[int objTypeId, int relTypeId]
  {
    get
    {
      ObjectOrderConfig objectOrderConfig;
      return this._configs.TryGetValue(this.GetConfigID(objTypeId, relTypeId), out objectOrderConfig) ? objectOrderConfig : (ObjectOrderConfig) null;
    }
  }

  public int Count => this._configs.Count;

  public ObjectOrderConfig Add(int objTypeId, int relTypeId, int order = 0)
  {
    ObjectOrderConfig objectOrderConfig = this[objTypeId, relTypeId];
    if (objectOrderConfig == null)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeId);
      if (objectType == null)
        return (ObjectOrderConfig) null;
      IMSRelationType relationType = MetaDataHelper.GetRelationType(relTypeId);
      if (relationType == null)
        return (ObjectOrderConfig) null;
      objectOrderConfig = new ObjectOrderConfig(objectType, relationType);
      objectOrderConfig.Order = order;
      this._configs.Add(this.GetConfigID(objTypeId, relTypeId), objectOrderConfig);
    }
    return objectOrderConfig;
  }

  public ObjectOrderConfig Add(Guid objTypeGuid, Guid relTypeGuid, int order = 0)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
    if (objectType == null)
      return (ObjectOrderConfig) null;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(relTypeGuid);
    if (relationType == null)
      return (ObjectOrderConfig) null;
    ObjectOrderConfig objectOrderConfig = this[objectType.ObjectTypeID, relationType.RelationTypeID];
    if (objectOrderConfig == null)
    {
      objectOrderConfig = new ObjectOrderConfig(objectType, relationType);
      objectOrderConfig.Order = order;
      this._configs.Add(this.GetConfigID(objectType.ObjectTypeID, relationType.RelationTypeID), objectOrderConfig);
    }
    return objectOrderConfig;
  }

  public bool Remove(int objTypeId, int relTypeId)
  {
    return this._configs.Remove(this.GetConfigID(objTypeId, relTypeId));
  }

  public object Clone()
  {
    ObjectsOrdersConfigs objectsOrdersConfigs = new ObjectsOrdersConfigs();
    objectsOrdersConfigs.Assign((object) this);
    return (object) objectsOrdersConfigs;
  }

  public void Assign(object source)
  {
    if (!(source is ObjectsOrdersConfigs objectsOrdersConfigs))
      return;
    this._configs.Clear();
    foreach (string key in objectsOrdersConfigs._configs.Keys)
    {
      ObjectOrderConfig config = objectsOrdersConfigs._configs[key];
      this._configs.Add(key, config.Clone() as ObjectOrderConfig);
    }
  }

  public void Clear() => this._configs.Clear();
}
