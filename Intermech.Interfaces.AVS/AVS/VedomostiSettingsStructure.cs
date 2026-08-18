// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.VedomostiSettingsStructure
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary> Описание уровней настроек ведомостей </summary>
public class VedomostiSettingsStructure : SettingsStructure
{
  private static VedomostiSettingsStructure _instance = new VedomostiSettingsStructure();
  /// <summary>Пока что единственный уровень настроек</summary>
  public SettingsLevel _commonTemplateLevel;

  /// <summary>Пока что единственный уровень настроек</summary>
  public static SettingsLevel CommonTemplateLevel
  {
    get => VedomostiSettingsStructure.Instance._commonTemplateLevel;
  }

  /// <summary>Ссылка на экземпляр</summary>
  public static VedomostiSettingsStructure Instance => VedomostiSettingsStructure._instance;

  /// <summary> Инициализация варианта структуры настроек </summary>
  protected override void Init()
  {
    this._commonTemplateLevel = new SettingsLevel((SettingsStructure) this, InheritanceSettingsLevel.CommonTemplate, "Общие настройки", AVSDocumentType.UserAVSDocument);
    this._allLevels = new SettingsLevel[1]
    {
      this._commonTemplateLevel
    };
  }

  protected static SettingsStructure settingsStructure
  {
    get => (SettingsStructure) VedomostiSettingsStructure._instance;
  }

  /// <summary> Получение дескриптора уровня настроек по идентификатору типа их сохраняющего </summary>
  /// <param name="typeID"> Идентификатор типа </param>
  /// <returns> Дескриптора уровня настроек </returns>
  public static SettingsLevel GetSettingsLevelForObjType(long typeID)
  {
    return VedomostiSettingsStructure.settingsStructure._allLevels[0];
  }

  /// <summary>Создание объекта с настройками
  /// связанного с некоторым объектом (спецификацией, шаблоном и т.п.)</summary>
  /// <param name="iUserSession"></param>
  /// <param name="objectID">ID объекта в атрибутах которого хранятся настройки</param>
  /// <param name="objectType">ID типа переданного объекта (-1 если неизвестен). Ускоряет работу</param>
  /// <param name="templateID">ID шаблона СП (-1 если это не СП)</param>
  /// <param name="settingsHolderAttributeID">ID Атрибута который хранит настройки сортировки в XML формате</param>
  /// <param name="settingsType">Тип объекта-контейнера создаваемых настроек</param>
  /// <returns> Объект "Настройки нумерации позиций" </returns>
  public static object CreateSettingsLevelFromObject(
    IUserSession iUserSession,
    long objectID,
    int objectType,
    long templateID,
    int settingsHolderAttributeID,
    Type settingsType)
  {
    return VedomostiSettingsStructure.settingsStructure.CreateSettingsLevelFromObject(iUserSession, objectID, objectType, templateID, settingsHolderAttributeID, settingsType);
  }
}
