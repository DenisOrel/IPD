
// Type: Intermech.Interfaces.IRedNoteStyle
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>стили коментариев</summary>
    public enum IRedNoteStyle
    {
      /// <summary>без рамки</summary>
      [Description("без рамки")] None,
      /// <summary>рамка без фасок</summary>
      [Description("рамка без фасок")] Box,
      /// <summary>рамка с фаской</summary>
      [Description("рамка с фаской")] BoxFacet,
      /// <summary>рамка с закруглением</summary>
      [Description("рамка с закруглением")] BoxBluntPoint,
      /// <summary>старый стиль</summary>
      [Description("старый стиль")] OldStyle,
    }
}
