// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalSchemaCheck
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalSchemaCheck : AcadSettingsCheck
{
  protected override string DoPerformCheck(
    AcadIntegratorSettings settings,
    SettingsValidatorContext context)
  {
    MechanicalSettings mechanicalSettings = settings.MechanicalSettings;
    if (mechanicalSettings.IsEnabled)
    {
      if (mechanicalSettings.AssemblyDrawings.Count == 0)
        return "Необходимо указать хотя бы один тип документа для сборочных чертежей.";
      if (mechanicalSettings.PartDrawings.Count == 0)
        return "Необходимо указать хотя бы один тип документа для чертежей деталей.";
      foreach (DrawingTypeSettings assemblyDrawing in mechanicalSettings.AssemblyDrawings)
      {
        string str = SettingsUtils.ValidateStmName(assemblyDrawing.StmName);
        if (str != null)
          return $"Ошибка в свойствах типа сборочных чертежей '{assemblyDrawing}'. {str}";
      }
      foreach (DrawingTypeSettings partDrawing in mechanicalSettings.PartDrawings)
      {
        string str = SettingsUtils.ValidateStmName(partDrawing.StmName);
        if (str != null)
          return $"Ошибка в свойствах типа чертежей деталей '{partDrawing}'. {str}";
      }
    }
    return (string) null;
  }
}
