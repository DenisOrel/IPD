
// Type: Intermech.UI.NullPercentageProgressSink
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Индикатор-заглушка, используемый в тех случаях, когда отображения хода выполнения процесса не требуется.
    /// </summary>
    internal sealed class NullPercentageProgressSink : IPercentageProgressSink, IProgressSink
    {
      private static readonly NullPercentageProgressSink defaultInstance = new NullPercentageProgressSink();

      /// <summary>
      /// Возвращает признак прерывания выполнения текущего процесса. Процесс должен периодически проверять значение этого свойства.
      /// Если значение свойства стало равно true, то процесс должен прервать свое выполнение.
      /// </summary>
      public bool IsCancelled
      {
        [DebuggerStepThrough] get => false;
      }

      /// <summary>Сообщает текущее состояние процесса.</summary>
      /// <param name="text">Описание текущего состояния процесса или выполняемой операции</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="text" /> не должен быть равен null</exception>
      public void SetState(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
      }

      /// <summary>
      /// Сообщает процент готовности процесса. Новое новое значение процента должно быть больше текущего значения.
      /// </summary>
      /// <param name="percentValue">Процент готовности процесса в диапазоне от 0 до 100</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Значение параметра <paramref name="percentValue" /> должно быть в интервале от 0 до 100</exception>
      public void SetProgress(double percentValue)
      {
        PercentValueHelper.NormalizeAndCheck(percentValue);
      }

      /// <summary>
      /// Создает и возвращает индикатор хода выполнения для вложенного процесса.
      /// </summary>
      /// <param name="progressDelta">Приращение процента готовности текущего процесса, которое соответствует полной длительности вложенного процесса</param>
      /// <returns>Индикатор хода выполнения для вложенного процесса</returns>
      /// <exception cref="T:ArgumentOutOfRangeException">Значение параметра <paramref name="percentDelta" /> должно быть больше 0</exception>
      public IPercentageProgressSink CreateNestedSink(double percentDelta)
      {
        if (percentDelta <= 0.0)
          throw new ArgumentOutOfRangeException(nameof (percentDelta));
        return (IPercentageProgressSink) NullPercentageProgressSink.Default;
      }

      /// <summary>
      /// Возвращает экземпляр индикатора, который может использоваться по умолчанию.
      /// </summary>
      public static NullPercentageProgressSink Default
      {
        [DebuggerStepThrough] get => NullPercentageProgressSink.defaultInstance;
      }
    }
}
