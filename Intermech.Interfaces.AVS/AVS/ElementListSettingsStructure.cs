// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.ElementListSettingsStructure
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Описание уровней настроек перечня элементов</summary>
public class ElementListSettingsStructure : SettingsStructure
{
  /// <summary>Уровень настроек документа. По умолчанию данные настройки распространяются на все документы выпущенные на основе данного шаблона</summary>
  private SettingsLevel _templateLevel;
  /// <summary>Уровень конкретного экземпляра документа</summary>
  private SettingsLevel _documentLevel;

  /// <summary> Инициализация варианта структуры настроек </summary>
  protected override void Init()
  {
    this._templateLevel = new SettingsLevel((SettingsStructure) this, InheritanceSettingsLevel.Template, "Настройки шаблона перечня элементов", AVSDocumentType.ElementList);
    this._documentLevel = new SettingsLevel(this._templateLevel, InheritanceSettingsLevel.Document, "Настройки перечня элементов", AVSDocumentType.ElementList);
    this._allLevels = new SettingsLevel[2]
    {
      this._templateLevel,
      this._documentLevel
    };
  }
}
