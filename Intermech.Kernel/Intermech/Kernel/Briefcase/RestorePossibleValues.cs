// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RestorePossibleValues
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class RestorePossibleValues(
  IUserSession session,
  List<IDСorresponds> importingObjectIDs,
  ImportEventLog eventLog) : RestoreImportingValues<AttributeTypePossibleValues>(session, importingObjectIDs, eventLog)
{
  protected override void OnRestore(AttributeTypePossibleValues item, BriefcaseImportProgress bip)
  {
    IDBAttributeType attributeType = this.session.GetAttributeType(item.AttributeID, false);
    DataTable possibleValues = attributeType.GetPossibleValues();
    int count = possibleValues.Rows.Count;
    IDictionaryEnumerator enumerator = item.Values.GetEnumerator();
    while (enumerator.MoveNext())
    {
      try
      {
        object[] value = (object[]) enumerator.Value;
        IDСorresponds idСorresponds = value != null ? this.importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == (long) value[0])) : (IDСorresponds) null;
        if (idСorresponds != null)
        {
          string empty = string.Empty;
          string str = !(Convert.ToString(value[1]) == string.Empty) ? Convert.ToString(value[1]) : this.session.GetObjectInfo(idСorresponds.HostObjectID).Caption;
          DataRow[] dataRowArray = possibleValues.Select($"{attributeType.ValueFieldName} = {idСorresponds.HostObjectID}");
          if (dataRowArray.Length == 0)
          {
            (this.session as UserSession).DataManager.ExecuteNonQuery("INSERT INTO IMS_POSSIBLE_VALUES (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE) VALUES (:attrID, -1, -1, :id, :val, :descr)", (this.session as UserSession).DataManager.Parameter(sc_12961.ssp_appserver_12962(), (object) item.AttributeID), (this.session as UserSession).DataManager.Parameter("id", (object) count), (this.session as UserSession).DataManager.Parameter("descr", (object) str), (this.session as UserSession).DataManager.Parameter("val", (object) idСorresponds.HostObjectID));
            ++count;
          }
          else if (dataRowArray.Length == 1)
            (this.session as UserSession).DataManager.ExecuteNonQuery(sc_12961.ssp_appserver_12963(), (this.session as UserSession).DataManager.Parameter("attrID", (object) item.AttributeID), (this.session as UserSession).DataManager.Parameter("id", (object) count), (this.session as UserSession).DataManager.Parameter("descr", (object) str), (this.session as UserSession).DataManager.Parameter(sc_12961.ssp_appserver_12964(), (object) idСorresponds.HostObjectID));
        }
        else
          this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_352"), (object) item.AttributeID));
      }
      catch (Exception ex)
      {
        this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_353"), (object) item.AttributeID, (object) ex.Message));
        throw;
      }
    }
  }
}
