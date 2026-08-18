// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMAttrManagerEvents_Event
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComEventInterface(typeof (IIMAttrManagerEvents), typeof (IIMAttrManagerEvents_EventProvider))]
public interface IIMAttrManagerEvents_Event
{
  event IIMAttrManagerEvents_OnAttributeAddedEventHandler OnAttributeAdded;

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void add_OnAttributeAdded(
    [In] IIMAttrManagerEvents_OnAttributeAddedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void remove_OnAttributeAdded(
    [In] IIMAttrManagerEvents_OnAttributeAddedEventHandler obj0);

  event IIMAttrManagerEvents_OnAttributeDeletedEventHandler OnAttributeDeleted;

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void add_OnAttributeDeleted(
    [In] IIMAttrManagerEvents_OnAttributeDeletedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void remove_OnAttributeDeleted(
    [In] IIMAttrManagerEvents_OnAttributeDeletedEventHandler obj0);

  event IIMAttrManagerEvents_OnAttributeChangedEventHandler OnAttributeChanged;

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void add_OnAttributeChanged(
    [In] IIMAttrManagerEvents_OnAttributeChangedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void remove_OnAttributeChanged(
    [In] IIMAttrManagerEvents_OnAttributeChangedEventHandler obj0);
}
