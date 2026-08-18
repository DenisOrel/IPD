
// Type: Intermech.Client.Core.SaveToDiskForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class SaveToDiskForm : Form, ISaveToDiskOptions
{
  /// <summary>в каком формате сохраняем документы Интермех</summary>
  private ImDocumentFormat fmt;
  /// <summary>куда сохраняем</summary>
  private string saveFolder = string.Empty;
  /// <summary>список связей, по которым выгружаются файлы</summary>
  private List<IMSRelationType> relations;
  /// <summary>
  /// суффикс, который будет добавлен в обозначение всех точных спецификаций
  /// при сохранении их на диск
  /// </summary>
  private string suffix = string.Empty;
  /// <summary>сохранять точные спецификации?</summary>
  private bool isExact;
  /// <summary>сохранять совместимые подписи</summary>
  private bool saveCompatibleSigns;
  /// <summary>Словарик с настройками формы</summary>
  private readonly Dictionary<int, string> settings = new Dictionary<int, string>();
  /// <summary>список типов объектов, по которым выгружаются файлы</summary>
  private List<int> objectTypes = new List<int>();
  /// <summary>Выгружать файлы для указанных типов объектов</summary>
  private bool objectTypesFiltr;
  /// <summary>Создавать ли иерархию папок при сохранении файлов</summary>
  private bool createHierarchy;
  /// <summary>Поддерживать длинные пути</summary>
  private bool longPathSupport;
  private FoldersRecentHolder frh = new FoldersRecentHolder();
  /// <summary>Элементы для сохранения на входе</summary>
  private ISelectedItems itemsToSave;
  /// <summary>
  /// Интерфейсы страниц дополнительных опций редактирования
  /// </summary>
  private List<ISaveToDiskPage> pagesList = new List<ISaveToDiskPage>();
  /// <summary>
  /// Список интерфейсов сохранения дополнительных параметров сохранения, назначаемых в форме через подписчиков-провайдеров
  /// </summary>
  public List<ISaveToDiskProcessor> SaveToDiskProcessorList = new List<ISaveToDiskProcessor>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckedListBox clbRealtions;
  private Button btnCancel;
  private Button btnOK;
  private RadioButton rbAllRelations;
  private RadioButton rbRelatniosType;
  private RadioButton rbXmlFormat;
  private RadioButton rbWmfFormat;
  private Label lbFolder;
  private FolderBrowserDialog fbSave;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private RadioButton rbPdfFormat;
  private GroupBox gbSpec;
  private CheckBox cbExact;
  private TextBox tbCommonPart;
  private Label label1;
  private CheckBox cbHierarchy;
  private GroupBox groupBox3;
  private RadioButton rbAllObjectTypes;
  private RadioButton rbObjectTypes;
  private TreeView tvObjectTypes;
  private CheckBox cbRenameWithAttribute;
  private Label lbAttribute;
  private TabControl tabControl;
  private TabPage tabPage;
  private Panel panel;
  private CheckBox cbSaveCompatibleSigns;
  private Button btnSelectFolder;
  private ComboBox cbFolderPath;
  private CheckBox cbLongPathSupport;
  private ToolTip toolTip;

  /// <summary>в каком формате сохраняем документы Интермех</summary>
  internal ImDocumentFormat Format
  {
    get => this.fmt;
    set => this.fmt = value;
  }

  /// <summary>куда сохраняем файлы</summary>
  public string Folder
  {
    get => this.saveFolder;
    set => this.saveFolder = value;
  }

  /// <summary>список связей, по которым выгружаются файлы</summary>
  public List<IMSRelationType> Relations
  {
    get => this.relations;
    set => this.relations = value;
  }

  /// <summary>
  /// суффикс, который будет добавлен в обозначение всех точных спецификаций
  /// при сохранении их на диск
  /// </summary>
  public string Suffix => !this.isExact ? string.Empty : this.tbCommonPart.Text;

  /// <summary>сохранять точные спецификации?</summary>
  public bool IsExact => this.isExact;

  /// <summary>сохранять совместимые подписи</summary>
  public bool SaveCompatibleSigns => this.saveCompatibleSigns;

  /// <summary>список типов объектов, по которым выгружаются файлы</summary>
  public List<int> ObjectTypes
  {
    get => this.objectTypes;
    set => this.objectTypes = value;
  }

  /// <summary>Выгружать файлы для указанных типов объектов</summary>
  public bool ObjectTypesFiltr => this.objectTypesFiltr;

  /// <summary>Создавать ли иерархию папок при сохранении файлов</summary>
  public bool CreateHierarchy => this.createHierarchy;

  /// <summary>Поддержка длинных путей</summary>
  public bool LongPathSupport => this.longPathSupport;

  /// <summary>Тип атрибута для переименования файла</summary>
  public string SelectedAttributeType { get; set; }

  /// <summary>ID атрибута для переименования файла</summary>
  public int SelectedAttributeID { get; private set; }

  public SaveToDiskForm() => this.InitializeComponent();

  public SaveToDiskForm(ISelectedItems items)
    : this()
  {
    this.itemsToSave = items;
    this.LoadRelations();
    this.LoadObjectTypes();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1872);
    int objectType = (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectType;
    if ((objectType == MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545") || objectType == MetaDataHelper.GetObjectTypeID("cad015b1-306c-11d8-b4e9-00304f19f545")) && ServicesManager.GetService(typeof (ISpecificationSaveService)) is ISpecificationSaveService)
    {
      this.isExact = this.cbExact.Checked = true;
    }
    else
    {
      this.saveCompatibleSigns = this.cbSaveCompatibleSigns.Checked = true;
      this.gbSpec.Visible = false;
      this.groupBox3.Height += this.gbSpec.Height + 6;
      this.tvObjectTypes.ImageList = Statics.IconSrv.ImageList;
    }
  }

  /// <summary>загрузим список существующих связей</summary>
  private void LoadRelations()
  {
    DataTable dataTable = DataHolders.RelationTypesHolder.LoadData(true);
    this.relations = new List<IMSRelationType>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(Convert.ToInt32(dataTable.Rows[index]["F_RELATION_TYPE"]));
      this.relations.Add(relationType);
      this.clbRealtions.Items.Add((object) new MyElement()
      {
        Caption = relationType.Description,
        Value = (object) relationType
      });
    }
  }

  /// <summary>загрузим список существующих типо объектов</summary>
  private void LoadObjectTypes()
  {
    DataTable dataTable = DataHolders.ObjectTypesHolder.LoadData(true, (object) -1);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index]["F_OBJECT_TYPE"]);
      TreeNode parentNode = this.tvObjectTypes.Nodes.Add(MetaDataHelper.GetObjectTypeName(int32));
      parentNode.Tag = (object) int32;
      parentNode.ImageIndex = parentNode.SelectedImageIndex = Statics.IconSrv.IndexOf(4, int32);
      this.FillObjectTypesTree(int32, parentNode);
    }
    this.tvObjectTypes.Sort();
  }

  private void FillObjectTypesTree(int parentObjectTypeID, TreeNode parentNode)
  {
    DataTable dataTable = DataHolders.ObjectTypesHolder.LoadData(false, (object) parentObjectTypeID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index]["F_OBJECT_TYPE"]);
      TreeNode parentNode1 = parentNode.Nodes.Add(MetaDataHelper.GetObjectTypeName(int32));
      parentNode1.Tag = (object) int32;
      parentNode1.ImageIndex = parentNode1.SelectedImageIndex = Statics.IconSrv.IndexOf(4, int32);
      this.FillObjectTypesTree(int32, parentNode1);
    }
  }

  /// <summary>Загружает положение и настройки формы.</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры</param>
  private void SaveToDiskForm_Load(object sender, EventArgs e)
  {
    this.settings.Clear();
    FormStorage.LoadLayout((Control) this, (IDictionary) this.settings);
    this.InitFolderPathRecents();
    this.SetControlsState();
    this.InitCustomPages();
  }

  private void InitFolderPathRecents()
  {
    this.frh.Load();
    this.cbFolderPath.Items.Clear();
    for (int index = 0; index < this.frh.ParamValues.Count; ++index)
      this.cbFolderPath.Items.Add((object) this.frh.ParamValues[index]);
  }

  private void AddToRecentsAndSave(string recentText)
  {
    for (int index = 0; index < this.frh.ParamValues.Count; ++index)
    {
      if (this.frh.ParamValues[index].Equals(recentText, StringComparison.InvariantCultureIgnoreCase))
        return;
    }
    if (this.frh.ParamValues.Count == 0)
      this.frh.ParamValues.Add(recentText);
    else
      this.frh.ParamValues.Insert(0, recentText);
    this.frh.Save();
  }

  private void InitCustomPages()
  {
    this.pagesList.Clear();
    if (!(ServicesManager.GetService(typeof (ISaveToDiskService)) is ISaveToDiskService service))
      return;
    foreach (ISaveToDiskPageProvider provider in service.Providers)
    {
      ISaveToDiskPage saveToDiskPage = provider.InitPage(this.itemsToSave, (ISaveToDiskOptions) this);
      if (saveToDiskPage != null)
        this.pagesList.Add(saveToDiskPage);
    }
    for (int index = 0; index < this.pagesList.Count; ++index)
    {
      this.tabControl.TabPages.Add(this.pagesList[index].PageName, this.pagesList[index].Caption);
      UserControl control = this.pagesList[index].Control;
      this.tabControl.TabPages[this.pagesList[index].PageName].Controls.Add((Control) control);
      control.Dock = DockStyle.Fill;
    }
  }

  /// <summary>Сохраняет положение и настройки формы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры</param>
  private void SaveToDiskForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.GetControlsState();
    FormStorage.SaveLayout((Control) this, (IDictionary) this.settings);
  }

  /// <summary>Собирает у контролов настройки в коллекцию settings</summary>
  protected virtual void GetControlsState()
  {
    if (this.settings.ContainsKey(1))
      this.settings.Remove(1);
    if (this.settings.ContainsKey(2))
      this.settings.Remove(2);
    if (this.settings.ContainsKey(3))
      this.settings.Remove(3);
    this.settings.Add(1, this.cbRenameWithAttribute.CheckState.ToString());
    this.settings.Add(2, this.SelectedAttributeType);
    this.settings.Add(3, this.SelectedAttributeID.ToString());
  }

  /// <summary>
  /// Устанавливает контролам настройки из коллекции settings
  /// </summary>
  protected virtual void SetControlsState()
  {
    if (!this.settings.ContainsKey(1) || this.settings[1] == "Unchecked")
    {
      this.cbRenameWithAttribute.CheckState = CheckState.Unchecked;
      this.lbAttribute.Text = LocalizationHolder.rm.GetString("Attribute_IsNotSelected");
    }
    else
    {
      if (!(this.settings[1] == "Checked"))
        return;
      this.cbRenameWithAttribute.CheckState = CheckState.Checked;
      if (!this.settings.ContainsKey(2))
      {
        this.SelectedAttributeType = string.Empty;
        this.SelectedAttributeID = -1;
      }
      else
      {
        this.SelectedAttributeType = this.settings[2];
        this.SelectedAttributeID = Convert.ToInt32(this.settings[3]);
      }
      this.lbAttribute.Text = this.SelectedAttributeType;
    }
  }

  /// <summary>обновим состояние кнопок</summary>
  private void UpdateControls()
  {
    bool flag = this.saveFolder != string.Empty;
    if (flag)
    {
      for (int index = 0; index < this.pagesList.Count; ++index)
      {
        if (!this.pagesList[index].CommitEnabled)
        {
          flag = false;
          break;
        }
      }
    }
    this.btnOK.Enabled = flag;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    this.AddToRecentsAndSave(this.cbFolderPath.Text);
    if (!this.rbAllRelations.Checked && this.clbRealtions.CheckedItems.Count != this.clbRealtions.Items.Count)
    {
      this.relations = new List<IMSRelationType>(this.clbRealtions.CheckedItems.Count);
      foreach (object checkedItem in this.clbRealtions.CheckedItems)
        this.relations.Add((checkedItem as MyElement).Value as IMSRelationType);
    }
    if (!this.rbAllObjectTypes.Checked)
    {
      this.objectTypes.Clear();
      foreach (TreeNode node in this.tvObjectTypes.Nodes)
        this.FillFiltr(node);
    }
    this.SaveToDiskProcessorList.Clear();
    for (int index = 0; index < this.pagesList.Count; ++index)
    {
      ISaveToDiskProcessor saveToDiskProcessor = this.pagesList[index].Commit();
      if (saveToDiskProcessor != null)
        this.SaveToDiskProcessorList.Add(saveToDiskProcessor);
    }
  }

  /// <summary>Заполняем список типов объектов</summary>
  private void FillFiltr(TreeNode curNode)
  {
    if (curNode.Checked)
      this.objectTypes.Add(Convert.ToInt32(curNode.Tag));
    foreach (TreeNode node in curNode.Nodes)
      this.FillFiltr(node);
  }

  private void rbWmfFormat_CheckedChanged(object sender, EventArgs e)
  {
    this.fmt = ImDocumentFormat.WmfFormat;
  }

  private void rbXmlFormat_CheckedChanged(object sender, EventArgs e)
  {
    this.fmt = ImDocumentFormat.XmlFormat;
  }

  private void rbRelatniosType_CheckedChanged(object sender, EventArgs e)
  {
    this.clbRealtions.Enabled = this.rbRelatniosType.Checked;
  }

  private void rbAllRelations_CheckedChanged(object sender, EventArgs e)
  {
    this.clbRealtions.Enabled = !this.rbAllRelations.Checked;
  }

  private void rbPdfFormat_CheckedChanged(object sender, EventArgs e)
  {
    this.fmt = ImDocumentFormat.PdfFormat;
  }

  private void cbExact_CheckedChanged(object sender, EventArgs e)
  {
    this.isExact = this.cbExact.Checked;
    this.tbCommonPart.Enabled = this.isExact;
  }

  /// <summary>создавать ли иерархию папок</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbHierarchy_CheckedChanged(object sender, EventArgs e)
  {
    this.createHierarchy = this.cbHierarchy.Checked;
  }

  /// <summary>выбрали выгрузку всех типов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void rbAllObjectTypes_CheckedChanged(object sender, EventArgs e)
  {
    this.rbObjectTypes.Checked = this.tvObjectTypes.Enabled = this.objectTypesFiltr = !this.rbAllObjectTypes.Checked;
  }

  /// <summary>выбрали  выгрузку по типам объектов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void rbObjectTypes_CheckedChanged(object sender, EventArgs e)
  {
    this.rbAllObjectTypes.Checked = !this.rbObjectTypes.Checked;
    this.tvObjectTypes.Enabled = this.objectTypesFiltr = this.rbObjectTypes.Checked;
  }

  /// <summary>Выбрали переименование файла значением атрибута</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbRenameWithAttribute_Click(object sender, EventArgs e)
  {
    if (this.cbRenameWithAttribute.CheckState == CheckState.Checked)
    {
      AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
      if (attributesSelectDlg.ShowDialog() == DialogResult.OK && attributesSelectDlg.SelectedAttributesID.Count > 0)
      {
        this.SelectedAttributeID = attributesSelectDlg.SelectedAttributesID[0];
        this.SelectedAttributeType = MetaDataHelper.GetAttributeTypeName(this.SelectedAttributeID);
        this.lbAttribute.Text = this.SelectedAttributeType.ToString();
      }
      else
      {
        this.cbRenameWithAttribute.CheckState = CheckState.Unchecked;
        this.lbAttribute.Text = LocalizationHolder.rm.GetString("Attribute_IsNotSelected");
      }
    }
    else
      this.ResetAttributeForRenaming();
  }

  private void ResetAttributeForRenaming()
  {
    this.lbAttribute.Text = LocalizationHolder.rm.GetString("Attribute_IsNotSelected");
    this.SelectedAttributeType = string.Empty;
    this.SelectedAttributeID = -1;
  }

  public string OptionSaveFolder => this.cbFolderPath.Text;

  private void btnCancel_Click(object sender, EventArgs e)
  {
    for (int index = 0; index < this.pagesList.Count; ++index)
      this.pagesList[index].Cancel();
  }

  /// <summary>
  /// Обработка чекбокса Сохранение совместимых подписей.
  /// При его включении возможность переименовать при помощи атрибута должна быть отключена.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void cbSaveCompatibleSigns_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbSaveCompatibleSigns.CheckState == CheckState.Checked)
    {
      this.ResetAttributeForRenaming();
      this.cbRenameWithAttribute.CheckState = CheckState.Unchecked;
      this.cbRenameWithAttribute.Enabled = false;
      this.lbAttribute.Enabled = false;
    }
    else
    {
      this.cbRenameWithAttribute.Enabled = true;
      this.lbAttribute.Enabled = true;
    }
    this.saveCompatibleSigns = this.cbSaveCompatibleSigns.CheckState == CheckState.Checked;
  }

  private void btnSelectFolder_Click(object sender, EventArgs e)
  {
    if (this.fbSave.ShowDialog() == DialogResult.OK)
      this.cbFolderPath.Text = this.saveFolder = this.fbSave.SelectedPath;
    this.UpdateControls();
  }

  private void cbFolderPath_SelectedValueChanged(object sender, EventArgs e)
  {
    this.saveFolder = this.cbFolderPath.Text;
    this.UpdateControls();
  }

  private void cbLongPathSupport_CheckedChanged(object sender, EventArgs e)
  {
    this.longPathSupport = this.cbLongPathSupport.Checked;
    this.UpdateControls();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SaveToDiskForm));
    this.clbRealtions = new CheckedListBox();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.rbAllRelations = new RadioButton();
    this.rbRelatniosType = new RadioButton();
    this.rbXmlFormat = new RadioButton();
    this.rbWmfFormat = new RadioButton();
    this.lbFolder = new Label();
    this.fbSave = new FolderBrowserDialog();
    this.groupBox1 = new GroupBox();
    this.groupBox2 = new GroupBox();
    this.rbPdfFormat = new RadioButton();
    this.gbSpec = new GroupBox();
    this.label1 = new Label();
    this.cbExact = new CheckBox();
    this.tbCommonPart = new TextBox();
    this.cbHierarchy = new CheckBox();
    this.groupBox3 = new GroupBox();
    this.tvObjectTypes = new TreeView();
    this.rbObjectTypes = new RadioButton();
    this.rbAllObjectTypes = new RadioButton();
    this.cbRenameWithAttribute = new CheckBox();
    this.lbAttribute = new Label();
    this.tabControl = new TabControl();
    this.tabPage = new TabPage();
    this.panel = new Panel();
    this.cbLongPathSupport = new CheckBox();
    this.btnSelectFolder = new Button();
    this.cbFolderPath = new ComboBox();
    this.cbSaveCompatibleSigns = new CheckBox();
    this.toolTip = new ToolTip();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.gbSpec.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.tabControl.SuspendLayout();
    this.tabPage.SuspendLayout();
    this.panel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.clbRealtions, "clbRealtions");
    this.clbRealtions.CheckOnClick = true;
    this.clbRealtions.FormattingEnabled = true;
    this.clbRealtions.Name = "clbRealtions";
    this.clbRealtions.Sorted = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.rbAllRelations, "rbAllRelations");
    this.rbAllRelations.Checked = true;
    this.rbAllRelations.Name = "rbAllRelations";
    this.rbAllRelations.TabStop = true;
    this.rbAllRelations.UseVisualStyleBackColor = true;
    this.rbAllRelations.CheckedChanged += new EventHandler(this.rbAllRelations_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbRelatniosType, "rbRelatniosType");
    this.rbRelatniosType.Name = "rbRelatniosType";
    this.rbRelatniosType.UseVisualStyleBackColor = true;
    this.rbRelatniosType.CheckedChanged += new EventHandler(this.rbRelatniosType_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbXmlFormat, "rbXmlFormat");
    this.rbXmlFormat.Checked = true;
    this.rbXmlFormat.Name = "rbXmlFormat";
    this.rbXmlFormat.TabStop = true;
    this.rbXmlFormat.UseVisualStyleBackColor = true;
    this.rbXmlFormat.CheckedChanged += new EventHandler(this.rbXmlFormat_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbWmfFormat, "rbWmfFormat");
    this.rbWmfFormat.Name = "rbWmfFormat";
    this.rbWmfFormat.UseVisualStyleBackColor = true;
    this.rbWmfFormat.CheckedChanged += new EventHandler(this.rbWmfFormat_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.lbFolder, "lbFolder");
    this.lbFolder.Name = "lbFolder";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.clbRealtions);
    this.groupBox1.Controls.Add((Control) this.rbAllRelations);
    this.groupBox1.Controls.Add((Control) this.rbRelatniosType);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Controls.Add((Control) this.rbPdfFormat);
    this.groupBox2.Controls.Add((Control) this.rbWmfFormat);
    this.groupBox2.Controls.Add((Control) this.rbXmlFormat);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbPdfFormat, "rbPdfFormat");
    this.rbPdfFormat.Name = "rbPdfFormat";
    this.rbPdfFormat.UseVisualStyleBackColor = true;
    this.rbPdfFormat.CheckedChanged += new EventHandler(this.rbPdfFormat_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.gbSpec, "gbSpec");
    this.gbSpec.Controls.Add((Control) this.label1);
    this.gbSpec.Controls.Add((Control) this.cbExact);
    this.gbSpec.Controls.Add((Control) this.tbCommonPart);
    this.gbSpec.Name = "gbSpec";
    this.gbSpec.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.cbExact, "cbExact");
    this.cbExact.Checked = true;
    this.cbExact.CheckState = CheckState.Checked;
    this.cbExact.Name = "cbExact";
    this.cbExact.UseVisualStyleBackColor = true;
    this.cbExact.CheckedChanged += new EventHandler(this.cbExact_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.tbCommonPart, "tbCommonPart");
    this.tbCommonPart.Name = "tbCommonPart";
    componentResourceManager.ApplyResources((object) this.cbHierarchy, "cbHierarchy");
    this.cbHierarchy.Name = "cbHierarchy";
    this.cbHierarchy.UseVisualStyleBackColor = true;
    this.cbHierarchy.CheckedChanged += new EventHandler(this.cbHierarchy_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Controls.Add((Control) this.tvObjectTypes);
    this.groupBox3.Controls.Add((Control) this.rbObjectTypes);
    this.groupBox3.Controls.Add((Control) this.rbAllObjectTypes);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tvObjectTypes, "tvObjectTypes");
    this.tvObjectTypes.CheckBoxes = true;
    this.tvObjectTypes.FullRowSelect = true;
    this.tvObjectTypes.Name = "tvObjectTypes";
    componentResourceManager.ApplyResources((object) this.rbObjectTypes, "rbObjectTypes");
    this.rbObjectTypes.Name = "rbObjectTypes";
    this.rbObjectTypes.UseVisualStyleBackColor = true;
    this.rbObjectTypes.CheckedChanged += new EventHandler(this.rbObjectTypes_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAllObjectTypes, "rbAllObjectTypes");
    this.rbAllObjectTypes.Checked = true;
    this.rbAllObjectTypes.Name = "rbAllObjectTypes";
    this.rbAllObjectTypes.TabStop = true;
    this.rbAllObjectTypes.UseVisualStyleBackColor = true;
    this.rbAllObjectTypes.CheckedChanged += new EventHandler(this.rbAllObjectTypes_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbRenameWithAttribute, "cbRenameWithAttribute");
    this.cbRenameWithAttribute.Name = "cbRenameWithAttribute";
    this.cbRenameWithAttribute.UseVisualStyleBackColor = true;
    this.cbRenameWithAttribute.Click += new EventHandler(this.cbRenameWithAttribute_Click);
    componentResourceManager.ApplyResources((object) this.lbAttribute, "lbAttribute");
    this.lbAttribute.Name = "lbAttribute";
    componentResourceManager.ApplyResources((object) this.tabControl, "tabControl");
    this.tabControl.Controls.Add((Control) this.tabPage);
    this.tabControl.Name = "tabControl";
    this.tabControl.SelectedIndex = 0;
    this.tabPage.Controls.Add((Control) this.panel);
    componentResourceManager.ApplyResources((object) this.tabPage, "tabPage");
    this.tabPage.Name = "tabPage";
    this.tabPage.UseVisualStyleBackColor = true;
    this.panel.Controls.Add((Control) this.cbLongPathSupport);
    this.panel.Controls.Add((Control) this.btnSelectFolder);
    this.panel.Controls.Add((Control) this.cbFolderPath);
    this.panel.Controls.Add((Control) this.cbSaveCompatibleSigns);
    this.panel.Controls.Add((Control) this.lbFolder);
    this.panel.Controls.Add((Control) this.lbAttribute);
    this.panel.Controls.Add((Control) this.cbRenameWithAttribute);
    this.panel.Controls.Add((Control) this.groupBox1);
    this.panel.Controls.Add((Control) this.groupBox3);
    this.panel.Controls.Add((Control) this.groupBox2);
    this.panel.Controls.Add((Control) this.cbHierarchy);
    this.panel.Controls.Add((Control) this.gbSpec);
    componentResourceManager.ApplyResources((object) this.panel, "panel");
    this.panel.Name = "panel";
    componentResourceManager.ApplyResources((object) this.cbLongPathSupport, "cbLongPathSupport");
    this.cbLongPathSupport.Name = "cbLongPathSupport";
    this.toolTip.SetToolTip((Control) this.cbLongPathSupport, componentResourceManager.GetString("cbLongPathSupport.ToolTip"));
    this.cbLongPathSupport.UseVisualStyleBackColor = true;
    this.cbLongPathSupport.CheckedChanged += new EventHandler(this.cbLongPathSupport_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnSelectFolder, "btnSelectFolder");
    this.btnSelectFolder.Name = "btnSelectFolder";
    this.btnSelectFolder.UseVisualStyleBackColor = true;
    this.btnSelectFolder.Click += new EventHandler(this.btnSelectFolder_Click);
    componentResourceManager.ApplyResources((object) this.cbFolderPath, "cbFolderPath");
    this.cbFolderPath.FormattingEnabled = true;
    this.cbFolderPath.Name = "cbFolderPath";
    this.cbFolderPath.SelectedValueChanged += new EventHandler(this.cbFolderPath_SelectedValueChanged);
    this.cbFolderPath.TextChanged += new EventHandler(this.cbFolderPath_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this.cbSaveCompatibleSigns, "cbSaveCompatibleSigns");
    this.cbSaveCompatibleSigns.Name = "cbSaveCompatibleSigns";
    this.cbSaveCompatibleSigns.UseVisualStyleBackColor = true;
    this.cbSaveCompatibleSigns.CheckedChanged += new EventHandler(this.cbSaveCompatibleSigns_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.tabControl);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.btnCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SaveToDiskForm);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.SaveToDiskForm_FormClosed);
    this.Load += new EventHandler(this.SaveToDiskForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.gbSpec.ResumeLayout(false);
    this.gbSpec.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.tabControl.ResumeLayout(false);
    this.tabPage.ResumeLayout(false);
    this.panel.ResumeLayout(false);
    this.panel.PerformLayout();
    this.ResumeLayout(false);
  }
}
