
// Type: Intermech.Interfaces.Briefcase.BriefcaseLocation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Класс для хранения выбора портфеля пользователем</summary>
    [Serializable]
    public class BriefcaseLocation
    {
      /// <summary>На каком компьютере</summary>
      public BriefcaseLocation.Computer ComputerLocation;
      /// <summary>Папка портфеля</summary>
      public string Path;

      public BriefcaseLocation(BriefcaseLocation.Computer computerLocation, string path)
      {
        this.ComputerLocation = computerLocation;
        this.Path = path;
      }

      public BriefcaseLocation()
      {
        this.ComputerLocation = BriefcaseLocation.Computer.Local;
        this.Path = string.Empty;
      }

      public enum Computer
      {
        Server,
        Local,
      }
    }
}
