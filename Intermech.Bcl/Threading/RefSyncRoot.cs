
// Type: Intermech.Threading.RefSyncRoot
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Реализует точку синхронизации с помощью ссылки на объект.
    /// </summary>
    public sealed class RefSyncRoot : ISyncRoot
    {
      private readonly object syncRoot;

      /// <summary>Создает точку синхронизации.</summary>
      /// <param name="syncRoot">Ссылка на объект, который будет использоваться для синхронизации доступа к ресурсу</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на объект не может быть null</exception>
      public RefSyncRoot(object syncRoot)
      {
        this.syncRoot = syncRoot != null ? syncRoot : throw new ArgumentNullException(nameof (syncRoot));
      }

      /// <summary>Создает точку синхронизации.</summary>
      public RefSyncRoot() => this.syncRoot = (object) this;

      void ISyncRoot.Lock() => Monitor.Enter(this.syncRoot);

      void ISyncRoot.Unlock() => Monitor.Exit(this.syncRoot);
    }
}
