// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechCardClient
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Bars;
using Intermech.Client.Core.CompositionView;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Protection;
using Intermech.Search;
using Intermech.TechAcad.Interfaces;
using Intermech.TechCard.Client.Cadmech_3D;
using Intermech.TechCard.Client.CompositionView;
using Intermech.TechCard.Client.Configurator;
using Intermech.Techcard.Client.FormDesigner.External.CAD.Classes;
using Intermech.TechCard.Client.Imbase;
using Intermech.TechCard.Client.ObjectTypeSupport.Article;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;
using Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element;
using Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route;
using Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.View;
using Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech;
using Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.View;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.View;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.View;
using Intermech.TechCard.Client.ObjectTypeSupport.ProductionCopy;
using Intermech.TechCard.Client.ObjectTypeSupport.SpecialTechObject.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcs.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProсElem.Creator;
using Intermech.TechCard.Client.ObjectTypeSupport.Zagot;
using Intermech.TechCard.Client.ObjectTypeSupport.ZagotGroup;
using Intermech.TechCard.Client.ObjectTypeSupport.ZagotGroup.View;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.Services;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.Services.CreateVersion;
using Intermech.TechCard.Client.Settings.Ceh_Route;
using Intermech.TechCard.Client.Settings.Draft;
using Intermech.TechCard.Client.Settings.Imbase;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes;
using Intermech.TechCard.Client.TcObjectsTypes.ArtsComposition;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Template;
using Intermech.TechCard.Client.TcObjectsTypes.CehZahod;
using Intermech.TechCard.Client.TcObjectsTypes.Document;
using Intermech.TechCard.Client.TcObjectsTypes.Draft;
using Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;
using Intermech.TechCard.Client.TcObjectsTypes.Generic;
using Intermech.TechCard.Client.TcObjectsTypes.Operations;
using Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node;
using Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Rule;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup;
using Intermech.TechCard.Client.TcObjectsTypes.Zagot;
using Intermech.TechCard.Client.UI.PropertyEditors;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>Клиентский плагин TechCard.</summary>
public class TechCardClient : IPackage, IConfigurable
{
  /// <summary>Признак "locked" плагина</summary>
  /// <remarks>
  /// Если плагин заблокирован - весь функционал не доступен
  /// </remarks>
  private bool _pluginLocked;
  /// <summary>Основная форма плагина</summary>
  private TechCardMainForm _mainForm;
  /// <summary>
  /// 
  /// </summary>
  internal static System.IServiceProvider _serviceProvider;
  /// <summary>
  /// 
  /// </summary>
  private TechCardTreeMultiSelect _techCardMultiSelect;
  /// <summary>Значение предыдущего контекста редактирования</summary>
  private static long _lastEditingContextId;

