
// Type: Intermech.Navigator.Parts.EtherealNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

public class EtherealNode : CompositeNode
{
  public DescriptorCollection descriptors;
  private static readonly Guid EtherealDescriptorGuid = new Guid("4337C9D7-C06A-4200-BB7A-B97C3B8667D8");

  public EtherealNode(IDescriptor rootDescriptor)
  {
    this.descriptors = new DescriptorCollection();
    this.descriptors.Add(EtherealNode.EtherealDescriptorGuid, rootDescriptor);
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(this.descriptors));
  }
}
