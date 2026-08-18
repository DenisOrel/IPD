// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.wfClientPlugin
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using ImSSP;
using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Forums;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Project.Controls;
using Intermech.Protection;
using Intermech.Scripting.Services;
using Intermech.Search;
using Intermech.Workflow.Base;
using Intermech.Workflow.Client.AutoNotification;
using Intermech.Workflow.Client.Email;
using Intermech.Workflow.Client.Properties;
using Intermech.Workflow.Design;
using Intermech.Workflow.Design.ScriptPad;
using Ninject;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class wfClientPlugin(IOCBasedPackageParameters createParameters) : 
  IOCBasedPackage(createParameters, LocalizationHolder.rm.GetString("Workflow.Client_30")),
  IConfigurable,
  ICommandTarget,
  IPackageExtension
{
  private MenuTemplateNode _launchNode;
  private readonly string _launchCaption = LocalizationHolder.rm.GetString("Workflow.Client_31");
  private MenuButtonItem _globalLaunchItem;
  private System.IServiceProvider _serviceProvider;
  public static readonly string MailWindowName = "MailWindow";
  private Guid MailWindowGuid = new Guid("CF666FB8-84B1-4b01-BA70-7EA17B353BF8");
  private bool _postInited;

  protected override void DoInitializeIOCContainer()
  {
    base.DoInitializeIOCContainer();
    this.GlobalIOCContainer.Bind<ICheckMailService>().To<CheckMailService>().InSingletonScope();
    this.IOCContainer.Bind<MessageCommandProvider>().ToSelf();
  }

  protected override void DoUnload()
  {
    this._globalLaunchItem = (MenuButtonItem) null;
    IObjectCreatorService service = (IObjectCreatorService) ApplicationServices.Container.GetService(typeof (IObjectCreatorService));
    service.UnregisterCreatorCustomService(wfConsts.ProcessesTypeID, typeof (NewProcessCreator));
    service.UnregisterCreatorCustomService(wfConsts.FileTypeID, typeof (NewFileCreator));
    service.UnregisterCreatorCustomService(wfConsts.AutoNotificationTypeID, typeof (AutoNotificationCreator));
    base.DoUnload();
  }

  protected override void DoLoad()
  {
    base.DoLoad();
    this._serviceProvider = (System.IServiceProvider) ApplicationServices.Container;
    IPluginManager service1 = (IPluginManager) ApplicationServices.Container.GetService(typeof (IPluginManager));
    if (service1 != null)
      service1.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    if (!(ApplicationServices.Container.GetService(typeof (ILicenser)) is ILicenser service2))
      throw new ProtectionException(LocalizationHolder.rm.GetString("Workflow.Client_32"));
    int appId = 366;
    byte[][] numArray = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 225,
        (byte) 180,
        (byte) 110,
        (byte) 14,
        (byte) 129,
        (byte) 39,
        (byte) 110,
        (byte) 38,
        (byte) 167,
        (byte) 77,
        (byte) 187,
        (byte) 199,
        (byte) 247,
        (byte) 76,
        (byte) 226,
        (byte) 204
      },
      new byte[16 /*0x10*/]
      {
        (byte) 31 /*0x1F*/,
        (byte) 233,
        (byte) 182,
        (byte) 254,
        (byte) 79,
        (byte) 153,
        (byte) 161,
        (byte) 178,
        (byte) 12,
        (byte) 234,
        (byte) 205,
        (byte) 234,
        (byte) 188,
        (byte) 86,
        (byte) 248,
        (byte) 100
      },
      new byte[16 /*0x10*/]
      {
        (byte) 16 /*0x10*/,
        (byte) 125,
        (byte) 92,
        (byte) 74,
        (byte) 189,
        (byte) 215,
        (byte) 130,
        (byte) 110,
        (byte) 78,
        (byte) 204,
        (byte) 183,
        (byte) 179,
        (byte) 10,
        (byte) 67,
        (byte) 159,
        (byte) 200
      },
      new byte[16 /*0x10*/]
      {
        (byte) 206,
        (byte) 234,
        (byte) 110,
        (byte) 194,
        (byte) 141,
        (byte) 178,
        (byte) 214,
        (byte) 121,
        (byte) 194,
        (byte) 119,
        (byte) 174,
        (byte) 32 /*0x20*/,
        (byte) 166,
        (byte) 228,
        (byte) 12,
        (byte) 56
      },
      new byte[16 /*0x10*/]
      {
        (byte) 171,
        (byte) 73,
        (byte) 252,
        (byte) 184,
        (byte) 175,
        (byte) 150,
        (byte) 177,
        (byte) 19,
        (byte) 183,
        (byte) 114,
        (byte) 41,
        (byte) 128 /*0x80*/,
        (byte) 114,
        (byte) 67,
        (byte) 126,
        (byte) 23
      },
      new byte[16 /*0x10*/]
      {
        (byte) 147,
        (byte) 104,
        (byte) 146,
        (byte) 23,
        (byte) 202,
        (byte) 176 /*0xB0*/,
        (byte) 234,
        (byte) 210,
        (byte) 105,
        (byte) 51,
        (byte) 27,
        (byte) 237,
        (byte) 196,
        (byte) 179,
        (byte) 222,
        (byte) 117
      },
      new byte[16 /*0x10*/]
      {
        (byte) 185,
        (byte) 164,
        (byte) 9,
        (byte) 98,
        (byte) 227,
        (byte) 230,
        (byte) 110,
        (byte) 10,
        (byte) 50,
        (byte) 0,
        (byte) 192 /*0xC0*/,
        (byte) 181,
        (byte) 111,
        (byte) 186,
        (byte) 27,
        (byte) 99
      },
      new byte[16 /*0x10*/]
      {
        (byte) 158,
        (byte) 189,
        (byte) 178,
        (byte) 183,
        (byte) 153,
        (byte) 254,
        (byte) 80 /*0x50*/,
        (byte) 96 /*0x60*/,
        (byte) 198,
        (byte) 3,
        (byte) 205,
        (byte) 178,
        (byte) 87,
        (byte) 1,
        (byte) 60,
        (byte) 241
      },
      new byte[16 /*0x10*/]
      {
        (byte) 174,
        (byte) 242,
        (byte) 27,
        (byte) 174,
        (byte) 80 /*0x50*/,
        (byte) 42,
        (byte) 28,
        (byte) 122,
        (byte) 203,
        (byte) 72,
        (byte) 146,
        (byte) 75,
        (byte) 173,
        (byte) 1,
        (byte) 252,
        (byte) 10
      },
      new byte[16 /*0x10*/]
      {
        (byte) 111,
        (byte) 159,
        (byte) 148,
        (byte) 122,
        (byte) 50,
        (byte) 12,
        (byte) 130,
        (byte) 101,
        (byte) 51,
        (byte) 68,
        (byte) 215,
        (byte) 91,
        (byte) 223,
        (byte) 156,
        (byte) 135,
        (byte) 223
      },
      new byte[16 /*0x10*/]
      {
        (byte) 77,
        (byte) 159,
        (byte) 249,
        (byte) 102,
        (byte) 58,
        (byte) 60,
        (byte) 228,
        (byte) 196,
        (byte) 218,
        (byte) 242,
        (byte) 133,
        (byte) 175,
        (byte) 214,
        (byte) 252,
        (byte) 164,
        (byte) 252
      },
      new byte[16 /*0x10*/]
      {
        (byte) 142,
        (byte) 42,
        (byte) 87,
        (byte) 126,
        (byte) 73,
        (byte) 142,
        (byte) 32 /*0x20*/,
        (byte) 100,
        (byte) 224 /*0xE0*/,
        (byte) 127 /*0x7F*/,
        (byte) 19,
        (byte) 69,
        (byte) 109,
        (byte) 165,
        (byte) 73,
        (byte) 41
      },
      new byte[16 /*0x10*/]
      {
        (byte) 86,
        (byte) 197,
        (byte) 210,
        (byte) 127 /*0x7F*/,
        (byte) 39,
        (byte) 207,
        (byte) 199,
        (byte) 143,
        (byte) 135,
        (byte) 143,
        (byte) 151,
        (byte) 154,
        (byte) 84,
        (byte) 35,
        (byte) 92,
        (byte) 49
      },
      new byte[16 /*0x10*/]
      {
        (byte) 58,
        (byte) 83,
        (byte) 242,
        (byte) 202,
        (byte) 120,
        (byte) 200,
        (byte) 194,
        byte.MaxValue,
        (byte) 112 /*0x70*/,
        (byte) 10,
        (byte) 18,
        (byte) 16 /*0x10*/,
        (byte) 248,
        (byte) 220,
        (byte) 77,
        (byte) 5
      },
      new byte[16 /*0x10*/]
      {
        (byte) 3,
        (byte) 181,
        (byte) 103,
        (byte) 207,
        (byte) 193,
        (byte) 180,
        (byte) 41,
        (byte) 113,
        (byte) 14,
        (byte) 87,
        (byte) 211,
        (byte) 94,
        (byte) 164,
        (byte) 109,
        (byte) 163,
        (byte) 206
      },
      new byte[16 /*0x10*/]
      {
        (byte) 239,
        (byte) 203,
        (byte) 239,
        (byte) 137,
        (byte) 128 /*0x80*/,
        (byte) 162,
        (byte) 202,
        (byte) 19,
        (byte) 35,
        (byte) 191,
        (byte) 140,
        (byte) 29,
        (byte) 165,
        (byte) 68,
        (byte) 64 /*0x40*/,
        (byte) 112 /*0x70*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 164,
        (byte) 150,
        (byte) 244,
        (byte) 232,
        (byte) 46,
        (byte) 125,
        (byte) 227,
        (byte) 125,
        (byte) 216,
        (byte) 106,
        (byte) 222,
        (byte) 170,
        (byte) 165,
        (byte) 73,
        (byte) 112 /*0x70*/,
        (byte) 183
      },
      new byte[16 /*0x10*/]
      {
        (byte) 194,
        (byte) 12,
        (byte) 38,
        (byte) 246,
        byte.MaxValue,
        (byte) 224 /*0xE0*/,
        (byte) 165,
        (byte) 6,
        (byte) 90,
        (byte) 175,
        (byte) 93,
        (byte) 36,
        (byte) 251,
        (byte) 198,
        (byte) 35,
        (byte) 143
      },
      new byte[16 /*0x10*/]
      {
        (byte) 149,
        (byte) 84,
        (byte) 171,
        (byte) 32 /*0x20*/,
        (byte) 149,
        (byte) 212,
        (byte) 59,
        (byte) 161,
        (byte) 155,
        (byte) 97,
        (byte) 112 /*0x70*/,
        (byte) 208 /*0xD0*/,
        (byte) 252,
        (byte) 105,
        (byte) 235,
        (byte) 26
      },
      new byte[16 /*0x10*/]
      {
        (byte) 42,
        (byte) 210,
        (byte) 231,
        (byte) 198,
        (byte) 18,
        (byte) 71,
        (byte) 32 /*0x20*/,
        (byte) 101,
        byte.MaxValue,
        (byte) 113,
        (byte) 107,
        (byte) 82,
        (byte) 43,
        (byte) 18,
        (byte) 170,
        (byte) 45
      },
      new byte[16 /*0x10*/]
      {
        (byte) 236,
        (byte) 200,
        (byte) 246,
        (byte) 189,
        (byte) 47,
        (byte) 116,
        (byte) 2,
        (byte) 134,
        (byte) 210,
        (byte) 233,
        (byte) 247,
        (byte) 87,
        (byte) 214,
        (byte) 127 /*0x7F*/,
        (byte) 204,
        (byte) 196
      },
      new byte[16 /*0x10*/]
      {
        (byte) 120,
        (byte) 252,
        (byte) 143,
        (byte) 1,
        (byte) 212,
        (byte) 145,
        (byte) 158,
        (byte) 218,
        (byte) 127 /*0x7F*/,
        (byte) 125,
        (byte) 244,
        (byte) 43,
        (byte) 32 /*0x20*/,
        (byte) 166,
        (byte) 115,
        (byte) 160 /*0xA0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 231,
        (byte) 28,
        (byte) 200,
        (byte) 79,
        (byte) 194,
        (byte) 130,
        (byte) 57,
        (byte) 208 /*0xD0*/,
        (byte) 90,
        (byte) 82,
        (byte) 76,
        (byte) 205,
        (byte) 12,
        (byte) 18,
        (byte) 129,
        (byte) 111
      },
      new byte[16 /*0x10*/]
      {
        (byte) 105,
        (byte) 166,
        (byte) 114,
        (byte) 70,
        (byte) 66,
        (byte) 194,
        (byte) 243,
        (byte) 223,
        (byte) 66,
        (byte) 110,
        (byte) 200,
        (byte) 105,
        (byte) 161,
        (byte) 85,
        (byte) 43,
        (byte) 228
      },
      new byte[16 /*0x10*/]
      {
        (byte) 126,
        (byte) 196,
        (byte) 190,
        (byte) 220,
        (byte) 83,
        (byte) 178,
        (byte) 136,
        (byte) 33,
        (byte) 82,
        (byte) 36,
        (byte) 78,
        (byte) 172,
        (byte) 220,
        (byte) 228,
        (byte) 203,
        (byte) 28
      },
      new byte[16 /*0x10*/]
      {
        (byte) 252,
        (byte) 22,
        (byte) 25,
        (byte) 75,
        (byte) 226,
        (byte) 87,
        (byte) 4,
        (byte) 98,
        (byte) 252,
        (byte) 49,
        (byte) 213,
        (byte) 183,
        (byte) 235,
        (byte) 14,
        (byte) 147,
        (byte) 167
      },
      new byte[16 /*0x10*/]
      {
        (byte) 149,
        (byte) 172,
        (byte) 233,
        (byte) 223,
        (byte) 160 /*0xA0*/,
        (byte) 156,
        (byte) 87,
        (byte) 105,
        (byte) 143,
        (byte) 37,
        (byte) 98,
        (byte) 220,
        (byte) 39,
        (byte) 53,
        (byte) 122,
        (byte) 191
      },
      new byte[16 /*0x10*/]
      {
        (byte) 60,
        (byte) 186,
        (byte) 116,
        (byte) 148,
        (byte) 66,
        (byte) 75,
        (byte) 158,
        (byte) 50,
        (byte) 169,
        (byte) 75,
        (byte) 148,
        (byte) 86,
        (byte) 136,
        (byte) 121,
        (byte) 146,
        (byte) 25
      },
      new byte[16 /*0x10*/]
      {
        (byte) 103,
        (byte) 135,
        (byte) 216,
        (byte) 18,
        (byte) 192 /*0xC0*/,
        (byte) 239,
        (byte) 113,
        (byte) 52,
        (byte) 148,
        (byte) 166,
        (byte) 252,
        (byte) 157,
        (byte) 52,
        (byte) 141,
        (byte) 243,
        (byte) 118
      },
      new byte[16 /*0x10*/]
      {
        (byte) 230,
        (byte) 46,
        (byte) 69,
        (byte) 90,
        (byte) 22,
        (byte) 213,
        (byte) 33,
        (byte) 154,
        (byte) 99,
        (byte) 138,
        (byte) 17,
        (byte) 71,
        (byte) 72,
        (byte) 192 /*0xC0*/,
        (byte) 3,
        (byte) 189
      },
      new byte[16 /*0x10*/]
      {
        (byte) 134,
        (byte) 47,
        (byte) 55,
        (byte) 96 /*0x60*/,
        (byte) 41,
        (byte) 162,
        (byte) 206,
        (byte) 226,
        (byte) 108,
        (byte) 85,
        (byte) 76,
        (byte) 45,
        (byte) 79,
        (byte) 171,
        (byte) 249,
        (byte) 29
      },
      new byte[16 /*0x10*/]
      {
        (byte) 19,
        (byte) 199,
        (byte) 152,
        (byte) 58,
        (byte) 181,
        (byte) 85,
        (byte) 89,
        (byte) 174,
        (byte) 126,
        (byte) 157,
        (byte) 231,
        (byte) 44,
        (byte) 230,
        (byte) 117,
        (byte) 45,
        (byte) 48 /*0x30*/
      }
    };
    service2.AllocateLicense(appId);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      object obj = (object) null;
      try
      {
        obj = sessionKeeper.Session.GetCustomService(typeof (IRouterService));
      }
      catch
      {
      }
      if (obj == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Client_33"), (object) this.Name));
      GlobalMailSettings.Init(sessionKeeper.Session);
      ClearOldProcessSettings.Init();
    }
    Library.Init((IPackage) this, (System.IServiceProvider) ApplicationServices.Container);
    MailSettings.Init();
    BaseHolder.CommandManager.AddTarget((ICommandTarget) this);
    MenuTemplate contextMenuTemplate = BaseHolder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("MarkRead", LocalizationHolder.rm.GetString("Workflow.Client_34"), -1, 0, 0));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("MarkUnread", LocalizationHolder.rm.GetString("Workflow.Client_35"), -1, 0, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode(sc_21674.ssp_workflow_21675(), LocalizationHolder.rm.GetString("Workflow.Client_36"), BaseHolder.NamedList.ImageIndex("wfNext"), 1, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SendToBack", LocalizationHolder.rm.GetString("Workflow.Client_37"), BaseHolder.NamedList.ImageIndex("wfBack"), 1, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AcceptWO", LocalizationHolder.rm.GetString("Workflow.Client_38"), BaseHolder.NamedList.ImageIndex("wfAcceptWO"), 1, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RejectWO", LocalizationHolder.rm.GetString("Workflow.Client_39"), BaseHolder.NamedList.ImageIndex("wfRejectWO"), 1, 2));
      MenuTemplateNode node = new MenuTemplateNode("Process", LocalizationHolder.rm.GetString("Workflow.Client_40"), BaseHolder.NamedList.ImageIndex("wfProcess"), 2, 0);
      contextMenuTemplate.Nodes.Add(node);
      node.Nodes.Add(new MenuTemplateNode("ViewProcess", LocalizationHolder.rm.GetString("Workflow.Client_41"), BaseHolder.NamedList.ImageIndex("wfViewProcess"), 0, 1, Keys.F3));
      node.Nodes.Add(new MenuTemplateNode("EditProcess", LocalizationHolder.rm.GetString("Workflow.Client_42"), -1, 0, 2));
      node.Nodes.Add(new MenuTemplateNode("AbortProcess", LocalizationHolder.rm.GetString("Workflow.Client_43"), BaseHolder.NamedList.ImageIndex("wfAbort"), 0, 3));
      node.Nodes.Add(new MenuTemplateNode("ReplaceParticipant", LocalizationHolder.rm.GetString("ReplaceParticipantCmd"), -1, 1, 4));
      node.Nodes.Add(new MenuTemplateNode("Recall", LocalizationHolder.rm.GetString("RecallCmd"), -1, 1, 5));
      node.Nodes.Add(new MenuTemplateNode("ProcessHistory", LocalizationHolder.rm.GetString("Workflow.Client_44"), -1, 2, 6, Keys.H | Keys.Control));
      this._launchNode = new MenuTemplateNode("RLaunchProcess", this._launchCaption, Intermech.Workflow.Design.Holder.LaunchProcessImageIndex, 41, 20);
      contextMenuTemplate.Nodes.Add(this._launchNode);
      this._launchNode.Nodes.Add(new MenuTemplateNode("LaunchProcess", LocalizationHolder.rm.GetString("Workflow.Client_Select"), -1, 0, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("DelMessage", LocalizationHolder.rm.GetString("Workflow.Client_46"), BaseHolder.NamedList.ImageIndex("wfDelete"), 3, 0, Keys.Delete));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("UndelMessage", LocalizationHolder.rm.GetString("Workflow.Client_47"), BaseHolder.NamedList.ImageIndex("wfUndelete"), 3, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode(sc_21674.ssp_workflow_21676(), LocalizationHolder.rm.GetString("Workflow.Client_48"), BaseHolder.NamedList.ImageIndex("wfAddAttach"), 0, 0));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("wfAttachFile", LocalizationHolder.rm.GetString("AttachFile"), -1, 0, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("wfDetach", LocalizationHolder.rm.GetString("Workflow.Client_49"), BaseHolder.NamedList.ImageIndex("wfRemoveAttach"), 0, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("DownloadMessages", LocalizationHolder.rm.GetString("Workflow.Client_81"), -1, 0, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ReplaceFile", LocalizationHolder.rm.GetString("ReplaceFile"), -1, 1, 0));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    MessageCommandProvider provider1 = this.IOCContainer.Get<MessageCommandProvider>();
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.ProcessAtomsTypeID, (ICommandsProvider) provider1);
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.ProcessesTypeID, (ICommandsProvider) provider1);
    BaseHolder.Factory.AddViewsProvider(1, wfConsts.ProcessAtomsTypeID, (IViewsProvider) new MailPropProvider());
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.WorkOfferTypeID, (ICommandsProvider) new WOCommandProvider(true));
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.MessageTypeID, (ICommandsProvider) new WOCommandProvider(false));
    RoutingObjectsCommandProvider provider2 = new RoutingObjectsCommandProvider();
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.SchemesTypeID, (ICommandsProvider) provider2);
    foreach (int applicableAttachmentType in wfFunx.GetApplicableAttachmentTypes())
      BaseHolder.Factory.AddCommandsProvider(1, applicableAttachmentType, (ICommandsProvider) provider2);
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.FileTypeID, (ICommandsProvider) new FileCommandProvider());
    Intermech.Workflow.Client.Services.Start();
    if (ApplicationServices.Container.GetService(typeof (IMainMenuService)) is IMainMenuService service4)
    {
      MenuButtonItem menuButtonItem1 = new MenuButtonItem(this.Name);
      menuButtonItem1.CommandName = "ShowMail";
      MenuButtonItem menuButtonItem2 = menuButtonItem1;
      menuButtonItem2.Click += new EventHandler(this.ShowMail);
      if (ApplicationServices.Container.GetService(typeof (IWellKnownWindowsOpenService)) is IWellKnownWindowsOpenService service3)
        service3.RegisterWindowOpeningHandler(wfClientPlugin.MailWindowName, new EventHandler(this.ShowMail));
      menuButtonItem2.ImageIndex = Intermech.Workflow.Design.Holder.MailImageIndex;
      MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
      {
        menuButtonItem2
      };
      service4.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Second, menuButtonItemArray);
    }
    MenuBar menuBar1 = ((BarManager) ApplicationServices.Container.GetService(typeof (BarManager))).MenuBar;
    MenuBarItem menuBar2 = menuBar1.FindMenuBar(BaseHolder.KeyMenuBarName);
    if (menuBar2 != null)
    {
      MenuBarItem menuBarItem1 = new MenuBarItem(LocalizationHolder.rm.GetString("Workflow.Client_50"));
      menuBarItem1.CommandName = "Processes";
      MenuBarItem menuBarItem2 = menuBarItem1;
      this._globalLaunchItem = new MenuButtonItem(this._launchCaption, Intermech.Workflow.Design.Holder.LaunchProcessImageIndex);
      menuBarItem2.Items.Add((ToolbarItemBase) this._globalLaunchItem);
      this._globalLaunchItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.LaunchItem_BeforePopup);
      MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Client_Select"));
      this._globalLaunchItem.Items.Add((ToolbarItemBase) menuButtonItem3);
      menuButtonItem3.Click += new EventHandler(this.LaunchProcessHandler);
      MenuButtonItem menuButtonItem4 = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Client_52"));
      menuButtonItem4.CommandName = "ShowProcesses";
      menuButtonItem4.BeginGroup = true;
      menuButtonItem4.ShortcutActive = true;
      menuButtonItem4.PrimaryShortcut = Keys.P | Keys.Control | Keys.Alt;
      MenuButtonItem menuButtonItem5 = menuButtonItem4;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem5);
      menuBarItem2.Items.Add((ToolbarItemBase) menuButtonItem5);
      MenuButtonItem menuButtonItem6 = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Client_53"));
      menuButtonItem6.CommandName = "RevisionHistory";
      menuButtonItem6.ShortcutActive = true;
      menuButtonItem6.PrimaryShortcut = Keys.R | Keys.Control | Keys.Alt;
      MenuButtonItem menuButtonItem7 = menuButtonItem6;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem7);
      menuBarItem2.Items.Add((ToolbarItemBase) menuButtonItem7);
      menuBar1.Items.Insert(menuBar2.Index, (ToolbarItemBase) menuBarItem2);
    }
    INavigationBar service5 = (INavigationBar) ApplicationServices.Container.GetService(typeof (INavigationBar));
    if (service5 != null && service5.FindPane("appPane") is IAppPane pane)
      pane.Add(this.Name, new EventHandler(this.ShowMail), Intermech.Workflow.Design.Holder.MailImageIndex);
    if (this._launchNode != null)
      Intermech.Navigator.ContextMenu.Services.AfterCreateMenu += new AfterCreateMenuHandler(this.AfterCreateMenu);
    if (ApplicationServices.Container.GetService(typeof (IStartupService)) is IStartupService service6)
      service6.MainFormShown += new EventHandler(this.startupService_MainFormShown);
    BaseHolder.Factory.AddViewsProvider(1, (IViewsProvider) new NotifyViewProvider());
    this.RegisterAdjustableViews();
    ((IContentProvider) ApplicationServices.Container.GetService(typeof (IContentProvider))).ContentCallback += new GetContentCallback(this.contProvider_ContentCallback);
    try
    {
      wfCommandProvider provider3 = new wfCommandProvider();
      BaseHolder.Factory.AddCommandsProvider(1, wfConsts.SchemesTypeID, (ICommandsProvider) provider3);
      BaseHolder.Factory.AddCommandsProvider(1, wfConsts.ProcessesTypeID, (ICommandsProvider) provider3);
    }
    catch
    {
    }
    BaseHolder.Factory.AddViewsProvider(1, wfConsts.SchemesTypeID, (IViewsProvider) new SchemeViewsProvider());
    BaseHolder.Factory.AddViewsProvider(1, wfConsts.ProcessesTypeID, (IViewsProvider) new SchemeViewsProvider());
    BaseHolder.Factory.AddViewsProvider(1, wfConsts.AutoNotificationTypeID, (IViewsProvider) new AutoNotificationViewProvider());
    IObjectCreatorService service7 = (IObjectCreatorService) ApplicationServices.Container.GetService(typeof (IObjectCreatorService));
    service7.RegisterCreatorCustomService(wfConsts.ProcessesTypeID, typeof (NewProcessCreator));
    service7.RegisterCreatorCustomService(wfConsts.FileTypeID, typeof (NewFileCreator));
    service7.RegisterCreatorCustomService(wfConsts.AutoNotificationTypeID, typeof (AutoNotificationCreator));
    MailNode.Init();
    if (!NotificationEventNames.CriticalEventNames.Contains("UnreadCountChanged"))
      NotificationEventNames.CriticalEventNames.Add("UnreadCountChanged");
    IPropertyPagesService service8 = (IPropertyPagesService) ApplicationServices.Container.GetService(typeof (IPropertyPagesService));
    if (service8 != null)
    {
      service8.AddPage(LocalizationHolder.GetString("SettingsPropertyPagePath"), (IPropertyPage) new GlobalMailSettingsPropertyPage());
      service8.AddPage($"{LocalizationHolder.GetString("SettingsPropertyPagePath")}\\{LocalizationHolder.GetString("SettingsPropertyPageAutoLaunch")}", (IPropertyPage) new AutoLaunchPropertyPage());
      IEmailService customService = (IEmailService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService));
      ICurrentUserAndRole service9 = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (customService != null)
      {
        if (service9 != null && service9.IsAdmin)
        {
          service8.AddPage(LocalizationHolder.GetString("SettingsPropertyPagePath") + "\\Очистка устаревших процессов", (IPropertyPage) new ClearOldProcessPropertyPage());
          service8.AddPage(LocalizationHolder.rm.GetString("Workflow.Client_83"), (IPropertyPage) new TimeTableControl());
        }
        EmailAccaunt[] accaunts1 = customService.GetAccaunts(service9.UserID, true);
        if (accaunts1 != null)
          service8.AddPage(LocalizationHolder.rm.GetString("Workflow.Client_84"), (IPropertyPage) new OwneredAccauntsControl(accaunts1));
        EmailAccaunt[] accaunts2 = customService.GetAccaunts(service9.UserID, false);
        if (accaunts2 != null)
        {
          ((IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes))).Register(EmailConsts.EMailMessageColumnSchemeGuid, (INodeColumnScheme) new EMailMessageColumnScheme());
          EmailConsts.CategoryEmail = BaseHolder.GuidMapper.Register(EmailConsts.CategoryEmailGuid);
          BaseHolder.Factory.AddNodeType(EmailConsts.CategoryEmail, typeof (EmailNode));
          BaseHolder.Factory.AddViewsProvider(EmailConsts.CategoryEmail, (IViewsProvider) new EmailViewProvider());
          BaseHolder.Factory.AddCommandsProvider(EmailConsts.CategoryEmail, (ICommandsProvider) new EmailNodeCommandProvider());
          ((ICategoryTypeIconService) ApplicationServices.Container.GetService(typeof (ICategoryTypeIconService)))?.AddIcon(Resources.MailBoxIcon, EmailConsts.CategoryEmail, 0);
          EmailConsts.CategoryEmailMessage = BaseHolder.GuidMapper.Register(EmailConsts.CategoryEmailMessageGuid);
          for (int index = 0; index < accaunts2.Length; ++index)
          {
            EmailDescriptor emailDescriptor = new EmailDescriptor(accaunts2[index].Email);
            BaseHolder.Factory.AddGlobalNode(Guid.NewGuid(), (IDescriptor) emailDescriptor, 21);
          }
        }
      }
    }
    BaseHolder.Factory.AddViewsProvider(1, wfConsts.objtypeEmailMessagesID, (IViewsProvider) new EmailMessagesViewProvider());
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.ActivitiesTypeID, (ICommandsProvider) new ActivityCommands());
    BaseHolder.Factory.AddViewsProvider(1, (IViewsProvider) new ForumViewsProvider());
    ApplicationServices.Container.AddService(typeof (IUserMessageSelector), (object) new UserMessageSelector());
    BaseHolder.NotificationService.Subscribe("MailRefresh", new NotificationEventHandler(this.MailRefreshEvent));
    if (ApplicationServices.Container.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service10)
    {
      Form mainForm = service10.MainForm;
      mainForm.Resize += new EventHandler(this.mainForm_Resize);
      this.mainForm_Resize((object) mainForm, (EventArgs) null);
    }
    HyperlinkHandler.RegisterCommand("mail", "", new HyperlinkEventHandler(wfClientPlugin.MailCommandHyperlinkHandler));
    (ApplicationServices.Container.GetService(typeof (IExceptionHandlerService)) as IExceptionHandlerService).HandleException += new ExceptionHandler(this.exceptionHandlerService_HandleException);
    ApplicationServices.Container.GetService<IConditionControllersService>().RegisterController((IConditionController) new EntersInTaskConditionController());
  }

  private void exceptionHandlerService_HandleException(object sender, ExceptionEventArgs e)
  {
    Exception exception = (Exception) null;
    if (e.Exception is WorkflowMakeBaseVersionException)
      exception = e.Exception;
    else if (e.Exception?.InnerException is WorkflowMakeBaseVersionException)
      exception = e.Exception.InnerException;
    if (exception == null)
      return;
    e.Handled = true;
    using (WorkflowErrorFormWithProcessesViewAndDeleting processesViewAndDeleting = new WorkflowErrorFormWithProcessesViewAndDeleting(true))
    {
      processesViewAndDeleting.Exception = (WorkflowMakeBaseVersionException) exception;
      int num = (int) processesViewAndDeleting.ShowDialog();
    }
  }

  private void mainForm_Resize(object sender, EventArgs e)
  {
    if (!(sender is Form form) || form.WindowState == FormWindowState.Minimized || !(ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service))
      return;
    service.PreviousMainFormState = form.WindowState;
  }

  private void MailRefreshEvent(object sender, NotificationEventArgs e)
  {
    if (e is MailRefreshWithoutCountingEventArgs || !(ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service))
      return;
    service.CountUnreadMail(!(e is MailRefreshWithoutFormPopupEventArgs));
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    IScriptPadService service = (IScriptPadService) this._serviceProvider.GetService(typeof (IScriptPadService));
    if (service == null)
      return;
    WorkflowScriptProjectInitializer initializer = new WorkflowScriptProjectInitializer();
    service.RegisterScriptProjectInitializer(ScriptTypeHelper.GetObjType4ScriptType(ScriptTypes.WorkflowCommon), (DBScriptProjectInitializer) initializer);
    service.RegisterScriptProjectInitializer(ScriptTypeHelper.GetObjType4ScriptType(ScriptTypes.WorkflowLocal), (DBScriptProjectInitializer) initializer);
  }

  private DockControl contProvider_ContentCallback(Guid guid, string persistString)
  {
    if (guid == ObjectRevisionHistoryView.revGuid)
      return ObjectRevisionHistoryView.ShowRevisionHistory(persistString);
    if (guid == ObjectRevisionHistoryView.procGuid)
      return ObjectRevisionHistoryView.ShowProcesses(persistString);
    return guid == this.MailWindowGuid ? (DockControl) ((IWellKnownNavigators) ApplicationServices.Container.GetService(typeof (IWellKnownNavigators))).Get(wfClientPlugin.MailWindowName) ?? (DockControl) this.CreateMailWindow() : (DockControl) null;
  }

  private void startupService_MainFormShown(object sender, EventArgs e)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service))
      return;
    service.StartListener();
  }

  private void AfterCreateMenu(Component contextMenu, System.IServiceProvider viewServices)
  {
    if (!(contextMenu is MenuBarItem menuBarItem))
      return;
    ISelectedItemsHost service = viewServices.GetService(typeof (ISelectedItemsHost)) as ISelectedItemsHost;
    foreach (MenuItemBase menuItemBase in (CollectionBase) menuBarItem.Items)
    {
      if (menuItemBase.Text == this._launchCaption)
      {
        menuItemBase.Tag = (object) service;
        menuItemBase.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.LaunchItem_BeforePopup);
      }
    }
  }

  private void LaunchItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    Icon icon = BaseHolder.IconService.GetIcon(4, wfConsts.SchemesTypeID);
    if (!(sender is MenuButtonItem menuButtonItem1))
      return;
    while (menuButtonItem1.Items.Count > 1)
      menuButtonItem1.Items.RemoveAt(0);
    int num1 = Math.Min(Intermech.Workflow.Design.Holder.RecentLaunched.Count, 6);
    for (int index = 0; index < num1; ++index)
    {
      MenuButtonItem menuButtonItem2 = new MenuButtonItem(Intermech.Workflow.Design.Holder.RecentLaunched.Captions[index], new EventHandler(this.LaunchProcessHandler));
      menuButtonItem2.Tag = (object) new Tuple<long, ISelectedItemsHost>(Intermech.Workflow.Design.Holder.RecentLaunched.IDs[index], menuButtonItem1.Tag as ISelectedItemsHost);
      menuButtonItem2.Icon = icon;
      MenuButtonItem menuButtonItem3 = menuButtonItem2;
      menuButtonItem1.Items.Insert(index, (ToolbarItemBase) menuButtonItem3);
    }
    menuButtonItem1.Items[menuButtonItem1.Items.Count - 1].BeginGroup = true;
    ISimpleSelectedItems selectedItems = this.SelectedItems;
    if (selectedItems == null || selectedItems.Count <= 0)
      return;
    bool flag = false;
    if (!(selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectType != wfConsts.SchemesTypeID)
      return;
    long num2 = itemData.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
      if (dbObject == null)
      {
        dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID * -1L);
        num2 = itemData.ObjectID * -1L;
      }
      IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrIsDebugID);
      if (attributeById != null)
        flag = attributeById.AsBoolean;
    }
    if (Intermech.Workflow.Design.Holder.IsAdmin)
    {
      MenuButtonItem menuButtonItem4 = new MenuButtonItem(CaptionTransform.GetCaption(itemData.Caption, itemData.Version), new EventHandler(this.LaunchProcessHandler));
      menuButtonItem4.Tag = (object) new Tuple<long, ISelectedItemsHost>(num2, menuButtonItem1.Tag as ISelectedItemsHost);
      menuButtonItem4.Icon = icon;
      MenuButtonItem menuButtonItem5 = menuButtonItem4;
      menuButtonItem1.Items[0].BeginGroup = true;
      menuButtonItem1.Items.Insert(0, (ToolbarItemBase) menuButtonItem5);
    }
    else
    {
      if (itemData.BaseVersion != 1L || flag)
        return;
      MenuButtonItem menuButtonItem6 = new MenuButtonItem(CaptionTransform.GetCaption(itemData.Caption, itemData.Version), new EventHandler(this.LaunchProcessHandler));
      menuButtonItem6.Tag = (object) new Tuple<long, ISelectedItemsHost>(num2, menuButtonItem1.Tag as ISelectedItemsHost);
      menuButtonItem6.Icon = icon;
      MenuButtonItem menuButtonItem7 = menuButtonItem6;
      menuButtonItem1.Items[0].BeginGroup = true;
      menuButtonItem1.Items.Insert(0, (ToolbarItemBase) menuButtonItem7);
    }
  }

  private ISimpleSelectedItems SelectedItems
  {
    get
    {
      return ApplicationServices.Container.GetService(typeof (ISimpleSelectedItems)) as ISimpleSelectedItems;
    }
  }

  private void LaunchProcessHandler(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem))
      return;
    ISelectedItemsHost selectedItemsHost = (ISelectedItemsHost) null;
    object tag = menuButtonItem.Tag;
    long int64;
    if (tag is Tuple<long, ISelectedItemsHost> tuple)
    {
      int64 = tuple.Item1;
      selectedItemsHost = tuple.Item2;
    }
    else
      int64 = Convert.ToInt64(tag);
    wfFunx.CreateProcess(int64, selectedItemsHost != null ? (ISimpleSelectedItems) selectedItemsHost.SelectedItems : this.SelectedItems);
  }

  private void ShowMail(object sender, EventArgs e)
  {
    WellKnownNavWindow wellKnownNavWindow = (WellKnownNavWindow) ((IWellKnownNavigators) ApplicationServices.Container.GetService(typeof (IWellKnownNavigators))).Get(wfClientPlugin.MailWindowName);
    DockManager service = (DockManager) ApplicationServices.Container.GetService(typeof (DockManager));
    if (wellKnownNavWindow == null)
    {
      DockControl dockControl = service.FindDockControl(this.MailWindowGuid);
      if (dockControl != null)
      {
        dockControl.Activate();
        wellKnownNavWindow = service.FindDockControl(this.MailWindowGuid) as WellKnownNavWindow;
      }
    }
    if (wellKnownNavWindow == null)
      wellKnownNavWindow = this.CreateMailWindow();
    wellKnownNavWindow.Show(service);
    wellKnownNavWindow.Activate();
  }

  private WellKnownNavWindow CreateMailWindow()
  {
    WellKnownNavWindow wellKnownNavWindow = new WellKnownNavWindow();
    wellKnownNavWindow.WellKnownName = wfClientPlugin.MailWindowName;
    wellKnownNavWindow.Guid = this.MailWindowGuid;
    wellKnownNavWindow.Text = this.Name;
    WellKnownNavWindow mailWindow = wellKnownNavWindow;
    ICategoryTypeIconService service = (ICategoryTypeIconService) ApplicationServices.Container.GetService(typeof (ICategoryTypeIconService));
    if (service != null)
    {
      int index = service.IndexOf(Intermech.Navigator.Consts.CategoryMail, 0);
      if (index >= 0)
        mailWindow.TabImage = service.ImageList.Images[index];
    }
    mailWindow.TreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.GetNavigatorColumns);
    mailWindow.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    mailWindow.TreeView.Build((IDescriptor) Intermech.Workflow.Client.Services.MailDescriptor);
    return mailWindow;
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    if (!MailSettings.Cfg.ClearTrashOnExit)
      return;
    this.EmptyTrash();
  }

  private void EmptyTrash()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectCollection(wfConsts.ProcessAtomsTypeID).Select(new DBRecordSetParams(TrashNode.StaticConditions, new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
          new ColumnDescriptor((object) wfConsts.AttrRecipID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
          new ColumnDescriptor((object) wfConsts.AttrSenderID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
        }));
        if (dataTable.Rows.Count <= 0)
          return;
        WaitingForm.StartProgress(LocalizationHolder.rm.GetString("Workflow.Client_56"), dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(row[0]), false);
          if (dbObject != null)
          {
            if (wfConsts.UserID.Equals(row[1]))
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrRecipDeletionID);
              if (attributeById != null)
                attributeById.AsInteger = 2L;
            }
            if (wfConsts.UserID.Equals(row[2]))
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrSenderDeletionID);
              if (attributeById != null)
                attributeById.AsInteger = 2L;
            }
          }
          if (!WaitingForm.IncProgress())
            break;
        }
      }
    }
    catch (Exception ex)
    {
      WaitingForm.CloseForm();
      wfFunx.SayError(ex.Message);
    }
  }

  public bool Execute(ICommandState commandState)
  {
    switch (commandState.CommandName)
    {
      case "ShowProcesses":
        wfFunx.ShowProcesses(this.SelectedItems);
        return true;
      case "RevisionHistory":
        wfFunx.ShowRevisionHistory(this.SelectedItems);
        return true;
      default:
        return false;
    }
  }

  public bool QueryStatus(ICommandState commandState)
  {
    string commandName = commandState.CommandName;
    if (!(commandName == "ShowProcesses") && !(commandName == "RevisionHistory"))
      return false;
    commandState.Enabled = true;
    return true;
  }

  public bool PostInit()
  {
    if (this._postInited)
      return true;
    this._postInited = true;
    OrganizerPlugin.Init();
    return true;
  }

  internal void RegisterAdjustableViews()
  {
    AdjustableViewsHelper.RegisterView("Workflow.NotifyView", LocalizationHolder.rm.GetString("Workflow.Client_75"), LocalizationHolder.rm.GetString("Workflow.Client_55"), "Intermech.Workflow.Client", "imgCopies", true, 36);
    AdjustableViewsHelper.RegisterView("Workflow.EMailMessageView", LocalizationHolder.rm.GetString("Workflow.Client_70"), string.Empty, string.Empty, "wfMessages", true, 0);
    AdjustableViewsHelper.RegisterView("Workflow.EmailInboxView", LocalizationHolder.rm.GetString("Workflow.Client_68"), string.Empty, string.Empty, string.Empty, true, 0);
    AdjustableViewsHelper.RegisterView("ForumView", LocalizationHolder.rm.GetString("Discussion"), LocalizationHolder.rm.GetString("Discussion"), "Intermech.Workflow.Client", "forum", true, 55);
  }

  public static void MailCommandHyperlinkHandler(string command, string id)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service))
      return;
    service.GoToMail();
  }
}
