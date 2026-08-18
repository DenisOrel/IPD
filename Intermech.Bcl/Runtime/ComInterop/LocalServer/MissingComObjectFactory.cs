
// Type: Intermech.Runtime.ComInterop.LocalServer.MissingComObjectFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Фабрика COM-объектов, чья активация не была выполнена приложением.
    /// При попытке создания экземпляра объекта фабрика бросает исключение COMException с кодом E_ABORT.
    /// </summary>
    internal sealed class MissingComObjectFactory : ComObjectFactory
    {
      /// <summary>Создает COM-объект.</summary>
      /// <param name="comServer">COM-сервер, которому принадлежит COM-класс</param>
      /// <param name="comClass">COM-класс создаваемого объекта</param>
      /// <returns>Созданный COM-объект</returns>
      protected override object DoCreateInstance(ComServer comServer, Type comClass)
      {
        string message = string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_ComClassIsUnknown, (object) Marshal.GenerateProgIdForType(comClass), (object) comClass.AssemblyQualifiedName);
        if (TraceSwitches.General.TraceError)
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "COM: {0}", (object) message));
        throw new COMException(message, -2147467260 /*0x80004004*/);
      }
    }
}
