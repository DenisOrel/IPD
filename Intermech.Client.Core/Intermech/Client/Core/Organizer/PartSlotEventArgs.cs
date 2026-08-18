
// Type: Intermech.Client.Core.Organizer.PartSlotEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class PartSlotEventArgs : EventArgs
{
  private IServiceProvider _provider;
  private ConditionStructure[] _conditions;
  private List<PartSlot> _slots;

  /// <summary>Условие, которому должны соответствовать объекты.</summary>
  public ConditionStructure[] Contitions => this._conditions;

  /// <summary>
  /// 
  /// </summary>
  public IServiceProvider Provider => this._provider;

  /// <summary>Конструктор.</summary>
  /// <param name="provider"></param>
  /// <param name="slots"></param>
  public PartSlotEventArgs(IServiceProvider provider, List<PartSlot> slots)
  {
    this._provider = provider;
    this._slots = slots;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="provider"></param>
  /// <param name="conditions">Условие, которому должны соответствовать объекты</param>
  /// <param name="slots"></param>
  public PartSlotEventArgs(
    IServiceProvider provider,
    ConditionStructure[] conditions,
    List<PartSlot> slots)
    : this(provider, slots)
  {
    this._conditions = conditions;
  }

  /// <summary>Добавление слота.</summary>
  /// <param name="slot">Добавляемый слот</param>
  public void AddSlot(PartSlot slot)
  {
    if (slot == null || this._slots.Contains(slot))
      return;
    this._slots.Add(slot);
  }

  /// <summary>Добавление списка слотов.</summary>
  /// <param name="slots">Список слотов</param>
  public void AddSlots(List<PartSlot> slots)
  {
    if (slots == null)
      return;
    foreach (PartSlot slot in slots)
    {
      if (slot != null && !this._slots.Contains(slot))
        this._slots.Add(slot);
    }
  }
}
