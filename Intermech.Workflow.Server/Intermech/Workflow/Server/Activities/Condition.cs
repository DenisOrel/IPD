// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Condition
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

public class Condition(UserSession uSession, DataTable objectsTable) : SystemActivity(uSession, objectsTable)
{
  private TempFormula _expertCondition;
  private ExpressionInfo _expressionCondition;
  private bool _condResult;
  private HashSet<WFLink> _nextLinks = new HashSet<WFLink>();

  public override ActivityKind Kind => ActivityKind.Condition;

  public bool UseExpertSystem
  {
    get => this.ExtProps.Ini.ReadBoolean("Props", "useExpertSystem", this.ExpertCondition != null);
  }

  public TempFormula ExpertCondition
  {
    get
    {
      if (this._expertCondition == null)
        this._expertCondition = MiscFunx.FormulaFromAttribute(this.Attributes.FindByID(wfConsts.AttrConditionID));
      return this._expertCondition;
    }
  }

  public ExpressionInfo ExpressionCondition
  {
    get
    {
      if (this._expressionCondition == null)
        this._expressionCondition = MiscFunx.GetExpressionFromAttr(this.Attributes.FindByID(wfConsts.AttrConditionFormulaID));
      return this._expressionCondition;
    }
  }

  internal override void PrepareActivity()
  {
    base.PrepareActivity();
    this._nextLinks.Clear();
    if (!this.UseExpertSystem)
    {
      object obj = MiscFunx.VerifyExpression(this.ExpressionCondition.FormulaForLink, this.GetActivityVariableValues().ToArray(), false);
      this.ActivityResult = ActivityResult.Next;
      this._condResult = obj is bool ? Convert.ToBoolean(obj) : throw new WorkflowException($"{LocalizationHolder.rm.GetString("Workflow.Server_48")}{this.ExpressionCondition.FormulaForLink}{LocalizationHolder.rm.GetString("Workflow.Server_49")}Вычисленное выражение не логического типа!");
    }
    else
    {
      if (!(this.UserSession.GetCustomService(typeof (IExpertServer)) is IExpertServer customService))
        throw new KernelException("Не найден модуль экспертной системы, выполнение невозможно.");
      int num = customService.StartTask(this.UserSession.SessionGUID);
      try
      {
        object obj = (object) null;
        if (this.CheckExpertResult(customService.CalcFormulaSimpleMode(num, (object) this.ExpertCondition, this.ObjectID, out obj), this.ExpertCondition, true))
        {
          this.ActivityResult = ActivityResult.Next;
          this._condResult = obj is bool && Convert.ToBoolean(obj);
        }
      }
      finally
      {
        if (customService.GetTrace(num))
          MiscFunx.GenerateExpertTrace(customService, num, (IUserSession) this.UserSession);
        customService.EndTask(num);
      }
    }
    LinkKind linkKind = LinkKind.True;
    if (!this._condResult)
      linkKind = LinkKind.False;
    foreach (WFLink allLinksFromThi in this.AllLinksFromThis)
    {
      if ((allLinksFromThi.Kind == linkKind || allLinksFromThi.Kind == LinkKind.Forward) && (!this.IsBlockStart || this.ThreadID == 0L || this.ThreadID == allLinksFromThi.ToID) && !this._nextLinks.Contains(allLinksFromThi))
        this._nextLinks.Add(allLinksFromThi);
    }
    if (this._nextLinks.Count == 0)
    {
      WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
      if (parallelBlockLink != null)
      {
        this._nextLinks.Add(parallelBlockLink);
        this.MessageText += "Не выполнено условие действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
      }
    }
    if (this._nextLinks.Count == 0)
      throw new NotificationException("Не выполнено условие действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса.");
  }

  internal override void PrepareNextStepLinks()
  {
    if (this.ErrorOccured)
    {
      base.PrepareNextStepLinks();
    }
    else
    {
      foreach (WFLink nextLink in this._nextLinks)
        this.AddLinkToNextStep(nextLink);
      this.ActivityResult = this._condResult ? ActivityResult.Next : ActivityResult.Back;
    }
    if (this.NextStepLinks.Count != 0)
      return;
    WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
    if (parallelBlockLink == null)
      return;
    this.AddLinkToNextStep(parallelBlockLink);
    this.MessageText += "Не выполнено условие действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
  }

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    bool flag;
    if (!this.UseExpertSystem)
    {
      flag = this.ExpressionCondition != null && !string.IsNullOrEmpty(this.ExpressionCondition.FormulaForLink);
      if (flag)
      {
        if (MiscFunx.VerifyExpression(this.ExpressionCondition.FormulaForLink, this.GetActivityVariableValues().ToArray(), false) is bool)
        {
          flag = true;
        }
        else
        {
          flag = false;
          MiscFunx.AddNewLined(ref s, "Вычисленное выражение не логического типа!");
        }
      }
    }
    else
    {
      flag = this.ExpertCondition.Count > 0;
      if (flag)
      {
        IExpertServer customService = this.UserSession.GetCustomService(typeof (IExpertServer)) as IExpertServer;
        int num = customService.StartTask(this.UserSession.SessionGUID);
        try
        {
          long objID = this.ProcessID;
          if (this.ObjectID < 0L && objID > 0L)
            objID = -objID;
          flag = MiscFunx.VerifyFormula(customService, num, objID, this.ExpertCondition);
        }
        finally
        {
          customService.EndTask(num);
        }
      }
    }
    if (!flag)
      MiscFunx.AddNewLined(ref s, MiscFunx.ActivityIncomplete(this.Name));
    return s;
  }
}
