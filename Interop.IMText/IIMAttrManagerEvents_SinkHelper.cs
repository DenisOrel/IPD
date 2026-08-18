// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMAttrManagerEvents_SinkHelper
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public sealed class IIMAttrManagerEvents_SinkHelper : IIMAttrManagerEvents
{
  public IIMAttrManagerEvents_OnAttributeAddedEventHandler m_OnAttributeAddedDelegate;
  public IIMAttrManagerEvents_OnAttributeDeletedEventHandler m_OnAttributeDeletedDelegate;
  public IIMAttrManagerEvents_OnAttributeChangedEventHandler m_OnAttributeChangedDelegate;
  public int m_dwCookie;

  public override void OnAttributeAdded([In] IMFaceAttr_COM obj0)
  {
    if (this.m_OnAttributeAddedDelegate == null)
      return;
    this.m_OnAttributeAddedDelegate(obj0);
  }

  public override void OnAttributeDeleted([In] IMFaceAttr_COM obj0)
  {
    if (this.m_OnAttributeDeletedDelegate == null)
      return;
    this.m_OnAttributeDeletedDelegate(obj0);
  }

  public override void OnAttributeChanged([In] IMFaceAttr_COM obj0)
  {
    if (this.m_OnAttributeChangedDelegate == null)
      return;
    this.m_OnAttributeChangedDelegate(obj0);
  }

  internal IIMAttrManagerEvents_SinkHelper()
  {
    this.m_dwCookie = 0;
    this.m_OnAttributeAddedDelegate = (IIMAttrManagerEvents_OnAttributeAddedEventHandler) null;
    this.m_OnAttributeDeletedDelegate = (IIMAttrManagerEvents_OnAttributeDeletedEventHandler) null;
    this.m_OnAttributeChangedDelegate = (IIMAttrManagerEvents_OnAttributeChangedEventHandler) null;
  }
}
