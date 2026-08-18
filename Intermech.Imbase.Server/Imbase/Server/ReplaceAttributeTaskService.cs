// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ReplaceAttributeTaskService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ReplaceAttributeTaskService : 
  BackgroundTaskService,
  IReplaceAttributeTaskService,
  IServiceForBackgroundTask
{
  protected override void StartProcess(Guid taskGuid, object inputData)
  {
    if (!(inputData is Tuple<IMSAttributeType, IMSAttributeType, long[]> tuple))
      return;
    BaseTaskForBackgroundTaskService task = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((System.Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (task == null)
      return;
    task.Running = true;
    UserSession session = (UserSession) null;
    try
    {
      session = this.GetSystemSession();
      IMSAttributeType imsAttributeType1 = tuple.Item1;
      IMSAttributeType imsAttributeType2 = tuple.Item2;
      long[] numArray = tuple.Item3;
      int length = numArray.Length;
      task.CountElements = length;
      for (int index = 0; index < length; ++index)
      {
        if (this.IsProcessStoped(task))
          throw new ReplaceAttributeTaskService.StopTaskException();
        long tableID = numArray[index];
        try
        {
          this.ReplaceAttribute((IUserSession) session, tableID, imsAttributeType1.AttributeGuid.ToString(), imsAttributeType2.AttributeGuid.ToString());
        }
        catch (Exception ex)
        {
          task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
        }
        task.Next();
      }
    }
    catch (ReplaceAttributeTaskService.StopTaskException ex)
    {
      task.Result.Messages.Add(new BackgroundTaskMessage(LocalizationHolder.rm.GetString("Imbase_Task_Stop")));
    }
    catch (Exception ex)
    {
      task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
    }
    finally
    {
      session?.Logout("Imbase.ReplaceAttribute.Service");
      task.Stopped = true;
    }
  }

  private UserSession GetSystemSession()
  {
    return (ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionTemporaryClone("Imbase.ReplaceAttribute.Service") ?? throw new Exception(LocalizationHolder.rm.GetString("Imbase_NullSession"))) as UserSession;
  }

  private void ReplaceAttribute(
    IUserSession session,
    long tableID,
    string strOldAttrGuid,
    string strNewAttrGuid)
  {
    IDBObject tableObject = session.GetObjectActualCopy(tableID, false);
    if (tableObject == null)
      return;
    if (tableObject.ObjectModifyMode == ObjectModifyModes.CantModify)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Table_CantModifyMode"), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    if (tableObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Table_CreateVersionMode"), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    bool flag = false;
    if (tableObject.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      if (tableObject.CheckoutBy == 0L)
      {
        tableObject = tableObject.CheckOut();
        flag = true;
      }
      else if (tableObject.CheckoutBy != session.UserID)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Table_CheckOutOtherUser"), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    }
    DataSet tablesInternal = TableLoadHelper.GetTablesInternal(tableObject);
    if (tablesInternal == null || !tablesInternal.Tables.Contains("IMS_DATA") || !tablesInternal.Tables.Contains("IMS_ATTR_TYPES"))
      return;
    DataTable table1 = tablesInternal.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = tablesInternal.Tables["IMS_DATA"];
    if (table1.AsEnumerable().Any<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strOldAttrGuid)) & table1.AsEnumerable().Any<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strNewAttrGuid)))
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("CantReplaceAttribute"), (object) MetaDataHelper.GetAttributeTypeName(new Guid(strOldAttrGuid)), (object) MetaDataHelper.GetAttributeTypeName(new Guid(strNewAttrGuid)), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
    {
      if (Convert.ToString(row["F_ATTRIBUTE_GUID"]) == strOldAttrGuid)
      {
        row["F_ATTRIBUTE_GUID"] = (object) strNewAttrGuid;
        if (table2.Columns.Contains(strOldAttrGuid))
          table2.Columns[strOldAttrGuid].ColumnName = strNewAttrGuid;
      }
      else
      {
        string str1 = Convert.ToString(row["F_FORMULA"]);
        if (!string.IsNullOrEmpty(str1))
        {
          string str2 = str1.Replace(strOldAttrGuid, strNewAttrGuid);
          if (!(str2 == str1))
            row["F_FORMULA"] = (object) str2;
        }
      }
    }
    tablesInternal.AcceptChanges();
    TableLoadHelper.StoreData(session, tableID, tablesInternal, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
    if (!flag)
      return;
    tableObject.CheckIn();
  }

  private bool IsProcessStoped(BaseTaskForBackgroundTaskService task)
  {
    while (task.Paused && !task.Stopped)
      Thread.Sleep(1000);
    return task.Stopped;
  }

  private class StopTaskException : ApplicationException
  {
  }
}
