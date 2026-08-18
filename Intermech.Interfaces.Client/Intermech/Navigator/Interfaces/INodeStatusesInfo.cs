// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeStatusesInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Необязательный интерфейс, предназначенный для обработки виртуальной
/// колонки навигатора "Статусы элемента". Эта колонка предназначена для
/// отображения группы иконок размером 16x16, каждая из которых представляет
/// один из статусов элемента навигации. При наведении мышки любую из иконок
/// навигатор показывает ее описание во всплывающей подсказке. Этот интерфейс
/// позволяет навигатору получать иконки для отображения, а также их описания.
/// </summary>
public interface INodeStatusesInfo
{
  /// <summary>
  /// Перечитать состояние сервиса, список статусов, значки и описания
  /// </summary>
  void Reload();

  /// <summary>
  /// Возвращает массив иконок размером 16x16, каждая из которых
  /// представляет один из статусов элемента навигации.
  /// </summary>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <param name="columnValue">Значение колонки</param>
  /// <returns>Массив иконок статусов</returns>
  Image[] GetIcons(INodeID nodeId, object columnValue);

  /// <summary>
  /// Возвращает текстовое описание иконки статуса. Если описания нет,
  /// то возвращает пустую строку.
  /// </summary>
  /// <remarks>
  /// Индекс иконки может быть больше количества иконок, отображенных
  /// на экране, т.к. навигатор вычисляет индекс указанной пользователем
  /// иконки по положению указателя мышки на экране.
  /// </remarks>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <param name="columnValue">Значение колонки</param>
  /// <param name="iconIndex">Порядковый номер иконки</param>
  /// <returns>Текстовое описание статуса</returns>
  string GetDescription(
    IServiceProvider services,
    INodeID nodeId,
    object columnValue,
    int iconIndex);

  /// <summary>
  /// Возвращает шрифт для указанной ячейки, если есть какие-то проблемы с её содержимым, или null
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <param name="columnValue">Значение колонки</param>
  /// <param name="parentFont">Текущий шрифт</param>
  /// <returns>Шрифт или null, если не требуется выделение особым шрифтом</returns>
  Font GetFont(IServiceProvider services, INodeID nodeId, object columnValue, Font parentFont);
}
