// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.Step2
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class Step2 : UserControl
{
  private int newObjectTypeID;
  private long newECOObjectID;
  private ObjectsClassifyType _classifyType;
  private long _lastClassif = -1;
  private bool _isClassified;
  private IContainer components;
  private ClassifyingControl classifyingControl1;
  private Panel panel1;
  private CheckBox checkBox1;
  private Panel panel2;

  public long LastClassif => this._lastClassif;

  public int NewObjectTypeID
  {
    set => this.newObjectTypeID = value;
  }

  public long NewECOObjectID
  {
    set => this.newECOObjectID = value;
  }

  public event EventHandler EnableChangedEvent;

  private ObjectsClassifyType ClassifyType
  {
    get
    {
      if (this._classifyType == ObjectsClassifyType.None)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._classifyType = ObjectsClassifyHelper.GetClassifierType(sessionKeeper.Session, this.newObjectTypeID);
      }
      return this._classifyType;
    }
  }

  public Step2(int aObjectTypeID, long aObjectID)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.Visible = true;
    this.newObjectTypeID = aObjectTypeID;
    this.newECOObjectID = aObjectID;
    this.FillTreeList();
  }

  private void FillTreeList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] classifierForObjType = (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GetClassifierForObjType((object) sessionKeeper.Session.SessionGUID, this.newObjectTypeID);
      if (classifierForObjType != null && classifierForObjType.Length != 0)
        this.classifyingControl1.RootClassifiers = classifierForObjType;
      else if (this.ClassifyType == ObjectsClassifyType.Obligatory)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.newObjectTypeID);
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_848"), (object) objectType.ObjectTypeName));
      }
    }
    this.checkBox1.Visible = this.ClassifyType == ObjectsClassifyType.Selective;
  }

  public bool Classification()
  {
    if (this.checkBox1.Checked)
      return false;
    try
    {
      ISelectedItemsHost classifyingControl1 = (ISelectedItemsHost) this.classifyingControl1;
      if (classifyingControl1.SelectedItems == null || classifyingControl1.SelectedItems.Count == 0)
        return true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
        if (!(classifyingControl1.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
          return false;
        if (this._lastClassif == itemData.Value)
          return true;
        IObjectClassificator objectClassificator = customService.GetObjectClassificator((object) sessionKeeper.Session.SessionGUID, itemData.Value);
        if (objectClassificator != null)
        {
          ClassifiedError classifiedError = Intermech.Navigator.Selections.Consts.ObjectClassify(sessionKeeper.Session, objectClassificator, this.newECOObjectID, false);
          if (objectClassificator.NonClassifiedObjects != null && objectClassificator.NonClassifiedObjects.Length != 0 && classifiedError.Exception != null)
            throw classifiedError.Exception;
          this._lastClassif = itemData.Value;
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    EventHandler enableChangedEvent = this.EnableChangedEvent;
    if (enableChangedEvent == null)
      return;
    enableChangedEvent(sender, e);
  }

  private void classifyingControl1_SelectedItemsChanged(
    object sender,
    ClassifierSelectedEventArgs e)
  {
    EventHandler enableChangedEvent = this.EnableChangedEvent;
    if (enableChangedEvent == null)
      return;
    enableChangedEvent(sender, (EventArgs) e);
  }

  public bool NextIsAccessible
  {
    get
    {
      ISelectedItemsHost classifyingControl1 = (ISelectedItemsHost) this.classifyingControl1;
      if (classifyingControl1.SelectedItems != null && classifyingControl1.SelectedItems.Count == 1)
      {
        if (classifyingControl1.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
        {
          if (this._lastClassif == itemData.Value)
          {
            this._isClassified = true;
            return true;
          }
          this._isClassified = false;
        }
        else
          this._isClassified = this._lastClassif != -1L;
      }
      if ((this.ClassifyType != ObjectsClassifyType.Obligatory || this._isClassified) && (this.ClassifyType != ObjectsClassifyType.Selective || this.checkBox1.Checked || this._isClassified))
        return true;
      return classifyingControl1.SelectedItems != null && classifyingControl1.SelectedItems.Count == 1 && classifyingControl1.SelectedItems.GetItemData(0, typeof (IDBObjectID)) != null;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step2));
    this.classifyingControl1 = new ClassifyingControl();
    this.panel1 = new Panel();
    this.checkBox1 = new CheckBox();
    this.panel2 = new Panel();
    ((ISupportInitialize) this.classifyingControl1).BeginInit();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.classifyingControl1, "classifyingControl1");
    this.classifyingControl1.Name = "classifyingControl1";
    this.classifyingControl1.SupportedEvents = IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    this.classifyingControl1.ClassifierSelected += new ClassifierSelectedEventHandler(this.classifyingControl1_SelectedItemsChanged);
    this.panel1.Controls.Add((Control) this.checkBox1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.panel2.Controls.Add((Control) this.classifyingControl1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (Step2);
    ((ISupportInitialize) this.classifyingControl1).EndInit();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
