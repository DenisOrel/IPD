
// Type: Intermech.Tools.LaunchActions.ShellVerbSettingsCodec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Tools.Settings;
using System.Xml;


namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Реализует кодек настроек для команды запуска приложения операционной системы средствами shell verb.
/// Он позволяет преобразовывать форму настроек из объекта .NET в xml-документ и обратно.
/// </summary>
public sealed class ShellVerbSettingsCodec : LaunchActionSettingsCodec
{
  protected override int GetEncoderFormatVersion() => 1;

  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new ShellVerbSettings();
  }

  /// <summary>
  /// Выполняет преобразование объекта с настройками в xml-документ.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками команды запуска приложения</param>
  /// <param name="settingsBuilder">Построитель xml</param>
  /// <returns>Настройки в форме xml-документа</returns>
  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    ShellVerbSettings shellVerbSettings = (ShellVerbSettings) settingsObject;
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("Verb", shellVerbSettings.Verb));
  }

  protected override string GetActionDisplayName(ISettingsObject settingsObject)
  {
    ShellVerbSettings shellVerbSettings = (ShellVerbSettings) settingsObject;
    return string.Format(LocalizationHolder.rm.GetString("Interfaces_700"), (object) shellVerbSettings.Verb);
  }

  /// <summary>
  /// Выполняет преобразование xml-документа в объект с настройками.
  /// </summary>
  /// <param name="xmlDocument">Настройки в форме xml-документа</param>
  /// <returns>Объект с настройками команды запуска приложения</returns>
  protected override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    ShellVerbSettings settings = (ShellVerbSettings) settingsObject;
    if (formatVersion == 1)
      this.DecodeV1(settingsBuilder, settings);
    else
      base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
  }

  private void DecodeV1(SettingsXmlBuilder settingsBuilder, ShellVerbSettings settings)
  {
    settings.Verb = settingsBuilder.DecodeText("Verb", string.Empty);
  }
}
