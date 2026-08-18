// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TemplateChanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события TemplateChanged</summary>
public class TemplateChanged_EventArgs : EventArgs
{
  /// <summary>Старый шаблон</summary>
  public DocumentTreeNode OldTemplate;
  /// <summary>Новый шаблон</summary>
  public DocumentTreeNode NewTemplate;

  /// <summary>Конструктор</summary>
  public TemplateChanged_EventArgs(DocumentTreeNode oldTemplate, DocumentTreeNode newTemplate)
  {
    this.OldTemplate = oldTemplate;
    this.NewTemplate = newTemplate;
  }
}
