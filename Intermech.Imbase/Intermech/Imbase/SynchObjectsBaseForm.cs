// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.SynchObjectsBaseForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class SynchObjectsBaseForm : Form
{
  private SynchObjectsSettingsCtrl _settingsCtrl;
  private SynchObjectsResultCtrl _resultCtrl;
  private SynchObjectsService _srv;
  private Dictionary<int, List<long>> _objDict;
  private int _typeID;
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnOK;

  private SynchObjectsBaseForm()
  {
    SynchObjectsSettingsCtrl objectsSettingsCtrl = new SynchObjectsSettingsCtrl();
    objectsSettingsCtrl.Dock = DockStyle.Fill;
    this._settingsCtrl = objectsSettingsCtrl;
    this._typeID = -1;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2176);
    this.SuspendLayout();
    this.Controls.Add((Control) this._settingsCtrl);
    this._settingsCtrl.BringToFront();
    this.ResumeLayout();
  }

  public SynchObjectsBaseForm(
    ISelectedItems items,
    IImbaseSynchObjectsService synchSrv,
    System.IServiceProvider viewServices)
    : this()
  {
    this.LoadInfo(items, viewServices);
  }

  public SynchObjectsBaseForm(Dictionary<int, List<long>> objects)
    : this()
  {
    this._objDict = objects;
  }

  private void On_btnOK_Click(object sender, EventArgs e)
  {
    if (this._srv != null && this._srv.Processing)
    {
      this._srv.StopTask();
    }
    else
    {
      int fildForRelation = this._settingsCtrl.FildForRelation;
      if (!this._settingsCtrl.SynchLinkedObjects && fildForRelation == 0)
        return;
      this._srv = this._typeID != -1 ? new SynchObjectsService(this._typeID, this._settingsCtrl.CreateVersion, fildForRelation) : new SynchObjectsService(this._objDict, this._settingsCtrl.CreateVersion, fildForRelation);
      this._srv.OnFinished += (Action) (() => this._btnOK.Enabled = false);
      this.NextStep();
      this._srv.StartTask();
    }
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this._srv == null || !this._srv.Processing)
      return;
    string caption = LocalizationHolder.rm.GetString("Imbase_Synch_Processing_Caption");
    if (MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase_Synch_Processing_Break"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      e.Cancel = true;
    else
      this._srv.StopTask();
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  private void LoadInfo(ISelectedItems items, System.IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None)
    {
      if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
        this._objDict = new Dictionary<int, List<long>>()
        {
          {
            itemData2.ObjectType,
            new List<long>((IEnumerable<long>) new long[1]
            {
              itemData2.ObjectID
            })
          }
        };
      else
        this._typeID = items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData1 ? itemData1.Value : -1;
    }
    else
    {
      if ((viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.None && !viewStateFlags.HasFlag((Enum) ViewStateFlags.None))
        return;
      this._objDict = new Dictionary<int, List<long>>(items.Count);
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData3)
        {
          if (!this._objDict.ContainsKey(itemData3.ObjectType))
            this._objDict.Add(itemData3.ObjectType, new List<long>());
          this._objDict[itemData3.ObjectType].Add(itemData3.ObjectID);
        }
      }
    }
  }

  private void NextStep()
  {
    this.SuspendLayout();
    this._btnOK.Text = LocalizationHolder.rm.GetString("Imbase_Cancel");
    this.Controls.Remove((Control) this._settingsCtrl);
    SynchObjectsResultCtrl objectsResultCtrl = new SynchObjectsResultCtrl((BaseSynchObjectsService) this._srv);
    objectsResultCtrl.Dock = DockStyle.Fill;
    this._resultCtrl = objectsResultCtrl;
    this.Controls.Add((Control) this._resultCtrl);
    this._resultCtrl.BringToFront();
    this.ResumeLayout();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._srv != null)
        this._srv.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SynchObjectsBaseForm));
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this.On_btnOK_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SynchObjectsBaseForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
