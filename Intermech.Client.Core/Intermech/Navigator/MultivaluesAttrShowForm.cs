
// Type: Intermech.Navigator.MultivaluesAttrShowForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator;

/// <summary>
/// Форма для отображения значений многозначных атрибутов объектов/связей
/// </summary>
public class MultivaluesAttrShowForm : Form
{
  /// <summary>
  /// Иденификатор типа атрибута, значение которого отображается в окне
  /// </summary>
  protected int attrID = -10000;
  /// <summary>
  /// Источник атрибута - объект/связь (остальные значения недопустимы)
  /// </summary>
  protected AttributeSourceTypes idSource = AttributeSourceTypes.Object;
  /// <summary>
  /// Идентификатор версии объекта/связи, чей атрибут отображается в окне
  /// </summary>
  protected long id;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel panelBottom;
  protected Button btnClose;
  private iGrid _grid;
  private ToolStrip _toolStrip;
  private ToolStripButton _copyToolStripButton;
  private ContextMenuStrip _contextMenuStrip;
  private ToolStripMenuItem _copyToolStripMenuItem;

  /// <summary>Стандартный конструктор</summary>
  public MultivaluesAttrShowForm() => this.InitializeComponent();

  /// <summary>Расширенный конструктор</summary>
  /// <param name="attrID">Иденификатор типа атрибута, значение которого отображается в окне</param>
  /// <param name="idSource">Источник атрибута - объект/связь (остальные значения недопустимы)</param>
  /// <param name="id">Идентификатор версии объекта/связи, чей атрибут отображается в окне</param>
  public MultivaluesAttrShowForm(int attrID, AttributeSourceTypes idSource, long id)
    : this()
  {
    this.Init(attrID, idSource, id);
  }

  /// <summary>
  /// Вызвать форму, отобразить значения многозначного атрибута
  /// </summary>
  /// <param name="attrID">Иденификатор типа атрибута, значение которого отображается в окне</param>
  /// <param name="idSource">Источник атрибута - объект/связь (остальные значения недопустимы)</param>
  /// <param name="id">Идентификатор версии объекта/связи, чей атрибут отображается в окне</param>
  /// <returns>Результат вызова формы как модального окна</returns>
  [STAThread]
  public static DialogResult Execute(int attrID, AttributeSourceTypes idSource, long id)
  {
    using (MultivaluesAttrShowForm multivaluesAttrShowForm = new MultivaluesAttrShowForm(attrID, idSource, id))
      return multivaluesAttrShowForm.ShowDialog();
  }

