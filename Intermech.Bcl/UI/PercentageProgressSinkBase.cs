
// Type: Intermech.UI.PercentageProgressSinkBase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Базовый класс индикатора хода выполнения процесса, который позволяет сообщать процент готовности процесса.
    /// </summary>
    public abstract class PercentageProgressSinkBase : IPercentageProgressSink, IProgressSink
    {
      private double lastPercentValue;

      /// <summary>Создает объект.</summary>
      protected PercentageProgressSinkBase() => this.lastPercentValue = 0.0;

      /// <summary>
      /// Возвращает признак прерывания выполнения текущего процесса. Процесс должен периодически проверять значение этого свойства.
      /// Если значение свойства стало равно true, то процесс должен прервать свое выполнение.
      /// </summary>
      public abstract bool IsCancelled { get; }

      /// <summary>Сообщает текущее состояние процесса.</summary>
      /// <param name="text">Описание текущего состояния процесса или выполняемой операции</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="text" /> не должен быть равен null</exception>
      public void SetState(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        this.DoSetState(text);
      }

      /// <summary>
      /// Сообщает процент готовности процесса. Новое новое значение процента должно быть больше текущего значения.
      /// </summary>
      /// <param name="percentValue">Процент готовности процесса в диапазоне от 0 до 100</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="percentValue" /> должен быть в диапазоне от 0 до 100</exception>
      public void SetProgress(double percentValue)
      {
        percentValue = PercentValueHelper.NormalizeAndCheck(percentValue);
        if (percentValue <= this.lastPercentValue)
          return;
        this.lastPercentValue = percentValue;
        this.DoSetProgress(percentValue);
      }

      /// <summary>
      /// Создает и возвращает индикатор хода выполнения для вложенного процесса.
      /// </summary>
      /// <param name="progressDelta">Приращение процента готовности текущего процесса, которое соответствует полной длительности вложенного процесса</param>
      /// <returns>Индикатор хода выполнения для вложенного процесса</returns>
      /// <exception cref="T:ArgumentOutOfRangeException">Значение параметра <paramref name="percentDelta" /> должно быть больше 0</exception>
      public IPercentageProgressSink CreateNestedSink(double percentDelta)
      {
        percentDelta = PercentDeltaHelper.NormalizeAndCheck(this.lastPercentValue, percentDelta);
        return (IPercentageProgressSink) new NestedProgressSink((IPercentageProgressSink) this, this.lastPercentValue, percentDelta);
      }

      /// <summary>Сообщает текущее состояние процесса.</summary>
      /// <param name="text">Описание текущего состояния процесса или выполняемой операции</param>
      protected abstract void DoSetState(string text);

      /// <summary>
      /// Сообщает процент готовности процесса. Новое новое значение процента должно быть больше текущего значения.
      /// </summary>
      /// <param name="percentValue">Процент готовности процесса в диапазоне от 0 до 100</param>
      protected abstract void DoSetProgress(double percentValue);

      private sealed class NestedProgressSink : PercentageProgressSinkBase
      {
        private IPercentageProgressSink parentProgressSink;
        private double parentPercentValue;
        private double parentPercentDelta;
        private double scaleFactor;

        public NestedProgressSink(
          IPercentageProgressSink parentProgressSink,
          double parentPercentValue,
          double parentPercentDelta)
        {
          this.parentProgressSink = parentProgressSink;
          this.parentPercentValue = parentPercentValue;
          this.parentPercentDelta = parentPercentDelta;
          this.scaleFactor = parentPercentDelta / 100.0;
        }

        /// <summary>
        /// Возвращает признак прерывания выполнения текущего процесса. Процесс должен периодически проверять значение этого свойства.
        /// Если значение свойства стало равно true, то процесс должен прервать свое выполнение.
        /// </summary>
        public override bool IsCancelled
        {
          [DebuggerStepThrough] get => this.parentProgressSink.IsCancelled;
        }

        /// <summary>Сообщает текущее состояние процесса.</summary>
        /// <param name="text">Описание текущего состояния процесса или выполняемой операции</param>
        protected override void DoSetState(string text) => this.parentProgressSink.SetState(text);

        /// <summary>
        /// Сообщает процент готовности процесса. Новое новое значение процента должно быть больше текущего значения.
        /// </summary>
        /// <param name="percentValue">Процент готовности процесса в диапазоне от 0 до 100</param>
        protected override void DoSetProgress(double percentValue)
        {
          this.parentProgressSink.SetProgress(this.parentPercentValue + percentValue * this.scaleFactor);
        }
      }
    }
}
