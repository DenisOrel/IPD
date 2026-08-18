
// Type: Intermech.Client.Core.FormDesigner.Controls.AttributeEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Форма для выбора GUID'а атрибута.</summary>
public class AttributeEditorForm : Form
{
  private FormLinks _formLinks;
  private List<MultiValueModes> _modes;
  private List<int> _sysSpecialAttr;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlMsg;
  private Panel _pnlType;
  private Panel _pnlSelectionAttr;
  private Label _lbMsg;
  private Label _lbType;
  private Label _lbSearch;
  private PictureBox _pictFirstHLine;
  private PictureBox _pictSecondHLine;
  private CheckBox _chbAllAttr;
  private ComboBox _cbType;
  private TextBox _txtFindAttr;
  private ListBox _lstAttrCollection;
  private Button _btnOK;
  private Button _btnCancel;
  private StatusStrip _status;
  private PictureBox pictureBox1;
  private ToolStripStatusLabel _statusMsg;

  /// <summary>
  /// 
  /// </summary>
  public AttributeInfo Result
  {
    get
    {
      AttributeInfo result = new AttributeInfo();
      if (this._lstAttrCollection.SelectedItem != null)
      {
        IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(Convert.ToString(this._lstAttrCollection.SelectedItem), false);
        result.AttributeGuid = attributeType != null ? attributeType.GUID : Guid.Empty;
      }
      return result;
    }
  }

