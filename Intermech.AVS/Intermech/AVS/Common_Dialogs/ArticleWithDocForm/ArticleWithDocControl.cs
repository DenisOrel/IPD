// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

internal class ArticleWithDocControl : UserControl, IContainerControl
{
  /// <summary>Провайдер сервисов</summary>
  private AVSWindow _avsWin;
  /// <summary>Ссылки на закладки формы</summary>
  private ArticleWithDocControl.Pages _pages;
  /// <summary>
  /// Список идентификаторов родительских изделий для создаваемого/просматриваемого изделия
  /// </summary>
  internal List<long> _parentIDs;
  /// <summary>
  /// Тип создаваемого/просматриваемого изделия
  /// (необходим для дальнейшего поиска по обозначению)
  /// </summary>
  internal int _articleType = -1;
  /// <summary>Список созданных связей</summary>
  internal ArticleWithDocControl.CreatedRelations _notifications;
  /// <summary>Режим открытия формы</summary>
  private OpenModes _mode;
  /// <summary>Тип отображаемой формы</summary>
  private FormType _formType;
  /// <summary>Пара изделие/документ, имеет смысл в режиме создания</summary>
  private CreatedPair _pair;
  /// <summary>Общие данные для закладок</summary>
  private IFormCommonData _commonData;
  private bool _changed;
  /// <summary>Идентификатор изделия (оно есть по любому)</summary>
  private long _articleID;
  internal TabControl tcMain;
  internal TabPage tpMasterData;
  internal TabPage tpArticle;
  internal TabPage tpDocument;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  internal Button bCancel;
  internal Button bOK;
  private ArticleWithDocControlTabPagesControl tabPagesControl;

  public ArticleWithDocControl()
  {
    this.InitializeComponent();
    this.tcMain = this.tabPagesControl.tcMain;
    this.tpMasterData = this.tabPagesControl.tpMasterData;
    this.tpArticle = this.tabPagesControl.tpArticle;
    this.tpDocument = this.tabPagesControl.tpDocument;
  }

  protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  /// <summary>Пара изделие/документ</summary>
  internal CreatedPair Pair => this._pair;

  internal IFormCommonData CommonData
  {
    get => this._commonData;
    set => this._commonData = value;
  }

  /// <summary>Флаг, изменились ли данные во вьюшке</summary>
  public bool Changed => this._changed;

  /// <summary>Значение атрибута формат</summary>
  public string Format => this._commonData.Format;

  internal void Init(OpenModes mode, AVSWindow avsWindow)
  {
    this.ChangeMode(mode);
    this._pair = new CreatedPair();
    this._notifications = new ArticleWithDocControl.CreatedRelations();
    this._avsWin = avsWindow;
  }

  private void ReloadCommonData(long articleID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.ReloadCommonData(sessionKeeper.Session.GetObject(articleID));
  }

