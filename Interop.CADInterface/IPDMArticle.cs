// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.IPDMArticle
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("32A98932-5817-4A23-B63D-7A9BFD3FF849")]
[ComImport]
public interface IPDMArticle : IParametersContainer
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  new string[] GetParameterNames([In] bool bConvertedNames);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  new void GetParameters(
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] pParameterNames,
    [In] bool bConvertedNames,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] out object[] ppValues,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I2)] out short[] ppIsReadOnly);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  new void SetParameters([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] pParameterNames, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT), In] object[] pValues, [In] bool bConvertedNames);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  new void DeleteParameters([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] pParameterNames, [In] bool bConvertedNames);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  IPDMDocument GetPDMDocument();

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void EditParameters();

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void ShowConditionsDialog([MarshalAs(UnmanagedType.Interface), In] IPDMArticle pPartArticle, [MarshalAs(UnmanagedType.BStr), In, Out] ref string pbstrConditionGUID);

  [DispId(8)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void ShowSelectOptionsDialog([MarshalAs(UnmanagedType.BStr), In, Out] ref string pbstrConfigString);

  [DispId(9)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool ComputeConditions(
    [MarshalAs(UnmanagedType.BStr), In] string bstrConfigString,
    [MarshalAs(UnmanagedType.BStr), In] string bstrConditionsGUID,
    [MarshalAs(UnmanagedType.BStr)] out string pbstrConfigStringInUse);

  [DispId(10)]
  string FullPath { [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }
}
