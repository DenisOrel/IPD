
// Type: Intermech.Tools.Data.Sync.AttributeSyncTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует задачу переноса атрибутов из одной системы в другую. Для каждого атрибута задача выбирает очередность, направление и способ переноса значения,
/// а затем выполняет перенос за один или более шагов.
/// </summary>
public class AttributeSyncTask : AttributeSyncTaskData
{
  private const int AttributeCountHeuristic = 16 /*0x10*/;
  private readonly List<AttributeSyncUnit> attributes;
  private readonly List<AttributeSyncDefect> defects;

  /// <summary>Создает объект.</summary>
  public AttributeSyncTask()
  {
    this.attributes = new List<AttributeSyncUnit>(16 /*0x10*/);
    this.defects = new List<AttributeSyncDefect>(16 /*0x10*/);
  }

  /// <summary>
  /// Возвращает список атрибутов, которые должны быть перенесены.
  /// </summary>
  public List<AttributeSyncUnit> Attributes => this.attributes;

  /// <summary>
  /// Возвращает коллекцию дефектов переноса атрибутов, выявленных при последнем выполнении задачи.
  /// </summary>
  public List<AttributeSyncDefect> Defects => this.defects;

  /// <summary>
  /// Событие по выбору направления и способа переноса атрибута из одной системы в другую.
  /// </summary>
  public event EventHandler<DetectAttributeSyncActionArgs> OnDetectAttributeAction;

