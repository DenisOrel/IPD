// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPropertyPagesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Сервис управления страницами свойств</summary>
public interface IPropertyPagesService
{
  /// <summary>
  /// Событие возникает, когда на страничке свойств вносятся изменения в текущие настройки
  /// </summary>
  event EventHandler Changed;

  /// <summary>Добавить страницу свойств</summary>
  /// <param name="path">Путь и название страницы. В качестве разделителя используется '\'</param>
  /// <param name="page">Страница свойств</param>
  void AddPage(string path, IPropertyPage page);
}
