// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentEditorPluginScriptService
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using System;

#nullable disable
namespace Intermech.Document.Client;

internal class DocumentEditorPluginScriptService : IDocumentEditorPluginScriptService
{
  public bool IsDocumentTemplateType(int objType)
  {
    return DocumentEditorPlugin.IsDocumentTemplateType(objType);
  }

  public bool IsDocumentType(int objType) => DocumentEditorPlugin.IsDocumentType(objType);

  public void SaveImDocumentObjectFile(
    long docObjectID,
    ImDocument document,
    string fileName,
    int fileIndex,
    bool isNewDocument)
  {
    DocumentEditorPlugin.SaveImDocumentObjectFile(docObjectID, document ?? throw new Exception("Документ не найден"), fileName, fileIndex, isNewDocument);
  }

  public ImDocument LoadDocumentFromDBObject(
    long docObjectID,
    int fileIndex = -1,
    bool createIfNotFound = false,
    bool updateDoc = true,
    bool loadInThread = false)
  {
    return DocumentEditorPlugin.LoadDocumentFromDBObject(docObjectID, fileIndex, createIfNotFound, updateDoc, loadInThread);
  }

  public Guid GetDocumentTemplateIDFromIMDocSettings(Guid documentType)
  {
    return DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(documentType);
  }
}
