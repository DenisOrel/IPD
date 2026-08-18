
// Type: Intermech.Client.Core.Show.Net.DwgLayer.DwgLayerTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Show;
using System;
using System.Collections;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.DwgLayer;

/// <summary>массив слоёв</summary>
[DebuggerDisplay("{Length}")]
public class DwgLayerTable : ILayerTable, IEnumerable
{
  /// <summary>Вернуть ссылку на интерфейс IEnumerator</summary>
  /// <returns>Ссылка на интерфейс IEnumerator</returns>
  public IEnumerator GetEnumerator() => this.Array.GetEnumerator();

  /// <summary>длинна массива слоёв</summary>
  public int Length => this.Array.Length;

  /// <summary>массив слоёв</summary>
  public ILayer[] Array { get; }

  /// <summary>получить по индексу в массиве слой</summary>
  /// <param name="index">индекс в таблице</param>
  /// <returns>слой</returns>
  public ILayer this[int index]
  {
    get
    {
      if (index >= 0 && index < this.Array.Length)
        return this.Array[index];
      throw new IndexOutOfRangeException();
    }
  }

  /// <summary>пересчитать границы включённых слоёв</summary>
  public RectangleD Bounds
  {
    get
    {
      RectangleD bounds = RectangleD.Empty;
      foreach (ILayer layer in this.Array)
      {
        if (layer.Visible && !(layer.Bound == RectangleD.Empty))
          bounds = RectangleD.Union(bounds == RectangleD.Empty ? layer.Bound : bounds, layer.Bound);
      }
      return bounds;
    }
  }

  /// <summary>габариты при всех слоях</summary>
  public RectangleD BoundsAll
  {
    get
    {
      RectangleD boundsAll = RectangleD.Empty;
      foreach (ILayer layer in this.Array)
      {
        if (!(layer.Bound == RectangleD.Empty))
          boundsAll = RectangleD.Union(boundsAll == RectangleD.Empty ? layer.Bound : boundsAll, layer.Bound);
      }
      return boundsAll;
    }
  }

  /// <summary>создать массив слоёв</summary>
  /// <param name="strings">массив имён слоёв</param>
  internal DwgLayerTable(string[] strings)
  {
    this.Array = new ILayer[strings.Length];
    for (int index = 0; index < strings.Length; ++index)
      this.Array[index] = (ILayer) new DwgLayerObject(index, strings[index]);
  }

  internal object CurrentObject { get; set; }

  internal void ClearBoundsAll()
  {
    foreach (ILayer layer in this.Array)
    {
      if (layer is DwgLayerObject dwgLayerObject)
        dwgLayerObject.SetBound(RectangleD.Empty);
    }
  }
}
