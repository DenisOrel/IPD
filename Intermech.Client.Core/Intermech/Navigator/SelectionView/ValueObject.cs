
// Type: Intermech.Navigator.SelectionView.ValueObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.PropertyEditors;
using System;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для представления значения в условии выборки
/// </summary>
internal sealed class ValueObject
{
  public valueObjectChanged ValueChanged;
  private int _attributeID;
  private object _Value;
  public SelectionParameterTypes ObjType;

  public object Value
  {
    get => this._Value;
    set
    {
      if (this._Value == value)
        return;
      this._Value = value;
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this);
    }
  }

  public ValueObject(object obj, SelectionParameterTypes objType, int attributeID)
  {
    this.Value = obj;
    this.ObjType = objType;
    this._attributeID = attributeID;
  }

  public override string ToString()
  {
    string str = "";
    if (this.Value != null)
    {
      if (this.ObjType == SelectionParameterTypes.sptHandler)
      {
        str = Convert.ToString((ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(this._attributeID).GetPropDescriptorValue((IElementInfo) null, this._attributeID, this.Value));
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          str = SelectionParameter.ConvertToString(sessionKeeper.Session, this.Value, this.ObjType);
      }
    }
    return str;
  }
}
