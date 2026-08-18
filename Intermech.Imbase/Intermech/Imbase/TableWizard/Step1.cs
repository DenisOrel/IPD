// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step1
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.DataFormats;
using Intermech.Imbase.TableWizard.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

[Serializable]
internal class Step1 : UserControl, IImbaseTableStep
{
  private ImbaseTableWizardForm _wizardForm;
  private Dictionary<System.Type, object> _context;
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private RadioButton rbExistTable;
  private RadioButton rbNewTable;
  private Label label3;
  private TableLayoutPanel tableSelect;
  private ButtonEdit _btnEdit;
  private GroupBox groupBox1;
  private Label label1;
  private Label label2;

  public Step1()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
  }

  public ImbaseTableWizardForm WizardForm
  {
    set
    {
      this._wizardForm = value;
      this._wizardForm._btnNext.Enabled = true;
      this._wizardForm._btnPrev.Enabled = false;
      this._wizardForm._btnFinish.Enabled = false;
      this._wizardForm.Text = LocalizationHolder.rm.GetString("Imbase.ImbaseTableWizard.Caption.TableRef");
    }
  }

  public Dictionary<System.Type, object> Context
  {
    get
    {
      Step1Params step1Params = (Step1Params) null;
      if (this._context.ContainsKey(typeof (Step1)))
        step1Params = this._context[typeof (Step1)] as Step1Params;
      if (this.rbExistTable.Checked)
      {
        this._context[typeof (Step1)] = (object) new Step1Params(1, (this._btnEdit.EditValue as Step1.ButtonEditWrapper).ObjectID);
        this._wizardForm.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
        if (step1Params == null)
          this._wizardForm.ObjectName = Convert.ToString((object) (this._btnEdit.EditValue as Step1.ButtonEditWrapper));
      }
      else if (this.rbNewTable.Checked)
      {
        this._context[typeof (Step1)] = (object) new Step1Params(2, 0L);
        this._wizardForm.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableTypeID;
        if (step1Params == null)
          this._wizardForm.ObjectName = string.Empty;
      }
      if (step1Params != null && !step1Params.Equals(this._context[typeof (Step1)]))
        this.ClearData();
      return this._context;
    }
    set
    {
      this._context = value;
      if (!this._context.ContainsKey(typeof (Step1)) || !(this._context[this.GetType()] is Step1Params step1Params))
        return;
      step1Params.Button = step1Params.TableID != 0L ? 1 : 2;
      if (step1Params.Button == 1)
      {
        this.rbExistTable.Checked = true;
        this._btnEdit.EditValue = (object) new Step1.ButtonEditWrapper(step1Params.TableID);
        this._wizardForm.ObjectName = Convert.ToString(this._btnEdit.EditValue);
        this._wizardForm._btnNext.Enabled = true;
      }
      else
      {
        if (step1Params.Button != 2)
          return;
        this.rbNewTable.Checked = true;
      }
    }
  }

  public System.Type NextStep => !this.rbNewTable.Checked ? typeof (Step4) : typeof (Step2);

  public System.Type PrevStep => (System.Type) null;

  private void OnbtnEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Intermech.Imbase.Consts.ImbaseTableTypeID));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] catalogsList = (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).GetCatalogsList(sessionKeeper.Session.SessionGUID);
      if (catalogsList != null)
      {
        if (catalogsList.Length != 0)
        {
          descriptors.Add((IDescriptor) new ImbaseRootNodeDescriptor(new List<long>((IEnumerable<long>) catalogsList)));
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ObjectTypesSelectedItemsAnalyzer(new List<int>()
          {
            Intermech.Imbase.Consts.ImbaseTableRefTypeID,
            Intermech.Imbase.Consts.ImbaseTableTypeID
          }, false), true);
        }
      }
    }
    IDBTypedObjectID[] dbTypedObjectIdArray = (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Imbase.Client_109"), (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Imbase.Client_110"), descriptors), typeof (IDBTypedObjectID), (System.IServiceProvider) null, SelectionOptions.Default);
    if (dbTypedObjectIdArray == null || dbTypedObjectIdArray.Length == 0)
      return;
    long num1 = dbTypedObjectIdArray[0].ObjectID;
    bool flag = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectInfo(num1).ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObjectActualCopy(num1, false).GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
        if (attributeById != null)
        {
          num1 = attributeById.AsInteger;
          flag = num1 != 0L;
        }
        else
          flag = false;
        if (!flag)
        {
          string caption = LocalizationHolder.rm.GetString("Imbase_SelectObject_Error");
          int num2 = (int) MessageBox.Show((IWin32Window) this._wizardForm, LocalizationHolder.rm.GetString("Imbase_GetTableFromLink_ErrorMsg"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }
    if (!flag)
      return;
    if (this._wizardForm.CheckTableID(num1))
    {
      this._btnEdit.EditValue = (object) new Step1.ButtonEditWrapper(num1);
      int objectTypeId = this._wizardForm.ObjectTypeID;
      this._wizardForm.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableTypeID;
      this._wizardForm.ObjectID = num1;
      this._wizardForm.ObjectTypeID = objectTypeId;
      this._wizardForm.ObjectName = Convert.ToString(this._btnEdit.EditValue);
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase_CreateTableRef_DublicateTable");
      int num3 = (int) MessageBox.Show((IWin32Window) this._wizardForm, LocalizationHolder.rm.GetString("Imbase_CreateTableRef_DublicateTable_Message"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private void OnbtnEdit_EditValueChanged(object sender, EventArgs e)
  {
    Step1.ButtonEditWrapper editValue = this._btnEdit.EditValue as Step1.ButtonEditWrapper;
    this._wizardForm._btnNext.Enabled = editValue != null && editValue.ObjectID != 0L;
  }

  private void OnrdButton_CheckedChanged(object sender, EventArgs e)
  {
    this.label2.Visible = this.tableSelect.Visible = this.rbExistTable.Checked;
    if (this.tableSelect.Visible)
      this._btnEdit.EditValue = (object) new Step1.ButtonEditWrapper(0L);
    this._wizardForm._btnNext.Enabled = this.rbNewTable.Checked;
    this._wizardForm.ObjectName = string.Empty;
  }

  private void ClearData()
  {
    this._wizardForm.DS.Tables["IMS_ATTR_TYPES"].Rows.Clear();
    this._wizardForm.DS.Tables["IMS_DATA"].Rows.Clear();
    if (this._context.ContainsKey(typeof (Step2)))
      this._context.Remove(typeof (Step2));
    if (!this._context.ContainsKey(typeof (Step4)))
      return;
    this._context.Remove(typeof (Step4));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step1));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.tableSelect = new TableLayoutPanel();
    this.label3 = new Label();
    this._btnEdit = new ButtonEdit();
    this.groupBox1 = new GroupBox();
    this.rbNewTable = new RadioButton();
    this.rbExistTable = new RadioButton();
    this.label1 = new Label();
    this.label2 = new Label();
    this.tableLayoutPanel1.SuspendLayout();
    this.tableSelect.SuspendLayout();
    this._btnEdit.Properties.BeginInit();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.tableSelect, 1, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.groupBox1, 1, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.label1, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.label2, 1, 5);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.tableSelect, "tableSelect");
    this.tableSelect.Controls.Add((Control) this.label3, 0, 0);
    this.tableSelect.Controls.Add((Control) this._btnEdit, 0, 1);
    this.tableSelect.Name = "tableSelect";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this._btnEdit, "_btnEdit");
    this._btnEdit.Name = "_btnEdit";
    this._btnEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._btnEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this._btnEdit.ButtonClick += new ButtonPressedEventHandler(this.OnbtnEdit_ButtonClick);
    this._btnEdit.EditValueChanged += new EventHandler(this.OnbtnEdit_EditValueChanged);
    this.groupBox1.Controls.Add((Control) this.rbNewTable);
    this.groupBox1.Controls.Add((Control) this.rbExistTable);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.FlatStyle = FlatStyle.System;
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbNewTable, "rbNewTable");
    this.rbNewTable.Checked = true;
    this.rbNewTable.Name = "rbNewTable";
    this.rbNewTable.TabStop = true;
    this.rbNewTable.UseVisualStyleBackColor = true;
    this.rbNewTable.CheckedChanged += new EventHandler(this.OnrdButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbExistTable, "rbExistTable");
    this.rbExistTable.Name = "rbExistTable";
    this.rbExistTable.UseVisualStyleBackColor = true;
    this.rbExistTable.CheckedChanged += new EventHandler(this.OnrdButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.DoubleBuffered = true;
    this.Name = nameof (Step1);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.tableSelect.ResumeLayout(false);
    this.tableSelect.PerformLayout();
    this._btnEdit.Properties.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }

  internal class ButtonEditWrapper
  {
    private string _caption = LocalizationHolder.rm.GetString("Imbase.Client_107");
    internal long ObjectID;

    internal ButtonEditWrapper(long objectID)
    {
      if (objectID == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
        if (objectActualCopy == null)
          return;
        this.ObjectID = objectID;
        this._caption = objectActualCopy.Caption.Length > 0 ? objectActualCopy.Caption : string.Format(LocalizationHolder.rm.GetString("Imbase.Client_108"), (object) this.ObjectID);
      }
    }

    public override string ToString() => this._caption;
  }
}
