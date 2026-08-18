
// Type: Intermech.DocumentView.IObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.DocumentView;

public abstract class IObject : IDisposable
{
  public static readonly RectangleF NullRect;
  private RectangleF myBounds = new RectangleF(0.0f, 0.0f, 10f, 10f);
  private RectangleF _oldBounds = RectangleF.Empty;
  private int myInternalFlags = 524671;
  private Layer _layer;
  private IView _view;

  public event EventHandler BoundsChanged;

  /// <summary>
  /// A static method for converting a PointF to a RectangleF, for calls to <see cref="M:Intermech.Map.MapObject.Changed(System.Int32,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// </summary>
  /// <param name="p"></param>
  public static RectangleF MakeRect(PointF p) => new RectangleF(p.X, p.Y, 0.0f, 0.0f);

  /// <summary>
  /// A static method for converting a SizeF to a RectangleF, for calls to <see cref="M:Intermech.Map.MapObject.Changed(System.Int32,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// </summary>
  /// <param name="s"></param>
  public static RectangleF MakeRect(SizeF s) => new RectangleF(0.0f, 0.0f, s.Width, s.Height);

  /// <summary>
  /// A static method for converting a float to a RectangleF, for calls to <see cref="M:Intermech.Map.MapObject.Changed(System.Int32,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// </summary>
  /// <param name="x"></param>
  public static RectangleF MakeRect(float x) => new RectangleF(x, 0.0f, 0.0f, 0.0f);

  internal bool OnDoubleClick(InputEventArgs evt, Intermech.DocumentView.View view)
  {
    throw new Exception(sc_2512.ssp_imclient_2513());
  }

  internal IObject Parent => (IObject) null;

  /// <summary>Calculate a new location for this object.</summary>
  /// <param name="origLoc"></param>
  /// <param name="newLoc"></param>
  /// <returns>
  /// A <c>PointF</c> in document coordinates.
  /// </returns>
  /// <remarks>
  /// This is normally called from <see cref="M:Intermech.Map.MapObject.DoMove(Intermech.Map.MapView,System.Drawing.PointF,System.Drawing.PointF)" />.
  /// </remarks>
  public virtual PointF ComputeMove(PointF origLoc, PointF newLoc) => newLoc;

  /// <summary>
  /// Determine if a given point is inside and on this object.
  /// </summary>
  /// <param name="p">
  /// A <c>PointF</c> in document coordinates.
  /// </param>
  /// <returns>
  /// True if the argument <paramref name="p" /> is considered to be "in"
  /// this object.
  /// </returns>
  /// <remarks>
  /// This method tries to return true for points near a stroke or near or
  /// inside a possibly filled object such as an ellipse or a polygon.
  /// This method ignores any drop shadow, but normally includes the
  /// width of any <c>Pen</c>.
  /// The default behavior of this method is to return true if the
  /// point <paramref name="p" /> is within this object's <see cref="P:Intermech.Map.MapObject.Bounds" />.
  /// </remarks>
  public virtual bool ContainsPoint(PointF p) => IObject.ContainsRect(this.Bounds, p);

  internal static bool ContainsRect(RectangleF a, PointF b)
  {
    return (double) a.X <= (double) b.X && (double) b.X <= (double) a.X + (double) a.Width && (double) a.Y <= (double) b.Y && (double) b.Y <= (double) a.Y + (double) a.Height;
  }

  internal static bool ContainsRect(RectangleF a, RectangleF b)
  {
    return (double) a.X <= (double) b.X && (double) b.X + (double) b.Width <= (double) a.X + (double) a.Width && (double) a.Y <= (double) b.Y && (double) b.Y + (double) b.Height <= (double) a.Y + (double) a.Height;
  }

