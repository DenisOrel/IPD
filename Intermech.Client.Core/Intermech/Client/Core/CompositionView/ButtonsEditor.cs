
// Type: Intermech.Client.Core.CompositionView.ButtonsEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Редактор кнопок</summary>
public class ButtonsEditor : Form
{
  /// <summary>Тип объектов, для которого вызван редактор</summary>
  private Guid _objTypeGuid = Guid.Empty;
  /// <summary>
  /// 
  /// </summary>
  private CommonButtonService _commonBS;
  /// <summary>
  /// 
  /// </summary>
  private CustomButtonService _customBS;
  /// <summary>
  /// 
  /// </summary>
  private ImageList _imageList = new ImageList();
  /// <summary>
  /// 
  /// </summary>
  private bool _modified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button bOk;
  private Button bCancel;
  private Button bDelete;
  private Button bClear;
  private ListView listView1;
  private Button bAdd;
  private ContextMenuStrip contextMenuStrip1;
  private Button bSettings;
  private Button bDown;
  private Button bUp;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.listView1.SmallImageList = this._imageList;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1618);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeServices()
  {
    this._commonBS = CompositionViewHolder.Services.GetService(typeof (CommonButtonService)) as CommonButtonService;
    this._customBS = CompositionViewHolder.Services.GetService(typeof (CustomButtonService)) as CustomButtonService;
    if (!(ServicesManager.GetService(typeof (CompositionViewButtons)) is CompositionViewButtons service))
      return;
    foreach (KeyValuePair<System.Type, string> keyValuePair in service)
    {
      ToolStripItem toolStripItem = (ToolStripItem) new ToolStripMenuItem(keyValuePair.Value);
      toolStripItem.Tag = (object) keyValuePair.Key;
      toolStripItem.Click += new EventHandler(this.ToolStripMenuItem_Click);
      this.contextMenuStrip1.Items.Add(toolStripItem);
    }
  }

  /// <summary>Загрузка настроек для типа</summary>
  /// <param name="typeGuid"></param>
  private void LoadData(Guid typeGuid)
  {
    List<CVButtonBase> cvButtonBaseList = this._commonBS.GetButtonsList(typeGuid);
    List<CVButtonBase> buttonsList = this._customBS.GetButtonsList(typeGuid);
    if (buttonsList != null && buttonsList.Count > 0)
      cvButtonBaseList = buttonsList;
    this.listView1.BeginUpdate();
    try
    {
      this.listView1.Items.Clear();
      foreach (CVButtonBase cvButtonBase in cvButtonBaseList)
        this.listView1.Items.Add(new ListViewItem(cvButtonBase.ToString())
        {
          ImageIndex = this._imageList.Images.AddStrip(cvButtonBase.Image),
          Tag = (object) cvButtonBase
        });
      this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }
    finally
    {
      this.listView1.EndUpdate();
    }
    this.UpdateControls();
  }

  /// <summary>Cохранение изменения в настройках</summary>
  private void SaveData()
  {
    List<CVButtonBase> buttons = new List<CVButtonBase>();
    foreach (ListViewItem listViewItem in this.listView1.Items)
      buttons.Add(listViewItem.Tag as CVButtonBase);
    if (CompositionViewHolder.IsAdmin)
    {
      switch (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_16"), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
      {
        case DialogResult.Yes:
          this._customBS.ClearButtons(this._objTypeGuid);
          this._commonBS.ClearButtons(this._objTypeGuid);
          this._commonBS.AddButton(this._objTypeGuid, buttons);
          this._customBS.SaveToBase();
          this._commonBS.SaveToBase();
          break;
        case DialogResult.No:
          this._customBS.ClearButtons(this._objTypeGuid);
          this._customBS.AddButton(this._objTypeGuid, buttons);
          this._customBS.SaveToBase();
          break;
      }
    }
    else
    {
      this._customBS.ClearButtons(this._objTypeGuid);
      this._customBS.AddButton(this._objTypeGuid, buttons);
      this._customBS.SaveToBase();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls()
  {
    int count = this.listView1.SelectedItems.Count;
    this.bDelete.Enabled = count > 0;
    this.bSettings.Enabled = count.Equals(1);
    if (count > 0 && (!(this.listView1.SelectedItems[0].Tag is CVButtonBase tag) || tag.Node != null))
      this.bSettings.Enabled = false;
    this.bUp.Enabled = count > 0 && this.listView1.SelectedItems[0].Index > 0;
    this.bDown.Enabled = count > 0 && this.listView1.SelectedItems[count - 1].Index < this.listView1.Items.Count - 1;
  }

  /// <summary>Тип объектов, для которого вызван редактор</summary>
  /// <param name="typeGuid"></param>
  public ButtonsEditor(Guid typeGuid)
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.InitializeServices();
    this._objTypeGuid = typeGuid;
    this.LoadData(typeGuid);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ButtonsEditor_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ButtonsEditor_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bAdd_MouseUp(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Left))
      return;
    this.contextMenuStrip1.Show((Control) this.bAdd, new Point(e.X, e.Y));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bDelete_Click(object sender, EventArgs e)
  {
    ListViewItem[] dest = new ListViewItem[this.listView1.SelectedItems.Count];
    this.listView1.SelectedItems.CopyTo((Array) dest, 0);
    this.listView1.BeginUpdate();
    try
    {
      foreach (ListViewItem listViewItem in dest)
      {
        this.listView1.Items.Remove(listViewItem);
        this._modified = true;
      }
    }
    finally
    {
      this.listView1.EndUpdate();
    }
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bClear_Click(object sender, EventArgs e)
  {
    if (this.listView1.Items.Count > 0)
    {
      this.listView1.Items.Clear();
      this._modified = true;
    }
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bSettings_Click(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.listView1.SelectedItems[0];
    if (!(selectedItem.Tag is CVButtonBase tag))
      return;
    CVButtonBase button = tag.Clone();
    using (ButtonParamsEditor buttonParamsEditor = new ButtonParamsEditor(button.Params))
    {
      if (!buttonParamsEditor.ShowDialog().Equals((object) DialogResult.OK))
        return;
      tag.ApplyParams(button);
      selectedItem.Text = tag.ToString();
      selectedItem.ImageIndex = this._imageList.Images.AddStrip(tag.Image);
      this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      this._modified = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bUp_Click(object sender, EventArgs e)
  {
    ListViewItem[] dest = new ListViewItem[this.listView1.SelectedItems.Count];
    this.listView1.SelectedItems.CopyTo((Array) dest, 0);
    int num = dest[0].Index - 1;
    this.listView1.BeginUpdate();
    try
    {
      for (int index = 0; index < dest.Length; ++index)
      {
        ListViewItem listViewItem = dest[index];
        listViewItem.Remove();
        this.listView1.Items.Insert(num++, listViewItem);
        this._modified = true;
      }
    }
    finally
    {
      this.listView1.EndUpdate();
    }
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bDown_Click(object sender, EventArgs e)
  {
    ListViewItem[] dest = new ListViewItem[this.listView1.SelectedItems.Count];
    this.listView1.SelectedItems.CopyTo((Array) dest, 0);
    int num = dest[this.listView1.SelectedItems.Count - 1].Index + 1;
    this.listView1.BeginUpdate();
    try
    {
      for (int index = dest.Length - 1; index >= 0; --index)
      {
        ListViewItem listViewItem = dest[index];
        listViewItem.Remove();
        this.listView1.Items.Insert(num--, listViewItem);
        this._modified = true;
      }
    }
    finally
    {
      this.listView1.EndUpdate();
    }
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bOk_Click(object sender, EventArgs e)
  {
    if (!this._modified)
    {
      this.DialogResult = DialogResult.Cancel;
    }
    else
    {
      this.SaveData();
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void listView1_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (!(sender is ToolStripItem toolStripItem) || (object) (toolStripItem.Tag as System.Type) == null || !(Activator.CreateInstance(toolStripItem.Tag as System.Type) is CVButtonBase instance) || !instance.Select())
      return;
    this.listView1.Items.Add(new ListViewItem(instance.ToString())
    {
      ImageIndex = this._imageList.Images.AddStrip(instance.Image),
      Tag = (object) instance
    });
    this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this._modified = true;
    this.UpdateControls();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ButtonsEditor));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.bDelete = new Button();
    this.bClear = new Button();
    this.listView1 = new ListView();
    this.bAdd = new Button();
    this.bSettings = new Button();
    this.bOk = new Button();
    this.bCancel = new Button();
    this.bDown = new Button();
    this.bUp = new Button();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.bDelete, 2, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.bClear, 2, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.listView1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.bAdd, 2, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.bSettings, 2, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.bOk, 1, 9);
    this.tableLayoutPanel1.Controls.Add((Control) this.bCancel, 2, 9);
    this.tableLayoutPanel1.Controls.Add((Control) this.bDown, 2, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.bUp, 2, 6);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.bDelete, "bDelete");
    this.bDelete.Name = "bDelete";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    componentResourceManager.ApplyResources((object) this.bClear, "bClear");
    this.bClear.Name = "bClear";
    this.bClear.UseVisualStyleBackColor = true;
    this.bClear.Click += new EventHandler(this.bClear_Click);
    this.tableLayoutPanel1.SetColumnSpan((Control) this.listView1, 2);
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.FullRowSelect = true;
    this.listView1.GridLines = true;
    this.listView1.HideSelection = false;
    this.listView1.Name = "listView1";
    this.tableLayoutPanel1.SetRowSpan((Control) this.listView1, 9);
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.List;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.Name = "bAdd";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.MouseUp += new MouseEventHandler(this.bAdd_MouseUp);
    componentResourceManager.ApplyResources((object) this.bSettings, "bSettings");
    this.bSettings.Name = "bSettings";
    this.bSettings.UseVisualStyleBackColor = true;
    this.bSettings.Click += new EventHandler(this.bSettings_Click);
    componentResourceManager.ApplyResources((object) this.bOk, "bOk");
    this.bOk.Name = "bOk";
    this.bOk.UseVisualStyleBackColor = true;
    this.bOk.Click += new EventHandler(this.bOk_Click);
    this.bCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bDown, "bDown");
    this.bDown.Name = "bDown";
    this.bDown.UseVisualStyleBackColor = true;
    this.bDown.Click += new EventHandler(this.bDown_Click);
    componentResourceManager.ApplyResources((object) this.bUp, "bUp");
    this.bUp.Name = "bUp";
    this.bUp.UseVisualStyleBackColor = true;
    this.bUp.Click += new EventHandler(this.bUp_Click);
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this.AcceptButton = (IButtonControl) this.bOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ButtonsEditor);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.ButtonsEditor_Load);
    this.FormClosed += new FormClosedEventHandler(this.ButtonsEditor_FormClosed);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
