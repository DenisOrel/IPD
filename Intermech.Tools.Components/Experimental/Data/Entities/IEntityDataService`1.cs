// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.IEntityDataService`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace Experimental.Data.Entities;

public interface IEntityDataService<TEntity>
{
  TEntity Load(object key);

  List<TEntity> LoadAll();

  List<TEntity> LoadAll(Expression<Func<TEntity, bool>> condition);

  void LoadReferences(TEntity entity, string propertyName);

  void LoadReferences<TProperty>(
    TEntity entity,
    Expression<Func<TEntity, TProperty>> propertySelector);

  void CreateEntity(TEntity entity);

  bool UpdateEntity(TEntity entity, ICollection<string> modifiedProperties);

  void RemoveEntity(TEntity entity);

  void AddChildEntity(TEntity parentEntity, string propertyName, object childEntityOrOccurence);

  void AddChildEntity<TProperty>(
    TEntity parentEntity,
    Expression<Func<TEntity, TProperty>> propertySelector,
    object childEntityOrOccurence);

  void UpdateChildEntityOccurence(
    TEntity parentEntity,
    string propertyName,
    object childOccurence,
    List<string> modifiedProperties);

  void UpdateChildEntityOccurence<TProperty>(
    TEntity parentEntity,
    Expression<Func<TEntity, TProperty>> propertySelector,
    object childOccurence,
    List<string> modifiedProperties);

  void RemoveChildEntity(TEntity parentEntity, string propertyName, object childEntityOrOccurence);

  void RemoveChildEntity<TProperty>(
    TEntity parentEntity,
    Expression<Func<TEntity, TProperty>> propertySelector,
    object childEntityOrOccurence);
}
