// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RequireRevisionProperty
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DatabaseConfigurator;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

internal class RequireRevisionProperty : ICategoryProps
{
  private PropDescriptor _propertyDescriptor;
  private string propName = LocalizationHolder.rm.GetString("ECO.Client_109");

  public void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
    if (pdh.PropDescriptorCollection == null)
      return;
    PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[pdh.PropDescriptorCollection.Count];
    pdh.PropDescriptorCollection.CopyTo((Array) propertyDescriptorArray, 0);
    ILCStep lcStep = pdh as ILCStep;
    foreach (PropDescriptor propDescriptor in propertyDescriptorArray)
    {
      if (propDescriptor.DisplayName.Equals(this.propName) && propDescriptor.ValueChanged)
      {
        ReqRevisionClass reqRevisionClass = new ReqRevisionClass(lcStep.LCStepProperties.LCStep, lcStep.LCStepProperties.ObjectTypeID, pdh.PropDescriptorCollection[3].IsReadOnly);
        propDescriptor.SetValue(propDescriptor.Component, (object) reqRevisionClass);
      }
    }
  }

  public string SubscriberID => LocalizationHolder.rm.GetString("ECO.Client_110");

  public PropDescriptor[] GetPropDescriptors(PropDescriptorHolder pdh, int category, object id)
  {
    this._propertyDescriptor = (PropDescriptor) null;
    foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
    {
      if (propDescriptor.DisplayName.Equals(this.propName))
      {
        this._propertyDescriptor = propDescriptor;
        break;
      }
    }
    if (this._propertyDescriptor == null)
    {
      ILCStep lcStep = pdh as ILCStep;
      this._propertyDescriptor = new PropDescriptor(0, (object) null, this.propName, (object) new ReqRevisionClass(lcStep.LCStepProperties.LCStep, lcStep.LCStepProperties.ObjectTypeID, pdh.PropDescriptorCollection[3].IsReadOnly), typeof (ReqRevisionClass), (TypeConverter) new ReqRevisionConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("ECO.Client_111"), false, true, false);
    }
    return new PropDescriptor[1]{ this._propertyDescriptor };
  }

  public bool Apply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    if (pdh.PropDescriptorCollection != null)
    {
      PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[pdh.PropDescriptorCollection.Count];
      pdh.PropDescriptorCollection.CopyTo((Array) propertyDescriptorArray, 0);
      foreach (PropDescriptor propDescriptor in propertyDescriptorArray)
      {
        if (propDescriptor.DisplayName.Equals(this.propName) && propDescriptor.ValueChanged)
        {
          ILCStep component = propDescriptor.Component as ILCStep;
          ReqRevisionClass reqRevisionClass = propDescriptor.GetValue((object) component) as ReqRevisionClass;
          reqRevisionClass._objectType = component.LCStepProperties.ObjectTypeID;
          return reqRevisionClass.SaveStep(component.LCStepProperties.LCStep);
        }
      }
    }
    return true;
  }

  public void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e)
  {
    this._propertyDescriptor.ValueChanged = true;
  }
}