  /// <summary>
  /// Заполняет список переносимых атрибутов, помещая в него все атрибуты из таблицы передающей стороны.
  /// Этот метод используется в тех случаях, когда вся таблица атрибутов передающей стороны должны быть
  /// отражена в принимающую тблицу.
  /// </summary>
  /// <param name="throwSetException">Значение флага, задающего обработку неудач при сохранении измененных значений атрибутов</param>
  /// <exception cref="T:System.InvalidOperationException">Таблица атрибутов передающей стороны не задана</exception>
  public void AddAllAttributesToSync(bool throwSetException)
  {
    if (this.SourceTable == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1640"));
    this.Attributes.Clear();
    this.Attributes.AddRange((IEnumerable<AttributeSyncUnit>) this.SourceTable.ConvertAll<AttributeSyncUnit>((Converter<ValueRecord, AttributeSyncUnit>) (item => new AttributeSyncUnit(item.Key, throwSetException))));
  }

  /// <summary>
  /// Находит указанный атрибут в списке переносимых атрибутов.
  /// </summary>
  /// <param name="attributeKey">Имя атрибута</param>
  /// <returns>Описатель переносимого атрибута или null</returns>
  public AttributeSyncUnit FindAttribute(StringKey attributeKey)
  {
    if (attributeKey == (StringKey) null)
      throw new ArgumentNullException(nameof (attributeKey));
    return this.Attributes.Find((Predicate<AttributeSyncUnit>) (item => item.Key == attributeKey));
  }

  /// <summary>
  /// Реализует заполнение всех полей данных текущего объекта, копируя их у указанного объекта.
  /// </summary>
  /// <param name="sourceObject">Объект, чьи поля следует скопировать</param>
  protected override void DoAssign(AttributeSyncTaskData sourceObject)
  {
    base.DoAssign(sourceObject);
    this.Attributes.Clear();
    this.Defects.Clear();
    if (!(sourceObject is AttributeSyncTask attributeSyncTask))
      return;
    this.Attributes.AddRange((IEnumerable<AttributeSyncUnit>) attributeSyncTask.Attributes);
  }

  /// <summary>Проверяет корректность исходных параметров задачи.</summary>
  /// <exception cref="T:System.InvalidOperationException">Исходные параметры задачи заданы неверно</exception>
  public override void ValidateParameters()
  {
    base.ValidateParameters();
    if (this.Attributes.Contains((AttributeSyncUnit) null))
      throw new InvalidOperationException("Список атрибутов для переноса не может содержать null.");
  }

  /// <summary>
  /// Выполняет подготовку и перенос атрибутов из одной системы в другую за один или более шагов. Для каждого атрибута задача предварительно
  /// выбирает очередность, направление и способ переноса значения.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Исходные параметры переноса заданы неверно</exception>
  public void Run()
  {
    this.ClearResult();
    this.ValidateParameters();
    if (this.Attributes.Count == 0)
      return;
    List<AttributeSyncUnit> attributes1 = new List<AttributeSyncUnit>(this.Attributes.Count);
    List<AttributeSyncAction> actions1 = new List<AttributeSyncAction>(this.Attributes.Count);
    List<AttributeSyncUnit> attributes2 = new List<AttributeSyncUnit>(this.Attributes.Count);
    List<AttributeSyncAction> actions2 = new List<AttributeSyncAction>(this.Attributes.Count);
    foreach (AttributeSyncUnit attribute in this.Attributes)
    {
      DetectAttributeSyncActionArgs attributeSyncActionArgs = this.DetectAttributeAction((AttributeSyncTaskData) this, attribute);
      switch (attributeSyncActionArgs.Direction)
      {
        case SyncDirection.Forward:
          attributes1.Add(attribute);
          actions1.Add(attributeSyncActionArgs.Action);
          continue;
        case SyncDirection.Backward:
          attributes2.Add(attribute);
          actions2.Add(attributeSyncActionArgs.Action);
          continue;
        default:
          continue;
      }
    }
    if (UIReport.Enabled)
    {
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_1628"), (object) this.EntityDisplayName));
      UIReport.Indent();
      if (attributes1.Count != 0)
        UIReport.ReportItem(new UIReportItem()
        {
          TraceLevel = TraceLevel.Verbose,
          Text = $"В направлении {this.SourceSyncHelper.ParticipantName}->{this.TargetSyncHelper.ParticipantName}:",
          Data = (object[]) attributes1.ToArray()
        });
      if (attributes2.Count != 0)
        UIReport.ReportItem(new UIReportItem()
        {
          TraceLevel = TraceLevel.Verbose,
          Text = $"В направлении {this.TargetSyncHelper.ParticipantName}->{this.SourceSyncHelper.ParticipantName}:",
          Data = (object[]) attributes2.ToArray()
        });
      UIReport.Unindent();
    }
    this.Defects.Capacity = Math.Max(this.Defects.Capacity, this.Attributes.Count);
    if (attributes1.Count != 0)
    {
      AttributeSyncTask taskData = new AttributeSyncTask();
      taskData.Assign((AttributeSyncTaskData) this);
      this.OnBeforeForwardRun((AttributeSyncTaskData) taskData, (IEnumerable<AttributeSyncUnit>) attributes1);
      this.RunActions((AttributeSyncTaskData) taskData, (IList<AttributeSyncUnit>) attributes1, (IList<AttributeSyncAction>) actions1);
    }
    if (attributes2.Count == 0)
      return;
    AttributeSyncTaskData taskData1 = new AttributeSyncTaskData();
    taskData1.Assign((AttributeSyncTaskData) this);
    taskData1.SwapSides();
    this.OnBeforeBackwardRun(taskData1, (IEnumerable<AttributeSyncUnit>) attributes2);
    this.RunActions(taskData1, (IList<AttributeSyncUnit>) attributes2, (IList<AttributeSyncAction>) actions2);
  }

  /// <summary>
  /// Вызывается непосредственно перед переносом атрибутов в прямом направлении. Метод вызывается только в том случае, если есть атрибуты, требующие переноса.
  /// </summary>
  /// <param name="taskData">Данные систем, участвующих в переносе атрибутов</param>
  /// <param name="attributes">Список атрибутов, которые будут перенесены</param>
  protected virtual void OnBeforeForwardRun(
    AttributeSyncTaskData taskData,
    IEnumerable<AttributeSyncUnit> attributes)
  {
  }

  /// <summary>
  /// Вызывается непосредственно перед переносом атрибутов в обратном направлении. Метод вызывается только в том случае, если есть атрибуты, требующие переноса.
  /// </summary>
  /// <param name="taskData">Данные систем, участвующих в переносе атрибутов. Принимающая и передающая сторона переставлены местами</param>
  /// <param name="attributes">Список атрибутов, которые будут перенесены</param>
  protected virtual void OnBeforeBackwardRun(
    AttributeSyncTaskData taskData,
    IEnumerable<AttributeSyncUnit> attributes)
  {
  }

  private void ClearResult() => this.Defects.Clear();

  private void RunActions(
    AttributeSyncTaskData taskData,
    IList<AttributeSyncUnit> attributes,
    IList<AttributeSyncAction> actions)
  {
    for (int index = 0; index < attributes.Count; ++index)
    {
      try
      {
        actions[index].Perform(taskData, attributes[index]);
      }
      catch (Exception ex)
      {
        if (!AttributeSyncTask.IsActionException(ex))
          throw;
        this.Defects.Add(new AttributeSyncDefect(attributes[index].Key, ex.Message));
      }
    }
  }

  private static bool IsActionException(Exception x)
  {
    switch (x)
    {
      case InvalidCastException _:
      case FormatException _:
        return true;
      default:
        return x is NotSupportedException;
    }
  }

  /// <summary>
  /// Выполняет подготовку и перенос атрибутов из одной системы в другую за один или более шагов. Для каждого атрибута задача предварительно
  /// выбирает очередность, направление и способ переноса значения. После завершения переноса выполняется проверка его корректности.
  /// Если имеются дефекты переноса, то метод сбрасывает исключение.
  /// </summary>
  /// <param name="criticalErrorsOnly">Признак, указывающий, что исключение должно сбрасываться только при критических ошибках переноса атрибутов</param>
  /// <exception cref="T:System.InvalidOperationException">Исходные параметры переноса заданы неверно</exception>
  /// <exception cref="T:Intermech.FaultException">При переносе атрибутов произошла ошибка</exception>
  public void RunChecked(bool criticalErrorsOnly = true)
  {
    this.Run();
    if (this.Defects.Count == 0)
      return;
    this.CheckLastRun(criticalErrorsOnly);
  }

  private DetectAttributeSyncActionArgs DetectAttributeAction(
    AttributeSyncTaskData taskData,
    AttributeSyncUnit attribute)
  {
    DetectAttributeSyncActionArgs attributeSyncActionArgs = new DetectAttributeSyncActionArgs(taskData, attribute);
    if (this.OnDetectAttributeAction != null)
      this.OnDetectAttributeAction((object) this, attributeSyncActionArgs);
    this.DoDetectAttributeAction(attributeSyncActionArgs);
    if (attributeSyncActionArgs.Action == null)
      attributeSyncActionArgs.Action = (AttributeSyncAction) NormalAttributeSyncAction.Instance;
    return attributeSyncActionArgs;
  }

  /// <summary>
  /// Выбирает направление и способ переноса значения для указанного атрибута.
  /// </summary>
  /// <param name="detectData">Сведения об атрибуте и результаты работы метода</param>
  protected virtual void DoDetectAttributeAction(DetectAttributeSyncActionArgs detectData)
  {
  }

  /// <summary>
  /// Проверяет дефекты последнего выполнения задачи на наличие критических ошибок. Если такие дефекты имеются, то метод сбрасывает исключение.
  /// </summary>
  /// <param name="criticalErrorsOnly">Признак, указывающий, что исключение должно сбрасываться только при критических ошибках переноса атрибутов</param>
  /// <exception cref="T:Intermech.FaultException">При переносе атрибутов произошла ошибка</exception>
  public void CheckLastRun(bool criticalErrorsOnly)
  {
    if (this.Defects.Count == 0)
      return;
    bool flag = false;
    foreach (AttributeSyncDefect defect1 in this.Defects)
    {
      AttributeSyncDefect defect = defect1;
      AttributeSyncUnit attributeSyncUnit = CollectionUtils.Find<AttributeSyncUnit>((IEnumerable<AttributeSyncUnit>) this.Attributes, (Predicate<AttributeSyncUnit>) (attr => attr.Key == defect.AttributeKey));
      if (!criticalErrorsOnly)
      {
        flag = true;
        break;
      }
      if (attributeSyncUnit.ThrowSetException)
      {
        flag = ((flag ? 1 : 0) | 1) != 0;
        break;
      }
    }
    if (flag)
      throw new FaultException(this.GetLastRunErrorMessage(flag & criticalErrorsOnly));
  }

  private string GetLastRunErrorMessage(bool hasCriticalErrors)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("SR_1634"), (object) this.EntityDisplayName));
    stringBuilder.AppendLine(LocalizationHolder.rm.GetString("SR_1635"));
    int num = 1;
    foreach (AttributeSyncDefect defect in this.Defects)
      stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("SR_1636"), (object) num++, (object) defect.AttributeKey, (object) defect.DefectDetails));
    stringBuilder.AppendLine();
    if (hasCriticalErrors)
      stringBuilder.AppendLine(LocalizationHolder.rm.GetString("SR_1637"));
    else
      stringBuilder.AppendLine(LocalizationHolder.rm.GetString("SR_1638"));
    return stringBuilder.ToString();
  }
}
