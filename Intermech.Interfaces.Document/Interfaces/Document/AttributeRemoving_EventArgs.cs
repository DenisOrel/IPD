// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AttributeRemoving_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргумент обработчика события AttributeRemoving</summary>
public class AttributeRemoving_EventArgs : EventArgs
{
  /// <summary>Имя атрибута</summary>
  public string AttributeName;
  /// <summary>Отменить удаление атрибута</summary>
  public bool Cancel;

  /// <summary>Конструктор аргумента события AttributeRemoving</summary>
  /// <param name="attributeName">Имя атрибута</param>
  public AttributeRemoving_EventArgs(string attributeName) => this.AttributeName = attributeName;
}
