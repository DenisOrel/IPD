
// Type: Intermech.DocumentView.Document
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.DocumentView;

public class Document : IDocument
{
  internal static bool myCaching = true;
  [NonSerialized]
  private DocumentChangedEventArgs myChangedEventArgs;
  private SizeF myDocumentSize;
  private PointF myDocumentTopLeft;
  private bool mySuspendsUpdates;
  private Color myPaperColor = Color.Empty;
  private bool myAllowSelect = true;
  private Layer _defaultLayer;
  private ArrayList _layers = new ArrayList();
  /// <summary>
  /// This is an empty <c>RectangleF</c>, which is convenient when calling <see cref="M:Intermech.Map.MapDocument.RaiseChanged(System.Int32,System.Int32,System.Object,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// </summary>
  protected static readonly RectangleF NullRect;

  /// <summary>
  /// The Changed event is raised whenever a document or a part of a document is modified.
  /// </summary>
  /// <remarks>
  /// Any Changed event handlers should not modify this document or any part of this document.
  /// </remarks>
  public event DocumentChangedEventHandler Changed;

  /// <summary>Create a document containing one empty layer.</summary>
  public Document()
  {
    this.mySuspendsUpdates = false;
    this.myChangedEventArgs = (DocumentChangedEventArgs) null;
  }

  /// <summary>
  /// Add an object to the <see cref="P:Intermech.Map.MapDocument.DefaultLayer" />.
  /// </summary>
  /// <param name="obj"></param>
  /// <remarks>
  /// It is an error if the <paramref name="obj" /> belongs to a different document
  /// or to a <see cref="T:Intermech.Map.MapGroup" />.
  /// If the object already belongs to this document, nothing happens.
  /// If the object is a link, it is conventional to add the link to
  /// the <see cref="P:Intermech.Map.MapDocument.LinksLayer" /> rather than to the <see cref="P:Intermech.Map.MapDocument.DefaultLayer" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapDocument.AddCopy(Intermech.Map.MapObject,System.Drawing.PointF)" />
  public virtual void Add(IObject obj)
  {
    this.DefaultLayer.Add(obj);
    this.ChangeValue((DocumentChangedEventArgs) null, false);
  }

  /// <summary>
  /// Gets or sets the layer that is considered the default layer for document
  /// operations that do not specify a layer.
  /// </summary>
  /// <value>
  /// The <see cref="T:Intermech.Map.MapLayer" /> value must not be null and must already
  /// belong to this document.
  /// </value>
  /// <seealso cref="P:Intermech.Map.MapDocument.LinksLayer" />
  /// <seealso cref="P:Intermech.Map.MapDocument.Layers" />
  [Description("The default layer used when adding objects to the document.")]
  public virtual Layer DefaultLayer
  {
    get
    {
      if (this._defaultLayer == null)
      {
        if (this._layers.Count == 0)
          this._layers.Add((object) new Layer());
        this._defaultLayer = (Layer) this._layers[0];
      }
      return this._defaultLayer;
    }
    set => this._defaultLayer = value;
  }

  /// <summary>
  /// Gets the collection of layers belonging to this document.
  /// </summary>
  /// <remarks>
  /// This value is the list of this document's layers.
  /// Use <see cref="T:Intermech.Map.MapLayerCollection" /> methods for creating new
  /// document layers, removing them, or operating on particular layers,
  /// such as the <see cref="P:Intermech.Map.MapLayerCollection.Default" /> one.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapDocument.DefaultLayer" />
  /// <seealso cref="P:Intermech.Map.MapDocument.LinksLayer" />
  /// <seealso cref="T:Intermech.Map.MapLayer" />
  /// <seealso cref="T:Intermech.Map.MapView" />
  [Browsable(false)]
  public virtual Layer[] Layers => (Layer[]) this._layers.ToArray(typeof (Layer));

