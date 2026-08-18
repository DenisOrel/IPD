
// Type: Intermech.Search.Configuration.ConfigurationOptionPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.Search.Configuration;

public sealed class ConfigurationOptionPropertyDescriptor : System.ComponentModel.PropertyDescriptor
{
  public ConfigurationOptionPropertyDescriptor(
    ConfigurationOptionInfo optionInfo,
    Attribute[] attributes)
    : base(optionInfo.DisplayName, attributes)
  {
    this.OptionInfo = optionInfo != null ? optionInfo : throw new ArgumentNullException(nameof (optionInfo));
  }

  public ConfigurationOptionInfo OptionInfo { get; private set; }

  public override bool CanResetValue(object component)
  {
    if (!(component is Intermech.Search.Configuration.Configuration))
      throw new ArgumentException();
    return true;
  }

  public override Type ComponentType => typeof (Intermech.Search.Configuration.Configuration);

  public override object GetValue(object component)
  {
    if (!(component is Intermech.Search.Configuration.Configuration))
      throw new ArgumentException();
    return ((Intermech.Search.Configuration.Configuration) component).GetValue(this.OptionInfo.Key);
  }

  public override bool IsReadOnly => this.OptionInfo.CheckAdmin && !this.IsAdmin();

  public override Type PropertyType => this.OptionInfo.Type;

  public override void ResetValue(object component)
  {
    if (!(component is Intermech.Search.Configuration.Configuration))
      throw new ArgumentException();
    ((Intermech.Search.Configuration.Configuration) component).ResetValue(this.OptionInfo.Key);
  }

  public override void SetValue(object component, object value)
  {
    if (!(component is Intermech.Search.Configuration.Configuration))
      throw new ArgumentException();
    ((Intermech.Search.Configuration.Configuration) component).SetValue(this.OptionInfo.Key, value);
  }

  public override bool ShouldSerializeValue(object component)
  {
    if (!(component is Intermech.Search.Configuration.Configuration))
      throw new ArgumentException();
    return ((Intermech.Search.Configuration.Configuration) component).ShouldSerializeValue(this.OptionInfo.Key);
  }

  private bool IsAdmin() => ServiceLocator.Get<ICurrentUserAndRole>().IsAdmin;
}
