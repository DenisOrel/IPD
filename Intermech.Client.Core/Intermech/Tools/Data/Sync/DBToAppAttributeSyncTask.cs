
// Type: Intermech.Tools.Data.Sync.DBToAppAttributeSyncTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using Intermech.Interfaces.Data;
using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует перенос атрибутов из объекта IPS в файла документа.
/// </summary>
public class DBToAppAttributeSyncTask : AppToDBAttributeSyncTaskBase
{
  /// <summary>
  /// Указывает атрибуты, прочитанные из объекта документа в базе IPS. Они будут являться передающей стороной в процессе синхронизации атрибутов.
  /// </summary>
  /// <param name="table">Таблица с атрибутами</param>
  /// <param name="attributableTypeRef">Вспомогательный объект для получения метаданных атрибутов документа в базе IPS</param>
  /// <exception cref="T:System.ArgumentNullException">Не указана таблица с атрибутами</exception>
  public void SetDatabaseAttributes(ValueBag table, IDBAttributableTypeRef attributableTypeRef)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    this.SetSource(table, (IAttributeSyncHelper) new DBAttributeSyncHelper(attributableTypeRef));
  }

  /// <summary>
  /// Указывает атрибуты, прочитанные из файла документа. Они будут являться принимающей стороной в процессе синхронизации атрибутов.
  /// </summary>
  /// <param name="table">Таблица с атрибутами</param>
  /// <param name="isOpenMetadata">Признак открытого формата метаданных у файла документа</param>
  /// <exception cref="T:System.ArgumentNullException">Не указана таблица с атрибутами</exception>
  public void SetApplicationAttributes(ValueBag table, bool isOpenMetadata)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    this.SetTarget(table, (IAttributeSyncHelper) new AppAttributeSyncHelper(isOpenMetadata));
  }

  /// <summary>
  /// Выбирает направление и способ переноса значения для указанного атрибута.
  /// </summary>
  /// <param name="detectData">Сведения об атрибуте и результаты работы метода</param>
  protected override void DoDetectAttributeAction(DetectAttributeSyncActionArgs detectData)
  {
    if (detectData.Action != null)
      return;
    this.DetectDatabaseFlags(detectData.Attribute);
    if (detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsSystemFlag])
      this.DetectSystemAttributeAction(detectData);
    else if (detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsObjectLinkFlag])
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectLinkAction;
    else
      base.DoDetectAttributeAction(detectData);
  }

  private void DetectSystemAttributeAction(DetectAttributeSyncActionArgs detectData)
  {
    if (detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.ObjectType.Text)
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectTypeAction;
    else if (detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.OwnerId.Text || detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.CheckoutById.Text || detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.ProjectId.Text)
    {
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectLinkAction;
    }
    else
    {
      if (!detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsObjectLinkFlag])
        return;
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectLinkAction;
    }
  }
}
