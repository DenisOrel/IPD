
// Type: Intermech.Client.Core.ObjectCreator.Controls.ObjectFileAttributesControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

internal sealed class ObjectFileAttributesControl : ObjectCreatorControl
{
  /// <summary>Для корректной обработки контекстного меню для дерева</summary>
  private TreeNode rightNode;
  private bool _disposing;
  private IContainer components;
  private Label label1;
  private Splitter splitter1;
  private Panel panel1;
  private Label label2;
  private Label label3;
  private TextBox textBox1;
  private TreeView treeView1;
  private ToolTip toolTip1;
  private ContextMenu contextMenu1;
  private OpenFileDialog openFileDialog1;
  private Button buttonImport;
  private MenuItem menuItem3;
  private MenuItem menuItemAttrAdd;
  private MenuItem menuItemValueAdd;
  private MenuItem menuItemValueDel;
  private MenuItem menuItemValueImport;
  private MenuItem menuItemAttrDel;
  private TextBox textBox2;
  private IButtonControl acceptBtn;
  private IButtonControl cancelBtn;
  /// <summary>
  /// автоматическое переименование файлов при конфликте имен
  /// </summary>
  public static bool AutoRename;

  private TreeNode Selectednode => this.rightNode ?? this.treeView1.SelectedNode;

