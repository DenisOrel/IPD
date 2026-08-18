// Decompiled with JetBrains decompiler
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingServerService : 
  LongLifeObject,
  IGroupAttributesChangingServerService
{
  public ObjectBlank[] FindObjects(Guid userSessionGuid, long[] objectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return objectVersionIds != null && objectVersionIds.Length != 0 && !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds) ? this.FindObjects(((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>()) : throw new ArgumentException();
  }

  public ObjectBlank[] SaveObjects(Guid userSessionGuid, ObjectBlank[] objectBlanks)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return objectBlanks != null ? this.SaveObjects(objectBlanks) : throw new ArgumentException();
  }

  private ObjectBlank[] FindObjects(long[] objectVersionIds)
  {
    List<ObjectBlank> objectBlankList = new List<ObjectBlank>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectVersionId in objectVersionIds)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId);
        AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeCaption);
        List<AttributeBlank> attributeBlankList = new List<AttributeBlank>();
        foreach (AttributeValues attributeValues in attributesValues)
        {
          bool isEditable = (attributeValues.AttributeID == -50 || attributeValues.AttributeType == FieldTypes.ftString || attributeValues.AttributeType == FieldTypes.ftMemo) && attributeValues.MultipleValued == MultiValueModes.SingleValue;
          object obj = attributeValues.Values.Length != 0 ? attributeValues.Values[0] : (object) null;
          if (obj is DBNull)
            obj = (object) null;
          attributeBlankList.Add(new AttributeBlank(attributeValues.AttributeID, !isEditable || attributeValues.ReadOnly, isEditable, obj));
        }
        bool canCheckOut = dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && ObjectHelper.IsUnknownObjectVersionID(dbObject.CheckoutBy);
        objectBlankList.Add(new ObjectBlank(dbObject.ObjectID, dbObject.ObjectType, canCheckOut, dbObject.CheckoutBy, attributeBlankList.ToArray()));
      }
    }
    return objectBlankList.ToArray();
  }

  private ObjectBlank[] SaveObjects(ObjectBlank[] objectBlanks)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ObjectBlank objectBlank in objectBlanks)
      {
        List<AttributeValues> attributeValuesList = new List<AttributeValues>();
        foreach (AttributeBlank attribute in objectBlank.Attributes)
        {
          if (attribute.IsChanged)
            attributeValuesList.Add(new AttributeValues(attribute.AttributeTypeID, attribute.Value));
        }
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectBlank.ObjectVersionID);
        try
        {
          dbObject.SetAttributesValues(attributeValuesList.ToArray());
        }
        catch
        {
          longList.Add(objectBlank.ObjectVersionID);
        }
      }
    }
    ObjectBlank[] objects = this.FindObjects(((IEnumerable<ObjectBlank>) objectBlanks).Select<ObjectBlank, long>((Func<ObjectBlank, long>) (o => o.ObjectVersionID)).ToArray<long>());
    foreach (ObjectBlank objectBlank1 in objects)
    {
      ObjectBlank objectBlank = objectBlank1;
      ObjectBlank objectBlank2 = ((IEnumerable<ObjectBlank>) objectBlanks).First<ObjectBlank>((Func<ObjectBlank, bool>) (o => o.ObjectVersionID == objectBlank.ObjectVersionID));
      objectBlank.Statuses = objectBlank2.Statuses;
      if (longList.Contains(objectBlank.ObjectVersionID))
      {
        objectBlank.Statuses &= ~ObjectBlankStatuses.Sussess;
        objectBlank.Statuses |= ObjectBlankStatuses.Error;
      }
      else
      {
        objectBlank.Statuses &= ~ObjectBlankStatuses.Error;
        objectBlank.Statuses |= ObjectBlankStatuses.Sussess;
      }
    }
    return objects;
  }
}
