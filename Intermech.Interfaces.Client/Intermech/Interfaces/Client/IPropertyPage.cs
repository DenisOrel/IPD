// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPropertyPage
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Элемент для отображения параметров настройки</summary>
public interface IPropertyPage
{
  /// <summary>Событие об изменении свойств на странице</summary>
  event EventHandler Changed;

  /// <summary>Тип страницы</summary>
  PropertyPageType Type { get; }

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

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  string HeaderText { get; }
}
