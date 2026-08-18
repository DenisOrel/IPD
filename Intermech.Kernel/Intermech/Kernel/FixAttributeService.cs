// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.FixAttributeService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel;

public class FixAttributeService : LongLifeObject, IFixAttributeService
{
  public void DeleteBlob(InvalidBlobInfo blobInfo, Guid sessionGuid)
  {
    if (!UserSession.GetSessionByID(sessionGuid).IsAdmin)
      throw new KernelExceptionID(sc_13809.ssp_appserver_13810(823931714));
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone("FixAttributeService.DeleteBlob");
      IDBObject dbObject = userSession.GetObject(blobInfo.objectID, false);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(blobInfo.attrID);
      (attributeById as DBAttribute).SetValidatingMode(1);
      if (attributeById == null)
        return;
      int num = -1;
      if (blobInfo.attrIndex >= attributeById.ValuesCount || attributeById.Descriptions.GetValue(blobInfo.attrIndex).ToString() != blobInfo.fileName)
      {
        object[] descriptions = (object[]) attributeById.Descriptions;
        for (int index = 0; index < descriptions.Length; ++index)
        {
          if (descriptions[index] != null && descriptions[index].ToString() == blobInfo.fileName)
            num = index;
        }
        if (num == 1)
          return;
      }
      else
        num = blobInfo.attrIndex;
      attributeById.Index = num;
      (attributeById as DBAdditionalAttribute).PurgeValue();
    }
    finally
    {
      userSession?.Logout("FixAttributeService.DeleteBlob");
    }
  }

  public void PugreObject(long objectID, Guid sessionGuid)
  {
    if (!UserSession.GetSessionByID(sessionGuid).IsAdmin)
      throw new KernelExceptionID(sc_13809.ssp_appserver_13811(1377901831));
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone("FixAttributeService.PugreObject");
      IDBObject dbObject = userSession.GetObject(objectID, false);
      if (dbObject == null)
        return;
      (dbObject as DBObject).Purge(0L);
    }
    finally
    {
      userSession?.Logout("FixAttributeService.PugreObject");
    }
  }
}
