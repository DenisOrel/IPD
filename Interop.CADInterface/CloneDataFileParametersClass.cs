// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.CloneDataFileParametersClass
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[ClassInterface(ClassInterfaceType.None)]
[Guid("0B438E7E-4B31-4ABB-8FE4-7C419B630EAD")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ComImport]
public class CloneDataFileParametersClass : ICloneDataFileParameters, CloneDataFileParameters
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern CloneDataFileParametersClass();

  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  public virtual extern string[] GetParameterNames([In] bool bConvertedNames);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void GetParameters(
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] pParameterNames,
    [In] bool bConvertedNames,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] out object[] ppValues,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I2)] out short[] ppIsReadOnly);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SetParameters(
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] pParameterNames,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT), In] object[] pValues,
    [In] bool bConvertedNames);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void DeleteParameters([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] pParameterNames, [In] bool bConvertedNames);

  [DispId(5)]
  public virtual extern bool ConvertedNames { [DispId(5), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SetDestination([MarshalAs(UnmanagedType.BStr), In] string bstrMasterPath, [MarshalAs(UnmanagedType.BStr), In] string bstrConfigurationName);

  [DispId(7)]
  public virtual extern bool IsDocumentParameters { [DispId(7), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(8)]
  public virtual extern string MasterPath { [DispId(8), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

  [DispId(9)]
  public virtual extern string ConfigurationName { [DispId(9), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }
}
