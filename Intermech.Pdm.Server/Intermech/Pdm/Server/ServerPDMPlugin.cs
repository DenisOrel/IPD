// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.ServerPDMPlugin
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pdm.Server.Classes;
using Intermech.Pdm.Server.Services;
using Intermech.Protection;
using Intermech.Search.GroupAttributesChanging;
using Intermech.Search.Mbom;
using Intermech.Search.MSOfficeAddins;
using Intermech.Search.Pdm.Analogs;
using Intermech.Search.Pdm.CompositionCopying;
using Intermech.Search.Pdm.Instances;
using Intermech.Search.Pdm.PreciseProducts;
using Intermech.Search.Pdm.SeriesDates;
using Intermech.Search.Pdm.Substitutes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Pdm.Server;

internal class ServerPDMPlugin : LongLifeObject, IPackage
{
  private static Guid _pluginGuid = new Guid("cad005f4-306c-11d8-b4e9-00304f19f545");
  private static int _substAttrID = 0;
  private static int _substGroupAttrID = 0;
  public static int _contextCompositionAttrID = 0;
  private static int _attrQuantity = 0;
  private static int _attrPosition = 0;
  private bool _autoAddRelation;
  private bool _autoSetStep;
  internal IEventLogHelper _eventLogHelper;
  private ServerPDMPlugin.ServerPDMPluginClass _serverPDMPluginClass = new ServerPDMPlugin.ServerPDMPluginClass();
  internal IVersionRulesCacheService _versionRulesCacheService;
  internal IElementStatusesService _elementStatusesService;
  internal IPluginStatusesTable _pluginStatusesTable;
  private ElementStatusesPluginDescription _pluginDescriptionVersAppls = new ElementStatusesPluginDescription(8, "{14BE37A7-84F7-44CB-97AA-15A713C703E0}", "{C96D8F98-D79E-42CB-9A0C-60C6C321C052}", LocalizationHolder.rm.GetString("Pdm.Server_50"), LocalizationHolder.rm.GetString("Pdm.Server_51"));
  private ElementStatusesPluginDescription _pluginDescription = new ElementStatusesPluginDescription(4, "cad005f4-306c-11d8-b4e9-00304f19f545", "cad005f9-306c-11d8-b4e9-00304f19f545", LocalizationHolder.rm.GetString("Pdm.Server_27"), LocalizationHolder.rm.GetString("Pdm.Server_28"));
  private ElementStatusesPluginDescription _pluginDescriptionHiddenCompositions = new ElementStatusesPluginDescription(2, "cad005fe-306c-11d8-b4e9-00304f19f545", "cad005ff-306c-11d8-b4e9-00304f19f545", LocalizationHolder.rm.GetString("Pdm.Server_29"), LocalizationHolder.rm.GetString("Pdm.Server_30"));
  private ElementStatusesPluginDescription _pluginDescriptionContexts = new ElementStatusesPluginDescription(8, "cad005fc-306c-11d8-b4e9-00304f19f545", "cad005f9-306c-11d8-b4e9-00304f19f545", LocalizationHolder.rm.GetString("Pdm.Server_31"), LocalizationHolder.rm.GetString("Pdm.Server_32"));
  private ElementStatusesPluginDescription _pluginArticleCompositions = new ElementStatusesPluginDescription(4, "{793BEF65-E7BC-40B5-A0FA-003472E7F548}", "{7F92D8D5-8B09-4893-8A5F-FE1DAB481A23}", LocalizationHolder.rm.GetString("Pdm.Server_45"), LocalizationHolder.rm.GetString("Pdm.Server_46"));
  internal static Dictionary<long, List<long>> lockAutoCreateRelation = new Dictionary<long, List<long>>();
  internal static bool IsOrderPointMode = false;
  internal static string OrderExistsAttrGuid = "aed97063-2fee-47ef-a4a5-fbec2ea4e8d9";
  internal static string QualityControlAttrGuid = "efd485ae-37c9-4f6a-bac6-a33473c25561";
  internal static int MaterialAttrID = 0;
  internal static int QualityControlAttrID = 0;
  internal static int OrderExistsAttrID = 0;
  public static VisSchemeSynchroCache VisCache = new VisSchemeSynchroCache();
  private ConcurrentBag<int> _InstanceTypes;
  private AnalogsServerModule _analogsServerModule = new AnalogsServerModule();
  private PreciseProductsServerModule _preciseProductsServerModule = new PreciseProductsServerModule();
  private MbomServerModule _mbomServerModule = new MbomServerModule();
  private MSOfficeAddinsServerModule _msOfficeAddinsServerModule = new MSOfficeAddinsServerModule();
  private CompositionCopyingServerModule _compositionCopyingServerModule;

  internal static Guid PluginGuid
  {
    [DebuggerStepThrough] get => ServerPDMPlugin._pluginGuid;
  }

