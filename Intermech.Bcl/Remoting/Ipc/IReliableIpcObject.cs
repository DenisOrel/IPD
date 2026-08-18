
// Type: Intermech.Remoting.Ipc.IReliableIpcObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Remoting.Ipc
{
    /// <summary>
    /// Интерфейс объекта приложения, который позволяет убедиться в работоспособоности подключения к этому объекту.
    /// </summary>
    public interface IReliableIpcObject
    {
      /// <summary>
      /// Метод для проверки работоспособности подключения к текущему объекту.
      /// </summary>
      /// <exception cref="T:System.Exception">Подключение к текущему объекту нарушено и должно быть переустановлено</exception>
      void KnockKnock();
    }
}
