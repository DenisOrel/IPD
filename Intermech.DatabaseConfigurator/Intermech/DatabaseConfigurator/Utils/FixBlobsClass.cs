// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.FixBlobsClass
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class FixBlobsClass : CustomBackgroundTask
{
  public List<InvalidBlobInfo> blobInfos = new List<InvalidBlobInfo>();

  public FixBlobsClass()
  {
    this._name = LocalizationHolder.rm.GetString("DatabaseConfigurator_236");
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
  }

  public void FindInvalidBlobs()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    try
    {
      List<IMSAttributeType> attributeTypesList = MetaDataHelper.GetAttributeTypesList();
      List<IMSAttributeType> imsAttributeTypeList = new List<IMSAttributeType>();
      foreach (IMSAttributeType imsAttributeType in attributeTypesList)
      {
        if (imsAttributeType.RealFieldType == FieldTypes.ftBlob || imsAttributeType.RealFieldType == FieldTypes.ftFile)
          imsAttributeTypeList.Add(imsAttributeType);
      }
      List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
      List<int> intList = new List<int>();
      intList.Add(-1);
      foreach (IMSObjectType imsObjectType in objectTypesList)
      {
        if (MetaDataHelper.GetObjectTypeParentID(imsObjectType.ObjectTypeID) == -1)
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(imsObjectType.ObjectTypeID);
          if (!imsObjectType.IsLocalType)
            childrenIdRecursive.Remove(imsObjectType.ObjectTypeID);
          if (childrenIdRecursive.Count > 0)
            intList.AddRange((IEnumerable<int>) childrenIdRecursive);
        }
      }
      foreach (int num1 in intList)
      {
        if (this._state == BackgroundTaskState.Stopped)
          return;
        foreach (IMSAttributeType imsAttributeType in imsAttributeTypeList)
        {
          if (this._state == BackgroundTaskState.Stopped)
            return;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            sessionKeeper.Session.ShowPersonalObjects = true;
            this._minValue = 0;
            this._value = 0;
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(imsAttributeType.AttributeID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.OR, 0, false)
            }, new object[1]
            {
              (object) ObligatoryObjectAttributes.F_OBJECT_ID
            });
            DataTable dataTable = sessionKeeper.Session.GetObjectCollection(num1).Select(paramSet);
            this._maxValue = dataTable.Rows.Count;
            this._name = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_237"), num1 == -1 ? (object) string.Empty : (object) $"{MetaDataHelper.GetObjectTypeName(num1)}. ", (object) imsAttributeType.Name);
            this.OnChanged(BackgroundTaskChangedType.Text);
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (this._state == BackgroundTaskState.Stopped)
                return;
              try
              {
                long int64 = Convert.ToInt64(row[0]);
                IDBObject dbObject = sessionKeeper.Session.GetObject(int64, false);
                if (dbObject != null)
                {
                  int typeId = dbObject.TypeID;
                  IDBAttribute attributeById = dbObject.GetAttributeByID(imsAttributeType.AttributeID);
                  if (attributeById != null)
                  {
                    for (int index = 0; index < attributeById.ValuesCount; ++index)
                    {
                      attributeById.Index = index;
                      if (attributeById.Value != DBNull.Value && attributeById.Value != null)
                      {
                        long asInteger = attributeById.AsInteger;
                        string asString = attributeById.AsString;
                        try
                        {
                          BlobInformation blobInformation = (attributeById as IBlobReader).OpenBlob(0);
                          int num2 = 128 /*0x80*/;
                          int dataBlockSize = blobInformation.PackedFileSize >= (long) num2 ? num2 : (int) blobInformation.PackedFileSize;
                          byte[] numArray = (attributeById as IBlobReader).ReadDataBlock(dataBlockSize);
                          if (numArray != null && numArray.Length < 0)
                            throw new Exception();
                          (attributeById as IBlobReader).CloseBlob();
                        }
                        catch
                        {
                          this.blobInfos.Add(new InvalidBlobInfo(typeId, int64, dbObject.NameInMessages, imsAttributeType.AttributeID, attributeById.Index, asInteger, asString));
                        }
                      }
                    }
                  }
                }
              }
              finally
              {
                ++this._value;
                this.OnChanged(BackgroundTaskChangedType.Value);
              }
            }
          }
        }
      }
      this.State = BackgroundTaskState.Terminated;
      Thread.Sleep(500);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      this.State = BackgroundTaskState.Error;
      this.OnChanged(BackgroundTaskChangedType.State);
      this._result = (object) ex.Message;
      this.OnChanged(BackgroundTaskChangedType.Result);
    }
  }
}
