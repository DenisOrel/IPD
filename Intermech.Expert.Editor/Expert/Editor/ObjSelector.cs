// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ObjSelector
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Controllers;
using SourceGrid3.Cells.Views;
using SourceGrid3.Styles;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for ObjSelector.</summary>
public class ObjSelector : Form
{
  private Panel panel1;
  private System.Windows.Forms.Button button1;
  private System.Windows.Forms.Button button2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private ObjSelector.DocScriptInfo[] docScripts;
  internal IView redVisModel;
  internal IView defVisModel;
  internal IView greenVisModel;
  private long selectedID;
  private long userID;
  private bool checkOwner = true;
  private Panel panel2;
  private Grid grid;
  private string ColName = LocalizationHolder.rm.GetString("Expert.Editor_113");

  public ObjSelector()
  {
    this.InitializeComponent();
    this.redVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.redVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
    this.defVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.defVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
    this.greenVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.greenVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
    this.redVisModel.BackColor = Color.White;
    this.redVisModel.ForeColor = Color.Red;
    this.greenVisModel.BackColor = Color.White;
    this.greenVisModel.ForeColor = Color.Green;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void FillObjList(long templID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.userID = sessionKeeper.Session.UserID;
      DataRow[] dataRowArray = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ExpertConsts.Consts.attrTemplateLink, RelationalOperators.Equal, (object) templID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
      }, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) new Guid(ExpertAttrGUIDs.objectName)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }, new SortOrders[1]{ SortOrders.ASC })).Select();
      this.docScripts = (ObjSelector.DocScriptInfo[]) Array.CreateInstance(typeof (ObjSelector.DocScriptInfo), dataRowArray.Length);
      int index = 0;
      foreach (DataRow dataRow in dataRowArray)
      {
        this.docScripts[index] = new ObjSelector.DocScriptInfo();
        this.docScripts[index].ID = Convert.ToInt64(dataRow[0]);
        this.docScripts[index].Name = Convert.ToString(dataRow[1]);
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.docScripts[index].ID);
        this.docScripts[index].ownerID = dbObject.CheckoutBy;
        if (this.docScripts[index].ownerID != 0L)
          this.docScripts[index].ownerName = sessionKeeper.Session.GetObject(this.docScripts[index].ownerID).Caption;
        ++index;
      }
    }
  }

  public void FillReportList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.userID = sessionKeeper.Session.UserID;
      IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(new Guid(ExpertObjGUIDs.ReportTemplate));
      object[] columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      };
      object[] sortColumns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      SortOrders[] orders = new SortOrders[1]
      {
        SortOrders.ASC
      };
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns, sortColumns, orders);
      DataRow[] dataRowArray1 = objectCollection1.Select(paramSet).Select();
      long[] instance = (long[]) Array.CreateInstance(typeof (long), dataRowArray1.Length);
      for (int index = 0; index < dataRowArray1.Length; ++index)
      {
        instance[index] = Convert.ToInt64(dataRowArray1[index][0]);
        if (instance[index] < 0L)
          instance[index] = -instance[index];
      }
      IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript);
      paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ExpertConsts.Consts.attrTemplateLink, RelationalOperators.In, (object) instance, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
      }, columns, sortColumns, orders);
      DataRow[] dataRowArray2 = objectCollection2.Select(paramSet).Select();
      this.docScripts = (ObjSelector.DocScriptInfo[]) Array.CreateInstance(typeof (ObjSelector.DocScriptInfo), dataRowArray2.Length);
      int index1 = 0;
      foreach (DataRow dataRow in dataRowArray2)
      {
        this.docScripts[index1] = new ObjSelector.DocScriptInfo();
        this.docScripts[index1].ID = Convert.ToInt64(dataRow[0]);
        this.docScripts[index1].Name = Convert.ToString(dataRow[1]);
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.docScripts[index1].ID);
        this.docScripts[index1].ownerID = dbObject.CheckoutBy;
        if (this.docScripts[index1].ownerID != 0L)
          this.docScripts[index1].ownerName = sessionKeeper.Session.GetObject(this.docScripts[index1].ownerID).Caption;
        ++index1;
      }
    }
  }

  private void UpdateGrid()
  {
    int num = 0;
    if (this.docScripts != null)
      num = this.docScripts.Length;
    SourceGrid3.Cells.Real.Cell cell1 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) this.ColName);
    cell1.AddController((IController) new Unselectable());
    cell1.AddController((IController) Resizable.ResizeWidth);
    this.grid.Redim(num + 1, 2);
    this.grid.FixedRows = 1;
    this.grid.Selection.SelectionMode = GridSelectionMode.Row;
    this.grid.Selection.FocusBackColor = this.grid.Selection.BackColor;
    this.grid[0, 0] = (ICell) cell1;
    SourceGrid3.Cells.Real.Cell cell2 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_114"));
    cell2.AddController((IController) new Unselectable());
    cell2.AddController((IController) Resizable.ResizeWidth);
    this.grid[0, 1] = (ICell) cell2;
    this.grid.Columns.AutoSize(false);
    this.grid.Selection.EnableMultiSelection = false;
    this.grid.Columns[0].Width = (this.grid.Width - 60) / 2 - 1;
    this.grid.Columns[1].Width = this.grid.Width - 2 - this.grid.Columns[0].Width;
    CustomEvents customEvents = new CustomEvents();
    customEvents.DoubleClick += new EventHandler(this.contr_DoubleClick);
    for (int index = 0; index < num; ++index)
    {
      SourceGrid3.Cells.Real.Cell cell3 = new SourceGrid3.Cells.Real.Cell((object) this.docScripts[index].Name);
      SourceGrid3.Cells.Real.Cell cell4 = new SourceGrid3.Cells.Real.Cell((object) this.docScripts[index].ownerName);
      cell3.AddController((IController) customEvents);
      cell4.AddController((IController) customEvents);
      if (this.docScripts[index].ownerID == 0L)
      {
        cell3.View = this.defVisModel;
        cell4.View = this.defVisModel;
      }
      else if (this.docScripts[index].ownerID == this.userID)
      {
        cell3.View = this.greenVisModel;
        cell4.View = this.greenVisModel;
      }
      else
      {
        cell3.View = this.redVisModel;
        cell4.View = this.redVisModel;
      }
      this.grid[index + 1, 0] = (ICell) cell3;
      this.grid[index + 1, 1] = (ICell) cell4;
    }
  }

  private void contr_DoubleClick(object sender, EventArgs e)
  {
    this.button1_Click((object) this.grid, new EventArgs());
    if (this.selectedID == 0L)
      return;
    this.DialogResult = DialogResult.OK;
  }

  public long SelectDocScriptForTemplate(long templID)
  {
    this.checkOwner = true;
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_115");
    this.FillObjList(templID);
    this.UpdateGrid();
    return this.ShowDialog() == DialogResult.OK && this.selectedID != 0L ? this.selectedID : 0L;
  }

  public long SelectReportTemplate()
  {
    this.checkOwner = false;
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_116");
    this.ColName = LocalizationHolder.rm.GetString("Expert.Editor_117");
    this.FillReportList();
    this.UpdateGrid();
    return this.ShowDialog() == DialogResult.OK && this.selectedID != 0L ? this.selectedID : 0L;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjSelector));
    this.panel1 = new Panel();
    this.button2 = new System.Windows.Forms.Button();
    this.button1 = new System.Windows.Forms.Button();
    this.panel2 = new Panel();
    this.grid = new Grid();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    this.panel2.Controls.Add((Control) this.grid);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.grid.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.GridToolTipActive = true;
    this.grid.Name = "grid";
    this.grid.SpecialKeys = GridSpecialKeys.Default;
    this.grid.StyleGrid = (StyleGrid) null;
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ObjSelector);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.ObjSelector_Load);
    this.FormClosed += new FormClosedEventHandler(this.ObjSelector_FormClosed);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void button1_Click(object sender, EventArgs e)
  {
    int index = this.grid.Selection.GetRowsIndex()[0] - 1;
    if (index < 0)
      this.DialogResult = DialogResult.None;
    else if (this.checkOwner && this.docScripts[index].ownerID != 0L && this.docScripts[index].ownerID != this.userID)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_118"), LocalizationHolder.rm.GetString("Expert.Editor_119"));
      this.DialogResult = DialogResult.None;
    }
    else
      this.selectedID = this.docScripts[index].ID;
  }

  private void ObjSelector_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ObjSelector_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  public class DocScriptInfo
  {
    public long ID;
    public string Name;
    public long ownerID;
    public string ownerName;
  }
}
