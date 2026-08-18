// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.FieldTypeSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class FieldTypeSelected : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
    if (formsEditorService != null)
    {
      ListBox listBox = new ListBox();
      listBox.BorderStyle = BorderStyle.None;
      listBox.IntegralHeight = true;
      listBox.Click += new EventHandler(this.listBox_Click);
      listBox.Tag = (object) formsEditorService;
      listBox.Items.Add((object) FieldTypes.ftString.GetDescription<FieldTypes>());
      listBox.Items.Add((object) FieldTypes.ftInteger.GetDescription<FieldTypes>());
      listBox.Items.Add((object) FieldTypes.ftDouble.GetDescription<FieldTypes>());
      listBox.Items.Add((object) FieldTypes.ftDateTime.GetDescription<FieldTypes>());
      int result;
      if (value != null && int.TryParse(value.ToString(), out result))
        listBox.SelectedItem = (object) ((FieldTypes) result).GetDescription<FieldTypes>();
      formsEditorService.DropDownControl((Control) listBox);
      if (listBox.SelectedItem != null)
      {
        foreach (FieldTypes fieldTypes in Enum.GetValues(typeof (FieldTypes)))
        {
          if (fieldTypes.GetDescription<FieldTypes>() == listBox.SelectedItem.ToString())
            return (object) (int) fieldTypes;
        }
      }
    }
    return value;
  }

  private void listBox_Click(object sender, EventArgs e)
  {
    if (!(sender is ListBox listBox) || listBox.SelectedItem == null || !(listBox.Tag is IWindowsFormsEditorService tag))
      return;
    tag.CloseDropDown();
  }
}
