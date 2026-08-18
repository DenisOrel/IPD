// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.SerializeAsLinkAttribute
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Атрибут Сериализовать поле как ссылку</summary>
[AttributeUsage(AttributeTargets.Field)]
public class SerializeAsLinkAttribute : Attribute
{
  /// <summary>Сериализовать поле как ссылку</summary>
  private bool serializeAsLink;

  /// <summary>Конструктор</summary>
  /// <param name="serializeAsLink">Сериализовать поле как ссылку</param>
  public SerializeAsLinkAttribute(bool serializeAsLink) => this.serializeAsLink = serializeAsLink;

  /// <summary>Конструктор</summary>
  public SerializeAsLinkAttribute() => this.serializeAsLink = true;

  /// <summary>Сериализовать поле как ссылку</summary>
  public bool SerializeAsLink
  {
    [DebuggerStepThrough] get => this.serializeAsLink;
    set => this.serializeAsLink = value;
  }
}
