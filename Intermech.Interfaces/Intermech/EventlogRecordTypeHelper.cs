
// Type: Intermech.EventlogRecordTypeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.Collections.Specialized;


namespace Intermech
{
    public class EventlogRecordTypeHelper
    {
      private static ListDictionary lst = new ListDictionary();

      static EventlogRecordTypeHelper()
      {
        EventlogRecordTypeHelper.lst[(object) EventlogRecordType.AccessDenied] = (object) LocalizationHolder.rm.GetString("Interfaces_124");
        EventlogRecordTypeHelper.lst[(object) EventlogRecordType.AccessGranted] = (object) LocalizationHolder.rm.GetString("Interfaces_125");
        EventlogRecordTypeHelper.lst[(object) EventlogRecordType.Information] = (object) LocalizationHolder.rm.GetString("Interfaces_126");
        EventlogRecordTypeHelper.lst[(object) EventlogRecordType.Warning] = (object) LocalizationHolder.rm.GetString("Interfaces_127");
        EventlogRecordTypeHelper.lst[(object) EventlogRecordType.Error] = (object) LocalizationHolder.rm.GetString("Interfaces_128");
      }

      public static string GetCaption(EventlogRecordType mode)
      {
        return EventlogRecordTypeHelper.lst[(object) mode] as string;
      }
    }
}
