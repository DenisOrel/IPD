// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Server.AVSServerService
// Assembly: Intermech.AVS.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DD9587A9-B8FC-4A8A-AB7E-E4D2C61BABE8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AVS.Server.dll

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Server;

public class AVSServerService : LongLifeObject, IAVSServerService
{
  private readonly DBAVSDocumentObjectCreator _dbSpecificationObjectCreator = new DBAVSDocumentObjectCreator();

  public void AfterCheckOutSpecification(IDBObject sender, IUserSession session)
  {
    (long num, List<long> products) = AvsIDCache.FindSpecificationAndAssemblyProducts(sender, "");
    if (!num.IsDefinedId())
      return;
    bool flag1 = sender.ObjectID == num;
    products.Add(num);
    List<IDBObject> dbObjectList = new List<IDBObject>(products.Count);
    foreach (long objectID in products)
    {
      if (objectID != sender.ObjectID)
      {
        IDBObject dbObject = session.GetObject(objectID);
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.ObjectID > 0L)
        {
          bool flag2 = objectID != num;
          if (dbObject.CheckoutBy.IsDefinedId() && dbObject.CheckoutBy != session.UserID)
            dbObject.CheckOut();
          else if (flag1 == flag2)
            dbObjectList.Add(dbObject);
        }
      }
    }
    foreach (IDBObject dbObject in dbObjectList)
      dbObject.CheckOut();
  }

  public List<IDBObject> GetProductsToCheckOut(long specificationID, IUserSession session)
  {
    List<IDBObject> productsToCheckOut = new List<IDBObject>();
    foreach (long specificationByRelation in AvsIDCache.FindProductForSpecificationByRelations(session, specificationID, ""))
    {
      IDBObject dbObject = session.GetObject(specificationByRelation);
      if (dbObject.CheckoutBy == 0L && dbObject.ObjectID >= 0L)
        productsToCheckOut.Add(dbObject);
    }
    return productsToCheckOut;
  }

  public void AfterCheckInSpecification(IDBObject sender, IUserSession session)
  {
    if (AvsIDCache.IsProductForSpecification(sender.ObjectType))
    {
      long assemblyProducts = AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new long[1]
      {
        sender.ObjectID
      }, "", true);
      if (assemblyProducts.IsDefinedId())
      {
        IDBObject dbObject = session.GetObject(assemblyProducts);
        if (dbObject.CheckoutBy != session.UserID || dbObject.ObjectID >= 0L)
          return;
        dbObject.CheckIn();
      }
      else
      {
        if (!sender.ParentVersionID.IsDefinedId() || AvsIDCache.ArticleIsRemovedFormGroupSpecification(sender))
          return;
        if (!Consts.IsUndefinedObjectId(AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new long[1]
        {
          sender.ParentVersionID
        }, "", true)))
          throw new KernelException($"Вы пытаетесь завершить редактирование объекта \"{sender.Caption}\" [{-sender.ObjectID}]. \r\nПеред завершением редактирования необходимо создать спецификацию (вызовите команду \"Редактировать\" для данного объекта).").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(-sender.ObjectID));
      }
    }
    else
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, AvsIDCache.ObjType_Specification))
        return;
      foreach (long specificationByRelation in AvsIDCache.FindProductForSpecificationByRelations(session, sender.ObjectID, ""))
      {
        IDBObject dbObject = session.GetObject(specificationByRelation, false);
        if (dbObject != null && dbObject.CheckoutBy == session.UserID && dbObject.ObjectID < 0L)
          dbObject.CheckIn();
      }
    }
  }

  public void AfterSaveToArcCopy(IDBObject sender, IUserSession session)
  {
    if (AvsIDCache.IsProductForSpecification(sender.ObjectType))
    {
      long assemblyProducts = AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new long[1]
      {
        sender.ObjectID
      }, "", true);
      if (!Consts.IsUndefinedObjectId(assemblyProducts))
      {
        IDBObject dbObject = session.GetObject(assemblyProducts);
        if (dbObject.CheckoutBy != session.UserID || dbObject.ObjectID >= 0L)
          return;
        dbObject.SaveToArcCopy();
      }
      else
      {
        if (Consts.IsUndefinedObjectId(sender.ParentVersionID) || AvsIDCache.ArticleIsRemovedFormGroupSpecification(sender))
          return;
        if (!Consts.IsUndefinedObjectId(AvsIDCache.FindSpecificationForAssemblyProducts(session, (IList<long>) new long[1]
        {
          sender.ParentVersionID
        }, "", true)))
          throw new KernelException($"Вы пытаетесь завершить редактирование объекта \"{sender.Caption}\" [{-sender.ObjectID}]. \r\nПеред завершением редактирования необходимо создать спецификацию (вызовите команду \"Редактировать\" для данного объекта).").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(-sender.ObjectID));
      }
    }
    else
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, AvsIDCache.ObjType_Specification))
        return;
      foreach (long specificationByRelation in AvsIDCache.FindProductForSpecificationByRelations(session, sender.ObjectID, ""))
      {
        IDBObject dbObject = session.GetObject(specificationByRelation);
        if (dbObject.CheckoutBy == session.UserID && dbObject.ObjectID < 0L)
          dbObject.SaveToArcCopy();
      }
    }
  }

  public void AfterUndoCheckOutSpecification(IDBObject sender, ObjectDeleteEventArgs args)
  {
    IUserSession userSession = sender != null ? sender.Session : throw new ArgumentNullException(nameof (sender));
    if ((args.DeleteMode & 2048L /*0x0800*/) == 0L)
      return;
    bool isAdminMode = ((ulong) args.DeleteMode & 16UL /*0x10*/) > 0UL;
    (long num, List<long> products) = AvsIDCache.FindSpecificationAndAssemblyProducts(sender, "");
    if (!num.IsDefinedId())
      return;
    bool flag1 = sender.ObjectID == num;
    List<IDBObject> dbObjectList = new List<IDBObject>(products.Count);
    products.Add(num);
    foreach (long objectID in products)
    {
      if (objectID != sender.ObjectID)
      {
        bool flag2 = objectID != num;
        IDBObject dbObject = userSession.GetObject(objectID);
        if (dbObject.CheckoutBy.IsDefinedId() && flag1 == flag2 && dbObject.CheckoutBy == userSession.UserID | isAdminMode)
          dbObjectList.Add(dbObject);
      }
    }
    foreach (IDBObject dbObject in dbObjectList)
      dbObject.CancelChanges(isAdminMode);
  }

  public void AddAvsDBObjectCreator(object creatorType)
  {
    if (!(ServerServices.GetService(typeof (IDBObjectService)) is ICreatorContainer service))
      return;
    object creator = service.GetCreator(creatorType);
    if (creator == null)
    {
      service.AddCreator(creatorType, (object) this._dbSpecificationObjectCreator);
    }
    else
    {
      if (creator is DBAVSDocumentObjectCreator)
        return;
      service.AddCreator(creatorType, (object) this._dbSpecificationObjectCreator, true);
    }
  }
}
