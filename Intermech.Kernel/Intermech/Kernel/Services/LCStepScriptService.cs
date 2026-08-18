// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.LCStepScriptService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Services;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services;

internal sealed class LCStepScriptService : LongLifeObject, ILCScriptService
{
  private IDBTimedEvents dbTimedEvents;
  private Lazy<Dictionary<Guid, long>> scriptIDCache;
  private static readonly Guid LCScriptTypeId = new Guid("cadd94ff-306c-11d8-b4e9-00304f19f545");
  private static readonly Guid Attr_LCScriptObject = new Guid("cadd9500-306c-11d8-b4e9-00304f19f545");
  private static readonly Guid Attr_ScriptText = new Guid("cad00366-306c-11d8-b4e9-00304f19f545");

  public LCStepScriptService(IDBTimedEvents dBTimedEvents)
  {
    this.dbTimedEvents = dBTimedEvents != null ? dBTimedEvents : throw new ArgumentNullException(nameof (dBTimedEvents));
    this.scriptIDCache = new Lazy<Dictionary<Guid, long>>(new Func<Dictionary<Guid, long>>(this.CreateScriptIDCache));
  }

  public void UpdateCache()
  {
    lock (this)
    {
      if (!this.scriptIDCache.IsValueCreated)
        return;
      this.scriptIDCache.Value.Clear();
      this.FillScriptIDCache(this.scriptIDCache.Value);
    }
  }

  public void ExecuteScript(IDBObject sender, IDBLifecycleStep nextstep, IUserSession session)
  {
    long scriptId = this.GetScriptID(nextstep.Properties.StepGuid);
    if (scriptId == -1L)
      return;
    this.ExecuteScriptInternal(session, scriptId, (object) sender, (object) nextstep, (object) session);
  }

  private string GetScriptCode(IUserSession session, long scriptID)
  {
    IDBObject objectActualCopy = session.GetObjectActualCopy(scriptID, false);
    if (objectActualCopy != null)
    {
      IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(LCStepScriptService.Attr_ScriptText);
      if (attributeByGuid != null)
        return attributeByGuid.Value.ToString();
    }
    return "";
  }

  private void ExecuteScriptInternal(IUserSession session, long scriptID, params object[] list)
  {
    string scriptCode = this.GetScriptCode(session, scriptID);
    if (scriptCode == null || !(scriptCode.Trim() != ""))
      return;
    string message = ScriptExecHelper.IsolatedExecScript(scriptCode, CSharpScriptInvocationOptions.Default, list);
    if (message != "")
      throw new Exception(message);
  }

  private long GetScriptID(Guid lcstep)
  {
    lock (this)
      return this.scriptIDCache.Value.ContainsKey(lcstep) ? this.scriptIDCache.Value[lcstep] : -1L;
  }

  private Dictionary<Guid, long> CreateScriptIDCache()
  {
    Dictionary<Guid, long> cacheInstance = new Dictionary<Guid, long>();
    this.FillScriptIDCache(cacheInstance);
    return cacheInstance;
  }

  private void FillScriptIDCache(Dictionary<Guid, long> cacheInstance)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) LCStepScriptService.Attr_LCScriptObject, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.SetColumnDescriptors(columns);
    IUserSession sessionTemporaryClone = this.dbTimedEvents.GetSystemSessionTemporaryClone(nameof (LCStepScriptService));
    DataTable dataTable;
    try
    {
      dataTable = sessionTemporaryClone.GetObjectCollection(LCStepScriptService.LCScriptTypeId).Select(paramSet);
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (LCStepScriptService));
    }
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      string str1 = Convert.ToString(row[1]);
      char[] chArray = new char[1]{ ';' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (GuidHelper.IsGuid(str2))
          cacheInstance[new Guid(str2)] = int64;
      }
    }
  }
}
