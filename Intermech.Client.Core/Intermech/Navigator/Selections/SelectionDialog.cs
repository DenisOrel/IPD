
// Type: Intermech.Navigator.Selections.SelectionDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.Client.Core.History;
using Intermech.Client.Core.Organizer;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections;

public class SelectionDialog : Form, ISelectorFilter
{
  /// <summary>ID объекта, св-ва которого редактируем</summary>
  private long _objID;
  /// <summary>ID типа объекта, св-ва которого редактируем</summary>
  private int _objTypeID = -1;
  /// <summary>Список выбранных типов объектов</summary>
  private ArrayList _IDList = new ArrayList();
  /// <summary>Список выбранных архивов</summary>
  private List<long> _listOfArchivesIDs = new List<long>();
  /// <summary>
  /// кнопки добавить/удалить работают для архивов.
  /// false - для типов объектов
  /// </summary>
  private bool _isSelectArchives;
  /// <summary>атриубт  Почта</summary>
  private static readonly Guid _postGuid = new Guid("cad0132f-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут Архивы</summary>
  private static readonly Guid _arcsGuid = new Guid("cad01485-306c-11d8-b4e9-00304f19f545");
  /// <summary>изменения на форме</summary>
  private bool _isModified;
  /// <summary>показывать ли принадлежность</summary>
  private bool _typeVisible = true;
  /// <summary>атрибут, в котором хранится принадлженость объекта</summary>
  private Guid _attrTypeGuid = Guid.Empty;
  /// <summary>выбранный тип объекта - выборка?</summary>
  private bool _isSelection = true;
  /// <summary>Допустимые типы</summary>
  private int[] _enableTypes;
  private ISelectionDialogTab[] _tabs;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnApply;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem toolStripMenuItem1;
  private ToolStripMenuItem toolStripMenuItem2;
  private ToolTip toolTip1;
  private Panel panel2;
  private Label label4;
  private Label label5;
  private Label label6;
  private Label label8;
  private TextBox textBox1;
  private ImageList ilAddImages;
  private TabPage tabPage1;
  private CheckBox checkBox1;
  private Label label3;
  private Panel panel1;
  private TreeView tvObjects;
  private System.Windows.Forms.ComboBox cbPost;
  private Panel panel4;
  private Button btnAdd;
  private Button btnDelete;
  private ButtonEdit buttonEdit1;
  private Label lbType;
  private System.Windows.Forms.ComboBox cbType;
  private Label lbComment;
  private TabControl tabControl1;
  private Panel panel5;
  private Panel panel6;
  private CheckBox cbLocalTypes;
  private CheckBox checkBox3;

  public SelectionDialog()
  {
    this.InitializeComponent();
    this.tvObjects.ImageList = Statics.IconSrv?.ImageList;
    this.tvObjects.Nodes[0].ImageIndex = this.tvObjects.Nodes[0].SelectedImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryObjectTypes, 0);
  }

  public bool IsModified
  {
    get => this._isModified;
    set
    {
      this._isModified = value;
      this.btnApply.Enabled = this.btnCancel.Enabled = this._isModified;
    }
  }

  public bool IsTypeVisible
  {
    set
    {
      this._typeVisible = this.lbComment.Visible = this.lbType.Visible = this.cbType.Visible = this.panel1.Visible = value;
    }
  }

  public void SetParent(Control parent) => this.SetParent(parent, false);

  public void SetParent(Control parent, bool visible)
  {
    if (parent == null)
      return;
    this.TopLevel = false;
    this.Dock = DockStyle.Fill;
    this.FormBorderStyle = FormBorderStyle.None;
    this.Visible = true;
    this.Parent = parent;
    this.btnApply.Visible = this.btnCancel.Visible = visible;
  }

  public bool TabsSave(IUserSession session, long newObjectID)
  {
    if (this._tabs != null)
    {
      for (int index = 0; index < this._tabs.Length; ++index)
        this._tabs[index].Save(session, newObjectID);
    }
    return true;
  }

  private object GetAttributeValue(IDBObject selObject, int attributeID)
  {
    IDBAttribute attributeById = selObject.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? (object) null : attributeById.Value;
  }

