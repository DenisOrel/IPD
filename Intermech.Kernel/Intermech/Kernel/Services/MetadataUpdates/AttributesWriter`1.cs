// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributesWriter`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class AttributesWriter<TAttributable> where TAttributable : IDBAttributable
{
  public void WriteAttributes(
    IUserSession session,
    TAttributable attributable,
    List<AttributeNode> attributes,
    IPropertyFactory propertyFactory,
    bool isNew)
  {
    foreach (AttributeNode attribute in attributes)
    {
      AttributeValues av = attribute.Value != null ? (AttributeValues) attribute.Value : (AttributeValues) null;
      if (av != null)
      {
        if (isNew)
          this.SetAttributeValue(session, attributable, av);
        else if (propertyFactory.IsPropertyObligatory(Convert.ToString((object) av.AttributeGuid)) || attributable.GetAttributeByID(av.AttributeID) == null)
          this.SetAttributeValue(session, attributable, av);
      }
    }
  }

  private void SetAttributeValue(
    IUserSession session,
    TAttributable attributable,
    AttributeValues av)
  {
    IDBAttributeType attributeType = session.GetAttributeType(av.AttributeID);
    IDBAttribute dbAttribute = attributable.GetAttributeByID(av.AttributeID);
    if (av.Values != null && av.Values.Length != 0)
    {
      bool flag = false;
      switch (av.AttributeType)
      {
        case FieldTypes.ftShortBlob:
        case FieldTypes.ftFile:
        case FieldTypes.ftBlob:
          if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
          {
            if (dbAttribute != null && dbAttribute.ValuesCount > 0)
            {
              if (av.Values == null || av.Values.Length == 0)
                flag = true;
              else if (dbAttribute.ValuesCount != av.Values.Length)
              {
                flag = true;
              }
              else
              {
                for (int index = 0; index < dbAttribute.ValuesCount; ++index)
                {
                  dbAttribute.Index = index;
                  if ((av.Values[index] as BlobRecord).ModifyDate != dbAttribute.AsDateTime)
                  {
                    flag = true;
                    break;
                  }
                }
              }
            }
            else if (av.Values != null && av.Values.Length != 0)
              flag = true;
          }
          else
          {
            BlobRecord blobRecord = av.Values[0] as BlobRecord;
            if (dbAttribute == null || dbAttribute.AsDateTime != blobRecord.ModifyDate)
              flag = true;
          }
          if (!flag)
            break;
          if (dbAttribute == null)
            dbAttribute = attributable.Attributes.AddAttribute(attributeType.AttributeID, false);
          if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
          {
            dbAttribute.Index = dbAttribute.ValuesCount - 1;
            while (dbAttribute.Index > 0)
              dbAttribute.DeleteValue();
          }
          for (int index = 0; index < av.Values.Length; ++index)
          {
            dbAttribute.Index = index;
            if (index > 0 && (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList))
              dbAttribute.AddValue((object) null);
            BlobRecord blobRecord = av.Values[index] as BlobRecord;
            if (blobRecord.Data != null && blobRecord.Data.Length != 0)
            {
              if (av.AttributeType == FieldTypes.ftShortBlob && blobRecord.Data.Length > Consts.MaxShortBlobSize)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1035"), (object) blobRecord.FileName, (object) blobRecord.Data.Length, (object) Consts.MaxShortBlobSize));
              BlobInformation blobInfo = new BlobInformation(blobRecord.RealFileSize, (long) blobRecord.Data.Length, blobRecord.ModifyDate, blobRecord.FileName, blobRecord.ArcMethod, blobRecord.FileName);
              (dbAttribute as IBlobWriter).OpenBlob(blobInfo, false);
              (dbAttribute as IBlobWriter).WriteDataBlock(blobRecord.Data);
            }
            else
            {
              BlobInformation blobInfo = BlobInformation.EmptyBlobInformation() with
              {
                FileName = blobRecord.FileName
              };
              (dbAttribute as IBlobWriter).OpenBlob(blobInfo, true);
            }
          }
          break;
        default:
          if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
          {
            if (dbAttribute != null && dbAttribute.Values != null && dbAttribute.ValuesCount > 0)
            {
              if (av.Values == null || av.Values.Length == 0)
                flag = true;
              else if (dbAttribute.ValuesCount != av.Values.Length)
              {
                flag = true;
              }
              else
              {
                for (int index = 0; index < dbAttribute.ValuesCount; ++index)
                {
                  if (CompareValuesHelper.NormalizedValue(dbAttribute.Values[index]) != null)
                  {
                    if (!this.EqualValues(attributeType.AttributeType, attributeType.AttributeID, dbAttribute.Values[index], av.Values[index]))
                    {
                      flag = true;
                      break;
                    }
                  }
                  else if (CompareValuesHelper.NormalizedValue(av.Values[index]) != null)
                  {
                    flag = true;
                    break;
                  }
                }
              }
            }
            else if (av.Values != null && av.Values.Length != 0)
              flag = true;
            if (!flag)
              break;
            if (dbAttribute == null)
              dbAttribute = attributable.Attributes.AddAttribute(attributeType.AttributeID, false);
            dbAttribute.Values = av.Values;
            break;
          }
          if (dbAttribute != null && CompareValuesHelper.NormalizedValue(dbAttribute.Value) != null)
          {
            if (!this.EqualValues(attributeType.AttributeType, attributeType.AttributeID, dbAttribute.Value, av.Values[0]))
              flag = true;
          }
          else if (CompareValuesHelper.NormalizedValue(av.Values[0]) != null)
            flag = true;
          if (!flag)
            break;
          if (dbAttribute == null)
            dbAttribute = attributable.Attributes.AddAttribute(attributeType.AttributeID, false);
          dbAttribute.Value = av.Values[0];
          break;
      }
    }
    else
    {
      if (dbAttribute == null)
        return;
      IDBAttributeType4 attributeType4 = this.GetAttributeType4(session, attributable, av.AttributeID);
      if (attributeType4 == null || attributeType4.Required == RequiredModes.Manual)
      {
        dbAttribute.Delete(0L);
      }
      else
      {
        if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
          dbAttribute.ClearValues();
        dbAttribute.Clear();
      }
    }
  }

  protected abstract IDBAttributeType4 GetAttributeType4(
    IUserSession session,
    TAttributable attributable,
    int attributeID);

  private bool EqualValues(FieldTypes fieldType, int attributeID, object value1, object value2)
  {
    if (fieldType == FieldTypes.ftSystem)
      fieldType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeID);
    switch (fieldType - 1)
    {
      case FieldTypes.ftUnknown:
label_9:
        return CompareValuesHelper.CompareStringValues(value1, value2);
      case FieldTypes.ftString:
label_4:
        return CompareValuesHelper.CompareIntValues(value1, value2);
      case FieldTypes.ftInteger:
        return CompareValuesHelper.CompareFloatValues(value1, value2);
      case FieldTypes.ftDouble:
        return CompareValuesHelper.CompareDateTimeValues(value1, value2);
      default:
        switch (fieldType - 12)
        {
          case FieldTypes.ftUnknown:
            return CompareValuesHelper.CompareBoolValues(value1, value2);
          case FieldTypes.ftString:
            return CompareValuesHelper.CompareMeasuredValues(value1, value2);
          case FieldTypes.ftInteger:
            goto label_4;
          case FieldTypes.ftDateTime:
            goto label_9;
          default:
            return value1.Equals(value2);
        }
    }
  }
}
