
// Type: Intermech.Threading.ISyncRoot
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Threading
{
    /// <summary>
    /// Позволяет реализовать точку синхронизации для ресурсов, защищаемых с помощью простой блокировки.
    /// </summary>
    public interface ISyncRoot
    {
      /// <summary>Получить блокировку для доступа к ресурсу.</summary>
      void Lock();

      /// <summary>Разблокировать ресурс.</summary>
      void Unlock();
    }
}
