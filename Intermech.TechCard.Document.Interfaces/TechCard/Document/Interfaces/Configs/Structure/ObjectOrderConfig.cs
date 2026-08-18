// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.ObjectOrderConfig
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public class ObjectOrderConfig : IAssignable, ICloneable
{
  public ObjectOrderConfig(IMSObjectType objType, IMSRelationType relType)
  {
    this.ObjectType = objType;
    this.RelationType = relType;
  }

  [NotNull]
  public IMSObjectType ObjectType { get; private set; }

  [NotNull]
  public IMSRelationType RelationType { get; private set; }

  public int Order { get; set; }

  public void Assign(object source)
  {
    if (!(source is ObjectOrderConfig objectOrderConfig))
      return;
    this.ObjectType = objectOrderConfig.ObjectType;
    this.RelationType = objectOrderConfig.RelationType;
    this.Order = objectOrderConfig.Order;
  }

  public void Clear()
  {
    this.ObjectType = (IMSObjectType) null;
    this.RelationType = (IMSRelationType) null;
    this.Order = 0;
  }

  public object Clone()
  {
    ObjectOrderConfig objectOrderConfig = new ObjectOrderConfig((IMSObjectType) null, (IMSRelationType) null);
    objectOrderConfig.Assign((object) this);
    return (object) objectOrderConfig;
  }
}
