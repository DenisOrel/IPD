
// Type: Intermech.Navigator.Parts.DescriptorSlotsList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Parts;

/// <summary>Helper класс для облегчения частой операции создания List-PartSlot для одного дескриптора, либо для массива таковых.</summary>
internal class DescriptorSlotsList : PartSlotList
{
  /// <summary>Конструктор из одного единственного дескриптора</summary>
  /// <param name="descriptor">Дескриптор</param>
  public DescriptorSlotsList(IDescriptor descriptor)
    : base(new PartSlot(CompositeNode.SinglePartGuid, (INodePart) new DescriptorsPart((DescriptorCollection) new AdvDescriptorCollection(descriptor))))
  {
  }

  /// <summary>Конструктор из массива дескрипторов</summary>
  /// <param name="descriptors">Массив дескрипторов</param>
  public DescriptorSlotsList(params IDescriptor[] descriptors)
    : base(new PartSlot(CompositeNode.SinglePartGuid, (INodePart) new DescriptorsPart((DescriptorCollection) new AdvDescriptorCollection(descriptors))))
  {
  }
}
