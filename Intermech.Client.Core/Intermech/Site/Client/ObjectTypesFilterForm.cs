
// Type: Intermech.Site.Client.ObjectTypesFilterForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Site.Client;

public class ObjectTypesFilterForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ObjectTypesSelectControl objectTypesTree;
  private Button bCancel;
  private Button bOK;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private CheckBox checkBox1;
  private RadioButton radioButton1;
  private SplitContainer splitContainer1;

  public ObjectTypesFilterForm() => this.InitializeComponent();

  public void LoadData(
    List<int> enableTypes,
    List<int> savedEnableTypes,
    List<int> savedDisableTypes,
    int accessLevel = 0)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.FillSecurityLevels(sessionKeeper.Session);
      this.AccessLevel = accessLevel;
      this.RefreshObjectTypesTree(enableTypes, savedDisableTypes, savedDisableTypes);
    }
  }

  public void RefreshObjectTypesTree(
    List<int> enableTypes,
    List<int> savedEnableTypes,
    List<int> savedDisableTypes)
  {
    this.objectTypesTree.BuildTree((IList<int>) (enableTypes ?? new List<int>(0)), -1, true);
    this.objectTypesTree.CheckNodes(true, savedDisableTypes ?? (savedEnableTypes != null ? enableTypes.Except<int>((IEnumerable<int>) savedEnableTypes).ToList<int>() : (List<int>) null));
    if (this.objectTypesTree.TreeView.Nodes.Count != 1)
      return;
    this.objectTypesTree.TreeView.Nodes[0].Expand();
  }

  public List<int> FilteredObjectTypes => this.objectTypesTree.UncheckedObjectTypes;

  private void ObjectTypesFilterForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ObjectTypesFilter_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void FillSecurityLevels(IUserSession session)
  {
    int securityLevel = session.SecurityLevel;
    DataTable possibleValues = session.GetAttributeType(new Guid("cad00816-306c-11d8-b4e9-00304f19f545")).GetPossibleValues();
    Point point = this.radioButton1.Location;
    for (int index = 0; index < possibleValues.Rows.Count; ++index)
    {
      DataRow row = possibleValues.Rows[index];
      RadioButton rb;
      if (index == 0)
      {
        rb = this.radioButton1;
      }
      else
      {
        rb = new RadioButton();
        rb.AutoSize = true;
        this.groupBox1.Controls.Add((Control) rb);
        point = new Point(point.X, point.Y + rb.Font.Height + 12);
        rb.Location = point;
      }
      this.SetRadioButton(rb, row);
      if ((int) rb.Tag > securityLevel)
        rb.Enabled = false;
    }
  }

  private void SetRadioButton(RadioButton rb, DataRow row)
  {
    rb.Text = Convert.ToString(row["F_DESCRIPTION"]);
    rb.Tag = (object) Convert.ToInt32(row["F_INTEGER_VALUE"]);
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    this.objectTypesTree.CheckNodes(this.checkBox1.Checked, (List<int>) null);
  }

  public int AccessLevel
  {
    get
    {
      foreach (Control control in (ArrangedElementCollection) this.groupBox1.Controls)
      {
        if (control is RadioButton radioButton && radioButton.Checked)
          return (int) radioButton.Tag;
      }
      return 0;
    }
    set
    {
      foreach (Control control in (ArrangedElementCollection) this.groupBox1.Controls)
      {
        if (control is RadioButton radioButton && (int) radioButton.Tag == value)
        {
          radioButton.Checked = true;
          break;
        }
      }
    }
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
    this.bCancel = new Button();
    this.bOK = new Button();
    this.groupBox1 = new GroupBox();
    this.radioButton1 = new RadioButton();
    this.groupBox2 = new GroupBox();
    this.objectTypesTree = new ObjectTypesSelectControl();
    this.checkBox1 = new CheckBox();
    this.splitContainer1 = new SplitContainer();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(459, 311);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 1;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(332, 311);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 2;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.groupBox1.Controls.Add((Control) this.radioButton1);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.MinimumSize = new Size(120, 153);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(241, 289);
    this.groupBox1.TabIndex = 3;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Разрешенные уровни доступа";
    this.radioButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.radioButton1.AutoSize = true;
    this.radioButton1.Checked = true;
    this.radioButton1.Location = new Point(15, 28);
    this.radioButton1.Name = "radioButton1";
    this.radioButton1.Size = new Size(85, 17);
    this.radioButton1.TabIndex = 0;
    this.radioButton1.TabStop = true;
    this.radioButton1.Text = "radioButton1";
    this.radioButton1.UseVisualStyleBackColor = true;
    this.groupBox2.Controls.Add((Control) this.objectTypesTree);
    this.groupBox2.Dock = DockStyle.Fill;
    this.groupBox2.Location = new Point(0, 0);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Padding = new Padding(7);
    this.groupBox2.Size = new Size(330, 289);
    this.groupBox2.TabIndex = 4;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Типы объектов";
    this.objectTypesTree.Dock = DockStyle.Fill;
    this.objectTypesTree.Location = new Point(7, 20);
    this.objectTypesTree.Name = "objectTypesTree";
    this.objectTypesTree.Size = new Size(316, 262);
    this.objectTypesTree.TabIndex = 0;
    this.checkBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.checkBox1.AutoSize = true;
    this.checkBox1.Location = new Point(12, 313);
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.Size = new Size(91, 17);
    this.checkBox1.TabIndex = 5;
    this.checkBox1.Text = "Выбрать все";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer1.Location = new Point(5, 12);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.groupBox2);
    this.splitContainer1.Panel2.Controls.Add((Control) this.groupBox1);
    this.splitContainer1.Size = new Size(575, 289);
    this.splitContainer1.SplitterDistance = 330;
    this.splitContainer1.TabIndex = 6;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(592, 348);
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(450, 260);
    this.Name = nameof (ObjectTypesFilterForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Типы объектов, разрешенных к публикации";
    this.FormClosing += new FormClosingEventHandler(this.ObjectTypesFilter_FormClosing);
    this.Load += new EventHandler(this.ObjectTypesFilterForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class SecurityLevelItem
  {
    public int Value { get; private set; }

    public string Caption { get; private set; }

    public SecurityLevelItem(int val, string caption)
    {
      this.Value = val;
      this.Caption = caption;
    }

    public override string ToString() => this.Caption;
  }
}
