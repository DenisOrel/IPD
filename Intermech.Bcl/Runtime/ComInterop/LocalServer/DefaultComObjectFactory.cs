
// Type: Intermech.Runtime.ComInterop.LocalServer.DefaultComObjectFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Реализация по умолчанию для фабрики COM-объектов.</summary>
    internal sealed class DefaultComObjectFactory : ComObjectFactory
    {
      /// <summary>Создает COM-объект.</summary>
      /// <param name="comServer">COM-сервер, которому принадлежит COM-класс</param>
      /// <param name="comClass">COM-класс создаваемого объекта</param>
      /// <returns>Созданный COM-объект</returns>
      protected override object DoCreateInstance(ComServer comServer, Type comClass)
      {
        return Activator.CreateInstance(comClass);
      }
    }
}