  /// <summary>Gets or sets the size of this document.</summary>
  /// <value>
  /// The <c>SizeF</c> value is in document coordinates and should have non-negative
  /// width and height.
  /// </value>
  /// <remarks>
  /// The default behavior is that this property automatically expands to include all
  /// of the objects in the document.  This policy is implemented in
  /// <see cref="M:Intermech.Map.MapDocument.UpdateDocumentBounds(Intermech.Map.MapObject)" />.  Set <see cref="P:Intermech.Map.MapDocument.FixedSize" /> to avoid this
  /// default policy, or override <see cref="M:Intermech.Map.MapDocument.UpdateDocumentBounds(Intermech.Map.MapObject)" /> to implement your
  /// own policy.
  /// This property automatically affects what a view can show and where the user can
  /// scroll to.
  /// </remarks>
  [Description("The size of this document.")]
  public virtual SizeF Size
  {
    get => this.myDocumentSize;
    set
    {
      if ((double) value.Width == -23.0)
      {
        if ((double) value.Height == -23.0)
          Document.myCaching = true;
        else if ((double) value.Height == -24.0)
          Document.myCaching = false;
      }
      SizeF documentSize = this.myDocumentSize;
      if ((double) value.Width < 0.0 || (double) value.Height < 0.0 || !(documentSize != value))
        return;
      this.myDocumentSize = value;
      this.RaiseChanged(202, 0, (object) null, 0, (object) null, IObject.MakeRect(documentSize), 0, (object) null, IObject.MakeRect(value));
    }
  }

  /// <summary>
  /// Gets or sets the top-left corner position of this document.
  /// </summary>
  /// <value>
  /// The <c>PointF</c> value is in document coordinates.
  /// Initially this value is (0, 0).
  /// </value>
  /// <remarks>
  /// The default behavior is that this property automatically moves toward
  /// negative coordinates to include all of the objects in the document.
  /// This policy is implemented in <see cref="M:Intermech.Map.MapDocument.UpdateDocumentBounds(Intermech.Map.MapObject)" />.
  /// Set <see cref="P:Intermech.Map.MapDocument.FixedSize" /> to avoid this default policy,
  /// or override <see cref="M:Intermech.Map.MapDocument.UpdateDocumentBounds(Intermech.Map.MapObject)" /> to implement your own policy.
  /// This property automatically affects what a view can show and where the user can
  /// scroll to.
  /// Note that the <see cref="P:Intermech.Map.MapView.ShowsNegativeCoordinates" /> property has
  /// no effect on any document.  That property constrains what the user can see,
  /// even if the document includes objects at negative positions.
  /// </remarks>
  [Description("The top-left corner position of this document.")]
  public virtual PointF TopLeft
  {
    get => this.myDocumentTopLeft;
    set
    {
      PointF documentTopLeft = this.myDocumentTopLeft;
      if (!(documentTopLeft != value))
        return;
      this.myDocumentTopLeft = value;
      this.RaiseChanged(203, 0, (object) null, 0, (object) null, IObject.MakeRect(documentTopLeft), 0, (object) null, IObject.MakeRect(value));
    }
  }

