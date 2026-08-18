// Decompiled with JetBrains decompiler
// Type: Intermech.Windows.Forms.ControlDesignTimeSerializationWrapper
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Windows.Forms;

[DesignTimeVisible(false)]
[DesignerSerializer(typeof (CodeDomSerializer), typeof (CodeDomSerializer))]
[CLSCompliant(false)]
[Serializable]
public class ControlDesignTimeSerializationWrapper : Component
{
  protected Control _Control;
  private readonly string _defaultAccessibleDescription;
  private readonly string _defaultAccessibleName;
  private readonly AccessibleRole _defaultAccessibleRole;
  private readonly bool _defaultAllowDrop;
  [RefreshProperties(RefreshProperties.Repaint)]
  private readonly AnchorStyles _defaultAnchor;
  private readonly Color _defaultBackColor;
  private readonly Image _defaultBackgroundImage;
  private readonly ImageLayout _defaultBackgroundImageLayout;
  private readonly bool _defaultCausesValidation;
  private readonly ContextMenuStrip _defaultContextMenuStrip;
  private readonly Cursor _defaultCursor;
  [RefreshProperties(RefreshProperties.Repaint)]
  private readonly DockStyle _defaultDock;
  private readonly bool _defaultEnabled;
  private readonly Font _defaultFont;
  private readonly Color _defaultForeColor;
  private readonly ImeMode _defaultImeMode;
  private readonly Point _defaultLocation;
  private readonly Padding _defaultMargin;
  private readonly Size _defaultMaximumSize;
  private readonly Size _defaultMinimumSize;
  private readonly Padding _defaultPadding;
  private readonly RightToLeft _defaultRightToLeft;
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private readonly Size _defaultSize;
  [MergableProperty(false)]
  private readonly int _defaultTabIndex;
  private readonly bool _defaultTabStop;
  [Bindable(true)]
  [TypeConverter(typeof (StringConverter))]
  private readonly string _defaultTag;
  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  private readonly bool _defaultUseWaitCursor;
  [RefreshProperties(RefreshProperties.Repaint)]
  private readonly bool _defaultVisible;

  public ControlDesignTimeSerializationWrapper([NotNull] Control control)
  {
    this._Control = control;
    this._defaultAccessibleDescription = control.AccessibleDescription;
    this._defaultAccessibleName = control.AccessibleName;
    this._defaultAccessibleRole = control.AccessibleRole;
    this._defaultAllowDrop = control.AllowDrop;
    this._defaultAnchor = control.Anchor;
    this._defaultBackColor = control.BackColor;
    this._defaultBackgroundImage = (Image) control.BackgroundImage?.Clone();
    this._defaultBackgroundImageLayout = control.BackgroundImageLayout;
    this._defaultCausesValidation = control.CausesValidation;
    this._defaultContextMenuStrip = control.ContextMenuStrip;
    this._defaultCursor = control.Cursor;
    this._defaultDock = control.Dock;
    this._defaultEnabled = control.Enabled;
    this._defaultFont = (Font) control.Font.Clone();
    this._defaultForeColor = control.ForeColor;
    this._defaultImeMode = control.ImeMode;
    this._defaultLocation = control.Location;
    this._defaultMargin = control.Margin;
    this._defaultMaximumSize = control.MaximumSize;
    this._defaultMinimumSize = control.MinimumSize;
    this._defaultPadding = control.Padding;
    this._defaultRightToLeft = control.RightToLeft;
    this._defaultSize = control.Size;
    this._defaultTabIndex = control.TabIndex;
    this._defaultTabStop = control.TabStop;
    this._defaultTag = control.Tag?.ToString() ?? string.Empty;
    this._defaultUseWaitCursor = control.UseWaitCursor;
    this._defaultVisible = control.Visible;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this._Control = (Control) null;
    base.Dispose(disposing);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Control Control
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Control;
  }

