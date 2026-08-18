
// Type: Intermech.Interfaces.EnvironmentConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public class EnvironmentConsts
    {
      /// <summary>Кэшированное имя компьютера</summary>
      private static string _MachineName = string.Empty;

      /// <summary>Имя данного компьютера из Environment.MachineName</summary>
      public static string MachineName
      {
        get
        {
          if (EnvironmentConsts._MachineName == string.Empty)
            EnvironmentConsts._MachineName = Environment.MachineName;
          return EnvironmentConsts._MachineName;
        }
      }
    }
}
