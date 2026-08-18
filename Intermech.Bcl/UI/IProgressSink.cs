
// Type: Intermech.UI.IProgressSink
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.UI
{
    /// <summary>
    /// Базовый интерфейс для индикаторов хода выполнения процесса.
    /// </summary>
    public interface IProgressSink
    {
      /// <summary>
      /// Возвращает признак прерывания выполнения текущего процесса. Процесс должен периодически проверять значение этого свойства.
      /// Если значение свойства стало равно true, то процесс должен прервать свое выполнение.
      /// </summary>
      bool IsCancelled { get; }

      /// <summary>Сообщает текущее состояние процесса.</summary>
      /// <param name="text">Описание текущего состояния процесса или выполняемой операции</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="text" /> не должен быть равен null</exception>
      void SetState(string text);
    }
}
