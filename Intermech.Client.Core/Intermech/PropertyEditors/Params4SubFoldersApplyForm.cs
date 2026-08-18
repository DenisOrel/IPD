
// Type: Intermech.PropertyEditors.Params4SubFoldersApplyForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.PropertyEditors;

/// <summary>
/// Форма для частного случая, но с заделом для перевода её в общую с изменяющимся количеством опций
/// </summary>
public class Params4SubFoldersApplyForm : Form
{
  private bool schemaChange;
  private List<object> cbStates;
  private List<PropDescriptor> pdlist;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox cb0;
  private CheckBox cb1;
  private CheckBox cb2;
  private CheckBox cb3;
  private CheckBox cb4;
  private CheckBox cb5;
  private CheckBox cb6;
  private TextBox textBox1;
  private Button btnOk;
  private Button btnSelectAll;
  private Button btnDeselectAll;
  private Button btnCancel;
  private CheckBox cbSchemaChange;
  private CheckedListBox pluginParams;
  private ToolTip toolTip;
  private TextBox tbOptions;

  public Params4SubFoldersApplyForm() => this.InitializeComponent();

  private void btnSelectAll_Click(object sender, EventArgs e)
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is CheckBox && control.Tag is string && control.Enabled)
        (control as CheckBox).Checked = true;
    }
    if (this.cbSchemaChange.Enabled)
      this.cbSchemaChange.Checked = true;
    this.pluginParams.Items.Clear();
    for (int index = 0; index < this.pdlist.Count; ++index)
      this.pluginParams.Items.Add((object) new PluginListObj(this.pdlist[index]), true);
  }

  private void btnDeselectAll_Click(object sender, EventArgs e)
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is CheckBox && control.Tag is string && control.Enabled)
        (control as CheckBox).Checked = false;
    }
    if (this.cbSchemaChange.Enabled)
      this.cbSchemaChange.Checked = false;
    this.pluginParams.Items.Clear();
    for (int index = 0; index < this.pdlist.Count; ++index)
      this.pluginParams.Items.Add((object) new PluginListObj(this.pdlist[index]), false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aCbStates">Статусы включенности-отключенности: количество элементов = ObjectTypeLegacy.Length</param>
  /// <param name="aOldOptions">Старое значение опций .Options у типа объекта</param>
  /// <param name="aOptions">Новое значение опций .Options у типа объекта</param>
  /// <param name="objSchemaChangeFlag">Изменять схемы у существующих объектов?</param>
  /// <param name="pdList">Список дополнительных полей (от подписчиков), которые возможно требуется распространить, переключатели выводятся в доп. в списке,
  /// на выходе остаются только те поля, которые нужно применить у подтипов, то есть те, что были выбраны в отображаемом списке.
  /// при старте формы считаются отключенными</param>
  /// <returns></returns>
  public DialogResult Execute(
    List<object> aCbStates,
    ObjectTypeOptions aOldOptions,
    ObjectTypeOptions aOptions,
    out bool objSchemaChangeFlag,
    List<PropDescriptor> pdList)
  {
    this.cbStates = aCbStates;
    this.schemaChange = false;
    this.pdlist = pdList;
    CheckBox checkBoxByTagValue = this.GetCheckBoxByTagValue(6);
    if (checkBoxByTagValue != null)
    {
      string diffCaptions = ObjectTypeOptionsHelper.GetDiffCaptions(aOldOptions, aOptions);
      checkBoxByTagValue.Text = LocalizationHolder.rm.GetString("Client.Core_144");
      this.tbOptions.Text = diffCaptions;
      this.toolTip.SetToolTip((Control) checkBoxByTagValue, diffCaptions);
    }
    this.pluginParams.Items.Clear();
    if (this.pdlist != null)
    {
      for (int index = 0; index < this.pdlist.Count; ++index)
        this.pluginParams.Items.Add((object) new PluginListObj(this.pdlist[index]), true);
    }
    int num = (int) this.ShowDialog();
    objSchemaChangeFlag = this.schemaChange;
    return (DialogResult) num;
  }

  private CheckBox GetCheckBoxByTagValue(int tagValue)
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is CheckBox && control.Tag is string && control.Tag.ToString() == tagValue.ToString())
        return (CheckBox) control;
    }
    return (CheckBox) null;
  }

  private void Params4SubFoldersApplyForm_Load(object sender, EventArgs e)
  {
    this.cbSchemaChange.Enabled = false;
    this.cbSchemaChange.Checked = false;
    for (int index = 0; index < this.cbStates.Count; ++index)
    {
      CheckBox checkBoxByTagValue = this.GetCheckBoxByTagValue(index);
      if (checkBoxByTagValue == null)
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_145") + index.ToString());
      checkBoxByTagValue.Enabled = true;
      if (index == 6)
        this.tbOptions.Enabled = true;
      if (this.cbStates[index] != null)
      {
        checkBoxByTagValue.Checked = (bool) this.cbStates[index];
        if (index == 5)
          this.cbSchemaChange.Enabled = true;
      }
      else
      {
        checkBoxByTagValue.Enabled = false;
        checkBoxByTagValue.Checked = false;
        if (index == 6)
          this.tbOptions.Enabled = false;
      }
    }
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    int count = this.cbStates.Count;
    this.cbStates.Clear();
    for (int tagValue = 0; tagValue < count; ++tagValue)
    {
      CheckBox checkBoxByTagValue = this.GetCheckBoxByTagValue(tagValue);
      if (checkBoxByTagValue != null)
      {
        if (checkBoxByTagValue.Enabled)
        {
          this.cbStates.Add((object) checkBoxByTagValue.Checked);
          if (tagValue == 5)
            this.schemaChange = this.cbSchemaChange.Checked;
        }
        else
          this.cbStates.Add((object) null);
      }
    }
    this.pdlist.Clear();
    foreach (PluginListObj checkedItem in this.pluginParams.CheckedItems)
      this.pdlist.Add(checkedItem.propDescriptor);
  }

  private void Params4SubFoldersApplyForm_FormClosing(object sender, FormClosingEventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Params4SubFoldersApplyForm));
    this.cb0 = new CheckBox();
    this.cb1 = new CheckBox();
    this.cb2 = new CheckBox();
    this.cb3 = new CheckBox();
    this.cb4 = new CheckBox();
    this.cb5 = new CheckBox();
    this.cb6 = new CheckBox();
    this.textBox1 = new TextBox();
    this.btnOk = new Button();
    this.btnSelectAll = new Button();
    this.btnDeselectAll = new Button();
    this.btnCancel = new Button();
    this.cbSchemaChange = new CheckBox();
    this.pluginParams = new CheckedListBox();
    this.toolTip = new ToolTip(this.components);
    this.tbOptions = new TextBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cb0, "cb0");
    this.cb0.Name = "cb0";
    this.cb0.Tag = (object) "0";
    this.cb0.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb1, "cb1");
    this.cb1.Name = "cb1";
    this.cb1.Tag = (object) "1";
    this.cb1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb2, "cb2");
    this.cb2.Name = "cb2";
    this.cb2.Tag = (object) "2";
    this.cb2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb3, "cb3");
    this.cb3.Name = "cb3";
    this.cb3.Tag = (object) "3";
    this.cb3.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb4, "cb4");
    this.cb4.Name = "cb4";
    this.cb4.Tag = (object) "6";
    this.cb4.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb5, "cb5");
    this.cb5.Name = "cb5";
    this.cb5.Tag = (object) "4";
    this.cb5.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb6, "cb6");
    this.cb6.Name = "cb6";
    this.cb6.Tag = (object) "5";
    this.cb6.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.BorderStyle = BorderStyle.None;
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.Yes;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnSelectAll, "btnSelectAll");
    this.btnSelectAll.Name = "btnSelectAll";
    this.btnSelectAll.UseVisualStyleBackColor = true;
    this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
    componentResourceManager.ApplyResources((object) this.btnDeselectAll, "btnDeselectAll");
    this.btnDeselectAll.Name = "btnDeselectAll";
    this.btnDeselectAll.UseVisualStyleBackColor = true;
    this.btnDeselectAll.Click += new EventHandler(this.btnDeselectAll_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.No;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbSchemaChange, "cbSchemaChange");
    this.cbSchemaChange.Name = "cbSchemaChange";
    this.cbSchemaChange.UseVisualStyleBackColor = true;
    this.pluginParams.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.pluginParams, "pluginParams");
    this.pluginParams.Name = "pluginParams";
    this.tbOptions.BorderStyle = BorderStyle.None;
    componentResourceManager.ApplyResources((object) this.tbOptions, "tbOptions");
    this.tbOptions.Name = "tbOptions";
    this.tbOptions.ReadOnly = true;
    this.tbOptions.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tbOptions);
    this.Controls.Add((Control) this.pluginParams);
    this.Controls.Add((Control) this.cbSchemaChange);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnDeselectAll);
    this.Controls.Add((Control) this.btnSelectAll);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.cb6);
    this.Controls.Add((Control) this.cb5);
    this.Controls.Add((Control) this.cb4);
    this.Controls.Add((Control) this.cb3);
    this.Controls.Add((Control) this.cb2);
    this.Controls.Add((Control) this.cb1);
    this.Controls.Add((Control) this.cb0);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.Name = nameof (Params4SubFoldersApplyForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.Params4SubFoldersApplyForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
