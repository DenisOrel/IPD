// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertServerPlugin
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.ApplicationModel;
using Intermech.Expert.Scenarios;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

internal class ExpertServerPlugin : IPackage, IConfigurable
{
  private static int _ExpertServerAppId;
  public IServiceProvider _serviceProvider;

  public void Unload()
  {
    (this._serviceProvider.GetService(typeof (ILicenser)) as ILicenser).ReleaseLicense(ExpertServerPlugin._ExpertServerAppId);
    ExpertServer.es.StopTimers();
  }

  public string Name => LocalizationHolder.rm.GetString("Expert.Server_86");

  private void ShowExpertInfoCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    (ServerServices.GetService(typeof (IExpertServer)) as IExpertServer).ShowExpertInfo();
  }

  public void Load(IServiceProvider serviceProvider)
  {
    this._serviceProvider = serviceProvider;
    if (ServerServices.GetService(typeof (IConsoleCommandRegistry)) is IConsoleCommandRegistry service1)
      service1.Add(new ConsoleCommandInfo("expert", string.Empty, string.Empty, new ConsoleCommandMethod(this.ShowExpertInfoCommand)));
    int appId = 341;
    byte[][] numArray1 = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 117,
        (byte) 83,
        (byte) 9,
        (byte) 52,
        (byte) 223,
        (byte) 117,
        (byte) 96 /*0x60*/,
        (byte) 234,
        (byte) 156,
        (byte) 77,
        (byte) 120,
        (byte) 29,
        (byte) 166,
        (byte) 229,
        (byte) 144 /*0x90*/,
        (byte) 227
      },
      new byte[16 /*0x10*/]
      {
        (byte) 145,
        (byte) 188,
        (byte) 108,
        (byte) 47,
        (byte) 55,
        (byte) 37,
        (byte) 250,
        (byte) 102,
        (byte) 54,
        (byte) 43,
        (byte) 253,
        (byte) 224 /*0xE0*/,
        (byte) 7,
        (byte) 3,
        (byte) 119,
        (byte) 216
      },
      new byte[16 /*0x10*/]
      {
        (byte) 101,
        (byte) 151,
        (byte) 225,
        (byte) 38,
        (byte) 90,
        (byte) 58,
        (byte) 59,
        (byte) 254,
        (byte) 132,
        (byte) 22,
        (byte) 193,
        (byte) 11,
        (byte) 56,
        (byte) 218,
        (byte) 181,
        (byte) 89
      },
      new byte[16 /*0x10*/]
      {
        (byte) 152,
        (byte) 15,
        (byte) 134,
        (byte) 251,
        (byte) 140,
        (byte) 134,
        (byte) 165,
        (byte) 48 /*0x30*/,
        (byte) 148,
        (byte) 5,
        (byte) 138,
        (byte) 173,
        (byte) 142,
        (byte) 72,
        (byte) 62,
        (byte) 32 /*0x20*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 117,
        (byte) 171,
        (byte) 221,
        (byte) 162,
        (byte) 48 /*0x30*/,
        (byte) 14,
        (byte) 233,
        (byte) 23,
        (byte) 134,
        (byte) 221,
        (byte) 48 /*0x30*/,
        (byte) 89,
        (byte) 100,
        (byte) 145,
        (byte) 239,
        (byte) 127 /*0x7F*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 79,
        (byte) 169,
        (byte) 91,
        (byte) 200,
        (byte) 72,
        (byte) 228,
        (byte) 44,
        (byte) 61,
        (byte) 13,
        (byte) 110,
        (byte) 95,
        (byte) 96 /*0x60*/,
        (byte) 84,
        (byte) 73,
        (byte) 212,
        (byte) 224 /*0xE0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 175,
        (byte) 99,
        (byte) 98,
        (byte) 74,
        (byte) 22,
        (byte) 162,
        (byte) 7,
        (byte) 58,
        (byte) 24,
        (byte) 10,
        (byte) 175,
        (byte) 200,
        (byte) 180,
        (byte) 84,
        (byte) 177,
        (byte) 194
      },
      new byte[16 /*0x10*/]
      {
        (byte) 8,
        (byte) 193,
        (byte) 179,
        (byte) 251,
        (byte) 91,
        (byte) 218,
        (byte) 9,
        (byte) 212,
        (byte) 168,
        (byte) 116,
        (byte) 64 /*0x40*/,
        (byte) 24,
        (byte) 62,
        (byte) 104,
        (byte) 25,
        (byte) 224 /*0xE0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 151,
        (byte) 31 /*0x1F*/,
        (byte) 168,
        (byte) 250,
        (byte) 7,
        (byte) 238,
        (byte) 178,
        (byte) 87,
        (byte) 173,
        (byte) 202,
        (byte) 159,
        (byte) 125,
        (byte) 9,
        (byte) 225,
        (byte) 154,
        (byte) 242
      },
      new byte[16 /*0x10*/]
      {
        (byte) 168,
        (byte) 195,
        (byte) 144 /*0x90*/,
        (byte) 166,
        (byte) 50,
        (byte) 182,
        (byte) 41,
        (byte) 85,
        (byte) 245,
        (byte) 129,
        (byte) 228,
        (byte) 166,
        (byte) 133,
        byte.MaxValue,
        (byte) 18,
        (byte) 151
      },
      new byte[16 /*0x10*/]
      {
        (byte) 39,
        (byte) 65,
        (byte) 92,
        (byte) 213,
        (byte) 120,
        (byte) 107,
        (byte) 67,
        (byte) 210,
        (byte) 163,
        (byte) 163,
        (byte) 137,
        (byte) 38,
        (byte) 95,
        (byte) 33,
        (byte) 99,
        (byte) 49
      },
      new byte[16 /*0x10*/]
      {
        (byte) 9,
        (byte) 23,
        (byte) 155,
        (byte) 146,
        (byte) 167,
        (byte) 112 /*0x70*/,
        (byte) 201,
        (byte) 12,
        (byte) 242,
        (byte) 66,
        (byte) 120,
        (byte) 46,
        (byte) 163,
        (byte) 188,
        (byte) 237,
        (byte) 205
      },
      new byte[16 /*0x10*/]
      {
        (byte) 43,
        (byte) 110,
        (byte) 194,
        (byte) 69,
        (byte) 233,
        (byte) 244,
        (byte) 120,
        (byte) 192 /*0xC0*/,
        (byte) 176 /*0xB0*/,
        (byte) 11,
        (byte) 156,
        (byte) 76,
        (byte) 2,
        (byte) 138,
        (byte) 49,
        (byte) 55
      },
      new byte[16 /*0x10*/]
      {
        (byte) 75,
        (byte) 14,
        (byte) 238,
        (byte) 57,
        (byte) 195,
        (byte) 196,
        (byte) 183,
        (byte) 217,
        (byte) 108,
        (byte) 143,
        (byte) 77,
        (byte) 248,
        (byte) 116,
        (byte) 117,
        (byte) 174,
        (byte) 80 /*0x50*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 112 /*0x70*/,
        (byte) 188,
        (byte) 161,
        (byte) 114,
        (byte) 248,
        (byte) 220,
        (byte) 74,
        (byte) 43,
        (byte) 220,
        (byte) 213,
        (byte) 87,
        (byte) 100,
        (byte) 101,
        (byte) 140,
        (byte) 247,
        (byte) 140
      },
      new byte[16 /*0x10*/]
      {
        (byte) 139,
        (byte) 236,
        (byte) 223,
        (byte) 69,
        (byte) 78,
        (byte) 231,
        (byte) 31 /*0x1F*/,
        (byte) 106,
        (byte) 96 /*0x60*/,
        (byte) 15,
        (byte) 145,
        (byte) 47,
        (byte) 160 /*0xA0*/,
        (byte) 70,
        (byte) 33,
        (byte) 170
      },
      new byte[16 /*0x10*/]
      {
        (byte) 103,
        (byte) 109,
        (byte) 81,
        (byte) 155,
        (byte) 30,
        (byte) 84,
        (byte) 9,
        (byte) 51,
        (byte) 201,
        (byte) 155,
        (byte) 36,
        (byte) 109,
        (byte) 207,
        (byte) 246,
        (byte) 208 /*0xD0*/,
        (byte) 25
      },
      new byte[16 /*0x10*/]
      {
        (byte) 79,
        (byte) 31 /*0x1F*/,
        (byte) 120,
        (byte) 214,
        (byte) 56,
        (byte) 192 /*0xC0*/,
        (byte) 60,
        (byte) 25,
        (byte) 37,
        byte.MaxValue,
        (byte) 4,
        (byte) 24,
        (byte) 106,
        (byte) 104,
        (byte) 74,
        (byte) 101
      },
      new byte[16 /*0x10*/]
      {
        (byte) 113,
        (byte) 81,
        (byte) 182,
        (byte) 163,
        (byte) 128 /*0x80*/,
        (byte) 240 /*0xF0*/,
        (byte) 57,
        (byte) 79,
        (byte) 36,
        (byte) 151,
        (byte) 105,
        (byte) 20,
        (byte) 156,
        (byte) 19,
        (byte) 91,
        (byte) 36
      },
      new byte[16 /*0x10*/]
      {
        (byte) 172,
        (byte) 194,
        (byte) 87,
        (byte) 183,
        (byte) 151,
        (byte) 118,
        (byte) 180,
        (byte) 177,
        (byte) 125,
        (byte) 170,
        (byte) 118,
        (byte) 202,
        (byte) 7,
        (byte) 204,
        (byte) 250,
        (byte) 148
      },
      new byte[16 /*0x10*/]
      {
        (byte) 19,
        (byte) 16 /*0x10*/,
        (byte) 228,
        (byte) 149,
        (byte) 56,
        (byte) 149,
        (byte) 25,
        (byte) 27,
        (byte) 179,
        (byte) 109,
        (byte) 169,
        (byte) 93,
        (byte) 184,
        (byte) 220,
        (byte) 92,
        (byte) 229
      },
      new byte[16 /*0x10*/]
      {
        (byte) 75,
        (byte) 219,
        (byte) 55,
        (byte) 191,
        (byte) 230,
        (byte) 32 /*0x20*/,
        (byte) 22,
        (byte) 170,
        (byte) 97,
        (byte) 78,
        (byte) 157,
        (byte) 129,
        (byte) 179,
        (byte) 85,
        (byte) 21,
        (byte) 6
      },
      new byte[16 /*0x10*/]
      {
        (byte) 5,
        (byte) 62,
        (byte) 242,
        (byte) 19,
        (byte) 214,
        (byte) 84,
        (byte) 12,
        (byte) 118,
        (byte) 52,
        (byte) 2,
        (byte) 118,
        (byte) 135,
        (byte) 234,
        (byte) 244,
        (byte) 150,
        (byte) 181
      },
      new byte[16 /*0x10*/]
      {
        (byte) 67,
        (byte) 135,
        (byte) 53,
        (byte) 35,
        (byte) 58,
        (byte) 154,
        (byte) 177,
        (byte) 195,
        (byte) 134,
        (byte) 0,
        (byte) 7,
        (byte) 16 /*0x10*/,
        (byte) 74,
        (byte) 39,
        (byte) 131,
        (byte) 70
      },
      new byte[16 /*0x10*/]
      {
        (byte) 63 /*0x3F*/,
        (byte) 0,
        (byte) 89,
        (byte) 190,
        (byte) 17,
        (byte) 58,
        (byte) 225,
        (byte) 219,
        (byte) 56,
        (byte) 154,
        (byte) 240 /*0xF0*/,
        (byte) 18,
        (byte) 71,
        (byte) 219,
        (byte) 151,
        (byte) 71
      },
      new byte[16 /*0x10*/]
      {
        (byte) 136,
        (byte) 251,
        (byte) 9,
        (byte) 119,
        (byte) 149,
        (byte) 176 /*0xB0*/,
        (byte) 62,
        (byte) 248,
        (byte) 201,
        (byte) 17,
        (byte) 47,
        (byte) 66,
        (byte) 193,
        (byte) 239,
        (byte) 179,
        (byte) 141
      },
      new byte[16 /*0x10*/]
      {
        (byte) 58,
        (byte) 132,
        (byte) 15,
        (byte) 94,
        (byte) 33,
        (byte) 72,
        (byte) 77,
        (byte) 191,
        (byte) 4,
        (byte) 110,
        (byte) 96 /*0x60*/,
        (byte) 53,
        (byte) 31 /*0x1F*/,
        (byte) 145,
        (byte) 49,
        (byte) 190
      },
      new byte[16 /*0x10*/]
      {
        (byte) 160 /*0xA0*/,
        (byte) 247,
        (byte) 131,
        (byte) 50,
        (byte) 227,
        (byte) 145,
        (byte) 13,
        (byte) 129,
        (byte) 54,
        (byte) 84,
        (byte) 80 /*0x50*/,
        (byte) 5,
        (byte) 195,
        (byte) 57,
        (byte) 83,
        (byte) 239
      },
      new byte[16 /*0x10*/]
      {
        (byte) 215,
        (byte) 243,
        (byte) 29,
        (byte) 183,
        (byte) 131,
        (byte) 157,
        (byte) 211,
        (byte) 202,
        (byte) 92,
        (byte) 22,
        (byte) 218,
        (byte) 60,
        (byte) 100,
        (byte) 249,
        (byte) 47,
        (byte) 88
      },
      new byte[16 /*0x10*/]
      {
        (byte) 61,
        (byte) 82,
        (byte) 151,
        (byte) 92,
        (byte) 181,
        (byte) 82,
        (byte) 194,
        (byte) 66,
        (byte) 88,
        (byte) 246,
        (byte) 9,
        (byte) 10,
        (byte) 22,
        (byte) 177,
        (byte) 241,
        (byte) 67
      },
      new byte[16 /*0x10*/]
      {
        (byte) 107,
        (byte) 47,
        (byte) 76,
        (byte) 102,
        (byte) 229,
        (byte) 55,
        (byte) 147,
        (byte) 247,
        (byte) 178,
        (byte) 154,
        (byte) 186,
        (byte) 163,
        (byte) 217,
        (byte) 171,
        (byte) 188,
        (byte) 166
      },
      new byte[16 /*0x10*/]
      {
        (byte) 16 /*0x10*/,
        (byte) 140,
        (byte) 76,
        (byte) 93,
        (byte) 167,
        (byte) 118,
        (byte) 150,
        (byte) 220,
        (byte) 46,
        (byte) 210,
        (byte) 58,
        (byte) 228,
        (byte) 44,
        (byte) 155,
        (byte) 68,
        (byte) 144 /*0x90*/
      }
    };
    IProtectionKey service2 = serviceProvider.GetService(typeof (IProtectionKey)) as IProtectionKey;
    ILicenser service3 = serviceProvider.GetService(typeof (ILicenser)) as ILicenser;
    if (service2 != null)
    {
      ExpertServerPlugin._ExpertServerAppId = appId;
      if (service3 != null)
      {
        service3.AllocateLicense(appId);
        int index1 = (Environment.TickCount & 15) * 2;
        byte[] queryData = numArray1[index1];
        byte[] numArray2 = numArray1[index1 + 1];
        byte[] response = new byte[numArray2.Length];
        service2.Query(true, ExpertServerPlugin._ExpertServerAppId, queryData, response);
        int length = queryData.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if ((int) numArray2[index2] != (int) response[index2])
            goto label_15;
        }
        if (ServerServices.GetService(typeof (IPluginManager)) is IPluginManager service4)
          service4.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
        ExpertServer.es._serviceProvider = serviceProvider;
        IDBObjectCreator creatorInstance = (IDBObjectCreator) new ExpertObjectCreator();
        ICreatorContainer service5 = this._serviceProvider.GetService(typeof (IDBObjectService)) as ICreatorContainer;
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertCond), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertFormula), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertFunction), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertScript), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertAttrRules), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertObjRules), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ExpertTable), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.DocScript), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.RecalcScript), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.SimpleFormula), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.ComplectTemplate), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.objESFolder), (object) creatorInstance);
        service5.AddCreator((object) new Guid(ExpertObjGUIDs.CommandScript), (object) creatorInstance);
        ExpertServer.es.Init();
        service5.AddCreator((object) ScenarioGUIDs.objtypeScenarioDocs, (object) new DBReportScenarioCreator());
        service5.AddCreator((object) ScenarioGUIDs.objtypeScenarioComplectDocs, (object) new DBDocComplectScenarioCreator());
        (this._serviceProvider.GetService(typeof (ICustomServices)) as ICustomServices).AddService(typeof (IExpertServer), (object) ExpertServer.es);
        ServerServices.AddService(typeof (IExpertServer), (object) ExpertServer.es);
        string str = System.Configuration.ConfigurationManager.AppSettings.Get("DocCompReport");
        ExpertServer.es.compTrace = str != null && str.ToUpper() == "TRUE";
        (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AfterCacheReload += new CacheReloadHandler(this.eHelper_AfterCacheReload);
        IUserSession sessionTemporaryClone = (this._serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("Expert.Load");
        try
        {
          (ServerServices.GetService(typeof (IAttachedSelectionsServerService)) as IAttachedSelectionsServerService).RegisterCategory(sessionTemporaryClone, sessionTemporaryClone.GetObjectType(new Guid(ExpertObjGUIDs.DocScript)).ObjectType);
        }
        finally
        {
          sessionTemporaryClone?.Logout("Expert.Load");
        }
      }
    }
