// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TextValidating_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Данные события TextChanged</summary>
public class TextValidating_EventArgs : EventArgs
{
  /// <summary>Текст</summary>
  public string Text;
  /// <summary>Отменить</summary>
  public bool Cancel;

  /// <summary>Конструктор</summary>
  /// <param name="newText">Текст</param>
  public TextValidating_EventArgs(string text) => this.Text = text;
}