  /// <summary>Gets or sets this object's natural position.</summary>
  /// <value>
  /// The <c>PointF</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// This property is normally the object's <see cref="F:Intermech.Map.MapObject.TopLeft" /> position.
  /// However, it is common for certain kinds of objects to assume that the
  /// assigned location actually refers to a different spot of the bounding
  /// rectangle.  For example, for <see cref="T:Intermech.Map.MapText" /> objects, the text's
  /// alignment property determines the <c>Location</c>.  For groups, one of
  /// the child objects might be the natural thing to be positioned as the
  /// user would see it.  For example, the icon of a node might provide the
  /// Location for the node as a whole.
  /// If you override this property, you should also override
  /// <see cref="M:Intermech.Map.MapObject.SetSizeKeepingLocation(System.Drawing.SizeF)" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapObject.SetSizeKeepingLocation(System.Drawing.SizeF)" />
  /// <seealso cref="P:Intermech.Map.MapObject.Bounds" />
  /// <seealso cref="M:Intermech.Map.MapObject.GetSpotLocation(System.Int32)" />
  /// <seealso cref="M:Intermech.Map.MapObject.SetSpotLocation(System.Int32,System.Drawing.PointF)" />
  [Category("Bounds")]
  [Description("The natural location for this object, perhaps different from Position.")]
  public virtual PointF Location
  {
    get => this.Position;
    set => this.Position = value;
  }

  /// <summary>
  /// Gets or sets this object's top-left corner's position.
  /// </summary>
  /// <value>
  /// The <c>PointF</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// This is just a convenience property that operates on this object's
  /// <see cref="P:Intermech.Map.MapObject.Bounds" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapObject.GetSpotLocation(System.Int32)" />
  /// <seealso cref="M:Intermech.Map.MapObject.SetSpotLocation(System.Int32,System.Drawing.PointF)" />
  /// <seealso cref="P:Intermech.Map.MapObject.Location" />
  [Category("Bounds")]
  [Browsable(false)]
  public PointF Position
  {
    get
    {
      RectangleF bounds = this.Bounds;
      return new PointF(bounds.X, bounds.Y);
    }
    set
    {
      this.Bounds = this.Bounds with
      {
        X = value.X,
        Y = value.Y
      };
    }
  }

  /// <summary>
  /// Gets the view that this object belongs to,
  /// or null if this is not in a layer or if this is in a document layer.
  /// </summary>
  /// <remarks>
  /// You cannot set this property--call <see cref="M:Intermech.Map.MapLayer.Add(Intermech.Map.MapObject)" /> instead.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapObject.IsInView" />
  [Category("Ownership")]
  [Description("The GoView to which this object belongs.")]
  public IView View
  {
    get => this._view;
    set => this._view = value;
  }

  internal static void InflateRect(ref RectangleF a, float w, float h)
  {
    a.X -= w;
    a.Width += w * 2f;
    a.Y -= h;
    a.Height += h * 2f;
  }

  internal static bool IntersectsRect(RectangleF a, RectangleF b)
  {
    float width1 = a.Width;
    float height1 = a.Height;
    float width2 = b.Width;
    float height2 = b.Height;
    if ((double) width2 >= 0.0 && (double) height2 >= 0.0 && (double) width1 >= 0.0 && (double) height1 >= 0.0)
    {
      float x1 = a.X;
      float y1 = a.Y;
      float x2 = b.X;
      float y2 = b.Y;
      float num1 = width2 + x2;
      float num2 = height2 + y2;
      float num3 = width1 + x1;
      float num4 = height1 + y1;
      if (((double) num1 <= (double) x2 || (double) num1 >= (double) x1) && ((double) num2 <= (double) y2 || (double) num2 >= (double) y1) && ((double) num3 <= (double) x1 || (double) num3 >= (double) x2))
        return (double) num4 <= (double) y1 || (double) num4 >= (double) y2;
    }
    return false;
  }