  private void ReloadCommonData(IDBObject article)
  {
    if (this._commonData != null)
      this._commonData.Changed -= new CommonDataChangedDelegate(this.commonData_Changed);
    this._commonData = (IFormCommonData) new FormCommonData();
    IDBAttribute byId1 = article.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"));
    this._commonData.Designation = byId1 != null ? byId1.AsString : string.Empty;
    if (byId1 != null)
      this._commonData.SetReadOnly("Designation", MetaDataHelper.GetAttribute4ObjectType(article.ObjectType, byId1.AttributeID).Options.HasFlag((Enum) AttributeOptions.DisableManualEdit));
    IDBAttribute byId2 = article.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute byId3 = article.Attributes.FindByID(this._avsWin.AVSDocument.Attr_UserAttributeForNameField.AttributeId);
    this._commonData.FullName = byId3 != null ? byId3.AsString : string.Empty;
    this._commonData.Name = byId2 != null ? byId2.AsString : string.Empty;
    if (byId2 != null)
      this._commonData.SetReadOnly("Name", MetaDataHelper.GetAttribute4ObjectType(article.ObjectType, byId2.AttributeID).Options.HasFlag((Enum) AttributeOptions.DisableManualEdit));
    IDBAttribute byId4 = article.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0038a-306c-11d8-b4e9-00304f19f545"));
    this._commonData.OKPCode = byId4 != null ? byId4.AsString : string.Empty;
    IDBAttribute byId5 = article.Attributes.FindByID(FormHelper.AttributeMaterialID);
    if (byId5 != null)
    {
      object obj = byId5.Value;
      if (!(obj is DBNull))
      {
        long int64 = Convert.ToInt64(obj);
        QuickObjectInfo objectInfo = article.Session.GetObjectInfo(int64);
        if (!objectInfo.Empty)
          this._commonData.Material = new MaterialInfo(int64, objectInfo.Caption);
      }
    }
    if (this._avsWin != null && this._mode != OpenModes.Create)
    {
      AVSRow avsRow = this._avsWin.GetSelectedSpecRows(false).FirstOrDefault<AVSRow>();
      if (avsRow != null)
      {
        if (this._pair.DocumentID.IsUndefinedId())
          this._commonData.Format = avsRow.GetFieldStringValue(avsRow.Field_Format, 0, -1, (List<RelationAttributeValuesCache>) null, true);
        this._commonData.PosDesignation = avsRow.GetFieldStringValue(avsRow.Field_PosDesignation, 0, -1, (List<RelationAttributeValuesCache>) null, true);
        this._commonData.Smotri = avsRow.GetFieldStringValue(avsRow.Attr_Smotri, -1, -1, (List<RelationAttributeValuesCache>) null, false);
      }
    }
    this._commonData.Changed += new CommonDataChangedDelegate(this.commonData_Changed);
  }