  /// <summary>Завершение загрузки плагина</summary>
  private void LoadComplete()
  {
    if (Intermech.Imbase.Consts.CatalogsNodeCategoryID == -2)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        Intermech.Imbase.Consts.Initialize(sessionKeeper.Session, ApplicationServices.Container.GetService<IMetaDataHelper>());
    }
    this.RegisterTechCardServices();
    CVTechcardButton.RegisterButton();
    ArtsCompositionColumnScheme.Register();
  }

  /// <summary>Имя плагина</summary>
  public string Name => LocalizationHolder.rm.GetString("TechCard.Client_328");

  /// <summary>Загрузка плагина</summary>
  /// <param name="serviceProvider"></param>
  public void Load(System.IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("TechCard.Client_329"));
    service1.AllocateLicense(TechCardProtectionKey.appId);
    TechCardClient._serviceProvider = serviceProvider;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._pluginLocked = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false) == null;
    if (this._pluginLocked)
    {
      this.Unload();
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_446"), LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      this.LoadResources();
      this._mainForm = new TechCardMainForm();
      this._techCardMultiSelect = new TechCardTreeMultiSelect();
      if (serviceProvider.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service2)
        service2.Register((IEnableTreeMultiSelect) this._techCardMultiSelect);
      if (serviceProvider.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service3)
        service3.Register((IEnableTreeColumnsSorting) this._techCardMultiSelect);
      if (!(serviceProvider.GetService(typeof (IPluginManager)) is IPluginManager service4))
        return;
      if (service4.IsLoadComplete)
        this.LoadComplete();
      else
        service4.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadResources()
  {
    if (!(TechCardClient._serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.Numerate.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgNumerate");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.NumerateObject.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgNumerateObject");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.NumerateComposition.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgNumerateComposition");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.MoveComposition.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgMoveComposition");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.MoveDown.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgMoveDown");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.MoveUp.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgMoveUp");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.MoveFirst.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgMoveFirst");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.MoveLast.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgMoveLast");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.TechAcadEditor.png"))
    {
      if (bitmap != null)
        service.Add((Image) bitmap, "imgTechAcadEditor");
    }
    using (Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.DocumentList.png"))
    {
      if (bitmap == null)
        return;
      service.Add((Image) bitmap, "imgDocumentList");
    }
  }

  /// <summary>Выгрузка плагина</summary>
  public void Unload()
  {
    if (TechCardClient.ServiceProvider == null)
      return;
    if (!(TechCardClient.ServiceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("TechCard.Client_329"));
    service1.ReleaseLicense(TechCardProtectionKey.appId);
    if (this._pluginLocked)
      return;
    if (TechCardClient.ServiceProvider.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service2)
      service2.Unregister((IEnableTreeMultiSelect) this._techCardMultiSelect);
    if (TechCardClient.ServiceProvider.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service3)
      service3.Unregister((IEnableTreeColumnsSorting) this._techCardMultiSelect);
    this.UnregisterTechCardServices();
  }

  /// <summary>Загрузка конфигурации</summary>
  /// <param name="configurationManager"></param>
  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  /// <summary>Сохранение конфигурации</summary>
  /// <param name="configurationManager"></param>
  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }

  /// <summary>Регистрация служб, подключение событий</summary>
  internal void RegisterTechCardServices()
  {
    IFactory service1 = TechCardClient.ServiceProvider.GetService(typeof (IFactory)) as IFactory;
    INamedImageList service2 = TechCardClient.ServiceProvider.GetService(typeof (INamedImageList)) as INamedImageList;
    MenuBar menuBar = ((BarManager) TechCardClient.ServiceProvider.GetService(typeof (BarManager))).MenuBar;
    if (this._mainForm.mbiAcadService.Detach() is MenuButtonItem menuButtonItem)
    {
      int num = service2 != null ? service2.ImageIndex("imgTechAcadEditor") : -1;
      IMainMenuService service3 = ServiceUtils.GetService<IMainMenuService>((object) ApplicationServices.Container, false);
      if (service3 != null)
      {
        if (service2 != null && num != -1)
          menuButtonItem.Image = service2.ImageList.Images[num];
        service3.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Last, menuButtonItem);
      }
      INavigationBar service4 = ServiceUtils.GetService<INavigationBar>((object) ApplicationServices.Container, false);
      if (service4 != null && service4.FindPane("appPane") is IAppPane pane)
        pane.Add(menuButtonItem.Text, new EventHandler(this._mainForm.mbiAcadService_Click), num);
    }
    ArrayList arrayList = new ArrayList();
    foreach (MenuBarItem menuBarItem in (CollectionBase) this._mainForm.menuBarTechCard.Items)
      arrayList.Add((object) menuBarItem);
    foreach (MenuBarItem menuBarItem in arrayList)
      menuBar.Items.Add(menuBarItem.Detach());
    ApplicationServices.Container.AddService(typeof (IArtsCompositionParamsService), (object) new ArtsCompositionParamsService());
    if (ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false) != null)
      ApplicationServices.Container.RemoveService(typeof (ITechCardObjectCreateAnalyzingService));
    ApplicationServices.Container.AddService(typeof (ITechCardObjectCreateAnalyzingService), (object) new TechCardObjectCreateAnalyzingService());
    ApplicationServices.Container.AddService(typeof (ITechCardImbaseObjectCreatorService), (object) new TechCardImbaseObjectCreatorService(TechCardClient.ServiceProvider));
    ApplicationServices.Container.AddService(typeof (ITechCardClassifyObjectService), (object) new TechCardClassifyObjectService());
    if (ServiceUtils.GetService<ITechCardCreateVersionService>((object) ApplicationServices.Container, false) == null)
      ApplicationServices.Container.AddService(typeof (ITechCardCreateVersionService), (object) new TechCardCreateVersionService());
    service1.AddViewsProvider(1, TechCardConsts.ObjectTypes.TechProcGroupID, (IViewsProvider) new TechProcGroupArtViewProvider());
    service1.AddViewsProvider(1, TechCardConsts.ObjectTypes.TechProcTipovID, (IViewsProvider) new TechProcGroupArtViewProvider());
    service1.AddViewsProvider(1, TechCardConsts.ObjectTypes.NumerationRuleID, (IViewsProvider) new NumRuleViewProvider());
    service1.AddViewsProvider(1, TechCardConsts.ObjectTypes.NumerationObjectID, (IViewsProvider) new NumNodeViewProvider());
    service1.AddViewsProvider(1, TechCardConsts.ObjectTypes.ZagotGroupID, (IViewsProvider) new ZagotGroupViewProvider());
    RouteElementsViewProvider.RegisterViewProvider(service1);
    CehRouteViewProvider.RegisterViewProvider(service1);
    DocumentViewProvider.RegisterViewProvider(service1);
    service1.AddViewsProvider(1, TechCardConsts.ObjectTypes.DraftOLEID, (IViewsProvider) new DraftOleViewProvider());
    Cadmech3DSettingsParamViewProvider.RegisterViewProvider(service1);
    ArtsCompositionApplicabilityViewProvider.RegisterViewProvider(service1);
    ProcessRouteViewProvider.RegisterViewProvider(service1);
    ProcRouteEntryViewProvider.RegisterViewProvider(service1);
    ProcRouteContextViewProvider.RegisterViewProvider(service1);
    this.RegisterContextCommandProviders();
    IObjectCreatorService service5 = TechCardClient.ServiceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.TechBaseObjectID, typeof (TechCardBaseObjectCreatorService));
    foreach (int baseUserObjectId in TechCardConsts.ObjectTypes.TechBaseUserObjectIds)
      service5.RegisterCreatorCustomService(baseUserObjectId, typeof (TechCardBaseObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.TechProcBaseID, typeof (TechObjectCreatorBaseService<TechProcObjectCreatorControl>));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.OborudBaseID, typeof (TechCardBaseObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.MaterialBaseID, typeof (TechCardBaseObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.NumerationRuleID, typeof (NumRuleObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.NumerationObjectID, typeof (NumNodeObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.PersonalBaseID, typeof (TechCardBaseObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.TechProcElemBaseID, typeof (TechProcElemObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.ZagotID, typeof (TechObjectCreatorBaseService<ZagotObjectCreatorControl>));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.CehRouteID, typeof (TechObjectCreatorBaseService<CehRoutesObjectCreatorControl>));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.TemplRouteBaseID, typeof (TechObjectCreatorBaseService<RouteTemplateObjectCreatorControl>));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.DraftOLEID, typeof (DraftOleObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.DraftCadmechID, typeof (DraftCadmObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.EdinicaSostavaID, typeof (ArtsCompositionObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.ProcRoutingID, typeof (ProcRouteObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.ProcRoutingEntryID, typeof (ProcRouteEntryObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.SpecialToolID, typeof (SpecialTechObjectCreatorService));
    foreach (int techSpecialObjectId in TechCardConsts.ObjectTypes.TechSpecialObjectIds)
      service5.RegisterCreatorCustomService(techSpecialObjectId, typeof (SpecialTechObjectCreatorService));
    service5.RegisterCreatorCustomService(TechCardConsts.ObjectTypes.ZagotInTpID, typeof (TechObjectCreatorBaseService<ZagotInTpObjectCreatorControl>));
    service5.AfterEntersInCreatedEvent += new AfterEntersInCreatedEventHandler(TechCardMultiObjectCreatorRiderCustomService.DoEntersInCreatedEvent);
    service5.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(TechCardMultiObjectCreatorRiderCustomService.DoObjectCreatorCompletedEvent);
    service5.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(TechCardMultiObjectCreatorRiderCustomService.DoObjectCreatorDraftCreatedEvent);
    this.RegisterSettingsPages();
    CompositionViewHolder.CompositionViewEvents.onBeforeAllCreations += new BeforeAllCreations(TcClientUtils.CompositionViewBeforeAllCreation);
    CompositionViewHolder.CompositionViewEvents.onAfterAllCreations += new AfterAllCreations(TcClientUtils.CompositionViewAfterAllCreation);
    CompositionViewHolder.CompositionViewEvents.OnAfterCommitCreation += new AfterCommitCreation(TcClientUtils.ComposionViewFolderObjectAfterCreation);
    CVTechcardButton.RegisterButton();
    ActionsCadInfo.RegisterAction();
    INotificationService service6 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (service6 != null)
    {
      service6.Subscribe("ApplicationClosed", new NotificationEventHandler(this.ApplicationClosed));
      service6.Subscribe("EditingContextChanged", new NotificationEventHandler(this.EditingContextChanged));
      service6.Subscribe(BeforeMapObjectViewEventArgs.BeforeMapObjectViewEvent, new NotificationEventHandler(DraftViewProcessor.BeforeMapObjectView));
    }
    this.RegisterCustomEditors4Attributes();
    this.RegisterNotificationHandlers();
  }

  /// <summary>Регистрация подписчиков для сервиса уведомлений</summary>
  private void RegisterNotificationHandlers() => CehRoutesNotificationHandler.Register();

  /// <summary>Добавление провайдеров контекстных меню</summary>
  private void RegisterContextCommandProviders()
  {
    IFactory service = ServiceUtils.GetService<IFactory>((object) TechCardClient.ServiceProvider, false);
    if (service == null)
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TechGenericObjectCommandProvider.RegisterCommandProvider(service);
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.TechProcBaseID, (ICommandsProvider) new TechProcBaseContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.TechProcEdinID, (ICommandsProvider) new TechProcContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.TemplRouteBaseID, (ICommandsProvider) new RouteTemplateBaseContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ZagotID, (ICommandsProvider) new ZagotContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ZagotGroupID, (ICommandsProvider) new ZagotGroupContextCommandProvider());
      TechCardAddObjectContextCommandProvider.RegisterCommandProvider(service);
      TechCardAddThroughObjectContextCommandProvider.RegisterCommandProvider(service);
      TechCardBaseNumerateCommandProvider.RegisterCommandProvider(service);
      TechCardBaseEditingContextsCommandsProvider.RegisterCommandProvider(service);
      TechCardBaseObjectContextCommandProvider.RegisterCommandProvider(service);
      ProductionCopyCommandProvider.RegisterCommandProvider(service);
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.TechBaseObjectID, (ICommandsProvider) new TechCardBaseCreateVersionCommandProvider());
      foreach (int baseUserObjectId in TechCardConsts.ObjectTypes.TechBaseUserObjectIds)
        service.AddCommandsProvider(1, baseUserObjectId, (ICommandsProvider) new TechCardBaseCreateVersionCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.TechDocID, (ICommandsProvider) new TechCardBaseCreateVersionCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ComlectTechDocBaseID, (ICommandsProvider) new TechCardBaseCreateVersionCommandProvider());
      service.AddCommandsProvider(1, MetaDataHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"), (ICommandsProvider) new Intermech.TechCard.Client.NotionObject.ArticleContextCommandProvider());
      TechProcGroupContextCommandProvider.RegisterCommandProvider(service);
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ArticleBaseID, (ICommandsProvider) new Intermech.TechCard.Client.ArticleObjectType.ArticleContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ArticleCopyBaseID, (ICommandsProvider) new Intermech.TechCard.Client.ArticleObjectType.ArticleContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ProcRoutingID, (ICommandsProvider) new ProcRouteCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.CehRouteID, (ICommandsProvider) new CehRoutesContextCommandProvider(service));
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.DraftCadmechID, (ICommandsProvider) new DraftCadmContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.DraftOLEID, (ICommandsProvider) new DraftOleContextCommandProvider());
      ArtsCompositionContextCommandProvider.RegisterCommandProvider(service);
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.CehZahodObjectID, (ICommandsProvider) new CehZahodContextCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.OperaciyaID, (ICommandsProvider) new OperationContextCommandProvider());
      Cadmech3DCommandProvider.RegisterCommandProvider(service);
      service.AddCommandsProvider(1, MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.NumerationObjectGUID), (ICommandsProvider) new NumNodeContextCommandProvider());
      RootObjectContextCommandProvider.RegisterCommandProvider(service);
      TechCardImbaseContextCommandProvider.RegisterCommandProvider(service);
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ProcRoutingID, (ICommandsProvider) new ProcRouteEntryCommandProvider());
      service.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ProcRoutingEntryID, (ICommandsProvider) new ProcRouteEntryCommandProvider());
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>Регистрация закладок для настроек</summary>
  internal void RegisterSettingsPages()
  {
    CehRoutesStringEditor.RegisterSettingsPage();
    DraftCadmechParamsEditor.RegisterSettingsPage();
    ImbaseFilterSetupEditor.RegisterSettingsPage();
    ImObjFilterSetupEditor.RegisterSettingsPage();
    TechCardParamsEditor.RegisterSettingsPage();
    Cadmech3DSettingsPage.RegisterSettingsPage();
    InheritArchiveRightsFromTechProcessOption.RegisterCategoryProp((System.IServiceProvider) ApplicationServices.Container);
    ArtsCompositionParamsEditor.RegisterSettingsPage();
  }

  /// <summary>Удаление служб и прочее</summary>
  internal void UnregisterTechCardServices()
  {
  }

  /// <summary>Регистрация редакторов для атрибутов</summary>
  internal void RegisterCustomEditors4Attributes()
  {
    IAttributePropertyDescriberService service = ServiceUtils.GetService<IAttributePropertyDescriberService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    if (service.GetDescriber(TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID) == null)
      service.RegisterDescriber(TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID, (IAttributePropertyDescriber) new MemberOfAssemblyDescriber());
    if (service.GetDescriber(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID) == null)
      service.RegisterDescriber(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID, (IAttributePropertyDescriber) new MemberOfExitAssemblyDescriber());
    if (service.GetDescriber(TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID) != null)
      return;
    service.RegisterDescriber(TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID, (IAttributePropertyDescriber) new MemberOfExitAssemblyDescriber());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void pluginManager_LoadComplete(object sender, EventArgs e) => this.LoadComplete();

  /// <summary>обработка события закрытия приложения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ApplicationClosed(object sender, EventArgs e)
  {
    try
    {
      ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false)?.UnloadAcad(false);
    }
    catch (Exception ex)
    {
      if (ex is FileNotFoundException)
        return;
      throw;
    }
  }

  /// <summary>Обработка события изменения контекста редактирования</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void EditingContextChanged(object sender, NotificationEventArgs e)
  {
    if (e == null || e.EventName != nameof (EditingContextChanged))
      return;
    ICurrentUserAndRole service1 = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, false);
    if (service1 == null || service1.CachedEditingContextID == TechCardClient._lastEditingContextId)
      return;
    TechCardClient._lastEditingContextId = service1.CachedEditingContextID;
    if (service1.CachedEditingContextID == 0L || service1.EditingContextMode == EditingContextMode.AutoUpdate)
      return;
    DockManager service2 = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
    if (service2 == null || !(service2.ActiveDockControl is NavWindow activeDockControl))
      return;
    NavigatorTreeNode rootNode = activeDockControl.TreeView.RootNode;
    if (rootNode == null || rootNode.NodeID == null || rootNode.NodeID.CategoryID != 1 || !(activeDockControl.TreeView.RootHandler.GetData(rootNode.NodeID, typeof (IDBObjectTypeID)) is IDBObjectTypeID data) || !TechCardConsts.Utils.IsTechcardObjectType((object) data.Value) || service1.CanSetContextAutoUpdateMode(service1.CachedEditingContextID) != CanSetContextModeCode.CanSetAutoUpdate)
      return;
    service1.EditingContextMode = EditingContextMode.AutoUpdate;
  }

  /// <summary>
  /// 
  /// </summary>
  internal static System.IServiceProvider ServiceProvider => TechCardClient._serviceProvider;
}
