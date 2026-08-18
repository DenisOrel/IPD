// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapToolTipText
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System.ComponentModel;


namespace Intermech.Map
{
    /// <summary>сведения о примитиве для TipText</summary>
    public interface IMapToolTipText
    {
      /// <summary>сформировать сведения о примитиве</summary>
      /// <returns>сведения о примитиве</returns>
      string GenerateToolTipText();

      /// <summary>сведения о примитиве</summary>
      [Description("A string to be displayed in a tooltip.")]
      string ToolTipText { get; set; }
    }
}
