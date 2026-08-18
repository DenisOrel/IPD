
// Type: Intermech.PropertyEditors.SubjectAreaEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Holders;
using Intermech.Localization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for AreaPropertyEditor.</summary>
public class SubjectAreaEditor : UITypeEditor
{
  private IWindowsFormsEditorService edSvc;
  private CheckedListBox clb;
  private bool blockOnCheck;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    switch (context)
    {
      case null:
      case ControlsContext _:
        return UITypeEditorEditStyle.Modal;
      default:
        return UITypeEditorEditStyle.DropDown;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sp"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (value == null)
      return value;
    this.edSvc = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    if (this.edSvc == null)
      return value;
    string s = string.Empty;
    if (value.GetType() == typeof (string))
      s = value as string;
    else if (value is SubjectAreaPropertyClass)
      s = (value as SubjectAreaPropertyClass).Areas;
    this.clb = new CheckedListBox();
    this.clb.BorderStyle = BorderStyle.None;
    this.clb.CheckOnClick = true;
    this.FillCLB(this.clb);
    this.SetClb(s);
    this.clb.ItemCheck += new ItemCheckEventHandler(this.clb_ItemCheck);
    if (this.GetEditStyle(context) == UITypeEditorEditStyle.Modal)
    {
      Form dialog = new Form();
      dialog.ShowInTaskbar = false;
      dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
      dialog.MaximizeBox = false;
      dialog.MinimizeBox = false;
      dialog.StartPosition = FormStartPosition.CenterParent;
      Panel panel = new Panel();
      dialog.Controls.Add((Control) panel);
      panel.Parent = (Control) dialog;
      panel.Dock = DockStyle.Bottom;
      panel.Height = 35;
      Button button1 = new Button();
      panel.Controls.Add((Control) button1);
      button1.Parent = (Control) panel;
      button1.Text = LocalizationHolder.rm.GetString("Client.Core_218");
      button1.DialogResult = DialogResult.OK;
      Button button2 = new Button();
      panel.Controls.Add((Control) button2);
      button2.Parent = (Control) panel;
      button2.Text = LocalizationHolder.rm.GetString("Client.Core_166");
      button2.DialogResult = DialogResult.Cancel;
      button2.Location = new Point(panel.Width - button2.Width - 8, (panel.Height - button2.Height) / 2);
      button1.Location = new Point(button2.Left - button1.Width - 8, (panel.Height - button1.Height) / 2);
      dialog.AcceptButton = (IButtonControl) button1;
      dialog.CancelButton = (IButtonControl) button2;
      dialog.Text = LocalizationHolder.rm.GetString("Client.Core_1117");
      dialog.Controls.Add((Control) this.clb);
      this.clb.Parent = (Control) dialog;
      this.clb.Dock = DockStyle.Fill;
      if (this.edSvc.ShowDialog(dialog) != DialogResult.OK)
        return !(value is SubjectAreaPropertyClass) ? (object) new SubjectAreaPropertyClass(value.ToString()) : value;
    }
    else
      this.edSvc.DropDownControl((Control) this.clb);
    string clb = this.GetClb();
    return !(clb != ((SubjectAreaPropertyClass) value).Areas) ? value : (object) new SubjectAreaPropertyClass(clb);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aCLB"></param>
  private void FillCLB(CheckedListBox aCLB)
  {
    aCLB.Items.Clear();
    DataTable dataTable = DataHolders.SubjectAreasHolder.LoadData(true);
    aCLB.Items.Add((object) new SubjectAreaPropertyClass(string.Empty));
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      aCLB.Items.Add((object) new SubjectAreaPropertyClass((string) row["F_AREA_ID"]));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  private void SetClb(string s)
  {
    this.blockOnCheck = true;
    try
    {
      if (s == string.Empty)
      {
        for (int index = 0; index < this.clb.Items.Count; ++index)
          this.clb.SetItemChecked(index, true);
      }
      else
      {
        for (int index = 0; index < this.clb.Items.Count; ++index)
          this.clb.SetItemChecked(index, false);
        for (int index = 0; index < s.Length; ++index)
          this.SetClbItemCheckedByID(s[index].ToString());
      }
    }
    finally
    {
      this.blockOnCheck = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="si"></param>
  private void SetClbItemCheckedByID(string si)
  {
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (!(((SubjectAreaPropertyClass) this.clb.Items[index]).Areas != si))
      {
        this.clb.SetItemChecked(index, true);
        break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="si"></param>
  /// <returns></returns>
  private bool GetClbItemCheckedByID(string si)
  {
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (!(((SubjectAreaPropertyClass) this.clb.Items[index]).Areas != si))
        return this.clb.GetItemChecked(index);
    }
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private string GetClb()
  {
    if (this.GetClbItemCheckedByID(string.Empty))
      return string.Empty;
    string empty = string.Empty;
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (this.clb.GetItemChecked(index))
        empty += ((SubjectAreaPropertyClass) this.clb.Items[index]).Areas;
    }
    return empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void clb_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this.blockOnCheck)
      return;
    string areas = ((SubjectAreaPropertyClass) this.clb.Items[e.Index]).Areas;
    if (areas == string.Empty)
    {
      bool flag = e.NewValue == CheckState.Checked;
      this.blockOnCheck = true;
      try
      {
        for (int index = 1; index < this.clb.Items.Count; ++index)
          this.clb.SetItemChecked(index, flag);
      }
      finally
      {
        this.blockOnCheck = false;
      }
    }
    else
    {
      if (!(areas != string.Empty) || e.NewValue != CheckState.Unchecked)
        return;
      this.blockOnCheck = true;
      try
      {
        this.clb.SetItemChecked(0, false);
      }
      finally
      {
        this.blockOnCheck = false;
      }
    }
  }
}