  public void Load(IServiceProvider serviceProvider)
  {
    int appId = 350;
    byte[][] numArray1 = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 15,
        (byte) 210,
        (byte) 87,
        (byte) 151,
        (byte) 156,
        (byte) 46,
        (byte) 169,
        (byte) 50,
        (byte) 70,
        (byte) 66,
        (byte) 169,
        (byte) 14,
        (byte) 157,
        (byte) 101,
        (byte) 80 /*0x50*/,
        (byte) 167
      },
      new byte[16 /*0x10*/]
      {
        (byte) 56,
        (byte) 144 /*0x90*/,
        (byte) 253,
        (byte) 57,
        (byte) 161,
        (byte) 207,
        (byte) 234,
        (byte) 104,
        (byte) 44,
        (byte) 252,
        (byte) 229,
        (byte) 99,
        (byte) 38,
        (byte) 69,
        (byte) 250,
        (byte) 224 /*0xE0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 141,
        (byte) 92,
        (byte) 56,
        (byte) 158,
        (byte) 112 /*0x70*/,
        (byte) 167,
        (byte) 33,
        (byte) 179,
        (byte) 224 /*0xE0*/,
        (byte) 115,
        (byte) 163,
        (byte) 9,
        (byte) 208 /*0xD0*/,
        (byte) 127 /*0x7F*/,
        (byte) 173,
        (byte) 214
      },
      new byte[16 /*0x10*/]
      {
        (byte) 95,
        (byte) 230,
        (byte) 46,
        (byte) 173,
        (byte) 193,
        (byte) 38,
        (byte) 191,
        (byte) 98,
        (byte) 215,
        (byte) 142,
        (byte) 180,
        (byte) 67,
        (byte) 182,
        (byte) 49,
        (byte) 159,
        (byte) 221
      },
      new byte[16 /*0x10*/]
      {
        (byte) 250,
        (byte) 54,
        (byte) 216,
        (byte) 166,
        (byte) 71,
        (byte) 74,
        (byte) 254,
        (byte) 209,
        (byte) 155,
        (byte) 201,
        (byte) 250,
        (byte) 206,
        (byte) 25,
        (byte) 55,
        (byte) 190,
        (byte) 209
      },
      new byte[16 /*0x10*/]
      {
        (byte) 47,
        (byte) 195,
        (byte) 170,
        (byte) 229,
        (byte) 152,
        (byte) 251,
        (byte) 204,
        (byte) 252,
        (byte) 250,
        (byte) 156,
        (byte) 49,
        (byte) 59,
        (byte) 38,
        (byte) 174,
        (byte) 173,
        (byte) 181
      },
      new byte[16 /*0x10*/]
      {
        (byte) 29,
        (byte) 154,
        (byte) 116,
        (byte) 5,
        (byte) 150,
        (byte) 50,
        (byte) 163,
        (byte) 104,
        (byte) 177,
        (byte) 164,
        (byte) 183,
        (byte) 41,
        (byte) 138,
        (byte) 160 /*0xA0*/,
        (byte) 98,
        (byte) 198
      },
      new byte[16 /*0x10*/]
      {
        (byte) 211,
        (byte) 164,
        (byte) 44,
        (byte) 1,
        (byte) 10,
        (byte) 148,
        (byte) 117,
        (byte) 214,
        (byte) 17,
        (byte) 107,
        (byte) 52,
        (byte) 176 /*0xB0*/,
        (byte) 246,
        (byte) 206,
        (byte) 83,
        (byte) 157
      },
      new byte[16 /*0x10*/]
      {
        (byte) 149,
        (byte) 210,
        (byte) 20,
        (byte) 102,
        (byte) 141,
        (byte) 169,
        (byte) 95,
        (byte) 66,
        (byte) 94,
        (byte) 103,
        (byte) 2,
        (byte) 166,
        (byte) 172,
        (byte) 83,
        (byte) 153,
        (byte) 169
      },
      new byte[16 /*0x10*/]
      {
        (byte) 161,
        (byte) 232,
        (byte) 19,
        (byte) 115,
        (byte) 36,
        (byte) 58,
        (byte) 15,
        (byte) 91,
        (byte) 136,
        (byte) 141,
        (byte) 118,
        (byte) 165,
        (byte) 0,
        (byte) 26,
        (byte) 21,
        (byte) 174
      },
      new byte[16 /*0x10*/]
      {
        (byte) 180,
        (byte) 210,
        (byte) 235,
        (byte) 53,
        (byte) 11,
        (byte) 167,
        (byte) 64 /*0x40*/,
        (byte) 200,
        (byte) 147,
        (byte) 111,
        (byte) 245,
        (byte) 163,
        (byte) 147,
        (byte) 212,
        (byte) 70,
        (byte) 251
      },
      new byte[16 /*0x10*/]
      {
        (byte) 0,
        (byte) 201,
        (byte) 171,
        (byte) 189,
        (byte) 33,
        (byte) 35,
        (byte) 231,
        (byte) 118,
        (byte) 67,
        (byte) 189,
        (byte) 245,
        (byte) 205,
        (byte) 112 /*0x70*/,
        (byte) 122,
        (byte) 30,
        (byte) 249
      },
      new byte[16 /*0x10*/]
      {
        (byte) 102,
        (byte) 52,
        (byte) 56,
        (byte) 71,
        (byte) 35,
        (byte) 87,
        (byte) 67,
        (byte) 115,
        (byte) 59,
        (byte) 49,
        (byte) 167,
        (byte) 46,
        (byte) 47,
        (byte) 252,
        (byte) 188,
        (byte) 93
      },
      new byte[16 /*0x10*/]
      {
        (byte) 201,
        (byte) 31 /*0x1F*/,
        (byte) 252,
        (byte) 112 /*0x70*/,
        (byte) 31 /*0x1F*/,
        (byte) 51,
        (byte) 131,
        (byte) 115,
        (byte) 170,
        (byte) 80 /*0x50*/,
        (byte) 42,
        (byte) 31 /*0x1F*/,
        (byte) 163,
        (byte) 28,
        (byte) 86,
        (byte) 157
      },
      new byte[16 /*0x10*/]
      {
        (byte) 10,
        (byte) 14,
        (byte) 116,
        (byte) 183,
        (byte) 180,
        (byte) 45,
        (byte) 187,
        (byte) 237,
        (byte) 146,
        (byte) 76,
        (byte) 211,
        (byte) 30,
        (byte) 180,
        (byte) 188,
        (byte) 175,
        (byte) 137
      },
      new byte[16 /*0x10*/]
      {
        (byte) 70,
        (byte) 178,
        (byte) 148,
        (byte) 111,
        byte.MaxValue,
        (byte) 79,
        (byte) 148,
        (byte) 132,
        (byte) 196,
        (byte) 252,
        (byte) 215,
        (byte) 49,
        (byte) 179,
        (byte) 86,
        (byte) 75,
        (byte) 167
      },
      new byte[16 /*0x10*/]
      {
        (byte) 32 /*0x20*/,
        (byte) 7,
        (byte) 64 /*0x40*/,
        (byte) 120,
        (byte) 27,
        (byte) 82,
        (byte) 134,
        (byte) 93,
        (byte) 142,
        (byte) 119,
        (byte) 250,
        (byte) 218,
        (byte) 0,
        (byte) 17,
        (byte) 152,
        (byte) 150
      },
      new byte[16 /*0x10*/]
      {
        (byte) 151,
        (byte) 111,
        (byte) 233,
        (byte) 81,
        (byte) 61,
        (byte) 182,
        (byte) 33,
        (byte) 208 /*0xD0*/,
        (byte) 235,
        (byte) 10,
        (byte) 66,
        (byte) 135,
        (byte) 125,
        (byte) 28,
        (byte) 123,
        (byte) 91
      },
      new byte[16 /*0x10*/]
      {
        (byte) 179,
        (byte) 151,
        (byte) 153,
        (byte) 133,
        (byte) 96 /*0x60*/,
        (byte) 143,
        (byte) 70,
        (byte) 57,
        (byte) 120,
        (byte) 74,
        (byte) 180,
        (byte) 214,
        (byte) 131,
        (byte) 37,
        (byte) 191,
        (byte) 127 /*0x7F*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 117,
        (byte) 179,
        (byte) 66,
        (byte) 221,
        (byte) 75,
        (byte) 16 /*0x10*/,
        (byte) 49,
        (byte) 147,
        (byte) 167,
        (byte) 134,
        (byte) 107,
        (byte) 207,
        (byte) 250,
        (byte) 55,
        (byte) 136,
        (byte) 83
      },
      new byte[16 /*0x10*/]
      {
        (byte) 129,
        (byte) 30,
        (byte) 14,
        (byte) 40,
        (byte) 188,
        (byte) 68,
        (byte) 66,
        (byte) 183,
        (byte) 136,
        (byte) 23,
        (byte) 41,
        (byte) 61,
        (byte) 51,
        (byte) 225,
        (byte) 237,
        (byte) 129
      },
      new byte[16 /*0x10*/]
      {
        (byte) 56,
        (byte) 143,
        (byte) 221,
        (byte) 190,
        (byte) 176 /*0xB0*/,
        (byte) 94,
        (byte) 224 /*0xE0*/,
        (byte) 100,
        (byte) 244,
        (byte) 204,
        (byte) 234,
        (byte) 200,
        (byte) 72,
        (byte) 139,
        (byte) 93,
        (byte) 231
      },
      new byte[16 /*0x10*/]
      {
        (byte) 179,
        (byte) 60,
        (byte) 106,
        (byte) 29,
        (byte) 34,
        (byte) 42,
        (byte) 40,
        (byte) 145,
        (byte) 192 /*0xC0*/,
        (byte) 6,
        byte.MaxValue,
        (byte) 208 /*0xD0*/,
        (byte) 157,
        (byte) 175,
        (byte) 217,
        (byte) 105
      },
      new byte[16 /*0x10*/]
      {
        (byte) 208 /*0xD0*/,
        (byte) 44,
        (byte) 130,
        (byte) 218,
        (byte) 37,
        (byte) 198,
        (byte) 130,
        (byte) 141,
        (byte) 68,
        (byte) 212,
        (byte) 230,
        (byte) 239,
        (byte) 205,
        (byte) 135,
        (byte) 238,
        (byte) 98
      },
      new byte[16 /*0x10*/]
      {
        (byte) 184,
        (byte) 122,
        (byte) 88,
        (byte) 226,
        (byte) 2,
        (byte) 111,
        (byte) 218,
        (byte) 111,
        (byte) 85,
        (byte) 225,
        (byte) 252,
        (byte) 73,
        (byte) 101,
        (byte) 9,
        (byte) 6,
        (byte) 151
      },
      new byte[16 /*0x10*/]
      {
        (byte) 196,
        (byte) 20,
        (byte) 41,
        (byte) 51,
        (byte) 130,
        (byte) 8,
        (byte) 56,
        (byte) 234,
        (byte) 225,
        (byte) 84,
        (byte) 164,
        (byte) 91,
        (byte) 12,
        (byte) 32 /*0x20*/,
        (byte) 44,
        (byte) 219
      },
      new byte[16 /*0x10*/]
      {
        (byte) 92,
        (byte) 51,
        (byte) 35,
        (byte) 71,
        (byte) 186,
        (byte) 161,
        (byte) 92,
        (byte) 87,
        (byte) 245,
        (byte) 125,
        (byte) 103,
        (byte) 181,
        (byte) 68,
        (byte) 113,
        (byte) 25,
        (byte) 217
      },
      new byte[16 /*0x10*/]
      {
        (byte) 106,
        (byte) 148,
        (byte) 83,
        (byte) 203,
        (byte) 109,
        (byte) 214,
        (byte) 75,
        (byte) 80 /*0x50*/,
        (byte) 4,
        (byte) 139,
        (byte) 75,
        (byte) 25,
        (byte) 166,
        (byte) 91,
        (byte) 13,
        (byte) 226
      },
      new byte[16 /*0x10*/]
      {
        (byte) 61,
        (byte) 191,
        (byte) 132,
        (byte) 184,
        (byte) 136,
        (byte) 11,
        (byte) 126,
        (byte) 68,
        (byte) 97,
        (byte) 251,
        (byte) 199,
        (byte) 64 /*0x40*/,
        (byte) 45,
        (byte) 223,
        (byte) 43,
        (byte) 183
      },
      new byte[16 /*0x10*/]
      {
        (byte) 18,
        (byte) 254,
        (byte) 39,
        (byte) 245,
        (byte) 207,
        (byte) 108,
        (byte) 200,
        (byte) 182,
        (byte) 137,
        (byte) 220,
        (byte) 145,
        (byte) 40,
        (byte) 206,
        (byte) 9,
        (byte) 123,
        (byte) 79
      },
      new byte[16 /*0x10*/]
      {
        byte.MaxValue,
        (byte) 104,
        (byte) 116,
        (byte) 106,
        (byte) 140,
        (byte) 146,
        (byte) 174,
        (byte) 22,
        (byte) 30,
        (byte) 0,
        (byte) 236,
        (byte) 132,
        (byte) 8,
        (byte) 36,
        (byte) 42,
        (byte) 228
      },
      new byte[16 /*0x10*/]
      {
        (byte) 60,
        (byte) 107,
        (byte) 49,
        (byte) 7,
        (byte) 93,
        (byte) 93,
        (byte) 254,
        (byte) 236,
        (byte) 221,
        (byte) 105,
        (byte) 125,
        (byte) 160 /*0xA0*/,
        (byte) 20,
        (byte) 171,
        (byte) 202,
        (byte) 150
      }
    };
    IProtectionKey service1 = serviceProvider.GetService(typeof (IProtectionKey)) as IProtectionKey;
    ((ILicenser) ServerServices.GetService(typeof (ILicenser))).AllocateLicense(appId);
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
    IPairedObjectsCreatorService service2 = ServiceUtils.GetService<IPairedObjectsCreatorService>((object) ServerServices.ServiceContainer, true);
    IgnoredSessionsBag disablePairedArticlesBySpecificationSwitch = new IgnoredSessionsBag();
    SpecificationVersionCreatorIDCache specificationVersionCreatorIdCache = new SpecificationVersionCreatorIDCache(MetadataResolvers.Factory);
    service2.RegisterCreator((Func<PairedObjectsCreator>) (() => (PairedObjectsCreator) new SpecificationVersionCreator(disablePairedArticlesBySpecificationSwitch, specificationVersionCreatorIdCache)));
    service2.RegisterCreator((Func<PairedObjectsCreator>) (() => (PairedObjectsCreator) new ArticleWithSpecificationVersionCreator(disablePairedArticlesBySpecificationSwitch, specificationVersionCreatorIdCache)));
    this._pluginStatusesTable = serviceProvider.GetService(typeof (IPluginStatusesTable)) as IPluginStatusesTable;
    this._eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    if (this._eventLogHelper != null)
    {
      this._eventLogHelper.BeforeDeleteRelationEvent += new Intermech.Interfaces.Server.DeleteRelationHandler(this.DeleteRelationHandler);
      this._eventLogHelper.AfterCreateObjectEvent += new Intermech.Interfaces.Server.AfterCreateObjectHandler(this.AfterCreateObjectHandler);
      this._eventLogHelper.BeforeRecordsSelectEvent += new Intermech.Interfaces.Server.BeforeRecordsSelectHandler(this.BeforeRecordsSelectHandler);
      this._eventLogHelper.GetRecordsListEvent += new GetRecordsListHandler(this.GetRecordsListEventHandler);
      this._eventLogHelper.GetRecordsListEvent += new GetRecordsListHandler(this.SortDataTableByPosition);
      this._eventLogHelper.AfterNextLCStepEvent += new NextLCStepHandler(this.AfterNextLCStepEvent);
    }
    ICustomServices service3 = serviceProvider.GetService(typeof (ICustomServices)) as ICustomServices;
    CompositionService serviceInstance1 = new CompositionService();
    PDMSystemService serviceInstance2 = new PDMSystemService();
    SubstitutesService serviceInstance3 = new SubstitutesService();
    GroupInstanceService serviceInstance4 = new GroupInstanceService(disablePairedArticlesBySpecificationSwitch);
    RelationsComparerService serviceInstance5 = new RelationsComparerService();
    serviceInstance5.RegisterRelationsComparer((IRelationsComparer) new RelationsComparer());
    if (service3.GetService(typeof (IObjectsDeleteAnalyzerService)) is IObjectsDeleteAnalyzerService service4)
      service4.RegisterAnalyzer((IObjectsDeleteAnalyzer) new PDMObjectsDeleteAnalyzer());
    if (service3.GetService(typeof (IObjectsChangingAnalyzerService)) is IObjectsChangingAnalyzerService service5)
      service5.RegisterAnalyzer((IObjectsChangingAnalyzer) new PDMObjectsCancelChangesAnalyzer());
    service3.AddService(typeof (ICompositionService), (object) serviceInstance1);
    service3.AddService(typeof (ISearchScheme), (object) serviceInstance1);
    service3.AddService(typeof (ISubstitutesService), (object) serviceInstance3);
    ServerServices.AddService(typeof (ISubstitutesService), (object) serviceInstance3);
    service3.AddService(typeof (IFileNameGenerator), (object) serviceInstance2);
    service3.AddService(typeof (IRelationsComparerService), (object) serviceInstance5);
    service3.AddService(typeof (IGroupInstanceService), (object) serviceInstance4);
    IArticleService serviceInstance6 = (IArticleService) new ArticleSrvService();
    service3.AddService(typeof (IArticleService), (object) serviceInstance6);
    ServerServices.AddService(typeof (IArticleService), (object) serviceInstance6);
    VersionApplicabilitiesService serviceInstance7 = new VersionApplicabilitiesService();
    service3.AddService(typeof (IVersionApplicabilitiesService), (object) serviceInstance7);
    ServerServices.AddService(typeof (IVersionApplicabilitiesService), (object) serviceInstance7);
    service3.AddService(typeof (ISearchSchemeSettingsService), (object) new SearchSchemeSettingsService());
    IRelVisObserverService serviceInstance8 = (IRelVisObserverService) new RelVisObserverService();
    ServerServices.AddService(typeof (IRelVisObserverService), (object) serviceInstance8);
    service3.AddService(typeof (IRelVisObserverService), (object) serviceInstance8);
    IVisualizerService serviceInstance9 = (IVisualizerService) VisServer.Init(serviceProvider);
    ServerServices.AddService(typeof (IVisualizerService), (object) serviceInstance9);
    service3.AddService(typeof (IVisualizerService), (object) serviceInstance9);
    service3.AddService(typeof (IInstancesServerService), (object) new InstancesServerService());
    this.LoadPluginResources(serviceProvider);
    ServerServices.AddService(typeof (IPdmServerPlugin), (object) this._serverPDMPluginClass);
    service3.AddService(typeof (IPdmServerPlugin), (object) this._serverPDMPluginClass);
    this._elementStatusesService = serviceProvider.GetService(typeof (IElementStatusesService)) as IElementStatusesService;
    if (this._elementStatusesService != null)
    {
      this._elementStatusesService.RegisterServerPlugin(this._pluginDescriptionVersAppls);
      this._elementStatusesService.RegisterServerPlugin(this._pluginDescription);
      this._elementStatusesService.RegisterServerPlugin(this._pluginDescriptionHiddenCompositions);
      this._elementStatusesService.RegisterServerPlugin(this._pluginDescriptionContexts);
      this._elementStatusesService.RegisterServerPlugin(this._pluginArticleCompositions);
    }
    ICreatorContainer service6 = ServerServices.GetService(typeof (IDBObjectService)) as ICreatorContainer;
    DBSearchSchemeObjectCreator creatorInstance1 = new DBSearchSchemeObjectCreator();
    service6.AddCreator((object) new Guid("cad0012a-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance1);
    service6.AddCreator((object) new Guid("cad0012b-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance1);
    VisDataObjectCreator creatorInstance2 = new VisDataObjectCreator();
    service6.AddCreator((object) new Guid("cadd9aa6-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance2);
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("PDMServer.Load");
    try
    {
      service3.AddService(typeof (IComponentSelectionService), (object) new ComponentSelectionService(sessionTemporaryClone, serviceProvider));
      SubstitutesSettings serviceInstance10 = new SubstitutesSettings(sessionTemporaryClone);
      ServerServices.AddService(typeof (ISubstitutesSettings), (object) serviceInstance10);
      service3.AddService(typeof (ISubstitutesSettings), (object) serviceInstance10);
      SubstitutesRemarksService serviceInstance11 = new SubstitutesRemarksService();
      ServerServices.AddService(typeof (ISubstitutesRemarksService), (object) serviceInstance11);
      service3.AddService(typeof (ISubstitutesRemarksService), (object) serviceInstance11);
      ServerPDMPlugin._substAttrID = sessionTemporaryClone.IdentHelper.SubstituteInGroup;
      ServerPDMPlugin._substGroupAttrID = sessionTemporaryClone.IdentHelper.SubstitutesGroupNoID;
      ServerPDMPlugin._contextCompositionAttrID = MetaDataHelper.GetAttributeID((object) "cad00651-306c-11d8-b4e9-00304f19f545");
      ServerPDMPlugin._attrQuantity = MetaDataHelper.GetAttributeID((object) "cad00267-306c-11d8-b4e9-00304f19f545");
      ServerPDMPlugin._attrPosition = MetaDataHelper.GetAttributeID((object) "cad00270-306c-11d8-b4e9-00304f19f545");
      IDBObjectType objectType1 = sessionTemporaryClone.GetObjectType(new Guid("cad00583-306c-11d8-b4e9-00304f19f545"), false);
      if (objectType1 != null)
      {
        DBExemplarCreator creatorInstance3 = new DBExemplarCreator();
        foreach (Guid childTypeGuid in ObjectTypesCacheHelper.GetChildTypeGuids(sessionTemporaryClone, objectType1.ObjectType))
          service6.AddCreator((object) childTypeGuid, (object) creatorInstance3);
      }
      ICreatorContainer service7 = ServerServices.GetService(typeof (IDBObjectCollectionService)) as ICreatorContainer;
      IDBObjectType objectType2 = sessionTemporaryClone.GetObjectType(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"), false);
      if (objectType2 != null && service7 != null)
      {
        DBArticleCollectionCreator creatorInstance4 = new DBArticleCollectionCreator();
        DBArticleCreator creatorInstance5 = new DBArticleCreator();
        foreach (Guid childTypeGuid in ObjectTypesCacheHelper.GetChildTypeGuids(sessionTemporaryClone, objectType2.ObjectType))
        {
          service7.AddCreator((object) childTypeGuid, (object) creatorInstance4);
          service6.AddCreator((object) childTypeGuid, (object) creatorInstance5);
        }
      }
      ArticleAttributesSyncService serviceInstance12 = new ArticleAttributesSyncService(sessionTemporaryClone);
      ServerServices.AddService(typeof (IArticleAttributesSyncService), (object) serviceInstance12);
      service3.AddService(typeof (IArticleAttributesSyncService), (object) serviceInstance12);
      IDBObjectType objectType3 = sessionTemporaryClone.GetObjectType(PDMPluginGuids.orderPointGuid, false);
      IDBObjectType objectType4 = sessionTemporaryClone.GetObjectType(new Guid("cad00132-306c-11d8-b4e9-00304f19f545"), false);
      IDBRelationType relationType = sessionTemporaryClone.GetRelationType(PDMPluginGuids.orderPointCompositionRelationGuid, false);
      if (objectType3 != null)
      {
        if (objectType4 != null)
        {
          if (relationType != null)
          {
            PDMPluginIDs.orderPointTypeID = objectType3.ObjectType;
            PDMPluginIDs.orderPointCompositionRelationTypeID = relationType.RelationType;
            PDMPluginIDs.assemblyUnitTypeID = objectType4.ObjectType;
            service3.AddService(typeof (IOrderPointService), (object) new OrderPointService());
            ServerPDMPlugin.IsOrderPointMode = true;
            ServerPDMPlugin.QualityControlAttrID = MetaDataHelper.GetAttributeID((object) ServerPDMPlugin.QualityControlAttrGuid);
            ServerPDMPlugin.OrderExistsAttrID = MetaDataHelper.GetAttributeID((object) ServerPDMPlugin.OrderExistsAttrGuid);
            ServerPDMPlugin.MaterialAttrID = MetaDataHelper.GetAttributeID((object) "cad0038c-306c-11d8-b4e9-00304f19f545");
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("PDMServer.Load");
    }
    if (ServerServices.GetService(typeof (ILinkedObjectsService)) is ILinkedObjectsService service8)
      service8.RegisterHandler((ILinkedObjectsHandler) new PDMLinkedObjectsHandler());
    service3.AddService(typeof (ISubstitutesServerService), (object) new SubstitutesServerService());
    service3.AddService(typeof (ISeriesDatesServerService), (object) new SeriesDatesServerService());
    ((IServerSynchronizersManager) ServerServices.GetService(typeof (IServerSynchronizersManager)))?.RegisterSynchronizer((IServerSynchronizer) ServerPDMPlugin.VisCache);
    this._analogsServerModule.Load();
    this._preciseProductsServerModule.Load();
    this._mbomServerModule.Load();
    this._msOfficeAddinsServerModule.Load();
    this._compositionCopyingServerModule = new CompositionCopyingServerModule(service3, (IInstancesServerService) service3.GetService(typeof (IInstancesServerService)), (IGroupAttributesChangingServerService) service3.GetService(typeof (IGroupAttributesChangingServerService)));
    this._compositionCopyingServerModule.Load();
    if (ServerServices.GetService(typeof (IDBRelationService)) is ICreatorContainer service9)
      service9.AddCreator((object) new Guid("cad00023-306c-11d8-b4e9-00304f19f545"), (object) new DBArticleRelationCreator());
    DBAttribute.RegisterAttribute4DisableUpdateContentDate(specificationVersionCreatorIdCache.InstanceGroupId.Id);
    if (!(ServerServices.GetService(typeof (IPluginManager)) is IPluginManager service10))
      return;
    FileInfo fileInfo = new FileInfo(Path.Combine(new FileInfo(typeof (ServerPDMPlugin).Assembly.Location).Directory.FullName, "Intermech.PdmConfigurator.Server.dll"));
    if (fileInfo.Exists)
      service10.Load(fileInfo.FullName, false);
    service10.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    if (!(ServerServices.GetService(typeof (IForumExtend)) is IForumExtend service))
      return;
    service.Extend += new ForumExtendEventHandler(this.forumExtender_Extend);
  }

  private void forumExtender_Extend(ForumEventArgs eventArgs)
  {
    if (!(ServerServices.GetService(typeof (IArticleService)) is IArticleService service))
      return;
    eventArgs.ResultIDs.AddRange((IEnumerable<long>) service.GetListInstances(eventArgs.ObjectID, (object) eventArgs.SessionGuid));
  }

  private bool IsInstanceType(int objectTypeID, IUserSession session)
  {
    if (this._InstanceTypes == null)
    {
      List<int> exemplarsObjectTypes = this.GetExemplarsObjectTypes(session);
      this._InstanceTypes = new ConcurrentBag<int>();
      for (int index = 0; index < exemplarsObjectTypes.Count; ++index)
        this._InstanceTypes.Add(exemplarsObjectTypes[index]);
    }
    return this._InstanceTypes.Contains<int>(objectTypeID);
  }

  private void AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (this._autoSetStep)
      return;
    IDbManager dataManager = (session as UserSession).DataManager;
    this._autoSetStep = true;
    try
    {
      IDBRelationType relationType = session.GetRelationType(PDMHelper.relationTypeInstances, false);
      if (!this.IsInstanceType(sender.ObjectType, session))
        return;
      DataTable dataTable = session.GetRelationCollection(relationType.RelationType).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) this._InstanceTypes.ToArray(), LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -2 }), sender.ObjectID, true);
      if (dataTable.Rows == null || dataTable.Rows.Count <= 0)
        return;
      dataManager.BeginTransaction();
      try
      {
        List<long> longList = new List<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          longList.Add(Convert.ToInt64(row[0]));
        session.GetLifecycleStepCollection(0).SetObjectsLCStep(longList.ToArray(), nextstep.LCStep);
        dataManager.Commit();
      }
      catch
      {
        dataManager.Rollback();
        throw;
      }
    }
    finally
    {
      this._autoSetStep = false;
    }
  }

  private List<int> GetExemplarsObjectTypes(IUserSession session)
  {
    List<int> exemplarsObjectTypes = new List<int>();
    IDBObjectType objectType1 = session.GetObjectType(new Guid("cad00583-306c-11d8-b4e9-00304f19f545"), false);
    IDBAttributeType attributeType = session.GetAttributeType(new Guid("cad0058f-306c-11d8-b4e9-00304f19f545"), false);
    if (objectType1 == null || attributeType == null)
      return (List<int>) null;
    List<Guid> childTypeGuids = ObjectTypesCacheHelper.GetChildTypeGuids(session, objectType1.ObjectType);
    List<int> intList = new List<int>();
    foreach (Guid anObjectTypeGuid in childTypeGuids)
    {
      IDBObjectType objectType2 = session.GetObjectType(anObjectTypeGuid, false);
      if (objectType2 != null && objectType2.Versionable != ObjectVersionModes.Abstract && objectType2.Attributes.GetAttributeByID(attributeType.AttributeID, false) == null)
        exemplarsObjectTypes.Add(objectType2.ObjectType);
    }
    return exemplarsObjectTypes;
  }

  public void Unload()
  {
    this._analogsServerModule.Unload();
    this._preciseProductsServerModule.Unload();
    this._mbomServerModule.Unload();
    this._msOfficeAddinsServerModule.Unload();
    if (this._compositionCopyingServerModule != null)
      this._compositionCopyingServerModule.Unload();
    ((ILicenser) ServerServices.GetService(typeof (ILicenser))).ReleaseLicense(32 /*0x20*/);
  }

  public string Name => ServerPDMPluginConsts.PDMPluginName;

  internal static byte[] LoadResource(string ResourceName)
  {
    Stream stream = (Stream) null;
    try
    {
      stream = typeof (ServerPDMPlugin).Assembly.GetManifestResourceStream(ResourceName);
      if (stream == null)
        return new byte[0];
      byte[] buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      return buffer;
    }
    finally
    {
      stream?.Close();
    }
  }

  private void LoadPluginResources(IServiceProvider serviceProvider)
  {
    string str = "Intermech.Pdm.Server.Resources.";
    this._pluginStatusesTable.AddStatus("{14BE37A7-84F7-44CB-97AA-15A713C703E0}", 15, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVersionBySeries), ServerPDMPlugin.LoadResource(str + "rsVersionBySeries.ico"));
    this._pluginStatusesTable.AddStatus("{14BE37A7-84F7-44CB-97AA-15A713C703E0}", 15, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVersionByDate), ServerPDMPlugin.LoadResource(str + "rsVersionByDate.ico"));
    this._pluginStatusesTable.AddStatus("{14BE37A7-84F7-44CB-97AA-15A713C703E0}", 15, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVarianceSeriesDate), ServerPDMPlugin.LoadResource(str + "rsVarianceSeriesDate.ico"));
    this._pluginStatusesTable.AddStatus("cad005f4-306c-11d8-b4e9-00304f19f545", 0, string.Empty, (byte[]) null);
    this._pluginStatusesTable.AddStatus("cad005f4-306c-11d8-b4e9-00304f19f545", 1, EnumDescConverter.GetEnumDescription((Enum) RelationAsSubstitutes.rsActualSubstitute), ServerPDMPlugin.LoadResource(str + "main.png"));
    this._pluginStatusesTable.AddStatus("cad005f4-306c-11d8-b4e9-00304f19f545", 2, EnumDescConverter.GetEnumDescription((Enum) RelationAsSubstitutes.rsSubstitute), ServerPDMPlugin.LoadResource(str + "alt.png"));
    this._pluginStatusesTable.AddStatus("cad005fe-306c-11d8-b4e9-00304f19f545", 0, string.Empty, (byte[]) null);
    this._pluginStatusesTable.AddStatus("cad005fe-306c-11d8-b4e9-00304f19f545", 1, LocalizationHolder.rm.GetString("Pdm.Server_34"), ServerPDMPlugin.LoadResource(str + "rsHiddenChilds.ico"));
    this._pluginStatusesTable.AddStatus("cad005fc-306c-11d8-b4e9-00304f19f545", 0, LocalizationHolder.rm.GetString("Pdm.Server_35"), (byte[]) null);
    this._pluginStatusesTable.AddStatus("cad005fc-306c-11d8-b4e9-00304f19f545", 1, LocalizationHolder.rm.GetString("Pdm.Server_36"), ServerPDMPlugin.LoadResource(str + "rsDesignContext.ico"));
    this._pluginStatusesTable.AddStatus("cad005fc-306c-11d8-b4e9-00304f19f545", 2, LocalizationHolder.rm.GetString("Pdm.Server_37"), ServerPDMPlugin.LoadResource(str + "rsTechContext.ico"));
    this._pluginStatusesTable.AddStatus("cad005fc-306c-11d8-b4e9-00304f19f545", 3, LocalizationHolder.rm.GetString("Pdm.Server_40"), ServerPDMPlugin.LoadResource(str + "rsTechnologicalContext.ico"));
    this._pluginStatusesTable.AddStatus("{793BEF65-E7BC-40B5-A0FA-003472E7F548}", 1, LocalizationHolder.rm.GetString("Pdm.Server_42"), ServerPDMPlugin.LoadResource(str + "rsArticleCommonPart.ico"));
    this._pluginStatusesTable.AddStatus("{793BEF65-E7BC-40B5-A0FA-003472E7F548}", 2, LocalizationHolder.rm.GetString("Pdm.Server_43"), (byte[]) null);
    this._pluginStatusesTable.AddStatus("{793BEF65-E7BC-40B5-A0FA-003472E7F548}", 0, LocalizationHolder.rm.GetString("Pdm.Server_44"), (byte[]) null);
  }

  internal void DeleteRelationHandler(IDBRelation sender, long deleteMode, IUserSession session)
  {
    if (sender == null || deleteMode == (long) Consts.PurgeMode || deleteMode == 512L /*0x0200*/)
      return;
    IDBAttribute byId1 = sender.Attributes.FindByID(session.IdentHelper.SubstitutesGroupNoID);
    IDBAttribute byId2 = sender.Attributes.FindByID(session.IdentHelper.SubstituteInGroup);
    if (byId1 == null || byId2 == null || byId1.Value == null || byId2.Value == null || byId1.Value == DBNull.Value || byId2.Value == DBNull.Value)
      return;
    long result1 = 0;
    if (!long.TryParse(byId1.Value.ToString(), out result1))
      return;
    long result2 = 0;
    if (long.TryParse(byId2.Value.ToString(), out result2) && (result1 != 0L || result2 != 0L))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_16993.ssp_pdm_server_16994()), (object) sender.RelationID));
  }

  private void AfterCreateObjectHandler(
    IDBObject newObject,
    IDBObject prototype,
    IUserSession session)
  {
    if (MetaDataHelper.GetAttribute4ObjectType(newObject.ObjectType, MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545")) == null)
      return;
    new SeriesDatesApplicabilityCollection().SaveToObject((IDBAttributable) newObject);
  }

  internal void BeforeRecordsSelectHandler(object sender, BeforeRecordsSelectEventArgs args)
  {
    HiddenCompositionFiltrationMode compositionFiltrationMode = HiddenCompositionFiltrationMode.None;
    List<long> longList1 = (List<long>) null;
    if (args == null || args.Session == null)
      return;
    if (this._versionRulesCacheService == null)
      this._versionRulesCacheService = args.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    if (ServerPDMPlugin._substAttrID == 0 || ServerPDMPlugin._substGroupAttrID == 0 || ServerPDMPlugin._contextCompositionAttrID == 0)
    {
      ServerPDMPlugin._substAttrID = args.Session.IdentHelper.SubstituteInGroup;
      ServerPDMPlugin._substGroupAttrID = args.Session.IdentHelper.SubstitutesGroupNoID;
      ServerPDMPlugin._contextCompositionAttrID = args.Session.IdentHelper.GetAttributeID("cad00651-306c-11d8-b4e9-00304f19f545");
    }
    if (!(sender is DBRelationCollection relationCollection) || relationCollection.RelationTypeID == -1)
      return;
    IDBRelationType relationType = args.Session.GetRelationType(relationCollection.RelationTypeID);
    if (relationType == null)
      return;
    IDBAttributeType attributeById1 = (IDBAttributeType) relationType.Attributes.GetAttributeByID(args.Session.IdentHelper.SubstitutesGroupNoID, false);
    IDBAttributeType attributeById2 = (IDBAttributeType) relationType.Attributes.GetAttributeByID(args.Session.IdentHelper.SubstituteInGroup, false);
    bool flag1 = attributeById1 != null && attributeById2 != null;
    bool flag2 = relationType.Attributes.GetAttributeByID(MetaDataHelper.GetAttributeID((object) "cad00651-306c-11d8-b4e9-00304f19f545"), false) != null;
    if (args.OldParameters.Tags == null)
      return;
    args.OldParameters.Tags[(object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}"] = (object) null;
    bool result1 = false;
    bool result2 = !flag1;
    bool result3 = compositionFiltrationMode == HiddenCompositionFiltrationMode.None;
    bool result4 = !flag2;
    dictionary = new Dictionary<long, long>();
    if (args.OldParameters.Tags[(object) "{7C2D15CB-FD98-4A41-A036-6D3E5AF3FD1B}"] != null && args.OldParameters.Tags[(object) "{7C2D15CB-FD98-4A41-A036-6D3E5AF3FD1B}"] is Dictionary<long, long> dictionary && dictionary.Count == 0)
      dictionary = (Dictionary<long, long>) null;
    if (dictionary != null)
    {
      bool flag3 = false;
      foreach (KeyValuePair<long, long> keyValuePair in dictionary)
      {
        flag3 = keyValuePair.Value != 0L;
        if (flag3)
          break;
      }
      if (!flag3)
        dictionary = (Dictionary<long, long>) null;
    }
    if (args.OldParameters.Tags[(object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"].ToString(), out result1))
      result1 = false;
    if (args.OldParameters.Tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"] != null && args.OldParameters.Tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"].GetType() == typeof (HiddenCompositionFiltrationMode))
      compositionFiltrationMode = (HiddenCompositionFiltrationMode) args.OldParameters.Tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"];
    if (args.OldParameters.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] != null)
    {
      object tag = args.OldParameters.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"];
      if (tag is IEnumerable)
        longList1 = ((IEnumerable) tag).Cast<long>().ToList<long>();
    }
    if (!(this._versionRulesCacheService[args.Session.UserID, (object) "{9D621C68-0820-47EC-9ABB-CC7D2EF820F6}"] is List<long>))
    {
      List<long> longList2 = new List<long>(0);
    }
    if (args.OldParameters.Tags[(object) "cad005f9-306c-11d8-b4e9-00304f19f545"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "cad005f9-306c-11d8-b4e9-00304f19f545"].ToString(), out result2))
      result2 = !flag1;
    bool flag4 = result2 & !flag1;
    if (args.OldParameters.Tags[(object) "cad005ff-306c-11d8-b4e9-00304f19f545"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "cad005ff-306c-11d8-b4e9-00304f19f545"].ToString(), out result3))
      result3 = compositionFiltrationMode == HiddenCompositionFiltrationMode.None;
    bool flag5 = result3 & compositionFiltrationMode == HiddenCompositionFiltrationMode.None;
    if (args.OldParameters.Tags[(object) "cad005f9-306c-11d8-b4e9-00304f19f545"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "cad005f9-306c-11d8-b4e9-00304f19f545"].ToString(), out result4))
      result4 = !flag2;
    bool flag6 = result4 & !flag2;
    bool result5 = false;
    if (args.OldParameters.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"].ToString(), out result5))
      result5 = false;
    if (result5)
    {
      result1 = false;
      flag1 = false;
    }
    bool result6 = false;
    if (args.OldParameters.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"].ToString(), out result6))
      result6 = false;
    if (result6)
      compositionFiltrationMode = HiddenCompositionFiltrationMode.None;
    bool result7 = false;
    if (args.OldParameters.Tags[(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"].ToString(), out result7))
      result7 = false;
    if (result7)
      flag2 = false;
    if (longList1 == null & flag2)
    {
      args.OldParameters.Tags.Remove((object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}");
      flag2 = false;
      longList1 = new List<long>();
    }
    bool flag7 = flag1 & result1 && !result5;
    bool flag8 = flag2 && !result7;
    if (flag7 | flag8 || compositionFiltrationMode != HiddenCompositionFiltrationMode.None)
    {
      int num1 = 0;
      if (flag7)
      {
        if (dictionary != null)
          num1 += dictionary.Count * 2 + 1;
        else
          num1 += 2;
      }
      if (flag8)
        ++num1;
      int newSize = num1;
      if (args.OldParameters.Conditions != null)
        newSize = args.OldParameters.Conditions.Length + num1;
      Array.Resize<ConditionStructure>(ref args.OldParameters.Conditions, newSize);
      int index1 = newSize - num1;
      ConditionStructure conditionStructure1;
      if (flag7)
      {
        LogicalOperators logicalOperator = LogicalOperators.NONE;
        if (flag8)
          logicalOperator = LogicalOperators.AND;
        if (dictionary == null)
        {
          conditionStructure1 = new ConditionStructure(ServerPDMPlugin._substAttrID, RelationalOperators.Equal, (object) 0, LogicalOperators.OR, 1, true);
          args.OldParameters.Conditions[index1] = conditionStructure1;
          if (index1 > 0)
            args.OldParameters.Conditions[index1 - 1].LogicalOperator = LogicalOperators.AND;
          int index2 = index1 + 1;
          conditionStructure1 = new ConditionStructure(ServerPDMPlugin._substAttrID, RelationalOperators.NotExistsOrEmpty, (object) 0, logicalOperator, -1, true);
          args.OldParameters.Conditions[index2] = conditionStructure1;
          index1 = index2 + 1;
        }
        else
        {
          int num2 = 0;
          foreach (KeyValuePair<long, long> keyValuePair in dictionary)
          {
            ConditionStructure conditionStructure2 = new ConditionStructure(ServerPDMPlugin._substGroupAttrID, RelationalOperators.Equal, (object) keyValuePair.Key, LogicalOperators.AND, num2 == 0 ? 2 : 1, true);
            ConditionStructure conditionStructure3 = new ConditionStructure(ServerPDMPlugin._substAttrID, RelationalOperators.Equal, (object) keyValuePair.Value, LogicalOperators.OR, -1, true);
            args.OldParameters.Conditions[index1] = conditionStructure2;
            args.OldParameters.Conditions[index1 + 1] = conditionStructure3;
            if (index1 > 0 && num2 == 0)
              args.OldParameters.Conditions[index1 - 1].LogicalOperator = LogicalOperators.AND;
            index1 += 2;
            ++num2;
          }
          conditionStructure1 = new ConditionStructure(ServerPDMPlugin._substAttrID, RelationalOperators.NotExistsOrEmpty, (object) 0, logicalOperator, -1, true);
          args.OldParameters.Conditions[index1] = conditionStructure1;
          ++index1;
        }
      }
      if (flag8)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(ServerPDMPlugin._contextCompositionAttrID);
        if (attributeType != null)
        {
          List<object> objectList = new List<object>((IEnumerable<object>) attributeType.PossibleValues);
          for (int index3 = 0; index3 < longList1.Count; ++index3)
            objectList.Remove((object) longList1[index3]);
          if (objectList.Count == 0)
            longList1.Clear();
        }
        conditionStructure1 = longList1.Count <= 0 ? new ConditionStructure(ServerPDMPlugin._contextCompositionAttrID, RelationalOperators.NOP, (object) DBNull.Value, LogicalOperators.NONE, 0, true) : new ConditionStructure(ServerPDMPlugin._contextCompositionAttrID, RelationalOperators.In, (object) longList1.ToArray(), LogicalOperators.NONE, 0, true);
        args.OldParameters.Conditions[index1] = conditionStructure1;
        if (index1 > 0)
          args.OldParameters.Conditions[index1 - 1].LogicalOperator = LogicalOperators.AND;
        int num3 = index1 + 1;
      }
    }
    if (flag4 && flag6 && result6)
      return;
    args.OldParameters.Tags[(object) "{A568A877-0F03-460F-A2F4-7ACB5C674BDC}"] = (object) true;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(1);
    if (!flag4)
    {
      columnDescriptorList.Add(new ColumnDescriptor((object) ServerPDMPlugin._substGroupAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
      columnDescriptorList.Add(new ColumnDescriptor((object) ServerPDMPlugin._substAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    }
    if (!flag6)
      columnDescriptorList.Add(new ColumnDescriptor((object) ServerPDMPlugin._contextCompositionAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    if (!result6)
      columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    ColumnDescriptor[] array = columnDescriptorList.ToArray();
    List<int> AddedColumnsPos = new List<int>(0);
    if (!(flag7 | flag8) && compositionFiltrationMode == HiddenCompositionFiltrationMode.None && flag4 && flag6 && result6)
      return;
    int length = args.OldParameters.Columns != null ? args.OldParameters.Columns.Length : 0;
    args.OldParameters.AddColumnDescriptors(array, AddedColumnsPos);
    args.OldParameters.Tags[(object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}"] = (object) AddedColumnsPos;
    args.OldParameters.Tags[(object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}.ofs"] = (object) length;
    args.NewParameters = new DBRecordSetParams?(args.OldParameters);
  }

  internal void GetRecordsListEventHandler(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (table == null || session == null)
      return;
    if (this._versionRulesCacheService == null)
      this._versionRulesCacheService = session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    if (ServerPDMPlugin._substAttrID == 0 || ServerPDMPlugin._substGroupAttrID == 0)
    {
      ServerPDMPlugin._substAttrID = session.IdentHelper.SubstituteInGroup;
      ServerPDMPlugin._substGroupAttrID = session.IdentHelper.SubstitutesGroupNoID;
    }
    if (!(sender is DBRelationCollection relationCollection) || relationCollection.RelationTypeID == -1)
      return;
    if (!((session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService)[session.UserID, (object) "{9D621C68-0820-47EC-9ABB-CC7D2EF820F6}"] is List<long> longList))
      longList = new List<long>(0);
    if (parameters.Tags == null)
      return;
    HiddenCompositionFiltrationMode compositionFiltrationMode = HiddenCompositionFiltrationMode.None;
    if (parameters.Tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"] != null && parameters.Tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"].GetType() == typeof (HiddenCompositionFiltrationMode))
      compositionFiltrationMode = (HiddenCompositionFiltrationMode) parameters.Tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"];
    bool result1 = false;
    if (parameters.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] != null && !bool.TryParse(parameters.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"].ToString(), out result1))
      result1 = false;
    if (result1)
      compositionFiltrationMode = HiddenCompositionFiltrationMode.None;
    int columnIndex1 = DBRecordSet.AttributeColumnIndex(parameters, (object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object);
    if (columnIndex1 >= 0 && longList.Count > 0 && compositionFiltrationMode != HiddenCompositionFiltrationMode.None && table.Rows.Count > 0)
    {
      if (compositionFiltrationMode == HiddenCompositionFiltrationMode.HideChilds)
      {
        long num;
        try
        {
          num = relationCollection._ProjectID != -1L ? SqlHelper.GetIDByObjectID(relationCollection._ProjectID, (session as UserSession).DataManager) : -1L;
        }
        catch
        {
          num = -1L;
        }
        if (longList.Contains(num))
        {
          table.Rows.Clear();
          table.AcceptChanges();
        }
      }
      if (compositionFiltrationMode == HiddenCompositionFiltrationMode.HideAll)
      {
        List<DataRow> dataRowList = new List<DataRow>(0);
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          long result2 = -1;
          if (long.TryParse(row[columnIndex1].ToString(), out result2) && longList.Contains(result2))
            dataRowList.Add(row);
        }
        for (int index = 0; index < dataRowList.Count; ++index)
          table.Rows.Remove(dataRowList[index]);
        table.AcceptChanges();
      }
    }
    try
    {
      bool result3 = false;
      if (parameters.Tags[(object) "{A568A877-0F03-460F-A2F4-7ACB5C674BDC}"] != null && !bool.TryParse(parameters.Tags[(object) "{A568A877-0F03-460F-A2F4-7ACB5C674BDC}"].ToString(), out result3))
        result3 = false;
      if (!result3)
        return;
      int statusesColumnIndex = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref table);
      if (statusesColumnIndex < 0 || !(ServerServices.GetService(typeof (IElementStatusesService)) is IElementStatusesService service))
        return;
      int columnIndex2 = DBRecordSet.AttributeColumnIndex(parameters, (object) MetaDataHelper.GetAttributeTypeID("cad001c0-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, table);
      int columnIndex3 = DBRecordSet.AttributeColumnIndex(parameters, (object) MetaDataHelper.GetAttributeTypeID("cad001c1-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, table);
      int columnIndex4 = DBRecordSet.AttributeColumnIndex(parameters, (object) MetaDataHelper.GetAttributeTypeID("cad00651-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, table);
      int columnIndex5 = DBRecordSet.AttributeColumnIndex(parameters, (object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, table);
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        long result4 = 0;
        if (columnIndex2 < 0 || !long.TryParse(row[columnIndex2].ToString(), out result4))
          result4 = 0L;
        long result5 = 0;
        if (columnIndex3 < 0 || !long.TryParse(row[columnIndex3].ToString(), out result5))
          result5 = 0L;
        RelationAsSubstitutes relationAsSubstitutes = RelationAsSubstitutes.rsNoSubstitutes;
        if (result4 != 0L)
        {
          relationAsSubstitutes = RelationAsSubstitutes.rsSubstitute;
          if (result5 == 0L)
            relationAsSubstitutes = RelationAsSubstitutes.rsActualSubstitute;
        }
        short int16_1 = Convert.ToInt16((object) relationAsSubstitutes);
        service.SetElementStatuses16("cad005f4-306c-11d8-b4e9-00304f19f545", row[statusesColumnIndex] as byte[], int16_1);
        long result6 = 0;
        if (columnIndex4 < 0 || !long.TryParse(row[columnIndex4].ToString(), out result6))
          result6 = 0L;
        short int16_2 = Convert.ToInt16(result6);
        service.SetElementStatuses16("cad005fc-306c-11d8-b4e9-00304f19f545", row[statusesColumnIndex] as byte[], int16_2);
        long result7 = 0;
        if (columnIndex5 < 0 || !long.TryParse(row[columnIndex5].ToString(), out result7))
          result7 = 0L;
        short num = 0;
        if (longList.Contains(result7))
          num = (short) 1;
        service.SetElementStatuses16("cad005fe-306c-11d8-b4e9-00304f19f545", row[statusesColumnIndex] as byte[], num);
      }
    }
    finally
    {
      if (parameters.Tags[(object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}"] is List<int> tag1 && tag1.Count > 0)
      {
        int tag = (int) parameters.Tags[(object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}.ofs"];
        int num = Math.Min(table.Columns.Count - tag - tag1.Count, 0);
        for (int index = tag1.Count - 1; index >= 0; --index)
          table.Columns.RemoveAt(tag1[index] + num);
      }
      parameters.Tags.Remove((object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}");
      parameters.Tags.Remove((object) "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}.ofs");
    }
  }

  internal static void CopyRow(DataRow fromRow, DataRow toRow)
  {
    if (fromRow == null || toRow == null)
      return;
    int count = fromRow.Table.Columns.Count;
    for (int columnIndex = 0; columnIndex < count; ++columnIndex)
      toRow[columnIndex] = fromRow[columnIndex];
  }

  internal void SortDataTableByPosition(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (table == null || table.Columns.Count == 0 || table.Rows.Count <= 1 || session == null)
      return;
    if (ServerPDMPlugin._attrPosition == 0)
      ServerPDMPlugin._attrPosition = MetaDataHelper.GetAttributeID((object) "cad00270-306c-11d8-b4e9-00304f19f545");
    if (!(sender is DBRelationCollection))
      return;
    Dictionary<object, List<int>> columnsAttrs = new Dictionary<object, List<int>>();
    DBRecordSet.AttributeFindColumns(parameters, table, (object) ServerPDMPlugin._attrPosition, AttributeSourceTypes.Relation, ref columnsAttrs);
    if (!columnsAttrs.ContainsKey((object) ServerPDMPlugin._attrPosition) || columnsAttrs[(object) ServerPDMPlugin._attrPosition].Count == 0)
      return;
    Dictionary<int, SortOrders> sortedAttrs = new Dictionary<int, SortOrders>();
    DBRecordSet.AttributeFindSortOrders(parameters, ref sortedAttrs);
    if (!sortedAttrs.ContainsKey(ServerPDMPlugin._attrPosition))
      return;
    int[] array = new int[sortedAttrs.Count];
    sortedAttrs.Keys.CopyTo(array, 0);
    List<int> intList = new List<int>(array.Length);
    for (int index = 0; index < array.Length; ++index)
      intList.Add(array[index]);
    int columnIndex1 = columnsAttrs[(object) ServerPDMPlugin._attrPosition][0];
    int columnIndex2 = -1;
    if (intList.IndexOf(ServerPDMPlugin._attrPosition) > 0)
    {
      int num = intList[intList.IndexOf(ServerPDMPlugin._attrPosition) - 1];
      DBRecordSet.AttributeFindColumns(parameters, table, (object) num, AttributeSourceTypes.Relation, ref columnsAttrs);
      columnIndex2 = columnsAttrs.ContainsKey((object) num) ? columnsAttrs[(object) num][0] : -1;
    }
    int count1 = table.Rows.Count;
    Dictionary<ServerPDMPlugin.PositionsKey, List<DataRow>> dictionary = new Dictionary<ServerPDMPlugin.PositionsKey, List<DataRow>>();
    ServerPDMPlugin.PositionsKey positionsKey = new ServerPDMPlugin.PositionsKey((object) DBNull.Value);
    for (int index = 0; index < count1; ++index)
    {
      DataRow toRow = table.NewRow();
      ServerPDMPlugin.CopyRow(table.Rows[index], toRow);
      ServerPDMPlugin.PositionsKey key = columnIndex2 < 0 ? positionsKey : new ServerPDMPlugin.PositionsKey(toRow[columnIndex2]);
      if (!dictionary.ContainsKey(key))
        dictionary.Add(key, new List<DataRow>());
      dictionary[key].Add(toRow);
    }
    List<DataRow> dataRowList = new List<DataRow>();
    foreach (KeyValuePair<ServerPDMPlugin.PositionsKey, List<DataRow>> keyValuePair in dictionary)
    {
      keyValuePair.Value.Sort((IComparer<DataRow>) new ServerPDMPlugin.PositionsComparer(columnIndex1));
      int count2 = keyValuePair.Value.Count;
      for (int index = 0; index < count2; ++index)
        dataRowList.Add(keyValuePair.Value[index]);
    }
    int index1 = sortedAttrs[ServerPDMPlugin._attrPosition] != SortOrders.DESC ? 0 : count1 - 1;
    for (int index2 = 0; index2 < count1; ++index2)
    {
      table.Rows.RemoveAt(0);
      table.Rows.Add(dataRowList[index1]);
      if (sortedAttrs[ServerPDMPlugin._attrPosition] == SortOrders.DESC)
        --index1;
      else
        ++index1;
    }
  }

  internal class ServerPDMPluginClass : LongLifeObject, IPdmServerPlugin
  {
    public Guid PluginGuid => ServerPDMPlugin.PluginGuid;

    public void LockAutoCreateRelationForArticle(long articleObjectID, long partID)
    {
      List<long> longList1;
      if (!ServerPDMPlugin.lockAutoCreateRelation.TryGetValue(articleObjectID, out longList1))
      {
        List<long> longList2;
        ServerPDMPlugin.lockAutoCreateRelation.Add(articleObjectID, longList2 = new List<long>());
        longList2.Add(partID);
      }
      else
      {
        if (longList1.Contains(partID))
          return;
        longList1.Add(partID);
      }
    }

    public void UnlockAutoCreateRelationForArticle(long articleObjectID, long partID)
    {
      List<long> longList;
      if (!ServerPDMPlugin.lockAutoCreateRelation.TryGetValue(articleObjectID, out longList))
        return;
      longList.Remove(partID);
      if (longList.Count != 0)
        return;
      ServerPDMPlugin.lockAutoCreateRelation.Remove(articleObjectID);
    }
  }

  internal class PositionsComparer : IComparer<DataRow>
  {
    protected readonly string digits = "1234567890";
    protected int columnIndex;

    public PositionsComparer(int columnIndex) => this.columnIndex = columnIndex;

    public int Compare(DataRow x, DataRow y)
    {
      int num1 = 0;
      int num2;
      if (x == null && y == null)
        return num2 = 0;
      if (x == null && y != null)
        return num2 = -1;
      if (x != null && y == null)
        return num2 = 1;
      object obj1 = x[this.columnIndex];
      object obj2 = y[this.columnIndex];
      if (obj1 == DBNull.Value && obj2 == DBNull.Value)
        return num2 = 0;
      if (obj1 == DBNull.Value && obj2 != DBNull.Value)
        return num2 = -1;
      if (obj1 != DBNull.Value && obj2 == DBNull.Value)
        return num2 = 1;
      string str1 = obj1.ToString();
      string str2 = obj2.ToString();
      if (str1 == string.Empty && str2 == string.Empty)
        return num2 = 0;
      long result1 = 0;
      long result2 = 0;
      string str3 = string.Empty;
      string str4 = string.Empty;
      bool flag1 = false;
      bool flag2 = false;
      StringBuilder stringBuilder = new StringBuilder();
      for (int startIndex = 0; startIndex < str1.Length; ++startIndex)
      {
        string str5 = str1.Substring(startIndex, 1);
        if (this.digits.Contains(str5))
        {
          stringBuilder.Append(str5);
        }
        else
        {
          str3 = str1.Substring(startIndex, str1.Length - startIndex);
          break;
        }
      }
      if (stringBuilder.Length > 0)
        flag1 = long.TryParse(stringBuilder.ToString(), out result1);
      stringBuilder.Length = 0;
      for (int startIndex = 0; startIndex < str2.Length; ++startIndex)
      {
        string str6 = str2.Substring(startIndex, 1);
        if (this.digits.Contains(str6))
        {
          stringBuilder.Append(str6);
        }
        else
        {
          str4 = str2.Substring(startIndex, str2.Length - startIndex);
          break;
        }
      }
      if (stringBuilder.Length > 0)
        flag2 = long.TryParse(stringBuilder.ToString(), out result2);
      if (!flag1 & flag2)
        return num2 = -1;
      if (flag1 && !flag2)
        return num2 = 1;
      if (flag1 & flag2)
        num1 = result1.CompareTo(result2);
      return num1 != 0 ? num1 : str3.ToUpper().CompareTo(str4.ToUpper());
    }
  }

  internal class PositionsKey
  {
    public object Value;

    public PositionsKey(object value) => this.Value = value;

    public override bool Equals(object obj)
    {
      return !(obj is ServerPDMPlugin.PositionsKey positionsKey) ? base.Equals(obj) : object.Equals(this.Value, positionsKey.Value);
    }

    public override int GetHashCode()
    {
      return this.Value == null ? DBNull.Value.GetHashCode() : this.Value.GetHashCode();
    }
  }
}
