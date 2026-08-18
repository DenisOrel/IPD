
// Type: Intermech.PropertyEditors.MeasureDescriptorClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.PropertyEditors;

public class MeasureDescriptorClass
{
  private MeasureDescriptor md;

  public MeasureDescriptorClass(MeasureDescriptor md) => this.md = md;

  public override string ToString() => this.md.ShortName;
}
