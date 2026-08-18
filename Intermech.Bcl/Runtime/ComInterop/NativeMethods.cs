
// Type: Intermech.Runtime.ComInterop.NativeMethods
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime.ComInterop.ComTypes;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace Intermech.Runtime.ComInterop
{
    internal static class NativeMethods
    {
      private const string ole32 = "ole32.dll";
      private const string oleaut32 = "oleaut32.dll";

      [DllImport("ole32.dll", PreserveSig = false)]
      [return: MarshalAs(UnmanagedType.LPWStr)]
      internal static extern string ProgIDFromCLSID([In] ref Guid clsid);

      [DllImport("ole32.dll", PreserveSig = false)]
      [return: MarshalAs(UnmanagedType.Interface)]
      internal static extern object CoCreateInstance(
        [In] ref Guid clsid,
        [MarshalAs(UnmanagedType.Interface)] object punkOuter,
        RegistrationClassContext context,
        [In] ref Guid iid);

      [DllImport("ole32.dll", PreserveSig = false)]
      internal static extern IStorage StgOpenStorageEx(
        [MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
        uint grfMode,
        uint stgfmt,
        uint grfAttrs,
        IntPtr pStgOptions,
        IntPtr reserved2,
        [In] ref Guid riid);

      [DllImport("ole32.dll", PreserveSig = false)]
      internal static extern IStorage StgOpenStorageOnILockBytes(
        ILockBytes plkbyt,
        IStorage pStgPriority,
        uint grfMode,
        IntPtr snbEnclude,
        uint reserved);

      [DllImport("ole32.dll", PreserveSig = false)]
      internal static extern uint CoRegisterClassObject(
        [MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
        [MarshalAs(UnmanagedType.Interface)] object pUnk,
        uint dwClsContext,
        uint flags);

      [DllImport("ole32.dll", PreserveSig = false)]
      internal static extern void CoRevokeClassObject(uint cookie);

      [DllImport("ole32.dll", PreserveSig = false)]
      internal static extern void CoSuspendClassObjects();

      [DllImport("ole32.dll", PreserveSig = false)]
      internal static extern void CoResumeClassObjects();

      [DllImport("ole32.dll")]
      internal static extern uint CoAddRefServerProcess();

      [DllImport("ole32.dll")]
      internal static extern uint CoReleaseServerProcess();

      [DllImport("ole32.dll", PreserveSig = false)]
      [return: MarshalAs(UnmanagedType.Interface)]
      internal static extern object CoGetClassObject(
        [MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
        RegistrationClassContext dwClsContext,
        IntPtr pServerInfo,
        [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

      [DllImport("ole32.dll")]
      internal static extern int CoRegisterMessageFilter(
        [MarshalAs(UnmanagedType.Interface)] IMessageFilter lpMessageFilter,
        [MarshalAs(UnmanagedType.Interface)] out IMessageFilter lplpMessageFilter);

      [DllImport("oleaut32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
      internal static extern ITypeLib LoadTypeLib(string fullPath);

      [DllImport("oleaut32.dll", CharSet = CharSet.Auto)]
      internal static extern int RegisterTypeLib(ITypeLib typeLib, string fullPath, string helpDir);

      [DllImport("oleaut32.dll", CharSet = CharSet.Auto)]
      internal static extern int UnRegisterTypeLib(
        ref Guid libID,
        short wVerMajor,
        short wVerMinor,
        int lCID,
        System.Runtime.InteropServices.ComTypes.SYSKIND tSysKind);

      [DllImport("oleaut32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
      [return: MarshalAs(UnmanagedType.BStr)]
      internal static extern string QueryPathOfRegTypeLib(
        ref Guid guid,
        short majorVersion,
        short minorVersion,
        int lcid);
    }
}
