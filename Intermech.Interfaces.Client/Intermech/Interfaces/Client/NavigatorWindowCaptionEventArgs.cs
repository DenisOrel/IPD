// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorWindowCaptionEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события, собирающего информацию о заголовке окна "Навигатора"
/// </summary>
public class NavigatorWindowCaptionEventArgs : EventArgs, IAssignable, ICloneable
{
  /// <summary>
  /// Корневой дескриптор, на основании которого построено содержимое окна
  /// </summary>
  private IDescriptor _rootDescriptor;
  /// <summary>Контейнер сервисов, которые используются в окне</summary>
  private IServiceProvider _services;
  /// <summary>Заголовок окна</summary>
  public string Text;
  /// <summary>Дополнительная информация для заголовка</summary>
  public string ExtraText;
  /// <summary>Текстовая подсказка для заголовка окна</summary>
  public string TextHint;

  /// <summary>
  /// Корневой дескриптор, на основании которого построено содержимое окна
  /// </summary>
  public IDescriptor RootDescriptor
  {
    [DebuggerStepThrough] get => this._rootDescriptor;
  }

  /// <summary>Контейнер сервисов, которые используются в окне</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
  }

  /// <summary>
  /// Создать аргументы для события NavigatorWindowCaptionEventHandler
  /// </summary>
  /// <param name="rootDescriptor">Корневой дескриптор, на основании которого построено содержимое окна</param>
  /// <param name="services">Контейнер сервисов, которые используются в окне</param>
  /// <param name="text">Заголовок окна</param>
  /// <param name="extraText">Дополнительная информация для заголовка</param>
  /// <param name="textHint">Текстовая подсказка для заголовка окна</param>
  public NavigatorWindowCaptionEventArgs(
    IDescriptor rootDescriptor,
    IServiceProvider services,
    string text,
    string extraText,
    string textHint)
  {
    this._rootDescriptor = rootDescriptor;
    this._services = services;
    this.Text = text;
    this.ExtraText = extraText;
    this.TextHint = textHint;
  }

  /// <summary>
  /// Создать аргументы для события NavigatorWindowCaptionEventHandler, заполнить
  /// аргументы из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public NavigatorWindowCaptionEventArgs(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._rootDescriptor = (IDescriptor) null;
    this._services = (IServiceProvider) null;
    this.Text = string.Empty;
    this.ExtraText = string.Empty;
    this.TextHint = string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is NavigatorWindowCaptionEventArgs captionEventArgs))
      return;
    this._rootDescriptor = captionEventArgs._rootDescriptor;
    this._services = captionEventArgs._services;
    this.Text = captionEventArgs.Text;
    this.ExtraText = captionEventArgs.ExtraText;
    this.TextHint = captionEventArgs.TextHint;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new NavigatorWindowCaptionEventArgs((object) this);
}
