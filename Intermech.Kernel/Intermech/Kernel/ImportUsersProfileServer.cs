// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ImportUsersProfileServer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Data;


namespace Intermech.Kernel;

public class ImportUsersProfileServer : LongLifeObject, IImportUsersProfile
{
  public void CopyProfile(long[] sourceUserIDs, long[] destUserIDs, bool throwExceptionIfEqual)
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IUserSession systemSession = (IUserSession) null;
    try
    {
      systemSession = service.GetSystemSessionTemporaryClone("ImportUsersProfileServer.CopyProfile1");
      systemSession.ShowPersonalObjects = true;
      foreach (long sourceUserId in sourceUserIDs)
      {
        foreach (long destUserId in destUserIDs)
        {
          if (!sourceUserId.Equals(destUserId))
            this.CopyProfile(sourceUserId, destUserId, systemSession);
          else if (throwExceptionIfEqual)
            throw new ArgumentException(LocalizationHolder.rm.GetString(sc_12353.ssp_appserver_12354()));
        }
      }
    }
    finally
    {
      systemSession?.Logout("ImportUsersProfileServer.CopyProfile1");
    }
  }

  public void CopyProfile(long sourceUserID, long destUserID, bool throwExceptionIfEqual)
  {
    if (sourceUserID.Equals(destUserID))
    {
      if (throwExceptionIfEqual)
        throw new ArgumentException(LocalizationHolder.rm.GetString("Kernel_848"));
    }
    else
    {
      IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("ImportUsersProfileServer.CopyProfile2");
      try
      {
        sessionTemporaryClone.ShowPersonalObjects = true;
        this.CopyProfile(sourceUserID, destUserID, sessionTemporaryClone);
      }
      finally
      {
        sessionTemporaryClone?.Logout("ImportUsersProfileServer.CopyProfile2");
      }
    }
  }

  private void CopyProfile(long sourceUserID, long destUserID, IUserSession systemSession)
  {
    IDBObjectCollection objectCollection = systemSession.GetObjectCollection(systemSession.IdentHelper.ConfigDataTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(ImportUsersProfileHolder.ObjectOwnerAttributeGuid, RelationalOperators.Equal, (object) sourceUserID, LogicalOperators.NONE, 0);
    conditionStructure.Content = ColumnContents.ID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable1 = objectCollection.Select(paramSet);
    conditionStructure.Value = (object) destUserID;
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable2 = objectCollection.Select(paramSet);
    if (dataTable1 == null || dataTable2 == null || !dataTable1.Rows.Count.Equals(1))
      return;
    long int64 = Convert.ToInt64(dataTable1.Rows[0][ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJECT_ID)]);
    long objectID;
    if (dataTable2.Rows.Count.Equals(0))
    {
      IDBObject dbObject1 = systemSession.GetObject(destUserID);
      IDBObject dbObject2 = objectCollection.Create();
      AttributeValues attributeValues = new AttributeValues(systemSession.GetAttributeType(ImportUsersProfileHolder.ObjectOwnerAttributeGuid).AttributeID, (object) destUserID);
      dbObject2.SetAttributesValues(new AttributeValues[1]
      {
        attributeValues
      });
      dbObject2.Caption = string.Format(LocalizationHolder.rm.GetString("Kernel_849"), (object) dbObject1.Caption);
      dbObject2.CommitCreation(true);
      objectID = dbObject2.ObjectID;
    }
    else
      objectID = Convert.ToInt64(dataTable2.Rows[0][ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJECT_ID)]);
    IDBObject dbObject3 = systemSession.GetObject(int64);
    IDBObject dbObject4 = systemSession.GetObject(objectID);
    int configFileAttributeId = systemSession.IdentHelper.ConfigFileAttributeID;
    this.CopyProfile(dbObject3.GetAttributeByID(configFileAttributeId), dbObject4.GetAttributeByID(systemSession.IdentHelper.ConfigFileAttributeID));
  }

  private void CopyProfile(IDBAttribute source, IDBAttribute dest)
  {
    if (source == null || dest == null)
      return;
    IBlobReader blobReader = source as IBlobReader;
    IBlobWriter blobWriter = dest as IBlobWriter;
    ArrayList arrayList = new ArrayList((ICollection) dest.Values);
    for (int index = 0; index < source.ValuesCount; ++index)
    {
      source.Index = index;
      if (blobReader.OpenBlob(-1).RealFileSize > 0L)
      {
        BlobInformation blobInfo = blobReader.OpenBlob(0);
        byte[] data = blobReader.ReadDataBlock();
        blobReader.CloseBlob();
        if (arrayList.Contains(source.Value))
        {
          dest.Index = arrayList.IndexOf(source.Value);
          if (blobWriter.OpenBlob(blobInfo, false))
            blobWriter.WriteDataBlock(data);
        }
        else
        {
          if (arrayList.Contains((object) string.Empty))
          {
            dest.Index = arrayList.IndexOf((object) string.Empty);
            if ((dest as IBlobReader).OpenBlob(-1).RealFileSize > 0L)
              dest.Index = dest.AddValue((object) null);
            else
              arrayList.Remove((object) string.Empty);
          }
          else
            dest.Index = dest.AddValue((object) null);
          if (blobWriter.OpenBlob(blobInfo, false))
          {
            blobWriter.WriteDataBlock(data);
            arrayList.Add((object) blobInfo.FileName);
          }
        }
      }
    }
  }
}
