
// Type: Intermech.Client.Core.ObjectCreator.Controls.ObjectRelationsControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>
/// Summary description for ObjectCreatorControlRelations.
/// </summary>
internal class ObjectRelationsControl : ObjectCreatorControl
{
  private Panel panel3;
  private Panel panel1;
  private Splitter splitter1;
  private Panel panel5;
  private ListBox listBox1;
  private ToolBar toolBar1;
  private ToolBarButton toolBarButtonAdd;
  private ToolBarButton toolBarButtonDel;
  private ToolBarButton toolBarButtonDelAll;
  private ContextMenu contextMenu1;
  private MenuItem menuItemAdd;
  private MenuItem menuItemDel;
  private MenuItem menuItemDelAll;
  private ImageList imageList1;
  private Label label1;
  private PictureBox pictureBox1;
  private ObjectPropertyGrid attrObjPropGrig;
  private IContainer components;

  /// <summary>Конструктор</summary>
  /// <param name="createdObject">Объект вспомогательного класса для работы с заготовкой </param>
  public ObjectRelationsControl(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    this.CreatedObject.ObjectTypeChanged += new CreatedObjectItem.OnObjectTypeChanged(this.ObjectTypePropertiesChange);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectRelationsControl));
    this.panel3 = new Panel();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.panel1 = new Panel();
    this.attrObjPropGrig = new ObjectPropertyGrid();
    this.splitter1 = new Splitter();
    this.panel5 = new Panel();
    this.listBox1 = new ListBox();
    this.contextMenu1 = new ContextMenu();
    this.menuItemAdd = new MenuItem();
    this.menuItemDel = new MenuItem();
    this.menuItemDelAll = new MenuItem();
    this.toolBar1 = new ToolBar();
    this.toolBarButtonAdd = new ToolBarButton();
    this.toolBarButtonDel = new ToolBarButton();
    this.toolBarButtonDelAll = new ToolBarButton();
    this.imageList1 = new ImageList();
    this.panel3.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panel1.SuspendLayout();
    this.panel5.SuspendLayout();
    this.SuspendLayout();
    this.panel3.Controls.Add((Control) this.label1);
    this.panel3.Controls.Add((Control) this.pictureBox1);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ForeColor = SystemColors.GrayText;
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.panel1.Controls.Add((Control) this.attrObjPropGrig);
    this.panel1.Controls.Add((Control) this.splitter1);
    this.panel1.Controls.Add((Control) this.panel5);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.attrObjPropGrig.CommandsActiveLinkColor = SystemColors.ActiveCaption;
    this.attrObjPropGrig.CommandsDisabledLinkColor = SystemColors.ControlDark;
    this.attrObjPropGrig.CommandsLinkColor = SystemColors.ActiveCaption;
    componentResourceManager.ApplyResources((object) this.attrObjPropGrig, "attrObjPropGrig");
    this.attrObjPropGrig.InternalMenuEnabled = true;
    this.attrObjPropGrig.LineColor = SystemColors.ScrollBar;
    this.attrObjPropGrig.LockTypeChange = true;
    this.attrObjPropGrig.Name = "attrObjPropGrig";
    this.attrObjPropGrig.ToolbarVisible = false;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panel5.Controls.Add((Control) this.listBox1);
    this.panel5.Controls.Add((Control) this.toolBar1);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    this.listBox1.ContextMenu = this.contextMenu1;
    componentResourceManager.ApplyResources((object) this.listBox1, "listBox1");
    this.listBox1.Name = "listBox1";
    this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
    this.contextMenu1.MenuItems.AddRange(new MenuItem[3]
    {
      this.menuItemAdd,
      this.menuItemDel,
      this.menuItemDelAll
    });
    this.menuItemAdd.Index = 0;
    componentResourceManager.ApplyResources((object) this.menuItemAdd, "menuItemAdd");
    this.menuItemAdd.Click += new EventHandler(this.menuItemAdd_Click);
    componentResourceManager.ApplyResources((object) this.menuItemDel, "menuItemDel");
    this.menuItemDel.Index = 1;
    this.menuItemDel.Click += new EventHandler(this.menuItemDel_Click);
    componentResourceManager.ApplyResources((object) this.menuItemDelAll, "menuItemDelAll");
    this.menuItemDelAll.Index = 2;
    this.menuItemDelAll.Click += new EventHandler(this.menuItemDelAll_Click);
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.Buttons.AddRange(new ToolBarButton[3]
    {
      this.toolBarButtonAdd,
      this.toolBarButtonDel,
      this.toolBarButtonDelAll
    });
    this.toolBar1.ImageList = this.imageList1;
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.ButtonClick += new ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
    componentResourceManager.ApplyResources((object) this.toolBarButtonAdd, "toolBarButtonAdd");
    this.toolBarButtonAdd.Name = "toolBarButtonAdd";
    componentResourceManager.ApplyResources((object) this.toolBarButtonDel, "toolBarButtonDel");
    this.toolBarButtonDel.Name = "toolBarButtonDel";
    componentResourceManager.ApplyResources((object) this.toolBarButtonDelAll, "toolBarButtonDelAll");
    this.toolBarButtonDelAll.Name = "toolBarButtonDelAll";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "add.ico");
    this.imageList1.Images.SetKeyName(1, "delete.ico");
    this.imageList1.Images.SetKeyName(2, "deleteAll.ico");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel3);
    this.Name = nameof (ObjectRelationsControl);
    this.panel3.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panel1.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Обновление данных (обновление и заполнение элементов управления)
  /// </summary>
  /// <param name="args"></param>
  /// <returns>Если обновление прошло успешно - true, иначе - false</returns>
  public override bool Refresh(PageRefreshArgs args)
  {
    this.listBox1.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> intList = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, this.CreatedObject.ObjectTypeID, -1).Rows)
      {
        int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        if (!intList.Contains(int32) && Convert.ToInt32(row["F_MIN_LINKS"]) != -1)
          intList.Add(int32);
      }
      List<long> longList = new List<long>();
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        DataTable dataTable = sessionKeeper.Session.GetRelationCollection(intList[index1]).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID
        }), this.CreatedObject.ObjectID);
        for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
        {
          long int64 = Convert.ToInt64(dataTable.Rows[index2][0]);
          if (!longList.Contains(int64))
            longList.Add(int64);
        }
      }
      for (int index = 0; index < longList.Count; ++index)
        this.listBox1.Items.Add((object) new ObjectRelationsControl.LocalRelation(longList[index], this.CreatedObject.ObjectID));
    }
    if (this.listBox1.Items.Count > 0)
      this.listBox1.SelectedIndex = 0;
    return base.Refresh(args);
  }

  /// <summary>Сохранение данных</summary>
  /// <param name="args"></param>
  /// <returns>Если сохранение прошло успешно - true, иначе - false</returns>
  public override bool Save(PageSaveArgs args)
  {
    this.attrObjPropGrig.Save();
    return base.Save(args);
  }

  /// <summary>обновить иконку и текст для типа при его изменении</summary>
  public void ObjectTypePropertiesChange()
  {
    this.pictureBox1.Image = this.CreatedObject.ObjectTypeImage;
    this.label1.Text = this.CreatedObject.ObjectTypeCaption;
  }

  /// <summary>
  /// Обновление доступности элементов управления относящихся к связям
  /// </summary>
  private void UpdateRelationsControls()
  {
    this.toolBarButtonDelAll.Enabled = this.menuItemDelAll.Enabled = this.listBox1.Items.Count > 0;
    this.toolBarButtonDel.Enabled = this.menuItemDel.Enabled = this.attrObjPropGrig.Enabled = this.listBox1.SelectedIndex > -1;
    ObjectRelationsControl.LocalRelation localRelation = this.listBox1.SelectedIndex > -1 ? (ObjectRelationsControl.LocalRelation) this.listBox1.Items[this.listBox1.SelectedIndex] : (ObjectRelationsControl.LocalRelation) null;
    if (localRelation != null)
      this.attrObjPropGrig.Load(localRelation.relID, AttributableElements.Relation, ClientConsts.GetAttributeValuesModes, true);
    else
      this.attrObjPropGrig.SelectedObject = (object) null;
  }

  /// <summary>Добавление новой связи</summary>
  private void AddRelation()
  {
    int num = this.listBox1.SelectedIndex;
    long[] numArray = ObjectRelationsControl.NewRelationCreator.AddNewRelationsInDialog(this.CreatedObject.ObjectID, this.CreatedObject.ObjectTypeID);
    if (numArray != null && numArray.Length != 0)
    {
      foreach (long aRelationID in numArray)
        num = this.listBox1.Items.Add((object) new ObjectRelationsControl.LocalRelation(aRelationID, this.CreatedObject.ObjectID));
    }
    this.listBox1.SelectedIndex = num;
  }

  /// <summary>Удаление выбранной связи</summary>
  private void DelRelation()
  {
    if (this.listBox1.SelectedItem == null)
      return;
    int selectedIndex = this.listBox1.SelectedIndex;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.GetRelation(((ObjectRelationsControl.LocalRelation) this.listBox1.SelectedItem).relID)?.Delete(0L);
      this.listBox1.Items.Remove(this.listBox1.SelectedItem);
      this.listBox1.SelectedIndex = this.listBox1.Items.Count <= 0 || selectedIndex != 0 ? selectedIndex - 1 : 0;
    }
  }

  /// <summary>Удаление всех связей</summary>
  private void DelAllRelations()
  {
    if (this.listBox1.SelectedItem != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (object obj in this.listBox1.Items)
          sessionKeeper.Session.GetRelation(((ObjectRelationsControl.LocalRelation) obj).relID)?.Delete(0L);
        this.listBox1.SelectedIndex = -1;
        this.listBox1.Items.Clear();
      }
    }
    this.UpdateRelationsControls();
  }

  private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateRelationsControls();
  }

  private void menuItemAdd_Click(object sender, EventArgs e) => this.AddRelation();

  private void menuItemDel_Click(object sender, EventArgs e) => this.DelRelation();

  private void menuItemDelAll_Click(object sender, EventArgs e) => this.DelAllRelations();

  private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e.Button == this.toolBarButtonAdd)
      this.AddRelation();
    else if (e.Button == this.toolBarButtonDel)
    {
      this.DelRelation();
    }
    else
    {
      if (e.Button != this.toolBarButtonDelAll)
        return;
      this.DelAllRelations();
    }
  }

  /// <summary>
  /// Локальный класс для вызова диалога выбора объектов для создания связей
  /// </summary>
  private class NewRelationCreator
  {
    /// <summary>Получить список допустимых для связи типов объектов</summary>
    /// <param name="objectTypeId">Идентификатор типа объекта, для которого будут создаваться связи</param>
    /// <returns>Список допустимых для связи типов объектов (и идентификаторов типов связей)</returns>
    private static ArrayList GetRelationsAppTypes(int objectTypeId)
    {
      ArrayList relationsAppTypes = new ArrayList();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
        if (applicabilityCollection != null)
        {
          DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, objectTypeId, -1);
          if (applicabilitiesList != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
              relationsAppTypes.Add((object) new ObjectRelationsControl.NewRelationCreator.LocaleLink(Convert.ToInt32(row["F_INOBJECT_TYPE"]), Convert.ToInt32(row["F_RELATION_TYPE"])));
          }
        }
      }
      return relationsAppTypes;
    }

    /// <summary>Выбор объектов</summary>
    /// <param name="objectTypesIds">Типы объектов, которые будут доступны в диалоге выбора</param>
    /// <returns>Массив идентификаторов выбранных объектов</returns>
    private static long[] SelectObjectsDialog(int[] objectTypesIds)
    {
      long[] numArray = (long[]) null;
      if (objectTypesIds != null && objectTypesIds.Length != 0)
      {
        DescriptorCollection descriptors = new DescriptorCollection();
        foreach (int objectTypesId in objectTypesIds)
          descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectTypesId));
        IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_283"), descriptors);
        if (SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_852"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default) is IDBObjectID[] dbObjectIdArray && dbObjectIdArray.Length != 0)
        {
          numArray = new long[dbObjectIdArray.Length];
          for (int index = 0; index < dbObjectIdArray.Length; ++index)
            numArray[index] = dbObjectIdArray[index].Value;
        }
      }
      return numArray;
    }

    /// <summary>
    /// Локльная функция для получения непосредственных child-ов для типа объекта
    /// </summary>
    /// <param name="objectTypeId">Идентификатор типа объекта</param>
    /// <param name="relationTypeId">Идентификатор типа связи</param>
    /// <param name="childArrayList">список в который производится добавление</param>
    private static void GetAllChilds(
      int objectTypeId,
      int relationTypeId,
      ArrayList childArrayList)
    {
      foreach (DataRow row in (InternalDataCollectionBase) DataHolders.ObjectTypesHolder.LoadData(false, (object) objectTypeId).Rows)
      {
        int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
        childArrayList.Add((object) new ObjectRelationsControl.NewRelationCreator.LocaleLink(int32, relationTypeId));
        ObjectRelationsControl.NewRelationCreator.GetAllChilds(int32, relationTypeId, childArrayList);
      }
    }

    /// <summary>Добавление новой связи в режиме диалога</summary>
    /// <param name="objectID"></param>
    /// <param name="objectTypeID">Тип объекта для которого будет добавляться связь</param>
    /// <returns>Массив идентификаторов созданных связей</returns>
    public static long[] AddNewRelationsInDialog(long objectID, int objectTypeID)
    {
      ArrayList relationsAppTypes = ObjectRelationsControl.NewRelationCreator.GetRelationsAppTypes(objectTypeID);
      List<int> intList = new List<int>();
      for (int index = 0; index < relationsAppTypes.Count; ++index)
      {
        int objectTypeId = ((ObjectRelationsControl.NewRelationCreator.LocaleLink) relationsAppTypes[index]).ObjectTypeID;
        if (!intList.Contains(objectTypeId))
          intList.Add(objectTypeId);
      }
      if (intList.Count == 0)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_853"));
        return (long[]) null;
      }
      long[] numArray = ObjectRelationsControl.NewRelationCreator.SelectObjectsDialog(intList.ToArray());
      ArrayList childArrayList = new ArrayList();
      foreach (object obj in relationsAppTypes)
      {
        childArrayList.Add(obj);
        ObjectRelationsControl.NewRelationCreator.GetAllChilds(((ObjectRelationsControl.NewRelationCreator.LocaleLink) obj).ObjectTypeID, ((ObjectRelationsControl.NewRelationCreator.LocaleLink) obj).RelationTypeID, childArrayList);
      }
      if (numArray == null || numArray.Length == 0)
        return (long[]) null;
      Hashtable hashtable = new Hashtable();
      for (int index = 0; index < childArrayList.Count; ++index)
      {
        int objectTypeId = ((ObjectRelationsControl.NewRelationCreator.LocaleLink) childArrayList[index]).ObjectTypeID;
        int relationTypeId = ((ObjectRelationsControl.NewRelationCreator.LocaleLink) childArrayList[index]).RelationTypeID;
        if (hashtable.ContainsKey((object) objectTypeId))
        {
          if (hashtable[(object) objectTypeId] != null && (int) hashtable[(object) objectTypeId] != relationTypeId)
            hashtable[(object) objectTypeId] = (object) null;
        }
        else
          hashtable.Add((object) objectTypeId, (object) relationTypeId);
      }
      List<long> longList = new List<long>(numArray.Length);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long num in numArray)
        {
          int relationType = -1;
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(num);
          int key = dbObject1 != null ? dbObject1.ObjectType : -1;
          if (key != -1)
          {
            if (hashtable[(object) key] != null)
            {
              relationType = Convert.ToInt32(hashtable[(object) key]);
            }
            else
            {
              ArrayList arrayList = new ArrayList();
              foreach (object obj in childArrayList)
              {
                if (key == ((ObjectRelationsControl.NewRelationCreator.LocaleLink) obj).ObjectTypeID)
                  arrayList.Add((object) ((ObjectRelationsControl.NewRelationCreator.LocaleLink) obj).RelationTypeID);
              }
              int[] relationTypesIds = new int[arrayList.Count];
              for (int index = 0; index < arrayList.Count; ++index)
                relationTypesIds[index] = Convert.ToInt32(arrayList[index]);
              string objectCaption = "";
              string objectTypeCaption = "";
              IDBObject dbObject2 = sessionKeeper.Session.GetObject(objectID);
              if (dbObject2 != null)
              {
                objectCaption = dbObject2.Caption;
                IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject2.ObjectType);
                if (objectType != null)
                  objectTypeCaption = objectType.ObjectTypeName;
              }
              bool useForAll = false;
              relationType = NewRelationDialog.GetRelationID(objectCaption, objectTypeCaption, relationTypesIds, out useForAll);
              if (relationType == -1)
                return (long[]) null;
              if (useForAll)
                hashtable[(object) key] = (object) relationType;
            }
          }
          IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(relationType).Create(num, objectID);
          if (dbRelation != null)
            longList.Add(dbRelation.RelationID);
        }
      }
      return longList.ToArray();
    }

    /// <summary>
    /// Локальная структура для хранения связки "Тип объекта" - "Тип связи"
    /// </summary>
    private struct LocaleLink(int objectTypeID, int relationTypeID)
    {
      /// <summary>
      /// идентификатор объекта, с которым необходимо создать связь
      /// </summary>
      public int ObjectTypeID = objectTypeID;
      /// <summary>идентификатор типа связи, которую нужно создать</summary>
      public int RelationTypeID = relationTypeID;
    }
  }

  /// <summary>
  /// Локальный класс для работы с созданными связями нового объекта
  /// </summary>
  private class LocalRelation
  {
    /// <summary>идентификатор связи</summary>
    public long relID;
    /// <summary>
    /// идентификатор объекта, по отношению к которому рассматривается связь
    /// </summary>
    public long objectID;

    /// <summary>Конструктор</summary>
    /// <param name="aRelationID">Идентификатор связи</param>
    /// <param name="consideredObjId">Идентификатор объекта, по отношению к которому рассматривается связь</param>
    public LocalRelation(long aRelationID, long consideredObjId)
    {
      this.relID = aRelationID;
      this.objectID = consideredObjId;
    }

    /// <summary>Конструктор</summary>
    /// <param name="aRelationID">Идентификатор связи</param>
    public LocalRelation(long aRelationID)
      : this(aRelationID, -1L)
    {
    }

    /// <summary>Перекрытый метод ToString()</summary>
    /// <returns>Строковое представление локального объекта связи</returns>
    public override string ToString()
    {
      string str = " ";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this.relID);
        if (relation != null)
        {
          IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
          IDBRelationType relationType = sessionKeeper.Session.GetRelationType(relation.RelationType);
          if (this.objectID != -1L)
          {
            if (relation.ProjID == this.objectID)
            {
              IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(relation.PartID, service.Filtration.OwnerID, true);
              str = $"{relationType.TypeName} {CaptionTransform.GetCaption(objectByVersionsRule.Caption, (long) objectByVersionsRule.VersionID)}";
            }
            else
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(relation.ProjID);
              str = $"{relationType.ReverseName} {CaptionTransform.GetCaption(dbObject.Caption, (long) dbObject.VersionID)}";
            }
          }
          else
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(relation.ProjID);
            IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(relation.PartID, service.Filtration.OwnerID, true);
            str = $"{CaptionTransform.GetCaption(dbObject.Caption, (long) dbObject.VersionID)} {relationType.TypeName} {CaptionTransform.GetCaption(objectByVersionsRule.Caption, (long) objectByVersionsRule.VersionID)}";
          }
        }
      }
      return str;
    }
  }
}
