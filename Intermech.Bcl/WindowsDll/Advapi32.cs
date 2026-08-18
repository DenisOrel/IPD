using Intermech.Diagnostics;
using System;
using System.Runtime.InteropServices;


namespace Intermech.WindowsDll
{
    [CLSCompliant(false)]
    public static class Advapi32
    {
      private const string LibName = "Advapi32.dll";
      private const string Namespace = "Advapi32::";

      [DllImport("advapi32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool OpenProcessToken(
        [NotEmpty] IntPtr processHandle,
        [MarshalAs(UnmanagedType.U4)] TokenAccessRights desiredAccess,
        out IntPtr tokenHandle);

      public static IntPtr OpenProcessToken_ThrowWinErrors(
        [NotEmpty] IntPtr processHandle,
        TokenAccessRights desiredAccess)
      {
        IntPtr tokenHandle;
        Exception exception;
        if (!Advapi32.TryOpenProcessToken(processHandle, desiredAccess, out tokenHandle, out exception))
          throw exception;
        return tokenHandle;
      }

      [ContractAnnotation("=> true, exception: null; => false, exception: NotNull")]
      public static bool TryOpenProcessToken(
        [NotEmpty] IntPtr processHandle,
        TokenAccessRights desiredAccess,
        out IntPtr tokenHandle,
        out Exception exception)
      {
        bool flag;
        try
        {
          flag = Advapi32.OpenProcessToken(processHandle, desiredAccess, out tokenHandle);
        }
        catch (Exception ex)
        {
          exception = ex;
          tokenHandle = IntPtr.Zero;
          return false;
        }
        if (!flag || tokenHandle == IntPtr.Zero)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          exception = (Exception) WindowsApiException.GetLastForce("Advapi32::OpenProcessToken", (ArgumentDescriptor) @(typeof (IntPtr), (object) processHandle), (ArgumentDescriptor) @(typeof (TokenAccessRights), (object) desiredAccess), (ArgumentDescriptor) @(Modifier.Out, typeof (IntPtr)));
          return false;
        }
        exception = (Exception) null;
        return true;
      }
    }
}
