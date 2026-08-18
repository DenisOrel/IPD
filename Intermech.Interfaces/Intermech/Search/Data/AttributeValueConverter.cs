
// Type: Intermech.Search.Data.AttributeValueConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.Data
{
    /// <summary>Стандартный конвертер значения атрибута</summary>
    public class AttributeValueConverter : IAttributeValueConverter
    {
      private LazyService<IAttributeTypeRepository> _attributeTypeRepository = new LazyService<IAttributeTypeRepository>();

      /// <summary>Конвертировать</summary>
      /// <param name="rawValue">Необработанное значение</param>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <returns></returns>
      public object Convert(object rawValue, int attributeTypeID)
      {
        if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
          throw new ArgumentException();
        if (this.IsSystemAttribute(attributeTypeID))
          return this.ConvertAttributeValue(rawValue, (ObligatoryObjectAttributes) attributeTypeID);
        IMSAttributeType imsAttributeType = this._attributeTypeRepository.Value.Find(attributeTypeID);
        if (imsAttributeType == null)
          throw new Exception();
        return this.ConvertAttributeValue(rawValue, imsAttributeType.FieldType, !imsAttributeType.Options.HasFlag((Enum) AttributeOptions.DisableNulls));
      }

      public object GetAttributeDefaultValue(int attributeTypeID)
      {
        if (this.IsSystemAttribute(attributeTypeID))
          return this.GetAttributeDefaultValue((ObligatoryObjectAttributes) attributeTypeID);
        IMSAttributeType imsAttributeType = this._attributeTypeRepository.Value.Find(attributeTypeID);
        if (imsAttributeType == null)
          throw new Exception();
        return !imsAttributeType.Options.HasFlag((Enum) AttributeOptions.DisableNulls) ? (object) null : this.GetAttributeDefaultValue(imsAttributeType.FieldType);
      }

      private bool IsSystemAttribute(int attributeTypeID)
      {
        return ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attributeTypeID);
      }

      private FieldTypes GetDataType(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return ObligatoryObjectAttributesHelper.GetDataType(obligatoryObjectAttribute);
      }

      private object GetAttributeDefaultValue(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        switch (obligatoryObjectAttribute)
        {
          case ObligatoryObjectAttributes.F_ELEMENT_STATUSES:
            return (object) null;
          case ObligatoryObjectAttributes.F_ATTRIBUTE_ID:
            return (object) 0;
          case ObligatoryObjectAttributes.F_USER_ID:
          case ObligatoryObjectAttributes.F_PART_ID:
          case ObligatoryObjectAttributes.F_PROJ_ID:
          case ObligatoryObjectAttributes.F_PROJECT_ID:
          case ObligatoryObjectAttributes.F_OWNER_ID:
          case ObligatoryObjectAttributes.F_CHKOUT_BY:
          case ObligatoryObjectAttributes.F_ID:
          case ObligatoryObjectAttributes.F_OBJECT_ID:
            return (object) 0L;
          case ObligatoryObjectAttributes.F_RELATION_TYPE:
            return (object) -1;
          case ObligatoryObjectAttributes.F_PRJLINK_ID:
            return (object) 0L;
          case ObligatoryObjectAttributes.F_BASE_VERSION:
            return (object) 0L;
          case ObligatoryObjectAttributes.F_LEVEL_ID:
            return (object) 0;
          case ObligatoryObjectAttributes.F_OBJECT_TYPE:
            return (object) -1;
          case ObligatoryObjectAttributes.F_VERSION_ID:
            return (object) 0;
          case ObligatoryObjectAttributes.F_LC_STEP:
            return (object) -1;
          default:
            return this.GetAttributeDefaultValue(this.GetDataType(obligatoryObjectAttribute));
        }
      }

      private object GetAttributeDefaultValue(FieldTypes dataType)
      {
        switch (dataType)
        {
          case FieldTypes.ftUnknown:
          case FieldTypes.ftString:
          case FieldTypes.ftShortBlob:
          case FieldTypes.ftFile:
          case FieldTypes.ftExternalLink:
          case FieldTypes.ftPassword:
          case FieldTypes.ftBlob:
          case FieldTypes.ftMeasured:
            return (object) null;
          case FieldTypes.ftInteger:
          case FieldTypes.ftAutoInc:
            return (object) 0L;
          case FieldTypes.ftDouble:
            return (object) 0.0;
          case FieldTypes.ftDateTime:
            return (object) new DateTime();
          case FieldTypes.ftObjectLink:
            return (object) 0L;
          case FieldTypes.ftBoolean:
            return (object) false;
          case FieldTypes.ftGuid:
            return (object) new Guid();
          default:
            return (object) null;
        }
      }

      private object ConvertAttributeValue(
        object value,
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        switch (obligatoryObjectAttribute)
        {
          case ObligatoryObjectAttributes.F_ELEMENT_STATUSES:
            return (object) (value as byte[]);
          case ObligatoryObjectAttributes.F_RELATION_TYPE:
            return (object) DataSetProcessor.GetInt32Value(value, -1);
          case ObligatoryObjectAttributes.F_LEVEL_ID:
            return (object) DataSetProcessor.GetInt32Value(value, 0);
          case ObligatoryObjectAttributes.F_OBJECT_TYPE:
            return (object) DataSetProcessor.GetInt32Value(value, -1);
          case ObligatoryObjectAttributes.F_VERSION_ID:
            return (object) DataSetProcessor.GetInt32Value(value, 0);
          case ObligatoryObjectAttributes.F_LC_STEP:
            return (object) DataSetProcessor.GetInt32Value(value, -1);
          default:
            return this.ConvertAttributeValue(value, this.GetDataType(obligatoryObjectAttribute));
        }
      }

      private object ConvertAttributeValue(object value, FieldTypes dataType, bool isNullable = false)
      {
        if (((value == null ? 1 : (value is DBNull ? 1 : 0)) & (isNullable ? 1 : 0)) != 0)
          return (object) null;
        switch (dataType)
        {
          case FieldTypes.ftUnknown:
          case FieldTypes.ftShortBlob:
          case FieldTypes.ftFile:
          case FieldTypes.ftExternalLink:
          case FieldTypes.ftPassword:
          case FieldTypes.ftBlob:
          case FieldTypes.ftMeasured:
            return value;
          case FieldTypes.ftString:
            return (object) DataSetProcessor.GetStringValue(value, this.GetAttributeDefaultValue(dataType) as string);
          case FieldTypes.ftInteger:
          case FieldTypes.ftAutoInc:
            return (object) DataSetProcessor.GetInt64Value(value, (long) this.GetAttributeDefaultValue(dataType));
          case FieldTypes.ftDouble:
            return (object) DataSetProcessor.GetDoubleValue(value, (double) this.GetAttributeDefaultValue(dataType));
          case FieldTypes.ftDateTime:
            return (object) DataSetProcessor.GetDateTimeValue(value, (DateTime) this.GetAttributeDefaultValue(dataType));
          case FieldTypes.ftObjectLink:
            return (object) DataSetProcessor.GetInt64Value(value, (long) this.GetAttributeDefaultValue(dataType));
          case FieldTypes.ftBoolean:
            if (value != null)
            {
              int result = 0;
              if (int.TryParse(value.ToString(), out result))
                return (object) (result != 0);
            }
            return (object) DataSetProcessor.GetBooleanValue(value, (bool) this.GetAttributeDefaultValue(dataType));
          case FieldTypes.ftGuid:
            return (object) DataSetProcessor.GetGuidValue(value, (Guid) this.GetAttributeDefaultValue(dataType));
          default:
            return (object) null;
        }
      }
    }
}
