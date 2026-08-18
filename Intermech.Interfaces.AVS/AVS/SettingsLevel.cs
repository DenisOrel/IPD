// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SettingsLevel
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Описание уровня настроек</summary>
public class SettingsLevel
{
  /// <summary>Наименование уровня настроек</summary>
  private string _LevelName;
  /// <summary>Вышестоящий уровень настроек</summary>
  private SettingsLevel _ParentLevel;
  /// <summary>Иерархия структуры настроек</summary>
  private SettingsStructure _settingsLevelStructure;
  /// <summary>Уровень наследования настроек документов</summary>
  public InheritanceSettingsLevel InheritanceLevel;
  /// <summary>Тип документа</summary>
  public AVSDocumentType DocumentType;

  /// <summary> Конструктор для корневого дескриптора уровня настроек </summary>
  /// <param name="settingsLevelStructure">Иерархия структуры настроек</param>
  /// <param name="inheritanceLevel">Уровень наследования настроек документов</param>
  /// <param name="levelName">Наименование уровня настроек</param>
  /// <param name="documentType">Тип документа</param>
  public SettingsLevel(
    SettingsStructure settingsLevelStructure,
    InheritanceSettingsLevel inheritanceLevel,
    string levelName,
    AVSDocumentType documentType)
    : this((SettingsLevel) null, inheritanceLevel, levelName, documentType)
  {
    this._settingsLevelStructure = settingsLevelStructure;
  }

  /// <summary> Конструктор для дочернего дескриптора уровня настроек </summary>
  /// <param name="parentLevel">Вышестоящий уровень настроек</param>
  /// <param name="inheritanceLevel">&gt;Уровень наследования настроек документов</param>
  /// <param name="levelName">Наименование уровня настроек </param>
  /// <param name="documentType">Тип документа</param>
  public SettingsLevel(
    SettingsLevel parentLevel,
    InheritanceSettingsLevel inheritanceLevel,
    string levelName,
    AVSDocumentType documentType)
  {
    this._ParentLevel = parentLevel;
    this.InheritanceLevel = inheritanceLevel;
    if (parentLevel != null)
      this._settingsLevelStructure = parentLevel.SettingsStructure;
    this._LevelName = levelName;
    this.DocumentType = documentType;
  }

  /// <summary>Наименование уровня настроек</summary>
  public string LevelName => this._LevelName;

  public override string ToString() => this._LevelName;

  /// <summary> Ссылка на вышестоящий уровень настроек </summary>
  public SettingsLevel ParentLevel => this._ParentLevel;

  /// <summary>Ссылка на структуру иерархии настроек в целом</summary>
  public SettingsStructure SettingsStructure => this._settingsLevelStructure;

  /// <summary>Признак того, что данный уровень настроек - корневой</summary>
  public bool IsRoot => this._ParentLevel == null;
}
