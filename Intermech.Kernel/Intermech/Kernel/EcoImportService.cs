// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EcoImportService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel;

internal class EcoImportService : LongLifeObject, IEcoImportService
{
  private object _syncRoot = new object();
  private Thread _thread;
  private long _progress;
  private int _fetchCount = -2;
  private long _lastKeyValue;
  private List<object> _lastOrderValue;
  private IDBEditingContextsServerService _contextSvc;
  private bool _eof;

  internal static IEcoImportService RegisterService()
  {
    if (!(ServerServices.GetService(typeof (IEcoImportService)) is IEcoImportService serviceInstance))
    {
      serviceInstance = (IEcoImportService) new EcoImportService();
      ServerServices.AddService(typeof (IEcoImportService), (object) serviceInstance);
      ((ICustomServices) ServerServices.GetService(typeof (ICustomServices)))?.AddService(typeof (IEcoImportService), (object) serviceInstance);
    }
    return serviceInstance;
  }

  public bool IsRunning
  {
    get
    {
      lock (this._syncRoot)
        return this._thread != null && this._thread.IsAlive;
    }
  }

  public long Progress
  {
    get
    {
      if (!this.IsRunning)
        throw new KernelExceptionID(373);
      lock (this._syncRoot)
        return this._progress;
    }
  }

  public bool Start()
  {
    if (this.IsRunning)
      throw new KernelExceptionID(372);
    lock (this._syncRoot)
    {
      this._thread = new Thread(new ThreadStart(this.MainThreadMethod));
      this.StartThread(this._thread, "EcoImportService thread");
      return true;
    }
  }

  public bool Stop()
  {
    lock (this._syncRoot)
      this._thread = this._thread != null && this._thread.IsAlive ? (Thread) null : throw new KernelExceptionID(373);
    return true;
  }

  private void StartThread(Thread thread, string name)
  {
    thread.Name = name;
    thread.IsBackground = true;
    thread.Start();
  }

  internal DataTable FetchNext(IUserSession session, int objType)
  {
    if (this._eof)
      return (DataTable) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(objType);
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cadd91f4-306c-11d8-b4e9-00304f19f545"), RelationalOperators.NotEqual, (object) true, LogicalOperators.NONE, 0, true)
    }, columns, recordCount: this._fetchCount);
    paramSet.LastKeyValue = this._lastKeyValue;
    paramSet.LastOrderValue = (object) this._lastOrderValue;
    DataTable dataTable;
    try
    {
      dataTable = objectCollection.Select(paramSet);
    }
    catch
    {
      dataTable = (DataTable) null;
    }
    if (dataTable == null || dataTable.Rows.Count == 0)
    {
      this._eof = true;
      return dataTable;
    }
    this._lastKeyValue = Convert.ToInt64(dataTable.Rows[dataTable.Rows.Count - 1][0]);
    this._lastOrderValue = new List<object>(1);
    this._lastOrderValue.Add((object) this._lastKeyValue);
    this._eof = dataTable.ExtendedProperties.ContainsKey((object) "Eof") && (bool) dataTable.ExtendedProperties[(object) "Eof"];
    if (this._eof)
    {
      this._lastKeyValue = 0L;
      this._lastOrderValue = (List<object>) null;
    }
    return dataTable;
  }

  private void ParseECO(IUserSession session, long ecoID)
  {
    if (!(session.GetObject(ecoID, false) is IDBEditingContextsObject editingContextsObject))
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd91f4-306c-11d8-b4e9-00304f19f545");
    IDBAttribute dbAttribute = editingContextsObject.GetAttributeByID(attributeTypeId);
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(editingContextsObject.ObjectType, attributeTypeId);
    if (attribute4ObjectType == null || dbAttribute != null && DataSetProcessor.GetInt64Value(dbAttribute.Value, Convert.ToInt64(false)) == Convert.ToInt64(true))
      return;
    EditingContextsObjectContainer contextsObjectContainer = editingContextsObject.GetEditingContextsObjectContainer(false, false);
    try
    {
      this._contextSvc.SetEditingContextsObject((object) session, contextsObjectContainer, false, true);
      if (dbAttribute == null && attribute4ObjectType.Required == RequiredModes.Manual)
        dbAttribute = editingContextsObject.Attributes.AddAttribute(attributeTypeId, false);
      if (dbAttribute == null)
        return;
      dbAttribute.Value = (object) true;
    }
    catch
    {
    }
  }

  private void MainThreadMethod()
  {
    try
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
      IUserSession session = (IUserSession) null;
      lock (this._syncRoot)
        this._progress = 0L;
      Dictionary<long, bool> dictionary = new Dictionary<long, bool>();
      try
      {
        this._contextSvc = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
        if (this._contextSvc == null)
          return;
        session = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (EcoImportService));
        if (session == null)
          return;
        for (int index1 = 0; index1 < childrenIdRecursive.Count && this._thread != null; ++index1)
        {
          this._lastKeyValue = 0L;
          this._lastOrderValue = (List<object>) null;
          this._eof = false;
          while (!this._eof)
          {
            if (this._thread == null)
              return;
            DataTable dataTable = this.FetchNext(session, childrenIdRecursive[index1]);
            if (dataTable != null)
            {
              int index2 = 0;
              while (true)
              {
                if (index2 < dataTable.Rows.Count && this._thread != null)
                {
                  long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index2], 0, 0L);
                  if (int64Value != 0L && !dictionary.ContainsKey(int64Value))
                  {
                    this.ParseECO(session, int64Value);
                    dictionary[int64Value] = true;
                    if (int64Value < 0L)
                      dictionary[-int64Value] = true;
                    lock (this._syncRoot)
                      ++this._progress;
                  }
                  ++index2;
                }
                else
                  goto label_25;
              }
            }
            else
              continue;
label_25:;
          }
        }
      }
      finally
      {
        session?.Logout(nameof (EcoImportService));
      }
    }
    catch
    {
    }
    finally
    {
      this._thread = (Thread) null;
    }
  }
}
