// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.DBUtils
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Kernel;

internal static class DBUtils
{
  public static void WriteAttribute(IDBAttributable dbObj, Guid attrId, object attrValue)
  {
    dbObj.GetAttributeByGuid(attrId, true).Value = attrValue;
  }

  public static T ReadAttribute<T>(IDBAttributable dbObj, Guid attrId)
  {
    IDBAttribute attributeByGuid = dbObj.GetAttributeByGuid(attrId, true);
    if (attributeByGuid.IsNull)
      return default (T);
    return attributeByGuid.Value is T obj ? obj : (T) Convert.ChangeType((object) attributeByGuid, typeof (T));
  }

  public static List<Guid> GetParentsInverted(Guid objectType, IUserSession userSession)
  {
    List<Guid> parentsInverted = new List<Guid>(16 /*0x10*/);
    IDBObjectType objectType1;
    for (int parentTypeId = userSession.GetObjectType(objectType).ParentTypeID; parentTypeId != -1; parentTypeId = objectType1.ParentTypeID)
    {
      objectType1 = userSession.GetObjectType(parentTypeId);
      parentsInverted.Add(((IDBGuid) objectType1).GUID);
    }
    return parentsInverted;
  }
}
