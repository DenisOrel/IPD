// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionClientPlugin
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionType;
using Intermech.AutoSelection.Client.ContextProviders;
using Intermech.AutoSelection.Client.Forms;
using Intermech.AutoSelection.Client.ObjectCreator;
using Intermech.AutoSelection.Client.Views;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.NavBars;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client;

public class AutoSelectionClientPlugin : IPackage, IConfigurable, ICommandTarget
{
  private const string SettingsCommandName = "AutoSelection.Settings";
  private Icon _objTypeRuleIcon;
  private IPluginManager _manager;
  private DockControl _startDock;
  private AutoSelectionTreeSetupForm _setupForm;

  public string Name => LocalizationHolder.rm.GetString("AutoSelection.Client_67");

  public void Load(System.IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("AutoSelection.Client_76"));
    service1.AllocateLicense(AutoSelectionProtectionKey.appId);
    AutoSelectionClientCache.ServiceProvider = serviceProvider;
    this._manager = ServiceUtils.GetService<IPluginManager>((object) ApplicationServices.Container, true);
    this._manager.LoadComplete += new EventHandler(this.manager_LoadComplete);
    ApplicationServices.Container.AddService(typeof (IAutoSelectionService), (object) new Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService());
    IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, true);
    IGuidMapper service3 = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true);
    AutosSelectConsts.CategoryAutoSelectionTypesNode = service3.Register(AutosSelectConsts.CategoryAutoSelectionTypesNodeGuid);
    service2.AddNodeType(AutosSelectConsts.CategoryAutoSelectionTypesNode, typeof (AutoSelectionTypesNode));
    service2.AddViewsProvider(AutosSelectConsts.CategoryAutoSelectionTypesNode, (IViewsProvider) new AutoSelectionTypesProvider());
    AutosSelectConsts.CategoryAutoSelectionTypeNode = service3.Register(AutosSelectConsts.CategoryAutoSelectionTypeNodeGuid);
    service2.AddNodeType(AutosSelectConsts.CategoryAutoSelectionTypeNode, typeof (AutoSelectionTypeNode));
    AutoSelectionContextProvider.RegisterCommandProvider(service2);
    AutoSelectionExecuteContextProvider.RegisterCommandProvider(service2);
    service2.AddViewsProvider(1, AutoSelectionConsts.objTypeRuleID, (IViewsProvider) new AutoSelectionViewProvider());
    IObjectCreatorService service4 = ServiceUtils.GetService<IObjectCreatorService>((object) serviceProvider, true);
    service4.RegisterCreatorCustomService(AutoSelectionConsts.objTypeRuleID, typeof (AutoSelectionRuleCreatorService));
    service4.AfterEntersInCreatedEvent += new AfterEntersInCreatedEventHandler(this.ObjectCreator_DoEntersInCreatedEvent);
    service4.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.ObjectCreator_DoObjectCreatorCompletedEvent);
    service4.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.ObjectCreator_DoObjectCreatorDraftCreatedEvent);
    ICategoryTypeIconService service5 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service5 != null && service5.IndexOf(4, AutoSelectionConsts.objTypeRuleID) >= 0)
      this._objTypeRuleIcon = service5.GetIcon(4, AutoSelectionConsts.objTypeRuleID);
    if (ServiceUtils.GetService<INavigationBar>((object) ApplicationServices.Container, false)?.FindPane("adminPane") is IAppPane pane)
    {
      if (this._objTypeRuleIcon != null)
        pane.Add(LocalizationHolder.rm.GetString("AutoSelection.Client_68"), new EventHandler(this.ShowAutoSelectSetupForm), this._objTypeRuleIcon);
      else
        pane.Add(LocalizationHolder.rm.GetString("AutoSelection.Client_68"), new EventHandler(this.ShowAutoSelectSetupForm), (Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/));
    }
    ICommandManager commandManager = (ICommandManager) serviceProvider.GetService(typeof (ICommandManager)) ?? (ICommandManager) new CommandManager();
    BarManager service6 = (BarManager) serviceProvider.GetService(typeof (BarManager));
    commandManager.AddTarget((ICommandTarget) this);
    MenuItemBase menuBar = (MenuItemBase) service6.MenuBar.FindMenuBar("mnService");
    if (menuBar != null)
    {
      Bitmap img = (Bitmap) null;
      if (this._objTypeRuleIcon != null)
        img = new Bitmap((Image) this._objTypeRuleIcon.ToBitmap(), new Size(16 /*0x10*/, 16 /*0x10*/));
      MenuButtonItem menuItem = DocumentMenuHelper.CreateMenuItem("AutoSelection.Settings", LocalizationHolder.rm.GetString("AutoSelection.Client_68"), "", (Image) img, true, false, commandManager);
      menuBar.Items.Add((ToolbarItemBase) menuItem);
    }
    this.RegisterViews();
  }

  public void Unload()
  {
    if (AutoSelectionClientCache.ServiceProvider.GetService(typeof (ILicenser)) is ILicenser service1)
    {
      int appId = 338;
      service1.ReleaseLicense(appId);
    }
    if (this._manager != null)
      this._manager.LoadComplete -= new EventHandler(this.manager_LoadComplete);
    this._manager_Unload();
    IGuidMapper service2 = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true);
    service2.Unregister(AutosSelectConsts.CategoryAutoSelectionTypesNode);
    service2.Unregister(AutosSelectConsts.CategoryAutoSelectionTypeNode);
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }

  public bool Execute(ICommandState commandState)
  {
    if (!(commandState.CommandName == "AutoSelection.Settings"))
      return false;
    this.ShowAutoSelectSetupForm((object) this, (EventArgs) null);
    return true;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (!(commandState.CommandName == "AutoSelection.Settings"))
      return false;
    commandState.Visible = true;
    commandState.Enabled = true;
    return true;
  }

  private void manager_LoadComplete(object sender, EventArgs e)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
  }

  private void _manager_Unload()
  {
  }

  private void ShowAutoSelectSetupForm(object sender, EventArgs e)
  {
    if (this._startDock == null)
    {
      this._startDock = new DockControl()
      {
        Guid = AutosSelectConsts.AutoSelectionFormDockGuid
      };
      AutoSelectionTreeSetupForm selectionTreeSetupForm = new AutoSelectionTreeSetupForm();
      selectionTreeSetupForm.TopLevel = false;
      selectionTreeSetupForm.FormBorderStyle = FormBorderStyle.None;
      selectionTreeSetupForm.Parent = (Control) this._startDock;
      selectionTreeSetupForm.Dock = DockStyle.Fill;
      this._setupForm = selectionTreeSetupForm;
      this._startDock.Text = this._setupForm.Text;
      this._startDock.ShowImageInDocumentTab = true;
      this._startDock.Closing += new CancelEventHandler(this.CloseAutoSelectSetupForm);
      this._setupForm.Visible = true;
      this._startDock.Show(ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, true));
      if (this._objTypeRuleIcon != null)
        this._startDock.TabImage = (Image) new Bitmap((Image) this._objTypeRuleIcon.ToBitmap(), new Size(16 /*0x10*/, 16 /*0x10*/));
    }
    else
      this._setupForm?.InitializeAccessInfo();
    if (sender == null)
      return;
    this._startDock.Activate();
  }

  private void CloseAutoSelectSetupForm(object sender, CancelEventArgs e)
  {
    this._setupForm = (AutoSelectionTreeSetupForm) null;
    this._startDock = (DockControl) null;
  }

  private void ObjectCreator_DoObjectCreatorDraftCreatedEvent(
    object sender,
    AfterDraftCreatedEventArgs args)
  {
  }

  private void ObjectCreator_DoObjectCreatorCompletedEvent(
    object sender,
    AfterObjectCreatedEventArgs args)
  {
    if (!(sender is IObjectCreatorService objectCreatorService) || args == null || args.ObjectID == -1L || args.ObjectID == 0L || args.IsVersion || args.PrototypeId != -1L && args.PrototypeId != 0L)
      return;
    List<ObjectCreatedInfo> source1 = new List<ObjectCreatedInfo>((IEnumerable<ObjectCreatedInfo>) objectCreatorService.GetObjectCreatedInfo());
    if (source1.Count == 0 && args.ObjectID != 0L && args.ObjectID != -1L)
      source1.Add(new ObjectCreatedInfo(args.ObjectID, args.ObjectTypeID, args.PrototypeId, args.IsVersion));
    List<ObjectCreatedInfo> list = source1.Where<ObjectCreatedInfo>((Func<ObjectCreatedInfo, bool>) (item =>
    {
      if (item.IsVersion)
        return false;
      return item.ObjectTypeId == 0 || item.PrototypeId == -1L;
    })).ToList<ObjectCreatedInfo>();
    if (list.Count == 0)
      return;
    IAutoSelectionService service = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    List<RelObjInfoItem> source2 = new List<RelObjInfoItem>();
    foreach (ObjectCreatedInfo objectCreatedInfo in list)
    {
      IAutoSelectionService selectionService = service;
      long objectId = objectCreatedInfo.ObjectId;
      ObjectRelationLink[] relationLinks1 = objectCreatedInfo.RelationLinks;
      long[] array1 = relationLinks1 != null ? ((IEnumerable<ObjectRelationLink>) relationLinks1).Select<ObjectRelationLink, long>((Func<ObjectRelationLink, long>) (item => item.LinkID)).ToArray<long>() : (long[]) null;
      ObjectRelationLink[] relationLinks2 = objectCreatedInfo.RelationLinks;
      long[] array2 = relationLinks2 != null ? ((IEnumerable<ObjectRelationLink>) relationLinks2).Select<ObjectRelationLink, long>((Func<ObjectRelationLink, long>) (item => item.ObjectID)).ToArray<long>() : (long[]) null;
      AutoSelectionParams args1 = new AutoSelectionParams(objectId, array1, array2, AutoSelectionMode.AutoObject);
      List<RelObjInfoItem> collection = selectionService.ExecuteSelection(args1);
      if (collection != null)
        source2.AddRange((IEnumerable<RelObjInfoItem>) collection);
    }
    if (source2.Count <= 0)
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source2.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source2.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) source2.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) source2.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
  }

  private void ObjectCreator_DoEntersInCreatedEvent(
    object sender,
    AfterEntersInCreatedEventArgs args)
  {
  }

  internal void RegisterViews()
  {
    AdjustableViewsHelper.RegisterView(LocalizationHolder.rm.GetString("AutoSelection.Client_66"), LocalizationHolder.rm.GetString("AutoSelection.Client_66"), "", "", "", true, 0);
  }
}
