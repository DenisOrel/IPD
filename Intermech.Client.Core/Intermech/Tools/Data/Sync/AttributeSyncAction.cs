
// Type: Intermech.Tools.Data.Sync.AttributeSyncAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует базовый класс для синхронизаторов отдельных значений атрибутов.
/// </summary>
public abstract class AttributeSyncAction
{
  /// <summary>Выполняет синхронизацию атрибута.</summary>
  /// <param name="taskData">Контейнер с основными данными задачи синхронизации</param>
  /// <param name="attribute">Синхронизируемый атрибут</param>
  /// <exception cref="T:System.ArgumentNullException">Один из аргументов метода не указан</exception>
  /// <exception cref="T:System.InvalidCastException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  /// <exception cref="T:System.FormatException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  /// <exception cref="T:System.NotSupportedException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  public void Perform(AttributeSyncTaskData taskData, AttributeSyncUnit attribute)
  {
    if (taskData == null)
      throw new ArgumentNullException(nameof (taskData));
    if (attribute == null)
      throw new ArgumentNullException(nameof (attribute));
    ValueRecord sourceItem = taskData.SourceTable.Find(attribute.Key);
    this.DoPerform(taskData, attribute, sourceItem);
  }

  /// <summary>Реализует синхронизацию атрибута.</summary>
  /// <param name="taskData">Контейнер с основными данными задачи синхронизации</param>
  /// <param name="attribute">Синхронизируемый атрибут</param>
  /// <param name="sourceItem">Значение атрибута на передающей стороне. Может быть null, если такой атрибут отсутствует у передающей стороны</param>
  /// <exception cref="T:System.InvalidCastException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  /// <exception cref="T:System.FormatException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  /// <exception cref="T:System.NotSupportedException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  protected abstract void DoPerform(
    AttributeSyncTaskData taskData,
    AttributeSyncUnit attribute,
    ValueRecord sourceItem);

  protected ValueRecord SyncToNewTarget(
    AttributeSyncTaskData taskData,
    AttributeSyncUnit attribute,
    ValueRecord sourceItem)
  {
    ValueRecord omittedValue = taskData.TargetSyncHelper.GetOmittedValue(attribute.Key);
    if (omittedValue != null)
    {
      object compatibleValue = taskData.TargetSyncHelper.GetCompatibleValue(omittedValue, sourceItem);
      if (!ValueRecord.IsNullValue(compatibleValue) && !object.Equals(compatibleValue, (object) string.Empty))
      {
        if (UIReport.Enabled)
        {
          UIReport.Indent();
          UIReport.ReportEvent($"{string.Format(LocalizationHolder.rm.GetString("SR_1629"), (object) attribute.Key)} ==> {string.Format(LocalizationHolder.rm.GetString("SR_1630"), compatibleValue)}");
          UIReport.Unindent();
        }
        omittedValue.Value = compatibleValue;
        omittedValue.Flags[NamedFlags.ThrowSetException] = attribute.ThrowSetException;
        return omittedValue;
      }
    }
    return (ValueRecord) null;
  }

  protected void SyncToExistingTarget(
    AttributeSyncTaskData taskData,
    AttributeSyncUnit attribute,
    ValueRecord sourceItem,
    ValueRecord targetItem)
  {
    object obj = targetItem.Value;
    object compatibleValue = taskData.TargetSyncHelper.GetCompatibleValue(targetItem, sourceItem);
    if (object.Equals(this.RoundValue(taskData, attribute, obj), this.RoundValue(taskData, attribute, compatibleValue)))
    {
      if (!UIReport.Enabled)
        return;
      UIReport.Indent();
      UIReport.ReportEvent($"{string.Format(LocalizationHolder.rm.GetString("SR_1629"), (object) attribute.Key)} ==> {LocalizationHolder.rm.GetString("SR_1631")}");
      UIReport.Unindent();
    }
    else
    {
      if (UIReport.Enabled)
      {
        UIReport.Indent();
        UIReport.ReportEvent($"{string.Format(LocalizationHolder.rm.GetString("SR_1629"), (object) attribute.Key)} ==> {string.Format(LocalizationHolder.rm.GetString("SR_1632"), compatibleValue, obj)}");
        UIReport.Unindent();
      }
      if (targetItem.Flags[NamedFlags.ReadOnly])
      {
        string str = string.Format(LocalizationHolder.rm.GetString("SR_1633"), (object) attribute.Key);
        if (UIReport.Enabled)
        {
          UIReport.Indent();
          UIReport.ReportEvent(str, TraceLevel.Warning);
          UIReport.Unindent();
        }
        throw new NotSupportedException(str);
      }
      targetItem.Value = compatibleValue;
      targetItem.Flags[NamedFlags.ThrowSetException] = attribute.ThrowSetException;
    }
  }

  private object RoundValue(
    AttributeSyncTaskData taskData,
    AttributeSyncUnit attribute,
    object value)
  {
    AttributeSyncOptions options = taskData.Options;
    if (ValueRecord.IsNullValue(value) || object.Equals(value, (object) string.Empty))
      return (object) null;
    switch (value)
    {
      case string _:
        string str = ((string) value).Trim();
        if (attribute.CaseInsensitive)
          str = str.ToLower();
        return !str.Equals(string.Empty) ? (object) str : (object) null;
      case MeasuredValue _:
        return (object) ((MeasuredValue) value).Caption.Replace('.', ',');
      case DateTime _ when options.TruncTimeToSeconds:
        long ticks = ((DateTime) value).Ticks;
        return (object) new DateTime(ticks - ticks % 10000000L);
      case double num:
        return (object) Math.Round(num, options.SignificantDigits);
      case float _:
        return (object) (float) Math.Round((double) value, options.SignificantDigits);
      case Decimal d:
        return (object) Math.Round(d, options.SignificantDigits);
      default:
        return value;
    }
  }
}
