
// Type: AxInventorViewControlLib.AxInventorViewControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using InventorApprentice;
using InventorViewControlLib;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace AxInventorViewControlLib;

[AxHost.Clsid("{a6336ab8-d3e1-489a-8186-ee40f2e027fe}")]
[DesignTimeVisible(true)]
public class AxInventorViewControl : AxHost
{
  private _DInventorViewControl ocx;
  private AxInventorViewControlEventMulticaster eventMulticaster;
  private AxHost.ConnectionPointCookie cookie;

  public AxInventorViewControl()
    : base("a6336ab8-d3e1-489a-8186-ee40f2e027fe")
  {
    this.SetAboutBoxDelegate(new AxHost.AboutBoxDelegate(this.AboutBox));
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(1)]
  public virtual string FileName
  {
    get
    {
      return this.ocx != null ? this.ocx.FileName : throw new AxHost.InvalidActiveXStateException(nameof (FileName), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (FileName), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.FileName = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(3)]
  public virtual int SheetIndex
  {
    get
    {
      return this.ocx != null ? this.ocx.SheetIndex : throw new AxHost.InvalidActiveXStateException(nameof (SheetIndex), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (SheetIndex), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.SheetIndex = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(4)]
  public virtual DisplayModeEnum DisplayMode
  {
    get
    {
      return this.ocx != null ? (DisplayModeEnum) this.ocx.DisplayMode : throw new AxHost.InvalidActiveXStateException(nameof (DisplayMode), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (DisplayMode), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.DisplayMode = (DisplayModeEnum) value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(5)]
  public virtual bool Perspective
  {
    get
    {
      return this.ocx != null ? this.ocx.Perspective : throw new AxHost.InvalidActiveXStateException(nameof (Perspective), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (Perspective), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.Perspective = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(6)]
  public virtual ViewOrientationTypeEnum ViewOrientationType
  {
    get
    {
      return this.ocx != null ? (ViewOrientationTypeEnum) this.ocx.ViewOrientationType : throw new AxHost.InvalidActiveXStateException(nameof (ViewOrientationType), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (ViewOrientationType), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.ViewOrientationType = (ViewOrientationTypeEnum) value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(7)]
  public virtual bool Interactive
  {
    get
    {
      return this.ocx != null ? this.ocx.Interactive : throw new AxHost.InvalidActiveXStateException(nameof (Interactive), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (Interactive), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.Interactive = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(8)]
  public virtual ViewingCommandEnum ActiveViewingCommand
  {
    get
    {
      return this.ocx != null ? this.ocx.ActiveViewingCommand : throw new AxHost.InvalidActiveXStateException(nameof (ActiveViewingCommand), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (ActiveViewingCommand), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.ActiveViewingCommand = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(9)]
  public virtual bool HideToolbar
  {
    get
    {
      return this.ocx != null ? this.ocx.HideToolbar : throw new AxHost.InvalidActiveXStateException(nameof (HideToolbar), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (HideToolbar), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.HideToolbar = value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(2)]
  public virtual ApprenticeServerDocument ApprenticeServerDocument
  {
    get
    {
      return this.ocx != null ? (ApprenticeServerDocument) this.ocx.ApprenticeServerDocument : throw new AxHost.InvalidActiveXStateException(nameof (ApprenticeServerDocument), AxHost.ActiveXInvokeKind.PropertyGet);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(10)]
  public virtual ClientView ClientView
  {
    get
    {
      return this.ocx != null ? (ClientView) this.ocx.ClientView : throw new AxHost.InvalidActiveXStateException(nameof (ClientView), AxHost.ActiveXInvokeKind.PropertyGet);
    }
  }

  public virtual void AboutBox()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (AboutBox), AxHost.ActiveXInvokeKind.MethodInvoke);
    this.ocx.AboutBox();
  }

  protected override void CreateSink()
  {
    try
    {
      this.eventMulticaster = new AxInventorViewControlEventMulticaster(this);
      this.cookie = new AxHost.ConnectionPointCookie((object) this.ocx, (object) this.eventMulticaster, typeof (_DInventorViewControlEvents));
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
      this.ocx = (_DInventorViewControl) this.GetOcx();
    }
    catch (Exception ex)
    {
    }
  }
}
