// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseStartup
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Bars;
using Intermech.DatabaseConfigurator;
using Intermech.Imbase.API;
using Intermech.Imbase.AttributesDescribers;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.CategoryProp;
using Intermech.Imbase.Commands;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Editors;
using Intermech.Imbase.ExceptionForms;
using Intermech.Imbase.FormDesigner;
using Intermech.Imbase.ImbaseObjectsCreators;
using Intermech.Imbase.Indexes;
using Intermech.Imbase.Params;
using Intermech.Imbase.Selection;
using Intermech.Imbase.Server;
using Intermech.Imbase.TableWizard;
using Intermech.Imbase.Templates;
using Intermech.Imbase.Views;
using Intermech.Imbase.Wizards.TableMixWizard;
using Intermech.ImbaseExcelUnloader.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Imbase.Sync;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Protection;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseStartup : IPackage, IConfigurable
{
  private System.IServiceProvider _serviceProvider;
  private IBackgroundTask _converterTask;
  private IBackgroundTask _synchObjsTask;
  private IBackgroundTask _restructuringTask;
  private IOutputView _outputView;
  private int _imAttr4ObjPropID = -1;
  private int _imAttributePropID = -1;
  internal static bool PluginLocked = false;
  private static readonly string[] LineSeparators = new string[2]
  {
    "\r\n",
    "\n"
  };

  public void Unload()
  {
    if (this._serviceProvider == null)
      return;
    if (this._serviceProvider.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service)
    {
      service.UnregisterCategoryProps(this._imAttr4ObjPropID);
      service.UnregisterCategoryProps(this._imAttributePropID);
    }
    ServiceUtils.GetService<ITableViewColorizer>((object) ServicesManager.ServiceContainer, true).ColorizeRows -= new TableView.ColorizeRowsEventHandler(ColorizerEvents.TableViewColorizer_ColorizeRows);
  }

  public string Name => LocalizationHolder.rm.GetString("Imbase.Client_135");

  public void Load(System.IServiceProvider serviceProvider)
  {
    int appId = 343;
    byte[][] numArray1 = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 213,
        (byte) 15,
        (byte) 55,
        (byte) 229,
        (byte) 114,
        (byte) 71,
        (byte) 233,
        (byte) 227,
        (byte) 226,
        (byte) 107,
        (byte) 203,
        (byte) 62,
        (byte) 14,
        (byte) 69,
        (byte) 53,
        (byte) 253
      },
      new byte[16 /*0x10*/]
      {
        (byte) 8,
        (byte) 61,
        (byte) 248,
        (byte) 243,
        (byte) 235,
        (byte) 96 /*0x60*/,
        (byte) 66,
        (byte) 251,
        (byte) 30,
        (byte) 56,
        (byte) 19,
        (byte) 124,
        (byte) 77,
        (byte) 8,
        (byte) 65,
        (byte) 186
      },
      new byte[16 /*0x10*/]
      {
        (byte) 19,
        (byte) 239,
        (byte) 234,
        (byte) 242,
        (byte) 190,
        (byte) 23,
        (byte) 117,
        (byte) 105,
        (byte) 5,
        (byte) 125,
        (byte) 243,
        (byte) 73,
        (byte) 85,
        (byte) 62,
        (byte) 15,
        (byte) 114
      },
      new byte[16 /*0x10*/]
      {
        (byte) 250,
        (byte) 34,
        (byte) 78,
        (byte) 215,
        (byte) 139,
        (byte) 191,
        (byte) 45,
        (byte) 48 /*0x30*/,
        (byte) 79,
        (byte) 118,
        (byte) 143,
        (byte) 248,
        (byte) 188,
        (byte) 100,
        (byte) 65,
        (byte) 100
      },
      new byte[16 /*0x10*/]
      {
        (byte) 70,
        (byte) 88,
        (byte) 71,
        (byte) 72,
        (byte) 69,
        (byte) 208 /*0xD0*/,
        (byte) 57,
        (byte) 175,
        (byte) 176 /*0xB0*/,
        (byte) 152,
        (byte) 78,
        (byte) 254,
        (byte) 82,
        (byte) 86,
        (byte) 85,
        (byte) 127 /*0x7F*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 43,
        (byte) 151,
        (byte) 34,
        (byte) 245,
        (byte) 50,
        (byte) 50,
        (byte) 186,
        (byte) 114,
        (byte) 149,
        (byte) 50,
        (byte) 139,
        (byte) 163,
        (byte) 66,
        (byte) 251,
        (byte) 203,
        (byte) 224 /*0xE0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 71,
        (byte) 113,
        (byte) 235,
        (byte) 164,
        (byte) 102,
        (byte) 114,
        (byte) 126,
        (byte) 190,
        (byte) 153,
        (byte) 205,
        (byte) 223,
        (byte) 244,
        (byte) 83,
        (byte) 160 /*0xA0*/,
        (byte) 141,
        (byte) 163
      },
      new byte[16 /*0x10*/]
      {
        (byte) 105,
        (byte) 205,
        (byte) 121,
        (byte) 223,
        (byte) 17,
        (byte) 122,
        (byte) 29,
        (byte) 235,
        (byte) 235,
        (byte) 133,
        (byte) 146,
        (byte) 27,
        (byte) 62,
        (byte) 185,
        (byte) 225,
        (byte) 153
      },
      new byte[16 /*0x10*/]
      {
        (byte) 92,
        (byte) 88,
        (byte) 248,
        (byte) 182,
        (byte) 254,
        (byte) 236,
        (byte) 184,
        (byte) 67,
        (byte) 181,
        (byte) 58,
        (byte) 66,
        (byte) 38,
        (byte) 200,
        (byte) 119,
        (byte) 23,
        (byte) 174
      },
      new byte[16 /*0x10*/]
      {
        (byte) 130,
        (byte) 19,
        (byte) 249,
        (byte) 59,
        (byte) 218,
        (byte) 86,
        (byte) 217,
        (byte) 216,
        (byte) 50,
        (byte) 166,
        (byte) 220,
        (byte) 163,
        (byte) 2,
        (byte) 10,
        (byte) 50,
        (byte) 227
      },
      new byte[16 /*0x10*/]
      {
        (byte) 39,
        (byte) 115,
        (byte) 1,
        (byte) 228,
        (byte) 83,
        (byte) 106,
        (byte) 12,
        (byte) 212,
        (byte) 218,
        (byte) 137,
        (byte) 174,
        (byte) 120,
        (byte) 240 /*0xF0*/,
        (byte) 69,
        (byte) 91,
        (byte) 16 /*0x10*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 110,
        (byte) 158,
        byte.MaxValue,
        (byte) 83,
        (byte) 19,
        (byte) 21,
        (byte) 197,
        (byte) 116,
        (byte) 254,
        (byte) 180,
        (byte) 246,
        (byte) 100,
        (byte) 11,
        (byte) 42,
        (byte) 19,
        (byte) 39
      },
      new byte[16 /*0x10*/]
      {
        (byte) 10,
        (byte) 170,
        (byte) 162,
        (byte) 249,
        (byte) 93,
        (byte) 159,
        (byte) 129,
        (byte) 91,
        (byte) 185,
        (byte) 168,
        (byte) 238,
        (byte) 43,
        (byte) 47,
        (byte) 87,
        (byte) 245,
        (byte) 34
      },
      new byte[16 /*0x10*/]
      {
        (byte) 56,
        (byte) 19,
        (byte) 228,
        (byte) 117,
        (byte) 74,
        (byte) 158,
        (byte) 241,
        (byte) 150,
        (byte) 162,
        (byte) 30,
        (byte) 134,
        (byte) 43,
        (byte) 152,
        (byte) 211,
        (byte) 166,
        (byte) 174
      },
      new byte[16 /*0x10*/]
      {
        (byte) 181,
        (byte) 49,
        (byte) 204,
        (byte) 223,
        (byte) 158,
        (byte) 169,
        (byte) 80 /*0x50*/,
        (byte) 241,
        (byte) 76,
        (byte) 35,
        (byte) 220,
        (byte) 116,
        (byte) 61,
        (byte) 64 /*0x40*/,
        (byte) 82,
        (byte) 112 /*0x70*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 125,
        (byte) 41,
        (byte) 135,
        (byte) 185,
        (byte) 15,
        (byte) 114,
        (byte) 120,
        (byte) 169,
        (byte) 235,
        (byte) 29,
        (byte) 51,
        (byte) 197,
        (byte) 82,
        (byte) 70,
        (byte) 108,
        (byte) 5
      },
      new byte[16 /*0x10*/]
      {
        (byte) 222,
        (byte) 49,
        (byte) 28,
        (byte) 30,
        (byte) 212,
        (byte) 29,
        (byte) 80 /*0x50*/,
        (byte) 136,
        (byte) 241,
        (byte) 166,
        (byte) 42,
        (byte) 170,
        (byte) 123,
        (byte) 224 /*0xE0*/,
        (byte) 164,
        (byte) 131
      },
      new byte[16 /*0x10*/]
      {
        (byte) 38,
        (byte) 138,
        (byte) 38,
        (byte) 123,
        (byte) 165,
        (byte) 213,
        (byte) 65,
        (byte) 220,
        (byte) 90,
        (byte) 220,
        (byte) 23,
        (byte) 55,
        (byte) 244,
        (byte) 40,
        (byte) 230,
        (byte) 216
      },
      new byte[16 /*0x10*/]
      {
        (byte) 147,
        (byte) 140,
        (byte) 194,
        (byte) 191,
        (byte) 120,
        (byte) 124,
        (byte) 114,
        (byte) 97,
        (byte) 89,
        (byte) 176 /*0xB0*/,
        (byte) 172,
        (byte) 58,
        (byte) 194,
        (byte) 13,
        (byte) 29,
        (byte) 121
      },
      new byte[16 /*0x10*/]
      {
        (byte) 88,
        (byte) 140,
        (byte) 123,
        (byte) 122,
        (byte) 56,
        (byte) 221,
        (byte) 148,
        (byte) 244,
        (byte) 231,
        (byte) 2,
        (byte) 138,
        (byte) 231,
        (byte) 233,
        (byte) 217,
        (byte) 133,
        (byte) 210
      },
      new byte[16 /*0x10*/]
      {
        (byte) 86,
        (byte) 125,
        (byte) 76,
        (byte) 254,
        (byte) 59,
        (byte) 215,
        (byte) 78,
        (byte) 152,
        (byte) 224 /*0xE0*/,
        (byte) 68,
        (byte) 237,
        (byte) 248,
        (byte) 126,
        (byte) 166,
        (byte) 118,
        (byte) 107
      },
      new byte[16 /*0x10*/]
      {
        (byte) 233,
        (byte) 134,
        (byte) 197,
        (byte) 223,
        (byte) 213,
        (byte) 108,
        (byte) 147,
        (byte) 182,
        (byte) 19,
        (byte) 160 /*0xA0*/,
        (byte) 45,
        (byte) 28,
        (byte) 142,
        (byte) 70,
        (byte) 47,
        (byte) 224 /*0xE0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 127 /*0x7F*/,
        (byte) 89,
        (byte) 225,
        (byte) 177,
        (byte) 125,
        (byte) 147,
        (byte) 248,
        (byte) 98,
        (byte) 191,
        (byte) 187,
        (byte) 196,
        (byte) 36,
        (byte) 112 /*0x70*/,
        (byte) 22,
        (byte) 198,
        (byte) 228
      },
      new byte[16 /*0x10*/]
      {
        (byte) 156,
        (byte) 192 /*0xC0*/,
        (byte) 247,
        (byte) 74,
        (byte) 120,
        (byte) 53,
        (byte) 165,
        (byte) 5,
        (byte) 174,
        (byte) 194,
        (byte) 128 /*0x80*/,
        (byte) 52,
        (byte) 199,
        (byte) 252,
        (byte) 24,
        (byte) 38
      },
      new byte[16 /*0x10*/]
      {
        (byte) 136,
        (byte) 177,
        (byte) 84,
        (byte) 253,
        (byte) 148,
        (byte) 166,
        (byte) 72,
        (byte) 187,
        (byte) 61,
        (byte) 42,
        (byte) 223,
        (byte) 51,
        (byte) 142,
        (byte) 122,
        (byte) 226,
        (byte) 179
      },
      new byte[16 /*0x10*/]
      {
        (byte) 33,
        (byte) 0,
        (byte) 114,
        (byte) 69,
        (byte) 169,
        (byte) 83,
        (byte) 171,
        (byte) 243,
        (byte) 67,
        (byte) 72,
        (byte) 113,
        (byte) 138,
        (byte) 123,
        (byte) 107,
        (byte) 161,
        (byte) 134
      },
      new byte[16 /*0x10*/]
      {
        (byte) 58,
        (byte) 65,
        (byte) 186,
        (byte) 105,
        (byte) 43,
        (byte) 108,
        (byte) 251,
        (byte) 194,
        (byte) 133,
        (byte) 233,
        (byte) 62,
        (byte) 127 /*0x7F*/,
        (byte) 19,
        (byte) 42,
        (byte) 39,
        (byte) 201
      },
      new byte[16 /*0x10*/]
      {
        (byte) 20,
        (byte) 48 /*0x30*/,
        (byte) 207,
        (byte) 79,
        (byte) 44,
        (byte) 69,
        (byte) 35,
        (byte) 67,
        (byte) 71,
        (byte) 53,
        (byte) 97,
        (byte) 177,
        (byte) 18,
        (byte) 40,
        (byte) 60,
        (byte) 205
      },
      new byte[16 /*0x10*/]
      {
        (byte) 126,
        (byte) 9,
        (byte) 111,
        (byte) 204,
        (byte) 81,
        (byte) 120,
        (byte) 215,
        (byte) 217,
        (byte) 17,
        (byte) 37,
        (byte) 115,
        (byte) 83,
        (byte) 179,
        (byte) 196,
        (byte) 212,
        (byte) 69
      },
      new byte[16 /*0x10*/]
      {
        (byte) 118,
        (byte) 21,
        (byte) 90,
        (byte) 20,
        (byte) 11,
        (byte) 72,
        (byte) 39,
        (byte) 81,
        (byte) 184,
        (byte) 224 /*0xE0*/,
        (byte) 190,
        (byte) 22,
        (byte) 95,
        (byte) 120,
        (byte) 52,
        (byte) 181
      },
      new byte[16 /*0x10*/]
      {
        (byte) 216,
        (byte) 16 /*0x10*/,
        (byte) 233,
        (byte) 47,
        (byte) 92,
        (byte) 92,
        (byte) 184,
        (byte) 28,
        (byte) 182,
        (byte) 145,
        (byte) 120,
        (byte) 170,
        (byte) 103,
        (byte) 200,
        (byte) 167,
        (byte) 19
      },
      new byte[16 /*0x10*/]
      {
        (byte) 73,
        (byte) 20,
        (byte) 113,
        (byte) 204,
        (byte) 252,
        (byte) 145,
        (byte) 172,
        (byte) 52,
        (byte) 175,
        (byte) 243,
        (byte) 77,
        (byte) 199,
        (byte) 185,
        (byte) 213,
        (byte) 110,
        (byte) 215
      }
    };
    this._serviceProvider = serviceProvider;
    this._outputView = (IOutputView) serviceProvider.GetService(typeof (IOutputView));
    IProtectionKey service1 = serviceProvider.GetService(typeof (IProtectionKey)) as IProtectionKey;
    (serviceProvider.GetService(typeof (ILicenser)) as ILicenser).AllocateLicense(appId);
    if (service1 == null)
      return;
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] queryData = numArray1[index1];
    byte[] numArray2 = numArray1[index1 + 1];
    byte[] response = new byte[numArray2.Length];
    service1.Query(true, appId, queryData, response);
    int length = queryData.Length;
    for (int index2 = 0; index2 < length; ++index2)
    {
      if ((int) numArray2[index2] != (int) response[index2])
        return;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IMetaDataHelper service2 = ApplicationServices.Container.GetService<IMetaDataHelper>();
      Consts.Initialize(session, service2);
      ImbaseStartup.PluginLocked = !(session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService);
      if (ImbaseStartup.PluginLocked)
      {
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("IMBASE_Server_Dont_Loaded_Msg"), LocalizationHolder.rm.GetString("IMB_WARN"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        IGuidMapper service3 = (IGuidMapper) serviceProvider.GetService(typeof (IGuidMapper));
        IFactory service4 = (IFactory) serviceProvider.GetService(typeof (IFactory));
        ICategoryTypeIconService service5 = (ICategoryTypeIconService) serviceProvider.GetService(typeof (ICategoryTypeIconService));
        INamedImageList service6 = (INamedImageList) serviceProvider.GetService(typeof (INamedImageList));
        if (ServicesManager.GetService(typeof (IViewsManagerService)) is IViewsManagerService service7)
          service7.OnActivateView += new Intermech.Interfaces.Client.ActivateViewEventHandler(this.ActivateViewEventHandler);
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service8)
          service8.Subscribe("NavigatorWindowOpened", new NotificationEventHandler(this.OnNavigatorNewWindowOpening));
        EditorHelper.Initialize(this._serviceProvider);
        EditorMixHelper.Initialize(this._serviceProvider);
        ServiceHolder.Initialize(this._serviceProvider, session);
        IViewsProvider provider1 = (IViewsProvider) new ImbaseViewsProvider();
        int num2 = Consts.RootNodeCategoryID = service3.Register(Consts.RootNodeGUID);
        service4.AddNodeType(num2, typeof (ImbaseRootNode));
        service4.AddViewsProvider(num2, provider1);
        Assembly assembly = this.GetType().Assembly;
        Icon resourceData1 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.ImbaseRoot.ico");
        if (resourceData1 != null)
        {
          service5.AddIcon(resourceData1, num2);
          service6.Add(resourceData1, "imgImbaseRoot");
          resourceData1.Dispose();
        }
        service4.AddGlobalNode(new Guid("44B69757-56D4-4b11-9700-E868D39E0ADC"), (IDescriptor) new ImbaseRootNodeDescriptor(), 50);
        if (this._serviceProvider.GetService(typeof (IMainMenuService)) is IMainMenuService service10)
        {
          MenuButtonItem menuButtonItem1 = new MenuButtonItem(ImbaseRootNodeDescriptor.RootNodeCaption, new EventHandler(this.OnViewRootNode));
          menuButtonItem1.Icon = resourceData1;
          service10.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Default, menuButtonItem1);
          bool isAdmin = session.IsAdmin;
          List<MenuButtonItem> menuButtonItemList = new List<MenuButtonItem>();
          MenuButtonItem menuButtonItem2 = new MenuButtonItem(LocalizationHolder.rm.GetString("Imbase_MenuItem_UpdateImbaseKeys"), new EventHandler(this.ConvertOldKeys));
          menuButtonItem2.CommandName = "File.UpdateImbaseKeys";
          menuButtonItem2.Visible = isAdmin;
          menuButtonItemList.Add(menuButtonItem2);
          MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("Imbase_ClearImbaseObjects"), new EventHandler(this.ClearImbaseObjects));
          menuButtonItem3.CommandName = "File.ClearImbaseObjects";
          menuButtonItem3.Visible = isAdmin;
          menuButtonItemList.Add(menuButtonItem3);
          MenuButtonItem menuButtonItem4 = new MenuButtonItem(LocalizationHolder.rm.GetString("Imbase_RestructuringTables"), new EventHandler(this.RestructuringTables));
          menuButtonItem4.CommandName = "File.RestructuringTables";
          menuButtonItem4.Visible = isAdmin;
          menuButtonItemList.Add(menuButtonItem4);
          MenuButtonItem menuButtonItem5 = new MenuButtonItem(LocalizationHolder.rm.GetString("Imbase_MenuItem_RenameAtts"), new EventHandler(this.RenameAttsClick));
          menuButtonItem5.CommandName = "File.RenameAtts";
          menuButtonItem5.Visible = isAdmin;
          menuButtonItemList.Add(menuButtonItem5);
          MenuButtonItem menuButtonItem6 = new MenuButtonItem("Импорт таблицы IMBASE ...", new EventHandler(this.ImportImbaseTable));
          menuButtonItem6.CommandName = "File.ImbaseImport";
          menuButtonItem6.Visible = isAdmin;
          menuButtonItemList.Add(menuButtonItem6);
          MenuButtonItem menuButtonItem7 = new MenuButtonItem("Обновление внутренних индексов IMBASE ...", new EventHandler(this.UpateInternalIndexs));
          menuButtonItem7.CommandName = "File.UpateInternalIndexs";
          menuButtonItem7.Visible = isAdmin;
          menuButtonItemList.Add(menuButtonItem7);
          ICurrentUserAndRole service9 = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true);
          MenuButtonItem menuButtonItem8 = new MenuButtonItem(LocalizationHolder.rm.GetString("ImbaseSync.Name"), new EventHandler(this.ImbaseSync));
          menuButtonItem8.CommandName = "File.ImbaseSync";
          menuButtonItem8.Visible = service9.IsAdmin;
          MenuButtonItem menuButtonItem9 = menuButtonItem8;
          menuButtonItemList.Add(menuButtonItem9);
          MenuButtonItem menuButtonItem10 = new MenuButtonItem(LocalizationHolder.rm.GetString("PumpTableMix.Name"), new EventHandler(this.PumpReceptures));
          menuButtonItem10.CommandName = "File.ImbasePumpReceptures";
          menuButtonItem10.Visible = service9.IsAdmin;
          MenuButtonItem menuButtonItem11 = menuButtonItem10;
          menuButtonItemList.Add(menuButtonItem11);
          service10.RegisterMenuItemsGroup(MainMenuItemSite.AdministratorUtilities, MainMenuItemPosition.Default, false, menuButtonItemList.ToArray());
          ImbaseHelper.SetIsAdmin(isAdmin);
        }
        if (this._serviceProvider.GetService(typeof (INavigationBar)) is INavigationBar service11 && service11.FindPane("appPane") is IAppPane pane)
          pane.Add(ImbaseRootNodeDescriptor.RootNodeCaption, new EventHandler(this.OnViewRootNode), resourceData1);
        int num3 = Consts.CatalogsNodeCategoryID = service3.Register(Consts.CatalogsNodeGUID);
        service4.AddNodeType(num3, typeof (CatalogsNode));
        service4.AddViewsProvider(num3, provider1);
        Icon resourceData2 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.Folder.ico");
        if (resourceData2 != null)
        {
          service5.AddIcon(resourceData2, num3);
          service6.Add(resourceData2, "imgCatalogs");
          Consts.CatalogsListCategoryId = service3.Register(new Guid("2B1FB532-6555-4fd3-A820-F4A2386297A1"));
          service5.AddIcon(resourceData2, Consts.CatalogsListCategoryId);
          resourceData2.Dispose();
        }
        Icon resourceData3 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.addFavorites.ico");
        if (resourceData3 != null)
        {
          service6.Add(resourceData3, "addFavorites");
          resourceData3.Dispose();
        }
        Icon resourceData4 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.delFavorites.ico");
        if (resourceData4 != null)
        {
          service6.Add(resourceData4, "delFavorites");
          resourceData4.Dispose();
        }
        Icon resourceData5 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.show.ico");
        if (resourceData5 != null)
        {
          service6.Add(resourceData5, "show");
          resourceData5.Dispose();
        }
        int categoryID = Consts.TablesNodeCategoryID = service3.Register(Consts.TablesNodeGUID);
        service4.AddNodeType(categoryID, typeof (TablesNode));
        service4.AddViewsProvider(categoryID, provider1);
        Consts.ImbaseComplexObjectsID = service3.Register(Consts.ImbaseComplexObjectsGuid);
        Icon resourceData6 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.ComplexImbaseObjects.ico");
        if (resourceData6 != null)
        {
          service5.AddIcon(resourceData6, Consts.ImbaseComplexObjectsID);
          resourceData6.Dispose();
        }
        Consts.ImbaseFoldersID = service3.Register(Consts.ImbaseFoldersGuid);
        Icon resourceData7 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.ImbaseFolders.ico");
        if (resourceData7 != null)
        {
          service5.AddIcon(resourceData7, Consts.ImbaseFoldersID);
          resourceData7.Dispose();
        }
        Consts.ImbaseCatalogRecordsID = service3.Register(Consts.ImbaseCatalogRecordsGuid);
        Icon resourceData8 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.ImbaseCatalogRecords.ico");
        if (resourceData8 != null)
        {
          service5.AddIcon(resourceData8, Consts.ImbaseCatalogRecordsID);
          resourceData8.Dispose();
        }
        Consts.ImbaseTableRefsID = service3.Register(Consts.ImbaseTableRefsGuid);
        Icon resourceData9 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.ImbaseTableRefs.ico");
        if (resourceData9 != null)
        {
          service5.AddIcon(resourceData9, Consts.ImbaseTableRefsID);
          resourceData9.Dispose();
        }
        Consts.ImbaseFavoritesID = service3.Register(Consts.ImbaseFavoritesGuid);
        Icon resourceData10 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.Favorites.ico");
        if (resourceData10 != null)
        {
          service5.AddIcon(resourceData10, Consts.ImbaseFavoritesID);
          resourceData10.Dispose();
        }
        Icon resourceData11 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.DisabledUserFilter.ico");
        if (resourceData11 != null)
        {
          service6.Add(resourceData11, "imgDisabledUserFilter");
          resourceData11.Dispose();
        }
        Icon resourceData12 = ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Imbase.Resources.EnabledUserFilter.ico");
        if (resourceData12 != null)
        {
          service6.Add(resourceData12, "imgEnabledUserFilter");
          resourceData12.Dispose();
        }
        service4.AddCommandsProvider(categoryID, (ICommandsProvider) new TablesNodeContext());
        if (ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service12)
        {
          service12.RegisterCreatorCustomService(Consts.ImbaseTableRefTypeID, typeof (ImbaseTableCreator));
          service12.RegisterCreatorCustomService(Consts.ImbaseTableTypeID, typeof (ImbaseTableCreator));
          service12.RegisterCreatorCustomService(Consts.ImbaseCatalogTypeID, typeof (ImbaseCatalogCreator));
          service12.RegisterCreatorCustomService(Consts.ImbaseFolderTypeID, typeof (Intermech.Imbase.ImbaseObjectsCreators.ImbaseFolderCreator));
          service12.RegisterCreatorCustomService(Consts.ImbaseFavoritesTypeID, typeof (Intermech.Imbase.ImbaseObjectsCreators.ImbaseFolderCreator));
          service12.RegisterCreatorCustomService(Consts.ImbaseTableMixTypeID, typeof (ImbaseTableMixCreator));
        }
        service4.AddNodeType(1, Consts.ImbaseTableRefTypeID, typeof (TableReferenceNode));
        service4.AddNodeType(1, Consts.ImbaseTableTypeID, typeof (TableReferenceNode));
        service4.AddNodeType(1, Consts.ImbaseTableTypeID, typeof (TableRefsObjectNode));
        service4.AddNodeType(1, Consts.ImbaseFolderTypeID, typeof (FolderNode));
        service4.AddViewsProvider(1, Consts.ImbaseFolderTypeID, provider1);
        service4.AddViewsProvider(1, Consts.ImbaseCatalogTypeID, provider1);
        service4.AddViewsProvider(1, Consts.ImbaseTableRefTypeID, provider1);
        service4.AddViewsProvider(1, Consts.ImbaseTableTypeID, provider1);
        service4.AddViewsProvider(1, Consts.ImbaseCatalogRecordTypeID, provider1);
        service4.AddViewsProvider(1, Consts.ImbaseTableMixTypeID, provider1);
        service4.AddViewsProvider(1, Consts.ImbaseFavoritesTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseFolderTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseCatalogTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseTableRefTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseTableTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseCatalogRecordTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseTableMixTypeID, provider1);
        service4.AddViewsProvider(4, Consts.ImbaseFavoritesTypeID, provider1);
        service4.OnMenuTemplateNodeTransformEventHandler += new Intermech.Navigator.ContextMenu.MenuTemplateNodeTransformEventHandler(this.MenuTemplateNodeTransformEventHandler);
        BaseObjectsInfoProvider provider2 = new BaseObjectsInfoProvider();
        foreach (DataRow dataRow in (ServicesManager.GetService(typeof (IClientCache)) as IClientCache).GetTable("IMS_ATTR4OBJ_TYPES").Select("F_ATTRIBUTE_ID = " + Consts.ImbaseObjectRefAttID.ToString()))
          service4.AddViewsProvider(1, Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), (IViewsProvider) provider2);
        ImbaseTableContextView._imageIndex = service6.ImageIndex("imgContains");
        ImbaseTableView._imageIndex = service6.ImageIndex("imgTableView");
        this.LoadPluginResources(serviceProvider, service6);
        ImbaseIndexesView.imageIndex = service6.ImageIndex("imgIndexes");
        BaseObjectsInfoView.imageIndex = service6.ImageIndex("imgProp");
        if (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service13)
        {
          if (service13.GetDescriber(Consts.CreatedObjectAttID) == null)
            service13.RegisterDescriber(Consts.CreatedObjectAttID, (IAttributePropertyDescriber) new CreatedObjectAttrDescriber());
          if (service13.GetDescriber(Consts.ImbaseCatalogRefAttID) == null)
            service13.RegisterDescriber(Consts.ImbaseCatalogRefAttID, (IAttributePropertyDescriber) new ImbaseCatalogRefAttDescriber());
          if (service13.GetDescriber(Consts.ObjectTypeAndAttCatalogLinkID) == null)
            service13.RegisterDescriber(Consts.ObjectTypeAndAttCatalogLinkID, (IAttributePropertyDescriber) new ObjectAndAttLinkAttDescriber());
          if (service13.GetDescriber(Consts.ImbaseTemplateDataAttID) == null)
            service13.RegisterDescriber(Consts.ImbaseTemplateDataAttID, (IAttributePropertyDescriber) new TemplatesObjectsDescriber());
          if (service13.GetDescriber(Consts.ImbaseTemplateAttID) == null)
            service13.RegisterDescriber(Consts.ImbaseTemplateAttID, (IAttributePropertyDescriber) new TemplatesTableRefDescriber());
          int attributeId1 = sessionKeeper.Session.GetAttributeType(new Guid("cad00210-306c-11d8-b4e9-00304f19f545")).AttributeID;
          if (service13.GetDescriber(attributeId1) == null)
            service13.RegisterDescriber(attributeId1, (IAttributePropertyDescriber) new SpecificationSectionDescriber());
          if (service13.GetDescriber(Consts.BlankCodeAttrID) == null)
            service13.RegisterDescriber(Consts.BlankCodeAttrID, (IAttributePropertyDescriber) new BlankCodeAttrDescriber());
          int attributeId2 = sessionKeeper.Session.GetAttributeType(PortalConsts.attributeComparisonAttributes).AttributeID;
          if (service13.GetDescriber(attributeId2) == null)
            service13.RegisterDescriber(attributeId2, (IAttributePropertyDescriber) new ComparisonAttributesDescriber());
          int attributeId3 = sessionKeeper.Session.GetAttributeType(PortalConsts.attributeEnterPoint).AttributeID;
          if (service13.GetDescriber(attributeId3) == null)
            service13.RegisterDescriber(attributeId3, (IAttributePropertyDescriber) new EnterPointDescriber());
          if (service13.GetDescriber(Consts.ImbaseObjectRefAttID) == null)
            service13.RegisterDescriber(Consts.ImbaseObjectRefAttID, (IAttributePropertyDescriber) new ImbaseLinkAttributesDescriber());
        }
        ImbaseCommands.Register();
        ServicesManager.AddService(typeof (IImbaseSelector), (object) new ImbaseSelector());
        ServicesManager.AddService(typeof (IImbaseFilterSelector), (object) new ImbaseFilterSelector());
        ServicesManager.AddService(typeof (ITableViewColorizer), (object) TableColorizer.Instance);
        ServicesManager.AddService(typeof (IImbaseExtendedService), (object) new ImbaseExtendedClientService());
        ServicesManager.AddService(typeof (ITableRowSelector), (object) UserRowSelector.Instance);
        if (ComHost.Configuration.ComSupportActive)
        {
          ComHost.ActivateClassFactory(typeof (ComImbaseAPI));
          ComHost.ActivateClassFactory(typeof (ImMaterial));
        }
        TableLoadHelper.TablesCache = (ITablesCache) new TablesCacheProxy();
        if (serviceProvider.GetService(typeof (IPicturesCache)) is IPicturesCache service14)
          service14.TranslateObject += new TranslateObjectIdEventHandler(this.PicturesCache_TranslateObject);
        if (this._serviceProvider.GetService(typeof (IPluginManager)) is IPluginManager service15)
          service15.LoadComplete += new EventHandler(this.PluginManager_LoadComplete);
        if (serviceProvider.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service16)
        {
          this._imAttr4ObjPropID = service16.RegisterCategoryProps(22, (ICategoryProps) new ImObj4AttrCatProps());
          this._imAttributePropID = service16.RegisterCategoryProps(3, (ICategoryProps) new ImObj4AttrCatProps());
        }
        ISelectObjectDialogService service17 = (ISelectObjectDialogService) ServicesManager.GetService(typeof (ISelectObjectDialogService));
        ImbaseRootNodeDescriptor rootDescriptor = new ImbaseRootNodeDescriptor();
        service17.Register(Consts.ImbaseRootObjectTypeID, (IDescriptor) rootDescriptor);
        service17.Register(Consts.ImbaseFolderTypeID, (IDescriptor) rootDescriptor);
        service17.Register(Consts.ImbaseCatalogTypeID, (IDescriptor) rootDescriptor);
        service17.Register(Consts.ImbaseTableRefTypeID, (IDescriptor) rootDescriptor);
        service17.Register(Consts.ImbaseTableTypeID, (IDescriptor) rootDescriptor);
        service17.Register(Consts.ImbaseCatalogRecordTypeID, (IDescriptor) rootDescriptor);
        Consts.ObjectsFromImbaseNodeCategoryID = service3.Register(Consts.ObjectsFromImbaseNodeGuid);
        service4.AddNodeType(Consts.ObjectsFromImbaseNodeCategoryID, typeof (ObjectsFromImbaseNode));
        service4.AddViewsProvider(Consts.ObjectsFromImbaseNodeCategoryID, (IViewsProvider) new ObjectsFromImbaseViewProvider());
        if (service5 != null)
        {
          Icon icon = service5.GetIcon(1, 0);
          if (icon != null)
            service5.AddIcon(icon, Consts.ObjectsFromImbaseNodeCategoryID);
        }
        ImbaseParamsEditor.RegisterSettingsPage();
        ServiceUtils.GetService<ITableViewColorizer>((object) ServicesManager.ServiceContainer, true).ColorizeRows += new TableView.ColorizeRowsEventHandler(ColorizerEvents.TableViewColorizer_ColorizeRows);
        IImbaseParamsService service18 = ServiceUtils.GetService<IImbaseParamsService>((object) session, true);
        if (service5 != null && service18.CommonParams.CheckApplicabilityBeforeCreateComposition)
        {
          if (service18.CommonParams.FolderApplicabilityIcons.NoRestrictionImage != null)
            service5.AddIcon(service18.CommonParams.FolderApplicabilityIcons.NoRestrictionImage, 4, Consts.ImbaseFolderTypeID, (object) ApplicabilityStatusEnum.NoLimit);
          if (service18.CommonParams.FolderApplicabilityIcons.DenyAddRecordImage != null)
            service5.AddIcon(service18.CommonParams.FolderApplicabilityIcons.DenyAddRecordImage, 4, Consts.ImbaseFolderTypeID, (object) ApplicabilityStatusEnum.ForbiddenUse);
          if (service18.CommonParams.FolderApplicabilityIcons.DenyAddObjectImage != null)
            service5.AddIcon(service18.CommonParams.FolderApplicabilityIcons.DenyAddObjectImage, 4, Consts.ImbaseFolderTypeID, (object) ApplicabilityStatusEnum.LimitedUse);
          if (service18.CommonParams.FolderApplicabilityIcons.DenyAllImage != null)
            service5.AddIcon(service18.CommonParams.FolderApplicabilityIcons.DenyAllImage, 4, Consts.ImbaseFolderTypeID, (object) ApplicabilityStatusEnum.TotalForbiddenUse);
        }
        (ApplicationServices.Container.GetService(typeof (IExceptionHandlerService)) as IExceptionHandlerService).HandleException += new ExceptionHandler(this.ExceptionHandlerService_HandleException);
      }
    }
  }

  private void ExceptionHandlerService_HandleException(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is ImbaseApplicablityException exception))
      return;
    e.Handled = true;
    using (ApplicabilityExceptionForm applicabilityExceptionForm = new ApplicabilityExceptionForm(exception))
    {
      applicabilityExceptionForm.InitializeData();
      int num = (int) applicabilityExceptionForm.ShowDialog();
    }
  }

  private void UpateInternalIndexs(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ITablesIndexerService)) is ITablesIndexerService customService)
    {
      TableIndexerBackGroundTask task = new TableIndexerBackGroundTask((IServiceForBackgroundTask) customService);
      service.AddTask((IBackgroundTask) task);
      task.StartTask((object) null);
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_TablesIndexerService_Null"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void ClearImbaseObjects(object sender, EventArgs e)
  {
    using (ClearImbaseObjectsDlg imbaseObjectsDlg = new ClearImbaseObjectsDlg())
    {
      int num = (int) imbaseObjectsDlg.ShowDialog();
    }
  }

  private void RenameAttsClick(object sender, EventArgs e) => RenameAttributes.Execute();

  private void ImportImbaseTable(object sender, EventArgs e) => ImbaseTableImporter.Execute();

  private void ImbaseSync(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IImbaseSyncService)) is IImbaseSyncService customService)
    {
      ImbaseSyncBackgroundTask task = new ImbaseSyncBackgroundTask((IServiceForBackgroundTask) customService);
      service.AddTask((IBackgroundTask) task);
      task.StartTask((object) null);
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_TablesIndexerService_Null"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void PumpReceptures(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IImbaseTableMixPumpService)) is IImbaseTableMixPumpService customService)
    {
      ImbaseTableMixPumpBackGroundTask task = new ImbaseTableMixPumpBackGroundTask((IServiceForBackgroundTask) customService);
      service.AddTask((IBackgroundTask) task);
      task.StartTask((object) null);
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_TablesIndexerService_Null"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void ConvertOldKeys(object sender, EventArgs e)
  {
    IBackgroundTaskView service = ServicesManager.GetService(typeof (IBackgroundTaskView)) as IBackgroundTaskView;
    if (this._converterTask != null)
      service.DeleteTask(this._converterTask);
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IKeyConverter)) is IKeyConverter customService))
      return;
    this._converterTask = (IBackgroundTask) new KeyConverterTask(customService);
    this._converterTask.Resume();
    service.AddTask(this._converterTask);
  }

  private void PicturesCache_TranslateObject(object sender, TranslateObjectEventArgs e)
  {
    if (e.TypeId != Consts.ImbaseTableRefTypeID)
      return;
    IDBObject dbObject = e.Session.GetObject(e.ObjectId, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.ImbaseTableRefAttID);
    if (attributeById == null)
      return;
    e.NewObjectId = Convert.ToInt64(attributeById.Values[0]);
  }

  private void TechFilterTune(object sender, EventArgs e)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    if (!(ServicesManager.GetService(typeof (IImbaseAPI)) is IImbaseAPI imbaseApi))
      imbaseApi = (IImbaseAPI) new ComImbaseAPI();
    if (imbaseApi == null)
      return;
    string empty4 = string.Empty;
    string empty5 = string.Empty;
    string empty6 = string.Empty;
    CadmechHelper.ShowTables(100, "", ref empty4, ref empty5, ref empty6);
  }

  private void OnViewRootNode(object sender, EventArgs e)
  {
    Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new ImbaseRootNodeDescriptor(), (System.IServiceProvider) null, new GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.DefaultSupportedColumnsObjects));
  }

  private void PluginManager_LoadComplete(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (IFormDesignerFormLinksManager)) is IFormDesignerFormLinksManager service1)
      service1.RegisterProvider(new FormDesignerFormLinksProviderType(LocalizationHolder.rm.GetString("Imbase.Client_138"), ImbaseTypeFormLinkProvider.sProviderGuid, typeof (ImbaseTypeFormLinkProvider)));
    if (!(this._serviceProvider.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service2))
      return;
    if (this._imAttr4ObjPropID == -1)
      this._imAttr4ObjPropID = service2.RegisterCategoryProps(22, (ICategoryProps) new ImObj4AttrCatProps());
    if (this._imAttributePropID != -1)
      return;
    this._imAttributePropID = service2.RegisterCategoryProps(3, (ICategoryProps) new ImObj4AttrCatProps());
  }

  private void LoadPluginResources(System.IServiceProvider serviceProvider, INamedImageList namedImageList)
  {
    if (namedImageList == null)
      return;
    Bitmap resourceData = ResourceHelper.GetResourceData<Bitmap>(this.GetType().Assembly, "Intermech.Imbase.Resources.IndexesAll.bmp");
    if (resourceData == null)
      return;
    resourceData.MakeTransparent();
    namedImageList.AddStrip((Image) resourceData, new string[2]
    {
      "imgIndexes",
      "imgFindByIndex"
    });
  }

  private void OnNavigatorNewWindowOpening(object sender, NotificationEventArgs e)
  {
    if (!(e is NavigatorWindowOpenedEventArgs windowOpenedEventArgs) || !(windowOpenedEventArgs.NavigatorWindow.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor rootDescriptor))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long objectId = rootDescriptor.ObjectID;
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectId);
      if (objectInfo.Empty || objectInfo.ObjectTypeID != Consts.ImbaseTableRefTypeID || !(windowOpenedEventArgs.ServiceProvider?.GetService(typeof (ChildrenViewActionContext)) is ChildrenViewActionContext service) || !(service.SourceActionNodeID is NodeID sourceActionNodeId))
        return;
      IDBObject dbObject = session.GetObject(sourceActionNodeId.ObjectID);
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(Consts.ImbaseObjectRefAttID);
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(Consts.ImbaseInternalOldKeyAttID);
      if (attributeById1 == null || attributeById2 == null)
        return;
      SelectedRecords.Add(Convert.ToInt64(attributeById1.Values[0]), new long[1]
      {
        Convert.ToInt64(attributeById2.Values[0])
      });
    }
  }

  private void RestructuringTables(object sender, EventArgs e)
  {
    using (RestructuringTablesDlg restructuringTablesDlg = new RestructuringTablesDlg())
    {
      if (restructuringTablesDlg.ShowDialog() != DialogResult.OK)
        return;
      IBackgroundTaskView service = ServicesManager.GetService(typeof (IBackgroundTaskView)) as IBackgroundTaskView;
      if (this._restructuringTask != null)
        service.DeleteTask(this._restructuringTask);
      if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IImbaseRestructuringTablesService)) is IImbaseRestructuringTablesService customService))
        return;
      this._restructuringTask = (IBackgroundTask) new ImbaseRestructuringTablesBackgroundTask(customService, restructuringTablesDlg.SourceID, restructuringTablesDlg.Settings);
      this._restructuringTask.Resume();
      service.AddTask(this._restructuringTask);
    }
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }

  private void LogMessage(string category, string message)
  {
    if (this._outputView == null)
      return;
    this.LogMessageCore(category, message);
  }

  private void LogError(string category, string errorMessage)
  {
    if (this._outputView == null)
      return;
    this.LogMessageCore(category, errorMessage);
    this._outputView.ShowView();
    this._outputView.Activate(category);
  }

  private void LogMessageCore(string category, string message)
  {
    foreach (string text in message.Split(ImbaseStartup.LineSeparators, StringSplitOptions.None))
      this._outputView.WriteString(category, text);
  }

  private void ActivateViewEventHandler(object sender, ActivateViewEventArgs e)
  {
    if (e == null || e.NewSelectedNodes == null || e.NewSelectedNodes.Count == 0)
      return;
    int num = e.OldSelectedNodes == null || e.OldSelectedNodes.Count <= 0 ? 0 : (e.OldSelectedNodes[0].CategoryID == Consts.CatalogsListCategoryId ? 1 : 0);
    bool flag1 = e.OldSelectedNodes != null && e.OldSelectedNodes.Count > 0 && e.OldSelectedNodes[0].CategoryID == 1 && (MetaDataHelper.IsObjectTypeChildOf(e.OldSelectedNodes[0].TypeID, Consts.ImbaseTableTypeID) || MetaDataHelper.IsObjectTypeChildOf(e.OldSelectedNodes[0].TypeID, Consts.ImbaseTableRefTypeID));
    bool flag2 = e.NewSelectedNodes[0].CategoryID == 1 && (MetaDataHelper.IsObjectTypeChildOf(e.NewSelectedNodes[0].TypeID, Consts.ImbaseTableTypeID) || MetaDataHelper.IsObjectTypeChildOf(e.NewSelectedNodes[0].TypeID, Consts.ImbaseTableRefTypeID));
    bool flag3 = e.OldSelectedNodes != null && e.OldSelectedNodes.Count > 0 && e.OldSelectedNodes[0].CategoryID == 1 && (MetaDataHelper.IsObjectTypeChildOf(e.OldSelectedNodes[0].TypeID, Consts.ImbaseCatalogTypeID) || MetaDataHelper.IsObjectTypeChildOf(e.OldSelectedNodes[0].TypeID, Consts.ImbaseFolderTypeID) || MetaDataHelper.IsObjectTypeChildOf(e.OldSelectedNodes[0].TypeID, Consts.ImbaseCatalogRecordTypeID));
    bool flag4 = e.NewSelectedNodes != null && e.NewSelectedNodes.Count > 0 && e.NewSelectedNodes[0].CategoryID == 1 && (MetaDataHelper.IsObjectTypeChildOf(e.NewSelectedNodes[0].TypeID, Consts.ImbaseCatalogTypeID) || MetaDataHelper.IsObjectTypeChildOf(e.NewSelectedNodes[0].TypeID, Consts.ImbaseFolderTypeID) || MetaDataHelper.IsObjectTypeChildOf(e.NewSelectedNodes[0].TypeID, Consts.ImbaseCatalogRecordTypeID));
    if (flag2 && !flag1)
      e.NewViewName = "ImbaseTableView";
    IFoldersView currActiveView = e.CurrActiveView as IFoldersView;
    if (flag4)
    {
      if (currActiveView != null)
      {
        if (currActiveView.RemainActiveView)
          return;
        e.NewViewName = "ChildrenView";
        return;
      }
      if (!flag3 && currActiveView == null)
        e.NewViewName = "ChildrenView";
    }
    if (e.OldSelectedNodes.Count != 0)
      return;
    if (flag2)
      e.NewViewName = "ImbaseTableView";
    if (!flag4)
      return;
    e.NewViewName = "ChildrenView";
  }

  private void MenuTemplateNodeTransformEventHandler(
    object sender,
    MenuTemplateNodeTransformEventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service) || e.Items == null || e.Items.Count != 1 || e.MenuTemplateNode == null || e.MenuTemplateNode.Name != "CreateCatalogsNode")
      return;
    int index = service.IndexOf(4, Consts.ImbaseCatalogTypeID);
    e.MenuTemplateNode.Image = index >= 0 ? service.ImageList.Images[index] : (Image) null;
  }
}
