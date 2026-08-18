// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.IDocEditorPropertyPage
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Элемент для отображения параметров настройки</summary>
public interface IDocEditorPropertyPage
{
  /// <summary>Событие об изменении свойств на странице</summary>
  event EventHandler Changed;

  /// <summary>Тип страницы</summary>
  DocEditorPropertyPageType Type { get; }

  /// <summary>Объект для отображения свойств</summary>
  object Control { get; }

  /// <summary>Имя страницы</summary>
  string PageName { get; }

  /// <summary>Сохранение изменений</summary>
  void Apply();

  /// <summary>Отмена изменений</summary>
  void Cancel();

  /// <summary>id раздела справки для данного элемента управления</summary>
  string HelpTopicID { get; }

  /// <summary>Текст заголовка (пустое значение - заголовок не отображается)</summary>
  string HeaderText { get; }
}
