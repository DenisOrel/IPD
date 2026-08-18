// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripts.LCStepScriptProperty
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripts;

internal class LCStepScriptProperty : ICategoryProps
{
  private PropDescriptor _propertyDescriptor;
  private string propName = "Сценарий";

  public string SubscriberID => "Сценарии";

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
    ILCStep lcStep = pdh as ILCStep;
    long script = LCStepScriptManager.GetScript(lcStep.LCStepProperties.StepGuid);
    LCStepScriptValue lcStepScriptValue = new LCStepScriptValue(lcStep.LCStepProperties.StepGuid, lcStep.LCStepProperties.ObjectTypeID, script, pdh.PropDescriptorCollection[3].IsReadOnly);
    if (this._propertyDescriptor == null)
    {
      TypeConverter converter = (TypeConverter) new LCStepScriptConverter();
      UITypeEditor editor = (UITypeEditor) new LCStepScriptEditor();
      if (pdh.PropDescriptorCollection[3].IsReadOnly)
        editor = (UITypeEditor) null;
      this._propertyDescriptor = (PropDescriptor) new LCStepPropDescriptor(0, (object) null, this.propName, (object) lcStepScriptValue, typeof (Nullable), converter, (object) editor, string.Empty, "Сценарий вызываемый перед изменением шага ЖЦ", pdh.PropDescriptorCollection[3].IsReadOnly, true, true);
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
          return propDescriptor.GetValue((object) component) is LCStepScriptValue lcStepScriptValue && lcStepScriptValue.SaveStep(component.LCStepProperties.StepGuid);
        }
      }
    }
    return true;
  }

  public void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
    if (pdh.PropDescriptorCollection == null)
      return;
    PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[pdh.PropDescriptorCollection.Count];
    pdh.PropDescriptorCollection.CopyTo((Array) propertyDescriptorArray, 0);
    foreach (PropDescriptor propDescriptor in propertyDescriptorArray)
    {
      if (propDescriptor.DisplayName.Equals(this.propName) && propDescriptor.ValueChanged)
      {
        ILCStep component = propDescriptor.Component as ILCStep;
        if (propDescriptor.GetValue((object) component) is LCStepScriptValue lcStepScriptValue)
        {
          long? nullable = new long?();
          lcStepScriptValue.NewScriptId = nullable;
        }
      }
    }
  }

  public void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e)
  {
    this._propertyDescriptor.ValueChanged = true;
  }
}