  /// <summary>
  /// Any change to a document or to a part of a document may call this method
  /// to invoke the  method, after the change has occurred.
  /// </summary>
  /// <param name="hint"></param>
  /// <param name="subhint"></param>
  /// <param name="obj"></param>
  /// <param name="oldI"></param>
  /// <param name="oldVal"></param>
  /// <param name="oldRect"></param>
  /// <param name="newI"></param>
  /// <param name="newVal"></param>
  /// <param name="newRect"></param>
  /// <remarks>
  /// <para>
  /// This implementation tries to reuse a <see cref="T:Intermech.Map.MapChangedEventArgs" /> instance
  /// that it initializes with the information in the parameters before calling
  /// <see cref="M:Intermech.Map.MapDocument.OnChanged(Intermech.Map.MapChangedEventArgs)" />.
  /// This method is often called by <see cref="M:Intermech.Map.MapObject.Changed(System.Int32,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// </para>
  /// <para>
  /// <list type="table">
  /// <listheader><term><see cref="T:Intermech.Map.MapDocument" /></term></listheader>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.RepaintAll" /></term> <term>100</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.BeginUpdateAllViews" /></term> <term>101</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.EndUpdateAllViews" /></term> <term>102</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.UpdateAllViews" /></term> <term>103</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedName" /></term> <term>201</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedSize" /></term> <term>202</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedTopLeft" /></term> <term>203</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedFixedSize" /></term> <term>204</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedPaperColor" /></term> <term>205</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedDataFormat" /></term> <term>206</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowSelect" /></term> <term>207</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowMove" /></term> <term>208</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowCopy" /></term> <term>209</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowResize" /></term> <term>210</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowReshape" /></term> <term>211</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowDelete" /></term> <term>212</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowInsert" /></term> <term>213</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowLink" /></term> <term>214</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedAllowEdit" /></term> <term>215</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.AllArranged" /></term> <term>220</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedUserFlags" /></term> <term>221</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedUserObject" /></term> <term>222</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedLinksLayer" /></term> <term>223</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedMaintainsPartID" /></term> <term>224</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.ChangedValidCycle" /></term> <term>225</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapDocument.LastHint" /></term> <term>10000</term> </item>
  /// </list>
  /// <list type="table">
  /// <listheader><term><see cref="T:Intermech.Map.MapLayerCollection" /></term></listheader>
  /// <item> <term><see cref="F:Intermech.Map.MapLayerCollection.InsertedLayer" /></term> <term>801</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayerCollection.RemovedLayer" /></term> <term>802</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayerCollection.MovedLayer" /></term> <term>803</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayerCollection.ChangedDefault" /></term> <term>804</term> </item>
  /// </list>
  /// <list type="table">
  /// <listheader><term><see cref="T:Intermech.Map.MapLayer" /></term></listheader>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedObject" /></term> <term>901 See also the GoObject.Changed method: <see cref="M:Intermech.Map.MapObject.Changed(System.Int32,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" /></term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.InsertedObject" /></term> <term>902</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.RemovedObject" /></term> <term>903</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedObjectLayer" /></term> <term>904</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowView" /></term> <term>910</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowSelect" /></term> <term>911</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowMove" /></term> <term>912</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowCopy" /></term> <term>913</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowResize" /></term> <term>914</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowReshape" /></term> <term>915</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowDelete" /></term> <term>916</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowInsert" /></term> <term>917</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowLink" /></term> <term>918</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowEdit" /></term> <term>919</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedAllowPrint" /></term> <term>920</term> </item>
  /// <item> <term><see cref="F:Intermech.Map.MapLayer.ChangedIdentifier" /></term> <term>930</term> </item>
  /// </list>
  /// Please note that this list may not be complete--in fact you are encouraged to
  /// add new subhints for your own properties and other changes.
  /// </para>
  /// </remarks>
  public virtual void RaiseChanged(
    int hint,
    int subhint,
    object obj,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect)
  {
    this.invokeOnChanged(hint, subhint, obj, oldI, oldVal, oldRect, newI, newVal, newRect, false);
  }

  private void invokeOnChanged(
    int hint,
    int subhint,
    object obj,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect,
    bool before)
  {
    if (this.SuspendsUpdates)
      return;
    DocumentChangedEventArgs evt = this.myChangedEventArgs;
    if (evt == null)
    {
      evt = new DocumentChangedEventArgs();
      evt.Document = (IDocument) this;
    }
    evt.IsBeforeChanging = before;
    evt.Hint = hint;
    evt.SubHint = subhint;
    evt.Object = obj;
    evt.OldInt = oldI;
    evt.OldValue = oldVal;
    evt.OldRect = oldRect;
    evt.NewInt = newI;
    evt.NewValue = newVal;
    evt.NewRect = newRect;
    this.myChangedEventArgs = (DocumentChangedEventArgs) null;
    this.OnChanged(evt);
    this.myChangedEventArgs = evt;
    evt.Object = (object) null;
    evt.OldValue = (object) null;
    evt.NewValue = (object) null;
  }

  /// <summary>
  /// Gets or sets whether any Changed event handlers are called upon a
  /// document or document object change.
  /// </summary>
  /// <value>
  /// A value of true means that any Changed event handlers and any
  /// UndoManager are not called.
  /// A value of false means that the notifications do take place.
  /// The default value is false.
  /// </value>
  /// <remarks>
  /// When this property is true, no views of this document will be updated
  /// as the document is changed, and no undo/redo information is kept.
  /// When you set the property to false again, you will need to make
  /// sure all the views are correct (you may wish to call <see cref="M:Intermech.Map.MapDocument.InvalidateViews" />)
  /// and that the <see cref="P:Intermech.Map.MapDocument.UndoManager" />
  /// (if any) is in a satisfactory state (you may wish to call
  /// <see cref="M:Intermech.Map.MapUndoManager.Clear" />,
  /// so that it cannot be confused by the loss of any undo/redo
  /// information while this property was true).
  /// No Changed event is raised when this property is set.
  /// </remarks>
  [Browsable(false)]
  public bool SuspendsUpdates
  {
    get => this.mySuspendsUpdates;
    set
    {
      this.mySuspendsUpdates = value;
      int num = value ? 1 : 0;
    }
  }

