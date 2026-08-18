// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ComHelper
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

#nullable disable
namespace Intermech.Extensions;

public class ComHelper
{
  [NotNull]
  [CanBeEmpty]
  public static string GetTypeName([NotNull] object comObj)
  {
    if (!Marshal.IsComObject(comObj) || !(comObj is ComHelper.IDispatch dispatch))
      return string.Empty;
    ITypeInfo typeInfo = (ITypeInfo) null;
    try
    {
      try
      {
        if (dispatch.GetTypeInfo(0, 0, out typeInfo) == 0)
        {
          if (typeInfo != null)
            goto label_7;
        }
        return string.Empty;
      }
      catch
      {
        return string.Empty;
      }
label_7:
      string strName;
      try
      {
        typeInfo.GetDocumentation(-1, out strName, out string _, out int _, out string _);
      }
      catch
      {
        return string.Empty;
      }
      return strName;
    }
    catch
    {
      return string.Empty;
    }
    finally
    {
      if (typeInfo != null)
        Marshal.ReleaseComObject((object) typeInfo);
    }
  }

  [Guid("00020400-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  private interface IDispatch
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfoCount(out int Count);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfo([MarshalAs(UnmanagedType.U4)] int iTInfo, [MarshalAs(UnmanagedType.U4)] int lcid, [CanBeNull] out ITypeInfo typeInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetIDsOfNames(ref Guid riid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgsNames, int cNames, int lcid, [MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Invoke(
      int dispIdMember,
      ref Guid riid,
      uint lcid,
      ushort wFlags,
      ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
      out object pVarResult,
      ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo,
      IntPtr[] pArgErr);
  }
}
