
// Type: Intermech.ApplicabilityModesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    public class ApplicabilityModesHelper
    {
      public static string GetCaption(ApplicabilityModes mode)
      {
        return EnumTypeHelper.GetCaption((Enum) mode);
      }

      public static ApplicabilityModes GetApplicabilityMode(string s)
      {
        return (ApplicabilityModes) EnumTypeHelper.GetEnumValue(typeof (ApplicabilityModes), s);
      }
    }
}
