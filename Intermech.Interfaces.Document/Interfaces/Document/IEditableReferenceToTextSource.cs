// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IEditableReferenceToTextSource
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на объекта источник текста, которую можно редактировать специальным диалогом</summary>
public interface IEditableReferenceToTextSource : IEditableReferenceToObject
{
  /// <summary>Ссылка на атрибут объекта</summary>
  [Browsable(false)]
  bool IsReferenceToAttribute { get; }

  /// <summary>Имя атрибута</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_376")]
  [CustomDescription("Attribute.Interfaces.Document_377")]
  [CustomCategory("Attribute.Interfaces.Document_378")]
  string AttributeName { get; set; }

  /// <summary>Можно вызвать диалог выбора атрибута для ссылки</summary>
  [Browsable(false)]
  bool CanCallSelectAttributeDialog { get; }

  /// <summary>Вызвать диалог выбора атрибута для ссылки</summary>
  void CallSelectAttributeDialog();

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  string[] GetAttributeNameList();

  /// <summary>Обновить информацию об атрибуте. Имеет смысл для ссылок на атрибуты объектов БД.</summary>
  void UpdateAttributeInfo();
}
