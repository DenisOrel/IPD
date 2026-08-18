
// Type: Intermech.Navigator.Snapshots.SnapshotMasterForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Navigator.Snapshots;

/// <summary>Форма мастера работы с итерациями</summary>
public class SnapshotMasterForm : Form
{
  /// <summary>Модель для работы с данными</summary>
  private readonly SnapshotMasterModel model;
  /// <summary>ID пользователя, вызвавшего форму</summary>
  private readonly long userID;
  /// <summary>Текущие выделеннные объекты</summary>
  private List<long> currentСheckedObject;
  /// <summary>
  /// Флаг, определяющий выделен ли узел дерева автоматически или пользователь его дергает
  /// </summary>
  private bool isAutoCheck;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Panel panel2;
  private Panel panel3;
  private TextBox tbSnapshotName;
  private Label lblSnapshotName;
  private Button btnCancel;
  private Button btnSave;
  private ComboBox cbSnapshotChoise;
  private Button btnFlagSnapshotsObjects;
  private Button btnFlagUsersCheckedOutObjects;
  private Button btnDeflagAllObjects;
  private Button btnFlagAllObjects;
  private Label label1;
  private NavigatorTreeView ntvObjectVersionComposition;

  /// <summary>
  /// Переменная для определения места вызова команд "Создать итерацию" и "Сохранить в итерацию"
  /// </summary>
  /// <value>
  /// 	<c>true</c> если форма мастера открыта; иначе, <c>false</c>.
  /// </value>
  public static bool IsSnapshotCompositionShown { get; set; }

