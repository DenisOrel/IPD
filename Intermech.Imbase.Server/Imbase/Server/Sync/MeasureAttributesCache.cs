// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.MeasureAttributesCache
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class MeasureAttributesCache
{
  private Dictionary<int, Dictionary<int, long>> _measureAttributes = new Dictionary<int, Dictionary<int, long>>();

  public long GetMeasureID(int recKey, int attributeID)
  {
    Dictionary<int, long> dictionary;
    return this._measureAttributes.TryGetValue(recKey, out dictionary) && dictionary.ContainsKey(attributeID) ? dictionary[attributeID] : 0L;
  }

  public void AddMeasure(int recKey, int attributeID, long measureID)
  {
    Dictionary<int, long> dictionary;
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
