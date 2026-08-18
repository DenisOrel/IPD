// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.FlagEnumUIEditor
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Controls;

public class FlagEnumUIEditor : UITypeEditor
{
  [NotNull]
  private readonly FlagCheckedListBox _flagEnumCb;

  public FlagEnumUIEditor()
  {
    this._flagEnumCb = new FlagCheckedListBox();
    this._flagEnumCb.BorderStyle = BorderStyle.None;
  }

  public override object EditValue(
    [CanBeNull] ITypeDescriptorContext context,
    [CanBeNull] IServiceProvider provider,
    [CanBeNull] object value)
  {
    if (context != null && context.Instance != null && provider != null)
    {
      IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      if (service != null)
      {
        Intermech.Diagnostics.Check.NotNull<PropertyDescriptor>(context.PropertyDescriptor, "PropertyDescriptor.PropertyType");
        Type propertyType = context.PropertyDescriptor.PropertyType;
        this._flagEnumCb.EnumValue = Convert.ChangeType(value, propertyType) as Enum;
        service.DropDownControl((Control) this._flagEnumCb);
        return (object) this._flagEnumCb.EnumValue;
      }
    }
    return (object) null;
  }

  public override UITypeEditorEditStyle GetEditStyle([CanBeNull] ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }
}
