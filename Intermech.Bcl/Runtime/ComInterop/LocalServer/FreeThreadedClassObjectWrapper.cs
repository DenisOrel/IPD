
// Type: Intermech.Runtime.ComInterop.LocalServer.FreeThreadedClassObjectWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime.ComInterop.ComTypes;
using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Обертка для объекта COM-класса, необходимая для работы в MTA-потоках. Реализация является thread safe.
    /// </summary>
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof (IClassFactory))]
    internal sealed class FreeThreadedClassObjectWrapper : FreeThreadedObject, IClassFactory
    {
      private ComClassObject comClassObject;

      /// <summary>Создает объект.</summary>
      /// <param name="comClassObject">Актуальная реализация объекта COM-класса</param>
      public FreeThreadedClassObjectWrapper(ComClassObject comClassObject)
        : base(false)
      {
        this.comClassObject = comClassObject;
      }

      public object CreateInstance(object pUnkOuter, Guid riid)
      {
        return this.comClassObject.CreateInstance(pUnkOuter, riid);
      }

      public void LockServer(int lockFlag) => this.comClassObject.LockServer(lockFlag);
    }
}
