// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkTimeVirtualColumn
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Design;

internal class WorkTimeVirtualColumn : IVirtualColumn, INodeColumnTransform
{
  private string _name;

  public string Name
  {
    get
    {
      if (this._name == null)
        this._name = LocalizationHolder.rm.GetString("Workflow.Design_1");
      return this._name;
    }
  }

  public FieldTypes AttrType => FieldTypes.ftString;

  public Type DataType => typeof (string);

  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    return (object) "???";
  }
}
