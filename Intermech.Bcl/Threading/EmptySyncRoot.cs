
// Type: Intermech.Threading.EmptySyncRoot
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Threading
{
    /// <summary>
    /// Реализует пустую точку синхронизации, которую можно использовать, когда многопоточный доступ к ресурсу не требуется.
    /// </summary>
    public sealed class EmptySyncRoot : ISyncRoot
    {
      /// <summary>
      /// Глобальный экземпляр пустой точки, который следует использовать вместо создание новых объектов этого типа.
      /// </summary>
      public static readonly EmptySyncRoot Value = new EmptySyncRoot();

      void ISyncRoot.Lock()
      {
      }

      void ISyncRoot.Unlock()
      {
      }
    }
}
