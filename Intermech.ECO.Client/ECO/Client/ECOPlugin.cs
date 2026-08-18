// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOPlugin
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.ObjectCreator;
using Intermech.DatabaseConfigurator;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.Search;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOPlugin : 
  IPackage,
  ICommandTarget,
  ICommandsProvider,
  IImDocumentManager,
  IConfigurable
{
  public static readonly string revBlanks = "cad0077b-306c-11d8-b4e9-00304f19f545";
  public static readonly string defBlankGuid = "cad0077c-306c-11d8-b4e9-00304f19f545";
  public int idOTSpecification;
  public int idAssemblyUnit;
  public int idOTComplex;
  public int idOTComplect;
  public int idPart;
  internal EcoPropertiesEditor _propertiesEditor;
  public static bool BlockECOOpening = false;
  public static bool ForceECOOpening = false;
  internal EcoPropertiesService eps;
  public long CJTemplateId;
  public ImDocument CJTempDoc;
  public bool IsAdmin;
  public bool IsLCChangeAllowed = true;
  public ICategoryTypeIconService IconService;
  private NotificationEventHandler cIn;
  private NotificationEventHandler cOut;
  private NotificationEventHandler cDel;
  private ISelectedItems navigatorMenuItems;
  private long _curRevId;
  public static System.IServiceProvider serviceProvider = (System.IServiceProvider) null;
  private SaveFileDialog saveToFileDialog;
  private string recentlySaveAsPath;
  private static DockManager dockManager = (DockManager) null;
  private ICommandManager commandManager;
  private long ecoTemplateID = -1;
  private int ecoType = -1;
  private MenuBarItem revMenu;
  public StatusBarPanel scalePanel;
  public IStatusBar iSB;
  public List<int> _allowedTypes;
  public Dictionary<int, List<int>> AllowedDict;
  private HashSet<int> _docTypes;
  private HashSet<int> _prodTypes;
  private HashSet<int> _izvTypes;
  public static ECOPlugin plugin = (ECOPlugin) null;
  private bool blockOnCheckedOut;
  internal Dictionary<long, ECOPlugin.RevInfo> revInfoList;
  private static IImbaseSelector imbaseSelector = (IImbaseSelector) null;
  private static INamedImageList namedImageList = (INamedImageList) null;
  private CreateVersionHandler cvh;
  private static HashSet<long> includingObjIds = new HashSet<long>();
  private ECOEditorForm curEditorForm;
  private CJEditorForm curCJEditorForm;
  private List<long> newObjVerList;
  public static readonly string lcAnnulGuid = "cad003c6-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrSeriesDates = "cadd940c-306c-11d8-b4e9-00304f19f545";
  public static readonly Guid attrSeriesDatesGuid = new Guid(ECOPlugin.attrSeriesDates);
  public ECOPlugin.SetPLForAll DoSetPLForAll;
  public Timer debTimer;
  public long revId;
  public int docTypeId;
  public int productTypeId;

  public void Load(System.IServiceProvider serviceProvider)
  {
    ILicenser service1 = (ILicenser) ServicesManager.GetService(typeof (ILicenser));
    int appId = 340;
    byte[][] numArray = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 170,
        (byte) 142,
        (byte) 68,
        (byte) 85,
        (byte) 11,
        (byte) 185,
        (byte) 91,
        (byte) 165,
        (byte) 58,
        (byte) 180,
        (byte) 75,
        (byte) 250,
        (byte) 159,
        (byte) 186,
        (byte) 52,
        (byte) 2
      },
      new byte[16 /*0x10*/]
      {
        (byte) 225,
        (byte) 154,
        (byte) 27,
        (byte) 37,
        (byte) 14,
        (byte) 83,
        (byte) 52,
        (byte) 243,
        (byte) 240 /*0xF0*/,
        (byte) 9,
        (byte) 221,
        (byte) 71,
        (byte) 161,
        (byte) 60,
        (byte) 76,
        (byte) 39
      },
      new byte[16 /*0x10*/]
      {
        (byte) 50,
        (byte) 236,
        (byte) 86,
        (byte) 248,
        (byte) 253,
        (byte) 26,
        (byte) 149,
        (byte) 197,
        (byte) 184,
        (byte) 245,
        (byte) 165,
        (byte) 107,
        (byte) 162,
        (byte) 67,
        (byte) 228,
        (byte) 233
      },
      new byte[16 /*0x10*/]
      {
        (byte) 45,
        (byte) 187,
        (byte) 74,
        (byte) 181,
        (byte) 235,
        (byte) 163,
        (byte) 8,
        (byte) 184,
        (byte) 57,
        (byte) 61,
        (byte) 180,
        (byte) 196,
        (byte) 90,
        (byte) 127 /*0x7F*/,
        (byte) 70,
        (byte) 206
      },
      new byte[16 /*0x10*/]
      {
        (byte) 172,
        (byte) 223,
        (byte) 243,
        (byte) 166,
        (byte) 72,
        (byte) 158,
        (byte) 106,
        (byte) 205,
        (byte) 211,
        (byte) 146,
        (byte) 37,
        (byte) 117,
        (byte) 87,
        (byte) 212,
        (byte) 67,
        (byte) 178
      },
      new byte[16 /*0x10*/]
      {
        (byte) 103,
        (byte) 180,
        (byte) 59,
        (byte) 173,
        (byte) 230,
        (byte) 33,
        (byte) 7,
        (byte) 44,
        (byte) 32 /*0x20*/,
        (byte) 246,
        (byte) 160 /*0xA0*/,
        (byte) 232,
        (byte) 250,
        (byte) 101,
        (byte) 74,
        (byte) 185
      },
      new byte[16 /*0x10*/]
      {
        (byte) 32 /*0x20*/,
        (byte) 74,
        (byte) 251,
        (byte) 99,
        (byte) 85,
        (byte) 1,
        (byte) 31 /*0x1F*/,
        (byte) 144 /*0x90*/,
        (byte) 248,
        (byte) 127 /*0x7F*/,
        (byte) 156,
        (byte) 21,
        (byte) 19,
        (byte) 239,
        (byte) 3,
        (byte) 247
      },
      new byte[16 /*0x10*/]
      {
        (byte) 131,
        (byte) 210,
        (byte) 102,
        (byte) 70,
        (byte) 157,
        (byte) 104,
        (byte) 100,
        (byte) 31 /*0x1F*/,
        (byte) 233,
        (byte) 108,
        (byte) 204,
        (byte) 55,
        (byte) 110,
        (byte) 127 /*0x7F*/,
        (byte) 215,
        (byte) 254
      },
      new byte[16 /*0x10*/]
      {
        (byte) 207,
        (byte) 97,
        (byte) 77,
        (byte) 191,
        (byte) 198,
        (byte) 142,
        (byte) 186,
        (byte) 147,
        (byte) 129,
        (byte) 136,
        (byte) 36,
        (byte) 106,
        (byte) 244,
        (byte) 32 /*0x20*/,
        (byte) 109,
        (byte) 228
      },
      new byte[16 /*0x10*/]
      {
        (byte) 102,
        (byte) 123,
        (byte) 67,
        (byte) 77,
        (byte) 188,
        (byte) 48 /*0x30*/,
        (byte) 236,
        (byte) 13,
        (byte) 179,
        (byte) 25,
        (byte) 242,
        (byte) 192 /*0xC0*/,
        (byte) 53,
        (byte) 186,
        (byte) 144 /*0x90*/,
        (byte) 186
      },
      new byte[16 /*0x10*/]
      {
        (byte) 178,
        (byte) 242,
        (byte) 75,
        (byte) 88,
        (byte) 71,
        (byte) 129,
        (byte) 148,
        (byte) 171,
        (byte) 191,
        (byte) 98,
        (byte) 173,
        (byte) 200,
        (byte) 1,
        (byte) 84,
        (byte) 185,
        (byte) 228
      },
      new byte[16 /*0x10*/]
      {
        (byte) 218,
        (byte) 174,
        (byte) 206,
        (byte) 239,
        (byte) 162,
        (byte) 219,
        (byte) 27,
        (byte) 125,
        (byte) 139,
        (byte) 222,
        (byte) 133,
        (byte) 245,
        (byte) 35,
        (byte) 174,
        (byte) 19,
        (byte) 137
      },
      new byte[16 /*0x10*/]
      {
        (byte) 143,
        (byte) 45,
        (byte) 165,
        (byte) 245,
        (byte) 134,
        (byte) 131,
        (byte) 235,
        (byte) 145,
        (byte) 144 /*0x90*/,
        (byte) 103,
        (byte) 226,
        (byte) 204,
        (byte) 221,
        (byte) 180,
        (byte) 155,
        (byte) 216
      },
      new byte[16 /*0x10*/]
      {
        (byte) 187,
        (byte) 25,
        (byte) 209,
        (byte) 218,
        (byte) 231,
        (byte) 148,
        (byte) 180,
        (byte) 91,
        (byte) 142,
        (byte) 205,
        (byte) 226,
        (byte) 1,
        (byte) 227,
        (byte) 189,
        (byte) 17,
        (byte) 93
      },
      new byte[16 /*0x10*/]
      {
        (byte) 17,
        (byte) 183,
        (byte) 99,
        (byte) 32 /*0x20*/,
        (byte) 164,
        (byte) 171,
        (byte) 175,
        (byte) 78,
        (byte) 205,
        (byte) 162,
        byte.MaxValue,
        (byte) 100,
        (byte) 225,
        (byte) 241,
        (byte) 146,
        (byte) 44
      },
      new byte[16 /*0x10*/]
      {
        (byte) 8,
        (byte) 206,
        (byte) 192 /*0xC0*/,
        (byte) 15,
        (byte) 26,
        (byte) 40,
        (byte) 136,
        (byte) 83,
        (byte) 230,
        (byte) 211,
        (byte) 11,
        (byte) 122,
        (byte) 36,
        (byte) 159,
        (byte) 50,
        (byte) 216
      },
      new byte[16 /*0x10*/]
      {
        (byte) 206,
        (byte) 64 /*0x40*/,
        (byte) 219,
        (byte) 198,
        (byte) 122,
        (byte) 120,
        (byte) 93,
        (byte) 47,
        (byte) 7,
        (byte) 185,
        (byte) 199,
        (byte) 223,
        (byte) 159,
        (byte) 199,
        (byte) 205,
        (byte) 10
      },
      new byte[16 /*0x10*/]
      {
        (byte) 122,
        (byte) 193,
        (byte) 31 /*0x1F*/,
        (byte) 35,
        (byte) 129,
        (byte) 188,
        (byte) 167,
        (byte) 155,
        (byte) 148,
        (byte) 200,
        (byte) 63 /*0x3F*/,
        (byte) 17,
        (byte) 7,
        (byte) 225,
        (byte) 177,
        (byte) 203
      },
      new byte[16 /*0x10*/]
      {
        (byte) 198,
        (byte) 212,
        (byte) 182,
        (byte) 73,
        (byte) 241,
        (byte) 153,
        (byte) 63 /*0x3F*/,
        (byte) 228,
        (byte) 126,
        (byte) 87,
        (byte) 39,
        (byte) 228,
        (byte) 131,
        (byte) 135,
        (byte) 176 /*0xB0*/,
        (byte) 158
      },
      new byte[16 /*0x10*/]
      {
        (byte) 29,
        (byte) 180,
        (byte) 82,
        (byte) 211,
        (byte) 17,
        (byte) 166,
        (byte) 197,
        (byte) 154,
        (byte) 108,
        (byte) 153,
        (byte) 110,
        (byte) 78,
        (byte) 44,
        (byte) 71,
        (byte) 54,
        (byte) 53
      },
      new byte[16 /*0x10*/]
      {
        (byte) 213,
        (byte) 181,
        (byte) 254,
        (byte) 156,
        (byte) 217,
        (byte) 213,
        (byte) 93,
        (byte) 153,
        (byte) 89,
        (byte) 51,
        (byte) 25,
        (byte) 235,
        (byte) 227,
        (byte) 207,
        (byte) 175,
        (byte) 6
      },
      new byte[16 /*0x10*/]
      {
        (byte) 103,
        (byte) 195,
        (byte) 100,
        (byte) 139,
        (byte) 230,
        (byte) 153,
        (byte) 31 /*0x1F*/,
        (byte) 123,
        (byte) 92,
        (byte) 125,
        (byte) 205,
        (byte) 68,
        (byte) 238,
        (byte) 78,
        (byte) 11,
        (byte) 29
      },
      new byte[16 /*0x10*/]
      {
        (byte) 212,
        (byte) 244,
        (byte) 157,
        (byte) 12,
        (byte) 59,
        (byte) 72,
        (byte) 58,
        (byte) 53,
        (byte) 0,
        (byte) 247,
        (byte) 72,
        (byte) 35,
        (byte) 81,
        (byte) 121,
        (byte) 172,
        (byte) 127 /*0x7F*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 89,
        (byte) 103,
        (byte) 78,
        (byte) 63 /*0x3F*/,
        (byte) 30,
        (byte) 109,
        (byte) 136,
        (byte) 117,
        (byte) 237,
        (byte) 193,
        (byte) 213,
        (byte) 195,
        (byte) 29,
        (byte) 200,
        (byte) 48 /*0x30*/,
        (byte) 48 /*0x30*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 8,
        (byte) 41,
        (byte) 10,
        (byte) 233,
        (byte) 200,
        (byte) 48 /*0x30*/,
        (byte) 49,
        (byte) 241,
        (byte) 119,
        (byte) 29,
        (byte) 239,
        (byte) 190,
        (byte) 127 /*0x7F*/,
        (byte) 84,
        (byte) 0,
        (byte) 174
      },
      new byte[16 /*0x10*/]
      {
        (byte) 243,
        (byte) 92,
        (byte) 165,
        (byte) 2,
        (byte) 64 /*0x40*/,
        (byte) 39,
        (byte) 107,
        (byte) 1,
        (byte) 210,
        (byte) 231,
        (byte) 90,
        (byte) 196,
        (byte) 46,
        (byte) 9,
        (byte) 145,
        (byte) 118
      },
      new byte[16 /*0x10*/]
      {
        (byte) 168,
        (byte) 32 /*0x20*/,
        (byte) 15,
        (byte) 142,
        (byte) 234,
        (byte) 164,
        (byte) 99,
        (byte) 141,
        (byte) 122,
        (byte) 201,
        (byte) 156,
        (byte) 225,
        (byte) 140,
        (byte) 58,
        (byte) 71,
        (byte) 224 /*0xE0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 23,
        (byte) 195,
        (byte) 28,
        (byte) 95,
        (byte) 136,
        (byte) 106,
        (byte) 49,
        (byte) 88,
        (byte) 126,
        (byte) 77,
        (byte) 93,
        (byte) 202,
        (byte) 199,
        (byte) 15,
        (byte) 191,
        (byte) 149
      },
      new byte[16 /*0x10*/]
      {
        (byte) 135,
        (byte) 154,
        (byte) 226,
        (byte) 185,
        (byte) 197,
        (byte) 197,
        (byte) 62,
        (byte) 163,
        (byte) 195,
        (byte) 171,
        (byte) 149,
        (byte) 29,
        (byte) 88,
        (byte) 124,
        (byte) 181,
        (byte) 172
      },
      new byte[16 /*0x10*/]
      {
        (byte) 64 /*0x40*/,
        (byte) 70,
        (byte) 31 /*0x1F*/,
        (byte) 41,
        (byte) 71,
        (byte) 172,
        (byte) 49,
        (byte) 106,
        (byte) 51,
        (byte) 91,
        (byte) 24,
        (byte) 44,
        (byte) 32 /*0x20*/,
        (byte) 153,
        (byte) 126,
        (byte) 215
      },
      new byte[16 /*0x10*/]
      {
        (byte) 246,
        (byte) 153,
        (byte) 123,
        (byte) 48 /*0x30*/,
        (byte) 106,
        (byte) 29,
        (byte) 149,
        (byte) 100,
        (byte) 197,
        (byte) 124,
        (byte) 111,
        byte.MaxValue,
        (byte) 148,
        (byte) 80 /*0x50*/,
        (byte) 101,
        (byte) 60
      },
      new byte[16 /*0x10*/]
      {
        (byte) 119,
        (byte) 27,
        (byte) 217,
        (byte) 44,
        (byte) 197,
        (byte) 176 /*0xB0*/,
        (byte) 125,
        (byte) 128 /*0x80*/,
        (byte) 82,
        (byte) 0,
        (byte) 186,
        byte.MaxValue,
        (byte) 207,
        (byte) 221,
        (byte) 153,
        (byte) 236
      }
    };
    service1.AllocateLicense(appId);
    ECOPlugin.plugin = this;
    DocumentPlugin.InitDocumentPlugin();
    DocumentEditorPlugin.InitDocumentPlugin();
    ECOPlugin.serviceProvider = serviceProvider;
    this.commandManager = (ICommandManager) serviceProvider.GetService(typeof (ICommandManager));
    if (this.commandManager == null)
      this.commandManager = (ICommandManager) new Intermech.Bars.CommandManager();
    MenuBar menuBar = ((BarManager) serviceProvider.GetService(typeof (BarManager))).MenuBar;
    menuBar.FindMenuBar("File");
    MenuItemBase menuItem1 = menuBar.FindMenuItem("File.New");
    if (menuItem1 != null)
    {
      MenuButtonItem menuItem2 = DocumentMenuHelper.CreateMenuItem("NewECO", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_68"), "", false, false, this.commandManager);
      menuItem1.Items.Add((ToolbarItemBase) menuItem2);
      MenuButtonItem menuItem3 = DocumentMenuHelper.CreateMenuItem("New.CJ", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_296"), "", false, false, this.commandManager);
      menuItem1.Items.Add((ToolbarItemBase) menuItem3);
      MenuButtonItem menuItem4 = DocumentMenuHelper.CreateMenuItem("New.CJRec", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_325"), "", false, false, this.commandManager);
      menuItem1.Items.Add((ToolbarItemBase) menuItem4);
    }
    if (ECOPlugin.dockManager == null)
      ECOPlugin.dockManager = (DockManager) serviceProvider.GetService(typeof (DockManager));
    ECOPlugin.dockManager.DocumentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.ActiveDocumentChanged);
    IECOServer ecoServer = (IECOServer) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.ecoTemplateID = sessionKeeper.Session.GetObject(new Guid(ECOPlugin.defBlankGuid)).ObjectID;
      this.ecoType = sessionKeeper.Session.GetObjectType(new Guid(RevHelper.guidObj_II)).ObjectType;
      this.UpdateAllowedTypes();
      ecoServer = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
      this.docTypeId = sessionKeeper.Session.GetObjectType(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).ObjectType;
      this.productTypeId = sessionKeeper.Session.GetObjectType(new Guid("cad00268-306c-11d8-b4e9-00304f19f545")).ObjectType;
      IDBObjectType objectType1 = sessionKeeper.Session.GetObjectType(new Guid("cad00133-306c-11d8-b4e9-00304f19f545"));
      if (objectType1 != null)
        this.idOTSpecification = objectType1.ObjectType;
      IDBObjectType objectType2 = sessionKeeper.Session.GetObjectType(new Guid("cad0025e-306c-11d8-b4e9-00304f19f545"));
      if (objectType2 != null)
        this.idOTComplex = objectType2.ObjectType;
      IDBObjectType objectType3 = sessionKeeper.Session.GetObjectType(new Guid("cad0025f-306c-11d8-b4e9-00304f19f545"));
      if (objectType3 != null)
        this.idOTComplect = objectType3.ObjectType;
      IDBObjectType objectType4 = sessionKeeper.Session.GetObjectType(new Guid("cad00132-306c-11d8-b4e9-00304f19f545"));
      if (objectType4 != null)
        this.idAssemblyUnit = objectType4.ObjectType;
      IDBObjectType objectType5 = sessionKeeper.Session.GetObjectType(new Guid("cad00250-306c-11d8-b4e9-00304f19f545"));
      if (objectType5 != null)
        this.idPart = objectType5.ObjectType;
    }
    IObjectCreatorService service2 = (IObjectCreatorService) serviceProvider.GetService(typeof (IObjectCreatorService));
    if (service2 != null)
    {
      foreach (int aObjectTypeID in MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_II))
        service2.RegisterCreatorCustomService(aObjectTypeID, typeof (ECOPlugin.RevObjectCreator));
      foreach (int aObjectTypeID in MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PI))
        service2.RegisterCreatorCustomService(aObjectTypeID, typeof (ECOPlugin.RevObjectCreator));
      foreach (int aObjectTypeID in MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PR))
        service2.RegisterCreatorCustomService(aObjectTypeID, typeof (ECOPlugin.RevObjectCreator));
      service2.RegisterCreatorCustomService(RevHelper.idObj_DI, typeof (DIObjectCreator));
      service2.RegisterCreatorCustomService(RevHelper.idObj_DPI, typeof (DIObjectCreator));
      service2.RegisterCreatorCustomService(RevHelper.idChangeJournal, typeof (ECOPlugin.CJObjectCreator));
      service2.RegisterCreatorCustomService(RevHelper.idObj_SN, typeof (ECOPlugin.RevObjectCreator));
      service2.BeforeCommitCreationEvent += new BeforeCommitCreationEventHandler(this.ObjCreator_BeforeCommitCreationEvent);
    }
    IObjectsCheckOutService service3 = (IObjectsCheckOutService) serviceProvider.GetService(typeof (IObjectsCheckOutService));
    if (service3 != null)
      service3.ObjectsCheckOutEventHandler += new ObjectsCheckOutEventHandler(this.ioCOS_ObjectsCheckOutEventHandler);
    IAVSClientService service4 = serviceProvider.GetService<IAVSClientService>(false);
    if (service4 != null)
      service4.BeforeCommitCreationAVSDocumentEvent += new BeforeCommitCreationAVSDocumentEventHandler(this.IAVSCS_BeforeCommitCreationAVSDocumentEvent);
    IFactory service5 = (IFactory) serviceProvider.GetService(typeof (IFactory));
    service5.AddCommandsProvider(1, RevHelper.idChangeJournal, (ICommandsProvider) this);
    service5.AddCommandsProvider(1, RevHelper.idObjCJRecord, (ICommandsProvider) this);
    (ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes).AddDefaultCommand(RevHelper.idObjCJRecord, "EditDocument", DefaultCommandHandler.ContectMenu);
    service5.AddCommandsProvider(1, MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545"), (ICommandsProvider) new CJCreate());
    service5.AddCommandsProvider(1, RevHelper.idObjCJRecord, (ICommandsProvider) new CJOpen());
    service5.AddCommandsProvider(1, (ICommandsProvider) new ConvertToECOProvider());
    MenuTemplate contextMenuTemplate = service5.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      service5.AddCommandsProvider(1, (ICommandsProvider) new ECOChangeMenuProvider());
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EngineeringChangeOrders", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_221"), -1, 40, 10)
      {
        Nodes = {
          new MenuTemplateNode("ReplacePI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_73"), -1, 34, 1),
          new MenuTemplateNode("ReplacePIWithContents", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_74"), -1, 34, 2),
          new MenuTemplateNode("AcceptPR", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_75"), -1, 34, 1),
          new MenuTemplateNode("AcceptPRWithContents", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_76"), -1, 34, 2),
          new MenuTemplateNode("CreateLinkedECO", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_77"), -1, 34, 3),
          new MenuTemplateNode("CreateCJRecord", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_326"), -1, 34, 4),
          new MenuTemplateNode("LinkToOther", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_245"), -1, 34, 5),
          new MenuTemplateNode("UnlinkToOther", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_246"), -1, 34, 6),
          new MenuTemplateNode("IssueDI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_281"), -1, 34, 7),
          new MenuTemplateNode("IssueDPI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_282"), -1, 34, 8),
          new MenuTemplateNode("UnreplacePI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_432"), -1, 34, 9),
          new MenuTemplateNode("ECO.ConvertToECO", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_69"), -1, 10, 15),
          new MenuTemplateNode("New.Change", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_382"), -1, 10, 10),
          new MenuTemplateNode("New.SetLitera", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_70"), -1, 10, 20),
          new MenuTemplateNode("New.Create", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_336"), -1, 10, 30),
          new MenuTemplateNode("New.Replace", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_71"), -1, 10, 40),
          new MenuTemplateNode("New.Annul", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_72"), -1, 10, 50),
          new MenuTemplateNode("NewCJ.ForIzdel", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_300"), -1, 37, 111),
          new MenuTemplateNode("AnnulPI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_72"), -1, 15, 40),
          new MenuTemplateNode("LinkToKI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_278"), -1, 34, 5),
          new MenuTemplateNode("AddLinkToKI", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_359"), -1, 34, 6),
          new MenuTemplateNode("UnhideHidden", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_403"), -1, 34, 7)
        }
      });
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CJRec.OpenCJ", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_301"), -1, 34, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CJRec.ReplaceCJs", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_324"), -1, 35, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ECO.LaunchShooter", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_417"), -1, 36, 1));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    this.commandManager.AddTarget((ICommandTarget) this);
    string str1 = sc_6342.ssp_eco_6343();
    Assembly assembly = this.GetType().Assembly;
    this.revMenu = new MenuBarItem(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_78"));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.AttachToECO", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_79"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_80"), assembly, str1 + "ECOInclude.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.AttachToECO_ExternalDoc", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_83"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_84"), assembly, str1 + "ECOIncludeExt.png", false, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.AttachIzdel", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_373"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_374"), false, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.DetachFromECO", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_85"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_86"), assembly, str1 + "ECOExclude.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.InsertList", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_274"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_274"), assembly, str1 + "ECOAddList.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.DeleteList", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_371"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_371"), assembly, str1 + "ECODelList.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.ProcChanges", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_372"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_372"), assembly, str1 + "ECOSort.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.LaunchShooter", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_409"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_410"), assembly, str1 + "ScrCapture.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.SetPLForAll", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_461"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_462"), true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.SpecSymbol", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_262"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_418"), assembly, str1 + "ECOInsSpecSymbol.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.ChangeReason", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_240"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_407"), assembly, str1 + "ECOReason.png", false, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.Card", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_241"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_408"), assembly, str1 + "ECOCard.png", true, true, this.commandManager));
    this.revMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("ECO.Tree", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_253"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_254"), assembly, str1 + "ECOTree.png", false, true, this.commandManager));
    MenuButtonItem menuItem5 = DocumentMenuHelper.CreateMenuItem("ECO.ReplaceTemplate", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_444"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_445"), true, true, this.commandManager);
    menuItem5.BeginGroup = true;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem5);
    MenuButtonItem menuItem6 = DocumentMenuHelper.CreateMenuItem("ECO.PasteObjects", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_223"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_224"), assembly, str1 + "testImg.png", true, true, this.commandManager);
    menuItem6.BeginGroup = true;
    menuBar.FindMenuBar("Edit")?.Items.Add((ToolbarItemBase) menuItem6);
    MenuButtonItem menuItem7 = DocumentMenuHelper.CreateMenuItem("ECO.CopyAllElems", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_405"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_421"), assembly, str1 + "ECOCopyAllElems.png", true, true, this.commandManager);
    menuItem7.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem7);
    MenuButtonItem menuItem8 = DocumentMenuHelper.CreateMenuItem("ECO.CopyTable", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_416"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_422"), assembly, str1 + "ECOCopyTable.png", false, true, this.commandManager);
    menuItem8.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem8);
    MenuButtonItem menuItem9 = DocumentMenuHelper.CreateMenuItem("ECO.PasteElems", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_406"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_423"), assembly, str1 + "ECOPasteElems.png", false, true, this.commandManager);
    menuItem9.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem9);
    MenuButtonItem menuItem10 = DocumentMenuHelper.CreateMenuItem("ECO.MoveElemUp", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_419"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_419"), assembly, str1 + "Up.png", true, true, this.commandManager);
    menuItem10.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem10);
    MenuButtonItem menuItem11 = DocumentMenuHelper.CreateMenuItem("ECO.MoveElemDown", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_420"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_420"), assembly, str1 + "Down.png", false, true, this.commandManager);
    menuItem11.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem11);
    string str2 = Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_200") + Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_298");
    MenuButtonItem menuItem12 = DocumentMenuHelper.CreateMenuItem("ECO.AddElemBefore", str2, str2, assembly, str1 + "ECOAddElemBefore.png", true, true, this.commandManager);
    menuItem12.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem12);
    string str3 = Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_200") + Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_299");
    MenuButtonItem menuItem13 = DocumentMenuHelper.CreateMenuItem("ECO.AddElemAfter", str3, str3, assembly, str1 + "ECOAddElemAfter.png", false, true, this.commandManager);
    menuItem13.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem13);
    MenuButtonItem menuItem14 = DocumentMenuHelper.CreateMenuItem("ECO.DeleteElem", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_201"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_201"), assembly, str1 + "ECODelElem.png", false, true, this.commandManager);
    menuItem14.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem14);
    MenuButtonItem menuItem15 = DocumentMenuHelper.CreateMenuItem("ECO.SortByDes", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_202"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_425"), assembly, str1 + "ECOSortChange.png", true, true, this.commandManager);
    menuItem15.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem15);
    MenuButtonItem menuItem16 = DocumentMenuHelper.CreateMenuItem("ECO.ChangeGoal", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_402"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_426"), assembly, str1 + "ECOChangeGoal.png", false, true, this.commandManager);
    menuItem16.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem16);
    MenuButtonItem menuItem17 = DocumentMenuHelper.CreateMenuItem("ECO.ImgFromObj", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_191"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_427"), assembly, str1 + "ECOImgFromObj.png", true, true, this.commandManager);
    menuItem17.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem17);
    MenuButtonItem menuItem18 = DocumentMenuHelper.CreateMenuItem("ECO.ImgFromFile", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_192"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_428"), assembly, str1 + "ECOImgFromFile.png", false, true, this.commandManager);
    menuItem18.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem18);
    MenuButtonItem menuItem19 = DocumentMenuHelper.CreateMenuItem("ECO.ImgFromClip", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_193"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_429"), assembly, str1 + "ECOImgFromClip.png", false, true, this.commandManager);
    menuItem19.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem19);
    MenuButtonItem menuItem20 = DocumentMenuHelper.CreateMenuItem("ECO.CreateOLE", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_194"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_430"), assembly, str1 + "ECOCreateOLE.png", false, true, this.commandManager);
    menuItem20.Visible = false;
    this.revMenu.Items.Add((ToolbarItemBase) menuItem20);
    if (ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service6)
    {
      MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
      {
        menuItem20
      };
      service6.RegisterMenuItemsGroup(MainMenuItemSite.Composition, MainMenuItemPosition.Default, false, menuButtonItemArray);
    }
    this.revMenu.CommandName = "ECO";
    this.commandManager.Add((ButtonItemBase) this.revMenu);
    menuBar.Items.Insert(2, (ToolbarItemBase) this.revMenu);
    DocumentMenuHelper.CreateMenuItem("ECO.CalculateWhereUsedColumn", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_91"), "", str1 + "InsertRowAbove.bmp", true, true, this.commandManager);
    RevIntegrator integrator = new RevIntegrator();
    integrator.Initialize();
    ClientContext.Integrators.RegisterIntegrator((IIntegrator) integrator);
    RevLaunchHandler handler = new RevLaunchHandler(integrator);
    ClientContext.LaunchActions.RegisterHandler((ILaunchHandler) handler);
    DocumentEditorPlugin.Instance.SpecialDocumentLaunchHandlers.Add(handler.Id);
    NavigatorWindowCaptionsHelper.OnGetNavigatorWindowCaption += new Intermech.Interfaces.Client.NavigatorWindowCaptionEventHandler(this.NavigatorWindowCaptionEventHandler);
    IContentProvider service7 = (IContentProvider) serviceProvider.GetService(typeof (IContentProvider));
    if (service7 != null)
      service7.ContentCallback += new GetContentCallback(this.RestoreDocumentWindow);
    IPluginManager service8 = serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager;
    service8.LoadComplete += new EventHandler(this.manager_LoadComplete);
    this.IsAdmin = ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service9 && service9.IsAdmin;
    if (ecoServer != null)
    {
      this.eps = new EcoPropertiesService(this.IsAdmin);
      ServicesManager.AddService(typeof (IEcoPropertiesService), (object) this.eps);
      this._propertiesEditor = new EcoPropertiesEditor();
      EcoDeliveryListPropertiesEditor propertiesEditor = new EcoDeliveryListPropertiesEditor(serviceProvider);
    }
    this.iSB = (IStatusBar) serviceProvider.GetService(typeof (IStatusBar));
    this.scalePanel = new StatusBarPanel();
    this.scalePanel.Name = "ImageScale";
    this.scalePanel.ToolTipText = Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_237");
    this.scalePanel.Width = 60;
    this.scalePanel.Alignment = HorizontalAlignment.Center;
    service2.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.iobjCr_OnObjectCreatorCompletedEvent);
    RevisionComplectClient.Load(serviceProvider);
    if (!service8.IsLoadComplete)
      return;
    this.manager_LoadComplete((object) service8, new EventArgs());
  }

  private List<int> GetFullParentTypes(int objTypeId)
  {
    List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(objTypeId);
    if (!parentsIdReverse.Contains(objTypeId))
      parentsIdReverse.Add(objTypeId);
    return parentsIdReverse;
  }

  private void iobjCr_OnObjectCreatorCompletedEvent(object sender, AfterObjectCreatedEventArgs ea)
  {
    List<int> fullParentTypes = this.GetFullParentTypes(ea.ObjectTypeID);
    if (!fullParentTypes.Contains(RevHelper.idObj_II) && !fullParentTypes.Contains(RevHelper.idObj_PI) && !fullParentTypes.Contains(RevHelper.idObj_PR))
      return;
    long objectId = ea.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject == null)
        return;
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        objectId = dbObject.CheckOut().ObjectID;
      ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectId));
    }
    if (!ECOPlugin.ForceECOOpening && !ea.RunEditor || ECOPlugin.BlockECOOpening)
      return;
    ECOPlugin.FindPlugin().InvokeCommandForObject(objectId, "EditDocument");
  }

  private void ioCOS_ObjectsCheckOutEventHandler(object sender, ObjectsCheckOutEventArgs e)
  {
    e.Handled = true;
    List<int> intList = new List<int>(e.ResultVersions.Count);
    List<HidingType> hideTypes = new List<HidingType>(e.ResultVersions.Count);
    ReqRevision rr = ReqRevision.NoRevision;
    RequireClass requireClass = RequireClass.NoRequire;
    for (int index = 0; index < e.ResultVersions.Count; ++index)
    {
      if (e.ResultVersions[index].Mode == ObjectCheckedOutVersionMode.NewVersion)
      {
        ObjectCheckOutVersionDescription sourceVersion = e.SourceVersions[index];
        ReqRevision revReq = RevReqHelper.GetRevReq(sourceVersion.F_LCSTEP_ID, sourceVersion.F_OBJECT_TYPE);
        ReqRevisionInfo reqRevisionInfo = new ReqRevisionInfo(revReq);
        if (index == 0)
        {
          intList.Add(0);
          hideTypes.Add(HidingType.Disabled);
          if (reqRevisionInfo.reqType > requireClass)
          {
            rr = revReq;
            requireClass = reqRevisionInfo.reqType;
          }
        }
        else if (reqRevisionInfo.reqType != RequireClass.NoRequire)
        {
          intList.Add(index);
          hideTypes.Add(reqRevisionInfo.reqType == RequireClass.Require ? HidingType.Disabled : HidingType.Hidden);
          if (reqRevisionInfo.reqType > requireClass)
          {
            rr = revReq;
            requireClass = reqRevisionInfo.reqType;
          }
        }
      }
    }
    if (intList.Count <= 0 || rr == ReqRevision.NoRevision)
      return;
    List<long> objIDs = new List<long>(intList.Count);
    foreach (int index in intList)
      objIDs.Add(e.ResultVersions[index].F_OBJECT_ID);
    e.Rollback = !this.OnCreateVersion(objIDs, hideTypes, rr, e.ResultVersions, out e.Cancel);
  }

  private void ObjCreator_BeforeCommitCreationEvent(object sender, BeforeCommitCreationEventArgs e)
  {
    if (!this.eps.Current.CheckObjectCreation || e.Object == null)
      return;
    ReqRevision revReq = RevReqHelper.GetRevReq(e.Object.LCStep, e.Object.ObjectType);
    if (new ReqRevisionInfo(revReq).reqType == RequireClass.NoRequire)
      return;
    this.DoAttachNewObjects(new List<long>()
    {
      e.Object.ObjectID
    }, new List<HidingType>() { HidingType.Disabled }, revReq);
  }

  private void IAVSCS_BeforeCommitCreationAVSDocumentEvent(
    object sender,
    BeforeCommitCreationAVSDocumentEventArgs e)
  {
    if (!this.eps.Current.CheckObjectCreation || e.Document == null || new ReqRevisionInfo(RevReqHelper.GetRevReq(e.Document.LCStep, e.Document.ObjectType)).reqType == RequireClass.NoRequire)
      return;
    List<long> longList = new List<long>()
    {
      e.Document.ObjectID
    };
    List<HidingType> hidingTypeList = new List<HidingType>()
    {
      HidingType.Disabled
    };
    if (e.NewObjectIDs == null)
      return;
    foreach (long newObjectId in e.NewObjectIDs)
    {
      longList.Add(newObjectId);
      hidingTypeList.Add(HidingType.CanBeHidden);
    }
  }

  public void DoAttachNewObjects(List<long> objIDs, List<HidingType> hideTypes, ReqRevision rr)
  {
    ReqRevisionInfo reqRevisionInfo = new ReqRevisionInfo(rr);
    if (!reqRevisionInfo.wantsCJRecord && !reqRevisionInfo.wantsECO)
      return;
    List<long> noDObjs = new List<long>();
    long num1 = -1;
    QuickObjectInfo qoi = new QuickObjectInfo();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = objIDs.Count - 1; index >= 0; --index)
      {
        qoi = sessionKeeper.Session.GetObjectInfo(objIDs[index]);
        if (ECOPlugin.includingObjIds.Contains(qoi.ID))
          return;
      }
      Guid attributeGuid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
      for (int index = objIDs.Count - 1; index >= 0; --index)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objIDs[index], false);
        if (dbObject != null)
        {
          if (num1 == -1L)
            num1 = dbObject.ModificationID;
          if (dbObject.ModificationID != num1)
            num1 = 0L;
          int num2 = ECOPlugin.plugin.eps.Current.ReplaceEmptyDesignByTemplate ? 1 : 0;
          bool flag = ECOPlugin.plugin.eps.Current.InvNumAttr != "";
          if (num2 == 0 || !flag)
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(attributeGuid);
            if (attributeByGuid == null || attributeByGuid.AsString == "")
            {
              noDObjs.Add(objIDs[index]);
              objIDs.RemoveAt(index);
              hideTypes.RemoveAt(index);
            }
          }
        }
      }
    }
    if (objIDs.Count <= 0)
    {
      int num3 = (int) MessageBox.Show(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_229"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      if (reqRevisionInfo.reqType == RequireClass.Require)
        throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
    }
    else
    {
      ImDocument documentECO = (ImDocument) null;
      long objectID = -1;
      IDBObject dbObject1 = (IDBObject) null;
      RevType revType = RevType.II;
      ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (service1.CachedEditingContextID != 0L && reqRevisionInfo.wantsECO && service1.EditingContextMode == EditingContextMode.AutoUpdate)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          List<long> linkedContexts = (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetLinkedContexts((object) session.SessionGUID, service1.CachedEditingContextModificationID);
          int index = 0;
          List<string> stringList = new List<string>();
          while (index < linkedContexts.Count)
          {
            IDBObject dbObject2 = session.GetObject(linkedContexts[index], false);
            if (dbObject2 != null)
            {
              bool flag = MetaDataHelper.IsObjectTypeChildOf(dbObject2.ObjectType, RevHelper.idObjRevision);
              if (flag)
              {
                int maxDocsAllowed = ECOPlugin.plugin.eps.Current.MaxDocsAllowed;
                if (maxDocsAllowed > 0 && ECOPlugin.GetECO_ObjectsCount(linkedContexts[index]) >= maxDocsAllowed)
                  flag = false;
              }
              if (flag)
              {
                stringList.Add(dbObject2.Caption);
                ++index;
              }
              else
                linkedContexts.RemoveAt(index);
            }
          }
          if (linkedContexts.Count > 0)
          {
            ChooseRev chooseRev = new ChooseRev();
            switch (chooseRev.Execute(stringList, objIDs))
            {
              case DialogResult.OK:
                objectID = linkedContexts[chooseRev.sel_index];
                dbObject1 = session.GetObject(objectID);
                revType = dbObject1.ObjectType != RevHelper.idObj_PI ? (dbObject1.ObjectType != RevHelper.idObj_PR ? RevType.II : RevType.PR) : RevType.PI;
                bool readOnly = false;
                dbObject1 = DocumentEditorPlugin.TryCheckOutDocument(dbObject1, ref readOnly);
                if (readOnly)
                  throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_99"));
                documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(dbObject1, 0, false, true, false);
                break;
              case DialogResult.Cancel:
                if (reqRevisionInfo.reqType != RequireClass.Require)
                  return;
                throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
            }
          }
        }
      }
      bool flag1 = reqRevisionInfo.wantsECO;
      if (reqRevisionInfo.wantsCJRecord && reqRevisionInfo.wantsECO)
      {
        ECOorCJForm ecOorCjForm = new ECOorCJForm();
        if (!ecOorCjForm.Execute(reqRevisionInfo.reqType == RequireClass.Require))
        {
          if (reqRevisionInfo.reqType == RequireClass.Suggest)
            return;
          throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
        }
        if (ecOorCjForm.IncludeToECO)
          flag1 = true;
        else if (ecOorCjForm.IncludeToCJ)
        {
          flag1 = false;
        }
        else
        {
          if (reqRevisionInfo.reqType != RequireClass.Require)
            return;
          throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
        }
      }
      if (flag1)
      {
        if (objectID == -1L)
        {
          using (RevisionWizardForm revisionWizardForm = new RevisionWizardForm(-1, reqRevisionInfo.reqType, true, objIDs, ECOGoal.Creation, qoi))
          {
            if (revisionWizardForm.ShowDialog() == DialogResult.OK)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                long selRevId = revisionWizardForm.SelRevId;
                INotificationService service2 = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
                if (selRevId == -1L)
                {
                  dbObject1 = sessionKeeper.Session.GetObject(revisionWizardForm.ECOObjectID);
                  documentECO = revisionWizardForm.DocumentECO;
                  service2?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbObject1.ObjectID));
                  long objectId = dbObject1.ObjectID;
                  dbObject1 = dbObject1.CheckOut();
                  service2?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
                  {
                    objectId
                  }, (IList<long>) new long[1]
                  {
                    dbObject1.ObjectID
                  }));
                }
                else
                {
                  dbObject1 = sessionKeeper.Session.GetObject(selRevId);
                  bool readOnly = false;
                  dbObject1 = DocumentEditorPlugin.TryCheckOutDocument(dbObject1, ref readOnly);
                  if (readOnly)
                    throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_99"));
                }
                documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(dbObject1, 0, false, true, false);
                revType = revisionWizardForm.RT;
              }
            }
            if (revisionWizardForm.abortVersion || reqRevisionInfo.reqType == RequireClass.Require && documentECO == null)
              throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
            if (dbObject1 == null)
              return;
            if (documentECO == null)
              return;
          }
        }
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_313"));
        if (reqRevisionInfo.reqType == RequireClass.Require)
          stringBuilder.AppendLine(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_314"));
        long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_312"), stringBuilder.ToString(), RevHelper.idChangeJournal, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
        if (numArray == null || numArray.Length == 0)
        {
          if (reqRevisionInfo.reqType != RequireClass.Require)
            return;
          throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          dbObject1 = sessionKeeper.Session.GetObject(numArray[0], false);
          bool readOnly = false;
          dbObject1 = DocumentEditorPlugin.TryCheckOutDocument(dbObject1, ref readOnly);
          if (readOnly)
            throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_99"));
          documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(dbObject1, 0, false, true, false);
          revType = RevType.CJ;
        }
      }
      Intermech.ECO.Client.ECO eco = new Intermech.ECO.Client.ECO(documentECO, dbObject1.ObjectID, dbObject1.ObjectGUID, revType);
      IncludeGoal includeGoal = new IncludeGoal();
      includeGoal.BlockCreate = true;
      int schemeId;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        schemeId = ECOPlugin.GetSchemeID(sessionKeeper.Session, objIDs);
      List<long> objIDs1 = new List<long>() { objIDs[0] };
      if (!includeGoal.Execute(objIDs1, eco.litera, noDObjs, (List<long>) null, revType, ECOGoal.Creation, schemeId))
      {
        if (rr == ReqRevision.ForceRevision)
          throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_463"));
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          int num4 = eco.litera != includeGoal.litera ? 1 : 0;
          eco.litera = includeGoal.litera;
          if (revType == RevType.CJ)
          {
            CJEditorForm cjEditorForm = this.CreateCJEditorForm(eco, false, true, true, false);
            IObjectCreatorService service3 = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
            service3.ObjectCreatorCanceledEvent += new ObjectCreatorCanceledEventHandler(this._ObjectCreatorCanceledEvent);
            service3.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this._AfterObjectCreatedEvent);
            this.curCJEditorForm = cjEditorForm;
            this.newObjVerList = new List<long>();
            foreach (long objId in objIDs)
              this.newObjVerList.Add(objId);
            if (this.eps.Current.AutoCheckOut)
            {
              foreach (long objId in objIDs)
              {
                IDBObject dbObject3 = sessionKeeper.Session.GetObject(objId, false);
                if (dbObject3 != null && dbObject3.ObjectModifyMode == ObjectModifyModes.Checkout)
                  dbObject3.CheckOut();
              }
            }
            this.curCJEditorForm.AttachItemsToCJ(objIDs[0], sessionKeeper.Session, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, (Hashtable) null);
          }
          else
          {
            if (num1 != 0L)
            {
              eco.linkedContextNo = Math.Abs(num1);
              IDBAttribute dbAttribute = dbObject1.Attributes.AddAttribute(RevHelper.idLinkedContNumber, false);
              if (dbAttribute != null)
                dbAttribute.AsInteger = Math.Abs(eco.linkedContextNo);
            }
            ECOEditorForm ecoEditorForm = this.CreateECOEditorForm(eco, false, true, true, false);
            ecoEditorForm.UpdateDocDesign();
            IObjectCreatorService service4 = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
            service4.ObjectCreatorCanceledEvent += new ObjectCreatorCanceledEventHandler(this._ObjectCreatorCanceledEvent);
            service4.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this._AfterObjectCreatedEvent);
            this.curEditorForm = ecoEditorForm;
            this.newObjVerList = new List<long>();
            foreach (long objId in objIDs)
              this.newObjVerList.Add(objId);
            ecoEditorForm.NewAttachItems(objIDs, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, includeGoal.separateChanges, hideTypes);
            eco.newVers.AddRange((IEnumerable<long>) objIDs);
          }
        }
      }
    }
  }

  private void manager_LoadComplete(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service1)
      service1.RegisterCategoryProps(7, (ICategoryProps) new RequireRevisionProperty());
    INotificationService service2 = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    if (service2 != null)
    {
      this.cIn = new NotificationEventHandler(this.OnObjectCheckedIn);
      service2.Subscribe("ObjectsCheckedIn", this.cIn);
      this.cOut = new NotificationEventHandler(this.OnObjectCheckedOut);
      service2.Subscribe("ObjectsCheckedOut", this.cOut);
      this.cDel = new NotificationEventHandler(this.OnObjectDeleted);
      service2.Subscribe("ObjectsRemoved", this.cDel);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ECOHolder.DeliveryListParametersInit(sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidObj_II), true, sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidObj_PI), true, sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidObj_PR), true, sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidObj_DI), true, sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidObj_DPI), true, sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidChangeJournal), true, sessionKeeper.Session);
      this.SetIntegratorActions(new Guid(RevHelper.guidObjTypeServiceNote), true, sessionKeeper.Session);
    }
    this.revInfoList = new Dictionary<long, ECOPlugin.RevInfo>();
    List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    this._docTypes = new HashSet<int>();
    foreach (int num in childrenIdRecursive1)
      this._docTypes.Add(num);
    this.PossibleLiteras = new List<string>(MetaDataHelper.GetAttributeType(RevHelper.idAttrLitera).PossibleValues.Cast<string>());
    List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
    this._prodTypes = new HashSet<int>();
    foreach (int num in childrenIdRecursive2)
      this._prodTypes.Add(num);
    this.InitIzvTypes();
    if (!this.IsAdmin)
    {
      ICurrentUserAndRole service3 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(service3.RoleID, true).GetAttributeByID(RevHelper.idAttrAllowLCChange);
        this.IsLCChangeAllowed = attributeById == null || attributeById.AsBoolean;
      }
    }
    this.IconService = (ICategoryTypeIconService) ECOPlugin.serviceProvider.GetService(typeof (ICategoryTypeIconService));
    IPropertyPagesService service4 = (IPropertyPagesService) ApplicationServices.Container.GetService(typeof (IPropertyPagesService));
    if (service4 != null)
    {
      service4.AddPage(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_437") + Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_436"), (IPropertyPage) new HideTypesPropertyPage());
      service4.AddPage(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_437") + Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_442"), (IPropertyPage) new IMCatalogsPropertyPage());
    }
    DocumentEditorPlugin.AfterLoadDocument += new AfterLoadDocumentEventHandler(this.FillRazoslatAfterLoadDoc);
  }

  public void Unload()
  {
    ((ILicenser) ServicesManager.GetService(typeof (ILicenser))).ReleaseLicense(340);
    RevHelper.Global.CreateVersion -= this.cvh;
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    if (service != null)
    {
      if (this.cIn != null)
        service.Unsubscribe(this.cIn);
      if (this.cOut != null)
        service.Unsubscribe(this.cOut);
    }
    this.revInfoList = (Dictionary<long, ECOPlugin.RevInfo>) null;
  }

  public int GetMaxLiteraIndex(IEnumerable<long> objIDs)
  {
    int maxLiteraIndex = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in objIDs)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objId, false);
        if (dbObject != null)
        {
          object[] valuesById = dbObject.GetValuesByID(RevHelper.idAttrLitera, false);
          if (valuesById != null && valuesById.Length != 0 && valuesById[0] != null && valuesById[0] != DBNull.Value)
          {
            int num = this.PossibleLiteras.IndexOf(Convert.ToString(valuesById[0]));
            if (num > maxLiteraIndex)
              maxLiteraIndex = num;
          }
        }
      }
    }
    return maxLiteraIndex;
  }

  private void NavigatorWindowCaptionEventHandler(object sender, NavigatorWindowCaptionEventArgs e)
  {
    if (e == null || e.RootDescriptor == null || UISettings.NavigatorWindowCaptionsMode == NavigatorWindowCaptionsMode.Default || !(e.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor rootDescriptor))
      return;
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      relationCollection.LocalTypesMode = true;
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, rootDescriptor.ObjectID);
      if (dataTable != null)
      {
        if (dataTable.Rows.Count > 0)
        {
          long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
          IDBObject dbObject = sessionKeeper.Session.GetObject(int64);
          if (dbObject != null)
            str = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString;
        }
      }
    }
    if (!(str != ""))
      return;
    if (UISettings.NavigatorWindowCaptionsMode == NavigatorWindowCaptionsMode.ExtraTexts)
      e.ExtraText = str;
    e.TextHint = $" {Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_231")} \"{str}\"";
  }

  private void OnObjectCheckedIn(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
      return;
    List<ObjInfoItem> objInfoItemList = (List<ObjInfoItem>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      objInfoItemList = this.FilterIzvObjects(sessionKeeper.Session, (IEnumerable<long>) objectsEventArgs.ObjectIDs);
      if (objInfoItemList == null)
        return;
    }
    for (int index = 0; index < objInfoItemList.Count; ++index)
    {
      long objectId = objInfoItemList[index].ObjectID;
      ECOEditorForm openedEcoEditor = this.GetOpenedECOEditor(objectId, true);
      if (openedEcoEditor != null)
        openedEcoEditor.DocumentControl.Document = DocumentEditorPlugin.LoadDocumentFromDBObject(objectId, 0);
    }
  }

  private void OnObjectCheckedOut(object sender, NotificationEventArgs e)
  {
    if (this.blockOnCheckedOut || !(e is DBObjectsCheckOutEventArgs checkOutEventArgs) || checkOutEventArgs.ObjectIDs == null)
      return;
    List<ObjInfoItem> objInfoItemList = (List<ObjInfoItem>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      objInfoItemList = this.FilterIzvObjects(sessionKeeper.Session, (IEnumerable<long>) checkOutEventArgs.ObjectIDs);
      if (objInfoItemList == null)
        return;
    }
    for (int index = 0; index < objInfoItemList.Count; ++index)
      this.GetOpenedECOEditor(objInfoItemList[index].ObjectID, true);
  }

  private void OnObjectDeleted(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
      return;
    List<ObjInfoItem> objInfoItemList = (List<ObjInfoItem>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      objInfoItemList = this.FilterIzvObjects(sessionKeeper.Session, (IEnumerable<long>) objectsEventArgs.ObjectIDs);
      if (objInfoItemList == null)
        return;
    }
    for (int index = 0; index < objInfoItemList.Count; ++index)
    {
      ECOEditorForm openedEcoEditor = this.GetOpenedECOEditor(objInfoItemList[index].ObjectID, true);
      if (openedEcoEditor != null)
      {
        openedEcoEditor.Document.Modified = false;
        openedEcoEditor.Close();
      }
    }
  }

  protected void InitIzvTypes()
  {
    this._izvTypes = new HashSet<int>();
    this._izvTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_II));
    this._izvTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PI));
    this._izvTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PR));
  }

  public List<ObjInfoItem> FilterIzvObjects(IUserSession ius, IEnumerable<long> objIds)
  {
    this._Assert(ius != null, "ius == null!");
    this._Assert(objIds != null, "objIds == null");
    this._Assert(this._izvTypes != null, "_izvTypes == null");
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    foreach (long objId in objIds)
      objInfoList.Add(new ObjInfoItem(objId));
    ITypedInfoService service = ServiceUtils.GetService<ITypedInfoService>((object) ius, true);
    this._Assert(service != null, "typedInfoService == null");
    List<ObjInfoItem> objInfoItemList = service.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoList, (object) ius.SessionGUID);
    if (objInfoItemList == null)
      return objInfoItemList;
    for (int index = objInfoItemList.Count - 1; index >= 0; --index)
    {
      ObjInfoItem objInfoItem = objInfoItemList[index];
      this._Assert((TypedInfoItem) objInfoItem != (TypedInfoItem) null, "oi == null");
      if (objInfoItem.ObjTypeID == -1 || !this._izvTypes.Contains(objInfoItem.ObjTypeID))
        objInfoItemList.RemoveAt(index);
    }
    return objInfoItemList;
  }

  internal void _Assert(bool cond, string errMessage)
  {
    if (cond)
      return;
    int num = (int) MessageBox.Show(errMessage, "Assert condition failed!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  public string Name => Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_92");

  private void ActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
  {
  }

  public ECOAncestorForm ActiveECOEditorForm
  {
    [DebuggerStepThrough] get => ECOPlugin.dockManager.ActiveDocument as ECOAncestorForm;
  }

  private DocumentControl ActiveImDocumentControl
  {
    [DebuggerStepThrough] get
    {
      return ECOPlugin.DockManager.ActiveDocument != null && ECOPlugin.DockManager.ActiveDocument is ECOEditorForm ? (ECOPlugin.DockManager.ActiveDocument as ECOEditorForm).DocumentControl : (DocumentControl) null;
    }
  }

  public ECOEditorForm OpenECOEditorForObject(
    long objectID,
    bool readOnly,
    bool show,
    bool checkExists,
    bool replace)
  {
    if (objectID == -1L)
      throw new ArgumentOutOfRangeException(nameof (objectID), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_93"));
    ECOEditorForm ecoEditorForm = (ECOEditorForm) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject documentObject = sessionKeeper.Session.GetObject(objectID, false);
      if (documentObject == null)
        return (ECOEditorForm) null;
      if (!MetaDataHelper.IsObjectTypeChildOf(documentObject.ObjectType, DocIDCache.ObjType_ECO))
        throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_94"));
      IDBObject docObject = DocumentEditorPlugin.TryCheckOutDocument(documentObject, ref readOnly);
      RevType rType = RevType.II;
      if (docObject.ObjectType == RevHelper.idObj_PI)
        rType = RevType.PI;
      if (docObject.ObjectType == RevHelper.idObj_PR)
        rType = RevType.PR;
      long num1 = 0;
      ICurrentUserAndRole service = ServicesManager.GetService<ICurrentUserAndRole>();
      if (readOnly && service != null)
        num1 = service.EditingContextID;
      Intermech.ECO.Client.ECO eco = new Intermech.ECO.Client.ECO(DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, 0, Guid.Empty, false, true, true, false) as ImDocument, docObject.ObjectID, docObject.ObjectGUID, rType);
      IDBAttribute attributeById = docObject.GetAttributeByID(RevHelper.idAttrVersion);
      if (attributeById != null)
        eco.ecoVersion = attributeById.AsInteger;
      ecoEditorForm = this.CreateECOEditorForm(eco, readOnly, show, checkExists, replace);
      if (readOnly)
      {
        if (service != null)
        {
          DocumentEditorPlugin.AfterLoadDocument -= new AfterLoadDocumentEventHandler(this.FillRazoslatAfterLoadDoc);
          int num2 = service.LockEditingContextID ? 1 : 0;
          if (num2 != 0)
            service.LockEditingContextID = false;
          service.EditingContextID = num1;
          if (num2 != 0)
            service.LockEditingContextID = true;
          DocumentEditorPlugin.AfterLoadDocument += new AfterLoadDocumentEventHandler(this.FillRazoslatAfterLoadDoc);
        }
      }
    }
    return ecoEditorForm;
  }

  public DIEditorForm OpenDIEditorForObject(
    long objectID,
    bool readOnly,
    bool show,
    bool checkExists,
    bool replace)
  {
    if (objectID == -1L)
      throw new ArgumentOutOfRangeException(nameof (objectID), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_93"));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject documentObject = sessionKeeper.Session.GetObject(objectID);
      if (documentObject.ObjectType != RevHelper.idObj_DI && documentObject.ObjectType != RevHelper.idObj_DPI)
        throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_283"));
      IDBObject docObject = DocumentEditorPlugin.TryCheckOutDocument(documentObject, ref readOnly);
      RevType rType = RevType.DI;
      if (docObject.ObjectType == RevHelper.idObj_DPI)
        rType = RevType.DPI;
      Intermech.ECO.Client.ECO eco = new Intermech.ECO.Client.ECO(DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, 0, false, true, false), objectID, docObject.ObjectGUID, rType);
      IDBAttribute attributeById = docObject.GetAttributeByID(RevHelper.idAttrVersion);
      if (attributeById != null)
        eco.ecoVersion = attributeById.AsInteger;
      return this.CreateDIEditorForm(eco, readOnly, show, checkExists, replace);
    }
  }

  public CJEditorForm OpenCJEditorForObject(
    long objectID,
    bool readOnly,
    bool show,
    bool checkExists,
    bool replace)
  {
    if (objectID == -1L)
      throw new ArgumentOutOfRangeException(nameof (objectID), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_93"));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject documentObject = sessionKeeper.Session.GetObject(objectID);
      if (!MetaDataHelper.IsObjectTypeChildOf(documentObject.ObjectType, RevHelper.idChangeJournal))
        throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_297"));
      IDBObject docObject = DocumentEditorPlugin.TryCheckOutDocument(documentObject, ref readOnly);
      RevType rType = RevType.CJ;
      Intermech.ECO.Client.ECO eco = new Intermech.ECO.Client.ECO(DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, 0, false, true, false), objectID, docObject.ObjectGUID, rType);
      IDBAttribute attributeById = docObject.GetAttributeByID(RevHelper.idAttrVersion);
      if (attributeById != null)
        eco.ecoVersion = attributeById.AsInteger;
      return this.CreateCJEditorForm(eco, readOnly, show, checkExists, replace);
    }
  }

  public static IDBObject CheckObjectReadOnlyQuiet(
    IDBObject documentObject,
    IUserSession session,
    ref bool readOnly)
  {
    readOnly = false;
    switch (documentObject.ObjectModifyMode)
    {
      case ObjectModifyModes.Checkout:
        if (documentObject.ObjectID > 0L)
        {
          if (documentObject.CheckoutBy == 0L)
          {
            readOnly = true;
            break;
          }
          if (documentObject.CheckoutBy == session.UserID)
          {
            documentObject = session.GetObject(-documentObject.ObjectID, false);
            break;
          }
          readOnly = true;
          break;
        }
        break;
      case ObjectModifyModes.CreateVersion:
        if (documentObject.ObjectID > 0L)
        {
          readOnly = true;
          break;
        }
        break;
      case ObjectModifyModes.CantModify:
        readOnly = true;
        break;
    }
    return documentObject;
  }

  public Intermech.ECO.Client.ECO CreateECO(int objTypeId, ECOGoal goal, string objectCaption)
  {
    using (RevisionWizardForm revisionWizardForm = new RevisionWizardForm(objTypeId, RequireClass.NoRequire, false, (List<long>) null, goal, objectCaption))
    {
      revisionWizardForm.MS = (MemoryStream) null;
      RevType rt = revisionWizardForm.RT;
      if (revisionWizardForm.ShowDialog() == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject ecoDBObject = sessionKeeper.Session.GetObject(revisionWizardForm.ECOObjectID);
          return this._ComposeECO(revisionWizardForm.DocumentECO, ecoDBObject, rt, (string) null);
        }
      }
    }
    return (Intermech.ECO.Client.ECO) null;
  }

  public Intermech.ECO.Client.ECO CreateECO(
    int objTypeId,
    ECOGoal goal,
    string objCapt,
    out bool Existing,
    bool blockOpening = false,
    List<long> selObjs = null)
  {
    Existing = false;
    using (RevisionWizardForm revisionWizardForm = new RevisionWizardForm(objTypeId, RequireClass.NoRequire, true, selObjs, goal, objCapt, blockOpening))
    {
      revisionWizardForm.MS = (MemoryStream) null;
      if (revisionWizardForm.ShowDialog() == DialogResult.OK)
      {
        RevType rt = revisionWizardForm.RT;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject ecoDBObject;
          if (revisionWizardForm.SelRevId != -1L)
          {
            ecoDBObject = sessionKeeper.Session.GetObject(revisionWizardForm.SelRevId);
            rt = ecoDBObject.ObjectType != RevHelper.idObj_PR ? (ecoDBObject.ObjectType != RevHelper.idObj_PI ? RevType.II : RevType.PI) : RevType.PR;
            Existing = true;
          }
          else
            ecoDBObject = sessionKeeper.Session.GetObject(revisionWizardForm.ECOObjectID);
          return this._ComposeECO(revisionWizardForm.DocumentECO, ecoDBObject, rt, (string) null);
        }
      }
    }
    return (Intermech.ECO.Client.ECO) null;
  }

  public Intermech.ECO.Client.ECO CreateECO(
    List<int> objTypes,
    ECOGoal goal,
    string objCapt,
    out bool Existing,
    bool blockOpening = false)
  {
    Existing = false;
    using (RevisionWizardForm revisionWizardForm = new RevisionWizardForm(objTypes, RequireClass.NoRequire, true, (List<long>) null, goal, objCapt, blockOpening))
    {
      revisionWizardForm.MS = (MemoryStream) null;
      RevType rt = revisionWizardForm.RT;
      if (revisionWizardForm.ShowDialog() == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject ecoDBObject;
          if (revisionWizardForm.SelRevId != -1L)
          {
            ecoDBObject = sessionKeeper.Session.GetObject(revisionWizardForm.SelRevId);
            rt = ecoDBObject.ObjectType != RevHelper.idObj_PR ? (ecoDBObject.ObjectType != RevHelper.idObj_PI ? RevType.II : RevType.PI) : RevType.PR;
            Existing = true;
          }
          else
            ecoDBObject = sessionKeeper.Session.GetObject(revisionWizardForm.ECOObjectID);
          return this._ComposeECO(revisionWizardForm.DocumentECO, ecoDBObject, rt, (string) null);
        }
      }
    }
    return (Intermech.ECO.Client.ECO) null;
  }

  private Intermech.ECO.Client.ECO _ComposeECO(
    ImDocument documentECO,
    IDBObject ecoDBObject,
    RevType rt,
    string litera)
  {
    long objectId = ecoDBObject.ObjectID;
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectId));
    if (ecoDBObject.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      ecoDBObject = ecoDBObject.CheckOut();
      service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
      {
        objectId
      }, (IList<long>) new long[1]{ ecoDBObject.ObjectID }));
    }
    if (documentECO == null)
      documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(ecoDBObject, 0, false, true, false);
    else if (documentECO.DBObjectID != ecoDBObject.ObjectID)
      DocumentEditorPlugin.UpdateDocumentDBObject(documentECO, ecoDBObject.ObjectID, true, true);
    Intermech.ECO.Client.ECO eco = new Intermech.ECO.Client.ECO(documentECO, ecoDBObject.ObjectID, ecoDBObject.ObjectGUID, rt);
    if (litera != null)
      eco.litera = litera;
    return eco;
  }

  public ECOEditorForm GetOpenedECOEditor(long ObjectID, bool replace)
  {
    if (ECOPlugin.dockManager != null && ECOPlugin.dockManager.DocumentContainer != null && ECOPlugin.dockManager.DocumentContainer.Documents != null)
    {
      foreach (DockControl document in ECOPlugin.dockManager.DocumentContainer.Documents)
      {
        if (document != null && document.Guid == ECOEditorForm.ECOWindowGuid)
        {
          if (document is ECOEditorForm)
          {
            long ecoId = (document as ECOEditorForm).ecoID;
            if (ecoId == ObjectID || ecoId == -ObjectID)
              return document as ECOEditorForm;
          }
          else
          {
            long ecoID;
            bool readOnly;
            this.ParsePersistString(document.PersistString, out ecoID, out readOnly);
            if (ecoID == ObjectID || ecoID == -ObjectID)
            {
              ECOEditorForm target = this.OpenECOEditorForObject(ecoID, readOnly, true, false, replace);
              if (replace)
                document.ReplaceTo((DockControl) target);
              return target;
            }
          }
        }
      }
    }
    return (ECOEditorForm) null;
  }

  public ECOEditorForm CreateECOEditorForm(
    Intermech.ECO.Client.ECO eco,
    bool readOnly,
    bool show,
    bool checkExists,
    bool replace)
  {
    if (eco.DocumentECO.DBObjectID == -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(eco.EcoDBObjectGuid);
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) eco.DocumentECO, dbObject);
      }
    }
    ECOEditorForm ecoEditorForm1 = (ECOEditorForm) null;
    if (checkExists)
    {
      ecoEditorForm1 = this.GetOpenedECOEditor(eco.DocumentECO.DBObjectID, replace);
      if (ecoEditorForm1 != null)
      {
        if (!readOnly && ecoEditorForm1.ReadOnly)
        {
          ecoEditorForm1.Close();
          ecoEditorForm1 = (ECOEditorForm) null;
        }
        else if (!replace)
        {
          ecoEditorForm1.ECO.litera = eco.litera;
          ecoEditorForm1.Activate();
          eco.DocumentECO = ecoEditorForm1.Document;
          return ecoEditorForm1;
        }
      }
    }
    ECOEditorForm ecoEditorForm2 = new ECOEditorForm((IImDocumentManager) this, eco.DocumentECO, readOnly);
    ecoEditorForm2.ECO = eco;
    ecoEditorForm2.SynchronizeECODocumentWithDB(ecoEditorForm2.ReadOnly);
    ecoEditorForm2.AfterLoadDoc();
    eco.DocumentECO.ModifiedChanged += new ModifiedChanged_EventHandler(this.DocumentModifiedChanged);
    if (show)
    {
      if (ECOPlugin.dockManager == null)
        throw new Exception("dockManager == null");
      ecoEditorForm2.Show(ECOPlugin.dockManager, DockState.Document);
      ecoEditorForm2.Select();
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, (ImDocumentEditorForm) ecoEditorForm2);
      RecentObjectsNode.MRUObjects.Add(eco.DocumentECO.DBObjectID, ObjectAction.Edit, DateTime.UtcNow);
    }
    else
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, (ImDocumentEditorForm) ecoEditorForm2);
    if (ecoEditorForm1 != null)
    {
      ecoEditorForm1.ReplaceTo((DockControl) ecoEditorForm2);
      ecoEditorForm1.Activate();
      ecoEditorForm2 = ecoEditorForm1;
    }
    return ecoEditorForm2;
  }

  public DIEditorForm GetOpenedDIEditor(long ObjectID, bool replace)
  {
    if (ECOPlugin.dockManager != null && ECOPlugin.dockManager.DocumentContainer != null && ECOPlugin.dockManager.DocumentContainer.Documents != null)
    {
      foreach (DockControl document in ECOPlugin.dockManager.DocumentContainer.Documents)
      {
        if (document != null && document.Guid == DIEditorForm.DIWindowGuid)
        {
          if (document is DIEditorForm)
          {
            long ecoId = (document as DIEditorForm).ecoID;
            if (ecoId == ObjectID || ecoId == -ObjectID)
              return document as DIEditorForm;
          }
          else
          {
            long ecoID;
            bool readOnly;
            this.ParsePersistString(document.PersistString, out ecoID, out readOnly);
            if (ecoID == ObjectID || ecoID == -ObjectID)
            {
              DIEditorForm target = this.OpenDIEditorForObject(ecoID, readOnly, true, false, replace);
              if (replace)
                document.ReplaceTo((DockControl) target);
              return target;
            }
          }
        }
      }
    }
    return (DIEditorForm) null;
  }

  public DIEditorForm CreateDIEditorForm(
    Intermech.ECO.Client.ECO eco,
    bool readOnly,
    bool show,
    bool checkExists,
    bool replace)
  {
    if (eco.DocumentECO.DBObjectID == -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(eco.EcoDBObjectGuid);
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) eco.DocumentECO, dbObject);
      }
    }
    if (checkExists)
    {
      DIEditorForm openedDiEditor = this.GetOpenedDIEditor(eco.DocumentECO.DBObjectID, replace);
      if (openedDiEditor != null)
      {
        openedDiEditor.ECO.litera = eco.litera;
        openedDiEditor.Activate();
        return openedDiEditor;
      }
    }
    DIEditorForm docWin = new DIEditorForm((IImDocumentManager) this, eco.DocumentECO, readOnly);
    docWin.ECO = eco;
    docWin.SynchronizeECODocumentWithDB(docWin.ReadOnly);
    docWin.AfterLoadDoc();
    eco.DocumentECO.ModifiedChanged += new ModifiedChanged_EventHandler(this.DocumentModifiedChanged);
    if (show)
    {
      if (ECOPlugin.dockManager == null)
        throw new Exception("dockManager == null");
      docWin.Show(ECOPlugin.dockManager, DockState.Document);
      docWin.Select();
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, (ImDocumentEditorForm) docWin);
      RecentObjectsNode.MRUObjects.Add(eco.DocumentECO.DBObjectID, ObjectAction.Edit, DateTime.UtcNow);
    }
    else
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, (ImDocumentEditorForm) docWin);
    return docWin;
  }

  public CJEditorForm GetOpenedCJEditor(long ObjectID, bool replace)
  {
    if (ECOPlugin.dockManager != null && ECOPlugin.dockManager.DocumentContainer != null && ECOPlugin.dockManager.DocumentContainer.Documents != null)
    {
      foreach (DockControl document in ECOPlugin.dockManager.DocumentContainer.Documents)
      {
        if (document != null && document.Guid == CJEditorForm.CJWindowGuid)
        {
          if (document is CJEditorForm)
          {
            long ecoId = (document as CJEditorForm).ecoID;
            if (ecoId == ObjectID || ecoId == -ObjectID)
              return document as CJEditorForm;
          }
          else
          {
            long ecoID;
            bool readOnly;
            this.ParsePersistString(document.PersistString, out ecoID, out readOnly);
            if (ecoID == ObjectID || ecoID == -ObjectID)
            {
              CJEditorForm target = this.OpenCJEditorForObject(ecoID, readOnly, true, false, replace);
              if (replace)
                document.ReplaceTo((DockControl) target);
              return target;
            }
          }
        }
      }
    }
    return (CJEditorForm) null;
  }

  public CJEditorForm CreateCJEditorForm(
    Intermech.ECO.Client.ECO eco,
    bool readOnly,
    bool show,
    bool checkExists,
    bool replace)
  {
    if (eco.DocumentECO.DBObjectID == -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(eco.EcoDBObjectGuid);
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) eco.DocumentECO, dbObject);
      }
    }
    if (checkExists)
    {
      CJEditorForm openedCjEditor = this.GetOpenedCJEditor(eco.DocumentECO.DBObjectID, replace);
      if (openedCjEditor != null)
      {
        openedCjEditor.ECO.litera = eco.litera;
        openedCjEditor.Activate();
        return openedCjEditor;
      }
    }
    CJEditorForm docWin = new CJEditorForm((IImDocumentManager) this, eco.DocumentECO, readOnly);
    docWin.ECO = eco;
    docWin.AfterLoadDoc();
    docWin.SynchronizeECODocumentWithDB(docWin.ReadOnly);
    eco.DocumentECO.ModifiedChanged += new ModifiedChanged_EventHandler(this.DocumentModifiedChanged);
    if (show)
    {
      if (ECOPlugin.dockManager == null)
        throw new Exception("dockManager == null");
      docWin.Show(ECOPlugin.dockManager, DockState.Document);
      docWin.Select();
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, (ImDocumentEditorForm) docWin);
      RecentObjectsNode.MRUObjects.Add(eco.DocumentECO.DBObjectID, ObjectAction.Edit, DateTime.UtcNow);
    }
    else
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, (ImDocumentEditorForm) docWin);
    return docWin;
  }

  private void DocumentModifiedChanged(object sender, ModifiedChanged_EventArgs e)
  {
    if (sender is ImDocument document)
      DocumentEditorPlugin.UpdateDocumentCaption(ECOPlugin.dockManager, document);
    if (this.commandManager == null)
      return;
    this.commandManager.QueryStatus();
  }

  public void ParsePersistString(string persistString, out long ecoID, out bool readOnly)
  {
    readOnly = false;
    ecoID = 0L;
    if (long.TryParse(persistString, out ecoID))
      ecoID = DBHelper.GetObjIDByGuid(DBHelper.GetObjGuidByID(ecoID));
    else if (persistString != null && persistString.Length != 36)
    {
      string empty = string.Empty;
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(persistString)))
      {
        object obj1 = new BinaryFormatter().Deserialize((Stream) serializationStream);
        if (!(obj1 is HybridDictionary))
          return;
        HybridDictionary hybridDictionary = obj1 as HybridDictionary;
        object objectGUID = hybridDictionary[(object) "DocumentGuid"];
        if (objectGUID != null && objectGUID is Guid guid && guid != Guid.Empty)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject((Guid) objectGUID, false);
            if (dbObject != null)
              ecoID = dbObject.ObjectID;
          }
        }
        object obj2 = hybridDictionary[(object) "ReadOnly"];
        if (obj2 == null || !(obj2 is bool flag))
          return;
        readOnly = flag;
      }
    }
    else
    {
      Guid objectGUID = new Guid(persistString);
      if (!(objectGUID != Guid.Empty))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
        if (dbObject == null)
          return;
        ecoID = dbObject.ObjectID;
      }
    }
  }

  public DockControl RestoreDocumentWindow(Guid guid, string persistString)
  {
    try
    {
      if (guid == ECOEditorForm.ECOWindowGuid)
      {
        long ecoID;
        bool readOnly;
        this.ParsePersistString(persistString, out ecoID, out readOnly);
        if (ecoID != 0L)
        {
          this.blockOnCheckedOut = true;
          try
          {
            return (DockControl) this.OpenECOEditorForObject(ecoID, readOnly, true, true, false);
          }
          finally
          {
            this.blockOnCheckedOut = false;
          }
        }
      }
      if (guid == DIEditorForm.DIWindowGuid)
      {
        long ecoID;
        bool readOnly;
        this.ParsePersistString(persistString, out ecoID, out readOnly);
        if (ecoID != 0L)
          return (DockControl) this.OpenDIEditorForObject(ecoID, readOnly, true, true, false);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return (DockControl) null;
  }

  public static void NewECO(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long ecoObjectId;
    using (SelIzvType selIzvType = new SelIzvType())
    {
      if (selIzvType.ShowDialog() != DialogResult.OK)
        return;
      ecoObjectId = selIzvType.EcoObjectID;
    }
    List<long> objIDs = new List<long>();
    if (items != null)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        objIDs.Add(itemData.ObjectID);
      }
    }
    if (ecoObjectId == -1L || items == null)
      return;
    ECOEditorForm openedEcoEditor = ECOPlugin.FindPlugin().GetOpenedECOEditor(ecoObjectId, false);
    if (openedEcoEditor == null)
      return;
    List<long> noDObjs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ECOPlugin.GetSchemeID(sessionKeeper.Session, objIDs);
      Guid attributeGuid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
      for (int index = objIDs.Count - 1; index >= 0; --index)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objIDs[index], false);
        if (dbObject != null)
        {
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(attributeGuid);
          if (attributeByGuid == null || attributeByGuid.AsString == "")
          {
            noDObjs.Add(objIDs[index]);
            objIDs.RemoveAt(index);
          }
        }
      }
      if (objIDs.Count <= 0)
        return;
    }
    IncludeGoal includeGoal = new IncludeGoal();
    if (!includeGoal.Execute(objIDs, openedEcoEditor.ECO.litera, noDObjs, (List<long>) null, openedEcoEditor.ECO.revType))
      return;
    List<long> finalObjectList = includeGoal.GetFinalObjectList();
    using (new SessionKeeper())
    {
      if (includeGoal.goal == ECOGoal.Litera)
        openedEcoEditor.ECO.litera = includeGoal.litera;
      openedEcoEditor.NewAttachItems(finalObjectList, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, includeGoal.separateChanges);
    }
  }

  public static void NewCJ(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IObjectCreatorService service = ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    ECOPlugin.ForceECOOpening = true;
    try
    {
      long objectByTypeDialog = service.CreateObjectByTypeDialog(RevHelper.idChangeJournal);
      switch (objectByTypeDialog)
      {
        case -1:
          break;
        case 0:
          break;
        default:
          ECOPlugin.FindPlugin().OpenCJEditorForObject(objectByTypeDialog, false, true, true, true);
          break;
      }
    }
    finally
    {
      ECOPlugin.ForceECOOpening = false;
    }
  }

  public static void NewCJForIzd(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    if (itemData == null || !ECOPlugin.plugin.CheckInitCJTemplate())
      return;
    long objectId = itemData.ObjectID;
    long cj = ECOPlugin.plugin.CreateCJ(objectId);
    if (cj == 0L)
      return;
    ECOPlugin.plugin.OpenCJEditorForObject(cj, false, true, true, true);
  }

  public static void OpenCJforRecord(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    if (itemData == null)
      return;
    long objectId = itemData.ObjectID;
    long objectID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrJournalLink);
        if (attributeById != null)
          objectID = Convert.ToInt64(attributeById.Value);
      }
    }
    if (objectID == 0L)
      return;
    ECOPlugin.plugin.OpenCJEditorForObject(objectID, false, true, true, true);
  }

  public static void ReplaceCJRecords(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> cjRecList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
      if (itemData != null && itemData.ObjectType == RevHelper.idObjCJRecord)
        cjRecList.Add(itemData.ObjectID);
    }
    if (cjRecList.Count <= 0)
      return;
    string revDesign = (string) null;
    ECOPlugin.ReplaceCJRecord(cjRecList, out revDesign);
  }

  public bool Execute(ICommandState commandState)
  {
    try
    {
      switch (commandState.CommandName)
      {
        case "NewECO":
          ECOPlugin.NewECO((ISelectedItems) null, (System.IServiceProvider) null, (object) null);
          return true;
        case "New.CJ":
          ECOPlugin.NewCJ((ISelectedItems) null, (System.IServiceProvider) null, (object) null);
          return true;
        case "New.CJRec":
          ECOPlugin.CreateCJRec(0L, 0L);
          return true;
        case "ECO.TestMenu":
          int num1 = (int) MessageBox.Show("ECO.TestMenu", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_96"), MessageBoxButtons.OK);
          return true;
        case "ECO.AddItem":
          int num2 = (int) MessageBox.Show("ECO.AddItem", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_97"), MessageBoxButtons.OK);
          return true;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return false;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    ECOAncestorForm activeEcoEditorForm = this.ActiveECOEditorForm;
    switch (commandState.CommandName)
    {
      case "CJRec.OpenCJ":
        commandState.Enabled = true;
        return true;
      case "ECO":
        commandState.Visible = activeEcoEditorForm != null;
        commandState.Enabled = activeEcoEditorForm != null;
        return true;
      case "ECO.AttachToECO":
        commandState.Enabled = activeEcoEditorForm != null && !activeEcoEditorForm.ReadOnly;
        return true;
      case "ECO.Card":
        commandState.Enabled = true;
        return true;
      case "ECO.Tree":
        commandState.Enabled = activeEcoEditorForm != null;
        return true;
      case "New.CJ":
        commandState.Enabled = true;
        return true;
      case "New.CJRec":
        commandState.Enabled = true;
        return true;
      case "New.Create":
        commandState.Enabled = true;
        return true;
      case "New.SetLitera":
        commandState.Enabled = true;
        return true;
      case "NewCJ.ForIzdel":
        commandState.Enabled = true;
        return true;
      case "NewECO":
        commandState.Enabled = true;
        return true;
      default:
        return false;
    }
  }

  private void ContextOpenECOEditor(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    try
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        if (itemData != null)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, DocIDCache.ObjType_ECO) || itemData.ObjectType == RevHelper.idChangeJournal)
          {
            if (itemData.ObjectType == RevHelper.idObj_DI || itemData.ObjectType == RevHelper.idObj_DPI)
              this.OpenDIEditorForObject(itemData.ObjectID, false, true, true, true);
            else if (itemData.ObjectType == RevHelper.idChangeJournal)
              this.OpenCJEditorForObject(itemData.ObjectID, false, true, true, true);
            else
              this.OpenECOEditorForObject(itemData.ObjectID, false, true, true, true);
            RecentObjectsNode.MRUObjects.Add(itemData.ObjectID, ObjectAction.Edit, DateTime.UtcNow);
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, new NotificationEventArgs("RecentObjectsChanged"));
          }
          if (itemData.ObjectType == RevHelper.idObjCJRecord)
            this.CJRecordActivate(itemData.ObjectID, false);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ContextViewECO(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    try
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        if (itemData != null)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, DocIDCache.ObjType_ECO) || itemData.ObjectType == RevHelper.idChangeJournal)
          {
            long objectId = itemData.ObjectID;
            int objectType = itemData.ObjectType;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
              if (dbObject != null)
              {
                IDBAttribute attributeById1 = dbObject.GetAttributeByID(RevHelper.idAttrScannedDoc);
                if (attributeById1 != null)
                {
                  if (attributeById1.AsBoolean)
                  {
                    if (!(dbObject.GetAttributeByID(RevHelper.idAttrFile) is IBlobReader attributeById2))
                      break;
                    BlobInformation blobInformation = attributeById2.OpenBlob(-1);
                    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
                    ClientContext.LaunchActions.LaunchByShell(new LaunchParams(LaunchType.View, objectId, objectType, editorRule)
                    {
                      ObjectFileName = blobInformation.FileName
                    });
                    break;
                  }
                }
              }
            }
            if (itemData.ObjectType == RevHelper.idObj_DI || itemData.ObjectType == RevHelper.idObj_DPI)
              this.OpenDIEditorForObject(objectId, true, true, true, true);
            else if (itemData.ObjectType == RevHelper.idChangeJournal)
              this.OpenCJEditorForObject(objectId, true, true, true, true);
            else
              this.OpenECOEditorForObject(objectId, true, true, true, true);
            RecentObjectsNode.MRUObjects.Add(objectId, ObjectAction.View, DateTime.UtcNow);
          }
          if (itemData.ObjectType == RevHelper.idObjCJRecord)
            this.CJRecordActivate(itemData.ObjectID, true);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ContextPrintECO(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    try
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        if (itemData != null && MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, DocIDCache.ObjType_ECO))
          RecentObjectsNode.MRUObjects.Add(itemData.ObjectID, ObjectAction.Print, DateTime.UtcNow);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void CJRecordActivate(long cjRecId, bool ReadOnly)
  {
    long objectID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(cjRecId, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrJournalLink);
        if (attributeById != null)
          objectID = Convert.ToInt64(attributeById.Value);
      }
    }
    if (objectID == 0L)
      return;
    ECOPlugin.plugin.OpenCJEditorForObject(objectID, ReadOnly, true, true, true).SelectRecById(cjRecId);
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      mergedCommands.Add("OpenDocument", new CommandInfo(4096 /*0x1000*/, new ClickEventHandler(this.ContextOpenECOEditor)));
      mergedCommands.Add("EditDocument", new CommandInfo(4096 /*0x1000*/, new ClickEventHandler(this.ContextOpenECOEditor)));
      mergedCommands.Add("ViewDocument", new CommandInfo(4096 /*0x1000*/, new ClickEventHandler(this.ContextViewECO)));
      mergedCommands.Add("PrintDocument", new CommandInfo(4096 /*0x1000*/, new ClickEventHandler(this.ContextPrintECO)));
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  [Browsable(false)]
  public ICommandManager CommandManager
  {
    get
    {
      if (this.commandManager == null)
        this.commandManager = (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
      return this.commandManager;
    }
  }

  [Browsable(false)]
  public bool IsElementSelecting
  {
    [DebuggerStepThrough] get => true;
    set
    {
    }
  }

  [Browsable(false)]
  public bool IsElementCreating
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  [Browsable(false)]
  public PageElementCreator SelectedElementCreator
  {
    [DebuggerStepThrough] get => (PageElementCreator) null;
    set
    {
    }
  }

  public void UpdateSelectedElementInfo()
  {
  }

  public void SelectionChanged()
  {
    DocumentControl imDocumentControl = this.ActiveImDocumentControl;
    if (imDocumentControl != null)
    {
      PropertyGridForm propertyGridDlg = this.ActiveECOEditorForm?.PropertyGridDlg;
      if (propertyGridDlg != null)
      {
        List<DocumentTreeNode> selectedNodes = imDocumentControl.SelectedNodes;
        if (selectedNodes != null && selectedNodes.Count > 0)
          propertyGridDlg.SelectedObjects = (object[]) selectedNodes.ToArray();
      }
    }
    this.UpdateSelectedElementInfo();
    this.commandManager.QueryStatus();
  }

  public void SetMessageText(string text)
  {
  }

  public void UpdatePagesInfo()
  {
  }

  [Browsable(false)]
  public SaveFileDialog SaveToFileDialog
  {
    [DebuggerStepThrough] get
    {
      if (this.saveToFileDialog == null)
        this.saveToFileDialog = ImDocumentEditorFormBase.CreateSaveFileDialog();
      return this.saveToFileDialog;
    }
  }

  [Browsable(false)]
  public string RecentlySaveAsPath
  {
    get => this.recentlySaveAsPath;
    set => this.recentlySaveAsPath = value;
  }

  public bool ShowInvisibleLines
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  public void UpdateFormatCommands()
  {
    if (this.ActiveECOEditorForm == null)
      return;
    this.ActiveECOEditorForm.UpdateFormatCommands();
  }

  public void ShowExceptionDialog(Exception e) => ExceptionHelper.ExceptionService.ShowException(e);

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    this.ConfigurationManager = configurationManager;
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
  }

  public IConfigurationManager ConfigurationManager { get; private set; }

  public ISelectedItems NavigatorMenuItems
  {
    get => this.navigatorMenuItems;
    set => this.navigatorMenuItems = value;
  }

  public long CurRevId
  {
    get => this._curRevId;
    set => this._curRevId = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private ISelectedItems SelectedMenuItems
  {
    get
    {
      ISelectedItems selectedMenuItems = this.navigatorMenuItems;
      if ((selectedMenuItems == null || selectedMenuItems.Count == 0) && this._curRevId != 0L)
        selectedMenuItems = ObjectExtensions.GetItems(this._curRevId);
      return selectedMenuItems;
    }
  }

  public void UpdateISimpleSelectedItemsService()
  {
    if (this.SelectedMenuItems == null)
      return;
    if ((ISimpleSelectedItems) ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this.SelectedMenuItems);
  }

  internal bool CheckInitCJTemplate()
  {
    this.UpdateCJTemplate();
    if (ECOPlugin.plugin.CJTemplateId == 0L)
      throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_288"));
    if (ECOPlugin.plugin.CJTempDoc == null)
      throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_289"), (object) ECOPlugin.plugin.CJTemplateId));
    return true;
  }

  internal bool UpdateCJTemplate()
  {
    if (this.CJTempDoc != null)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string conditionValue = Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_287");
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(RevHelper.idRevTemplate).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(RevHelper.idAttrDesign, RelationalOperators.Equal, (object) conditionValue, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
      }, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
      }));
      if (dataTable != null)
      {
        if (dataTable.Rows.Count > 0)
        {
          this.CJTemplateId = Convert.ToInt64(dataTable.Rows[0][0]);
          this.CJTempDoc = DocumentEditorPlugin.LoadDocumentFromDBObject(this.CJTemplateId, 0);
        }
      }
    }
    return this.CJTempDoc != null;
  }

  internal long GetProdJournal(long prodId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idChangeJournalLink);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      long prodJournal = 0;
      try
      {
        DataTable dataTable = relationCollection.ConsistFrom(paramSet, prodId);
        if (dataTable == null || dataTable.Rows.Count == 0)
          return 0;
        prodJournal = Convert.ToInt64(dataTable.Rows[0][0]);
      }
      catch
      {
      }
      return prodJournal;
    }
  }

  internal long CreateCJ(long prodId)
  {
    if (!this.CheckInitCJTemplate())
      return 0;
    long prodJournal = this.GetProdJournal(prodId);
    if (prodJournal != 0L)
    {
      int num = (int) MessageBox.Show(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_293"), (object) prodId, (object) prodJournal), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      return 0;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string str = "";
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(prodId, false);
      if (dbObject1 != null)
      {
        IDBAttribute attributeById = dbObject1.GetAttributeByID(RevHelper.idAttrDesign);
        if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
          str = Convert.ToString(attributeById.Value);
        if (str == "")
        {
          int num = (int) MessageBox.Show(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_294"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
          return 0;
        }
        str += Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_295");
      }
      if (sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService)
        customService.StartTransaction();
      long objectID = 0;
      long relationID = 0;
      try
      {
        IDBObject dbObject2 = sessionKeeper.Session.GetObjectCollection(RevHelper.idChangeJournal).Create();
        ImDocument ownerNode = new ImDocument(ECOPlugin.plugin.CJTempDoc, true, true);
        ownerNode.Reference = (ReferenceBase) new ReferenceToDBObject((DocumentTreeNode) ownerNode, dbObject2, false);
        if (ownerNode.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idRevDesignation) is TextData templateRecursive)
          templateRecursive.Text = str;
        MemoryStream aSourceStream = new MemoryStream();
        ownerNode.SaveToXml((Stream) aSourceStream);
        new BlobProcWriter(dbObject2.Attributes.FindByID(RevHelper.idAttrFile) ?? dbObject2.Attributes.AddAttribute(RevHelper.idAttrFile, false), 0, new BlobInformation(aSourceStream.Length, 0L, DateTime.Now, str + ".revx", ArcMethods.ZLibPacked, ""), (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        IDBAttribute dbAttribute = dbObject2.Attributes.AddAttribute(RevHelper.idAttrDesign, false);
        if (dbAttribute != null)
          dbAttribute.AsString = str;
        dbObject2.CommitCreation(true);
        objectID = dbObject2.ObjectID;
        relationID = sessionKeeper.Session.GetRelationCollection(RevHelper.idChangeJournalLink).Create(prodId, dbObject2.ObjectID).RelationID;
      }
      catch (Exception ex)
      {
        objectID = 0L;
        throw;
      }
      finally
      {
        if (customService != null)
        {
          if (objectID != 0L)
          {
            customService.Commit();
            INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
            if (service != null)
            {
              service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectID));
              service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", relationID));
            }
          }
          else
            customService.Rollback();
        }
      }
      return objectID;
    }
  }

  public List<int> allowedTypes
  {
    get
    {
      this.UpdateAllowedTypes();
      return this._allowedTypes;
    }
  }

  public List<string> PossibleLiteras { get; set; }

  public HashSet<int> DocTypes => this._docTypes;

  public HashSet<int> ProdTypes => this._prodTypes;

  public HashSet<int> IzvTypes => this._izvTypes;

  public static IImbaseSelector ImbaseSelector
  {
    get
    {
      if (ECOPlugin.imbaseSelector == null)
        ECOPlugin.imbaseSelector = ServicesManager.GetService(typeof (IImbaseSelector)) as IImbaseSelector;
      return ECOPlugin.imbaseSelector;
    }
  }

  public static INamedImageList NamedImageList
  {
    get
    {
      if (ECOPlugin.namedImageList == null)
        ECOPlugin.namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      return ECOPlugin.namedImageList;
    }
  }

  public bool OnCreateVersion(
    List<long> objIDs,
    List<HidingType> hideTypes,
    ReqRevision rr,
    List<ObjectCheckOutVersionDescription> objDescs,
    out bool verCanceled)
  {
    verCanceled = false;
    ReqRevisionInfo reqRevisionInfo = new ReqRevisionInfo(rr);
    if (reqRevisionInfo.reqType == RequireClass.NoRequire || !reqRevisionInfo.wantsCJRecord && !reqRevisionInfo.wantsECO)
      return true;
    List<long> noDObjs = new List<long>();
    long num1 = -1;
    QuickObjectInfo qoi = new QuickObjectInfo();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = objIDs.Count - 1; index >= 0; --index)
      {
        qoi = sessionKeeper.Session.GetObjectInfo(objIDs[index]);
        if (ECOPlugin.includingObjIds.Contains(qoi.ID))
          return true;
      }
      Guid attributeGuid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
      for (int index = objIDs.Count - 1; index >= 0; --index)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objIDs[index], false);
        if (dbObject != null)
        {
          if (num1 == -1L)
            num1 = dbObject.ModificationID;
          if (dbObject.ModificationID != num1)
            num1 = 0L;
          int num2 = ECOPlugin.plugin.eps.Current.ReplaceEmptyDesignByTemplate ? 1 : 0;
          bool flag = ECOPlugin.plugin.eps.Current.InvNumAttr != "";
          if (num2 == 0 || !flag)
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(attributeGuid);
            if (attributeByGuid == null || attributeByGuid.AsString == "")
            {
              noDObjs.Add(objIDs[index]);
              objIDs.RemoveAt(index);
              hideTypes.RemoveAt(index);
              objDescs?.RemoveAt(index);
            }
          }
        }
      }
    }
    if (objIDs.Count <= 0)
    {
      int num3 = (int) MessageBox.Show(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_229"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      return reqRevisionInfo.reqType != RequireClass.Require;
    }
    ImDocument documentECO = (ImDocument) null;
    long objectID = -1;
    IDBObject dbObject1 = (IDBObject) null;
    RevType revType = RevType.II;
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (service1.CachedEditingContextID != 0L && reqRevisionInfo.wantsECO && service1.EditingContextMode == EditingContextMode.AutoUpdate)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        List<long> linkedContexts = (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetLinkedContexts((object) session.SessionGUID, service1.CachedEditingContextModificationID);
        int index = 0;
        List<string> stringList = new List<string>();
        while (index < linkedContexts.Count)
        {
          IDBObject dbObject2 = session.GetObject(linkedContexts[index], false);
          if (dbObject2 != null)
          {
            bool flag = MetaDataHelper.IsObjectTypeChildOf(dbObject2.ObjectType, RevHelper.idObjRevision);
            if (flag)
            {
              int maxDocsAllowed = ECOPlugin.plugin.eps.Current.MaxDocsAllowed;
              if (maxDocsAllowed > 0 && ECOPlugin.GetECO_ObjectsCount(linkedContexts[index]) >= maxDocsAllowed)
                flag = false;
            }
            if (flag)
            {
              stringList.Add(dbObject2.Caption);
              ++index;
            }
            else
              linkedContexts.RemoveAt(index);
          }
        }
        if (linkedContexts.Count > 0)
        {
          ChooseRev chooseRev = new ChooseRev();
          switch (chooseRev.Execute(stringList, objIDs))
          {
            case DialogResult.OK:
              objectID = linkedContexts[chooseRev.sel_index];
              dbObject1 = session.GetObject(objectID);
              revType = dbObject1.ObjectType != RevHelper.idObj_PI ? (dbObject1.ObjectType != RevHelper.idObj_PR ? RevType.II : RevType.PR) : RevType.PI;
              bool readOnly = false;
              dbObject1 = DocumentEditorPlugin.TryCheckOutDocument(dbObject1, ref readOnly);
              if (readOnly)
                throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_99"));
              documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(dbObject1, 0, false, true, false);
              break;
            case DialogResult.Cancel:
              return reqRevisionInfo.reqType != RequireClass.Require;
          }
        }
      }
    }
    bool flag1 = reqRevisionInfo.wantsECO;
    if (reqRevisionInfo.wantsCJRecord && reqRevisionInfo.wantsECO)
    {
      ECOorCJForm ecOorCjForm = new ECOorCJForm();
      if (!ecOorCjForm.Execute(reqRevisionInfo.reqType == RequireClass.Require))
        return reqRevisionInfo.reqType == RequireClass.Suggest;
      if (ecOorCjForm.IncludeToECO)
      {
        flag1 = true;
      }
      else
      {
        if (!ecOorCjForm.IncludeToCJ)
          return reqRevisionInfo.reqType != RequireClass.Require;
        flag1 = false;
      }
    }
    if (flag1)
    {
      if (objectID == -1L)
      {
        using (RevisionWizardForm revisionWizardForm = new RevisionWizardForm(-1, reqRevisionInfo.reqType, true, objIDs, ECOGoal.VersionCreate, qoi))
        {
          if (revisionWizardForm.ShowDialog() == DialogResult.OK)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              long selRevId = revisionWizardForm.SelRevId;
              INotificationService service2 = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
              if (selRevId == -1L)
              {
                dbObject1 = sessionKeeper.Session.GetObject(revisionWizardForm.ECOObjectID);
                documentECO = revisionWizardForm.DocumentECO;
                service2?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbObject1.ObjectID));
                long objectId = dbObject1.ObjectID;
                dbObject1 = dbObject1.CheckOut();
                service2?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
                {
                  objectId
                }, (IList<long>) new long[1]
                {
                  dbObject1.ObjectID
                }));
              }
              else
              {
                dbObject1 = sessionKeeper.Session.GetObject(selRevId);
                bool readOnly = false;
                dbObject1 = DocumentEditorPlugin.TryCheckOutDocument(dbObject1, ref readOnly);
                if (readOnly)
                  throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_99"));
              }
              documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(dbObject1, 0, false, true, false);
              revType = revisionWizardForm.RT;
            }
          }
          if (revisionWizardForm.abortVersion)
            verCanceled = true;
          if (revisionWizardForm.abortVersion || reqRevisionInfo.reqType == RequireClass.Require && documentECO == null)
            return false;
          if (dbObject1 != null)
          {
            if (documentECO != null)
              goto label_90;
          }
          return true;
        }
      }
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_313"));
      if (reqRevisionInfo.reqType == RequireClass.Require)
        stringBuilder.AppendLine(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_314"));
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_312"), stringBuilder.ToString(), RevHelper.idChangeJournal, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
      if (numArray == null || numArray.Length == 0)
        return reqRevisionInfo.reqType != RequireClass.Require;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        dbObject1 = sessionKeeper.Session.GetObject(numArray[0], false);
        bool readOnly = false;
        dbObject1 = DocumentEditorPlugin.TryCheckOutDocument(dbObject1, ref readOnly);
        if (readOnly)
          throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_99"));
        documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(dbObject1, 0, false, true, false);
        revType = RevType.CJ;
      }
    }
