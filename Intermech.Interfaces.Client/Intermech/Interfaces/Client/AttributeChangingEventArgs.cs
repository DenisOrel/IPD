// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AttributeChangingEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// 
/// </summary>
public class AttributeChangingEventArgs : EventArgs
{
  /// <summary>Идентификатор формы.</summary>
  public long FormID { get; set; }

  /// <summary>Идентификатор основного объекта/связи.</summary>
  public long ObjectID { get; set; }

  /// <summary>Тип основного объекта/связи.</summary>
  public int ObjectTypeID { get; set; }

  /// <summary>
  /// Старые (до изменения) значения атрибутов основного объекта/связи.
  /// </summary>
  public IEnumerable<AttributeValues> OldObjectAttributes { get; set; }

  /// <summary>Новые значения атрибутов основного объекта/связи.</summary>
  /// <remarks>
  /// Сюда подписчики записывают значения атрибутов, после их изменения.
  /// Измененные значения вернутся в форму и будут отображены в контролах.
  /// </remarks>
  public IEnumerable<AttributeValues> NewObjectAttributes { get; set; }

  /// <summary>Идентификатор дополнительной связи.</summary>
  public long RelationID { get; set; }

  /// <summary>Тип дополнительной связи.</summary>
  public int RelationTypeID { get; set; }

  /// <summary>
  /// Старые (до изменения) значения атрибутов дополнительной связи.
  /// </summary>
  public IEnumerable<AttributeValues> OldRelationAttributes { get; set; }

  /// <summary>Новые значения атрибутов дополнительной связи.</summary>
  /// <remarks>
  /// Сюда подписчики записывают значения атрибутов, после их изменения.
  /// Измененные значения вернутся в форму и будут отображены в контролах.
  /// </remarks>
  public IEnumerable<AttributeValues> NewRelationAttributes { get; set; }
}
