
// Type: Intermech.Runtime.ComInterop.LocalServer.ComObjectFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Базовый класс для фабрики COM-объектов. Реализация должна быть thread safe.
    /// </summary>
    public abstract class ComObjectFactory
    {
      /// <summary>Создает COM-объект.</summary>
      /// <param name="comServer">COM-сервер, которому принадлежит COM-класс</param>
      /// <param name="comClass">COM-класс создаваемого объекта</param>
      /// <returns>Созданный COM-объект</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="comServer" />, <paramref name="comClass" /> не должны быть равны null</exception>
      public object CreateInstance(ComServer comServer, Type comClass)
      {
        if (comServer == null)
          throw new ArgumentNullException(nameof (comServer));
        return !(comClass == (Type) null) ? this.DoCreateInstance(comServer, comClass) : throw new ArgumentNullException(nameof (comClass));
      }

      /// <summary>Создает COM-объект.</summary>
      /// <param name="comServer">COM-сервер, которому принадлежит COM-класс</param>
      /// <param name="comClass">COM-класс создаваемого объекта</param>
      /// <returns>Созданный COM-объект</returns>
      protected abstract object DoCreateInstance(ComServer comServer, Type comClass);
    }
}
