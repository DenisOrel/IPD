// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.RecalcScript
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class RecalcScript : ExpertRules
{
  public RecalcScript(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.objType = ExpertScriptType.RecalcScript;
  }

  protected override AttributeValues[] SaveData()
  {
    AttributeValues[] attributeValuesArray = base.SaveData();
    AttribPair key = new AttribPair(this.resAttrID, this.resObjTypeID);
    ScriptTreeNode val = ExpertServer.LoadScriptTree(this.xDoc);
    ExpertServer.es.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, ExpertServer.es.recalcScripts);
    IExpertServerSynchronizer service = (IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer));
    if (service == null)
      return attributeValuesArray;
    service.AddEvent(ExpServerCache.cacheRecalcScript, (long) this.resAttrID, (long) this.resObjTypeID, this.UserSession.DataManager);
    return attributeValuesArray;
  }

  protected override void DoDelete()
  {
    base.DoDelete();
    AttribPair key = new AttribPair(this.resAttrID, this.resObjTypeID);
    ExpertServer.es.DelValueFromCache<AttribPair, ScriptTreeNode>(key, ExpertServer.es.recalcScripts);
    ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheRecalcScript, (long) this.resAttrID, (long) this.resObjTypeID, this.UserSession.DataManager);
  }

  public override bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    this.UnpackXML();
    ScriptTreeNode root1 = ExpertServer.LoadScriptTree(this.xDoc);
    bool flag = false;
    for (int index = 0; index < root1.Items.Count; ++index)
    {
      ScriptTreeNode node = (ScriptTreeNode) root1.Items[index];
      flag = flag || this.ReplaceAttrForOneNode(node, session, fromAttribute, toAttribute);
    }
    if (this.cond != null)
      flag = flag || this.cond.PerformAttrChange(fromAttribute, toAttribute);
    if (fromAttribute.AttributeID == this.resAttrID)
    {
      IDBObjectCollection objectCollection = this.Session.GetObjectCollection(this.ObjectType);
      ConditionStructure[] conditions = new ConditionStructure[2];
      int attrResAttrGuid = ExpertConsts.Consts.attrResAttrGUID;
      Guid guid = toAttribute.GUID;
      string conditionValue = guid.ToString();
      conditions[0] = new ConditionStructure(attrResAttrGuid, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, false);
      conditions[1] = new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) this.resObjTypeGUID.ToString(), LogicalOperators.NONE, 0, false);
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        if (this.Session.GetObject(Convert.ToInt64(dataTable.Rows[0][0])) is RecalcScript recalcScript)
        {
          recalcScript.Load();
          ScriptTreeNode root2 = XMLScripter.LoadScript(recalcScript.Script);
          for (int index = 0; index < root1.Items.Count; ++index)
            root2.Items.Add(root1.Items[index]);
          recalcScript.SaveScript(root2);
          recalcScript.WriteBLOB();
          flag = true;
          this.Delete(0L);
        }
        return flag;
      }
      guid = toAttribute.GUID;
      this.resAttrGuid = guid.ToString();
      this.resAttrID = toAttribute.AttributeID;
      this.SetAttributesValues(this.GetResPair());
    }
    if (flag)
    {
      this.SaveScript(root1);
      this.WriteBLOB();
    }
    return flag;
  }
}
