// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DIntegratorSettingsCodec
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Settings;
using System.Xml;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DIntegratorSettingsCodec(
  string integratorName,
  ISettingsObjectFactory factory) : CADSettingsCodec(integratorName, factory)
{
  protected override void EncodeCustomSettings(
    CADSettings settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    base.EncodeCustomSettings(settingsObject, settingsBuilder);
    this.EncodeDrawing2DSettings((K3DIntegratorSettings) settingsObject, settingsBuilder);
  }

  private void EncodeDrawing2DSettings(
    K3DIntegratorSettings settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("Drawings2D");
    settingsBuilder.AppendAttribute((XmlNode) element, "enabled", (object) settingsObject.EnableDrawings2DSupport);
    settingsBuilder.AppendElement((XmlNode) element);
  }

  protected override bool CanEncodeServerData(
    ISettingsObject settingsObject,
    DocumentGroup documentGroup)
  {
    return (!(documentGroup.Name == "PartDrawing2D") && !(documentGroup.Name == "AssemblyDrawing2D") || ((K3DIntegratorSettings) settingsObject).EnableDrawings2DSupport) && base.CanEncodeServerData(settingsObject, documentGroup);
  }

  protected override void DecodeCustomSettings(
    SettingsXmlBuilder settingsBuilder,
    CADSettings settingsObject)
  {
    base.DecodeCustomSettings(settingsBuilder, settingsObject);
    K3DIntegratorSettings settingsObject1 = (K3DIntegratorSettings) settingsObject;
    this.DecodeDrawing2DSettings(settingsBuilder, settingsObject1);
  }

  private void DecodeDrawing2DSettings(
    SettingsXmlBuilder settingsBuilder,
    K3DIntegratorSettings settingsObject)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("Drawings2D[@enabled]");
    if (parentNode != null)
      settingsObject.EnableDrawings2DSupport = settingsBuilder.ReadAttribute<bool>(parentNode, "enabled", false);
    else
      settingsObject.EnableDrawings2DSupport = false;
  }
}