  /// <summary>
  /// Called when a document object's bounds changes to possibly update the document's bounds.
  /// </summary>
  /// <param name="obj"></param>
  /// <remarks>
  /// This method does nothing if <see cref="P:Intermech.Map.MapDocument.FixedSize" /> is true.
  /// Otherwise it increases the <see cref="P:Intermech.Map.MapDocument.Size" /> property and moves
  /// the <see cref="P:Intermech.Map.MapDocument.TopLeft" /> point farther towards negative coordinates
  /// as needed to include the <paramref name="obj" />'s bounds.
  /// By default this method never shrinks the document.
  /// Note also that this method is not called while <see cref="P:Intermech.Map.MapDocument.SuspendsUpdates" /> is true.
  /// If you do add objects or modify their bounds while <see cref="P:Intermech.Map.MapDocument.SuspendsUpdates" />
  /// is true, afterwards you can explicitly set <see cref="P:Intermech.Map.MapDocument.TopLeft" /> and <see cref="P:Intermech.Map.MapDocument.Size" />
  /// to accommodate the new or modified document objects.
  /// </remarks>
  public virtual void UpdateDocumentBounds(IObject obj)
  {
    if (obj == null)
      return;
    SizeF size = this.Size;
    PointF topLeft = this.TopLeft;
    RectangleF bounds = obj.Bounds;
    float x = Math.Min(topLeft.X, bounds.X);
    float y = Math.Min(topLeft.Y, bounds.Y);
    float num1 = Math.Max(topLeft.X + size.Width, bounds.X + bounds.Width);
    double num2 = (double) Math.Max(topLeft.Y + size.Height, bounds.Y + bounds.Height);
    float width = num1 - x;
    double num3 = (double) y;
    float height = (float) (num2 - num3);
    if ((double) x < (double) topLeft.X || (double) y < (double) topLeft.Y)
      this.TopLeft = new PointF(x, y);
    if ((double) width <= (double) size.Width && (double) height <= (double) size.Height)
      return;
    this.Size = new SizeF(width, height);
  }

  public virtual void ChangeValue(DocumentChangedEventArgs e, bool undo)
  {
    this.Size = new SizeF(0.0f, 0.0f);
    this.TopLeft = new PointF(0.0f, 0.0f);
    foreach (Layer layer in this._layers)
    {
      foreach (IObject iobject in layer.Objects)
        this.UpdateDocumentBounds(iobject);
    }
    this.OnChanged(e);
  }

  public void Clear()
  {
    if (this._layers == null)
      return;
    foreach (Layer layer in this._layers)
      layer.Clear();
    this._layers.Clear();
    if (this._defaultLayer == null)
      return;
    this._defaultLayer.Clear();
    this._defaultLayer = (Layer) null;
  }

