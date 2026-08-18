
// Type: Intermech.PropertyEditors.RequiredModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for RequiredModeEditor.</summary>
public class RequiredModePropertyClass
{
  private RequiredModes requiredMode;

  public RequiredModes RequiredMode => this.requiredMode;

  public RequiredModePropertyClass(RequiredModes aRequiredMode)
  {
    this.requiredMode = aRequiredMode;
  }

  public override string ToString() => RequiredModesHelper.GetCaption(this.requiredMode);
}
