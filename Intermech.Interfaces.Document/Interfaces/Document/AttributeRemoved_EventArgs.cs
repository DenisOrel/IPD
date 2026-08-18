// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AttributeRemoved_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргумент обработчика события AttributeRemoved</summary>
public class AttributeRemoved_EventArgs : EventArgs
{
  /// <summary>Имя атрибута</summary>
  public string AttributeName;
  public bool UpdateUI;
  public bool UpdateLayout;

  /// <summary>Конструктор аргумента события AttributeRemoved</summary>
  /// <param name="attributeName">Имя атрибута</param>
  public AttributeRemoved_EventArgs(string attributeName, bool updateUI, bool updateLayout)
  {
    this.AttributeName = attributeName;
    this.UpdateUI = updateUI;
    this.UpdateLayout = updateLayout;
  }
}
