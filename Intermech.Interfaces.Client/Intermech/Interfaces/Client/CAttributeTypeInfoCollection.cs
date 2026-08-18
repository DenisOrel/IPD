// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Коллекция со списком типов атрибутов</summary>
internal class CAttributeTypeInfoCollection(
  MetadataInfoParentContext serviceContext,
  object parentID,
  bool filtering) : MetadataInfoCollection(serviceContext, parentID, filtering), IDBAttributeTypeInfoCollection, IDBCollection
{
  protected override string DBKeyField
  {
    [DebuggerStepThrough] get => "F_ATTRIBUTE_ID";
  }

  protected override string DBTableName
  {
    [DebuggerStepThrough] get => "IMS_ATTRIBUTES";
  }

  protected override string GetParentSQL()
  {
    return AttributeCacheHelper.GetAttributesForParentSQL(this.ServiceContext.ClientCache.GetTable("IMS_ATTR_IN_GROUPS"), this.ParentID);
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    DataTable dataTable = base.Select(orderBy, addInfo);
    if (addInfo != null && addInfo.Length != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        dataTable = AttributeCacheHelper.AddInfoToTable(dataTable, addInfo, sessionKeeper.Session);
    }
    this.FillCaptions(dataTable);
    return dataTable;
  }
}
