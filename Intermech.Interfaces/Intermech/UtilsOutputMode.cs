
// Type: Intermech.UtilsOutputMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>Способ вывода результатов операции</summary>
    [Flags]
    public enum UtilsOutputMode
    {
      None = 0,
      Console = 1,
      LogFile = 2,
      Both = LogFile | Console, // 0x00000003
    }
}
