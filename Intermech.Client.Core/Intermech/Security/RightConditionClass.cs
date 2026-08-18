
// Type: Intermech.Security.RightConditionClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Security;

internal class RightConditionClass
{
  public long Value;
  public string Text;

  public RightConditionClass(long val, string text)
  {
    this.Value = val;
    this.Text = text;
  }

  public override string ToString() => this.Text;
}
