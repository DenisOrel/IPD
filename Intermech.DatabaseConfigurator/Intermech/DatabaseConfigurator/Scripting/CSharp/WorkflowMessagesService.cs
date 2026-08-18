// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.WorkflowMessagesService
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Workflow;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class WorkflowMessagesService
{
  private Lazy<bool> isAvailable;

  public WorkflowMessagesService()
  {
    this.isAvailable = new Lazy<bool>(new Func<bool>(this.TestWorkflowIsAvailable));
  }

  private bool TestWorkflowIsAvailable()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.IsServiceAvailable((object) sessionKeeper.Session, typeof (IRouterService));
  }

  public bool IsAvailable
  {
    [DebuggerStepThrough] get => this.isAvailable.Value;
  }

  private void CheckIfAvailable()
  {
    if (!this.IsAvailable)
      throw new NotSupportedException("Workflow module is not available.");
  }

  public long FindMessageBySubject(string subject)
  {
    if (string.IsNullOrEmpty(subject))
      throw new ArgumentException("Не задана тема сообщения", nameof (subject));
    this.CheckIfAvailable();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(wfConsts.MessageTypeID, new DBRecordSetParams()
      {
        RecordCount = 1,
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        },
        Conditions = new ConditionStructure[2]
        {
          new ConditionStructure(wfConsts.AttrRecipID, RelationalOperators.Equal, (object) sessionKeeper.Session.UserID, LogicalOperators.AND, 0, false),
          new ConditionStructure(-50, RelationalOperators.Equal, (object) subject, LogicalOperators.NONE, 0, false)
        }
      });
      return dataTable.Rows.Count != 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
    }
  }

  public void SendSystemMessage(string subject, string text, ProcessPriority priority)
  {
    if (string.IsNullOrEmpty(subject))
      throw new ArgumentException("Не задана тема сообщения", nameof (subject));
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    this.CheckIfAvailable();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IRouterService>((object) sessionKeeper.Session, true).CreateMessage(sessionKeeper.Session.SessionGUID, sessionKeeper.Session.UserID, subject, text, sessionKeeper.Session.IdentHelper.SystemID).GetAttributeByID(wfConsts.AttrPriorityID).AsInteger = (long) priority;
  }
}
