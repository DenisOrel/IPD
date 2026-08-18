// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ConstructionalSchemaCheck
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class ConstructionalSchemaCheck : AcadSettingsCheck
{
  protected override string DoPerformCheck(
    AcadIntegratorSettings settings,
    SettingsValidatorContext context)
  {
    ConstructionalSettings constructionalSettings = settings.ConstructionalSettings;
    if (constructionalSettings.IsEnabled)
    {
      if (constructionalSettings.Drawings.Count == 0)
        return "Необходимо указать хотя бы один тип документа для СПДС-чертежей.";
      foreach (DrawingTypeSettings drawing in constructionalSettings.Drawings)
      {
        string str = SettingsUtils.ValidateStmName(drawing.StmName);
        if (str != null)
          return $"Ошибка в свойствах типа СПДС-чертежей '{drawing}'. {str}";
      }
    }
    return (string) null;
  }
}
