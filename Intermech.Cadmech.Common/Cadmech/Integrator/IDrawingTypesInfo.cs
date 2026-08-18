// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.IDrawingTypesInfo
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Позволяет работать с настройками чертежей, относящихся к определенноу схеме обработки чертежей.
/// </summary>
public interface IDrawingTypesInfo
{
  /// <summary>
  /// Возвращает признак поддержки этой схемы обработки чертежей.
  /// </summary>
  bool IsEnabled { get; }

  /// <summary>
  /// Проверяет, включена ли поддержка этой схемы обработки чертежей. Если нет, то метод сбрасывает исключение.
  /// </summary>
  /// <exception cref="T:Intermech.FaultException">Поддержка схемы обработки чертежей отключена в настройках интегратора</exception>
  void CheckEnabled();

  /// <summary>
  /// Возвращает список всех типов чертежей, обрабатываемых в рамках данной схемы.
  /// </summary>
  /// <returns>Список типов документов</returns>
  List<LocalId<int>> GetAllDocumentTypes();

  /// <summary>
  /// Позволяет проверить, поддерживает ли схема указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Результат проверки</returns>
  bool IsGroupSupported(Guid groupType);

  /// <summary>
  /// Возвращает идентификаторы типов документов, входящих в указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Список идентификаторов типов документов</returns>
  /// <exception cref="T:System.InvalidOperationException">Не удалось найти группу документов по указанному идентификатору</exception>
  List<LocalId<int>> GetDrawingTypesByGroupType(Guid groupType);

  /// <summary>
  /// Выполняет определение группы чертежей по идентификатору типа чертежа.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <param name="throwIfNotFound">Признак, нужно ли сбрасывать исключение, если определить группу не удалось</param>
  /// <returns>Идентификатор группы чертежей или Guid.Empty</returns>
  /// <exception cref="T:System.InvalidOperationException">Не удалось определить группу</exception>
  Guid GetGroupTypeByDrawingType(int documentType, bool throwIfNotFound);

  /// <summary>
  /// Выполняет поиск настроек для типа чертежей по идентификатору типа чертежей.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Найденные настройки или null</returns>
  DrawingTypeSettings FindSettings(int documentType);

  /// <summary>
  /// Выполняет поиск настроек для типа чертежей по идентификатору типа чертежей.
  /// Если указанный тип чертежей не найден в настройках интегратора, то метод сбрасывает исключение.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Найденные настройки</returns>
  /// <exception cref="T:System.InvalidOperationException">Не найдены настройки для указанного типа чертежей</exception>
  DrawingTypeSettings GetSettings(int documentType);
}
