
// Type: Intermech.Runtime.ComInterop.ClsidProvider
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Win32;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Реализует провайдер COM-объекта на основе CLSID. Внимание! Разрядность COM-объекта должна соответствовать разрядности IPS.
    /// </summary>
    public sealed class ClsidProvider : ComObjectProvider
    {
      private readonly Guid clsid;

      /// <summary>Создает объект.</summary>
      /// <param name="clsid">CLSID COM-объекта</param>
      /// <param name="inprocessServer">Признак, что COM-объект реализован как in-process сервер</param>
      /// <exception cref="T:System.ArgumentException">CLSID COM-объекта не задан</exception>
      public ClsidProvider(Guid clsid, bool inprocessServer)
        : base(inprocessServer)
      {
        this.clsid = !(clsid == Guid.Empty) ? clsid : throw new ArgumentException();
      }

      /// <summary>
      /// Реализует ленивое получение управляемого типа COM-объекта.
      /// </summary>
      /// <param name="throwOnError">Признак, что нужно сгенерировать исключение, если указанный COM-объект не зарегистрирован</param>
      /// <returns>Управляемый тип COM-объекта или null, если указанный COM-объект не зарегистрирован</returns>
      /// <exception cref="T:System.Runtime.InteropServices.COMException">Указанный COM-объект не зарегистрирован</exception>
      public override Type GetComType(bool throwOnError)
      {
        return Type.GetTypeFromCLSID(this.clsid, throwOnError);
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
          Guid clsid = this.clsid;
          return Marshal.GetActiveObject(NativeMethods.ProgIDFromCLSID(ref clsid));
        }
        catch (COMException ex)
        {
          return (object) null;
        }
      }

      /// <summary>Возвращает признак, что COM-объект зарегистрирован.</summary>
      public override bool IsRegistered()
      {
        string subkey1 = $"CLSID\\{this.clsid:B}";
        try
        {
          return RegistryHelper.SubKeyExists(RegistryHive.ClassesRoot, subkey1) && Array.Exists(RegistryHelper.EnumSubKeys(RegistryHive.ClassesRoot, subkey1), (Predicate<string>) (subkey => StringComparer.CurrentCultureIgnoreCase.Compare(subkey, "InprocServer32") == 0 || StringComparer.CurrentCultureIgnoreCase.Compare(subkey, "LocalServer32") == 0));
        }
        catch
        {
          return false;
        }
      }
    }
}
