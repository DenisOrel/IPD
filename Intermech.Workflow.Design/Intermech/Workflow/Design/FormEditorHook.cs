// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.FormEditorHook
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for FormEditorHook.</summary>
public class FormEditorHook : IFormDesignerEditorHook
{
  private long _processID;

  public FormEditorHook(long processID) => this._processID = processID;

  public bool CanExecuteSelector => true;

  private List<VarType> GetFilter(object context, PropertyDescriptor pd)
  {
    List<VarType> filter;
    switch (context)
    {
      case AttrTextEdit _:
      case AttrTextBtn _:
      case AttrLabel _:
      case AttrMaskedTextEdit _:
        filter = (List<VarType>) null;
        break;
      case UsersTreeView _:
      case UsersComboBox _:
        filter = new List<VarType>((IEnumerable<VarType>) new VarType[1]
        {
          VarType.ParticipantList
        });
        break;
      case AttrComboBox _:
      case ListBox _:
        List<VarType> varTypeList1;
        if (!(context is CheckedListBox))
          varTypeList1 = new List<VarType>((IEnumerable<VarType>) new VarType[1]
          {
            VarType.StringList
          });
        else
          varTypeList1 = new List<VarType>((IEnumerable<VarType>) new VarType[1]
          {
            VarType.Unknown
          });
        filter = varTypeList1;
        break;
      case AttrPassword _:
        filter = new List<VarType>((IEnumerable<VarType>) new VarType[1]);
        break;
      case AttrMemoEdit _:
        filter = new List<VarType>((IEnumerable<VarType>) new VarType[2]
        {
          VarType.Text,
          VarType.String
        });
        break;
      case AttrDateEdit _:
        filter = new List<VarType>((IEnumerable<VarType>) new VarType[1]
        {
          VarType.DateTime
        });
        break;
      case AttrCheckBox _:
        filter = new List<VarType>((IEnumerable<VarType>) new VarType[2]
        {
          VarType.Integer,
          VarType.Boolean
        });
        break;
      case EnhRadioGroup _:
        List<VarType> varTypeList2;
        if (!(pd.DisplayName == LocalizationHolder.GetCustomDescription("Attribute.Workflow.Design_InVar")))
          varTypeList2 = new List<VarType>((IEnumerable<VarType>) new VarType[1]
          {
            VarType.Integer
          });
        else
          varTypeList2 = new List<VarType>((IEnumerable<VarType>) new VarType[1]
          {
            VarType.StringList
          });
        filter = varTypeList2;
        break;
      default:
        filter = new List<VarType>((IEnumerable<VarType>) new VarType[1]
        {
          VarType.Unknown
        });
        break;
    }
    return filter;
  }

  public bool ExecuteSelector(object context, PropertyDescriptor pd, ref object value)
  {
    int attrTypeID = wfFunx.SelectVariable(this._processID, this.GetFilter(context, pd));
    if (attrTypeID != 0)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
      if (attributeType != null)
      {
        value = (object) new AttributeInfo(attributeType.AttributeGuid, Guid.Empty);
        return true;
      }
    }
    return false;
  }
}
