
// Type: Intermech.Client.Core.AttributesHistory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core;

/// <summary>
/// для отображения истории изменения атрибутов выбранного объекта
/// </summary>
public class AttributesHistory : Form
{
  /// <summary>описание выбранного объекта</summary>
  private IDBTypedObjectID _objectInfo;
  /// <summary>
  /// Описание связи к объекту, если объект выбран в дереве. Если не в дереве, то тут пустышка
  /// </summary>
  private IDBRelationID _relationInfo;
  /// <summary>Список атрибутов на тип объектов</summary>
  private List<IMSAttribute4ObjectType> _attrListForObject = new List<IMSAttribute4ObjectType>();
  /// <summary>Список атрибутов на тип связи</summary>
  private List<IMSAttribute4RelationType> _attrListForRelation = new List<IMSAttribute4RelationType>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel2;
  private ComboBox cbAttributes;
  private iGrid igHistory;
  private iGCellStyle igHistoryCol0CellStyle;
  private iGColHdrStyle igHistoryCol0ColHdrStyle;
  private iGCellStyle igHistoryCol1CellStyle;
  private iGColHdrStyle igHistoryCol1ColHdrStyle;
  private iGCellStyle igHistoryCol2CellStyle;
  private iGColHdrStyle igHistoryCol2ColHdrStyle;
  private Panel panel1;
  private Button btnOK;

  public AttributesHistory(IDBTypedObjectID objectInfo, IDBRelationID relationInfo)
  {
    this.InitializeComponent();
    this.igHistory.Cols[0].Text = (object) LocalizationHolder.rm.GetString("Client.Core_1579");
    this.igHistory.Cols[1].Text = (object) LocalizationHolder.rm.GetString("Client.Core_1580");
    this.igHistory.Cols[2].Text = (object) LocalizationHolder.rm.GetString("Client.Core_1581");
    this._objectInfo = objectInfo;
    this._relationInfo = relationInfo;
    string str = $"[{objectInfo.ObjectID}] ";
    string objectTypeName = MetaDataHelper.GetObjectTypeName(objectInfo.ObjectType);
    if (objectInfo.Caption != string.Empty)
      str = $"{str} \"{objectInfo.Caption}\"";
    this.Text = $"История значений атрибута для объекта {$"{str} (\"{objectTypeName}\") "}";
    this.LoadAttributes();
    this.LoadCurrentAttributeHistory();
  }

  private void AttributesHistory_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void AttributesHistory_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>заполнеие атрибутов для объекта</summary>
  private void LoadAttributes()
  {
    this._attrListForObject = MetaDataHelper.GetAttribute4ObjectTypeList(this._objectInfo.ObjectType);
    if (this._relationInfo.RelationType != -1)
      this._attrListForRelation = MetaDataHelper.GetAttribute4RelationTypeList(this._relationInfo.RelationType);
    List<MyElement> myElementList = new List<MyElement>();
    foreach (IMSAttribute4ObjectType attribute4ObjectType in this._attrListForObject)
    {
      MyElement myElement = new MyElement()
      {
        Caption = MetaDataHelper.GetAttributeTypeName(attribute4ObjectType.AttributeID),
        Value = (object) attribute4ObjectType.AttributeID
      };
      myElementList.Add(myElement);
    }
    foreach (IMSAttribute4RelationType attribute4RelationType in this._attrListForRelation)
    {
      MyElement myElement = new MyElement()
      {
        Caption = MetaDataHelper.GetAttributeTypeName(attribute4RelationType.AttributeID) + LocalizationHolder.rm.GetString("Relation_sign"),
        Value = (object) attribute4RelationType.AttributeID
      };
      myElementList.Add(myElement);
    }
    myElementList.Sort(new Comparison<MyElement>(AttributesHistory.AttrNameCompare));
    this.cbAttributes.Items.AddRange((object[]) myElementList.ToArray());
    this.cbAttributes.SelectedIndex = 0;
  }

