// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordIntegratorSettingsCodec
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators.Simple;
using Intermech.Tools.Settings;
using System;
using System.Xml;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordIntegratorSettingsCodec(string integratorName) : SingleFileSettingsCodec(integratorName)
{
  private const string SynchronizeObjectsReferencesInDocumentWithDocumentCompositionOptionName = "SynchronizeObjectsReferencesInDocumentWithDocumentComposition";

  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new MSWordIntegratorSettings();
  }

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    base.EncodeSettings(settingsObject, settingsBuilder);
    this.EncodeOptions((MSWordIntegratorSettings) settingsObject, settingsBuilder);
  }

  private void EncodeOptions(MSWordIntegratorSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element1 = settingsBuilder.CreateElement("RunAutoMacro");
    settingsBuilder.AppendAttribute((XmlNode) element1, "AutoOpen", (object) settings.RunAutoOpenMacro);
    settingsBuilder.AppendElement((XmlNode) element1);
    XmlElement element2 = settingsBuilder.CreateElement("SynchronizeObjectsReferencesInDocumentWithDocumentComposition");
    element2.InnerText = settings.SynchronizeObjectsReferencesInDocumentWithDocumentComposition.ToString();
    settingsBuilder.AppendElement((XmlNode) element2);
  }

  protected override void DecodeV1(
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    base.DecodeV1(settingsBuilder, settingsObject);
    MSWordIntegratorSettings settings = (MSWordIntegratorSettings) settingsObject;
    this.DecodeOptions(settingsBuilder, settings);
  }

  private void DecodeOptions(SettingsXmlBuilder settingsBuilder, MSWordIntegratorSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("RunAutoMacro");
    if (parentNode != null)
      settings.RunAutoOpenMacro = settingsBuilder.ReadAttribute<bool>(parentNode, "AutoOpen", settings.RunAutoOpenMacro);
    XmlNode xmlNode = settingsBuilder.SelectSingleNode("SynchronizeObjectsReferencesInDocumentWithDocumentComposition");
    if (xmlNode == null || string.IsNullOrEmpty(xmlNode.InnerText))
      return;
    settings.SynchronizeObjectsReferencesInDocumentWithDocumentComposition = Convert.ToBoolean(xmlNode.InnerText);
  }
}
