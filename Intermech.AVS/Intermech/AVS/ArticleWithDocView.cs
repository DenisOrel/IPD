// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ArticleWithDocView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Common_Dialogs.ArticleWithDocForm;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

[ViewDescriptionProvider(typeof (ArticleWithDocView.ArticleWithDocViewDescriptionProvider))]
internal class ArticleWithDocView : UserControl, IView
{
  private Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm _form;
  /// <summary>Провайдер сервисов</summary>
  private System.IServiceProvider _provider;
  /// <summary>Идентификатор выделенного в спецификации объекта</summary>
  private long _selectedID;
  /// <summary>Идентификатор типа выделенного в спецификации объекта</summary>
  private int _selectedObjTypeID = -1;
  /// <summary>Запись формы Б или переменные данные формы В</summary>
  private bool _isFormBRow;
  private List<long> _relationIDs = new List<long>();
  /// <summary>Индекс изображения для меню и закладки</summary>
  private int _imageIndex = -1;
  /// <summary>Режим инициализации формы на вьюшке</summary>
  private bool _initmode;
  /// <summary>Порядковый номер вьюшки</summary>
  private int _viewIndex = int.MinValue;
  /// <summary>Название вьюшки</summary>
  private string _viewName = "Запись спецификации";
  private bool isActive;

  public ArticleWithDocView()
  {
    this.InitializeComponent();
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service == null)
      return;
    this._imageIndex = service.ImageIndex("imgSpecRow");
  }

  /// <summary>Обработка события обновления объектов </summary>
  public void ObjectsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    if (!this.Visible || this.IsDisposed)
      return;
    if (!this.isActive)
      return;
    try
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
        return;
      for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
      {
        if (this._selectedID == objectsEventArgs.ObjectIDs[index] && this._form != null)
          this._form.Article.page_ReloadData((object) this, new EventArgs());
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(new Exception("active == " + this.isActive.ToString(), ex));
    }
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (!(ServicesManager.GetService(typeof (IAVSViewsService)) is IAVSViewsService service) || service.AVSWindow == null)
      return;
    List<AVSRow> selectedSpecRows = service.AVSWindow.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count == 0)
      return;
    this._selectedID = selectedSpecRows[0].ObjectId;
    this._selectedObjTypeID = selectedSpecRows[0].ObjType;
    this._relationIDs.Clear();
    this._isFormBRow = selectedSpecRows[0].IsFormB;
    if (selectedSpecRows[0].Relations != null && selectedSpecRows[0].Relations.Count > 0)
    {
      foreach (RelationAttributeValuesCache relation in selectedSpecRows[0].Relations)
      {
        if (relation.RelationId != -1L)
          this._relationIDs.Add(relation.RelationId);
      }
    }
    this._provider = provider;
    this._initmode = true;
  }

  public void Activate(IView previousView)
  {
    this.isActive = true;
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service1)
    {
      service1.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service1.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service1.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service1.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectsWasChangedHandler));
    }
    if (!this._initmode || !(ServicesManager.GetService(typeof (IAVSViewsService)) is IAVSViewsService service2))
      return;
    if (this._form == null)
      this._form = new Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm();
    if (service2.AVSWindow.ReadOnly)
      this._form.Init(OpenModes.InViewReadOnly, service2.AVSWindow);
    else
      this._form.Init(OpenModes.InView, service2.AVSWindow);
    if (this._selectedID.IsUndefinedId() || this._selectedObjTypeID.IsUndefinedTypeId())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IArticleService service3 = (IArticleService) ServicesManager.GetService(typeof (IArticleService));
      IFiltrationService service4 = (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
      IDBObject article = sessionKeeper.Session.GetObject(this._selectedID);
      long objectId = article.ObjectID;
      string filtrationServiceOwnerId = service4.FiltrationServiceOwnerID;
      IUserSession session = sessionKeeper.Session;
      IDBObject mainDocument = service3.FindMainDocument(objectId, filtrationServiceOwnerId, (object) session);
      List<IDBRelation> relations = new List<IDBRelation>(this._relationIDs.Count);
      for (int index = 0; index < this._relationIDs.Count; ++index)
        relations.Add(sessionKeeper.Session.GetRelation(this._relationIDs[index]));
      FormType formType = FormType.Single;
      if (service2.AVSWindow.AVSDocument.AVSDocType != AVSDocumentType.AutoIndustrySpecification)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(article.ObjectType, AvsIDCache.ObjType_DetailWithoutDrawing))
          formType = FormType.NonDraft;
        if (this._isFormBRow)
          formType = formType != FormType.NonDraft ? FormType.GroupB : FormType.NonDraftB;
      }
      else
      {
        formType = FormType.Autoprom_Single;
        if (MetaDataHelper.IsObjectTypeChildOf(article.ObjectType, AvsIDCache.ObjType_DetailWithoutDrawing))
          formType = FormType.Autoprom_NonDraft;
        if (this._isFormBRow)
          formType = formType != FormType.Autoprom_NonDraft ? FormType.Autoprom_GroupB : FormType.Autoprom_NonDraftB;
      }
      this._form.Article.Initialize(sessionKeeper.Session, article, mainDocument, relations, formType);
      this._form.SetParent((Control) this);
    }
    this._initmode = false;
  }

  public void Deactivate(IView nextView)
  {
    this.isActive = false;
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
    {
      service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service.Unsubscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectsWasChangedHandler));
    }
    if (this._form == null || !this._form.Article.Changed || MessageBox.Show($"В закладке \"{this.Caption}\" остались не сохраненные данные. Сохранить?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this._form.Article.Save();
  }

  protected override void Dispose(bool disposing)
  {
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
    {
      service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      service.Unsubscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectsWasChangedHandler));
    }
    base.Dispose(disposing);
  }

  public string Caption => this._viewName;

  public int ImageIndex => this._imageIndex;

  public int OrderID => this._viewIndex;

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScroll = true;
    this.Name = nameof (ArticleWithDocView);
    this.Size = new Size(649, 212);
    this.ResumeLayout(false);
  }

  private sealed class ArticleWithDocViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Запись спецификации",
        ImageIndex = -1,
        OrderID = int.MinValue
      };
    }
  }
}