  private void CreateAttributes(
    IDBObject selObject,
    out AttributeValues[] oldValues,
    out AttributeValues[] newValues)
  {
    List<AttributeValues> attributeValuesList1 = new List<AttributeValues>();
    List<AttributeValues> attributeValuesList2 = new List<AttributeValues>();
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
    string initValue1 = Convert.ToString(this.GetAttributeValue(selObject, attributeTypeId1));
    if (initValue1 != this.buttonEdit1.Text)
    {
      attributeValuesList1.Add(new AttributeValues(attributeTypeId1, (object) initValue1));
      attributeValuesList2.Add(new AttributeValues(attributeTypeId1, (object) this.buttonEdit1.Text));
    }
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(this._attrTypeGuid);
    object attributeValue = this.GetAttributeValue(selObject, attributeTypeId2);
    int num1 = attributeValue != null ? Convert.ToInt32(attributeValue) : -1;
    MyElement selectedItem = this.cbType.SelectedItem as MyElement;
    int num2 = (int) selectedItem.Value;
    if (num1 != num2)
    {
      attributeValuesList1.Add(new AttributeValues(attributeTypeId2, attributeValue));
      attributeValuesList2.Add(new AttributeValues(attributeTypeId2, selectedItem.Value));
    }
    int attributeTypeId3 = MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545");
    string initValue2 = Convert.ToString(this.GetAttributeValue(selObject, attributeTypeId3));
    if (initValue2 != this.textBox1.Text)
    {
      attributeValuesList1.Add(new AttributeValues(attributeTypeId3, (object) initValue2));
      attributeValuesList2.Add(new AttributeValues(attributeTypeId3, (object) this.textBox1.Text));
    }
    if (this._isSelection)
    {
      int attributeTypeId4 = MetaDataHelper.GetAttributeTypeID("cad00155-306c-11d8-b4e9-00304f19f545");
      bool boolean1 = Convert.ToBoolean(this.GetAttributeValue(selObject, attributeTypeId4));
      if (boolean1 != this.checkBox1.Checked)
      {
        attributeValuesList1.Add(new AttributeValues(attributeTypeId4, (object) boolean1));
        attributeValuesList2.Add(new AttributeValues(attributeTypeId4, (object) this.checkBox1.Checked));
      }
      int attributeTypeId5 = MetaDataHelper.GetAttributeTypeID(Consts.attTypeSearchInLocalTypes);
      bool boolean2 = Convert.ToBoolean(this.GetAttributeValue(selObject, attributeTypeId5));
      if (boolean2 != this.cbLocalTypes.Checked)
      {
        attributeValuesList1.Add(new AttributeValues(attributeTypeId5, (object) boolean2));
        attributeValuesList2.Add(new AttributeValues(attributeTypeId5, (object) this.cbLocalTypes.Checked));
      }
      int attributeTypeId6 = MetaDataHelper.GetAttributeTypeID("cadd99b3-306c-11d8-b4e9-00304f19f545");
      bool boolean3 = Convert.ToBoolean(this.GetAttributeValue(selObject, attributeTypeId6));
      if (boolean3 != this.checkBox3.Checked)
      {
        attributeValuesList1.Add(new AttributeValues(attributeTypeId6, (object) boolean3));
        attributeValuesList2.Add(new AttributeValues(attributeTypeId6, (object) this.checkBox3.Checked));
      }
    }
    oldValues = attributeValuesList1.ToArray();
    newValues = attributeValuesList2.ToArray();
  }

