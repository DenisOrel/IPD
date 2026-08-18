// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.KeepEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase;

internal class KeepEditor : DropDownEditor
{
  private IWindowsFormsEditorService svc;

  private void ListBoxClick(object sender, EventArgs e) => this.svc.CloseDropDown();

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    KeepConverter keepConverter = new KeepConverter();
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    listBox.Height = 32 /*0x20*/;
    foreach (string str in (IEnumerable) keepConverter.Hash.forward.Values)
      listBox.Items.Add((object) str);
    listBox.SelectedItem = value != null ? keepConverter.Hash[value] : keepConverter.Hash[(object) true];
    listBox.Click += new EventHandler(this.ListBoxClick);
    this.svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    return keepConverter.Hash[listBox.SelectedItem];
  }
}
