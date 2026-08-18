
// Type: Intermech.Tools.Data.Sync.SkipAttributeSyncAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Используется в тех случаях, когда нужно подавить синхронизацию определенного атрибута.
/// </summary>
public sealed class SkipAttributeSyncAction : AttributeSyncAction
{
  private static readonly SkipAttributeSyncAction instance = new SkipAttributeSyncAction();

  /// <summary>Реализует синхронизацию атрибута.</summary>
  /// <param name="taskData">Контейнер с основными данными задачи синхронизации</param>
  /// <param name="attribute">Синхронизируемый атрибут</param>
  /// <param name="sourceItem">Значение атрибута на передающей стороне. Может быть null, если такой атрибут отсутствует у передающей стороны</param>
  /// <exception cref="T:System.InvalidCastException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  /// <exception cref="T:System.FormatException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  /// <exception cref="T:System.NotSupportedException">В процессе синхронизации значения атрибута произошла ошибка</exception>
  protected override void DoPerform(
    AttributeSyncTaskData taskData,
    AttributeSyncUnit attribute,
    ValueRecord sourceItem)
  {
  }

  /// <summary>Возвращает константный экземпляр объекта.</summary>
  public static SkipAttributeSyncAction Instance => SkipAttributeSyncAction.instance;
}