  /// <summary>
  /// Создано для случая, когда на форме необходимо поменять значение системных атрибутов.
  /// Поэтому, чтобы не появлялся полный список системных атрибутов, необходимо указать идентификаторы атрибутов, которые необходимо отобразить.
  /// </summary>
  public List<int> SysSpecialAttr
  {
    get => this._sysSpecialAttr;
    set
    {
      FieldTypes fieldType = AttributesTypeHelper.GetFieldType(Convert.ToString(this._cbType.SelectedItem));
      if (value == null || value.Count == 0)
      {
        if (fieldType == FieldTypes.ftSystem)
          this._cbType.SelectedIndex = 0;
        object caption = (object) AttributesTypeHelper.GetCaption(FieldTypes.ftSystem);
        if (!this._cbType.Items.Contains(caption))
          return;
        this._cbType.Items.Remove(caption);
      }
      else
      {
        this._sysSpecialAttr = value;
        if (fieldType != FieldTypes.ftSystem)
          return;
        this._chbAllAttr.Checked = this._chbAllAttr.Enabled = false;
        this.FillListBox(this.LoadAttributes(fieldType, this._modes));
      }
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="formLinks"></param>
  /// <param name="ai"></param>
  /// <param name="fields"></param>
  /// <param name="modes"></param>
  public AttributeEditorForm(
    FormLinks formLinks,
    AttributeInfo ai,
    FieldTypes[] fields,
    MultiValueModes[] modes)
  {
    this.InitializeComponent();
    this._formLinks = formLinks;
    this._modes = new List<MultiValueModes>((IEnumerable<MultiValueModes>) modes);
    this._chbAllAttr.Enabled = !(this._chbAllAttr.Checked = this._formLinks == null || this._formLinks.Count == 0);
    new List<FieldTypes>((IEnumerable<FieldTypes>) fields).ForEach((Action<FieldTypes>) (x => this._cbType.Items.Add((object) AttributesTypeHelper.GetCaption(x))));
    try
    {
      if (ai == null || !(ai.AttributeGuid != Guid.Empty))
        return;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(ai.AttributeGuid);
      if (attributeType == null)
        return;
      this._cbType.SelectedItem = (object) AttributesTypeHelper.GetCaption(attributeType.FieldType);
      this._lstAttrCollection.SelectedItem = (object) attributeType.Name;
    }
    finally
    {
      if (this._cbType.SelectedIndex == -1)
        this._cbType.SelectedIndex = 0;
      this.SetLabelMessageText(formLinks);
    }
  }

  /// <summary>Изменения состояния чекбокса.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cbType_SelectedIndexChanged(object sender, EventArgs e)
  {
    FieldTypes fieldType = AttributesTypeHelper.GetFieldType((string) this._cbType.SelectedItem);
    if (fieldType == FieldTypes.ftSystem)
      this._chbAllAttr.Checked = this._chbAllAttr.Enabled = this._sysSpecialAttr == null;
    else
      this._chbAllAttr.Enabled = true;
    this.FillListBox(this.LoadAttributes(fieldType, this._modes));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbAllAttr_CheckedChanged(object sender, EventArgs e)
  {
    this.FillListBox(this.LoadAttributes(AttributesTypeHelper.GetFieldType((string) this._cbType.SelectedItem), this._modes));
  }

  /// <summary>Двойной клик по списку атрибутов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lstAttrCollection_DoubleClick(object sender, EventArgs e)
  {
    if (this._lstAttrCollection.SelectedIndex <= -1)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Изменение индекса выделенного атрибута.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lstAttrCollection_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnOK.Enabled = this._lstAttrCollection.SelectedIndex >= 0;
    if (this._lstAttrCollection.SelectedIndex <= -1)
      return;
    this._txtFindAttr.TextChanged -= new EventHandler(this.On_txtFindAttr_TextChanged);
    this._txtFindAttr.Text = Convert.ToString(this._lstAttrCollection.SelectedItem);
    this._txtFindAttr.TextChanged += new EventHandler(this.On_txtFindAttr_TextChanged);
  }

  /// <summary>Изменение текста в контроле поиска.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtFindAttr_TextChanged(object sender, EventArgs e)
  {
    this._lstAttrCollection.SelectedIndexChanged -= new EventHandler(this.On_lstAttrCollection_SelectedIndexChanged);
    bool flag = false;
    if (!string.IsNullOrEmpty(this._txtFindAttr.Text))
    {
      foreach (string str in this._lstAttrCollection.Items)
      {
        if (str.StartsWith(this._txtFindAttr.Text, StringComparison.CurrentCultureIgnoreCase))
        {
          flag = true;
          this._lstAttrCollection.SelectedItem = (object) str;
          break;
        }
      }
    }
    this._btnOK.Enabled = flag;
    if (!flag)
      this._lstAttrCollection.SelectedIndex = -1;
    this._lstAttrCollection.SelectedIndexChanged += new EventHandler(this.On_lstAttrCollection_SelectedIndexChanged);
  }

  /// <summary>Закрытие формы.</summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Загрузка формы.</summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Заполнение листбокса.</summary>
  /// <param name="collection"></param>
  private void FillListBox(string[] collection)
  {
    this._lstAttrCollection.Items.Clear();
    this._txtFindAttr.Text = string.Empty;
    if (collection != null)
      this._lstAttrCollection.Items.AddRange((object[]) collection);
    this._statusMsg.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1141"), (object) this._lstAttrCollection.Items.Count);
  }

  /// <summary>Загрузка атрибутов.</summary>
  /// <param name="fieldtype"></param>
  /// <param name="modes"></param>
  /// <returns></returns>
  private string[] LoadAttributes(FieldTypes fieldtype, List<MultiValueModes> modes)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<string> stringList = new List<string>();
      if (this._chbAllAttr.Checked)
      {
        DataTable dataTable = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select("", (object) fieldtype);
        Guid empty = Guid.Empty;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["F_GUID"] != DBNull.Value && row["F_GUID"] != null)
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(Convert.ToString(row["F_GUID"])));
            if (modes.Contains(attributeType.MultipleValued))
              stringList.Add(attributeType.Name);
          }
        }
      }
      else if (this._sysSpecialAttr != null && AttributesTypeHelper.GetFieldType(Convert.ToString(this._cbType.SelectedItem)) == FieldTypes.ftSystem)
      {
        foreach (int anAttributeType in this._sysSpecialAttr)
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeType, false);
          if (attributeType != null && modes.Contains(attributeType.MultipleValued))
            stringList.Add(attributeType.Name);
        }
      }
      else
      {
        foreach (IFormDesignerFormLinksProvider formLink1 in (List<IFormDesignerFormLinksProvider>) this._formLinks)
        {
          if (!formLink1.Loaded)
            formLink1.Load(this._formLinks.FormID);
          foreach (FormLink formLink2 in formLink1.FormLinks)
          {
            List<int> attributes = formLink2.Attributes;
            if (attributes != null)
            {
              foreach (int anAttributeType in attributes)
              {
                IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeType, false);
                if (attributeType != null && attributeType.AttributeType == fieldtype && modes.Contains(attributeType.MultipleValued) && !stringList.Contains(attributeType.Name))
                  stringList.Add(attributeType.Name);
              }
            }
          }
        }
      }
      stringList.Sort((IComparer<string>) StringComparer.CurrentCulture);
      return stringList.ToArray();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formLinks"></param>
  private void SetLabelMessageText(FormLinks formLinks)
  {
    if (formLinks.FirstOrDefault<IFormDesignerFormLinksProvider>((System.Func<IFormDesignerFormLinksProvider, bool>) (x => x.FormLinks.Count > 0)) == null)
      return;
    this._pnlMsg.Visible = false;
    this._pictFirstHLine.Visible = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttributeEditorForm));
    this._status = new StatusStrip();
    this._statusMsg = new ToolStripStatusLabel();
    this._pnlMsg = new Panel();
    this.pictureBox1 = new PictureBox();
    this._lbMsg = new Label();
    this._pnlSelectionAttr = new Panel();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._lstAttrCollection = new ListBox();
    this._txtFindAttr = new TextBox();
    this._lbSearch = new Label();
    this._cbType = new ComboBox();
    this._chbAllAttr = new CheckBox();
    this._lbType = new Label();
    this._pictFirstHLine = new PictureBox();
    this._pnlType = new Panel();
    this._pictSecondHLine = new PictureBox();
    this._status.SuspendLayout();
    this._pnlMsg.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this._pnlSelectionAttr.SuspendLayout();
    ((ISupportInitialize) this._pictFirstHLine).BeginInit();
    this._pnlType.SuspendLayout();
    ((ISupportInitialize) this._pictSecondHLine).BeginInit();
    this.SuspendLayout();
    this._status.BackColor = SystemColors.ActiveBorder;
    componentResourceManager.ApplyResources((object) this._status, "_status");
    this._status.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._statusMsg
    });
    this._status.Name = "_status";
    this._statusMsg.Name = "_statusMsg";
    componentResourceManager.ApplyResources((object) this._statusMsg, "_statusMsg");
    this._pnlMsg.BackColor = SystemColors.InactiveBorder;
    this._pnlMsg.Controls.Add((Control) this.pictureBox1);
    this._pnlMsg.Controls.Add((Control) this._lbMsg);
    componentResourceManager.ApplyResources((object) this._pnlMsg, "_pnlMsg");
    this._pnlMsg.Name = "_pnlMsg";
    this.pictureBox1.Image = (Image) Intermech.Client.Core.Properties.Resources.Arrow_Right;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this._lbMsg, "_lbMsg");
    this._lbMsg.Name = "_lbMsg";
    this._pnlSelectionAttr.BackColor = SystemColors.ActiveBorder;
    this._pnlSelectionAttr.Controls.Add((Control) this._btnOK);
    this._pnlSelectionAttr.Controls.Add((Control) this._btnCancel);
    this._pnlSelectionAttr.Controls.Add((Control) this._lstAttrCollection);
    this._pnlSelectionAttr.Controls.Add((Control) this._txtFindAttr);
    this._pnlSelectionAttr.Controls.Add((Control) this._lbSearch);
    componentResourceManager.ApplyResources((object) this._pnlSelectionAttr, "_pnlSelectionAttr");
    this._pnlSelectionAttr.Name = "_pnlSelectionAttr";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._lstAttrCollection, "_lstAttrCollection");
    this._lstAttrCollection.FormattingEnabled = true;
    this._lstAttrCollection.Name = "_lstAttrCollection";
    this._lstAttrCollection.SelectedIndexChanged += new EventHandler(this.On_lstAttrCollection_SelectedIndexChanged);
    this._lstAttrCollection.DoubleClick += new EventHandler(this.On_lstAttrCollection_DoubleClick);
    componentResourceManager.ApplyResources((object) this._txtFindAttr, "_txtFindAttr");
    this._txtFindAttr.Name = "_txtFindAttr";
    this._txtFindAttr.TextChanged += new EventHandler(this.On_txtFindAttr_TextChanged);
    componentResourceManager.ApplyResources((object) this._lbSearch, "_lbSearch");
    this._lbSearch.BackColor = Color.Transparent;
    this._lbSearch.Name = "_lbSearch";
    componentResourceManager.ApplyResources((object) this._cbType, "_cbType");
    this._cbType.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cbType.Name = "_cbType";
    this._cbType.SelectedIndexChanged += new EventHandler(this.On_cbType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._chbAllAttr, "_chbAllAttr");
    this._chbAllAttr.BackColor = Color.Transparent;
    this._chbAllAttr.Name = "_chbAllAttr";
    this._chbAllAttr.UseVisualStyleBackColor = false;
    this._chbAllAttr.CheckedChanged += new EventHandler(this.On_chbAllAttr_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._lbType, "_lbType");
    this._lbType.BackColor = Color.Transparent;
    this._lbType.Name = "_lbType";
    this._pictFirstHLine.BackgroundImage = (Image) Intermech.Client.Core.Properties.Resources.Horizontal_Line;
    componentResourceManager.ApplyResources((object) this._pictFirstHLine, "_pictFirstHLine");
    this._pictFirstHLine.Name = "_pictFirstHLine";
    this._pictFirstHLine.TabStop = false;
    this._pnlType.BackColor = Color.LightSteelBlue;
    this._pnlType.Controls.Add((Control) this._lbType);
    this._pnlType.Controls.Add((Control) this._cbType);
    this._pnlType.Controls.Add((Control) this._chbAllAttr);
    componentResourceManager.ApplyResources((object) this._pnlType, "_pnlType");
    this._pnlType.Name = "_pnlType";
    this._pictSecondHLine.BackgroundImage = (Image) Intermech.Client.Core.Properties.Resources.Horizontal_Line;
    componentResourceManager.ApplyResources((object) this._pictSecondHLine, "_pictSecondHLine");
    this._pictSecondHLine.Name = "_pictSecondHLine";
    this._pictSecondHLine.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._pictSecondHLine);
    this.Controls.Add((Control) this._pnlSelectionAttr);
    this.Controls.Add((Control) this._pnlType);
    this.Controls.Add((Control) this._pictFirstHLine);
    this.Controls.Add((Control) this._pnlMsg);
    this.Controls.Add((Control) this._status);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (AttributeEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Tag = (object) "  ";
    this._status.ResumeLayout(false);
    this._status.PerformLayout();
    this._pnlMsg.ResumeLayout(false);
    this._pnlMsg.PerformLayout();
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this._pnlSelectionAttr.ResumeLayout(false);
    this._pnlSelectionAttr.PerformLayout();
    ((ISupportInitialize) this._pictFirstHLine).EndInit();
    this._pnlType.ResumeLayout(false);
    this._pnlType.PerformLayout();
    ((ISupportInitialize) this._pictSecondHLine).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
