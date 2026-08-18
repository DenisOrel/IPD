// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.RenameAttributes
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class RenameAttributes : Form
{
  private DataTable _attListTable;
  private List<IMSAttributeType> _attList;
  private IContainer components;
  private Button button1;
  private ListBox lbGroups;
  private ListBox sourceAttsListBox;
  private Label label1;
  private Label label2;
  private Label label3;
  private ListBox destAttsListBox;
  private Button btReplace;
  private ProgressBar progressBar1;
  private CheckBox cbDeleteAtt;
  private Label lbObjects;
  private Label lbTables;
  private Button btAnalize;
  private Label lbCatption;
  private Label lbBadTypes;
  private Button btRefreshList;

  public RenameAttributes() => this.InitializeComponent();

  public static void Execute()
  {
    using (RenameAttributes renameAttributes = new RenameAttributes())
    {
      int num = (int) renameAttributes.ShowDialog();
    }
  }

  private void CollectData()
  {
    this._attList = MetaDataHelper.GetAttributeTypesList();
    this._attListTable = new DataTable();
    this._attListTable.Columns.Add("F_NAME", typeof (string));
    this._attListTable.Columns.Add("F_1", typeof (int));
    this._attListTable.Columns.Add("F_2", typeof (int));
    this._attListTable.Columns.Add("F_3", typeof (int));
    this._attListTable.Columns.Add("F_4", typeof (int));
    this._attListTable.Columns.Add("F_5", typeof (int));
    this._attListTable.Columns.Add("F_6", typeof (int));
    this._attListTable.Columns.Add("F_7", typeof (int));
    this._attListTable.Columns.Add("F_8", typeof (int));
    this._attListTable.Columns.Add("F_9", typeof (int));
    this._attListTable.PrimaryKey = new DataColumn[1]
    {
      this._attListTable.Columns[0]
    };
    this._attListTable.DefaultView.Sort = "F_NAME";
    foreach (IMSAttributeType att in this._attList)
      this.AddAtt(att, this._attListTable);
    for (int index = this._attListTable.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = this._attListTable.Rows[index];
      if (DBNull.Value.Equals(row[2]) || Convert.ToInt32(row[2]) == 0)
        row.Delete();
    }
    this._attListTable.AcceptChanges();
    this.lbGroups.DataSource = (object) this._attListTable;
    this.lbGroups.DisplayMember = "F_NAME";
  }

  private void AddAtt(IMSAttributeType attType, DataTable dt)
  {
    string key = attType.Name;
    int length = key.IndexOf('^');
    if (length != -1)
      key = key.Substring(0, length);
    DataRow row = dt.Rows.Find((object) key);
    if (row == null)
      row = dt.Rows.Add((object) key);
    this.AddAtt(row, attType.AttributeID);
  }

  private void AddAtt(DataRow row, int attId)
  {
    int num = row.Table.Columns.Count - 1;
    for (int columnIndex = 1; columnIndex < num; ++columnIndex)
    {
      if (DBNull.Value.Equals(row[columnIndex]) || Convert.ToInt32(row[columnIndex]) == 0)
      {
        row[columnIndex] = (object) attId;
        break;
      }
    }
  }

  private void RenameAttributes_Shown(object sender, EventArgs e) => this.CollectData();

  private void OnGroups_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ClearData();
    this.btReplace.Enabled = false;
    if (!(this.lbGroups.SelectedItem is DataRowView selectedItem))
      return;
    DataRow row = selectedItem.Row;
    if (row == null)
      return;
    IMSAttributeType[] attributes = this.GetAttributes(row);
    this.sourceAttsListBox.Items.Clear();
    this.sourceAttsListBox.Items.AddRange((object[]) attributes);
    this.destAttsListBox.Items.Clear();
    this.destAttsListBox.Items.AddRange((object[]) attributes);
    try
    {
      this.sourceAttsListBox.SelectedIndex = 1;
      this.destAttsListBox.SelectedIndex = 0;
    }
    catch
    {
    }
  }

  private void ClearData()
  {
    this.lbBadTypes.Text = string.Empty;
    this.lbCatption.Text = string.Empty;
    this.cbDeleteAtt.Checked = false;
    this.progressBar1.Value = 0;
    this.progressBar1.Maximum = 0;
    this.lbObjects.Text = string.Empty;
    this.lbTables.Text = string.Empty;
    this.lbObjects.Tag = (object) null;
    this.lbTables.Tag = (object) null;
  }

  private IMSAttributeType GetAttribute(int attId)
  {
    foreach (IMSAttributeType att in this._attList)
    {
      if (att.AttributeID == attId)
        return att;
    }
    return (IMSAttributeType) null;
  }

  private IMSAttributeType[] GetAttributes(DataRow dr)
  {
    List<IMSAttributeType> imsAttributeTypeList = new List<IMSAttributeType>();
    int num = dr.Table.Columns.Count - 1;
    for (int columnIndex = 1; columnIndex < num && !DBNull.Value.Equals(dr[columnIndex]) && Convert.ToInt32(dr[columnIndex]) != 0; ++columnIndex)
    {
      IMSAttributeType attribute = this.GetAttribute(Convert.ToInt32(dr[columnIndex]));
      if (attribute != null)
        imsAttributeTypeList.Add(attribute);
    }
    return imsAttributeTypeList.ToArray();
  }

  private void OnGroups_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    Brush brush = Brushes.DarkGray;
    string empty = string.Empty;
    if (this.lbGroups.Items[e.Index] is DataRowView dataRowView)
    {
      DataRow row = dataRowView.Row;
      if (row != null)
      {
        empty = row[0].ToString();
        if (DBNull.Value.Equals(row[3]) || Convert.ToInt32(row[3]) == 0)
          brush = Brushes.Black;
      }
    }
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = Brushes.White;
    e.Graphics.DrawString(empty, e.Font, brush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    e.DrawFocusRectangle();
  }

  private void OnAtts_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (!(sender is ListBox listBox))
      return;
    e.DrawBackground();
    Brush brush = Brushes.Black;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = Brushes.White;
    IMSAttributeType imsAttributeType = listBox.Items[e.Index] as IMSAttributeType;
    string s = $"{imsAttributeType.Name} [{imsAttributeType.ShortName}]";
    e.Graphics.DrawString(s, e.Font, brush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    e.DrawFocusRectangle();
  }

  private void OnSourceAtts_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.lbBadTypes.Text = string.Empty;
    this.ClearData();
    this.btReplace.Enabled = false;
    if (this.sourceAttsListBox.SelectedItem is IMSAttributeType selectedItem1 && this.destAttsListBox.SelectedItem is IMSAttributeType selectedItem2)
    {
      if (selectedItem1.AttributeID == selectedItem2.AttributeID)
        return;
      if (selectedItem1.FieldType != selectedItem2.FieldType)
      {
        this.lbBadTypes.Text = "Несовпадение типов атрибутов !!!";
        return;
      }
    }
    this.btReplace.Enabled = true;
  }

  private List<long> GetObjectsList()
  {
    IMSAttributeType selectedItem = this.sourceAttsListBox.SelectedItem as IMSAttributeType;
    List<long> objectsList = (List<long>) null;
    DBRecordSetParams rParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(selectedItem.AttributeID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.NONE, 0, true)
    }, new object[1]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = ImbaseHelper.SelectObjects(sessionKeeper.Session, rParams, -1);
      objectsList = new List<long>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        objectsList.Add(Convert.ToInt64(row[0]));
    }
    return objectsList;
  }

  private void OnAnalize_Click(object sender, EventArgs e)
  {
    List<long> objectsList = this.GetObjectsList();
    this.lbObjects.Text = "Объектов : " + objectsList.Count.ToString();
    this.lbObjects.Tag = (object) objectsList;
    this.progressBar1.Maximum = objectsList.Count;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IMSAttributeType selectedItem = this.sourceAttsListBox.SelectedItem as IMSAttributeType;
      List<long> tablesWithAtt = (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).GetTablesWithAtt(sessionKeeper.Session.SessionGUID, selectedItem.AttributeID);
      this.lbTables.Text = "Таблиц : " + tablesWithAtt.Count.ToString();
      this.lbTables.Tag = (object) tablesWithAtt;
      this.progressBar1.Maximum += tablesWithAtt.Count;
    }
    Application.DoEvents();
  }

  private void OnReplace_Click(object sender, EventArgs e)
  {
    this.Enabled = false;
    try
    {
      if (this.lbTables.Tag == null)
        this.OnAnalize_Click((object) this.btAnalize, EventArgs.Empty);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IMSAttributeType selectedItem1 = this.sourceAttsListBox.SelectedItem as IMSAttributeType;
        IMSAttributeType selectedItem2 = this.destAttsListBox.SelectedItem as IMSAttributeType;
        if (this.lbObjects.Tag is List<long> tag1)
        {
          int index = 0;
          for (int count = tag1.Count; index < count; ++index)
          {
            long objectID = tag1[index];
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
            this.lbCatption.Text = "Объект : " + objectInfo.Caption;
            Application.DoEvents();
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
            if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
              dbObject.CheckOut();
            else if (dbObject.ObjectModifyMode != ObjectModifyModes.InBase)
              throw new Exception($"Невозможно изменить объект {objectInfo.Caption}.");
            IDBAttribute attributeById = dbObject.GetAttributeByID(selectedItem1.AttributeID);
            if (attributeById != null)
            {
              dbObject.Attributes.AddAttribute(selectedItem2.AttributeID, false).Values = attributeById.Values;
              attributeById.Delete(0L);
            }
            if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
              dbObject.CheckIn();
            this.progressBar1.PerformStep();
            this.lbObjects.Text = "Объектов : " + (count - index - 1).ToString();
            Application.DoEvents();
          }
        }
        if (this.lbTables.Tag is List<long> tag2)
        {
          int index = 0;
          for (int count = tag2.Count; index < count; ++index)
          {
            long num = tag2[index];
            this.lbCatption.Text = "Таблица : " + sessionKeeper.Session.GetObjectInfo(num).Caption;
            DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, num, true);
            this.ReplaceAttribute(tables, selectedItem1.AttributeGuid, selectedItem2.AttributeGuid);
            if (tables.HasChanges())
            {
              tables.AcceptChanges();
              TableLoadHelper.StoreData(sessionKeeper.Session, num, tables, (ITablesIndexer) null);
            }
            this.progressBar1.PerformStep();
            this.lbTables.Text = "Таблиц : " + (count - index - 1).ToString();
            Application.DoEvents();
          }
        }
        if (!this.cbDeleteAtt.Checked)
          return;
        sessionKeeper.Session.GetAttributeType(selectedItem1.AttributeID).Delete(0L);
        (this.lbGroups.SelectedItem as DataRowView).Row.Delete();
        this.OnGroups_SelectedIndexChanged((object) this.lbGroups, EventArgs.Empty);
      }
    }
    finally
    {
      this.Enabled = true;
    }
  }

  private void ReplaceAttribute(DataSet dataSet, Guid srcGuid, Guid destGuid)
  {
    string str1 = srcGuid.ToString();
    string newValue = destGuid.ToString();
    DataTable table = dataSet.Tables["IMS_ATTR_TYPES"];
    DataColumn column = dataSet.Tables["IMS_DATA"].Columns[str1];
    if (column != null)
      column.ColumnName = newValue;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (row["F_ATTRIBUTE_GUID"].ToString().Equals(str1, StringComparison.InvariantCultureIgnoreCase))
      {
        row["F_ATTRIBUTE_GUID"] = (object) newValue;
      }
      else
      {
        string str2 = row["F_FORMULA"].ToString();
        if (str2.IndexOf(str1) != -1)
          row["F_FORMULA"] = (object) str2.Replace(str1, newValue);
      }
    }
  }

  private void OnRefresh_Click(object sender, EventArgs e)
  {
    this.CollectData();
    this.OnGroups_SelectedIndexChanged((object) this.lbGroups, EventArgs.Empty);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.button1 = new Button();
    this.lbGroups = new ListBox();
    this.sourceAttsListBox = new ListBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.destAttsListBox = new ListBox();
    this.btReplace = new Button();
    this.progressBar1 = new ProgressBar();
    this.cbDeleteAtt = new CheckBox();
    this.lbObjects = new Label();
    this.lbTables = new Label();
    this.btAnalize = new Button();
    this.lbCatption = new Label();
    this.lbBadTypes = new Label();
    this.btRefreshList = new Button();
    this.SuspendLayout();
    this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Location = new Point(1034, 451);
    this.button1.Name = "button1";
    this.button1.Size = new Size(75, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "OK";
    this.button1.UseVisualStyleBackColor = true;
    this.lbGroups.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbGroups.DrawMode = DrawMode.OwnerDrawFixed;
    this.lbGroups.FormattingEnabled = true;
    this.lbGroups.HorizontalScrollbar = true;
    this.lbGroups.ItemHeight = 17;
    this.lbGroups.Location = new Point(12, 29);
    this.lbGroups.Name = "lbGroups";
    this.lbGroups.Size = new Size(384, 395);
    this.lbGroups.TabIndex = 1;
    this.lbGroups.DrawItem += new DrawItemEventHandler(this.OnGroups_DrawItem);
    this.lbGroups.SelectedIndexChanged += new EventHandler(this.OnGroups_SelectedIndexChanged);
    this.sourceAttsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.sourceAttsListBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.sourceAttsListBox.FormattingEnabled = true;
    this.sourceAttsListBox.HorizontalScrollbar = true;
    this.sourceAttsListBox.Location = new Point(402, 29);
    this.sourceAttsListBox.Name = "sourceAttsListBox";
    this.sourceAttsListBox.Size = new Size(344, 147);
    this.sourceAttsListBox.Sorted = true;
    this.sourceAttsListBox.TabIndex = 3;
    this.sourceAttsListBox.DrawItem += new DrawItemEventHandler(this.OnAtts_DrawItem);
    this.sourceAttsListBox.SelectedIndexChanged += new EventHandler(this.OnSourceAtts_SelectedIndexChanged);
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(408, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(69, 13);
    this.label1.TabIndex = 4;
    this.label1.Text = "Что меняем";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 13);
    this.label2.Name = "label2";
    this.label2.Size = new Size(98, 13);
    this.label2.TabIndex = 5;
    this.label2.Text = "Группы атрибутов";
    this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(760, 13);
    this.label3.Name = "label3";
    this.label3.Size = new Size(83, 13);
    this.label3.TabIndex = 7;
    this.label3.Text = "На что меняем";
    this.destAttsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.destAttsListBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.destAttsListBox.FormattingEnabled = true;
    this.destAttsListBox.HorizontalScrollbar = true;
    this.destAttsListBox.Location = new Point(752, 29);
    this.destAttsListBox.Name = "destAttsListBox";
    this.destAttsListBox.Size = new Size(358, 147);
    this.destAttsListBox.Sorted = true;
    this.destAttsListBox.TabIndex = 6;
    this.destAttsListBox.DrawItem += new DrawItemEventHandler(this.OnAtts_DrawItem);
    this.destAttsListBox.SelectedIndexChanged += new EventHandler(this.OnSourceAtts_SelectedIndexChanged);
    this.btReplace.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btReplace.Enabled = false;
    this.btReplace.Location = new Point(402, 306);
    this.btReplace.Name = "btReplace";
    this.btReplace.Size = new Size(75, 23);
    this.btReplace.TabIndex = 8;
    this.btReplace.Text = "Заменить";
    this.btReplace.UseVisualStyleBackColor = true;
    this.btReplace.Click += new EventHandler(this.OnReplace_Click);
    this.progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.progressBar1.Location = new Point(402, 275);
    this.progressBar1.Name = "progressBar1";
    this.progressBar1.Size = new Size(708, 20);
    this.progressBar1.Step = 1;
    this.progressBar1.TabIndex = 11;
    this.cbDeleteAtt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.cbDeleteAtt.AutoSize = true;
    this.cbDeleteAtt.Location = new Point(504, 312);
    this.cbDeleteAtt.Name = "cbDeleteAtt";
    this.cbDeleteAtt.Size = new Size(187, 17);
    this.cbDeleteAtt.TabIndex = 12;
    this.cbDeleteAtt.Text = "Удалить атрибут после замены";
    this.cbDeleteAtt.UseVisualStyleBackColor = true;
    this.lbObjects.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.lbObjects.AutoSize = true;
    this.lbObjects.Location = new Point(501, 206);
    this.lbObjects.Name = "lbObjects";
    this.lbObjects.Size = new Size(60, 13);
    this.lbObjects.TabIndex = 13;
    this.lbObjects.Text = "Объектов:";
    this.lbTables.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.lbTables.AutoSize = true;
    this.lbTables.Location = new Point(501, 227);
    this.lbTables.Name = "lbTables";
    this.lbTables.Size = new Size(47, 13);
    this.lbTables.TabIndex = 14;
    this.lbTables.Text = "Таблиц:";
    this.btAnalize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btAnalize.Location = new Point(402, 206);
    this.btAnalize.Name = "btAnalize";
    this.btAnalize.Size = new Size(75, 23);
    this.btAnalize.TabIndex = 15;
    this.btAnalize.Text = "Анализ";
    this.btAnalize.UseVisualStyleBackColor = true;
    this.btAnalize.Click += new EventHandler(this.OnAnalize_Click);
    this.lbCatption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.lbCatption.AutoSize = true;
    this.lbCatption.Location = new Point(402, 256 /*0x0100*/);
    this.lbCatption.Name = "lbCatption";
    this.lbCatption.Size = new Size(137, 13);
    this.lbCatption.TabIndex = 16 /*0x10*/;
    this.lbCatption.Text = "Выполняемое действие...";
    this.lbBadTypes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.lbBadTypes.AutoSize = true;
    this.lbBadTypes.ForeColor = Color.OrangeRed;
    this.lbBadTypes.Location = new Point(402, 181);
    this.lbBadTypes.Name = "lbBadTypes";
    this.lbBadTypes.Size = new Size(13, 13);
    this.lbBadTypes.TabIndex = 17;
    this.lbBadTypes.Text = "_";
    this.btRefreshList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btRefreshList.Location = new Point(15, 449);
    this.btRefreshList.Name = "btRefreshList";
    this.btRefreshList.Size = new Size(139, 23);
    this.btRefreshList.TabIndex = 18;
    this.btRefreshList.Text = "Обновить список";
    this.btRefreshList.UseVisualStyleBackColor = true;
    this.btRefreshList.Click += new EventHandler(this.OnRefresh_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(1117, 484);
    this.Controls.Add((Control) this.btRefreshList);
    this.Controls.Add((Control) this.lbBadTypes);
    this.Controls.Add((Control) this.lbCatption);
    this.Controls.Add((Control) this.btAnalize);
    this.Controls.Add((Control) this.lbTables);
    this.Controls.Add((Control) this.lbObjects);
    this.Controls.Add((Control) this.cbDeleteAtt);
    this.Controls.Add((Control) this.progressBar1);
    this.Controls.Add((Control) this.btReplace);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.destAttsListBox);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.sourceAttsListBox);
    this.Controls.Add((Control) this.lbGroups);
    this.Controls.Add((Control) this.button1);
    this.MinimumSize = new Size(1099, 523);
    this.Name = nameof (RenameAttributes);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Переименование атрибутов";
    this.Shown += new EventHandler(this.RenameAttributes_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
