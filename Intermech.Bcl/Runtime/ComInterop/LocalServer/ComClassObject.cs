
// Type: Intermech.Runtime.ComInterop.LocalServer.ComClassObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Тип для объектов COM-классов (COM Class Object), через которые COM-клиенты будут взаимодействовать с COM-классами приложения.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class ComClassObject
    {
      private ComServer comServer;
      private Type comClass;
      private IReferenceCounter processRefCounter;
      private ComObjectFactory comObjectFactory;

      public ComClassObject(
        ComServer comServer,
        Type comClass,
        ComObjectFactory comObjectFactory,
        IReferenceCounter processRefCounter)
      {
        this.comServer = comServer;
        this.comClass = comClass;
        this.comObjectFactory = comObjectFactory;
        this.processRefCounter = processRefCounter;
      }

      public object CreateInstance(object pUnkOuter, Guid riid)
      {
        object instance = this.comObjectFactory.CreateInstance(this.comServer, this.comClass);
        this.comServer.RaiseComObjectCreated(instance);
        return instance;
      }

      public void LockServer(int lockFlag)
      {
        if (lockFlag != 0)
          this.processRefCounter.Increment();
        else
          this.processRefCounter.Decrement();
      }
    }
}
