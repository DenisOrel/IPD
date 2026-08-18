
// Type: Intermech.Client.Core.ThumbnailDocs.StructuredStorage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace Intermech.Client.Core.ThumbnailDocs;

public sealed class StructuredStorage
{
  public static readonly Guid SummaryInformationFormatId = new Guid("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}");
  public static readonly Guid DocSummaryInformationFormatId = new Guid("{D5CDD502-2E9C-101B-9397-08002B2CF9AE}");
  public static readonly Guid UserDefinedPropertiesId = new Guid("{D5CDD505-2E9C-101B-9397-08002B2CF9AE}");
  private List<StructuredProperty> _properties = new List<StructuredProperty>();
  private const int STG_E_FILENOTFOUND = -2147287038 /*0x80030002*/;
  private const int STG_E_PATHNOTFOUND = -2147287037 /*0x80030003*/;
  private const int STG_E_ACCESSDENIED = -2147287035 /*0x80030005*/;

  public StructuredStorage(string filePath)
  {
    this.FilePath = filePath != null ? filePath : throw new ArgumentNullException(nameof (filePath));
    StructuredStorage.IPropertySetStorage ppObjectOpen;
    int error = StructuredStorage.StgOpenStorageEx(this.FilePath, StructuredStorage.STGM.STGM_SHARE_DENY_NONE | StructuredStorage.STGM.STGM_DIRECT_SWMR, StructuredStorage.STGFMT.STGFMT_ANY, 0, IntPtr.Zero, IntPtr.Zero, typeof (StructuredStorage.IPropertySetStorage).GUID, out ppObjectOpen);
    switch (error)
    {
      case -2147287038 /*0x80030002*/:
      case -2147287037 /*0x80030003*/:
        throw new FileNotFoundException((string) null, this.FilePath);
      case 0:
        try
        {
          this.LoadPropertySet(ppObjectOpen, StructuredStorage.SummaryInformationFormatId);
          this.LoadPropertySet(ppObjectOpen, StructuredStorage.DocSummaryInformationFormatId);
        }
        finally
        {
          Marshal.ReleaseComObject((object) ppObjectOpen);
        }
        this.LoadProperties(StructuredStorage.UserDefinedPropertiesId);
        break;
      default:
        throw new Win32Exception(error);
    }
  }

  public StructuredStorage(string filePath, Guid formatId, string DirName = "")
  {
    this.FilePath = filePath != null ? filePath : throw new ArgumentNullException(nameof (filePath));
    this.LoadProperties(formatId, DirName);
  }

  public string FilePath { get; private set; }

  public IReadOnlyList<StructuredProperty> Properties
  {
    get => (IReadOnlyList<StructuredProperty>) this._properties;
  }

