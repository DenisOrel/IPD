// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSPlugin
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSViews;
using Intermech.AVS.CAD;
using Intermech.AVS.Common_Dialogs;
using Intermech.AVS.Common_Dialogs.ArticleWithDocForm;
using Intermech.AVS.ECAD;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.AVS.Tool;
using Intermech.AVS.Victor;
using Intermech.Bars;
using Intermech.Cadmech.Integrator;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Protection;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Плагин Intermech.AVS</summary>
public class AVSPlugin : 
  IPackage,
  ICommandTarget2,
  ICommandTarget,
  IImDocumentManager,
  IConfigurable,
  ICommandsProvider
{
  internal static int appId = 339;
  internal static byte[][] b = new byte[32 /*0x20*/][]
  {
    new byte[16 /*0x10*/]
    {
      (byte) 139,
      (byte) 104,
      (byte) 68,
      (byte) 157,
      (byte) 151,
      (byte) 57,
      (byte) 60,
      (byte) 96 /*0x60*/,
      (byte) 10,
      (byte) 170,
      (byte) 116,
      (byte) 67,
      (byte) 68,
      (byte) 34,
      (byte) 226,
      (byte) 248
    },
    new byte[16 /*0x10*/]
    {
      (byte) 184,
      (byte) 36,
      (byte) 52,
      (byte) 59,
      (byte) 100,
      (byte) 229,
      (byte) 154,
      (byte) 84,
      (byte) 188,
      (byte) 17,
      (byte) 153,
      (byte) 235,
      (byte) 136,
      (byte) 206,
      (byte) 97,
      (byte) 9
    },
    new byte[16 /*0x10*/]
    {
      (byte) 76,
      (byte) 172,
      (byte) 112 /*0x70*/,
      (byte) 207,
      (byte) 72,
      (byte) 77,
      (byte) 226,
      (byte) 110,
      (byte) 39,
      (byte) 81,
      (byte) 196,
      (byte) 115,
      (byte) 135,
      (byte) 203,
      (byte) 147,
      (byte) 165
    },
    new byte[16 /*0x10*/]
    {
      (byte) 235,
      (byte) 138,
      (byte) 142,
      (byte) 80 /*0x50*/,
      (byte) 64 /*0x40*/,
      (byte) 29,
      (byte) 217,
      (byte) 150,
      (byte) 245,
      (byte) 154,
      (byte) 82,
      (byte) 189,
      (byte) 60,
      (byte) 217,
      (byte) 56,
      (byte) 66
    },
    new byte[16 /*0x10*/]
    {
      (byte) 77,
      (byte) 103,
      (byte) 85,
      (byte) 67,
      (byte) 186,
      (byte) 43,
      (byte) 77,
      (byte) 85,
      (byte) 212,
      (byte) 126,
      (byte) 43,
      (byte) 156,
      (byte) 4,
      (byte) 232,
      (byte) 159,
      (byte) 20
    },
    new byte[16 /*0x10*/]
    {
      (byte) 57,
      (byte) 15,
      (byte) 90,
      byte.MaxValue,
      (byte) 129,
      (byte) 22,
      (byte) 84,
      (byte) 250,
      (byte) 148,
      (byte) 92,
      (byte) 200,
      (byte) 45,
      (byte) 109,
      (byte) 246,
      (byte) 196,
      (byte) 96 /*0x60*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 53,
      (byte) 230,
      (byte) 136,
      (byte) 191,
      (byte) 51,
      (byte) 106,
      (byte) 136,
      (byte) 62,
      (byte) 133,
      (byte) 214,
      (byte) 117,
      (byte) 69,
      (byte) 117,
      (byte) 200,
      (byte) 216,
      (byte) 62
    },
    new byte[16 /*0x10*/]
    {
      (byte) 6,
      (byte) 230,
      (byte) 36,
      (byte) 61,
      (byte) 118,
      (byte) 102,
      (byte) 65,
      (byte) 110,
      (byte) 247,
      (byte) 194,
      (byte) 31 /*0x1F*/,
      (byte) 69,
      (byte) 182,
      (byte) 49,
      (byte) 155,
      (byte) 60
    },
    new byte[16 /*0x10*/]
    {
      (byte) 22,
      (byte) 207,
      (byte) 246,
      (byte) 220,
      (byte) 204,
      (byte) 159,
      (byte) 62,
      (byte) 33,
      (byte) 94,
      (byte) 26,
      (byte) 251,
      (byte) 201,
      (byte) 33,
      (byte) 69,
      (byte) 105,
      (byte) 63 /*0x3F*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 88,
      (byte) 54,
      (byte) 218,
      (byte) 64 /*0x40*/,
      (byte) 187,
      (byte) 253,
      (byte) 165,
      (byte) 234,
      (byte) 237,
      (byte) 165,
      (byte) 214,
      (byte) 132,
      (byte) 232,
      (byte) 137,
      (byte) 18,
      (byte) 237
    },
    new byte[16 /*0x10*/]
    {
      (byte) 178,
      (byte) 175,
      (byte) 62,
      (byte) 161,
      (byte) 67,
      (byte) 212,
      (byte) 114,
      (byte) 226,
      (byte) 222,
      (byte) 23,
      (byte) 189,
      (byte) 228,
      (byte) 181,
      (byte) 8,
      (byte) 224 /*0xE0*/,
      (byte) 11
    },
    new byte[16 /*0x10*/]
    {
      (byte) 206,
      (byte) 241,
      (byte) 171,
      (byte) 180,
      (byte) 247,
      (byte) 174,
      (byte) 160 /*0xA0*/,
      (byte) 73,
      (byte) 118,
      (byte) 222,
      (byte) 240 /*0xF0*/,
      (byte) 14,
      (byte) 152,
      (byte) 183,
      (byte) 35,
      (byte) 136
    },
    new byte[16 /*0x10*/]
    {
      (byte) 166,
      (byte) 237,
      (byte) 133,
      (byte) 53,
      (byte) 241,
      (byte) 194,
      (byte) 71,
      (byte) 149,
      (byte) 168,
      (byte) 103,
      (byte) 96 /*0x60*/,
      (byte) 24,
      (byte) 110,
      (byte) 191,
      (byte) 28,
      (byte) 64 /*0x40*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 196,
      (byte) 244,
      (byte) 10,
      (byte) 44,
      (byte) 230,
      (byte) 232,
      (byte) 60,
      (byte) 135,
      (byte) 121,
      (byte) 44,
      (byte) 91,
      (byte) 175,
      (byte) 210,
      (byte) 185,
      (byte) 247,
      (byte) 26
    },
    new byte[16 /*0x10*/]
    {
      (byte) 108,
      (byte) 54,
      (byte) 147,
      (byte) 197,
      (byte) 137,
      (byte) 55,
      (byte) 250,
      (byte) 66,
      (byte) 92,
      (byte) 19,
      (byte) 254,
      (byte) 181,
      (byte) 226,
      (byte) 89,
      (byte) 108,
      (byte) 86
    },
    new byte[16 /*0x10*/]
    {
      (byte) 140,
      (byte) 31 /*0x1F*/,
      (byte) 217,
      (byte) 39,
      (byte) 111,
      (byte) 241,
      (byte) 178,
      (byte) 65,
      (byte) 32 /*0x20*/,
      (byte) 107,
      (byte) 124,
      (byte) 66,
      (byte) 19,
      (byte) 204,
      (byte) 247,
      (byte) 4
    },
    new byte[16 /*0x10*/]
    {
      (byte) 58,
      (byte) 239,
      (byte) 34,
      (byte) 52,
      (byte) 192 /*0xC0*/,
      (byte) 52,
      (byte) 77,
      (byte) 239,
      (byte) 104,
      (byte) 168,
      (byte) 248,
      (byte) 220,
      (byte) 115,
      (byte) 90,
      (byte) 116,
      (byte) 31 /*0x1F*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 164,
      (byte) 232,
      (byte) 80 /*0x50*/,
      (byte) 238,
      (byte) 149,
      (byte) 231,
      (byte) 228,
      (byte) 203,
      (byte) 65,
      (byte) 53,
      (byte) 110,
      (byte) 47,
      (byte) 171,
      (byte) 61,
      (byte) 202,
      (byte) 16 /*0x10*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 251,
      (byte) 124,
      (byte) 119,
      (byte) 127 /*0x7F*/,
      (byte) 116,
      (byte) 26,
      (byte) 129,
      (byte) 110,
      (byte) 242,
      (byte) 66,
      (byte) 216,
      (byte) 222,
      (byte) 196,
      (byte) 146,
      (byte) 179,
      (byte) 78
    },
    new byte[16 /*0x10*/]
    {
      (byte) 212,
      (byte) 101,
      (byte) 124,
      (byte) 205,
      (byte) 26,
      (byte) 54,
      (byte) 3,
      (byte) 206,
      (byte) 131,
      (byte) 83,
      (byte) 61,
      (byte) 176 /*0xB0*/,
      (byte) 50,
      (byte) 254,
      (byte) 113,
      (byte) 107
    },
    new byte[16 /*0x10*/]
    {
      (byte) 251,
      (byte) 9,
      (byte) 222,
      (byte) 127 /*0x7F*/,
      (byte) 156,
      (byte) 231,
      (byte) 128 /*0x80*/,
      (byte) 55,
      (byte) 232,
      (byte) 163,
      (byte) 93,
      (byte) 225,
      (byte) 62,
      (byte) 30,
      (byte) 207,
      (byte) 2
    },
    new byte[16 /*0x10*/]
    {
      (byte) 203,
      (byte) 156,
      (byte) 215,
      (byte) 132,
      (byte) 180,
      (byte) 66,
      (byte) 64 /*0x40*/,
      (byte) 173,
      (byte) 166,
      (byte) 238,
      (byte) 141,
      (byte) 84,
      (byte) 167,
      (byte) 45,
      (byte) 124,
      (byte) 240 /*0xF0*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 159,
      (byte) 222,
      (byte) 186,
      (byte) 101,
      (byte) 225,
      (byte) 236,
      (byte) 226,
      (byte) 31 /*0x1F*/,
      (byte) 110,
      (byte) 180,
      (byte) 66,
      (byte) 125,
      (byte) 91,
      (byte) 111,
      (byte) 43,
      (byte) 213
    },
    new byte[16 /*0x10*/]
    {
      (byte) 30,
      (byte) 133,
      (byte) 50,
      (byte) 104,
      (byte) 226,
      (byte) 14,
      (byte) 31 /*0x1F*/,
      (byte) 80 /*0x50*/,
      (byte) 198,
      (byte) 112 /*0x70*/,
      (byte) 231,
      (byte) 97,
      (byte) 119,
      (byte) 14,
      (byte) 218,
      (byte) 132
    },
    new byte[16 /*0x10*/]
    {
      (byte) 245,
      (byte) 170,
      (byte) 225,
      (byte) 106,
      (byte) 124,
      (byte) 245,
      (byte) 131,
      (byte) 19,
      (byte) 252,
      (byte) 63 /*0x3F*/,
      (byte) 151,
      (byte) 209,
      (byte) 189,
      (byte) 160 /*0xA0*/,
      (byte) 47,
      (byte) 211
    },
    new byte[16 /*0x10*/]
    {
      (byte) 239,
      (byte) 94,
      (byte) 206,
      (byte) 235,
      (byte) 224 /*0xE0*/,
      (byte) 107,
      (byte) 197,
      (byte) 35,
      (byte) 209,
      (byte) 15,
      (byte) 206,
      (byte) 246,
      (byte) 4,
      (byte) 221,
      (byte) 59,
      (byte) 196
    },
    new byte[16 /*0x10*/]
    {
      (byte) 115,
      (byte) 213,
      (byte) 63 /*0x3F*/,
      (byte) 229,
      (byte) 216,
      (byte) 77,
      (byte) 108,
      (byte) 14,
      (byte) 39,
      (byte) 190,
      (byte) 16 /*0x10*/,
      (byte) 231,
      (byte) 164,
      (byte) 2,
      (byte) 220,
      (byte) 121
    },
    new byte[16 /*0x10*/]
    {
      (byte) 118,
      (byte) 54,
      (byte) 171,
      (byte) 19,
      (byte) 248,
      (byte) 202,
      (byte) 35,
      (byte) 246,
      (byte) 221,
      (byte) 39,
      (byte) 225,
      (byte) 37,
      (byte) 4,
      (byte) 21,
      (byte) 234,
      (byte) 220
    },
    new byte[16 /*0x10*/]
    {
      (byte) 120,
      (byte) 135,
      (byte) 235,
      (byte) 148,
      (byte) 61,
      (byte) 100,
      (byte) 134,
      (byte) 170,
      (byte) 91,
      (byte) 166,
      (byte) 104,
      (byte) 162,
      (byte) 85,
      (byte) 17,
      (byte) 97,
      (byte) 205
    },
    new byte[16 /*0x10*/]
    {
      (byte) 58,
      (byte) 101,
      (byte) 248,
      (byte) 204,
      (byte) 48 /*0x30*/,
      (byte) 94,
      (byte) 89,
      (byte) 40,
      (byte) 253,
      (byte) 141,
      (byte) 70,
      (byte) 142,
      (byte) 0,
      (byte) 104,
      (byte) 233,
      (byte) 181
    },
    new byte[16 /*0x10*/]
    {
      (byte) 132,
      (byte) 110,
      (byte) 9,
      (byte) 134,
      (byte) 48 /*0x30*/,
      (byte) 69,
      (byte) 253,
      (byte) 167,
      (byte) 61,
      (byte) 210,
      (byte) 67,
      (byte) 189,
      (byte) 228,
      (byte) 155,
      (byte) 110,
      (byte) 249
    },
    new byte[16 /*0x10*/]
    {
      (byte) 55,
      (byte) 183,
      (byte) 169,
      (byte) 114,
      (byte) 130,
      (byte) 82,
      (byte) 58,
      (byte) 15,
      (byte) 171,
      (byte) 250,
      (byte) 75,
      (byte) 189,
      (byte) 123,
      (byte) 168,
      (byte) 253,
      (byte) 25
    }
  };
  /// <summary>Ссылка на единственный экземпляр класса типа AVSPlugin</summary>
  private static AVSPlugin _instance = (AVSPlugin) null;
  /// <summary>Служба для вызова методов из других потоков</summary>
  private static IInvokeService _iInvokeService = (IInvokeService) null;
  /// <summary>Сервис работы с правилами подбора версий</summary>
  private static IFiltrationService _iFiltrationService = (IFiltrationService) null;
  /// <summary>Служба для работы с исполнениями</summary>
  private static IArticleService _iArticleService = (IArticleService) null;
  /// <summary>Служба для работы со спецификациями (со стороны PDM)</summary>
  private static IPDMSpecificationsService _PDMSpecificationsService;
  /// <summary>Загружен ли плагин "Intermech.PDM.Server"</summary>
  private static bool _pdmServerLoaded = false;
  /// <summary>Интерфейс модуля Intermech.PDM, позволяющий вызывать команды для работы с допустимыми заменителями</summary>
  private static IPDMSubstitutesService _pdmClientSubstitutesService;
  private static IColumnSchemes _iColumnSchemes = (IColumnSchemes) null;
  private static IImportStructureFromCadService _iImportStructureFromCadService = (IImportStructureFromCadService) null;
  private static IClientMetadataCache _metadataCache = (IClientMetadataCache) null;
  private static int lll = 0;
  /// <summary>Служба по созданию новых объектов</summary>
  private static IObjectCreatorService _iObjectCreatorService = (IObjectCreatorService) null;
  private static List<int> _objTypes_AssemlyUnit = (List<int>) null;
  private static List<int> _objTypes_Specification = (List<int>) null;
  private static SaveFileDialog saveDlg = (SaveFileDialog) null;
  private static INotificationService _notificationService = (INotificationService) null;
  private MenuButtonItem _miAddOtherRecordTypes;
  /// <summary>TextBoxElement выделенный ранее</summary>
  private RectangleElement prevSelectedTextBox;
  internal AVSWindow activeAVSWindow;
  internal bool avsWindow;
  internal ImDocumentEditorForm activeImDocumentEditorForm;
  internal bool docWindow;
  internal bool isAVSTemplate;
  internal bool isSpecificationTemplate;
  internal bool isElementListTemplate;
  internal bool isVedomostTemplate;
  internal bool isVedomost;
  internal Vedomost_VB_Static.TypePageVedom typePageVedom;
  internal bool isConstrTablTemplate;
  internal bool isConstrTabl;
  internal bool isConstrSpecification;
  internal Dictionary<string, AVSPluginExecuteCommand> VedomostSettingsMenu = new Dictionary<string, AVSPluginExecuteCommand>();
  internal Dictionary<string, AVSPluginExecuteCommand> TablSettingsMenu = new Dictionary<string, AVSPluginExecuteCommand>();
  /// <summary>Команды меню окна редактора ведомостей.
  /// Команды, которые обрабатываются окном сюда добавляются без обработчика только для того чтобы они скрывались,
  /// при переключении на другое окно</summary>
  internal Dictionary<string, AVSPluginExecuteCommand> VedomostEditorVBMenu = new Dictionary<string, AVSPluginExecuteCommand>();
  internal Dictionary<string, AVSPluginExecuteCommand> TablEditorVBMenu = new Dictionary<string, AVSPluginExecuteCommand>();
  /// <summary>Команды меню окна редактора ведомостей</summary>
  internal List<string> VedomostContexMenuList = new List<string>();
  public static string TestSameDesignation1 = "ИНТМ.123456.002";
  public static string TestSameDesignation2 = "ИНТМ.123456.123";
  private SaveFileDialog saveToFileDialog;
  private string recentlySaveAsPath;
  private static System.IServiceProvider serviceProvider = (System.IServiceProvider) null;
  private static IImbaseSelector imbaseSelector = (IImbaseSelector) null;
  private static IAVSServerService avsServerService = (IAVSServerService) null;
  private static DockManager dockManager = (DockManager) null;
  private ICommandManager commandManager;
  private ImageList imageList = new ImageList();
  private IConfigurationManager configManager;
  /// <summary>Список колонок TreeList в AVSWindow</summary>
  internal static List<AvsRowAttributeInfo> specificationGridViewCols = new List<AvsRowAttributeInfo>();
  internal static List<AvsRowAttributeInfo> elementListGridViewCols = new List<AvsRowAttributeInfo>();
  internal Dictionary<string, ExternalAVSCommand> ExternalAVSCommands = new Dictionary<string, ExternalAVSCommand>();
  private IStatusBar statusBar;
  private INamedImageList iNamedImageList;

  /// <summary>Распределить лицензию для AVS</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static void AllocateAVSLicense()
  {
    ILicenser service = ServicesManager.GetService<ILicenser>(false);
    if (service == null)
      throw new ProtectionException("Интерфейс лицензий не найден");
    if (AVSPlugin.lll == 0)
      service.AllocateLicense(AVSPlugin.appId);
    ++AVSPlugin.lll;
  }

  /// <summary>Освободить лицензию AVS</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static void ReleaseAVSLicense()
  {
    ILicenser service = ServicesManager.GetService<ILicenser>();
    --AVSPlugin.lll;
    if (AVSPlugin.lll != 0)
      return;
    service.ReleaseLicense(AVSPlugin.appId);
  }

  /// <summary>Конструктор</summary>
  public AVSPlugin()
  {
    AVSPlugin._instance = AVSPlugin._instance == null ? this : throw new Exception("Constructor of class AVSPlugin can be called only once. Please, use static Instance property.");
  }

  /// <summary>
  /// Признак того, что плагин был успешно загружен и инициализирован
  /// </summary>
  public bool IsLoaded { get; private set; }

  /// <summary> Ссылка на единственный экземпляр класса типа AVSPlugin </summary>
  public static AVSPlugin Instance
  {
    get
    {
      if (AVSPlugin._instance == null)
        AVSPlugin._instance = new AVSPlugin();
      return AVSPlugin._instance;
    }
  }

  /// <summary>Служба для вызова методов из других потоков</summary>
  public static IInvokeService IInvokeService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._iInvokeService == null)
        AVSPlugin._iInvokeService = (IInvokeService) ServicesManager.GetService(typeof (IInvokeService));
      return AVSPlugin._iInvokeService;
    }
  }

  /// <summary> Сервис работы с правилами подбора версий </summary>
  public static IFiltrationService IFiltrationService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._iFiltrationService == null)
        AVSPlugin._iFiltrationService = (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
      return AVSPlugin._iFiltrationService;
    }
  }

  /// <summary>Служба для работы с исполнениями</summary>
  public static IArticleService IArticleService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._iArticleService == null)
        AVSPlugin._iArticleService = (IArticleService) ServicesManager.GetService(typeof (IArticleService));
      return AVSPlugin._iArticleService;
    }
  }

  /// <summary>Служба для работы со спецификациями (со стороны PDM)</summary>
  public static IPDMSpecificationsService PDMSpecificationsService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._PDMSpecificationsService == null)
        AVSPlugin._PDMSpecificationsService = ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService;
      return AVSPlugin._PDMSpecificationsService;
    }
  }

  /// <summary>Загружен ли плагин "Intermech.PDM.Server"</summary>
  public static bool PDMServerLoaded
  {
    [DebuggerStepThrough] get => AVSPlugin._pdmServerLoaded;
  }

  /// <summary>Интерфейс модуля Intermech.PDM, позволяющий вызывать команды для работы с допустимыми заменителями</summary>
  public static IPDMSubstitutesService PDMClientSubstitutesService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._pdmClientSubstitutesService == null)
        AVSPlugin._pdmClientSubstitutesService = ServicesManager.GetService(typeof (IPDMSubstitutesService)) as IPDMSubstitutesService;
      return AVSPlugin._pdmClientSubstitutesService;
    }
  }

  /// <summary> Сервис управления докингом </summary>
  public static DockManager DockManager
  {
    get
    {
      if (AVSPlugin.dockManager == null)
        AVSPlugin.dockManager = (DockManager) ServicesManager.GetService(typeof (DockManager));
      return AVSPlugin.dockManager;
    }
  }

  /// <summary> Сервис работы с схемами колонок </summary>
  public static IColumnSchemes IColumnSchemes
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._iColumnSchemes == null)
        AVSPlugin._iColumnSchemes = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
      return AVSPlugin._iColumnSchemes;
    }
  }

  internal static IClientMetadataCache MDCache
  {
    get
    {
      if (AVSPlugin._metadataCache == null)
        AVSPlugin._metadataCache = ServicesManager.GetService<IClientMetadataCache>();
      return AVSPlugin._metadataCache;
    }
  }

  /// <summary>Служба по созданию новых объектов</summary>
  public static IObjectCreatorService IObjectCreatorService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._iObjectCreatorService == null)
        AVSPlugin._iObjectCreatorService = (IObjectCreatorService) ServicesManager.GetService(typeof (IObjectCreatorService));
      return AVSPlugin._iObjectCreatorService;
    }
  }

  public static List<int> ObjTypes_AssemlyUnit
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._objTypes_AssemlyUnit == null)
      {
        AVSPlugin._objTypes_AssemlyUnit = MetaDataHelper.GetObjectTypeChildrenID(AvsIDCache.ObjType_AssemblyUnit);
        if (!AVSPlugin._objTypes_AssemlyUnit.Contains(AvsIDCache.ObjType_AssemblyUnit))
          AVSPlugin._objTypes_AssemlyUnit.Add(AvsIDCache.ObjType_AssemblyUnit);
      }
      return AVSPlugin._objTypes_AssemlyUnit;
    }
  }

  public static List<int> ObjTypes_Specification
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._objTypes_Specification == null)
      {
        AVSPlugin._objTypes_Specification = MetaDataHelper.GetObjectTypeChildrenID(AvsIDCache.ObjType_Specification);
        if (!AVSPlugin._objTypes_Specification.Contains(AvsIDCache.ObjType_Specification))
          AVSPlugin._objTypes_Specification.Add(AvsIDCache.ObjType_Specification);
      }
      return AVSPlugin._objTypes_Specification;
    }
  }

  /// <summary>Получить список всех разделов спецификаций</summary>
  /// <returns></returns>
  public static List<SpecificationSectionInfo> GetSpecificationSections()
  {
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    return SpecificationSectionInfo.Sections;
  }

  /// <summary>Спецификация пришла из другого узла портала</summary>
  internal static bool IsSpecificationFromAnotherPortal(long documentID)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(documentID);
      if (dbObject != null)
      {
        if (sessionKeeper.Session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService)
        {
          SiteInfo info = customService.Info;
          if (info != null)
          {
            char code = info.Code;
            string siteId = dbObject.SiteID;
            if (siteId.Length >= 2)
            {
              if ((int) siteId[0] != (int) code)
                flag = true;
            }
          }
        }
      }
    }
    return flag;
  }

  /// <summary>Провайдер сервисов клиента</summary>
  public static System.IServiceProvider ServiceProvider
  {
    [DebuggerStepThrough] get => AVSPlugin.serviceProvider;
  }

  /// <summary>Диалог сохранения документа спецификации в файл на диске</summary>
  internal static SaveFileDialog SaveDlg
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin.saveDlg == null)
        AVSPlugin.saveDlg = ImDocumentEditorFormBase.CreateSaveFileDialog();
      return AVSPlugin.saveDlg;
    }
  }

  /// <summary>Активное окно AVS, null, если активное окно клиента не AVS</summary>
  public AVSWindow ActiveAVSWindow => AVSPlugin.DockManager.ActiveDocument as AVSWindow;

  /// <summary>Активное окно AVS, null, если активное окно клиента не AVS</summary>
  public long ActiveDocumentId
  {
    get
    {
      AVSWindow activeAvsWindow = this.ActiveAVSWindow;
      return activeAvsWindow != null ? activeAvsWindow.DocumentID : -1L;
    }
  }

  /// <summary>Активное окно редактора документа, null, если активно не окно редактора документа</summary>
  public ImDocumentEditorForm ActiveImDocumentEditorForm
  {
    [DebuggerStepThrough] get => AVSPlugin.DockManager.ActiveDocument as ImDocumentEditorForm;
  }

  /// <summary>Элемент управления документа активного окна AVS</summary>
  private Intermech.Document.UI.DocumentControl ActiveImDocumentControl
  {
    [DebuggerStepThrough] get
    {
      return AVSPlugin.DockManager.ActiveDocument != null && AVSPlugin.DockManager.ActiveDocument is AVSWindow ? (AVSPlugin.DockManager.ActiveDocument as AVSWindow).DocumentControl : (Intermech.Document.UI.DocumentControl) null;
    }
  }

  /// <summary>Сервис выбора элементов ImBase</summary>
  public static IImbaseSelector ImbaseSelector
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin.imbaseSelector == null)
        AVSPlugin.imbaseSelector = ServicesManager.GetService(typeof (IImbaseSelector)) as IImbaseSelector;
      return AVSPlugin.imbaseSelector;
    }
  }

  /// <summary>Восстановить окно после загрузки клиента</summary>
  /// <param name="guid">Guid окна</param>
  /// <param name="persistString">Строка данных окна</param>
  /// <returns>Окно</returns>
  public DockControl RestoreDocumentWindow(Guid guid, string persistString)
  {
    try
    {
      if (guid == DocumentEditorPlugin.AVSWindowGuid)
        return this.RestoreAVSWindow(guid, persistString);
      if (guid == DocumentEditorPlugin.VedomostWindowGuid)
        return this.RestoreVedomostEditorWindow(guid, persistString);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return (DockControl) null;
  }

  /// <summary>Восстановить окно AVS после загрузки клиента</summary>
  /// <param name="guid">Guid окна</param>
  /// <param name="persistString">Строка данных окна</param>
  /// <returns>Окно</returns>
  private DockControl RestoreAVSWindow(Guid guid, string persistString)
  {
    if (guid != DocumentEditorPlugin.AVSWindowGuid)
      return (DockControl) null;
    long num1 = -1;
    int objType = -1;
    readOnly = false;
    HybridDictionary restoreParams = DocumentEditorPlugin.ReadConfigDictionaryFromPersistString(persistString);
    Guid guid1 = AvsIDCache.ConvertToGuid(restoreParams[(object) "AssemblyGuid"]);
    if (guid1 != Guid.Empty)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(guid1, false);
        if (dbObject != null)
        {
          if (dbObject.AccessLevel <= sessionKeeper.Session.SecurityLevel)
          {
            num1 = dbObject.ObjectID;
            objType = dbObject.ObjectType;
          }
        }
      }
    }
    object obj1 = restoreParams[(object) "ReadOnly"];
    if (obj1 == null || !(obj1 is bool readOnly))
      ;
    if (Intermech.Consts.IsUndefinedObjectId(num1))
      return (DockControl) null;
    AVSWindow avsWindow = this.OpenAVSWindow(new OpenAVSDocArgs(num1, objType, false, readOnly, restoreParams));
    if (avsWindow != null)
    {
      object obj2 = restoreParams[(object) "BottomPanelType"];
      if (obj2 != null && obj2 is AVSWindow.enumBottomPanelType enumBottomPanelType)
        avsWindow.BottomPanelType = enumBottomPanelType;
      object obj3 = restoreParams[(object) "BottomPanelHeight"];
      if (obj3 != null && obj3 is int num2)
        avsWindow._panelBottom.Height = num2;
    }
    return (DockControl) avsWindow;
  }

  /// <summary>Восстановить окно ВЕДОМОСТИ ИЛИ ТАБЛИЦЫ AVS после загрузки клиента</summary>
  /// <param name="guid">Guid окна</param>
  /// <param name="persistString">Строка данных окна</param>
  /// <returns>Окно</returns>
  private DockControl RestoreVedomostEditorWindow(Guid guid, string persistString)
  {
    if (guid != DocumentEditorPlugin.VedomostWindowGuid)
      return (DockControl) null;
    DocumentWindowData persistString1 = DocumentEditorPlugin.ParsePersistString(persistString);
    return !persistString1.IsEmpty ? (DockControl) DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(persistString1.DocumentObjectID, persistString1.ReadOnly, false, new DocumentWindowCreatorDelegate(VedomostEditorWindow.VedomostEditorWindowCreator)) : (DockControl) null;
  }

  private void DockManagerActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
  {
    try
    {
      if (!(e.NewActiveDocument is AVSWindow newActiveDocument))
        return;
      newActiveDocument.LoadColumnsStateIfNeeded();
      newActiveDocument.OnActivated();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void DocumentModifiedChanged(object sender, ModifiedChanged_EventArgs e)
  {
    try
    {
      if (!(sender is ImDocument document))
        return;
      this.UpdateDocumentCaptions(document);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void UpdateDocumentCaption(AVSWindow avsWin)
  {
    if (avsWin == null)
      throw new ArgumentNullException(nameof (avsWin));
    avsWin.UpdateDocumentWindowCaption();
  }

  private void UpdateDocumentCaptions(AVSWindow docContent)
  {
    this.UpdateDocumentCaptions(docContent.Document);
  }

  private void UpdateDocumentCaptions(ImDocument document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (document.IsTemplate && document.TemplateOwner != null)
      document = document.TemplateOwner as ImDocument;
    ImDocumentData documentTemplate = document.DocumentTemplate;
    if (document.IsTemplate)
      document = document.TemplateOwner as ImDocument;
    for (int index = 0; index < AVSPlugin.DockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (AVSPlugin.DockManager.DocumentContainer.Documents[index] is AVSWindow document1)
        document1.UpdateDocumentWindowCaption();
    }
  }

  private void UpdateDocumentCaptions()
  {
    for (int index = 0; index < AVSPlugin.DockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (AVSPlugin.DockManager.DocumentContainer.Documents[index] is AVSWindow document)
        document.UpdateDocumentWindowCaption();
    }
  }

  /// <summary>Фабричный метод для создания экземпляра класса AVSDocument подходящего для заданного типа объекта БД</summary>
  /// <returns></returns>
  internal static AVSDocument CreateAVSDocumentForDBObject(int dbObjectType)
  {
    return AvsIDCache.IsSpecification(dbObjectType) || !MetaDataHelper.IsObjectTypeChildOf(dbObjectType, AvsIDCache.ObjType_Document) ? (AVSDocument) new AVSSpecification() : (!AvsIDCache.IsElementList(dbObjectType) ? new AVSDocument() : (AVSDocument) new AVSElementList());
  }

  /// <summary>Создать новые объекты Сборочной единицы и Спецификации</summary>
  public void CreateNewAssemblyWithSpecification()
  {
    long objectByTypeDialog = (AVSPlugin.serviceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).CreateObjectByTypeDialog(AvsIDCache.ObjType_Specification);
    if (objectByTypeDialog.IsUndefinedId())
      return;
    RecentObjectsNode.MRUObjects.Add(objectByTypeDialog, ObjectAction.Create, DateTime.UtcNow);
  }

  /// <summary>Проверить возможность открытия на просмотр документа</summary>
  /// <param name="args">Аргументы и контекст вызова метода</param>
  /// <returns></returns>
  private bool CheckAllowableForOpenReadOnly(OpenAVSDocArgs args)
  {
    long documentId = -1;
    List<string> reasonList;
    if (AVSDocument.SpecificationIsNeedUpdate(args.ObjectId, args.ObjectType, out documentId, out reasonList))
    {
      if (documentId == -1L)
      {
        if (MessageBox.Show("Спецификация на данное изделие отсутствует.\r\nСоздать спецификацию в редакторе AVS?", "AVS", MessageBoxButtons.OKCancel) != DialogResult.OK)
          return false;
        args.NeedUpdate = true;
        args.ReadOnly = false;
      }
      else if (IMMessageBox.Show("AVS", "Спецификация может не соответствовать изделию!\r\n\r\nОткрыть спецификацию в редакторе AVS, чтобы обновить?\r\n\r\n\r\nСписок несоответствий:", MessageBoxButtons.YesNo, (IList<string>) reasonList) == DialogResult.Yes)
      {
        args.NeedUpdate = true;
        args.ReadOnly = false;
      }
    }
    return true;
  }

  /// <summary>Открыть окно AVS</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="readOnly">Режим только для чтения</param>
  /// <param name="externalCommands">Массив внешних команд, которые можно вызвать из окна</param>
  public AVSWindow OpenAVSWindow(
    long objectId,
    int objectType = -1,
    bool readOnly = false,
    ExternalAVSCommand[] externalCommands = null)
  {
    return this.OpenAVSWindow(new OpenAVSDocArgs(objectId, objectType, readOnly: readOnly, externalCommands: externalCommands));
  }

  /// <summary>Открыть окно AVS</summary>
  /// <param name="args">Аргументы и контекст вызова метода</param>
  public AVSWindow OpenAVSWindow(OpenAVSDocArgs args)
  {
    if (!args.ReadOnly && !DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(LaunchType.Edit, args.ObjectId, out string _).Item1)
      return (AVSWindow) null;
    DockControl avsWindow = this.FindAVSWindow(args.ObjectId, args.ObjectType, args.ObjectGuid);
    if (!(avsWindow is AVSWindow target) || !args.ReadOnly && target.ReadOnly || args.ForceReload)
    {
      if (args.ReadOnly && !this.CheckAllowableForOpenReadOnly(args))
        return (AVSWindow) null;
      AVSDocument avsDocument = this.LoadAVSDocument(args);
      if (avsDocument.DontOpenDocument)
      {
        if (!string.IsNullOrEmpty(args.ErrorMessage))
        {
          int num = (int) MessageBox.Show(args.ErrorMessage, "AVS");
        }
        return (AVSWindow) null;
      }
      target = new AVSWindow((IImDocumentManager) this, avsDocument, avsDocument.ReadOnly || args.ReadOnly, args.RestoreParams, args.ExternalCommands);
      if (avsWindow == null)
        avsWindow = this.FindAVSWindow(target.DocumentID, target.DocumentType, target.DocumentGuid);
      avsWindow?.ReplaceTo((DockControl) target);
      target.UpdateDocumentWindowCaption();
      this.UpdateDocumentCaptions();
      target.Show(AVSPlugin.DockManager, DockState.Document);
      target.Select();
      if (avsDocument.templateUpdated)
      {
        int num1 = (int) IMMessageBox.Show("Уведомление", "Шаблон документа изменился и был обновлен", MessageBoxButtons.OK, IMMessageBoxImage.Information);
      }
      if (AvsConfig.General.ShowEvents || avsDocument.AvsRowEventMessageViewer.Events.Values.SelectMany<List<AvsRowEventMessage>, AvsRowEventMessage>((System.Func<List<AvsRowEventMessage>, IEnumerable<AvsRowEventMessage>>) (e => (IEnumerable<AvsRowEventMessage>) e)).Any<AvsRowEventMessage>((System.Func<AvsRowEventMessage, bool>) (ev => ev.EventType == AVSEventType.SkipUpdateRowField)))
      {
        avsDocument.AvsRowEventMessageViewer.Show();
      }
      else
      {
        avsDocument.AvsRowEventMessageViewer.Clear();
        avsDocument.AvsRowEventMessageViewer.Close();
      }
      if (target.ReadOnly)
        RecentObjectsNode.MRUObjects.Add(args.ObjectId, ObjectAction.View, DateTime.UtcNow);
      else
        RecentObjectsNode.MRUObjects.Add(args.ObjectId, ObjectAction.Open, DateTime.UtcNow);
    }
    else
      target.Activate();
    args.AvsWindow = target;
    return target;
  }

  /// <summary>Открыть конструкторский документ</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="readOnly">Режим только для чтения</param>
  public AVSDocument LoadAVSDocument(long objectId, int objectType, bool readOnly)
  {
    return this.LoadAVSDocument(new OpenAVSDocArgs(objectId, objectType, false, readOnly));
  }

  /// <summary>Открыть конструкторский документ</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="readOnly">Режим только для чтения</param>
  public AVSDocument LoadAVSDocument(long objectId, bool readOnly)
  {
    return this.LoadAVSDocument(new OpenAVSDocArgs(objectId, readOnly));
  }

  /// <summary>Открыть конструкторский документ</summary>
  /// <param name="args">Аргументы и контекст вызова метода</param>
  private AVSDocument LoadAVSDocument(OpenAVSDocArgs args)
  {
    AVSDocument documentForDbObject = AVSPlugin.CreateAVSDocumentForDBObject(args.ObjectType);
    documentForDbObject.LoadAVSDocumentFromDB(args);
    if (args.NeedUpdate && args.SaveIfUpdatedForLoad)
      documentForDbObject.SaveAVSDocumentToDB();
    return documentForDbObject;
  }

  /// <summary>Найти окно AVS для заданного объекта</summary>
  public DockControl FindAVSWindow(long objectId, int objectType, Guid objectGuid)
  {
    if (objectId.IsUndefinedId())
      return (DockControl) null;
    if (AVSPlugin.DockManager == null)
      throw new Exception("dockManager == null");
    if (objectGuid == Guid.Empty || objectType.IsUndefinedTypeId())
    {
      QuickObjectInfo objectInfo = Session.GetObjectInfo(objectId);
      objectGuid = objectInfo.VersionGuid;
      objectType = objectInfo.ObjectTypeID;
    }
    foreach (DockControl document in AVSPlugin.DockManager.DocumentContainer.Documents)
    {
      if (document is AVSWindow avsWindow)
      {
        if (avsWindow.DocumentGuid == objectGuid)
          return (DockControl) avsWindow;
        if (avsWindow.AVSDocument != null && avsWindow.AVSDocument.IsSpecification && avsWindow.AVSDocument.productsInfo != null)
        {
          for (int index = 0; index < avsWindow.AVSDocument.productsInfo.Count; ++index)
          {
            if (avsWindow.AVSDocument.productsInfo[index].Guid == objectGuid)
              return (DockControl) avsWindow;
          }
        }
      }
      else if (document.Guid == DocumentEditorPlugin.AVSWindowGuid && AvsIDCache.ConvertToGuid(DocumentEditorPlugin.ReadConfigDictionaryFromPersistString(document.PersistString)[(object) "AssemblyGuid"]) == objectGuid)
        return document;
    }
    return (DockControl) null;
  }

  public AVSWindow ReloadSpecification(AVSDocument avsDocument)
  {
    AVSWindow avsWindow = avsDocument.AVSWindow;
    try
    {
      long documentId = avsDocument.DocumentID;
      int documentDbObjectType = avsDocument.DocumentDBObjectType;
      bool readOnly = avsDocument.ReadOnly;
      if (AVSPlugin.DockManager == null)
        throw new Exception("dockManager == null");
      Math.Abs(documentId);
      Guid empty = Guid.Empty;
      return this.OpenAVSWindow(new OpenAVSDocArgs(documentId, documentDbObjectType, readOnly: readOnly)
      {
        ForceReload = true,
        CreateUndo = new bool?(false)
      });
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return (AVSWindow) null;
    }
  }

  /// <summary>Печатать конструкторский документа</summary>
  /// <param name="objId">Идентификатор объекта документа или изделия, на который выпущен документ</param>
  /// <param name="objType">Тип объекта</param>
  public void PrintAVSDocument(long objId, int objType)
  {
    this.PrintAVSDocument(objId, objType, -1, -1);
  }

  /// <summary>Печатать конструкторский документа</summary>
  /// <param name="objId">Идентификатор объекта документа или изделия, на который выпущен документ</param>
  /// <param name="objType">Тип объекта</param>
  /// <param name="fileAttributeID">Идентификатор файлового атрибута объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  public void PrintAVSDocument(long objId, int objType, int fileAttributeID, int fileIndex)
  {
    try
    {
      long num = -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (objType.IsUndefinedTypeId())
          objType = sessionKeeper.Session.GetObjectInfo(objId).ObjectTypeID;
        num = !MetaDataHelper.IsObjectTypeChildOf(objType, AvsIDCache.ObjType_Document) ? AVSDocument.GetSpecificationIDForProduct(objId, sessionKeeper.Session) : objId;
      }
      if (!num.IsDefinedId())
        return;
      DocumentEditorPlugin.Instance.PrintImDocumentObject(num, fileAttributeID, fileIndex);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Вызвать диалог выбора колонок в TreeList</summary>
  public virtual void SelectGridCols()
  {
    AVSWindow activeAvsWindow = this.ActiveAVSWindow;
    if (activeAvsWindow == null)
      return;
    activeAvsWindow.SaveColumnsState();
    List<AvsRowAttributeInfo> gridViewColumns = activeAvsWindow.GetGridViewColumns();
    ColumnsCache.StartCache();
    try
    {
      List<AVSColumnScheme> avsColumnSchemeList = new List<AVSColumnScheme>();
      DocFieldsColumnsScheme fieldsColumnsScheme = new DocFieldsColumnsScheme((IEnumerable<Intermech.Interfaces.Attributes.AttributeInfo>) activeAvsWindow.AVSDocument.CollectDocumentRowAttrInfo());
      avsColumnSchemeList.Add((AVSColumnScheme) fieldsColumnsScheme);
      AvsVirtualAttributeColumnsScheme attributeColumnsScheme = new AvsVirtualAttributeColumnsScheme((IEnumerable<Intermech.Interfaces.Attributes.AttributeInfo>) activeAvsWindow.AVSDocument.GetVirtualAttributeList());
      avsColumnSchemeList.Add((AVSColumnScheme) attributeColumnsScheme);
      RelationColumnsScheme relationColumnsScheme = new RelationColumnsScheme();
      List<int> typesUsedInDocument = activeAvsWindow.AVSDocument.GetRelationTypesUsedInDocument();
      relationColumnsScheme.AddRelationTypes((IList<int>) typesUsedInDocument);
      avsColumnSchemeList.Add((AVSColumnScheme) relationColumnsScheme);
      ObjectColumnsScheme objectColumnsScheme = new ObjectColumnsScheme();
      List<int> objectTypeIDs = new List<int>();
      foreach (int relTypeID in typesUsedInDocument)
        objectTypeIDs.AddRange((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(AvsIDCache.ObjType_AssemblyUnit, relTypeID));
      objectColumnsScheme.AddObjectTypes((IList<int>) objectTypeIDs);
      avsColumnSchemeList.Add((AVSColumnScheme) objectColumnsScheme);
      NodeColumnCollection columnCollection = new NodeColumnCollection();
      foreach (AVSColumnScheme scheme in avsColumnSchemeList)
      {
        if (AVSPlugin.IColumnSchemes != null)
        {
          AVSPlugin.IColumnSchemes.Unregister(scheme.SchemeGuid);
          AVSPlugin.IColumnSchemes.Register(scheme.SchemeGuid, (INodeColumnScheme) scheme);
        }
        ReadOnlyCollection<object> possibleAttributesIds = scheme.PossibleAttributesIDs;
        List<NodeColumn> collection = new List<NodeColumn>();
        foreach (object columnID in possibleAttributesIds)
          collection.Add(scheme.CreateColumn(scheme.SchemeGuid, columnID));
        columnCollection.AddRange((IEnumerable<NodeColumn>) collection);
      }
      Helper.AddAllColumns(columnCollection);
      Helper.AddAllColumnsRelation(columnCollection);
      Dictionary<int, Guid> dictionary = new Dictionary<int, Guid>(gridViewColumns.Count);
      NodeColumnCollection columns = new NodeColumnCollection();
      object obj = (object) null;
      foreach (AvsRowAttributeInfo info in gridViewColumns)
      {
        NodeColumn nodeColumn1 = (fieldsColumnsScheme.GetColumn((INodeColumnSource) info) ?? attributeColumnsScheme.GetColumn((INodeColumnSource) info)) ?? (!info.IsRelationAttribute ? objectColumnsScheme.GetColumnByAttributeID((object) info.AttributeId) : relationColumnsScheme.GetColumnByAttributeID((object) info.AttributeId));
        if (nodeColumn1 == null)
        {
          NodeColumn[] byAttrId = columnCollection.FindByAttrID(info.AttributeId);
          if (byAttrId != null)
          {
            foreach (NodeColumn col in byAttrId)
            {
              if (info.IsRelationAttribute)
              {
                if (this.IsRelationNodeColumn(col))
                  nodeColumn1 = col;
              }
              else if (!this.IsRelationNodeColumn(col))
                nodeColumn1 = col;
            }
          }
        }
        if (nodeColumn1 != null && (obj == null || !obj.Equals(nodeColumn1.ID)))
        {
          if (!dictionary.ContainsKey(info.AttributeId))
            dictionary[info.AttributeId] = info.AttributeGuid;
          AVSColumn columnById = activeAvsWindow.GetColumnByID(info.AttributeGuid);
          if (columnById != null)
          {
            nodeColumn1.Width = columnById.Width;
            if (nodeColumn1.Attribute != null)
            {
              NodeColumn[] byAttrId = columnCollection.FindByAttrID(nodeColumn1.Attribute.AttributeGuid);
              if (byAttrId != null)
              {
                foreach (NodeColumn nodeColumn2 in byAttrId)
                  nodeColumn2.Width = nodeColumn1.Width;
              }
            }
            obj = nodeColumn1.ID;
            columns.Add(nodeColumn1);
          }
        }
        if (nodeColumn1 != null)
        {
          nodeColumn1.Width = info.TableViewColumnWidth;
          if (nodeColumn1.Attribute != null)
          {
            NodeColumn[] byAttrId = columnCollection.FindByAttrID(nodeColumn1.Attribute.AttributeGuid);
            if (byAttrId != null)
            {
              foreach (NodeColumn nodeColumn3 in byAttrId)
                nodeColumn3.Width = nodeColumn1.Width;
            }
          }
        }
      }
      List<AvsRowAttributeInfo> rowAttributeInfoList = new List<AvsRowAttributeInfo>();
      rowAttributeInfoList.AddRange((IEnumerable<AvsRowAttributeInfo>) gridViewColumns);
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      foreach (AVSWindow avsWindow in avsWindowsList)
        avsWindow.LockTreeColumnsSave();
      try
      {
        if (AppearanceTuningForm.Execute((INode) null, ContentType.None, columnCollection, columns) != DialogResult.OK)
          return;
        bool flag1 = false;
        activeAvsWindow.LockVisualUpdates();
        try
        {
          gridViewColumns.Clear();
          activeAvsWindow.virtualTree.ClearColumns();
          if (columns.Count <= 0)
            return;
          foreach (NodeColumn nodeColumn in (List<NodeColumn>) columns)
          {
            if (nodeColumn.ID is int && (int) nodeColumn.ID == AvsIDCache.Attr_Count)
            {
              flag1 = true;
              break;
            }
          }
          bool flag2 = flag1 && (this.ActiveAVSWindow.AVSDocument.IsFormB || this.ActiveAVSWindow.AVSDocument.AvsDocumentForm == AVSDocumentForm.V);
          foreach (NodeColumn col in (List<NodeColumn>) columns)
          {
            AvsRowAttributeInfo newSpecRowAttributeInfo = (fieldsColumnsScheme.FindAttribute((int) col.ID) ?? attributeColumnsScheme.FindAttribute((int) col.ID)) as AvsRowAttributeInfo;
            if (newSpecRowAttributeInfo == null)
            {
              Guid attributeGuidById;
              if (dictionary.ContainsKey((int) col.ID))
              {
                attributeGuidById = dictionary[(int) col.ID];
              }
              else
              {
                attributeGuidById = DBHelper.GetAttributeGuidByID((int) col.ID);
                dictionary[(int) col.ID] = attributeGuidById;
              }
              if (!(attributeGuidById == Guid.Empty))
              {
                bool flag3 = this.IsRelationNodeColumn(col);
                if (flag2 && (int) col.ID == AvsIDCache.Attr_Count)
                {
                  int count = this.ActiveAVSWindow.AVSDocument.productsInfo.Count;
                }
                newSpecRowAttributeInfo = new AvsRowAttributeInfo(flag3 ? FieldSource.Relation : FieldSource.Object, attributeGuidById, (int) col.ID, col.Caption);
              }
              else
                continue;
            }
            AvsRowAttributeInfo rowAttributeInfo = rowAttributeInfoList.Find((Predicate<AvsRowAttributeInfo>) (x => x.AttributeId == newSpecRowAttributeInfo.AttributeId));
            if (rowAttributeInfo != null)
              newSpecRowAttributeInfo.Pinned = rowAttributeInfo.Pinned;
            newSpecRowAttributeInfo.TableViewColumnWidth = col.Width;
            gridViewColumns.Add(newSpecRowAttributeInfo);
          }
          this.UpdateGridViewCols(gridViewColumns);
        }
        finally
        {
          activeAvsWindow.UnlockDocumentUpdates(true);
        }
      }
      finally
      {
        if (AVSPlugin.IColumnSchemes != null)
        {
          foreach (AVSColumnScheme avsColumnScheme in avsColumnSchemeList)
            AVSPlugin.IColumnSchemes.Unregister(avsColumnScheme.SchemeGuid);
        }
        foreach (AVSWindow avsWindow in avsWindowsList)
          avsWindow.UnlockTreeColumnsSave();
        activeAvsWindow.SaveColumnsState();
      }
    }
    finally
    {
      ColumnsCache.FinishCache();
    }
  }

  /// <summary>Колонка является атрибутом связи</summary>
  /// <param name="col"></param>
  /// <returns></returns>
  private bool IsRelationNodeColumn(NodeColumn col)
  {
    return col != null && AVSPlugin.IColumnSchemes?[col.SchemeGuid] is RelationColumnsScheme;
  }

  /// <summary>Окна документов в dockManager клиента</summary>
  protected DockControl[] Documents
  {
    [DebuggerStepThrough] get
    {
      return AVSPlugin.DockManager != null && AVSPlugin.DockManager.DocumentContainer != null ? AVSPlugin.DockManager.DocumentContainer.Documents : new DockControl[0];
    }
  }

  /// <summary>Обновить колонки в TreeList всех AVSWindow клиента</summary>
  /// <param name="gridViewCols">Столбцы табличного вида</param>
  protected void UpdateGridViewCols(List<AvsRowAttributeInfo> gridViewCols)
  {
    foreach (DockControl document in this.Documents)
    {
      if (document is AVSWindow avsWindow)
        avsWindow.UpdateGridViewCols();
    }
  }

  /// <summary>Получить форму сортировки для шаблона</summary>
  /// <param name="doc"></param>
  /// <returns></returns>
  public static FormSetupSorting GetTemplateSetupSorting(
    ImDocument doc,
    long docId,
    int docType,
    long docTemplateId)
  {
    // ISSUE: unable to decompile the method.
  }

  /// <summary> Сервис событий </summary>
  public static INotificationService NotificationService
  {
    [DebuggerStepThrough] get
    {
      if (AVSPlugin._notificationService == null)
        AVSPlugin._notificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      return AVSPlugin._notificationService;
    }
  }

  /// <summary> Получить список всех открытых окон AVS-а </summary>
  public List<AVSWindow> GetAVSWindowsList()
  {
    if (AVSPlugin.DockManager == null || AVSPlugin.DockManager.DocumentContainer == null || AVSPlugin.DockManager.DocumentContainer.Documents == null)
      return new List<AVSWindow>();
    List<AVSWindow> avsWindowsList = new List<AVSWindow>(AVSPlugin.DockManager.DocumentContainer.Documents.Length);
    foreach (DockControl document in AVSPlugin.DockManager.DocumentContainer.Documents)
    {
      if (document != null && document is AVSWindow)
        avsWindowsList.Add((AVSWindow) document);
    }
    return avsWindowsList;
  }

  /// <summary> При изменении допустимых замен </summary>
  private void iFiltrationService_OnFiltrationChanged(
    IFiltrationSettings NewFiltration,
    bool FiltrationValid)
  {
  }

  /// <summary>При изменении допустимых замен </summary>
  public void SubstitutesChangedHandler(object sender, NotificationEventArgs e)
  {
  }

  /// <summary>Объект был взят на изменение </summary>
  public void ObjectWasCheckedOutHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBObjectsEventArgs objectsEventArgs) || avsWindowsList.Count <= 0 || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      if (objectsEventArgs is DBObjectsCheckOutEventArgs checkOutEventArgs)
      {
        for (int index = 0; index < checkOutEventArgs.NewObjectIDs.Count; ++index)
        {
          foreach (AVSWindow avsWindow in avsWindowsList)
          {
            if (!avsWindow.ReadOnly && avsWindow.AVSDocument != sender)
              avsWindow.ObjectWasCheckedOut(checkOutEventArgs.ObjectIDs[index], checkOutEventArgs.NewObjectIDs[index]);
          }
        }
      }
      else
      {
        for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
        {
          long oldObjectID = objectsEventArgs.ObjectIDs[index];
          long newObjectID;
          if (oldObjectID > 0L)
          {
            newObjectID = -oldObjectID;
          }
          else
          {
            newObjectID = oldObjectID;
            oldObjectID = -newObjectID;
          }
          foreach (AVSWindow avsWindow in avsWindowsList)
          {
            if (!avsWindow.ReadOnly && avsWindow.AVSDocument != sender)
              avsWindow.ObjectWasCheckedOut(oldObjectID, newObjectID);
          }
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Объект был возвращён в архив </summary>
  public void ObjectWasCheckedInHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBObjectsEventArgs objectsEventArgs) || avsWindowsList.Count <= 0 || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      if (objectsEventArgs is DBObjectsCheckOutEventArgs checkOutEventArgs)
      {
        for (int index = 0; index < checkOutEventArgs.NewObjectIDs.Count; ++index)
        {
          foreach (AVSWindow avsWindow in avsWindowsList)
          {
            if (!avsWindow.ReadOnly && avsWindow.AVSDocument != sender)
              avsWindow.ObjectWasCheckedIn(checkOutEventArgs.ObjectIDs[index], checkOutEventArgs.NewObjectIDs[index]);
          }
        }
      }
      else
      {
        for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
        {
          long oldObjectID = objectsEventArgs.ObjectIDs[index];
          long newObjectID;
          if (oldObjectID < 0L)
          {
            newObjectID = -oldObjectID;
          }
          else
          {
            newObjectID = oldObjectID;
            oldObjectID = -newObjectID;
          }
          foreach (AVSWindow avsWindow in avsWindowsList)
          {
            if (!avsWindow.ReadOnly && avsWindow.AVSDocument != sender)
              avsWindow.ObjectWasCheckedIn(oldObjectID, newObjectID);
          }
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Изменения объекта были отменены </summary>
  public void ObjectChangesWasCanceledHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      if (e is DBObjectsExtendedEventArgs extendedEventArgs && MetaDataHelper.IsObjectTypeChildOf(extendedEventArgs.ObjectType, AvsIDCache.ObjType_SpecificationSection))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.UpdateCacheSpecSections(sessionKeeper.Session, extendedEventArgs.ObjectIDs);
      }
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBObjectsEventArgs objectsEventArgs) || avsWindowsList.Count <= 0 || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      if (objectsEventArgs is DBObjectsCheckOutEventArgs checkOutEventArgs)
      {
        for (int index = 0; index < checkOutEventArgs.NewObjectIDs.Count; ++index)
        {
          foreach (AVSWindow avsWindow in avsWindowsList)
          {
            if (!avsWindow.ReadOnly && avsWindow.AVSDocument != sender)
              avsWindow.ObjectChangesWasCanceled(checkOutEventArgs.ObjectIDs[index], checkOutEventArgs.NewObjectIDs[index]);
          }
        }
      }
      else
      {
        for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
        {
          long oldObjectID = objectsEventArgs.ObjectIDs[index];
          long newObjectID;
          if (oldObjectID < 0L)
          {
            newObjectID = -oldObjectID;
          }
          else
          {
            newObjectID = oldObjectID;
            oldObjectID = -newObjectID;
          }
          foreach (AVSWindow avsWindow in avsWindowsList)
          {
            if (!avsWindow.ReadOnly && avsWindow.AVSDocument != sender)
              avsWindow.ObjectChangesWasCanceled(oldObjectID, newObjectID);
          }
        }
      }
      foreach (AVSWindow avsWindow in avsWindowsList)
        avsWindow.AVSDocument.UpdateNotificationObjectsData(e);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события создания объектов</summary>
  public void ObjectsWasCreatedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      if (e is DBObjectsEventArgs objectsEventArgs1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          List<long> longList = new List<long>(objectsEventArgs1.ObjectIDs.Count);
          for (int index = 0; index < objectsEventArgs1.ObjectIDs.Count; ++index)
          {
            int objectTypeId = objectsEventArgs1.ObjectTypeIDs[index];
            switch (objectTypeId)
            {
              case -1:
              case 0:
                objectTypeId = sessionKeeper.Session.GetObjectInfo(objectsEventArgs1.ObjectIDs[index]).ObjectTypeID;
                break;
            }
            if (MetaDataHelper.IsObjectTypeChildOf(objectTypeId, AvsIDCache.ObjType_SpecificationSection))
              longList.Add(objectsEventArgs1.ObjectIDs[index]);
          }
          if (longList.Count > 0)
            SpecificationSectionInfo.UpdateCacheSpecSections(sessionKeeper.Session, (IList<long>) longList.ToArray());
        }
      }
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBObjectsEventArgs objectsEventArgs2) || avsWindowsList.Count <= 0 || objectsEventArgs2.ObjectIDs == null || objectsEventArgs2.ObjectIDs.Count <= 0)
        return;
      foreach (AVSWindow avsWindow in avsWindowsList)
      {
        if (avsWindow.AVSDocument != null)
          avsWindow.AVSDocument.UpdateNotificationObjectsData(e);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события обновления объектов </summary>
  public void ObjectsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      if (e is DBObjectsExtendedEventArgs extendedEventArgs && MetaDataHelper.IsObjectTypeChildOf(extendedEventArgs.ObjectType, AvsIDCache.ObjType_SpecificationSection))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long[] sectionsID = new long[extendedEventArgs.ObjectIDs.Count];
          for (int index = 0; index < sectionsID.Length; ++index)
            sectionsID[index] = extendedEventArgs.ObjectIDs[index];
          SpecificationSectionInfo.UpdateCacheSpecSections(sessionKeeper.Session, (IList<long>) sectionsID);
        }
      }
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBObjectsEventArgs objectsEventArgs) || avsWindowsList.Count <= 0 || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
        return;
      foreach (AVSWindow avsWindow in avsWindowsList)
      {
        if (avsWindow.AVSDocument != null && !avsWindow.IsSuspended_ObjectsWasChangedHandler)
        {
          if (!avsWindow.ReadOnly && sender != avsWindow.AVSDocument)
            avsWindow.AVSDocument.ReloadObjectsAttributesFromDB(objectsEventArgs.ObjectIDs);
          if (avsWindow.AVSDocument.variableDataChapter_FormA != null)
            avsWindow.AVSDocument.variableDataChapter_FormA.UpdateNotificationData(e);
          avsWindow.AVSDocument.UpdateNotificationObjectsData(e);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события обновления связей </summary>
  public void RelationsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBRelationsEventArgs relationsEventArgs) || avsWindowsList.Count <= 0 || relationsEventArgs.RelationIDs == null || relationsEventArgs.RelationIDs.Count <= 0)
        return;
      foreach (AVSWindow avsWindow in avsWindowsList)
      {
        if (!avsWindow.ReadOnly && sender != avsWindow.AVSDocument)
          avsWindow.RelationsWasChangedHandler(relationsEventArgs.RelationIDs);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события создания связей </summary>
  public void RelationsWasCreatedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBRelationsEventArgs relationsEventArgs) || avsWindowsList.Count <= 0)
        return;
      DBRelationsEventArgsFromForm eventArgsFromForm = e as DBRelationsEventArgsFromForm;
      if (relationsEventArgs.RelationIDs == null || relationsEventArgs.RelationIDs.Count <= 0)
        return;
      foreach (AVSWindow avsWindow in avsWindowsList)
      {
        if (!avsWindow.ReadOnly && sender != avsWindow.AVSDocument && (eventArgsFromForm == null || !avsWindow.Suspended_DBRelationsEventArgsFromForm))
        {
          Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            for (int index = 0; index < relationsEventArgs.RelationIDs.Count; ++index)
            {
              if (!relationsEventArgs.RelationIDs[index].IsUndefinedId())
              {
                int relationType = relationsEventArgs.GetRelationType(relationsEventArgs.RelationIDs[index]);
                long projId = relationsEventArgs.GetProjID(relationsEventArgs.RelationIDs[index]);
                if (relationType.IsUndefinedTypeId() || projId.IsUndefinedId())
                {
                  IDBRelation relation = sessionKeeper.Session.GetRelation(relationsEventArgs.RelationIDs[index], false);
                  if (relation != null)
                  {
                    relationType = relation.RelationType;
                    projId = relation.ProjID;
                  }
                  else
                    continue;
                }
                if (avsWindow.AVSDocument.ContainsProduct(projId))
                  AVSDocument.AddRelationToTypedDictionary(dictionary, relationType, relationsEventArgs.RelationIDs[index]);
              }
            }
          }
          if (dictionary.Count > 0)
            avsWindow.RelationsWasCreatedHandler(dictionary);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события удаления связей </summary>
  public void RelationsWasRemovedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBRelationsEventArgs relationsEventArgs) || relationsEventArgs.RelationIDs == null || relationsEventArgs.RelationIDs.Count <= 0 || avsWindowsList.Count <= 0)
        return;
      foreach (AVSWindow avsWindow in avsWindowsList)
      {
        if (!avsWindow.ReadOnly && sender != avsWindow.AVSDocument)
          avsWindow.AVSDocument.RemoveRelation_NotificationHandler(relationsEventArgs.RelationIDs);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события удаления объектов </summary>
  public void ObjectsWasRemovedHandler(object sender, NotificationEventArgs e)
  {
    try
    {
      List<AVSWindow> avsWindowsList = this.GetAVSWindowsList();
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0 || avsWindowsList.Count <= 0)
        return;
      foreach (AVSWindow avsWindow in avsWindowsList)
      {
        if (!avsWindow.ReadOnly && sender != avsWindow.AVSDocument && (objectsEventArgs.ObjectIDs.IndexOf(avsWindow.DocumentID) != -1 || avsWindow.AVSDocument.productsInfo.Count == 1 && objectsEventArgs.ObjectIDs.IndexOf(avsWindow.AVSDocument.productsInfo[0].Id) != -1))
        {
          avsWindow.DocumentDBObjectWasRemoved = true;
          avsWindow.Close();
          break;
        }
        if (avsWindow.AVSDocument != null)
        {
          avsWindow.AVSDocument.UpdateNotificationObjectsData(e);
          avsWindow.AVSDocument.RemoveObject_NotificationHandler(objectsEventArgs.ObjectIDs);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Имя плагина клиента</summary>
  public string Name
  {
    [DebuggerStepThrough] get => "Редактор конструкторских документов (AVS)";
  }

  /// <summary>Выполнить проверку наличия модуля "Intermech.Pdm.Server" на сервере приложений</summary>
  public void CheckPDMPlugins()
  {
    if (AVSPlugin._PDMSpecificationsService == null)
      AVSPlugin._PDMSpecificationsService = ServicesManager.GetService<IPDMSpecificationsService>(false);
    if (AVSPlugin._PDMSpecificationsService == null)
      throw new Exception("Не загружен модуль PDM, необходимый для полноценной работы AVS");
    this.CheckPDMSubstitutionCommands();
    if (AVSPlugin._pdmServerLoaded)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IPdmServerPlugin)) is IPdmServerPlugin))
        throw new Exception("Не загружен серверный модуль PDM, необходимый для полноценной работы AVS");
      AVSPlugin._pdmServerLoaded = true;
    }
  }

  /// <summary>
  /// Проверить меню допзамен и, если надо, дозагрузить иконки из ресурсов плагина PDM
  /// </summary>
  private void CheckPDMSubstitutionCommands()
  {
    BarManager service = (BarManager) AVSPlugin.serviceProvider.GetService(typeof (BarManager));
    this.iNamedImageList = this.iNamedImageList ?? AVSPlugin.serviceProvider.GetService(typeof (INamedImageList)) as INamedImageList;
    MenuBarItem menuBarItem = service.MenuBar.Items.OfType<MenuBarItem>().FirstOrDefault<MenuBarItem>((System.Func<MenuBarItem, bool>) (i => i.CommandName == "AVS"));
    if (menuBarItem == null)
      return;
    if (menuBarItem.Items.OfType<MenuItemBase>().FirstOrDefault<MenuItemBase>((System.Func<MenuItemBase, bool>) (i => i is MenuButtonItem menuButtonItem1 && menuButtonItem1.CommandName == "PDM.CreateSubstitutesGroup")) is MenuButtonItem menuButtonItem2)
    {
      if (menuButtonItem2.Image != null)
        return;
      menuButtonItem2.Image = this.iNamedImageList?.ImageList.Images[this.iNamedImageList.ImageIndex("icoCreateSubstitutesGroup.PDM")];
      MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("PDM.CreateSubstitutesGroup");
      if (contextMenuItem != null)
        contextMenuItem.Image = menuButtonItem2.Image;
    }
    if (menuBarItem.Items.OfType<MenuItemBase>().FirstOrDefault<MenuItemBase>((System.Func<MenuItemBase, bool>) (i => i is MenuButtonItem menuButtonItem3 && menuButtonItem3.CommandName == "PDM.MakeActualSubstitute")) is MenuButtonItem menuButtonItem4)
    {
      menuButtonItem4.Image = this.iNamedImageList?.ImageList.Images[this.iNamedImageList.ImageIndex("icoMakeActualSubstitute.PDM")];
      MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("PDM.MakeActualSubstitute");
      if (contextMenuItem != null)
        contextMenuItem.Image = menuButtonItem4.Image;
    }
    if (menuBarItem.Items.OfType<MenuItemBase>().FirstOrDefault<MenuItemBase>((System.Func<MenuItemBase, bool>) (i => i is MenuButtonItem menuButtonItem5 && menuButtonItem5.CommandName == "PDM.EditSubstitutesGroup")) is MenuButtonItem menuButtonItem6)
    {
      menuButtonItem6.Image = this.iNamedImageList?.ImageList.Images[this.iNamedImageList.ImageIndex("icoEditSubstitutesGroup.PDM")];
      MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("PDM.EditSubstitutesGroup");
      if (contextMenuItem != null)
        contextMenuItem.Image = menuButtonItem6.Image;
    }
    if (!(menuBarItem.Items.OfType<MenuItemBase>().FirstOrDefault<MenuItemBase>((System.Func<MenuItemBase, bool>) (i => i is MenuButtonItem menuButtonItem7 && menuButtonItem7.CommandName == "PDM.DeleteSubstitutesGroup")) is MenuButtonItem menuButtonItem8))
      return;
    menuButtonItem8.Image = this.iNamedImageList?.ImageList.Images[this.iNamedImageList.ImageIndex("icoDeleteSubstitutesGroup.PDM")];
    MenuButtonItem contextMenuItem1 = NodeContextMenu.GetContextMenuItem("PDM.DeleteSubstitutesGroup");
    if (contextMenuItem1 == null)
      return;
    contextMenuItem1.Image = menuButtonItem8.Image;
  }

  /// <summary>
  /// Зарегистрировать все закладки, добавляемые модулем расширения в Навигатор
  /// </summary>
  internal void RegisterViews()
  {
    AdjustableViewsHelper.RegisterView("AVSRowFormatPanel", "Форматирование", "Форматирование строки спецификации", "Intermech.AVS", "", true, 15);
    AdjustableViewsHelper.RegisterView("ProductPropsUserControl", "Свойства исполнения", "Свойства исполнения", "Intermech.AVS", "", true, 15);
    AdjustableViewsHelper.RegisterView("AVS.ArticleWithDocView", "Запись спецификации", "Свойства записи спецификации", "Intermech.AVS", "", true, 15);
    AdjustableViewsHelper.RegisterView("AVS.DocumentTypesWeights", "Приоритеты типов документов", "Закладка позволяет указывать приоритеты размещения типов документов в спецификациях", "Intermech.AVS", "", true, 15);
    AdjustableViewsHelper.RegisterView("AVS.RemarkAttributes", "Атрибуты в примечаниях", "Закладка позволяет указывать приоритеты размещения типов документов в спецификациях", "Intermech.AVS", "", true, 16 /*0x10*/);
  }

  /// <summary>Загрузить плагин</summary>
  /// <param name="serviceProvider">Провайдер сервисов клиента</param>
  public void Load(System.IServiceProvider serviceProvider)
  {
    IPluginManager service1 = serviceProvider.GetService<IPluginManager>();
    service1.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    try
    {
      AVSPlugin.serviceProvider = serviceProvider;
      if (AVSPlugin.dockManager == null)
        AVSPlugin.dockManager = (DockManager) serviceProvider.GetService(typeof (DockManager));
      AVSVisualizer.Initialize(serviceProvider);
      ExactSpecificationVisualizer.Initialize(serviceProvider);
      this.iNamedImageList = this.iNamedImageList ?? serviceProvider.GetService(typeof (INamedImageList)) as INamedImageList;
      IPropertyPagesService service2 = (IPropertyPagesService) serviceProvider.GetService(typeof (IPropertyPagesService));
      if (service2 != null)
      {
        service2.AddPage(AvsConfig.General.PageName, (IPropertyPage) AvsConfig.General);
        service2.AddPage(AvsConfig.Podbor.PageName, (IPropertyPage) AvsConfig.Podbor);
        service2.AddPage(AvsConfig.PositionDesignation.PageName, (IPropertyPage) AvsConfig.PositionDesignation);
        service2.AddPage(AvsConfig.CheckSP.PageName, (IPropertyPage) AvsConfig.CheckSP);
        service2.AddPage(AvsConfig.CheckEL.PageName, (IPropertyPage) AvsConfig.CheckEL);
      }
      AVSPlugin._iImportStructureFromCadService = (IImportStructureFromCadService) new ImportStructureFromCadService();
      ServicesManager.AddService(typeof (IImportStructureFromCadService), (object) AVSPlugin._iImportStructureFromCadService);
      ServicesManager.AddService(typeof (IECADIntegratorsDocumentService), (object) new ECADIntegratorsDocumentService());
      ServicesManager.AddService(typeof (IElementListCreatorService), (object) new ElementListCreatorService());
      ServicesManager.AddService(typeof (IAVSClientService), (object) new AVSClientService());
      IPreviewExtender service3 = (IPreviewExtender) serviceProvider.GetService(typeof (IPreviewExtender));
      if (service3 != null)
        service3.Extend += new ExtendEventHandler(this.previewExtender_Extend);
      IAuthFilesService service4 = ServicesManager.GetService<IAuthFilesService>(false);
      if (service4 != null)
        service4.AuthFileAssignEvent += new AuthFileAssignEventHandler(this.AuthFilesService_AuthFileAssignEvent);
      IDefaultCommands4ObjTypes service5 = (IDefaultCommands4ObjTypes) ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes));
      if (service5 != null)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid("cad00580-306c-11d8-b4e9-00304f19f545"));
        if (objectType != null)
          service5.AddDefaultCommand(objectType.ObjectTypeID, "EditDocument", DefaultCommandHandler.ContectMenu);
      }
      AVSIntegrator integrator = new AVSIntegrator();
      integrator.Initialize();
      ClientContext.Integrators.RegisterIntegrator((IIntegrator) integrator);
      AVSLaunchHandler handler = new AVSLaunchHandler(integrator);
      ClientContext.LaunchActions.RegisterHandler((ILaunchHandler) handler);
      DocumentEditorPlugin.Instance.SpecialDocumentLaunchHandlers.Add(handler.Id);
      if (AVSPlugin.NotificationService != null)
      {
        AVSPlugin.NotificationService.Subscribe("ObjectsCreated", new NotificationEventHandler(this.ObjectsWasCreatedHandler));
        AVSPlugin.NotificationService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
        AVSPlugin.NotificationService.Subscribe("ObjectsRemoved", new NotificationEventHandler(this.ObjectsWasRemovedHandler));
        AVSPlugin.NotificationService.Subscribe("RelationsChanged", new NotificationEventHandler(this.RelationsWasChangedHandler));
        AVSPlugin.NotificationService.Subscribe("RelationsCreated", new NotificationEventHandler(this.RelationsWasCreatedHandler));
        AVSPlugin.NotificationService.Subscribe("RelationsRemoved", new NotificationEventHandler(this.RelationsWasRemovedHandler));
        AVSPlugin.NotificationService.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.ObjectWasCheckedOutHandler));
        AVSPlugin.NotificationService.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.ObjectWasCheckedInHandler));
        AVSPlugin.NotificationService.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectChangesWasCanceledHandler));
      }
      service1.Load(typeof (DocumentEditorPlugin).Assembly.Location);
      DocumentPlugin.InitDocumentPlugin();
      DocumentEditorPlugin.InitDocumentPlugin();
      DocumentEditorPlugin.AfterLoadDocument += new AfterLoadDocumentEventHandler(this.DocumentEditorPlugin_AfterLoadDocument);
      AvsIDCache.InitTypeNameDictionary();
      this.commandManager = (ICommandManager) serviceProvider.GetService(typeof (ICommandManager));
      if (this.commandManager == null)
        this.commandManager = (ICommandManager) new Intermech.Bars.CommandManager();
      MenuBar menuBar1 = ((BarManager) serviceProvider.GetService(typeof (BarManager))).MenuBar;
      MenuButtonItem menuButtonItem1 = (MenuButtonItem) null;
      this.imageList = menuBar1.ImageList;
      if (AVSPlugin.dockManager == null)
        AVSPlugin.dockManager = (DockManager) serviceProvider.GetService(typeof (DockManager));
      AVSPlugin.dockManager.DocumentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.DockManagerActiveDocumentChanged);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        try
        {
          AVSDocumentsSettings.Instance.LoadFromDB(sessionKeeper.Session);
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
        if (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service6 && service6.GetDescriber(AvsIDCache.Attr_PossibleTypes) == null)
          service6.RegisterDescriber(AvsIDCache.Attr_PossibleTypes, (IAttributePropertyDescriber) new ObjectTypeAttDescriber());
        try
        {
          AVSPlugin.avsServerService = (IAVSServerService) sessionKeeper.Session.GetCustomService(typeof (IAVSServerService));
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
          throw;
        }
      }
      if (AVSPlugin.IObjectCreatorService != null)
      {
        foreach (int aObjectTypeID in AVSPlugin.ObjTypes_Specification)
          AVSPlugin.IObjectCreatorService.RegisterCreatorCustomService(aObjectTypeID, typeof (NewSpecificationFormService));
      }
      AVSPlugin.imbaseSelector = serviceProvider.GetService(typeof (IImbaseSelector)) as IImbaseSelector;
      IFactory service7 = (IFactory) serviceProvider.GetService(typeof (IFactory));
      service7.AddViewsProvider(1, (IViewsProvider) new AVSPartViewsProvider());
      service7.AddViewsProvider(1, (IViewsProvider) new AVSTemplatesViewsProvider());
      service7.AddViewsProvider(1, AvsIDCache.ObjType_ConstructorDocumentTemplate, (IViewsProvider) new DocumentTypesWeightsEditorViewProvider());
      service7.AddViewsProvider(1, (IViewsProvider) new AVSTemplatesViewsProvider());
      service7.AddCommandsProvider(1, AvsIDCache.ObjType_Specification, (ICommandsProvider) this);
      service7.AddCommandsProvider(1, AvsIDCache.ObjType_Document, (ICommandsProvider) this);
      service7.AddCommandsProvider(1, AvsIDCache.ObjType_AssemblyUnit, (ICommandsProvider) this);
      VedomostVBProvider provider = new VedomostVBProvider();
      service7.AddCommandsProvider(1, AvsIDCache.ObjType_Specification, (ICommandsProvider) provider);
      MenuTemplate contextMenuTemplate = service7.ContextMenuTemplate;
      MenuTemplateNode menuTemplateNode1 = contextMenuTemplate.Nodes.FirstOrDefault<MenuTemplateNode>((System.Func<MenuTemplateNode, bool>) (x => x.Name == "Reports"));
      if (menuTemplateNode1 != null)
      {
        MenuTemplateNode node = new MenuTemplateNode("CreaveVedomostVB", "Создать конструкторскую ведомость", -1, 30, 31 /*0x1F*/);
        menuTemplateNode1.Nodes.Add(node);
      }
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ClearDocument", "Очистить конструкторский документ", -1, 30, 31 /*0x1F*/));
      service7.AddCommandsProvider(1, AvsIDCache.ObjType_AssemblyUnit, (ICommandsProvider) provider);
      this.commandManager.AddTarget((ICommandTarget) this);
      string str1 = "Intermech.Document.Model.Resources.";
      string str2 = "Intermech.AVS.Resources.";
      DocumentMenuHelper.CreateMenuCommands(this.commandManager);
      menuBar1.FindMenuBar("File");
      ICategoryTypeIconService service8 = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      MenuItemBase menuItem1 = menuBar1.FindMenuItem("File.New");
      if (menuItem1 != null)
      {
        MenuButtonItem menuItem2 = DocumentMenuHelper.CreateMenuItem("New.AVSDocument", "Спецификация", "", false, false, this.commandManager);
        if (service8 != null && service8.IndexOf(4, AvsIDCache.ObjType_Specification) >= 0)
          menuItem2.Icon = service8.GetIcon(4, AvsIDCache.ObjType_Specification);
        menuItem1.Items.Add((ToolbarItemBase) menuItem2);
      }
      MenuBarItem menuBar2 = menuBar1.FindMenuBar("View");
      DocumentMenuHelper.CreateMenuItem("AVS.NavigatorCommands", "Команды &навигатора", "Команда навигатора", true, true, this.commandManager);
      int num1 = 0;
      MenuButtonItem menuItem3 = DocumentMenuHelper.CreateMenuItem("AVS.PageViewMode", "Страничный вид", "", true, false, this.commandManager);
      MenuItemBase.MenuItemCollection items1 = menuBar2.Items;
      int index1 = num1;
      int num2 = index1 + 1;
      MenuButtonItem menuButtonItem2 = menuItem3;
      items1.Insert(index1, (ToolbarItemBase) menuButtonItem2);
      MenuButtonItem menuItem4 = DocumentMenuHelper.CreateMenuItem("AVS.GridViewMode", "Табличный вид", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items2 = menuBar2.Items;
      int index2 = num2;
      int num3 = index2 + 1;
      MenuButtonItem menuButtonItem3 = menuItem4;
      items2.Insert(index2, (ToolbarItemBase) menuButtonItem3);
      MenuButtonItem menuItem5 = DocumentMenuHelper.CreateMenuItem("AVS.SelectGridColumns", "Настройка табличного вида...", "", "imgViewSettings", true, true, this.commandManager);
      MenuItemBase.MenuItemCollection items3 = menuBar2.Items;
      int index3 = num3;
      int num4 = index3 + 1;
      MenuButtonItem menuButtonItem4 = menuItem5;
      items3.Insert(index3, (ToolbarItemBase) menuButtonItem4);
      MenuButtonItem menuItem6 = DocumentMenuHelper.CreateMenuItem("AVS.DocumentProperty", "Свойства документа...", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items4 = menuBar2.Items;
      int index4 = num4;
      int num5 = index4 + 1;
      MenuButtonItem menuButtonItem5 = menuItem6;
      items4.Insert(index4, (ToolbarItemBase) menuButtonItem5);
      MenuButtonItem menuItem7 = DocumentMenuHelper.CreateMenuItem("AVS.AssemblyProperty", "Свойства исполнения...", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items5 = menuBar2.Items;
      int index5 = num5;
      int num6 = index5 + 1;
      MenuButtonItem menuButtonItem6 = menuItem7;
      items5.Insert(index5, (ToolbarItemBase) menuButtonItem6);
      MenuItemBase menuBar3 = (MenuItemBase) menuBar1.FindMenuBar("Edit");
      int num7 = menuBar3.FindItem("Paste").Index + 1;
      MenuButtonItem menuItem8 = DocumentMenuHelper.CreateMenuItem("AVS.InsertTitlePage", "Вставить титульный лист", "Вставить титульный лист", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items6 = menuBar3.Items;
      int index6 = num7;
      int num8 = index6 + 1;
      MenuButtonItem menuButtonItem7 = menuItem8;
      items6.Insert(index6, (ToolbarItemBase) menuButtonItem7);
      MenuButtonItem menuItem9 = DocumentMenuHelper.CreateMenuItem("AVS.DeleteTitlePage", "Удалить титульный лист", "Удалить титульный лист", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items7 = menuBar3.Items;
      int index7 = num8;
      int num9 = index7 + 1;
      MenuButtonItem menuButtonItem8 = menuItem9;
      items7.Insert(index7, (ToolbarItemBase) menuButtonItem8);
      MenuButtonItem menuItem10 = DocumentMenuHelper.CreateMenuItem("AVS.PasteBreak", "Вставить разрыв строки", "Вставить разрыв строки", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items8 = menuBar3.Items;
      int index8 = num9;
      int num10 = index8 + 1;
      MenuButtonItem menuButtonItem9 = menuItem10;
      items8.Insert(index8, (ToolbarItemBase) menuButtonItem9);
      MenuButtonItem menuItem11 = DocumentMenuHelper.CreateMenuItem("AVS.PasteNonBreakSpace", "Вставить неразрывный пробел", "Вставить неразрывный пробел", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items9 = menuBar3.Items;
      int index9 = num10;
      int num11 = index9 + 1;
      MenuButtonItem menuButtonItem10 = menuItem11;
      items9.Insert(index9, (ToolbarItemBase) menuButtonItem10);
      MenuButtonItem menuItem12 = DocumentMenuHelper.CreateMenuItem("AVS.AddSkipLineBefore", "Пропустить строку перед записью", "Пропустить строку перед записью", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items10 = menuBar3.Items;
      int index10 = num11;
      int num12 = index10 + 1;
      MenuButtonItem menuButtonItem11 = menuItem12;
      items10.Insert(index10, (ToolbarItemBase) menuButtonItem11);
      MenuButtonItem menuItem13 = DocumentMenuHelper.CreateMenuItem("AVS.AddSkipLineAfter", "Пропустить строку после записи", "Пропустить строку после записи", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items11 = menuBar3.Items;
      int index11 = num12;
      int num13 = index11 + 1;
      MenuButtonItem menuButtonItem12 = menuItem13;
      items11.Insert(index11, (ToolbarItemBase) menuButtonItem12);
      MenuButtonItem menuItem14 = DocumentMenuHelper.CreateMenuItem("AVS.UndoSkipLineBefore", "Отменить пропуск строки перед записью", "Отменить пропуск строки перед записью", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items12 = menuBar3.Items;
      int index12 = num13;
      int num14 = index12 + 1;
      MenuButtonItem menuButtonItem13 = menuItem14;
      items12.Insert(index12, (ToolbarItemBase) menuButtonItem13);
      MenuButtonItem menuItem15 = DocumentMenuHelper.CreateMenuItem("AVS.UndoSkipLineAfter", "Отменить пропуск строки после записи", "Отменить пропуск строки после записи", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items13 = menuBar3.Items;
      int index13 = num14;
      int num15 = index13 + 1;
      MenuButtonItem menuButtonItem14 = menuItem15;
      items13.Insert(index13, (ToolbarItemBase) menuButtonItem14);
      MenuButtonItem menuItem16 = DocumentMenuHelper.CreateMenuItem("AVS.FromNewPage", "Начать с новой страницы", "Начать с новой страницы", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items14 = menuBar3.Items;
      int index14 = num15;
      int num16 = index14 + 1;
      MenuButtonItem menuButtonItem15 = menuItem16;
      items14.Insert(index14, (ToolbarItemBase) menuButtonItem15);
      MenuButtonItem menuItem17 = DocumentMenuHelper.CreateMenuItem("AVS.UndoFromNewPage", "Отменить вывод с новой страницы", "Отменить вывод с новой страницы", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items15 = menuBar3.Items;
      int index15 = num16;
      int num17 = index15 + 1;
      MenuButtonItem menuButtonItem16 = menuItem17;
      items15.Insert(index15, (ToolbarItemBase) menuButtonItem16);
      MenuButtonItem menuItem18 = DocumentMenuHelper.CreateMenuItem("AVS.InsertAdditionalPages", "Добавить дополнительный лист", "Добавить дополнительный лист", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items16 = menuBar3.Items;
      int index16 = num17;
      int num18 = index16 + 1;
      MenuButtonItem menuButtonItem17 = menuItem18;
      items16.Insert(index16, (ToolbarItemBase) menuButtonItem17);
      MenuButtonItem menuItem19 = DocumentMenuHelper.CreateMenuItem("AVS.RemoveAdditionalPages", "Удалить дополнительные листы", "Удалить дополнительные листы", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items17 = menuBar3.Items;
      int index17 = num18;
      int num19 = index17 + 1;
      MenuButtonItem menuButtonItem18 = menuItem19;
      items17.Insert(index17, (ToolbarItemBase) menuButtonItem18);
      MenuButtonItem menuItem20 = DocumentMenuHelper.CreateMenuItem("AVS.Hide", "Скрыть", "Скрыть", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items18 = menuBar3.Items;
      int index18 = num19;
      int num20 = index18 + 1;
      MenuButtonItem menuButtonItem19 = menuItem20;
      items18.Insert(index18, (ToolbarItemBase) menuButtonItem19);
      MenuButtonItem menuItem21 = DocumentMenuHelper.CreateMenuItem("AVS.UnHide", "Показать", "Показать", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items19 = menuBar3.Items;
      int index19 = num20;
      int num21 = index19 + 1;
      MenuButtonItem menuButtonItem20 = menuItem21;
      items19.Insert(index19, (ToolbarItemBase) menuButtonItem20);
      MenuButtonItem menuItem22 = DocumentMenuHelper.CreateMenuItem("AVS.Group", "Объединить", "Объединить", false, true, this.commandManager);
      MenuItemBase.MenuItemCollection items20 = menuBar3.Items;
      int index20 = num21;
      int num22 = index20 + 1;
      MenuButtonItem menuButtonItem21 = menuItem22;
      items20.Insert(index20, (ToolbarItemBase) menuButtonItem21);
      MenuButtonItem menuItem23 = DocumentMenuHelper.CreateMenuItem("AVS.SortBefore", "Привязать к следующей записи", "Привязать запись перед следующей за ней сейчас", true, false, this.commandManager);
      MenuItemBase.MenuItemCollection items21 = menuBar3.Items;
      int index21 = num22;
      int num23 = index21 + 1;
      MenuButtonItem menuButtonItem22 = menuItem23;
      items21.Insert(index21, (ToolbarItemBase) menuButtonItem22);
      MenuButtonItem menuItem24 = DocumentMenuHelper.CreateMenuItem("AVS.SortAfter", "Привязать к предыдущей записи", "Привязать запись после находящейся перед ней сейчас", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items22 = menuBar3.Items;
      int index22 = num23;
      int num24 = index22 + 1;
      MenuButtonItem menuButtonItem23 = menuItem24;
      items22.Insert(index22, (ToolbarItemBase) menuButtonItem23);
      MenuButtonItem menuItem25 = DocumentMenuHelper.CreateMenuItem("AVS.DisconnectSort", "Отвязать сортировку записи", "Сортировать запись независимо от других", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items23 = menuBar3.Items;
      int index23 = num24;
      int num25 = index23 + 1;
      MenuButtonItem menuButtonItem24 = menuItem25;
      items23.Insert(index23, (ToolbarItemBase) menuButtonItem24);
      MenuBarItem menuBarItem1 = new MenuBarItem("Документ");
      menuBarItem1.CommandName = "AVS";
      menuBarItem1.ToolTipText = "Команды редактирования конструкторского документа";
      menuBarItem1.Visible = false;
      int num26 = 0;
      ToolbarItemBaseCollection items24 = menuBar1.Items;
      int index24 = num26;
      num25 = index24 + 1;
      MenuBarItem menuBarItem2 = menuBarItem1;
      items24.Insert(index24, (ToolbarItemBase) menuBarItem2);
      this.commandManager.Add((ButtonItemBase) menuBarItem1);
      service7.ContextMenuTemplate["Create"]?.Nodes.Add(new MenuTemplateNode("CreateElementList", "Перечень элементов", -1, 10, 10));
      MenuTemplateNode menuTemplateNode2 = service7.ContextMenuTemplate["CheckIn"];
      if (menuTemplateNode2 != null && menuTemplateNode2.ImageIndex >= 0)
      {
        INamedImageList iNamedImageList = this.iNamedImageList;
        if (iNamedImageList != null)
        {
          Image image = iNamedImageList.ImageList.Images[menuTemplateNode2.ImageIndex];
        }
      }
      MenuButtonItem menuItem26 = DocumentMenuHelper.CreateMenuItem("AVS.AddNewSpecRow", "&Создать запись...", "Создать новое изделие или документ и добавить его в спецификацию", str1 + "CreateNewRecord_v70.png", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem26);
      MenuButtonItem menuItem27 = DocumentMenuHelper.CreateMenuItem("AVS.CreateVedomost_VB", "Создать конструкторскую ведомость...", "", true, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem27);
      MenuButtonItem menuItem28 = DocumentMenuHelper.CreateMenuItem("AVS.CreateDocumentFromFile_VB", "Создать документ из файла AVS6", "", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem28);
      MenuButtonItem menuItem29 = DocumentMenuHelper.CreateMenuItem("Expert.CreateVedomost", "Создать ведомость...", "", true, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem29);
      if (service8.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00196-306c-11d8-b4e9-00304f19f545"))) >= 0)
        menuItem29.Icon = service8.GetIcon(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00196-306c-11d8-b4e9-00304f19f545")));
      MenuButtonItem menuItem30 = DocumentMenuHelper.CreateMenuItem("AVS.CreateElementList", "Создать перечень элементов", "", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem30);
      if (service8.IndexOf(4, MetaDataHelper.GetObjectTypeID(AvsIDCache.ObjType_ElementList0Guid)) >= 0)
        menuItem30.Icon = service8.GetIcon(4, MetaDataHelper.GetObjectTypeID(AvsIDCache.ObjType_ElementList0Guid));
      MenuButtonItem menuItem31 = DocumentMenuHelper.CreateMenuItem("AVS.ReplaceSpecRow", "&Заменить изделие в записи...", "Заменить изделие в записи на другое изделие", "", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem31);
      MenuButtonItem menuItem32 = DocumentMenuHelper.CreateMenuItem("AVS.ReplaceSpecRowVersion", "&Заменить версию изделия в записи...", "Заменить версию изделия в записи", "", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem32);
      MenuButtonItem menuItem33 = DocumentMenuHelper.CreateMenuItem("AVS.ReplaceSpecRowFromImbase", "Заменить изделие в записи из IMBASE...", "Заменить изделие в записи на другое изделие из IMBASE", "", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem33);
      MenuButtonItem menuItem34 = DocumentMenuHelper.CreateMenuItem("AVS.ReplaceDocInSpecRow", "&Заменить документ в записи...", "Заменить документ в записи на другой документ", "", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem34);
      MenuButtonItem menuItem35 = DocumentMenuHelper.CreateMenuItem("AVS.AddSpecRow", "Добавить &существующий объект...", "Добавить уже существующее изделие или документ", str1 + "Insert-Object_v70.png", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem35);
      MenuButtonItem menuItem36 = DocumentMenuHelper.CreateMenuItem("AVS.AddDopComplect", "Добавить комплект, поставляемый отдельно...", "Добавить в спецификацию комплект, поставляемый отдельно", str1 + ".png", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem36);
      MenuButtonItem menuItem37 = DocumentMenuHelper.CreateMenuItem("AVS.AddSpecRowFromImbase", "Добавить &из IMBASE...", "Добавить изделия из IMBASE", str1 + "InsertFromImbase_v70.png", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem37);
      MenuButtonItem menuItem38 = DocumentMenuHelper.CreateMenuItem("AVS.AddGroupSpecRowFromImbase", "Групповой ввод записей из IMBASE...", "Добавить несколько изделий из IMBASE", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem38);
      MenuButtonItem menuItem39 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.Submenu", "Подборные компоненты", "", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem39);
      MenuButtonItem menuItem40 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.CreateNew", "Создать новый объект...", "Создать новый подборный компонент и добавить его в документ", false, false, this.commandManager);
      menuItem39.Items.Add((ToolbarItemBase) menuItem40);
      MenuButtonItem menuItem41 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.AddExisting", "Добавить существующий объект...", "Добавить существующий подборный компонент", false, false, this.commandManager);
      menuItem39.Items.Add((ToolbarItemBase) menuItem41);
      MenuButtonItem menuItem42 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.AddFromImbase", "Добавить из Imbase...", "Добавить подборный компонент из Imbase", false, false, this.commandManager);
      menuItem39.Items.Add((ToolbarItemBase) menuItem42);
      MenuButtonItem menuItem43 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.Reset", "Сброс подбора", "Сбросить подбор для выбранного компонента", false, false, this.commandManager);
      menuItem39.Items.Add((ToolbarItemBase) menuItem43);
      MenuButtonItem menuItem44 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.LimitAndValueModeSubmenu", "Допустимые значения показывать как...", "", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem44);
      MenuButtonItem contextMenuItem1 = NodeContextMenu.GetContextMenuItem(menuItem44.CommandName);
      MenuButtonItem menuItem45 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.RangeModeForRow", "Диапазон для записи подбора", "Для выбранной записи, выводится диапазон номиналов от минимального до максимального значения.", false, true, this.commandManager);
      menuItem44.Items.Add((ToolbarItemBase) menuItem45);
      contextMenuItem1.Items.Add((ToolbarItemBase) NodeContextMenu.GetContextMenuItem(menuItem45.CommandName));
      MenuButtonItem menuItem46 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.ListModeForRow", "Все номиналы в записи подбора", "Для выбранной записи, выводится список всех значений номиналов.", false, true, this.commandManager);
      menuItem44.Items.Add((ToolbarItemBase) menuItem46);
      contextMenuItem1.Items.Add((ToolbarItemBase) NodeContextMenu.GetContextMenuItem(menuItem46.CommandName));
      MenuButtonItem menuItem47 = DocumentMenuHelper.CreateMenuItem("AVS.Podbor.UseLimitValueModeForRow", "Показывать как задано в схеме", "Показывать значением атрибута «Предельные значения» в котором хранятся значения пришедшие со схемы.", false, true, this.commandManager);
      menuItem44.Items.Add((ToolbarItemBase) menuItem47);
      contextMenuItem1.Items.Add((ToolbarItemBase) NodeContextMenu.GetContextMenuItem(menuItem47.CommandName));
      MenuButtonItem menuItem48 = DocumentMenuHelper.CreateMenuItem("AVS.AddLRIRecord", "Добавить запись в ЛРИ", "Добавить запись в лист регистрации изменений", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem48);
      MenuButtonItem menuItem49 = DocumentMenuHelper.CreateMenuItem("AVS.AddLRIRecord_Before", "Вставить запись в ЛРИ перед", "Вставить запись в лист регистрации изменений после текущей записи", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem49);
      MenuButtonItem menuItem50 = DocumentMenuHelper.CreateMenuItem("AVS.AddLRIRecord_After", "Вставить запись в ЛРИ после", " Вставить запись в лист регистрации изменений  перед текущей записью", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem50);
      MenuButtonItem menuItem51 = DocumentMenuHelper.CreateMenuItem("AVS.AddOtherRecordTypes", "&Другие типы записей", "Добавить запись", false, true, this.commandManager);
      this._miAddOtherRecordTypes = menuItem51;
      menuItem51.Items.Add("[Нет записей]");
      menuItem51.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miAddNoteMenu_BeforePopup);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem51);
      MenuButtonItem contextMenuItem2 = NodeContextMenu.GetContextMenuItem("AVS.AddOtherRecordTypes");
      if (contextMenuItem2 != null)
      {
        contextMenuItem2.Items.Add("[Нет записей]");
        contextMenuItem2.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miAddNoteMenu_BeforePopup);
      }
      MenuButtonItem menuItem52 = DocumentMenuHelper.CreateMenuItem("AVS.DeleteRecords", "&Удалить записи", "Удалить выбранные записи", str1 + "DeleteRecord_v70.png", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem52);
      MenuButtonItem menuItem53 = DocumentMenuHelper.CreateMenuItem("AVS.DeleteObjects", "&Удалить объекты", "Удалить выбранные объекты", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem53);
      MenuButtonItem menuItem54 = DocumentMenuHelper.CreateMenuItem("AVS.AddSpecSection", "Добавить р&аздел", "Добавить в спецификацию раздел", str1 + "Section-New2_v70.png", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem54);
      MenuButtonItem menuButtonItem25 = new MenuButtonItem("[Нет записей]");
      menuButtonItem25.CommandName = "AVS.AddSpecSection.None";
      menuItem54.Items.Add((ToolbarItemBase) menuButtonItem25);
      menuItem54.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miAddSpecSection_BeforePopup);
      MenuButtonItem contextMenuItem3 = NodeContextMenu.GetContextMenuItem("AVS.AddSpecSection");
      if (contextMenuItem3 != null)
      {
        MenuButtonItem menuButtonItem26 = new MenuButtonItem("[Нет записей]");
        menuButtonItem26.CommandName = "AVS.AddSpecSection.None";
        contextMenuItem3.Items.Add((ToolbarItemBase) menuButtonItem26);
        contextMenuItem3.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miAddSpecSection_BeforePopup);
      }
      MenuButtonItem menuItem55 = DocumentMenuHelper.CreateMenuItem("AVS.AddAdditionalChapter", "Добавить часть...", "Добавить часть в спецификацию", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem55);
      MenuButtonItem menuItem56 = DocumentMenuHelper.CreateMenuItem("AVS.CommonPositions", "Совместные позиции...", "Настройка совместных позиций", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem56);
      MenuButtonItem menuItem57 = DocumentMenuHelper.CreateMenuItem("AVS.SumPositionDesignation", "Суммировать", "Суммировать записи с одинаковыми объектами", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem57);
      MenuButtonItem menuItem58 = DocumentMenuHelper.CreateMenuItem("AVS.UpdateDocumentStructure", "Собрать объекты в одну запись", "Собрать объекты с количеством в разных исполнениях в одну запись", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem58);
      MenuButtonItem menuItem59 = DocumentMenuHelper.CreateMenuItem("AVS.MoveSpecRow", "Изменить &раздел записей...", "Переместить выбранные записи в другой раздел", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem59);
      MenuButtonItem menuItem60 = DocumentMenuHelper.CreateMenuItem("AVS.MoveSpecRowToChapter", "Изменить часть записей...", "Переместить выбранные записи в другую часть", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem60);
      MenuButtonItem menuItem61 = DocumentMenuHelper.CreateMenuItem("AVS.ChangeRecordIspolnenie", "Изменить исполнение записей...", "Переместить выделенные записи в другое исполнение", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem61);
      MenuButtonItem menuItem62 = DocumentMenuHelper.CreateMenuItem("AVS.AddZagotovkaForPart", "Добавить заготовку для изделия...", "Добавить запись о выбранной из базы данных заготовке для текущего изделия", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem62);
      MenuButtonItem menuItem63 = DocumentMenuHelper.CreateMenuItem("AVS.AddZagotovkaForPart_FromImBase", "Добавить заготовку для изделия из IMBASE...", "Добавить запись с выбранной из IMBASE заготовкой для текущего изделия", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem63);
      MenuButtonItem menuItem64 = DocumentMenuHelper.CreateMenuItem("AVS.ConvertFromZagotovka", "Преобразовать из заготовки в изделие", "Преобразовать запись с заготовкой в запись с изделием", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem64);
      MenuButtonItem menuItem65 = DocumentMenuHelper.CreateMenuItem("AVS.DeleteEmptySections", "Удалить &пустые разделы", "Удалить разделы, в которых нету ни одной записи", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem65);
      MenuButtonItem menuItem66 = DocumentMenuHelper.CreateMenuItem("AVS.ShowEmptySections", "Показать переменные данные", "Показать переменные данные для всех исполнений", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem66);
      MenuButtonItem menuItem67 = DocumentMenuHelper.CreateMenuItem("AVS.ShowAllDocRows", "Показать все записи", "Показать записи с пустыми количествами", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem67);
      MenuButtonItem menuItem68 = DocumentMenuHelper.CreateMenuItem("AVS.HideDocRowsWithoutCount", "Скрыть пустые записи", "Скрыть записи с пустыми количествами", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem68);
      MenuButtonItem menuItem69 = DocumentMenuHelper.CreateMenuItem("AVS.ShowSameChapters", "Показать состав всех исполнений", "Показать состав всех исполнений", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem69);
      MenuButtonItem menuItem70 = DocumentMenuHelper.CreateMenuItem("AVS.HideSameChapters", "Скрыть состав одинаковых исполнений", "Скрыть состав одинаковых исполнений", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem70);
      DocumentMenuHelper.CreateMenuItem("AVS.Property", "Панель свойств", "Открывает панель с дополнительными видами", str1 + "PropertiesHS_v70.png", true, true, this.commandManager);
      MenuButtonItem contextMenuItem4 = NodeContextMenu.GetContextMenuItem("AVS.Property");
      if (contextMenuItem4 != null)
      {
        menuBarItem1.Items.Add((ToolbarItemBase) contextMenuItem4);
        contextMenuItem4.BeginGroup = true;
      }
      MenuItemBase menuItem71 = menuBar1.FindMenuItem("File.ParametersCard");
      if (menuItem71 != null)
        DocumentMenuHelper.CreateMenuItem("AVSParametersCard", "Свойства исполнения (Карточка)", "Свойства исполнения (Карточка)", menuItem71.Image, false, true, this.commandManager);
      MenuButtonItem contextMenuItem5 = NodeContextMenu.GetContextMenuItem("AVSParametersCard");
      if (contextMenuItem5 != null)
      {
        menuBarItem1.Items.Add((ToolbarItemBase) contextMenuItem5);
        contextMenuItem5.BeginGroup = true;
      }
      MenuButtonItem menuItem72 = DocumentMenuHelper.CreateMenuItem("PDM.CreateSubstitutesGroup", PDMPluginConsts.menuCreateSubstitutesGroup, PDMPluginConsts.menuCreateSubstitutesGroup, "icoCreateSubstitutesGroup.PDM", true, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem72);
      MenuButtonItem menuItem73 = DocumentMenuHelper.CreateMenuItem("PDM.MakeActualSubstitute", PDMPluginConsts.menuMakeActualSubstitute, PDMPluginConsts.menuMakeActualSubstitute, "icoMakeActualSubstitute.PDM", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem73);
      MenuButtonItem menuItem74 = DocumentMenuHelper.CreateMenuItem("PDM.EditSubstitutesGroup", PDMPluginConsts.menuEditSubstitutesGroup, PDMPluginConsts.menuEditSubstitutesGroup, "icoEditSubstitutesGroup.PDM", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem74);
      MenuButtonItem menuItem75 = DocumentMenuHelper.CreateMenuItem("PDM.DeleteSubstitutesGroup", PDMPluginConsts.menuDeleteSubstitutesGroup, PDMPluginConsts.menuDeleteSubstitutesGroup, "icoDeleteSubstitutesGroup.PDM", false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem75);
      Image img = (Image) null;
      if (service7 != null)
      {
        MenuTemplateNode menuTemplateNode3 = service7.ContextMenuTemplate["CheckOut"];
        if (menuTemplateNode3 != null)
        {
          img = menuTemplateNode3.ImageIndex < 0 ? DocumentMenuHelper.LoadImageFromResurces(str1 + "CheckOut.png") : this.iNamedImageList?.ImageList.Images[menuTemplateNode3.ImageIndex];
          menuButtonItem1 = DocumentMenuHelper.CreateMenuItem("AVS.CheckOut", menuTemplateNode3.Text, "Взять выбранные объекты на редактирование", img, true, true, this.commandManager);
        }
        MenuTemplateNode menuTemplateNode4 = service7.ContextMenuTemplate["CheckIn"];
        if (menuTemplateNode4 != null)
        {
          img = menuTemplateNode4.ImageIndex < 0 ? DocumentMenuHelper.LoadImageFromResurces(str1 + "CheckIn.png") : this.iNamedImageList?.ImageList.Images[menuTemplateNode4.ImageIndex];
          menuButtonItem1 = DocumentMenuHelper.CreateMenuItem("AVS.CheckIn", menuTemplateNode4.Text, "Завершить редактирование выбранных объектов", img, false, true, this.commandManager);
        }
      }
      if (service7 != null)
      {
        MenuTemplateNode menuTemplateNode5 = service7.ContextMenuTemplate["OpenInNewWindow"];
        if (menuTemplateNode5 != null)
        {
          if (menuTemplateNode5.ImageIndex >= 0)
            img = this.iNamedImageList?.ImageList.Images[menuTemplateNode5.ImageIndex];
          MenuButtonItem menuItem76 = DocumentMenuHelper.CreateMenuItem("AVS.OpenInNewWindow", menuTemplateNode5.Text, "", img, false, true, this.commandManager);
          if (menuItem76 != null)
          {
            menuItem76.Shortcut = (Shortcut) 131085 /*0x02000D*/;
            menuBar3.Items.Add((ToolbarItemBase) menuItem76);
          }
        }
      }
      MenuButtonItem menuItem77 = DocumentMenuHelper.CreateMenuItem("AVS.SpecificationForm", "&Форма документа...", "Изменить форму документа", true, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem77);
      MenuButtonItem menuItem78 = DocumentMenuHelper.CreateMenuItem("AVS.ProductsList", "Список исполнений...", "Вызвать диалог со списком исполнений", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem78);
      MenuButtonItem menuItem79 = DocumentMenuHelper.CreateMenuItem("AVS.ParentProductsList", "Список родительских изделий...", "Вызвать диалог со списком изделий, состав которых попадет в документ", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem79);
      MenuButtonItem menuItem80 = DocumentMenuHelper.CreateMenuItem("AVS.Sort", "С&ортировать", "Сортировать записи в документе", "Intermech.Document.Model.Resources.SortAscend_v70.png", true, false, this.commandManager);
      menuItem80.Shortcut = Shortcut.F9;
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem80);
      MenuButtonItem menuItem81 = DocumentMenuHelper.CreateMenuItem("AVS.SortRazdel", "С&ортировать раздел", "Сортировать записи в текущем разделе", "Intermech.Document.Model.Resources.SortRazdel_v70.png", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem81);
      MenuButtonItem menuItem82 = DocumentMenuHelper.CreateMenuItem("AVS.NumberPositions", "&Нумеровать позиции", "Нумеровать непронумерованные позиции в спецификации", "Intermech.Document.Model.Resources.NumberPositions_v70.png", false, false, this.commandManager);
      menuItem82.Shortcut = Shortcut.CtrlF9;
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem82);
      MenuButtonItem menuItem83 = DocumentMenuHelper.CreateMenuItem("AVS.GroupRows.Submenu", "Группировка записей", "", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem83);
      MenuButtonItem menuItem84 = DocumentMenuHelper.CreateMenuItem("AVS.GroupRowsByHeader", "&Группировать записи", "Группировать записи под общим заголовком", false, false, this.commandManager);
      menuItem83.Items.Add((ToolbarItemBase) menuItem84);
      MenuButtonItem menuItem85 = DocumentMenuHelper.CreateMenuItem("AVS.UnGroupRowsByHeader", "&Разгруппировать записи", "Отменить группировку записей по заголовку", false, false, this.commandManager);
      menuItem83.Items.Add((ToolbarItemBase) menuItem85);
      string str3 = "Для сгруппированных всегда выводить 'Размеры и параметры'";
      MenuButtonItem menuItem86 = DocumentMenuHelper.CreateMenuItem("AVS.DontIncludeClassNameInGroupRow", str3, str3, false, false, this.commandManager);
      menuItem83.Items.Add((ToolbarItemBase) menuItem86);
      string str4 = "Добавлять 'Класс' если 'Размеры и параметры' - число или буква";
      MenuButtonItem menuItem87 = DocumentMenuHelper.CreateMenuItem("AVS.IncludeClassNameInGroupRow", str4, str4, false, false, this.commandManager);
      menuItem83.Items.Add((ToolbarItemBase) menuItem87);
      MenuButtonItem menuItem88 = DocumentMenuHelper.CreateMenuItem("AVS.ClearNumberPositions", "&Очистить графу \"Позиции\"", "Очистить графу \"Позиции\" в спецификации", "Intermech.Document.Model.Resources.ClearNumberPositions_v70.png", false, false, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem88);
      MenuButtonItem menuItem89 = DocumentMenuHelper.CreateMenuItem("AVS.RefreshFormatAndSmotri", "&Обновить Формат и текстовые ссылки", "Обновить графу \"Формат\" и текстовые ссылки в наименовании", str1 + "RefreshFormat_v70.png", false, true, this.commandManager);
      if (menuItem89 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem89);
      MenuButtonItem menuItem90 = DocumentMenuHelper.CreateMenuItem("AVS.ClearSmotri", "&Очистить свойство \"Смотри\" для всех записей документа", "&Очистить свойство \"Смотри\" для всех записей документа", str1 + "ClearAttributeSmotri_v70.png", false, true, this.commandManager);
      if (menuItem90 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem90);
      MenuButtonItem menuItem91 = DocumentMenuHelper.CreateMenuItem("AVS.RefreshMass", "&Обновить Массу", "Рассчитать заново массу бесчертёжных деталей, а также массу специфицируемого изделия", str1 + "UpdateMass_v70.png", false, true, this.commandManager);
      if (menuItem91 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem91);
      MenuButtonItem menuItem92 = DocumentMenuHelper.CreateMenuItem("AVS.CheckErrors", "&Проверить на ошибки", "Проверить спецификацию на ошибки", str1 + "CheckErrors_v70.png", false, true, this.commandManager);
      if (menuItem92 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem92);
      MenuButtonItem menuItem93 = DocumentMenuHelper.CreateMenuItem("AVS.RowUp", "&Строку вверх", "Переместить запись спецификации вверх", DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, str2 + "arrow_up_SpecRow_blueStandart_v70.png"), false, false, this.commandManager);
      if (menuItem93 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem93);
      MenuButtonItem menuItem94 = DocumentMenuHelper.CreateMenuItem("AVS.RowDown", "&Строку вниз", "Переместить запись спецификации вниз", DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, str2 + "arrow_bottom_SpecRow_blueStandart_v70.png"), false, false, this.commandManager);
      if (menuItem94 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem94);
      MenuButtonItem menuItem95 = DocumentMenuHelper.CreateMenuItem("AVS.RowProperties", "&Свойства выделенной записи", "Свойства выделенной записи", DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, str2 + "PropertyGrid_v70.png"), false, true, this.commandManager);
      if (menuItem95 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem95);
      MenuButtonItem menuItem96 = DocumentMenuHelper.CreateMenuItem("AVS.SetOccurenceKey", "Связать с CAD", "Связать с CAD", DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, str2 + "cad_link_v70.png"), false, true, this.commandManager);
      menuBarItem1.Items.Add((ToolbarItemBase) menuItem96);
      MenuButtonItem menuItem97 = DocumentMenuHelper.CreateMenuItem("AVS.ReplaceTemplate", "Заменить шаблон документа...", "Заменить шаблон конструкторского документа", true, false, this.commandManager);
      if (menuItem97 != null)
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem97);
      MenuButtonItem menuItem98 = DocumentMenuHelper.CreateMenuItem("AVS.FinishWork", "&Вернуться в CAD-систему", "Завершить редактирование спецификации и вернуться в CAD-систему", string.Empty, false, true, this.commandManager, true);
      if (menuItem98 != null)
      {
        menuItem98.Locked = true;
        menuBarItem1.Items.Add((ToolbarItemBase) menuItem98);
      }
      MenuBarItem menuBarItem3 = new MenuBarItem("Настройки AVS");
      menuBarItem3.CommandName = "SpecificationTemplate";
      menuBarItem3.ToolTipText = "Настройки AVS";
      menuBarItem3.Visible = false;
      menuBar1.Items.Add((ToolbarItemBase) menuBarItem3);
      this.commandManager.Add((ButtonItemBase) menuBarItem3);
      MenuBarItem menuBar4 = menuBar1.FindMenuBar("mnService");
      menuBarItem3.Visible = false;
      int num27 = menuBar4.Items.Count + 2;
      MenuButtonItem menuItem99 = DocumentMenuHelper.CreateMenuItem("AVS.Properties", "Настройки конструкторского документа...", "Настройки конструкторского документа", "", true, false, this.commandManager);
      MenuItemBase.MenuItemCollection items25 = menuBar4.Items;
      int index25 = num27;
      int num28 = index25 + 1;
      MenuButtonItem menuButtonItem27 = menuItem99;
      items25.Insert(index25, (ToolbarItemBase) menuButtonItem27);
      MenuButtonItem menuItem100 = DocumentMenuHelper.CreateMenuItem("AVS.SortingSchema", "Сортировка...", "Настроить правила сортировки записей", "Intermech.Document.Model.Resources.SortSetup.bmp", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items26 = menuBar4.Items;
      int index26 = num28;
      int num29 = index26 + 1;
      MenuButtonItem menuButtonItem28 = menuItem100;
      items26.Insert(index26, (ToolbarItemBase) menuButtonItem28);
      MenuButtonItem menuItem101 = DocumentMenuHelper.CreateMenuItem("AVS.DocumentTypesWeights", "Сортировка документов по типу...", "Настроить правила сортировки типов документов", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items27 = menuBar4.Items;
      int index27 = num29;
      int num30 = index27 + 1;
      MenuButtonItem menuButtonItem29 = menuItem101;
      items27.Insert(index27, (ToolbarItemBase) menuButtonItem29);
      MenuButtonItem menuItem102 = DocumentMenuHelper.CreateMenuItem("AVS.RemarkAttributes", "Атрибуты в примечаниях...", "Настроить список атрибутов, отображаемых в примечаниях спецификаций", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items28 = menuBar4.Items;
      int index28 = num30;
      int num31 = index28 + 1;
      MenuButtonItem menuButtonItem30 = menuItem102;
      items28.Insert(index28, (ToolbarItemBase) menuButtonItem30);
      MenuButtonItem menuItem103 = DocumentMenuHelper.CreateMenuItem("AVS.VersionAttributes", "Заголовки исполнений в переменных данных...", "Настроить список атрибутов, отображаемых в заголовках исполнений в переменных данных", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items29 = menuBar4.Items;
      int index29 = num31;
      int num32 = index29 + 1;
      MenuButtonItem menuButtonItem31 = menuItem103;
      items29.Insert(index29, (ToolbarItemBase) menuButtonItem31);
      MenuButtonItem menuItem104 = DocumentMenuHelper.CreateMenuItem("AVS.SetupNumberingSchema", "Нумерация...", "Настроить правила нумерации позиций в спецификации", "Intermech.Document.Model.Resources.NumberPositionsSetup.bmp", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items30 = menuBar4.Items;
      int index30 = num32;
      int num33 = index30 + 1;
      MenuButtonItem menuButtonItem32 = menuItem104;
      items30.Insert(index30, (ToolbarItemBase) menuButtonItem32);
      MenuButtonItem menuItem105 = DocumentMenuHelper.CreateMenuItem("AVS.SkipLinesSetup", "Пропуск строк...", "Настроить правила пропуска строк в спецификации", "Intermech.Document.Model.Resources.SkipLines.bmp", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items31 = menuBar4.Items;
      int index31 = num33;
      int num34 = index31 + 1;
      MenuButtonItem menuButtonItem33 = menuItem105;
      items31.Insert(index31, (ToolbarItemBase) menuButtonItem33);
      MenuButtonItem menuItem106 = DocumentMenuHelper.CreateMenuItem("AVS.DynamicGroupHeaderSetup", "Группировка записей под общим заголовком...", "Настроить группировку записей под общим заголовком в AVS", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items32 = menuBar4.Items;
      int index32 = num34;
      int num35 = index32 + 1;
      MenuButtonItem menuButtonItem34 = menuItem106;
      items32.Insert(index32, (ToolbarItemBase) menuButtonItem34);
      MenuButtonItem menuItem107 = DocumentMenuHelper.CreateMenuItem("AVS.DesignationTrimSetup", "Обозначения исполнений...", "Настройка записи обозначений исполнений и их сокращения в записях", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items33 = menuBar4.Items;
      int index33 = num35;
      int num36 = index33 + 1;
      MenuButtonItem menuButtonItem35 = menuItem107;
      items33.Insert(index33, (ToolbarItemBase) menuButtonItem35);
      MenuButtonItem menuItem108 = DocumentMenuHelper.CreateMenuItem("AVS.KeyWordsSetup", "Ключевые слова для материалов...", "Настройка ключевых слов для материалов", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items34 = menuBar4.Items;
      int index34 = num36;
      int num37 = index34 + 1;
      MenuButtonItem menuButtonItem36 = menuItem108;
      items34.Insert(index34, (ToolbarItemBase) menuButtonItem36);
      MenuButtonItem menuItem109 = DocumentMenuHelper.CreateMenuItem("AVS.SpecSectionsSetup", "Разделы спецификации...", "Настройка разделов конструкторской спецификации", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items35 = menuBar4.Items;
      int index35 = num37;
      int num38 = index35 + 1;
      MenuButtonItem menuButtonItem37 = menuItem109;
      items35.Insert(index35, (ToolbarItemBase) menuButtonItem37);
      MenuButtonItem menuItem110 = DocumentMenuHelper.CreateMenuItem("AVS.ImbaseCatalogsSetup", "Каталоги IMBASE...", "Настройка каталогов ImBase", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items36 = menuBar4.Items;
      int index36 = num38;
      int num39 = index36 + 1;
      MenuButtonItem menuButtonItem38 = menuItem110;
      items36.Insert(index36, (ToolbarItemBase) menuButtonItem38);
      MenuButtonItem menuItem111 = DocumentMenuHelper.CreateMenuItem("AVS.AdditionalChaptersSetup", "Части спецификации...", "Настройка частей конструкторской спецификации", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items37 = menuBar4.Items;
      int index37 = num39;
      int num40 = index37 + 1;
      MenuButtonItem menuButtonItem39 = menuItem111;
      items37.Insert(index37, (ToolbarItemBase) menuButtonItem39);
      MenuButtonItem menuItem112 = DocumentMenuHelper.CreateMenuItem("AVS.SetupAVSTemplates", "Настройка шаблонов AVS", "Настроить шаблоны для типов документов", "", true, false, this.commandManager);
      MenuItemBase.MenuItemCollection items38 = menuBar4.Items;
      int index38 = num40;
      num6 = index38 + 1;
      MenuButtonItem menuButtonItem40 = menuItem112;
      items38.Insert(index38, (ToolbarItemBase) menuButtonItem40);
      int num41 = 0;
      MenuButtonItem menuItem113 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.SetupAVSSorting", "Сортировка...", "Настроить правила сортировки записей в спецификациях", "Intermech.Document.Model.Resources.SortSetup.bmp", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items39 = menuBarItem3.Items;
      int index39 = num41;
      int num42 = index39 + 1;
      MenuButtonItem menuButtonItem41 = menuItem113;
      items39.Insert(index39, (ToolbarItemBase) menuButtonItem41);
      MenuButtonItem menuItem114 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.DocumentTypesWeights", "Сортировка документов по типу...", "Настроить правила сортировки типов документов", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items40 = menuBarItem3.Items;
      int index40 = num42;
      int num43 = index40 + 1;
      MenuButtonItem menuButtonItem42 = menuItem114;
      items40.Insert(index40, (ToolbarItemBase) menuButtonItem42);
      MenuButtonItem menuItem115 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.RemarkAttributes", "Атрибуты в примечаниях...", "Настроить список атрибутов, отображаемых в примечаниях спецификаций", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items41 = menuBarItem3.Items;
      int index41 = num43;
      int num44 = index41 + 1;
      MenuButtonItem menuButtonItem43 = menuItem115;
      items41.Insert(index41, (ToolbarItemBase) menuButtonItem43);
      MenuButtonItem menuItem116 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.SetupAVSNumbering", "Нумерация...", "Настроить правила нумерации позиций в спецификациях", "Intermech.Document.Model.Resources.NumberPositionsSetup.bmp", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items42 = menuBarItem3.Items;
      int index42 = num44;
      int num45 = index42 + 1;
      MenuButtonItem menuButtonItem44 = menuItem116;
      items42.Insert(index42, (ToolbarItemBase) menuButtonItem44);
      MenuButtonItem menuItem117 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.SetupAVSSkipLines", "Пропуск строк...", "Настроить правила пропуска строк в спецификациях", "Intermech.Document.Model.Resources.SkipLines.bmp", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items43 = menuBarItem3.Items;
      int index43 = num45;
      int num46 = index43 + 1;
      MenuButtonItem menuButtonItem45 = menuItem117;
      items43.Insert(index43, (ToolbarItemBase) menuButtonItem45);
      MenuButtonItem menuItem118 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.SetupAVSDynamicGroupHeader", "Группировка записей под общим заголовком...", "Настроить группировку записей под общим заголовком в AVS", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items44 = menuBarItem3.Items;
      int index44 = num46;
      int num47 = index44 + 1;
      MenuButtonItem menuButtonItem46 = menuItem118;
      items44.Insert(index44, (ToolbarItemBase) menuButtonItem46);
      MenuButtonItem menuItem119 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.DesignationTrimSetup", "Обозначения исполнений...", "Настройка записи обозначений исполнений и их сокращения в записях", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items45 = menuBarItem3.Items;
      int index45 = num47;
      int num48 = index45 + 1;
      MenuButtonItem menuButtonItem47 = menuItem119;
      items45.Insert(index45, (ToolbarItemBase) menuButtonItem47);
      MenuButtonItem menuItem120 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.KeyWordsSetup", "Ключевые слова для материалов...", "Настройка ключевых слов для материалов", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items46 = menuBarItem3.Items;
      int index46 = num48;
      int num49 = index46 + 1;
      MenuButtonItem menuButtonItem48 = menuItem120;
      items46.Insert(index46, (ToolbarItemBase) menuButtonItem48);
      MenuButtonItem menuItem121 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.SpecSectionsSetup", "Разделы конструкторского документа...", "Настройка разделов конструкторской спецификации", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items47 = menuBarItem3.Items;
      int index47 = num49;
      int num50 = index47 + 1;
      MenuButtonItem menuButtonItem49 = menuItem121;
      items47.Insert(index47, (ToolbarItemBase) menuButtonItem49);
      MenuButtonItem menuItem122 = DocumentMenuHelper.CreateMenuItem("SpecificationTemplate.SetupAVSTemplates", "Настройка шаблонов AVS...", "Настроить шаблоны для типов документов", "", false, false, this.commandManager);
      MenuItemBase.MenuItemCollection items48 = menuBarItem3.Items;
      int index48 = num50;
      num6 = index48 + 1;
      MenuButtonItem menuButtonItem50 = menuItem122;
      items48.Insert(index48, (ToolbarItemBase) menuButtonItem50);
      menuBar1.FindMenuBar("mnHelp").Items.Insert(0, (ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("AVS.TEST", "EXPRESS TEST", "", true, false, this.commandManager));
      this.AddNewMenuItem((MenuItemBase) menuBar4, "AVSPluginExecuteCommand_A_NastrVed", "Настройка конструкторских ведомостей", beginGroup: true);
      this.AddNewMenuItem((MenuItemBase) menuBar4, "AVSPluginExecuteCommand_A_NastrTabl", "Настройка конструкторских таблиц");
      this.AddNewMenuItem((MenuItemBase) menuBar4, "AVSPluginExecuteCommand_A_Nastr_ReadFilesAvs6", "Настройка чтения документов AVS6", "Настройка чтения документов (ведомости, таблицы), разработанных в программе AVS6");
      if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr && (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain))
        this.AddNewMenuItem((MenuItemBase) menuBar4, "Conformity_Template_Nastr_Command", "Настройка соответствия шаблонов и настройки", "Настройка соответствия шаблонов и настройки (ведомости, таблицы)");
      VedomostEditorWindow.CreateMenuAndBaseContextCommands();
      IContentProvider service9 = (IContentProvider) serviceProvider.GetService(typeof (IContentProvider));
      if (service9 != null)
        service9.ContentCallback += new GetContentCallback(this.RestoreDocumentWindow);
      if (this.iNamedImageList != null)
      {
        Image image = DocumentMenuHelper.LoadImageFromResurces(typeof (AVSPlugin).Assembly, "Intermech.AVS.Resources.NumberPositionsSetup.bmp");
        if (image != null)
          SetupNumberingSchemaControl.PageImageIndex = this.iNamedImageList.Add(image, "NumberPositionsSetup");
      }
      ServicesManager.AddService(typeof (ISpecificationSaveService), (object) new SpecificationSaveService());
      this.RegisterViews();
      this.IsLoaded = true;
    }
    catch (Exception ex)
    {
      ServicesManager.RemoveService(typeof (IImportStructureFromCadService));
      ServicesManager.RemoveService(typeof (IECADIntegratorsDocumentService));
      ServicesManager.RemoveService(typeof (IElementListCreatorService));
      ServicesManager.RemoveService(typeof (IAVSClientService));
      ServicesManager.RemoveService(typeof (ISpecificationSaveService));
      service1.LoadComplete -= new EventHandler(this.pluginManager_LoadComplete);
      DocumentEditorPlugin.AfterLoadDocument -= new AfterLoadDocumentEventHandler(this.DocumentEditorPlugin_AfterLoadDocument);
      IPreviewExtender service10 = serviceProvider.GetService<IPreviewExtender>(false);
      if (service10 != null)
        service10.Extend -= new ExtendEventHandler(this.previewExtender_Extend);
      IAuthFilesService service11 = ServicesManager.GetService<IAuthFilesService>(false);
      if (service11 != null)
        service11.AuthFileAssignEvent -= new AuthFileAssignEventHandler(this.AuthFilesService_AuthFileAssignEvent);
      if (AVSPlugin.dockManager != null)
        AVSPlugin.dockManager.DocumentContainer.ActiveDocumentChanged -= new ActiveDocumentEventHandler(this.DockManagerActiveDocumentChanged);
      IContentProvider service12 = serviceProvider.GetService<IContentProvider>(false);
      if (service12 != null)
        service12.ContentCallback -= new GetContentCallback(this.RestoreDocumentWindow);
      throw;
    }
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e) => this.CheckPDMPlugins();

  private void AuthFilesService_AuthFileAssignEvent(
    object sender,
    AuthFileAssignEventArgs eventArgs)
  {
    if (!AVSDocumentsSettings.Instance.IsAVSDocumentSupportedType(eventArgs.ObjectType))
      return;
    string str1 = (string) null;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(eventArgs.ObjectId);
        if (dbObject == null)
          return;
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid == null)
          return;
        int authenticalFile = AVSPlugin.FindAuthenticalFile(attributeByGuid);
        AVSDocument avsDocument = this.LoadAVSDocument(eventArgs.ObjectId, eventArgs.ObjectType, true);
        if (avsDocument == null)
          return;
        string str2 = FileNameHelper.ReplaceInvalidFileNameChars(avsDocument.DefaultFileName + ".pdf");
        str1 = Path.Combine(Path.GetTempPath(), str2);
        avsDocument.Document.SaveToPdf(str1, false);
        if (authenticalFile != -1)
          AVSPlugin.SaveToAuthenticalFile(attributeByGuid, authenticalFile, str1);
        else
          AVSPlugin.SaveToNewAuthenticalFile(attributeByGuid, str2, str1);
        eventArgs.IsHandled = true;
      }
    }
    catch
    {
      throw;
    }
    finally
    {
      if (!string.IsNullOrEmpty(str1) && File.Exists(str1))
        File.Delete(str1);
    }
  }

  private static void SaveToNewAuthenticalFile(
    IDBAttribute dbAttribute,
    string fileName,
    string fullFileName)
  {
    dbAttribute.Index = dbAttribute.AddValue((object) FileTypes.ftAuthentical);
    IBlobReader blobReader = (IBlobReader) dbAttribute;
    try
    {
      BlobInformation aBlobInformation = blobReader.OpenBlob(-1) with
      {
        FileType = FileTypes.ftAuthentical,
        FileName = fileName,
        ArcMethod = ArcMethods.ZLibPacked
      };
      using (FileStream aSourceStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read))
        new BlobProcWriter(dbAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
    }
    catch (Exception ex)
    {
      dbAttribute.DeleteValue();
      throw;
    }
    blobReader.CloseBlob();
  }

  private static void SaveToAuthenticalFile(
    IDBAttribute dbAttribute,
    int authenticalFileIndex,
    string fullFileName)
  {
    dbAttribute.Index = authenticalFileIndex;
    IBlobReader blobReader = (IBlobReader) dbAttribute;
    BlobInformation aBlobInformation = blobReader.OpenBlob(-1);
    blobReader.CloseBlob();
    aBlobInformation.ModifyDate = DateTime.Now;
    using (FileStream aSourceStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read))
      new BlobProcWriter(dbAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
  }

  private static int FindAuthenticalFile(IDBAttribute dbAttribute)
  {
    int authenticalFile = -1;
    for (int index = 0; index < dbAttribute.ValuesCount; ++index)
    {
      dbAttribute.Index = index;
      if (dbAttribute is IBlobReader blobReader)
      {
        if (blobReader.OpenBlob(-1).FileType == FileTypes.ftAuthentical)
          authenticalFile = index;
        blobReader.CloseBlob();
      }
    }
    return authenticalFile;
  }

  public MenuButtonItem AddNewMenuItem(
    MenuItemBase menu,
    string commandName,
    string commandCaption,
    string commandHint = "",
    bool beginGroup = false,
    bool createContextMenuItem = false)
  {
    if (string.IsNullOrWhiteSpace(commandHint))
      commandHint = commandCaption;
    MenuButtonItem menuItem = DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, beginGroup, createContextMenuItem, this.commandManager);
    menu.Items.Add((ToolbarItemBase) menuItem);
    return menuItem;
  }

  public MenuButtonItem AddNewContextMenuItem(
    MenuItemBase menu,
    string commandName,
    string commandCaption,
    string commandHint = "",
    bool beginGroup = false,
    bool createContextMenuItem = false)
  {
    if (string.IsNullOrWhiteSpace(commandHint))
      commandHint = commandCaption;
    MenuButtonItem menuItem = DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, beginGroup, createContextMenuItem, this.commandManager);
    menu.Items.Add((ToolbarItemBase) menuItem);
    return menuItem;
  }

  public MenuButtonItem AddNewMenuItemIcon(
    MenuItemBase menu,
    string commandName,
    string commandCaption,
    string iconName,
    string commandHint = "",
    bool beginGroup = false,
    bool createContextMenuItem = false)
  {
    if (string.IsNullOrWhiteSpace(commandHint))
      commandHint = commandCaption;
    MenuButtonItem menuItem = DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, iconName, beginGroup, createContextMenuItem, this.commandManager);
    menu.Items.Add((ToolbarItemBase) menuItem);
    return menuItem;
  }

  private void DocumentEditorPlugin_AfterLoadDocument(
    object sender,
    AfterLoadDocumentEventHandlerArgs e)
  {
    if (e == null || e.Document == null)
      return;
    bool flag = MetaDataHelper.IsObjectTypeChildOf(e.DocumentTypeID, AvsIDCache.ObjType_ConstructorDocumentTemplate);
    if (AvsConfig.General.PatchStampReferences & flag)
      AVSDocument.PatchDocumentAttr((ImDocumentData) e.Document, e.DocumentGuid);
    AVSDocument.PatchProductNumbersHeader((ImDocumentData) e.Document);
    if (MetaDataHelper.IsObjectTypeChildOf(e.DocumentTypeID, AvsIDCache.ObjType_ConstructorDocument) || MetaDataHelper.IsObjectTypeChildOf(e.DocumentTypeID, AvsIDCache.ObjType_ConstructorDocumentTemplate))
    {
      if (AvsConfig.General.PatchLriId)
        AVSDocument.PatchAVSDocumentLRI((ImDocumentData) e.Document);
      AVSDocument.PatchAVSDocumentLiteraReference((ImDocumentData) e.Document);
    }
    if (flag)
    {
      AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(e.DocumentGuid, out InheritanceSettingsLevel _);
      if (settingsForTemplate != null && AVSDocumentsSettings.IsElementListDocType(settingsForTemplate.AVSDocType))
        AVSDocument.PatchFunctionalGroupHeaderTemplate((ImDocumentData) e.Document);
    }
    if (e.DocumentGuid == AvsIDCache.StdTemplateElementList)
      AVSElementList.PatchFieldReferences(e.Document);
    if (flag)
      AVSDocument.PatchDynamicHeaderInTemplate((ImDocumentData) e.Document);
    if (!MetaDataHelper.IsObjectTypeChildOf(e.DocumentTypeID, AvsIDCache.ObjType_Vedomost) || e.Document.MaterialKeyWords != null)
      return;
    string attributeValue = e.Document.GetAttributeValue("iDSP", true);
    long idSchemeHolder = -1;
    if (!string.IsNullOrWhiteSpace(attributeValue))
      idSchemeHolder = (long) int.Parse(attributeValue);
    e.Document.SetMaterialKeyWords(FormSetupKeyWords.GetKeywords(idSchemeHolder));
  }

  private void previewExtender_Extend(ExtendEventArgs eventArgs)
  {
    if (eventArgs == null || eventArgs.ObjectID == -1L)
      return;
    int dbObjectType = eventArgs.ObjectType;
    if (dbObjectType == -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        dbObjectType = sessionKeeper.Session.GetObjectInfo(eventArgs.ObjectID).ObjectTypeID;
    }
    if (!AVSDocument.IsProductForSpecification2(dbObjectType))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long assemblyProducts = AvsIDCache.FindSpecificationForAssemblyProducts(sessionKeeper.Session, (IList<long>) new long[1]
      {
        eventArgs.ObjectID
      }, "", true);
      if (assemblyProducts.IsUndefinedId())
        return;
      IDBAttribute specificationFileAttribute = AvsIDCache.FindSpecificationFileAttribute(sessionKeeper.Session.GetObject(assemblyProducts), out bool _);
      if (specificationFileAttribute == null)
        return;
      FileBlobItem fileBlobItem = new FileBlobItem(assemblyProducts, specificationFileAttribute.AttributeID, specificationFileAttribute.Index);
      if (eventArgs.Items.Contains(fileBlobItem))
        return;
      eventArgs.Items.Add(fileBlobItem);
    }
  }

  private void iobjCr_ObjectCreatorCompleatedEvent(long objectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int objectTypeId = sessionKeeper.Session.GetObjectInfo(objectID).ObjectTypeID;
      if (!MetaDataHelper.IsObjectTypeChildOf(objectTypeId, AvsIDCache.ObjType_Specification) && !AvsIDCache.IsElementList(objectTypeId) && !AVSDocument.IsProductForSpecification2(objectTypeId))
        return;
      this.OpenAVSWindow(objectID, objectTypeId);
    }
  }

  private void miAddNoteMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem1))
      return;
    menuButtonItem1.Items.Clear();
    if (this.ActiveAVSWindow != null)
    {
      foreach (TableData notesTemplate in this.ActiveAVSWindow.AVSDocument.NotesTemplates)
      {
        menuButtonItem1.Items.Add(notesTemplate.Name);
        MenuButtonItem menuButtonItem2 = menuButtonItem1.Items[menuButtonItem1.Items.Count - 1];
        menuButtonItem2.Tag = (object) notesTemplate;
        menuButtonItem2.Click += new EventHandler(this.mi_CreateOtherRecord);
      }
    }
    if (menuButtonItem1.Items.Count != 0)
      return;
    menuButtonItem1.Items.Add("[Нет записей]");
    menuButtonItem1.Items[0].Enabled = false;
  }

  private void mi_CreateOtherRecord(object sender, EventArgs e)
  {
    AVSWindow activeAvsWindow = this.ActiveAVSWindow;
    if (activeAvsWindow == null || sender == null || !(sender is MenuButtonItem) || activeAvsWindow.AVSDocument == null || ((ToolbarItemBase) sender).Tag == null || !(((ToolbarItemBase) sender).Tag is TableData))
      return;
    DocumentTreeNode contextOnlyFirstNode = this.ActiveAVSWindow.GetCommandContext_OnlyFirstNode();
    if (!(((ToolbarItemBase) sender).Tag is TableData rowTemplate))
      return;
    AVSDocumentContext contextChapters = activeAvsWindow.AVSDocument.GetContextChapters(contextOnlyFirstNode);
    if (activeAvsWindow.AVSDocument.AvsDocumentForm == AVSDocumentForm.V && contextChapters.Section != null && contextChapters.Section.IsFormB)
    {
      TableData nameVarDataFormV = activeAvsWindow.AVSDocument.FindNoteTemplateByName_VarDataFormV(rowTemplate.Name);
      if (nameVarDataFormV != null)
        rowTemplate = nameVarDataFormV;
    }
    activeAvsWindow.AVSDocument.InsertNewNoteDocRow(contextChapters, (string) null, rowTemplate, true, true);
  }

  private void miAddSpecSection_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    AvsMenuHelper.CreateAddSpecSectionItems(sender);
  }

  /// <summary>Выгрузить плагин. Пока невостребована</summary>
  public void Unload() => this.IsLoaded = false;

  /// <summary>Создать набор колонок для табличного вида</summary>
  /// <param name="avsWindow">Окно документа</param>
  /// <param name="gridViewCols">Коллекция для набор колонок</param>
  public static void CreateDefaultGridViewCols(
    AVSWindow avsWindow,
    List<AvsRowAttributeInfo> gridViewCols)
  {
    if (gridViewCols == null)
      throw new ArgumentNullException(nameof (gridViewCols));
    gridViewCols.Clear();
    if (avsWindow.AVSDocument != null && avsWindow.AVSDocument.docRowFields != null)
    {
      if (avsWindow.AVSDocument.docRowFields.Count == 0)
        avsWindow.AVSDocument.UpdateDocumentRowFieldsInfo();
      gridViewCols.Clear();
      if (avsWindow.AVSDocument.AvsDocumentForm != AVSDocumentForm.V)
        gridViewCols.AddRange((IEnumerable<AvsRowAttributeInfo>) avsWindow.AVSDocument.docRowFields);
      else
        gridViewCols.AddRange((IEnumerable<AvsRowAttributeInfo>) avsWindow.AVSDocument.docRowFields_VarFormV);
      for (int index1 = 0; index1 < gridViewCols.Count; ++index1)
      {
        for (int index2 = gridViewCols.Count - 1; index2 > index1; --index2)
        {
          if (object.Equals((object) gridViewCols[index1], (object) gridViewCols[index2]))
            gridViewCols.RemoveAt(index2);
        }
      }
    }
    else
    {
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      IDBAttributeTypeInfo attributeType1 = service.GetAttributeType(AvsIDCache.Attr_Format, false);
      if (attributeType1 != null)
        gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Object, ((IDBGuid) attributeType1).GUID, AvsIDCache.Attr_Format, attributeType1.Name, ColumnContents.Text));
      IDBAttributeTypeInfo attributeType2 = service.GetAttributeType(AvsIDCache.Attr_Zone);
      gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Relation, ((IDBGuid) attributeType2).GUID, AvsIDCache.Attr_Zone, attributeType2.Name, ColumnContents.Text));
      IDBAttributeTypeInfo attributeType3 = service.GetAttributeType(AvsIDCache.Attr_Position, false);
      if (attributeType3 != null)
        gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Relation, ((IDBGuid) attributeType3).GUID, AvsIDCache.Attr_Position, attributeType3.Name, ColumnContents.Text));
      IDBAttributeTypeInfo attributeType4 = service.GetAttributeType(AvsIDCache.Attr_Designation, false);
      if (attributeType4 != null)
        gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Object, ((IDBGuid) attributeType4).GUID, AvsIDCache.Attr_Designation, attributeType4.Name, ColumnContents.Text));
      IDBAttributeTypeInfo attributeType5 = service.GetAttributeType(AvsIDCache.Attr_Name, false);
      if (attributeType5 != null)
        gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Object, ((IDBGuid) attributeType5).GUID, AvsIDCache.Attr_Name, attributeType5.Name, ColumnContents.Text));
      IDBAttributeTypeInfo attributeType6 = service.GetAttributeType(AvsIDCache.Attr_Count, false);
      if (attributeType6 != null)
        gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Relation, ((IDBGuid) attributeType6).GUID, AvsIDCache.Attr_Count, attributeType6.Name, ColumnContents.Text));
      IDBAttributeTypeInfo attributeTypeInfo = avsWindow.AVSDocument == null ? service.GetAttributeType(AvsIDCache.Attr_Note, false) : service.GetAttributeType(avsWindow.AVSDocument.Attr_Note.AttributeId, false);
      if (attributeTypeInfo != null)
        gridViewCols.Add(new AvsRowAttributeInfo(FieldSource.Relation, ((IDBGuid) attributeTypeInfo).GUID, AvsIDCache.Attr_Note, attributeTypeInfo.Name, ColumnContents.Text));
    }
    for (int index = 0; index < gridViewCols.Count; ++index)
    {
      if (gridViewCols[index].AttributeId == AvsIDCache.Attr_Format)
        gridViewCols[index].TableViewColumnWidth = 85;
      else if (gridViewCols[index].AttributeId == AvsIDCache.Attr_Zone || gridViewCols[index].AttributeId == AvsIDCache.Attr_Position || gridViewCols[index].AttributeId == AvsIDCache.Attr_Count)
        gridViewCols[index].TableViewColumnWidth = 40;
      else if (gridViewCols[index].AttributeId == AvsIDCache.Attr_Designation)
        gridViewCols[index].TableViewColumnWidth = 200;
      else if (gridViewCols[index].AttributeId == AvsIDCache.Attr_Name)
        gridViewCols[index].TableViewColumnWidth = 350;
      else if (gridViewCols[index].AttributeId == AvsIDCache.Attr_Note || gridViewCols[index].AttributeId == AvsIDCache.Attr_NotePE)
        gridViewCols[index].TableViewColumnWidth = 150;
    }
  }

  /// <summary>Загрузить конфигурацию плагина</summary>
  /// <param name="configurationManager">Менеджер конфигураций</param>
  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    try
    {
      bool flag = AvsConfig.LoadValuesFromServer();
      byte[] avsGridLayoutData1 = flag ? AvsConfig.General.SpecificationGridLayout : (byte[]) null;
      byte[] avsGridLayoutData2 = flag ? AvsConfig.General.ElementListGridLayout : (byte[]) null;
      this.configManager = configurationManager;
      IConfiguration config = configurationManager.Open("AVS");
      if (config != null)
      {
        if (!flag)
          AvsConfig.LoadConfigs(config);
        AVSPlugin.TestSameDesignation1 = config.GetProperty("TestSameDesignation1");
        AVSPlugin.TestSameDesignation2 = config.GetProperty("TestSameDesignation2");
        if (avsGridLayoutData1 == null && avsGridLayoutData2 == null)
        {
          string property1 = config.GetProperty("AVSSpecGridCols");
          if (string.IsNullOrWhiteSpace(property1))
            property1 = config.GetProperty("AVSGridCols");
          if (!string.IsNullOrWhiteSpace(property1))
            avsGridLayoutData1 = Convert.FromBase64String(property1);
          string property2 = config.GetProperty("AVSElementListGridCols");
          if (!string.IsNullOrWhiteSpace(property2))
            avsGridLayoutData2 = Convert.FromBase64String(property2);
        }
      }
      if (avsGridLayoutData1 != null)
      {
        try
        {
          AVSPlugin.specificationGridViewCols = AVSPlugin.ParseGridLayout(avsGridLayoutData1);
        }
        catch (Exception ex)
        {
        }
      }
      if (avsGridLayoutData2 == null)
        return;
      try
      {
        AVSPlugin.elementListGridViewCols = AVSPlugin.ParseGridLayout(avsGridLayoutData2);
      }
      catch (Exception ex)
      {
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  internal static List<AvsRowAttributeInfo> ParseGridLayout(byte[] avsGridLayoutData)
  {
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    binaryFormatter.Binder = (SerializationBinder) new AVSPlugin.DeserializationBinder();
    object obj;
    using (MemoryStream serializationStream = new MemoryStream(avsGridLayoutData))
      obj = binaryFormatter.Deserialize((Stream) serializationStream);
    List<AvsRowAttributeInfo> avsDocGridViewCols = new List<AvsRowAttributeInfo>();
    if (obj != null && obj is List<AvsRowAttributeInfo>)
    {
      avsDocGridViewCols = (List<AvsRowAttributeInfo>) obj;
      if (avsDocGridViewCols.Count > 0)
      {
        IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
        List<AvsRowAttributeInfo> attributeListForEl = AVSElementList.GetVirtualAttributeListForEL();
        attributeListForEl.AddRange((IEnumerable<AvsRowAttributeInfo>) AvsIDCache.VirtualAttributes);
        for (int i = avsDocGridViewCols.Count - 1; i >= 0; i--)
        {
          if (!avsDocGridViewCols[i].IsDocField)
          {
            IDBAttributeTypeInfo attributeType = service.GetAttributeType(avsDocGridViewCols[i].AttributeGuid, false);
            if (attributeType != null)
            {
              avsDocGridViewCols[i].AttributeId = attributeType.AttributeID;
              avsDocGridViewCols[i].Name = attributeType.Name;
            }
            else
            {
              AvsRowAttributeInfo rowAttributeInfo = attributeListForEl.FirstOrDefault<AvsRowAttributeInfo>((System.Func<AvsRowAttributeInfo, bool>) (v => v.AttributeId == avsDocGridViewCols[i].AttributeId));
              if (rowAttributeInfo != null)
                avsDocGridViewCols[i].Name = rowAttributeInfo.Name;
              else
                avsDocGridViewCols.RemoveAt(i);
            }
          }
        }
      }
    }
    return avsDocGridViewCols;
  }

  /// <summary>Сохранить конфигурацию плагина</summary>
  /// <param name="configurationManager">Менеджер конфигураций</param>
  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    try
    {
      IConfiguration configuration = configurationManager.Open("AVS") ?? configurationManager.Create("AVS");
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (MemoryStream serializationStream = new MemoryStream())
      {
        binaryFormatter.Serialize((Stream) serializationStream, (object) AVSPlugin.specificationGridViewCols);
        byte[] array = serializationStream.ToArray();
        string base64String = Convert.ToBase64String(array);
        configuration.SetProperty("AVSSpecGridCols", base64String);
        AvsConfig.General.SpecificationGridLayout = array;
      }
      using (MemoryStream serializationStream = new MemoryStream())
      {
        binaryFormatter.Serialize((Stream) serializationStream, (object) AVSPlugin.elementListGridViewCols);
        byte[] array = serializationStream.ToArray();
        string base64String = Convert.ToBase64String(array);
        configuration.SetProperty("AVSElementListGridCols", base64String);
        AvsConfig.General.ElementListGridLayout = array;
      }
      configuration.SetProperty("TestSameDesignation1", AVSPlugin.TestSameDesignation1);
      configuration.SetProperty("TestSameDesignation2", AVSPlugin.TestSameDesignation2);
      AvsConfig.SaveValuesToServer();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Менеджер команд клиента</summary>
  [Browsable(false)]
  public ICommandManager CommandManager
  {
    [DebuggerStepThrough] get
    {
      if (this.commandManager == null)
        this.commandManager = (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
      return this.commandManager;
    }
  }

  /// <summary>Обработчик изменения выделения в AVSWindow</summary>
  public void SelectionChanged()
  {
    try
    {
      this.commandManager.QueryStatus();
      PropertyGridForm propertyGridForm = (PropertyGridForm) null;
      if (this.ActiveImDocumentEditorForm != null)
        propertyGridForm = this.ActiveImDocumentEditorForm.PropertyGridDlg;
      if (propertyGridForm != null && DocumentEditorPlugin.Instance != null)
      {
        Intermech.Document.UI.DocumentControl imDocumentControl = this.ActiveImDocumentControl;
        if (imDocumentControl != null)
        {
          List<DocumentTreeNode> selectedNodes = imDocumentControl.SelectedNodes;
          if (selectedNodes != null && selectedNodes.Count > 0)
            propertyGridForm.SelectedObjects = (object[]) selectedNodes.ToArray();
        }
      }
      if (this.ActiveImDocumentEditorForm == null || !(this.ActiveImDocumentEditorForm is AVSWindow))
        return;
      (this.ActiveImDocumentEditorForm as AVSWindow).UpdateSBChapterPanel();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Вызов диалога текста и установка его</summary>
  public void SetTextWithDlg()
  {
    try
    {
      Intermech.Document.UI.DocumentControl imDocumentControl = this.ActiveImDocumentControl;
      AVSDocument avsDocument = (AVSDocument) null;
      if (this.ActiveAVSWindow != null)
        avsDocument = this.ActiveAVSWindow.AVSDocument;
      if (avsDocument == null || !(imDocumentControl.SelectedNode is TextData selectedNode))
        return;
      TextData textData = avsDocument.GetLiteraCellFromTitleBlock();
      if (!avsDocument.IsSpecification)
        return;
      TableData parentNode = (TableData) null;
      int num = -1;
      long aId = -1;
      if ((avsDocument.IsFormB || avsDocument.AvsDocumentForm == AVSDocumentForm.V) && selectedNode != textData)
      {
        PageData page = selectedNode.Page;
        if (avsDocument.productKodAndLiteraTemplate != null)
          parentNode = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) avsDocument.productKodAndLiteraTemplate) as TableData;
        if (parentNode == null && avsDocument.productKodAndLitera2Template != null)
          parentNode = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) avsDocument.productKodAndLitera2Template) as TableData;
        if (parentNode != null && selectedNode.IsChildForNode((DocumentTreeNode) parentNode, false))
        {
          if (!(selectedNode.Parent.Nodes[0] is TextData node))
            return;
          if (node.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(referenceToTextSource.AttributeGuid);
            if (attributeType != null)
              referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, attributeType.AttributeID, referenceToTextSource.AttributeName);
          }
          if (referenceToTextSource == null || referenceToTextSource.AttributeID == -1)
            return;
          num = referenceToTextSource.AttributeID;
          int index = avsDocument.GetFirstProductIndex(page) + selectedNode.Index;
          if (index >= avsDocument.productsInfo.Count)
            return;
          aId = avsDocument.productsInfo[index].Id;
          textData = selectedNode;
        }
      }
      else if (textData != null && textData == selectedNode)
      {
        aId = avsDocument.productsInfo[0].Id;
        num = AvsIDCache.Attr_Litera;
      }
      if (aId == -1L || num == -1)
        return;
      AttributeProcessor attributeProcessor = new AttributeProcessor(0L, AttributableElements.Object);
      attributeProcessor.Load(aId, AttributableElements.Object, GetAttributeValuesModes.None, false);
      IAttributeEditorControl editorControl = attributeProcessor.GetEditorControl(num, new int?(0), UITypeEditorEditStyle.Modal, true);
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(avsDocument.ProductType, num);
      if (!(editorControl is Form form) || attribute4ObjectType == null || form.ShowDialog() != DialogResult.OK || num != AvsIDCache.Attr_Litera)
        return;
      if (avsDocument.GetLiteraCellFromTitleBlock() == textData)
      {
        object initValue = (object) null;
        if (attributeProcessor.FindAttributeValues(num).Values.Length != 0)
          initValue = attributeProcessor.FindAttributeValues(num).Values[0];
        List<long> objectIDs = new List<long>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (ProductInfo productInfo in avsDocument.productsInfo)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(productInfo.Id);
            if (dbObject != null)
            {
              AttributeValues[] valuesList = new AttributeValues[1]
              {
                new AttributeValues(num, initValue)
              };
              dbObject.SetAttributesValues(valuesList);
              objectIDs.Add(productInfo.Id);
            }
          }
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(avsDocument.DocumentID, false);
          if (dbObject1 != null)
          {
            AttributeValues[] valuesList = new AttributeValues[1]
            {
              new AttributeValues(num, initValue)
            };
            dbObject1.SetAttributesValues(valuesList);
            objectIDs.Add(avsDocument.DocumentID);
          }
        }
        if (AVSPlugin.NotificationService == null)
          return;
        AVSPlugin.NotificationService.FireEvent((object) avsDocument.AVSWindow, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
      }
      else
        attributeProcessor.Save();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Включен режим выбора элементов. Если имеет значение true,
  /// то IsElementCreating не может иметь значение true</summary>
  [Browsable(false)]
  public bool IsElementSelecting
  {
    [DebuggerStepThrough] get => true;
    set
    {
    }
  }

  /// <summary>Включен режим создания элементов. Если имеет значение true,
  /// то IsElementSelecting не может иметь значение true</summary>
  [Browsable(false)]
  public bool IsElementCreating
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  /// <summary>Объект управляющий созданием элемента</summary>
  [Browsable(false)]
  public PageElementCreator SelectedElementCreator
  {
    [DebuggerStepThrough] get => (PageElementCreator) null;
    set
    {
    }
  }

  /// <summary>Обновить информацию об выбранных элементах</summary>
  public void UpdateSelectedElementInfo()
  {
  }

  /// <summary>Установить строку сообщения (например в строке статуса)</summary>
  /// <param name="text">Текст сообщения</param>
  public void SetMessageText(string text)
  {
    if (this.statusBar == null || this.statusBar.StatusBar == null || this.statusBar.StatusBar.Panels.Count <= 0)
      return;
    this.statusBar.StatusBar.Panels[0].Text = text;
  }

  /// <summary>Обновить информацию о количестве страниц и текущей странице</summary>
  public void UpdatePagesInfo()
  {
    if (this.ActiveImDocumentEditorForm == null)
      return;
    this.ActiveImDocumentEditorForm.UpdateSBPagePanel();
  }

  /// <summary>Диалог сохранения документа в файл на диске</summary>
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

  /// <summary>Последнее путь использовавшийся при сохранении как</summary>
  [Browsable(false)]
  public string RecentlySaveAsPath
  {
    [DebuggerStepThrough] get => this.recentlySaveAsPath;
    set => this.recentlySaveAsPath = value;
  }

  /// <summary>Обновить меню и инструменты форматирования</summary>
  public void UpdateFormatCommands()
  {
    if (this.ActiveImDocumentEditorForm == null)
      return;
    this.ActiveImDocumentEditorForm.UpdateFormatCommands();
  }

  /// <summary>Отобразить информацию о возникшей исключительной ситуации (Exception)</summary>
  /// <param name="e">Возникшее исключение</param>
  /// <returns>Тип нажатой в окне кнопки</returns>
  public void ShowExceptionDialog(Exception e) => ExceptionHelper.ExceptionService.ShowException(e);

  /// <summary>Выполнить команду</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда выполнена</returns>
  public bool Execute(ICommandState commandState)
  {
    try
    {
      if (commandState == null)
        return false;
      this.BeginQuery();
      switch (commandState.CommandName)
      {
        case "AVS.SetupAVSTemplates":
          AVSWindow.ShowSetupAVSTemplates();
          return true;
        case "AVS.TEST":
          this.Test();
          int num1 = (int) MessageBox.Show("Test finished");
          return true;
        case "AVSPluginExecuteCommand_A_NastrTabl":
          Vedomost_VB_Static.AVSPluginExecuteCommand_A_NastrTabl(commandState);
          return true;
        case "AVSPluginExecuteCommand_A_NastrVed":
          Vedomost_VB_Static.AVSPluginExecuteCommand_A_NastrVed(commandState);
          return true;
        case "AVSPluginExecuteCommand_A_Nastr_ReadFilesAvs6":
          Vedomost_VB_Static.AVSPluginExecuteCommand_A_Nastr_ReadFilesAvs6(commandState);
          return true;
        case "AVSPluginExecuteCommand_ReCreate":
          Vedomost_VB_Static.AVSPluginExecuteCommand_ReCreate(commandState);
          return true;
        case "AVSPluginExecuteCommand_ReDrawing":
          Vedomost_VB_Static.AVSPluginExecuteCommand_ReDrawing(commandState);
          return true;
        case "Conformity_Template_Nastr_Command":
          Vedomost_VB_Static.Conformity_Template_Nastr_Command(commandState);
          return true;
        case "New.AVSDocument":
          this.CreateNewAssemblyWithSpecification();
          return true;
        default:
          if (this.activeAVSWindow != null)
          {
            switch (commandState.CommandName)
            {
              case "AVS.SelectGridColumns":
                this.SelectGridCols();
                return true;
              case "AVS.OpenInNewWindow":
                List<long> longList = new List<long>();
                foreach (AVSRow selectedSpecRow in this.activeAVSWindow.GetSelectedSpecRows(false))
                {
                  if (selectedSpecRow.ObjectId != -1L)
                    longList.Add(selectedSpecRow.ObjectId);
                }
                AVSPlugin.DoOpenInNewWindowCommand(longList.ToArray());
                return true;
            }
          }
          else if (this.isAVSTemplate)
          {
            switch (commandState.CommandName)
            {
              case "SpecificationTemplate.DesignationTrimSetup":
                int num2 = (int) new FormSetupDesignationTrim((SettingsStructure) null, this.activeImDocumentEditorForm.DocumentID).ShowDialog();
                return true;
              case "SpecificationTemplate.DocumentTypesWeights":
                int num3 = (int) DocumentTypesWeightsEditorForm.EditSystemCollection();
                return true;
              case "SpecificationTemplate.KeyWordsSetup":
                int num4 = (int) new FormSetupKeyWords((SettingsStructure) null, this.activeImDocumentEditorForm.DocumentID).ShowDialog();
                return true;
              case "SpecificationTemplate.RemarkAttributes":
                if (this.activeImDocumentEditorForm != null)
                {
                  if (AVSDocumentsSettings.Instance.IsSpecificationTemplate(this.activeImDocumentEditorForm.DocumentGuid))
                  {
                    int num5 = (int) RemarkAttributesForm.Execute(AVSDocument.ObjID_CommonSpecificationTemplate, AvsIDCache.Attr_NoteFieldSettings);
                  }
                  else if (this.activeImDocumentEditorForm.DocumentGuid == AvsIDCache.StdTemplateElementList)
                  {
                    int num6 = (int) RemarkAttributesForm.Execute(this.activeImDocumentEditorForm.DocumentID, AvsIDCache.Attr_NoteFieldSettings);
                  }
                }
                return true;
              case "SpecificationTemplate.SetupAVSDynamicGroupHeader":
                int num7 = (int) new DynamicGroupHeaderSettingsForm((SettingsStructure) null, this.activeImDocumentEditorForm.DocumentID).ShowDialog();
                return true;
              case "SpecificationTemplate.SetupAVSNumbering":
                int num8 = (int) new SetupNumberingSchemaForm((SettingsStructure) null, this.activeImDocumentEditorForm.DocumentID).ShowDialog();
                return true;
              case "SpecificationTemplate.SetupAVSSkipLines":
                int num9 = (int) new FormSetupSkipLines((SettingsStructure) null, this.activeImDocumentEditorForm.DocumentID).ShowDialog();
                return true;
              case "SpecificationTemplate.SetupAVSSorting":
                int num10 = (int) AVSPlugin.GetTemplateSetupSorting(this.activeImDocumentEditorForm.Document, this.activeImDocumentEditorForm.DocumentID, this.activeImDocumentEditorForm.DocumentType, -1L).ShowDialog();
                return true;
              case "SpecificationTemplate.SetupAVSTemplates":
                ImDocumentEditorForm documentEditorForm = this.activeImDocumentEditorForm;
                AVSWindow.ShowSetupAVSTemplates(documentEditorForm != null ? documentEditorForm.DocumentID : -1L);
                return true;
              case "SpecificationTemplate.SpecSectionsSetup":
                int num11 = (int) new SpecSectionsEditor((SettingsStructure) null, this.activeImDocumentEditorForm.DocumentID).ShowDialog();
                return true;
            }
          }
          else
          {
            AVSPluginExecuteCommand pluginExecuteCommand1;
            if ((this.isVedomostTemplate || this.isVedomost) && this.VedomostSettingsMenu.TryGetValue(commandState.CommandName, out pluginExecuteCommand1) && pluginExecuteCommand1 != null)
            {
              int num12 = pluginExecuteCommand1(commandState) ? 1 : 0;
            }
            AVSPluginExecuteCommand pluginExecuteCommand2;
            if ((this.isConstrTablTemplate || this.isConstrTabl) && this.TablSettingsMenu.TryGetValue(commandState.CommandName, out pluginExecuteCommand2) && pluginExecuteCommand2 != null)
            {
              int num13 = pluginExecuteCommand2(commandState) ? 1 : 0;
            }
          }
          this.EndQuery();
          break;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return false;
  }

  public static void DoOpenInNewWindowCommand(long[] objIDs)
  {
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objIDs);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
  }

  /// <summary> Начало проверки статуса команд  </summary>
  public void BeginQuery()
  {
    this.activeAVSWindow = this.ActiveAVSWindow;
    if (AVSPlugin.DockManager.ActiveDockControl != this.activeAVSWindow)
      this.activeAVSWindow = (AVSWindow) null;
    this.avsWindow = this.activeAVSWindow != null;
    this.activeImDocumentEditorForm = this.ActiveImDocumentEditorForm;
    this.docWindow = this.activeImDocumentEditorForm != null;
    this.isAVSTemplate = this.activeImDocumentEditorForm != null && MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_ConstructorDocumentTemplate);
    this.isSpecificationTemplate = this.isAVSTemplate && AVSDocumentsSettings.Instance.IsSpecificationTemplate(this.activeImDocumentEditorForm.DocumentGuid);
    this.isElementListTemplate = this.isAVSTemplate && AVSDocumentsSettings.Instance.IsElementListTemplate(this.activeImDocumentEditorForm.DocumentGuid);
    this.isVedomostTemplate = this.activeImDocumentEditorForm != null && MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_VedomostDocumentTemplate);
    this.isVedomost = this.activeImDocumentEditorForm != null && (MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_Vedomost) || MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_DocumsExpluat) || MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_DocumsProg));
    this.isConstrTablTemplate = this.activeImDocumentEditorForm != null && MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_ConstrTablTemplate);
    this.isConstrTabl = this.activeImDocumentEditorForm != null && MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_ConstrTabl);
    this.isConstrSpecification = this.activeImDocumentEditorForm != null && MetaDataHelper.IsObjectTypeChildOf(this.activeImDocumentEditorForm.DocumentType, AvsIDCache.ObjType_Specification);
    if (this.isVedomost)
    {
      if (this.activeImDocumentEditorForm is VedomostEditorWindow documentEditorForm)
      {
        if (documentEditorForm.ReadOnly)
        {
          this.isVedomost = false;
          this.isVedomostTemplate = true;
        }
        else
          this.typePageVedom = documentEditorForm.typePageVedom;
      }
      else
        this.isVedomost = false;
    }
    if (Vedomost_VB_Static.Is_Check_For_VB)
      return;
    Vedomost_VB_Static.Check_For_VB();
  }

  /// <summary> Завершения проверки статуса команд  </summary>
  public void EndQuery()
  {
    this.activeAVSWindow = (AVSWindow) null;
    this.avsWindow = false;
    this.activeImDocumentEditorForm = (ImDocumentEditorForm) null;
    this.docWindow = false;
    this.isAVSTemplate = false;
    this.isVedomostTemplate = false;
    this.isVedomost = false;
    this.isConstrTablTemplate = false;
    this.isConstrTabl = false;
  }

  /// <summary>Проверить состояние команды</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда найдена</returns>
  public bool QueryStatus(ICommandState commandState)
  {
    try
    {
      if (commandState == null)
        return false;
      bool flag1 = false;
      switch (commandState.CommandName)
      {
        case "AVS.AddNewSpecRow":
        case "AVS.AddSpecRow":
        case "AVS.AddSpecRowFromImbase":
        case "AVS.AddSpecSection":
        case "AVS.ClearNumberPositions":
        case "AVS.NumberPositions":
        case "AVS.OpenInNewWindow":
        case "AVS.Property":
        case "AVS.RowDown":
        case "AVS.RowProperties":
        case "AVS.RowUp":
        case "AVS.SelectGridColumns":
        case "AVS.SetOccurenceKey":
        case "AVS.Sort":
        case "AVS.SortRazdel":
          flag1 = true;
          break;
      }
      this.avsWindow = this.activeAVSWindow != null;
      bool flag2 = this.activeAVSWindow != null && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.IsSpecification;
      switch (commandState.CommandName)
      {
        case "AVS":
        case "AVSSetting":
          commandState.Enabled = this.avsWindow;
          commandState.Visible = commandState.Enabled;
          return true;
        case "AVS.AddAdditionalChapter":
        case "AVS.AddOtherRecordTypes":
        case "AVS.CreateElementList":
        case "AVS.CreateVedomost_VB":
        case "AVS.DocumentProperty":
        case "AVS.DynamicGroupHeaderSetup":
        case "AVS.GridViewMode":
        case "AVS.PageViewMode":
        case "AVS.Properties":
        case "AVS.ReplaceDocInSpecRow":
        case "AVS.ReplaceSpecRow":
        case "AVS.ReplaceSpecRowFromImbase":
        case "AVS.ReplaceSpecRowVersion":
        case "AVS.ShowDocumentTreeView_Vyvod":
        case "AVS.SkipLinesSetup":
        case "AVS.SortingSchema":
        case "AVS.VersionAttributes":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = this.avsWindow;
          return true;
        case "AVS.AddDopComplect":
        case "AVS.AddNewSpecRow":
        case "AVS.AddSpecRow":
        case "AVS.AddSpecRowFromImbase":
        case "AVS.AddSpecSection":
        case "AVS.CheckIn":
        case "AVS.CheckOut":
        case "AVS.SetOccurenceKey":
        case "AVS.Sort":
        case "AVS.SortRazdel":
          commandState.Visible = this.avsWindow | flag1;
          commandState.Enabled = this.avsWindow;
          return true;
        case "AVS.AddIspoln":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = commandState.Visible && this.avsWindow && this.activeAVSWindow.ObjectIsSelected && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.AvsDocumentForm != 0;
          return true;
        case "AVS.AddRecordsVB":
          if (this.isVedomost && this.typePageVedom == Vedomost_VB_Static.TypePageVedom.Info)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "AVS.AddSkipLineAfter":
        case "AVS.AddSkipLineBefore":
        case "AVS.FromNewPage":
        case "AVS.UndoFromNewPage":
        case "AVS.UndoSkipLineAfter":
        case "AVS.UndoSkipLineBefore":
          List<AVSRow> source1 = this.activeAVSWindow?.GetSelectedSpecRows(false) ?? new List<AVSRow>();
          commandState.Visible = this.avsWindow && source1.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.AddZagotovkaForPart":
        case "AVS.AddZagotovkaForPart_FromImBase":
        case "AVS.ConvertFromZagotovka":
          commandState.Visible = this.avsWindow && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.IsSpecification;
          commandState.Enabled = commandState.Visible && this.avsWindow && this.activeAVSWindow.ObjectIsSelected;
          return true;
        case "AVS.AdditionalChaptersSetup":
        case "AVS.AssemblyProperty":
        case "AVS.DeleteEmptySections":
        case "AVS.DesignationTrimSetup":
        case "AVS.DocumentTypesWeights":
        case "AVS.HideDocRowsWithoutCount":
        case "AVS.HideSameChapters":
        case "AVS.KeyWordsSetup":
        case "AVS.SetupNumberingSchema":
        case "AVS.ShowAllDocRows":
        case "AVS.ShowEmptySections":
        case "AVS.ShowSameChapters":
        case "AVS.SpecSectionsSetup":
          commandState.Visible = this.avsWindow & flag2;
          commandState.Enabled = this.avsWindow & flag2;
          return true;
        case "AVS.ChangeRecordIspolnenie":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = commandState.Visible && this.avsWindow && this.activeAVSWindow.ObjectIsSelected && this.activeAVSWindow.AVSDocument != null;
          return true;
        case "AVS.CheckErrors":
          commandState.Visible = this.avsWindow && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.IsSpecification && !this.activeAVSWindow.AVSDocument.IsEmpty;
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.ClearNumberPositions":
          commandState.Visible = (this.avsWindow | flag1) & flag2;
          commandState.Enabled = this.avsWindow & flag2;
          return true;
        case "AVS.ClearSmotri":
        case "AVS.RefreshFormatAndSmotri":
        case "AVS.RefreshMass":
          commandState.Visible = this.avsWindow && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.IsSpecification && !this.activeAVSWindow.AVSDocument.IsEmpty;
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.CommonPositions":
          List<AVSRow> source2 = this.activeAVSWindow?.GetSelectedSpecRows(true) ?? new List<AVSRow>();
          commandState.Visible = this.avsWindow & flag2 && source2.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.CopyRecord":
        case "AVS.DeleteObjects":
        case "AVS.DeleteRecords":
        case "AVS.SpecificationForm":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = this.avsWindow && this.activeAVSWindow.ObjectIsSelected;
          return true;
        case "AVS.CreateDocumentFromFile_VB":
          if (AvsConfig.General.AskAVS6 && Vedomost_VB_Static.IsAvs6ToIps)
          {
            commandState.Visible = this.avsWindow;
            commandState.Enabled = this.avsWindow;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "AVS.DeleteTitlePage":
        case "AVS.DisconnectSort":
        case "AVS.InsertAdditionalPages":
        case "AVS.InsertTitlePage":
        case "AVS.PasteBreak":
        case "AVS.PasteNonBreakSpace":
        case "AVS.RemoveAdditionalPages":
        case "AVS.SortAfter":
        case "AVS.SortBefore":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = this.avsWindow;
          return true;
        case "AVS.DontIncludeClassNameInGroupRow":
        case "AVS.IncludeClassNameInGroupRow":
          commandState.Visible = this.avsWindow && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.Document.DynamicGroupHeaderIsEnabled;
          commandState.Enabled = commandState.Visible && !this.activeAVSWindow.ReadOnly && this.activeAVSWindow.GetSelectedSpecRows(false).Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.ObjectId.IsDefinedId()));
          return true;
        case "AVS.FinishWork":
          commandState.Visible = false;
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.GroupRows.Submenu":
          ICommandState commandState1 = commandState;
          ICommandState commandState2 = commandState;
          AVSWindow activeAvsWindow = this.activeAVSWindow;
          int num1 = activeAvsWindow != null ? (activeAvsWindow.ReadOnly ? 1 : 0) : 1;
          int num2;
          bool flag3 = (num2 = num1 == 0 ? 1 : 0) != 0;
          commandState2.Visible = num2 != 0;
          int num3 = flag3 ? 1 : 0;
          commandState1.Enabled = num3 != 0;
          return true;
        case "AVS.GroupRowsByHeader":
          commandState.Visible = this.avsWindow && this.activeAVSWindow.AVSDocument != null && !this.activeAVSWindow.AVSDocument.Document.DynamicGroupHeaderIsEnabled;
          commandState.Enabled = commandState.Visible && !this.activeAVSWindow.ReadOnly;
          return true;
        case "AVS.ImbaseCatalogsSetup":
          commandState.Visible = this.avsWindow && !flag2;
          return true;
        case "AVS.MoveSpecRow":
        case "AVS.MoveSpecRowToChapter":
          commandState.Visible = this.avsWindow & flag2;
          commandState.Enabled = this.avsWindow & flag2 && this.activeAVSWindow.ObjectIsSelected;
          return true;
        case "AVS.NavigatorCommands":
          commandState.Visible = this.avsWindow && (AVSSelectedItemsHelper.GetSelectedIds(this.activeAVSWindow).Count > 0 || AVSSelectedItemsHelper.GetSelectedNodes(this.activeAVSWindow, false, false).Count > 0);
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.OpenInNewWindow":
          commandState.Visible = this.avsWindow | flag1;
          commandState.Enabled = this.avsWindow && this.activeAVSWindow.ObjectIsSelected;
          return true;
        case "AVS.ParentProductsList":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.ProductsList":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = commandState.Visible && this.avsWindow && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.AvsDocumentForm != 0;
          return true;
        case "AVS.Property":
          commandState.Visible = this.avsWindow | flag1;
          commandState.Enabled = this.avsWindow;
          commandState.Checked = this.activeAVSWindow != null && this.activeAVSWindow.BottomPanelType == AVSWindow.enumBottomPanelType.SelectedRowProperties;
          return true;
        case "AVS.RemarkAttributes":
          commandState.Visible = ((!ImDocumentData.ShowDebugInfo ? 0 : (this.avsWindow ? 1 : 0)) & (flag2 ? 1 : 0)) != 0;
          commandState.Enabled = this.avsWindow & flag2;
          return true;
        case "AVS.ReplaceTemplate":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = this.avsWindow && !this.activeAVSWindow.ReadOnly;
          return true;
        case "AVS.RowDown":
        case "AVS.RowProperties":
        case "AVS.RowUp":
          commandState.Visible = ((!this.avsWindow ? 0 : (this.activeAVSWindow.AVSDocument != null ? 1 : 0)) | (flag1 ? 1 : 0)) != 0;
          commandState.Enabled = this.avsWindow && this.activeAVSWindow.AVSDocument != null;
          return true;
        case "AVS.SelectGridColumns":
          commandState.Visible = this.avsWindow | flag1;
          commandState.Enabled = this.avsWindow && this.activeAVSWindow.ViewMode == AVSViewMode.Grid;
          return true;
        case "AVS.SetupAVSTemplates":
          commandState.Visible = !this.avsWindow && !this.isAVSTemplate;
          commandState.Enabled = !this.avsWindow && !this.isAVSTemplate;
          return true;
        case "AVS.SumPositionDesignation":
        case "AVS.UpdateDocumentStructure":
          commandState.Visible = this.avsWindow && (flag2 || this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.IsElementList);
          commandState.Enabled = commandState.Visible;
          return true;
        case "AVS.TEST":
          commandState.Visible = ImDocumentData.ShowDebugInfo && Environment.MachineName == "KOLTSOV";
          commandState.Enabled = true;
          return true;
        case "AVS.UnGroupRowsByHeader":
          commandState.Visible = this.avsWindow && this.activeAVSWindow.AVSDocument != null && this.activeAVSWindow.AVSDocument.Document.DynamicGroupHeaderIsEnabled;
          commandState.Enabled = commandState.Visible && !this.activeAVSWindow.ReadOnly;
          return true;
        case "AVS.VB.AddVedRow_Additional1":
        case "AVS.VB.AddVedRow_Additional2":
        case "AVS.VB.AddVedRow_Additional3":
        case "AVS.VB.AddVedRow_Additional4":
        case "AVS.VB.AddVedRow_Empty":
        case "AVS.VB.AddVedRow_PodZagolovok":
        case "AVS.VB.AddVedRow_Remark":
        case "AVS.VB.AddVedRow_RemarkShort":
        case "AVS.VB.AddVedRow_TitlePart":
        case "AVS.VB.AddVedRow_Zagolovok":
          if (!this.isVedomost || this.typePageVedom != Vedomost_VB_Static.TypePageVedom.Info)
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          else
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          return true;
        case "AVS.VB.Create_Document_From_Avs6File":
          if (AvsConfig.General.AskAVS6 && Vedomost_VB_Static.IsAvs6ToIps)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "AVS.VB.Menu":
          string machineName = Environment.MachineName;
          commandState.Enabled = this.isVedomost || this.isConstrTabl;
          commandState.Visible = commandState.Enabled;
          return true;
        case "AVS.VB.TablMenu":
          commandState.Enabled = this.isConstrTabl;
          commandState.Visible = commandState.Enabled;
          return true;
        case "AVSParametersCard":
          commandState.Visible = this.avsWindow;
          commandState.Enabled = this.avsWindow;
          this.iNamedImageList = this.iNamedImageList ?? (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
          MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("AVSParametersCard");
          if (contextMenuItem != null && contextMenuItem.Image == null && this.iNamedImageList.ImageList != null && this.iNamedImageList.ImageList.Images != null)
          {
            BarManager service = (BarManager) ServicesManager.GetService(typeof (BarManager));
            if (service != null && service.MenuBar != null)
            {
              MenuItemBase menuItem = service.MenuBar.FindMenuItem("mnObjects.ParametersCard");
              if (menuItem != null && menuItem.ImageIndex >= 0 && menuItem.ImageIndex < this.iNamedImageList.ImageList.Images.Count)
                contextMenuItem.Image = this.iNamedImageList.ImageList.Images[menuItem.ImageIndex];
            }
          }
          return true;
        case "AVSPluginExecuteCommand_A_NastrTabl":
          if (this.isConstrTabl || this.isConstrTablTemplate || this.isConstrSpecification)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "AVSPluginExecuteCommand_A_NastrVed":
          if (this.isVedomost || this.isVedomostTemplate || this.isConstrSpecification)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "AVSPluginExecuteCommand_A_Nastr_ReadFilesAvs6":
          if (AvsConfig.General.AskAVS6 && (this.isConstrTabl || this.isConstrTablTemplate || this.isVedomost || this.isVedomostTemplate || this.isVedomostTemplate || this.isConstrSpecification))
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "Conformity_Template_Nastr_Command":
          if (this.isConstrTabl || this.isConstrTablTemplate || this.isVedomost || this.isVedomostTemplate || this.isVedomostTemplate || this.isConstrSpecification)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "DocEditor.ReplaceTemplate":
          if (!this.avsWindow)
            return false;
          commandState.Visible = false;
          commandState.Enabled = false;
          return true;
        case "Filled_Data_From_File_Avs6":
          if (AvsConfig.General.AskAVS6 && (this.isConstrTabl || this.isVedomost) && Vedomost_VB_Static.IsAvs6ToIps)
          {
            commandState.Visible = true;
            commandState.Enabled = true;
          }
          else
          {
            commandState.Visible = false;
            commandState.Enabled = false;
          }
          return true;
        case "New.AVSDocument":
          commandState.Visible = true;
          commandState.Enabled = true;
          return true;
        case "SpecificationTemplate":
        case "SpecificationTemplate.DocumentTypesWeights":
        case "SpecificationTemplate.SetupAVSTemplates":
          commandState.Visible = this.isAVSTemplate;
          commandState.Enabled = this.isAVSTemplate;
          return true;
        case "SpecificationTemplate.DesignationTrimSetup":
        case "SpecificationTemplate.SetupAVSNumbering":
        case "SpecificationTemplate.SpecSectionsSetup":
          commandState.Enabled = this.isSpecificationTemplate;
          commandState.Visible = commandState.Enabled;
          return true;
        case "SpecificationTemplate.KeyWordsSetup":
        case "SpecificationTemplate.SetupAVSDynamicGroupHeader":
        case "SpecificationTemplate.SetupAVSSkipLines":
        case "SpecificationTemplate.SetupAVSSorting":
          commandState.Enabled = this.isSpecificationTemplate || this.isElementListTemplate;
          commandState.Visible = commandState.Enabled;
          return true;
        case "SpecificationTemplate.RemarkAttributes":
          commandState.Visible = ImDocumentData.ShowDebugInfo && this.isSpecificationTemplate;
          commandState.Enabled = this.isSpecificationTemplate;
          return true;
        default:
          if (this.VedomostSettingsMenu.ContainsKey(commandState.CommandName))
          {
            if (this.isVedomostTemplate || this.isVedomost)
            {
              commandState.Visible = true;
              commandState.Enabled = true;
            }
            else
            {
              commandState.Visible = false;
              commandState.Enabled = false;
            }
            return true;
          }
          if (this.TablSettingsMenu.ContainsKey(commandState.CommandName))
          {
            if (this.isConstrTablTemplate || this.isConstrTabl)
            {
              commandState.Visible = true;
              commandState.Enabled = true;
            }
            else
            {
              commandState.Visible = false;
              commandState.Enabled = false;
            }
            return true;
          }
          if (this.VedomostEditorVBMenu.ContainsKey(commandState.CommandName))
          {
            if (this.isVedomost)
            {
              commandState.Visible = true;
              commandState.Enabled = true;
            }
            else
            {
              commandState.Visible = false;
              commandState.Enabled = false;
            }
            return true;
          }
          if (this.TablEditorVBMenu.ContainsKey(commandState.CommandName))
          {
            if (this.isConstrTabl)
            {
              commandState.Visible = true;
              commandState.Enabled = true;
            }
            else
            {
              commandState.Visible = false;
              commandState.Enabled = false;
            }
            return true;
          }
          if (this.ExternalAVSCommands.ContainsKey(commandState.CommandName))
          {
            commandState.Visible = this.avsWindow;
            commandState.Enabled = this.avsWindow && !this.activeAVSWindow.ReadOnly;
            return true;
          }
          break;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return false;
  }

  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для
  /// выделенных элементов навигации одной категории и типа.
  /// Например, если в «Навигаторе» выделены элементы навигации нескольких разных категорий и типов,
  /// то данная команда будет вызываться для каждой из подгрупп этих элементов, сгруппированных
  /// по их категориям и типам. Наиболее применяемый метод данного интерфейса.
  /// Позволяет перекрывать команды контекстного меню для элементов навигации определённых категорий,
  /// типов, задавая более высокий приоритет описаниям этих команд.
  /// ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе данных  для того,
  /// чтобы проверить, можно ли отображать команду меню или нет!
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  CommandsInfo ICommandsProvider.GetMergedCommands(
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    if (items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectID < 0L || !MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545")) && !MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545")))
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("OpenDocument", new CommandInfo(3, new ClickEventHandler(ObjectCommands.ViewCommand)));
    return mergedCommands;
  }

  CommandsInfo ICommandsProvider.GetGroupCommands(
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    try
    {
      if (items.Count == 1)
      {
        if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          int num = MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545")) ? 1 : 0;
          bool flag = MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545"));
          if (num != 0)
            groupCommands.Add("OpenInNewWindow", new CommandInfo(0, new ClickEventHandler(AVSPlugin.OpenInNewWindowCommand)));
          if ((num | (flag ? 1 : 0)) != 0)
            groupCommands.Add("CreateElementList", new CommandInfo(0, new ClickEventHandler(this.CreateElementList)));
          if (ImDocumentEditorConfig.Instance.ShowDebugInfo)
            groupCommands.Add("ClearDocument", new CommandInfo(0, new ClickEventHandler(this.ClearDocument)));
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return groupCommands;
  }

  private void ClearDocument(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      IDBObject dbObject = session.GetObject(itemData.ObjectID, false);
      if (dbObject == null)
        return;
      (long num, List<long> products) = AvsIDCache.FindSpecificationAndAssemblyProducts(dbObject, "");
      if (num.IsDefinedId())
        (num == dbObject.ObjectID ? (IDBAttributable) dbObject : (IDBAttributable) session.GetObject(itemData.ObjectID, false)).GetAttributeByID(AvsIDCache.Attr_File)?.Clear();
      foreach (long projId in products)
      {
        DataTable childSostavData = DataHelper.GetChildSostavData(projId, session, (IEnumerable<int>) AVSSpecification.GetDefaultRelationTypesUsedInSpecification(), false);
        if (childSostavData != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
          {
            Convert.ToInt64(row["F_OBJECT_ID"]);
            long int64 = Convert.ToInt64(row["F_PRJLINK_ID"]);
            IDBRelation relation = session.GetRelation(int64, false);
            if (relation != null)
            {
              relation.GetAttributeByID(AvsIDCache.Attr_SpecificationSection)?.Clear();
              relation.GetAttributeByID(AvsIDCache.Attr_SortIndex)?.Clear();
            }
          }
        }
      }
    }
  }

  private void CreateElementList(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    AVSPlugin.CreateElementListById(itemData.ObjectID, itemData.ObjectType);
  }

  internal static void CreateElementListById(long selObjId, int selObjTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = false;
      long assemblyId = -1;
      DataTable dataTable;
      if (selObjTypeId == AvsIDCache.ObjType_Specification)
      {
        dataTable = DataHelper.GetParentSostavData(selObjId, sessionKeeper.Session, (IEnumerable<int>) new List<int>()
        {
          AvsIDCache.Relation_Document
        }, false);
      }
      else
      {
        assemblyId = selObjId;
        dataTable = DataHelper.GetChildSostavData(selObjId, sessionKeeper.Session, (IEnumerable<int>) new List<int>()
        {
          AvsIDCache.Relation_Document
        }, false);
      }
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64_1 = Convert.ToInt64(row["F_OBJECT_ID"]);
          long int64_2 = Convert.ToInt64(row["F_OBJECT_TYPE"]);
          if (assemblyId == -1L && int64_2 == (long) AvsIDCache.ObjType_AssemblyUnit)
            assemblyId = int64_1;
          if (AVSPlugin.ServiceProvider.GetService(typeof (IFactory)) is IFactory service)
          {
            ICommandsProvider[] commandsProviders = service.GetCommandsProviders();
            if (commandsProviders != null)
            {
              foreach (ICommandsProvider commandsProvider1 in commandsProviders)
              {
                if (commandsProvider1 is ECADCommandsProvider commandsProvider2 && (long) commandsProvider2.ObjType == int64_2)
                {
                  commandsProvider2.CreateElementList(sessionKeeper.Session, int64_1);
                  flag = true;
                }
              }
            }
          }
        }
      }
      if (flag || assemblyId == -1L)
        return;
      (ServicesManager.GetService(typeof (IElementListCreatorService)) as ElementListCreatorService).CreateElementList(sessionKeeper.Session, assemblyId);
    }
  }

  /// <summary>Обработчик команды меню "Табличный отчет"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void OpenInNewWindowCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    try
    {
      if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
        ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) AvsIDCache.Attr_Name, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
        // ISSUE: variable of a boxed type
        __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
        long objectId = itemData.ObjectID;
        int relationDocument = AvsIDCache.Relation_Document;
        List<ColumnDescriptor> columns = columnDescriptorList;
        string filtrationServiceOwnerId = service.FiltrationServiceOwnerID;
        int[] numArray = Array.Empty<int>();
        DataTable source1 = customService.LoadCompositionApplicability((object) sessionGuid, objectId, relationDocument, (IEnumerable<ColumnDescriptor>) columns, filtrationServiceOwnerId, numArray);
        string selectedDesignation = "";
        IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_Designation);
          if (attributeById != null)
            selectedDesignation = attributeById.AsString;
        }
        long num = 0;
        if (source1 != null)
        {
          EnumerableRowCollection<\u003C\u003Ef__AnonymousType0<long, int, string, string>> source2 = source1.AsEnumerable().Select(product => new
          {
            objId = product[0] != DBNull.Value ? Convert.ToInt64(product[0]) : -1L,
            objType = product[2] != DBNull.Value ? Convert.ToInt32(product[2]) : -1,
            name = product[3] != DBNull.Value ? Convert.ToString(product[3]) : "",
            designation = product[4] != DBNull.Value ? Convert.ToString(product[4]) : ""
          });
          List<\u003C\u003Ef__AnonymousType0<long, int, string, string>> list1 = source2.ToList();
          if (list1.Count > 0)
          {
            List<\u003C\u003Ef__AnonymousType0<long, int, string, string>> source3 = source2.Where(x => MetaDataHelper.IsObjectTypeChildOf(x.objType, AvsIDCache.ObjType_Product) || x.objType == AvsIDCache.ObjType_Product).ToList();
            if (source3.Count == 0)
              source3 = list1;
            List<\u003C\u003Ef__AnonymousType0<long, int, string, string>> list2 = source3.Where(x => x.designation == selectedDesignation).ToList();
            num = list2.Count != 0 ? list2[0].objId : source3[0].objId;
          }
        }
        if (num != 0L)
          Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(Intermech.Navigator.ContextMenu.Services.GetItems(num), viewServices), viewServices);
        else
          Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(itemData.ObjectID), viewServices);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>
  /// Временный метод для запуска модульных тестов без каркаса
  /// </summary>
  private void Test()
  {
    this.Test_SummPosDesignation();
    this.Test_SummPosDesignationWithFuncGroupAndPodbor();
    this.Test_FindFileIndexForExtensions();
  }

  private void Test_FindFileIndexForExtensions()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("{f477c520-9bea-465a-934e-98323b842119}"), false);
        if (dbObject == null)
        {
          int num1 = (int) MessageBox.Show("Тестовый объект не найден, тест пропущен", nameof (Test_FindFileIndexForExtensions));
        }
        else
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_File);
          if (attributeById == null)
          {
            int num2 = (int) MessageBox.Show("Тестовый атрибут не найден, тест пропущен", nameof (Test_FindFileIndexForExtensions));
          }
          DocumentEditorPluginBase.FindAnyImDocumentInAttribute(attributeById);
          DocumentEditorPlugin.FindOldSPFileExtensionInAttribute(attributeById);
          DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(attributeById, (IList<string>) ImDocumentData.OldImDocumentExtensions);
          DocumentEditorPluginBase.FindFirstFileExtensionInAttribute(attributeById, (IList<string>) ImDocumentData.OldBlankExtensions);
        }
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }

  private void Test_SummPosDesignation()
  {
    List<string> stringList1 = new List<string>((IEnumerable<string>) new string[1]
    {
      "C1,C2,C3,C4,C11"
    });
    List<string> stringList2 = new List<string>((IEnumerable<string>) new string[5]
    {
      "C1",
      "C2",
      "C3",
      "C4",
      "C11"
    });
    List<string> stringList3 = new List<string>((IEnumerable<string>) new string[9]
    {
      "C1",
      "C2",
      "C3",
      "C4",
      "C11",
      "C1",
      "C2",
      "C3",
      "C4"
    });
    List<string> stringList4 = new List<string>((IEnumerable<string>) new string[9]
    {
      "C1",
      "C2",
      "C3",
      "C4",
      "C11",
      "C13",
      "C14",
      "C15",
      "C40"
    });
    List<string> stringList5 = new List<string>((IEnumerable<string>) new string[2]
    {
      "C1",
      "C2"
    });
    List<string> stringList6 = new List<string>((IEnumerable<string>) new string[2]
    {
      "C1",
      "C11"
    });
    List<string> stringList7 = new List<string>((IEnumerable<string>) new string[2]
    {
      "C1",
      "C3"
    });
    List<string> stringList8 = new List<string>((IEnumerable<string>) new string[3]
    {
      "C1",
      "C2",
      "C3"
    });
    List<string> stringList9 = new List<string>((IEnumerable<string>) new string[1]
    {
      "C1, C2, C3, C4, C5, C6"
    });
    PosDesignationRecord.ParsePositionalDesignation("C-2, C-4-C-7, C-11");
    new List<string>() { "ЩА-3-ЩА-6,ЩА- 7-ЩА-10,ЩА-13" };
  }

  private void Test_SummPosDesignationWithFuncGroupAndPodbor()
  {
    new List<PosDesignationRecord>((IEnumerable<PosDesignationRecord>) new PosDesignationRecord[3]
    {
      new PosDesignationRecord("C1", "A1", (string) null),
      new PosDesignationRecord("C4", "A1", (string) null),
      new PosDesignationRecord("C11", "A1", (string) null)
    }).AddRange((IEnumerable<PosDesignationRecord>) PosDesignationRecord.ParsePositionalDesignation("C2, C3", "A1"));
    List<PosDesignationRecord> designationRecordList1 = new List<PosDesignationRecord>((IEnumerable<PosDesignationRecord>) new PosDesignationRecord[9]
    {
      new PosDesignationRecord("C1", "A1", (string) null),
      new PosDesignationRecord("C2", "A1", (string) null),
      new PosDesignationRecord("C3", "A1", (string) null),
      new PosDesignationRecord("C4", "A1", (string) null),
      new PosDesignationRecord("C11", "A1", (string) null),
      new PosDesignationRecord("C1", "A1", (string) null),
      new PosDesignationRecord("C2", "A1", (string) null),
      new PosDesignationRecord("C3", "A1", (string) null),
      new PosDesignationRecord("C4", "A1", (string) null)
    });
    new List<PosDesignationRecord>((IEnumerable<PosDesignationRecord>) new PosDesignationRecord[7]
    {
      new PosDesignationRecord("C3", "A1", (string) null),
      new PosDesignationRecord("C4", "A1", (string) null),
      new PosDesignationRecord("C11", "A1", (string) null),
      new PosDesignationRecord("C13", "A1", (string) null),
      new PosDesignationRecord("C14", "A1", (string) null),
      new PosDesignationRecord("C15", "A1", (string) null),
      new PosDesignationRecord("C40", "A1", (string) null)
    }).AddRange((IEnumerable<PosDesignationRecord>) PosDesignationRecord.ParsePositionalDesignation("C1,C2", "A1"));
    List<PosDesignationRecord> designationRecordList2 = new List<PosDesignationRecord>((IEnumerable<PosDesignationRecord>) new PosDesignationRecord[2]
    {
      new PosDesignationRecord("C1", "A1", (string) null),
      new PosDesignationRecord("C2", "A2", (string) null)
    });
    List<PosDesignationRecord> designationRecordList3 = new List<PosDesignationRecord>((IEnumerable<PosDesignationRecord>) new PosDesignationRecord[14]
    {
      new PosDesignationRecord("R1", "", ""),
      new PosDesignationRecord("R2", "", ""),
      new PosDesignationRecord("R5", "", ""),
      new PosDesignationRecord("R7", "", ""),
      new PosDesignationRecord("R8", "", ""),
      new PosDesignationRecord("R9", "", ""),
      new PosDesignationRecord("R11", "", ""),
      new PosDesignationRecord("R3", "", "*"),
      new PosDesignationRecord("R4", "", "*"),
      new PosDesignationRecord("R6", "", "*"),
      new PosDesignationRecord("R10", "", "*"),
      new PosDesignationRecord("R12", "", "*"),
      new PosDesignationRecord("R13", "", "*"),
      new PosDesignationRecord("R14", "", "*")
    });
    List<PosDesignationRecord> designationRecordList4 = new List<PosDesignationRecord>((IEnumerable<PosDesignationRecord>) new PosDesignationRecord[21]
    {
      new PosDesignationRecord("R1", "", ""),
      new PosDesignationRecord("R2", "", ""),
      new PosDesignationRecord("R5", "", ""),
      new PosDesignationRecord("R7", "", ""),
      new PosDesignationRecord("R8", "", ""),
      new PosDesignationRecord("R9", "", ""),
      new PosDesignationRecord("R12", "A1", ""),
      new PosDesignationRecord("R13", "A1", ""),
      new PosDesignationRecord("R14", "A1", ""),
      new PosDesignationRecord("R1", "A2", ""),
      new PosDesignationRecord("R2", "A2", ""),
      new PosDesignationRecord("R3", "", "*"),
      new PosDesignationRecord("R4", "", "*"),
      new PosDesignationRecord("R6", "", "*"),
      new PosDesignationRecord("R10", "", "*"),
      new PosDesignationRecord("R11", "", "*"),
      new PosDesignationRecord("R12", "", "*"),
      new PosDesignationRecord("R15", "A1", "*"),
      new PosDesignationRecord("R16", "A1", "*"),
      new PosDesignationRecord("R3", "A2", "*"),
      new PosDesignationRecord("R4", "A2", "*")
    });
  }

  public IConfigurationManager ConfigurationManager => this.configManager;

  /// <summary>
  /// Экземпляр плагина создан и успешно загружен (инициализирован)
  /// </summary>
  public static bool HasLoadedInstance
  {
    get
    {
      AVSPlugin instance = AVSPlugin._instance;
      return instance != null && instance.IsLoaded;
    }
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать номер раздела спецификации, значением - идентификатор раздела </summary>
  public static Dictionary<int, long> GetSectionNumToSectionIdDictionary()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return AVSPlugin.GetSectionNumToSectionIdDictionary(sessionKeeper.Session);
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать номер раздела спецификации, значением - идентификатор раздела </summary>
  public static Dictionary<int, long> GetSectionNumToSectionIdDictionary(IUserSession session)
  {
    if (session == null)
      return AVSPlugin.GetSectionNumToSectionIdDictionary();
    DataTable dataTable = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SectionNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable == null)
      return (Dictionary<int, long>) null;
    Dictionary<int, long> sectionIdDictionary = new Dictionary<int, long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (!(row[1] is DBNull) && !(row[0] is DBNull))
        sectionIdDictionary[Convert.ToInt32(row[1])] = Convert.ToInt64(row[0]);
    }
    return sectionIdDictionary;
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать номер части спецификации, значением - идентификатор части </summary>
  public static Dictionary<int, long> GetPartNumToPartIdDictionary(IUserSession session)
  {
    if (session == null)
      return AVSPlugin.GetSectionNumToSectionIdDictionary();
    DataTable dataTable = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationChapter).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_PartNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable == null)
      return (Dictionary<int, long>) null;
    Dictionary<int, long> partIdDictionary = new Dictionary<int, long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (!(row[1] is DBNull) && !(row[0] is DBNull))
        partIdDictionary[Convert.ToInt32(row[1])] = Convert.ToInt64(row[0]);
    }
    return partIdDictionary;
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать номер раздела спецификации, значением - Guid раздела </summary>
  public static Dictionary<string, long> GetSectionNumToSectionGuidDictionary()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return AVSPlugin.GetSectionNumToSectionGuidDictionary(sessionKeeper.Session);
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать заголовок раздела спецификации, значением - идентификатор раздела </summary>
  public Dictionary<string, long> GetSectionCaptionToSectionIdDictionary(IUserSession session)
  {
    if (session == null)
      return this.GetSectionCaptionToSectionIdDictionary();
    DataTable dataTable = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationChapter).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable == null)
      return (Dictionary<string, long>) null;
    Dictionary<string, long> sectionIdDictionary = new Dictionary<string, long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (!(row[1] is DBNull) && !(row[0] is DBNull))
        sectionIdDictionary[Convert.ToString(row[1])] = Convert.ToInt64(row[0]);
    }
    return sectionIdDictionary;
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать заголовок раздела спецификации, значением - идентификатор раздела </summary>
  public Dictionary<string, long> GetSectionCaptionToSectionIdDictionary()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.GetSectionCaptionToSectionIdDictionary(sessionKeeper.Session);
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать номер раздела спецификации, значением - Guid раздела </summary>
  public static Dictionary<string, long> GetSectionNumToSectionGuidDictionary(IUserSession session)
  {
    if (session == null)
      return AVSPlugin.GetSectionNumToSectionGuidDictionary();
    DataTable dataTable = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable == null)
      return (Dictionary<string, long>) null;
    Dictionary<string, long> sectionGuidDictionary = new Dictionary<string, long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (!(row[1] is DBNull) && !(row[0] is DBNull))
        sectionGuidDictionary[Convert.ToString(row[0])] = Convert.ToInt64(row[1]);
    }
    return sectionGuidDictionary;
  }

  /// <summary> Получить хэш-таблицу, где ключом будет выступать идентификатор раздела спецификации, значением - номер раздела спецификации </summary>
  public static Dictionary<long, int> GetSectionIdToSectionNumDictionary(IUserSession session)
  {
    if (session == null)
      return (Dictionary<long, int>) null;
    DataTable dataTable = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SectionNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable == null)
      return (Dictionary<long, int>) null;
    Dictionary<long, int> sectionNumDictionary = new Dictionary<long, int>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (!(row[1] is DBNull) && !(row[0] is DBNull))
        sectionNumDictionary[Convert.ToInt64(row[0])] = Convert.ToInt32(row[1]);
    }
    return sectionNumDictionary;
  }

  /// <summary> Получить 2 хэш-таблицы,
  /// в первой ключом будет выступать номер раздела спецификации, значением - идентификатор раздела
  /// во второй ключом будет выступать идентификатор раздела спецификации, значением - номер раздела спецификации
  /// </summary>
  public static bool GetSectionNumAndSectionIdDictionarys(
    IUserSession session,
    out Dictionary<int, long> sectionNumToSectionIdDictionary,
    out Dictionary<long, int> sectionIdToSectionNumDictionary)
  {
    if (session == null)
    {
      sectionNumToSectionIdDictionary = (Dictionary<int, long>) null;
      sectionIdToSectionNumDictionary = (Dictionary<long, int>) null;
      return false;
    }
    DataTable dataTable = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SectionNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable != null)
    {
      sectionNumToSectionIdDictionary = new Dictionary<int, long>(dataTable.Rows.Count);
      sectionIdToSectionNumDictionary = new Dictionary<long, int>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (!(row[1] is DBNull) && !(row[0] is DBNull))
        {
          sectionNumToSectionIdDictionary[Convert.ToInt32(row[1])] = Convert.ToInt64(row[0]);
          sectionIdToSectionNumDictionary[Convert.ToInt64(row[0])] = Convert.ToInt32(row[1]);
        }
      }
      return true;
    }
    sectionNumToSectionIdDictionary = (Dictionary<int, long>) null;
    sectionIdToSectionNumDictionary = (Dictionary<long, int>) null;
    return false;
  }

  /// <summary>
  /// Результат проверки возможности открытия на просмотр документа
  /// </summary>
  private enum AllowableForOpenAsReadonly
  {
    /// <summary>Можно открывать</summary>
    OK,
    /// <summary>Необходимо обновить перед открытием</summary>
    NeedUpdate,
    /// <summary>Необходимо отменить открытие</summary>
    NeedCancel,
  }

  private sealed class DeserializationBinder : SerializationBinder
  {
    public override System.Type BindToType(string assemblyName, string typeName)
    {
      switch (typeName)
      {
        case "Intermech.Interfaces.AVS.AVSFieldSource":
          return typeof (FieldSource);
        case "Intermech.Interfaces.AVS.SpecRowAttributeInfo":
          return typeof (AvsRowAttributeInfo);
        default:
          return typeName.Contains("List") && typeName.Contains("SpecRowAttributeInfo") ? typeof (List<AvsRowAttributeInfo>) : System.Type.GetType($"{typeName}, {assemblyName}");
      }
    }
  }
}
