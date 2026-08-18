
// Type: Intermech.UI.IPercentageProgressSink
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.UI
{
    /// <summary>
    /// Интерфейс индикатора хода выполнения процесса, который позволяет сообщать процент готовности процесса.
    /// </summary>
    public interface IPercentageProgressSink : IProgressSink
    {
      /// <summary>
      /// Сообщает процент готовности процесса. Новое новое значение процента должно быть больше текущего значения.
      /// </summary>
      /// <param name="percentValue">Процент готовности процесса в диапазоне от 0 до 100</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Значение параметра <paramref name="percentValue" /> должно быть в интервале от 0 до 100</exception>
      void SetProgress(double percentValue);

      /// <summary>
      /// Создает и возвращает индикатор хода выполнения для вложенного процесса.
      /// </summary>
      /// <param name="progressDelta">Приращение процента готовности текущего процесса, которое соответствует полной длительности вложенного процесса</param>
      /// <returns>Индикатор хода выполнения для вложенного процесса</returns>
      /// <exception cref="T:ArgumentOutOfRangeException">Значение параметра <paramref name="percentDelta" /> должно быть больше 0</exception>
      IPercentageProgressSink CreateNestedSink(double percentDelta);
    }
}
