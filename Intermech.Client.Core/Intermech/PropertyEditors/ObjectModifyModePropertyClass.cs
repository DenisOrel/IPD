
// Type: Intermech.PropertyEditors.ObjectModifyModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjectModifyModeEditor.</summary>
public class ObjectModifyModePropertyClass
{
  private ObjectModifyModes objectModifyMode;

  public ObjectModifyModes ObjectModifyMode => this.objectModifyMode;

  public ObjectModifyModePropertyClass(ObjectModifyModes aObjectModifyMode)
  {
    this.objectModifyMode = aObjectModifyMode;
  }

  public override string ToString() => ObjectModifyModesHelper.GetCaption(this.objectModifyMode);
}
