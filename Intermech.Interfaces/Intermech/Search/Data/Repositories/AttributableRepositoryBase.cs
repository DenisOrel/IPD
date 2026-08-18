
// Type: Intermech.Search.Data.Repositories.AttributableRepositoryBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.Data.Repositories
{
    public abstract class AttributableRepositoryBase
    {
      protected object GetAttributeValue(IDBAttributable attributable, AttributeValues attributeValue)
      {
        object[] source = attributeValue.Values;
        if (attributeValue.AttributeID == -16)
          return (object) ((bool) attributeValue.Values[0] ? 1L : 0L);
        if (attributeValue.AttributeType == FieldTypes.ftBlob || attributeValue.AttributeType == FieldTypes.ftFile || attributeValue.AttributeType == FieldTypes.ftShortBlob)
          source = this.GetBlobInfo(attributable, attributeValue.AttributeID).ToArray();
        return attributeValue.MultipleValued == MultiValueModes.SingleValue || attributeValue.MultipleValued == MultiValueModes.SingleValueFromList ? (source == null ? (object) null : this.ConvertAttributeValue(source[0], attributeValue.AttributeType)) : (source == null ? (object) new List<object>(0) : (object) ((IEnumerable<object>) source).Select<object, object>((System.Func<object, object>) (o => this.ConvertAttributeValue(o, attributeValue.AttributeType))).ToList<object>());
      }

      protected object ConvertAttributeValue(object value, FieldTypes dataType)
      {
        if (value is DBNull)
          return (object) null;
        return dataType == FieldTypes.ftGuid && value is string ? (object) Guid.Parse((string) value) : value;
      }

      protected List<object> GetBlobInfo(IDBAttributable attributable, int attributeTypeID)
      {
        List<object> blobInfo1 = new List<object>();
        IDBAttribute attributeById = attributable.GetAttributeByID(attributeTypeID);
        int num = 0;
        for (int valuesCount = attributeById.ValuesCount; num < valuesCount; ++num)
        {
          attributeById.Index = num;
          IBlobReader blobReader = attributeById as IBlobReader;
          try
          {
            BlobInformation blobInformation = blobReader.OpenBlob(0);
            BlobInfo blobInfo2 = new BlobInfo()
            {
              ArcMethod = blobInformation.ArcMethod,
              AuthorVersionID = blobInformation.Author,
              BlobID = blobInformation.BlobID,
              FileName = blobInformation.FileName,
              FileType = blobInformation.FileType,
              ModifyDate = blobInformation.ModifyDate,
              Note = blobInformation.Note,
              PackedFileSize = blobInformation.PackedFileSize,
              RealFileSize = blobInformation.RealFileSize
            };
            blobInfo1.Add((object) blobInfo2);
          }
          finally
          {
            blobReader.CloseBlob();
          }
        }
        return blobInfo1;
      }

      protected IAttributeCollection CreateAttributeCollectionFromDataRow(
        DataRow dataRow,
        List<int> attributeTypeIds)
      {
        AttributeCollection collectionFromDataRow = new AttributeCollection();
        IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
        for (int index = 0; index < dataRow.ItemArray.Length; ++index)
        {
          int attributeTypeId = attributeTypeIds[index];
          object obj = attributeValueConverter.Convert(dataRow[index], attributeTypeId);
          collectionFromDataRow.Add(new _Attribute(attributeTypeId)
          {
            Value = obj
          });
        }
        return (IAttributeCollection) collectionFromDataRow;
      }
    }
}
