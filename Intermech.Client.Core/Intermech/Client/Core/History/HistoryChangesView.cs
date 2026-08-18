
// Type: Intermech.Client.Core.History.HistoryChangesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core.History;

/// <summary>
/// Форма отображающая историю изменений для определенного объекта
/// </summary>
public class HistoryChangesView : Form
{
  private System.ComponentModel.Container components;
  private GridControl gridControl1;
  private GridView gridView1;
  private Panel panel1;
  private Button button1;
  private Panel panel2;
  private long _ID = -1;
  private int _typeID = -1;
  private AttributableElements _type;
  private int _attrID = -1;
  private string _caption = LocalizationHolder.rm.GetString("Client.Core_222");

  /// <summary>Конструктор</summary>
  /// <param name="ID">Идентификатор объекта</param>
  /// <param name="typeID">Идентификатор типа объекта</param>
  /// <param name="type">Определитель объекта</param>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  public HistoryChangesView(long ID, int typeID, AttributableElements type, int attrID)
  {
    this.InitializeComponent();
    this._ID = ID;
    this._typeID = typeID;
    this._type = type;
    this._attrID = attrID;
    this.LoadData();
  }

  /// <summary>Загрузка данных и их отображение</summary>
  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this.Text = string.Format(this._caption, (object) session.GetAttributeType(this._attrID).Name);
      IDBAHistoryCollection historyCollection = session.GetHistoryCollection(this._attrID);
      ConditionStructure conditionStructure1 = new ConditionStructure(-58, RelationalOperators.Equal, (object) this._attrID, LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure2 = new ConditionStructure((string) null, RelationalOperators.Equal, (object) null, LogicalOperators.AND, 0, false);
      switch (this._type)
      {
        case AttributableElements.None:
          return;
        case AttributableElements.Object:
          conditionStructure2.Attribute = (object) -7;
          if (this._typeID >= 0)
          {
            conditionStructure2.Value = (object) this._typeID;
            break;
          }
          break;
        case AttributableElements.Relation:
          conditionStructure2.Attribute = (object) -23;
          if (this._typeID >= 0)
          {
            conditionStructure2.Value = (object) this._typeID;
            break;
          }
          break;
      }
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[3]
      {
        conditionStructure1,
        conditionStructure2,
        new ConditionStructure(-3, RelationalOperators.Equal, (object) this._ID, LogicalOperators.NONE, 0, false)
        {
          Content = ColumnContents.ID
        }
      }, new object[3]
      {
        (object) ObligatoryObjectAttributes.F_SET_DATE,
        (object) historyCollection.TextFieldID,
        (object) ObligatoryObjectAttributes.F_USER_ID
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_SET_DATE
      }, new SortOrders[1]{ SortOrders.DESC });
      DataTable dataTable1 = historyCollection.Select(paramSet);
      DataTable dataTable2 = new DataTable(string.Empty);
      if (dataTable1 != null)
      {
        dataTable2.Columns.AddRange(new DataColumn[3]
        {
          new DataColumn(dataTable1.Columns[0].ColumnName, dataTable1.Columns[0].DataType),
          new DataColumn("Значение атрибута", dataTable1.Columns[1].DataType),
          new DataColumn(dataTable1.Columns[2].ColumnName, typeof (string))
        });
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        {
          object[] itemArray = row.ItemArray;
          long int64 = Convert.ToInt64(row[2]);
          IDBObject dbObject = session.GetObject(int64);
          itemArray[2] = (object) dbObject.Caption;
          dataTable2.Rows.Add(itemArray);
        }
      }
      this.gridControl1.BeginUpdate();
      try
      {
        this.gridControl1.DataSource = (object) null;
        this.gridControl1.DataSource = (object) dataTable2;
        FormatInfo displayFormat = this.gridView1.Columns[ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_SET_DATE)].DisplayFormat;
        displayFormat.FormatType = FormatType.DateTime;
        displayFormat.FormatString = "F";
      }
      finally
      {
        this.gridControl1.EndUpdate();
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this.gridView1 != null)
        this.gridView1.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (HistoryChangesView));
    this.gridControl1 = new GridControl();
    this.gridView1 = new GridView();
    this.panel1 = new Panel();
    this.button1 = new Button();
    this.panel2 = new Panel();
    this.gridControl1.BeginInit();
    this.gridView1.BeginInit();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.gridControl1, "gridControl1");
    this.gridControl1.EmbeddedNavigator.Name = "";
    this.gridControl1.MainView = (BaseView) this.gridView1;
    this.gridControl1.Name = "gridControl1";
    this.gridControl1.Styles.AddReplace("FocusedRow", (object) new ViewStyleEx("FocusedRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, SystemColors.Highlight, SystemColors.HighlightText, SystemColors.InactiveCaption, LinearGradientMode.Horizontal));
    this.gridControl1.Styles.AddReplace("FocusedCell", (object) new ViewStyleEx("FocusedCell", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "FocusedRow", StyleOptions.UseFont | StyleOptions.UseForeColor, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.WindowText, SystemColors.InactiveCaption, LinearGradientMode.Horizontal));
    this.gridView1.FocusRectStyle = DrawFocusRectStyle.None;
    this.gridView1.GridControl = this.gridControl1;
    componentResourceManager.ApplyResources((object) this.gridView1, "gridView1");
    this.gridView1.Name = "gridView1";
    this.gridView1.OptionsBehavior.Editable = false;
    this.gridView1.OptionsView.ShowIndicator = false;
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Name = "button1";
    this.panel2.Controls.Add((Control) this.gridControl1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (HistoryChangesView);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.HistoryChangesView_Load);
    this.Closed += new EventHandler(this.HistoryChangesView_Closed);
    this.gridControl1.EndInit();
    this.gridView1.EndInit();
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void HistoryChangesView_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void HistoryChangesView_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }
}
