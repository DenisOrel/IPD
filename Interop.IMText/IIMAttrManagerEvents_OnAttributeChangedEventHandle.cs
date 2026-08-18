// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMAttrManagerEvents_OnAttributeChangedEventHandler
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void IIMAttrManagerEvents_OnAttributeChangedEventHandler([MarshalAs(UnmanagedType.Interface), In] IMFaceAttr_COM pAttr);
