// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.InverseSynchObjectsForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class InverseSynchObjectsForm : Form
{
  private SynchObjectsResultCtrl _resultCtrl;
  private InverseSynchObjectsService _srv;
  private List<long> _objIDs;
  private int _typeID = -1;
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnOK;
  private SplitContainer _spltContainer;
  private ListView _lv;
  private ColumnHeader _colAttrsName;

  private InverseSynchObjectsForm()
  {
    this.InitializeComponent();
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
  }

  public InverseSynchObjectsForm(ISelectedItems items, System.IServiceProvider viewServices)
    : this()
  {
    this.LoadInfo(items, viewServices);
    this.LoadAttributes();
    this.AddControl();
  }

  public InverseSynchObjectsForm(List<long> objIDs)
    : this()
  {
    this._objIDs = objIDs;
    this.LoadAttributes();
    this.AddControl();
  }

  private void On_btnOK_Click(object sender, EventArgs e)
  {
    if (this._srv == null)
      return;
    if (this._srv.Processing)
    {
      this._srv.StopTask();
    }
    else
    {
      this._lv.Enabled = false;
      this._btnOK.Text = LocalizationHolder.rm.GetString("Imbase_Cancel");
      List<int> intList = new List<int>(this._lv.CheckedItems.Count);
      foreach (ListViewItem checkedItem in this._lv.CheckedItems)
        intList.Add(Convert.ToInt32(checkedItem.Tag));
      this._srv.AttributeIDs = intList;
      this._srv.StartTask();
    }
  }

  private void On_lv_SizeChanged(object sender, EventArgs e)
  {
    this._colAttrsName.Width = this._lv.Width - 20;
  }

  private void On_lv_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    this._btnOK.Enabled = this._lv.CheckedItems.Count > 0;
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

  private void AddControl()
  {
    this._srv = this._typeID != -1 ? new InverseSynchObjectsService(this._typeID) : new InverseSynchObjectsService(this._objIDs);
    this._srv.OnFinished += (Action) (() => this._btnOK.Enabled = false);
    this.SuspendLayout();
    SynchObjectsResultCtrl objectsResultCtrl = new SynchObjectsResultCtrl((BaseSynchObjectsService) this._srv);
    objectsResultCtrl.Dock = DockStyle.Fill;
    this._resultCtrl = objectsResultCtrl;
    this._spltContainer.Panel2.Controls.Add((Control) this._resultCtrl);
    this.ResumeLayout();
  }

  private void LoadInfo(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0)
      return;
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 128L /*0x80*/) != 0L)
    {
      if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
        this._objIDs = new List<long>()
        {
          itemData2.ObjectID
        };
      else
        this._typeID = items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData1 ? itemData1.Value : -1;
    }
    else
    {
      this._objIDs = new List<long>(items.Count);
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData3)
          this._objIDs.Add(itemData3.ObjectID);
      }
    }
  }

  private void LoadAttributes()
  {
    List<int> intList = new List<int>();
    this._lv.BeginUpdate();
    if (this._typeID != -1)
    {
      foreach (int ObjectTypeID in MetaDataHelper.GetObjectTypeChildrenIDRecursive(this._typeID))
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(ObjectTypeID);
        if (attribute4ObjectTypeList != null)
        {
          foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
          {
            if (!intList.Contains(attribute4ObjectType.AttributeID))
            {
              int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attribute4ObjectType.FieldType);
              this._lv.Items.Add(new ListViewItem(MetaDataHelper.GetAttributeTypeName(attribute4ObjectType.AttributeID), imageIndex)
              {
                Tag = (object) attribute4ObjectType.AttributeID
              });
              intList.Add(attribute4ObjectType.AttributeID);
            }
          }
        }
      }
    }
    else if (this._objIDs != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long objId in this._objIDs)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objId, false);
          if (objectActualCopy != null)
          {
            AttributeValues[] attributesValues = objectActualCopy.GetAttributesValues(GetAttributeValuesModes.IncludeName);
            if (attributesValues != null)
            {
              foreach (AttributeValues attributeValues in attributesValues)
              {
                if (!intList.Contains(attributeValues.AttributeID))
                {
                  int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeValues.AttributeType);
                  this._lv.Items.Add(new ListViewItem(MetaDataHelper.GetAttributeTypeName(attributeValues.AttributeID), imageIndex)
                  {
                    Tag = (object) attributeValues.AttributeID
                  });
                  intList.Add(attributeValues.AttributeID);
                }
              }
            }
          }
        }
      }
    }
    this._lv.EndUpdate();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InverseSynchObjectsForm));
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._spltContainer = new SplitContainer();
    this._lv = new ListView();
    this._colAttrsName = new ColumnHeader();
    this._pnlBottom.SuspendLayout();
    this._spltContainer.BeginInit();
    this._spltContainer.Panel1.SuspendLayout();
    this._spltContainer.SuspendLayout();
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
    componentResourceManager.ApplyResources((object) this._spltContainer, "_spltContainer");
    this._spltContainer.Name = "_spltContainer";
    this._spltContainer.Panel1.Controls.Add((Control) this._lv);
    this._lv.CheckBoxes = true;
    this._lv.Columns.AddRange(new ColumnHeader[1]
    {
      this._colAttrsName
    });
    componentResourceManager.ApplyResources((object) this._lv, "_lv");
    this._lv.HeaderStyle = ColumnHeaderStyle.None;
    this._lv.Name = "_lv";
    this._lv.Sorting = SortOrder.Ascending;
    this._lv.UseCompatibleStateImageBehavior = false;
    this._lv.View = View.Details;
    this._lv.ItemChecked += new ItemCheckedEventHandler(this.On_lv_ItemChecked);
    this._lv.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this._colAttrsName, "_colAttrsName");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._spltContainer);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (InverseSynchObjectsForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._pnlBottom.ResumeLayout(false);
    this._spltContainer.Panel1.ResumeLayout(false);
    this._spltContainer.EndInit();
    this._spltContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
