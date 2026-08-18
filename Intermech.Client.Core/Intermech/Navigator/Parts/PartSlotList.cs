
// Type: Intermech.Navigator.Parts.PartSlotList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

/// <summary>Helper класс для облегчения частой операции создания List-PartSlot для одного слота, либо для массива таковых</summary>
public class PartSlotList : List<PartSlot>
{
  /// <summary>Конструктор списка слотов с одним единственным слотом содержимого</summary>
  /// <param name="partSlot">Слот содержимого, который должен быть автоматически добавлен в список слотов ещё при его создании</param>
  public PartSlotList(PartSlot partSlot)
    : base(1)
  {
    this.Add(partSlot);
  }

  /// <summary>Конструктор списка слотов с массивом слотов содержимого</summary>
  /// <param name="partSlots">Массив слотов содержимого, который должен быть автоматически добавлен в список слотов ещё при его создании</param>
  public PartSlotList(params PartSlot[] partSlots)
    : base(partSlots.Length)
  {
    this.Add(partSlots);
  }

  /// <summary>Добавить слот содержимого в список</summary>
  /// <param name="partSlot">Слот содержимого, который должен быть автоматически добавлен в список слотов</param>
  public new void Add(PartSlot partSlot) => base.Add(partSlot);

  /// <summary>Добавить массив слотов содержимого в список</summary>
  /// <param name="partSlots">Массив слотов содержимого, который должен быть автоматически добавлен в список слотов</param>
  public void Add(params PartSlot[] partSlots)
  {
    foreach (PartSlot partSlot in partSlots)
      base.Add(partSlot);
  }
}
