// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BaseReferenceNodeType
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Тип ссылки на элемент</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum BaseReferenceNodeType
{
  /// <summary>этот элемент</summary>
  [CustomDescription("Attribute.Interfaces.Document_413")] ntThisNode,
  /// <summary>Родитель элемента</summary>
  [CustomDescription("Attribute.Interfaces.Document_414")] ntParentNode,
  /// <summary>страница</summary>
  [CustomDescription("Attribute.Interfaces.Document_415")] ntParentPage,
  /// <summary>Документ</summary>
  [CustomDescription("Attribute.Interfaces.Document_416")] ntParentDocument,
  /// <summary>Выбранный элемент</summary>
  [CustomDescription("Attribute.Interfaces.Document_417")] ntSelectedNode,
  /// <summary>Получать ссылку у родителя</summary>
  [CustomDescription("Attribute.Interfaces.Document_418")] ntUseParentLink,
  /// <summary>Получать ссылку у документа</summary>
  [CustomDescription("Attribute.Interfaces.Document_419")] ntUseParentDocumentLink,
}
