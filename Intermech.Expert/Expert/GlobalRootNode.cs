// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.GlobalRootNode
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

public class GlobalRootNode : GlobalNode
{
  public List<ColumnDescriptor> all_descs;
  public Dictionary<int, int> objTypesToNodes;
  public Dictionary<int, List<ColumnDescriptor>> objAttrs4ObjTypes;
  public Dictionary<int, List<ColumnDescriptor>> relAttrs4ObjTypes;
}
