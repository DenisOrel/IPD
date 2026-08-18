// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EnhRadioGroupWrapper
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

internal class EnhRadioGroupWrapper : ControlWrapper, IWrapper
{
  private EnhRadioGroup _parent = new EnhRadioGroup();

  public EnhRadioGroupWrapper()
  {
  }

  public EnhRadioGroupWrapper(EnhRadioGroup parent)
    : base((Control) parent)
  {
    this._parent = parent;
  }

  [CustomDisplayName("Attribute.Workflow.Design_InVar")]
  [CustomCategory("Attribute.Workflow.Design_18")]
  [CustomDescription("Attribute.Workflow.Design_19")]
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
  [CustomDescription("Attribute.Workflow.Design_22")]
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

  [Browsable(false)]
  public override bool TabStop
  {
    get => this._parent.TabStop;
    set => this.SetValue(this._pdc[nameof (TabStop)], (object) value);
  }

  [Browsable(false)]
  public override int TabIndex
  {
    get => this._parent.TabIndex;
    set => this.SetValue(this._pdc[nameof (TabIndex)], (object) value);
  }

  public object BaseClass => (object) this._parent;

  object IWrapper.BaseClass => (object) this._parent;
}
