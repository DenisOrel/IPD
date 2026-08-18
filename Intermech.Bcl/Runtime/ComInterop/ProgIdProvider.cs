
// Type: Intermech.Runtime.ComInterop.ProgIdProvider
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>Реализует провайдер COM-объекта на основе ProgId.</summary>
    public sealed class ProgIdProvider : ComObjectProvider
    {
      private readonly string progId;

      /// <summary>Создает объект.</summary>
      /// <param name="progId">ProgId COM-объекта</param>
      /// <param name="inprocessServer">Признак, что COM-объект реализован как in-process сервер</param>
      /// <exception cref="T:System.ArgumentException">ProgId COM-объекта не задан</exception>
      public ProgIdProvider(string progId, bool inprocessServer)
        : base(inprocessServer)
      {
        this.progId = !string.IsNullOrEmpty(progId) ? progId : throw new ArgumentException();
      }

      /// <summary>Возвращает ProgId COM-объекта.</summary>
      public string ProgId => this.progId;

      /// <summary>
      /// Реализует ленивое получение управляемого типа COM-объекта.
      /// </summary>
      /// <param name="throwOnError">Признак, что нужно сгенерировать исключение, если указанный COM-объект не зарегистрирован</param>
      /// <returns>Управляемый тип COM-объекта или null, если указанный COM-объект не зарегистрирован</returns>
      /// <exception cref="T:System.Runtime.InteropServices.COMException">Указанный COM-объект не зарегистрирован</exception>
      public override Type GetComType(bool throwOnError)
      {
        return Type.GetTypeFromProgID(this.progId, throwOnError);
      }

      /// <summary>
      /// Возвращает рабочий экземпляр COM-объекта, глобально опубликованный в системе для доступа из других приложений. Если такого экземпляра
      /// COM-объекта нет, то метод вернет null.
      /// </summary>
      /// <returns>COM-объект или null</returns>
      public override object TryGetRunningInstance()
      {
        try
        {
          return Marshal.GetActiveObject(this.progId);
        }
        catch (COMException ex)
        {
          return (object) null;
        }
      }

      /// <summary>Возвращает признак, что COM-объект зарегистрирован.</summary>
      public override bool IsRegistered() => Type.GetTypeFromProgID(this.progId, false) != (Type) null;
    }
}
