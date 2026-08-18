// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.TPStructureObjectConfig
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public class TPStructureObjectConfig : IAssignable, ICloneable
{
  public TPStructureObjectConfig(IMSObjectType objType)
  {
    this.ObjectType = objType;
    this.ChildsOrdersConfigs = new ObjectsOrdersConfigs()
    {
      ParentConfig = this
    };
  }

  [NotNull]
  public IMSObjectType ObjectType { get; private set; }

  [NotNull]
  public ObjectsOrdersConfigs ChildsOrdersConfigs { get; }

  public void Assign(object source)
  {
    this.ChildsOrdersConfigs.Clear();
    if (source is TPStructureObjectConfig structureObjectConfig)
    {
      this.ObjectType = structureObjectConfig.ObjectType;
      this.ChildsOrdersConfigs.Assign((object) structureObjectConfig.ChildsOrdersConfigs);
    }
    this.ChildsOrdersConfigs.ParentConfig = this;
  }

  public void Clear() => this.ChildsOrdersConfigs.Clear();

  public object Clone()
  {
    TPStructureObjectConfig structureObjectConfig = new TPStructureObjectConfig((IMSObjectType) null);
    structureObjectConfig.Assign((object) this);
    return (object) structureObjectConfig;
  }
}
