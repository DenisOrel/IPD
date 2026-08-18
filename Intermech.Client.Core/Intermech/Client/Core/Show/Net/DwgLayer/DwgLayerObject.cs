
// Type: Intermech.Client.Core.Show.Net.DwgLayer.DwgLayerObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Show;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.DwgLayer;

/// <summary>слой чертежа</summary>
[DebuggerDisplay("[{Visible ? 1 : 0}] {Name} ")]
public class DwgLayerObject : ILayer, IDllIndex
{
  /// <summary>положение слоя в DLL </summary>
  public int Index { get; }

  /// <summary>имя слоя</summary>
  public string Name { get; }

  /// <summary>состояние слоя</summary>
  public bool Visible { get; set; } = true;

  /// <summary>габариты слоя</summary>
  public RectangleD Bound { get; private set; }

  /// <summary>обновить габариты слоя</summary>
  /// <param name="box">новые габариты слоя</param>
  internal void SetBound(RectangleD box) => this.Bound = box;

  /// <summary>создать слой</summary>
  /// <param name="index">индекс в DLL</param>
  /// <param name="name">имя слоя</param>
  internal DwgLayerObject(int index, string name)
  {
    this.Index = index;
    this.Name = name;
  }
}
