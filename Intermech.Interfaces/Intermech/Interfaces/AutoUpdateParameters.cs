
// Type: Intermech.Interfaces.AutoUpdateParameters
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Параметры опции для системы автообновления</summary>
    public class AutoUpdateParameters : Attribute
    {
      /// <summary>Используется в скриптах автообновления</summary>
      public bool UsedInScripts { get; private set; }

      /// <summary>Создает объект</summary>
      /// <param name="usedInScripts">Опция используется в скриптах автообновления</param>
      public AutoUpdateParameters(bool usedInScripts) => this.UsedInScripts = usedInScripts;
    }
}
