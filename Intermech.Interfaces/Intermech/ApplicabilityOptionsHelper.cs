
// Type: Intermech.ApplicabilityOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech
{
    public class ApplicabilityOptionsHelper
    {
      public static string GetCaption(ApplicabilityOptions option)
      {
        return EnumTypeHelper.GetCaption((Enum) option);
      }

      public static ApplicabilityOptions GetApplicabilityOption(string s)
      {
        return (ApplicabilityOptions) EnumTypeHelper.GetEnumValue(typeof (ApplicabilityOptions), s);
      }

      public static string GetCaptions(ApplicabilityOptions options)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if ((ApplicabilityOptions.EnableMultiLink & options) == ApplicabilityOptions.EnableMultiLink)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.EnableMultiLink) + ", ");
        if ((ApplicabilityOptions.ChangeLCStep & options) == ApplicabilityOptions.ChangeLCStep)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.ChangeLCStep) + ", ");
        if ((ApplicabilityOptions.DefaultRelation & options) == ApplicabilityOptions.DefaultRelation)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.DefaultRelation) + ", ");
        if ((ApplicabilityOptions.SyncIdentifiers & options) == ApplicabilityOptions.SyncIdentifiers)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.SyncIdentifiers) + ", ");
        if ((ApplicabilityOptions.SyncCheckin & options) == ApplicabilityOptions.SyncCheckin)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.SyncCheckin) + ", ");
        if ((ApplicabilityOptions.SoftInstantiation & options) == ApplicabilityOptions.SoftInstantiation)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.SoftInstantiation) + ", ");
        if ((ApplicabilityOptions.DisableCopy2Version & options) == ApplicabilityOptions.DisableCopy2Version)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.DisableCopy2Version) + ", ");
        if ((ApplicabilityOptions.AutoInstantiation & options) == ApplicabilityOptions.AutoInstantiation)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.AutoInstantiation) + ", ");
        if ((ApplicabilityOptions.CopyAttributes2Child & options) == ApplicabilityOptions.CopyAttributes2Child)
          stringBuilder.Append(ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.CopyAttributes2Child) + ", ");
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }
    }
}