  private void LoadPropertySet(
    StructuredStorage.IPropertySetStorage propertySetStorage,
    Guid fmtid)
  {
    StructuredStorage.IPropertyStorage ppprstg;
    int error1 = propertySetStorage.Open(fmtid, StructuredStorage.STGM.STGM_SHARE_EXCLUSIVE, out ppprstg);
    switch (error1)
    {
      case -2147287038 /*0x80030002*/:
        break;
      case -2147287035 /*0x80030005*/:
        break;
      case 0:
        StructuredStorage.IEnumSTATPROPSTG ppenum;
        ppprstg.Enum(out ppenum);
        if (ppenum == null)
          break;
        try
        {
          StructuredStorage.STATPROPSTG rgelt = new StructuredStorage.STATPROPSTG();
          int pceltFetched;
          do
          {
            int error2 = ppenum.Next(1, ref rgelt, out pceltFetched);
            switch (error2)
            {
              case 0:
              case 1:
                if (pceltFetched == 1)
                {
                  string propertyName = StructuredStorage.GetPropertyName(fmtid, ppprstg, rgelt);
                  StructuredStorage.PROPSPEC[] rgpspec = new StructuredStorage.PROPSPEC[1]
                  {
                    new StructuredStorage.PROPSPEC()
                  };
                  rgpspec[0].ulKind = rgelt.lpwstrName != null ? StructuredStorage.PRSPEC.PRSPEC_LPWSTR : StructuredStorage.PRSPEC.PRSPEC_PROPID;
                  IntPtr ptr = IntPtr.Zero;
                  if (rgelt.lpwstrName != null)
                  {
                    ptr = Marshal.StringToCoTaskMemUni(rgelt.lpwstrName);
                    rgpspec[0].union.lpwstr = ptr;
                  }
                  else
                    rgpspec[0].union.propid = rgelt.propid;
                  StructuredStorage.PROPVARIANT[] rgpropvar = new StructuredStorage.PROPVARIANT[1]
                  {
                    new StructuredStorage.PROPVARIANT()
                  };
                  try
                  {
                    int error3 = ppprstg.ReadMultiple(1U, rgpspec, rgpropvar);
                    if (error3 != 0)
                      throw new Win32Exception(error3);
                  }
                  finally
                  {
                    if (ptr != IntPtr.Zero)
                      Marshal.FreeCoTaskMem(ptr);
                  }
                  object obj;
                  try
                  {
                    switch (rgpropvar[0].vt)
                    {
                      case StructuredStorage.VARTYPE.VT_I2:
                        obj = (object) rgpropvar[0].union.iVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_I4:
                        obj = (object) rgpropvar[0].union.lVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_R4:
                        obj = (object) rgpropvar[0].union.fltVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_R8:
                        obj = (object) rgpropvar[0].union.dblVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_CY:
                        obj = (object) Decimal.FromOACurrency(rgpropvar[0].union.cyVal);
                        break;
                      case StructuredStorage.VARTYPE.VT_DATE:
                        obj = (object) DateTime.FromOADate(rgpropvar[0].union.date);
                        break;
                      case StructuredStorage.VARTYPE.VT_BSTR:
                        obj = (object) Marshal.PtrToStringUni(rgpropvar[0].union.bstrVal);
                        break;
                      case StructuredStorage.VARTYPE.VT_DISPATCH:
                        obj = Marshal.GetObjectForIUnknown(rgpropvar[0].union.pdispVal);
                        break;
                      case StructuredStorage.VARTYPE.VT_ERROR:
                      case StructuredStorage.VARTYPE.VT_HRESULT:
                        obj = (object) rgpropvar[0].union.scode;
                        break;
                      case StructuredStorage.VARTYPE.VT_BOOL:
                        obj = (object) (rgpropvar[0].union.boolVal != (short) 0);
                        break;
                      case StructuredStorage.VARTYPE.VT_UNKNOWN:
                        obj = Marshal.GetObjectForIUnknown(rgpropvar[0].union.punkVal);
                        break;
                      case StructuredStorage.VARTYPE.VT_DECIMAL:
                        IntPtr zero = IntPtr.Zero;
                        Marshal.StructureToPtr<StructuredStorage.PROPVARIANT>(rgpropvar[0], zero, false);
                        obj = Marshal.PtrToStructure(zero, typeof (Decimal));
                        break;
                      case StructuredStorage.VARTYPE.VT_I1:
                        obj = (object) rgpropvar[0].union.cVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_UI1:
                        obj = (object) rgpropvar[0].union.bVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_UI2:
                        obj = (object) rgpropvar[0].union.uiVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_UI4:
                        obj = (object) rgpropvar[0].union.ulVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_I8:
                        obj = (object) rgpropvar[0].union.hVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_UI8:
                        obj = (object) rgpropvar[0].union.uhVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_INT:
                        obj = (object) rgpropvar[0].union.intVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_UINT:
                        obj = (object) rgpropvar[0].union.uintVal;
                        break;
                      case StructuredStorage.VARTYPE.VT_LPSTR:
                        obj = (object) Marshal.PtrToStringAnsi(rgpropvar[0].union.pszVal);
                        break;
                      case StructuredStorage.VARTYPE.VT_LPWSTR:
                        obj = (object) Marshal.PtrToStringUni(rgpropvar[0].union.pwszVal);
                        break;
                      case StructuredStorage.VARTYPE.VT_FILETIME:
                        obj = (object) DateTime.FromFileTime(rgpropvar[0].union.filetime);
                        break;
                      case StructuredStorage.VARTYPE.VT_CF:
                        byte[] destination1 = new byte[Marshal.SizeOf(typeof (StructuredStorage.CLIPDATA))];
                        Marshal.Copy(rgpropvar[0].union.pszVal, destination1, 0, Marshal.SizeOf(typeof (StructuredStorage.CLIPDATA)));
                        GCHandle gcHandle = GCHandle.Alloc((object) destination1, GCHandleType.Pinned);
                        StructuredStorage.CLIPDATA structure = (StructuredStorage.CLIPDATA) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (StructuredStorage.CLIPDATA));
                        gcHandle.Free();
                        int length = (int) structure.cbSize - 4;
                        if (length != 0)
                        {
                          byte[] destination2 = new byte[length];
                          Marshal.Copy(structure.pClipData, destination2, 0, length);
                          obj = (object) destination2;
                          break;
                        }
                        obj = (object) null;
                        break;
                      case StructuredStorage.VARTYPE.VT_CLSID:
                        byte[] numArray = new byte[16 /*0x10*/];
                        Marshal.Copy(rgpropvar[0].union.pszVal, numArray, 0, 16 /*0x10*/);
                        obj = (object) new Guid(numArray);
                        break;
                      default:
                        obj = (object) null;
                        break;
                    }
                  }
                  finally
                  {
                    StructuredStorage.PropVariantClear(ref rgpropvar[0]);
                  }
                  this._properties.Add(new StructuredProperty(fmtid, propertyName, rgelt.propid)
                  {
                    Value = obj
                  });
                }
                continue;
              default:
                throw new Win32Exception(error2);
            }
          }
          while (pceltFetched == 1);
          break;
        }
        finally
        {
          Marshal.ReleaseComObject((object) ppenum);
        }
      default:
        throw new Win32Exception(error1);
    }
  }

  private static string GetPropertyName(
    Guid fmtid,
    StructuredStorage.IPropertyStorage propertyStorage,
    StructuredStorage.STATPROPSTG stg)
  {
    if (!string.IsNullOrEmpty(stg.lpwstrName))
      return stg.lpwstrName;
    int[] rgpropid = new int[1]{ stg.propid };
    string[] rglpwstrName = new string[1]{ (string) null };
    return propertyStorage.ReadPropertyNames(1U, rgpropid, rglpwstrName) == 0 ? rglpwstrName[0] : (string) null;
  }

  public void LoadProperties(Guid formatId, string DirName = "")
  {
    StructuredStorage.IPropertySetStorage ppObjectOpen = (StructuredStorage.IPropertySetStorage) null;
    StructuredStorage.IStorage ppstgOpen = (StructuredStorage.IStorage) null;
    StructuredStorage.IStorage ppstg = (StructuredStorage.IStorage) null;
    Guid guid = typeof (StructuredStorage.IPropertySetStorage).GUID;
    try
    {
      int error1;
      if (DirName == "")
      {
        error1 = StructuredStorage.StgOpenStorageEx(this.FilePath, StructuredStorage.STGM.STGM_SHARE_DENY_NONE | StructuredStorage.STGM.STGM_DIRECT_SWMR, StructuredStorage.STGFMT.STGFMT_ANY, 0, IntPtr.Zero, IntPtr.Zero, guid, out ppObjectOpen);
      }
      else
      {
        int error2 = StructuredStorage.StgOpenStorage(this.FilePath, (StructuredStorage.IStorage) null, StructuredStorage.STGM.STGM_SHARE_DENY_NONE | StructuredStorage.STGM.STGM_DIRECT_SWMR, IntPtr.Zero, 0U, out ppstgOpen);
        if (error2 != 0)
          throw new Win32Exception(error2);
        error1 = ppstgOpen.OpenStorage(DirName, (StructuredStorage.IStorage) null, StructuredStorage.STGM.STGM_SHARE_EXCLUSIVE, IntPtr.Zero, 0U, out ppstg);
        if (error1 != 0)
          throw new Win32Exception(error1);
        ppObjectOpen = (StructuredStorage.IPropertySetStorage) ppstg;
      }
      if (error1 == -2147287038 /*0x80030002*/ || error1 == -2147287037 /*0x80030003*/)
        throw new FileNotFoundException((string) null, this.FilePath);
      if (error1 != 0)
        throw new Win32Exception(error1);
      this.LoadPropertySet(ppObjectOpen, formatId);
    }
    finally
    {
      if (ppObjectOpen != null)
        Marshal.ReleaseComObject((object) ppObjectOpen);
      if (ppstg != null)
        Marshal.ReleaseComObject((object) ppstg);
      if (ppstgOpen != null)
        Marshal.ReleaseComObject((object) ppstgOpen);
    }
  }

  [DllImport("ole32.dll")]
  private static extern int StgOpenStorage(
    [MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
    StructuredStorage.IStorage pstgPriority,
    StructuredStorage.STGM grfMode,
    IntPtr snbExclude,
    uint reserved,
    out StructuredStorage.IStorage ppstgOpen);

  [DllImport("ole32.dll")]
  private static extern int StgOpenStorageEx(
    [MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
    StructuredStorage.STGM grfMode,
    StructuredStorage.STGFMT stgfmt,
    int grfAttrs,
    IntPtr pStgOptions,
    IntPtr reserved2,
    [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
    out StructuredStorage.IPropertySetStorage ppObjectOpen);

  [DllImport("ole32.dll")]
  private static extern int StgOpenStorageEx(
    [MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
    StructuredStorage.STGM grfMode,
    StructuredStorage.STGFMT stgfmt,
    int grfAttrs,
    IntPtr pStgOptions,
    IntPtr reserved2,
    [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
    out StructuredStorage.IStorage ppObjectOpen);

  [DllImport("ole32.dll")]
  private static extern int StgOpenPropStg(
    ref StructuredStorage.IStorage pUnk,
    Guid fmtid,
    StructuredStorage.STGM grfFlags,
    uint dwReserved,
    out StructuredStorage.IPropertyStorage ppPropStg);

  [DllImport("ole32.dll")]
  private static extern int PropVariantClear(ref StructuredStorage.PROPVARIANT pvar);

  private enum PRSPEC
  {
    PRSPEC_LPWSTR,
    PRSPEC_PROPID,
  }

  private enum STGFMT
  {
    STGFMT_ANY = 4,
  }

  [Flags]
  private enum STGM
  {
    STGM_READ = 0,
    STGM_READWRITE = 2,
    STGM_SHARE_DENY_NONE = 64, // 0x00000040
    STGM_SHARE_DENY_WRITE = 32, // 0x00000020
    STGM_SHARE_EXCLUSIVE = 16, // 0x00000010
    STGM_DIRECT_SWMR = 4194304, // 0x00400000
  }

  private enum VARTYPE : short
  {
    VT_I2 = 2,
    VT_I4 = 3,
    VT_R4 = 4,
    VT_R8 = 5,
    VT_CY = 6,
    VT_DATE = 7,
    VT_BSTR = 8,
    VT_DISPATCH = 9,
    VT_ERROR = 10, // 0x000A
    VT_BOOL = 11, // 0x000B
    VT_UNKNOWN = 13, // 0x000D
    VT_DECIMAL = 14, // 0x000E
    VT_I1 = 16, // 0x0010
    VT_UI1 = 17, // 0x0011
    VT_UI2 = 18, // 0x0012
    VT_UI4 = 19, // 0x0013
    VT_I8 = 20, // 0x0014
    VT_UI8 = 21, // 0x0015
    VT_INT = 22, // 0x0016
    VT_UINT = 23, // 0x0017
    VT_HRESULT = 25, // 0x0019
    VT_LPSTR = 30, // 0x001E
    VT_LPWSTR = 31, // 0x001F
    VT_FILETIME = 64, // 0x0040
    VT_CF = 71, // 0x0047
    VT_CLSID = 72, // 0x0048
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct PROPVARIANTunion
  {
    [FieldOffset(0)]
    public sbyte cVal;
    [FieldOffset(0)]
    public byte bVal;
    [FieldOffset(0)]
    public short iVal;
    [FieldOffset(0)]
    public ushort uiVal;
    [FieldOffset(0)]
    public int lVal;
    [FieldOffset(0)]
    public uint ulVal;
    [FieldOffset(0)]
    public int intVal;
    [FieldOffset(0)]
    public uint uintVal;
    [FieldOffset(0)]
    public long hVal;
    [FieldOffset(0)]
    public ulong uhVal;
    [FieldOffset(0)]
    public float fltVal;
    [FieldOffset(0)]
    public double dblVal;
    [FieldOffset(0)]
    public short boolVal;
    [FieldOffset(0)]
    public int scode;
    [FieldOffset(0)]
    public long cyVal;
    [FieldOffset(0)]
    public double date;
    [FieldOffset(0)]
    public long filetime;
    [FieldOffset(0)]
    public IntPtr bstrVal;
    [FieldOffset(0)]
    public IntPtr pszVal;
    [FieldOffset(0)]
    public IntPtr pwszVal;
    [FieldOffset(0)]
    public IntPtr punkVal;
    [FieldOffset(0)]
    public IntPtr pdispVal;
  }

  private struct PROPSPEC
  {
    public StructuredStorage.PRSPEC ulKind;
    public StructuredStorage.PROPSPECunion union;
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct PROPSPECunion
  {
    [FieldOffset(0)]
    public int propid;
    [FieldOffset(0)]
    public IntPtr lpwstr;
  }

  private struct PROPVARIANT
  {
    public StructuredStorage.VARTYPE vt;
    public ushort wReserved1;
    public ushort wReserved2;
    public ushort wReserved3;
    public StructuredStorage.PROPVARIANTunion union;
  }

  private struct STATPROPSTG
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string lpwstrName;
    public int propid;
    public StructuredStorage.VARTYPE vt;
  }

  private struct STATPROPSETSTG
  {
    public Guid fmtid;
    public Guid clsid;
    public uint grfFlags;
    public System.Runtime.InteropServices.ComTypes.FILETIME mtime;
    public System.Runtime.InteropServices.ComTypes.FILETIME ctime;
    public System.Runtime.InteropServices.ComTypes.FILETIME atime;
    public uint dwOSVersion;
  }

  private struct CLIPDATA
  {
    public uint cbSize;
    public int ulClipFmt;
    public IntPtr pClipData;
  }

  [Guid("0000013B-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  private interface IEnumSTATPROPSETSTG
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, ref StructuredStorage.STATPROPSETSTG rgelt, out int pceltFetched);
  }

  [Guid("00000139-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  private interface IEnumSTATPROPSTG
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, ref StructuredStorage.STATPROPSTG rgelt, out int pceltFetched);
  }

  [Guid("0000000B-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  private interface IStorage
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void CreateStream(
      string pwcsName,
      uint grfMode,
      uint reserved1,
      uint reserved2,
      out IStream ppstm);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void OpenStream(
      string pwcsName,
      IntPtr reserved1,
      uint grfMode,
      uint reserved2,
      out IStream ppstm);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void CreateStorage(
      string pwcsName,
      uint grfMode,
      uint reserved1,
      uint reserved2,
      out StructuredStorage.IStorage ppstg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OpenStorage(
      [MarshalAs(UnmanagedType.BStr)] string pwcsName,
      StructuredStorage.IStorage pstgPriority,
      StructuredStorage.STGM grfMode,
      IntPtr snbExclude,
      uint reserved,
      out StructuredStorage.IStorage ppstg);
  }

  [Guid("00000138-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  private interface IPropertyStorage
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ReadMultiple(
      uint cpspec,
      [MarshalAs(UnmanagedType.LPArray)] StructuredStorage.PROPSPEC[] rgpspec,
      [MarshalAs(UnmanagedType.LPArray), Out] StructuredStorage.PROPVARIANT[] rgpropvar);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int WriteMultiple(
      uint cpspec,
      [MarshalAs(UnmanagedType.LPArray)] StructuredStorage.PROPSPEC[] rgpspec,
      [MarshalAs(UnmanagedType.LPArray)] StructuredStorage.PROPVARIANT[] rgpropvar,
      uint propidNameFirst);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DeleteMultiple(uint cpspec, [MarshalAs(UnmanagedType.LPArray)] StructuredStorage.PROPSPEC[] rgpspec);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ReadPropertyNames(uint cpropid, [MarshalAs(UnmanagedType.LPArray)] int[] rgpropid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr), Out] string[] rglpwstrName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NotDeclared1();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NotDeclared2();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Commit(uint grfCommitFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NotDeclared3();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Enum(out StructuredStorage.IEnumSTATPROPSTG ppenum);
  }

  [Guid("0000013A-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  private interface IPropertySetStorage
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Create(
      [MarshalAs(UnmanagedType.LPStruct)] Guid rfmtid,
      [MarshalAs(UnmanagedType.LPStruct)] Guid pclsid,
      uint grfFlags,
      StructuredStorage.STGM grfMode,
      out StructuredStorage.IPropertyStorage ppprstg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Open(
      [MarshalAs(UnmanagedType.LPStruct)] Guid rfmtid,
      StructuredStorage.STGM grfMode,
      out StructuredStorage.IPropertyStorage ppprstg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NotDeclared3();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Enum(out StructuredStorage.IEnumSTATPROPSETSTG ppenum);
  }
}
