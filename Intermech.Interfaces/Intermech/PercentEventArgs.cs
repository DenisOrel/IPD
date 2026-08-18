
// Type: Intermech.PercentEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech
{
    /// <summary>
    /// Аргументы события для отслеживания прогресса выполнения какого-либо процесса.
    /// </summary>
    public class PercentEventArgs : EventArgs
    {
      private readonly int _percent;

      /// <summary>Создает объект.</summary>
      /// <param name="percent">Значение прогресса в процентах</param>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Значение процентов вне допустимого диапазона 0..100.</exception>
      public PercentEventArgs(int percent)
      {
        this._percent = percent >= 0 && percent <= 100 ? percent : throw new ArgumentOutOfRangeException(nameof (percent));
      }

      /// <summary>Возвращает значение прогресса выполнения в процентах.</summary>
      public int Percent
      {
        [DebuggerStepThrough] get => this._percent;
      }
    }
}
