
// Type: Intermech.PropertyEditors.ComputeValueModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ComputeValueModeEditor.</summary>
public class ComputeValueModePropertyClass
{
  private ComputeValueModes computeValueMode;

  public ComputeValueModes ComputeValueMode => this.computeValueMode;

  public ComputeValueModePropertyClass(ComputeValueModes aComputeValueMode)
  {
    this.computeValueMode = aComputeValueMode;
  }

  public override string ToString() => ComputeValueModesHelper.GetCaption(this.computeValueMode);
}
