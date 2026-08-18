// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IDocumentElement
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Элемент документа</summary>
public interface IDocumentElement
{
  /// <summary>Документ, которому принадлежит элемент</summary>
  ImDocumentData OwnerDocument { get; }

  /// <summary>Документ владеющий шаблоном документа,
  /// которому принадлежит элемент.
  /// Если элемент не принадлежит шаблону, то null</summary>
  ImDocumentData DocumentTemplateOwner { get; }
}
