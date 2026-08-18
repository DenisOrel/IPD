// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionTestForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection.AutoSelectionLog;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionTestForm : Form
{
  private AutoSelectionNodeTest _nodeTest;
  private readonly AutoSelectionLogForm _logForm;
  private IContainer components;
  private Panel pnlBotom;
  private Button btnCancel;
  private Button btnStart;
  private TabControl tcMain;
  private TabPage tpSettings;
  private TabPage tpSelectionLog;
  private PropertyGrid pgSettings;

  private void UpdateNodeRec()
  {
    this.btnStart.Enabled = this._nodeTest != null;
    this.pgSettings.SelectedObject = (object) this._nodeTest;
    this._logForm.RootLogRec = (AutoSelectionLogRec) null;
  }

  protected void ExecuteTest()
  {
    if (this.NodeTest == null)
      return;
    long num = 0;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        switch (this.NodeTest.ObjectMode)
        {
          case AutoSelectionTestObjectMode.CreateNew:
            IDBObjectCollection objectCollection = session.GetObjectCollection(this.NodeTest.ObjectType.Value);
            IDBObject dbObject = this.NodeTest.ObjectID.Value != 0L ? objectCollection.Create(this.NodeTest.ObjectID.Value) : objectCollection.Create();
            List<AttributeValues> attributeValuesList = new List<AttributeValues>();
            if (this.NodeTest.ImbaseObjectID.Value != 0L)
            {
              int attributeId = MetaDataHelper.GetAttributeID((object) AutosSelectConsts.ImbaseObjectLinkAttrGuid.ToString());
              attributeValuesList.Add(new AttributeValues(attributeId, (object) this.NodeTest.ImbaseObjectID.Value));
            }
            foreach (AutoSelAttrVal defObjAttr in (List<AutoSelAttrVal>) this.NodeTest.DefObjAttrList)
            {
              if (defObjAttr.AttrMode == AutoSelectionAttrMode.SkipExists)
              {
                IDBAttribute byGuid = dbObject.Attributes.FindByGUID(defObjAttr.AttrGuid);
                if (byGuid != null && !byGuid.IsNull)
                  continue;
              }
              int attributeId = MetaDataHelper.GetAttributeID((object) defObjAttr.AttrGuid.ToString());
              attributeValuesList.Add(new AttributeValues(attributeId, defObjAttr.AttrValue));
            }
            if (attributeValuesList.Count != 0)
              dbObject.SetAttributesValues(attributeValuesList.ToArray());
            dbObject.CommitCreation(false);
            num = dbObject.ObjectID;
            break;
          case AutoSelectionTestObjectMode.UseCurrent:
            num = this.NodeTest.ObjectID.Value;
            break;
        }
        IExpertUser expertUserService = AutoSelectionUtils.ServiceKeeper.GetExpertUserService();
        if (expertUserService != null && expertUserService.ShowTraceWindow)
          ExpertUser.rur.Clear();
        Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService selectionService = new Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService();
        selectionService.ExecuteSelection(num, 0L, true, this.NodeTest.Mode);
        IAutoSelectionLog getLastExecuteLog = selectionService.GetLastExecuteLog;
        if (getLastExecuteLog == null || getLastExecuteLog.Count <= 0)
          return;
        this._logForm.RootLogRec = getLastExecuteLog[0] as AutoSelectionLogRec;
      }
    }
    finally
    {
      if (num != 0L || this.NodeTest.ObjectMode == AutoSelectionTestObjectMode.CreateNew)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(num, false);
          if (dbObject != null)
          {
            dbObject.Delete(0L);
            if (num < 0L)
              sessionKeeper.Session.GetObject(Math.Abs(num), false)?.Delete(0L);
          }
        }
      }
    }
  }

  public AutoSelectionTestForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1459);
    AutoSelectionLogForm selectionLogForm = new AutoSelectionLogForm();
    selectionLogForm.splitContainer1.Parent = (Control) this.tpSelectionLog;
    this._logForm = selectionLogForm;
    this.UpdateNodeRec();
  }

  internal AutoSelectionNodeTest NodeTest
  {
    get => this._nodeTest;
    set
    {
      this._nodeTest = value;
      this.UpdateNodeRec();
    }
  }

  private void btnStart_Click(object sender, EventArgs e)
  {
    this.ExecuteTest();
    this.tcMain.SelectedTab = this.tpSelectionLog;
  }

  private void pgSettings_Click(object sender, EventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionTestForm));
    this.pnlBotom = new Panel();
    this.btnCancel = new Button();
    this.btnStart = new Button();
    this.tcMain = new TabControl();
    this.tpSettings = new TabPage();
    this.pgSettings = new PropertyGrid();
    this.tpSelectionLog = new TabPage();
    this.pnlBotom.SuspendLayout();
    this.tcMain.SuspendLayout();
    this.tpSettings.SuspendLayout();
    this.SuspendLayout();
    this.pnlBotom.Controls.Add((Control) this.btnCancel);
    this.pnlBotom.Controls.Add((Control) this.btnStart);
    componentResourceManager.ApplyResources((object) this.pnlBotom, "pnlBotom");
    this.pnlBotom.Name = "pnlBotom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnStart, "btnStart");
    this.btnStart.Name = "btnStart";
    this.btnStart.UseVisualStyleBackColor = true;
    this.btnStart.Click += new EventHandler(this.btnStart_Click);
    this.tcMain.Controls.Add((Control) this.tpSettings);
    this.tcMain.Controls.Add((Control) this.tpSelectionLog);
    componentResourceManager.ApplyResources((object) this.tcMain, "tcMain");
    this.tcMain.Name = "tcMain";
    this.tcMain.SelectedIndex = 0;
    this.tpSettings.Controls.Add((Control) this.pgSettings);
    componentResourceManager.ApplyResources((object) this.tpSettings, "tpSettings");
    this.tpSettings.Name = "tpSettings";
    this.tpSettings.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.pgSettings, "pgSettings");
    this.pgSettings.Name = "pgSettings";
    this.pgSettings.SelectedObject = (object) this.tpSelectionLog;
    this.pgSettings.Click += new EventHandler(this.pgSettings_Click);
    componentResourceManager.ApplyResources((object) this.tpSelectionLog, "tpSelectionLog");
    this.tpSelectionLog.Name = "tpSelectionLog";
    this.tpSelectionLog.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tcMain);
    this.Controls.Add((Control) this.pnlBotom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoSelectionTestForm);
    this.ShowInTaskbar = false;
    this.pnlBotom.ResumeLayout(false);
    this.tcMain.ResumeLayout(false);
    this.tpSettings.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
