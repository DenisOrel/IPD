// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityMemberPresenceStatus
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>Статусы присутсвия свойств у доменного объекта.</summary>
public enum EntityMemberPresenceStatus
{
  /// <summary>
  /// Свойство присутствует у доменного объекта.
  /// Это значит, что свойство были инициализировано при создании объекта, а его значение имеет смысл и может использоваться.
  /// </summary>
  Present,
  /// <summary>
  /// Свойство отсутствует у доменного объекта, хотя оно определено в типе этого объекта.
  /// Это значит, что свойство не было инициализировано при создании объекта, а его значение не имеет смысла и не должно использоваться.
  /// Например, при создании объекта его навигационные свойства обычное не инициализируются.
  /// </summary>
  NotPresent,
}
