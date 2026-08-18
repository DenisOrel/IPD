
// Type: Intermech.PropertyEditors.CategoryPropsClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class CategoryPropsClass : ICategoryProps
{
  private PropDescriptor pdd;

  public void SetVal(string sss)
  {
    if (this.pdd == null)
      return;
    this.pdd.SetValue(this.pdd.Component, (object) sss);
  }

  public void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
  }

  public bool Apply(PropDescriptorHolder pdh, int category, object id, object idOld) => false;

  public string SubscriberID => "Test";

  public void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e)
  {
  }

  public PropDescriptor[] GetPropDescriptors(PropDescriptorHolder pdh, int category, object id)
  {
    if (!(ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService))
      return (PropDescriptor[]) null;
    if (this.pdd == null)
      this.pdd = new PropDescriptor(0, (object) null, "Asdfas", (object) "value1234", typeof (string), (TypeConverter) null, (object) null, string.Empty, string.Empty, false, true, false);
    else
      this.pdd.SetValue(this.pdd.Component, (object) "1234");
    return (PropDescriptor[]) new ArrayList()
    {
      (object) this.pdd
    }.ToArray(typeof (PropDescriptor));
  }
}
