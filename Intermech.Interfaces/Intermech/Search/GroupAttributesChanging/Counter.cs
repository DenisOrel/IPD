
// Type: Intermech.Search.GroupAttributesChanging.Counter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.GroupAttributesChanging
{
    public sealed class Counter
    {
      private int _capacity;

      public Counter(int id, int startValue)
      {
        this.ID = id;
        this.StartValue = startValue;
        this.Value = startValue;
        this.Capacity = 1;
      }

      public int Capacity
      {
        get => this._capacity;
        set
        {
          if (value <= 0)
            throw new ArgumentException();
          if (this._capacity == value)
            return;
          this._capacity = value;
        }
      }

      public int ID { get; private set; }

      public int StartValue { get; private set; }

      public int Step { get; set; }

      public int Value { get; private set; }

      public void Increment() => this.Value += this.Step;

      public override string ToString() => this.Value.ToString(new string('0', this.Capacity));
    }
}
