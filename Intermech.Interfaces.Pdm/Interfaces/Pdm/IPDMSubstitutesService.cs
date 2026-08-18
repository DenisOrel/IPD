// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IPDMSubstitutesService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс, позволяющий вызывать команды для работы с допустимыми заменителями
/// </summary>
public interface IPDMSubstitutesService
{
  /// <summary>
  /// Может ли плагин предоставлять возможности, доступные в русскоязычных странах
  /// (например, отображать странички и колонки, связанные с расшифровкой допустимых замен, т.п.)
  /// </summary>
  bool CanUseRussianFeatures { get; }

  /// <summary>Создать группу заменителей</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации (приводится к типу ISelectedItems).</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <param name="additionalInfo">Дополнительные данные</param>
  void CreateSubstitutesGroup(
    object items,
    IServiceProvider viewServices,
    object additionalInfo,
    long desiredGroupNumber = -1);

  /// <summary>Сделать заменитель актуальным</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации (приводится к типу ISelectedItems).</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <param name="additionalInfo">Дополнительные данные</param>
  void MakeActualSubstitute(object items, IServiceProvider viewServices, object additionalInfo);

  /// <summary>Настроить группу заменителей</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации (приводится к типу ISelectedItems).</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <param name="additionalInfo">Дополнительные данные</param>
  void EditSubstitutesGroup(object items, IServiceProvider viewServices, object additionalInfo);

  /// <summary>Удалить группу заменителей</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации (приводится к типу ISelectedItems).</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <param name="additionalInfo">Дополнительные данные</param>
  void DeleteSubstitutesGroup(object items, IServiceProvider viewServices, object additionalInfo);

  /// <summary>
  /// Получить перечень доступных команд, работающих с допустимыми заменителями
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации (приводится к типу ISelectedItems).</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <returns>Перечислитель, в котором будут указаны допустимые команды</returns>
  PDMSubstitutesCommands GetEnabledSubstitutesCommands(object items, IServiceProvider viewServices);

  /// <summary>
  /// Получить перечень доступных команд, работающих с допустимыми заменителями
  /// </summary>
  /// <param name="parObjectType">Идентификатор родительского типа объектов (чей состав изучается)</param>
  /// <param name="relTypes">Список типов связей (состав в виде списка типов связей).</param>
  /// <returns>Перечислитель, в котором будут указаны допустимые команды</returns>
  PDMSubstitutesCommands GetEnabledSubstitutesCommands(int parObjectType, List<int> relTypes);
}
