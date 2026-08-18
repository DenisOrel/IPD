
// Type: Intermech.Runtime.ComInterop.StgServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime.ComInterop.ComTypes;
using System;
using System.IO;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Предоставляет общедоступные методы для работы со структурированными хранилищами.
    /// </summary>
    public static class StgServices
    {
      /// <summary>
      /// Возвращает глобальный идентификатор объекта, создавшего это составное хранилище.
      /// </summary>
      /// <param name="fileInfo">Информация о файле</param>
      /// <param name="fileStream">Содержимое файла</param>
      /// <returns>Глобальный идентификатор объекта-создателя</returns>
      /// <exception cref="T:System.ArgumentNullException">Не задан один из аргументов метода</exception>
      /// <exception cref="T:System.Runtime.InteropServices.COMException">Ошибка чтения хранилища</exception>
      public static Guid GetStorageGuid(FileInfo fileInfo, Stream fileStream)
      {
        IStorage o;
        try
        {
          o = NativeMethods.StgOpenStorageOnILockBytes((ILockBytes) new StreamLockBytes(fileInfo, fileStream), (IStorage) null, 32U /*0x20*/, IntPtr.Zero, 0U);
        }
        catch (COMException ex)
        {
          if (ex.ErrorCode == -2147286960 /*0x80030050*/)
            return Guid.Empty;
          throw;
        }
        try
        {
          System.Runtime.InteropServices.ComTypes.STATSTG pstatstg;
          o.Stat(out pstatstg, STATFLAG.STATFLAG_NONAME);
          return pstatstg.clsid;
        }
        finally
        {
          Marshal.ReleaseComObject((object) o);
        }
      }

      /// <summary>
      /// Возвращает глобальный идентификатор объекта, создавшего это составное хранилище.
      /// </summary>
      /// <param name="fileInfo">Информация о файле</param>
      /// <param name="fileStream">Содержимое файла</param>
      /// <returns>Глобальный идентификатор объекта-создателя</returns>
      /// <exception cref="T:System.ArgumentNullException">Не задан один из аргументов метода</exception>
      /// <exception cref="T:System.Runtime.InteropServices.COMException">Ошибка чтения хранилища</exception>
      public static Guid GetStorageGuidEx(FileInfo fileInfo, Stream fileStream)
      {
        IStorage o = StgServices.TryOpenStorage(fileInfo.FullName);
        if (o == null)
          return Guid.Empty;
        try
        {
          System.Runtime.InteropServices.ComTypes.STATSTG pstatstg;
          o.Stat(out pstatstg, STATFLAG.STATFLAG_NONAME);
          return pstatstg.clsid;
        }
        finally
        {
          Marshal.ReleaseComObject((object) o);
        }
      }

      private static IStorage TryOpenStorage(string filePath)
      {
        try
        {
          uint grfMode = 65600 /*0x010040*/;
          uint stgfmt = 0;
          uint grfAttrs = 0;
          Guid iidIstorage = ComGuids.IID_IStorage;
          return NativeMethods.StgOpenStorageEx(filePath, grfMode, stgfmt, grfAttrs, IntPtr.Zero, IntPtr.Zero, ref iidIstorage);
        }
        catch (COMException ex)
        {
          if (ex.ErrorCode == -2147286960 /*0x80030050*/)
            return (IStorage) null;
          throw;
        }
      }

      [DllImport("ole32.dll")]
      public static extern int StgIsStorageFile([MarshalAs(UnmanagedType.LPWStr)] string pwcsName);
    }
}