  /// <summary>Инициализация формы данными</summary>
  /// <param name="attrID">Иденификатор типа атрибута, значение которого отображается в окне</param>
  /// <param name="idSource">Источник атрибута - объект/связь (остальные значения недопустимы)</param>
  /// <param name="id">Идентификатор версии объекта/связи, чей атрибут отображается в окне</param>
  protected virtual void Init(int attrID, AttributeSourceTypes idSource, long id)
  {
    this.attrID = attrID;
    this.idSource = idSource;
    this.id = id;
    this._grid.Cols.Clear();
    iGCellStyle iGcellStyle = new iGCellStyle(true);
    iGcellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    iGcellStyle.ReadOnly = iGBool.True;
    iGcellStyle.ValueType = typeof (string);
    iGcellStyle.TextFormatFlags |= iGStringFormatFlags.WordWrap;
    iGColHdrStyle iGcolHdrStyle = new iGColHdrStyle(true);
    iGcolHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    iGCol iGcol = this._grid.Cols.Add(new iGColPattern(80 /*0x50*/, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) "", "Value", -1, (object) string.Empty, (object) null, -1));
    iGcol.CellStyle = iGcellStyle;
    iGcol.ColHdrStyle = iGcolHdrStyle;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string str1 = "";
      string str2 = "";
      IDBAttribute dbAttribute = (IDBAttribute) null;
      if (idSource == AttributeSourceTypes.Object)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(id, false);
        if (dbObject != null)
        {
          str1 = dbObject.Caption;
          dbAttribute = dbObject.GetAttributeByID(attrID);
          str2 = dbAttribute != null ? dbAttribute.Name : MetaDataHelper.GetAttributeTypeName(attrID);
        }
      }
      if (idSource == AttributeSourceTypes.Relation)
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(id, false);
        if (relation != null)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(relation.PartID, false);
          if (dbObject != null)
            str1 = dbObject.Caption;
          dbAttribute = relation.GetAttributeByID(attrID);
          str2 = dbAttribute != null ? dbAttribute.Name : MetaDataHelper.GetAttributeTypeName(attrID);
        }
      }
      this.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1246"), (object) str2, (object) str1);
      this._grid.Rows.Clear();
      if (dbAttribute != null)
      {
        string[] strArray = this.Descriptions = dbAttribute.Descriptions;
        if (strArray != null)
        {
          foreach (string str3 in strArray)
          {
            iGRow iGrow = this._grid.Rows.Add();
            iGrow.Cells["Value"].ValueType = typeof (string);
            iGrow.Cells["Value"].Value = (object) str3;
          }
        }
      }
    }
    this.UpdateControl();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string[] Descriptions { get; set; }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void MultivaluesAttrShowForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void MultivaluesAttrShowForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    foreach (iGRow row in (IEnumerable) this._grid.Rows)
      row.AutoHeight();
  }

  private void CopyToolStripButton_Click(object sender, EventArgs e) => this.Copy();

  private void CopyToolStripMenuItem_Click(object sender, EventArgs e) => this.Copy();

  private void Grid_SelectionChanged(object sender, EventArgs e) => this.UpdateControl();

  private void UpdateControl()
  {
    this.btnClose.Enabled = true;
    this._copyToolStripButton.Enabled = this._copyToolStripMenuItem.Enabled = this.CanCopy();
  }

  private bool CanCopy() => this._grid.SelectedCells.Count > 0;

  private void Copy()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (iGCell selectedCell in this._grid.SelectedCells)
    {
      if (selectedCell.Value is string)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(" ");
        stringBuilder.Append((string) selectedCell.Value);
      }
    }
    if (stringBuilder.Length <= 0)
      return;
    Clipboard.SetText(stringBuilder.ToString());
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MultivaluesAttrShowForm));
    this.panelBottom = new Panel();
    this.btnClose = new Button();
    this._grid = new iGrid();
    this._toolStrip = new ToolStrip();
    this._copyToolStripButton = new ToolStripButton();
    this._contextMenuStrip = new ContextMenuStrip(this.components);
    this._copyToolStripMenuItem = new ToolStripMenuItem();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
    this._toolStrip.SuspendLayout();
    this._contextMenuStrip.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.btnClose);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
    this.btnClose.Cursor = Cursors.Default;
    this.btnClose.DialogResult = DialogResult.Cancel;
    this.btnClose.Name = "btnClose";
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    this._grid.AutoResizeCols = true;
    this._grid.ContextMenuStrip = this._contextMenuStrip;
    this._grid.Header.AllowPress = false;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.Header.Visible = false;
    this._grid.Name = "_grid";
    this._grid.SelectionMode = iGSelectionMode.MultiExtended;
    this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this._toolStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._copyToolStripButton
    });
    componentResourceManager.ApplyResources((object) this._toolStrip, "_toolStrip");
    this._toolStrip.Name = "_toolStrip";
    this._copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._copyToolStripButton, "_copyToolStripButton");
    this._copyToolStripButton.Name = "_copyToolStripButton";
    this._copyToolStripButton.Click += new EventHandler(this.CopyToolStripButton_Click);
    this._contextMenuStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._copyToolStripMenuItem
    });
    this._contextMenuStrip.Name = "_contextMenuStrip";
    componentResourceManager.ApplyResources((object) this._contextMenuStrip, "_contextMenuStrip");
    componentResourceManager.ApplyResources((object) this._copyToolStripMenuItem, "_copyToolStripMenuItem");
    this._copyToolStripMenuItem.Name = "_copyToolStripMenuItem";
    this._copyToolStripMenuItem.Click += new EventHandler(this.CopyToolStripMenuItem_Click);
    this.AcceptButton = (IButtonControl) this.btnClose;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnClose;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._toolStrip);
    this.Controls.Add((Control) this._grid);
    this.Controls.Add((Control) this.panelBottom);
    this.MinimizeBox = false;
    this.Name = nameof (MultivaluesAttrShowForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.MultivaluesAttrShowForm_FormClosed);
    this.Load += new EventHandler(this.MultivaluesAttrShowForm_Load);
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this._grid).EndInit();
    this._toolStrip.ResumeLayout(false);
    this._toolStrip.PerformLayout();
    this._contextMenuStrip.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
