// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IRedNoteStyle
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System.ComponentModel;


namespace Intermech.Map
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
