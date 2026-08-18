
// Type: Intermech.Navigator.SelectionView.LifecycleStepSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>Summary description for LifecycleStepSelector.</summary>
public class LifecycleStepSelector : Form
{
  private int objectTypeID;
  private int lifecycleStepID;
  private Panel panel1;
  private GroupBox groupBox1;
  private TextBox textBoxObjType;
  private Button buttonOk;
  private Button buttonCancel;
  private Button buttonObjType;
  private Label labelObjType;
  private ComboBox comboBoxLifecycleStep;
  private Label labelLifecycleStep;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  private int ObjectTypeID
  {
    get => this.objectTypeID;
    set
    {
      if (this.objectTypeID == value)
        return;
      this.comboBoxLifecycleStep.Items.Clear();
      string str = "";
      this.objectTypeID = value;
      if (this.objectTypeID != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper != null)
          {
            IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.objectTypeID);
            if (objectType != null)
              str = objectType.ObjectTypeName;
            IDBLifecycleStepCollection lifecycleStepCollection = sessionKeeper.Session.GetLifecycleStepCollection(this.objectTypeID);
            if (lifecycleStepCollection != null)
            {
              DataSet schema = lifecycleStepCollection.GetSchema();
              if (schema != null)
              {
                DataRow[] dataRowArray = schema.Tables["IMS_LC_STEPS"].Select();
                if (dataRowArray != null)
                {
                  foreach (DataRow dataRow in dataRowArray)
                    this.comboBoxLifecycleStep.Items.Add((object) new localLifecycleStep(Convert.ToInt32(dataRow["F_LC_STEP"]), Convert.ToString(dataRow["F_LC_NAME"])));
                }
              }
            }
          }
        }
      }
      this.textBoxObjType.Text = str;
      this.comboBoxLifecycleStep.Enabled = this.comboBoxLifecycleStep.Items.Count > 0;
      this.comboBoxLifecycleStep.SelectedIndex = this.comboBoxLifecycleStep.Items.Count > 0 ? 0 : -1;
    }
  }

  public int LifecycleStepID => this.lifecycleStepID;

  public LifecycleStepSelector() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LifecycleStepSelector));
    this.buttonObjType = new Button();
    this.textBoxObjType = new TextBox();
    this.panel1 = new Panel();
    this.buttonCancel = new Button();
    this.buttonOk = new Button();
    this.groupBox1 = new GroupBox();
    this.labelObjType = new Label();
    this.comboBoxLifecycleStep = new ComboBox();
    this.labelLifecycleStep = new Label();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonObjType, "buttonObjType");
    this.buttonObjType.Name = "buttonObjType";
    this.buttonObjType.Click += new EventHandler(this.buttonObjType_Click);
    componentResourceManager.ApplyResources((object) this.textBoxObjType, "textBoxObjType");
    this.textBoxObjType.BackColor = SystemColors.Window;
    this.textBoxObjType.Name = "textBoxObjType";
    this.textBoxObjType.ReadOnly = true;
    this.panel1.Controls.Add((Control) this.buttonCancel);
    this.panel1.Controls.Add((Control) this.buttonOk);
    this.panel1.Controls.Add((Control) this.groupBox1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelObjType, "labelObjType");
    this.labelObjType.Name = "labelObjType";
    componentResourceManager.ApplyResources((object) this.comboBoxLifecycleStep, "comboBoxLifecycleStep");
    this.comboBoxLifecycleStep.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxLifecycleStep.Name = "comboBoxLifecycleStep";
    this.comboBoxLifecycleStep.SelectedIndexChanged += new EventHandler(this.comboBoxLifecycleStep_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.labelLifecycleStep, "labelLifecycleStep");
    this.labelLifecycleStep.Name = "labelLifecycleStep";
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.labelLifecycleStep);
    this.Controls.Add((Control) this.comboBoxLifecycleStep);
    this.Controls.Add((Control) this.labelObjType);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.buttonObjType);
    this.Controls.Add((Control) this.textBoxObjType);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (LifecycleStepSelector);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void buttonObjType_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_392"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    this.ObjectTypeID = Convert.ToInt32(selectorForm.IDList[0]);
  }

  private void comboBoxLifecycleStep_SelectedIndexChanged(object sender, EventArgs e)
  {
    localLifecycleStep selectedItem = (localLifecycleStep) this.comboBoxLifecycleStep.SelectedItem;
    if (selectedItem != null)
      this.lifecycleStepID = selectedItem.Step;
    this.buttonOk.Enabled = this.comboBoxLifecycleStep.SelectedIndex > -1;
  }
}
