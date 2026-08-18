
// Type: Intermech.Navigator.AdvDescriptorCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator;

/// <summary>Helper класс для облегчения создания объектов типа DescriptorsCollection
/// Сделано отдельно дабы не лезть пока что в чужой код, хотя в дальнейшем планируется перенести код в DescriptorsCollection.</summary>
public class AdvDescriptorCollection : DescriptorCollection
{
  /// <summary>Конструктор, создающий колллекцию длескрипторов, состоящую из одного элемента</summary>
  /// <param name="descriptor">Дескриптор, который должен присутствовать в коллекции</param>
  public AdvDescriptorCollection(IDescriptor descriptor) => this.Add(descriptor);

  /// <summary>Конструктор, создающий колллекцию длескрипторов, состоящую из одного элемента</summary>
  /// <param name="descriptors">Дескрипторы, которые должны присутствовать в коллекции</param>
  public AdvDescriptorCollection(params IDescriptor[] descriptors)
  {
    foreach (IDescriptor descriptor in descriptors)
      this.Add(descriptor);
  }
}