  /// <summary>Gets or sets whether the user can resize this object.</summary>
  /// <value>
  /// This defaults to true.  However, for some objects, such
  /// as <see cref="T:Intermech.Map.MapText" /> and <see cref="T:Intermech.Map.MapPort" />,
  /// this defaults to false.
  /// You should normally call the <see cref="M:Intermech.Map.MapObject.CanResize" /> method
  /// instead of getting this property.
  /// </value>
  /// <remarks>
  /// A false value prevents the user from resizing this object
  /// by the normal mechanisms.
  /// Even when this property value is true, this object might not be
  /// resizable by the user because the layer or document disallows it,
  /// or because the view disallows it.
  /// Your code can always resize objects programmatically by calling
  /// <c>obj.Size = newSize</c> or <c>obj.Bounds = newRect</c>.
  /// For an object to be resizable, its <see cref="P:Intermech.Map.MapObject.SelectionObject" />
  /// is really what should be resizable.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapObject.CanResize" />
  /// <seealso cref="M:Intermech.Map.MapLayer.CanResizeObjects" />
  /// <seealso cref="M:Intermech.Map.MapObject.DoResize(Intermech.Map.MapView,System.Drawing.RectangleF,System.Drawing.PointF,System.Int32,Intermech.Map.MapInputState,System.Drawing.SizeF,System.Drawing.SizeF)" />
  /// <seealso cref="P:Intermech.Map.MapObject.Reshapable" />
  [Category("Behavior")]
  [Description("Whether users can resize this object.")]
  [DefaultValue(true)]
  public virtual bool Resizable
  {
    get => (this.InternalFlags & 16 /*0x10*/) != 0;
    set
    {
      bool oldVal = (this.InternalFlags & 16 /*0x10*/) != 0;
      if (oldVal == value)
        return;
      if (value)
        this.InternalFlags |= 16 /*0x10*/;
      else
        this.InternalFlags &= -17;
      this.Changed(1007, 0, (object) oldVal, IObject.NullRect, 0, (object) value, IObject.NullRect);
    }
  }

  /// <summary>Gets or sets whether the user can select this object.</summary>
  /// <value>
  /// This defaults to true.  However, for some objects, such
  /// as <see cref="T:Intermech.Map.MapPort" />, this defaults to false.
  /// You should normally call the <see cref="M:Intermech.Map.MapObject.CanSelect" /> method
  /// instead of getting this property.
  /// </value>
  /// <remarks>
  /// A false value prevents the user from selecting this object
  /// by the normal mechanisms.
  /// Even when this property value is true, this object might not be
  /// selectable by the user because its layer or document disallows it,
  /// or because the view disallows it.
  /// Your code can always select objects programmatically by calling
  /// <c>aView.Selection.Select(obj)</c> or <c>aView.Selection.Add(obj)</c>.
  /// When this object's <c>CanSelect</c> is false, then if this object is
  /// part of a group, the normal selection mechanism will see if the
  /// group's <c>CanSelect</c> is true.  If so, the group will be selected.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapObject.CanSelect" />
  /// <seealso cref="M:Intermech.Map.MapLayer.CanSelectObjects" />
  [Description("Whether users can select this object.")]
  [Category("Behavior")]
  [DefaultValue(true)]
  public virtual bool Selectable
  {
    get => (this.InternalFlags & 2) != 0;
    set
    {
      bool oldVal = (this.InternalFlags & 2) != 0;
      if (oldVal == value)
        return;
      if (value)
        this.InternalFlags |= 2;
      else
        this.InternalFlags &= -3;
      this.Changed(1004, 0, (object) oldVal, IObject.NullRect, 0, (object) value, IObject.NullRect);
    }
  }

  public virtual void Changed(
    int subhint,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect)
  {
  }

  /// <summary>Gets or sets this object's height.</summary>
  /// <value>
  /// The <c>float</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// This is just a convenience property that operates on this object's
  /// <see cref="P:Intermech.Map.MapObject.Bounds" />.
  /// </remarks>
  [Category("Bounds")]
  [Description("The height of the Bounds.")]
  public float Height
  {
    get => this.Bounds.Height;
    set => this.Bounds = this.Bounds with { Height = value };
  }

  public virtual void OnGotSelection(ISelection sel)
  {
  }

