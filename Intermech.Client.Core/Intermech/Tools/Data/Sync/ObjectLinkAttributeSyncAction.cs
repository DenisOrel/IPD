
// Type: Intermech.Tools.Data.Sync.ObjectLinkAttributeSyncAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Data;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует синхронизатор значений для атрибутов типа "ссылка на объект".
/// </summary>
internal sealed class ObjectLinkAttributeSyncAction : NormalAttributeSyncAction
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
      long objectId = sourceItem.Read<long>(0L);
      string str1;
      switch (objectId)
      {
        case -1:
        case 0:
          str1 = string.Empty;
          break;
        default:
          str1 = DBHelper.GetObjectCaption(objectId);
          break;
      }
      string str2 = str1;
      ValueRecord sourceItem1 = new ValueRecord(attribute.Key, (object) str2);
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