  /// <summary>жмём кнопку далее и сохраняем сделанные изменения</summary>
  public void SelectionSave(bool creation)
  {
    if (!this._isModified)
      return;
    object obj = (this.cbType.SelectedItem as MyElement).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject selObject = sessionKeeper.Session.GetObject(this._objID, true);
      AttributeValues[] oldValues;
      AttributeValues[] newValues;
      this.CreateAttributes(selObject, out oldValues, out newValues);
      selObject.SetAttributesValues(newValues);
      if (this._typeVisible)
      {
        if (obj.Equals((object) SelectionType.Context) || obj.Equals((object) SelectionType.ObjectType) || obj.Equals((object) ClassificatorType.ObjectType) || obj.Equals((object) SelectionType.ListObjects))
        {
          IDBAttribute dbAttribute = selObject.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          if (dbAttribute != null && dbAttribute.ValuesCount > 0)
            dbAttribute.ClearValues();
          if (this.tvObjects.Nodes[0].Nodes.Count > 0)
          {
            ArrayList arrayList = new ArrayList(this.tvObjects.Nodes[0].Nodes.Count);
            foreach (TreeNode node in this.tvObjects.Nodes[0].Nodes)
              arrayList.Add((object) ((IDBObjectType) node.Tag).PropertiesStructure.ObjectTypeGuid.ToString());
            if (dbAttribute == null)
              dbAttribute = selObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00149-306c-11d8-b4e9-00304f19f545"), false);
            dbAttribute.Values = (object[]) arrayList.ToArray(typeof (object));
          }
          selObject.Attributes.FindByGUID(SelectionDialog._arcsGuid)?.Delete(0L);
          selObject.Attributes.FindByGUID(SelectionDialog._postGuid)?.Delete(0L);
        }
        else if (obj.Equals((object) SelectionType.Archiv) || obj.Equals((object) ClassificatorType.Archiv))
        {
          IDBAttribute dbAttribute = selObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(SelectionDialog._arcsGuid), false);
          dbAttribute.ClearValues();
          if (this.tvObjects.Nodes[0].Nodes.Count > 0)
          {
            List<object> objectList = new List<object>(this.tvObjects.Nodes[0].Nodes.Count);
            foreach (TreeNode node in this.tvObjects.Nodes[0].Nodes)
              objectList.Add(node.Tag);
            dbAttribute.Values = objectList.ToArray();
          }
          selObject.Attributes.FindByGUID(SelectionDialog._postGuid)?.Delete(0L);
          IDBAttribute byGuid = selObject.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          if (byGuid != null && byGuid.ValuesCount > 0)
            byGuid.ClearValues();
        }
        else if (obj.Equals((object) SelectionType.Mail))
        {
          selObject.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(SelectionDialog._postGuid).AttributeID, false, new object[1]
          {
            (object) this.cbPost.SelectedIndex
          });
          selObject.Attributes.FindByGUID(SelectionDialog._arcsGuid)?.Delete(0L);
          IDBAttribute byGuid = selObject.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          if (byGuid != null && byGuid.ValuesCount > 0)
            byGuid.ClearValues();
        }
        else if (obj.Equals((object) SelectionType.Organizer))
        {
          int attributeId = sessionKeeper.Session.GetAttributeType(new Guid("cad015d1-306c-11d8-b4e9-00304f19f545")).AttributeID;
          (selObject.Attributes.FindByID(attributeId) ?? selObject.Attributes.AddAttribute(attributeId, false)).Value = this.cbPost.SelectedValue;
        }
        else
        {
          selObject.Attributes.FindByGUID(SelectionDialog._arcsGuid)?.Delete(0L);
          selObject.Attributes.FindByGUID(SelectionDialog._postGuid)?.Delete(0L);
          IDBAttribute byGuid = selObject.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          if (byGuid != null && byGuid.ValuesCount > 0)
            byGuid.ClearValues();
        }
      }
      if (!creation)
        this.TabsSave(sessionKeeper.Session, selObject.ObjectID);
      this.IsModified = false;
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", this._objID, selObject.ObjectType, oldValues, newValues));
    }
  }

  /// <summary>Добавим в дерево выбранные типы объектов</summary>
  private void AddObjectTypeInTree()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.AddObjectTypeInTree(sessionKeeper.Session);
  }

  /// <summary>Добавим в дерево выбранные объекты</summary>
  private void AddObjectTypeInTree(IUserSession session)
  {
    this.tvObjects.BeginUpdate();
    try
    {
      foreach (int id in this._IDList)
      {
        IDBObjectType objectType = session.GetObjectType(id);
        bool flag = false;
        foreach (TreeNode node in this.tvObjects.Nodes[0].Nodes)
        {
          if ((node.Tag as IDBObjectType).ObjectType == objectType.ObjectType)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          TreeNode node = new TreeNode(objectType.ObjectTypeName)
          {
            Tag = (object) objectType
          };
          node.ImageIndex = node.SelectedImageIndex = Statics.IconSrv.IndexOf(4, id);
          this.tvObjects.Nodes[0].Nodes.Add(node);
          this.IsModified = true;
          this._isModified = true;
        }
      }
    }
    finally
    {
      this.tvObjects.Sort();
      this.tvObjects.EndUpdate();
      this.tvObjects.ExpandAll();
    }
  }

  /// <summary>Добавим в дерево выбранные объекты</summary>
  private void AddArchivesInTree(IUserSession session)
  {
    this.tvObjects.BeginUpdate();
    try
    {
      foreach (long listOfArchivesId in this._listOfArchivesIDs)
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(listOfArchivesId);
        if (!objectInfo.Empty)
        {
          bool flag = false;
          foreach (TreeNode node in this.tvObjects.Nodes[0].Nodes)
          {
            if ((long) node.Tag == listOfArchivesId)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            TreeNode node = new TreeNode(objectInfo.Caption)
            {
              Tag = (object) objectInfo.ObjectID
            };
            node.ImageIndex = node.SelectedImageIndex = Statics.IconSrv.IndexOf(4, objectInfo.ObjectTypeID);
            this.tvObjects.Nodes[0].Nodes.Add(node);
          }
        }
      }
    }
    finally
    {
      this.tvObjects.Sort();
      this.tvObjects.EndUpdate();
      this.tvObjects.ExpandAll();
    }
  }

  /// <summary>Загрузить данные</summary>
  /// <param name="objID"> id объекта</param>
  /// <param name="objTypeID">id типа объекта</param>
  public void SelectionLoad(long objID, int objTypeID)
  {
    this._objID = objID;
    this._objTypeID = objTypeID;
    this.cbType.Items.Clear();
    this.label4.Text = MetaDataHelper.GetObjectType(objTypeID).ObjectName;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(objID, true);
      int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545");
      this._isSelection = MetaDataHelper.IsObjectTypeChildOf(objTypeID, objectTypeId);
      this._attrTypeGuid = this._isSelection ? new Guid("cad00158-306c-11d8-b4e9-00304f19f545") : new Guid("cad00e8f-306c-11d8-b4e9-00304f19f545");
      foreach (DataRow possibleValuesRow in sessionKeeper.Session.GetAttributeType(this._attrTypeGuid).GetPossibleValuesRows())
      {
        MyElement myElement = new MyElement();
        long int64 = Convert.ToInt64(possibleValuesRow["F_INLIST_ID"]);
        myElement.Value = this._isSelection ? (object) (SelectionType) int64 : (object) (ClassificatorType) int64;
        myElement.Caption = Convert.ToString(possibleValuesRow["F_DESCRIPTION"]);
        this.cbType.Items.Add((object) myElement);
      }
      IDBAttribute attributeByGuid1 = dbObject1.GetAttributeByGuid(this._attrTypeGuid, false);
      this.cbType.SelectedIndex = attributeByGuid1 != null ? Convert.ToInt32(attributeByGuid1.Value) : 0;
      IDBObject dbObject2 = sessionKeeper.Session.GetObject(dbObject1.OwnerID, false);
      this.label6.Text = dbObject2 == null ? string.Empty : dbObject2.Caption;
      IDBAttribute attributeByGuid2 = dbObject1.GetAttributeByGuid(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), false);
      this.textBox1.Text = attributeByGuid2 == null ? string.Empty : attributeByGuid2.AsString;
      this.buttonEdit1.Text = dbObject1.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), true).AsString;
      if (this._isSelection)
      {
        this.checkBox1.Checked = dbObject1.GetAttributeByGuid(new Guid("cad00155-306c-11d8-b4e9-00304f19f545"), true).AsBoolean;
        this.cbLocalTypes.Checked = dbObject1.GetAttributeByGuid(Consts.attTypeSearchInLocalTypes, true).AsBoolean;
        this.lbComment.Text = LocalizationHolder.rm.GetString("Client.Core_1510");
        if (this.checkBox1.Checked)
        {
          IDBAttribute attributeByGuid3 = dbObject1.GetAttributeByGuid(new Guid("cadd99b3-306c-11d8-b4e9-00304f19f545"));
          this.checkBox3.Enabled = true;
          this.checkBox3.Checked = attributeByGuid3 != null && attributeByGuid3.AsBoolean;
        }
        else
          this.checkBox3.Enabled = false;
      }
      else
      {
        this.checkBox1.Visible = false;
        this.cbLocalTypes.Visible = false;
        this.checkBox3.Visible = false;
        this.lbComment.Text = LocalizationHolder.rm.GetString("Client.Core_1511");
      }
      object obj = (this.cbType.SelectedItem as MyElement).Value;
      if (obj.Equals((object) SelectionType.Context) || obj.Equals((object) SelectionType.ObjectType) || obj.Equals((object) ClassificatorType.ObjectType))
        this.ObjectsLoad();
      else if (obj.Equals((object) SelectionType.Archiv) || obj.Equals((object) ClassificatorType.Archiv))
        this.ArchiveLoad();
      else if (obj.Equals((object) SelectionType.Mail))
        this.PostLoad();
      if (this._isSelection)
      {
        bool isPersonal = MetaDataHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545") == dbObject1.ObjectType;
        if (this._tabs != null)
        {
          for (int index = 0; index < this._tabs.Length; ++index)
            this._tabs[index].Initialize(sessionKeeper.Session, objID, isPersonal);
        }
        else
        {
          this._tabs = ((ISelectionDialogTabsService) ServicesManager.GetService(typeof (ISelectionDialogTabsService))).Tabs;
          if (this._tabs != null)
          {
            for (int index = 0; index < this._tabs.Length; ++index)
            {
              this._tabs[index].OnChanged += new EventHandler(this.SelectionDialog_OnChanged);
              TabPage tabPage = new TabPage(this._tabs[index].Caption);
              this.tabControl1.TabPages.Add(tabPage);
              Control tabControl = this._tabs[index].TabControl;
              this._tabs[index].Initialize(sessionKeeper.Session, objID, isPersonal);
              tabControl.Dock = DockStyle.Fill;
              tabPage.Controls.Add(tabControl);
            }
          }
        }
      }
      this.IsModified = false;
    }
  }

  private void SetModified() => this.IsModified = true;

  private void SelectionDialog_OnChanged(object sender, EventArgs e)
  {
    this.Invoke((Delegate) new MethodInvoker(this.SetModified));
  }

  private void ArchiveLoad()
  {
    this.tvObjects.Nodes[0].Nodes.Clear();
    this.tvObjects.Nodes[0].Text = LocalizationHolder.rm.GetString("Client.Core_1235");
    this.tvObjects.Nodes[0].ImageIndex = this.tvObjects.Nodes[0].SelectedImageIndex = Statics.IconSrv.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad0011e-306c-11d8-b4e9-00304f19f545")));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._objID, true).GetAttributeByGuid(SelectionDialog._arcsGuid, false);
      if (attributeByGuid == null)
        return;
      foreach (object obj in attributeByGuid.Values)
      {
        if (obj.ToString().Length != 0)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(Convert.ToInt64(obj));
          if (!objectInfo.Empty)
          {
            TreeNode node = new TreeNode(objectInfo.Caption)
            {
              Tag = (object) objectInfo.ObjectID
            };
            node.ImageIndex = node.SelectedImageIndex = Statics.IconSrv.IndexOf(4, objectInfo.ObjectTypeID);
            this.tvObjects.Nodes[0].Nodes.Add(node);
          }
        }
      }
      this.tvObjects.Sort();
      this.tvObjects.ExpandAll();
    }
  }

  private void PostLoad()
  {
    this.cbPost.DataSource = (object) null;
    this.cbPost.Items.Clear();
    MyElement selectedItem = this.cbType.SelectedItem as MyElement;
    if (selectedItem.Value.Equals((object) SelectionType.Mail))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (DataRow possibleValuesRow in sessionKeeper.Session.GetAttributeType(SelectionDialog._postGuid).GetPossibleValuesRows())
          this.cbPost.Items.Add((object) Convert.ToString(possibleValuesRow["F_DESCRIPTION"]));
        IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._objID, true).GetAttributeByGuid(SelectionDialog._postGuid);
        if (attributeByGuid != null && attributeByGuid.AsString.Length != 0)
          this.cbPost.SelectedIndex = Convert.ToInt32(attributeByGuid.AsString);
        else
          this.cbPost.SelectedIndex = 0;
      }
    }
    else
    {
      if (!selectedItem.Value.Equals((object) SelectionType.Organizer))
        return;
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add(new DataColumn("ValueMamber"));
      dataTable.Columns.Add(new DataColumn("DisplayMamber"));
      bool flag = false;
      int result = 0;
      if (ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute byId = sessionKeeper.Session.GetObject(this._objID, true).Attributes.FindByID(sessionKeeper.Session.GetAttributeType(new Guid("cad015d1-306c-11d8-b4e9-00304f19f545")).AttributeID);
          if (byId != null)
          {
            object obj = byId.Value;
            if (obj != null)
            {
              if (obj != DBNull.Value)
                int.TryParse(obj.ToString(), out result);
            }
          }
        }
        Dictionary<int, string> nodesCaption = service.NodesCaption;
        if (nodesCaption != null)
        {
          foreach (KeyValuePair<int, string> keyValuePair in nodesCaption)
          {
            dataTable.Rows.Add((object) keyValuePair.Key, (object) keyValuePair.Value);
            if (result == keyValuePair.Key)
              flag = true;
          }
        }
      }
      int objectTypeId = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
      dataTable.Rows.Add((object) objectTypeId, (object) LocalizationHolder.rm.GetString("Organaizer_TaskCaption"));
      if (objectTypeId == result)
        flag = true;
      this.cbPost.DataSource = (object) dataTable;
      this.cbPost.ValueMember = "ValueMamber";
      this.cbPost.DisplayMember = "DisplayMamber";
      if (!flag)
        return;
      this.cbPost.SelectedValue = (object) result;
    }
  }

  private void ObjectsLoad()
  {
    this.tvObjects.Nodes[0].Nodes.Clear();
    this.tvObjects.Nodes[0].Text = LocalizationHolder.rm.GetString("Client.Core_1234");
    this.tvObjects.Nodes[0].ImageIndex = this.tvObjects.Nodes[0].SelectedImageIndex = Statics.IconSrv.IndexOf(4, 0);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._objID, true).GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null && attributeByGuid.ValuesCount > 0)
      {
        foreach (object obj in attributeByGuid.Values)
        {
          if (obj != null && obj != DBNull.Value)
          {
            IDBObjectType objectType = sessionKeeper.Session.GetObjectType(new Guid(obj.ToString()), false);
            if (objectType != null)
            {
              TreeNode node = new TreeNode(objectType.ObjectTypeName)
              {
                Tag = (object) objectType
              };
              node.ImageIndex = node.SelectedImageIndex = Statics.IconSrv.IndexOf(4, objectType.PropertiesStructure.ObjectType);
              this.tvObjects.Nodes[0].Nodes.Add(node);
            }
          }
        }
      }
      this.tvObjects.Sort();
      this.tvObjects.ExpandAll();
    }
  }

  private void AddObjectType()
  {
    using (SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_1216"), typeof (ObjectTypeFolder), true))
    {
      selectorForm.SelectorFilter = (ISelectorFilter) this;
      if (selectorForm.ShowDialog() != DialogResult.OK)
        return;
      this._IDList = selectorForm.IDList;
      this.AddObjectTypeInTree();
    }
  }

  private void DeleteObject()
  {
    if (this.tvObjects.SelectedNode != null)
    {
      this.tvObjects.BeginUpdate();
      this.tvObjects.SelectedNode.Remove();
      this.tvObjects.Sort();
      this.tvObjects.EndUpdate();
    }
    this.IsModified = true;
    this.btnDelete.Enabled = this.contextMenuStrip1.Items[1].Enabled = this.tvObjects.Nodes[0].Nodes.Count > 0;
  }

  private void AddObject()
  {
    if (this._isSelectArchives)
      this.AddArchives();
    else
      this.AddObjectType();
    this.IsModified = true;
    this.btnDelete.Enabled = this.contextMenuStrip1.Items[1].Enabled = this.tvObjects.Nodes[0].Nodes.Count > 0;
  }

  private void AddArchives()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] collection = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_1217"), LocalizationHolder.rm.GetString("Client.Core_1218"), MetaDataHelper.GetObjectTypeID(new Guid("cad0011e-306c-11d8-b4e9-00304f19f545")), SelectionOptions.Default);
      if (collection == null || collection.Length == 0)
        return;
      this._listOfArchivesIDs = new List<long>((IEnumerable<long>) collection);
      this.AddArchivesInTree(sessionKeeper.Session);
    }
  }

  private void ToolStripMenuItem2_Click(object sender, EventArgs e) => this.DeleteObject();

  private void ButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    using (ObjectsHistory objectsHistory = new ObjectsHistory((object) this._objTypeID, AttributableElements.Object, (object) MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")))
    {
      objectsHistory.SelectedValue = (object) this.buttonEdit1.Text.Trim();
      if (objectsHistory.ShowDialog() != DialogResult.OK)
        return;
      this.buttonEdit1.Text = (string) objectsHistory.SelectedValue;
      this.IsModified = true;
    }
  }

  private void CbType_SelectedIndexChanged(object sender, EventArgs e)
  {
    object obj = (this.cbType.SelectedItem as MyElement).Value;
    if (obj.Equals((object) SelectionType.Context) || obj.Equals((object) SelectionType.ObjectType) || obj.Equals((object) SelectionType.ListObjects) || obj.Equals((object) ClassificatorType.ObjectType))
    {
      this.panel4.Visible = this.tvObjects.Visible = true;
      this.cbPost.Visible = false;
      this.btnAdd.Visible = this.btnDelete.Visible = true;
      this._isSelectArchives = false;
      this.btnAdd.Image = this.ilAddImages.Images[0];
      this.btnDelete.Enabled = this.contextMenuStrip1.Items[1].Enabled = this.tvObjects.Nodes[0].Nodes.Count > 0;
      this.ObjectsLoad();
    }
    else if (obj.Equals((object) SelectionType.Mail) || obj.Equals((object) SelectionType.Organizer))
    {
      this.cbPost.Visible = true;
      this.tvObjects.Visible = false;
      this.btnAdd.Visible = this.btnDelete.Visible = false;
      this.PostLoad();
    }
    else if (obj.Equals((object) SelectionType.Archiv) || obj.Equals((object) ClassificatorType.Archiv))
    {
      this.cbPost.Visible = false;
      this.panel4.Visible = this.tvObjects.Visible = true;
      this._isSelectArchives = true;
      this.btnAdd.Image = this.ilAddImages.Images[1];
      this.btnAdd.Visible = this.btnDelete.Visible = true;
      this.btnDelete.Enabled = this.contextMenuStrip1.Items[1].Enabled = this.tvObjects.Nodes[0].Nodes.Count > 0;
      this.ArchiveLoad();
    }
    else
      this.tvObjects.Visible = this.panel4.Visible = this.cbPost.Visible = false;
    this.cbLocalTypes.Enabled = !obj.Equals((object) SelectionType.ObjectType);
    this.IsModified = true;
  }

  private void ToolStripMenuItem1_Click(object sender, EventArgs e) => this.AddObject();

  private void BtnAdd_Click(object sender, EventArgs e) => this.AddObject();

  private void BtnDelete_Click(object sender, EventArgs e) => this.DeleteObject();

  private void TvObjects_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.btnDelete.Enabled = this.contextMenuStrip1.Items[1].Enabled = e.Node != this.tvObjects.Nodes[0];
  }

  private void CheckBox1_CheckedChanged(object sender, EventArgs e)
  {
    this.IsModified = true;
    this.checkBox3.Enabled = this.checkBox1.Checked;
  }

  private void CbPost_SelectedIndexChanged(object sender, EventArgs e) => this.IsModified = true;

  private void ButtonEdit1_TextChanged(object sender, EventArgs e) => this.IsModified = true;

  private void BtnCancel_Click(object sender, EventArgs e)
  {
    this.SelectionLoad(this._objID, this._objTypeID);
  }

  private void BtnApply_Click(object sender, EventArgs e)
  {
    this.SelectionSave(false);
    this.IsModified = false;
  }

  private void TextBox1_TextChanged_1(object sender, EventArgs e) => this.IsModified = true;

  public bool IsInFilter(int category, object id)
  {
    if (category != 4)
      return false;
    if (this._enableTypes == null)
      this._enableTypes = (ServicesManager.GetService(typeof (IClientCache)) as IClientCache).GetVisibleList(category);
    return Array.IndexOf<int>(this._enableTypes, (int) id) >= 0;
  }

  private void CbLocalTypes_CheckedChanged(object sender, EventArgs e) => this.IsModified = true;

  private void CheckBox3_CheckedChanged(object sender, EventArgs e) => this.IsModified = true;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionDialog));
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.toolStripMenuItem1 = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripMenuItem();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panel2 = new Panel();
    this.label4 = new Label();
    this.label5 = new Label();
    this.label6 = new Label();
    this.label8 = new Label();
    this.toolTip1 = new ToolTip(this.components);
    this.btnDelete = new Button();
    this.btnAdd = new Button();
    this.textBox1 = new TextBox();
    this.tabPage1 = new TabPage();
    this.checkBox3 = new CheckBox();
    this.cbLocalTypes = new CheckBox();
    this.lbComment = new Label();
    this.checkBox1 = new CheckBox();
    this.label3 = new Label();
    this.panel1 = new Panel();
    this.tvObjects = new TreeView();
    this.cbPost = new System.Windows.Forms.ComboBox();
    this.panel4 = new Panel();
    this.buttonEdit1 = new ButtonEdit();
    this.lbType = new Label();
    this.cbType = new System.Windows.Forms.ComboBox();
    this.tabControl1 = new TabControl();
    this.panel5 = new Panel();
    this.panel6 = new Panel();
    this.ilAddImages = new ImageList(this.components);
    this.contextMenuStrip1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel4.SuspendLayout();
    this.buttonEdit1.Properties.BeginInit();
    this.tabControl1.SuspendLayout();
    this.panel5.SuspendLayout();
    this.panel6.SuspendLayout();
    this.SuspendLayout();
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.toolStripMenuItem2
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Click += new EventHandler(this.ToolStripMenuItem1_Click);
    this.toolStripMenuItem2.BackColor = SystemColors.Control;
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem2, "toolStripMenuItem2");
    this.toolStripMenuItem2.Click += new EventHandler(this.ToolStripMenuItem2_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnApply.Click += new EventHandler(this.BtnApply_Click);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.BackColor = SystemColors.ScrollBar;
    this.panel2.Controls.Add((Control) this.label4);
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.toolTip1.SetToolTip((Control) this.btnDelete, componentResourceManager.GetString("btnDelete.ToolTip"));
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.BtnDelete_Click);
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.toolTip1.SetToolTip((Control) this.btnAdd, componentResourceManager.GetString("btnAdd.ToolTip"));
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.BtnAdd_Click);
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.BackColor = SystemColors.ControlLightLight;
    this.textBox1.Name = "textBox1";
    this.textBox1.TextChanged += new EventHandler(this.TextBox1_TextChanged_1);
    this.tabPage1.BackColor = SystemColors.Control;
    this.tabPage1.Controls.Add((Control) this.textBox1);
    this.tabPage1.Controls.Add((Control) this.label8);
    this.tabPage1.Controls.Add((Control) this.checkBox3);
    this.tabPage1.Controls.Add((Control) this.cbLocalTypes);
    this.tabPage1.Controls.Add((Control) this.lbComment);
    this.tabPage1.Controls.Add((Control) this.checkBox1);
    this.tabPage1.Controls.Add((Control) this.label3);
    this.tabPage1.Controls.Add((Control) this.panel1);
    this.tabPage1.Controls.Add((Control) this.buttonEdit1);
    this.tabPage1.Controls.Add((Control) this.lbType);
    this.tabPage1.Controls.Add((Control) this.cbType);
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Name = "tabPage1";
    componentResourceManager.ApplyResources((object) this.checkBox3, "checkBox3");
    this.checkBox3.Name = "checkBox3";
    this.checkBox3.UseVisualStyleBackColor = true;
    this.checkBox3.CheckedChanged += new EventHandler(this.CheckBox3_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbLocalTypes, "cbLocalTypes");
    this.cbLocalTypes.Name = "cbLocalTypes";
    this.cbLocalTypes.UseVisualStyleBackColor = true;
    this.cbLocalTypes.CheckedChanged += new EventHandler(this.CbLocalTypes_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.lbComment, "lbComment");
    this.lbComment.ForeColor = Color.DimGray;
    this.lbComment.Name = "lbComment";
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.CheckBox1_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.tvObjects);
    this.panel1.Controls.Add((Control) this.cbPost);
    this.panel1.Controls.Add((Control) this.panel4);
    this.panel1.Name = "panel1";
    this.tvObjects.BackColor = SystemColors.ControlLightLight;
    this.tvObjects.ContextMenuStrip = this.contextMenuStrip1;
    componentResourceManager.ApplyResources((object) this.tvObjects, "tvObjects");
    this.tvObjects.Name = "tvObjects";
    this.tvObjects.Nodes.AddRange(new TreeNode[1]
    {
      (TreeNode) componentResourceManager.GetObject("tvObjects.Nodes")
    });
    this.tvObjects.AfterSelect += new TreeViewEventHandler(this.TvObjects_AfterSelect);
    componentResourceManager.ApplyResources((object) this.cbPost, "cbPost");
    this.cbPost.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbPost.FormattingEnabled = true;
    this.cbPost.Name = "cbPost";
    this.cbPost.SelectedIndexChanged += new EventHandler(this.CbPost_SelectedIndexChanged);
    this.panel4.Controls.Add((Control) this.btnAdd);
    this.panel4.Controls.Add((Control) this.btnDelete);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.buttonEdit1, "buttonEdit1");
    this.buttonEdit1.Name = "buttonEdit1";
    this.buttonEdit1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit1.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Tahoma", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ControlLightLight, SystemColors.WindowText);
    this.buttonEdit1.ButtonClick += new ButtonPressedEventHandler(this.ButtonEdit1_ButtonClick);
    this.buttonEdit1.TextChanged += new EventHandler(this.ButtonEdit1_TextChanged);
    componentResourceManager.ApplyResources((object) this.lbType, "lbType");
    this.lbType.Name = "lbType";
    componentResourceManager.ApplyResources((object) this.cbType, "cbType");
    this.cbType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbType.FormattingEnabled = true;
    this.cbType.Name = "cbType";
    this.cbType.SelectedIndexChanged += new EventHandler(this.CbType_SelectedIndexChanged);
    this.tabControl1.Controls.Add((Control) this.tabPage1);
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Controls.Add((Control) this.btnCancel);
    this.panel5.Controls.Add((Control) this.btnApply);
    this.panel5.Controls.Add((Control) this.label5);
    this.panel5.Controls.Add((Control) this.label6);
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.BackColor = SystemColors.ScrollBar;
    this.panel6.Controls.Add((Control) this.tabControl1);
    this.panel6.Name = "panel6";
    this.ilAddImages.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilAddImages.ImageStream");
    this.ilAddImages.TransparentColor = Color.Transparent;
    this.ilAddImages.Images.SetKeyName(0, "obj_type.png");
    this.ilAddImages.Images.SetKeyName(1, "archives.png");
    this.AutoScaleMode = AutoScaleMode.None;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.panel6);
    this.Controls.Add((Control) this.panel5);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (SelectionDialog);
    this.contextMenuStrip1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.tabPage1.ResumeLayout(false);
    this.tabPage1.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.buttonEdit1.Properties.EndInit();
    this.tabControl1.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.panel6.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
