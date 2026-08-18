
// Type: Intermech.Client.Core.ObjectCreator.Controls.ObjectTypesControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>Summary description for ObjectCreatorControlTypes.</summary>
internal class ObjectTypesControl : ObjectCreatorControl
{
  /// <summary>Таблица свойств выбранного типа объекта</summary>
  private DataTable objectTypePropertiesTable;
  private int relationTypeID = -1;
  private int objectTypeID = -1;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private Label label3;
  private Panel panelTop;
  private ComboBox comboBox1;
  private PictureBox pictureBoxSelObjType;
  private Button buttonSelObjType;
  private TextBox textBoxSelObjType;
  private Label labelSelObjType;
  private DataGrid dataGridSelObjType;

  public ObjectTypesControl(CreatedObjectItem createdObject, ArrayList objectTypes)
    : base(createdObject)
  {
    this.InitializeComponent();
    this.objectTypePropertiesTable = new DataTable("ObjectProperties");
    this.objectTypePropertiesTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_854"));
    this.objectTypePropertiesTable.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_855"));
    this.dataGridSelObjType.DataSource = (object) this.objectTypePropertiesTable;
    this.dataGridSelObjType.TableStyles.Add(new DataGridTableStyle());
    this.dataGridSelObjType.TableStyles[0].MappingName = this.objectTypePropertiesTable.TableName;
    this.dataGridSelObjType.TableStyles[0].RowHeadersVisible = false;
    foreach (DataGridColumnStyle gridColumnStyle in (BaseCollection) this.dataGridSelObjType.TableStyles[0].GridColumnStyles)
      gridColumnStyle.Width = this.dataGridSelObjType.ClientSize.Width / 2;
    foreach (object objectType in objectTypes)
      this.comboBox1.Items.Add(objectType);
    if (this.comboBox1.Items.Count == 1)
      this.comboBox1.SelectedIndex = 0;
    this.buttonSelObjType.Visible = this.buttonSelObjType.Enabled = this.textBoxSelObjType.Visible = this.textBoxSelObjType.Enabled = !(this.comboBox1.Visible = this.comboBox1.Items.Count > 0);
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectTypesControl));
    this.label3 = new Label();
    this.panelTop = new Panel();
    this.comboBox1 = new ComboBox();
    this.pictureBoxSelObjType = new PictureBox();
    this.buttonSelObjType = new Button();
    this.textBoxSelObjType = new TextBox();
    this.labelSelObjType = new Label();
    this.dataGridSelObjType = new DataGrid();
    this.panelTop.SuspendLayout();
    ((ISupportInitialize) this.pictureBoxSelObjType).BeginInit();
    this.dataGridSelObjType.BeginInit();
    this.SuspendLayout();
    this.label3.AccessibleDescription = (string) null;
    this.label3.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.ForeColor = SystemColors.GrayText;
    this.label3.Name = "label3";
    this.panelTop.AccessibleDescription = (string) null;
    this.panelTop.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panelTop, "panelTop");
    this.panelTop.BackgroundImage = (Image) null;
    this.panelTop.Controls.Add((Control) this.comboBox1);
    this.panelTop.Controls.Add((Control) this.pictureBoxSelObjType);
    this.panelTop.Controls.Add((Control) this.buttonSelObjType);
    this.panelTop.Controls.Add((Control) this.textBoxSelObjType);
    this.panelTop.Controls.Add((Control) this.labelSelObjType);
    this.panelTop.Font = (Font) null;
    this.panelTop.Name = "panelTop";
    this.comboBox1.AccessibleDescription = (string) null;
    this.comboBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
    this.comboBox1.BackgroundImage = (Image) null;
    this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox1.Font = (Font) null;
    this.comboBox1.Name = "comboBox1";
    this.comboBox1.Sorted = true;
    this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
    this.pictureBoxSelObjType.AccessibleDescription = (string) null;
    this.pictureBoxSelObjType.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.pictureBoxSelObjType, "pictureBoxSelObjType");
    this.pictureBoxSelObjType.BackgroundImage = (Image) null;
    this.pictureBoxSelObjType.Font = (Font) null;
    this.pictureBoxSelObjType.ImageLocation = (string) null;
    this.pictureBoxSelObjType.Name = "pictureBoxSelObjType";
    this.pictureBoxSelObjType.TabStop = false;
    this.buttonSelObjType.AccessibleDescription = (string) null;
    this.buttonSelObjType.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.buttonSelObjType, "buttonSelObjType");
    this.buttonSelObjType.BackgroundImage = (Image) null;
    this.buttonSelObjType.Font = (Font) null;
    this.buttonSelObjType.Name = "buttonSelObjType";
    this.buttonSelObjType.Click += new EventHandler(this.buttonSelObjType_Click);
    this.textBoxSelObjType.AccessibleDescription = (string) null;
    this.textBoxSelObjType.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textBoxSelObjType, "textBoxSelObjType");
    this.textBoxSelObjType.BackgroundImage = (Image) null;
    this.textBoxSelObjType.Name = "textBoxSelObjType";
    this.textBoxSelObjType.ReadOnly = true;
    this.labelSelObjType.AccessibleDescription = (string) null;
    this.labelSelObjType.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.labelSelObjType, "labelSelObjType");
    this.labelSelObjType.Font = (Font) null;
    this.labelSelObjType.Name = "labelSelObjType";
    this.dataGridSelObjType.AccessibleDescription = (string) null;
    this.dataGridSelObjType.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.dataGridSelObjType, "dataGridSelObjType");
    this.dataGridSelObjType.BackgroundColor = SystemColors.Window;
    this.dataGridSelObjType.BackgroundImage = (Image) null;
    this.dataGridSelObjType.CaptionBackColor = SystemColors.ControlDark;
    this.dataGridSelObjType.CaptionFont = (Font) null;
    this.dataGridSelObjType.DataMember = "";
    this.dataGridSelObjType.Font = (Font) null;
    this.dataGridSelObjType.HeaderForeColor = SystemColors.ControlText;
    this.dataGridSelObjType.Name = "dataGridSelObjType";
    this.dataGridSelObjType.ReadOnly = true;
    this.dataGridSelObjType.RowHeadersVisible = false;
    this.dataGridSelObjType.Resize += new EventHandler(this.dataGridSelObjType_Resize);
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.dataGridSelObjType);
    this.Controls.Add((Control) this.panelTop);
    this.Controls.Add((Control) this.label3);
    this.Font = (Font) null;
    this.Name = nameof (ObjectTypesControl);
    this.panelTop.ResumeLayout(false);
    this.panelTop.PerformLayout();
    ((ISupportInitialize) this.pictureBoxSelObjType).EndInit();
    this.dataGridSelObjType.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Создание строки в таблице свойств выбранного типа объекта
  /// </summary>
  /// <param name="aValName">Наименование свойства выбранного типа объекта</param>
  /// <param name="aValue">Значение свойства выбранного типа объекта</param>
  private void AddNewObjRow(string aValName, string aValue)
  {
    DataRow row = this.objectTypePropertiesTable.NewRow();
    row[0] = (object) aValName;
    row[1] = (object) aValue;
    this.objectTypePropertiesTable.Rows.Add(row);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  protected void UpdatePropertiesTable(int objectType)
  {
    this.objectTypePropertiesTable.Clear();
    if (objectType == -1)
      return;
    IDBObjectTypeInfo objectType1 = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(objectType);
    if (objectType1 == null)
      return;
    this.objectTypeID = objectType1.ObjectType;
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_856"), Convert.ToString(objectType1.ObjectType));
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_857"), objectType1.ObjectTypeName);
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_858"), objectType1.ObjectTypeShortName);
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_859"), objectType1.ObjectInstanceName);
    string aValue;
    switch (objectType1.Versionable)
    {
      case ObjectVersionModes.Abstract:
        aValue = LocalizationHolder.rm.GetString("Client.Core_860");
        break;
      case ObjectVersionModes.SingleVersion:
        aValue = LocalizationHolder.rm.GetString("Client.Core_861");
        break;
      case ObjectVersionModes.MultiVersion:
        aValue = LocalizationHolder.rm.GetString("Client.Core_862");
        break;
      default:
        aValue = "not defined";
        break;
    }
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_863"), aValue);
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_864"), objectType1.Note);
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_865"), Convert.ToString(objectType1.DefaultRelation));
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_866"), Convert.ToString(objectType1.ParentTypeID));
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_867"), Convert.ToString(objectType1.CaptionAttribute));
    this.AddNewObjRow(LocalizationHolder.rm.GetString("Client.Core_868"), objectType1.AnyAttributes ? LocalizationHolder.rm.GetString("Client.Core_869") : LocalizationHolder.rm.GetString("Client.Core_870"));
  }

  /// <summary>
  /// Обновление визуальных компонентов связанных с выбранным типом обьъекта (на первом шаге)
  /// </summary>
  public override bool Refresh(PageRefreshArgs args)
  {
    this.UpdatePropertiesTable(this.CreatedObject.ObjectTypeID);
    return base.Refresh(args);
  }

  /// <summary>Сохранение</summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public override bool Save(PageSaveArgs args)
  {
    this.CreatedObject.Create((long) this.objectTypeID);
    this.CreatedObject.RelationTypeID = this.relationTypeID;
    return base.Save(args);
  }

  /// <summary>Вызов диалога выбора типа объекта</summary>
  private void SelectObjectType()
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_88"), typeof (ObjectTypeFolder), false);
    if (selectorForm == null)
      return;
    selectorForm.StartPosition = FormStartPosition.CenterScreen;
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    this.UpdatePropertiesTable(Convert.ToInt32(selectorForm.IDList[0]));
  }

  private void buttonSelObjType_Click(object sender, EventArgs e) => this.SelectObjectType();

  private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.comboBox1.SelectedItem == null)
      return;
    ObjectTypesControl.LocalObjectType selectedItem = (ObjectTypesControl.LocalObjectType) this.comboBox1.SelectedItem;
    this.relationTypeID = selectedItem.relTypeID;
    this.UpdatePropertiesTable(selectedItem.objTypeID);
  }

  private void dataGridSelObjType_Resize(object sender, EventArgs e)
  {
    this.dataGridSelObjType.TableStyles[0].GridColumnStyles[1].Width = 2;
    foreach (DataGridColumnStyle gridColumnStyle in (BaseCollection) this.dataGridSelObjType.TableStyles[0].GridColumnStyles)
      gridColumnStyle.Width = this.dataGridSelObjType.ClientSize.Width / 2;
  }

  /// <summary>Локальный класс для работы с типами объекта</summary>
  private class LocalObjectType
  {
    public int objTypeID;
    public int relTypeID;
    private string caption;

    public LocalObjectType(int objectTypeID, int relationTypeID, string caption)
    {
      this.objTypeID = objectTypeID;
      this.relTypeID = relationTypeID;
      this.caption = caption;
    }

    public override string ToString()
    {
      string str = this.caption;
      if (str == "")
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.objTypeID);
          if (objectType != null)
            str = objectType.ObjectTypeName;
        }
      }
      return str;
    }
  }
}
