
// Type: Intermech.Tools.LaunchActions.ExtAppSettingsCodec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;
using System.Diagnostics;
using System.Xml;


namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Реализует кодек настроек для команды запуска приложения операционной системы. Он позволяет
/// преобразовывать форму настроек из объекта .NET в xml-документ и обратно.
/// </summary>
public sealed class ExtAppSettingsCodec : LaunchActionSettingsCodec
{
  protected override int GetEncoderFormatVersion() => 1;

  public override ISettingsObject CreateEmptySettings() => (ISettingsObject) new ExtAppSettings();

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
    ExtAppSettings extAppSettings = (ExtAppSettings) settingsObject;
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("AppName", extAppSettings.ApplicationName));
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("Executable", extAppSettings.Executable));
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("Arguments", extAppSettings.Arguments));
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("WorkDirectory", extAppSettings.WorkDirectory));
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("WindowStyle", Enum.GetName(typeof (ProcessWindowStyle), (object) extAppSettings.WindowStyle)));
  }

  protected override string GetActionDisplayName(ISettingsObject settingsObject)
  {
    return ((ExtAppSettings) settingsObject).ApplicationName;
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
    ExtAppSettings settings = (ExtAppSettings) settingsObject;
    if (formatVersion == 1)
      this.DecodeV1(settingsBuilder, settings);
    else
      base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
  }

  private void DecodeV1(SettingsXmlBuilder settingsBuilder, ExtAppSettings settings)
  {
    settings.ApplicationName = settingsBuilder.DecodeText("AppName", string.Empty);
    settings.Executable = settingsBuilder.DecodeText("Executable", string.Empty);
    settings.Arguments = settingsBuilder.DecodeText("Arguments", string.Empty);
    settings.WorkDirectory = settingsBuilder.DecodeText("WorkDirectory", string.Empty);
    settings.WindowStyle = (ProcessWindowStyle) Enum.Parse(typeof (ProcessWindowStyle), settingsBuilder.DecodeText("WindowStyle", "Maximized"));
  }
}