  public ObjectFileAttributesControl(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    FileAttributeStatics.InitImageList();
    this.treeView1.ImageList = FileAttributeStatics.imageList;
    this._StepIsReadyCheckRequired = true;
    this._NeedSaveWhenNotVisible = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    this._disposing = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectFileAttributesControl));
    this.splitter1 = new Splitter();
    this.panel1 = new Panel();
    this.buttonImport = new Button();
    this.textBox2 = new TextBox();
    this.textBox1 = new TextBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.treeView1 = new TreeView();
    this.contextMenu1 = new ContextMenu();
    this.menuItemAttrAdd = new MenuItem();
    this.menuItemAttrDel = new MenuItem();
    this.menuItem3 = new MenuItem();
    this.menuItemValueAdd = new MenuItem();
    this.menuItemValueDel = new MenuItem();
    this.menuItemValueImport = new MenuItem();
    this.toolTip1 = new ToolTip(this.components);
    this.openFileDialog1 = new OpenFileDialog();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.openFileDialog1.RestoreDirectory = true;
    this.splitter1.AccessibleDescription = (string) null;
    this.splitter1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.BackgroundImage = (Image) null;
    this.splitter1.Font = (Font) null;
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.toolTip1.SetToolTip((Control) this.splitter1, componentResourceManager.GetString("splitter1.ToolTip"));
    this.panel1.AccessibleDescription = (string) null;
    this.panel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackgroundImage = (Image) null;
    this.panel1.Controls.Add((Control) this.buttonImport);
    this.panel1.Controls.Add((Control) this.textBox2);
    this.panel1.Controls.Add((Control) this.textBox1);
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Font = (Font) null;
    this.panel1.Name = "panel1";
    this.panel1.TabStop = true;
    this.toolTip1.SetToolTip((Control) this.panel1, componentResourceManager.GetString("panel1.ToolTip"));
    this.buttonImport.AccessibleDescription = (string) null;
    this.buttonImport.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.buttonImport, "buttonImport");
    this.buttonImport.BackgroundImage = (Image) null;
    this.buttonImport.Font = (Font) null;
    this.buttonImport.Name = "buttonImport";
    this.toolTip1.SetToolTip((Control) this.buttonImport, componentResourceManager.GetString("buttonImport.ToolTip"));
    this.buttonImport.Click += new EventHandler(this.ButtonImport_Click);
    this.textBox2.AccessibleDescription = (string) null;
    this.textBox2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
    this.textBox2.BackgroundImage = (Image) null;
    this.textBox2.Font = (Font) null;
    this.textBox2.Name = "textBox2";
    this.toolTip1.SetToolTip((Control) this.textBox2, componentResourceManager.GetString("textBox2.ToolTip"));
    this.textBox2.Enter += new EventHandler(this.TextBox_Enter);
    this.textBox2.Leave += new EventHandler(this.TextBox_Leave);
    this.textBox2.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
    this.textBox1.AccessibleDescription = (string) null;
    this.textBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.BackgroundImage = (Image) null;
    this.textBox1.Font = (Font) null;
    this.textBox1.Name = "textBox1";
    this.toolTip1.SetToolTip((Control) this.textBox1, componentResourceManager.GetString("textBox1.ToolTip"));
    this.textBox1.Enter += new EventHandler(this.TextBox_Enter);
    this.textBox1.Leave += new EventHandler(this.TextBox_Leave);
    this.textBox1.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
    this.label3.AccessibleDescription = (string) null;
    this.label3.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Font = (Font) null;
    this.label3.Name = "label3";
    this.toolTip1.SetToolTip((Control) this.label3, componentResourceManager.GetString("label3.ToolTip"));
    this.label2.AccessibleDescription = (string) null;
    this.label2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Font = (Font) null;
    this.label2.Name = "label2";
    this.toolTip1.SetToolTip((Control) this.label2, componentResourceManager.GetString("label2.ToolTip"));
    this.label1.AccessibleDescription = (string) null;
    this.label1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ForeColor = SystemColors.ControlDarkDark;
    this.label1.Name = "label1";
    this.toolTip1.SetToolTip((Control) this.label1, componentResourceManager.GetString("label1.ToolTip"));
    this.treeView1.AccessibleDescription = (string) null;
    this.treeView1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.treeView1, "treeView1");
    this.treeView1.BackgroundImage = (Image) null;
    this.treeView1.ContextMenu = this.contextMenu1;
    this.treeView1.Font = (Font) null;
    this.treeView1.FullRowSelect = true;
    this.treeView1.HideSelection = false;
    this.treeView1.Name = "treeView1";
    this.toolTip1.SetToolTip((Control) this.treeView1, componentResourceManager.GetString("treeView1.ToolTip"));
    this.treeView1.AfterSelect += new TreeViewEventHandler(this.TreeView1_AfterSelect);
    this.treeView1.AfterExpand += new TreeViewEventHandler(this.TreeView1_AfterExpand);
    this.treeView1.MouseDown += new MouseEventHandler(this.ObjectFileAttributesControl_MouseDown);
    this.contextMenu1.MenuItems.AddRange(new MenuItem[6]
    {
      this.menuItemAttrAdd,
      this.menuItemAttrDel,
      this.menuItem3,
      this.menuItemValueAdd,
      this.menuItemValueDel,
      this.menuItemValueImport
    });
    componentResourceManager.ApplyResources((object) this.contextMenu1, "contextMenu1");
    componentResourceManager.ApplyResources((object) this.menuItemAttrAdd, "menuItemAttrAdd");
    this.menuItemAttrAdd.Index = 0;
    this.menuItemAttrAdd.Click += new EventHandler(this.MenuItemAttrAdd_Click);
    componentResourceManager.ApplyResources((object) this.menuItemAttrDel, "menuItemAttrDel");
    this.menuItemAttrDel.Index = 1;
    this.menuItemAttrDel.Click += new EventHandler(this.MenuItemAttrDel_Click);
    componentResourceManager.ApplyResources((object) this.menuItem3, "menuItem3");
    this.menuItem3.Index = 2;
    componentResourceManager.ApplyResources((object) this.menuItemValueAdd, "menuItemValueAdd");
    this.menuItemValueAdd.Index = 3;
    this.menuItemValueAdd.Click += new EventHandler(this.MenuItemValueAdd_Click);
    componentResourceManager.ApplyResources((object) this.menuItemValueDel, "menuItemValueDel");
    this.menuItemValueDel.Index = 4;
    this.menuItemValueDel.Click += new EventHandler(this.MenuItemValueDel_Click);
    componentResourceManager.ApplyResources((object) this.menuItemValueImport, "menuItemValueImport");
    this.menuItemValueImport.Index = 5;
    this.menuItemValueImport.Click += new EventHandler(this.MenuItemValueImport_Click);
    componentResourceManager.ApplyResources((object) this.openFileDialog1, "openFileDialog1");
    this.openFileDialog1.RestoreDirectory = true;
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.treeView1);
    this.Controls.Add((Control) this.label1);
    this.Font = (Font) null;
    this.Name = nameof (ObjectFileAttributesControl);
    this.toolTip1.SetToolTip((Control) this, componentResourceManager.GetString("$this.ToolTip"));
    this.MouseDown += new MouseEventHandler(this.ObjectFileAttributesControl_MouseDown);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Обновление данных (обновление и заполнение элементов управления)
  /// </summary>
  /// <param name="error">?Сообщение об ошибке. Если ошибки не было, то пустая строка.</param>
  /// <returns>Если обновление прошло успешно - true, иначе - false</returns>
  public override bool Refresh(PageRefreshArgs args)
  {
    this.treeView1.Nodes.Clear();
    this.UpdateControls();
    this.HandleFileAttributes(true);
    if (this.treeView1.Nodes.Count > 0)
    {
      this.treeView1.ExpandAll();
      TreeNode node = this.treeView1.Nodes[0];
      this.treeView1.SelectedNode = node.Nodes.Count > 0 ? node.FirstNode : node;
    }
    return base.Refresh(args);
  }

  /// <summary>Сохранение данных</summary>
  /// <param name="error">Сообщение об ошибке. Если ошибки не было, то пустая строка.</param>
  /// <returns>Если сохранение прошло успешно - true, иначе - false</returns>
  public override bool Save(PageSaveArgs args)
  {
    if ((args.NextPageIndex == -1 || args.NextPageIndex - this.PageIndex > 0) && this.CreatedObject.FileAttrs.IsExistsUnassigned())
      this.HandleFileAttributes(false);
    return base.Save(args);
  }

  private void HandleFileAttributes(bool addNode)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dBObject = sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID);
      foreach (IDBAttribute objectAttributeById in dBObject.Attributes.GetAttributesByType(FieldTypes.ftFile))
      {
        if (!this.CreatedObject.FileAttrs.IsAssigned(objectAttributeById.AttributeID))
        {
          if (objectAttributeById.IsNull)
          {
            int attributeId = objectAttributeById.AttributeID;
            SetFileAttrPrototype.Execute(objectAttributeById, sessionKeeper.Session, dBObject);
            objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.CreatedObject.ObjectID, attributeId);
          }
          this.CreatedObject.FileAttrs.AssignedAdd(objectAttributeById.AttributeID);
        }
        if (addNode)
          this.AddNodeAttr(objectAttributeById);
      }
    }
  }

  /// <summary>Диалог выбора файловых атрибутов</summary>
  /// <returns>Массив идентификаторов выбранных атрибутов</returns>
  private int[] SelectAttributeDialog()
  {
    ArrayList arrayList1 = new ArrayList();
    for (int index = 0; index < this.treeView1.Nodes.Count; ++index)
      arrayList1.Add((object) Convert.ToInt32(this.treeView1.Nodes[index].Tag));
    ArrayList posibleAttrSelObject = this.CreatedObject.FileAttrs.GetPosibleAttrSelObject(arrayList1.ToArray(typeof (int)) as int[]);
    FileAttributeSelectorForm attributeSelectorForm = new FileAttributeSelectorForm();
    ArrayList arrayList2 = new ArrayList();
    ArrayList attrSelObjects = posibleAttrSelObject;
    ArrayList arrayList3;
    ref ArrayList local = ref arrayList3;
    if (attributeSelectorForm.SelectDialog(attrSelObjects, out local) == DialogResult.OK && arrayList3 != null)
    {
      foreach (object obj in arrayList3)
        arrayList2.Add((object) ((AttrSelObject) obj).id);
    }
    return arrayList2.ToArray(typeof (int)) as int[];
  }

  /// <summary>Добавление в дерево узла атрибута</summary>
  /// <param name="attr">Интерфейс атрибута </param>
  /// <returns>Созданный узел для атрибута</returns>
  private TreeNode AddNodeAttr(IDBAttribute attr)
  {
    TreeNode attrNode = this.treeView1.Nodes.Add(attr.Name);
    attrNode.Tag = (object) attr.AttributeID;
    attrNode.ImageIndex = attrNode.SelectedImageIndex = FileAttributeStatics.FieldTypeToImageIndex(attr.AttributeType.AttributeType);
    for (int index = 0; index < attr.ValuesCount; ++index)
      this.AddNodeVal(attrNode, new ObjectFileAttributesControl.BlobInfoClass(attr, index));
    return attrNode;
  }

  /// <summary>Добавление в дерево узла значения атрибута</summary>
  /// <param name="attrNode">Узел атрибута для которого будет добавляться значение</param>
  /// <param name="info">Информация о добавляемом значении атрибута</param>
  /// <returns>Созданный узел для значения атрибута</returns>
  private TreeNode AddNodeVal(TreeNode attrNode, ObjectFileAttributesControl.BlobInfoClass info)
  {
    attrNode.Nodes.Add(info.OwnerNode);
    info.OwnerNode.ImageIndex = info.OwnerNode.SelectedImageIndex = FileAttributeStatics.GetExtImageIndex(Path.GetExtension(info.FileName).ToLower());
    return info.OwnerNode;
  }

  /// <summary>Получение текущего файлового атрибута</summary>
  /// <returns>Узел для редактируемого файлового атрибута</returns>
  private TreeNode GetCurrentAttribute()
  {
    TreeNode selectednode = this.Selectednode;
    return selectednode == null || selectednode.Parent == null ? selectednode : selectednode.Parent;
  }

  /// <summary>
  /// Локальная функция для предоставления информации о текущем значении редактируемого атрибута
  /// </summary>
  /// <returns>Информация о редактируемом значении атрибута (если нет выбранного узла, то null)</returns>
  private ObjectFileAttributesControl.BlobInfoClass GetCurrentInfo()
  {
    TreeNode selectednode = this.Selectednode;
    return selectednode == null || selectednode.Tag == null || !(selectednode.Tag is ObjectFileAttributesControl.BlobInfoClass) ? (ObjectFileAttributesControl.BlobInfoClass) null : selectednode.Tag as ObjectFileAttributesControl.BlobInfoClass;
  }

  /// <summary>Добавление файловых атрибутов к создаваемому объекту</summary>
  private void AttrAdd()
  {
    int[] numArray = this.SelectAttributeDialog();
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dBObject = sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID);
      foreach (int attributeID in numArray)
      {
        IDBAttribute attr = dBObject.Attributes.AddAttribute(attributeID, true);
        int attributeId = attr.AttributeID;
        SetFileAttrPrototype.Execute(attr, sessionKeeper.Session, dBObject);
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.CreatedObject.ObjectID, attributeId);
        this.CreatedObject.FileAttrs.AssignedAdd(objectAttributeById.AttributeID);
        this.treeView1.SelectedNode = this.AddNodeAttr(objectAttributeById);
      }
    }
  }

  /// <summary>Удаление выбранного файлового атрибута</summary>
  private void AttrDel()
  {
    TreeNode currentAttribute = this.GetCurrentAttribute();
    if (currentAttribute == null)
      return;
    int int32 = Convert.ToInt32(currentAttribute.Tag);
    if (int32 == -1)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.GetObjectAttributeByID(this.CreatedObject.ObjectID, int32).Delete(0L);
      this.CreatedObject.FileAttrs.AssignedRemove(int32);
      currentAttribute.Remove();
      this.UpdateControls();
    }
  }

  /// <summary>Добавление значения к выбранному файловому атрибуту</summary>
  /// <param name="attrNode">Узел выбранного файлового атрибута</param>
  private void AttrValueAdd(TreeNode attrNode)
  {
    this.openFileDialog1.Multiselect = true;
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    int int32 = Convert.ToInt32(attrNode.Tag);
    using (RemoteLock remoteLock = new RemoteLock())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.CreatedObject.ObjectID, int32);
        remoteLock.Add((object) objectAttributeById);
        List<ObjectFileAttributesControl.BlobInfoClass> blobInfoClassList = new List<ObjectFileAttributesControl.BlobInfoClass>(this.openFileDialog1.FileNames.Length);
        try
        {
          this.PrepareAddOrUpdate();
          for (int index1 = 0; index1 < this.openFileDialog1.FileNames.Length; ++index1)
          {
            int index2 = objectAttributeById.AddValue((object) null);
            ObjectFileAttributesControl.BlobInfoClass blobInfoClass = new ObjectFileAttributesControl.BlobInfoClass(objectAttributeById, index2);
            if (!blobInfoClass.ImportFile(this.openFileDialog1.FileNames[index1]))
            {
              objectAttributeById.Index = index2;
              objectAttributeById.DeleteValue();
              break;
            }
            blobInfoClassList.Add(blobInfoClass);
            this.AddNodeVal(attrNode, blobInfoClassList[index1]);
          }
        }
        catch
        {
          throw;
        }
        this.treeView1.SelectedNode = attrNode.Nodes[attrNode.Nodes.Count - 1];
      }
    }
  }

  /// <summary>
  /// Удаление редактируемого значения у выбранного файлового атрибута
  /// </summary>
  /// <param name="info">Информация о редактируемом значении</param>
  private void AttrValueDel(ObjectFileAttributesControl.BlobInfoClass info)
  {
    if (info == null || info.RemoveValue())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TreeNode currentAttribute = this.GetCurrentAttribute();
      int int32 = Convert.ToInt32(currentAttribute.Tag);
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.CreatedObject.ObjectID, int32);
      int index = objectAttributeById.AddValue((object) null);
      this.AddNodeVal(currentAttribute, new ObjectFileAttributesControl.BlobInfoClass(objectAttributeById, index));
      info.RemoveValue();
    }
  }

  /// <summary>Импорт файла в значение файлового атрибута</summary>
  /// <param name="info">Информация о редактируемом значении, в которое будет импортирован файл</param>
  /// <returns>Выбрали ли файл для добавления</returns>
  private bool AttrValueImport(ObjectFileAttributesControl.BlobInfoClass info)
  {
    this.openFileDialog1.Multiselect = false;
    if (info == null || this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return false;
    this.PrepareAddOrUpdate();
    if (!info.ImportFile(this.openFileDialog1.FileName))
      return false;
    if (this.Selectednode != this.rightNode)
      this.UpdateValues(info);
    else
      this.rightNode = (TreeNode) null;
    return true;
  }

  /// <summary>
  /// Обновление эементов управления связанных с текущим значением атрибута
  /// </summary>
  /// <param name="info">информация о текущем значении атрибута</param>
  private void UpdateValues(ObjectFileAttributesControl.BlobInfoClass info)
  {
    if (info == null)
      return;
    this.textBox1.Text = info.FileName;
    this.textBox2.Text = info.Note;
  }

  /// <summary>Сохранение изменений в текущем значении атрибута</summary>
  /// <param name="info"></param>
  private void SaveValues(ObjectFileAttributesControl.BlobInfoClass info)
  {
    if (info == null)
      return;
    string text = this.textBox1.Text;
    if (text == string.Empty)
      return;
    if (Path.GetExtension(text) == string.Empty)
    {
      string str = Path.GetExtension(info.FileName);
      if (str == string.Empty)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DocumentTypeSettings settings = ((IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService))).GetSettings(sessionKeeper.Session.SessionGUID, this.CreatedObject.ObjectTypeID);
          if (settings.DocumentFileExt != string.Empty)
            this.textBox1.Text = text + settings.DocumentFileExt;
        }
      }
      else
        this.textBox1.Text = text + str;
    }
    info.FileName = this.textBox1.Text;
    info.Note = this.textBox2.Text;
    info.SaveValue();
  }

  /// <summary>Обновление доступности пунктов меню</summary>
  private void UpdateMenu()
  {
    TreeNode currentAttribute = this.GetCurrentAttribute();
    this.menuItemAttrDel.Enabled = currentAttribute != null;
    this.menuItemValueAdd.Enabled = currentAttribute != null && (currentAttribute.Nodes.Count == 0 || this.CreatedObject.FileAttrs.IsMultiValue(Convert.ToInt32(currentAttribute.Tag)));
    this.menuItemValueImport.Enabled = this.menuItemValueDel.Enabled = this.GetCurrentInfo() != null;
  }

  /// <summary>Обновление злементов управления</summary>
  private void UpdateControls()
  {
    ObjectFileAttributesControl.BlobInfoClass currentInfo = this.GetCurrentInfo();
    this.panel1.Visible = this.buttonImport.Enabled = currentInfo != null;
    this.UpdateMenu();
    this.UpdateValues(currentInfo);
  }

  /// <summary>
  /// Поиск фомы, на которой находится данный элемент управления (ObjectFileAttributesControl)
  /// </summary>
  /// <returns>Экземпляр формы-владельца, или null если не удалось получить parent</returns>
  private Form GetParentForm()
  {
    Control parent = this.Parent;
    while (true)
    {
      switch (parent)
      {
        case null:
        case Form _:
          goto label_3;
        default:
          parent = parent.Parent;
          continue;
      }
    }
label_3:
    return parent == null ? (Form) null : parent as Form;
  }

  private void ClearDefaulButtons()
  {
    Form parentForm = this.GetParentForm();
    if (parentForm == null)
      return;
    this.acceptBtn = parentForm.AcceptButton;
    this.cancelBtn = parentForm.CancelButton;
    parentForm.AcceptButton = (IButtonControl) null;
    parentForm.CancelButton = (IButtonControl) null;
  }

  private void SetDefaultButtons()
  {
    Form parentForm = this.GetParentForm();
    if (parentForm == null)
      return;
    if (this.acceptBtn != null)
      parentForm.AcceptButton = this.acceptBtn;
    if (this.cancelBtn != null)
      parentForm.CancelButton = this.cancelBtn;
    this.acceptBtn = (IButtonControl) null;
    this.cancelBtn = (IButtonControl) null;
  }

  private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) => this.UpdateControls();

  private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e)
  {
    if (this.treeView1.SelectedNode == null || this.treeView1.SelectedNode.Level != 0 || this.treeView1.SelectedNode.Nodes.Count <= 0)
      return;
    this.treeView1.SelectedNode = this.treeView1.SelectedNode.Nodes[0];
  }

  private void TextBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Return)
    {
      if (sender == this.textBox1)
      {
        this.textBox2.Focus();
      }
      else
      {
        if (sender != this.textBox2 || this.acceptBtn == null)
          return;
        (this.acceptBtn as Button).Focus();
      }
    }
    else
    {
      if (e.KeyCode != Keys.Escape)
        return;
      this.UpdateValues(this.GetCurrentInfo());
    }
  }

  private void TextBox_Enter(object sender, EventArgs e) => this.ClearDefaulButtons();

  private void TextBox_Leave(object sender, EventArgs e)
  {
    if (this._disposing)
      return;
    this.SaveValues(this.GetCurrentInfo());
    this.SetDefaultButtons();
  }

  private void ButtonImport_Click(object sender, EventArgs e)
  {
    this.AttrValueImport(this.GetCurrentInfo());
  }

  private void MenuItemAttrAdd_Click(object sender, EventArgs e) => this.AttrAdd();

  private void MenuItemAttrDel_Click(object sender, EventArgs e) => this.AttrDel();

  private void MenuItemValueAdd_Click(object sender, EventArgs e)
  {
    this.AttrValueAdd(this.GetCurrentAttribute());
  }

  private void MenuItemValueDel_Click(object sender, EventArgs e)
  {
    this.AttrValueDel(this.GetCurrentInfo());
  }

  private void MenuItemValueImport_Click(object sender, EventArgs e)
  {
    this.AttrValueImport(this.GetCurrentInfo());
  }

  private void ObjectFileAttributesControl_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Right)
    {
      TreeNode nodeAt = this.treeView1.GetNodeAt(e.X, e.Y);
      this.rightNode = nodeAt == null || this.treeView1.SelectedNode == nodeAt ? (TreeNode) null : nodeAt;
      this.UpdateMenu();
    }
    else
      this.rightNode = (TreeNode) null;
  }

  private void PrepareAddOrUpdate() => ObjectFileAttributesControl.AutoRename = false;

  /// <summary>
  /// Локальный класс для работы со значенияим атрибута (а точнее с информацией о блобах)
  /// </summary>
  private class BlobInfoClass
  {
    /// <summary>Идентификатор объекта, чьим атрибутом является данный</summary>
    private readonly long _objectId;
    /// <summary>
    /// Идентификатор атрибута для которого принадлежит это значение
    /// </summary>
    private readonly int _attrID;
    /// <summary>Индекс значения в списке значений атрибута</summary>
    private int _index;
    /// <summary>Поле для хранения наименования файла</summary>
    private string _fileName;
    /// <summary>Поле для хранения комментария</summary>
    private string _note;
    /// <summary>
    /// Узел для которого экземляр данного класса задан в качестве Tag
    /// </summary>
    public TreeNode OwnerNode;

    /// <summary>Наименование файла</summary>
    public string FileName
    {
      get => this._fileName;
      set
      {
        if (!(this._fileName != value))
          return;
        this._fileName = value;
        if (this.OwnerNode == null)
          return;
        if (this._fileName == string.Empty)
        {
          this.OwnerNode.Text = LocalizationHolder.rm.GetString("Client.Core_849");
          this.OwnerNode.ImageIndex = this.OwnerNode.StateImageIndex = -1;
        }
        else
        {
          this.OwnerNode.Text = this._fileName;
          this.OwnerNode.ImageIndex = this.OwnerNode.SelectedImageIndex = FileAttributeStatics.GetExtImageIndex(Path.GetExtension(this._fileName).ToLower());
        }
      }
    }

    /// <summary>Комментарии</summary>
    public string Note
    {
      get => this._note;
      set
      {
        if (!(this._note != value))
          return;
        this._note = value;
      }
    }

    /// <summary>Конструктор</summary>
    /// <param name="attr">Интерфейс атрибута, к которому относится данное значение</param>
    /// <param name="index">Индекс значения в списке значений атрибута</param>
    public BlobInfoClass(IDBAttribute attr, int index)
    {
      this._attrID = attr.AttributeID;
      this._objectId = attr.DBObjectID;
      attr.Index = index;
      if (!(attr is IBlobReader blobReader))
        return;
      BlobInformation blobInformation = blobReader.OpenBlob(-1);
      this.OwnerNode = new TreeNode()
      {
        Tag = (object) this
      };
      this.FileName = blobInformation.FileName;
      this.Note = blobInformation.Note;
      this._index = index;
    }

    /// <summary>
    /// Сохранение текущего значения вайлового атрибута в базе
    /// </summary>
    public void SaveValue()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._objectId, this._attrID);
        objectAttributeById.Index = this._index;
        (objectAttributeById as IBlobWriter).OpenBlob(new BlobInformation()
        {
          FileName = this.FileName,
          Note = this.Note,
          ModifyDate = DateTime.Now
        }, true);
      }
    }

    /// <summary>
    /// Корректировка индекса значения (в списке значений атрибута) - связана с удалением одного из значений
    /// т.е. при удалении, например, 4-го значение с индексом 5 становится 4-ым, и т.д.
    /// </summary>
    /// <param name="DeletedIndex">Индекс удаленного значения</param>
    private void CorrectIndex(int DeletedIndex)
    {
      if (this._index <= DeletedIndex)
        return;
      --this._index;
    }

    /// <summary>
    /// Удаление текущего значения у выбранного атрибута (и удаление узла дерева с которым это значение связано)
    /// </summary>
    /// <returns>Если нужно удалить возвращает false</returns>
    public bool RemoveValue()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._objectId, this._attrID);
        if (objectAttributeById.ValuesCount > 1)
        {
          objectAttributeById.Index = this._index;
          objectAttributeById.DeleteValue();
          if (this.OwnerNode != null)
          {
            TreeNode parent = this.OwnerNode.Parent;
            if (parent != null)
            {
              for (int index = 0; index < parent.Nodes.Count; ++index)
              {
                if (parent.Nodes[index].Tag != null && parent.Nodes[index].Tag is ObjectFileAttributesControl.BlobInfoClass)
                  (parent.Nodes[index].Tag as ObjectFileAttributesControl.BlobInfoClass).CorrectIndex(this._index);
              }
            }
            int index1 = this.OwnerNode.Index;
            TreeView treeView = this.OwnerNode.TreeView;
            this.OwnerNode.Remove();
            if (treeView != null && parent != null)
              treeView.SelectedNode = parent.Nodes.Count > index1 ? parent.Nodes[index1] : parent.Nodes[parent.Nodes.Count - 1];
          }
          return true;
        }
        if (objectAttributeById.AttributeType is IDBAttributeType4 attributeType && attributeType.Required != RequiredModes.Manual)
          return false;
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_850"), LocalizationHolder.rm.GetString("Client.Core_851"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return true;
      }
    }

    /// <summary>
    /// Импорт файла в значение файлового атрибута (и сохранение в базе)
    /// </summary>
    /// <param name="_filename">Полный путь к импортируемому файлу</param>
    /// <returns>true-добавлен; false- не добавлен, exception заглушен (false возвращается только при дублировании имени файла + отказе пользователя продолжать далее) </returns>
    public bool ImportFile(string _filename)
    {
      string path = _filename;
      bool flag = true;
      while (flag)
      {
        using (FileStream aSourceStream = new FileStream(_filename, FileMode.Open, FileAccess.Read))
        {
          BlobProcWriter blobProcWriter = new BlobProcWriter(this._objectId, AttributableElements.Object, this._attrID, this._index, 0, new BlobInformation()
          {
            ArcMethod = ArcMethods.ZLibPacked,
            FileName = path,
            Note = this.Note,
            ModifyDate = DateTime.Now
          }, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          try
          {
            blobProcWriter.WriteData();
            this.FileName = Path.GetFileName(path);
          }
          catch (Exception ex)
          {
            if (ex is KernelExceptionID && (((KernelExceptionID) ex).ErrorID == 336 || ((KernelExceptionID) ex).ErrorID == 324))
            {
              if (ObjectFileAttributesControl.AutoRename)
              {
                path = Path.Combine(Path.GetDirectoryName(_filename), FileAttributeEditForm.AutoRename(_filename));
                continue;
              }
              FileAttributeRenameForm attributeRenameForm = new FileAttributeRenameForm();
              string empty = string.Empty;
              string conflictFullName = _filename;
              ref string local = ref empty;
              switch (attributeRenameForm.ShowDialog(conflictFullName, out local))
              {
                case DialogResult.OK:
                  path = Path.Combine(Path.GetDirectoryName(_filename), empty);
                  continue;
                case DialogResult.Cancel:
                  return false;
                case DialogResult.Yes:
                  ObjectFileAttributesControl.AutoRename = true;
                  path = Path.Combine(Path.GetDirectoryName(_filename), FileAttributeEditForm.AutoRename(_filename));
                  continue;
              }
            }
            throw;
          }
        }
        flag = false;
      }
      return true;
    }
  }
}
