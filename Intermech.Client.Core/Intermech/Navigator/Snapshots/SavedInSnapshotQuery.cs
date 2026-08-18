
// Type: Intermech.Navigator.Snapshots.SavedInSnapshotQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;


namespace Intermech.Navigator.Snapshots;

/// <summary>Query содержимого итерации (фактически возвращает только параметры сохранённого в итерации головного объекта)</summary>
internal class SavedInSnapshotQuery([NotNull, ItemNotNull] DescriptorCollection descriptors, bool sortedQuery) : 
  DescriptorsQuery(descriptors, sortedQuery),
  INodeQuery
{
}
