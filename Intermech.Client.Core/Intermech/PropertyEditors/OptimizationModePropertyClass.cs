
// Type: Intermech.PropertyEditors.OptimizationModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for OptimizationModeEditor.</summary>
public class OptimizationModePropertyClass
{
  private OptimizationModes optimizationMode = OptimizationModes.Read;

  public OptimizationModes OptimizationMode => this.optimizationMode;

  public OptimizationModePropertyClass(OptimizationModes aOptimizationMode)
  {
    this.optimizationMode = aOptimizationMode;
  }

  public override string ToString() => OptimizationModesHelper.GetCaption(this.optimizationMode);
}