  /// <summary>Инициализация диалога</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="article">Изделие</param>
  /// <param name="document">Документ, для БЧ null</param>
  /// <param name="relations">Связи с родительскими изделиями (м.б. 1 и более )</param>
  /// <param name="formType">Тип отображаемого диалога</param>
  public void Initialize(
    IUserSession session,
    IDBObject article,
    IDBObject document,
    List<IDBRelation> relations,
    FormType formType)
  {
    FormHelper.Init(formType);
    this._formType = formType;
    this._articleID = article.ObjectID;
    this.ReloadCommonData(article);
    ClassificatedObjects classifObjects = new ClassificatedObjects();
    classifObjects.articleID = article.ObjectID;
    classifObjects.articleType = article.ObjectType;
    classifObjects.articleReadOnly = article.ReadOnly;
    if (document != null)
    {
      classifObjects.documentID = document.ObjectID;
      classifObjects.documentType = document.ObjectType;
      classifObjects.documentReadOnly = document.ReadOnly;
    }
    CommonDataType disableControls = CommonDataType.None;
    if (article.ReadOnly || document != null && document.ReadOnly)
      disableControls = CommonDataType.Designation | CommonDataType.Name | CommonDataType.OKPCode;
    if (article.GetAttributeByID(AvsIDCache.Attr_BasedOnCADModel) != null)
      disableControls = CommonDataType.Designation | CommonDataType.Name | CommonDataType.OKPCode;
    if (article.ReadOnly)
      disableControls = disableControls | CommonDataType.Material | CommonDataType.Size;
    List<AVSRow> selectedSpecRows = (List<AVSRow>) null;
    if (this._avsWin != null && this._mode != OpenModes.Create && !this.Pair.NewRelations)
    {
      selectedSpecRows = this._avsWin.GetSelectedSpecRows(false);
      this._commonData.Changed += new CommonDataChangedDelegate(this.commonData_Changed);
    }
    ArticleWithDocControl.Pages pages = this._pages;
    this._pages = new ArticleWithDocControl.Pages();
    IDBRelation relation = (IDBRelation) null;
    if (relations.Count > 0)
      relation = relations[0];
    switch (formType)
    {
      case FormType.Single:
        SingleMasterControl page1 = pages != null ? pages.GetPage(typeof (SingleMasterControl)) as SingleMasterControl : (SingleMasterControl) null;
        if (page1 == null)
          page1 = new SingleMasterControl(relation, classifObjects, selectedSpecRows, disableControls);
        else
          page1.Init(relation, classifObjects, selectedSpecRows, disableControls);
        this._pages.AddPage((IPageControl) page1, (Control) this.tpMasterData);
        break;
      case FormType.GroupB:
      case FormType.Autoprom_GroupB:
        GroupMasterControl page2 = pages != null ? pages.GetPage(typeof (GroupMasterControl)) as GroupMasterControl : (GroupMasterControl) null;
        if (page2 == null)
          page2 = new GroupMasterControl(relations, classifObjects, disableControls, selectedSpecRows);
        else
          page2.Init(relations, classifObjects, disableControls, selectedSpecRows);
        page2.FormType = formType;
        this._pages.AddPage((IPageControl) page2, (Control) this.tpMasterData);
        break;
      case FormType.NonDraft:
      case FormType.Autoprom_NonDraft:
        NonDraftMasterControl page3 = pages != null ? pages.GetPage(typeof (NonDraftMasterControl)) as NonDraftMasterControl : (NonDraftMasterControl) null;
        if (page3 == null)
          page3 = new NonDraftMasterControl(relation, classifObjects, selectedSpecRows, disableControls, article);
        else
          page3.Init(relation, classifObjects, selectedSpecRows, disableControls, article);
        page3.FormType = formType;
        this._pages.AddPage((IPageControl) page3, (Control) this.tpMasterData);
        break;
      case FormType.NonDraftB:
      case FormType.Autoprom_NonDraftB:
        NonDraftGroupMasterControl page4 = pages != null ? pages.GetPage(typeof (NonDraftGroupMasterControl)) as NonDraftGroupMasterControl : (NonDraftGroupMasterControl) null;
        if (page4 == null)
          page4 = new NonDraftGroupMasterControl(relations, classifObjects, disableControls, selectedSpecRows);
        else
          page4.Init(relations, classifObjects, disableControls, selectedSpecRows);
        page4.FormType = formType;
        this._pages.AddPage((IPageControl) page4, (Control) this.tpMasterData);
        break;
      case FormType.Autoprom_Single:
        if (relations.Count > 0)
        {
          AutoMasterControl page5 = pages != null ? pages.GetPage(typeof (AutoMasterControl)) as AutoMasterControl : (AutoMasterControl) null;
          if (page5 == null)
            page5 = new AutoMasterControl(relations[0], classifObjects, selectedSpecRows, disableControls);
          else
            page5.Init(relations[0], classifObjects, selectedSpecRows, disableControls);
          this._pages.AddPage((IPageControl) page5, (Control) this.tpMasterData);
          break;
        }
        break;
    }
    if (this._pages.Items.Count > 0)
    {
      this._pages.Items[0].ClassificatedEvent += new ClassificatedEventHandler(this.ClassificatedEvent);
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
      bool flag = true;
      if (article != null && !childrenIdRecursive.Contains(article.ObjectType))
      {
        flag = false;
        this.tcMain.TabPages.Remove(this.tpArticle);
      }
      if (flag)
      {
        ArticleControl page6 = pages != null ? pages.GetPage(typeof (ArticleControl)) as ArticleControl : (ArticleControl) null;
        if (page6 == null)
          page6 = new ArticleControl(article, disableControls, this._pages.Items[0]);
        else
          page6.Init(article, disableControls, this._pages.Items[0]);
        this._pages.AddPage((IPageControl) page6, (Control) this.tpArticle);
      }
      if (formType != FormType.NonDraft && formType != FormType.NonDraftB && document != null)
      {
        if (!(pages?.GetPage(typeof (DocumentControl)) is DocumentControl page7))
          page7 = new DocumentControl(document, disableControls, this._pages.Items[0]);
        else
          page7.Init(document, disableControls, this._pages.Items[0]);
        this._pages.AddPage((IPageControl) page7, (Control) this.tpDocument);
      }
    }
    if (formType == FormType.NonDraft || formType == FormType.NonDraftB || document == null)
      this.tcMain.TabPages.Remove(this.tpDocument);
    foreach (IPageControl pageControl in this._pages.Items)
    {
      pageControl.CommonData = this._commonData;
      pageControl.Reload(session, this._mode);
      pageControl.Changed += new EventHandler(this.page_Changed);
      pageControl.ReloadData += new EventHandler(this.page_ReloadData);
    }
    if (pages != null)
    {
      foreach (IPageControl pageControl in pages.Items)
      {
        if (!this._pages.Items.Contains(pageControl))
          (pageControl as Control).Parent = (Control) null;
      }
    }
    if (this._mode == OpenModes.View || this._mode == OpenModes.CreateAdd)
    {
      if (article != null)
        this._pair.ArticleID = article.ObjectID;
      if (this._formType != FormType.NonDraft && document != null)
        this._pair.DocumentID = document.ObjectID;
    }
    if (this._mode == OpenModes.InView)
    {
      this.bOK.Enabled = this.bCancel.Enabled = false;
      foreach (IPageControl pageControl in this._pages.Items)
        pageControl.AutoNotifications = true;
    }
    this._changed = false;
  }

