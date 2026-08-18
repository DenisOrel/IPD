// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPSettings
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Localization;
using Intermech.MRP2;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Настройки MRP-системы</summary>
public class MRPSettings : LongLifeObject, IMRPSettings
{
  /// <summary>Название модуля - "Intermech.MRP"</summary>
  protected static string _moduleName = "Intermech.MRP";
  /// <summary>Название секции - "MRP_PO"</summary>
  protected static string _sectionName = "MRP_PO";
  /// <summary>
  /// Учитывать контекст составов - позволяет включать в состав производственного
  /// заказа записи с указанными значениями контекстов состава. По умолчанию используются
  /// записи Общего и Производственного контекстов состава.
  /// </summary>
  protected bool _useCompositionContext;
  /// <summary>
  /// Выбрать заменители - значение True требует указать для каждого случая допустимых замен
  /// определённый заменитель. По умолчанию значение False, и состав производственного
  /// заказа формируется по актуальным заменителям.
  /// </summary>
  protected bool _useSubstitutes;
  /// <summary>
  /// Выбрать заменители - значение True требует указать для каждого случая допустимых замен
  /// определённый заменитель. По умолчанию значение False, и состав производственного
  /// заказа формируется по актуальным заменителям.
  /// </summary>
  protected bool _useDocumentation;
  /// <summary>
  /// Включать в состав производственных заказов составы покупных изделий - значение True указывает
  /// на необходимость включения в состав экземпляров и партий составы покупных партий и изделий.
  /// </summary>
  protected bool _useBoughtArticles;

  /// <summary>Создать пустой экземпляр класса</summary>
  public MRPSettings()
  {
  }

  /// <summary>
  /// Загрузить все настройки из глобальной конфигурации системы
  /// </summary>
  public MRPSettings(IUserSession session) => this.LoadSettings(session);

  /// <summary>
  /// Учитывать контекст составов - позволяет включать в состав производственного
  /// заказа записи с указанными значениями контекстов состава. По умолчанию используются
  /// записи Общего и Производственного контекстов состава.
  /// </summary>
  public virtual bool UseCompositionContext
  {
    [DebuggerStepThrough] get => this._useCompositionContext;
    set => this._useCompositionContext = value;
  }

  /// <summary>
  /// Выбрать заменители - значение True требует указать для каждого случая допустимых замен
  /// определённый заменитель. По умолчанию значение False, и состав производственного
  /// заказа формируется по актуальным заменителям.
  /// </summary>
  public virtual bool UseSubstitutes
  {
    [DebuggerStepThrough] get => this._useSubstitutes;
    set => this._useSubstitutes = value;
  }

  /// <summary>
  /// Включать в состав производственных заказов документацию - значение True указывает
  /// на необходимость включения в состав экземпляров и партий связей с версиями
  /// документации, которая выпущена на соответствующие изделия/комплектации.
  /// </summary>
  public virtual bool UseDocumentation
  {
    [DebuggerStepThrough] get => this._useDocumentation;
    set => this._useDocumentation = value;
  }

  /// <summary>
  /// Включать в состав производственных заказов составы покупных изделий - значение True указывает
  /// на необходимость включения в состав экземпляров и партий составы покупных партий и изделий.
  /// </summary>
  public bool UseBoughtArticles
  {
    [DebuggerStepThrough] get => this._useBoughtArticles;
    set => this._useBoughtArticles = value;
  }

  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если настройки успешно загружены</returns>
  public virtual bool LoadSettings(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    this._useCompositionContext = configurations.ReadBool(MRPSettings._moduleName, MRPSettings._sectionName, "CC", false, DBConfigMode.GlobalOnly);
    this._useSubstitutes = configurations.ReadBool(MRPSettings._moduleName, MRPSettings._sectionName, "Sb", false, DBConfigMode.GlobalOnly);
    this._useDocumentation = configurations.ReadBool(MRPSettings._moduleName, MRPSettings._sectionName, "Doc", false, DBConfigMode.GlobalOnly);
    this._useBoughtArticles = configurations.ReadBool(MRPSettings._moduleName, MRPSettings._sectionName, "BA", false, DBConfigMode.GlobalOnly);
    MRP2Consts.InitCopyTypesSettings(session);
    return true;
  }

  /// <summary>Внести изменения в глобальную конфигурацию системы</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если изменения успешно внесены</returns>
  public virtual bool SaveSettings(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    configurations.WriteBool(MRPSettings._moduleName, MRPSettings._sectionName, "CC", this._useCompositionContext, 0L);
    configurations.WriteBool(MRPSettings._moduleName, MRPSettings._sectionName, "Sb", this._useSubstitutes, 0L);
    configurations.WriteBool(MRPSettings._moduleName, MRPSettings._sectionName, "Doc", this._useDocumentation, 0L);
    configurations.WriteBool(MRPSettings._moduleName, MRPSettings._sectionName, "BA", this._useBoughtArticles, 0L);
    return true;
  }

  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// (серверная реализация)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <returns>true, если настройки успешно загружены</returns>
  public virtual bool LoadSettings(Guid sessionGuid)
  {
    throw new Exception(MRPLocalization.rm.GetString("Interfaces.MRP_1"));
  }

  /// <summary>
  /// Внести изменения в глобальную конфигурацию системы
  /// (серверная реализация)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <returns>true, если изменения успешно внесены</returns>
  public virtual bool SaveSettings(Guid sessionGuid)
  {
    throw new Exception(MRPLocalization.rm.GetString("Interfaces.MRP_2"));
  }
}
