// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IntegrationErrors.IntegrationErrorsWindow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Mvp;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.IntegrationErrors;

internal class IntegrationErrorsWindow : Form
{
  private static readonly string emptyXmlString = new DBObjectErrorsBuilder().ToXmlString();
  private DBObjectErrorsBuilder errorsBuilder;
  private bool hasChanges;
  private bool editMode;
  private IContainer components;
  private ListView listView1;
  private ColumnHeader chText;
  private ToolStrip toolStrip1;
  private ToolStripButton tsbDeleteError;
  private Label lbObjectInfo;
  private Label lbObjectInfoLabel;
  private Label lbObjectId;
  private Label lbObjectIdLabel;
  private Button btApplyChanges;
  private Button btRevertChanges;
  private Button btClose;
  private GroupBox gbObjectInfo;
  private PictureBox pbObjectTypeIcon;
  private ToolStripButton tsbEditMode;
  private ToolStripSeparator toolStripSeparator1;

  public IntegrationErrorsWindow() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ObjectId { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Inject]
  public ICategoryTypeIconService CategoryTypeIconService { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Inject]
  public INotificationService NotificationService { get; set; }

  private void ValidateProperties()
  {
    if (this.ObjectId == 0L)
      throw new PresenterPropertyException("ObjectId", "Не задан идентификатор версии объекта.");
  }

  private DBObjectErrorsBuilder ReadIntegrationErrors()
  {
    string xmlString = this.ReadIntegrationErrorsXmlString();
    return xmlString != null ? new DBObjectErrorsBuilder(xmlString) : new DBObjectErrorsBuilder();
  }

