
// Type: Intermech.Remoting.Compression.PropReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Remoting.Compression
{
    internal static class PropReader
    {
      public static bool ReadBoolean(string value, bool defaultValue)
      {
        switch (value)
        {
          case "1":
            return true;
          case "0":
            return false;
          default:
            return defaultValue;
        }
      }
    }
}
