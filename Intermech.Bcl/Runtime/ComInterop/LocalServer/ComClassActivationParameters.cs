
// Type: Intermech.Runtime.ComInterop.LocalServer.ComClassActivationParameters
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Параметры активации COM-класса.</summary>
    internal sealed class ComClassActivationParameters
    {
      private Type comClass;
      private ComObjectFactory comObjectFactory;

      /// <summary>Создает объект.</summary>
      /// <param name="comClass">COM-класс</param>
      /// <param name="comObjectFactory">Фабрика COM-объектов этого класса. Параметр может быть не задан, в этом случае будет использована фабрика по умолчанию</param>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="comClass" />, <paramref name="comObjectFactory" /> не должны быть равны null</exception>
      public ComClassActivationParameters(Type comClass, ComObjectFactory comObjectFactory)
      {
        if (comClass == (Type) null)
          throw new ArgumentNullException(nameof (comClass));
        if (comObjectFactory == null)
          throw new ArgumentNullException(nameof (comObjectFactory));
        this.comClass = comClass;
        this.comObjectFactory = comObjectFactory;
      }

      /// <summary>Возвращает COM-класс.</summary>
      public Type ComClass
      {
        [DebuggerStepThrough] get => this.comClass;
      }

      /// <summary>
      /// Возвращает или задает фабрику COM-объектов этого класса.
      /// </summary>
      public ComObjectFactory ComObjectFactory
      {
        [DebuggerStepThrough] get => this.comObjectFactory;
      }
    }
}
