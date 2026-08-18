// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.AfterLoadDocumentEventHandler
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Делегат для обработки события после загрузки файла документа</summary>
public delegate void AfterLoadDocumentEventHandler(
  object sender,
  AfterLoadDocumentEventHandlerArgs e);
