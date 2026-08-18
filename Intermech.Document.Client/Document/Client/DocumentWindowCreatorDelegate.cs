// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentWindowCreatorDelegate
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Document.UI;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Делегат для конструктора окна документа в зависимости от плагина</summary>
/// <param name="documentManager">Менеджер документов</param>
/// <param name="document">Документ</param>
/// <param name="readOnly">Только для чтения</param>
public delegate ImDocumentEditorForm DocumentWindowCreatorDelegate(
  IImDocumentManager documentManager,
  ImDocument document,
  bool readOnly);