  /// <summary>Gets the layer to which this object belongs.</summary>
  /// <remarks>
  /// If this object is not part of any layer, either directly
  /// as a top-level object, or as part of a group,
  /// then this property value will be null.
  /// You cannot set this property--call <see cref="M:Intermech.Map.MapLayer.Add(Intermech.Map.MapObject)" /> instead.
  /// <see cref="M:Intermech.Map.MapObject.CopyObject(Intermech.Map.MapCopyDictionary)" /> will not set this property directly, nor will
  /// it automatically add the copied object to some layer to set this property
  /// indirectly.
  /// The caller of <see cref="M:Intermech.Map.MapObject.CopyObject(Intermech.Map.MapCopyDictionary)" /> is responsible for deciding which
  /// <see cref="T:Intermech.Map.MapLayer" /> to add the newly copied object, if any.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapObject.IsInDocument" />
  /// <seealso cref="P:Intermech.Map.MapObject.Document" />
  /// <seealso cref="P:Intermech.Map.MapObject.IsInView" />
  /// <seealso cref="P:Intermech.Map.MapObject.View" />
  /// <seealso cref="M:Intermech.Map.MapObject.OnLayerChanged(Intermech.Map.MapLayer,Intermech.Map.MapLayer,Intermech.Map.MapObject)" />
  [Category("Ownership")]
  [Description("The GoLayer to which this object belongs.")]
  public Layer Layer => this._layer;

  internal bool OnSingleClick(InputEventArgs evt, Intermech.DocumentView.View view)
  {
    throw new Exception(sc_2512.ssp_imclient_2514());
  }

  internal bool OnMouseOver(InputEventArgs evt, Intermech.DocumentView.View view)
  {
    throw new Exception(sc_2512.ssp_imclient_2515());
  }

  internal bool OnHover(InputEventArgs evt, Intermech.DocumentView.View view)
  {
    throw new Exception(sc_2512.ssp_imclient_2516());
  }

  internal int InternalFlags
  {
    get => this.myInternalFlags;
    set => this.myInternalFlags = value;
  }

  private bool SkipsBoundsChanged
  {
    get => (this.InternalFlags & 16384 /*0x4000*/) != 0;
    set
    {
      if (value)
        this.InternalFlags |= 16384 /*0x4000*/;
      else
        this.InternalFlags &= -16385;
    }
  }

  /// <summary>Gets or sets whether the bounds are up to date.</summary>
  /// <remarks>
  /// This is typically set to true as some change is made to this
  /// object that requires recalculation of the bounds.
  /// This flag is automatically set to false and the
  /// <see cref="M:Intermech.Map.MapObject.ComputeBounds" /> method then actually does that
  /// calculation on demand.
  /// Setting this property does not raise any Changed events.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapObject.Bounds" />
  [Browsable(false)]
  protected bool InvalidBounds
  {
    get => (this.InternalFlags & 32768 /*0x8000*/) != 0;
    set
    {
      if (value)
        this.InternalFlags |= 32768 /*0x8000*/;
      else
        this.InternalFlags &= -32769;
    }
  }

  public RectangleF OldBounds => this._oldBounds;

