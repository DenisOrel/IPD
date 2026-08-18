
// Type: Intermech.Runtime.ComInterop.ComActivator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Реализует активатор, позволяющий создавать COM-объекты.
    /// </summary>
    public static class ComActivator
    {
      private static Guid IUnknownIID = new Guid("{00000000-0000-0000-C000-000000000046}");

      /// <summary>Создает COM-объект.</summary>
      /// <param name="progId">ProdId создаваемого объекта</param>
      /// <param name="context">Требуемый контекст выполнения объекта</param>
      /// <returns>Созданный COM-объект</returns>
      public static object CreateInstance(string progId, RegistrationClassContext context)
      {
        return ComActivator.CreateInstance(Type.GetTypeFromProgID(progId, true).GUID, context);
      }

      /// <summary>Создает COM-объект.</summary>
      /// <param name="clsid">CLSID создаваемого объекта</param>
      /// <param name="context">Требуемый контекст выполнения объекта</param>
      /// <returns>Созданный COM-объект</returns>
      public static object CreateInstance(Guid clsid, RegistrationClassContext context)
      {
        return NativeMethods.CoCreateInstance(ref clsid, (object) null, context, ref ComActivator.IUnknownIID);
      }
    }
}
