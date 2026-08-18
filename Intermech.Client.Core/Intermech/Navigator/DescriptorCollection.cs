
// Type: Intermech.Navigator.DescriptorCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator;

/// <summary>
/// Коллекция дескрипторов элементов из пространства навигации.
/// </summary>
public class DescriptorCollection : IEnumerable<IDescriptor>, IEnumerable, IPersistable
{
  /// <summary>Коллекция слотов с дескрипторами</summary>
  private List<DescriptorSlot> slots = new List<DescriptorSlot>();

  public DescriptorCollection()
  {
  }

  public DescriptorCollection(IEnumerable<IDescriptor> descriptors)
    : this()
  {
    this.AddRange(descriptors);
  }

  public DescriptorCollection(IEnumerable<Tuple<Guid, IDescriptor>> descriptors)
    : this()
  {
    this.AddRange(descriptors);
  }

  public DescriptorCollection(PersistentState persistentState)
  {
    for (int index = 0; index < persistentState.MemberCount; ++index)
    {
      if (persistentState.GetValue(index.ToString()) is PersistentState persistentState1)
      {
        Guid descriptorGuid = (Guid) persistentState1.GetValue("PartGuid");
        if (FormatterServices.RestoreObject((PersistentState) persistentState1.GetValue("Descriptor")) is IDescriptor descriptor)
          this.Add(descriptorGuid, descriptor);
      }
    }
  }

  public IEnumerable<DescriptorSlot> Slots => (IEnumerable<DescriptorSlot>) this.slots;

  /// <summary>
  /// Возвращает объект, который может использоваться для
  /// синхронизации доступа к объектам этого класса.
  /// </summary>
  public object SyncRoot => (object) this.slots;

  /// <summary>Количество дескрипторов в коллекции</summary>
  public int Count => this.slots.Count;

  /// <summary>Получить дескриптор по указанному индексу</summary>
  /// <param name="index">Индекс дескриптора</param>
  /// <returns>Дескриптор с указанным индексом</returns>
  public IDescriptor this[int index] => this.slots[index].Object;

  /// <summary>Очищает коллекцию дескрипторов.</summary>
  public void Clear() => this.slots.Clear();

  /// <summary>Добавляет новый дескриптор в коллекцию.</summary>
  /// <param name="descriptor">Дескриптор элемента навигации</param>
  public void Add(IDescriptor descriptor) => this.Add(Guid.NewGuid(), descriptor);

  /// <summary>Добавляет новый дескриптор в коллекцию.</summary>
  /// <param name="descriptorGuid">Guid дескриптора</param>
  /// <param name="descriptor">Дескриптор элемента навигации</param>
  public void Add(Guid descriptorGuid, IDescriptor descriptor)
  {
    this.Validate(descriptorGuid);
    this.Validate(descriptor);
    this.slots.Add(new DescriptorSlot(PartGuidMapper.GetUniqueId(descriptorGuid), descriptor));
  }

  public void AddRange(IEnumerable<IDescriptor> descriptors)
  {
    foreach (IDescriptor descriptor in descriptors)
      this.Add(descriptor);
  }

  public void AddRange(IEnumerable<Tuple<Guid, IDescriptor>> descriptors)
  {
    foreach (Tuple<Guid, IDescriptor> descriptor in descriptors)
      this.Add(descriptor.Item1, descriptor.Item2);
  }

  /// <summary>Удаляет дескриптор из указанной позиции в коллекции.</summary>
  /// <param name="index">Индекс дексриптора в коллекции</param>
  public void RemoveAt(int index) => this.slots.RemoveAt(index);

  /// <summary>Отыскивает в коллекции индекс указанного дескриптора</summary>
  /// <param name="descriptor">Дескриптор элемента навигации</param>
  /// <returns>Индекс указанного дескриптора или -1</returns>
  public int IndexOf(IDescriptor descriptor)
  {
    this.Validate(descriptor);
    for (int index = 0; index < this.slots.Count; ++index)
    {
      if (this.slots[index].Object == descriptor)
        return index;
    }
    return -1;
  }

  /// <summary>
  /// Отыскивает уникальный идентификатор указанного дескриптора
  /// </summary>
  /// <param name="descriptor">Дескриптор элемента навигации</param>
  /// <returns>Уникальный идентификатор указанного дескриптора</returns>
  public int GetUniqueId(IDescriptor descriptor)
  {
    this.Validate(descriptor);
    return this.slots[this.IndexOf(descriptor)].UniqueId;
  }

  /// <summary>
  /// Отыскивает уникальный идентификатор дексриптора с указанным индексом
  /// </summary>
  /// <param name="index">Индекс дескриптора в коллекции</param>
  /// <returns>Уникальный идентификатор дескриптора с указанным индексом</returns>
  public int GetUniqueId(int index) => this.slots[index].UniqueId;

  /// <summary>
  /// Отыскивает декскриптор по его уникальному идентификатору
  /// </summary>
  /// <param name="uniqueId">Уникальный идентификатор дескриптора</param>
  /// <returns>Найденный дескриптор или null</returns>
  public IDescriptor FindDescriptor(int uniqueId)
  {
    for (int index = 0; index < this.slots.Count; ++index)
    {
      if (this.slots[index].UniqueId == uniqueId)
        return this.slots[index].Object;
    }
    return (IDescriptor) null;
  }

  /// <summary>Проверить указанный идентификатор дескриптора</summary>
  /// <param name="descriptorGuid">Идентификатор дескриптора</param>
  private void Validate(Guid descriptorGuid)
  {
    if (descriptorGuid == Guid.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString(sc_4256.ssp_imclient_4257()), nameof (descriptorGuid));
  }

  /// <summary>Проверить указанный дескриптор</summary>
  /// <param name="descriptor">Дескриптор</param>
  private void Validate(IDescriptor descriptor)
  {
    if (descriptor == null)
      throw new ArgumentNullException(sc_4256.ssp_imclient_4258(), LocalizationHolder.rm.GetString("Client.Core_624"));
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) this.slots.Select<DescriptorSlot, IDescriptor>((Func<DescriptorSlot, IDescriptor>) (slot => slot.Object)).GetEnumerator();
  }

  public IEnumerator<IDescriptor> GetEnumerator()
  {
    return this.slots.Select<DescriptorSlot, IDescriptor>((Func<DescriptorSlot, IDescriptor>) (slot => slot.Object)).GetEnumerator();
  }

  public void GetObjectData(PersistentState state)
  {
    for (int index = 0; index < this.slots.Count; ++index)
    {
      PersistentState persistentState = new PersistentState();
      persistentState.AddValue("PartGuid", (object) PartGuidMapper.GetGuid(this.slots[index].UniqueId));
      persistentState.AddValue("Descriptor", (object) FormatterServices.GetObjectState((object) this.slots[index].Object));
      state.AddValue(index.ToString(), (object) persistentState);
    }
  }
}
