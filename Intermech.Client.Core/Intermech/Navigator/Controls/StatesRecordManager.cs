
// Type: Intermech.Navigator.Controls.StatesRecordManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Navigator.Controls;

internal class StatesRecordManager
{
  private IList _entries = (IList) new ArrayList();

  public void Clear() => this._entries.Clear();

  public void InsertColumn(int index, bool value)
  {
    for (int index1 = 0; index1 < this._entries.Count; ++index1)
      ((StatesManagerEntry) this._entries[index1]).Record.InsertColumn(index, value);
  }

  public void RemoveColumn(int index)
  {
    for (int index1 = 0; index1 < this._entries.Count; ++index1)
      ((StatesManagerEntry) this._entries[index1]).Record.RemoveColumn(index);
  }

  public StatesRecord Share(StatesRecord record)
  {
    StatesRecord statesRecord = (StatesRecord) null;
    for (int index = 0; index < this._entries.Count; ++index)
    {
      StatesManagerEntry entry = (StatesManagerEntry) this._entries[index];
      if (entry.Record.Equals((object) record))
      {
        ++entry.UseCount;
        statesRecord = entry.Record;
        break;
      }
    }
    if (statesRecord == null)
    {
      StatesManagerEntry statesManagerEntry = new StatesManagerEntry(record);
      this._entries.Add((object) statesManagerEntry);
      ++statesManagerEntry.UseCount;
      statesRecord = record;
    }
    return statesRecord;
  }

  public void Unshare(StatesRecord record)
  {
    for (int index = 0; index < this._entries.Count; ++index)
    {
      StatesManagerEntry entry = (StatesManagerEntry) this._entries[index];
      if (entry.Record == record)
      {
        --entry.UseCount;
        if (entry.UseCount != 0)
          break;
        this._entries.RemoveAt(index);
        break;
      }
    }
  }
}
