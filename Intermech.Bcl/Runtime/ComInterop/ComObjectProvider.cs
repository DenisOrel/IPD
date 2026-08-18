
// Type: Intermech.Runtime.ComInterop.ComObjectProvider
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Позволяет реализовать провайдер типа для COM-объекта. Такой провайдер обеспечивает ленивое получение
    /// managed типа для COM-объекта, а также выбор типа COM-сервера.
    /// </summary>
    public abstract class ComObjectProvider
    {
      private readonly bool inprocServer;

      /// <summary>Создает провайдер COM-объекта.</summary>
      /// <param name="inprocServer">Признак, что COM-объект реализован как in-process сервер</param>
      public ComObjectProvider(bool inprocServer) => this.inprocServer = inprocServer;

      /// <summary>
      /// Реализует ленивое получение управляемого типа COM-объекта.
      /// </summary>
      /// <param name="throwOnError">Признак, что нужно сгенерировать исключение, если указанный COM-объект не зарегистрирован</param>
      /// <returns>Управляемый тип COM-объекта или null, если указанный COM-объект не зарегистрирован</returns>
      /// <exception cref="T:System.Runtime.InteropServices.COMException">Указанный COM-объект не зарегистрирован</exception>
      public abstract Type GetComType(bool throwOnError);

      /// <summary>Создает экземпляр COM-объекта.</summary>
      /// <returns>Созданный COM-объект</returns>
      public object CreateInstance()
      {
        return ComActivator.CreateInstance(this.GetComType(true).GUID, this.InprocServer ? RegistrationClassContext.InProcessServer : RegistrationClassContext.LocalServer);
      }

      /// <summary>
      /// Возвращает рабочий экземпляр COM-объекта, глобально опубликованный в системе для доступа из других приложений. Если такого экземпляра
      /// COM-объекта нет, то метод вернет null.
      /// </summary>
      /// <returns>COM-объект или null</returns>
      public abstract object TryGetRunningInstance();

      /// <summary>
      /// Возвращает признак, что COM-объект реализован как in-process сервер.
      /// </summary>
      public bool InprocServer => this.inprocServer;

      /// <summary>Возвращает признак, что COM-объект зарегистрирован.</summary>
      public abstract bool IsRegistered();
    }
}
