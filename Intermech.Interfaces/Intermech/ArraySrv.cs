
// Type: Intermech.ArraySrv
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Drawing;
using System.IO;


namespace Intermech
{
    public class ArraySrv
    {
      public static bool Compare(byte[] a, byte[] b)
      {
        if (a == null || b == null)
          return a == b;
        if (a.Length != b.Length)
          return false;
        for (int index = 0; index < a.Length; ++index)
        {
          if ((int) a[index] != (int) b[index])
            return false;
        }
        return true;
      }

      public static byte[] IconToArray(Icon icon)
      {
        using (MemoryStream outputStream = new MemoryStream())
        {
          icon?.Save((Stream) outputStream);
          return outputStream.ToArray();
        }
      }
    }
}
