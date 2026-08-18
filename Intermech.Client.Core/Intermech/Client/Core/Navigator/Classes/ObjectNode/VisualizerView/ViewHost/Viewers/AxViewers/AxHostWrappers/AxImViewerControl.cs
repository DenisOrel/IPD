
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.AxImViewerControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Interop.IMViewer.Controls;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

/// <summary>AxHost для ImViewer</summary>
[AxHost.Clsid("{43C7E03D-DC17-40EA-A97A-E7C09696DEE3}")]
[DesignTimeVisible(true)]
public class AxImViewerControl : AxHost
{
  private IMViewerOCX ocx;
  private AxImViewerControlEventMulticaster eventMulticaster;
  private AxHost.ConnectionPointCookie cookie;

  public AxImViewerControl()
    : base("43C7E03D-DC17-40EA-A97A-E7C09696DEE3")
  {
    this.SetAboutBoxDelegate(new AxHost.AboutBoxDelegate(this.AboutBox));
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(1)]
  public virtual bool Open(string sFullPath)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.Open(sFullPath) : throw new AxHost.InvalidActiveXStateException(nameof (Open), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(2)]
  public virtual bool GetCadmechCOM(ref object ppCadmech)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetCadmechCOM(ref ppCadmech) : throw new AxHost.InvalidActiveXStateException(nameof (GetCadmechCOM), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(3)]
  public virtual bool GetIMViewerApp(ref object ppApp)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetIMViewerApp(ref ppApp) : throw new AxHost.InvalidActiveXStateException(nameof (GetIMViewerApp), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(4)]
  public virtual bool OpenConfig(string sFullPath, string sConfigName)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (OpenConfig), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    return this.ocx.OpenConfig(sFullPath, sConfigName);
  }

  public virtual void AboutBox()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (AboutBox), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.AboutBox();
  }

  protected override void CreateSink()
  {
    try
    {
      this.eventMulticaster = new AxImViewerControlEventMulticaster(this);
      this.cookie = new AxHost.ConnectionPointCookie((object) this.ocx, (object) this.eventMulticaster, typeof (_DIMViewerOCXEvents));
    }
    catch (Exception ex)
    {
    }
  }

  protected override void DetachSink()
  {
    try
    {
      this.cookie.Disconnect();
    }
    catch (Exception ex)
    {
    }
  }

  protected override void AttachInterfaces()
  {
    try
    {
      this.ocx = (IMViewerOCX) this.GetOcx();
    }
    catch (Exception ex)
    {
    }
  }
}
