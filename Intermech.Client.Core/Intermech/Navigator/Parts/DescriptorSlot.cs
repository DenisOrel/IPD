
// Type: Intermech.Navigator.Parts.DescriptorSlot
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Parts;

/// <summary>Слот, в котором хранится дескриптор</summary>
/// <summary>Создать экземпляр класса</summary>
/// <param name="uniqueId">Уникальный идентификатор дескриптора</param>
/// <param name="descriptor">Дескриптор</param>
public class DescriptorSlot(int uniqueId, IDescriptor descriptor) : Slot<IDescriptor>(uniqueId, descriptor)
{
}
