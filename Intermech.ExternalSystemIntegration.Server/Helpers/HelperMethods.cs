// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Helpers.HelperMethods
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Helpers;

internal class HelperMethods
{
  public static void SetObjectStatus(Guid aSessionGuid, long aObjectId, StatusEnum aStatus)
  {
    if (!(UserSession.GetSessionByID(aSessionGuid) is UserSession sessionById))
      return;
    IDBObject dbObject = sessionById.GetObject(aObjectId);
    if (!(dbObject is IStatus))
      return;
    (dbObject as IStatus).Status = Convert.ToInt64((object) aStatus);
  }

  public static void AddErrorText(Guid aSessionGuid, long aObjectId, string aErrorText)
  {
    if (!(UserSession.GetSessionByID(aSessionGuid) is UserSession sessionById))
      return;
    IDBObject dbObject = sessionById.GetObject(aObjectId);
    if (dbObject == null)
      return;
    IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(Const.ErrorTextAttrTypeID, false);
    if (dbAttribute == null)
      return;
    dbAttribute.Value = (object) aErrorText;
  }

  public static void WriteErrorMsg(Guid aSessionGuid, string aErrorText)
  {
    if (!(UserSession.GetSessionByID(aSessionGuid) is UserSession sessionById))
      return;
    sessionById.EventLog.AddToTrace(aErrorText, 0, Const.LogFileName);
  }

  public static void WriteCompareErrorMsg(Guid aSessionGuid, string aErrorText)
  {
    if (!(UserSession.GetSessionByID(aSessionGuid) is UserSession sessionById))
      return;
    sessionById.EventLog.AddToTrace(aErrorText, 0, Const.CompareLogFileName);
  }
}
