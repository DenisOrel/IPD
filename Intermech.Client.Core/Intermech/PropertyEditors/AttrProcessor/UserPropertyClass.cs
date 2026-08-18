
// Type: Intermech.PropertyEditors.AttrProcessor.UserPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors.AttrProcessor;

internal class UserPropertyClass
{
  private object value;

  public virtual object Value
  {
    get => this.value;
    set => this.value = value;
  }

  public UserPropertyClass(object aValue) => this.value = aValue;

  public override string ToString() => this.value == null ? string.Empty : this.value.ToString();
}
