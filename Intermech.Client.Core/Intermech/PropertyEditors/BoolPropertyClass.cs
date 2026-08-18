
// Type: Intermech.PropertyEditors.BoolPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

public class BoolPropertyClass
{
  private bool boolean;
  private bool isNull;

  public bool Boolean => this.boolean;

  public bool IsNull => this.isNull;

  public BoolPropertyClass(bool aBoolean)
    : this(aBoolean, false)
  {
  }

  public BoolPropertyClass(bool aBoolean, bool aIsNull)
  {
    this.boolean = aBoolean;
    this.isNull = aIsNull;
  }

  public override string ToString()
  {
    return this.isNull ? string.Empty : BoolSrv.YesNoConvert(this.boolean);
  }
}
