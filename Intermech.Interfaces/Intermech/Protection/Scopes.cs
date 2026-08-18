
// Type: Intermech.Protection.Scopes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Protection
{
    internal static class Scopes
    {
      public static string LocalScope
      {
        get
        {
          return "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspscope>    <license_manager hostname=\"localhost\" /></haspscope>";
        }
      }

      public static string ById(ulong haspId)
      {
        return $"<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspscope>    <hasp id=\"{haspId}\" /></haspscope>";
      }

      public static string FormatHaspId
      {
        get
        {
          return "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspformat root=\"haspscope\">    <hasp>        <attribute name=\"id\" />    </hasp></haspformat>";
        }
      }
    }
}
