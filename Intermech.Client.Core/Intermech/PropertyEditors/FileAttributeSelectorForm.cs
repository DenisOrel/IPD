
// Type: Intermech.PropertyEditors.FileAttributeSelectorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FileAttributeSelectorForm.</summary>
public class FileAttributeSelectorForm : Form
{
  private Button buttonOk;
  private Button buttonCancel;
  private ListView listView;
  private ColumnHeader columnHeader;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private ArrayList selectedAttrs;

  internal event FileAttributeSelectorForm.BeforeClosingEventHandler BeforeClosing;

  public FileAttributeSelectorForm() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileAttributeSelectorForm));
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.listView = new ListView();
    this.columnHeader = new ColumnHeader();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.CheckBoxes = true;
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader
    });
    this.listView.FullRowSelect = true;
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.columnHeader, "columnHeader");
    this.AcceptButton = (IButtonControl) this.buttonOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonOk);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.Name = nameof (FileAttributeSelectorForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }

  public DialogResult SelectDialog(ArrayList attrSelObjects, out ArrayList attrsSelected)
  {
    FileAttributeStatics.InitImageList();
    this.listView.SmallImageList = FileAttributeStatics.imageList;
    this.listView.Items.Clear();
    this.selectedAttrs = (ArrayList) null;
    for (int index = 0; index < attrSelObjects.Count; ++index)
      this.listView.Items.Add(attrSelObjects[index].ToString(), FileAttributeStatics.FieldTypeToImageIndex(((AttrSelObject) attrSelObjects[index]).type)).Tag = attrSelObjects[index];
    this.listView.Sort();
    int num = (int) this.ShowDialog();
    attrsSelected = this.selectedAttrs;
    return (DialogResult) num;
  }

  private void FillSelected(out ArrayList attrsSelected)
  {
    attrsSelected = new ArrayList();
    ListView.CheckedListViewItemCollection checkedItems = this.listView.CheckedItems;
    if (checkedItems.Count == 0)
    {
      if (this.listView.SelectedItems == null)
        return;
      for (int index = 0; index < this.listView.SelectedItems.Count; ++index)
        attrsSelected.Add(this.listView.SelectedItems[index].Tag);
    }
    else
    {
      for (int index = 0; index < checkedItems.Count; ++index)
        attrsSelected.Add(checkedItems[index].Tag);
    }
  }

  private void buttonOk_Click(object sender, EventArgs e)
  {
    if (this.listView.CheckedItems.Count == 0 && this.listView.SelectedItems.Count == 0)
    {
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this.FillSelected(out this.selectedAttrs);
      if (this.BeforeClosing == null)
        return;
      CancelEventArgs e1 = new CancelEventArgs(false);
      this.BeforeClosing((object) this, e1, this.selectedAttrs);
      if (!e1.Cancel)
        return;
      this.DialogResult = DialogResult.None;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <param name="selected">list of AttrSelObject</param>
  internal delegate void BeforeClosingEventHandler(
    object sender,
    CancelEventArgs e,
    ArrayList selected);
}
