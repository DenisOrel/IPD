// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.IEntityBatchUpdateLog
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Data.Entities;

public interface IEntityBatchUpdateLog
{
  void Clear();

  void CreateEntity(object entity);

  void UpdateEntity(object entity);

  void RemoveEntity(object entity);

  void AddChildEntity(EntityRelationQuickInfo entityRelation);

  void UpdateChildEntityOccurence(EntityRelationQuickInfo entityRelation);

  void RemoveChildEntity(EntityRelationQuickInfo entityRelation);
}
