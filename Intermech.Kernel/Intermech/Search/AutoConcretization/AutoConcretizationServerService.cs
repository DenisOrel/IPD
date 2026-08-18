// Decompiled with JetBrains decompiler
// Type: Intermech.Search.AutoConcretization.AutoConcretizationServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.AutoConcretization;

public sealed class AutoConcretizationServerService : 
  LongLifeObject,
  IAutoConcretizationServerService
{
  public bool CanModifyCompositionAutoConcretizationAttribute(
    Guid userSessionGuid,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? this.CanModifyCompositionAutoConcretizationAttribute(objectVersionID) : throw new ArgumentException();
  }

  public void DisableAutoConcretization(Guid userSessionGuid, long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.DisableAutoConcretization(objectVersionID);
    }
  }

  public void EnableAutoConcretization(Guid userSessionGuid, long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.EnableAutoConcretization(objectVersionID);
    }
  }

  public bool IsAutoConcretizationEnabled(Guid userSessionGuid, IDBObject projObject)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(projObject.ObjectID) ? this.IsAutoConcretizationEnabled(projObject) : throw new ArgumentException();
  }

  private bool CanModifyCompositionAutoConcretizationAttribute(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
        return true;
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.TypeID, AutoConcretizationConstants.CompositionAutoConcretizationAttributeTypeID);
      return attribute4ObjectType != null && attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.ModifyInBase);
    }
  }

  private void DisableAutoConcretization(long objectVersionID)
  {
    AutoConcretizationServerService.SetAutoConcretizationAttributeValue(objectVersionID, false);
  }

  private static void SetAutoConcretizationAttributeValue(
    long objectVersionID,
    bool autoConcretization)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(objectVersionID).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(AutoConcretizationConstants.CompositionAutoConcretizationAttributeTypeID, (object) autoConcretization)
      });
  }

  private void EnableAutoConcretization(long objectVersionID)
  {
    AutoConcretizationServerService.SetAutoConcretizationAttributeValue(objectVersionID, true);
  }

  private bool IsAutoConcretizationEnabled(IDBObject projObject)
  {
    IDBAttribute attributeById = projObject.GetAttributeByID(AutoConcretizationConstants.CompositionAutoConcretizationAttributeTypeID);
    return attributeById != null && attributeById.AsBoolean;
  }
}
