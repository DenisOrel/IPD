// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Позволяет реализовать закладку, встраиваемую в навигатор. Закладка представляет собой
/// элемент управления (потомок от System.Windows.Forms), поддерживающий этот интерфейс.
/// С его помощью навигатор инициализирует закладку, сообщает о переключении между закладками,
/// а также получает справочную информацию о названии закладки, ее иконке и положении среди
/// других закладок.
/// </summary>
public interface IView
{
  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  void Initialize(ISelectedItems items, IServiceProvider provider);

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  void Activate(IView previousView);

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.
  /// </param>
  void Deactivate(IView nextView);

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  [Obsolete("Use IViewDescriptionProvider.", false)]
  string Caption { get; }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  [Obsolete("Use IViewDescriptionProvider.", false)]
  int ImageIndex { get; }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  [Obsolete("Use IViewDescriptionProvider.", false)]
  int OrderID { get; }
}
