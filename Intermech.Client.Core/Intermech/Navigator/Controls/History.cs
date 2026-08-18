
// Type: Intermech.Navigator.Controls.History
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

internal class History : INavigate
{
  private IHistoryProvider _provider;
  private bool _navigating;
  private int _capacity;
  private List<HistoryItem> _backwardItems;
  private List<HistoryItem> _forwardItems;

  public History(IHistoryProvider provider, int capacity)
  {
    this._provider = provider;
    this._navigating = false;
    this._capacity = capacity;
    this._backwardItems = new List<HistoryItem>(this._capacity);
    this._forwardItems = new List<HistoryItem>(this._capacity);
  }

  public void Clear()
  {
    if (this._navigating)
      return;
    this._forwardItems.Clear();
    this._backwardItems.Clear();
    this.FireChangedEvent();
  }

  public void Update()
  {
    if (this._navigating)
      return;
    this._forwardItems.Clear();
    if (this._backwardItems.Count == this._capacity)
      this._backwardItems.RemoveAt(this._capacity - 1);
    this._backwardItems.Insert(0, this._provider.CurrentItem);
    this.FireChangedEvent();
  }

  private void FireChangedEvent()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  private string[] GetItemNames(List<HistoryItem> items)
  {
    string[] itemNames = new string[items.Count];
    for (int index = 0; index < itemNames.Length; ++index)
      itemNames[index] = items[index].DisplayName;
    return itemNames;
  }

  private void DoNavigate(int index, bool forwardDirection)
  {
    List<HistoryItem> historyItemList1;
    List<HistoryItem> historyItemList2;
    if (forwardDirection)
    {
      historyItemList1 = this._forwardItems;
      historyItemList2 = this._backwardItems;
    }
    else
    {
      historyItemList1 = this._backwardItems;
      historyItemList2 = this._forwardItems;
    }
    this._navigating = true;
    try
    {
      historyItemList2.Insert(0, this._provider.CurrentItem);
      this._provider.ApplyItem(historyItemList1[index]);
    }
    finally
    {
      historyItemList1.RemoveAt(index);
      for (int index1 = 0; index1 < index; ++index1)
      {
        HistoryItem historyItem = historyItemList1[0];
        historyItemList1.RemoveAt(0);
        historyItemList2.Insert(0, historyItem);
      }
      this._navigating = false;
      this.FireChangedEvent();
    }
  }

  public event EventHandler Changed;

  public void Back() => this.DoNavigate(0, false);

  public void Back(int steps) => this.DoNavigate(steps - 1, false);

  public void Forward() => this.DoNavigate(0, true);

  public void Forward(int steps) => this.DoNavigate(steps - 1, true);

  public bool CanBack => this._backwardItems.Count != 0;

  public bool CanForward => this._forwardItems.Count != 0;

  public string BackName => LocalizationHolder.rm.GetString("Client.Core_583");

  public string ForwardName => LocalizationHolder.rm.GetString("Client.Core_584");

  public string[] BackNames => this.GetItemNames(this._backwardItems);

  public string[] ForwardNames => this.GetItemNames(this._forwardItems);
}
