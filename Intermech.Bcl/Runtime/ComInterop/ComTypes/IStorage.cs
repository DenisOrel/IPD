
// Type: Intermech.Runtime.ComInterop.ComTypes.IStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace Intermech.Runtime.ComInterop.ComTypes
{
    [Guid("0000000B-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    internal interface IStorage
    {
      void CreateStream(
        string pwcsName,
        uint grfMode,
        uint reserved1,
        uint reserved2,
        out IStream ppstm);

      void OpenStream(
        string pwcsName,
        IntPtr reserved1,
        uint grfMode,
        uint reserved2,
        out IStream ppstm);

      void CreateStorage(
        string pwcsName,
        uint grfMode,
        uint reserved1,
        uint reserved2,
        out IStorage ppstg);

      void OpenStorage(
        string pwcsName,
        IStorage pstgPriority,
        uint grfMode,
        IntPtr snbExclude,
        uint reserved,
        out IStorage ppstg);

      void CopyTo(uint ciidExclude, Guid rgiidExclude, IntPtr snbExclude, IStorage pstgDest);

      void MoveElementTo(string pwcsName, IStorage pstgDest, string pwcsNewName, uint grfFlags);

      void Commit(uint grfCommitFlags);

      void Revert();

      void EnumElements(uint reserved1, IntPtr reserved2, uint reserved3, out IEnumSTATSTG ppenum);

      void DestroyElement(string pwcsName);

      void RenameElement(string pwcsOldName, string pwcsNewName);

      void SetElementTimes(string pwcsName, System.Runtime.InteropServices.ComTypes.FILETIME pctime, System.Runtime.InteropServices.ComTypes.FILETIME patime, System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

      void SetClass(Guid clsid);

      void SetStateBits(uint grfStateBits, uint grfMask);

      void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, STATFLAG grfStatFlag);
    }
}
