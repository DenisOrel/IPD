// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StartupConfigurationsCheck
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class StartupConfigurationsCheck : AcadSettingsCheck
{
  private readonly Regex profileNameRegex = new Regex("^<<.+>>$", RegexOptions.IgnoreCase | RegexOptions.Singleline);

  protected override string DoPerformCheck(
    AcadIntegratorSettings settings,
    SettingsValidatorContext context)
  {
    if (settings.StartupConfigurations.Count == 0)
      return "Настройки интегратора не содержат параметров подключения к приложению.";
    List<Guid> guidList = new List<Guid>(settings.StartupConfigurations.Count);
    foreach (AcadStartupConfiguration startupConfiguration in settings.StartupConfigurations)
    {
      Guid guid = startupConfiguration.UserRole != null ? startupConfiguration.UserRole.Id : Guid.Empty;
      if (guidList.Contains(guid))
        return $"Дублирование параметров подключения к приложению не разрешено (роль '{SettingsUtils.GetRoleCaption(startupConfiguration.UserRole)}').";
      guidList.Add(guid);
    }
    foreach (AcadStartupConfiguration startupConfiguration in settings.StartupConfigurations)
    {
      if (startupConfiguration.UseSpecificProfile && !this.profileNameRegex.IsMatch(startupConfiguration.ProfileName))
        return $"Параметры подключения к приложению для роли '{SettingsUtils.GetRoleCaption(startupConfiguration.UserRole)}' содержат ошибку: некорректное имя.";
    }
    return (string) null;
  }
}
