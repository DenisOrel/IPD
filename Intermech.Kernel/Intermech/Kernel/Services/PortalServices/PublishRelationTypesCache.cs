// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishRelationTypesCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class PublishRelationTypesCache : 
  PublishTypesCache<Dictionary<Guid, RelationMigrateType>>
{
  public PublishRelationTypesCache()
    : base("Portal.RelationTypes")
  {
  }

  protected override Dictionary<Guid, RelationMigrateType> Check(
    IUserSession session,
    Dictionary<Guid, RelationMigrateType> cache)
  {
    Dictionary<Guid, RelationMigrateType> dictionary;
    if (cache != null)
    {
      dictionary = cache.Where<KeyValuePair<Guid, RelationMigrateType>>((System.Func<KeyValuePair<Guid, RelationMigrateType>, bool>) (x => MetaDataHelper.ExistsRelationType(x.Key))).ToDictionary<KeyValuePair<Guid, RelationMigrateType>, Guid, RelationMigrateType>((System.Func<KeyValuePair<Guid, RelationMigrateType>, Guid>) (x => x.Key), (System.Func<KeyValuePair<Guid, RelationMigrateType>, RelationMigrateType>) (x => x.Value));
    }
    else
    {
      DataTable dataTable = session.GetRelationTypeCollection().Select("F_DESCRIPTION");
      dictionary = new Dictionary<Guid, RelationMigrateType>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        dictionary.Add(new Guid(Convert.ToString(row["F_GUID"])), RelationMigrateType.DependsSetting);
    }
    return dictionary;
  }
}
