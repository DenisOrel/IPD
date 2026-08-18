
// Type: Intermech.Navigator.ContextCommands.ObjectLCHistory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.ContextCommands;

public class ObjectLCHistory : Form
{
  private IDBObject _obj;
  private bool _version;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button ok_b;
  private iGrid iGrid1;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private Panel panel1;

  public ObjectLCHistory(IDBObject obj, bool version)
  {
    this._obj = obj;
    this._version = version;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 704);
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 40, workingArea.Height / 100 * 30);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this.iGrid1.Cols.Add("", 20);
    this.iGrid1.Cols[0].AllowSizing = false;
    this.iGrid1.Cols.Add(LocalizationHolder.rm.GetString("Client.Core_429"), 150);
    this.iGrid1.Cols.Add(LocalizationHolder.rm.GetString("Client.Core_588"), 185);
    this.iGrid1.Cols.Add(LocalizationHolder.rm.GetString("Client.Core_42"), 185);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int objectType1 = this._obj.ObjectType;
      string nameInMessages = this._obj.NameInMessages;
      IDBObjectType objectType2 = sessionKeeper.Session.GetObjectType(objectType1);
      if (this._version)
        this.Text = $"{LocalizationHolder.rm.GetString("Client.Core_589")}{nameInMessages}\"";
      else
        this.Text = $"{LocalizationHolder.rm.GetString("Client.Core_590")}{nameInMessages}\"";
      using (MemoryStream memoryStream = new MemoryStream(objectType2.Icon))
      {
        if (memoryStream.Length > 0L)
          this.Icon = new Icon((Stream) memoryStream);
      }
      if (sessionKeeper.Session.GetObjectType(objectType1).Versionable == ObjectVersionModes.MultiVersion && this._version)
      {
        this.iGrid1.Cols.Add(LocalizationHolder.rm.GetString("Client.Core_591"), 60);
        this._version = true;
      }
      else
        this._version = false;
    }
  }

  private void ObjectLCHistory_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    using (new SessionKeeper())
    {
      IObjectLevelIDsCache objectLevelIdsCache = CacheManager.Cache("ObjectLevelIDsCache") as IObjectLevelIDsCache;
      DataTable lcHistory = this._obj.GetLCHistory(this._version);
      int rowIndex = 0;
      this.iGrid1.ImageList = objectLevelIdsCache.ImageList;
      foreach (DataRow row in (InternalDataCollectionBase) lcHistory.Rows)
      {
        int int32_1 = Convert.ToInt32(row["F_LC_STEP"]);
        DateTime dateTime = Convert.ToDateTime(row["F_START_DATE"]);
        string lcStepName = MetaDataHelper.GetLCStepName(int32_1);
        IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(int32_1);
        string key = MetaDataHelper.GetLCLevelName(lcStep != null ? lcStep.LevelID : 0).ToString();
        this.iGrid1.Rows.Add();
        this.iGrid1.Cells[rowIndex, 0].ImageIndex = objectLevelIdsCache.ImageList.Images.IndexOfKey(key);
        this.iGrid1.Cells[rowIndex, 1].Value = (object) dateTime.ToString();
        this.iGrid1.Cells[rowIndex, 2].Value = (object) lcStepName;
        this.iGrid1.Cells[rowIndex, 3].Value = (object) key;
        if (this._version)
        {
          int int32_2 = Convert.ToInt32(row["F_VERSION_ID"]);
          this.iGrid1.Cells[rowIndex, 4].Value = (object) int32_2.ToString();
        }
        ++rowIndex;
      }
    }
  }

  private void button1_Click(object sender, EventArgs e) => this.Close();

  private void ObjectLCHistory_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectLCHistory));
    this.ok_b = new Button();
    this.iGrid1 = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.panel1 = new Panel();
    ((ISupportInitialize) this.iGrid1).BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.ok_b, "ok_b");
    this.ok_b.Name = "ok_b";
    this.ok_b.UseVisualStyleBackColor = true;
    this.ok_b.Click += new EventHandler(this.button1_Click);
    this.iGrid1.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.iGrid1.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.iGrid1.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    componentResourceManager.ApplyResources((object) this.iGrid1, "iGrid1");
    this.iGrid1.Header.Height = (int) componentResourceManager.GetObject("iGrid1.Header.Height");
    this.iGrid1.Name = "iGrid1";
    this.iGrid1.ReadOnly = true;
    this.iGrid1.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.iGrid1DefaultCellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.panel1.Controls.Add((Control) this.ok_b);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.AcceptButton = (IButtonControl) this.ok_b;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.iGrid1);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ObjectLCHistory);
    this.FormClosed += new FormClosedEventHandler(this.ObjectLCHistory_FormClosed);
    this.Load += new EventHandler(this.ObjectLCHistory_Load);
    ((ISupportInitialize) this.iGrid1).EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