  /// <summary>
  /// Called when any part of this document has changed, to invoke all Changed event handlers.
  /// </summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This method is called after this document of a part of this document has been modified.
  /// To avoid confusion, this method and any method that it calls should not modify the
  /// document.
  /// Besides invoking all Changed event handlers, this also calls
  /// <see cref="M:Intermech.Map.MapUndoManager.DocumentChanged(System.Object,Intermech.Map.MapChangedEventArgs)" /> if there is an <see cref="P:Intermech.Map.MapDocument.UndoManager" />
  /// and sets <see cref="P:Intermech.Map.MapDocument.IsModified" /> to true, unless <see cref="P:Intermech.Map.MapDocument.SkipsUndoManager" /> is true.
  /// Furthermore by default this method calls <see cref="M:Intermech.Map.MapDocument.UpdateDocumentBounds(Intermech.Map.MapObject)" /> if
  /// an object is inserted into a layer or it its bounds change.
  /// </remarks>
  protected virtual void OnChanged(DocumentChangedEventArgs evt)
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, evt);
  }

  /// <summary>
  /// Gets or sets the color for this document's background.
  /// </summary>
  /// <value>
  /// The default value is <c>Color.Empty</c>.
  /// </value>
  /// <remarks>
  /// Documents can have their own background, independent of any background
  /// color provided by a view.  The normal behavior is that a view will
  /// use the document's <c>PaperColor</c> property when that color is
  /// not <c>Color.Empty</c>, but will otherwise use the view's <c>BackColor</c>
  /// property.  However, there may be times when both or neither color is
  /// used in a rendering of the document.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.PaintPaperColor(System.Drawing.Graphics,System.Drawing.RectangleF)" />
  [Description("The color of the document's background.")]
  [Category("Appearance")]
  public virtual Color PaperColor
  {
    get => this.myPaperColor;
    set
    {
      Color paperColor = this.myPaperColor;
      if (!(paperColor != value))
        return;
      this.myPaperColor = value;
      this.RaiseChanged(205, 0, (object) null, 0, (object) paperColor, Document.NullRect, 0, (object) value, Document.NullRect);
    }
  }

  /// <summary>
  /// Get the smallest rectangle that includes the bounds of all of the
  /// objects in a collection.
  /// </summary>
  /// <param name="objects"></param>
  /// <param name="view">May be null.</param>
  /// <returns>
  /// A <c>RectangleF</c> that encloses all of the objects in the
  /// collection, which might not include the (0, 0) origin point
  /// </returns>
  /// <remarks>
  /// This method uses <see cref="M:Intermech.Map.MapObject.ExpandPaintBounds(System.Drawing.RectangleF,Intermech.Map.MapView)" /> to include
  /// areas beyond the immediate <see cref="P:Intermech.Map.MapObject.Bounds" />, perhaps
  /// affected by the <paramref name="view" />.
  /// </remarks>
  public static RectangleF ComputeBounds(ICollection objects, IView view)
  {
    bool flag = false;
    float x = 0.0f;
    float y = 0.0f;
    float num1 = 0.0f;
    float num2 = 0.0f;
    foreach (IObject iobject in (IEnumerable) objects)
    {
      if (iobject.CanView())
      {
        RectangleF bounds = iobject.Bounds;
        RectangleF rectangleF = iobject.ExpandPaintBounds(bounds, view);
        if (!flag)
        {
          flag = true;
          x = rectangleF.X;
          y = rectangleF.Y;
          num1 = rectangleF.X + rectangleF.Width;
          num2 = rectangleF.Y + rectangleF.Height;
        }
        else
        {
          if ((double) rectangleF.X < (double) x)
            x = rectangleF.X;
          if ((double) rectangleF.Y < (double) y)
            y = rectangleF.Y;
          if ((double) rectangleF.X + (double) rectangleF.Width > (double) num1)
            num1 = rectangleF.X + rectangleF.Width;
          if ((double) rectangleF.Y + (double) rectangleF.Height > (double) num2)
            num2 = rectangleF.Y + rectangleF.Height;
        }
      }
    }
    return flag ? new RectangleF(x, y, num1 - x, num2 - y) : new RectangleF();
  }

  public RectangleF ComputeBounds(Layer[] layers, IView view)
  {
    ArrayList objects = new ArrayList();
    foreach (Layer layer in layers)
      objects.AddRange((ICollection) layer.Objects);
    return Document.ComputeBounds((ICollection) objects, view);
  }

  /// <summary>
  /// Gets or sets whether the user can select objects in this document.
  /// </summary>
  /// <remarks>
  /// A false value prevents the user from selecting objects in this document
  /// by the normal mechanisms.
  /// Even when this property value is true, some objects might not be
  /// selectable by the user because the object or its layer disallows it,
  /// or because the view disallows it, or because the object is not visible.
  /// Your code can always select objects programmatically by calling
  /// <c>aView.Selection.Select(obj)</c> or <c>aView.Selection.Add(obj)</c>.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapDocument.CanSelectObjects" />
  /// <seealso cref="P:Intermech.Map.MapLayer.AllowSelect" />
  /// <seealso cref="P:Intermech.Map.MapObject.Selectable" />
  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Whether the user can select objects in this document.")]
  public virtual bool AllowSelect
  {
    get => this.myAllowSelect;
    set
    {
      bool allowSelect = this.myAllowSelect;
      if (allowSelect == value)
        return;
      this.myAllowSelect = value;
      this.RaiseChanged(207, 0, (object) this, 0, (object) allowSelect, Document.NullRect, 0, (object) value, Document.NullRect);
    }
  }

  /// <summary>
  /// Called to see if the user can select objects in this document.
  /// </summary>
  /// <remarks>
  /// By default this just returns <c>AllowSelect</c>,
  /// This property is used by methods such as <see cref="M:Intermech.Map.MapView.SelectInRectangle(System.Drawing.RectangleF)" />
  /// and <see cref="M:Intermech.Map.MapDocument.PickObject(System.Drawing.PointF,System.Boolean)" />.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapDocument.AllowSelect" />
  /// <seealso cref="M:Intermech.Map.MapLayer.CanSelectObjects" />
  /// <seealso cref="M:Intermech.Map.MapObject.CanSelect" />
  public virtual bool CanSelectObjects() => this.AllowSelect;
}
