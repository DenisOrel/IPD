// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishObjectTypesCache
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

internal sealed class PublishObjectTypesCache : PublishTypesCache<List<int>>
{
  public PublishObjectTypesCache()
    : base("Portal.ObjectTypes")
  {
  }

  protected override List<int> Check(IUserSession session, List<int> cache)
  {
    List<int> intList;
    if (cache != null)
    {
      intList = cache.Where<int>((System.Func<int, bool>) (x => MetaDataHelper.ExistsObjectType(x))).ToList<int>();
    }
    else
    {
      DataTable dataTable = (session as UserSession).DataManager.ExecuteDataTable($"SELECT F_OBJECT_TYPE FROM IMS_ATTR4OBJ_TYPES WHERE F_ATTRIBUTE_ID = {MetaDataHelper.GetAttributeTypeID(PortalConsts.attributePublicationNecessary)}");
      intList = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        intList.Add(Convert.ToInt32(row[0]));
    }
    return intList;
  }
}
