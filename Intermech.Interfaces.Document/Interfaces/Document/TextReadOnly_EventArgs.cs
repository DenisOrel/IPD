// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TextReadOnly_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Данные события TextReadOnly</summary>
public class TextReadOnly_EventArgs : EventArgs
{
  /// <summary>Только для чтения</summary>
  public bool ReadOnly;
}
