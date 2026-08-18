
// Type: Intermech.Interfaces.Configuration.AppSettingsBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Configuration
{
    /// <summary>Базовый класс для хранения параметров</summary>
    [Serializable]
    public abstract class AppSettingsBase
    {
      /// <summary>
      /// 
      /// </summary>
      protected virtual void InitializeData()
      {
      }

      /// <summary>
      /// 
      /// </summary>
      public AppSettingsBase() => this.InitializeData();
    }
}
