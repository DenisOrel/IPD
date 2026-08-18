
// Type: Intermech.Navigator.UnitedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator;

/// <summary>
/// Реализует коллекцию элементов навигации, скомбинированную из
/// нескольких других таких коллекций.
/// </summary>
public class UnitedItems : ISelectedItems, ISimpleSelectedItems
{
  private ISelectedItems[] _clusters;
  private int _count;
  private bool _isCollage;
  private int[] _ranges;

  /// <summary>
  /// Создает коллекцию элементов навигации из произвольного количества
  /// других коллекций.
  /// </summary>
  /// <param name="clusters">Массив коллекций, которые войдут в состав создаваемой</param>
  public UnitedItems(params ISelectedItems[] clusters)
  {
    this._clusters = clusters;
    this._isCollage = false;
    this._count = 0;
    this._ranges = new int[clusters.Length + 1];
    this._ranges[0] = 0;
    for (int index = 0; index < this._clusters.Length; ++index)
    {
      this._isCollage |= this._clusters[index].IsCollage;
      this._count += this._clusters[index].Count;
      this._ranges[index + 1] = this._count;
    }
  }

  public bool IsCollage => this._isCollage;

  public int Count => this._count;

  public object GetItemData(int index, Type dataFormat)
  {
    for (int index1 = 0; index1 < this._clusters.Length; ++index1)
    {
      if (index < this._ranges[index1 + 1])
        return this._clusters[index1].GetItemData(index - this._ranges[index1], dataFormat);
    }
    throw new ArgumentOutOfRangeException(sc_4228.ssp_imclient_4229());
  }

  public INodeID GetItemID(int index)
  {
    for (int index1 = 0; index1 < this._clusters.Length; ++index1)
    {
      if (index < this._ranges[index1 + 1])
        return this._clusters[index1].GetItemID(index - this._ranges[index1]);
    }
    throw new ArgumentOutOfRangeException(sc_4228.ssp_imclient_4230());
  }

  public object GetParentData(int index, Type dataFormat)
  {
    for (int index1 = 0; index1 < this._clusters.Length; ++index1)
    {
      if (index < this._ranges[index1 + 1])
        return this._clusters[index1].GetParentData(index - this._ranges[index1], dataFormat);
    }
    throw new ArgumentOutOfRangeException(sc_4228.ssp_imclient_4231());
  }

  public NodeIDPath GetParentPath(int index)
  {
    for (int index1 = 0; index1 < this._clusters.Length; ++index1)
    {
      if (index < this._ranges[index1 + 1])
        return this._clusters[index1].GetParentPath(index - this._ranges[index1]);
    }
    throw new ArgumentOutOfRangeException(sc_4228.ssp_imclient_4232());
  }
}
