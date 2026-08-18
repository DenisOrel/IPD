// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.Changed_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события Changed</summary>
public class Changed_EventArgs : EventArgs
{
  /// <summary>Произошли изменения не влияющие на дату модификации документа</summary>
  public bool SaveModificationDate;

  /// <summary>Конструктор</summary>
  public Changed_EventArgs()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="saveModificationDate">Изменения не влияющие на дату модификации документа</param>
  public Changed_EventArgs(bool saveModificationDate)
  {
    this.SaveModificationDate = saveModificationDate;
  }
}
