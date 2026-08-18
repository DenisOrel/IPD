// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ProcessingSchemaSettings
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
/// Реализует базовый класс для настроек одной конкретной схемы обработки чертежей.
/// </summary>
public abstract class ProcessingSchemaSettings : IDrawingTypesInfo
{
  private bool isEnabled;

  /// <summary>
  /// Включает и выключает поддержку этой схемы обработки чертежей.
  /// </summary>
  public bool IsEnabled
  {
    get => this.isEnabled;
    set => this.isEnabled = value;
  }

  /// <summary>
  /// Проверяет, включена ли поддержка этой схемы обработки чертежей. Если нет, то метод сбрасывает исключение.
  /// </summary>
  /// <exception cref="T:Intermech.FaultException">Поддержка схемы обработки чертежей отключена в настройках интегратора</exception>
  public void CheckEnabled()
  {
    if (!this.IsEnabled)
      throw new FaultException(this.GetNotEnabledErrorMessage());
  }

  /// <summary>
  /// Возвращает текст сообщения о том, что эта схема обработки чертежей отключена в настройках интегратора.
  /// </summary>
  /// <returns></returns>
  protected abstract string GetNotEnabledErrorMessage();

  /// <summary>
  /// Возвращает список всех типов чертежей, обрабатываемых в рамках данной схемы.
  /// </summary>
  /// <returns>Список типов документов</returns>
  public abstract List<LocalId<int>> GetAllDocumentTypes();

  /// <summary>
  /// Позволяет проверить, поддерживает ли схема указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Результат проверки</returns>
  public abstract bool IsGroupSupported(Guid groupType);

  /// <summary>
  /// Возвращает идентификаторы типов документов, входящих в указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Список идентификаторов типов документов</returns>
  /// <exception cref="T:System.InvalidOperationException">Не удалось найти группу документов по указанному идентификатору</exception>
  public virtual List<LocalId<int>> GetDrawingTypesByGroupType(Guid groupType)
  {
    throw new InvalidOperationException("Не удалось найти группу документов по указанному идентификатору.");
  }

  /// <summary>
  /// Выполняет определение группы чертежей по идентификатору типа чертежа.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <param name="throwIfNotFound">Признак, нужно ли сбрасывать исключение, если определить группу не удалось</param>
  /// <returns>Идентификатор группы чертежей или Guid.Empty</returns>
  public Guid GetGroupTypeByDrawingType(int documentType, bool throwIfNotFound)
  {
    this.CheckEnabled();
    Guid groupType = this.DoGetGroupType(documentType);
    if (groupType != Guid.Empty || !throwIfNotFound)
      return groupType;
    throw new InvalidOperationException("Не удалось определить группу чертежей по идентификатору чертежа.");
  }

  /// <summary>
  /// Реализует определение группы чертежей по идентификатору типа чертежа.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Идентификатор группы чертежей или Guid.Empty</returns>
  protected virtual Guid DoGetGroupType(int documentType) => Guid.Empty;

  /// <summary>
  /// Выполняет поиск настроек для типа чертежей по идентификатору типа чертежей.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Найденные настройки или null</returns>
  public DrawingTypeSettings FindSettings(int documentType)
  {
    return this.isEnabled ? this.DoFindSettings(documentType) : (DrawingTypeSettings) null;
  }

  /// <summary>
  /// Реализует поиск настроек для типа чертежей по идентификатору типа чертежей.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Найденные настройки или null</returns>
  protected virtual DrawingTypeSettings DoFindSettings(int documentType)
  {
    return (DrawingTypeSettings) null;
  }

  /// <summary>
  /// Выполняет поиск настроек для типа чертежей по идентификатору типа чертежей.
  /// Если указанный тип чертежей не найден в настройках интегратора, то метод сбрасывает исключение.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Найденные настройки</returns>
  /// <exception cref="T:System.InvalidOperationException">Не найдены настройки для указанного типа чертежей</exception>
  public DrawingTypeSettings GetSettings(int documentType)
  {
    this.CheckEnabled();
    return this.FindSettings(documentType) ?? throw new InvalidOperationException($"Тип объектов '{documentType}' не является типом чертежа AutoCAD.");
  }
}
