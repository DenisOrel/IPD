// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Case
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Case(UserSession uSession, DataTable objectsTable) : SystemActivity(uSession, objectsTable)
{
  private ConditionList _expertConditions;
  private List<ExpressionInfo> _expressionConditions;
  private HashSet<WFLink> _nextLinks = new HashSet<WFLink>();
  public bool FilterCheckError;

  public override ActivityKind Kind => ActivityKind.Case;

  public bool UseExpertSystem
  {
    get
    {
      return this.ExtProps.Ini.ReadBoolean("Props", "useExpertSystem", this.ExpertConditions != null && !this.ExpertConditions.IsEmpty);
    }
  }

  public ConditionList ExpertConditions
  {
    get => this._expertConditions ?? (this._expertConditions = new ConditionList((IDBObject) this));
  }

  public void SaveExpertConditions()
  {
    if (this._expertConditions == null)
      return;
    this._expertConditions.Save(this.Attributes.FindByID(wfConsts.AttrConditionID));
  }

  public void SaveExpressionConditions()
  {
    if (this.ExpressionConditions == null)
      return;
    MiscFunx.ExpressionsToAttribute(this.ExpressionConditions, this.Attributes.FindByID(wfConsts.AttrConditionFormulaID));
  }

  public List<ExpressionInfo> ExpressionConditions
  {
    get
    {
      if (this._expressionConditions == null)
        this._expressionConditions = new List<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(this.Attributes.FindByID(wfConsts.AttrConditionFormulaID)));
      return this._expressionConditions;
    }
  }

  public bool FilterObjects
  {
    get
    {
      return MiscFunx.IsFlagSet(this.GetAttributeByID(wfConsts.AttrAddIDID), ActivityFlags.FilterObjects);
    }
  }

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    if (!this.UseExpertSystem)
    {
      bool flag = this.ExpressionConditions != null && this.ExpressionConditions.Count > 0;
      if (flag)
      {
        if (this.FilterObjects)
          return s;
        List<AttributeValues> activityVariableValues = this.GetActivityVariableValues();
        foreach (ExpressionInfo expressionCondition in this.ExpressionConditions)
        {
          if (MiscFunx.VerifyExpression(expressionCondition.FormulaForLink, activityVariableValues.ToArray(), false) is bool)
          {
            flag = true;
          }
          else
          {
            flag = false;
            MiscFunx.AddNewLined(ref s, "Вычисленное выражение не логического типа!");
            break;
          }
        }
      }
      if (!flag)
        MiscFunx.AddNewLined(ref s, MiscFunx.ActivityIncomplete(this.Name));
    }
    else
    {
      bool flag = this.ExpertConditions.Count > sc_22135.ssp_workflow_server_22136(340806247);
      if (flag)
      {
        if (this.FilterObjects)
          return s;
        if (this.UserSession.GetCustomService(typeof (IExpertServer)) is IExpertServer customService)
        {
          int num = customService.StartTask(this.UserSession.SessionGUID);
          try
          {
            long objID = this.ProcessID;
            if (this.ObjectID < 0L && objID > 0L)
              objID = -objID;
            for (int index = 0; index < this.ExpertConditions.Count; ++index)
            {
              if (!MiscFunx.VerifyFormula(customService, num, objID, this.ExpertConditions[index].ExpertFormula))
              {
                flag = false;
                break;
              }
            }
          }
          finally
          {
            customService.EndTask(num);
          }
        }
      }
      if (!flag)
        MiscFunx.AddNewLined(ref s, MiscFunx.ActivityIncomplete(this.Name));
    }
    return s;
  }

  internal override void PrepareActivity()
  {
    base.PrepareActivity();
    List<WFLink> src = this.AllLinksFromThis;
    this._nextLinks.Clear();
    if (!this.UseExpertSystem)
    {
      if (this.ExpressionConditions == null || this.ExpressionConditions.Count == 0)
        throw new NotificationException("Не найдены формулы для расчёта. Продолжение процесса невозможно!");
      List<AttributeValues> activityVariableValues = this.GetActivityVariableValues();
      if (this.FilterObjects)
      {
        if (this.Attachments.Count > 0)
        {
          foreach (Attachment attachment in (List<Attachment>) this.Attachments)
          {
            attachment.Tag = (object) null;
            IDBObject dbObject = this.UserSession.GetObject(attachment.ObjectID, false);
            for (int i = 0; i < src.Count; i++)
            {
              if (src[i].Kind == LinkKind.True && (!this.IsBlockStart || this.ThreadID == 0L || this.ThreadID == src[i].ToID))
              {
                int index = this.ExpressionConditions.FindIndex((Predicate<ExpressionInfo>) (x => x.LinkID == src[i].ObjectID));
                if (index != -1)
                {
                  ExpressionInfo expressionCondition = this.ExpressionConditions[index];
                  object obj = (object) false;
                  List<int> intList = new List<int>(0);
                  if (expressionCondition.ObjectTypeForLink != -1)
                    intList = MetaDataHelper.GetObjectTypeChildrenIDRecursive(expressionCondition.ObjectTypeForLink);
                  if (expressionCondition.ObjectTypeForLink == -1 || intList.Contains(attachment.TypeID))
                  {
                    List<AttributeValues> attributeValuesList = new List<AttributeValues>((IEnumerable<AttributeValues>) activityVariableValues);
                    if (dbObject != null)
                      attributeValuesList.AddRange((IEnumerable<AttributeValues>) dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes));
                    obj = MiscFunx.VerifyExpression(expressionCondition.FormulaForLink, attributeValuesList.ToArray(), false);
                  }
                  if (obj is bool)
                  {
                    if (Convert.ToBoolean(obj))
                    {
                      if (!this._nextLinks.Contains(src[i]))
                        this._nextLinks.Add(src[i]);
                      if (!(attachment.Tag is List<long> longList))
                      {
                        longList = new List<long>();
                        attachment.Tag = (object) longList;
                      }
                      longList.Add(src[i].ToID);
                      this.ModifyAttachmentInCaseActivity = true;
                    }
                  }
                  else
                  {
                    string str1 = "Вычисленное выражение не логического типа!";
                    if (obj is ExpressionVerifyError expressionVerifyError && !string.IsNullOrEmpty(expressionVerifyError.ErrorText))
                      str1 = expressionVerifyError.ErrorText;
                    string str2 = dbObject == null ? attachment.ObjectID.ToString() : $"{dbObject.Caption} ({attachment.ObjectID})";
                    this.DumpError($"Невозможно вычислить условие ({expressionCondition.FormulaForLink}) для объекта '{str2}', результат: {str1}", string.Empty);
                    this.ErrorOccured = true;
                    this.FilterCheckError = true;
                    return;
                  }
                }
              }
            }
          }
          foreach (Attachment attachment in (List<Attachment>) this.Attachments)
          {
            if (attachment.Tag == null)
            {
              List<long> longList = new List<long>();
              attachment.Tag = (object) longList;
              for (int i = 0; i < src.Count; i++)
              {
                int index = this.ExpressionConditions.FindIndex((Predicate<ExpressionInfo>) (x => x.LinkID == src[i].ObjectID));
                if (src[i].Kind == LinkKind.False || index != -1 && this.ExpressionConditions[index].ElseLink && src[i].IsDirect)
                {
                  if (!this._nextLinks.Contains(src[i]))
                    this._nextLinks.Add(src[i]);
                  longList.Add(src[i].ToID);
                  this.ModifyAttachmentInCaseActivity = true;
                }
              }
            }
          }
          if (this._nextLinks.Count == 0)
          {
            WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
            if (parallelBlockLink != null)
            {
              this._nextLinks.Add(parallelBlockLink);
              this.MessageText += "Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
            }
          }
          if (this._nextLinks.Count == 0)
            throw new NotificationException("Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса.");
        }
        else
          this.CheckExpressionCondition(src, activityVariableValues);
      }
      else
        this.CheckExpressionCondition(src, activityVariableValues);
    }
    else
    {
      if (!(this.UserSession.GetCustomService(typeof (IExpertServer)) is IExpertServer customService))
        throw new KernelException("Не найден модуль экспертной системы, выполнение невозможно.");
      int num = customService.StartTask(this.UserSession.SessionGUID);
      try
      {
        object obj = (object) null;
        if (this.FilterObjects)
        {
          foreach (Variable variable in this.VariableList)
            customService.SetParmValue(num, -1L, variable.AttrTypeID, variable.TypedValue);
          if (this.Attachments.Count > 0)
          {
            foreach (Attachment attachment in (List<Attachment>) this.Attachments)
            {
              attachment.Tag = (object) null;
              for (int index = 0; index < src.Count; ++index)
              {
                if (src[index].Kind == LinkKind.True && (!this.IsBlockStart || this.ThreadID == 0L || this.ThreadID == src[index].ToID))
                {
                  ConditionInfo conditionInfo = this.ExpertConditions.Find(src[index].ObjectID);
                  if (conditionInfo != null)
                  {
                    if (!this.CheckExpertResult(customService.CalcFormulaSimpleMode(num, (object) conditionInfo.ExpertFormula, attachment.ObjectID, out obj), conditionInfo.ExpertFormula))
                    {
                      this.ErrorOccured = true;
                      this.FilterCheckError = true;
                      this.ActivityResult = ActivityResult.Back;
                      return;
                    }
                    if (obj is bool && Convert.ToBoolean(obj))
                    {
                      if (!this._nextLinks.Contains(src[index]))
                        this._nextLinks.Add(src[index]);
                      if (!(attachment.Tag is List<long> longList))
                      {
                        longList = new List<long>();
                        attachment.Tag = (object) longList;
                      }
                      longList.Add(src[index].ToID);
                      this.ModifyAttachmentInCaseActivity = true;
                    }
                  }
                }
              }
            }
            foreach (Attachment attachment in (List<Attachment>) this.Attachments)
            {
              if (attachment.Tag == null)
              {
                List<long> longList = new List<long>();
                attachment.Tag = (object) longList;
                for (int index = 0; index < src.Count; ++index)
                {
                  ConditionInfo conditionInfo = this.ExpertConditions.Find(src[index].ObjectID);
                  if (src[index].Kind == LinkKind.False || conditionInfo != null && conditionInfo.ExpertFormula == null && src[index].IsDirect)
                  {
                    if (!this._nextLinks.Contains(src[index]))
                      this._nextLinks.Add(src[index]);
                    longList.Add(src[index].ToID);
                    this.ModifyAttachmentInCaseActivity = true;
                  }
                }
              }
            }
            if (this._nextLinks.Count == 0)
            {
              WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
              if (parallelBlockLink != null)
              {
                this._nextLinks.Add(parallelBlockLink);
                this.MessageText += "Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
              }
            }
            if (this._nextLinks.Count == 0)
              throw new NotificationException("Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса.");
          }
          else
            this.CheckCaseConditions(src, customService, num);
        }
        else
          this.CheckCaseConditions(src, customService, num);
      }
      finally
      {
        if (customService.GetTrace(num))
          MiscFunx.GenerateExpertTrace(customService, num, (IUserSession) this.UserSession);
        customService.EndTask(num);
      }
    }
  }

  private void CheckExpressionCondition(
    List<WFLink> allLinks,
    List<AttributeValues> activityAllAttributeValues)
  {
    for (int i = 0; i < allLinks.Count; i++)
    {
      if (allLinks[i].Kind == LinkKind.True && (!this.IsBlockStart || this.ThreadID == 0L || this.ThreadID == allLinks[i].ToID))
      {
        int index = this.ExpressionConditions.FindIndex((Predicate<ExpressionInfo>) (x => x.LinkID == allLinks[i].ObjectID));
        if (index != -1)
        {
          object obj = MiscFunx.VerifyExpression(this.ExpressionConditions[index].FormulaForLink, activityAllAttributeValues.ToArray(), false);
          if (obj is bool && Convert.ToBoolean(obj) && !this._nextLinks.Contains(allLinks[i]))
            this._nextLinks.Add(allLinks[i]);
        }
      }
    }
    if (this._nextLinks.Count == sc_22135.ssp_workflow_server_22137(240860787))
    {
      for (int index = 0; index < allLinks.Count; ++index)
      {
        if (allLinks[index].Kind == LinkKind.False && !this._nextLinks.Contains(allLinks[index]))
          this._nextLinks.Add(allLinks[index]);
      }
    }
    if (this._nextLinks.Count == 0)
    {
      WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
      if (parallelBlockLink != null)
      {
        this._nextLinks.Add(parallelBlockLink);
        this.MessageText += "Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
      }
    }
    if (this._nextLinks.Count == 0)
      throw new NotificationException("Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса.");
  }

  private void CheckCaseConditions(List<WFLink> src, IExpertServer expert, int taskID)
  {
    for (int index = 0; index < src.Count; ++index)
    {
      if (src[index].Kind == LinkKind.True && (!this.IsBlockStart || this.ThreadID == 0L || this.ThreadID == src[index].ToID))
      {
        ConditionInfo conditionInfo = this.ExpertConditions.Find(src[index].ObjectID);
        if (conditionInfo != null)
        {
          object obj;
          if (!this.CheckExpertResult(expert.CalcFormulaSimpleMode(taskID, (object) conditionInfo.ExpertFormula, this.ObjectID, out obj), conditionInfo.ExpertFormula))
            return;
          if (obj is bool && Convert.ToBoolean(obj) && !this._nextLinks.Contains(src[index]))
            this._nextLinks.Add(src[index]);
        }
      }
    }
    if (this._nextLinks.Count == sc_22135.ssp_workflow_server_22138(1937352251))
    {
      for (int index = 0; index < src.Count; ++index)
      {
        if (src[index].Kind == LinkKind.False && !this._nextLinks.Contains(src[index]))
          this._nextLinks.Add(src[index]);
      }
    }
    if (this._nextLinks.Count == 0)
    {
      WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
      if (parallelBlockLink != null)
      {
        this._nextLinks.Add(parallelBlockLink);
        this.MessageText += "Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
      }
    }
    if (this._nextLinks.Count == 0)
      throw new NotificationException("Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса.");
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
    }
    if (this.NextStepLinks.Count != 0)
      return;
    WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
    if (parallelBlockLink == null)
      return;
    this.AddLinkToNextStep(parallelBlockLink);
    this.MessageText += "Не выполнено ни одно из условий действия, а также не найдено условие 'ИНАЧЕ' для продолжения процесса. Процесс пойдёт по блоку параллельного выполнения.";
  }

  public override void TransferAttachments(WFActivity toAct)
  {
    if (this.FilterObjects && !this.FilterCheckError)
    {
      long num = toAct.ParentActivityID;
      if (num == 0L)
        num = toAct.ObjectID;
      foreach (Attachment attachment1 in (List<Attachment>) this.Attachments)
      {
        if (attachment1.Tag is List<long> tag && tag.Contains(num))
        {
          Attachment attachment2 = new Attachment(attachment1);
          toAct.Attachments.Add(attachment2, false);
        }
      }
      toAct.SaveAttachments();
    }
    else
      base.TransferAttachments(toAct);
  }
}