  public string AccessibleDescription
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.AccessibleDescription;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.AccessibleDescription = value;
    }
  }

  public bool ShouldSerializeAccessibleDescription()
  {
    if (this._Control.AccessibleDescription == null && this._defaultAccessibleDescription != null)
      return true;
    return this._Control.AccessibleDescription != null && !this._Control.AccessibleDescription.Equals(this._defaultAccessibleDescription);
  }

  public string AccessibleName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.AccessibleName;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.AccessibleName = value;
    }
  }

  public bool ShouldSerializeAccessibleName()
  {
    if (this._Control.AccessibleName == null && this._defaultAccessibleName != null)
      return true;
    return this._Control.AccessibleName != null && !this._Control.AccessibleName.Equals(this._defaultAccessibleName);
  }

  public AccessibleRole AccessibleRole
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.AccessibleRole;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.AccessibleRole = value;
    }
  }

  public bool ShouldSerializeAccessibleRole()
  {
    return !this._Control.AccessibleRole.Equals((object) this._defaultAccessibleRole);
  }

  public bool AllowDrop
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.AllowDrop;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.AllowDrop = value;
    }
  }

  public bool ShouldSerializeAllowDrop() => !this._Control.AllowDrop.Equals(this._defaultAllowDrop);

  public AnchorStyles Anchor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Anchor;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Anchor = value;
    }
  }

  public bool ShouldSerializeAnchor() => !this._Control.Anchor.Equals((object) this._defaultAnchor);

  public Color BackColor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.BackColor;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.BackColor = value;
    }
  }

  public bool ShouldSerializeBackColor()
  {
    return !this._Control.BackColor.Equals((object) this._defaultBackColor);
  }

  public Image BackgroundImage
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.BackgroundImage;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.BackgroundImage = value;
    }
  }

  public bool ShouldSerializeBackgroundImage()
  {
    if (this._Control.BackgroundImage == null && this._defaultBackgroundImage != null)
      return true;
    return this._Control.BackgroundImage != null && !this._Control.BackgroundImage.Equals((object) this._defaultBackgroundImage);
  }

  public ImageLayout BackgroundImageLayout
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.BackgroundImageLayout;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.BackgroundImageLayout = value;
    }
  }

  public bool ShouldSerializeBackgroundImageLayout()
  {
    return !this._Control.BackgroundImageLayout.Equals((object) this._defaultBackgroundImageLayout);
  }

  public bool CausesValidation
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.CausesValidation;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.CausesValidation = value;
    }
  }

  public bool ShouldSerializeCausesValidation()
  {
    return !this._Control.CausesValidation.Equals(this._defaultCausesValidation);
  }

  public ContextMenuStrip ContextMenuStrip
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.ContextMenuStrip;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.ContextMenuStrip = value;
    }
  }

  public bool ShouldSerializeContextMenuStrip()
  {
    return this._Control.ContextMenuStrip != this._defaultContextMenuStrip;
  }

  public Cursor Cursor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Cursor;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Cursor = value;
    }
  }

  public bool ShouldSerializeCursor() => this._Control.Cursor != this._defaultCursor;

  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [ParenthesizePropertyName(true)]
  [RefreshProperties(RefreshProperties.All)]
  public ControlBindingsCollection DataBindings
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.DataBindings;
    }
  }

  public DockStyle Dock
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Dock;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Dock = value;
    }
  }

  public bool ShouldSerializeDock() => !this._Control.Dock.Equals((object) this._defaultDock);

  public bool Enabled
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Enabled;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Enabled = value;
    }
  }

  public bool ShouldSerializeEnabled() => !this._Control.Enabled.Equals(this._defaultEnabled);

  [NotNull]
  public Font Font
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Font;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Font = value;
    }
  }

  public bool ShouldSerializeFont() => !this._Control.Font.Equals((object) this._defaultFont);

  public void ResetFont() => this._Control.Font = (Font) this._defaultFont.Clone();

  public Color ForeColor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.ForeColor;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.ForeColor = value;
    }
  }

  public bool ShouldSerializeForeColor()
  {
    return !this._Control.ForeColor.Equals((object) this._defaultForeColor);
  }

  public ImeMode ImeMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.ImeMode;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.ImeMode = value;
    }
  }

  public bool ShouldSerializeImeMode()
  {
    return !this._Control.ImeMode.Equals((object) this._defaultImeMode);
  }

  public Point Location
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Location;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Location = value;
    }
  }

  public bool ShouldSerializeLocation()
  {
    return this._Control.Dock == DockStyle.None && !this._Control.Location.Equals((object) this._defaultLocation);
  }

  public Padding Margin
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Margin;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Margin = value;
    }
  }

  public bool ShouldSerializeMargin() => !this._Control.Margin.Equals((object) this._defaultMargin);

  public Size MaximumSize
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.MaximumSize;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.MaximumSize = value;
    }
  }

  public bool ShouldSerializeMaximumSize()
  {
    return !this._Control.MaximumSize.Equals((object) this._defaultMaximumSize);
  }

  public Size MinimumSize
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.MinimumSize;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.MinimumSize = value;
    }
  }

  public bool ShouldSerializeMinimumSize()
  {
    return !this._Control.MinimumSize.Equals((object) this._defaultMinimumSize);
  }

  public Padding Padding
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Padding;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Padding = value;
    }
  }

  public bool ShouldSerializePadding()
  {
    return !this._Control.Padding.Equals((object) this._defaultPadding);
  }

  public RightToLeft RightToLeft
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.RightToLeft;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.RightToLeft = value;
    }
  }

  public bool ShouldSerializeRightToLeft()
  {
    return !this._Control.RightToLeft.Equals((object) this._defaultRightToLeft);
  }

  public Size Size
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Size;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Size = value;
    }
  }

  public bool ShouldSerializeSize()
  {
    switch (this._Control.Dock)
    {
      case DockStyle.Top:
      case DockStyle.Bottom:
        return this._Control.Size.Height != this._defaultSize.Height;
      case DockStyle.Left:
      case DockStyle.Right:
        return this._Control.Size.Width != this._defaultSize.Width;
      case DockStyle.Fill:
        return false;
      default:
        return !this._Control.Size.Equals((object) this._defaultSize);
    }
  }

  public int TabIndex
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.TabIndex;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.TabIndex = value;
    }
  }

  public bool ShouldSerializeTabIndex() => !this._Control.TabIndex.Equals(this._defaultTabIndex);

  public bool TabStop
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.TabStop;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.TabStop = value;
    }
  }

  public bool ShouldSerializeTabStop() => !this._Control.TabStop.Equals(this._defaultTabStop);

  public object Tag
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Tag;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Tag = value;
    }
  }

  public bool ShouldSerializeTag()
  {
    if (this._Control.Tag == null && this._defaultTag != string.Empty)
      return true;
    return this._Control.Tag != null && !this._Control.Tag.ToString().Equals(this._defaultTag);
  }

  public bool UseWaitCursor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.UseWaitCursor;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.UseWaitCursor = value;
    }
  }

  public bool ShouldSerializeUseWaitCursor()
  {
    return !this._Control.UseWaitCursor.Equals(this._defaultUseWaitCursor);
  }

  public bool Visible
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._Control.Visible;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Control.Visible = value;
    }
  }

  public bool ShouldSerializeVisible() => !this._Control.Visible.Equals(this._defaultVisible);

  public event EventHandler BackColorChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.BackColorChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.BackColorChanged -= value;
    }
  }

  public event EventHandler BackgroundImageChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.BackgroundImageChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.BackgroundImageChanged -= value;
    }
  }

  public event EventHandler BackgroundImageLayoutChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.BackgroundImageLayoutChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.BackgroundImageLayoutChanged -= value;
    }
  }

  public event EventHandler BindingContextChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.BindingContextChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.BindingContextChanged -= value;
    }
  }

  public event EventHandler CausesValidationChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.CausesValidationChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.CausesValidationChanged -= value;
    }
  }

  public event UICuesEventHandler ChangeUICues
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ChangeUICues += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ChangeUICues -= value;
    }
  }

  public event EventHandler Click
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Click += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Click -= value;
    }
  }

  public event EventHandler ClientSizeChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ClientSizeChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ClientSizeChanged -= value;
    }
  }

  public event EventHandler ContextMenuStripChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ContextMenuStripChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ContextMenuStripChanged -= value;
    }
  }

  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public event ControlEventHandler ControlAdded
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ControlAdded += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ControlAdded -= value;
    }
  }

  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public event ControlEventHandler ControlRemoved
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ControlRemoved += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ControlRemoved -= value;
    }
  }

  public event EventHandler CursorChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.CursorChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.CursorChanged -= value;
    }
  }

  public event EventHandler DockChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.DockChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.DockChanged -= value;
    }
  }

  public event EventHandler DoubleClick
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.DoubleClick += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.DoubleClick -= value;
    }
  }

  public event DragEventHandler DragDrop
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.DragDrop += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.DragDrop -= value;
    }
  }

  public event DragEventHandler DragEnter
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.DragEnter += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.DragEnter -= value;
    }
  }

  public event EventHandler DragLeave
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.DragLeave += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.DragLeave -= value;
    }
  }

  public event DragEventHandler DragOver
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.DragOver += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.DragOver -= value;
    }
  }

  public event EventHandler EnabledChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.EnabledChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.EnabledChanged -= value;
    }
  }

  public event EventHandler Enter
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Enter += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Enter -= value;
    }
  }

  public event EventHandler FontChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.FontChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.FontChanged -= value;
    }
  }

  public event EventHandler ForeColorChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ForeColorChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ForeColorChanged -= value;
    }
  }

  public event GiveFeedbackEventHandler GiveFeedback
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.GiveFeedback += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.GiveFeedback -= value;
    }
  }

  public event HelpEventHandler HelpRequested
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.HelpRequested += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.HelpRequested -= value;
    }
  }

  public event EventHandler ImeModeChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ImeModeChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ImeModeChanged -= value;
    }
  }

  public event KeyEventHandler KeyDown
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.KeyDown += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.KeyDown -= value;
    }
  }

  public event KeyPressEventHandler KeyPress
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.KeyPress += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.KeyPress -= value;
    }
  }

  public event KeyEventHandler KeyUp
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.KeyUp += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.KeyUp -= value;
    }
  }

  public event LayoutEventHandler Layout
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Layout += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Layout -= value;
    }
  }

  public event EventHandler Leave
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Leave += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Leave -= value;
    }
  }

  public event EventHandler LocationChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.LocationChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.LocationChanged -= value;
    }
  }

  public event EventHandler MarginChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MarginChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MarginChanged -= value;
    }
  }

  public event EventHandler MouseCaptureChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseCaptureChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseCaptureChanged -= value;
    }
  }

  public event MouseEventHandler MouseClick
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseClick += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseClick -= value;
    }
  }

  public event MouseEventHandler MouseDoubleClick
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseDoubleClick += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseDoubleClick -= value;
    }
  }

  public event MouseEventHandler MouseDown
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseDown += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseDown -= value;
    }
  }

  public event EventHandler MouseEnter
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseEnter += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseEnter -= value;
    }
  }

  public event EventHandler MouseHover
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseHover += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseHover -= value;
    }
  }

  public event EventHandler MouseLeave
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseLeave += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseLeave -= value;
    }
  }

  public event MouseEventHandler MouseMove
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseMove += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseMove -= value;
    }
  }

  public event MouseEventHandler MouseUp
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.MouseUp += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.MouseUp -= value;
    }
  }

  public event EventHandler Move
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Move += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Move -= value;
    }
  }

  public event EventHandler PaddingChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.PaddingChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.PaddingChanged -= value;
    }
  }

  public event PaintEventHandler Paint
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Paint += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Paint -= value;
    }
  }

  public event EventHandler ParentChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.ParentChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.ParentChanged -= value;
    }
  }

  public event PreviewKeyDownEventHandler PreviewKeyDown
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.PreviewKeyDown += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.PreviewKeyDown -= value;
    }
  }

  public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.QueryAccessibilityHelp += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.QueryAccessibilityHelp -= value;
    }
  }

  public event QueryContinueDragEventHandler QueryContinueDrag
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.QueryContinueDrag += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.QueryContinueDrag -= value;
    }
  }

  public event EventHandler RegionChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.RegionChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.RegionChanged -= value;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public event EventHandler Resize
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Resize += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Resize -= value;
    }
  }

  public event EventHandler RightToLeftChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.RightToLeftChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.RightToLeftChanged -= value;
    }
  }

  public event EventHandler SizeChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.SizeChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.SizeChanged -= value;
    }
  }

  public event EventHandler StyleChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.StyleChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.StyleChanged -= value;
    }
  }

  public event EventHandler SystemColorsChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.SystemColorsChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.SystemColorsChanged -= value;
    }
  }

  public event EventHandler TabIndexChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.TabIndexChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.TabIndexChanged -= value;
    }
  }

  public event EventHandler TabStopChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.TabStopChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.TabStopChanged -= value;
    }
  }

  public event EventHandler Validated
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Validated += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Validated -= value;
    }
  }

  public event CancelEventHandler Validating
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.Validating += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.Validating -= value;
    }
  }

  public event EventHandler VisibleChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._Control.VisibleChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._Control.VisibleChanged -= value;
    }
  }
}
