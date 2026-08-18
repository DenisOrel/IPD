// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.NullEntityBatchUpdateLog
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Data.Entities;

internal sealed class NullEntityBatchUpdateLog : IEntityBatchUpdateLog
{
  public void Clear()
  {
  }

  public void CreateEntity(object entity)
  {
  }

  public void UpdateEntity(object entity)
  {
  }

  public void RemoveEntity(object entity)
  {
  }

  public void AddChildEntity(EntityRelationQuickInfo entityRelation)
  {
  }

  public void UpdateChildEntityOccurence(EntityRelationQuickInfo entityRelation)
  {
  }

  public void RemoveChildEntity(EntityRelationQuickInfo entityRelation)
  {
  }
}
