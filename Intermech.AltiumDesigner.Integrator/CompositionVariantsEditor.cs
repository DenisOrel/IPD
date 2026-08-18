// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.CompositionVariantsEditor
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators.Electrical;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class CompositionVariantsEditor : UITypeEditor
{
  private IWindowsFormsEditorService _svc;

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this._svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    CompositionVariantsProxy compositionVariantsProxy = value == null || !(value is CompositionVariantsProxy) ? new CompositionVariantsProxy(CompositionVariants.NoUsed) : (CompositionVariantsProxy) value;
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    foreach (CompositionVariants val in Enum.GetValues(typeof (CompositionVariants)))
      listBox.Items.Add((object) new CompositionVariantsProxy(val));
    listBox.SelectedItem = (object) compositionVariantsProxy;
    listBox.Click += new EventHandler(this.ListBoxClick);
    this._svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    return listBox.SelectedItem != null ? listBox.SelectedItem : value;
  }

  private void ListBoxClick(object sender, EventArgs e)
  {
    if (this._svc == null)
      return;
    this._svc.CloseDropDown();
  }
}
