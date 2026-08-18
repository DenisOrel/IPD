// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.IsTechCardTypeAttribute
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Custom attribute for metadata types</summary>
[AttributeUsage(AttributeTargets.Field)]
internal class IsTechCardTypeAttribute : Attribute
{
  /// <summary>Конструктор</summary>
  /// <param name="isTechCardType"></param>
  protected IsTechCardTypeAttribute(bool isTechCardType) => this.IsTechCardType = isTechCardType;

  /// <summary>Признак технологического типа</summary>
  public bool IsTechCardType { get; private set; }
}