  private string ReadIntegrationErrorsXmlString()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(this.ObjectId, true).GetAttributeByID(IDCache.Default.IntegrationErrors.Id);
      return attributeById != null ? (string) attributeById.Value : (string) null;
    }
  }

  private void WriteIntegrationErrors(DBObjectErrorsBuilder builder)
  {
    this.WriteIntegrationErrorsXmlString(builder.ToXmlString());
  }

  private void WriteIntegrationErrorsXmlString(string xmlString)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, true);
      IDBAttribute attributeById = dbObject.GetAttributeByID(IDCache.Default.IntegrationErrors.Id);
      if (xmlString == IntegrationErrorsWindow.emptyXmlString)
        attributeById?.Delete(0L);
      else if (attributeById != null)
        attributeById.Value = (object) xmlString;
      else
        dbObject.Attributes.AddAttribute(IDCache.Default.IntegrationErrors.Id, true, new object[1]
        {
          (object) xmlString
        });
    }
  }

  private bool CanHaveIntegrationStatus(int objectType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return new DirectObjectAttributesRef(objectType).GetAttributableType(sessionKeeper.Session).GetAttributeByID(IDCache.Default.IntegrationStatus.Id) != null;
  }

  private DBObjectIntegrationStatus ReadIntegrationStatus()
  {
    return new DBObjectIntegrationStatus(this.ReadIntegrationStatusString());
  }

  private string ReadIntegrationStatusString()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(this.ObjectId, true).GetAttributeByID(IDCache.Default.IntegrationStatus.Id);
      return attributeById != null ? (string) attributeById.Value : string.Empty;
    }
  }

  private void WriteIntegrationStatus(DBObjectIntegrationStatus status)
  {
    this.WriteIntegrationStatusString(status.IsEmpty ? string.Empty : status.Value);
  }

  private void WriteIntegrationStatusString(string statusString)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, true);
      IDBAttribute attributeById = dbObject.GetAttributeByID(IDCache.Default.IntegrationStatus.Id);
      if (string.IsNullOrEmpty(statusString))
        attributeById?.Delete(0L);
      else if (attributeById != null)
        attributeById.Value = (object) statusString;
      else
        dbObject.Attributes.AddAttribute(IDCache.Default.IntegrationStatus.Id, true, new object[1]
        {
          (object) statusString
        });
    }
  }

  private void IntegrationErrorsWindow_Load(object sender, EventArgs e)
  {
    this.ValidateProperties();
    this.PopulateObjectInfo();
    this.errorsBuilder = this.ReadIntegrationErrors();
    this.hasChanges = false;
    this.PopulateErrorsView();
    this.UpdateApplyRevertButtons();
    this.editMode = false;
    this.tsbEditMode.Enabled = true;
    this.tsbEditMode.Checked = false;
  }

  private void IntegrationErrorsWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.errorsBuilder = (DBObjectErrorsBuilder) null;
    this.hasChanges = false;
    this.ClearObjectInfo();
    this.listView1.Items.Clear();
    this.UpdateApplyRevertButtons();
    this.editMode = false;
    this.tsbEditMode.Enabled = false;
    this.tsbEditMode.Checked = false;
  }

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateCommandButtons();
  }

  private void UpdateCommandButtons()
  {
    if (this.listView1.SelectedItems.Count == 0)
      this.tsbDeleteError.Enabled = false;
    else
      this.tsbDeleteError.Enabled = this.editMode;
  }

  private void tsbEditMode_Click(object sender, EventArgs e)
  {
    this.tsbEditMode.Checked = !this.tsbEditMode.Checked;
    this.editMode = this.tsbEditMode.Checked;
    this.UpdateCommandButtons();
  }

  private void tsbDeleteError_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show($"Внимание! Удаление ошибки не приведет к ее исправлению в объекте IPS. Настоятельно рекомендуется не удалять ошибку, а выполнить приведенные в ней рекомендации.{Environment.NewLine}{Environment.NewLine}Вы действительно хотите удалить ошибку?", "Удаление ошибки интеграции", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    ListViewItem selectedItem = this.listView1.SelectedItems[0];
    this.errorsBuilder.Remove((DBObjectErrorInfo) selectedItem.Tag);
    this.hasChanges = ((this.hasChanges ? 1 : 0) | 1) != 0;
    this.UpdateApplyRevertButtons();
    int index = selectedItem.Index;
    selectedItem.Remove();
    if (index < this.listView1.Items.Count || this.listView1.Items.Count == 0)
      return;
    this.listView1.Items[index - 1].Selected = true;
  }

  private void btApplyChanges_Click(object sender, EventArgs e)
  {
    this.WriteIntegrationErrors(this.errorsBuilder);
    if (this.CanHaveIntegrationStatus(DBHelper.GetObjectType(this.ObjectId)))
    {
      DBObjectIntegrationStatus integrationStatus = new DBObjectIntegrationStatus(string.Empty);
      foreach (DBObjectErrorInfo dbObjectErrorInfo in this.errorsBuilder.GetAll())
      {
        int indexByErrorCategory = DBObjectIntegrationStatus.GetBitIndexByErrorCategory(dbObjectErrorInfo.Category);
        if (!integrationStatus.Read(indexByErrorCategory))
          integrationStatus.Write(indexByErrorCategory, true);
      }
      DBObjectIntegrationStatus status = this.ReadIntegrationStatus();
      foreach (int forErrorCategory in (IEnumerable<int>) DBObjectIntegrationStatus.GetBitIndexesForErrorCategories())
        status.Write(forErrorCategory, integrationStatus.Read(forErrorCategory));
      this.WriteIntegrationStatus(status);
    }
    this.hasChanges = false;
    this.UpdateApplyRevertButtons();
    if (this.NotificationService == null)
      return;
    this.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.ObjectId));
  }

  private void btRevertChanges_Click(object sender, EventArgs e)
  {
    this.errorsBuilder = this.ReadIntegrationErrors();
    this.hasChanges = false;
    this.PopulateErrorsView();
    this.UpdateApplyRevertButtons();
  }

  private void btClose_Click(object sender, EventArgs e)
  {
    if (this.hasChanges)
    {
      if (MessageBox.Show("Список ошибок интеграции был изменен. Сохранить сделанные изменения?", "Несохраненные изменения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.btApplyChanges.PerformClick();
      else
        this.btRevertChanges.PerformClick();
    }
    this.Close();
  }

  private void PopulateObjectInfo()
  {
    this.lbObjectInfo.Text = DBHelper.GetObjectNameInMessages(this.ObjectId);
    this.lbObjectId.Text = this.ObjectId.ToString();
    if (this.CategoryTypeIconService == null)
      return;
    int index = this.CategoryTypeIconService.IndexOf(4, DBHelper.GetObjectType(this.ObjectId));
    if (index < 0)
      return;
    this.pbObjectTypeIcon.Image = this.CategoryTypeIconService.ImageList.Images[index];
  }

  private void ClearObjectInfo()
  {
    this.lbObjectInfo.Text = string.Empty;
    this.lbObjectId.Text = string.Empty;
    if (this.pbObjectTypeIcon.Image == null)
      return;
    Image image = this.pbObjectTypeIcon.Image;
    this.pbObjectTypeIcon.Image = (Image) null;
    image.Dispose();
  }

  private void PopulateErrorsView()
  {
    this.listView1.BeginUpdate();
    try
    {
      this.listView1.Items.Clear();
      List<DBObjectErrorInfo> all = this.errorsBuilder.GetAll();
      all.Sort(new Comparison<DBObjectErrorInfo>(this.SortByCategoryAndText));
      foreach (DBObjectErrorInfo dbObjectErrorInfo in all)
        this.listView1.Items.Add(new ListViewItem()
        {
          Tag = (object) dbObjectErrorInfo,
          Text = dbObjectErrorInfo.Text
        });
      if (this.listView1.Items.Count == 0)
        return;
      this.listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
      this.listView1.Items[0].Selected = true;
    }
    finally
    {
      this.listView1.EndUpdate();
    }
  }

  private int SortByCategoryAndText(DBObjectErrorInfo x, DBObjectErrorInfo y)
  {
    int num = x.Category.CompareTo(y.Category);
    if (num == 0)
      num = x.Text.CompareTo(y.Text);
    return num;
  }

  private void UpdateApplyRevertButtons()
  {
    this.btApplyChanges.Enabled = this.hasChanges;
    this.btRevertChanges.Enabled = this.hasChanges;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IntegrationErrorsWindow));
    this.listView1 = new ListView();
    this.chText = new ColumnHeader();
    this.toolStrip1 = new ToolStrip();
    this.tsbEditMode = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tsbDeleteError = new ToolStripButton();
    this.lbObjectInfo = new Label();
    this.lbObjectInfoLabel = new Label();
    this.lbObjectId = new Label();
    this.lbObjectIdLabel = new Label();
    this.btApplyChanges = new Button();
    this.btRevertChanges = new Button();
    this.btClose = new Button();
    this.gbObjectInfo = new GroupBox();
    this.pbObjectTypeIcon = new PictureBox();
    this.toolStrip1.SuspendLayout();
    this.gbObjectInfo.SuspendLayout();
    ((ISupportInitialize) this.pbObjectTypeIcon).BeginInit();
    this.SuspendLayout();
    this.listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listView1.Columns.AddRange(new ColumnHeader[1]
    {
      this.chText
    });
    this.listView1.FullRowSelect = true;
    this.listView1.HideSelection = false;
    this.listView1.Location = new Point(12, 129);
    this.listView1.MultiSelect = false;
    this.listView1.Name = "listView1";
    this.listView1.Size = new Size(800, 336);
    this.listView1.TabIndex = 2;
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.Details;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    this.chText.Text = "Текст ошибки";
    this.chText.Width = 268;
    this.toolStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsbEditMode,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tsbDeleteError
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(824, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this.tsbEditMode.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.tsbEditMode.Enabled = false;
    this.tsbEditMode.Image = (Image) componentResourceManager.GetObject("tsbEditMode.Image");
    this.tsbEditMode.ImageTransparentColor = Color.Magenta;
    this.tsbEditMode.Name = "tsbEditMode";
    this.tsbEditMode.Size = new Size(141, 22);
    this.tsbEditMode.Text = "Режим редактирования";
    this.tsbEditMode.Click += new EventHandler(this.tsbEditMode_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this.tsbDeleteError.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.tsbDeleteError.Enabled = false;
    this.tsbDeleteError.Image = (Image) componentResourceManager.GetObject("tsbDeleteError.Image");
    this.tsbDeleteError.ImageTransparentColor = Color.Magenta;
    this.tsbDeleteError.Name = "tsbDeleteError";
    this.tsbDeleteError.Size = new Size(102, 22);
    this.tsbDeleteError.Text = "Удалить ошибку";
    this.tsbDeleteError.Click += new EventHandler(this.tsbDeleteError_Click);
    this.lbObjectInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbObjectInfo.Location = new Point(133, 23);
    this.lbObjectInfo.Name = "lbObjectInfo";
    this.lbObjectInfo.Size = new Size(659, 23);
    this.lbObjectInfo.TabIndex = 1;
    this.lbObjectInfo.TextAlign = ContentAlignment.MiddleLeft;
    this.lbObjectInfoLabel.AutoSize = true;
    this.lbObjectInfoLabel.Location = new Point(50, 28);
    this.lbObjectInfoLabel.Name = "lbObjectInfoLabel";
    this.lbObjectInfoLabel.Size = new Size(77, 13);
    this.lbObjectInfoLabel.TabIndex = 0;
    this.lbObjectInfoLabel.Text = "Имя объекта:";
    this.lbObjectId.Location = new Point(136, 46);
    this.lbObjectId.Name = "lbObjectId";
    this.lbObjectId.Size = new Size(125, 23);
    this.lbObjectId.TabIndex = 3;
    this.lbObjectId.TextAlign = ContentAlignment.MiddleLeft;
    this.lbObjectIdLabel.AutoSize = true;
    this.lbObjectIdLabel.Location = new Point(50, 51);
    this.lbObjectIdLabel.Name = "lbObjectIdLabel";
    this.lbObjectIdLabel.Size = new Size(66, 13);
    this.lbObjectIdLabel.TabIndex = 2;
    this.lbObjectIdLabel.Text = "Ид. версии:";
    this.btApplyChanges.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btApplyChanges.Enabled = false;
    this.btApplyChanges.Location = new Point(515, 484);
    this.btApplyChanges.Name = "btApplyChanges";
    this.btApplyChanges.Size = new Size(95, 25);
    this.btApplyChanges.TabIndex = 3;
    this.btApplyChanges.Text = "Применить";
    this.btApplyChanges.UseVisualStyleBackColor = true;
    this.btApplyChanges.Click += new EventHandler(this.btApplyChanges_Click);
    this.btRevertChanges.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btRevertChanges.Enabled = false;
    this.btRevertChanges.Location = new Point(616, 484);
    this.btRevertChanges.Name = "btRevertChanges";
    this.btRevertChanges.Size = new Size(95, 25);
    this.btRevertChanges.TabIndex = 4;
    this.btRevertChanges.Text = "Отменить";
    this.btRevertChanges.UseVisualStyleBackColor = true;
    this.btRevertChanges.Click += new EventHandler(this.btRevertChanges_Click);
    this.btClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btClose.Location = new Point(717, 484);
    this.btClose.Name = "btClose";
    this.btClose.Size = new Size(95, 25);
    this.btClose.TabIndex = 5;
    this.btClose.Text = "Закрыть";
    this.btClose.UseVisualStyleBackColor = true;
    this.btClose.Click += new EventHandler(this.btClose_Click);
    this.gbObjectInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbObjectInfo.Controls.Add((Control) this.pbObjectTypeIcon);
    this.gbObjectInfo.Controls.Add((Control) this.lbObjectInfoLabel);
    this.gbObjectInfo.Controls.Add((Control) this.lbObjectInfo);
    this.gbObjectInfo.Controls.Add((Control) this.lbObjectId);
    this.gbObjectInfo.Controls.Add((Control) this.lbObjectIdLabel);
    this.gbObjectInfo.Location = new Point(12, 39);
    this.gbObjectInfo.Name = "gbObjectInfo";
    this.gbObjectInfo.Padding = new Padding(5);
    this.gbObjectInfo.Size = new Size(800, 84);
    this.gbObjectInfo.TabIndex = 1;
    this.gbObjectInfo.TabStop = false;
    this.gbObjectInfo.Text = "Объект IPS";
    this.pbObjectTypeIcon.BorderStyle = BorderStyle.FixedSingle;
    this.pbObjectTypeIcon.Location = new Point(8, 24);
    this.pbObjectTypeIcon.Name = "pbObjectTypeIcon";
    this.pbObjectTypeIcon.Size = new Size(36, 20);
    this.pbObjectTypeIcon.SizeMode = PictureBoxSizeMode.CenterImage;
    this.pbObjectTypeIcon.TabIndex = 4;
    this.pbObjectTypeIcon.TabStop = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(824, 521);
    this.Controls.Add((Control) this.gbObjectInfo);
    this.Controls.Add((Control) this.btClose);
    this.Controls.Add((Control) this.btRevertChanges);
    this.Controls.Add((Control) this.btApplyChanges);
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this.listView1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(640, 400);
    this.Name = nameof (IntegrationErrorsWindow);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Ошибки интеграции";
    this.FormClosed += new FormClosedEventHandler(this.IntegrationErrorsWindow_FormClosed);
    this.Load += new EventHandler(this.IntegrationErrorsWindow_Load);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.gbObjectInfo.ResumeLayout(false);
    this.gbObjectInfo.PerformLayout();
    ((ISupportInitialize) this.pbObjectTypeIcon).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
