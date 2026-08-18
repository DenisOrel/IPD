// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityValidationException
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities;

public class EntityValidationException : EntityException
{
  private object entity;

  public EntityValidationException(string message)
    : base(message)
  {
  }

  public EntityValidationException(object entity, string message)
    : base(message)
  {
    this.Entity = entity != null ? entity : throw new ArgumentNullException(nameof (entity));
  }

  /// <summary>
  /// Возвращает доменный объект с ошибкой.
  /// Значение свойства может быть равно null, если конкретный доменный объект с ошибкой неизвестен.
  /// </summary>
  public object Entity
  {
    get => this.entity;
    private set => this.entity = value;
  }
}
