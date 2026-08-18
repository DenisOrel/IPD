// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFIntegratorSettingsCodec
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators.Simple;
using Intermech.Tools.Settings;
using System.Xml;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFIntegratorSettingsCodec(string integratorName) : SingleFileSettingsCodec(integratorName)
{
  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new PDFIntegratorSettings();
  }

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    base.EncodeSettings(settingsObject, settingsBuilder);
    PDFIntegratorSettings settings = (PDFIntegratorSettings) settingsObject;
    settingsBuilder.AppendElement((XmlNode) this.EncodeOptions(settings, settingsBuilder));
  }

  private XmlElement EncodeOptions(
    PDFIntegratorSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("Options");
    settingsBuilder.AppendAttribute((XmlNode) element, "ProcessSubject", (object) settings.ProcessSubject);
    return element;
  }

  protected override void DecodeV1(
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    base.DecodeV1(settingsBuilder, settingsObject);
    PDFIntegratorSettings settings = (PDFIntegratorSettings) settingsObject;
    this.DecodeOptions(settingsBuilder, settings);
  }

  private void DecodeOptions(SettingsXmlBuilder settingsBuilder, PDFIntegratorSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("Options");
    if (parentNode == null)
      return;
    settings.ProcessSubject = settingsBuilder.ReadAttribute<bool>(parentNode, "ProcessSubject", settings.ProcessSubject);
  }
}
