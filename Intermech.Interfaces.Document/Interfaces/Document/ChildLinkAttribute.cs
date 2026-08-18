// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ChildLinkAttribute
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Атрибут означает что поле является ссылкой на дочерний объект относительно владельца поля</summary>
[AttributeUsage(AttributeTargets.Field)]
[Serializable]
public class ChildLinkAttribute : Attribute
{
  /// <summary>Ссылка на дочерний объект</summary>
  private bool isChildLink;

  /// <summary>Конструктор</summary>
  /// <param name="isChildLink">Ссылка на дочерний объект</param>
  public ChildLinkAttribute(bool isChildLink) => this.isChildLink = isChildLink;

  /// <summary>Конструктор</summary>
  public ChildLinkAttribute() => this.isChildLink = true;

  /// <summary>Ссылка на дочерний объект</summary>
  public bool IsChildLink
  {
    [DebuggerStepThrough] get => this.isChildLink;
  }
}
