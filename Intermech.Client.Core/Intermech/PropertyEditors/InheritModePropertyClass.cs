
// Type: Intermech.PropertyEditors.InheritModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

public class InheritModePropertyClass
{
  private InheritModes inheritMode;

  public InheritModes InheritMode => this.inheritMode;

  public InheritModePropertyClass(InheritModes aInheritMode) => this.inheritMode = aInheritMode;

  public override bool Equals(object obj)
  {
    return obj is InheritModePropertyClass ? ((InheritModePropertyClass) obj).InheritMode == this.inheritMode : base.Equals(obj);
  }

  public override string ToString() => InheritModesHelper.GetCaption(this.inheritMode);
}
