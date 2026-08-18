// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.NotInheritedBaseTechObjType
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>
/// Custom attribute for tech object type none inherited from base type
/// </summary>
/// <remarks>При использовании данного атрибута обязательно
/// должен присутствовать атрибут "IsObjectType"</remarks>
internal class NotInheritedBaseTechObjType : Attribute
{
  /// <summary>Конструктор</summary>
  /// <param name="value"></param>
  public NotInheritedBaseTechObjType(bool value) => this.Value = value;

  /// <summary>Attribute's value</summary>
  public bool Value { get; private set; }
}
