// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CObjectTypeInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Коллекция с информацией о типе объектов</summary>
internal class CObjectTypeInfoCollection(
  MetadataInfoParentContext serviceContext,
  object parentID,
  bool filtering) : MetadataInfoCollection(serviceContext, parentID, filtering), IDBObjectTypeInfoCollection, IDBCollection
{
  protected override string DBKeyField
  {
    [DebuggerStepThrough] get => "F_OBJECT_TYPE";
  }

  protected override string DBTableName
  {
    [DebuggerStepThrough] get => "IMS_OBJECT_TYPES";
  }

  protected override string GetParentSQL()
  {
    int[] visibleIDs = (int[]) null;
    if (this.Filtering)
      visibleIDs = this.ServiceContext.ClientCache.GetVisibleList(4);
    return ObjectTypesCacheHelper.GetParentSQL(this.ServiceContext.ClientCache.GetTable("IMS_OBJTYPES_TREE"), (int) this.ParentID, visibleIDs);
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    return ObjectTypesCacheHelper.AddInfoToTable(this.ServiceContext.ClientCache.CacheDataSet, base.Select(orderBy, addInfo), addInfo);
  }

  /// <summary>
  /// Метод проверяет имеет ли право данный юзер просматривать список объектов того типа, для которого создана данная коллекция.
  /// </summary>
  public bool CanViewObjects()
  {
    if (CObjectTypeCollection.DisabledViewObjectTypes != null)
      return CObjectTypeCollection.DisabledViewObjectTypes.IndexOf((int) this.ParentID) < 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectTypeCollection((int) this.ParentID).CanViewObjects();
  }
}
