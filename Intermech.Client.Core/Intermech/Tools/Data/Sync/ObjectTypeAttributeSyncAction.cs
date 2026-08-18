
// Type: Intermech.Tools.Data.Sync.ObjectTypeAttributeSyncAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Data;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует синхронизатор значений для системного атрибута "Тип объекта".
/// </summary>
internal sealed class ObjectTypeAttributeSyncAction : NormalAttributeSyncAction
{
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
    if (sourceItem != null && sourceItem.DataType == typeof (long))
    {
      int objectType = (int) sourceItem.Read<long>(-1L);
      string str = objectType != -1 ? DBHelper.CreateObjectTypeGID(objectType).Name : string.Empty;
      ValueRecord sourceItem1 = new ValueRecord(attribute.Key, (object) str);
      base.DoPerform(taskData, attribute, sourceItem1);
    }
    else if (sourceItem != null && sourceItem.DataType == typeof (string))
    {
      base.DoPerform(taskData, attribute, sourceItem);
    }
    else
    {
      ValueRecord sourceItem2 = new ValueRecord(attribute.Key, (object) string.Empty);
      base.DoPerform(taskData, attribute, sourceItem2);
    }
  }
}
