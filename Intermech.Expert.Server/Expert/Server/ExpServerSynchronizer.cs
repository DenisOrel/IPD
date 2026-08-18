// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpServerSynchronizer
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

internal class ExpServerSynchronizer : CustomServerSynchronizer, IExpertServerSynchronizer
{
  internal static Dictionary<string, ExpServerCache> codeTableFrom = new Dictionary<string, ExpServerCache>()
  {
    {
      "!UNKNOWN",
      ExpServerCache.cacheUnknown
    },
    {
      "ATR_NAME",
      ExpServerCache.cacheAttrNames
    },
    {
      "ATR_RULE",
      ExpServerCache.cacheAttrRules
    },
    {
      "OBJ_RULE",
      ExpServerCache.cacheObjRules
    },
    {
      "REC_SCRP",
      ExpServerCache.cacheRecalcScript
    },
    {
      "EXP_TABL",
      ExpServerCache.cacheTables
    },
    {
      "EXP_FLDR",
      ExpServerCache.cacheExpFolder
    },
    {
      "EXP_SCPT",
      ExpServerCache.cacheScripts
    },
    {
      "EXP_FORM",
      ExpServerCache.cacheFormula
    },
    {
      "EXP_COND",
      ExpServerCache.cacheCond
    },
    {
      "OBJ_FLDR",
      ExpServerCache.cacheObjFromFolder
    },
    {
      "VIS_SCHM",
      ExpServerCache.cacheVisDataScheme
    }
  };
  internal static Dictionary<ExpServerCache, string> codeTableTo = new Dictionary<ExpServerCache, string>()
  {
    {
      ExpServerCache.cacheUnknown,
      "!UNKNOWN"
    },
    {
      ExpServerCache.cacheAttrNames,
      "ATR_NAME"
    },
    {
      ExpServerCache.cacheAttrRules,
      "ATR_RULE"
    },
    {
      ExpServerCache.cacheObjRules,
      "OBJ_RULE"
    },
    {
      ExpServerCache.cacheRecalcScript,
      "REC_SCRP"
    },
    {
      ExpServerCache.cacheTables,
      "EXP_TABL"
    },
    {
      ExpServerCache.cacheExpFolder,
      "EXP_FLDR"
    },
    {
      ExpServerCache.cacheScripts,
      "EXP_SCPT"
    },
    {
      ExpServerCache.cacheFormula,
      "EXP_FORM"
    },
    {
      ExpServerCache.cacheCond,
      "EXP_COND"
    },
    {
      ExpServerCache.cacheObjFromFolder,
      "OBJ_FLDR"
    },
    {
      ExpServerCache.cacheVisDataScheme,
      "VIS_SCHM"
    }
  };
  private ExpertServer _es;