label_15:
    ICacheDataset service6 = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
    ExpertConsts.Init(service6.GetTable("IMS_OBJECT_TYPES"), service6.GetTable("IMS_ATTRIBUTES"));
    IEventLogHelper service7 = this._serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service7.BeforeDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(this.iLogH_BeforeDeleteAttributeTypeEvent);
    service7.AfterCombineAttributesEvent += new CombineAttributesHandler(this.iLogH_AfterCombineAttributesEvent);
    service7.GetUsedAttributesEvent += new GetUsedAttributesHandler(this.ILogH_GetUsedAttributesEvent);
    ExpertFunc.GetUserFunc += new GetUserDataHandler(ExpertServerPlugin.GetUserFunction);
    if (!(ServerServices.GetService(typeof (ICategoryExportManager)) is ICategoryExportManager service8))
      return;
    ICategoryExport iCategoryExport = (ICategoryExport) new ExpObjectExporter();
    service8.RegisterCategoryExport(1, iCategoryExport);
    service8.RegisterCategoryExport(3, iCategoryExport);
  }

  private void ILogH_GetUsedAttributesEvent(IUserSession session, UsedAttributesEventArgs args)
  {
    ExpertServer.es.GetUsedAttrs(session, args);
  }

  private void iLogH_AfterCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    ExpertServer.es.AttributesCombined(fromAttribute, toAttribute, session, combineMode);
  }

  private void eHelper_AfterCacheReload(IDbManager db) => ExpertServer.es.StartTimers();

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    if (ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service1)
    {
      if (ServerServices.GetService(typeof (IFormDesignerServer)) is IFormDesignerServer service)
      {
        service.Register(-1, AttributableElements.Object, new UpdateHandlerInfo(100, new UpdateHandler(this.fdService_Update)));
        service.Register(-1, AttributableElements.Relation, new UpdateHandlerInfo(100, new UpdateHandler(this.fdService_Update)));
      }
      ExpertServer.es.iis = (IImbaseServer) service1.GetService(typeof (IImbaseServer));
      ExpertServer.es.iies = (IImbaseExtendedService) service1.GetService(typeof (IImbaseExtendedService));
    }
    ExpertServer.es.InitDocComplectTypes();
    IUserSession sessionTemporaryClone = (this._serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("Expert.LoadComplete");
    try
    {
      ESFolderKeeper.Keeper.LoadAllFormulae(sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.LoadComplete");
    }
    IEventLogHelper service2 = (IEventLogHelper) this._serviceProvider.GetService(typeof (IEventLogHelper));
    if (service2 != null)
    {
      service2.AfterIncludeAttributeToGroup += new AttributeGroupIncludeExcludeHandler(ExpertServer.es.iel_AfterIncludeAttributeToGroup);
      service2.AfterExcludeAttributeFromGroup += new AttributeGroupIncludeExcludeHandler(ExpertServer.es.iel_AfterExcludeAttributeFromGroup);
    }
    IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(ExpertConsts.Consts.linkSimpleSortId, ExpertConsts.Consts.attrCompListNum);
    ExpertServer.es.needListNumsOnLinks = attribute4RelationType != null;
  }

  private void fdService_Update(object sender, UpdateHandlerEventArgs args)
  {
    if (args == null || args.Parent == null)
      return;
    IUserSession session = args.Parent.Session;
    long relId = 0;
    long[] objIds;
    switch (args.Kind)
    {
      case AttributableElements.Object:
        if (args.ParentRelation != null)
        {
          relId = args.ParentRelation.RelationID;
          objIds = new long[2]
          {
            (args.Parent as IDBObject).ObjectID,
            relId
          };
          break;
        }
        objIds = new long[1]
        {
          (args.Parent as IDBObject).ObjectID
        };
        break;
      case AttributableElements.Relation:
        IDBRelation parent = args.Parent as IDBRelation;
        relId = parent.RelationID;
        IDBAttribute attributeById = parent.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
        long objectID = attributeById != null ? attributeById.AsInteger : 0L;
        if (objectID == 0L)
        {
          IDBObject objectById = session.GetObjectByID(parent.PartID, false);
          objectID = objectById != null ? objectById.ObjectID : relId;
        }
        if (objectID != relId)
          objectID = session.GetObjectActualCopy(objectID, false).ObjectID;
        objIds = new long[1]{ objectID };
        break;
      default:
        return;
    }
    List<FormInformation> formInformationList1 = new List<FormInformation>(args.NewList != null ? (IEnumerable<FormInformation>) args.NewList : (IEnumerable<FormInformation>) args.OldList);
    List<FormInformation> formInformationList2 = new List<FormInformation>();
    if (formInformationList1.Count > 0)
    {
      int taskId = ExpertServer.es.StartTask(session.SessionGUID);
      try
      {
        foreach (FormInformation formInformation in formInformationList1)
        {
          if (formInformation != null && !formInformationList2.Contains(formInformation))
          {
            if (!formInformation.HasFormula)
            {
              formInformationList2.Add(formInformation);
            }
            else
            {
              if (!(formInformation.FormulaData is TempFormula tf))
              {
                tf = CondHelper.LoadObjectCond(session, formInformation.ID);
                formInformation.FormulaData = (object) tf;
              }
              if (tf == null)
              {
                formInformationList2.Add(formInformation);
              }
              else
              {
                object obj;
                ExpertResult expertResult = ExpertServer.es.CalcFormula(taskId, (object) tf, objIds, out obj, relId);
                bool boolean = Convert.ToBoolean(obj);
                if (expertResult.Equals((object) ExpertResult.OK) & boolean)
                  formInformationList2.Add(formInformation);
              }
            }
          }
        }
      }
      finally
      {
        ExpertServer.es.EndTask(taskId);
      }
    }
    args.NewList = formInformationList2;
  }

  internal long[] GetObjectsForAttr(IUserSession ius, Guid attrTypeGUID)
  {
    IDBObjectCollection objectCollection1 = ius.GetObjectCollection(ExpertConsts.Consts.objObject);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrAttrGUIDs, RelationalOperators.Equal, (object) attrTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    });
    DataTable dataTable1 = objectCollection1.Select(paramSet);
    HashSet<long> hashSet = new HashSet<long>();
    if (dataTable1 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        hashSet.Add(Convert.ToInt64(row[0]));
    }
    IDBObjectCollection objectCollection2 = ius.GetObjectCollection(ExpertConsts.Consts.objFormula);
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) attrTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    });
    DataTable dataTable2 = objectCollection2.Select(paramSet);
    if (dataTable2 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        hashSet.Add(Convert.ToInt64(row[0]));
    }
    return hashSet.ToArray<long>();
  }

  private void iLogH_BeforeDeleteAttributeTypeEvent(IDBAttributeType sender, IUserSession session)
  {
    Guid guid = (sender as IDBGuid).GUID;
    long[] objectsForAttr = this.GetObjectsForAttr(session, guid);
    if (objectsForAttr != null && objectsForAttr.Length != 0)
      throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_87"), (object) guid.ToString(), (object) objectsForAttr.Length));
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Open("ExpertServer");
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Create("ExpertServer");
  }

  public static FuncData GetUserFunction(int funcId) => ExpertServer.es.GetFuncData(funcId);
}
