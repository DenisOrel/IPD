
// Type: Intermech.Search.Diff.ListItemDiffPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.Diff;

public sealed class ListItemDiffPropertyDescriptor : DiffPropertyDescriptorBase<ListItemDiff>
{
  public ListItemDiffPropertyDescriptor(Type componentType, int index, Type propertyType)
    : base(componentType, ListItemDiffPropertyDescriptor.GetName(index), propertyType)
  {
    this.Index = index;
  }

  public int Index { get; private set; }

  public override ListItemDiff GetDiff(IDiffCollection<ListItemDiff> diffCollection)
  {
    if (diffCollection == null)
      throw new ArgumentNullException("component");
    return diffCollection is ListItemDiffCollection ? ((ListItemDiffCollection) diffCollection)[this.Index] : throw new ArgumentException();
  }

  private static string GetName(int index) => $"[{index.ToString()}]";
}
