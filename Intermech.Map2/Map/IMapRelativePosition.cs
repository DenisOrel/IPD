// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapRelativePosition
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System.Drawing;


namespace Intermech.Map
{
    /// <summary>пересчёт положения объектов относительно элемента в документе</summary>
    public interface IMapRelativePosition
    {
      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      IMapRelative Relative { get; set; }

      /// <summary>ID элемента базового элемента</summary>
      string RelativeId { get; set; }

      /// <summary>получить базовую точку элемента</summary>
      PointF BasePoint { get; }
    }
}
