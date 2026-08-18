// Decompiled with JetBrains decompiler
// Type: Intermech.Windows.Forms.UserControlDesignTimeSerializationWrapper
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Windows.Forms;

[DesignTimeVisible(false)]
[CLSCompliant(false)]
[Serializable]
public class UserControlDesignTimeSerializationWrapper : ControlDesignTimeSerializationWrapper
{
  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  private readonly bool _defaultAutoSize;
  [Browsable(true)]
  private readonly AutoSizeMode _defaultAutoSizeMode;
  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  private readonly AutoValidate _defaultAutoValidate;
  [Browsable(true)]
  [Category("Alignment")]
  [Description("Specifies the alignment of text.")]
  private readonly BorderStyle _defaultBorderStyle;
  private readonly bool _defaultAutoScroll;
  private readonly Size _defaultAutoScrollMargin;
  private readonly Size _defaultAutoScrollMinSize;

  public UserControlDesignTimeSerializationWrapper([NotNull] UserControl userControl)
    : base((Control) userControl)
  {
    this._defaultBorderStyle = userControl.BorderStyle;
    this._defaultAutoSize = userControl.AutoSize;
    this._defaultAutoSizeMode = userControl.AutoSizeMode;
    this._defaultAutoValidate = userControl.AutoValidate;
    this._defaultAutoScroll = userControl.AutoScroll;
    this._defaultAutoScrollMargin = userControl.AutoScrollMargin;
    this._defaultAutoScrollMinSize = userControl.AutoScrollMinSize;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected UserControl _userControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (UserControl) this._Control;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public UserControl UserControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl;
    }
  }

  public bool AutoSize
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.AutoSize;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.AutoSize = value;
    }
  }

  public bool ShouldSerializeAutoSize()
  {
    return !this._userControl.AutoSize.Equals(this._defaultAutoSize);
  }

  public AutoSizeMode AutoSizeMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.AutoSizeMode;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.AutoSizeMode = value;
    }
  }

  public bool ShouldSerializeAutoSizeMode()
  {
    return !this._userControl.AutoSizeMode.Equals((object) this._defaultAutoSizeMode);
  }

  public AutoValidate AutoValidate
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.AutoValidate;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.AutoValidate = value;
    }
  }

  public bool ShouldSerializeAutoValidate()
  {
    return !this._userControl.AutoValidate.Equals((object) this._defaultAutoValidate);
  }

  public BorderStyle BorderStyle
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.BorderStyle;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.BorderStyle = value;
    }
  }

  public bool ShouldSerializeBorderStyle()
  {
    return !this._userControl.BorderStyle.Equals((object) this._defaultBorderStyle);
  }

  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  public event EventHandler AutoSizeChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._userControl.AutoSizeChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._userControl.AutoSizeChanged -= value;
    }
  }

  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  public event EventHandler AutoValidateChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._userControl.AutoValidateChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._userControl.AutoValidateChanged -= value;
    }
  }

  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  public event EventHandler Load
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._userControl.Load += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._userControl.Load -= value;
    }
  }

  public bool AutoScroll
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.AutoScroll;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.AutoScroll = value;
    }
  }

  public bool ShouldSerializeAutoScroll()
  {
    return !this._userControl.AutoScroll.Equals(this._defaultAutoScroll);
  }

  public Size AutoScrollMargin
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.AutoScrollMargin;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.AutoScrollMargin = value;
    }
  }

  public bool ShouldSerializeAutoScrollMargin()
  {
    return !this._userControl.AutoScrollMargin.Equals((object) this._defaultAutoScrollMargin);
  }

  public Size AutoScrollMinSize
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userControl.AutoScrollMinSize;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._userControl.AutoScrollMinSize = value;
    }
  }

  public bool ShouldSerializeAutoScrollMinSize()
  {
    return !this._userControl.AutoScrollMinSize.Equals((object) this._defaultAutoScrollMinSize);
  }

  public event ScrollEventHandler Scroll
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._userControl.Scroll += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._userControl.Scroll -= value;
    }
  }
}
