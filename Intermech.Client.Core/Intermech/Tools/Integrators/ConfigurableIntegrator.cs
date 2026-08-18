
// Type: Intermech.Tools.Integrators.ConfigurableIntegrator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;


namespace Intermech.Tools.Integrators;

public abstract class ConfigurableIntegrator : IntegratorBase
{
  /// <summary>Возвращает сервис настроек интегратора.</summary>
  /// <returns>Сервис настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Сервис не реализован в интеграторе</exception>
  protected abstract IPersistentIntegratorSettingsService GetSettingsService();

  /// <summary>
  /// Возвращает необязательный сервис моделей представления для настроек интегратора.
  /// </summary>
  /// <returns>Сервис моделей представления или null</returns>
  protected abstract IIntegratorSettingsViewModelService TryGetSettingsViewModelService();

  /// <summary>
  /// Создает и возвращает визуальный редактор настроек интегратора.
  /// </summary>
  /// <returns>Элемент управления</returns>
  public override DataEditorControl CreateSettingsEditor()
  {
    IPersistentIntegratorSettingsService settingsService = this.GetSettingsService();
    IIntegratorSettingsViewModelService viewModelService = this.TryGetSettingsViewModelService();
    IntegratorSettingsPropertyEditor settingsEditor = new IntegratorSettingsPropertyEditor();
    settingsEditor.Initialize((IIntegrator) this, settingsService, viewModelService);
    return (DataEditorControl) settingsEditor;
  }

  /// <summary>
  /// Возвращает шаблон для серверного объекта интегратора в форме xml-документа.
  /// Он используется при создании нового объекта интегратора в базе IPS.
  /// </summary>
  public override string GetServerObjectTemplate()
  {
    return new EmptySettingsCodec(this.DisplayName).Encode((ISettingsObject) new EmptySettingsCodec.EmptySettings()).OuterXml;
  }
}