  private static int AttrNameCompare(MyElement x, MyElement y)
  {
    return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : string.Compare(x.Caption, y.Caption, StringComparison.Ordinal));
  }

  /// <summary>загрузка информации об атрибутах</summary>
  private void LoadCurrentAttributeHistory()
  {
    if (!(this.cbAttributes.SelectedItem is MyElement selectedItem))
      return;
    int int32 = Convert.ToInt32(selectedItem.Value);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAHistoryCollection historyCollection = sessionKeeper.Session.GetHistoryCollection(int32);
      ConditionStructure conditionStructure1 = new ConditionStructure(-58, RelationalOperators.Equal, (object) int32, LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure2;
      ConditionStructure conditionStructure3;
      if (selectedItem.Caption.Contains(LocalizationHolder.rm.GetString("Relation_sign")))
      {
        conditionStructure2 = new ConditionStructure(-23, RelationalOperators.Equal, (object) this._relationInfo.RelationType, LogicalOperators.AND, 0, false);
        conditionStructure3 = new ConditionStructure(-3, RelationalOperators.Equal, (object) this._relationInfo.Value, LogicalOperators.NONE, 0, false);
      }
      else
      {
        conditionStructure2 = new ConditionStructure(-7, RelationalOperators.Equal, (object) this._objectInfo.ObjectType, LogicalOperators.AND, 0, false);
        conditionStructure3 = new ConditionStructure(-3, RelationalOperators.Equal, (object) this._objectInfo.ID, LogicalOperators.NONE, 0, false);
      }
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[3]
      {
        conditionStructure1,
        conditionStructure2,
        conditionStructure3
      }, new object[3]
      {
        (object) ObligatoryObjectAttributes.F_SET_DATE,
        (object) historyCollection.TextFieldID,
        (object) ObligatoryObjectAttributes.F_USER_ID
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_SET_DATE
      }, new SortOrders[1]{ SortOrders.DESC });
      paramSet.AddTag((object) "UserCaptions", (object) true);
      foreach (DataRow row in (InternalDataCollectionBase) historyCollection.Select(paramSet).Rows)
      {
        iGRow iGrow = this.igHistory.Rows.Add();
        iGrow.Cells[0].Value = row[0];
        iGrow.Cells[1].Value = row[1];
        iGrow.Cells[2].Value = row[2];
        iGrow.AutoHeight();
      }
      this.igHistory.SortObject.Add(0);
      this.igHistory.Sort();
    }
  }

  /// <summary>выбрали другой атрибут</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbAttributes_SelectionChangeCommitted(object sender, EventArgs e)
  {
    this.igHistory.Rows.Clear();
    this.LoadCurrentAttributeHistory();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttributesHistory));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    this.igHistoryCol0CellStyle = new iGCellStyle(true);
    this.igHistoryCol0ColHdrStyle = new iGColHdrStyle(true);
    this.igHistoryCol1CellStyle = new iGCellStyle(true);
    this.igHistoryCol1ColHdrStyle = new iGColHdrStyle(true);
    this.igHistoryCol2CellStyle = new iGCellStyle(true);
    this.igHistoryCol2ColHdrStyle = new iGColHdrStyle(true);
    this.panel2 = new Panel();
    this.cbAttributes = new ComboBox();
    this.igHistory = new iGrid();
    this.panel1 = new Panel();
    this.btnOK = new Button();
    this.panel2.SuspendLayout();
    ((ISupportInitialize) this.igHistory).BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.igHistoryCol0CellStyle, "igHistoryCol0CellStyle");
    this.igHistoryCol0CellStyle.ValueType = typeof (DateTime);
    this.igHistoryCol1CellStyle.TextFormatFlags = iGStringFormatFlags.WordWrap;
    this.panel2.Controls.Add((Control) this.cbAttributes);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.cbAttributes, "cbAttributes");
    this.cbAttributes.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbAttributes.Name = "cbAttributes";
    this.cbAttributes.SelectionChangeCommitted += new EventHandler(this.cbAttributes_SelectionChangeCommitted);
    this.igHistory.AutoResizeCols = true;
    iGcolPattern1.CellStyle = this.igHistoryCol0CellStyle;
    iGcolPattern1.ColHdrStyle = this.igHistoryCol0ColHdrStyle;
    iGcolPattern1.SortOrder = iGSortOrder.Descending;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern2.CellStyle = this.igHistoryCol1CellStyle;
    iGcolPattern2.ColHdrStyle = this.igHistoryCol1ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    iGcolPattern3.CellStyle = this.igHistoryCol2CellStyle;
    iGcolPattern3.ColHdrStyle = this.igHistoryCol2ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern3, "iGColPattern3");
    this.igHistory.Cols.AddRange(new iGColPattern[3]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3
    });
    componentResourceManager.ApplyResources((object) this.igHistory, "igHistory");
    this.igHistory.GroupBox.BackColor = SystemColors.AppWorkspace;
    this.igHistory.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this.igHistory.GroupBox.HintForeColor = SystemColors.ControlText;
    this.igHistory.GroupBox.Text = componentResourceManager.GetString("igHistory.GroupBox.Text");
    this.igHistory.GroupBox.Visible = true;
    this.igHistory.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this.igHistory.Header.Height = (int) componentResourceManager.GetObject("igHistory.Header.Height");
    this.igHistory.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this.igHistory.Name = "igHistory";
    this.igHistory.ReadOnly = true;
    this.igHistory.RowMode = true;
    this.igHistory.Tag = (object) "     ";
    this.panel1.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnOK;
    this.Controls.Add((Control) this.igHistory);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AttributesHistory);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.AttributesHistory_Load);
    this.FormClosing += new FormClosingEventHandler(this.AttributesHistory_FormClosing);
    this.panel2.ResumeLayout(false);
    ((ISupportInitialize) this.igHistory).EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
