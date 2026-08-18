
// Type: Intermech.Tools.Data.Sync.AppToDBAttributeSyncTaskBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует базовый класс для задач перенос атрибутов между файлом документа и объектом документа IPS.
/// </summary>
public abstract class AppToDBAttributeSyncTaskBase : AttributeSyncTask
{
  internal static readonly StringKey IsSystemFlag = new StringKey("IsSystem");
  internal static readonly StringKey IsComputableFlag = new StringKey("IsComputable");
  internal static readonly StringKey IsObjectLinkFlag = new StringKey("IsObjectLink");
  internal static readonly ObjectLinkAttributeSyncAction defaultObjectLinkAction = new ObjectLinkAttributeSyncAction();
  internal static readonly ObjectTypeAttributeSyncAction defaultObjectTypeAction = new ObjectTypeAttributeSyncAction();

  internal void DetectDatabaseFlags(AttributeSyncUnit attribute)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((string) attribute.Key, false);
      if (attributeType == null)
        return;
      bool flag = attributeType.AttributeType == FieldTypes.ftSystem;
      attribute.Flags[AppToDBAttributeSyncTaskBase.IsSystemFlag] = flag;
      if (DBAttributeHelper.GetFieldType(attributeType) == FieldTypes.ftObjectLink)
        attribute.Flags[AppToDBAttributeSyncTaskBase.IsObjectLinkFlag] = true;
      else if (attributeType.Computed != ComputeValueModes.NotComputableValue)
      {
        attribute.Flags[AppToDBAttributeSyncTaskBase.IsComputableFlag] = !flag;
      }
      else
      {
        if (!this.IsWritableBySystemOnly(attributeType))
          return;
        attribute.Flags[AppToDBAttributeSyncTaskBase.IsComputableFlag] = true;
      }
    }
  }

  private bool IsWritableBySystemOnly(IDBAttributeType dbAttrType)
  {
    Guid guid = ((IDBGuid) dbAttrType).GUID;
    return guid == AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.ContentModifyDate.Guid || guid == AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.ChangeNumber.Guid;
  }

  internal static class InternalCaches
  {
    private static readonly AppToDBAttributeSyncTaskIDCache idCache = new AppToDBAttributeSyncTaskIDCache(MetadataResolvers.Factory);

    internal static AppToDBAttributeSyncTaskIDCache IDCache
    {
      [DebuggerStepThrough] get => AppToDBAttributeSyncTaskBase.InternalCaches.idCache;
    }
  }
}
