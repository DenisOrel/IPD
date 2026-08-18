// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImExternalDocument
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Базовый класс для внешних документов</summary>
public class ImExternalDocument(bool withTemplate) : ImDocument(withTemplate)
{
  private bool linksUpdated;
  private string externalDocumentType = "";

  /// <summary>Тип документа</summary>
  public string ExternalDocumentType
  {
    get => this.externalDocumentType;
    set => this.externalDocumentType = value;
  }

  /// <summary>Сссылки были обновлены</summary>
  public bool LinksUpdated
  {
    get => this.linksUpdated;
    set => this.linksUpdated = value;
  }
}
