
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.DispatchUtility
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

internal class DispatchUtility
{
  private const int S_OK = 0;
  private const int LOCALE_SYSTEM_DEFAULT = 2048 /*0x0800*/;

  public static bool ImplementsIDispatch(object obj) => obj is DispatchUtility.IDispatchInfo;

  public static Type GetType(object obj, bool throwIfNotFound)
  {
    DispatchUtility.RequireReference<object>(obj, nameof (obj));
    return DispatchUtility.GetType((DispatchUtility.IDispatchInfo) obj, throwIfNotFound);
  }

  public static bool TryGetDispId(object obj, string name, out int dispId)
  {
    DispatchUtility.RequireReference<object>(obj, nameof (obj));
    return DispatchUtility.TryGetDispId((DispatchUtility.IDispatchInfo) obj, name, out dispId);
  }

  public static object Invoke(object obj, int dispId, object[] args)
  {
    string memberName = $"[DispId={(object) dispId}]";
    return DispatchUtility.Invoke(obj, memberName, args);
  }

  public static object Invoke(object obj, string memberName, object[] args)
  {
    DispatchUtility.RequireReference<object>(obj, nameof (obj));
    return obj.GetType().InvokeMember(memberName, BindingFlags.InvokeMethod | BindingFlags.GetProperty, (Binder) null, obj, args, (CultureInfo) null);
  }

  private static void RequireReference<T>(T value, string name) where T : class
  {
    if ((object) value == null)
      throw new ArgumentNullException(name);
  }

  private static Type GetType(DispatchUtility.IDispatchInfo dispatch, bool throwIfNotFound)
  {
    DispatchUtility.RequireReference<DispatchUtility.IDispatchInfo>(dispatch, nameof (dispatch));
    Type typeInfo = (Type) null;
    int typeInfoCount1;
    int typeInfoCount2 = dispatch.GetTypeInfoCount(out typeInfoCount1);
    if (typeInfoCount2 == 0 && typeInfoCount1 > 0)
      dispatch.GetTypeInfo(0, 2048 /*0x0800*/, out typeInfo);
    if (typeInfo == (Type) null & throwIfNotFound)
    {
      Marshal.ThrowExceptionForHR(typeInfoCount2);
      throw new TypeLoadException();
    }
    return typeInfo;
  }

  private static bool TryGetDispId(
    DispatchUtility.IDispatchInfo dispatch,
    string name,
    out int dispId)
  {
    DispatchUtility.RequireReference<DispatchUtility.IDispatchInfo>(dispatch, nameof (dispatch));
    DispatchUtility.RequireReference<string>(name, nameof (name));
    bool dispId1 = false;
    Guid empty = Guid.Empty;
    int dispId2 = dispatch.GetDispId(ref empty, ref name, 1, 2048 /*0x0800*/, out dispId);
    switch (dispId2)
    {
      case -2147352570 /*0x80020006*/:
        if (dispId == -1)
        {
          dispId1 = false;
          break;
        }
        goto default;
      case 0:
        dispId1 = true;
        break;
      default:
        Marshal.ThrowExceptionForHR(dispId2);
        break;
    }
    return dispId1;
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00020400-0000-0000-C000-000000000046")]
  [ComImport]
  private interface IDispatchInfo
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfoCount(out int typeInfoCount);

    void GetTypeInfo(int typeInfoIndex, int lcid, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (TypeToTypeInfoMarshaler))] out Type typeInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDispId(ref Guid riid, ref string name, int nameCount, int lcid, out int dispId);
  }
}
