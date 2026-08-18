
// Type: Intermech.PropertyEditors.LCAccessTypePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for LCAccessTypeEditor.</summary>
public class LCAccessTypePropertyClass
{
  private LCAccessTypes lcAccessType;

  public LCAccessTypes LCAccessType => this.lcAccessType;

  public LCAccessTypePropertyClass(LCAccessTypes aLCAccessType)
  {
    this.lcAccessType = aLCAccessType;
  }

  public override string ToString() => LCAccessTypesHelper.GetCaption(this.lcAccessType);
}
