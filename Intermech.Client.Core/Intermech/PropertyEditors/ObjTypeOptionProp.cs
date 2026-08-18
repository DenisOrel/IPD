
// Type: Intermech.PropertyEditors.ObjTypeOptionProp
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DatabaseConfigurator;
using System;


namespace Intermech.PropertyEditors;

public class ObjTypeOptionProp : ICategoryProps
{
  protected PropDescriptor propertyDescriptor;
  protected string subscriberID;
  protected object attributeValue;

  public ObjTypeOptionProp(string subscriberID) => this.subscriberID = subscriberID;

  protected virtual bool OnApply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    return false;
  }

  protected virtual PropDescriptor[] OnGetDescriptors(
    PropDescriptorHolder pdh,
    int category,
    object id)
  {
    return (PropDescriptor[]) null;
  }

  public string SubscriberID => this.subscriberID;

  public PropDescriptor[] GetPropDescriptors(PropDescriptorHolder pdh, int category, object id)
  {
    return this.OnGetDescriptors(pdh, category, id);
  }

  public bool Apply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    return this.OnApply(pdh, category, id, idOld);
  }

  public void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
  }

  public void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e)
  {
  }
}
