// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.LaunchHandlers.OpenDwgWithProfileSettingsValidator
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.LaunchActions;
using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator.LaunchHandlers;

internal sealed class OpenDwgWithProfileSettingsValidator : LaunchActionSettingsValidator
{
  protected override string DoValidate(
    ISettingsObject settingsObject,
    SettingsValidatorContext context)
  {
    OpenDwgWithProfileSettings withProfileSettings = settingsObject != null ? (OpenDwgWithProfileSettings) settingsObject : throw new ArgumentNullException(nameof (settingsObject));
    if (string.IsNullOrEmpty(withProfileSettings.ProfileName))
      return "Настройки команды запуска приложения: не указано название профиля AutoCAD.";
    return !withProfileSettings.ProfileName.StartsWith("<<") || !withProfileSettings.ProfileName.EndsWith(">>") ? "Настройки команды запуска приложения: название профиля AutoCAD должно быть заключено в кавычки << и >>." : base.DoValidate(settingsObject, context);
  }
}