  public ExpServerSynchronizer(ExpertServer es)
    : base(new Guid("f2b4ee0d-cd4b-4739-a93e-978664a57f66"), "Служба синхронизации кэшей экспертной системы")
  {
    this._es = es;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    string[] strArray = eventProps.StringInfo.Split(';');
    ExpServerCache objKind = ExpServerCache.cacheUnknown;
    if (ExpServerSynchronizer.codeTableFrom.ContainsKey(strArray[0]))
      objKind = ExpServerSynchronizer.codeTableFrom[strArray[0]];
    if (strArray.Length == 0 || objKind == ExpServerCache.cacheUnknown)
      return;
    long int64_1 = Convert.ToInt64(strArray[1]);
    long int64_2 = Convert.ToInt64(strArray[2]);
    IDBObject expertObject1 = this.GetExpertObject(session, objKind, int64_1, int64_2);
    ScriptTreeNode val1 = (ScriptTreeNode) null;
    AttribPair key = new AttribPair((int) int64_1, (int) int64_2);
    switch (objKind)
    {
      case ExpServerCache.cacheAttrRules:
        if (expertObject1 != null)
        {
          ExpertAttrRules expertAttrRules = (ExpertAttrRules) expertObject1;
          expertAttrRules.Load();
          val1 = XMLScripter.LoadScript(expertAttrRules.Script);
        }
        ExpertServer.es.SetValueToCache<AttribPair, ScriptTreeNode>(key, val1, ExpertServer.es.attrRules);
        break;
      case ExpServerCache.cacheObjRules:
        if (expertObject1 != null)
        {
          ExpertObjRules expertObjRules = (ExpertObjRules) expertObject1;
          expertObjRules.Load();
          val1 = XMLScripter.LoadScript(expertObjRules.Script);
        }
        ExpertServer.es.SetValueToCache<AttribPair, ScriptTreeNode>(key, val1, ExpertServer.es.objRules);
        break;
      case ExpServerCache.cacheRecalcScript:
        if (expertObject1 != null)
        {
          RecalcScript recalcScript = (RecalcScript) expertObject1;
          recalcScript.Load();
          val1 = XMLScripter.LoadScript(recalcScript.Script);
        }
        ExpertServer.es.SetValueToCache<AttribPair, ScriptTreeNode>(key, val1, ExpertServer.es.recalcScripts);
        break;
      case ExpServerCache.cacheTables:
        eTableCollection val2 = (eTableCollection) null;
        if (expertObject1 != null)
        {
          ExpertTable expertTable = (ExpertTable) expertObject1;
          expertTable.Load();
          val2 = expertTable.LoadTableData();
        }
        ExpertServer.es.SetValueToCache<long, eTableCollection>(int64_1, val2, ExpertServer.es.expertTables);
        break;
      case ExpServerCache.cacheScripts:
        if (expertObject1 != null)
        {
          ExpertScript expertScript = (ExpertScript) expertObject1;
          expertScript.Load();
          val1 = XMLScripter.LoadScript(expertScript.Script);
        }
        ExpertServer.es.SetValueToCache<long, ScriptTreeNode>(int64_1, val1, ExpertServer.es.expertScripts);
        break;
      case ExpServerCache.cacheFormula:
        if (expertObject1 == null)
          break;
        ExpertFormula expertFormula = (ExpertFormula) expertObject1;
        expertFormula.Load();
        ExpertServer.ExpertFormulaInfo val3 = new ExpertServer.ExpertFormulaInfo(expertFormula.GetTempFormula(), expertFormula.resAttrGuid, expertFormula.resObjTypeGuid);
        ExpertServer.es.SetValueToCache<long, ExpertServer.ExpertFormulaInfo>(int64_1, val3, ExpertServer.es.expertFormulae);
        break;
      case ExpServerCache.cacheCond:
        TempFormula val4 = (TempFormula) null;
        if (expertObject1 != null)
        {
          ExpertObject expertObject2 = (ExpertObject) expertObject1;
          expertObject2.Load();
          val4 = expertObject2.Cond;
        }
        ExpertServer.es.SetValueToCache<long, TempFormula>(int64_1, val4, ExpertServer.es.expertConds);
        break;
      case ExpServerCache.cacheObjFromFolder:
        ESFolderKeeper.Keeper.RemoveFromFolderCache(Math.Abs(int64_1));
        break;
    }
  }

  internal IDBObject GetExpertObject(
    IUserSession ius,
    ExpServerCache objKind,
    long attrTypeId,
    long objTypeId)
  {
    int num = -1;
    int objectType;
    switch (objKind)
    {
      case ExpServerCache.cacheAttrRules:
        objectType = ExpertConsts.Consts.objAttrRules;
        break;
      case ExpServerCache.cacheObjRules:
        objectType = ExpertConsts.Consts.objObjRules;
        break;
      case ExpServerCache.cacheRecalcScript:
        objectType = ExpertConsts.Consts.objRecalcScript;
        break;
      case ExpServerCache.cacheTables:
        num = ExpertConsts.Consts.objTable;
        return ius.GetObject(attrTypeId, false);
      case ExpServerCache.cacheScripts:
        num = ExpertConsts.Consts.objScript;
        return ius.GetObject(attrTypeId, false);
      case ExpServerCache.cacheFormula:
        num = ExpertConsts.Consts.objFormula;
        return ius.GetObject(attrTypeId, false);
      case ExpServerCache.cacheCond:
        num = ExpertConsts.Consts.objScript;
        return ius.GetObject(attrTypeId, false);
      case ExpServerCache.cacheVisDataScheme:
        num = ExpertConsts.Consts.objVisScheme;
        return ius.GetObject(attrTypeId, false);
      default:
        return (IDBObject) null;
    }
    string conditionValue1 = MetaDataHelper.GetAttributeTypeGuid((int) attrTypeId).ToString();
    string conditionValue2 = MetaDataHelper.GetObjectTypeGuid((int) objTypeId).ToString();
    DataTable dataTable = ius.GetObjectCollection(objectType).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) conditionValue2, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
      new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) conditionValue1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }));
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return (IDBObject) null;
    long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
    return ius.GetObject(int64, false);
  }

  public void AddEvent(ExpServerCache cacheType, long Id1, long Id2, IDbManager db)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps($"{ExpServerSynchronizer.codeTableTo[cacheType]};{Convert.ToString(Id1)};{Convert.ToString(Id2)}"), db);
  }
}
