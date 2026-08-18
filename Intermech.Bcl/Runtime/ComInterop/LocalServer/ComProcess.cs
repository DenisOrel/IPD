
// Type: Intermech.Runtime.ComInterop.LocalServer.ComProcess
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Объект процесса приложения с COM-сервером. Реализация является thread safe.
    /// </summary>
    internal sealed class ComProcess
    {
      private static readonly ComProcess instance = new ComProcess();
      private ComProcessReferenceCounter processRefCounter;
      private LiveComObjectsTracker liveComObjectsTracker;

      private ComProcess()
      {
        this.processRefCounter = new ComProcessReferenceCounter();
        this.liveComObjectsTracker = new LiveComObjectsTracker((IReferenceCounter) this.processRefCounter, TimeSpan.FromSeconds(5.0));
      }

      public ComProcessReferenceCounter ProcessRefCounter
      {
        [DebuggerStepThrough] get => this.processRefCounter;
      }

      public LiveComObjectsTracker LiveComObjectsTracker
      {
        [DebuggerStepThrough] get => this.liveComObjectsTracker;
      }

      public static ComProcess Instance
      {
        [DebuggerStepThrough] get => ComProcess.instance;
      }
    }
}
