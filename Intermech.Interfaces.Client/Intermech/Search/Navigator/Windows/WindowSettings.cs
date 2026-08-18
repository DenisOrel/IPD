// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.WindowSettings
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>Настройки окна навигатора</summary>
[Serializable]
public class WindowSettings : WindowSettingsBase
{
  public override object Clone()
  {
    WindowSettings windowSettings = new WindowSettings();
    windowSettings.TreeWidth = this.TreeWidth;
    windowSettings.TreeColumns = this.TreeColumns != null ? (NodeColumnCollection) this.TreeColumns.Clone() : (NodeColumnCollection) null;
    return (object) windowSettings;
  }
}
