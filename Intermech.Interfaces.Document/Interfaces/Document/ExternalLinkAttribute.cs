// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ExternalLinkAttribute
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Атрибут означает что ссылка внешняя относительно владельца поля</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ExternalLinkAttribute : Attribute
{
  /// <summary>Внешняя ссылка</summary>
  private bool isExternal;

  /// <summary>Конструктор</summary>
  /// <param name="isExternal">Внешняя ссылка</param>
  public ExternalLinkAttribute(bool isExternal) => this.isExternal = isExternal;

  /// <summary>Конструктор</summary>
  public ExternalLinkAttribute() => this.isExternal = true;

  /// <summary>Внешняя ссылка</summary>
  public bool IsExternal
  {
    [DebuggerStepThrough] get => this.isExternal;
    set => this.isExternal = value;
  }
}
