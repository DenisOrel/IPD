
// Type: Intermech.Navigator.Controls.StatesRecord
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

public class StatesRecord
{
  private bool[] _states;
  private int _completeCount;

  public StatesRecord(int length, bool value)
  {
    this._states = new bool[length];
    if (value)
    {
      for (int index = 0; index < this._states.Length; ++index)
        this._states[index] = true;
      this._completeCount = this._states.Length;
    }
    else
      this._completeCount = 0;
  }

  public StatesRecord(StatesRecord cloned)
  {
    this._states = new bool[cloned._states.Length];
    cloned._states.CopyTo((Array) this._states, 0);
    this._completeCount = cloned._completeCount;
  }

  public void InsertColumn(int index, bool value)
  {
    bool[] destinationArray = new bool[this._states.Length + 1];
    if (index > 0)
      Array.Copy((Array) this._states, 0, (Array) destinationArray, 0, index);
    if (index + 1 < this._states.Length)
      Array.Copy((Array) this._states, index, (Array) destinationArray, index + 1, this._states.Length - index);
    destinationArray[index] = value;
    this._states = destinationArray;
    if (!value)
      return;
    ++this._completeCount;
  }

  public void RemoveColumn(int index)
  {
    bool[] destinationArray = new bool[this._states.Length - 1];
    if (index > 0)
      Array.Copy((Array) this._states, 0, (Array) destinationArray, 0, index);
    if (index + 1 < this._states.Length)
      Array.Copy((Array) this._states, index + 1, (Array) destinationArray, index, destinationArray.Length - index);
    if (this._states[index])
      --this._completeCount;
    this._states = destinationArray;
  }

  public int Length => this._states.Length;

  public bool this[int index]
  {
    get => this._states[index];
    set
    {
      if (this._states[index] == value)
        return;
      this._states[index] = value;
      this._completeCount += value ? 1 : -1;
    }
  }

  public bool IsComplete => this._completeCount == this._states.Length;

  public bool IsEmpty => this._completeCount == 0;

  public bool IsPartial => this._completeCount != this._states.Length;

  public override bool Equals(object obj)
  {
    StatesRecord statesRecord = (StatesRecord) obj;
    if (statesRecord == null || this._completeCount != statesRecord._completeCount || this._states.Length != statesRecord._states.Length)
      return false;
    if (this._completeCount == 0 || this._completeCount == this._states.Length)
      return true;
    for (int index = 0; index < this._states.Length; ++index)
    {
      if (this._states[index] != statesRecord._states[index])
        return false;
    }
    return true;
  }

  public override int GetHashCode()
  {
    long num = 0;
    for (int index = 0; index < this._states.Length; ++index)
    {
      if (this._states[index])
        ++num;
      num <<= 1;
    }
    return (int) (num >> 32 /*0x20*/ ^ num);
  }
}
