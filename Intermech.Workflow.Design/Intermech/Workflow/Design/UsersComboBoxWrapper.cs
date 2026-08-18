// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UsersComboBoxWrapper
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Wrappers;
using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class UsersComboBoxWrapper : ControlWrapper, IWrapper
{
  private UsersComboBox _parent = new UsersComboBox();

  public UsersComboBoxWrapper()
  {
  }

  public UsersComboBoxWrapper(UsersComboBox parent)
    : base((Control) parent)
  {
    this._parent = parent;
  }

  [CustomDisplayName("Attribute.Workflow.Design_InVar")]
  [CustomCategory("Attribute.Workflow.Design_18")]
  [CustomDescription("Attribute.Workflow.Design_25")]
  [TypeConverter(typeof (AttributeInfo2TypeNamesConverter))]
  [Editor(typeof (AttributeEditor), typeof (UITypeEditor))]
  [MultiValueModes(MultiValueModes.SingleValue)]
  [RefreshProperties(RefreshProperties.All)]
  public AttributeInfo SrcVariable
  {
    get => new AttributeInfo(this._parent.SrcVariable, Guid.Empty);
    set
    {
      this.SetValue(this._pdc[nameof (SrcVariable)], (object) (value != null ? value.AttributeGuid : Guid.Empty));
    }
  }

  [CustomDisplayName("Attribute.Workflow.Design_20")]
  [CustomCategory("Attribute.Workflow.Design_21")]
  [CustomDescription("Attribute.Workflow.Design_28")]
  [TypeConverter(typeof (AttributeInfo2TypeNamesConverter))]
  [Editor(typeof (AttributeEditor), typeof (UITypeEditor))]
  [MultiValueModes(MultiValueModes.SingleValue)]
  [RefreshProperties(RefreshProperties.All)]
  public AttributeInfo DstVariable
  {
    get => new AttributeInfo(this._parent.DstVariable, Guid.Empty);
    set
    {
      this.SetValue(this._pdc[nameof (DstVariable)], (object) (value != null ? value.AttributeGuid : Guid.Empty));
    }
  }

  [CustomDisplayName("Attribute.Workflow.Design_35")]
  [CustomCategory("Attribute.Workflow.Design_36")]
  [CustomDescription("Attribute.Workflow.Design_UserComboDesc")]
  public bool RequiresValue
  {
    get => this._parent.RequiresValue;
    set => this.SetValue(this._pdc[nameof (RequiresValue)], (object) value);
  }

  public object BaseClass => (object) this._parent;

  object IWrapper.BaseClass => (object) this._parent;
}
