
// Type: Intermech.Interfaces.LCStepOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech.Interfaces
{
    public class LCStepOptionsHelper
    {
      public static string GetCaption(LCStepOptions option) => EnumTypeHelper.GetCaption((Enum) option);

      public static LCStepOptions GetLCStepOption(string s)
      {
        return (LCStepOptions) EnumTypeHelper.GetEnumValue(typeof (LCStepOptions), s);
      }

      public static string GetCaptions(LCStepOptions options)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if ((LCStepOptions.DisableParallelVersions & options) == LCStepOptions.DisableParallelVersions)
          stringBuilder.Append(LCStepOptionsHelper.GetCaption(LCStepOptions.DisableParallelVersions) + ", ");
        if ((LCStepOptions.BaseVersion & options) == LCStepOptions.BaseVersion)
          stringBuilder.Append(LCStepOptionsHelper.GetCaption(LCStepOptions.BaseVersion) + ", ");
        if ((LCStepOptions.RestoreSoftInstantiation & options) == LCStepOptions.RestoreSoftInstantiation)
          stringBuilder.Append(LCStepOptionsHelper.GetCaption(LCStepOptions.RestoreSoftInstantiation) + ", ");
        if ((LCStepOptions.DisableContextParallelVersions & options) == LCStepOptions.DisableContextParallelVersions)
          stringBuilder.Append(LCStepOptionsHelper.GetCaption(LCStepOptions.DisableContextParallelVersions) + ", ");
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }
    }
}
