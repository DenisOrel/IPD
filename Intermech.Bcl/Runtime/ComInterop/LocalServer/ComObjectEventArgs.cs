
// Type: Intermech.Runtime.ComInterop.LocalServer.ComObjectEventArgs
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Аргументы события с участием COM-объектов.</summary>
    public class ComObjectEventArgs : EventArgs
    {
      private object comObject;

      /// <summary>Создает объект.</summary>
      /// <param name="comObject">COM-объект</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comObject" /> не должен быть равен null</exception>
      public ComObjectEventArgs(object comObject)
      {
        this.comObject = comObject != null ? comObject : throw new ArgumentNullException(nameof (comObject));
      }

      /// <summary>Возвращает COM-объект.</summary>
      public object ComObject
      {
        [DebuggerStepThrough] get => this.comObject;
      }
    }
}