  internal void page_ReloadData(object sender, EventArgs e)
  {
    this.ReloadCommonData(this._articleID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IPageControl pageControl in this._pages.Items)
      {
        pageControl.CommonData = this._commonData;
        pageControl.Reload(sessionKeeper.Session, this._mode);
      }
    }
    if (this._mode == OpenModes.InView)
      this.bOK.Enabled = this.bCancel.Enabled = false;
    this._changed = false;
  }

  private void ClassificatedEvent(object sender, ClassificatedEventArgs args)
  {
    for (int index = 1; index < this._pages.Items.Count; ++index)
      this._pages.Items[index].OnSetClassifyAttributes(args.Classifier, args.ClassifierID);
  }

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.Dock = DockStyle.None;
      this.Visible = false;
    }
    else
    {
      this.Dock = DockStyle.Fill;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  /// <summary>Метод обработки события изменения общих данных</summary>
  /// <param name="attributeID">ID атрибута</param>
  /// <param name="value">Значение</param>
  /// <returns></returns>
  private bool OnSearchAttributeChanged(int attributeID, object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IFiltrationService service1 = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      string str = (string) null;
      if (attributeID == AvsIDCache.Attr_Designation)
        str = Convert.ToString(value);
      long num = -1;
      DialogResult dialogResult = DialogResult.Yes;
      if (attributeID == AvsIDCache.Attr_Designation)
      {
        string designation = Convert.ToString(value);
        if (ServicesManager.GetService(typeof (IArticleService)) is IArticleService service2)
        {
          num = service2.FindArticleID(designation, (string) null, (string) null, service1.FiltrationServiceOwnerID, (object) sessionKeeper.Session);
          switch (num)
          {
            case -1:
            case 0:
              return false;
            default:
              dialogResult = MessageBox.Show($"В базе данных найдено изделие с обозначением \"{designation}\". Использовать найденное изделие?", string.Format("Изменение обозначения"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
              break;
          }
        }
        else
        {
          ConditionStructure conditionStructure = new ConditionStructure(attributeID, RelationalOperators.Equal, value, LogicalOperators.AND, 0, false);
          IDBObjectCollection objectCollection = !MetaDataHelper.IsObjectTypeChildOf(this._articleType, AvsIDCache.ObjType_Product) ? sessionKeeper.Session.GetObjectCollection(this._articleType) : sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_Product);
          objectCollection.ShowAllModifications = true;
          DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
          {
            conditionStructure
          }, new object[1]{ (object) -2 }));
          if (dataTable.Rows.Count == 0)
            return false;
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributeID);
          dialogResult = MessageBox.Show($"В базе данных найдено изделие с таким же значением атрибута \"{attributeType.Name}\". Использовать найденное изделие?", $"Изменение атрибута \"{attributeType.Name}\"", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
          num = Convert.ToInt64(dataTable.Rows[0][0]);
        }
      }
      if (dialogResult == DialogResult.Yes)
      {
        IDBObject document = (IDBObject) null;
        if (this._formType != FormType.NonDraft)
          document = (ServicesManager.GetService(typeof (IArticleService)) as IArticleService).FindMainDocument(num, service1.FiltrationServiceOwnerID, (object) sessionKeeper.Session);
        IDBObject article = sessionKeeper.Session.GetObject(num);
        List<IDBRelation> relations = new List<IDBRelation>(this._parentIDs.Count);
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this._pair.RelationType);
        List<DBRelationsEventArgsFromForm> eventArgsFromFormList = new List<DBRelationsEventArgsFromForm>(this._parentIDs.Count);
        List<long> collection = new List<long>();
        foreach (long parentId in this._parentIDs)
        {
          IDBRelation dbRelation = relationCollection.Create(parentId, article.ObjectID);
          relations.Add(dbRelation);
          eventArgsFromFormList.Add(new DBRelationsEventArgsFromForm("RelationsCreated", dbRelation.RelationID));
          collection.Add(dbRelation.RelationID);
        }
        this._notifications.ParentRelations = eventArgsFromFormList;
        this.Pair.RelationIDs.Clear();
        this.Pair.RelationIDs.AddRange((IEnumerable<long>) collection);
        this.Pair.NewRelations = true;
        this.ChangeMode(OpenModes.CreateAdd);
        foreach (IPageControl pageControl in this._pages.Items)
        {
          pageControl.Changed -= new EventHandler(this.page_Changed);
          pageControl.SetParent((Control) null);
        }
        this.Initialize(sessionKeeper.Session, article, document, relations, this._formType);
        return true;
      }
    }
    return false;
  }

  /// <summary>Вызывается при изменении режима открытия</summary>
  /// <param name="mode"></param>
  internal void ChangeMode(OpenModes mode)
  {
    this._mode = mode;
    switch (mode)
    {
      case OpenModes.Create:
        this.Text = "Создание записи";
        break;
      case OpenModes.View:
        this.Text = "Выбор записи";
        break;
      case OpenModes.CreateAdd:
        this.Text = "Выбор записи";
        break;
      case OpenModes.InView:
        this.bOK.Text = "Применить";
        break;
      case OpenModes.InViewReadOnly:
        this.bOK.Text = "Применить";
        this.bOK.Visible = false;
        this.bCancel.Visible = false;
        break;
    }
  }

  /// <summary>Общие данные изменились</summary>
  /// <param name="sender">Отправитель сообщения</param>
  /// <param name="args">Аргумент</param>
  private void commonData_Changed(object sender, CommonDataChangedEventArgs args)
  {
    if (this._mode == OpenModes.Create)
    {
      if (args.Type == CommonDataType.Designation && this._commonData.Designation != string.Empty)
      {
        if (this.OnSearchAttributeChanged(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) this._commonData.Designation))
          return;
      }
      else if (args.Type == CommonDataType.OKPCode && this._commonData.OKPCode != string.Empty && (this._formType == FormType.Autoprom_Single || this._formType == FormType.Autoprom_GroupB || this._formType == FormType.Autoprom_NonDraft || this._formType == FormType.Autoprom_NonDraftB) && this.OnSearchAttributeChanged(MetaDataHelper.GetAttributeTypeID("cad0038a-306c-11d8-b4e9-00304f19f545"), (object) this._commonData.OKPCode))
        return;
    }
    foreach (IPageControl pageControl in this._pages.Items)
      pageControl.CommonDataChanged(args.Type);
  }

  /// <summary>Данные в закладках изменились</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void page_Changed(object sender, EventArgs e)
  {
    if (this._mode == OpenModes.InView)
      this.bOK.Enabled = this.bCancel.Enabled = true;
    this._changed = true;
  }

  public void Save()
  {
    this._pair.ArticleID = this._articleID;
    this._commonData.Check();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService1 = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService1.StartTransaction();
      try
      {
        System.Type o;
        switch (this._mode)
        {
          case OpenModes.Create:
            o = typeof (DocumentControl);
            break;
          default:
            o = typeof (ArticleControl);
            break;
        }
        List<IPageControl> pageControlList = new List<IPageControl>(this._pages.Items.Count);
        IPageControl pageControl1 = (IPageControl) null;
        foreach (IPageControl pageControl2 in this._pages.Items)
        {
          if (pageControl2.GetType().Equals(o))
            pageControl1 = pageControl2;
          else
            pageControlList.Add(pageControl2);
        }
        if (pageControl1 != null)
          pageControlList.Add(pageControl1);
        foreach (IPageControl pageControl3 in pageControlList)
          pageControl3.Save(sessionKeeper.Session, this._mode, this._pair);
        if (pageControlList.Count < 2 && this._articleID != 0L && this._articleID != -1L)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._articleID);
          dbObject.SetAttributesValues(new AttributeValues[2]
          {
            new AttributeValues(AvsIDCache.Attr_Name, (object) this.CommonData.Name),
            new AttributeValues(AvsIDCache.Attr_Designation, (object) this.CommonData.Designation)
          });
          if (this._mode == OpenModes.Create)
          {
            dbObject.CommitCreation(false);
            if (this.CommonData.ClassifierID != 0L && sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService2)
            {
              // ISSUE: variable of a boxed type
              __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
              long classifierId = this.CommonData.ClassifierID;
              long[] objectIDs = new long[1]
              {
                dbObject.ObjectID
              };
              customService2.IncludeObjects((object) sessionGuid, classifierId, objectIDs);
            }
            try
            {
              this._pair.ArticleID = this._articleID = dbObject.CheckOut(false).ObjectID;
            }
            catch (Exception ex)
            {
            }
          }
        }
        customService1.Commit();
        if (this._avsWin != null)
        {
          if (this._mode != OpenModes.Create)
          {
            List<AVSRow> selectedSpecRows = this._avsWin.GetSelectedSpecRows(false);
            if (selectedSpecRows.Count > 0)
            {
              if (selectedSpecRows[0].HasDocNodes)
                selectedSpecRows[0].TextLinkToMainDocument = this._commonData.Smotri;
            }
          }
        }
      }
      catch
      {
        customService1.Rollback();
        throw;
      }
    }
    this._changed = false;
  }

  /// <summary>Нажали клавишу OK</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bOK_Click(object sender, EventArgs e)
  {
    this.Save();
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._mode == OpenModes.Create || this._mode == OpenModes.View || this._mode == OpenModes.CreateAdd)
    {
      if (service != null)
      {
        if (this._mode == OpenModes.Create)
        {
          service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", this._pair.ArticleID));
          if (this._formType != FormType.NonDraft && this._pair.DocumentID != 0L && this._pair.DocumentID != -1L)
          {
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", this._pair.DocumentID));
            if (this._notifications.MainRelation != null)
              service.FireEvent((object) this, (NotificationEventArgs) this._notifications.MainRelation);
          }
        }
        if (this._notifications.ParentRelations != null)
        {
          foreach (DBRelationsEventArgsFromForm parentRelation in this._notifications.ParentRelations)
            service.FireEvent((object) this, (NotificationEventArgs) parentRelation);
        }
      }
      if (this.ParentForm is Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm)
      {
        this.ParentForm.DialogResult = DialogResult.OK;
        this.ParentForm.Close();
      }
    }
    else if (this._mode == OpenModes.InView)
    {
      this.ReloadCommonData(this._articleID);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (IPageControl pageControl in this._pages.Items)
        {
          pageControl.CommonData = this._commonData;
          pageControl.Reload(sessionKeeper.Session, this._mode);
        }
      }
      this.bOK.Enabled = this.bCancel.Enabled = false;
    }
    this._changed = false;
  }

  /// <summary>Нажали клавишу "Cancel"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bCancel_Click(object sender, EventArgs e)
  {
    if (this.Pair.NewRelations)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < this.Pair.RelationIDs.Count; ++index)
          sessionKeeper.Session.GetRelation(this.Pair.RelationIDs[index]).Delete(0L);
      }
      this.Pair.NewRelations = false;
      this.Pair.RelationIDs.Clear();
    }
    if ((this._mode == OpenModes.Create || this._mode == OpenModes.View || this._mode == OpenModes.CreateAdd) && this.ParentForm is Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm)
    {
      this.ParentForm.DialogResult = DialogResult.Cancel;
      this.ParentForm.Close();
    }
    if (this._mode == OpenModes.InView)
    {
      this.ReloadCommonData(this._articleID);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (IPageControl pageControl in this._pages.Items)
        {
          pageControl.CommonData = this._commonData;
          pageControl.Reload(sessionKeeper.Session, this._mode);
        }
      }
      this.bOK.Enabled = this.bCancel.Enabled = false;
    }
    this._changed = false;
  }

  bool IContainerControl.ActivateControl(Control active)
  {
    this.ActiveControl = active;
    return true;
  }

  Control IContainerControl.ActiveControl
  {
    get => this.ActiveControl;
    set => this.ActiveControl = value;
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
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.tabPagesControl = new ArticleWithDocControlTabPagesControl();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOK);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 412);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(604, 43);
    this.panel1.TabIndex = 2;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(479, 6);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.Location = new Point(352, 6);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.tabPagesControl.AutoScroll = true;
    this.tabPagesControl.Dock = DockStyle.Fill;
    this.tabPagesControl.Location = new Point(0, 0);
    this.tabPagesControl.Name = "tabPagesControl";
    this.tabPagesControl.Size = new Size(604, 412);
    this.tabPagesControl.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tabPagesControl);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ArticleWithDocControl);
    this.Size = new Size(604, 455);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Класс для хранения событий по созданию новых связей
  /// для дальнейшей их генерации
  /// </summary>
  internal class CreatedRelations
  {
    /// <summary>Связь между изделием и документом</summary>
    public DBRelationsEventArgsFromForm MainRelation;
    /// <summary>Связь между изделием и парентами</summary>
    public List<DBRelationsEventArgsFromForm> ParentRelations;
  }

  /// <summary>Отображаемые закладки</summary>
  private class Pages
  {
    /// <summary>Список закладок</summary>
    private List<IPageControl> _pages;

    /// <summary>Конструктор</summary>
    public Pages() => this._pages = new List<IPageControl>(3);

    public IPageControl GetPage(System.Type typeofPage)
    {
      foreach (IPageControl page in this._pages)
      {
        if (page.GetType() == typeofPage)
          return page;
      }
      return (IPageControl) null;
    }

    /// <summary>Добавить новую закладку в список</summary>
    /// <param name="page">Закладка</param>
    /// <param name="parentControl">Родительский контрол, на которую помещаем закладку</param>
    public void AddPage(IPageControl page, Control parentControl)
    {
      this._pages.Add(page);
      page.SetParent(parentControl);
    }

    /// <summary>Список закладок</summary>
    public List<IPageControl> Items => this._pages;
  }
}
