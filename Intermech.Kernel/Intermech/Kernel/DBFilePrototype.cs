// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBFilePrototype
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBFilePrototype(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  private void AddPrototypeToCache()
  {
    IDBAttribute attributeByGuid1 = this.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 == null || attributeByGuid1.IsNull)
      return;
    IDBAttribute attributeByGuid2 = this.GetAttributeByGuid(new Guid("cad001d0-306c-11d8-b4e9-00304f19f545"));
    int attributeID = attributeByGuid2 == null || attributeByGuid2.IsNull ? this.UserSession.IdentHelper.FileAttributeID : this.UserSession.IdentHelper.GetAttributeID(attributeByGuid2.AsString);
    long ownerId = !((this.ObjectTypeClass as IDBGuid).GUID == new Guid("cad00347-306c-11d8-b4e9-00304f19f545")) ? 0L : this.OwnerID;
    IDBAttribute attributeByGuid3 = this.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
    for (int index1 = 0; index1 < attributeByGuid3.ValuesCount; ++index1)
    {
      attributeByGuid3.Index = index1;
      int objectTypeId = this.UserSession.IdentHelper.GetObjectTypeID(attributeByGuid3.AsString);
      object filePrototype = (this.UserSession.DBCache as CacheDataset)._FilePrototypes[(object) new FilePrototypeID(attributeID, objectTypeId, ownerId)];
      long[] numArray1;
      if (filePrototype == null)
      {
        numArray1 = new long[1]{ this.ObjectID };
      }
      else
      {
        numArray1 = (long[]) filePrototype;
        bool flag = false;
        for (int index2 = 0; index2 < numArray1.Length; ++index2)
        {
          if (numArray1[index2] == this.ObjectID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          long[] numArray2 = new long[numArray1.Length + 1];
          numArray1.CopyTo((Array) numArray2, 0);
          numArray2[numArray1.Length] = this.ObjectID;
          numArray1 = numArray2;
        }
      }
      (this.UserSession.DBCache as CacheDataset)._FilePrototypes[(object) new FilePrototypeID(attributeID, objectTypeId, ownerId)] = (object) numArray1;
    }
    attributeByGuid3.Index = 0;
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    this.AddPrototypeToCache();
  }

  protected override void DoDelete()
  {
    base.DoDelete();
    this.UserSession.DBCache.DeleteFilePrototype(this.ObjectID);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    if (this.IsCreationMode || attribute.AttributeID != this.UserSession.IdentHelper.GetAttributeID("cad00149-306c-11d8-b4e9-00304f19f545"))
      return;
    this.UserSession.DBCache.DeleteFilePrototype(this.ObjectID);
    this.AddPrototypeToCache();
  }

  protected override void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    base.DoAfterDeleteAdditionalAttributeValue(attribute, deletedValue);
    this.UserSession.DBCache.DeleteFilePrototype(this.ObjectID);
    this.AddPrototypeToCache();
  }
}
