// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.SystemActivity
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class SystemActivity(UserSession uSession, DataTable objectsTable) : WFActivity(uSession, objectsTable)
{
  protected bool CheckExpertResult(ExpertResult res, TempFormula condition)
  {
    return this.CheckExpertResult(res, condition, false);
  }

  protected bool CheckExpertResult(ExpertResult res, TempFormula condition, bool throwException)
  {
    if (res == ExpertResult.OK)
      return true;
    string message = LocalizationHolder.rm.GetString("Workflow.Server_48") + condition.ToString() + LocalizationHolder.rm.GetString("Workflow.Server_49") + res.ToString();
    if (throwException)
      throw new WorkflowException(message);
    this.DumpError(message, string.Empty);
    return false;
  }

  public virtual void ValidateParticipants(ref string s)
  {
  }

  protected List<AttributeValues> GetActivityVariableValues()
  {
    List<AttributeValues> activityVariableValues = new List<AttributeValues>();
    if (this.Process is WFProcess)
    {
      foreach (Variable variable in this.VariableList)
      {
        AttributeValues attributeValues = new AttributeValues(variable.AttrTypeID, variable.TypedValue)
        {
          AttributeName = variable.Name
        };
        activityVariableValues.Add(attributeValues);
      }
      foreach (Variable globalVariable in (VarList) this.GlobalVariables)
      {
        AttributeValues attributeValues = new AttributeValues(globalVariable.AttrTypeID, globalVariable.TypedValue)
        {
          AttributeName = globalVariable.Name
        };
        activityVariableValues.Add(attributeValues);
      }
    }
    else if (this.Process != null)
    {
      long objectID = this.ProcessID;
      if (this.ObjectID < 0L && objectID > 0L)
        objectID = -objectID;
      IDBObject src = this.UserSession.GetObject(objectID);
      VarList varList = new VarList((IUserSession) this.UserSession, false, false);
      varList.Load(src);
      varList.AddSystemVariables(src);
      GlobalVariablesList globalVariablesList = new GlobalVariablesList((IUserSession) this.UserSession, false, false);
      switch (src)
      {
        case IScheme scheme:
          globalVariablesList.Load(scheme);
          break;
        case IActivity activity:
          globalVariablesList.Load(activity.Process);
          break;
      }
      foreach (Variable variable in varList)
      {
        Variable actVariable = variable;
        AttributeValues attributeValues = new AttributeValues(actVariable.AttrTypeID, actVariable.TypedValue)
        {
          AttributeName = actVariable.Name
        };
        if (activityVariableValues.FindIndex((Predicate<AttributeValues>) (x => x.AttributeID == actVariable.AttrTypeID)) == -1)
          activityVariableValues.Add(attributeValues);
      }
      foreach (Variable variable in (VarList) globalVariablesList)
      {
        AttributeValues attributeValues = new AttributeValues(variable.AttrTypeID, variable.TypedValue)
        {
          AttributeName = variable.Name
        };
        activityVariableValues.Add(attributeValues);
      }
    }
    return activityVariableValues;
  }
}
