
// Type: Intermech.PropertyEditors.ObjectVersionModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjectVersionModeEditor.</summary>
public class ObjectVersionModePropertyClass
{
  private ObjectVersionModes objectVersionMode;

  public ObjectVersionModes ObjectVersionMode => this.objectVersionMode;

  public ObjectVersionModePropertyClass(ObjectVersionModes aObjectVersionMode)
  {
    this.objectVersionMode = aObjectVersionMode;
  }

  public override string ToString() => ObjectVersionModesHelper.GetCaption(this.objectVersionMode);
}
