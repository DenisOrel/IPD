// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.ApproveGraphValueReplaceService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Signs.Interfaces;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Server;

internal class ApproveGraphValueReplaceService : LongLifeObject, IApproveGraphValueReplaceService
{
  private string _notSession = "Замена значений граф подписей для действий 'Утверждение' не выполнена. Системная сессия не получена.";
  private string _notAdmin = "Замену значений граф подписей для действий 'Утверждение' может выполнять только пользователь с правами администратора!";

  public static void Register()
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICustomServices)) is ICustomServices service))
      return;
    service.AddService(typeof (IApproveGraphValueReplaceService), (object) new ApproveGraphValueReplaceService());
  }

  public string ReplaceGraphsInApproveExecutedProcessAndAllSchemes(
    Dictionary<string, string> changedGraphValues,
    Guid currentSessionGuid)
  {
    int num = 0;
    StringBuilder errorBuilder = new StringBuilder();
    IUserSession systemSession = (IUserSession) null;
    try
    {
      if (!UserSession.GetSessionByID(currentSessionGuid).IsAdmin)
        throw new KernelException(this._notAdmin);
      systemSession = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service ? service.GetSystemSessionTemporaryClone("ApproveGraphValueReplaceService.ReplaceGraphsInApproveExecutedProcessAndAllSchemes") : throw new KernelException(this._notSession);
      if (systemSession == null)
        throw new KernelException(this._notSession);
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
      };
      IDBObjectCollection objectCollection1 = systemSession.GetObjectCollection(wfConsts.ProcessesTypeID);
      IDBObjectCollection objectCollection2 = systemSession.GetObjectCollection(wfConsts.ApproveTypeID);
      DBRecordSetParams paramSet1 = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.Equal, (object) 4, LogicalOperators.NONE, 0, false)
      });
      paramSet1.SetColumnDescriptors(columns);
      DataTable dataTable = objectCollection1.Select(paramSet1);
      List<long> longList = new List<long>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long result = -1;
        if (long.TryParse(row.ItemArray[0].ToString(), out result))
          longList.Add(result);
      }
      DBRecordSetParams paramSet2 = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-4, RelationalOperators.NotEqual, (object) wfConsts.ActivityExecLCStepID, LogicalOperators.OR, 0, false),
        new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.ID)
      });
      paramSet2.SetColumnDescriptors(columns);
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection2.Select(paramSet2).Rows)
      {
        long result = -1;
        if (long.TryParse(row.ItemArray[0].ToString(), out result))
          num += this.CheckAndChangeApproveGraphs(changedGraphValues, systemSession, result, errorBuilder);
      }
    }
    catch (Exception ex)
    {
      if (errorBuilder.Length > 0)
        errorBuilder.AppendLine();
      errorBuilder.Append($"В процессе патча граф подписей для действия 'Утверждение' возникла ошибка: {ex.Message}.");
    }
    finally
    {
      systemSession?.Logout("ApproveGraphValueReplaceService.ReplaceGraphsInApproveExecutedProcessAndAllSchemes");
    }
    string str = $"В результате патча граф подписей для всех действий 'Утверждение' было изменено {num} действий.";
    return errorBuilder.Length <= 0 ? str : $"{str} \n В процессе патча произошли ошибки:\n{errorBuilder}";
  }

  public string ReplaceGraphsInAllApprove(
    Dictionary<string, string> changedGraphValues,
    Guid currentSessionGuid)
  {
    int num = 0;
    StringBuilder errorBuilder = new StringBuilder();
    IUserSession systemSession = (IUserSession) null;
    try
    {
      if (!UserSession.GetSessionByID(currentSessionGuid).IsAdmin)
        throw new KernelException(this._notAdmin);
      systemSession = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service ? service.GetSystemSessionTemporaryClone("ApproveGraphValueReplaceService.ReplaceGraphsInAllApprove") : (IUserSession) null;
      if (systemSession == null)
        throw new KernelException(this._notSession);
      foreach (DataRow row in (InternalDataCollectionBase) systemSession.GetObjectCollection(wfConsts.ApproveTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
      })).Rows)
      {
        long result;
        if (long.TryParse(row.ItemArray[0].ToString(), out result))
          num += this.CheckAndChangeApproveGraphs(changedGraphValues, systemSession, result, errorBuilder);
      }
    }
    catch (Exception ex)
    {
      if (errorBuilder.Length > 0)
        errorBuilder.AppendLine();
      errorBuilder.Append($"В процессе патча граф подписей для действия 'Утверждение' возникла ошибка: {ex.Message}.");
    }
    finally
    {
      systemSession?.Logout("ApproveGraphValueReplaceService.ReplaceGraphsInAllApprove");
    }
    string str = $"В результате патча граф подписей для всех действий 'Утверждение' было изменено {num} действий.";
    return errorBuilder.Length <= 0 ? str : $"{str} \n В процессе патча произошли ошибки:\n{errorBuilder}";
  }

  public string ReplaceApproveGraphsByProcess(
    Dictionary<string, string> changedGraphValues,
    long processID,
    Guid currentSessionGuid)
  {
    int num = 0;
    StringBuilder errorBuilder = new StringBuilder();
    IUserSession systemSession = (IUserSession) null;
    try
    {
      if (!UserSession.GetSessionByID(currentSessionGuid).IsAdmin)
        throw new KernelException(this._notAdmin);
      systemSession = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service ? service.GetSystemSessionTemporaryClone("ApproveGraphValueReplaceService.ReplaceApproveGraphsInProcess") : (IUserSession) null;
      if (systemSession == null)
        throw new KernelException(this._notSession);
      if (systemSession.GetObject(processID, false) is IProcess process)
      {
        foreach (IActivity activity in process.Activities)
        {
          if (activity.Kind == ActivityKind.Approve)
            num += this.CheckAndChangeApproveGraphs(changedGraphValues, systemSession, activity.ObjectID, errorBuilder);
        }
        IDBAttribute attributeById = process.GetAttributeByID(wfConsts.AttrCreateActivitiesOnDemandID);
        bool flag = false;
        if (attributeById != null)
          flag = attributeById.AsBoolean;
        long prototypeSchemeId = process.PrototypeSchemeID;
        if (prototypeSchemeId != 0L & flag)
          num += this.PatchScheme(changedGraphValues, prototypeSchemeId, systemSession, errorBuilder);
      }
    }
    catch (Exception ex)
    {
      if (errorBuilder.Length > 0)
        errorBuilder.AppendLine();
      errorBuilder.Append($"В процессе патча граф подписей для действия 'Утверждение' в процессе с идентификатором '{processID}' возникла ошибка: {ex.Message}.");
    }
    finally
    {
      systemSession?.Logout("ApproveGraphValueReplaceService.ReplaceApproveGraphsInProcess");
    }
    string str = $"В результате патча граф подписей для действий 'Утверждение' в процессе с идентификатором '{processID}', и его родительского шаблона, было изменено {num} действий.";
    return errorBuilder.Length <= 0 ? str : $"{str} \n В процессе патча произошли ошибки:\n{errorBuilder}";
  }

  public string ReplaceApproveGraphsByScheme(
    Dictionary<string, string> changedGraphValues,
    long schemeID,
    Guid currentSessionGuid)
  {
    int num = 0;
    StringBuilder errorBuilder = new StringBuilder();
    IUserSession systemSession = (IUserSession) null;
    try
    {
      if (!UserSession.GetSessionByID(currentSessionGuid).IsAdmin)
        throw new KernelException(this._notAdmin);
      systemSession = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service ? service.GetSystemSessionTemporaryClone("ApproveGraphValueReplaceService.ReplaceApproveGraphsInScheme") : (IUserSession) null;
      if (systemSession == null)
        throw new KernelException(this._notSession);
      num = this.PatchScheme(changedGraphValues, schemeID, systemSession, errorBuilder);
    }
    catch (Exception ex)
    {
      if (errorBuilder.Length > 0)
        errorBuilder.AppendLine();
      errorBuilder.Append($"В процессе патча граф подписей для действия 'Утверждение' в шаблоне с идентификатором '{schemeID}' возникла ошибка: {ex.Message}.");
    }
    finally
    {
      systemSession?.Logout("ApproveGraphValueReplaceService.ReplaceApproveGraphsInScheme");
    }
    string str = $"В результате патча граф подписей для действий 'Утверждение' в шаблоне с идентификатором '{schemeID}' было изменено {num} действий.";
    return errorBuilder.Length <= 0 ? str : $"{str} \n В процессе патча произошли ошибки:\n{errorBuilder}";
  }

  private int PatchScheme(
    Dictionary<string, string> changedGraphValues,
    long schemeID,
    IUserSession systemSession,
    StringBuilder errorBuilder)
  {
    int num = 0;
    if (systemSession.GetObject(schemeID, false) is IScheme scheme)
    {
      if (scheme is IProcess)
        throw new KernelException("Ошибка замены значений граф подписей для действий 'Утверждение'. В метод замены в шаблоне, подан процесс!");
      foreach (IActivity activity in scheme.Activities)
      {
        if (activity.Kind == ActivityKind.Approve)
          num += this.CheckAndChangeApproveGraphs(changedGraphValues, systemSession, activity.ObjectID, errorBuilder);
      }
    }
    return num;
  }

  private int CheckAndChangeApproveGraphs(
    Dictionary<string, string> changedGraphValues,
    IUserSession systemSession,
    long approveObjectID,
    StringBuilder errorBuilder)
  {
    int num = 0;
    if (systemSession.GetObject(approveObjectID, false) is Approve approve)
    {
      RequiredSigns requiredSigns = approve.RequiredSigns;
      SignsDataItemModel individualSettingForTypes = approve.IndividualSettingForTypes;
      bool flag1 = false;
      bool flag2 = false;
      GraphsSet graphsSet = new GraphsSet();
      if (requiredSigns != null && !requiredSigns.IsEmpty)
      {
        foreach (string graphs1 in requiredSigns.GraphsSet)
        {
          GraphsCollection graphs2 = requiredSigns.GraphsSet[graphs1];
          if (graphs2 != null)
          {
            GraphsCollection graphsCollection = new GraphsCollection();
            foreach (GraphClass graphClass1 in graphs2)
            {
              string str;
              if (changedGraphValues.TryGetValue(graphClass1.Value, out str))
              {
                GraphClass graphClass2 = new GraphClass(str, graphClass1.StrongCheck, graphClass1.II);
                if (!graphsCollection.Contains(graphClass2))
                  graphsCollection.Add(graphClass2);
                flag1 = true;
                num = 1;
              }
              else
                graphsCollection.Add(graphClass1);
            }
            graphsSet.Add(graphs1, graphsCollection);
          }
        }
      }
      if (individualSettingForTypes != null && individualSettingForTypes.Nodes.Count > 0)
      {
        foreach (SignsDataItem node in (Collection<SignsDataItem>) individualSettingForTypes.Nodes)
        {
          foreach (SignsGroup group in (Collection<SignsGroup>) node.Groups)
          {
            foreach (SignsDataItemChildren child in (Collection<SignsDataItemChildren>) group.Children)
            {
              if (changedGraphValues.ContainsKey(child.GraphForType))
              {
                child.GraphForType = changedGraphValues[child.GraphForType];
                flag2 = true;
                num = 1;
              }
            }
          }
        }
      }
      if (flag1 | flag2)
      {
        if (approve.LCStep != wfConsts.ActivityExecLCStepID)
        {
          if (approve.CheckoutBy == 0L)
          {
            try
            {
              approve = approve.CheckOut(true) as Approve;
            }
            catch (Exception ex)
            {
              if (errorBuilder.Length > 0)
                errorBuilder.AppendLine();
              errorBuilder.Append($"Действие '{approve?.Caption}' с идентификатором '{approve?.ObjectID}' не пропатчено. В процессе патча возникла ошибка: {ex.Message}.");
              return 0;
            }
          }
          else if (approve.CheckoutBy != systemSession.UserID)
          {
            if (errorBuilder.Length > 0)
              errorBuilder.AppendLine();
            QuickObjectInfo objectInfo = systemSession.GetObjectInfo(approve.CheckoutBy);
            string caption = approve.CheckoutBy.ToString();
            if (!objectInfo.Empty)
              caption = objectInfo.Caption;
            errorBuilder.Append($"Действие '{approve.Caption}' с идентификатором '{approve.ObjectID}' взято на редактирование пользователем '{caption}'. Требуется завершить редактирование данного действия и повторить операцию патча граф.");
            return 0;
          }
        }
        try
        {
          if (flag1)
          {
            IDBAttribute byId = approve?.Attributes.FindByID(wfConsts.AttrRequiredSignsID);
            if (byId != null)
            {
              using (MemoryStream destination = new MemoryStream())
              {
                graphsSet.Save((Stream) destination);
                destination.Position = 0L;
                using (StreamReader streamReader = new StreamReader((Stream) destination))
                  byId.Value = (object) streamReader.ReadToEnd();
              }
            }
          }
          if (flag2)
          {
            using (MemoryStream memoryStream = new MemoryStream())
            {
              new XmlSerializer(typeof (SignsDataItemModel)).Serialize((Stream) memoryStream, (object) individualSettingForTypes);
              memoryStream.Position = 0L;
              using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
                approve?.Attributes.AddAttribute(wfConsts.AttrGraphForTypeID, false, new object[1]
                {
                  (object) streamReader.ReadToEnd()
                });
            }
          }
        }
        catch (Exception ex)
        {
          if (errorBuilder.Length > 0)
            errorBuilder.AppendLine();
          errorBuilder.Append($"Действие '{approve.Caption}' с идентификатором '{approve.ObjectID}' не пропатчено. В процессе патча возникла ошибка: {ex.Message}.");
          return 0;
        }
        finally
        {
          int? lcStep = approve?.LCStep;
          int activityExecLcStepId = wfConsts.ActivityExecLCStepID;
          if (!(lcStep.GetValueOrDefault() == activityExecLcStepId & lcStep.HasValue))
          {
            long? checkoutBy = approve?.CheckoutBy;
            long userId = systemSession.UserID;
            if (checkoutBy.GetValueOrDefault() == userId & checkoutBy.HasValue && approve != null)
            {
              // ISSUE: explicit non-virtual call
              __nonvirtual (approve.CheckIn());
            }
          }
        }
      }
    }
    return num;
  }
}
