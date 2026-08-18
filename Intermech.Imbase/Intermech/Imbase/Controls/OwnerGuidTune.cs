// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.OwnerGuidTune
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class OwnerGuidTune : UserControl
{
  private IContainer components;
  private GroupBox grbMain;
  private RadioButton rbRole;
  private RadioButton rbArea;
  private RadioButton rbUser;
  private RadioButton rbCommon;
  private ComboBox cbData;

  private void InitializeData()
  {
  }

  private void FillItemsData()
  {
    this.cbData.BeginUpdate();
    try
    {
      this.cbData.Items.Clear();
      this.cbData.Enabled = true;
      Guid empty = Guid.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        switch (this.OwnerType)
        {
          case OwnerGuidTune.OwnerFilterType.Common:
            this.cbData.Enabled = false;
            break;
          case OwnerGuidTune.OwnerFilterType.Area:
            DataTable dataTable = session.GetSubjectAreaCollection().Select(string.Empty);
            string areaId = sessionKeeper.Session.AreaID;
            DataRow[] dataRowArray = session.IsAdmin || areaId == string.Empty ? dataTable.Select() : dataTable.Select($"F_AREA_ID='{areaId}'");
            if (dataRowArray == null)
              break;
            foreach (DataRow dataRow in dataRowArray)
              this.cbData.Items.Add((object) new ItemData(dataRow["F_AREA_NAME"].ToString(), dataRow["F_GUID"].ToString()));
            break;
          case OwnerGuidTune.OwnerFilterType.Role:
          case OwnerGuidTune.OwnerFilterType.User:
            if (session.IsAdmin)
            {
              this.cbData.Items.AddRange((object[]) OwnerGuidTune.GetItemsOfType(this.OwnerType == OwnerGuidTune.OwnerFilterType.Role ? new Guid("cad00007-306c-11d8-b4e9-00304f19f545") : new Guid("cad00002-306c-11d8-b4e9-00304f19f545")));
              break;
            }
            long objectID = this.OwnerType == OwnerGuidTune.OwnerFilterType.Role ? sessionKeeper.Session.RoleID : sessionKeeper.Session.UserID;
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
            this.cbData.Items.Add((object) new ItemData(dbObject.Caption, dbObject.ObjectGUID.ToString()));
            break;
        }
      }
    }
    finally
    {
      this.cbData.EndUpdate();
    }
  }

  private void OnOwnerChanged()
  {
    EventHandler ownerChanged = this.OwnerChanged;
    if (ownerChanged == null)
      return;
    ownerChanged((object) this, EventArgs.Empty);
  }

  public OwnerGuidTune()
  {
    this.InitializeComponent();
    this.InitializeData();
  }

  public string Caption
  {
    get => this.grbMain.Text;
    set => this.grbMain.Text = value;
  }

  public string OwnerGuid
  {
    get
    {
      switch (this.OwnerType)
      {
        case OwnerGuidTune.OwnerFilterType.Area:
        case OwnerGuidTune.OwnerFilterType.Role:
        case OwnerGuidTune.OwnerFilterType.User:
          return this.cbData.SelectedItem is ItemData selectedItem ? selectedItem.Guid : (string) null;
        default:
          return (string) null;
      }
    }
  }

  public OwnerGuidTune.OwnerFilterType OwnerType
  {
    get
    {
      if (this.rbCommon.Checked)
        return OwnerGuidTune.OwnerFilterType.Common;
      if (this.rbArea.Checked)
        return OwnerGuidTune.OwnerFilterType.Area;
      return this.rbRole.Checked ? OwnerGuidTune.OwnerFilterType.Role : OwnerGuidTune.OwnerFilterType.User;
    }
  }

  public event EventHandler OwnerChanged;

  private static DBRecordSetParams CreateParamsSet()
  {
    return new DBRecordSetParams()
    {
      Columns = new object[2]
      {
        (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID),
        (object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION)
      },
      RecordCount = -1,
      TableName = "*"
    };
  }

  private static ItemData[] GetItemsOfType(Guid guid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(guid).Select(OwnerGuidTune.CreateParamsSet());
      List<ItemData> itemDataList = new List<ItemData>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        itemDataList.Add(new ItemData(row[1].ToString(), row[0].ToString()));
      return itemDataList.ToArray();
    }
  }

  private void rbCommon_CheckedChanged(object sender, EventArgs e)
  {
    this.OnOwnerChanged();
    this.cbData.Enabled = sender != this.rbCommon;
    this.cbData.Items.Clear();
  }

  private void cbData_DropDown(object sender, EventArgs e)
  {
    if (this.cbData.Items.Count != 0)
      return;
    this.FillItemsData();
  }

  private void cbData_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!(sender is ComboBox comboBox) || comboBox.SelectedIndex == -1)
      return;
    this.OnOwnerChanged();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OwnerGuidTune));
    this.grbMain = new GroupBox();
    this.cbData = new ComboBox();
    this.rbRole = new RadioButton();
    this.rbArea = new RadioButton();
    this.rbUser = new RadioButton();
    this.rbCommon = new RadioButton();
    this.grbMain.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.grbMain, "grbMain");
    this.grbMain.Controls.Add((Control) this.cbData);
    this.grbMain.Controls.Add((Control) this.rbRole);
    this.grbMain.Controls.Add((Control) this.rbArea);
    this.grbMain.Controls.Add((Control) this.rbUser);
    this.grbMain.Controls.Add((Control) this.rbCommon);
    this.grbMain.Name = "grbMain";
    this.grbMain.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbData, "cbData");
    this.cbData.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbData.FormattingEnabled = true;
    this.cbData.Name = "cbData";
    this.cbData.Sorted = true;
    this.cbData.DropDown += new EventHandler(this.cbData_DropDown);
    this.cbData.SelectedIndexChanged += new EventHandler(this.cbData_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.rbRole, "rbRole");
    this.rbRole.Name = "rbRole";
    this.rbRole.Tag = (object) "3";
    this.rbRole.UseVisualStyleBackColor = true;
    this.rbRole.CheckedChanged += new EventHandler(this.rbCommon_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbArea, "rbArea");
    this.rbArea.Name = "rbArea";
    this.rbArea.Tag = (object) "2";
    this.rbArea.UseVisualStyleBackColor = true;
    this.rbArea.CheckedChanged += new EventHandler(this.rbCommon_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbUser, "rbUser");
    this.rbUser.Name = "rbUser";
    this.rbUser.Tag = (object) "1";
    this.rbUser.UseVisualStyleBackColor = true;
    this.rbUser.CheckedChanged += new EventHandler(this.rbCommon_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbCommon, "rbCommon");
    this.rbCommon.Checked = true;
    this.rbCommon.Name = "rbCommon";
    this.rbCommon.TabStop = true;
    this.rbCommon.Tag = (object) "0";
    this.rbCommon.UseVisualStyleBackColor = true;
    this.rbCommon.CheckedChanged += new EventHandler(this.rbCommon_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grbMain);
    this.Name = nameof (OwnerGuidTune);
    this.grbMain.ResumeLayout(false);
    this.grbMain.PerformLayout();
    this.ResumeLayout(false);
  }

  public enum OwnerFilterType
  {
    Common,
    Area,
    Role,
    User,
  }
}
