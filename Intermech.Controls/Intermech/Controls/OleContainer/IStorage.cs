
// Type: Intermech.Controls.OleContainer.IStorage
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("0000000b-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IStorage
{
  int CreateStream(
    string pwcsName,
    uint grfMode,
    uint reserved1,
    uint reserved2,
    out IStream ppstm);

  int OpenStream(
    string pwcsName,
    IntPtr reserved1,
    uint grfMode,
    uint reserved2,
    out IStream ppstm);

  int CreateStorage(
    string pwcsName,
    uint grfMode,
    uint reserved1,
    uint reserved2,
    out IStorage ppstg);

  int OpenStorage(
    string pwcsName,
    IStorage pstgPriority,
    uint grfMode,
    IntPtr snbExclude,
    uint reserved,
    out IStorage ppstg);

  int CopyTo(uint ciidExclude, Guid rgiidExclude, IntPtr snbExclude, IStorage pstgDest);

  int MoveElementTo(string pwcsName, IStorage pstgDest, string pwcsNewName, uint grfFlags);

  int Commit(uint grfCommitFlags);

  int Revert();

  int EnumElements(uint reserved1, IntPtr reserved2, uint reserved3, out IEnumSTATSTG ppenum);

  int DestroyElement(string pwcsName);

  int RenameElement(string pwcsOldName, string pwcsNewName);

  int SetElementTimes(string pwcsName, FILETIME pctime, FILETIME patime, FILETIME pmtime);

  int SetClass(Guid clsid);

  int SetStateBits(uint grfStateBits, uint grfMask);

  int Stat(out STATSTG pstatstg, uint grfStatFlag);
}
