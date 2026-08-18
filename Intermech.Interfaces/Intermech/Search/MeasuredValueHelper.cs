
// Type: Intermech.Search.MeasuredValueHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search
{
    public static class MeasuredValueHelper
    {
      public static long GetDefaultMeasureVerisonIDFromValidationRule(string validationRule)
      {
        string[] strArray = !string.IsNullOrEmpty(validationRule) ? validationRule.Split(',') : throw new ArgumentException();
        if (strArray.Length > 1)
        {
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(strArray[1]);
          if (descriptor != null && !descriptor.Empty)
            return descriptor.MeasureID;
        }
        return 0;
      }

      public static MeasureDescriptor[] GetMeasureDescriptorsForAttributeType(int attributeTypeID)
      {
        IMSAttributeType attributeType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttributeType(attributeTypeID) : throw new ArgumentException();
        if (attributeType == null || MeasureHelper.Measures == null)
          return new MeasureDescriptor[0];
        return ObjectHelper.IsUnknownObjectID(attributeType.SizeType) || attributeType.SizeType == -1L ? MeasureHelper.Measures : ((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures).Where<MeasureDescriptor>((Func<MeasureDescriptor, bool>) (o => o.PhysicalQuantityID == attributeType.SizeType)).ToArray<MeasureDescriptor>();
      }

      public static long GetDefaultMeasureVersionID(MeasureDescriptor[] measureDescriptors)
      {
        if (measureDescriptors == null || measureDescriptors.Length == 0)
          throw new ArgumentException();
        if (measureDescriptors.Length == 1)
          return measureDescriptors[0].MeasureID;
        MeasureDescriptor firstMeasureDescriptor = measureDescriptors[0];
        if (((IEnumerable<MeasureDescriptor>) measureDescriptors).All<MeasureDescriptor>((Func<MeasureDescriptor, bool>) (o => o.PhysicalQuantityID == firstMeasureDescriptor.PhysicalQuantityID)))
        {
          MeasureDescriptor measureDescriptor = ((IEnumerable<MeasureDescriptor>) measureDescriptors).FirstOrDefault<MeasureDescriptor>((Func<MeasureDescriptor, bool>) (o => o.IsDefault));
          if (measureDescriptor != null)
            return measureDescriptor.MeasureID;
        }
        return 0;
      }

      public static bool TryParse(
        string text,
        out MeasuredValue result,
        long defaultMeasureVersionID,
        MeasureDescriptor[] measureDescriptors)
      {
        try
        {
          double result1 = 0.0;
          if (double.TryParse(text, out result1))
          {
            if (ObjectHelper.IsUnknownObjectVersionID(defaultMeasureVersionID) && measureDescriptors != null && measureDescriptors.Length != 0)
              defaultMeasureVersionID = MeasuredValueHelper.GetDefaultMeasureVersionID(measureDescriptors);
            result = new MeasuredValue(result1, defaultMeasureVersionID);
          }
          else
            result = MeasureHelper.ConvertToMeasuredValue(text);
          return true;
        }
        catch
        {
          result = (MeasuredValue) null;
          return false;
        }
      }
    }
}