  /// <summary>Called after this object's bounds has changed.</summary>
  /// <param name="old">
  /// A <c>RectangleF</c> in document coordinates holding the previous bounds.
  /// </param>
  /// <remarks>
  /// By default this method does nothing.
  /// This method is called as part of the <see cref="P:Intermech.Map.MapObject.Bounds" /> setter, after
  /// the property value has been saved.
  /// However, this method is not called when the bounds are changed due to a call
  /// to <see cref="M:Intermech.Map.MapObject.ComputeBounds" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapGroup.OnBoundsChanged(System.Drawing.RectangleF)" />
  /// <seealso cref="M:Intermech.Map.MapGroup.OnChildBoundsChanged(Intermech.Map.MapObject,System.Drawing.RectangleF)" />
  protected virtual void OnBoundsChanged(RectangleF old)
  {
    this._oldBounds = old;
    if (this.BoundsChanged == null)
      return;
    this.BoundsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>Called to see if the user can see this object.</summary>
  /// <remarks>
  /// This returns true if <c>Visible</c>, if its parent
  /// is visible, and if this object is
  /// part of a layer, if <c>Layer.CanViewObjects</c> is true.
  /// This predicate is used by methods such as <see cref="M:Intermech.Map.MapObject.Paint(System.Drawing.Graphics,Intermech.Map.MapView)" />.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapObject.Visible" />
  /// <seealso cref="M:Intermech.Map.MapLayer.CanViewObjects" />
  public virtual bool CanView() => true;

  /// <summary>
  /// Called to get a String to display as a tooltip for this object.
  /// </summary>
  /// <param name="view"></param>
  /// <returns>
  /// A <c>String</c>, or null to indicate no tooltip for this object.
  /// </returns>
  /// <remarks>
  /// By default this method does nothing but return null.
  /// A non-null <c>String</c> indicates this
  /// object handled the event and thus that the calling view
  /// need not continue calling the method up the chain of parents.
  /// <see cref="M:Intermech.Map.MapView.DoToolTipObject(Intermech.Map.MapObject)" /> is the normal caller.
  /// </remarks>
  public virtual string GetToolTip(IView view) => (string) null;

  /// <summary>Gets or sets the bounding rectangle for this object.</summary>
  /// <value>
  /// This <c>RectangleF</c> value describes the size and position of the object
  /// in document coordinates.
  /// The <c>Width</c> and <c>Height</c> must be non-negative.
  /// </value>
  /// <remarks>
  /// When getting the bounds, if <see cref="P:Intermech.Map.MapObject.InvalidBounds" /> is true,
  /// we call <see cref="M:Intermech.Map.MapObject.ComputeBounds" /> to get the correct updated bounds.
  /// When setting the bounds, we call <see cref="M:Intermech.Map.MapObject.OnBoundsChanged(System.Drawing.RectangleF)" />,
  /// <see cref="M:Intermech.Map.MapGroup.OnChildBoundsChanged(Intermech.Map.MapObject,System.Drawing.RectangleF)" /> on the <see cref="P:Intermech.Map.MapObject.Parent" /> (if any),
  /// and <see cref="M:Intermech.Map.MapObject.Changed(System.Int32,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" /> with a subhint of <c>ChangedBounds</c>.
  /// You should override setting this property if you want to make sure this
  /// object never gets certain bounds, such as a size that's too small or large,
  /// or a position that is "out-of-bounds" for your application.
  /// However, if you only want to constrain how the user is allowed to
  /// move this object around with the mouse, you should override
  /// <see cref="M:Intermech.Map.MapObject.ComputeMove(System.Drawing.PointF,System.Drawing.PointF)" /> instead, or override <see cref="M:Intermech.Map.MapObject.DoMove(Intermech.Map.MapView,System.Drawing.PointF,System.Drawing.PointF)" />
  /// if the constraint should be specific to a particular view or if
  /// something other than the <see cref="P:Intermech.Map.MapObject.Location" /> should be set.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapObject.Position" />
  /// <seealso cref="P:Intermech.Map.MapObject.Size" />
  /// <seealso cref="P:Intermech.Map.MapObject.Center" />
  /// <seealso cref="P:Intermech.Map.MapObject.Left" />
  /// <seealso cref="P:Intermech.Map.MapObject.Top" />
  /// <seealso cref="P:Intermech.Map.MapObject.Width" />
  /// <seealso cref="P:Intermech.Map.MapObject.Height" />
  /// <seealso cref="P:Intermech.Map.MapObject.Right" />
  /// <seealso cref="P:Intermech.Map.MapObject.Bottom" />
  /// <seealso cref="M:Intermech.Map.MapObject.GetSpotLocation(System.Int32)" />
  /// <seealso cref="M:Intermech.Map.MapObject.SetSpotLocation(System.Int32,System.Drawing.PointF)" />
  /// <seealso cref="P:Intermech.Map.MapObject.Location" />
  [Browsable(false)]
  [Category("Bounds")]
  public virtual RectangleF Bounds
  {
    get
    {
      if (this.InvalidBounds && !this.SkipsBoundsChanged)
      {
        this.InvalidBounds = false;
        this.SkipsBoundsChanged = true;
        this.Bounds = this.ComputeBounds();
        this.SkipsBoundsChanged = false;
      }
      return this.myBounds;
    }
    set
    {
      RectangleF bounds = this.myBounds;
      if ((double) value.Width < 0.0 || (double) value.Height < 0.0 || !(bounds != value))
        return;
      this.myBounds = value;
      if (!this.SkipsBoundsChanged)
      {
        this.SkipsBoundsChanged = true;
        this.OnBoundsChanged(bounds);
        if (this.InvalidBounds)
        {
          this.InvalidBounds = false;
          this.Bounds = this.ComputeBounds();
        }
      }
      this.SkipsBoundsChanged = false;
    }
  }

  /// <summary>
  /// Recalculates the actual bounding rectangle for this object when it might
  /// be invalid.
  /// </summary>
  /// <returns>The true bounding rectangle, in document coordinates.</returns>
  /// <remarks>
  /// This method is called if the <see cref="P:Intermech.Map.MapObject.InvalidBounds" /> property
  /// is true, and some code needs the value of the <see cref="P:Intermech.Map.MapObject.Bounds" />
  /// property or after the bounds have changed and <see cref="M:Intermech.Map.MapObject.OnBoundsChanged(System.Drawing.RectangleF)" />
  /// or <see cref="M:Intermech.Map.MapGroup.OnChildBoundsChanged(Intermech.Map.MapObject,System.Drawing.RectangleF)" /> have been called.
  /// The <see cref="P:Intermech.Map.MapObject.InvalidBounds" /> property is set back to false
  /// just before calling this method.
  /// </remarks>
  protected virtual RectangleF ComputeBounds() => this.Bounds;

  /// <summary>Gets or sets this object's width.</summary>
  /// <value>
  /// The <c>float</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// This is just a convenience property that operates on this object's
  /// <see cref="P:Intermech.Map.MapObject.Bounds" />.
  /// </remarks>
  [Description("The width of the Bounds.")]
  [Category("Bounds")]
  public float Width
  {
    get => this.Bounds.Width;
    set => this.Bounds = this.Bounds with { Width = value };
  }

  /// <summary>
  /// Expand a bounding rectangle to better represent where
  /// this object is painted.
  /// </summary>
  /// <param name="rect">
  /// A <c>RectangleF</c> in document coordinates.
  /// </param>
  /// <param name="view">
  /// The view in which the object is being painted.
  /// This may be null, if the particular view is not known.
  /// </param>
  /// <returns>
  /// A <c>RectangleF</c> in document coordinates that may be slightly
  /// larger than the <paramref name="rect" /> argument, to account for
  /// where this object may be painted.
  /// </returns>
  /// <remarks>
  /// The <see cref="P:Intermech.Map.MapObject.Bounds" /> property provides the abstract position and
  /// size of an object.  However, the actual painted area is often somewhat
  /// larger, because of the thickness of a <c>Pen</c> or because of a
  /// shadow.
  /// The default behavior of this method is just to return the
  /// <paramref name="rect" /> value.
  /// </remarks>
  public virtual RectangleF ExpandPaintBounds(RectangleF rect, IView view) => rect;

  internal void SetLayer(Layer layer, IObject obj, bool undoing) => obj._layer = layer;

  public abstract void Paint(Graphics g, IView view);

  public virtual void Dispose()
  {
  }

  public virtual void DoMove(IView view, PointF origLoc, PointF newLoc)
  {
    this.Location = this.ComputeMove(origLoc, newLoc);
  }
}
