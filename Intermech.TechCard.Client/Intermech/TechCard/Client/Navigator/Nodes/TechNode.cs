// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Nodes.TechNode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator;
using Intermech.Navigator.Parts;
using Intermech.TechCard.Client.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Nodes;

/// <summary>
/// 
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="descriptors"></param>
internal class TechNode(DescriptorCollection descriptors) : Intermech.Navigator.CustomNode.Node(descriptors)
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new TechDescriptorsPart(this._descriptors));
  }
}
