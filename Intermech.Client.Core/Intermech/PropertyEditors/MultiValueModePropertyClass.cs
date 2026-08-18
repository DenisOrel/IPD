
// Type: Intermech.PropertyEditors.MultiValueModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for MultiValueModeEditor.</summary>
public class MultiValueModePropertyClass
{
  private MultiValueModes multiValueMode;

  public MultiValueModes MultiValueMode => this.multiValueMode;

  public MultiValueModePropertyClass(MultiValueModes aMultiValueMode)
  {
    this.multiValueMode = aMultiValueMode;
  }

  public override string ToString() => MultiValueModesHelper.GetCaption(this.multiValueMode);
}
