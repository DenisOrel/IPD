// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AutopromSpecificationSettingsStructure
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Описание уровней настроек автомобилестроительной спецификации</summary>
public class AutopromSpecificationSettingsStructure : SettingsStructure
{
  /// <summary>Общий уровень, настройки, которые по-умолчанию распространяются на все объекты к которыми применимы данные настройки</summary>
  private SettingsLevel _commonTemplateLevel;
  /// <summary>Уровень настроек шаблона спецификаций. По умолчанию данные настройки распространяются на все спецификации выпущенные на основе данного шаблона</summary>
  private SettingsLevel _templateLevel;
  /// <summary>Уровень конкретного экземпляра документа</summary>
  private SettingsLevel _documentLevel;

  /// <summary>Инициализация варианта структуры настроек</summary>
  protected override void Init()
  {
    this._commonTemplateLevel = new SettingsLevel((SettingsStructure) this, InheritanceSettingsLevel.CommonTemplate, "Общие настройки спецификаций", AVSDocumentType.AutoIndustrySpecification);
    this._templateLevel = new SettingsLevel(this._commonTemplateLevel, InheritanceSettingsLevel.Template, "Настройки шаблона спецификации", AVSDocumentType.AutoIndustrySpecification);
    this._documentLevel = new SettingsLevel(this._templateLevel, InheritanceSettingsLevel.Document, "Настройки спецификации", AVSDocumentType.AutoIndustrySpecification);
    this._allLevels = new SettingsLevel[3]
    {
      this._commonTemplateLevel,
      this._templateLevel,
      this._documentLevel
    };
  }
}
