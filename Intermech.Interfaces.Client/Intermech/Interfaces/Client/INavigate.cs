// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INavigate
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс для поддержки навигации по иерархии.</summary>
public interface INavigate
{
  /// <summary>Состояние навигатора изменилось</summary>
  event EventHandler Changed;

  /// <summary>Переход на один уровень назад</summary>
  void Back();

  /// <summary>Переход назад на n-шагов</summary>
  /// <param name="steps">Количество шагов</param>
  void Back(int steps);

  /// <summary>Переход на один уровень вперед</summary>
  void Forward();

  /// <summary>Переход вперед на n-шагов</summary>
  /// <param name="steps">Количество шагов</param>
  void Forward(int steps);

  /// <summary>Возможен переход назад</summary>
  bool CanBack { get; }

  /// <summary>Возможен переход вперед</summary>
  bool CanForward { get; }

  /// <summary>Строчка хинта для перехода назад</summary>
  string BackName { get; }

  /// <summary>Строчка хинта для перехода вперед</summary>
  string ForwardName { get; }

  /// <summary>Строчки для выпадающего меню перехода назад</summary>
  string[] BackNames { get; }

  /// <summary>Строчки для выпадающего меню перехода вперед</summary>
  string[] ForwardNames { get; }
}
