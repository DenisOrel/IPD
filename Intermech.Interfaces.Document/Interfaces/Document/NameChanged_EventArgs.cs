// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.NameChanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события NameChanged</summary>
public class NameChanged_EventArgs : EventArgs
{
  /// <summary>Новое имя</summary>
  public string NewName;

  /// <summary>Конструктор</summary>
  /// <param name="newName">Новое имя</param>
  public NameChanged_EventArgs(string newName) => this.NewName = newName;
}
