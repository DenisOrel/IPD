// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectsComparer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace Intermech.Kernel.Services.PortalServices;

internal class ObjectsComparer
{
  public static void Compare(
    IUserSession session,
    IDBObject importObj,
    ImportingObject briefObject,
    ImportReceipt receipt)
  {
    List<int> intList = new List<int>();
    if (importObj.ObjectType != briefObject.Object.ObjectType)
      receipt.AddObjectRecord(session, importObj, briefObject, $"Изменен тип объекта c {MetaDataHelper.GetObjectTypeName(importObj.ObjectType)} на {MetaDataHelper.GetObjectTypeName(briefObject.Object.ObjectType)}");
    foreach (AttributeRecord attribute in briefObject.Attributes)
    {
      if (!intList.Contains(attribute.AttributeId))
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute.AttributeId);
        IDBAttribute attributeById = importObj.GetAttributeByID(attribute.AttributeId);
        if (attributeById == null)
        {
          receipt.AddAttributeRecord(briefObject, attributeType.Name, "Объекту добавлен новый атрибут");
          intList.Add(attribute.AttributeId);
        }
        else
        {
          bool flag = attribute.InlistId >= attributeById.ValuesCount;
          if (!flag)
          {
            attributeById.Index = attribute.InlistId;
            if (attributeById.IsNull)
              flag = true;
          }
          switch (attributeType.RealFieldType)
          {
            case FieldTypes.ftString:
            case FieldTypes.ftPassword:
            case FieldTypes.ftGuid:
              if (flag)
              {
                if (attribute.StringValue != null)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, Convert.ToString(attribute.StringValue));
                  continue;
                }
                continue;
              }
              if (!CompareValuesHelper.CompareStringValues(attribute.StringValue, (object) attributeById.AsString))
              {
                receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(attributeById.AsString), Convert.ToString(attribute.StringValue));
                continue;
              }
              continue;
            case FieldTypes.ftInteger:
            case FieldTypes.ftBoolean:
            case FieldTypes.ftAutoInc:
              if (flag)
              {
                if (attribute.IntegerValue != null)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, Convert.ToString(attribute.IntegerValue));
                  continue;
                }
                continue;
              }
              if (!CompareValuesHelper.CompareIntValues(attribute.IntegerValue, (object) attributeById.AsInteger))
              {
                receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(attributeById.AsInteger), Convert.ToString(attribute.IntegerValue));
                continue;
              }
              continue;
            case FieldTypes.ftDouble:
              if (flag)
              {
                if (attribute.DoubleValue != null)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, Convert.ToString(attribute.DoubleValue, (IFormatProvider) CultureInfo.CurrentCulture));
                  continue;
                }
                continue;
              }
              if (!CompareValuesHelper.CompareFloatValues(attribute.DoubleValue, (object) attributeById.AsDouble))
              {
                receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(attributeById.AsDouble, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToString(attribute.DoubleValue, (IFormatProvider) CultureInfo.CurrentCulture));
                continue;
              }
              continue;
            case FieldTypes.ftDateTime:
              if (flag)
              {
                if (attribute.DateValue != null)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, Convert.ToString(attribute.DateValue, (IFormatProvider) CultureInfo.CurrentCulture));
                  continue;
                }
                continue;
              }
              if (!CompareValuesHelper.CompareDateTimeValues(attribute.DateValue, (object) (attributeById.AsDateTime - session.TimeZoneOffset)))
              {
                receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(attributeById.AsDateTime, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToString(attribute.DateValue, (IFormatProvider) CultureInfo.CurrentCulture));
                continue;
              }
              continue;
            case FieldTypes.ftShortBlob:
            case FieldTypes.ftBlob:
              if (flag)
              {
                if (attribute.IntegerValue != null && Convert.ToInt64(attribute.IntegerValue) > 0L)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, (string) null, "Добавлено новое значение");
                  continue;
                }
                continue;
              }
              IBlobReader blobReader1 = attributeById as IBlobReader;
              BlobInformation blobInformation1 = blobReader1.OpenBlob(-1);
              try
              {
                long int64 = Convert.ToInt64(attribute.IntegerValue);
                if (!CompareValuesHelper.CompareIntValues((object) blobInformation1.RealFileSize, (object) int64))
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, StringsHelper.GetSizeString(blobInformation1.RealFileSize), StringsHelper.GetSizeString(int64));
                  continue;
                }
                if (!CompareValuesHelper.CompareDateTimeValues((object) (blobInformation1.ModifyDate - session.TimeZoneOffset), attribute.DateValue))
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(blobInformation1.ModifyDate - session.TimeZoneOffset, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToString(attribute.DateValue, (IFormatProvider) CultureInfo.CurrentCulture));
                  continue;
                }
                continue;
              }
              finally
              {
                blobReader1.CloseBlob();
              }
            case FieldTypes.ftFile:
              if (flag)
              {
                if (attribute.IntegerValue != null && Convert.ToInt64(attribute.IntegerValue) > 0L)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(attribute.StringValue), (string) null, "Добавлен новый файл");
                  continue;
                }
                continue;
              }
              IBlobReader blobReader2 = attributeById as IBlobReader;
              BlobInformation blobInformation2 = blobReader2.OpenBlob(-1);
              try
              {
                long int64 = Convert.ToInt64(attribute.IntegerValue);
                if (!CompareValuesHelper.CompareStringValues((object) Convert.ToString(attribute.StringValue), (object) blobInformation2.FileName))
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, blobInformation2.FileName, Convert.ToString(attribute.StringValue));
                  continue;
                }
                if (!CompareValuesHelper.CompareIntValues((object) blobInformation2.RealFileSize, (object) int64))
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, StringsHelper.GetSizeString(blobInformation2.RealFileSize), StringsHelper.GetSizeString(int64), Convert.ToString(attribute.StringValue));
                  continue;
                }
                if (!CompareValuesHelper.CompareDateTimeValues((object) (blobInformation2.ModifyDate - session.TimeZoneOffset), attribute.DateValue))
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(blobInformation2.ModifyDate - session.TimeZoneOffset, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToString(attribute.DateValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToString(attribute.StringValue));
                  continue;
                }
                continue;
              }
              finally
              {
                blobReader2.CloseBlob();
              }
            case FieldTypes.ftObjectLink:
              if (flag || attributeById.AsInteger == 0L)
              {
                if (attribute.StringValue != null)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, Convert.ToString(attribute.StringValue));
                  continue;
                }
                continue;
              }
              QuickObjectInfo objectInfo = session.GetObjectInfo(attributeById.AsInteger);
              if (!CompareValuesHelper.CompareStringValues(attribute.StringValue, (object) objectInfo.VersionGuid.ToString()))
              {
                receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, objectInfo.VersionGuid.ToString(), Convert.ToString(attribute.StringValue));
                continue;
              }
              continue;
            case FieldTypes.ftMemo:
              if (flag)
              {
                if (attribute.IntegerValue != null && Convert.ToInt64(attribute.IntegerValue) > 0L)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, (string) null, "Добавлено новое значение");
                  continue;
                }
                continue;
              }
              IMemoReader memoReader = attributeById as IMemoReader;
              int num = memoReader.OpenMemo(0);
              try
              {
                if (!CompareValuesHelper.CompareIntValues((object) Convert.ToInt32(attribute.IntegerValue), (object) num))
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, StringsHelper.GetSizeString((long) num), StringsHelper.GetSizeString((long) Convert.ToInt32(attribute.IntegerValue)));
                  continue;
                }
                continue;
              }
              finally
              {
                memoReader.CloseMemo();
              }
            case FieldTypes.ftMeasured:
              if (flag)
              {
                if (attribute.StringValue != null)
                {
                  receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, (string) null, Convert.ToString(attribute.StringValue));
                  continue;
                }
                continue;
              }
              if (!CompareValuesHelper.CompareFloatValues(attribute.DoubleValue, (object) attributeById.AsDouble))
              {
                receipt.AddAttributeRecord(briefObject, attributeType.Name, attribute.InlistId, Convert.ToString(attributeById.AsString), Convert.ToString(attribute.StringValue));
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
  }
}
