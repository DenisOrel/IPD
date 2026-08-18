// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ConfigEditorModeView
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ConfigEditorModeView
{
  private static ConfigEditorModeView _modeView;

  private ConfigEditorModeView()
  {
  }

  public static ConfigEditorModeView GetModeView()
  {
    if (ConfigEditorModeView._modeView == null)
      ConfigEditorModeView._modeView = new ConfigEditorModeView();
    return ConfigEditorModeView._modeView;
  }

  internal bool ThisExportConfig { get; private set; }

  internal bool UserDataOnly { get; private set; }

  internal void GetConfig(XmlExchangeExportSettings exportSettings, bool rootExportConfig = true)
  {
    if (!rootExportConfig)
    {
      this.UserDataOnly = true;
      this.ThisExportConfig = false;
    }
    else
    {
      if (exportSettings == null)
        return;
      this.UserDataOnly = exportSettings.ExtraDataMode.HasFlag((Enum) XmlExportExtraDataMode.UserDataOnly);
      this.ThisExportConfig = true;
    }
  }
}
