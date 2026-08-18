// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.LCStepSetupForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class LCStepSetupForm : FormEx
{
  private LCInfo _lcInfo;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private ButtonEdit TypeBox;
  private Label label1;
  private RadioButton LSButton;
  private RadioButton LCButton;
  private GroupBox groupBox1;
  private ComboBoxEx LSBox;
  private ComboBoxEx LCBox;

  public LCStepSetupForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1304);
    this.LSBox.Bounds = this.LCBox.Bounds;
    this.FillLCLevels();
  }

  private void FillLCLevels()
  {
    this.LCBox.ImageList = BaseHolder.IconService.ImageList;
    if (!(ApplicationServices.Container.GetService(typeof (IClientMetadataCache)) is IClientMetadataCache service))
      return;
    foreach (DataRow row in (InternalDataCollectionBase) service.GetLifecycleLevelCollection().Select("").Rows)
    {
      int int32 = Convert.ToInt32(row["F_LEVEL_ID"]);
      int imageindex = BaseHolder.IconService.IndexOf(8, int32);
      this.LCBox.Items.Add((object) new IDComboItem(row["F_LEVEL_NAME"].ToString(), (long) int32, imageindex));
    }
    this.LCBox.Sorted = true;
    this.LCBox.SelectedIndex = 0;
  }

  private void FillLCSteps(int objType)
  {
    this.LSBox.ImageList = BaseHolder.IconService.ImageList;
    this.LSBox.BeginUpdate();
    this.LSBox.Items.Clear();
    try
    {
      if (objType == -1)
      {
        this.LCButton.Checked = true;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataRow[] dataRowArray = sessionKeeper.Session.GetLifecycleStepCollection(objType).GetSchema().Tables["IMS_LC_STEPS"].Select();
          if (dataRowArray != null)
          {
            foreach (DataRow dataRow in dataRowArray)
            {
              int int32_1 = Convert.ToInt32(dataRow["F_LC_STEP"]);
              int int32_2 = Convert.ToInt32(dataRow["F_LEVEL_ID"]);
              int imageindex = BaseHolder.IconService.IndexOf(8, int32_2);
              this.LSBox.Items.Add((object) new IDComboItem(dataRow["F_LC_NAME"].ToString(), (long) int32_1, imageindex)
              {
                Data = (object) int32_2
              });
            }
          }
          this.LSBox.Sorted = true;
          this.LSBox.SelectedIndex = 0;
        }
      }
    }
    finally
    {
      this.LSBox.EndUpdate();
    }
  }

  private void TypeBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    using (SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString(sc_21836.ssp_workflow_21837()), 4, false))
    {
      if (selectorForm.ShowDialog() == DialogResult.OK)
      {
        if (selectorForm.IDList.Count > 0)
        {
          int int32 = Convert.ToInt32(selectorForm.IDList[0]);
          this.TypeBox.Tag = (object) int32;
          this.TypeBox.Text = selectorForm.NameList[0].ToString();
          this.FillLCSteps(int32);
        }
        else
        {
          this.TypeBox.Tag = (object) -1;
          this.TypeBox.Text = wfConsts.AllObjectsCaption;
          this.FillLCSteps(-1);
        }
      }
    }
    this.UpdateEnabled();
  }

  private void UpdateEnabled()
  {
    bool flag = this.TypeBox.Tag != null;
    this.LSButton.Enabled = flag && !-1.Equals(this.TypeBox.Tag);
    this.LSBox.Enabled = flag;
    this.OkButton.Enabled = flag;
  }

  private void LCStepSetupForm_Load(object sender, EventArgs e) => this.UpdateEnabled();

  public LCInfo LCInfo
  {
    get
    {
      if (this._lcInfo == null)
        this._lcInfo = new LCInfo();
      this._lcInfo.ObjectType = Convert.ToInt32(this.TypeBox.Tag);
      System.Windows.Forms.ComboBox comboBox;
      if (this.LCButton.Checked)
      {
        this._lcInfo.Kind = LCKind.Level;
        comboBox = (System.Windows.Forms.ComboBox) this.LCBox;
      }
      else
      {
        this._lcInfo.Kind = LCKind.Step;
        this._lcInfo.LevelID = Convert.ToInt32((this.LSBox.SelectedItem as IDComboItem).Data);
        comboBox = (System.Windows.Forms.ComboBox) this.LSBox;
      }
      if (comboBox != null)
        this._lcInfo.StepID = Convert.ToInt32(((IDComboItem) comboBox.SelectedItem).ID);
      return this._lcInfo;
    }
    set
    {
      this._lcInfo = new LCInfo();
      this._lcInfo.Assign(value);
      this.TypeBox.Tag = (object) this._lcInfo.ObjectType;
      try
      {
        this.FillLCSteps(this._lcInfo.ObjectType);
      }
      catch
      {
      }
      if (this._lcInfo.Kind == LCKind.Level)
      {
        this.LCButton.Checked = true;
        for (int index = 0; index < this.LCBox.Items.Count; ++index)
        {
          if (((IDComboItem) this.LCBox.Items[index]).ID == (long) this._lcInfo.StepID)
          {
            this.LCBox.SelectedIndex = index;
            break;
          }
        }
      }
      else
      {
        this.LSButton.Checked = true;
        for (int index = 0; index < this.LSBox.Items.Count; ++index)
        {
          if (((IDComboItem) this.LSBox.Items[index]).ID == (long) this._lcInfo.StepID)
          {
            this.LSBox.SelectedIndex = index;
            break;
          }
        }
      }
      if (this._lcInfo.ObjectType == -1)
        this.TypeBox.Text = wfConsts.AllObjectsCaption;
      else
        this.TypeBox.Text = this._lcInfo.TypeName;
    }
  }

  private void LCButton_CheckedChanged(object sender, EventArgs e)
  {
    this.LCBox.Visible = this.LCButton.Checked;
    this.LSBox.Visible = !this.LCButton.Checked;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LCStepSetupForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.TypeBox = new ButtonEdit();
    this.label1 = new Label();
    this.LSButton = new RadioButton();
    this.LCButton = new RadioButton();
    this.groupBox1 = new GroupBox();
    this.LCBox = new ComboBoxEx();
    this.LSBox = new ComboBoxEx();
    this.Panel2.SuspendLayout();
    this.TypeBox.Properties.BeginInit();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    componentResourceManager.ApplyResources((object) this.TypeBox, "TypeBox");
    this.TypeBox.Name = "TypeBox";
    this.TypeBox.Properties.BorderStyle = BorderStyles.Flat;
    this.TypeBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 12, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.TypeBox.Properties.ReadOnly = true;
    this.TypeBox.ButtonClick += new ButtonPressedEventHandler(this.TypeBox_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.LSButton, "LSButton");
    this.LSButton.Name = "LSButton";
    this.LSButton.UseVisualStyleBackColor = true;
    this.LSButton.CheckedChanged += new EventHandler(this.LCButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.LCButton, "LCButton");
    this.LCButton.Checked = true;
    this.LCButton.Name = "LCButton";
    this.LCButton.TabStop = true;
    this.LCButton.UseVisualStyleBackColor = true;
    this.LCButton.CheckedChanged += new EventHandler(this.LCButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.LCBox);
    this.groupBox1.Controls.Add((Control) this.LSBox);
    this.groupBox1.Controls.Add((Control) this.LSButton);
    this.groupBox1.Controls.Add((Control) this.LCButton);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.LCBox, "LCBox");
    this.LCBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.LCBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.LCBox.FormattingEnabled = true;
    this.LCBox.ImageList = (ImageList) null;
    this.LCBox.Name = "LCBox";
    componentResourceManager.ApplyResources((object) this.LSBox, "LSBox");
    this.LSBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.LSBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.LSBox.FormattingEnabled = true;
    this.LSBox.ImageList = (ImageList) null;
    this.LSBox.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("LSBox.Items")
    });
    this.LSBox.Name = "LSBox";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.TypeBox);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (LCStepSetupForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.LCStepSetupForm_Load);
    this.Panel2.ResumeLayout(false);
    this.TypeBox.Properties.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
