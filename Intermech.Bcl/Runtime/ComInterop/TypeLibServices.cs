
// Type: Intermech.Runtime.ComInterop.TypeLibServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>Содержит сервисы для работы с библиотеками типов.</summary>
    public static class TypeLibServices
    {
      /// <summary>Читает заголовок библиотеки типов.</summary>
      /// <param name="fullPath">Абсолютный путь к файлу библиотеки</param>
      /// <returns>Заголовок библиотеки</returns>
      /// <exception cref="T:System.ArgumentException">Не задан путь к файлу библиотеки, либо путь к файлу не является абсолютным</exception>
      /// <exception cref="T:System.Runtime.Interop.COMException">При чтении файла библиотеки произошла ошибка</exception>
      public static System.Runtime.InteropServices.ComTypes.TYPELIBATTR ReadLibraryAttributes(
        string fullPath)
      {
        if (string.IsNullOrEmpty(fullPath))
          throw new ArgumentException(Resources.Arg_NullOrEmptyFileName, nameof (fullPath));
        ITypeLib o = Path.IsPathRooted(fullPath) ? NativeMethods.LoadTypeLib(fullPath) : throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Resources.Arg_AbsolutePathRequired, (object) fullPath), nameof (fullPath));
        try
        {
          IntPtr ppTLibAttr;
          o.GetLibAttr(out ppTLibAttr);
          return (System.Runtime.InteropServices.ComTypes.TYPELIBATTR) Marshal.PtrToStructure(ppTLibAttr, typeof (System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
        }
        finally
        {
          Marshal.ReleaseComObject((object) o);
        }
      }

      /// <summary>
      /// Возвращает путь к зарегистрированной библиотеке типов.
      /// </summary>
      /// <param name="typeLibId">Глобальный идентификатор библиотеки типов</param>
      /// <param name="major">Старший номер версии библиотеки типов</param>
      /// <param name="minor">Младший номер версии библиотеки типов</param>
      /// <returns>Путь к библиотеке типов или null, если такая библиотека не зарегистрирована</returns>
      public static string GetRegisteredPath(Guid typeLibId, short major, short minor)
      {
        try
        {
          string registeredPath = NativeMethods.QueryPathOfRegTypeLib(ref typeLibId, major, minor, CultureInfo.InvariantCulture.LCID);
          if (registeredPath != null && registeredPath.Length != 0)
          {
            int num = registeredPath.Length - 1;
            if (registeredPath[num] == char.MinValue)
              registeredPath = registeredPath.Remove(num, 1);
          }
          return registeredPath;
        }
        catch (COMException ex)
        {
          if (ex.ErrorCode == -2147319779)
            return (string) null;
          throw;
        }
      }

      /// <summary>
      /// Позволяет проверить, зарегистрирована ли требуемая библиотека типов.
      /// </summary>
      /// <param name="typeLibId">Глобальный идентификатор библиотеки типов</param>
      /// <param name="major">Старший номер версии библиотеки типов</param>
      /// <param name="minor">Младший номер версии библиотеки типов</param>
      /// <returns>true, если требуемая библиотека типов зарегистрирована</returns>
      public static bool IsRegistered(Guid typeLibId, short major, short minor)
      {
        string registeredPath = TypeLibServices.GetRegisteredPath(typeLibId, major, minor);
        return !string.IsNullOrEmpty(registeredPath) && File.Exists(registeredPath);
      }

      /// <summary>Выполняет регистрацию библиотеки типов.</summary>
      /// <param name="fullPath">Абсолютный путь к файлу библиотеки</param>
      /// <returns>Заголовок библиотеки</returns>
      /// <exception cref="T:System.ArgumentException">Не задан путь к файлу библиотеки, либо путь к файлу не является абсолютным</exception>
      /// <exception cref="T:System.Runtime.Interop.COMException">При регистрации библиотеки произошла ошибка</exception>
      public static System.Runtime.InteropServices.ComTypes.TYPELIBATTR RegisterLibrary(string fullPath)
      {
        if (string.IsNullOrEmpty(fullPath))
          throw new ArgumentException(Resources.Arg_NullOrEmptyFileName, nameof (fullPath));
        ITypeLib typeLib = Path.IsPathRooted(fullPath) ? NativeMethods.LoadTypeLib(fullPath) : throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Resources.Arg_AbsolutePathRequired, (object) fullPath), nameof (fullPath));
        try
        {
          IntPtr ppTLibAttr;
          typeLib.GetLibAttr(out ppTLibAttr);
          System.Runtime.InteropServices.ComTypes.TYPELIBATTR structure = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR) Marshal.PtrToStructure(ppTLibAttr, typeof (System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
          int errorCode = NativeMethods.RegisterTypeLib(typeLib, fullPath, (string) null);
          if (errorCode != 0)
            throw Marshal.GetExceptionForHR(errorCode);
          return structure;
        }
        finally
        {
          Marshal.ReleaseComObject((object) typeLib);
        }
      }

      /// <summary>Отменяет регистрацию библиотеки типов.</summary>
      /// <param name="fullPath">Абсолютный путь к файлу библиотеки</param>
      /// <returns>Заголовок библиотеки</returns>
      /// <exception cref="T:System.ArgumentException">Не задан путь к файлу библиотеки, либо путь к файлу не является абсолютным</exception>
      /// <exception cref="T:System.Runtime.Interop.COMException">При отмене регистрации библиотеки произошла ошибка</exception>
      public static System.Runtime.InteropServices.ComTypes.TYPELIBATTR UnregisterLibrary(
        string fullPath)
      {
        if (string.IsNullOrEmpty(fullPath))
          throw new ArgumentException(Resources.Arg_NullOrEmptyFileName, nameof (fullPath));
        ITypeLib o = Path.IsPathRooted(fullPath) ? NativeMethods.LoadTypeLib(fullPath) : throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Resources.Arg_AbsolutePathRequired, (object) fullPath), nameof (fullPath));
        try
        {
          IntPtr ppTLibAttr;
          o.GetLibAttr(out ppTLibAttr);
          System.Runtime.InteropServices.ComTypes.TYPELIBATTR structure = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR) Marshal.PtrToStructure(ppTLibAttr, typeof (System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
          int errorCode = NativeMethods.UnRegisterTypeLib(ref structure.guid, structure.wMajorVerNum, structure.wMinorVerNum, structure.lcid, structure.syskind);
          switch (errorCode)
          {
            case -2147319780:
            case 0:
              return structure;
            default:
              throw Marshal.GetExceptionForHR(errorCode);
          }
        }
        finally
        {
          Marshal.ReleaseComObject((object) o);
        }
      }
    }
}
