// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.AttributableLinksImporter`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal abstract class AttributableLinksImporter<TAttributableLink> : 
  LinksImporter<TAttributableLink>
  where TAttributableLink : LinksBase
{
  protected readonly string valueFieldName;

  public AttributableLinksImporter(
    UserSession session,
    List<IDСorresponds> importingObjects,
    List<long> recordKindStated,
    Action<string> addIntoLogFunc,
    string valueFieldName)
    : base(session, importingObjects, recordKindStated, addIntoLogFunc)
  {
    this.valueFieldName = valueFieldName;
  }

  protected virtual long CheckBeforeImport(LinksBase link)
  {
    if (link.OldLinkID == 0L)
      return 0;
    IDСorresponds idСorresponds = this.importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == link.OldLinkID));
    if (idСorresponds != null)
      return idСorresponds.HostObjectID;
    this.addIntoLogFunc(string.Format(LocalizationHolder.rm.GetString("Kernel_300"), (object) link.OldLinkID));
    return 0;
  }

  protected abstract long GetAttributableID(TAttributableLink link);

  protected abstract string GetAttributeTableName(TAttributableLink link);

  protected abstract string[] GetUpdateTables(TAttributableLink link);

  protected virtual void OnAfterUpdate(params IDbDataParameter[] commandParameters)
  {
  }

  public override bool Import(TAttributableLink link)
  {
    long num = this.CheckBeforeImport((LinksBase) link);
    if (num == 0L)
      return false;
    this.session.StartTransaction();
    try
    {
      IDbDataParameter intPar = this.session.DataManager.Parameter("int", (object) num);
      IDbDataParameter strPar = this.session.DataManager.Parameter("capt", (object) link.Caption);
      IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("list", (object) link.InListID);
      IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("attrID", (object) link.AttributeID);
      IDbDataParameter exPar = this.session.DataManager.Parameter("attributableID", (object) this.GetAttributableID(link));
      this.session.DataManager.ExecuteNonQuery($"UPDATE {this.GetAttributeTableName(link)} SET F_INTEGER_VALUE = :int, F_STRING_VALUE = :capt, F_DOUBLE_VALUE=NULL, F_DATE_VALUE=NULL WHERE F_ATTRIBUTE_ID = :attrID AND {this.valueFieldName} = :attributableID AND F_INLIST_ID = :list", intPar, strPar, dbDataParameter2, exPar, dbDataParameter1);
      this.OnAfterUpdate(intPar, exPar, dbDataParameter2, dbDataParameter1);
      this.UpdateViews(this.session.GetAttributeType(link.AttributeID), exPar, intPar, strPar, $"UPDATE {{0}} SET {{1}} WHERE {this.valueFieldName} = :attributableID", this.GetUpdateTables(link));
      this.session.Commit();
      return true;
    }
    catch (Exception ex)
    {
      this.session.Rollback();
      this.addIntoLogFunc(string.Format(LocalizationHolder.rm.GetString("Kernel_299"), (object) this.GetAttributableID(link), (object) ex.Message, (object) link.AttributeID));
      return false;
    }
  }

  private void UpdateViews(
    IDBAttributeType attrType,
    IDbDataParameter exPar,
    IDbDataParameter intPar,
    IDbDataParameter strPar,
    string updateStr,
    string[] tables)
  {
    if (tables == null)
      return;
    string[] fieldNames = attrType.FieldNames;
    foreach (string table in tables)
    {
      List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
      string str = string.Empty;
      if (attrType.AttributeType == FieldTypes.ftInteger)
      {
        str = fieldNames[0] + " = :int";
        dbDataParameterList.Add(intPar);
      }
      else if (attrType.AttributeType == FieldTypes.ftObjectLink)
      {
        str = $"{fieldNames[0]} = :capt, {fieldNames[1]} = :int";
        dbDataParameterList.Add(strPar);
        dbDataParameterList.Add(intPar);
      }
      dbDataParameterList.Add(exPar);
      this.session.DataManager.ExecuteNonQuery(string.Format(updateStr, (object) table, (object) str), dbDataParameterList.ToArray());
    }
  }
}
