// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.AssingTableAttRights
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class AssingTableAttRights : Form, ISecurityCallback
{
  private long _rootId = -1;
  private string _collectionName;
  private List<long> _tableIds = new List<long>();
  private Dictionary<string, ListViewItem> _attributes = new Dictionary<string, ListViewItem>();
  private IContainer components;
  private ListView listView;
  private ProgressBar progressBar;
  private Button btOk;
  private Button btCancel;
  private Timer scanTimer;
  private Label lbCompleted;
  private ColumnHeader col1;

  internal static void ShowAccessRightsDialog(long rootId)
  {
    using (AssingTableAttRights assingTableAttRights = new AssingTableAttRights())
    {
      assingTableAttRights.SetData(rootId);
      if (assingTableAttRights.ShowDialog() != DialogResult.OK)
        return;
      assingTableAttRights.EditSecurity();
    }
  }

  private void SetData(long rootId)
  {
    this._rootId = rootId;
    this.listView.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
  }

  private void EditSecurity()
  {
    ListView.CheckedListViewItemCollection checkedItems = this.listView.CheckedItems;
    List<object> objectList = new List<object>(checkedItems.Count);
    StringBuilder stringBuilder = new StringBuilder();
    foreach (ListViewItem listViewItem in checkedItems)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(',');
      stringBuilder.Append(listViewItem.Text);
      int recordId = int.Parse(listViewItem.Name);
      foreach (long tableId in listViewItem.Tag as List<long>)
        objectList.Add((object) ImbaseHelper.CreateCategoryId(tableId, (long) recordId));
    }
    this._collectionName = "Права доступа к атрибутам " + stringBuilder.ToString();
    using (SecurityEditorForm securityEditorForm = new SecurityEditorForm())
      securityEditorForm.Execute(objectList.ToArray(), (ISecurityCallback) this, false);
  }

  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    long categoryId = (long) id;
    long tableId = -1;
    int attId = -1;
    ref long local1 = ref tableId;
    ref int local2 = ref attId;
    ImbaseHelper.GetObjectAndId(categoryId, out local1, out local2);
    IDBSecurity securityForAtt = EditorHelper.GetServer(session).GetSecurityForAtt(session.SessionGUID, tableId, attId);
    if (!(securityForAtt is IDBNamedSecurityCollection securityCollection))
      return securityForAtt;
    securityCollection.SetCollectionName(this._collectionName);
    return securityForAtt;
  }

  public int MaintainedCategory => 26;

  public Tuple<int, object> Applicability => (Tuple<int, object>) null;

  public AssingTableAttRights() => this.InitializeComponent();

  private void OnTimerTick(object sender, EventArgs e)
  {
    this.scanTimer.Enabled = false;
    this.progressBar.Visible = true;
    this.progressBar.Value = 0;
    this.btOk.Enabled = false;
    this.listView.BeginUpdate();
    this.listView.Items.Clear();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        QuickObjectInfo objectInfo = session.GetObjectInfo(this._rootId);
        if (objectInfo.Empty)
          return;
        if (objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseFolderTypeID)
        {
          this.ScanTableRef(session, this._rootId);
        }
        else
        {
          IDBAttribute attributeById = session.GetObject(this._rootId).GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
          if (attributeById == null || attributeById.Value == null)
            return;
          string strKey = attributeById.Value.ToString();
          DataTable tableIds = this.GetTableIDs(session, strKey);
          if (tableIds == null || tableIds.Rows.Count == 0)
            return;
          string format = $"{this.lbCompleted.Text}{"{0}"}%";
          int count = tableIds.Rows.Count;
          this.progressBar.Maximum = count;
          foreach (DataRow row in (InternalDataCollectionBase) tableIds.Rows)
          {
            this.lbCompleted.Text = string.Format(format, (object) Convert.ToInt32(Math.Floor((double) ++this.progressBar.Value / Convert.ToDouble(count) * 100.0)));
            Application.DoEvents();
            if (this.DialogResult == DialogResult.Cancel)
              break;
            if (!(row[0] is DBNull))
            {
              try
              {
                this.ScanTableRef(session, Convert.ToInt64(row[0]));
              }
              catch (Exception ex)
              {
                Trace.WriteLine(ex.Message);
              }
              ++this.progressBar.Value;
              Application.DoEvents();
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
    finally
    {
      this.listView.CheckBoxes = true;
      this.listView.Columns[0].Width = -2;
      this.listView.EndUpdate();
      this.listView.Sorting = SortOrder.Ascending;
      this.listView.Sort();
      this.progressBar.Visible = false;
      this.lbCompleted.Visible = false;
    }
  }

  private void OnShown(object sender, EventArgs e) => this.scanTimer.Enabled = true;

  private void ScanTableRef(IUserSession session, long objectId)
  {
    long linkId = 0;
    long tableId = 0;
    TableLoadHelper.CheckObjectId(session, objectId, ref linkId, ref tableId);
    if (tableId == 0L || this._tableIds.Contains(tableId))
      return;
    this._tableIds.Add(tableId);
    DataSet tables = TableLoadHelper.GetTables(session, tableId, true);
    if (tables == null || tables.Tables.Count == 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) tables.Tables["IMS_ATTR_TYPES"].Rows)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"])));
      if (attributeType != null)
      {
        string key = Convert.ToString(attributeType.AttributeID);
        if (this._attributes.ContainsKey(key))
        {
          List<long> tag = this._attributes[key].Tag as List<long>;
          if (!tag.Contains(tableId))
            tag.Add(tableId);
        }
        else
        {
          int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
          ListViewItem listViewItem = new ListViewItem(attributeType.Name, imageIndex)
          {
            Name = key
          };
          listViewItem.Tag = (object) new List<long>()
          {
            tableId
          };
          this.listView.Items.Add(listViewItem);
          this._attributes.Add(key, listViewItem);
        }
      }
    }
  }

  private DataTable GetTableIDs(IUserSession session, string strKey)
  {
    return session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) strKey, LogicalOperators.NONE, 0, true)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    })
    {
      Contents = new ColumnContents[1]{ ColumnContents.ID }
    });
  }

  private void OnListView_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    this.btOk.Enabled = this.listView.CheckedItems.Count > 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.listView = new ListView();
    this.col1 = new ColumnHeader();
    this.progressBar = new ProgressBar();
    this.btOk = new Button();
    this.btCancel = new Button();
    this.scanTimer = new Timer(this.components);
    this.lbCompleted = new Label();
    this.SuspendLayout();
    this.listView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.col1
    });
    this.listView.Location = new Point(12, 12);
    this.listView.Name = "listView";
    this.listView.Size = new Size(359, 285);
    this.listView.TabIndex = 0;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.ItemChecked += new ItemCheckedEventHandler(this.OnListView_ItemChecked);
    this.col1.Text = "Атрибут";
    this.col1.Width = 355;
    this.progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.progressBar.Location = new Point(12, 303);
    this.progressBar.Name = "progressBar";
    this.progressBar.Size = new Size(359, 11);
    this.progressBar.Step = 1;
    this.progressBar.TabIndex = 1;
    this.progressBar.Visible = false;
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Enabled = false;
    this.btOk.Location = new Point(215, 320);
    this.btOk.Name = "btOk";
    this.btOk.Size = new Size(75, 23);
    this.btOk.TabIndex = 2;
    this.btOk.Text = "Назначить";
    this.btOk.UseVisualStyleBackColor = true;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(296, 320);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 3;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.scanTimer.Tick += new EventHandler(this.OnTimerTick);
    this.lbCompleted.AutoSize = true;
    this.lbCompleted.Location = new Point(12, 325);
    this.lbCompleted.Name = "lbCompleted";
    this.lbCompleted.Size = new Size(70, 13);
    this.lbCompleted.TabIndex = 4;
    this.lbCompleted.Text = "Выполнено :";
    this.AcceptButton = (IButtonControl) this.btOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(383, 344);
    this.Controls.Add((Control) this.lbCompleted);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.Controls.Add((Control) this.progressBar);
    this.Controls.Add((Control) this.listView);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AssingTableAttRights);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выберите поля для назначения прав доступа";
    this.Shown += new EventHandler(this.OnShown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
