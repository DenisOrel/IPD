// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.SynchObjectsSettingsCtrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class SynchObjectsSettingsCtrl : UserControl
{
  private IContainer components;
  private ListView _lvIndexes;
  private ColumnHeader colName;
  private RadioButton _rbSynchLinkedObject;
  private RadioButton _rbSynchByIndex;
  private CheckBox _chbVersion;

  public bool SynchLinkedObjects => this._rbSynchLinkedObject.Checked;

  public int FildForRelation
  {
    get
    {
      return !this._rbSynchLinkedObject.Checked && this._lvIndexes.SelectedItems.Count != 0 ? Convert.ToInt32(this._lvIndexes.SelectedItems[0].Name) : 0;
    }
  }

  public bool CreateVersion => this._chbVersion.Checked;

  public SynchObjectsSettingsCtrl()
  {
    this.InitializeComponent();
    this._lvIndexes.SmallImageList = Statics.IconSrv.ImageList;
  }

  private void OnRadio_CheckedChanged(object sender, EventArgs e)
  {
    this._lvIndexes.Enabled = Convert.ToInt16((sender as RadioButton).Tag) == (short) 1;
    if (this._lvIndexes.SelectedItems.Count <= 0)
      return;
    this._lvIndexes.Items[this._lvIndexes.SelectedItems[0].Index].Selected = false;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this._lvIndexes.BeginUpdate();
    try
    {
      this._lvIndexes.Items.Clear();
      DataTable dataTable = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
        {
          this._lvIndexes.Items.Add(LocalizationHolder.rm.GetString("Imbase.Client_1149"));
        }
        else
        {
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          string[] colsNames = new string[1]
          {
            IndexesField.F_ATTRIBUTE_ID
          };
          dataTable = customService.GetIndexes(sessionGuid, -1L, colsNames);
        }
      }
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        string empty = string.Empty;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          int int32 = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
          string key = Convert.ToString(int32);
          if (!this._lvIndexes.Items.ContainsKey(key))
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(int32);
            if (attributeType != null)
            {
              int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
              this._lvIndexes.Items.Add(new ListViewItem(attributeType.Name, imageIndex)
              {
                Name = key
              });
            }
          }
        }
        this._rbSynchByIndex.Enabled = this._lvIndexes.Items.Count > 0;
      }
      else
        this._lvIndexes.Items.Add(LocalizationHolder.rm.GetString("Imbase_Synch_IndexinCatalogs_Empty"));
    }
    finally
    {
      this._lvIndexes.EndUpdate();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SynchObjectsSettingsCtrl));
    this._lvIndexes = new ListView();
    this.colName = new ColumnHeader();
    this._rbSynchLinkedObject = new RadioButton();
    this._rbSynchByIndex = new RadioButton();
    this._chbVersion = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._lvIndexes, "_lvIndexes");
    this._lvIndexes.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName
    });
    this._lvIndexes.FullRowSelect = true;
    this._lvIndexes.HeaderStyle = ColumnHeaderStyle.None;
    this._lvIndexes.HideSelection = false;
    this._lvIndexes.Items.AddRange(new ListViewItem[2]
    {
      (ListViewItem) componentResourceManager.GetObject("_lvIndexes.Items"),
      (ListViewItem) componentResourceManager.GetObject("_lvIndexes.Items1")
    });
    this._lvIndexes.MultiSelect = false;
    this._lvIndexes.Name = "_lvIndexes";
    this._lvIndexes.UseCompatibleStateImageBehavior = false;
    this._lvIndexes.View = View.Details;
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    componentResourceManager.ApplyResources((object) this._rbSynchLinkedObject, "_rbSynchLinkedObject");
    this._rbSynchLinkedObject.Checked = true;
    this._rbSynchLinkedObject.Name = "_rbSynchLinkedObject";
    this._rbSynchLinkedObject.TabStop = true;
    this._rbSynchLinkedObject.Tag = (object) "0";
    this._rbSynchLinkedObject.UseVisualStyleBackColor = true;
    this._rbSynchLinkedObject.CheckedChanged += new EventHandler(this.OnRadio_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._rbSynchByIndex, "_rbSynchByIndex");
    this._rbSynchByIndex.Name = "_rbSynchByIndex";
    this._rbSynchByIndex.Tag = (object) "1";
    this._rbSynchByIndex.UseVisualStyleBackColor = true;
    this._rbSynchByIndex.CheckedChanged += new EventHandler(this.OnRadio_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._chbVersion, "_chbVersion");
    this._chbVersion.Name = "_chbVersion";
    this._chbVersion.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._chbVersion);
    this.Controls.Add((Control) this._lvIndexes);
    this.Controls.Add((Control) this._rbSynchLinkedObject);
    this.Controls.Add((Control) this._rbSynchByIndex);
    this.DoubleBuffered = true;
    this.Name = nameof (SynchObjectsSettingsCtrl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
