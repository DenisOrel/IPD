// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

public class DBModelBuilder
{
  private Dictionary<Type, DBObjectEntityBuilder> entities;
  private Dictionary<Type, DBRelationEntityBuilder> childOccurences;
  private DataPropertyHelper dataPropertyHelper;

  public DBModelBuilder()
  {
    this.entities = new Dictionary<Type, DBObjectEntityBuilder>();
    this.childOccurences = new Dictionary<Type, DBRelationEntityBuilder>();
    this.dataPropertyHelper = DataPropertyHelper.DefaultInstance;
  }

  public void Clear()
  {
    this.entities.Clear();
    this.childOccurences.Clear();
  }

  public DBObjectEntityBuilder Entity<TEntity>()
  {
    Type type = typeof (TEntity);
    if (this.dataPropertyHelper.IsAllowedDataPropertyType(type))
      throw new ModelConfigurationException(1, $"Тип '{type}' не может использоваться в качестве типа доменного объекта.");
    if (this.childOccurences.ContainsKey(type))
      throw new InvalidOperationException($"Тип '{type}' уже указан в качестве объекта-связки между доменными объектами.");
    DBObjectEntityBuilder objectEntityBuilder;
    if (!this.entities.TryGetValue(type, out objectEntityBuilder))
    {
      objectEntityBuilder = new DBObjectEntityBuilder(type);
      this.entities.Add(type, objectEntityBuilder);
    }
    return objectEntityBuilder;
  }

  public DBRelationEntityBuilder ChildOccurence<TChildOccurence>()
  {
    Type type = typeof (TChildOccurence);
    if (this.dataPropertyHelper.IsAllowedDataPropertyType(type))
      throw new ModelConfigurationException(1, $"Тип '{type}' не может использоваться в качестве типа доменного объекта.");
    if (this.entities.ContainsKey(type))
      throw new InvalidOperationException($"Тип '{type}' уже указан в качестве доменного объекта.");
    DBRelationEntityBuilder relationEntityBuilder;
    if (!this.childOccurences.TryGetValue(type, out relationEntityBuilder))
    {
      relationEntityBuilder = new DBRelationEntityBuilder(type);
      this.childOccurences.Add(type, relationEntityBuilder);
    }
    return relationEntityBuilder;
  }

  protected IDictionary<Type, DBObjectEntityBuilder> Entities
  {
    [DebuggerStepThrough] get => (IDictionary<Type, DBObjectEntityBuilder>) this.entities;
  }

  protected IDictionary<Type, DBRelationEntityBuilder> ChildOccurences
  {
    [DebuggerStepThrough] get => (IDictionary<Type, DBRelationEntityBuilder>) this.childOccurences;
  }
}
