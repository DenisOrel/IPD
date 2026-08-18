// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.LaunchHandlers.OpenDwgWithProfileSettings
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator.LaunchHandlers;

internal sealed class OpenDwgWithProfileSettings : ISettingsObject
{
  public static readonly Guid HandlerID = new Guid("E595EE15-8CF3-4DAF-B273-29ACDFFCC3BC");
  private string _profileName;

  public OpenDwgWithProfileSettings() => this._profileName = "<<Профиль без имени>>";

  public string ProfileName
  {
    get => this._profileName;
    set => this._profileName = value;
  }
}
