
// Type: Intermech.Interfaces.ObjectsVisibilityFlags
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Набор флажков, управляющих видимостью объекта в составе
    /// </summary>
    [Flags]
    [Serializable]
    public enum ObjectsVisibilityFlags
    {
      /// <summary>Настроек видимости нет</summary>
      None = 0,
      /// <summary>Объект можно отображать в составе</summary>
      Visible = 1,
      /// <summary>Объект запрещено отображать в составе</summary>
      Hidden = 4,
    }
}
