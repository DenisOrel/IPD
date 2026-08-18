// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IEditableReferenceToObject
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на объект, которую можно редактировать специальным диалогом</summary>
public interface IEditableReferenceToObject
{
  /// <summary>Получить подтипы ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Массив имен подтипов ссылки. Имена должны быть уникальными в пределах одного типа ссылки</returns>
  string[] GetReferenceSubTypes(DocumentTreeNode owner, Type refInterface);

  /// <summary>Установить заданный подтип ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="subType">Имя подтипа ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  void SetReferenceSubType(DocumentTreeNode owner, string subType, Type refInterface);

  /// <summary>Получить индекс текущего подтипа ссылки</summary>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Индекс текущего подтипа ссылки</returns>
  int GetReferenceSubTypeIndex(Type refInterface);

  /// <summary>Заголовок объекта с которым связана ссылка. Если объект не найден, то null</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_373")]
  [CustomDescription("Attribute.Interfaces.Document_374")]
  [CustomCategory("Attribute.Interfaces.Document_375")]
  string ObjectCaption { get; }

  /// <summary>Можно вызвать диалог выбора объекта для ссылки</summary>
  [Browsable(false)]
  bool CanCallSelectObjectDialog { get; }

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  void CallSelectObjectDialog();

  /// <summary>Можно вызвать диалог выбора ссылочного атрибута</summary>
  [Browsable(false)]
  bool CanCallSelectLinkAttributeDialog { get; }

  /// <summary>Вызвать диалог выбора ссылочного атрибута</summary>
  void CallSelectLinkAttributeDialog();

  /// <summary>Используется ссылочный атрибут</summary>
  [Browsable(false)]
  bool UseLinkAttribute { get; }

  /// <summary>Имя ссылочного атрибута</summary>
  [Browsable(false)]
  string LinkAttributeName { get; set; }

  /// <summary>Получить список имен ссылочных атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  string[] GetLinkAttributeNameList();
}
