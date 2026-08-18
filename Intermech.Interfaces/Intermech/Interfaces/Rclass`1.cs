
// Type: Intermech.Interfaces.Rclass`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>объект с реакцией на изменение свойства Value</summary>
    /// <typeparam name="T">тип объекта</typeparam>
    [DebuggerDisplay("{_Value}")]
    public class Rclass<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private T _Value;

      /// <summary>конструктор</summary>
      /// <param name="value"></param>
      public Rclass(T value = null) => this._Value = value;

      /// <summary>свойство с реакцией на изменение</summary>
      public T Value
      {
        [DebuggerStepThrough] get => this._Value;
        set
        {
          if (EqualityComparer<T>.Default.Equals(this._Value, value))
            return;
          this._Value = value;
          this.OnValueChanged();
        }
      }

      /// <summary>Преобразует указанный val в Объект <see cref="T:Intermech.Interfaces.Rclass`1" /> </summary>
      /// <param name="val">Преобразуемый <see cref="T:Intermech.Interfaces.Rclass`1" /> </param>
      /// <returns>Объект T, который является результатом преобразований этого метода.</returns>
      public static implicit operator T(Rclass<T> val) => val.Value;

      /// <summary>реакция при изменении значения свойства Value</summary>
      public event EventHandler<EventArgs<T>> ValueChanged;

      /// <summary>реакция при изменении значения свойства Value</summary>
      public void OnValueChanged()
      {
        if (this.ValueChanged == null)
          return;
        this.ValueChanged((object) this, new EventArgs<T>(this._Value));
      }
    }
}
