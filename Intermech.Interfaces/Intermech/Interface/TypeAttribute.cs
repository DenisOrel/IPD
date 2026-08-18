
// Type: Intermech.Interface.TypeAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace Intermech.Interface
{
    public sealed class TypeAttribute
    {
      /// <summary>ID атрибута</summary>
      public int AttributeID { get; private set; }

      /// <summary>Тиа данных</summary>
      public FieldTypes FieldType { get; private set; }

      /// <summary>Флаг вычисляемого атрибута</summary>
      public bool Calculated { get; private set; }

      /// <summary>Значение по умолчанию</summary>
      public string DefaultValue { get; private set; }

      public TypeAttribute(IMSAttribute4 imsAttribute4)
      {
        this.AttributeID = imsAttribute4.AttributeID;
        this.FieldType = imsAttribute4.FieldType;
        this.DefaultValue = imsAttribute4.DefaultValue;
        this.Calculated = imsAttribute4.Computed != 0;
      }

      public AttributeRecord ConvertTo(IUserSession session, Dictionary<long, string> objectsCache = null)
      {
        AttributeRecord attributeRecord = new AttributeRecord(this.AttributeID);
        if (string.IsNullOrEmpty(this.DefaultValue))
          return attributeRecord;
        switch (this.FieldType)
        {
          case FieldTypes.ftString:
          case FieldTypes.ftGuid:
            attributeRecord.StringValue = (object) this.DefaultValue;
            break;
          case FieldTypes.ftInteger:
            long result1;
            if (long.TryParse(this.DefaultValue, out result1))
            {
              attributeRecord.IntegerValue = (object) result1;
              break;
            }
            break;
          case FieldTypes.ftDouble:
            double result2;
            if (double.TryParse(this.DefaultValue, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
            {
              attributeRecord.DoubleValue = (object) result2;
              break;
            }
            break;
          case FieldTypes.ftDateTime:
            if (this.DefaultValue == Consts.CurrentDateFunction)
            {
              attributeRecord.DateValue = (object) DateTime.UtcNow;
              break;
            }
            break;
          case FieldTypes.ftObjectLink:
            long result3;
            if (long.TryParse(this.DefaultValue, out result3))
            {
              string str = (string) null;
              if (objectsCache == null || !objectsCache.TryGetValue(result3, out str))
              {
                QuickObjectInfo objectInfo = session.GetObjectInfo(result3);
                if (!objectInfo.Empty)
                {
                  str = objectInfo.Caption;
                  objectsCache?.Add(result3, str);
                }
              }
              attributeRecord.IntegerValue = (object) result3;
              attributeRecord.StringValue = (object) str;
              break;
            }
            break;
          case FieldTypes.ftBoolean:
            bool result4;
            if (bool.TryParse(this.DefaultValue, out result4))
            {
              attributeRecord.IntegerValue = (object) (result4 ? 1 : 0);
              break;
            }
            break;
          case FieldTypes.ftMeasured:
            MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(this.DefaultValue);
            if (measuredValue != null)
            {
              MeasuredValue baseMeasure = MeasureHelper.ConvertToBaseMeasure(measuredValue);
              if (CompareValuesHelper.NormalizedValue((object) baseMeasure.Value) != null)
              {
                attributeRecord.DoubleValue = (object) baseMeasure.Value;
                attributeRecord.IntegerValue = (object) baseMeasure.MeasureID;
                attributeRecord.StringValue = (object) measuredValue.Caption;
                break;
              }
              break;
            }
            break;
        }
        return attributeRecord;
      }
    }
}
