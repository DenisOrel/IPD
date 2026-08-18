
// Type: Intermech.Navigator.Controls.StatesRecordCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Navigator.Controls;

internal class StatesRecordCollection : CollectionBase
{
  public int Add(StatesRecord record) => this.List.Add((object) record);

  public void Insert(int index, StatesRecord record) => this.List.Insert(index, (object) record);

  public bool Contains(StatesRecord record) => this.List.Contains((object) record);

  public int IndexOf(StatesRecord record) => this.List.IndexOf((object) record);

  public void Remove(StatesRecord record) => this.List.Remove((object) record);

  public StatesRecord this[int index] => (StatesRecord) this.List[index];
}
