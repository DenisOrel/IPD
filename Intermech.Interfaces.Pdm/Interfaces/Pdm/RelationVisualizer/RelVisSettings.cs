// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelationVisualizer.RelVisSettings
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm.RelationVisualizer;

/// <summary>Служба получения настроек Визуализатора связей</summary>
public class RelVisSettings : IRelVisSettings
{
  /// <summary>Название модуля - "Intermech.Interfaces.Pdm"</summary>
  protected static string _moduleName = "IPS.Interfaces.Pdm";
  /// <summary>Название секции - "RelationVisualiser"</summary>
  protected static string _sectionName = "RelationVisualiser";
  /// <summary>Максимальная отображаемая длина заголовка</summary>
  protected static string _keyMaxCaptionLength = "RV_MaxCaptionLength";
  /// <summary>Загружать ли из БД невидимую часть дерева</summary>
  protected static string _keyisReadUnvisibleTree = "RV_isReadUnvisibleTree";
  /// <summary>Разрешать ли создавать связи мышкой</summary>
  protected static string _keyisInteractiveRelationCreator = "RV_isInteractiveRelationCreator";
  /// <summary>Формула именования, если у объекта нет заголовка</summary>
  protected static string _keynoExistCaptionFormula = "RV_noExistCaptionFormula";
  /// <summary>Пользовательские настройки</summary>
  private UserSettings settings = new UserSettings();

  /// <summary>Конструктор</summary>
  /// <param name="session"></param>
  public RelVisSettings(IUserSession session) => this.LoadSettings(session);

  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если настройки успешно загружены</returns>
  public bool LoadSettings(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    this.settings.MaxCaptionLength = Convert.ToUInt32(configurations.ReadInteger(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keyMaxCaptionLength, 12L, DBConfigMode.UserOnly));
    this.settings.NeedInvisibleTree = Convert.ToBoolean(configurations.ReadBool(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keyisReadUnvisibleTree, true, DBConfigMode.UserOnly));
    this.settings.allowCreatingRelations = Convert.ToBoolean(configurations.ReadBool(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keyisInteractiveRelationCreator, false, DBConfigMode.UserOnly));
    this.settings.NoCaptionFormula = (RelVisPred.NoCaptionFormula) Convert.ToUInt32(configurations.ReadInteger(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keynoExistCaptionFormula, 0L, DBConfigMode.UserOnly));
    return true;
  }

  /// <summary>Внести изменения в глобальную конфигурацию системы</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если изменения успешно внесены</returns>
  public bool SaveSettings(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    configurations.WriteInteger(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keyMaxCaptionLength, (long) this.settings.MaxCaptionLength);
    configurations.WriteBool(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keyisReadUnvisibleTree, this.settings.NeedInvisibleTree);
    configurations.WriteBool(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keyisInteractiveRelationCreator, this.settings.allowCreatingRelations);
    configurations.WriteInteger(RelVisSettings._moduleName, RelVisSettings._sectionName, RelVisSettings._keynoExistCaptionFormula, (long) this.settings.NoCaptionFormula);
    return true;
  }

  /// <summary>Пользовательски енастройки</summary>
  public UserSettings Settings
  {
    get => this.settings;
    set => this.settings = value;
  }
}
