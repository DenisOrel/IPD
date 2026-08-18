using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace Intermech.Printing
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
    internal struct PrinterDefaults
    {
      [MarshalAs(UnmanagedType.SysInt)]
      private readonly IntPtr dDataType;
      [MarshalAs(UnmanagedType.SysInt)]
      private readonly IntPtr dDeviceMode;
      [MarshalAs(UnmanagedType.U4)]
      public int DesiredAccess;

      internal PrinterDefaults(bool allAccess)
      {
        this.dDataType = IntPtr.Zero;
        this.dDeviceMode = IntPtr.Zero;
        if (allAccess)
        {
          new PrintingPermission(PermissionState.Unrestricted).Demand();
          this.DesiredAccess = 983052 /*0x0F000C*/;
        }
        else
          this.DesiredAccess = 8;
      }
    }
}