label_90:
    Intermech.ECO.Client.ECO eco = new Intermech.ECO.Client.ECO(documentECO, dbObject1.ObjectID, dbObject1.ObjectGUID, revType);
    IncludeGoal includeGoal = new IncludeGoal();
    includeGoal.BlockCreate = true;
    int schemeId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      schemeId = ECOPlugin.GetSchemeID(sessionKeeper.Session, objIDs);
    List<long> objIDs1 = new List<long>() { objIDs[0] };
    if (!includeGoal.Execute(objIDs1, eco.litera, noDObjs, (List<long>) null, revType, schemaId: schemeId))
      return rr != ReqRevision.ForceRevision;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int num4 = eco.litera != includeGoal.litera ? 1 : 0;
      eco.litera = includeGoal.litera;
      if (includeGoal.goal == ECOGoal.Change)
      {
        if (this.eps.Current.LeaveOTDNumberForChange)
        {
          if (sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService)
          {
            foreach (long objId in objIDs)
            {
              long deliveryListId = customService.GetDeliveryListID(sessionKeeper.Session.SessionGUID, objId);
              if (deliveryListId == 0L)
              {
                customService.CreateDeliveryList(sessionKeeper.Session.SessionGUID, objId);
              }
              else
              {
                IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(deliveryListId, RevHelper.idActualCopyAtt);
                for (int index = 0; index < objectAttributeById.ValuesCount; ++index)
                {
                  objectAttributeById.Index = index;
                  objectAttributeById.Clear();
                }
              }
            }
          }
        }
        else
        {
          foreach (long objId in objIDs)
          {
            IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(objId, RevHelper.idAttrInvNoOTD);
            if (objectAttributeById != null)
              objectAttributeById.Value = (object) DBNull.Value;
          }
        }
      }
      if (revType == RevType.CJ)
      {
        CJEditorForm cjEditorForm = this.CreateCJEditorForm(eco, false, true, true, false);
        IObjectCreatorService service3 = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
        service3.ObjectCreatorCanceledEvent += new ObjectCreatorCanceledEventHandler(this._ObjectCreatorCanceledEvent);
        service3.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this._AfterObjectCreatedEvent);
        this.curCJEditorForm = cjEditorForm;
        this.newObjVerList = new List<long>();
        foreach (long objId in objIDs)
          this.newObjVerList.Add(objId);
        if (this.eps.Current.AutoCheckOut)
        {
          foreach (long objId in objIDs)
          {
            IDBObject dbObject3 = sessionKeeper.Session.GetObject(objId, false);
            if (dbObject3 != null && dbObject3.ObjectModifyMode == ObjectModifyModes.Checkout)
              dbObject3.CheckOut();
          }
        }
        this.curCJEditorForm.AttachItemsToCJ(objIDs[0], sessionKeeper.Session, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, (Hashtable) null);
      }
      else
      {
        if (num1 != 0L)
        {
          eco.linkedContextNo = Math.Abs(num1);
          IDBAttribute dbAttribute = dbObject1.Attributes.AddAttribute(RevHelper.idLinkedContNumber, false);
          if (dbAttribute != null)
            dbAttribute.AsInteger = Math.Abs(eco.linkedContextNo);
        }
        ECOEditorForm ecoEditorForm = this.CreateECOEditorForm(eco, false, true, true, false);
        ecoEditorForm.UpdateDocDesign();
        IObjectCreatorService service4 = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
        service4.ObjectCreatorCanceledEvent += new ObjectCreatorCanceledEventHandler(this._ObjectCreatorCanceledEvent);
        service4.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this._AfterObjectCreatedEvent);
        this.curEditorForm = ecoEditorForm;
        this.newObjVerList = new List<long>();
        foreach (long objId in objIDs)
          this.newObjVerList.Add(objId);
        List<long> allVersions = new List<long>();
        objDescs.ForEach((Action<ObjectCheckOutVersionDescription>) (x => allVersions.Add(x.F_OBJECT_ID)));
        ecoEditorForm.NewAttachItems(objIDs, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, includeGoal.separateChanges, hideTypes, allVersions: allVersions);
        eco.newVers.AddRange((IEnumerable<long>) objIDs);
      }
    }
    return true;
  }

  private void _ObjectCreatorCanceledEvent(object sender, ObjectCreatorCanceledEventArgs ea)
  {
    if (this.curEditorForm != null && ea.CreatedZagId != 0L)
      this.curEditorForm.DeleteLinksToCanceledVersions(this.newObjVerList);
    if (this.curCJEditorForm != null && ea.CreatedZagId != 0L)
      this.curCJEditorForm.DeleteLinksToCanceledVersions(this.newObjVerList);
    IObjectCreatorService service = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
    service.ObjectCreatorCanceledEvent -= new ObjectCreatorCanceledEventHandler(this._ObjectCreatorCanceledEvent);
    service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this._AfterObjectCreatedEvent);
    this.curEditorForm = (ECOEditorForm) null;
    this.curCJEditorForm = (CJEditorForm) null;
    this.newObjVerList = (List<long>) null;
  }

  private void _AfterObjectCreatedEvent(object sender, AfterObjectCreatedEventArgs ea)
  {
    IObjectCreatorService service = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
    service.ObjectCreatorCanceledEvent -= new ObjectCreatorCanceledEventHandler(this._ObjectCreatorCanceledEvent);
    service.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this._AfterObjectCreatedEvent);
    if (this.curEditorForm != null)
      this.curEditorForm._AddNewPendingLinks();
    this.curEditorForm = (ECOEditorForm) null;
    this.curCJEditorForm = (CJEditorForm) null;
    this.newObjVerList = (List<long>) null;
    bool nonActiveWindows = UISettings.AutoupdateNonActiveWindows;
    UISettings.AutoupdateNonActiveWindows = true;
    try
    {
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", ea.ObjectID));
    }
    finally
    {
      UISettings.AutoupdateNonActiveWindows = nonActiveWindows;
    }
  }

  public static DockManager DockManager => ECOPlugin.dockManager;

  private static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    bool beginGroup,
    ICommandManager commandManager)
  {
    MenuButtonItem menuItem = new MenuButtonItem(commandCaption);
    menuItem.CommandName = commandName;
    menuItem.ToolTipText = commandHint;
    menuItem.BeginGroup = beginGroup;
    commandManager?.Add((ButtonItemBase) menuItem);
    return menuItem;
  }

  public static ECOPlugin FindPlugin()
  {
    if (ECOPlugin.plugin != null)
      return ECOPlugin.plugin;
    ECOPlugin plugin1 = (ECOPlugin) null;
    IPluginManager service = (IPluginManager) ServicesManager.GetService(typeof (IPluginManager));
    if (service != null)
    {
      foreach (IPlugin plugin2 in (IEnumerable<IPlugin>) service.Plugins)
      {
        foreach (IPackage package in (IEnumerable<IPackage>) plugin2.Packages)
        {
          if (package is ECOPlugin)
          {
            plugin1 = (ECOPlugin) package;
            break;
          }
        }
        if (plugin1 != null)
          break;
      }
    }
    ECOPlugin.plugin = plugin1;
    return plugin1;
  }

  public bool InvokeCommandForObject(long objectID, string Command)
  {
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objectID);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2);
    if (!commandsTable.Contains(Command))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(Command, commandsTable, (System.IServiceProvider) viewServices1);
    return true;
  }

  public bool GoalAllowed(long objId, ECOGoal goal, ref long linkContNum)
  {
    List<ECOGoal> ecoGoalList = new List<ECOGoal>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objId);
        string str1 = dbObject != null ? dbObject.Caption : throw new Exception($"{Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_212")}{Convert.ToString(objId)}]");
        if (this.allowedTypes.IndexOf(dbObject.TypeID) < 0)
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.TypeID);
          throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_213"), (object) objectType.ObjectTypeName));
        }
        IDBAttribute attributeById1 = dbObject.GetAttributeByID(RevHelper.idLinkedContNumber);
        linkContNum = attributeById1 != null ? Convert.ToInt64(attributeById1.Value) : -1L;
        int num = ECOPlugin.plugin.eps.Current.ReplaceEmptyDesignByTemplate ? 1 : 0;
        bool flag = ECOPlugin.plugin.eps.Current.InvNumAttr != "";
        if (num == 0 || !flag)
        {
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(DocIDCache.Attr_Designation);
          string str2 = "";
          if (attributeById2 != null)
            str2 = attributeById2.Description;
          if (str2.Trim() == "")
            throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_217"), (object) str1));
        }
        if (goal == ECOGoal.Annul)
        {
          IUserSession session = sessionKeeper.Session;
          IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
          DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
          {
            (object) -21,
            (object) -20
          });
          relationCollection.LocalTypesMode = true;
          DataTable dataTable = relationCollection.EntersInVersion(paramSet, dbObject.ObjectID);
          if (dataTable.Rows.Count != 0)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64_1 = Convert.ToInt64(row[0]);
              long int64_2 = Convert.ToInt64(row[1]);
              int objectTypeId1 = session.GetObjectInfo(int64_1).ObjectTypeID;
              int objectTypeId2 = session.GetObjectInfo(dbObject.ObjectID).ObjectTypeID;
              int relationType4PrjLinkId = MetaDataHelper.GetRelationType4PrjLinkID(session, int64_2);
              IMSRelationType relationType = MetaDataHelper.GetRelationType(relationType4PrjLinkId);
              if (relationType != null && (relationType.Options & RelationTypeOptions.EnableCheckAnnulment) != RelationTypeOptions.None && !ECOPlugin.IsSynchroMove(objectTypeId1, objectTypeId2, relationType4PrjLinkId))
                throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_219"), (object) str1));
            }
          }
        }
      }
      catch
      {
        throw;
      }
    }
    return true;
  }

  public static int GetSchemeID(IUserSession ius, List<long> objIDs)
  {
    int schemeId = -1;
    foreach (long objId in objIDs)
    {
      IDBObject dbObject = ius.GetObject(objId, false);
      if (dbObject == null)
      {
        dbObject = ius.GetObject(Math.Abs(objId), false);
        if (dbObject == null)
          return -1;
      }
      IDBObjectType objectType = ius.GetObjectType(dbObject.ObjectType);
      if (objectType == null)
        return -1;
      if (schemeId == -1)
        schemeId = objectType.SchemaID;
      else if (schemeId != objectType.SchemaID)
        return -1;
    }
    return schemeId;
  }

  public static int GetSchemeID(List<ObjectCheckOutVersionDescription> objDescs)
  {
    int schemeId = -1;
    foreach (ObjectCheckOutVersionDescription objDesc in objDescs)
    {
      if (schemeId == -1)
        schemeId = objDesc.F_LCSCHEMA_ID;
      else if (schemeId != objDesc.F_LCSCHEMA_ID)
        return -1;
    }
    return schemeId;
  }

  public static bool IsAnnulStep(IUserSession ius, int stepId)
  {
    IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep(stepId);
    if (lifecycleStep == null)
      return false;
    IDBLifecycleLevelType lifecycleLevel = ius.GetLifecycleLevel(lifecycleStep.LevelID);
    return lifecycleLevel != null && lifecycleLevel.LevelID == ius.IdentHelper.AnnulmentLevelID;
  }

  public static int GetAnnulStepForObjects(IUserSession ius, List<long> objIDs)
  {
    int schemeId = ECOPlugin.GetSchemeID(ius, objIDs);
    if (schemeId <= 0)
      return -1;
    int annulmentLevelId = ius.IdentHelper.AnnulmentLevelID;
    foreach (DataRow row in (InternalDataCollectionBase) ius.GetLCSchema(schemeId).GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"].Rows)
    {
      if (Convert.ToInt32(row["F_LEVEL_ID"]) == annulmentLevelId)
        return Convert.ToInt32(row["F_LC_STEP"]);
    }
    return -1;
  }

  public static bool IsSynchroMove(int parentObjType, int childObjType, int relType)
  {
    IMSApplicability applicability = MetaDataHelper.GetApplicability(parentObjType, childObjType, relType);
    return applicability != null && (applicability.Options & ApplicabilityOptions.ChangeLCStep) != 0;
  }

  public bool AllowLitera(long objId)
  {
    int childTypeID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objId, false);
      if (dbObject != null)
        childTypeID = dbObject.ObjectType;
    }
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(childTypeID);
    if (!objectTypeParentsId.Contains(childTypeID))
      objectTypeParentsId.Add(childTypeID);
    return objectTypeParentsId.Contains(ECOPlugin.plugin.idOTSpecification) || objectTypeParentsId.Contains(ECOPlugin.plugin.idAssemblyUnit) || objectTypeParentsId.Contains(ECOPlugin.plugin.idPart) || objectTypeParentsId.Contains(ECOPlugin.plugin.idOTComplex) || objectTypeParentsId.Contains(ECOPlugin.plugin.idOTComplect);
  }

  public static bool GetSynchroParents(long objId, List<long> synchroList)
  {
    bool synchroParents = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
      relationCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -21,
        (object) -20,
        (object) -7
      });
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, objId);
      if (dataTable.Rows.Count != 0)
      {
        int objectTypeId = session.GetObjectInfo(objId).ObjectTypeID;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64_1 = Convert.ToInt64(row[0]);
          long int64_2 = Convert.ToInt64(row[1]);
          int int32 = Convert.ToInt32(row[2]);
          if (!ECOPlugin.plugin.IzvTypes.Contains(int32))
          {
            int relationType4PrjLinkId = MetaDataHelper.GetRelationType4PrjLinkID(session, int64_2);
            if (ECOPlugin.IsSynchroMove(int32, objectTypeId, relationType4PrjLinkId))
            {
              if (!synchroList.Contains(int64_1))
                synchroList.Add(int64_1);
              synchroParents = true;
            }
          }
        }
      }
    }
    return synchroParents;
  }

  public static bool ObjTypeHasSeriesDates(int objTypeID)
  {
    return MetaDataHelper.GetAttribute4ObjectType(MetaDataHelper.GetObjectTypeGuid(objTypeID), ECOPlugin.attrSeriesDatesGuid) != null;
  }

  public static bool ObjTypeHasSeriesDates(Guid objTypeGuid)
  {
    return MetaDataHelper.GetAttribute4ObjectType(objTypeGuid, ECOPlugin.attrSeriesDatesGuid) != null;
  }

  public static bool EnabledSeriesDates()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.EnabledSeriesDates;
  }

  public static void SendByProcess(
    IUserSession ius,
    long templateId,
    IEnumerable<long> objList,
    string Subject = null,
    string Message = null,
    List<int> fileIndexes = null)
  {
    IProcess process = (ius.GetCustomService(typeof (IRouterService)) as IRouterService).CreateProcess(ius.SessionGUID, templateId);
    if (process.StartActivity == null)
      throw new Exception("Start activity not found!");
    foreach (long objectid in objList)
      process.StartActivity.Attachments.Add(objectid);
    if (Subject != null)
    {
      IVariable variable = process.StartActivity.Variables.Find("SUBJECT");
      if (variable != null)
        variable.Value = Subject;
    }
    if (Message != null)
    {
      IVariable variable = process.StartActivity.Variables.Find("MESSAGE");
      if (variable != null)
        variable.Value = Message;
    }
    if (fileIndexes != null && fileIndexes.Count > 0)
    {
      IVariable variable = process.StartActivity.Variables.Find("ATTACHMENT_FILEINDEXES");
      if (variable != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < fileIndexes.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(";");
          stringBuilder.Append(Convert.ToString(fileIndexes[index]));
        }
        variable.Value = stringBuilder.ToString();
      }
    }
    process.StartProcess();
  }

  public static long GetActualRevisionVersion(IUserSession ius, long revId)
  {
    DataTable allObjectVersions = ius.GetAllObjectVersions(revId, false, false, false, "F_OBJECT_ID", "F_VERSION_ID", "F_LC_STEP");
    long actualRevisionVersion = 0;
    int num = -1;
    for (int index = 0; index < allObjectVersions.Rows.Count - 1; ++index)
    {
      DataRow row = allObjectVersions.Rows[index];
      if (Convert.ToInt32(row[2]) == RevHelper.idLC_Actualize)
        return Convert.ToInt64(row[0]);
      int int32 = Convert.ToInt32(row[1]);
      if (int32 > num)
      {
        num = int32;
        actualRevisionVersion = Convert.ToInt64(row[0]);
      }
    }
    return actualRevisionVersion;
  }

  public void UpdateAllowedTypes()
  {
    if (this._allowedTypes == null)
      this._allowedTypes = new List<int>();
    this._UpdateAllowedTypes(RevHelper.idObjRevision, this._allowedTypes);
  }

  public List<int> _UpdateAllowedTypes(int revType, List<int> result = null)
  {
    if (result == null)
      result = new List<int>();
    else
      result.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(-2, true).Select("F_OBJECT_TYPE");
      HashSet<long> longSet = new HashSet<long>();
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          object obj = row["F_OBJECT_TYPE"];
          if (obj != null && obj != DBNull.Value)
          {
            long int64 = Convert.ToInt64(obj);
            longSet.Add(int64);
          }
        }
      }
      foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(revType, RevHelper.idLinkRevision)))
      {
        if (MetaDataHelper.GetObjectType(num).VersionsMode != ObjectVersionModes.SingleVersion && MetaDataHelper.GetApplicability(revType, num, RevHelper.idLinkRevision).ApplicabilityMode != ApplicabilityModes.Disabled && !result.Contains(num) && longSet.Contains((long) num))
          result.Add(num);
      }
    }
    if (this.AllowedDict == null)
      this.AllowedDict = new Dictionary<int, List<int>>();
    if (this.AllowedDict.ContainsKey(revType))
      this.AllowedDict[revType] = result;
    else
      this.AllowedDict.Add(revType, result);
    return result;
  }

  public List<int> GetAllowedTypes(int revType)
  {
    return this.AllowedDict != null && this.AllowedDict.ContainsKey(revType) ? this.AllowedDict[revType] : this._UpdateAllowedTypes(revType);
  }

  public static void GetSchemeData(int schemaID, out DataTable dt1, out DataTable dt2)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataSet schema = sessionKeeper.Session.GetLCSchema(schemaID).GetStepsCollection().GetSchema();
      dt1 = schema.Tables["IMS_LC_STEPS"];
      dt2 = ((IClientSession) sessionKeeper.Session).ClientCache.GetTable("IMS_LEVELS");
    }
  }

  public static int GetFutureLCStepId(IUserSession ius, int lcStepId)
  {
    IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep(lcStepId);
    string str1 = "cad003c4-306c-11d8-b4e9-00304f19f545";
    string str2 = "cadd970b-306c-11d8-b4e9-00304f19f545";
    if (!lifecycleStep.Properties.StepGuid.ToString().Equals(str2))
      return lcStepId;
    DataTable dt1;
    ECOPlugin.GetSchemeData(lifecycleStep.SchemaID, out dt1, out DataTable _);
    int futureLcStepId = -1;
    DataRow[] dataRowArray1 = dt1.Select($"F_GUID = '{str1}'");
    if (dataRowArray1 != null && dataRowArray1.Length != 0)
    {
      futureLcStepId = Convert.ToInt32(dataRowArray1[0]["F_LC_STEP"]);
    }
    else
    {
      DataRow[] dataRowArray2 = dt1.Select("F_LEVEL_ID = " + Convert.ToString(RevHelper.idLevelManufacturing));
      if (dataRowArray2 != null)
      {
        foreach (DataRow dataRow in dataRowArray2)
        {
          int int32 = Convert.ToInt32(dataRow["F_LC_STEP"]);
          if (int32 != lcStepId)
          {
            futureLcStepId = int32;
            break;
          }
        }
      }
    }
    return futureLcStepId;
  }

  public static bool RemoveDefaultText(TableElement change)
  {
    if (change.Nodes.Count <= 1 || !(change.Nodes[1].Template.Id == Intermech.ECO.Client.ECO.fldVar1))
      return false;
    change.RemoveChildNodeAt(1, false, false);
    return true;
  }

  internal void FillRazoslatAfterLoadDoc(object sender, AfterLoadDocumentEventHandlerArgs e)
  {
    if (!this.IzvTypes.Contains(e.DocumentTypeID))
      return;
    ImDocument document = e.Document;
    if (document == null)
      return;
    TextData textData = (TextData) null;
    DocumentTreeNode templateRecursive = document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSendTo);
    if (templateRecursive != null && templateRecursive is TextData)
      textData = templateRecursive as TextData;
    if (textData == null)
      return;
    List<ECOPlugin.ECOInfo> ecoInfoList = ECOPlugin.LoadECOStructure(e.DocumentID, false);
    long objId = 0;
    foreach (ECOPlugin.ECOInfo ecoInfo in ecoInfoList)
    {
      long sendList = RevHelper.GetSendList(ecoInfo.ID);
      if (sendList != 0L)
      {
        if (objId == 0L)
          objId = sendList;
        if (sendList != objId)
        {
          objId = 0L;
          break;
        }
      }
    }
    if (objId == 0L)
      return;
    List<string> abonList = Intermech.ECO.Client.ECO.GetAbonList(objId);
    if (abonList == null)
      return;
    string str = ECOPlugin.FormatAbonents(abonList);
    if (!(str != textData.Text))
      return;
    textData.AssignText(str, false, true, true);
  }

  internal static string FormatAbonents(List<string> abonList)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < abonList.Count; ++index)
    {
      stringBuilder.Append(abonList[index]);
      if (index < abonList.Count - 1)
        stringBuilder.Append(", ");
    }
    return stringBuilder.ToString();
  }

  internal void IssueDI(long II_ID) => this.CreateDIorDPI(false, II_ID);

  internal void IssueDPI(long PI_ID) => this.CreateDIorDPI(true, PI_ID);

  public long GetDopIzvId(IUserSession ius, long ecoID)
  {
    IDBRelationCollection relationCollection = ius.GetRelationCollection(RevHelper.idLinkFromDI);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, ecoID);
    return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
  }

  internal long CreateDIorDPI(bool DPI, long baseECO_ID)
  {
    long diorDpi = 0;
    int num1 = 0;
    long objectID = 0;
    string empty = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (DPI)
      {
        long annulingRevision = ECO_PICommands.GetAnnulingRevision(baseECO_ID);
        if (annulingRevision != 0L)
        {
          int num2 = (int) MessageBox.Show(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_366"), (object) baseECO_ID) + string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_367"), (object) annulingRevision), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_117"), MessageBoxButtons.OK);
          return 0;
        }
        IDBObject piObj = sessionKeeper.Session.GetObject(baseECO_ID, false);
        if (piObj == null)
          return 0;
        if (ECO_PICommands.IsLevelForbidden(piObj))
        {
          int num3 = (int) MessageBox.Show(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_366"), (object) baseECO_ID) + Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_369"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_117"), MessageBoxButtons.OK);
          return 0;
        }
      }
      empty = Convert.ToString(sessionKeeper.Session.GetObject(baseECO_ID, false).GetAttributeByID(RevHelper.idAttrDesign).Value);
      long dopIzvId = this.GetDopIzvId(sessionKeeper.Session, baseECO_ID);
      if (dopIzvId != 0L)
      {
        DataTable allObjectVersions = sessionKeeper.Session.GetAllObjectVersions(dopIzvId, false, false, false, "F_OBJECT_ID", "F_VERSION_ID");
        int num4 = -1;
        foreach (DataRow row in (InternalDataCollectionBase) allObjectVersions.Rows)
        {
          int int32 = Convert.ToInt32(row[1]);
          if (int32 > num4)
          {
            num4 = int32;
            objectID = Convert.ToInt64(row[0]);
          }
        }
        num1 = num4 + 1;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBTransactions customService = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      try
      {
        customService?.StartTransaction();
        string oldValue = DPI ? " ПИ" : " ИИ";
        string newValue = DPI ? " ДПИ" : " ДИ";
        if (num1 > 0)
          newValue = !ECOPlugin.plugin.eps.Current.NoSlashInDPIDesign ? $"{newValue}/{Convert.ToString(num1)}" : newValue + Convert.ToString(num1);
        string str = !empty.Contains(oldValue) ? empty + newValue : empty.Replace(oldValue, newValue);
        IDBObjectCollection objectCollection = session.GetObjectCollection(DPI ? RevHelper.idObj_DPI : RevHelper.idObj_DI);
        bool flag = objectID != 0L;
        IDBObject dbObject = flag ? objectCollection.CreateVersion(objectID) : objectCollection.Create();
        AttributeValues[] valuesList = new AttributeValues[2]
        {
          new AttributeValues(RevHelper.idLinkedContNumber, (object) Math.Abs(baseECO_ID)),
          new AttributeValues(RevHelper.idAttrDesign, (object) str)
        };
        dbObject.SetAttributesValues(valuesList);
        diorDpi = dbObject.ObjectID;
        ImDocument document = DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObject(baseECO_ID, false), 0, false, true, false);
        if (document != null)
        {
          if (document.FindNode(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_19")) is TableElement node)
          {
            node.UniteTable();
            node.Clear(false, false);
            document.UpdateLayout(0, true, false);
          }
          int[] numArray = new int[18]
          {
            1,
            2,
            101,
            102,
            103,
            104,
            106,
            107,
            108,
            109,
            110,
            111,
            112 /*0x70*/,
            121,
            122,
            123,
            501,
            502
          };
          foreach (int num5 in numArray)
          {
            string nodeTemplateId = "I" + Convert.ToString(num5);
            if (document.FindFirstNodeFromTemplate_Recursive(nodeTemplateId) is TextData templateRecursive)
            {
              templateRecursive.AssignReferenceToTextSource((ReferenceBase) null, true, false, false);
              if (num5 == 122)
              {
                templateRecursive.Text = str;
                templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, str);
              }
              else
              {
                templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, templateRecursive.Text);
                templateRecursive.Text = "";
              }
            }
          }
          DocumentEditorPlugin.SaveImDocumentObjectFile(diorDpi, document, document.Name, 0, true);
        }
        dbObject.CommitCreation(true);
        diorDpi = dbObject.ObjectID;
        if (!flag)
        {
          IDBRelationCollection relationCollection = session.GetRelationCollection(RevHelper.idLinkFromDI);
          try
          {
            relationCollection.Create(dbObject.ObjectID, baseECO_ID);
          }
          catch
          {
          }
        }
        customService?.Commit();
      }
      catch
      {
        customService?.Rollback();
        throw;
      }
      if (diorDpi != 0L)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(diorDpi, false);
        if (dbObject != null)
        {
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
            diorDpi = dbObject.CheckOut().ObjectID;
          this.OpenDIEditorForObject(diorDpi, false, true, true, true);
        }
      }
    }
    return diorDpi;
  }

  private static string GetListString(List<long> idList)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (long id in idList)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(Convert.ToString(id));
    }
    return stringBuilder.ToString();
  }

  public static long ReplaceCJRecord(List<long> cjRecList, out string revDesign)
  {
    revDesign = string.Empty;
    List<long> idList1 = new List<long>();
    List<long> idList2 = new List<long>();
    List<long> idList3 = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = cjRecList.Count - 1; index >= 0; --index)
      {
        long cjRec = cjRecList[index];
        IDBObject dbObject = sessionKeeper.Session.GetObject(cjRec, false);
        if (dbObject == null)
        {
          idList1.Add(cjRec);
          cjRecList.RemoveAt(index);
        }
        else if (dbObject.LCStep != RevHelper.idStepWaitingForII)
        {
          idList2.Add(cjRec);
          cjRecList.RemoveAt(index);
        }
        else
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrReplacedByECO);
          if (attributeById != null && attributeById.Value != DBNull.Value)
          {
            idList3.Add(cjRec);
            cjRecList.RemoveAt(index);
          }
        }
      }
    }
    if (idList1.Count > 0 || idList2.Count > 0 || idList3.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (idList1.Count > 0)
        stringBuilder.AppendLine(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_315"), (object) ECOPlugin.GetListString(idList1)));
      if (idList2.Count > 0)
        stringBuilder.AppendLine(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_319"), (object) ECOPlugin.GetListString(idList2)));
      if (idList3.Count > 0)
        stringBuilder.AppendLine(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_320"), (object) ECOPlugin.GetListString(idList3)));
      int num = (int) MessageBox.Show(stringBuilder.ToString(), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OK);
      if (cjRecList.Count == 0)
        return 0;
    }
    List<long> objIds = new List<long>();
    List<ECOGoal> ecoGoalList = new List<ECOGoal>();
    List<int> intList = new List<int>();
    string objCaption = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[4]
      {
        new ColumnDescriptor((object) RevHelper.idAttrVerId, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
        new ColumnDescriptor((object) RevHelper.idAttrIncludeGoal, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
        new ColumnDescriptor((object) RevHelper.idAttrFutureLC, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
      };
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
      relationCollection.LocalTypesMode = true;
      foreach (long cjRec in cjRecList)
      {
        DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, columns), cjRec);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long num1 = Convert.ToInt64(row[1]);
            if (row[0] != DBNull.Value)
            {
              long int64 = Convert.ToInt64(row[0]);
              if (int64 != 0L)
                num1 = int64;
            }
            objIds.Add(num1);
            ECOGoal ecoGoal = row[2] != DBNull.Value ? (ECOGoal) Convert.ToInt32(row[2]) : ECOGoal.Change;
            ecoGoalList.Add(ecoGoal);
            int num2 = row[3] != DBNull.Value ? Convert.ToInt32(row[3]) : -1;
            intList.Add(num2);
          }
        }
      }
      if (objIds.Count > 0)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objIds[0]);
        if (!objectInfo.Empty)
          objCaption = objectInfo.Caption;
      }
    }
    RevisionWizardForm revisionWizardForm = new RevisionWizardForm(RevHelper.idObj_II, RequireClass.NoRequire, true, objIds, ECOGoal.Stamp, objCaption, true);
    ECOPlugin.BlockECOOpening = true;
    try
    {
      if (revisionWizardForm.ShowDialog() != DialogResult.OK)
        return 0;
      if (revisionWizardForm.SelRevId == -1L)
      {
        if (revisionWizardForm.ECOObjectID == -1L)
          return 0;
      }
    }
    finally
    {
      ECOPlugin.BlockECOOpening = false;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService)
        customService.StartTransaction();
      try
      {
        bool flag = revisionWizardForm.SelRevId != -1L;
        long num3 = flag ? revisionWizardForm.SelRevId : revisionWizardForm.ECOObjectID;
        IDBObject docObject = session.GetObject(num3);
        IDBAttribute byId = docObject.Attributes.FindByID(RevHelper.idAttrDesign);
        if (byId != null)
          revDesign = Convert.ToString(byId.Value);
        INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
        if (service != null)
        {
          if (!flag)
            service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", docObject.ObjectID));
          if (docObject.ObjectModifyMode == ObjectModifyModes.Checkout)
          {
            long objectId = docObject.ObjectID;
            docObject = docObject.CheckOut();
            service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
            {
              objectId
            }, (IList<long>) new long[1]
            {
              docObject.ObjectID
            }));
          }
        }
        ECOEditorForm ecoEditorForm = ECOPlugin.plugin.GetOpenedECOEditor(docObject.ObjectID, false);
        Intermech.ECO.Client.ECO eco;
        if (ecoEditorForm == null)
        {
          eco = new Intermech.ECO.Client.ECO(revisionWizardForm.DocumentECO ?? DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, 0, false, true, false), num3, docObject.ObjectGUID, revisionWizardForm.RT);
          ecoEditorForm = ECOPlugin.plugin.CreateECOEditorForm(eco, false, true, true, true);
        }
        else
          eco = ecoEditorForm.ECO;
        try
        {
          List<long> parts = new List<long>();
          Dictionary<long, long> dictionary = new Dictionary<long, long>();
          for (int index1 = 0; index1 < objIds.Count; ++index1)
          {
            long num4 = objIds[index1];
            IDBObject dbObject1 = session.GetObject(num4, false);
            if (dbObject1 != null)
            {
              IDBObjectCollection objectCollection = session.GetObjectCollection(dbObject1.ObjectType);
              IDBObject dbObject2 = (IDBObject) null;
              if (dictionary.ContainsKey(Math.Abs(num4)))
              {
                long objectID = dictionary[Math.Abs(num4)];
                dbObject2 = session.GetObject(objectID, false) ?? session.GetObject(num4);
              }
              else
              {
                ECOPlugin.includingObjIds.Add(dbObject1.ID);
                CreateVersionResult versionInternal = (objectCollection as IClientDBObjectCollection).CreateVersionInternal(num4);
                try
                {
                  for (int index2 = 0; index2 < versionInternal.SourceVersions.Count; ++index2)
                  {
                    long key = Math.Abs(versionInternal.SourceVersions[index2].F_OBJECT_ID);
                    if (key != Math.Abs(num4) && !dictionary.ContainsKey(key))
                      dictionary.Add(key, versionInternal.TargetVersions[index2].F_OBJECT_ID);
                  }
                  versionInternal.NewObjectVersion.CommitCreation(true);
                  versionInternal.Commit(session);
                  long objectId = versionInternal.NewObjectVersion.ObjectID;
                  dbObject2 = session.GetObject(objectId);
                }
                catch
                {
                  versionInternal.Rollback(session);
                  throw;
                }
                finally
                {
                  ECOPlugin.includingObjIds.Remove(dbObject1.ID);
                }
              }
              string forceChangeNo = "";
              IDBAttribute attributeById = dbObject1.GetAttributeByID(RevHelper.idAttrChangeNo);
              if (attributeById != null && attributeById.Value != DBNull.Value)
              {
                forceChangeNo = Convert.ToString(attributeById.Value);
                string str = Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_316");
                if (forceChangeNo.EndsWith(str))
                  forceChangeNo = forceChangeNo.Substring(0, forceChangeNo.Length - str.Length);
              }
              int schemeId = 0;
              int num5 = intList[index1];
              if (num5 != -1)
              {
                IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(num5, false);
                if (lifecycleStep == null)
                  num5 = -1;
                else
                  schemeId = lifecycleStep.SchemaID;
              }
              parts.Clear();
              parts.Add(dbObject2.ObjectID);
              AttributeValues[] valuesList = new AttributeValues[1]
              {
                new AttributeValues(RevHelper.idAttrChangesGroupNum, (object) eco.linkedContextNo)
              };
              dbObject2.SetAttributesValues(valuesList);
              ecoEditorForm.NewAttachItems(parts, ecoGoalList[index1], schemeId, num5, false, forceChangeNo: forceChangeNo);
            }
          }
        }
        finally
        {
          ecoEditorForm.Document.UpdateLayout(0, true, true);
        }
        foreach (long cjRec in cjRecList)
        {
          IDBObject dbObject = session.GetObject(cjRec);
          dbObject.GetAttributeByID(RevHelper.idAttrReplacedByECO).AsInteger = num3;
          dbObject.LCStep = RevHelper.idStepKeeping;
        }
        customService?.Commit();
        return docObject.ObjectID;
      }
      catch
      {
        customService?.Rollback();
        throw;
      }
    }
  }

  public static long CreateCJRec(long cjID, long objectID)
  {
    if (cjID == 0L)
    {
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_312"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_313"), RevHelper.idChangeJournal, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
      if (numArray != null && numArray.Length != 0)
        cjID = numArray[0];
    }
    if (objectID == 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(cjID, false);
        if (dbObject == null)
          return 0;
        objectID = CJEditorForm.AskForObject(dbObject.NameInMessages);
      }
    }
    if (cjID == 0L || objectID == 0L)
      return 0;
    CJEditorForm cjEditorForm = ECOPlugin.FindPlugin().OpenCJEditorForObject(cjID, false, true, true, true);
    if (cjEditorForm == null)
      return 0;
    IncludeGoal includeGoal = new IncludeGoal();
    if (!includeGoal.ExecuteForCJ(objectID, cjEditorForm.ECO.litera))
      return 0;
    objectID = includeGoal.GetFinalObject();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return cjEditorForm.AttachItemsToCJ(objectID, sessionKeeper.Session, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, (Hashtable) null);
  }

  internal static bool HasTerm(IDBObject revObj)
  {
    IDBAttribute attributeById1 = revObj.GetAttributeByID(RevHelper.idAttrChangeDateStart);
    if (attributeById1 != null && attributeById1.Value != null && attributeById1.Value != DBNull.Value)
      return true;
    IDBAttribute attributeById2 = revObj.GetAttributeByID(RevHelper.idAttrChangeDateEnd);
    return attributeById2 != null && attributeById2.Value != null && attributeById2.Value != DBNull.Value;
  }

  private void SetIntegratorActions(Guid objType, bool set, IUserSession session)
  {
    ILaunchActionServer service = ServiceUtils.GetService<ILaunchActionServer>((object) session, true);
    ILaunchHandler handler = ClientContext.LaunchActions.GetHandler(RevIntegrator.IntegratorId, false);
    if (handler == null)
      return;
    if (set)
    {
      string serverObjectTemplate = handler.GetServerObjectTemplate();
      if (this.GetInfo(service, objType, LaunchType.Edit) == null)
      {
        LaunchActionInfo action = service.CreateAction(objType, (ITarget) AllUsersTarget.Value, LaunchType.Edit, handler.Id, serverObjectTemplate);
        service.SetDefaultAction(objType, (ITarget) AllUsersTarget.Value, action.ActionId);
      }
      if (this.GetInfo(service, objType, LaunchType.Print) == null)
      {
        LaunchActionInfo action = service.CreateAction(objType, (ITarget) AllUsersTarget.Value, LaunchType.Print, handler.Id, serverObjectTemplate);
        service.SetDefaultAction(objType, (ITarget) AllUsersTarget.Value, action.ActionId);
      }
      if (this.GetInfo(service, objType, LaunchType.View) != null)
        return;
      LaunchActionInfo action1 = service.CreateAction(objType, (ITarget) AllUsersTarget.Value, LaunchType.View, handler.Id, serverObjectTemplate);
      service.SetDefaultAction(objType, (ITarget) AllUsersTarget.Value, action1.ActionId);
    }
    else
    {
      LaunchActionInfo info1 = this.GetInfo(service, objType, LaunchType.Edit);
      if (info1 != null)
        service.RemoveAction(info1.ActionId);
      LaunchActionInfo info2 = this.GetInfo(service, objType, LaunchType.Print);
      if (info2 != null)
        service.RemoveAction(info2.ActionId);
      LaunchActionInfo info3 = this.GetInfo(service, objType, LaunchType.View);
      if (info3 == null)
        return;
      service.RemoveAction(info3.ActionId);
    }
  }

  private LaunchActionInfo GetInfo(
    ILaunchActionServer launchActions,
    Guid objType,
    LaunchType type)
  {
    foreach (LaunchActionInfo action in launchActions.GetActionList(objType, (ITarget) AllUsersTarget.Value, type))
    {
      if (action.HandlerId == RevIntegrator.IntegratorId)
        return action;
    }
    return (LaunchActionInfo) null;
  }

  public static long TryActivateContext(long ECO_ObjectId)
  {
    ICurrentUserAndRole service = ServicesManager.GetService<ICurrentUserAndRole>();
    long editingContextId = service != null ? service.EditingContextID : 0L;
    if (service != null && editingContextId != ECO_ObjectId)
      service.EditingContextID = ECO_ObjectId;
    return editingContextId;
  }

  public static List<ECOPlugin.ECOInfo> LoadECOStructure(long ECO_ObjectId, bool restoreContext = true)
  {
    long ECO_ObjectId1 = ECOPlugin.TryActivateContext(ECO_ObjectId);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
        relationCollection.LocalTypesMode = true;
        return relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[13]
        {
          ImDataHelper.MakeDescriptor((object) ObligatoryObjectAttributes.F_PRJ_GUID),
          ImDataHelper.MakeDescriptor((object) ObligatoryObjectAttributes.F_PART_ID),
          ImDataHelper.MakeDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
          ImDataHelper.MakeDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrHiding),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrFlags),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrVerId),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrChangeNo),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrFutureLC),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrDelWhenExcluded),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrMainObjectGuid),
          ImDataHelper.MakeDescriptor((object) RevHelper.idAttrIncludeGoal),
          ImDataHelper.MakeDescriptor((object) ObligatoryObjectAttributes.F_ID)
        }), ECO_ObjectId).AsEnumerable().Select<DataRow, ECOPlugin.ECOInfo>((System.Func<DataRow, ECOPlugin.ECOInfo>) (row => new ECOPlugin.ECOInfo()
        {
          RelGuid = row.GetField<Guid>((object) ObligatoryObjectAttributes.F_PRJ_GUID),
          PartId = row.GetField<long>((object) ObligatoryObjectAttributes.F_PART_ID),
          ObjectID = row.GetField<long>((object) ObligatoryObjectAttributes.F_OBJECT_ID),
          ProjID = row.GetField<long>((object) ObligatoryObjectAttributes.F_PROJ_ID),
          hideType = row.GetField<HidingType?>((object) RevHelper.idAttrHiding),
          flags = row.GetField<long?>((object) RevHelper.idAttrFlags),
          newVerId = row.GetField<long?>((object) RevHelper.idAttrVerId),
          changeNo = row.GetField<string>((object) RevHelper.idAttrChangeNo),
          futureStepId = row.GetField<int?>((object) RevHelper.idAttrFutureLC),
          needDelete = row.GetField<bool>((object) RevHelper.idAttrDelWhenExcluded),
          mainVerGuid = row.GetField<string>((object) RevHelper.idAttrMainObjectGuid),
          goal = row.GetField<ECOGoal?>((object) RevHelper.idAttrIncludeGoal),
          ID = row.GetField<long>((object) ObligatoryObjectAttributes.F_ID)
        })).ToList<ECOPlugin.ECOInfo>();
      }
    }
    finally
    {
      if (restoreContext)
        ECOPlugin.TryActivateContext(ECO_ObjectId1);
    }
  }

  public static int GetECO_ObjectsCount(long objId)
  {
    return ECOPlugin.LoadECOStructure(objId).Count<ECOPlugin.ECOInfo>((System.Func<ECOPlugin.ECOInfo, bool>) (info =>
    {
      HidingType? hideType = info.hideType;
      HidingType hidingType = HidingType.Hidden;
      if (hideType.GetValueOrDefault() == hidingType & hideType.HasValue)
        return false;
      ECOGoal? goal = info.goal;
      ECOGoal ecoGoal = ECOGoal.Litera;
      return !(goal.GetValueOrDefault() == ecoGoal & goal.HasValue);
    }));
  }

  public static bool ValidateExcessDocuments(int currDocs)
  {
    int maxDocsAllowed = ECOPlugin.plugin.eps.Current.MaxDocsAllowed;
    if (maxDocsAllowed == 0)
      return true;
    int num1 = currDocs < maxDocsAllowed ? 1 : 0;
    if (num1 != 0)
      return num1 != 0;
    int num2 = (int) MessageBox.Show(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_431"), (object) maxDocsAllowed), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    return num1 != 0;
  }

  public class RevObjectCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
  {
    internal Step3 s3;
    public static long linkedNumber = 0;
    public static bool BlockLinking = false;
    public static bool allowChooseTemplate = true;
    public static Dictionary<ObjectCreatePages, bool> RevShowDict = new Dictionary<ObjectCreatePages, bool>()
    {
      {
        ObjectCreatePages.Classifier,
        true
      },
      {
        ObjectCreatePages.Properties,
        true
      },
      {
        ObjectCreatePages.Template,
        true
      }
    };

    public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

    public long CreateObjectDialog(
      int aObjectTypeID,
      long protoObjID,
      int[] linkTypesID,
      long[] relatedObjIDs,
      DateTime startRelationTime,
      bool IsVersion)
    {
      return -1;
    }

    public bool AcceptDialog(
      int ObjectTypeID,
      long TemplateObjectID,
      int[] RelationTypeIDs,
      long[] RelatedObjectIDs,
      DateTime StartDate,
      bool isVersion)
    {
      return false;
    }

    public bool AfterCreate(long newObjectID)
    {
      if (ECOPlugin.RevObjectCreator.linkedNumber == 0L)
      {
        ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
        if (service.CachedEditingContextID != 0L)
        {
          long objectID = service.CachedEditingContextModificationID;
          string str = (string) null;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
            if (dbObject == null)
              objectID = 0L;
            else
              str = dbObject.Caption;
          }
          if (objectID != 0L && MessageBox.Show($"{Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_244")}\r\n{str}?", Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_68"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            ECOPlugin.RevObjectCreator.linkedNumber = service.CachedEditingContextModificationID;
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(newObjectID, false);
        if (dbObject != null)
        {
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(RevHelper.idLinkedContNumber, false);
          if (dbAttribute != null)
            dbAttribute.AsInteger = ECOPlugin.RevObjectCreator.linkedNumber == 0L ? Math.Abs(newObjectID) : Math.Abs(ECOPlugin.RevObjectCreator.linkedNumber);
        }
      }
      return true;
    }

    public IDictionary<ObjectCreatePages, bool> VisiblePages
    {
      get => (IDictionary<ObjectCreatePages, bool>) ECOPlugin.RevObjectCreator.RevShowDict;
    }

    public bool OnCommitAction(
      IUserSession session,
      long newObjectID,
      List<NotificationEventArgs> nea)
    {
      IDBObject dbObject = session.GetObject(newObjectID, false);
      if (dbObject == null)
        return false;
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        dbObject = dbObject.CheckOut();
        newObjectID = dbObject.ObjectID;
      }
      IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDopDesign);
      if (attributeById != null)
        attributeById.AsString = " ";
      if (ECOPlugin.RevObjectCreator.linkedNumber != 0L && session.GetCustomService(typeof (IECOServer)) is IECOServer customService)
        customService.RecordLinkMessage(session.SessionGUID, newObjectID, ECOPlugin.RevObjectCreator.linkedNumber);
      ECOPlugin.RevObjectCreator.linkedNumber = 0L;
      return true;
    }

    public bool OnCancelAction(
      IUserSession session,
      long newObjectID,
      List<NotificationEventArgs> nea)
    {
      ECOPlugin.RevObjectCreator.linkedNumber = 0L;
      return true;
    }

    public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
    {
      this.s3 = new Step3((CreatedObjectItem) CreatedObject);
      this.s3.SetupControls(ECOPlugin.RevObjectCreator.allowChooseTemplate);
      return new Dictionary<UserControl, int>()
      {
        {
          (UserControl) this.s3,
          1
        }
      };
    }
  }

  public class CJObjectCreator : IObjectCreatorCustomService
  {
    public long CreateObjectDialog(
      int aObjectTypeID,
      long protoObjID,
      int[] linkTypesID,
      long[] relatedObjIDs,
      DateTime startRelationTime,
      bool IsVersion)
    {
      if (protoObjID != -1L)
      {
        int num = (int) MessageBox.Show(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_290"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
        return 0;
      }
      if (!ECOPlugin.plugin.CheckInitCJTemplate())
        return 0;
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_291"), Intermech.Localization.LocalizationHolder.rm.GetString("ECO.Client_292"), ECOPlugin.plugin.productTypeId, SelectionOptions.Default);
      if (numArray == null || numArray.Length == 0)
        return 0;
      long prodId = numArray[0];
      return ECOPlugin.plugin.CreateCJ(prodId);
    }
  }

  internal class RevInfo
  {
    private long _Id;
    private bool _Annuled;
    private bool _Stamped;

    public long Id
    {
      get => this._Id;
      set => this._Id = value;
    }

    public bool Annuled
    {
      get => this._Annuled;
      set => this._Annuled = value;
    }

    public bool Stamped
    {
      get => this._Stamped;
      set => this._Stamped = value;
    }

    public RevInfo(long revId, bool Annuled, bool Stamped)
    {
      this._Id = revId;
      this._Annuled = Annuled;
      this._Stamped = Stamped;
    }
  }

  public delegate string SetPLForAll(List<PendingLink> objList);

  public class ECOInfo
  {
    public Guid RelGuid;
    public long PartId;
    public long ObjectID;
    public long ProjID;
    public HidingType? hideType;
    public long? flags;
    public long? newVerId;
    public string changeNo;
    public int? futureStepId;
    public bool needDelete;
    public string mainVerGuid;
    public ECOGoal? goal;
    public long ID;
  }
}
