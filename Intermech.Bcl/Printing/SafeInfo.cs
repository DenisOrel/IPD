using Intermech.Diagnostics;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;


namespace Intermech.Printing
{
    [Browsable(false)]
    [CLSCompliant(false)]
    public abstract class SafeInfo : SafeHandleZeroOrMinusOneIsInvalid
    {
      protected int _IntPtrSize = Marshal.SizeOf(typeof (IntPtr));
      internal int _Level;
      protected SafeHandle _PrinterSafeHandle;
      protected int _BytesNeeded;
      protected int _Size;

      protected abstract void RefreshInfo();

      protected SafeInfo(SafeHandle pSafeHandle, int infoLevel)
        : base(true)
      {
        this._PrinterSafeHandle = pSafeHandle;
        this._Level = infoLevel;
        this.RefreshInfo();
      }

      protected SafeInfo(IntPtr pMemoryHandle, int infoLevel)
        : base(false)
      {
        this.handle = pMemoryHandle;
        this._Level = infoLevel;
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      [return: MarshalAs(UnmanagedType.U1)]
      protected override bool ReleaseHandle()
      {
        if (!this.IsInvalid && this._Size > 0)
        {
          Marshal.FreeHGlobal(this.handle);
          this.SetHandle(IntPtr.Zero);
        }
        return true;
      }

      [SuppressUnmanagedCodeSecurity]
      protected void AllocMem()
      {
        if (this._Size >= this._BytesNeeded)
          return;
        if (this._Size != 0)
          Marshal.FreeHGlobal(this.handle);
        if (this._BytesNeeded != 0)
          this.handle = Marshal.AllocHGlobal(this._BytesNeeded);
        this._Size = this._BytesNeeded;
      }

      protected IntPtr PrinterHandle => this._PrinterSafeHandle.DangerousGetHandle();

      [SuppressUnmanagedCodeSecurity]
      protected short GetShortField(int offset)
      {
        return !this.IsInvalid ? Marshal.ReadInt16(this.handle, offset) : (short) 0;
      }

      [SuppressUnmanagedCodeSecurity]
      internal int GetIntField(int offset)
      {
        return !this.IsInvalid ? Marshal.ReadInt32(this.handle, this._IntPtrSize * offset) : 0;
      }

      [SuppressUnmanagedCodeSecurity]
      protected long GetLongField(int offset)
      {
        return !this.IsInvalid ? Marshal.ReadInt64(this.handle, this._IntPtrSize * offset) : 0L;
      }

      [CanBeNull]
      [SuppressUnmanagedCodeSecurity]
      protected internal string GetStringField(int offset)
      {
        if (this.IsInvalid)
          return (string) null;
        lock (this)
          return Marshal.PtrToStringUni(Marshal.ReadIntPtr(this.handle, this._IntPtrSize * offset)) ?? string.Empty;
      }

      [SuppressUnmanagedCodeSecurity]
      protected internal IntPtr GetIntPtrField(int offset)
      {
        return !this.IsInvalid ? Marshal.ReadIntPtr(this.handle, this._IntPtrSize * offset) : IntPtr.Zero;
      }

      [NotNull]
      protected byte[] GetBytes()
      {
        if (this.IsInvalid)
          throw new InvalidOperationException();
        byte[] destination = new byte[this._Size];
        Marshal.Copy(this.handle, destination, 0, this._Size);
        return destination;
      }
    }
}