  public SnapshotMasterForm() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  /// <param name="typedObject">Информация об объекте, для которого создаём итерацию</param>
  /// <param name="commandName"> Команда, вызывающая конструктор</param>
  public SnapshotMasterForm(IDBTypedObjectID typedObject, string commandName)
    : this()
  {
    if (ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service)
      this.Icon = service.GetIcon(4, typedObject.ObjectType);
    this.model = new SnapshotMasterModel(typedObject);
    this.userID = this.model.UserID;
    this.ntvObjectVersionComposition.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Utils.GetNavigatorColumns);
    this.ntvObjectVersionComposition.SetColumns(Utils.CaptionAndStatesesColumns(NodeColumnSortOrder.Ascending));
    this.LoadSettings();
    this.ntvObjectVersionComposition.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.ntvObjectVersionComposition.AllowCheckParentWithoutChildren = true;
    this.ntvObjectVersionComposition.DisableColumnsSorting = true;
    this.LoadObjectVersionComposition();
    this.InitSnapshotChoiceCombobox(commandName);
    this.UpdateSnapshotNameTextbox(this.cbSnapshotChoise.SelectedItem.ToString());
    this.model.OnChanged += new EventHandler(this.model_OnChanged);
  }

  /// <summary>Обработка изменения модели.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void model_OnChanged(object sender, EventArgs e)
  {
    this.currentСheckedObject = this.GetTreeViewCheckedObjects();
    List<long> list = this.currentСheckedObject.Select<long, long>(new Func<long, long>(Math.Abs)).ToList<long>();
    this.LoadObjectVersionComposition();
    SnapshotMasterForm.CheckNodes(this.ntvObjectVersionComposition.RootNode, CheckState.Unchecked);
    this.FlagObjects(this.ntvObjectVersionComposition.RootNode, list);
  }

  /// <summary>Инициализирует комбобокс выбора итерации</summary>
  /// <param name="command">Команда, по которой была вызвана форма</param>
  private void InitSnapshotChoiceCombobox(string command)
  {
    this.cbSnapshotChoise.Items.Add((object) LocalizationHolder.rm.GetString("Client.Core_1621"));
    if (this.model.ObjectSnapshotsInfo.Count != 0)
      this.cbSnapshotChoise.Items.AddRange((object[]) this.model.ObjectSnapshotsInfo.ToArray());
    SnapshotInfo displayedSnapshot = this.model.DisplayedSnapshot;
    if (command == "CreateSnapshot" || displayedSnapshot == null)
    {
      this.cbSnapshotChoise.SelectedIndex = this.cbSnapshotChoise.Items.IndexOf((object) LocalizationHolder.rm.GetString("Client.Core_1621"));
      this.btnFlagSnapshotsObjects.Enabled = false;
    }
    else
      this.cbSnapshotChoise.SelectedIndex = this.cbSnapshotChoise.Items.IndexOf((object) displayedSnapshot);
  }

  /// <summary>Смена выделенного элемента в комбобоксе</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void cbSnapshotChoise_SelectedIndexChanged(object sender, EventArgs e)
  {
    string snapshotName = this.cbSnapshotChoise.SelectedItem.ToString();
    this.UpdateSnapshotNameTextbox(snapshotName);
    if (!snapshotName.Equals(LocalizationHolder.rm.GetString("Client.Core_1621")))
    {
      this.model.DisplayedSnapshot = (SnapshotInfo) this.cbSnapshotChoise.SelectedItem;
      this.btnFlagSnapshotsObjects.Enabled = true;
    }
    else
    {
      this.model.DisplayedSnapshot = (SnapshotInfo) null;
      this.btnFlagSnapshotsObjects.Enabled = false;
    }
    SnapshotMasterForm.CheckNodes(this.ntvObjectVersionComposition.RootNode, CheckState.Unchecked);
    this.FlagObjects(this.ntvObjectVersionComposition.RootNode, this.model.DisplayedSnapshotComposition);
  }

  /// <summary>
  /// Изменение текста в текстбоксе с наименованием итерации
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void tbSnapshotName_TextChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Обновляет текстбокс с именем итерации</summary>
  /// <param name="snapshotName">Наименование итерации</param>
  private void UpdateSnapshotNameTextbox(string snapshotName)
  {
    if (snapshotName.Equals(LocalizationHolder.rm.GetString("Client.Core_1621")))
    {
      this.tbSnapshotName.Enabled = true;
      this.tbSnapshotName.Text = string.Empty;
    }
    else
    {
      this.tbSnapshotName.Enabled = false;
      this.tbSnapshotName.Text = snapshotName;
    }
  }

  /// <summary>
  /// Загружает дерево объектов в соответствии с выбранной итерацией
  /// </summary>
  private void LoadObjectVersionComposition()
  {
    if (this.model.ObjectID == 0L)
      return;
    this.Refresh();
    try
    {
      this.ntvObjectVersionComposition.BeginUpdate();
      try
      {
        this.ntvObjectVersionComposition.Build((IDescriptor) new Descriptor(this.model.ObjectID));
      }
      finally
      {
        this.ntvObjectVersionComposition.EndUpdate();
      }
    }
    finally
    {
      this.ntvObjectVersionComposition.AutoScrollOnExpand = true;
    }
    NavigatorTreeNode rootNode = this.ntvObjectVersionComposition.RootNode;
    rootNode.CheckState = CheckState.Checked;
    rootNode.Expanded = true;
  }

  /// <summary>Событие на изменение состояния узла</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:Intermech.Navigator.Controls.NodeEventArgs" /> instance containing the event data.</param>
  private void ntvObjectVersionComposition_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (e.Node.CheckState == CheckState.Checked)
      e.Node.Expanded = true;
    if (e.Node.Equals((object) this.ntvObjectVersionComposition.RootNode))
    {
      if (e.Node.CheckState != CheckState.Unchecked)
        return;
      this.isAutoCheck = true;
      e.Node.CheckState = CheckState.Checked;
    }
    if (e.Node.CheckState == CheckState.Unchecked)
    {
      SnapshotMasterForm.CheckNodes(e.Node, CheckState.Unchecked);
      e.Node.Expanded = false;
    }
    else
    {
      if (!this.isAutoCheck)
        SnapshotMasterForm.CheckNodes(e.Node, CheckState.Checked);
      if (e.Node.Parent != null && e.Node.Parent.Level != 0)
      {
        this.isAutoCheck = true;
        if (e.Node.Parent.CheckState == CheckState.Unchecked)
          e.Node.Parent.CheckState = CheckState.Checked;
        this.isAutoCheck = false;
      }
    }
    this.isAutoCheck = false;
  }

  /// <summary>Отметить или снять отметки с дочерних узлов дерева</summary>
  /// <param name="treeNode">Узел дерева</param>
  /// <param name="checkState">Состояние узла</param>
  private static void CheckNodes(NavigatorTreeNode treeNode, CheckState checkState)
  {
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) treeNode.Children)
    {
      child.CheckState = checkState;
      SnapshotMasterForm.CheckNodes(child, checkState);
    }
  }

  /// <summary>
  /// Смена выделенного узла.
  /// Используется для выделения корневого объекта после добавления информационного столбца в дерево.
  /// И сохранения ИД выделенного узла
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void ntvObjectVersionComposition_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (this.ntvObjectVersionComposition.SelectedItems.Count == 0)
      return;
    if (this.ntvObjectVersionComposition.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      this.model.AbsCurrentObjectID = Math.Abs(itemData.ObjectID);
    NavigatorTreeNode rootNode = this.ntvObjectVersionComposition.RootNode;
    if (rootNode == null || rootNode.CheckState != CheckState.Unchecked)
      return;
    rootNode.CheckState = CheckState.Checked;
  }

  /// <summary>Hажатие кнопки "Выделить объекты выбранной итерации"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnFlagSnapshotsObjects_Click(object sender, EventArgs e)
  {
    this.FlagObjects(this.ntvObjectVersionComposition.RootNode, this.model.DisplayedSnapshotComposition);
  }

  /// <summary>Выделяет объекты, входящие в состав итерации</summary>
  /// <param name="node">Узел дерева</param>
  private void FlagObjects(NavigatorTreeNode node, List<long> objectsToFlag)
  {
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (!(child.NodeID is NodeID nodeId))
        break;
      if (objectsToFlag.Contains(Math.Abs(nodeId.ObjectID)))
      {
        this.isAutoCheck = true;
        child.CheckState = CheckState.Checked;
        this.FlagObjects(child, objectsToFlag);
      }
    }
  }

  /// <summary>Нажатие кнопки "Снять отметки со всех объектов"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnDeflagAllObjects_Click(object sender, EventArgs e)
  {
    SnapshotMasterForm.CheckNodes(this.ntvObjectVersionComposition.RootNode, CheckState.Unchecked);
  }

  /// <summary>Нажатие кнопки "Установить отметки на всех объектах"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnFlagAllObjects_Click(object sender, EventArgs e)
  {
    SnapshotMasterForm.CheckNodes(this.ntvObjectVersionComposition.RootNode, CheckState.Checked);
  }

  /// <summary>Нажатие кнопки "Объекты, взятые на изменение мной"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnFlagUsersCheckedOutObjects_Click(object sender, EventArgs e)
  {
    this.FlagCheckedOutObjects(this.ntvObjectVersionComposition.RootNode);
  }

  /// <summary>
  /// Отмечает в дереве объекты, взятые на изменение пользователем
  /// </summary>
  /// <param name="node">Узел дерева</param>
  private void FlagCheckedOutObjects(NavigatorTreeNode node)
  {
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (!(child.NodeID is NodeID nodeId))
        break;
      if (nodeId.CheckedOutBy == this.userID)
      {
        this.isAutoCheck = true;
        child.CheckState = CheckState.Checked;
        this.FlagCheckedOutObjects(child);
      }
    }
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (string.IsNullOrWhiteSpace(this.tbSnapshotName.Text))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1622"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      this.model.SaveDisplayedSnapshot(this.GetTreeViewCheckedObjects(), this.model.DisplayedSnapshot, this.tbSnapshotName.Text);
      if (this.cbSnapshotChoise.SelectedItem.ToString().Equals(LocalizationHolder.rm.GetString("Client.Core_1621")))
      {
        this.cbSnapshotChoise.Items.Add((object) this.model.DisplayedSnapshot);
        this.cbSnapshotChoise.SelectedIndex = this.cbSnapshotChoise.Items.IndexOf((object) this.model.DisplayedSnapshot);
      }
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("SnapshotsChanged", this.model.ID));
      this.Close();
    }
  }

  /// <summary>Получает список выделенных в дереве объектов</summary>
  /// <returns>Список выделенных в дереве объектов</returns>
  private List<long> GetTreeViewCheckedObjects()
  {
    List<long> viewCheckedObjects = new List<long>();
    viewCheckedObjects.Add(this.model.ObjectID);
    foreach (long nodeCheckedObject in SnapshotMasterForm.GetNodeCheckedObjects(this.ntvObjectVersionComposition.RootNode))
    {
      if (!viewCheckedObjects.Contains(nodeCheckedObject))
        viewCheckedObjects.Add(nodeCheckedObject);
    }
    return viewCheckedObjects;
  }

  /// <summary>Получает список выделенных объектов внутри узла</summary>
  /// <param name="node">The node.</param>
  /// <returns></returns>
  private static List<long> GetNodeCheckedObjects(NavigatorTreeNode node)
  {
    List<long> nodeCheckedObjects = new List<long>();
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (child.NodeID is NodeID nodeId && child.CheckState == CheckState.Checked && !nodeCheckedObjects.Contains(nodeId.ObjectID))
      {
        nodeCheckedObjects.Add(nodeId.ObjectID);
        foreach (long nodeCheckedObject in SnapshotMasterForm.GetNodeCheckedObjects(child))
        {
          if (!nodeCheckedObjects.Contains(nodeCheckedObject))
            nodeCheckedObjects.Add(nodeCheckedObject);
        }
      }
    }
    return nodeCheckedObjects;
  }

  /// <summary>Нажатие кнопки "Отмена"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnCancel_Click(object sender, EventArgs e) => this.Close();

  /// <summary>Загрузка настроек.</summary>
  private void LoadSettings()
  {
    string name = this.GetType().ToString();
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration config = service.Open(name);
    if (config == null)
      return;
    this.LoadTreeViewSettings(config);
  }

  /// <summary>Загрузка настроек дерева состава итерации.</summary>
  /// <param name="config">The config.</param>
  private void LoadTreeViewSettings(IConfiguration config)
  {
    if (!config.HasProperty(this.ntvObjectVersionComposition.Name + "_CollumnsLayout"))
      return;
    this.SetCollumnsState(config.GetProperty(this.ntvObjectVersionComposition.Name + "_CollumnsLayout"));
  }

  /// <summary>Установка состояния колонок дерева.</summary>
  /// <param name="columnsState">State of the columns.</param>
  private void SetCollumnsState(string columnsState)
  {
    XmlDocument xmlDocument = new XmlDocument();
    try
    {
      xmlDocument.LoadXml(columnsState);
    }
    catch (XmlException ex)
    {
    }
    XmlNode xmlNode1 = xmlDocument.SelectSingleNode("Settings");
    if (xmlNode1 == null)
      return;
    XmlNode xmlNode2 = xmlNode1.SelectSingleNode("Columns");
    NodeColumnCollection columnCollection = (NodeColumnCollection) null;
    if (xmlNode2 != null)
    {
      columnCollection = new NodeColumnCollection();
      columnCollection.LoadData(xmlNode2);
    }
    if (columnCollection == null || columnCollection.Count <= 0)
      return;
    this.ntvObjectVersionComposition.TreeColumns = columnCollection;
  }

  /// <summary>Сохранение настроек.</summary>
  private void SaveSettings()
  {
    string name = typeof (SnapshotMasterForm).ToString();
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration config = service.Create(name);
    if (config == null)
      return;
    this.SaveTreeViewSettings(config);
  }

  /// <summary>Сохраниет настройки дерева состава итерации.</summary>
  /// <param name="config">The config.</param>
  private void SaveTreeViewSettings(IConfiguration config)
  {
    string columnsState = this.GetColumnsState();
    config.SetProperty(this.ntvObjectVersionComposition.Name + "_CollumnsLayout", columnsState);
  }

  /// <summary>Получает состояние колонок дерева состава итерации.</summary>
  /// <returns></returns>
  private string GetColumnsState()
  {
    if (this.ntvObjectVersionComposition == null)
      return string.Empty;
    NodeColumnCollection treeColumns = this.ntvObjectVersionComposition.TreeColumns;
    if (treeColumns != null)
    {
      if (treeColumns.Count != 0)
      {
        try
        {
          XmlDocument xmlDocument = new XmlDocument();
          XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Settings");
          xmlDocument.AppendChild(element1);
          XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Columns");
          treeColumns.SaveData(element2);
          element1.AppendChild(element2);
          using (TextWriter w1 = (TextWriter) new StringWriter())
          {
            XmlWriter w2 = (XmlWriter) new XmlTextWriter(w1);
            w2.WriteStartDocument();
            xmlDocument.WriteTo(w2);
            w2.WriteEndDocument();
            w2.Flush();
            w2.Close();
            return w1.ToString();
          }
        }
        catch
        {
          return string.Empty;
        }
      }
    }
    return string.Empty;
  }

  private void SnapshotMasterForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SnapshotMasterForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveSettings();
    FormStorage.SaveLayout((Control) this);
  }

  private void SnapshotMasterForm_Activated(object sender, EventArgs e)
  {
    SnapshotMasterForm.IsSnapshotCompositionShown = true;
  }

  private void SnapshotMasterForm_Deactivate(object sender, EventArgs e)
  {
    SnapshotMasterForm.IsSnapshotCompositionShown = false;
  }

  /// <summary>Форма закрывается.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> instance containing the event data.</param>
  private void SnapshotMasterForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.model.Unsubcribe();
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
    this.panel1 = new Panel();
    this.tbSnapshotName = new TextBox();
    this.lblSnapshotName = new Label();
    this.panel2 = new Panel();
    this.label1 = new Label();
    this.btnCancel = new Button();
    this.btnSave = new Button();
    this.cbSnapshotChoise = new ComboBox();
    this.panel3 = new Panel();
    this.btnFlagSnapshotsObjects = new Button();
    this.btnFlagUsersCheckedOutObjects = new Button();
    this.btnDeflagAllObjects = new Button();
    this.btnFlagAllObjects = new Button();
    this.ntvObjectVersionComposition = new NavigatorTreeView();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.ntvObjectVersionComposition.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.tbSnapshotName);
    this.panel1.Controls.Add((Control) this.lblSnapshotName);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(761, 53);
    this.panel1.TabIndex = 1;
    this.tbSnapshotName.Location = new Point(152, 16 /*0x10*/);
    this.tbSnapshotName.Name = "tbSnapshotName";
    this.tbSnapshotName.Size = new Size(314, 20);
    this.tbSnapshotName.TabIndex = 2;
    this.tbSnapshotName.TextChanged += new EventHandler(this.tbSnapshotName_TextChanged);
    this.lblSnapshotName.AutoSize = true;
    this.lblSnapshotName.Location = new Point(13, 19);
    this.lblSnapshotName.Name = "lblSnapshotName";
    this.lblSnapshotName.Size = new Size(136, 13);
    this.lblSnapshotName.TabIndex = 1;
    this.lblSnapshotName.Text = "Наименование итерации:";
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.btnCancel);
    this.panel2.Controls.Add((Control) this.btnSave);
    this.panel2.Controls.Add((Control) this.cbSnapshotChoise);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(0, 373);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(761, 75);
    this.panel2.TabIndex = 2;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(16 /*0x10*/, 14);
    this.label1.Name = "label1";
    this.label1.Size = new Size(117, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Выбранная итерация:";
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(613, 26);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size((int) sbyte.MaxValue, 28);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnSave.Location = new Point(476, 26);
    this.btnSave.Name = "btnSave";
    this.btnSave.Size = new Size(126, 28);
    this.btnSave.TabIndex = 1;
    this.btnSave.Text = "Сохранить";
    this.btnSave.UseVisualStyleBackColor = true;
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    this.cbSnapshotChoise.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbSnapshotChoise.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbSnapshotChoise.FormattingEnabled = true;
    this.cbSnapshotChoise.Location = new Point(16 /*0x10*/, 33);
    this.cbSnapshotChoise.Name = "cbSnapshotChoise";
    this.cbSnapshotChoise.Size = new Size(436, 21);
    this.cbSnapshotChoise.TabIndex = 0;
    this.cbSnapshotChoise.SelectedIndexChanged += new EventHandler(this.cbSnapshotChoise_SelectedIndexChanged);
    this.panel3.Controls.Add((Control) this.btnFlagSnapshotsObjects);
    this.panel3.Controls.Add((Control) this.btnFlagUsersCheckedOutObjects);
    this.panel3.Controls.Add((Control) this.btnDeflagAllObjects);
    this.panel3.Controls.Add((Control) this.btnFlagAllObjects);
    this.panel3.Dock = DockStyle.Right;
    this.panel3.Location = new Point(594, 53);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(167, 320);
    this.panel3.TabIndex = 3;
    this.btnFlagSnapshotsObjects.Anchor = AnchorStyles.Top;
    this.btnFlagSnapshotsObjects.Location = new Point(19, 84);
    this.btnFlagSnapshotsObjects.Name = "btnFlagSnapshotsObjects";
    this.btnFlagSnapshotsObjects.Size = new Size((int) sbyte.MaxValue, 53);
    this.btnFlagSnapshotsObjects.TabIndex = 3;
    this.btnFlagSnapshotsObjects.Text = "Выделить объекты выбранной итерации";
    this.btnFlagSnapshotsObjects.UseVisualStyleBackColor = true;
    this.btnFlagSnapshotsObjects.Click += new EventHandler(this.btnFlagSnapshotsObjects_Click);
    this.btnFlagUsersCheckedOutObjects.Anchor = AnchorStyles.Top;
    this.btnFlagUsersCheckedOutObjects.Location = new Point(19, 152);
    this.btnFlagUsersCheckedOutObjects.Name = "btnFlagUsersCheckedOutObjects";
    this.btnFlagUsersCheckedOutObjects.Size = new Size((int) sbyte.MaxValue, 53);
    this.btnFlagUsersCheckedOutObjects.TabIndex = 2;
    this.btnFlagUsersCheckedOutObjects.Text = "Выделить объекты, взятые на изменение мной";
    this.btnFlagUsersCheckedOutObjects.UseVisualStyleBackColor = true;
    this.btnFlagUsersCheckedOutObjects.Click += new EventHandler(this.btnFlagUsersCheckedOutObjects_Click);
    this.btnDeflagAllObjects.Anchor = AnchorStyles.Top;
    this.btnDeflagAllObjects.Location = new Point(19, 221);
    this.btnDeflagAllObjects.Name = "btnDeflagAllObjects";
    this.btnDeflagAllObjects.Size = new Size((int) sbyte.MaxValue, 53);
    this.btnDeflagAllObjects.TabIndex = 1;
    this.btnDeflagAllObjects.Text = "Снять отметки со всех объектов";
    this.btnDeflagAllObjects.UseVisualStyleBackColor = true;
    this.btnDeflagAllObjects.Click += new EventHandler(this.btnDeflagAllObjects_Click);
    this.btnFlagAllObjects.Anchor = AnchorStyles.Top;
    this.btnFlagAllObjects.Location = new Point(19, 16 /*0x10*/);
    this.btnFlagAllObjects.Name = "btnFlagAllObjects";
    this.btnFlagAllObjects.Size = new Size(130, 53);
    this.btnFlagAllObjects.TabIndex = 0;
    this.btnFlagAllObjects.Text = "Выделить все объекты";
    this.btnFlagAllObjects.UseVisualStyleBackColor = true;
    this.btnFlagAllObjects.Click += new EventHandler(this.btnFlagAllObjects_Click);
    this.ntvObjectVersionComposition.AllowDrop = true;
    this.ntvObjectVersionComposition.AllowMultiSelect = false;
    this.ntvObjectVersionComposition.AllowUserPinnedColumns = false;
    this.ntvObjectVersionComposition.DisableCheckedOutColumn = true;
    this.ntvObjectVersionComposition.Dock = DockStyle.Fill;
    this.ntvObjectVersionComposition.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.ntvObjectVersionComposition.ImageList = (ImageList) null;
    this.ntvObjectVersionComposition.LineStyle = LineStyle.Dot;
    this.ntvObjectVersionComposition.Location = new Point(0, 53);
    this.ntvObjectVersionComposition.Name = "ntvObjectVersionComposition";
    this.ntvObjectVersionComposition.RowEvenStyle.WordWrap = false;
    this.ntvObjectVersionComposition.RowOddStyle.WordWrap = false;
    this.ntvObjectVersionComposition.RowSelectedStyle.WordWrap = false;
    this.ntvObjectVersionComposition.RowStyle.BorderColor = SystemColors.Control;
    this.ntvObjectVersionComposition.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.ntvObjectVersionComposition.RowStyle.BorderWidth = 1;
    this.ntvObjectVersionComposition.RowStyle.WordWrap = false;
    this.ntvObjectVersionComposition.SelectBeforeEdit = true;
    this.ntvObjectVersionComposition.ShowRootRow = false;
    this.ntvObjectVersionComposition.Size = new Size(594, 320);
    this.ntvObjectVersionComposition.SuppressErrorMessages = true;
    this.ntvObjectVersionComposition.TabIndex = 4;
    this.ntvObjectVersionComposition.SelectedItemsChanged += new EventHandler(this.ntvObjectVersionComposition_SelectedItemsChanged);
    this.AcceptButton = (IButtonControl) this.btnSave;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(761, 448);
    this.Controls.Add((Control) this.ntvObjectVersionComposition);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MinimumSize = new Size(616, 453);
    this.Name = nameof (SnapshotMasterForm);
    this.ShowIcon = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Мастер работы с итерациями";
    this.Activated += new EventHandler(this.SnapshotMasterForm_Activated);
    this.Deactivate += new EventHandler(this.SnapshotMasterForm_Deactivate);
    this.FormClosing += new FormClosingEventHandler(this.SnapshotMasterForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.SnapshotMasterForm_FormClosed);
    this.Load += new EventHandler(this.SnapshotMasterForm_Load);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.ntvObjectVersionComposition.EndInit();
    this.ResumeLayout(false);
  }
}
