// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step2
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Editors;
using Intermech.Imbase.TableWizard.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

[Serializable]
internal class Step2 : UserControl, IImbaseTableStep
{
  private ImbaseTableWizardForm _wizardForm;
  private Dictionary<System.Type, object> _context;
  private StructureEditorCtrl _structEditorCtrl = new StructureEditorCtrl();
  private IContainer components;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private Panel panel1;
  private TextBox txtObjName;
  private Label lbObjName;
  private ImageList _imgList;

  public Step2()
  {
    this.InitializeComponent();
    this._structEditorCtrl.SetVisibleReplaceBtn(false);
    this.Controls.Add((Control) this._structEditorCtrl);
    this._structEditorCtrl.BringToFront();
    this._structEditorCtrl.DataChanged += new EventHandler(this.On_structEditorCtrl_DataChanged);
    this.Dock = DockStyle.Fill;
  }

  private void On_structEditorCtrl_DataChanged(object sender, EventArgs e)
  {
    this.SetEnabledButton();
  }

  private void On_txtObjName_TextChanged(object sender, EventArgs e)
  {
    this._wizardForm._btnFinish.Enabled = !string.IsNullOrEmpty(this.txtObjName.Text) & this._wizardForm._btnNext.Enabled;
  }

  public ImbaseTableWizardForm WizardForm
  {
    set
    {
      this._wizardForm = value;
      if (this._wizardForm == null)
        return;
      this._wizardForm._btnPrev.Enabled = !this._wizardForm.FinalObjIsTbl;
      this._wizardForm.Text = LocalizationHolder.rm.GetString("Imbase.ImbaseTableWizard.Caption.Table");
    }
  }

  public Dictionary<System.Type, object> Context
  {
    get
    {
      Step2Params S2P = new Step2Params(this._structEditorCtrl.Items);
      this._structEditorCtrl.SaveData();
      this._wizardForm.DS.AcceptChanges();
      if (this._wizardForm._bNextClick)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          TableLoadHelper.StoreData(sessionKeeper.Session, this._wizardForm.ObjectID, this._wizardForm.DS, sessionKeeper.Session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
      }
      this._context[typeof (Step2)] = (object) S2P;
      this._wizardForm.ObjectName = this.txtObjName.Text;
      this.AddVirtualAttsToObject(S2P);
      return this._context;
    }
    set
    {
      this._context = value;
      if (this._context.ContainsKey(typeof (Step2)))
      {
        if (this._context[typeof (Step2)] is Step2Params step2Params)
        {
          this._structEditorCtrl.LoadData(this._wizardForm.DS, false, false);
          foreach (ListViewItem listViewItem in this._structEditorCtrl.Items)
          {
            Guid g = new Guid(listViewItem.Name);
            if (!step2Params.GUIDs.Contains(g))
            {
              listViewItem.Remove();
            }
            else
            {
              listViewItem.Tag = (object) step2Params.GetProperties(g);
              StructureEditorPropGridDescriptor.AttTypePropsList.Add(step2Params.GetProperties(g).AttrTypeProps);
            }
          }
          if (this._structEditorCtrl.Items.Count > 0)
            this._structEditorCtrl.Items[0].Selected = true;
        }
      }
      else if (this._context.ContainsKey(typeof (Step1)))
      {
        this._structEditorCtrl.LoadData(this._wizardForm.DS, false, true);
        if (!(this._context[typeof (Step1)] is Step1Params))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(this._wizardForm.ObjectTypeID).Create();
          this._wizardForm.ObjectID = dbObject.ObjectID;
          dbObject.Caption = this._wizardForm.ObjectName;
        }
      }
      else
        this._structEditorCtrl.LoadData(this._wizardForm.DS, false, true);
      this.txtObjName.Text = this._wizardForm.ObjectName;
      this.SetEnabledButton();
    }
  }

  public System.Type NextStep => typeof (Step3);

  public System.Type PrevStep => this._wizardForm.FinalObjIsTbl ? (System.Type) null : typeof (Step1);

  private void AddVirtualAttsToObject(Step2Params S2P)
  {
    if (S2P == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._wizardForm.ObjectID, false);
      if (objectActualCopy == null)
        return;
      objectActualCopy.Caption = this._wizardForm.ObjectName;
      IDBAttributeCollection attributes = objectActualCopy.Attributes;
      for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
      {
        IDBAttribute dbAttribute = attributes[AttrIndex];
        IDBAttributeType4 attributeType = dbAttribute.AttributeType as IDBAttributeType4;
        Guid attributeGuid = dbAttribute.AttributeType.PropertiesStructure.AttributeGuid;
        if (S2P.GUIDs.Contains(attributeGuid) && S2P.GetProperties(attributeGuid).Required != 0)
        {
          if (attributeType == null || attributeType.Required != RequiredModes.AutoRequired)
            dbAttribute.Delete(0L);
        }
        else if (attributeType == null || attributeType.Required != RequiredModes.AutoRequired)
          dbAttribute.Delete(0L);
      }
      foreach (Guid guiD in S2P.GUIDs)
      {
        if (S2P.GetProperties(guiD).Required == 0)
        {
          int id = S2P.GetID(guiD);
          if (id != 0)
            objectActualCopy.Attributes.AddAttribute(id, false);
        }
      }
      IDBAttribute dbAttribute1 = objectActualCopy.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttGUID), false);
      if (dbAttribute1 == null)
        return;
      dbAttribute1.Value = (object) $"T{Math.Abs(objectActualCopy.ObjectID).ToString("D9")}";
    }
  }

  private void SetEnabledButton()
  {
    if (this._structEditorCtrl.Items.Count > 0)
    {
      this._wizardForm._btnNext.Enabled = true;
      this._wizardForm._btnFinish.Enabled = !string.IsNullOrEmpty(this.txtObjName.Text);
    }
    else
      this._wizardForm._btnNext.Enabled = this._wizardForm._btnFinish.Enabled = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step2));
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.panel1 = new Panel();
    this.txtObjName = new TextBox();
    this.lbObjName = new Label();
    this._imgList = new ImageList(this.components);
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.ReadOnly = true;
    this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.panel1.Controls.Add((Control) this.txtObjName);
    this.panel1.Controls.Add((Control) this.lbObjName);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.txtObjName, "txtObjName");
    this.txtObjName.Name = "txtObjName";
    this.txtObjName.TextChanged += new EventHandler(this.On_txtObjName_TextChanged);
    componentResourceManager.ApplyResources((object) this.lbObjName, "lbObjName");
    this.lbObjName.Name = "lbObjName";
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "Top.ico");
    this._imgList.Images.SetKeyName(1, "Up.ico");
    this._imgList.Images.SetKeyName(2, "Down.ico");
    this._imgList.Images.SetKeyName(3, "Bottom.ico");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.MinimumSize = new Size(670, 336);
    this.Name = nameof (Step2);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
