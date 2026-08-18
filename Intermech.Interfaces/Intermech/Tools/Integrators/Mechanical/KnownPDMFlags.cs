
// Type: Intermech.Tools.Integrators.Mechanical.KnownPDMFlags
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Tools.Integrators.Mechanical
{
    /// <summary>
    /// Описывает известные и поддерживаемые в IPS значения PDM-флагов
    /// </summary>
    public static class KnownPDMFlags
    {
      public const int None = 0;
      public const int UseConfigurationWithoutModel = 1;
      public const int IngoreAllConfigurations = 2;
      public const int TotallyIgnoreModel = 3;
      public const int SatelliteModelWithoutConfigurations = 4;
      public const int SatelliteModelWithConfigurations = 5;
      public const int IgnoreThisConfigurationOnly = 6;
    }
}
