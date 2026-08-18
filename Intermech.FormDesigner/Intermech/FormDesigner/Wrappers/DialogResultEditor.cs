// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DialogResultEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class DialogResultEditor : UITypeEditor
{
  private IWindowsFormsEditorService svc;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    DialogResultConverter dialogResultConverter = new DialogResultConverter();
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    foreach (string str in (IEnumerable) dialogResultConverter.hash.forward.Values)
      listBox.Items.Add((object) str);
    listBox.SelectedItem = dialogResultConverter.hash[value];
    listBox.Click += new EventHandler(this.ListBoxClick);
    this.svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    return dialogResultConverter.hash[listBox.SelectedItem];
  }

  private void ListBoxClick(object sender, EventArgs e) => this.svc.CloseDropDown();
}
