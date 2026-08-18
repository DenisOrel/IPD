// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RefToDBObjectType
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

/// <summary>Тип ссылки на объект БД</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum RefToDBObjectType
{
  /// <summary>Выбранный объект. Объект БД, заданный явно</summary>
  [CustomDescription("Attribute.Interfaces.Document_397")] rtSelectedObject,
  /// <summary>Получать ссылку на объект у родителя. Ид объекта БД извлекается из ссылки у родительского узла.</summary>
  [CustomDescription("Attribute.Interfaces.Document_398")] rtUseParentObjectLink,
  /// <summary>Получать ссылку на объект у документа. Ид объекта БД извлекается из ссылки у документа.</summary>
  [CustomDescription("Attribute.Interfaces.Document_399")] rtUseParentDocumentObjectLink,
  /// <summary>Выбранная связь. Связь БД, заданная явно.</summary>
  [CustomDescription("Attribute.Interfaces.Document_400")] rtSelectedRelation,
  /// <summary>Получать ссылку на связь у родителя. Ид связи БД извлекается из ссылки у родительского узла.</summary>
  [CustomDescription("Attribute.Interfaces.Document_401")] rtUseParentRelationLink,
  /// <summary>Получать ссылку на связь у документа. Ид связи БД извлекается из ссылки у документа.</summary>
  [CustomDescription("Attribute.Interfaces.Document_402")] rtUseParentDocumentRelationLink,
  /// <summary>Получать ссылку на объект через атрибут объекта документа</summary>
  [CustomDescription("Attribute.Interfaces.Document_570")] rtUseLinkFromDocumentObjectAttribute,
  /// <summary>Получать ссылку на ЭЦП объекта документа</summary>
  [CustomDescription("Attribute.Interfaces.Document_193")] rtUseSignFromDocument,
  /// <summary>Получать ссылку на ЭЦП объекта системы</summary>
  [CustomDescription("Attribute.Interfaces.Document_194")] rtUseSignFromObject,
  /// <summary>Получать ссылку на подпись через атрибут объекта документа</summary>
  [CustomDescription("Attribute.Interfaces.Document_196")] rtUseLinkFromDocumentObjectSign,
}
