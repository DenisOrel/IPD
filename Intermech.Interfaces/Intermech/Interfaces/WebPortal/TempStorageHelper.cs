
// Type: Intermech.Interfaces.WebPortal.TempStorageHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Interfaces.WebPortal
{
    public static class TempStorageHelper
    {
      public static string CreatePathFromGuid(string rootPath, string unitGuid)
      {
        string path1 = rootPath;
        StringReader stringReader = new StringReader(unitGuid.Substring(0, 8));
        while (stringReader.Peek() >= 0)
        {
          char[] buffer = new char[2];
          stringReader.ReadBlock(buffer, 0, 2);
          path1 = Path.Combine(path1, new string(buffer));
        }
        return Path.Combine(path1, unitGuid.Substring(8));
      }
    }
}
