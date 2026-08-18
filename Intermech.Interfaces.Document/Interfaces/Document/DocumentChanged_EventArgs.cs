// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentChanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события DocumentChanged</summary>
public class DocumentChanged_EventArgs : EventArgs
{
  /// <summary>Старый документ</summary>
  public ImDocumentData OldDocument;
  /// <summary>Новый документ</summary>
  public ImDocumentData NewDocument;

  public DocumentChanged_EventArgs(ImDocumentData oldDocument, ImDocumentData newDocument)
  {
    this.OldDocument = oldDocument;
    this.NewDocument = newDocument;
  }
}
