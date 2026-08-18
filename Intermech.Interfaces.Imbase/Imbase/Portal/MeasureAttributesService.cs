// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.MeasureAttributesService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Portal;

internal class MeasureAttributesService
{
  /// <summary>
  /// Кэш единиц измерения для атрибутов конкретного объекта
  /// </summary>
  private Dictionary<long, Dictionary<int, long>> _measureAttributes = new Dictionary<long, Dictionary<int, long>>();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="recKey"></param>
  /// <param name="attributeID"></param>
  /// <returns></returns>
  public long GetMeasureID(int recKey, int attributeID)
  {
    Dictionary<int, long> dictionary = (Dictionary<int, long>) null;
    return this._measureAttributes.TryGetValue((long) recKey, out dictionary) && dictionary.ContainsKey(attributeID) ? dictionary[attributeID] : 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="recKey"></param>
  /// <param name="attributeID"></param>
  public void AddMeasure(long recKey, int attributeID, long measureID)
  {
    Dictionary<int, long> dictionary = (Dictionary<int, long>) null;
    if (!this._measureAttributes.TryGetValue(recKey, out dictionary))
    {
      dictionary = new Dictionary<int, long>();
      this._measureAttributes.Add(recKey, dictionary);
    }
    if (dictionary.ContainsKey(attributeID))
      return;
    dictionary.Add(attributeID, measureID);
  }
}
