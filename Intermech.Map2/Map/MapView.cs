// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapView
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapView : Control, IMapLayerCollectionContainer, IMapLayerAbilities
    {
      public static float MillimetersPerInch = 25.4f;
      protected bool my_IsZoomToFit;
      private bool myAllowCopy;
      private bool myAllowDelete;
      private bool myAllowDragOut;
      private bool myAllowEdit;
      private bool myAllowInsert;
      private bool myAllowKey;
      private bool myAllowLink;
      private bool myAllowMouse;
      private bool myAllowMove;
      private bool myAllowReshape;
      private bool myAllowResize;
      private bool myAllowSelect;
      private Size myAutoPanRegion;
      private int myAutoScrollDelay;
      [NonSerialized]
      private Point myAutoScrollPoint;
      private Size myAutoScrollRegion;
      private int myAutoScrollTime;
      [NonSerialized]
      private System.Threading.Timer myAutoScrollTimer;
      [NonSerialized]
      private bool myAutoScrollTimerEnabled;
      [NonSerialized]
      private SolidBrush myBackgroundBrush;
      private Border3DStyle myBorder3DStyle;
      protected Size myBorderSize;
      private BorderStyle myBorderStyle;
      private float myBoundingHandlePenWidth;
      [NonSerialized]
      public Bitmap myBuffer;
      [NonSerialized]
      private bool useBuffer = true;
      [NonSerialized]
      private bool myCancelMouseDown;
      private Control myCorner;
      [NonSerialized]
      private Cursor myDefaultCursor;
      private IMapTool myDefaultTool;
      [NonSerialized]
      private MapChangedEventHandler myDocChangedEventHandler;
      private MapDocument myDocument;
      private bool myDragsRealtime;
      [NonSerialized]
      private MapControl myEditControl;
      private bool myExternalDragDropsOnEnter;
      private MapObject myExternalDragImage;
      private MapInputEventArgs myFirstInput;
      [NonSerialized]
      private ArrayList myMapControls;
      [NonSerialized]
      public Graphics myGraphics;
      private SizeF myGridCellSize;
      private Color myGridColor;
      private PointF myGridOrigin;
      [NonSerialized]
      private Pen myGridPen;
      private DashStyle myGridPenDashStyle;
      private float myGridPenWidth;
      private MapViewGridStyle myGridStyle;
      private bool myHidesSelection;
      protected float myHorizScale;
      private HScrollBar myHorizScroll;
      private ScrollEventHandler myHorizScrollHandler;
      private int myHoverDelay;
      private Point myHoverPoint;
      [NonSerialized]
      private System.Threading.Timer myHoverTimer;
      [NonSerialized]
      private bool myHoverTimerEnabled;
      private ImageList myImageList;
      private InterpolationMode myInterpolationMode;
      private MapInputEventArgs myLastInput;
      private MapLayerCollection myLayers;
      private int myMaximumSelectionCount;
      [NonSerialized]
      private Control myModalControl;
      protected ArrayList _mouseDownTools;
      protected ArrayList _mouseMoveTools;
      protected ArrayList _mouseUpTools;
      private System.Type myNewLinkClass;
      private Color myNoFocusSelectionColor;
      protected PointF myOrigin;
      [NonSerialized]
      public PaintEventArgs myPaintEventArgs;
      private float myPaintGreekScale;
      private float myPaintNothingScale;
      [NonSerialized]
      private bool myPanning;
      [NonSerialized]
      private Point myPanningOrigin;
      private float myPortGravity;
      [NonSerialized]
      private bool myPretendInternalDrag;
      private Color myPrimarySelectionColor;
      [NonSerialized]
      private MapView.PrintInfo myPrintInfo;
      private float myPrintScale;
      [NonSerialized]
      private Queue myQueuedEvents;
      protected float myResizeHandlePenWidth;
      protected SizeF myResizeHandleSize;
      [NonSerialized]
      private EventHandler mySafeOnDocumentChangedDelegate;
      protected int myScrollBarHeight;
      protected int myScrollBarWidth;
      private Size myScrollSmallChange;
      private Color mySecondarySelectionColor;
      private MapSelection mySelection;
      private bool mySelectsByFirstChar;
      [NonSerialized]
      private SolidBrush myShadowBrush;
      private Color myShadowColor;
      private SizeF myShadowOffset;
      [NonSerialized]
      private Pen myShadowPen;
      private MapViewScrollBarVisibility myShowHorizScroll;
      private bool myShowsNegativeCoordinates;
      private MapViewScrollBarVisibility myShowVertScroll;
      private SmoothingMode mySmoothingMode;
      private MapViewSnapStyle mySnapDrag;
      private MapViewSnapStyle mySnapResize;
      protected int mySuppressPaint;
      [NonSerialized]
      private PointF[][] myTempArrays;
      private TextRenderingHint myTextRenderingHint;
      private IMapTool myTool;
      [NonSerialized]
      private ToolTip myToolTip;
      protected bool myUpdatingScrollBars;
      internal static Assembly myVersionAssembly;
      internal static string myVersionName = "";
      protected float myVertScale;
      private VScrollBar myVertScroll;
      private ScrollEventHandler myVertScrollHandler;

      public event MapInputEventHandler BackgroundContextClicked;

      public event MapInputEventHandler BackgroundDoubleClicked;

      public event MapInputEventHandler BackgroundHover;

      public event MapInputEventHandler BackgroundSingleClicked;

      public event EventHandler ClipboardPasted;

      public event MapChangedEventHandler DocumentChanged;

      public event MapInputEventHandler ExternalObjectsDropped;

      public event MapSelectionEventHandler LinkCreated;

      public event MapSelectionEventHandler LinkRelinked;

      public event MapObjectEventHandler ObjectContextClicked;

      public event MapObjectEventHandler ObjectDoubleClicked;

      public event MapSelectionEventHandler ObjectEdited;

      public event MapSelectionEventHandler ObjectGotSelection;

      public event MapObjectEventHandler ObjectHover;

      public event MapSelectionEventHandler ObjectLostSelection;

      public event MapSelectionEventHandler ObjectResized;

      public event MapObjectEventHandler ObjectSingleClicked;

      public event PropertyChangedEventHandler PropertyChanged;

      public event EventHandler SelectionCopied;

      public event EventHandler SelectionDeleted;

      public event CancelEventHandler SelectionDeleting;

      public event EventHandler SelectionMoved;

      public event EventHandler ViewChanged;

      public event EventHandler ViewChanging;

      static MapView() => MapView.myVersionAssembly = (Assembly) null;

      public MapView()
      {
        this.myVertScroll = (VScrollBar) null;
        this.myHorizScroll = (HScrollBar) null;
        this.myScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
        this.myScrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
        this.myVertScrollHandler = (ScrollEventHandler) null;
        this.myHorizScrollHandler = (ScrollEventHandler) null;
        this.myShowVertScroll = MapViewScrollBarVisibility.IfNeeded;
        this.myShowHorizScroll = MapViewScrollBarVisibility.IfNeeded;
        this.myCorner = (Control) null;
        this.mySafeOnDocumentChangedDelegate = (EventHandler) null;
        this.myQueuedEvents = (Queue) null;
        this.myAllowDragOut = true;
        this.myExternalDragImage = (MapObject) null;
        this.myPretendInternalDrag = false;
        this.myExternalDragDropsOnEnter = false;
        this.myGraphics = (Graphics) null;
        this.myPaintEventArgs = (PaintEventArgs) null;
        this.mySuppressPaint = 0;
        this.myUpdatingScrollBars = true;
        this.myAutoScrollRegion = new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
        this.myAutoScrollTime = 100;
        this.myAutoScrollDelay = 1000;
        this.myAutoScrollTimer = (System.Threading.Timer) null;
        this.myAutoScrollTimerEnabled = false;
        this.myAutoScrollPoint = new Point();
        this.myPanning = false;
        this.myPanningOrigin = new Point();
        this.myToolTip = (ToolTip) null;
        this.myDefaultCursor = (Cursor) null;
        this.myHoverTimer = (System.Threading.Timer) null;
        this.myHoverTimerEnabled = false;
        this.myHoverDelay = 1000;
        this.myHoverPoint = new Point(0, 0);
        this.myPrintInfo = (MapView.PrintInfo) null;
        this.myPrintScale = 0.8f;
        this.myEditControl = (MapControl) null;
        this.myMapControls = (ArrayList) null;
        this.myModalControl = (Control) null;
        this.myCancelMouseDown = false;
        this.myImageList = (ImageList) null;
        this.myBorderStyle = BorderStyle.Fixed3D;
        this.myBorder3DStyle = Border3DStyle.Etched;
        this.myBorderSize = SystemInformation.Border3DSize;
        this.myDocument = (MapDocument) null;
        this.myDocChangedEventHandler = (MapChangedEventHandler) null;
        this.mySelection = (MapSelection) null;
        this.myMaximumSelectionCount = 1000000;
        this.myPrimarySelectionColor = Color.Chartreuse;
        this.mySecondarySelectionColor = Color.Cyan;
        this.myNoFocusSelectionColor = Color.LightGray;
        this.myResizeHandleSize = new SizeF(6f, 6f);
        this.myResizeHandlePenWidth = 1f;
        this.myBoundingHandlePenWidth = 2f;
        this.myHidesSelection = false;
        this.mySelectsByFirstChar = true;
        this.myLayers = (MapLayerCollection) null;
        this.myScrollSmallChange = new Size(16 /*0x10*/, 16 /*0x10*/);
        this.myAutoPanRegion = new Size(16 /*0x10*/, 16 /*0x10*/);
        this.myShowsNegativeCoordinates = true;
        this.myOrigin = new PointF();
        this.myHorizScale = 1f;
        this.myVertScale = 1f;
        this.mySmoothingMode = SmoothingMode.HighQuality;
        this.myTextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        this.myInterpolationMode = InterpolationMode.High;
        this.myAllowSelect = true;
        this.myAllowMove = true;
        this.myAllowCopy = true;
        this.myAllowResize = true;
        this.myAllowReshape = true;
        this.myAllowDelete = true;
        this.myAllowInsert = true;
        this.myAllowLink = true;
        this.myAllowEdit = true;
        this.myAllowMouse = true;
        this.myAllowKey = true;
        this.myBuffer = (Bitmap) null;
        this.myTempArrays = (PointF[][]) null;
        this.myFirstInput = new MapInputEventArgs();
        this.myLastInput = new MapInputEventArgs();
        this.myTool = (IMapTool) null;
        this.myDefaultTool = (IMapTool) null;
        this._mouseDownTools = (ArrayList) null;
        this._mouseMoveTools = (ArrayList) null;
        this._mouseUpTools = (ArrayList) null;
        this.myDragsRealtime = false;
        this.myPortGravity = 100f;
        this.myNewLinkClass = typeof (MapLink);
        this.myBackgroundBrush = (SolidBrush) null;
        this.myGridStyle = MapViewGridStyle.None;
        this.myGridOrigin = new PointF();
        this.myGridCellSize = new SizeF(50f, 50f);
        this.myGridColor = Color.LightGray;
        this.myGridPen = (Pen) null;
        this.myGridPenWidth = 1f;
        this.myGridPenDashStyle = DashStyle.Solid;
        this.mySnapDrag = MapViewSnapStyle.None;
        this.mySnapResize = MapViewSnapStyle.None;
        this.myShadowOffset = new SizeF(0.0f, 0.0f);
        this.myShadowColor = Color.FromArgb((int) sbyte.MaxValue, Color.Gray);
        this.myShadowBrush = (SolidBrush) null;
        this.myShadowPen = (Pen) null;
        this.myPaintNothingScale = 0.13f;
        this.myPaintGreekScale = 0.24f;
        this.init((MapDocument) null);
      }

      public MapView(MapDocument doc)
      {
        this.myVertScroll = (VScrollBar) null;
        this.myHorizScroll = (HScrollBar) null;
        this.myScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
        this.myScrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
        this.myVertScrollHandler = (ScrollEventHandler) null;
        this.myHorizScrollHandler = (ScrollEventHandler) null;
        this.myShowVertScroll = MapViewScrollBarVisibility.IfNeeded;
        this.myShowHorizScroll = MapViewScrollBarVisibility.IfNeeded;
        this.myCorner = (Control) null;
        this.mySafeOnDocumentChangedDelegate = (EventHandler) null;
        this.myQueuedEvents = (Queue) null;
        this.myAllowDragOut = true;
        this.myExternalDragImage = (MapObject) null;
        this.myPretendInternalDrag = false;
        this.myExternalDragDropsOnEnter = false;
        this.myGraphics = (Graphics) null;
        this.myPaintEventArgs = (PaintEventArgs) null;
        this.mySuppressPaint = 0;
        this.myUpdatingScrollBars = true;
        this.myAutoScrollRegion = new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
        this.myAutoScrollTime = 100;
        this.myAutoScrollDelay = 1000;
        this.myAutoScrollTimer = (System.Threading.Timer) null;
        this.myAutoScrollTimerEnabled = false;
        this.myAutoScrollPoint = new Point();
        this.myPanning = false;
        this.myPanningOrigin = new Point();
        this.myToolTip = (ToolTip) null;
        this.myDefaultCursor = (Cursor) null;
        this.myHoverTimer = (System.Threading.Timer) null;
        this.myHoverTimerEnabled = false;
        this.myHoverDelay = 1000;
        this.myHoverPoint = new Point(0, 0);
        this.myPrintInfo = (MapView.PrintInfo) null;
        this.myPrintScale = 0.8f;
        this.myEditControl = (MapControl) null;
        this.myMapControls = (ArrayList) null;
        this.myModalControl = (Control) null;
        this.myCancelMouseDown = false;
        this.myImageList = (ImageList) null;
        this.myBorderStyle = BorderStyle.Fixed3D;
        this.myBorder3DStyle = Border3DStyle.Etched;
        this.myBorderSize = SystemInformation.Border3DSize;
        this.myDocument = (MapDocument) null;
        this.myDocChangedEventHandler = (MapChangedEventHandler) null;
        this.mySelection = (MapSelection) null;
        this.myMaximumSelectionCount = 1000000;
        this.myPrimarySelectionColor = Color.Chartreuse;
        this.mySecondarySelectionColor = Color.Cyan;
        this.myNoFocusSelectionColor = Color.LightGray;
        this.myResizeHandleSize = new SizeF(6f, 6f);
        this.myResizeHandlePenWidth = 1f;
        this.myBoundingHandlePenWidth = 2f;
        this.myHidesSelection = false;
        this.mySelectsByFirstChar = true;
        this.myLayers = (MapLayerCollection) null;
        this.myScrollSmallChange = new Size(16 /*0x10*/, 16 /*0x10*/);
        this.myAutoPanRegion = new Size(16 /*0x10*/, 16 /*0x10*/);
        this.myShowsNegativeCoordinates = true;
        this.myOrigin = new PointF();
        this.myHorizScale = 1f;
        this.myVertScale = 1f;
        this.mySmoothingMode = SmoothingMode.HighQuality;
        this.myTextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        this.myInterpolationMode = InterpolationMode.High;
        this.myAllowSelect = true;
        this.myAllowMove = true;
        this.myAllowCopy = true;
        this.myAllowResize = true;
        this.myAllowReshape = true;
        this.myAllowDelete = true;
        this.myAllowInsert = true;
        this.myAllowLink = true;
        this.myAllowEdit = true;
        this.myAllowMouse = true;
        this.myAllowKey = true;
        this.myBuffer = (Bitmap) null;
        this.myTempArrays = (PointF[][]) null;
        this.myFirstInput = new MapInputEventArgs();
        this.myLastInput = new MapInputEventArgs();
        this.myTool = (IMapTool) null;
        this.myDefaultTool = (IMapTool) null;
        this._mouseDownTools = (ArrayList) null;
        this._mouseMoveTools = (ArrayList) null;
        this._mouseUpTools = (ArrayList) null;
        this.myDragsRealtime = false;
        this.myPortGravity = 100f;
        this.myNewLinkClass = typeof (MapLink);
        this.myBackgroundBrush = (SolidBrush) null;
        this.myGridStyle = MapViewGridStyle.None;
        this.myGridOrigin = new PointF();
        this.myGridCellSize = new SizeF(50f, 50f);
        this.myGridColor = Color.LightGray;
        this.myGridPen = (Pen) null;
        this.myGridPenWidth = 1f;
        this.myGridPenDashStyle = DashStyle.Solid;
        this.mySnapDrag = MapViewSnapStyle.None;
        this.mySnapResize = MapViewSnapStyle.None;
        this.myShadowOffset = new SizeF(5f, 5f);
        this.myShadowColor = Color.FromArgb((int) sbyte.MaxValue, Color.Gray);
        this.myShadowBrush = (SolidBrush) null;
        this.myShadowPen = (Pen) null;
        this.myPaintNothingScale = 0.13f;
        this.myPaintGreekScale = 0.24f;
        this.init(doc);
      }

      public virtual bool AbortTransaction() => this.Document.AbortTransaction();

      internal void AddMapControl(MapControl g, Control c)
      {
        if (this.myMapControls == null)
          this.myMapControls = new ArrayList();
        this.myMapControls.Add((object) g);
        this.Controls.Add(c);
      }

      internal PointF[] AllocTempPointArray(int len)
      {
        if (this.myTempArrays == null || len >= this.myTempArrays.Length)
          this.myTempArrays = new PointF[Math.Max(len + 1, 10)][];
        PointF[] tempArray = this.myTempArrays[len];
        if (tempArray == null)
          return new PointF[len];
        this.myTempArrays[len] = (PointF[]) null;
        return tempArray;
      }

      private void autoScrollCallback(object obj)
      {
        if (!this.IsHandleCreated || this.IsDisposed)
          return;
        this.Invoke((Delegate) obj);
      }

      private void autoScrollTick(object sender, EventArgs evt)
      {
        if (!this.myAutoScrollTimerEnabled)
          return;
        PointF pointF = this.myPanning ? this.ComputeAutoPanDocPosition(this.myPanningOrigin, this.myAutoScrollPoint) : this.ComputeAutoScrollDocPosition(this.myAutoScrollPoint);
        if (pointF == this.DocPosition)
        {
          this.myAutoScrollTimer.Change(this.AutoScrollDelay, -1);
        }
        else
        {
          this.DocPosition = pointF;
          this.myAutoScrollTimer.Change(this.AutoScrollTime, -1);
        }
      }

      public virtual void BeginUpdate() => ++this.mySuppressPaint;

      public virtual bool CanCopyObjects() => this.AllowCopy && this.Document.CanCopyObjects();

      public virtual bool CanDeleteObjects() => this.AllowDelete && this.Document.CanDeleteObjects();

      public virtual bool CanEditCopy()
      {
        return this.CanCopyObjects() && !this.Selection.IsEmpty && this.Selection.Primary.CanCopy();
      }

      public virtual bool CanEditCut()
      {
        if (!this.CanCopyObjects() || !this.CanDeleteObjects() || this.Selection.IsEmpty)
          return false;
        MapObject primary = this.Selection.Primary;
        return primary.CanCopy() && primary.CanDelete();
      }

      public virtual bool CanEditDelete()
      {
        return this.CanDeleteObjects() && !this.Selection.IsEmpty && this.Selection.Primary.CanDelete();
      }

      public virtual bool CanEditEdit()
      {
        return this.CanEditObjects() && !this.Selection.IsEmpty && this.Selection.Primary.CanEdit();
      }

      public virtual bool CanEditObjects() => this.AllowEdit && this.Document.CanEditObjects();

      [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Clipboard=\"AllClipboard\"/>\r\n</PermissionSet>\r\n")]
      public virtual bool CanEditPaste()
      {
        if (!this.CanInsertObjects())
          return false;
        MapDocument document = this.Document;
        if (document == null)
          return false;
        IDataObject dataObject = Clipboard.GetDataObject();
        return dataObject != null && dataObject.GetDataPresent(document.DataFormat);
      }

      public virtual bool CanInsertObjects() => this.AllowInsert && this.Document.CanInsertObjects();

      public virtual bool CanLinkObjects() => this.AllowLink && this.Document.CanLinkObjects();

      public virtual bool CanMoveObjects() => this.AllowMove && this.Document.CanMoveObjects();

      public virtual bool CanRedo() => this.Document.CanRedo();

      public virtual bool CanReshapeObjects() => this.AllowReshape && this.Document.CanReshapeObjects();

      public virtual bool CanResizeObjects() => this.AllowResize && this.Document.CanResizeObjects();

      public virtual bool CanScroll(bool down, bool vertical)
      {
        PointF docPosition = this.DocPosition;
        SizeF docExtentSize = this.DocExtentSize;
        PointF documentTopLeft = this.DocumentTopLeft;
        SizeF documentSize = this.DocumentSize;
        if (vertical)
        {
          if (down)
          {
            ++docPosition.Y;
            docPosition.Y = Math.Min(docPosition.Y, Math.Max(documentTopLeft.Y, documentTopLeft.Y + documentSize.Height - docExtentSize.Height));
          }
          else
          {
            --docPosition.Y;
            docPosition.Y = Math.Max(docPosition.Y, documentTopLeft.Y);
          }
        }
        else if (down)
        {
          ++docPosition.X;
          docPosition.X = Math.Min(docPosition.X, Math.Max(documentTopLeft.X, documentTopLeft.X + documentSize.Width - docExtentSize.Width));
        }
        else
        {
          --docPosition.X;
          docPosition.X = Math.Max(docPosition.X, documentTopLeft.X);
        }
        return docPosition != this.DocPosition;
      }

      public virtual bool CanSelectObjects() => this.AllowSelect && this.Document.CanSelectObjects();

      public virtual bool CanUndo() => this.Document.CanUndo();

      private void CleanUpModalControl()
      {
        if (this.myEditControl == null || this.myModalControl == null)
          return;
        MapControl editControl = this.myEditControl;
        Control modalControl = this.myModalControl;
        this.myEditControl = (MapControl) null;
        this.myModalControl = (Control) null;
        Control comp = modalControl;
        editControl.DisposeControl(comp, this);
      }

      public virtual PointF ComputeAutoPanDocPosition(Point originPnt, Point viewPnt)
      {
        Point view = this.ConvertDocToView(this.DocPosition);
        Size autoPanRegion = this.AutoPanRegion;
        int width1 = this.ScrollSmallChange.Width;
        int height1 = this.ScrollSmallChange.Height;
        Rectangle displayRectangle = this.DisplayRectangle;
        int num1 = viewPnt.X - originPnt.X;
        int num2 = viewPnt.Y - originPnt.Y;
        int width2 = autoPanRegion.Width;
        int height2 = autoPanRegion.Height;
        int num3 = 2 * width2;
        int num4 = 2 * height2;
        if (num1 < -num3)
        {
          int num5 = (num1 + width2) * (num1 + width2) / 100;
          view.X -= Math.Min(displayRectangle.Width, width1 * num5);
        }
        else if (num1 < -width2)
          view.X -= width1;
        else if (num1 > num3)
        {
          int num6 = (num1 - width2) * (num1 - width2) / 100;
          view.X += Math.Min(displayRectangle.Width, width1 * num6);
        }
        else if (num1 > width2)
          view.X += width1;
        if (num2 < -num4)
        {
          int num7 = (num2 + height2) * (num2 + height2) / 100;
          view.Y -= Math.Min(displayRectangle.Height, height1 * num7);
        }
        else if (num2 < -height2)
          view.Y -= height1;
        else if (num2 > num4)
        {
          int num8 = (num2 - height2) * (num2 - height2) / 100;
          view.Y += Math.Min(displayRectangle.Height, height1 * num8);
        }
        else if (num2 > height2)
          view.Y += height1;
        return this.ConvertViewToDoc(view);
      }

      public virtual PointF ComputeAutoScrollDocPosition(Point viewPnt)
      {
        Point view = this.ConvertDocToView(this.DocPosition);
        Size autoScrollRegion = this.AutoScrollRegion;
        int width = this.ScrollSmallChange.Width;
        int height = this.ScrollSmallChange.Height;
        Rectangle displayRectangle = this.DisplayRectangle;
        if (viewPnt.X >= displayRectangle.X && viewPnt.X < displayRectangle.X + autoScrollRegion.Width)
        {
          view.X -= width;
          if (viewPnt.X < displayRectangle.X + autoScrollRegion.Width / 2)
            view.X -= width;
          if (viewPnt.X < displayRectangle.X + autoScrollRegion.Width / 4)
            view.X -= 2 * width;
        }
        else if (viewPnt.X <= displayRectangle.X + displayRectangle.Width && viewPnt.X > displayRectangle.X + displayRectangle.Width - autoScrollRegion.Width)
        {
          view.X += width;
          if (viewPnt.X > displayRectangle.X + displayRectangle.Width - autoScrollRegion.Width / 2)
            view.X += width;
          if (viewPnt.X > displayRectangle.X + displayRectangle.Width - autoScrollRegion.Width / 4)
            view.X += 2 * width;
        }
        if (viewPnt.Y >= displayRectangle.Y && viewPnt.Y < displayRectangle.Y + autoScrollRegion.Height)
        {
          view.Y -= height;
          if (viewPnt.Y < displayRectangle.Y + autoScrollRegion.Height / 2)
            view.Y -= height;
          if (viewPnt.Y < displayRectangle.Y + autoScrollRegion.Height / 4)
            view.Y -= 2 * height;
        }
        else if (viewPnt.Y <= displayRectangle.Y + displayRectangle.Height && viewPnt.Y > displayRectangle.Y + displayRectangle.Height - autoScrollRegion.Height)
        {
          view.Y += height;
          if (viewPnt.Y > displayRectangle.Y + displayRectangle.Height - autoScrollRegion.Height / 2)
            view.Y += height;
          if (viewPnt.Y > displayRectangle.Y + displayRectangle.Height - autoScrollRegion.Height / 4)
            view.Y += 2 * height;
        }
        return this.ConvertViewToDoc(view);
      }

      public virtual RectangleF ComputeDocumentBounds()
      {
        return MapDocument.ComputeBounds((IMapCollection) this.Document, this);
      }

      /// <summary>преоброзовать точку из системы координат Документа в систему координат Окна</summary>
      /// <param name="p">точка в системе координат Документа</param>
      /// <returns>точка в системе координат Окна</returns>
      public virtual Point ConvertDocToView(PointF p)
      {
        PointF docPosition = this.DocPosition;
        return new Point((int) Math.Floor(((double) p.X - (double) docPosition.X) * (double) this.myHorizScale) + this.myBorderSize.Width, (int) Math.Floor(((double) p.Y - (double) docPosition.Y) * (double) this.myVertScale) + this.myBorderSize.Height);
      }

      /// <summary>преоброзовать размер из системы координат Документа в систему координат Окна</summary>
      /// <param name="s">размер в системе координат Документа</param>
      /// <returns>размер в системе координат Окна</returns>
      public virtual Size ConvertDocToView(SizeF s)
      {
        return new Size((int) Math.Ceiling((double) s.Width * (double) this.myHorizScale), (int) Math.Ceiling((double) s.Height * (double) this.myVertScale));
      }

      /// <summary>преоброзовать прямоугольник из системы координат Документа в систему координат Окна</summary>
      /// <param name="r">прямоугольник в системе координат Документа</param>
      /// <returns>прямоугольник в системе координат Окна</returns>
      public virtual Rectangle ConvertDocToView(RectangleF r)
      {
        return new Rectangle(this.ConvertDocToView(r.Location), this.ConvertDocToView(r.Size));
      }

      /// <summary>преоброзовать точку из системы координат Окна в систему координат Документа </summary>
      /// <param name="p">точка в системе координат Окна</param>
      /// <returns>точка в системе координат Документа</returns>
      public virtual PointF ConvertViewToDoc(Point p)
      {
        PointF docPosition = this.DocPosition;
        return new PointF((float) (p.X - this.myBorderSize.Width) / this.myHorizScale + docPosition.X, (float) (p.Y - this.myBorderSize.Height) / this.myVertScale + docPosition.Y);
      }

      /// <summary>преоброзовать размер из системы координат Окна в систему координат Документа </summary>
      /// <param name="s">размер в системе координат Окна</param>
      /// <returns>размер в системе координат Документа</returns>
      public virtual SizeF ConvertViewToDoc(Size s)
      {
        return new SizeF((float) s.Width / this.myHorizScale, (float) s.Height / this.myVertScale);
      }

      /// <summary>преоброзовать прямоугольник из системы координат Окна в систему координат Документа </summary>
      /// <param name="r">прямоугольник в системе координат Окна</param>
      /// <returns>прямоугольник в системе координат Документа</returns>
      public virtual RectangleF ConvertViewToDoc(Rectangle r)
      {
        return new RectangleF(this.ConvertViewToDoc(r.Location), this.ConvertViewToDoc(r.Size));
      }

      public virtual void CopySelection(MapSelection sel, SizeF offset, bool grid)
      {
        if (sel == null)
          sel = this.Selection;
        if (sel == this.Selection && !this.CanCopyObjects() || sel.IsEmpty)
          return;
        MapDocument document = this.Document;
        string tname = (string) null;
        try
        {
          this.StartTransaction();
          MapCopyDictionary copyDictionary = document.CreateCopyDictionary();
          document.CopyFromCollection((IMapCollection) sel, true, true, offset, copyDictionary);
          this.Selection.Clear();
          IDictionaryEnumerator enumerator = copyDictionary.GetEnumerator();
          while (enumerator.MoveNext())
          {
            if (enumerator.Value is MapObject mapObject1 && mapObject1.IsTopLevel && mapObject1.Document == document)
              this.Selection.Add(mapObject1);
          }
          if (grid)
          {
            MapObject mapObject2 = (MapObject) null;
            foreach (MapObject mapObject3 in (MapCollection) this.Selection)
            {
              if (!(mapObject3 is IMapLink))
              {
                mapObject2 = mapObject3;
                break;
              }
            }
            SizeF sizeF = offset;
            if (mapObject2 != null)
            {
              PointF location = mapObject2.Location;
              PointF nearestGridPoint = this.FindNearestGridPoint(location);
              sizeF.Width = nearestGridPoint.X - location.X;
              sizeF.Height = nearestGridPoint.Y - location.Y;
              foreach (MapObject mapObject4 in (MapCollection) this.Selection)
              {
                if (mapObject4 is IMapLink)
                  mapObject4.Position = new PointF(mapObject4.Left + sizeF.Width, mapObject4.Top + sizeF.Height);
              }
            }
            foreach (MapObject mapObject5 in (MapCollection) this.Selection)
            {
              if (!(mapObject5 is IMapLink))
              {
                PointF location = mapObject5.Location;
                PointF nearestGridPoint = this.FindNearestGridPoint(location);
                mapObject5.DoMove(this, location, nearestGridPoint);
              }
            }
          }
          tname = "Copy Selection";
        }
        finally
        {
          this.FinishTransaction(tname);
        }
      }

      [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Clipboard=\"AllClipboard\"/>\r\n</PermissionSet>\r\n")]
      public virtual void CopyToClipboard(IMapCollection coll)
      {
        if (coll == null || coll.IsEmpty)
        {
          Clipboard.SetDataObject((object) new DataObject());
        }
        else
        {
          MapDocument document = this.Document;
          if (document == null)
            return;
          MapDocument instance = (MapDocument) Activator.CreateInstance(document.GetType());
          instance.UndoManager = (MapUndoManager) null;
          instance.MergeLayersFrom(document);
          SizeF offset = new SizeF();
          instance.CopyFromCollection(coll, true, true, offset, (MapCopyDictionary) null);
          Clipboard.SetDataObject((object) this.CreateDataObject(coll, instance));
        }
      }

      protected virtual DataObject CreateDataObject(IMapCollection coll, MapDocument clipdoc)
      {
        DataObject dataObject = new DataObject();
        dataObject.SetData(clipdoc.DataFormat, (object) clipdoc);
        Bitmap bitmapFromCollection = this.GetBitmapFromCollection((IMapCollection) clipdoc);
        dataObject.SetData(DataFormats.Bitmap, true, (object) bitmapFromCollection);
        string data = (string) null;
        foreach (MapObject mapObject in clipdoc)
        {
          if (mapObject is IMapLabeledNode mapLabeledNode)
            data = data != null ? data + Environment.NewLine + mapLabeledNode.Text : mapLabeledNode.Text;
          else if (mapObject is MapText mapText)
            data = data != null ? data + Environment.NewLine + mapText.Text : mapText.Text;
        }
        if (data != null)
          dataObject.SetData(DataFormats.UnicodeText, true, (object) data);
        return dataObject;
      }

      public virtual IMapTool CreateDefaultTool() => (IMapTool) new MapToolManager(this);

      public virtual MapDocument CreateDocument() => new MapDocument();

      public virtual IMapLink CreateLink(IMapPort fromPort, IMapPort toPort)
      {
        if (fromPort != null && toPort != null && fromPort.MapObject != null && toPort.MapObject != null)
        {
          IMapLink instance = (IMapLink) Activator.CreateInstance(this.NewLinkClass);
          if (instance != null && instance.MapObject != null)
          {
            instance.FromPort = fromPort;
            instance.ToPort = toPort;
            MapSubGraph.ReparentToCommonSubGraph(instance.MapObject, fromPort.MapObject, toPort.MapObject, true, this.Document.LinksLayer);
            return instance;
          }
        }
        return (IMapLink) null;
      }

      public virtual MapSelection CreateSelection() => new MapSelection(this);

      public virtual void DeleteSelection(MapSelection sel)
      {
        if (sel == null)
          sel = this.Selection;
        if (sel == this.Selection && !this.CanDeleteObjects() || sel.IsEmpty)
          return;
        string tname = (string) null;
        CancelEventArgs evt = new CancelEventArgs();
        this.RaiseSelectionDeleting(evt);
        if (evt.Cancel)
          return;
        try
        {
          this.StartTransaction();
          MapObject[] mapObjectArray = sel.CopyArray();
          for (int index = mapObjectArray.Length - 1; index >= 0; --index)
          {
            MapObject mapObject = mapObjectArray[index];
            if (mapObject != null && mapObject.CanDelete())
            {
              mapObject.Remove();
              sel.Remove(mapObject);
            }
          }
          tname = "Delete Selection";
        }
        finally
        {
          this.FinishTransaction(tname);
        }
        this.RaiseSelectionDeleted();
      }

      public virtual void DetectHover(Point viewPnt)
      {
        if (this.myHoverTimer == null)
        {
          this.myHoverTimer = new System.Threading.Timer(new TimerCallback(this.hoverCallback), (object) new EventHandler(this.hoverTick), -1, -1);
          this.myHoverTimerEnabled = false;
        }
        if (this.myHoverPoint != viewPnt)
          this.StopHoverTimer();
        if (!this.myHoverTimerEnabled)
        {
          this.myHoverTimer.Change(this.HoverDelay, -1);
          this.myHoverTimerEnabled = true;
        }
        this.myHoverPoint = viewPnt;
      }

      protected override void Dispose(bool disposing)
      {
        if (this.myAutoScrollTimer != null)
        {
          this.myAutoScrollTimer.Dispose();
          this.myAutoScrollTimer = (System.Threading.Timer) null;
        }
        if (this.myHoverTimer != null)
        {
          this.myHoverTimer.Dispose();
          this.myHoverTimer = (System.Threading.Timer) null;
        }
        base.Dispose(disposing);
        if (this.myModalControl != null)
        {
          this.myModalControl.Dispose();
          this.myModalControl = (Control) null;
        }
        this.myDocument.Changed -= this.myDocChangedEventHandler;
        if (this.myBuffer != null)
        {
          this.myBuffer.Dispose();
          this.myBuffer = (Bitmap) null;
        }
        if (this.myBackgroundBrush != null)
        {
          this.myBackgroundBrush.Dispose();
          this.myBackgroundBrush = (SolidBrush) null;
        }
        if (this.myGridPen != null)
        {
          this.myGridPen.Dispose();
          this.myGridPen = (Pen) null;
        }
        if (this.myShadowBrush != null)
        {
          this.myShadowBrush.Dispose();
          this.myShadowBrush = (SolidBrush) null;
        }
        if (this.myShadowPen == null)
          return;
        this.myShadowPen.Dispose();
        this.myShadowPen = (Pen) null;
      }

      public virtual void DoAutoPan(Point originPnt, Point viewPnt)
      {
        this.myPanning = true;
        this.myPanningOrigin = originPnt;
        this.myAutoScrollPoint = viewPnt;
        this.DoInternalAutoScroll();
      }

      public virtual void DoAutoScroll(Point viewPnt)
      {
        this.myPanning = false;
        this.myAutoScrollPoint = viewPnt;
        this.DoInternalAutoScroll();
      }

      public virtual void DoBackgroundMouseOver(MapInputEventArgs evt)
      {
        Cursor defaultCursor = this.DefaultCursor;
        if (!(this.Cursor != defaultCursor))
          return;
        this.Cursor = defaultCursor;
      }

      public virtual void DoCancelMouse()
      {
        this.myCancelMouseDown = true;
        this.Tool.DoCancelMouse();
      }

      public virtual bool DoContextClick(MapInputEventArgs evt)
      {
        MapObject mapObject = this.PickObject(true, false, evt.DocPoint, false);
        if (mapObject != null)
        {
          this.RaiseObjectContextClicked(mapObject, evt);
          for (; mapObject != null; mapObject = (MapObject) mapObject.Parent)
          {
            if (mapObject.OnContextClick(evt, this))
              return true;
          }
        }
        else
          this.RaiseBackgroundContextClicked(evt);
        return false;
      }

      public virtual bool DoDoubleClick(MapInputEventArgs evt)
      {
        MapObject mapObject = this.PickObject(true, false, evt.DocPoint, false);
        if (mapObject != null)
        {
          this.RaiseObjectDoubleClicked(mapObject, evt);
          for (; mapObject != null; mapObject = (MapObject) mapObject.Parent)
          {
            if (mapObject.OnDoubleClick(evt, this))
              return true;
          }
        }
        else
          this.RaiseBackgroundDoubleClicked(evt);
        return false;
      }

      public virtual void DoEndEdit() => this.EditControl?.DoEndEdit(this);

      protected virtual void DoExternalDrag(DragEventArgs evt)
      {
        this.FollowExternalDragImage(this.LastInput.DocPoint);
        if (this.CanInsertObjects())
        {
          evt.Effect = DragDropEffects.All;
          this.DoAutoScroll(this.LastInput.ViewPoint);
        }
        else
          evt.Effect = DragDropEffects.None;
      }

      protected virtual IMapCollection DoExternalDrop(DragEventArgs evt)
      {
        if (evt.Data.GetData(typeof (MapSelection)) is MapSelection data)
        {
          MapDocument document = this.Document;
          if (document != null)
          {
            PointF docPoint = this.LastInput.DocPoint;
            MapObject primary = data.Primary;
            if (primary != null)
            {
              string tname = (string) null;
              MapCollection mapCollection = new MapCollection();
              try
              {
                this.StartTransaction();
                SizeF offset = MapTool.SubtractPoints(docPoint, new PointF(primary.Left + data.HotSpot.Width, primary.Top + data.HotSpot.Height));
                MapCopyDictionary mapCopyDictionary = document.CopyFromCollection((IMapCollection) data, false, false, offset, (MapCopyDictionary) null);
                foreach (MapObject mapObject in (IEnumerable) mapCopyDictionary.Values)
                {
                  if (mapObject != null && mapObject.IsTopLevel)
                  {
                    mapCollection.Add(mapObject);
                    if (this.GridSnapDrag != MapViewSnapStyle.None)
                    {
                      PointF location = mapObject.Location;
                      PointF nearestGridPoint = this.FindNearestGridPoint(location);
                      mapObject.DoMove(this, location, nearestGridPoint);
                    }
                  }
                }
                MapSelection selection = this.Selection;
                selection.Clear();
                MapObject mapObject1 = (MapObject) mapCopyDictionary[(object) primary];
                if (mapObject1 != null && mapCollection.Contains(mapObject1))
                  selection.Add(mapObject1);
                MapCollectionEnumerator enumerator = mapCollection.GetEnumerator();
                while (enumerator.MoveNext())
                {
                  MapObject current = enumerator.Current;
                  selection.Add(current);
                }
                selection.HotSpot = data.HotSpot;
                tname = "Drop";
                this.RaiseExternalObjectsDropped(this.LastInput);
              }
              finally
              {
                this.FinishTransaction(tname);
              }
              return (IMapCollection) mapCollection;
            }
          }
        }
        return (IMapCollection) null;
      }

      public virtual bool DoHover(MapInputEventArgs evt)
      {
        MapObject mapObject = this.PickObject(true, false, evt.DocPoint, false);
        if (mapObject != null)
        {
          this.RaiseObjectHover(mapObject, evt);
          for (; mapObject != null; mapObject = (MapObject) mapObject.Parent)
          {
            if (mapObject.OnHover(evt, this))
              return true;
          }
        }
        else
          this.RaiseBackgroundHover(evt);
        return false;
      }

      private void DoInternalAutoScroll()
      {
        if (this.myAutoScrollTimer == null)
        {
          this.myAutoScrollTimer = new System.Threading.Timer(new TimerCallback(this.autoScrollCallback), (object) new EventHandler(this.autoScrollTick), -1, -1);
          this.myAutoScrollTimerEnabled = false;
        }
        if ((this.myPanning ? this.ComputeAutoPanDocPosition(this.myPanningOrigin, this.myAutoScrollPoint) : this.ComputeAutoScrollDocPosition(this.myAutoScrollPoint)) != this.DocPosition)
        {
          if (this.myAutoScrollTimerEnabled)
            return;
          if (!this.Focused)
            this.myAutoScrollTimer.Change(this.AutoScrollDelay, -1);
          else
            this.myAutoScrollTimer.Change(this.AutoScrollTime, -1);
          this.myAutoScrollTimerEnabled = true;
        }
        else
        {
          if (this.myPanning)
            return;
          this.StopAutoScroll();
        }
      }

      protected virtual void DoInternalDrag(DragEventArgs evt) => this.DoMouseMove();

      protected virtual void DoInternalDrop(DragEventArgs evt) => this.DoMouseUp();

      /// <summary>действия когда клавиша клавиатуры нажата</summary>
      public virtual void DoKeyDown() => this.Tool.DoKeyDown();

      /// <summary>действия когда клавиша мыши нажата</summary>
      public virtual void DoMouseDown()
      {
        int num = this.Focused ? 1 : 0;
        this.InitFocus();
        if (num != 0 || !this.myCancelMouseDown)
          this.Tool.DoMouseDown();
        this.myCancelMouseDown = false;
      }

      public virtual void DoMouseHover() => this.Tool.DoMouseHover();

      /// <summary>действия когда мышь двигают</summary>
      public virtual void DoMouseMove() => this.Tool.DoMouseMove();

      public virtual bool DoMouseOver(MapInputEventArgs evt)
      {
        MapObject mapObject = this.PickObject(true, true, evt.DocPoint, false);
        this.DoToolTipObject(mapObject);
        bool flag = false;
        for (; mapObject != null; mapObject = (MapObject) mapObject.Parent)
        {
          if (mapObject.OnMouseOver(evt, this))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.DoBackgroundMouseOver(evt);
        this.DetectHover(evt.ViewPoint);
        return flag;
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public virtual void DoMouseUp()
      {
        if (this.Tool == null)
          return;
        this.Tool.DoMouseUp();
      }

      public virtual void DoMouseWheel()
      {
        if (this.Tool == null)
          return;
        this.Tool.DoMouseWheel();
      }

      public virtual bool DoSingleClick(MapInputEventArgs evt)
      {
        MapObject mapObject = this.PickObject(true, true, evt.DocPoint, false);
        if (mapObject != null)
        {
          this.RaiseObjectSingleClicked(mapObject, evt);
          for (; mapObject != null; mapObject = (MapObject) mapObject.Parent)
          {
            if (mapObject.OnSingleClick(evt, this))
              return true;
          }
        }
        else
          this.RaiseBackgroundSingleClicked(evt);
        return false;
      }

      public virtual void DoToolTipObject(MapObject obj)
      {
        if (this.ToolTip == null)
          return;
        string toolTip = this.ToolTip.GetToolTip((Control) this);
        string caption = (string) null;
        for (; obj != null; obj = (MapObject) obj.Parent)
        {
          caption = obj.GetToolTip(this);
          if (caption != null)
            break;
        }
        if (caption == null)
          caption = "";
        if (!(caption != toolTip))
          return;
        this.ToolTip.SetToolTip((Control) this, caption);
      }

      public virtual void DoWheel(MapInputEventArgs evt)
      {
        if (evt.Delta == 0)
          return;
        if (evt.Control)
        {
          this.DocScale *= (float) (1.0 + (double) evt.Delta / 2400.0);
        }
        else
        {
          int num = -evt.Delta / 120;
          if (evt.Shift)
            this.ScrollLine((float) num, 0.0f);
          else
            this.ScrollLine(0.0f, (float) num);
        }
      }

      protected virtual void DrawGridCrosses(Graphics g, SizeF cross, RectangleF clipRect)
      {
        float width = this.GridCellSize.Width;
        float height = this.GridCellSize.Height;
        Color color = this.GridColor;
        if (color == Color.Empty)
          color = this.ForeColor;
        float gridPenWidth = this.GridPenWidth;
        if (this.myGridPen == null || this.myGridPen.Color != color || (double) this.myGridPen.Width != (double) gridPenWidth || this.myGridPen.DashStyle != DashStyle.Solid)
        {
          if (this.myGridPen != null)
            this.myGridPen.Dispose();
          this.myGridPen = new Pen(color, gridPenWidth);
        }
        float x1 = clipRect.X - width;
        float y1 = clipRect.Y - height;
        float x2 = clipRect.X + clipRect.Width + width;
        float y2 = clipRect.Y + clipRect.Height + height;
        PointF nearestGridPoint1 = this.FindNearestGridPoint(new PointF(x1, y1));
        PointF nearestGridPoint2 = this.FindNearestGridPoint(new PointF(x2, y2));
        if ((double) cross.Height < 2.0 && (double) cross.Width < 2.0)
        {
          float num = 1f;
          for (float x3 = nearestGridPoint1.X; (double) x3 < (double) nearestGridPoint2.X; x3 += width)
          {
            for (float y3 = nearestGridPoint1.Y; (double) y3 < (double) nearestGridPoint2.Y; y3 += height)
              MapShape.DrawEllipse(g, this, this.myGridPen, (Brush) null, x3, y3, num, num);
          }
        }
        else
        {
          for (float x4 = nearestGridPoint1.X; (double) x4 < (double) nearestGridPoint2.X; x4 += width)
          {
            for (float y4 = nearestGridPoint1.Y; (double) y4 < (double) nearestGridPoint2.Y; y4 += height)
            {
              MapShape.DrawLine(g, this, this.myGridPen, x4, y4 - cross.Height / 2f, x4, y4 + cross.Height / 2f);
              MapShape.DrawLine(g, this, this.myGridPen, x4 - cross.Width / 2f, y4, x4 + cross.Width / 2f, y4);
            }
          }
        }
      }

      protected virtual void DrawGridLines(Graphics g, RectangleF clipRect)
      {
        float width = this.GridCellSize.Width;
        float height = this.GridCellSize.Height;
        Color color = this.GridColor;
        float gridPenWidth = this.GridPenWidth;
        DashStyle gridPenDashStyle = this.GridPenDashStyle;
        if (color == Color.Empty)
          color = this.ForeColor;
        if (this.myGridPen == null || this.myGridPen.Color != color || (double) this.myGridPen.Width != (double) gridPenWidth || this.myGridPen.DashStyle != gridPenDashStyle)
        {
          if (this.myGridPen != null)
            this.myGridPen.Dispose();
          this.myGridPen = new Pen(color, gridPenWidth);
          this.myGridPen.DashStyle = gridPenDashStyle;
        }
        float x1 = clipRect.X - width;
        float y1 = clipRect.Y - height;
        float x2 = clipRect.X + clipRect.Width + width;
        float y2 = clipRect.Y + clipRect.Height + height;
        PointF nearestGridPoint1 = this.FindNearestGridPoint(new PointF(x1, y1));
        PointF nearestGridPoint2 = this.FindNearestGridPoint(new PointF(x2, y2));
        for (float x3 = nearestGridPoint1.X; (double) x3 < (double) nearestGridPoint2.X; x3 += width)
          MapShape.DrawLine(g, this, this.myGridPen, x3, clipRect.Y, x3, clipRect.Y + clipRect.Height);
        for (float y3 = nearestGridPoint1.Y; (double) y3 < (double) nearestGridPoint2.Y; y3 += height)
          MapShape.DrawLine(g, this, this.myGridPen, clipRect.X, y3, clipRect.X + clipRect.Width, y3);
      }

      public virtual void DrawXorBox(Rectangle rect)
      {
        try
        {
          this.Refresh();
          this.DrawRectangle(rect);
        }
        catch
        {
          this.Refresh();
        }
      }

      public void DrawXorLine(int ax, int ay, int bx, int by)
      {
        Point p1 = new Point(ax, ay);
        Point p2 = new Point(bx, by);
        Point screen1 = this.PointToScreen(p1);
        Point screen2 = this.PointToScreen(p2);
        Color color = this.Document.PaperColor;
        if (color == Color.Empty)
          color = this.BackColor;
        Point end = screen2;
        Color backColor = color;
        ControlPaint.DrawReversibleLine(screen1, end, backColor);
      }

      public void DrawRectangle(Rectangle rect)
      {
        using (Graphics graphics = this.CreateGraphics())
        {
          Pen pen = new Pen(Color.Black, 1f)
          {
            DashStyle = DashStyle.Solid
          };
          graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }

      public virtual void EditCopy()
      {
        if (!this.CanCopyObjects())
          return;
        string tname = (string) null;
        try
        {
          this.StartTransaction();
          this.CopyToClipboard((IMapCollection) this.Selection);
          tname = "Copy";
        }
        catch (Exception ex)
        {
          MapObject.Trace("EditCopy: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.FinishTransaction(tname);
        }
      }

      public virtual void EditCut()
      {
        if (!this.CanCopyObjects() || !this.CanDeleteObjects())
          return;
        string tname = (string) null;
        try
        {
          this.StartTransaction();
          this.CopyToClipboard((IMapCollection) this.Selection);
          this.DeleteSelection(this.Selection);
          tname = "Cut";
        }
        catch (Exception ex)
        {
          MapObject.Trace("EditCut: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.FinishTransaction(tname);
        }
      }

      public virtual void EditDelete() => this.DeleteSelection(this.Selection);

      public virtual void EditEdit() => this.EditObject(this.Selection.Primary);

      public virtual void EditObject(MapObject obj)
      {
        if (obj == null || !this.CanEditObjects() || !obj.CanEdit())
          return;
        obj.DoBeginEdit(this);
      }

      public virtual void EditPaste()
      {
        if (!this.CanInsertObjects())
          return;
        MapDocument document = this.Document;
        string tname = (string) null;
        try
        {
          this.StartTransaction();
          MapCopyDictionary mapCopyDictionary = this.PasteFromClipboard();
          if (mapCopyDictionary != null)
          {
            bool flag = false;
            IDictionaryEnumerator enumerator = mapCopyDictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
              if (enumerator.Key is MapObject key && key.IsTopLevel && enumerator.Value is MapObject mapObject && mapObject.IsTopLevel && mapObject.Document == document)
              {
                if (!flag)
                {
                  flag = true;
                  this.Selection.Clear();
                }
                this.Selection.Add(mapObject);
              }
            }
          }
          tname = "Paste";
          this.RaiseClipboardPasted();
        }
        catch (Exception ex)
        {
          MapObject.Trace("EditPaste: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.FinishTransaction(tname);
        }
      }

      public virtual void EndUpdate()
      {
        if (this.mySuppressPaint <= 0)
          return;
        --this.mySuppressPaint;
        this.Update();
      }

      public virtual IMapTool FindMouseTool(System.Type tooltype)
      {
        IList mouseDownTools = this.MouseDownTools;
        for (int index = 0; index < mouseDownTools.Count; ++index)
        {
          if (mouseDownTools[index].GetType() == tooltype)
            return (IMapTool) mouseDownTools[index];
        }
        IList mouseMoveTools = this.MouseMoveTools;
        for (int index = 0; index < mouseMoveTools.Count; ++index)
        {
          if (mouseMoveTools[index].GetType() == tooltype)
            return (IMapTool) mouseMoveTools[index];
        }
        IList mouseUpTools = this.MouseUpTools;
        for (int index = 0; index < mouseUpTools.Count; ++index)
        {
          if (mouseUpTools[index].GetType() == tooltype)
            return (IMapTool) mouseUpTools[index];
        }
        return (IMapTool) null;
      }

      public virtual PointF FindNearestGridPoint(PointF p)
      {
        float x1 = p.X;
        float y1 = p.Y;
        float x2 = this.GridOrigin.X;
        float y2 = this.GridOrigin.Y;
        float width = this.GridCellSize.Width;
        float height = this.GridCellSize.Height;
        float num1 = (float) Math.Floor((double) (x1 - x2) / (double) width) * width + x2;
        float num2 = (float) Math.Floor((double) (y1 - y2) / (double) height) * height + y2;
        float num3 = (float) (((double) x1 - (double) num1) * ((double) x1 - (double) num1) + ((double) y1 - (double) num2) * ((double) y1 - (double) num2));
        float x3 = num1;
        float y3 = num2;
        float num4 = num1 + width;
        float num5 = num2;
        float num6 = (float) (((double) x1 - (double) num4) * ((double) x1 - (double) num4) + ((double) y1 - (double) num5) * ((double) y1 - (double) num5));
        if ((double) num6 < (double) num3)
        {
          num3 = num6;
          x3 = num4;
          y3 = num5;
        }
        float num7 = num1;
        float num8 = num2 + height;
        float num9 = (float) (((double) x1 - (double) num7) * ((double) x1 - (double) num7) + ((double) y1 - (double) num8) * ((double) y1 - (double) num8));
        if ((double) num9 < (double) num3)
        {
          num3 = num9;
          x3 = num7;
          y3 = num8;
        }
        float num10 = num4;
        float num11 = num8;
        if (((double) x1 - (double) num10) * ((double) x1 - (double) num10) + ((double) y1 - (double) num11) * ((double) y1 - (double) num11) < (double) num3)
        {
          x3 = num10;
          y3 = num11;
        }
        return new PointF(x3, y3);
      }

      public virtual bool FinishTransaction(string tname) => this.Document.FinishTransaction(tname);

      private void FollowExternalDragImage(PointF pt)
      {
        if (this.myExternalDragImage == null)
          return;
        this.myExternalDragImage.Location = pt;
      }

      internal void FreeTempPointArray(PointF[] a)
      {
        int length = a.Length;
        if (this.myTempArrays == null || length >= this.myTempArrays.Length)
          return;
        this.myTempArrays[length] = a;
      }

      public Bitmap GetBitmapFromCollection(IMapCollection coll)
      {
        RectangleF bounds = MapDocument.ComputeBounds(coll, this);
        return this.GetBitmapFromCollection(coll, bounds, true);
      }

      public virtual Bitmap GetBitmapFromCollection(IMapCollection coll, RectangleF bounds, bool paper)
      {
        int width = (int) Math.Ceiling((double) bounds.Width);
        int height = (int) Math.Ceiling((double) bounds.Height);
        if (width < 1)
          width = 1;
        if (height < 1)
          height = 1;
        Bitmap bitmapFromCollection = new Bitmap(width, height);
        Graphics g = Graphics.FromImage((Image) bitmapFromCollection);
        g.PageUnit = GraphicsUnit.Pixel;
        g.SmoothingMode = this.SmoothingMode;
        g.TextRenderingHint = this.TextRenderingHint;
        g.InterpolationMode = this.InterpolationMode;
        g.TranslateTransform(-bounds.X, -bounds.Y);
        PointF origin = this.myOrigin;
        float horizScale = this.myHorizScale;
        float vertScale = this.myVertScale;
        Size borderSize = this.myBorderSize;
        this.myOrigin = new PointF(bounds.X, bounds.Y);
        this.myHorizScale = 1f;
        this.myVertScale = 1f;
        this.myBorderSize = new Size(0, 0);
        try
        {
          if (paper)
          {
            RectangleF a = bounds;
            MapObject.InflateRect(ref a, 1f, 1f);
            this.PaintPaperColor(g, a);
          }
          foreach (MapObject mapObject in (IEnumerable) coll)
          {
            if (mapObject.CanView())
              mapObject.Paint(g, this);
          }
        }
        finally
        {
          this.myOrigin = origin;
          this.myHorizScale = horizScale;
          this.myVertScale = vertScale;
          this.myBorderSize = borderSize;
        }
        g.Dispose();
        return bitmapFromCollection;
      }

      protected virtual MapObject GetExternalDragImage(DragEventArgs evt)
      {
        if (!(evt.Data.GetData(typeof (MapSelection)) is MapSelection data))
          return (MapObject) null;
        MapObject primary = data.Primary;
        RectangleF bounds = MapDocument.ComputeBounds((IMapCollection) data, (MapView) null);
        Image bitmapFromCollection = (Image) this.GetBitmapFromCollection((IMapCollection) data, bounds, false);
        MapView.ExternalDragImage externalDragImage = new MapView.ExternalDragImage();
        externalDragImage.Image = bitmapFromCollection;
        SizeF sizeF = MapTool.SubtractPoints(primary.Position, bounds.Location);
        externalDragImage.Offset = new SizeF(sizeF.Width + data.HotSpot.Width, sizeF.Height + data.HotSpot.Height);
        return (MapObject) externalDragImage;
      }

      public virtual SolidBrush GetShadowBrush()
      {
        if (this.myShadowBrush == null || this.myShadowBrush.Color != this.ShadowColor)
        {
          if (this.myShadowBrush != null)
            this.myShadowBrush.Dispose();
          this.myShadowBrush = new SolidBrush(this.ShadowColor);
        }
        return this.myShadowBrush;
      }

      public virtual Pen GetShadowPen(float width)
      {
        if (this.myShadowPen == null || this.myShadowPen.Color != this.ShadowColor || (double) MapShape.GetPenWidth(this.myShadowPen) != (double) width)
        {
          if (this.myShadowPen != null)
            this.myShadowPen.Dispose();
          this.myShadowPen = new Pen(this.ShadowColor, width);
        }
        return this.myShadowPen;
      }

      public virtual void HandleScroll(object sender, ScrollEventArgs e)
      {
        if (e.Type == ScrollEventType.EndScroll)
          return;
        int newValue = e.NewValue;
        this.InitFocus();
        PointF docPosition = this.DocPosition;
        if (sender == this.VerticalScrollBar)
        {
          docPosition.Y = (float) newValue / this.myVertScale;
          this.DocPosition = docPosition;
        }
        else
        {
          if (sender != this.HorizontalScrollBar)
            return;
          docPosition.X = (float) newValue / this.myHorizScale;
          this.DocPosition = docPosition;
        }
      }

      private void HideExternalDragImage()
      {
        if (this.myExternalDragImage == null)
          return;
        this.myExternalDragImage.Remove();
        this.myExternalDragImage = (MapObject) null;
      }

      private void hoverCallback(object obj)
      {
        if (!this.IsHandleCreated)
          return;
        try
        {
          this.Invoke((Delegate) obj);
        }
        catch (ObjectDisposedException ex)
        {
        }
      }

      private void hoverTick(object sender, EventArgs e)
      {
        if (!this.myHoverTimerEnabled)
          return;
        MapInputEventArgs lastInput = this.LastInput;
        lastInput.ViewPoint = this.myHoverPoint;
        lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
        lastInput.Buttons = Control.MouseButtons;
        lastInput.Modifiers = Control.ModifierKeys;
        lastInput.Delta = 0;
        lastInput.Key = Keys.None;
        this.DoMouseHover();
      }

      private void init(MapDocument doc)
      {
        this.myDocChangedEventHandler = new MapChangedEventHandler(this.SafeOnDocumentChanged);
        this.myDocument = doc;
        this.myLayers = new MapLayerCollection();
        this.myLayers.init((IMapLayerCollectionContainer) this);
        if (this.myDocument == null)
          this.myDocument = this.CreateDocument();
        this.myDocument.Changed += this.myDocChangedEventHandler;
        this.InitializeLayersFromDocument();
        this.mySelection = this.CreateSelection();
        this.myDefaultTool = this.CreateDefaultTool();
        this.myTool = this.DefaultTool;
        this.myTool.Start();
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        this.myVertScroll = new VScrollBar();
        this.myHorizScroll = new HScrollBar();
        this.myCorner = new Control();
        this.myCorner.BackColor = SystemColors.Control;
        this.Controls.Add((Control) this.myVertScroll);
        this.Controls.Add((Control) this.myHorizScroll);
        this.Controls.Add(this.myCorner);
        this.myVertScroll.SmallChange = this.ScrollSmallChange.Height;
        this.myHorizScroll.SmallChange = this.ScrollSmallChange.Width;
        this.myToolTip = new ToolTip();
        this.myVertScrollHandler = new ScrollEventHandler(this.HandleScroll);
        this.myVertScroll.Scroll += this.myVertScrollHandler;
        this.myHorizScrollHandler = new ScrollEventHandler(this.HandleScroll);
        this.myHorizScroll.Scroll += this.myHorizScrollHandler;
        this.myVertScroll.RightToLeft = RightToLeft.No;
        this.myHorizScroll.RightToLeft = RightToLeft.No;
        this.InitAllowDrop(true);
        this.BackColor = Color.White;
      }

      internal bool InitAllowDrop(bool dnd)
      {
        try
        {
          this.InitAllowDrop2(dnd);
        }
        catch (SecurityException ex)
        {
          this.AllowDragOut = false;
          MapObject.Trace("MapView.init: " + ex.ToString());
          return false;
        }
        return true;
      }

      private void InitAllowDrop2(bool dnd) => this.AllowDrop = dnd;

      public void InitFocus()
      {
        try
        {
          this.InitFocus2();
        }
        catch (SecurityException ex)
        {
          MapObject.Trace("Focus: " + ex.ToString());
          this.OnGotFocus(EventArgs.Empty);
        }
      }

      private void InitFocus2() => this.Focus();

      public virtual void InitializeLayersFromDocument()
      {
        if (this.Layers == null)
          return;
        this.BeginUpdate();
        this.DocPosition = PointF.Empty;
        MapLayerCollectionEnumerator backwards = this.Layers.Backwards;
        while (backwards.MoveNext())
        {
          MapLayer current = backwards.Current;
          if (current.IsInView)
            current.Clear();
          else
            this.Layers.Remove(current);
        }
        MapLayer moving = this.Layers.Default;
        foreach (MapLayer layer in this.Document.Layers)
          this.Layers.InsertDocumentLayerAfter((MapLayer) null, layer);
        this.Layers.MoveAfter((MapLayer) null, moving);
        this.EndUpdate();
      }

      private void InternalOnDocumentChanged(object sender, EventArgs e)
      {
        if (this.myQueuedEvents == null)
          return;
        MapChangedEventArgs e1 = (MapChangedEventArgs) null;
        lock (this.myQueuedEvents)
          e1 = this.myQueuedEvents.Dequeue() as MapChangedEventArgs;
        if (e1 == null)
          return;
        this.OnDocumentChanged((object) e1.Document, e1);
      }

      public virtual bool IsInternalDragDrop(DragEventArgs evt)
      {
        return this.Tool is MapToolDragging && !this.myPretendInternalDrag;
      }

      public virtual void LayoutScrollBars(bool update)
      {
        if (this.myUpdatingScrollBars)
          return;
        Rectangle clientRectangle = this.ClientRectangle;
        int x = clientRectangle.Width - this.myBorderSize.Width;
        VScrollBar verticalScrollBar = this.VerticalScrollBar;
        if (verticalScrollBar != null && verticalScrollBar.Visible)
          x -= this.myScrollBarWidth;
        int y = clientRectangle.Height - this.myBorderSize.Height;
        HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
        if (horizontalScrollBar != null && horizontalScrollBar.Visible)
          y -= this.myScrollBarHeight;
        Control cornerControl = this.CornerControl;
        if (cornerControl != null)
        {
          if (verticalScrollBar != null && verticalScrollBar.Visible && horizontalScrollBar != null && horizontalScrollBar.Visible)
          {
            cornerControl.Bounds = new Rectangle(x, y, this.myScrollBarWidth, this.myScrollBarHeight);
            cornerControl.Visible = true;
          }
          else
            cornerControl.Visible = false;
        }
        Size scrollSmallChange;
        if (verticalScrollBar != null && verticalScrollBar.Visible)
        {
          verticalScrollBar.Bounds = new Rectangle(x, this.myBorderSize.Height, this.myScrollBarWidth, y - this.myBorderSize.Height);
          VScrollBar vscrollBar = verticalScrollBar;
          int height1 = this.ScrollSmallChange.Height;
          int height2 = verticalScrollBar.Height;
          scrollSmallChange = this.ScrollSmallChange;
          int height3 = scrollSmallChange.Height;
          int val2 = height2 - height3;
          int num = Math.Max(height1, val2);
          vscrollBar.LargeChange = num;
        }
        if (horizontalScrollBar != null && horizontalScrollBar.Visible)
        {
          horizontalScrollBar.Bounds = new Rectangle(this.myBorderSize.Width, y, x - this.myBorderSize.Width, this.myScrollBarHeight);
          HScrollBar hscrollBar = horizontalScrollBar;
          scrollSmallChange = this.ScrollSmallChange;
          int width1 = scrollSmallChange.Width;
          int width2 = horizontalScrollBar.Width;
          scrollSmallChange = this.ScrollSmallChange;
          int width3 = scrollSmallChange.Width;
          int val2 = width2 - width3;
          int num = Math.Max(width1, val2);
          hscrollBar.LargeChange = num;
        }
        if (!update)
          return;
        this.UpdateScrollBars();
      }

      public virtual PointF LimitDocPosition(PointF point)
      {
        PointF documentTopLeft = this.DocumentTopLeft;
        SizeF documentSize = this.DocumentSize;
        SizeF docExtentSize = this.DocExtentSize;
        float num1 = documentTopLeft.X + documentSize.Width - docExtentSize.Width;
        float num2 = documentTopLeft.Y + documentSize.Height - docExtentSize.Height;
        if ((double) num1 < (double) documentTopLeft.X)
          point.X = documentTopLeft.X;
        else if ((double) point.X > (double) num1 && (double) num1 > (double) documentTopLeft.X)
          point.X = num1;
        else if ((double) point.X < (double) documentTopLeft.X)
          point.X = documentTopLeft.X;
        if ((double) num2 < (double) documentTopLeft.Y)
        {
          point.Y = documentTopLeft.Y;
          return point;
        }
        if ((double) point.Y > (double) num2 && (double) num2 > (double) documentTopLeft.Y)
        {
          point.Y = num2;
          return point;
        }
        if ((double) point.Y < (double) documentTopLeft.Y)
          point.Y = documentTopLeft.Y;
        return point;
      }

      public virtual float LimitDocScale(float s) => s;

      public virtual bool MatchesNodeLabel(IMapLabeledNode node, char c)
      {
        if (node == null)
          return false;
        string text = node.Text;
        switch (text)
        {
          case null:
            return false;
          case "":
            return false;
          default:
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return (int) char.ToUpper(text[0], currentCulture) == (int) char.ToUpper(c, currentCulture);
        }
      }

      public virtual void MoveSelection(MapSelection sel, SizeF offset, bool grid)
      {
        if (sel == null)
          sel = this.Selection;
        if (sel == this.Selection && !this.CanMoveObjects() || sel.IsEmpty)
          return;
        string tname = (string) null;
        try
        {
          this.StartTransaction();
          MapObject mapObject = (MapObject) null;
          MapCollectionEnumerator enumerator1 = sel.GetEnumerator();
          while (enumerator1.MoveNext())
          {
            MapObject current = enumerator1.Current;
            if (!(current is IMapLink))
            {
              mapObject = current;
              break;
            }
          }
          SizeF sizeF = offset;
          if (mapObject != null)
          {
            PointF location = mapObject.Location;
            PointF p = new PointF(location.X + offset.Width, location.Y + offset.Height);
            if (grid)
              p = this.FindNearestGridPoint(p);
            sizeF.Width = p.X - location.X;
            sizeF.Height = p.Y - location.Y;
          }
          MapCollectionEnumerator enumerator2 = sel.GetEnumerator();
          while (enumerator2.MoveNext())
          {
            MapObject current = enumerator2.Current;
            if (current is IMapLink)
              current.DoMove(this, current.Position, new PointF(current.Left + sizeF.Width, current.Top + sizeF.Height));
          }
          MapCollectionEnumerator enumerator3 = sel.GetEnumerator();
          while (enumerator3.MoveNext())
          {
            MapObject current = enumerator3.Current;
            if (!(current is IMapLink))
            {
              PointF location = current.Location;
              PointF pointF = new PointF(location.X + offset.Width, location.Y + offset.Height);
              if (grid)
                pointF = this.FindNearestGridPoint(pointF);
              current.DoMove(this, location, pointF);
            }
          }
          tname = "Move Selection";
        }
        finally
        {
          this.FinishTransaction(tname);
        }
      }

      protected override void OnBackColorChanged(EventArgs evt)
      {
        base.OnBackColorChanged(evt);
        this.UpdateView();
      }

      protected virtual void OnBackgroundContextClicked(MapInputEventArgs evt)
      {
        if (this.BackgroundContextClicked == null)
          return;
        this.BackgroundContextClicked((object) this, evt);
      }

      protected virtual void OnBackgroundDoubleClicked(MapInputEventArgs evt)
      {
        if (this.BackgroundDoubleClicked == null)
          return;
        this.BackgroundDoubleClicked((object) this, evt);
      }

      protected virtual void OnBackgroundHover(MapInputEventArgs evt)
      {
        if (this.BackgroundHover == null)
          return;
        this.BackgroundHover((object) this, evt);
      }

      protected override void OnBackgroundImageChanged(EventArgs evt)
      {
        base.OnBackgroundImageChanged(evt);
        this.UpdateView();
      }

      protected virtual void OnBackgroundSingleClicked(MapInputEventArgs evt)
      {
        if (this.BackgroundSingleClicked == null)
          return;
        this.BackgroundSingleClicked((object) this, evt);
      }

      protected virtual void OnClipboardPasted(EventArgs evt)
      {
        if (this.ClipboardPasted == null)
          return;
        this.ClipboardPasted((object) this, evt);
      }

      protected override void OnCreateControl()
      {
        base.OnCreateControl();
        this.myUpdatingScrollBars = false;
        this.LayoutScrollBars(true);
      }

      protected virtual void OnDocumentChanged(object sender, MapChangedEventArgs e)
      {
        MapObject mapObject = e.MapObject;
        if (e.IsBeforeChanging)
        {
          if (e.Hint == 901 && mapObject != null)
          {
            RectangleF bounds = mapObject.Bounds;
            Rectangle view = this.ConvertDocToView(mapObject.ExpandPaintBounds(bounds, this));
            view.Inflate(2, 2);
            this.Invalidate(view);
          }
          this.RaiseDocumentChanged(sender, e);
        }
        else
        {
          int hint = e.Hint;
          if (hint <= 220)
          {
            switch (hint - 100)
            {
              case 0:
    label_8:
                this.Selection.AddAllSelectionHandles();
                this.UpdateView();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              case 1:
                this.BeginUpdate();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              case 2:
                this.EndUpdate();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              case 3:
                this.Update();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              default:
                switch (hint - 202)
                {
                  case 0:
                    this.UpdateScrollBars();
                    if (this.DocumentChanged == null)
                      return;
                    this.DocumentChanged(sender, e);
                    return;
                  case 1:
                    this.UpdateScrollBars();
                    if (this.DocumentChanged == null)
                      return;
                    this.DocumentChanged(sender, e);
                    return;
                  case 2:
                    if (this.DocumentChanged == null)
                      return;
                    this.DocumentChanged(sender, e);
                    return;
                  case 3:
                    goto label_8;
                  default:
                    if (hint != 220)
                    {
                      if (this.DocumentChanged == null)
                        return;
                      this.DocumentChanged(sender, e);
                      return;
                    }
                    goto label_8;
                }
            }
          }
          else if (hint <= 904)
          {
            switch (hint - 801)
            {
              case 0:
                MapLayer doclayer = (MapLayer) e.Object;
                MapLayer oldValue1 = (MapLayer) e.OldValue;
                if (e.SubHint != 1)
                {
                  this.Layers.InsertDocumentLayerBefore(oldValue1, doclayer);
                  this.Selection.AddAllSelectionHandles();
                  this.UpdateView();
                  if (this.DocumentChanged == null)
                    break;
                  this.DocumentChanged(sender, e);
                  break;
                }
                this.Layers.InsertDocumentLayerAfter(oldValue1, doclayer);
                this.Selection.AddAllSelectionHandles();
                this.UpdateView();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              case 1:
                this.Layers.Remove((MapLayer) e.Object);
                this.Selection.AddAllSelectionHandles();
                this.UpdateView();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              case 2:
                MapLayer moving = (MapLayer) e.Object;
                MapLayer oldValue2 = (MapLayer) e.OldValue;
                try
                {
                  if (e.SubHint == 1)
                    this.Layers.MoveAfter(oldValue2, moving);
                  else
                    this.Layers.MoveBefore(oldValue2, moving);
                }
                catch (ArgumentException ex)
                {
                }
                this.Selection.AddAllSelectionHandles();
                this.UpdateView();
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
              default:
                if (hint != 901)
                {
                  if ((uint) (hint - 902) <= 2U)
                  {
                    if (e.Hint == 903)
                      this.removeFromSelection(mapObject);
                    RectangleF bounds = mapObject.Bounds;
                    Rectangle view = this.ConvertDocToView(mapObject.ExpandPaintBounds(bounds, this));
                    view.Inflate(2, 2);
                    this.Invalidate(view);
                    if (this.DocumentChanged == null)
                      break;
                    this.DocumentChanged(sender, e);
                    break;
                  }
                  if (this.DocumentChanged == null)
                    break;
                  this.DocumentChanged(sender, e);
                  break;
                }
                RectangleF bounds1 = mapObject.Bounds;
                Rectangle view1 = this.ConvertDocToView(mapObject.ExpandPaintBounds(bounds1, this));
                view1.Inflate(2, 2);
                if (e.SubHint != 1001)
                {
                  if (e.SubHint == 1003)
                    this.updateSelectionHandles(mapObject);
                  else if (e.SubHint == 1052)
                    this.removeFromSelection(e.OldValue as MapObject);
                  this.Invalidate(view1);
                  if (this.DocumentChanged == null)
                    break;
                  this.DocumentChanged(sender, e);
                  break;
                }
                MapSelection selection = this.Selection;
                if (selection.GetHandleCount(mapObject) > 0)
                {
                  if (!mapObject.CanView())
                  {
                    mapObject.RemoveSelectionHandles(selection);
                  }
                  else
                  {
                    IMapHandle anExistingHandle = selection.GetAnExistingHandle(mapObject);
                    mapObject.AddSelectionHandles(selection, anExistingHandle.SelectedObject);
                  }
                }
                RectangleF oldRect = e.OldRect;
                Rectangle view2 = this.ConvertDocToView(mapObject.ExpandPaintBounds(oldRect, this));
                view2.Inflate(2, 2);
                this.Invalidate(view2);
                this.Invalidate(view1);
                if (this.DocumentChanged == null)
                  break;
                this.DocumentChanged(sender, e);
                break;
            }
          }
          else if (hint == 910)
          {
            this.Selection.AddAllSelectionHandles();
            this.UpdateView();
            if (this.DocumentChanged == null)
              return;
            this.DocumentChanged(sender, e);
          }
          else
          {
            if (this.DocumentChanged == null)
              return;
            this.DocumentChanged(sender, e);
          }
        }
      }

      protected void RaiseDocumentChanged(object sender, MapChangedEventArgs e)
      {
        MapChangedEventHandler documentChanged = this.DocumentChanged;
        if (documentChanged == null)
          return;
        documentChanged(sender, e);
      }

      protected override void OnDoubleClick(EventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowMouse)
        {
          MapInputEventArgs mapInputEventArgs = lastInput;
          int buttons = (int) lastInput.Buttons;
          Point viewPoint = lastInput.ViewPoint;
          int x = viewPoint.X;
          viewPoint = lastInput.ViewPoint;
          int y = viewPoint.Y;
          int delta = lastInput.Delta;
          MouseEventArgs mouseEventArgs = new MouseEventArgs((MouseButtons) buttons, 2, x, y, delta);
          mapInputEventArgs.MouseEventArgs = mouseEventArgs;
          lastInput.DoubleClick = true;
          this.DoMouseUp();
        }
        base.OnDoubleClick(evt);
        lastInput.DoubleClick = false;
        lastInput.MouseEventArgs = (MouseEventArgs) null;
      }

      protected override void OnDragDrop(DragEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowMouse)
        {
          try
          {
            Point p = new Point(evt.X, evt.Y);
            lastInput.ViewPoint = this.PointToClient(p);
            lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
            lastInput.Buttons = Control.MouseButtons;
            lastInput.Modifiers = Control.ModifierKeys;
            lastInput.Delta = 0;
            lastInput.Key = Keys.None;
            lastInput.DragEventArgs = evt;
            if (this.IsInternalDragDrop(evt))
            {
              this.DoInternalDrop(evt);
            }
            else
            {
              this.HideExternalDragImage();
              if (this.myPretendInternalDrag)
                this.DoMouseUp();
              else
                this.DoExternalDrop(evt);
            }
            this.myPretendInternalDrag = false;
          }
          catch (Exception ex)
          {
            MapObject.Trace("OnDragDrop: " + ex.ToString());
            throw ex;
          }
        }
        base.OnDragDrop(evt);
        lastInput.DragEventArgs = (DragEventArgs) null;
      }

      protected override void OnDragEnter(DragEventArgs evt)
      {
        if (this.ExternalDragDropsOnEnter && !this.IsInternalDragDrop(evt) && this.CanInsertObjects())
        {
          IMapCollection mapCollection = this.DoExternalDrop(evt);
          if (mapCollection != null && !mapCollection.IsEmpty)
          {
            this.myPretendInternalDrag = true;
            evt.Effect = DragDropEffects.All;
            MapToolDragging mapToolDragging = (MapToolDragging) null;
            IList mouseMoveTools = this.MouseMoveTools;
            for (int index = 0; index < mouseMoveTools.Count; ++index)
            {
              if (mouseMoveTools[index] is MapToolDragging)
              {
                mapToolDragging = (MapToolDragging) mouseMoveTools[index];
                break;
              }
            }
            if (mapToolDragging == null)
              mapToolDragging = new MapToolDragging(this);
            MapInputEventArgs firstInput = this.FirstInput;
            Point p = new Point(evt.X, evt.Y);
            firstInput.ViewPoint = this.PointToClient(p);
            firstInput.DocPoint = this.ConvertViewToDoc(firstInput.ViewPoint);
            firstInput.Buttons = Control.MouseButtons;
            firstInput.Modifiers = Control.ModifierKeys;
            firstInput.Delta = 0;
            firstInput.Key = Keys.None;
            firstInput.DragEventArgs = evt;
            this.LastInput.ViewPoint = firstInput.ViewPoint;
            this.LastInput.DocPoint = firstInput.DocPoint;
            this.LastInput.Buttons = firstInput.Buttons;
            this.LastInput.Modifiers = firstInput.Modifiers;
            this.LastInput.Delta = firstInput.Delta;
            this.LastInput.Key = firstInput.Key;
            this.LastInput.DragEventArgs = firstInput.DragEventArgs;
            mapToolDragging.CurrentObject = this.Selection.Primary;
            mapToolDragging.MoveOffset = this.Selection.HotSpot;
            mapToolDragging.mySelectionSet = true;
            this.Tool = (IMapTool) mapToolDragging;
            base.OnDragEnter(evt);
            return;
          }
        }
        if (!this.IsInternalDragDrop(evt))
        {
          MapObject externalDragImage = this.GetExternalDragImage(evt);
          if (externalDragImage != null)
          {
            this.ShowExternalDragImage(externalDragImage);
            this.FollowExternalDragImage(this.LastInput.DocPoint);
          }
        }
        base.OnDragEnter(evt);
      }

      protected override void OnDragLeave(EventArgs e)
      {
        this.StopHoverTimer();
        this.StopAutoScroll();
        if (this.myPretendInternalDrag)
        {
          this.myPretendInternalDrag = false;
          this.DeleteSelection(this.Selection);
          this.Tool = (IMapTool) null;
        }
        else if (this.IsInternalDragDrop((DragEventArgs) null))
        {
          if (this.Tool is MapToolDragging tool)
            tool.ClearDragSelection();
        }
        else
          this.HideExternalDragImage();
        base.OnDragLeave(e);
      }

      protected override void OnDragOver(DragEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowMouse)
        {
          try
          {
            Point p = new Point(evt.X, evt.Y);
            lastInput.ViewPoint = this.PointToClient(p);
            lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
            lastInput.Buttons = Control.MouseButtons;
            lastInput.Modifiers = Control.ModifierKeys;
            lastInput.Delta = 0;
            lastInput.Key = Keys.None;
            lastInput.DragEventArgs = evt;
            if (this.IsInternalDragDrop(evt))
            {
              this.DoInternalDrag(evt);
            }
            else
            {
              if (this.myPretendInternalDrag)
                this.DoMouseMove();
              this.DoExternalDrag(evt);
            }
          }
          catch (Exception ex)
          {
            MapObject.Trace("OnDragOver: " + ex.ToString());
            throw ex;
          }
        }
        base.OnDragOver(evt);
        lastInput.DragEventArgs = (DragEventArgs) null;
      }

      protected virtual void OnExternalObjectsDropped(MapInputEventArgs evt)
      {
        if (this.ExternalObjectsDropped == null)
          return;
        this.ExternalObjectsDropped((object) this, evt);
      }

      protected override void OnGotFocus(EventArgs evt)
      {
        base.OnGotFocus(evt);
        this.CleanUpModalControl();
        if (this.Selection == null)
          return;
        this.Selection.OnGotFocus();
      }

      protected override void OnKeyDown(KeyEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowKey)
        {
          lastInput.Buttons = MouseButtons.None;
          lastInput.Modifiers = evt.Modifiers;
          lastInput.Delta = 0;
          lastInput.Key = evt.KeyCode;
          lastInput.KeyEventArgs = evt;
          this.DoKeyDown();
        }
        base.OnKeyDown(evt);
        lastInput.KeyEventArgs = (KeyEventArgs) null;
      }

      protected virtual void OnLinkCreated(MapSelectionEventArgs evt)
      {
        if (this.LinkCreated == null)
          return;
        this.LinkCreated((object) this, evt);
      }

      protected virtual void OnLinkRelinked(MapSelectionEventArgs evt)
      {
        if (this.LinkRelinked == null)
          return;
        this.LinkRelinked((object) this, evt);
      }

      protected override void OnLostFocus(EventArgs evt)
      {
        base.OnLostFocus(evt);
        if (this.Selection == null)
          return;
        this.Selection.OnLostFocus();
      }

      protected override void OnMouseDown(MouseEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowMouse)
        {
          lastInput.ViewPoint = new Point(evt.X, evt.Y);
          lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
          lastInput.Buttons = evt.Button;
          lastInput.Modifiers = Control.ModifierKeys;
          lastInput.Delta = evt.Delta;
          lastInput.Key = Keys.None;
          lastInput.MouseEventArgs = evt;
          this.FirstInput.ViewPoint = lastInput.ViewPoint;
          this.FirstInput.DocPoint = lastInput.DocPoint;
          this.FirstInput.Buttons = lastInput.Buttons;
          this.FirstInput.Modifiers = lastInput.Modifiers;
          this.FirstInput.Delta = lastInput.Delta;
          this.FirstInput.Key = lastInput.Key;
          this.FirstInput.MouseEventArgs = evt;
          this.DoMouseDown();
        }
        base.OnMouseDown(evt);
        lastInput.MouseEventArgs = (MouseEventArgs) null;
        this.FirstInput.MouseEventArgs = (MouseEventArgs) null;
      }

      protected override void OnMouseLeave(EventArgs e)
      {
        this.StopHoverTimer();
        this.StopAutoScroll();
        base.OnMouseLeave(e);
      }

      protected override void OnMouseMove(MouseEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        Point point = new Point(evt.X, evt.Y);
        if (this.AllowMouse)
        {
          lastInput.ViewPoint = point;
          lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
          lastInput.Buttons = evt.Button;
          lastInput.Modifiers = Control.ModifierKeys;
          lastInput.Delta = evt.Delta;
          lastInput.Key = Keys.None;
          lastInput.MouseEventArgs = evt;
          this.DoMouseMove();
        }
        base.OnMouseMove(evt);
        lastInput.MouseEventArgs = (MouseEventArgs) null;
      }

      protected override void OnMouseUp(MouseEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowMouse)
        {
          lastInput.ViewPoint = new Point(evt.X, evt.Y);
          lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
          lastInput.Buttons = evt.Button;
          lastInput.Modifiers = Control.ModifierKeys;
          lastInput.Delta = evt.Delta;
          lastInput.Key = Keys.None;
          lastInput.MouseEventArgs = evt;
          this.DoMouseUp();
        }
        base.OnMouseUp(evt);
        lastInput.MouseEventArgs = (MouseEventArgs) null;
      }

      protected override void OnMouseWheel(MouseEventArgs evt)
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (this.AllowMouse)
        {
          lastInput.ViewPoint = new Point(evt.X, evt.Y);
          lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
          lastInput.Buttons = evt.Button;
          lastInput.Modifiers = Control.ModifierKeys;
          lastInput.Delta = evt.Delta;
          lastInput.Key = Keys.None;
          lastInput.MouseEventArgs = evt;
          this.DoMouseWheel();
        }
        base.OnMouseWheel(evt);
        lastInput.MouseEventArgs = (MouseEventArgs) null;
      }

      protected virtual void OnObjectContextClicked(MapObjectEventArgs evt)
      {
        MapObjectEventHandler objectContextClicked = this.ObjectContextClicked;
        if (objectContextClicked == null)
          return;
        objectContextClicked((object) this, evt);
      }

      protected virtual void OnObjectDoubleClicked(MapObjectEventArgs evt)
      {
        MapObjectEventHandler objectDoubleClicked = this.ObjectDoubleClicked;
        if (objectDoubleClicked == null)
          return;
        objectDoubleClicked((object) this, evt);
      }

      protected virtual void OnObjectEdited(MapSelectionEventArgs evt)
      {
        MapSelectionEventHandler objectEdited = this.ObjectEdited;
        if (objectEdited == null)
          return;
        objectEdited((object) this, evt);
      }

      protected virtual void OnObjectGotSelection(MapSelectionEventArgs evt)
      {
        MapSelectionEventHandler objectGotSelection = this.ObjectGotSelection;
        if (objectGotSelection == null)
          return;
        objectGotSelection((object) this, evt);
      }

      protected virtual void OnObjectHover(MapObjectEventArgs evt)
      {
        MapObjectEventHandler objectHover = this.ObjectHover;
        if (objectHover == null)
          return;
        objectHover((object) this, evt);
      }

      protected virtual void OnObjectLostSelection(MapSelectionEventArgs evt)
      {
        MapSelectionEventHandler objectLostSelection = this.ObjectLostSelection;
        if (objectLostSelection == null)
          return;
        objectLostSelection((object) this, evt);
      }

      protected virtual void OnObjectResized(MapSelectionEventArgs evt)
      {
        MapSelectionEventHandler objectResized = this.ObjectResized;
        if (objectResized == null)
          return;
        objectResized((object) this, evt);
      }

      protected virtual void OnObjectSingleClicked(MapObjectEventArgs evt)
      {
        MapObjectEventHandler objectSingleClicked = this.ObjectSingleClicked;
        if (objectSingleClicked == null)
          return;
        objectSingleClicked((object) this, evt);
      }

      protected override void OnPaint(PaintEventArgs evt)
      {
        try
        {
          this.onPaintCanvas(evt);
          this.UpdateMapControlBounds();
        }
        catch (Exception ex)
        {
          MapObject.Trace("OnPaint: " + ex.ToString());
          throw ex;
        }
        base.OnPaint(evt);
      }

      protected virtual void UpdateMapControlBounds()
      {
        if (this.myMapControls == null || this.myMapControls.Count <= 0)
          return;
        Rectangle displayRectangle = this.DisplayRectangle;
        foreach (MapControl mapControl in this.myMapControls)
        {
          Control control = mapControl.FindControl(this);
          if (control != null)
          {
            Rectangle view = this.ConvertDocToView(mapControl.Bounds);
            if (!displayRectangle.IntersectsWith(view))
              control.Bounds = view;
          }
        }
      }

      protected virtual void SetTransformPageUnit(Graphics graphics)
      {
        graphics.TranslateTransform((float) this.myBorderSize.Width, (float) this.myBorderSize.Height);
        graphics.ScaleTransform(this.myHorizScale, this.myVertScale);
        PointF docPosition = this.DocPosition;
        graphics.TranslateTransform(-docPosition.X, -docPosition.Y);
      }

      protected virtual void onPaintCanvas(PaintEventArgs evt)
      {
        if (this.mySuppressPaint > 0)
          return;
        this.myPaintEventArgs = evt;
        Graphics graphics1 = evt.Graphics;
        this.myGraphics = graphics1;
        graphics1.PageUnit = GraphicsUnit.Pixel;
        Rectangle clipRectangle = evt.ClipRectangle;
        if (clipRectangle.Width <= 0 || clipRectangle.Height <= 0)
          return;
        Rectangle clientRectangle = this.ClientRectangle;
        if (this.myBuffer == null || this.myBuffer.Width < clientRectangle.Width || this.myBuffer.Height < clientRectangle.Height)
        {
          if (this.myBuffer != null)
            this.myBuffer.Dispose();
          this.myBuffer = new Bitmap(clientRectangle.Width + 1, clientRectangle.Height + 1, graphics1);
        }
        GraphicsState gstate = graphics1.Save();
        if (this.UseBuffer)
        {
          using (Graphics graphics2 = Graphics.FromImage((Image) this.myBuffer))
          {
            graphics2.PageUnit = GraphicsUnit.Pixel;
            this.PaintBorder(graphics2, clientRectangle, clipRectangle);
            Rectangle rectangle = Rectangle.Intersect(clipRectangle, this.DisplayRectangle);
            graphics2.IntersectClip(rectangle);
            RectangleF doc = this.ConvertViewToDoc(rectangle);
            this.SetTransformPageUnit(graphics2);
            this.PaintView(graphics2, doc);
            graphics1.DrawImage((Image) this.myBuffer, this.myPaintEventArgs.ClipRectangle, this.myPaintEventArgs.ClipRectangle, GraphicsUnit.Pixel);
          }
        }
        else
        {
          Graphics graphics3 = graphics1;
          graphics3.PageUnit = GraphicsUnit.Pixel;
          this.PaintBorder(graphics3, clientRectangle, clipRectangle);
          Rectangle rectangle = Rectangle.Intersect(clipRectangle, this.DisplayRectangle);
          graphics3.IntersectClip(rectangle);
          RectangleF doc = this.ConvertViewToDoc(rectangle);
          this.SetTransformPageUnit(graphics3);
          this.PaintView(graphics3, doc);
        }
        graphics1.Restore(gstate);
        this.myGraphics = graphics1;
      }

      protected virtual void OnPropertyChanged(PropertyChangedEventArgs evt)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged != null)
          propertyChanged((object) this, evt);
        if (evt.PropertyName != "DocScale")
          this.Selection?.AddAllSelectionHandles();
        if (!(evt.PropertyName != "Tool"))
          return;
        this.UpdateView();
      }

      protected override void OnQueryContinueDrag(QueryContinueDragEventArgs evt)
      {
        if (this.AllowMouse)
        {
          try
          {
            if (evt.EscapePressed)
            {
              if (this.myPretendInternalDrag)
              {
                this.myPretendInternalDrag = false;
                this.DeleteSelection(this.Selection);
              }
              this.DoCancelMouse();
            }
          }
          catch (Exception ex)
          {
            MapObject.Trace("OnQueryContinueDrag: " + ex.ToString());
            throw ex;
          }
        }
        base.OnQueryContinueDrag(evt);
      }

      protected virtual void OnSelectionCopied(EventArgs evt)
      {
        EventHandler selectionCopied = this.SelectionCopied;
        if (selectionCopied == null)
          return;
        selectionCopied((object) this, evt);
      }

      protected virtual void OnSelectionDeleted(EventArgs evt)
      {
        EventHandler selectionDeleted = this.SelectionDeleted;
        if (selectionDeleted == null)
          return;
        selectionDeleted((object) this, evt);
      }

      protected virtual void OnSelectionDeleting(CancelEventArgs evt)
      {
        CancelEventHandler selectionDeleting = this.SelectionDeleting;
        if (selectionDeleting == null)
          return;
        selectionDeleting((object) this, evt);
      }

      protected virtual void OnSelectionMoved(EventArgs evt)
      {
        EventHandler selectionMoved = this.SelectionMoved;
        if (selectionMoved == null)
          return;
        selectionMoved((object) this, evt);
      }

      protected override void OnSizeChanged(EventArgs evt)
      {
        base.OnSizeChanged(evt);
        this.LayoutScrollBars(false);
        this.UpdateView();
      }

      protected override void OnStyleChanged(EventArgs evt)
      {
        base.OnStyleChanged(evt);
        this.UpdateView();
      }

      protected override void OnSystemColorsChanged(EventArgs evt)
      {
        base.OnSystemColorsChanged(evt);
        this.UpdateView();
      }

      protected override void OnVisibleChanged(EventArgs evt)
      {
        base.OnVisibleChanged(evt);
        if (!this.Visible)
          return;
        this.LayoutScrollBars(false);
        this.UpdateView();
      }

      protected virtual void PaintBackgroundDecoration(Graphics g, RectangleF clipRect)
      {
        Image backgroundImage = this.BackgroundImage;
        if (backgroundImage != null)
        {
          RectangleF rectangleF = clipRect;
          rectangleF.Width = Math.Min(rectangleF.Width, (float) short.MaxValue);
          rectangleF.Height = Math.Min(rectangleF.Height, (float) short.MaxValue);
          g.DrawImage(backgroundImage, rectangleF, rectangleF, GraphicsUnit.Pixel);
        }
        switch (this.GridStyle)
        {
          case MapViewGridStyle.Dot:
            this.DrawGridCrosses(g, new SizeF(1f, 1f), clipRect);
            break;
          case MapViewGridStyle.Cross:
            this.DrawGridCrosses(g, new SizeF(6f, 6f), clipRect);
            break;
          case MapViewGridStyle.Line:
            this.DrawGridLines(g, clipRect);
            break;
        }
      }

      protected void PaintBorder(Graphics g, Rectangle rect, Rectangle clipRect)
      {
        switch (this.BorderStyle)
        {
          case BorderStyle.None:
            break;
          case BorderStyle.FixedSingle:
            if (clipRect.X > rect.X + this.myBorderSize.Width && clipRect.Y > rect.Y + this.myBorderSize.Height && clipRect.X + clipRect.Width < rect.X + rect.Width - this.myBorderSize.Width && clipRect.Y + clipRect.Height < rect.Y + rect.Height - this.myBorderSize.Height)
              break;
            g.DrawRectangle(MapShape.SystemPens_WindowFrame, rect);
            break;
          default:
            if (clipRect.X > rect.X + this.myBorderSize.Width && clipRect.Y > rect.Y + this.myBorderSize.Height && clipRect.X + clipRect.Width < rect.X + rect.Width - this.myBorderSize.Width && clipRect.Y + clipRect.Height < rect.Y + rect.Height - this.myBorderSize.Height)
              break;
            ControlPaint.DrawBorder3D(g, rect, this.Border3DStyle);
            break;
        }
      }

      protected virtual void PaintObjects(bool doc, bool view, Graphics g, RectangleF clipRect)
      {
        foreach (MapLayer layer in this.Layers)
        {
          if (doc && layer.IsInDocument || view && layer.IsInView)
            layer.Paint(g, this, clipRect);
        }
      }

      protected virtual void PaintPaperColor(Graphics g, RectangleF clipRect)
      {
        Color color = this.Document.PaperColor;
        if (color == Color.Empty)
          color = this.BackColor;
        if (this.myBackgroundBrush == null || this.myBackgroundBrush.Color != color)
        {
          this.myBackgroundBrush?.Dispose();
          this.myBackgroundBrush = new SolidBrush(color);
        }
        g.FillRectangle((Brush) this.myBackgroundBrush, clipRect);
      }

      protected virtual void PaintView(Graphics g, RectangleF clipRect)
      {
        this.PaintPaperColor(g, clipRect);
        this.PaintBackgroundDecoration(g, clipRect);
        g.SmoothingMode = this.SmoothingMode;
        g.TextRenderingHint = this.TextRenderingHint;
        g.InterpolationMode = this.InterpolationMode;
        this.PaintObjects(true, true, g, clipRect);
      }

      [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Clipboard=\"AllClipboard\"/>\r\n</PermissionSet>\r\n")]
      public virtual MapCopyDictionary PasteFromClipboard()
      {
        MapDocument document = this.Document;
        if (document != null)
        {
          IDataObject dataObject = Clipboard.GetDataObject();
          if (dataObject == null)
            return (MapCopyDictionary) null;
          object data = dataObject.GetData(document.DataFormat);
          if (data != null && data is MapDocument)
          {
            MapDocument coll = (MapDocument) data;
            return document.CopyFromCollection((IMapCollection) coll, false, false, new SizeF(1f, 1f), (MapCopyDictionary) null);
          }
        }
        return (MapCopyDictionary) null;
      }

      public virtual MapObject PickObject(bool doc, bool view, PointF p, bool selectableOnly)
      {
        if (!selectableOnly || this.CanSelectObjects())
        {
          foreach (MapLayer backward in this.Layers.Backwards)
          {
            if (doc && backward.IsInDocument || view && backward.IsInView)
            {
              MapObject mapObject = backward.PickObject(p, selectableOnly);
              if (mapObject != null)
                return mapObject;
            }
          }
        }
        return (MapObject) null;
      }

      public virtual IMapCollection PickObjects(
        bool doc,
        bool view,
        PointF p,
        bool selectableOnly,
        IMapCollection coll,
        int max)
      {
        if (selectableOnly && !this.CanSelectObjects())
          return (IMapCollection) null;
        if (coll == null)
          coll = (IMapCollection) new MapCollection();
        foreach (MapLayer backward in this.Layers.Backwards)
        {
          if (coll.Count >= max)
            return coll;
          if (doc && backward.IsInDocument || view && backward.IsInView)
            backward.PickObjects(p, selectableOnly, coll, max);
        }
        return coll;
      }

      public virtual void Print()
      {
        try
        {
          PrintDocument pd = new PrintDocument();
          pd.PrintPage += new PrintPageEventHandler(this.PrintDocumentPage);
          pd.DocumentName = this.Document.Name;
          if (this.PrintShowDialog(pd) == DialogResult.Cancel)
            return;
          pd.Print();
        }
        catch (Exception ex)
        {
          MapObject.Trace("Print: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.myPrintInfo = (MapView.PrintInfo) null;
        }
      }

      protected virtual void PrintDecoration(
        Graphics g,
        PrintPageEventArgs e,
        int hpnum,
        int hpmax,
        int vpnum,
        int vpmax)
      {
        float x = (float) e.MarginBounds.X;
        float y = (float) e.MarginBounds.Y;
        float width = (float) e.MarginBounds.Width;
        float height = (float) e.MarginBounds.Height;
        float num1 = x + width;
        float num2 = y + height;
        g.DrawLine(MapShape.Pens_Black, x, y, x + 10f, y);
        g.DrawLine(MapShape.Pens_Black, x, y, x, y + 10f);
        g.DrawLine(MapShape.Pens_Black, num1, y, num1 - 10f, y);
        g.DrawLine(MapShape.Pens_Black, num1, y, num1, y + 10f);
        g.DrawLine(MapShape.Pens_Black, x, num2, x + 10f, num2);
        g.DrawLine(MapShape.Pens_Black, x, num2, x, num2 - 10f);
        g.DrawLine(MapShape.Pens_Black, num1, num2, num1 - 10f, num2);
        g.DrawLine(MapShape.Pens_Black, num1, num2, num1, num2 - 10f);
      }

      protected virtual void PrintDocumentPage(object sender, PrintPageEventArgs e)
      {
        Graphics graphics = e.Graphics;
        if (this.myPrintInfo == null)
        {
          this.myPrintInfo = new MapView.PrintInfo();
          this.myPrintInfo.DocRect = new RectangleF(this.PrintDocumentTopLeft, this.PrintDocumentSize);
          this.myPrintInfo.HorizScale = this.PrintScale;
          this.myPrintInfo.VertScale = this.myPrintInfo.HorizScale;
          Rectangle marginBounds = e.MarginBounds;
          this.myPrintInfo.PrintSize = new SizeF((float) marginBounds.Width / this.myPrintInfo.HorizScale, (float) marginBounds.Height / this.myPrintInfo.VertScale);
          if ((double) this.myPrintInfo.PrintSize.Width > 0.0 && (double) this.myPrintInfo.PrintSize.Height > 0.0)
          {
            this.myPrintInfo.NumPagesAcross = (int) Math.Ceiling((double) this.myPrintInfo.DocRect.Width / (double) this.myPrintInfo.PrintSize.Width);
            this.myPrintInfo.NumPagesDown = (int) Math.Ceiling((double) this.myPrintInfo.DocRect.Height / (double) this.myPrintInfo.PrintSize.Height);
            switch (e.PageSettings.PrinterSettings.PrintRange)
            {
              case PrintRange.Selection:
                this.myPrintInfo.CurPage = 0;
                break;
              case PrintRange.SomePages:
                this.myPrintInfo.CurPage = e.PageSettings.PrinterSettings.FromPage;
                break;
              default:
                this.myPrintInfo.CurPage = 0;
                break;
            }
          }
        }
        if (this.myPrintInfo.NumPagesAcross <= 0 || this.myPrintInfo.NumPagesDown <= 0)
          return;
        int hpnum = this.myPrintInfo.CurPage % this.myPrintInfo.NumPagesAcross;
        int vpnum = this.myPrintInfo.CurPage / this.myPrintInfo.NumPagesAcross;
        PointF origin = this.myOrigin;
        float horizScale = this.myHorizScale;
        float vertScale = this.myVertScale;
        Size borderSize = this.myBorderSize;
        this.myOrigin = new PointF(this.myPrintInfo.DocRect.X + (float) hpnum * this.myPrintInfo.PrintSize.Width, this.myPrintInfo.DocRect.Y + (float) vpnum * this.myPrintInfo.PrintSize.Height);
        this.myHorizScale = this.myPrintInfo.HorizScale;
        this.myVertScale = this.myPrintInfo.VertScale;
        this.myBorderSize = new Size(e.MarginBounds.X, e.MarginBounds.Y);
        RectangleF clipRect = new RectangleF(this.myOrigin.X, this.myOrigin.Y, Math.Min(this.myPrintInfo.PrintSize.Width, this.myPrintInfo.DocRect.Width), Math.Min(this.myPrintInfo.PrintSize.Height, this.myPrintInfo.DocRect.Height));
        try
        {
          this.PrintDecoration(graphics, e, hpnum, this.myPrintInfo.NumPagesAcross, vpnum, this.myPrintInfo.NumPagesDown);
          graphics.IntersectClip(e.MarginBounds);
          graphics.TranslateTransform((float) this.myBorderSize.Width, (float) this.myBorderSize.Height);
          graphics.ScaleTransform(this.myHorizScale, this.myVertScale);
          graphics.TranslateTransform(-this.myOrigin.X, -this.myOrigin.Y);
          this.PrintView(graphics, clipRect);
        }
        finally
        {
          this.myOrigin = origin;
          this.myHorizScale = horizScale;
          this.myVertScale = vertScale;
          this.myBorderSize = borderSize;
        }
        int num;
        switch (e.PageSettings.PrinterSettings.PrintRange)
        {
          case PrintRange.Selection:
            num = this.myPrintInfo.NumPagesAcross * this.myPrintInfo.NumPagesDown - 1;
            break;
          case PrintRange.SomePages:
            num = e.PageSettings.PrinterSettings.ToPage;
            break;
          default:
            num = this.myPrintInfo.NumPagesAcross * this.myPrintInfo.NumPagesDown - 1;
            break;
        }
        e.HasMorePages = this.myPrintInfo.CurPage < num;
        if (e.HasMorePages)
          ++this.myPrintInfo.CurPage;
        else
          this.myPrintInfo = (MapView.PrintInfo) null;
      }

      public virtual void PrintPreview()
      {
        try
        {
          PrintDocument pd = new PrintDocument();
          pd.PrintPage += new PrintPageEventHandler(this.PrintDocumentPage);
          pd.DocumentName = this.Document.Name;
          this.PrintPreviewShowDialog(pd);
        }
        catch (Exception ex)
        {
          MapObject.Trace("PrintPreview: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.myPrintInfo = (MapView.PrintInfo) null;
        }
      }

      protected virtual void PrintPreviewShowDialog(PrintDocument pd)
      {
        int num = (int) new PrintPreviewDialog()
        {
          UseAntiAlias = true,
          Document = pd
        }.ShowDialog();
      }

      protected virtual DialogResult PrintShowDialog(PrintDocument pd)
      {
        return new PrintDialog()
        {
          AllowSomePages = true,
          Document = pd
        }.ShowDialog();
      }

      protected virtual void PrintView(Graphics g, RectangleF clipRect)
      {
        this.PaintBackgroundDecoration(g, clipRect);
        g.SmoothingMode = this.SmoothingMode;
        g.TextRenderingHint = this.TextRenderingHint;
        g.InterpolationMode = this.InterpolationMode;
        this.PaintObjects(true, false, g, clipRect);
      }

      protected override bool ProcessCmdKey(ref Message m, Keys keyData)
      {
        Control control = this.EditControl?.GetControl(this);
        return (control == null || !control.Focused) && base.ProcessCmdKey(ref m, keyData);
      }

      protected override bool ProcessDialogKey(Keys key)
      {
        Control control = this.EditControl?.GetControl(this);
        return (control == null || !control.Focused) && base.ProcessDialogKey(key);
      }

      public void RaiseBackgroundContextClicked(MapInputEventArgs evt)
      {
        this.OnBackgroundContextClicked(evt);
      }

      public void RaiseBackgroundDoubleClicked(MapInputEventArgs evt)
      {
        this.OnBackgroundDoubleClicked(evt);
      }

      public void RaiseBackgroundHover(MapInputEventArgs evt) => this.OnBackgroundHover(evt);

      public void RaiseBackgroundSingleClicked(MapInputEventArgs evt)
      {
        this.OnBackgroundSingleClicked(evt);
      }

      public virtual void RaiseChanged(
        int hint,
        int subhint,
        object x,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        int num = hint;
        if (num <= 904)
        {
          if ((uint) (num - 801) > 2U)
          {
            switch (num - 901)
            {
              case 0:
              case 3:
                if (!(x is MapObject mapObject1))
                  break;
                RectangleF bounds1 = mapObject1.Bounds;
                Rectangle view1 = this.ConvertDocToView(mapObject1.ExpandPaintBounds(bounds1, this));
                view1.Inflate(2, 2);
                if (hint == 901 && subhint == 1001)
                {
                  oldRect = mapObject1.ExpandPaintBounds(oldRect, this);
                  Rectangle view2 = this.ConvertDocToView(oldRect);
                  view2.Inflate(2, 2);
                  if (view1.IntersectsWith(view2))
                  {
                    this.Invalidate(Rectangle.Union(view1, view2));
                    break;
                  }
                  this.Invalidate(view1);
                  this.Invalidate(view2);
                  break;
                }
                this.Invalidate(view1);
                break;
              case 1:
                if (!(x is MapObject mapObject2))
                  break;
                RectangleF bounds2 = mapObject2.Bounds;
                Rectangle view3 = this.ConvertDocToView(mapObject2.ExpandPaintBounds(bounds2, this));
                view3.Inflate(2, 2);
                this.Invalidate(view3);
                break;
              case 2:
                if (!(x is MapObject mapObject3))
                  break;
                RectangleF bounds3 = mapObject3.Bounds;
                Rectangle view4 = this.ConvertDocToView(mapObject3.ExpandPaintBounds(bounds3, this));
                view4.Inflate(2, 2);
                this.Invalidate(view4);
                break;
            }
          }
          else
            this.UpdateView();
        }
        else if (num == 910)
          this.UpdateView();
      }

      public void RaiseClipboardPasted() => this.OnClipboardPasted(EventArgs.Empty);

      public void RaiseExternalObjectsDropped(MapInputEventArgs evt)
      {
        this.OnExternalObjectsDropped(evt);
      }

      public void RaiseLinkCreated(MapObject obj) => this.OnLinkCreated(new MapSelectionEventArgs(obj));

      public void RaiseLinkRelinked(MapObject obj)
      {
        this.OnLinkRelinked(new MapSelectionEventArgs(obj));
      }

      public void RaiseObjectContextClicked(MapObject obj, MapInputEventArgs evt)
      {
        this.OnObjectContextClicked(new MapObjectEventArgs(obj, evt));
      }

      public void RaiseObjectDoubleClicked(MapObject obj, MapInputEventArgs evt)
      {
        this.OnObjectDoubleClicked(new MapObjectEventArgs(obj, evt));
      }

      public void RaiseObjectEdited(MapObject obj)
      {
        this.OnObjectEdited(new MapSelectionEventArgs(obj));
      }

      public void RaiseObjectGotSelection(MapObject obj)
      {
        this.OnObjectGotSelection(new MapSelectionEventArgs(obj));
      }

      public void RaiseObjectHover(MapObject obj, MapInputEventArgs evt)
      {
        this.OnObjectHover(new MapObjectEventArgs(obj, evt));
      }

      public void RaiseObjectLostSelection(MapObject obj)
      {
        this.OnObjectLostSelection(new MapSelectionEventArgs(obj));
      }

      public void RaiseObjectResized(MapObject obj)
      {
        this.OnObjectResized(new MapSelectionEventArgs(obj));
      }

      public void RaiseObjectSingleClicked(MapObject obj, MapInputEventArgs evt)
      {
        this.OnObjectSingleClicked(new MapObjectEventArgs(obj, evt));
      }

      public void RaisePropertyChangedEvent(string propname)
      {
        this.OnPropertyChanged(new PropertyChangedEventArgs(propname));
      }

      public void RaiseSelectionCopied() => this.OnSelectionCopied(EventArgs.Empty);

      public void RaiseSelectionDeleted() => this.OnSelectionDeleted(EventArgs.Empty);

      public void RaiseSelectionDeleting(CancelEventArgs evt) => this.OnSelectionDeleting(evt);

      public void RaiseSelectionMoved() => this.OnSelectionMoved(EventArgs.Empty);

      public virtual void Redo()
      {
        if (!this.CanRedo())
          return;
        this.Document.Redo();
      }

      protected void removeFromSelection(MapObject obj)
      {
        if (obj is MapGroup mapGroup)
        {
          foreach (MapObject mapObject in mapGroup.GetEnumerator())
            this.removeFromSelection(mapObject);
        }
        this.Selection.Remove(obj);
      }

      internal void RemoveMapControl(MapControl g, Control c)
      {
        if (this.myMapControls == null)
          return;
        this.myMapControls.Remove((object) g);
        this.Controls.Remove(c);
      }

      public virtual IMapTool ReplaceMouseTool(System.Type tooltype, IMapTool newtool)
      {
        IList mouseDownTools = this.MouseDownTools;
        for (int index = 0; index < mouseDownTools.Count; ++index)
        {
          if (mouseDownTools[index].GetType() == tooltype)
          {
            IMapTool mapTool = (IMapTool) mouseDownTools[index];
            if (newtool == null)
            {
              mouseDownTools.RemoveAt(index);
              return mapTool;
            }
            mouseDownTools[index] = (object) newtool;
            return mapTool;
          }
        }
        IList mouseMoveTools = this.MouseMoveTools;
        for (int index = 0; index < mouseMoveTools.Count; ++index)
        {
          if (mouseMoveTools[index].GetType() == tooltype)
          {
            IMapTool mapTool = (IMapTool) mouseMoveTools[index];
            if (newtool == null)
            {
              mouseMoveTools.RemoveAt(index);
              return mapTool;
            }
            mouseMoveTools[index] = (object) newtool;
            return mapTool;
          }
        }
        IList mouseUpTools = this.MouseUpTools;
        for (int index = 0; index < mouseUpTools.Count; ++index)
        {
          if (mouseUpTools[index].GetType() == tooltype)
          {
            IMapTool mapTool = (IMapTool) mouseUpTools[index];
            if (newtool == null)
            {
              mouseUpTools.RemoveAt(index);
              return mapTool;
            }
            mouseUpTools[index] = (object) newtool;
            return mapTool;
          }
        }
        return (IMapTool) null;
      }

      public void OnViewChanged()
      {
        EventHandler viewChanged = this.ViewChanged;
        if (viewChanged == null)
          return;
        viewChanged((object) this, EventArgs.Empty);
      }

      public void OnViewChanging()
      {
        EventHandler viewChanging = this.ViewChanging;
        if (viewChanging == null)
          return;
        viewChanging((object) this, EventArgs.Empty);
      }

      public void SetPosAndScale(PointF newPos, float newScale)
      {
        this.DocScale = newScale;
        this.DocPosition = newPos;
        this.UpdateView();
      }

      public virtual void ZoomIn() => this.ZoomToScale(0.85f);

      public virtual void ZoomOut() => this.ZoomToScale(1.15f);

      /// <summary>свойство был ли вписан рисунок</summary>
      public bool IsZoomToFit => this.my_IsZoomToFit;

      public virtual void ZoomToFit()
      {
        this.ZoomToBox(this.ComputeDocumentBounds());
        this.my_IsZoomToFit = true;
      }

      public virtual void Zoom1to1()
      {
        RectangleF documentBounds = this.ComputeDocumentBounds();
        this.DocScale = 1f;
        this.DocPosition = new PointF(documentBounds.X, documentBounds.Y);
      }

      public virtual void ZoomToScale(float scale)
      {
        PointF docPosition = this.DocPosition;
        Rectangle displayRectangle = this.DisplayRectangle;
        PointF doc = this.ConvertViewToDoc(new Point((displayRectangle.Left + displayRectangle.Right) / 2, (displayRectangle.Top + displayRectangle.Bottom) / 2));
        this.ZoomToBox(new RectangleF(0.0f, 0.0f, (float) (((double) doc.X - (double) docPosition.X) * 2.0) * scale, (float) (((double) doc.Y - (double) docPosition.Y) * 2.0) * scale)
        {
          X = (float) ((double) doc.X * (1.0 - (double) scale) + (double) docPosition.X * (double) scale),
          Y = (float) ((double) doc.Y * (1.0 - (double) scale) + (double) docPosition.Y * (double) scale)
        });
      }

      public virtual void RescaleToFit()
      {
        RectangleF documentBounds = this.ComputeDocumentBounds();
        float num = this.DocScale;
        if ((double) documentBounds.Width > 0.0 && (double) documentBounds.Height > 0.0)
        {
          Size size = this.DisplayRectangle.Size;
          num = Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
        }
        this.DocScale = num;
        this.DocPosition = new PointF(documentBounds.X, documentBounds.Y);
      }

      public virtual void ZoomToScale(PointF ptdoc, float scale)
      {
        PointF docPosition = this.DocPosition;
        Rectangle displayRectangle = this.DisplayRectangle;
        PointF doc = this.ConvertViewToDoc(new Point((displayRectangle.Left + displayRectangle.Right) / 2, (displayRectangle.Top + displayRectangle.Bottom) / 2));
        RectangleF rectangleF = new RectangleF(0.0f, 0.0f, (float) (((double) doc.X - (double) docPosition.X) * 2.0) * scale, (float) (((double) doc.Y - (double) docPosition.Y) * 2.0) * scale);
        rectangleF.X = (float) ((1.0 - (double) scale) * (double) ptdoc.X + (double) docPosition.X * (double) scale);
        rectangleF.Y = (float) ((1.0 - (double) scale) * (double) ptdoc.Y + (double) docPosition.Y * (double) scale);
        this.OnViewChanging();
        float num = this.DocScale;
        if ((double) rectangleF.Width > 0.0 && (double) rectangleF.Height > 0.0)
        {
          Size size = this.DisplayRectangle.Size;
          num = Math.Min((float) size.Width / rectangleF.Width, (float) size.Height / rectangleF.Height);
        }
        this.DocScale = num;
        this.DocPosition = rectangleF.Location;
        this.UpdateView();
      }

      public virtual void ZoomToBox(RectangleF docBox)
      {
        this.OnViewChanging();
        float num = this.DocScale;
        if ((double) docBox.Width > 0.0 && (double) docBox.Height > 0.0)
        {
          Size size = this.DisplayRectangle.Size;
          num = Math.Min((float) size.Width / docBox.Width, (float) size.Height / docBox.Height);
        }
        this.DocScale = num;
        this.DocPosition = new PointF(docBox.X, docBox.Y);
        this.OnViewChanging();
        this.UpdateView();
      }

      public PointF Dpi
      {
        get
        {
          float x = 96f;
          float y = 96f;
          using (Graphics graphics = this.CreateGraphics())
          {
            x = graphics.DpiX;
            y = graphics.DpiY;
          }
          return new PointF(x, y);
        }
      }

      public float PixelsPerMM => this.Dpi.X / MapView.MillimetersPerInch;

      private void ResetAutoPanRegion() => this.AutoPanRegion = new Size(16 /*0x10*/, 16 /*0x10*/);

      private void ResetAutoScrollRegion()
      {
        this.AutoScrollRegion = new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
      }

      private void ResetGridColor() => this.GridColor = Color.LightGray;

      private void ResetNoFocusSelectionColor() => this.NoFocusSelectionColor = Color.LightGray;

      private void ResetPrimarySelectionColor() => this.PrimarySelectionColor = Color.Chartreuse;

      private void ResetScrollSmallChange()
      {
        this.ScrollSmallChange = new Size(16 /*0x10*/, 16 /*0x10*/);
      }

      private void ResetSecondarySelectionColor() => this.SecondarySelectionColor = Color.Cyan;

      private void ResetShadowColor()
      {
        this.ShadowColor = Color.FromArgb((int) sbyte.MaxValue, Color.Gray);
      }

      internal void SafeOnDocumentChanged(object sender, MapChangedEventArgs e)
      {
        if (this.InvokeRequired)
        {
          if (this.mySafeOnDocumentChangedDelegate == null)
            this.mySafeOnDocumentChangedDelegate = new EventHandler(this.InternalOnDocumentChanged);
          if (this.myQueuedEvents == null)
            this.myQueuedEvents = new Queue();
          MapChangedEventArgs changedEventArgs = new MapChangedEventArgs(e);
          lock (this.myQueuedEvents)
            this.myQueuedEvents.Enqueue((object) changedEventArgs);
          this.Invoke((Delegate) this.mySafeOnDocumentChangedDelegate);
        }
        else
          this.OnDocumentChanged(sender, e);
      }

      public virtual void ScrollLine(float dx, float dy)
      {
        PointF docPosition = this.DocPosition;
        SizeF docExtentSize = this.DocExtentSize;
        PointF documentTopLeft = this.DocumentTopLeft;
        SizeF documentSize = this.DocumentSize;
        Size scrollSmallChange = this.ScrollSmallChange;
        float num1 = dx * (float) scrollSmallChange.Width / this.myHorizScale;
        float num2 = dy * (float) scrollSmallChange.Height / this.myVertScale;
        docPosition.X += num1;
        docPosition.Y += num2;
        docPosition.X = (double) num1 < 0.0 ? Math.Max(docPosition.X, documentTopLeft.X) : Math.Min(docPosition.X, Math.Max(documentTopLeft.X, documentTopLeft.X + documentSize.Width - docExtentSize.Width));
        docPosition.Y = (double) num2 < 0.0 ? Math.Max(docPosition.Y, documentTopLeft.Y) : Math.Min(docPosition.Y, Math.Max(documentTopLeft.Y, documentTopLeft.Y + documentSize.Height - docExtentSize.Height));
        this.DocPosition = docPosition;
      }

      public virtual void ScrollPage(float dx, float dy)
      {
        PointF docPosition = this.DocPosition;
        SizeF docExtentSize = this.DocExtentSize;
        PointF documentTopLeft = this.DocumentTopLeft;
        SizeF documentSize = this.DocumentSize;
        Size scrollSmallChange = this.ScrollSmallChange;
        float num1 = dx * Math.Max((float) scrollSmallChange.Width, docExtentSize.Width - (float) scrollSmallChange.Width) / this.myHorizScale;
        float num2 = dy * Math.Max((float) scrollSmallChange.Height, docExtentSize.Height - (float) scrollSmallChange.Height) / this.myVertScale;
        docPosition.X += num1;
        docPosition.Y += num2;
        docPosition.X = (double) num1 < 0.0 ? Math.Max(docPosition.X, documentTopLeft.X) : Math.Min(docPosition.X, Math.Max(documentTopLeft.X, documentTopLeft.X + documentSize.Width - docExtentSize.Width));
        docPosition.Y = (double) num2 < 0.0 ? Math.Max(docPosition.Y, documentTopLeft.Y) : Math.Min(docPosition.Y, Math.Max(documentTopLeft.Y, documentTopLeft.Y + documentSize.Height - docExtentSize.Height));
        this.DocPosition = docPosition;
      }

      public virtual void ScrollRectangleToVisible(RectangleF contentRect)
      {
        RectangleF docExtent = this.DocExtent;
        if (MapObject.ContainsRect(docExtent, contentRect))
          return;
        this.DocPosition = new PointF((double) contentRect.Width >= (double) docExtent.Width ? contentRect.X : (float) ((double) contentRect.X + (double) contentRect.Width / 2.0 - (double) docExtent.Width / 2.0), (double) contentRect.Height >= (double) docExtent.Height ? contentRect.Y : (float) ((double) contentRect.Y + (double) contentRect.Height / 2.0 - (double) docExtent.Height / 2.0));
      }

      public virtual void SelectAll()
      {
        if (!this.CanSelectObjects())
          return;
        ArrayList arrayList = new ArrayList();
        foreach (MapLayer layer in this.Layers)
        {
          if (layer.IsInDocument && layer.CanViewObjects() && layer.CanSelectObjects())
          {
            foreach (MapObject mapObject in layer)
            {
              if (mapObject.CanView() && mapObject.CanSelect())
                arrayList.Add((object) mapObject);
            }
          }
        }
        foreach (MapObject mapObject in arrayList)
          this.Selection.Add(mapObject);
      }

      public virtual void SelectInRectangle(RectangleF rect)
      {
        if (!this.CanSelectObjects())
          return;
        ArrayList coll = new ArrayList();
        foreach (MapLayer layer in this.Layers)
        {
          if (layer.IsInDocument && layer.CanViewObjects() && layer.CanSelectObjects())
          {
            foreach (MapObject mapObject in layer)
              this.selectObjectInRectangle(mapObject, rect, false, coll);
          }
        }
        foreach (MapObject mapObject in coll)
          this.Selection.Add(mapObject);
      }

      public virtual bool SelectNextNode(char c)
      {
        if (this.CanSelectObjects())
        {
          IMapLabeledNode mapLabeledNode = (IMapLabeledNode) null;
          MapObject primary = this.Selection.Primary;
          if (primary != null && primary is IMapLabeledNode)
            mapLabeledNode = (IMapLabeledNode) primary;
          MapLayerCollectionObjectEnumerator enumerator = this.Document.GetEnumerator();
          if (mapLabeledNode != null)
          {
            while (enumerator.MoveNext() && enumerator.Current != mapLabeledNode)
              ;
          }
          while (enumerator.MoveNext())
          {
            MapObject current = enumerator.Current;
            if (current is IMapLabeledNode node && this.MatchesNodeLabel(node, c))
            {
              this.Selection.Select(current);
              this.ScrollRectangleToVisible(current.Bounds);
              return true;
            }
          }
          foreach (MapObject mapObject in this.Document)
          {
            IMapLabeledNode node = mapObject as IMapLabeledNode;
            if (node != mapLabeledNode)
            {
              if (node != null && this.MatchesNodeLabel(node, c))
              {
                this.Selection.Select(mapObject);
                this.ScrollRectangleToVisible(mapObject.Bounds);
                return true;
              }
            }
            else
              break;
          }
        }
        return false;
      }

      private void selectObjectInRectangle(MapObject obj, RectangleF rect, bool top, ArrayList coll)
      {
        if (!obj.CanView())
          return;
        if (obj.CanSelect())
        {
          MapObject selectionObject = obj.SelectionObject;
          if ((selectionObject != null ? (selectionObject.ContainedByRectangle(rect) ? 1 : 0) : (obj.ContainedByRectangle(rect) ? 1 : 0)) != 0)
          {
            coll.Add((object) obj);
            return;
          }
        }
        if (top || !(obj is MapGroup))
          return;
        foreach (MapObject mapObject in ((MapGroup) obj).GetEnumerator())
          this.selectObjectInRectangle(mapObject, rect, false, coll);
      }

      public virtual void SetModifiable(bool b)
      {
        this.AllowMove = b;
        this.AllowResize = b;
        this.AllowReshape = b;
        this.AllowDelete = b;
        this.AllowInsert = b;
        this.AllowLink = b;
        this.AllowEdit = b;
      }

      private bool ShouldSerializeAutoPanRegion()
      {
        return this.AutoPanRegion != new Size(16 /*0x10*/, 16 /*0x10*/);
      }

      private bool ShouldSerializeAutoScrollRegion()
      {
        return this.AutoScrollRegion != new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
      }

      private bool ShouldSerializeGridColor() => this.GridColor != Color.LightGray;

      private bool ShouldSerializeNoFocusSelectionColor()
      {
        return this.NoFocusSelectionColor != Color.LightGray;
      }

      private bool ShouldSerializePrimarySelectionColor()
      {
        return this.PrimarySelectionColor != Color.Chartreuse;
      }

      private bool ShouldSerializeScrollSmallChange()
      {
        return this.ScrollSmallChange != new Size(16 /*0x10*/, 16 /*0x10*/);
      }

      private bool ShouldSerializeSecondarySelectionColor()
      {
        return this.SecondarySelectionColor != Color.Cyan;
      }

      private bool ShouldSerializeShadowColor()
      {
        return this.ShadowColor != Color.FromArgb((int) sbyte.MaxValue, Color.Gray);
      }

      private void ShowExternalDragImage(MapObject img)
      {
        this.myExternalDragImage = img;
        this.Layers.Default.Add(img);
      }

      public virtual bool StartTransaction() => this.Document.StartTransaction();

      public void StopAutoScroll()
      {
        if (this.myAutoScrollTimer == null)
          return;
        this.myAutoScrollTimer.Change(-1, -1);
        this.myAutoScrollTimerEnabled = false;
      }

      private void StopHoverTimer()
      {
        if (this.myHoverTimer == null)
          return;
        this.myHoverTimer.Change(-1, -1);
        this.myHoverTimerEnabled = false;
      }

      public virtual void Undo()
      {
        if (!this.CanUndo())
          return;
        this.Document.Undo();
      }

      protected void UpdateBorderWidths()
      {
        Size borderSize = this.myBorderSize;
        Size size;
        switch (this.BorderStyle)
        {
          case BorderStyle.None:
            size = new Size();
            break;
          case BorderStyle.FixedSingle:
            size = SystemInformation.BorderSize;
            break;
          default:
            size = SystemInformation.Border3DSize;
            break;
        }
        if (!(size != this.myBorderSize))
          return;
        this.myBorderSize = size;
        this.LayoutScrollBars(false);
      }

      public virtual void UpdateScrollBars()
      {
        if (this.myUpdatingScrollBars)
          return;
        HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
        VScrollBar verticalScrollBar = this.VerticalScrollBar;
        if (verticalScrollBar == null && horizontalScrollBar == null)
          return;
        PointF documentTopLeft = this.DocumentTopLeft;
        SizeF documentSize = this.DocumentSize;
        int num1 = (int) Math.Floor((double) documentTopLeft.X * (double) this.myHorizScale);
        int num2 = (int) Math.Floor((double) documentTopLeft.Y * (double) this.myVertScale);
        int val1_1 = (int) Math.Floor(((double) documentTopLeft.X + (double) documentSize.Width) * (double) this.myHorizScale);
        int val1_2 = (int) Math.Floor(((double) documentTopLeft.Y + (double) documentSize.Height) * (double) this.myVertScale);
        PointF docPosition = this.DocPosition;
        int val2_1 = (int) Math.Floor((double) docPosition.X * (double) this.myHorizScale);
        int val2_2 = (int) Math.Floor((double) docPosition.Y * (double) this.myVertScale);
        Size size = this.Size;
        size.Width -= 2 * this.myBorderSize.Width;
        if (size.Width < 0)
          size.Width = 0;
        size.Height -= 2 * this.myBorderSize.Height;
        if (size.Height < 0)
          size.Height = 0;
        bool flag1 = val1_2 - num2 > size.Height || val2_2 > num2 || val2_2 < val1_2 - size.Height;
        bool flag2 = verticalScrollBar != null && (this.ShowVerticalScrollBar == MapViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag1);
        bool flag3 = val1_1 - num1 > size.Width || val2_1 > num1 || val2_1 < val1_1 - size.Width;
        bool flag4 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag3);
        if (flag2)
        {
          size.Width -= this.myScrollBarWidth;
          size.Width = Math.Max(0, size.Width);
        }
        if (flag4)
        {
          size.Height -= this.myScrollBarHeight;
          size.Height = Math.Max(0, size.Height);
        }
        bool flag5 = val1_2 - num2 > size.Height || val2_2 > num2 || val2_2 < val1_2 - size.Height;
        bool flag6 = verticalScrollBar != null && (this.ShowVerticalScrollBar == MapViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag5);
        bool flag7 = val1_1 - num1 > size.Width || val2_1 > num1 || val2_1 < val1_1 - size.Width;
        bool flag8 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag7);
        this.myUpdatingScrollBars = true;
        bool flag9 = false;
        if (verticalScrollBar != null)
        {
          int num3 = val1_2 - size.Height;
          if (val2_2 > num3 && num3 > num2)
            val2_2 = num3;
          else if (val2_2 < num2)
            val2_2 = num2;
          int num4 = Math.Max(Math.Max(val1_2, val2_2 + size.Height) - 12, val2_2);
          if (verticalScrollBar.Minimum != num2)
            verticalScrollBar.Minimum = num2;
          if (verticalScrollBar.Maximum != num4)
            verticalScrollBar.Maximum = num4;
          if (verticalScrollBar.Value != val2_2)
            verticalScrollBar.Value = val2_2;
          if (verticalScrollBar.Visible != flag6)
            flag9 = true;
          verticalScrollBar.Visible = flag6;
          verticalScrollBar.Enabled = flag5;
        }
        if (horizontalScrollBar != null)
        {
          int num5 = val1_1 - size.Width;
          if (val2_1 > num5 && num5 > num1)
            val2_1 = num5;
          else if (val2_1 < num1)
            val2_1 = num1;
          int num6 = Math.Max(Math.Max(val1_1, val2_1 + size.Width) - 12, val2_1);
          if (horizontalScrollBar.Minimum != num1)
            horizontalScrollBar.Minimum = num1;
          if (horizontalScrollBar.Maximum != num6)
            horizontalScrollBar.Maximum = num6;
          if (horizontalScrollBar.Value != val2_1)
            horizontalScrollBar.Value = val2_1;
          if (horizontalScrollBar.Visible != flag8)
            flag9 = true;
          horizontalScrollBar.Visible = flag8;
          horizontalScrollBar.Enabled = flag7;
        }
        this.myUpdatingScrollBars = false;
        if (!flag9)
          return;
        this.LayoutScrollBars(false);
      }

      protected void updateSelectionHandles(MapObject obj)
      {
        if (obj is MapGroup mapGroup)
        {
          foreach (MapObject mapObject in mapGroup.GetEnumerator())
            this.updateSelectionHandles(mapObject);
        }
        MapObject selectionObject = obj.SelectionObject;
        if (selectionObject == null)
          return;
        MapSelection selection = this.Selection;
        if (selection.Contains(obj) && obj.CanView())
          selectionObject.AddSelectionHandles(selection, obj);
        else
          selectionObject.RemoveSelectionHandles(selection);
      }

      public virtual void UpdateView()
      {
        this.UpdateBorderWidths();
        this.UpdateScrollBars();
        this.Invalidate();
      }

      [Category("Behavior")]
      [Description("Whether the user can copy selected objects.")]
      [DefaultValue(true)]
      public bool AllowCopy
      {
        get => this.myAllowCopy;
        set
        {
          if (this.myAllowCopy == value)
            return;
          this.myAllowCopy = value;
          this.RaisePropertyChangedEvent(nameof (AllowCopy));
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can delete selected objects.")]
      [Category("Behavior")]
      public bool AllowDelete
      {
        get => this.myAllowDelete;
        set
        {
          if (this.myAllowDelete == value)
            return;
          this.myAllowDelete = value;
          this.RaisePropertyChangedEvent(nameof (AllowDelete));
        }
      }

      [Description("Whether the user can drag the selection out of this view to another window.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool AllowDragOut
      {
        get => this.myAllowDragOut;
        set
        {
          if (this.myAllowDragOut == value)
            return;
          this.myAllowDragOut = value;
          this.RaisePropertyChangedEvent(nameof (AllowDragOut));
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether the user can edit objects.")]
      public bool AllowEdit
      {
        get => this.myAllowEdit;
        set
        {
          if (this.myAllowEdit == value)
            return;
          this.myAllowEdit = value;
          this.RaisePropertyChangedEvent(nameof (AllowEdit));
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether the user can insert new objects.")]
      public bool AllowInsert
      {
        get => this.myAllowInsert;
        set
        {
          if (this.myAllowInsert == value)
            return;
          this.myAllowInsert = value;
          this.RaisePropertyChangedEvent(nameof (AllowInsert));
        }
      }

      [Category("Behavior")]
      [Description("Whether the user can type keystroke commands in this view.")]
      [DefaultValue(true)]
      public bool AllowKey
      {
        get => this.myAllowKey;
        set
        {
          if (this.myAllowKey == value)
            return;
          this.myAllowKey = value;
          this.RaisePropertyChangedEvent(nameof (AllowKey));
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can link ports.")]
      [Category("Behavior")]
      public bool AllowLink
      {
        get => this.myAllowLink;
        set
        {
          if (this.myAllowLink == value)
            return;
          this.myAllowLink = value;
          this.RaisePropertyChangedEvent(nameof (AllowLink));
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can use the mouse in this view.")]
      [Category("Behavior")]
      public bool AllowMouse
      {
        get => this.myAllowMouse;
        set
        {
          if (this.myAllowMouse == value)
            return;
          this.myAllowMouse = value;
          this.RaisePropertyChangedEvent(nameof (AllowMouse));
        }
      }

      [Description("Whether the user can move selected objects.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool AllowMove
      {
        get => this.myAllowMove;
        set
        {
          if (this.myAllowMove == value)
            return;
          this.myAllowMove = value;
          this.RaisePropertyChangedEvent(nameof (AllowMove));
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether the user can reshape objects, if resizable.")]
      public bool AllowReshape
      {
        get => this.myAllowReshape;
        set
        {
          if (this.myAllowReshape == value)
            return;
          this.myAllowReshape = value;
          this.RaisePropertyChangedEvent(nameof (AllowReshape));
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can resize selected objects.")]
      [Category("Behavior")]
      public bool AllowResize
      {
        get => this.myAllowResize;
        set
        {
          if (this.myAllowResize == value)
            return;
          this.myAllowResize = value;
          this.RaisePropertyChangedEvent(nameof (AllowResize));
        }
      }

      [Category("Behavior")]
      [Description("Whether the user can select objects, if visible.")]
      [DefaultValue(true)]
      public bool AllowSelect
      {
        get => this.myAllowSelect;
        set
        {
          if (this.myAllowSelect == value)
            return;
          this.myAllowSelect = value;
          this.RaisePropertyChangedEvent(nameof (AllowSelect));
        }
      }

      [Description("The area around the original pan point outside of which the mouse will automatically cause the view to scroll.")]
      [Category("Behavior")]
      public virtual Size AutoPanRegion
      {
        get => this.myAutoPanRegion;
        set
        {
          if (!(this.myAutoPanRegion != value))
            return;
          if (value.Width < 0 || value.Height < 0)
            throw new ArgumentOutOfRangeException("New Size value for MapView.AutoPanRegion must have non-negative dimensions");
          this.myAutoPanRegion = value;
          this.RaisePropertyChangedEvent(nameof (AutoPanRegion));
        }
      }

      [Category("Behavior")]
      [DefaultValue(1000)]
      [Description("How long to wait in the autoscroll margin before performing any autoscrolling.")]
      public int AutoScrollDelay
      {
        get => this.myAutoScrollDelay;
        set
        {
          if (this.myAutoScrollDelay == value || value < 0)
            return;
          this.myAutoScrollDelay = value;
          this.RaisePropertyChangedEvent(nameof (AutoScrollDelay));
        }
      }

      [Description("The margin in the view where a mouse drag will automatically cause the view to scroll.")]
      [Category("Behavior")]
      public virtual Size AutoScrollRegion
      {
        get => this.myAutoScrollRegion;
        set
        {
          if (!(this.myAutoScrollRegion != value))
            return;
          if (value.Width < 0 || value.Height < 0)
            throw new ArgumentOutOfRangeException("New Size value for MapView.AutoScrollRegion must have non-negative dimensions");
          this.myAutoScrollRegion = value;
          this.RaisePropertyChangedEvent(nameof (AutoScrollRegion));
        }
      }

      [Description("How long to wait before changing the DocPosition during autoscrolling.")]
      [DefaultValue(100)]
      [Category("Behavior")]
      public int AutoScrollTime
      {
        get => this.myAutoScrollTime;
        set
        {
          if (this.myAutoScrollTime == value || value < 0)
            return;
          this.myAutoScrollTime = value;
          this.RaisePropertyChangedEvent(nameof (AutoScrollTime));
        }
      }

      [Category("Appearance")]
      [DefaultValue(6)]
      [Description("The 3D border style for this view, when BorderStyle is Fixed3D.")]
      public virtual Border3DStyle Border3DStyle
      {
        get => this.myBorder3DStyle;
        set
        {
          if (this.myBorder3DStyle == value)
            return;
          this.myBorder3DStyle = value;
          this.RaisePropertyChangedEvent(nameof (Border3DStyle));
        }
      }

      [DefaultValue(2)]
      [Category("Appearance")]
      [Description("The border style for this view.")]
      public virtual BorderStyle BorderStyle
      {
        get => this.myBorderStyle;
        set
        {
          if (this.myBorderStyle == value)
            return;
          this.myBorderStyle = value;
          this.UpdateBorderWidths();
          this.RaisePropertyChangedEvent(nameof (BorderStyle));
        }
      }

      [DefaultValue(2f)]
      [Description("The width of the pen used to draw the standard bounding handle")]
      [Category("Selection")]
      public virtual float BoundingHandlePenWidth
      {
        get => this.myBoundingHandlePenWidth;
        set
        {
          if ((double) this.myBoundingHandlePenWidth == (double) value)
            return;
          this.myBoundingHandlePenWidth = value;
          this.RaisePropertyChangedEvent(nameof (BoundingHandlePenWidth));
        }
      }

      [Category("Behavior")]
      [Description("[Only supported in MapDiagram Pocket]")]
      [DefaultValue(800)]
      public virtual int ContextClickTime
      {
        get => 800;
        set
        {
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual Control CornerControl
      {
        get => this.myCorner;
        set
        {
          Control corner = this.myCorner;
          if (corner == value)
            return;
          if (corner != null)
            this.Controls.Remove(corner);
          this.myCorner = value;
          if (value != null)
            this.Controls.Add(value);
          this.LayoutScrollBars(true);
          this.RaisePropertyChangedEvent(nameof (CornerControl));
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public override Cursor Cursor
      {
        get => base.Cursor;
        set
        {
          if (this.myDefaultCursor == (Cursor) null)
            this.myDefaultCursor = this.Cursor;
          if (!(this.Cursor != value))
            return;
          base.Cursor = value;
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public new virtual Cursor DefaultCursor
      {
        get => this.myDefaultCursor == (Cursor) null ? this.Cursor : this.myDefaultCursor;
        set
        {
          if (!(this.myDefaultCursor != value))
            return;
          this.myDefaultCursor = value;
          this.RaisePropertyChangedEvent(nameof (DefaultCursor));
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual IMapTool DefaultTool
      {
        get => this.myDefaultTool;
        set
        {
          if (this.myDefaultTool == value)
            return;
          this.myDefaultTool = value ?? throw new ArgumentOutOfRangeException("New value for MapView.DefaultTool must not be null");
          this.RaisePropertyChangedEvent(nameof (DefaultTool));
        }
      }

      /// <summary>прямоугольник окна прорисовки (Pixel)</summary>
      [Browsable(false)]
      public override Rectangle DisplayRectangle
      {
        get
        {
          Size size = this.Size;
          int val2_1 = size.Width - 2 * this.myBorderSize.Width;
          if (this.VerticalScrollBar != null && this.VerticalScrollBar.Visible)
            val2_1 -= this.myScrollBarWidth;
          int val2_2 = size.Height - 2 * this.myBorderSize.Height;
          if (this.HorizontalScrollBar != null && this.HorizontalScrollBar.Visible)
            val2_2 -= this.myScrollBarHeight;
          return new Rectangle(this.myBorderSize.Width, this.myBorderSize.Height, Math.Max(1, val2_1), Math.Max(1, val2_2));
        }
      }

      /// <summary>проекция окна прорисовки на Документ (mm)</summary>
      [Browsable(false)]
      public RectangleF DocExtent => new RectangleF(this.DocPosition, this.DocExtentSize);

      /// <summary>размер окна прорисовки в системе координат Документа </summary>
      [Browsable(false)]
      public virtual SizeF DocExtentSize
      {
        get
        {
          Size size = this.DisplayRectangle.Size;
          return new SizeF((float) size.Width / this.myHorizScale, (float) size.Height / this.myVertScale);
        }
      }

      /// <summary>проекция начала окна на Документ </summary>
      [Category("Appearance")]
      [TypeConverter(typeof (MapPointFConverter))]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Description("The position in the document that this view is displaying.")]
      public virtual PointF DocPosition
      {
        get => this.myOrigin;
        set
        {
          PointF origin = this.myOrigin;
          PointF pointF1 = this.LimitDocPosition(value);
          PointF pointF2 = pointF1;
          if (!(origin != pointF2))
            return;
          this.myOrigin = pointF1;
          this.RaisePropertyChangedEvent(nameof (DocPosition));
        }
      }

      [Description("The scale at which this view displays its document.")]
      [DefaultValue(1f)]
      [Category("Appearance")]
      public virtual float DocScale
      {
        get => this.myHorizScale;
        set
        {
          float num = this.LimitDocScale(value);
          this.my_IsZoomToFit = false;
          if ((double) this.myHorizScale == (double) num && (double) this.myVertScale == (double) num)
            return;
          this.myHorizScale = num;
          this.myVertScale = num;
          this.RaisePropertyChangedEvent(nameof (DocScale));
        }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public virtual MapDocument Document
      {
        get => this.myDocument;
        set
        {
          if (value == null)
            throw new ArgumentOutOfRangeException("New value for MapView.Document must not be null");
          MapDocument document = this.Document;
          if (value == document)
            return;
          if (document != null && this.myDocChangedEventHandler != null)
            document.Changed -= this.myDocChangedEventHandler;
          if (this.Tool != null)
            this.DoCancelMouse();
          this.DoEndEdit();
          this.Selection?.Clear();
          this.myDocument = value;
          value.Changed += this.myDocChangedEventHandler;
          this.RaisePropertyChangedEvent(nameof (Document));
          this.InitializeLayersFromDocument();
        }
      }

      /// <summary>размеры документа; если нет документа то (0,0) </summary>
      [Browsable(false)]
      public virtual SizeF DocumentSize
      {
        get
        {
          MapDocument document = this.Document;
          if (document == null)
            return SizeF.Empty;
          SizeF size = document.Size;
          ref SizeF local1 = ref size;
          double width = (double) local1.Width;
          SizeF shadowOffset = this.ShadowOffset;
          double num1 = (double) Math.Abs(shadowOffset.Width);
          local1.Width = (float) (width + num1);
          ref SizeF local2 = ref size;
          double height = (double) local2.Height;
          shadowOffset = this.ShadowOffset;
          double num2 = (double) Math.Abs(shadowOffset.Height);
          local2.Height = (float) (height + num2);
          if (!this.ShowsNegativeCoordinates)
          {
            PointF topLeft = document.TopLeft;
            if ((double) topLeft.X < 0.0)
              size.Width += topLeft.X;
            if ((double) topLeft.Y < 0.0)
              size.Height += topLeft.Y;
          }
          return size;
        }
      }

      /// <summary>точка начала документа; если нет документа то (0,0) </summary>
      [Browsable(false)]
      public virtual PointF DocumentTopLeft
      {
        get
        {
          if (!this.ShowsNegativeCoordinates || this.Document == null)
            return PointF.Empty;
          PointF topLeft = this.Document.TopLeft;
          SizeF shadowOffset = this.ShadowOffset;
          if ((double) shadowOffset.Width < 0.0)
            topLeft.X += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
            topLeft.Y += shadowOffset.Height;
          return topLeft;
        }
      }

      [DefaultValue(false)]
      [Description("Whether a user's drag of the selection occurs continuously instead of dragging an outline.")]
      [Category("Behavior")]
      public virtual bool DragsRealtime
      {
        get => this.myDragsRealtime;
        set
        {
          if (this.myDragsRealtime == value)
            return;
          this.myDragsRealtime = value;
          this.RaisePropertyChangedEvent(nameof (DragsRealtime));
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual MapControl EditControl
      {
        get => this.myEditControl;
        set
        {
          MapControl editControl = this.myEditControl;
          if (editControl == value)
            return;
          if (editControl != null && editControl.View == this)
            editControl.Remove();
          if (value == null)
            return;
          this.myEditControl = value;
          this.Layers.Default.Add((MapObject) value);
          this.myModalControl = value.GetControl(this);
        }
      }

      [Description("Whether the user drags newly dropped objects on a drag enter.")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public bool ExternalDragDropsOnEnter
      {
        get => this.myExternalDragDropsOnEnter;
        set => this.myExternalDragDropsOnEnter = value;
      }

      [Browsable(false)]
      public MapInputEventArgs FirstInput => this.myFirstInput;

      [Category("Grid")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Description("The size of each cell in the grid.")]
      public virtual SizeF GridCellSize
      {
        get => this.myGridCellSize;
        set
        {
          if (!(this.myGridCellSize != value))
            return;
          if ((double) value.Width <= 0.0 || (double) value.Height <= 0.0)
            throw new ArgumentOutOfRangeException("New SizeF value for MapView.GridCellSize must have positive dimensions");
          this.myGridCellSize = value;
          this.RaisePropertyChangedEvent(nameof (GridCellSize));
        }
      }

      [Description("The color used in drawing the grid lines.")]
      [Category("Grid")]
      public virtual Color GridColor
      {
        get => this.myGridColor;
        set
        {
          if (!(this.myGridColor != value))
            return;
          this.myGridColor = value;
          this.RaisePropertyChangedEvent(nameof (GridColor));
        }
      }

      [TypeConverter(typeof (MapPointFConverter))]
      [Description("The origin for the grid.")]
      [Browsable(false)]
      [Category("Grid")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual PointF GridOrigin
      {
        get => this.myGridOrigin;
        set
        {
          if (!(this.myGridOrigin != value))
            return;
          this.myGridOrigin = value;
          this.RaisePropertyChangedEvent(nameof (GridOrigin));
        }
      }

      [Category("Grid")]
      [Description("The pen dash style used in drawing the grid lines.")]
      [DefaultValue(0)]
      public virtual DashStyle GridPenDashStyle
      {
        get => this.myGridPenDashStyle;
        set
        {
          if (this.myGridPenDashStyle == value || value == DashStyle.Custom)
            return;
          this.myGridPenDashStyle = value;
          this.RaisePropertyChangedEvent(nameof (GridPenDashStyle));
        }
      }

      [Description("The width of the pen used in drawing the grid lines.")]
      [Category("Grid")]
      [DefaultValue(1f)]
      public virtual float GridPenWidth
      {
        get => this.myGridPenWidth;
        set
        {
          if ((double) this.myGridPenWidth == (double) value)
            return;
          this.myGridPenWidth = value;
          this.RaisePropertyChangedEvent(nameof (GridPenWidth));
        }
      }

      [Description("The interactive dragging behavior for positioning objects.")]
      [Category("Grid")]
      [DefaultValue(0)]
      public virtual MapViewSnapStyle GridSnapDrag
      {
        get => this.mySnapDrag;
        set
        {
          if (this.mySnapDrag == value)
            return;
          this.mySnapDrag = value;
          this.RaisePropertyChangedEvent(nameof (GridSnapDrag));
        }
      }

      [Category("Grid")]
      [Description("The interactive resizing behavior for resizing objects.")]
      [DefaultValue(0)]
      public virtual MapViewSnapStyle GridSnapResize
      {
        get => this.mySnapResize;
        set
        {
          if (this.mySnapResize == value)
            return;
          this.mySnapResize = value;
          this.RaisePropertyChangedEvent(nameof (GridSnapResize));
        }
      }

      [DefaultValue(0)]
      [Description("The appearance style of the grid.")]
      [Category("Grid")]
      public virtual MapViewGridStyle GridStyle
      {
        get => this.myGridStyle;
        set
        {
          if (this.myGridStyle == value)
            return;
          this.myGridStyle = value;
          this.RaisePropertyChangedEvent(nameof (GridStyle));
        }
      }

      [Description("Whether the selection disappears when this view loses focus.")]
      [Category("Selection")]
      [DefaultValue(false)]
      public virtual bool HidesSelection
      {
        get => this.myHidesSelection;
        set
        {
          if (this.myHidesSelection == value)
            return;
          this.myHidesSelection = value;
          this.RaisePropertyChangedEvent(nameof (HidesSelection));
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual HScrollBar HorizontalScrollBar
      {
        get => this.myHorizScroll;
        set
        {
          HScrollBar horizScroll = this.myHorizScroll;
          if (horizScroll == value)
            return;
          if (horizScroll != null)
          {
            horizScroll.Scroll -= this.myHorizScrollHandler;
            this.Controls.Remove((Control) horizScroll);
          }
          this.myHorizScroll = value;
          if (value != null)
          {
            value.SmallChange = this.ScrollSmallChange.Width;
            this.Controls.Add((Control) value);
            value.Scroll += this.myHorizScrollHandler;
          }
          this.LayoutScrollBars(true);
          this.RaisePropertyChangedEvent(nameof (HorizontalScrollBar));
        }
      }

      [Description("How long a mouse should stay at one spot before a hover event occurs.")]
      [DefaultValue(1000)]
      [Category("Behavior")]
      public int HoverDelay
      {
        get => this.myHoverDelay;
        set
        {
          if (this.myHoverDelay == value)
            return;
          this.myHoverDelay = value;
          this.RaisePropertyChangedEvent(nameof (HoverDelay));
        }
      }

      [DefaultValue(null)]
      [Category("Appearance")]
      [Description("The ImageList from which MapImage objects can draw an image.")]
      public virtual ImageList ImageList
      {
        get => this.myImageList;
        set
        {
          if (this.myImageList == value)
            return;
          this.myImageList = value;
          this.RaisePropertyChangedEvent(nameof (ImageList));
        }
      }

      [Description("How images are rendered when scaled or stretched")]
      [DefaultValue(2)]
      [Category("Appearance")]
      public InterpolationMode InterpolationMode
      {
        get => this.myInterpolationMode;
        set
        {
          if (this.myInterpolationMode == value)
            return;
          this.myInterpolationMode = value;
          this.RaisePropertyChangedEvent(nameof (InterpolationMode));
        }
      }

      [Browsable(false)]
      public virtual bool IsEditing => this.EditControl != null;

      [Browsable(false)]
      public virtual bool IsPrinting => this.myPrintInfo != null;

      [Browsable(false)]
      public MapInputEventArgs LastInput => this.myLastInput;

      [Browsable(false)]
      public MapLayerCollection Layers => this.myLayers;

      [Category("Selection")]
      [Description("The maximum number of selected objects")]
      [DefaultValue(1000000)]
      public virtual int MaximumSelectionCount
      {
        get => this.myMaximumSelectionCount;
        set
        {
          if (value == this.myMaximumSelectionCount || value < 0)
            return;
          this.myMaximumSelectionCount = value;
          this.RaisePropertyChangedEvent(nameof (MaximumSelectionCount));
          while (this.Selection.Count > value)
            this.Selection.Remove(this.Selection.Last);
        }
      }

      [Browsable(false)]
      public virtual IList MouseDownTools
      {
        get
        {
          if (this._mouseDownTools == null)
          {
            this._mouseDownTools = new ArrayList();
            this._mouseDownTools.Add((object) new MapToolAction(this));
            this._mouseDownTools.Add((object) new MapToolContext(this));
            this._mouseDownTools.Add((object) new MapToolPanning(this));
            this._mouseDownTools.Add((object) new MapToolRelinking(this));
            this._mouseDownTools.Add((object) new MapToolResizing(this));
            this._mouseDownTools.Add((object) new MapToolLinkingNew(this));
          }
          return (IList) this._mouseDownTools;
        }
      }

      [Browsable(false)]
      public virtual IList MouseMoveTools
      {
        get
        {
          if (this._mouseMoveTools == null)
          {
            this._mouseMoveTools = new ArrayList();
            this._mouseMoveTools.Add((object) new MapToolDragging(this));
            this._mouseMoveTools.Add((object) new MapToolRubberBanding(this));
          }
          return (IList) this._mouseMoveTools;
        }
      }

      [Browsable(false)]
      public virtual IList MouseUpTools
      {
        get
        {
          if (this._mouseUpTools == null)
          {
            this._mouseUpTools = new ArrayList();
            this._mouseUpTools.Add((object) new MapToolSelecting(this));
          }
          return (IList) this._mouseUpTools;
        }
      }

      [Category("Behavior")]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Description("The Type of the link to be created when linking.")]
      [DefaultValue(typeof (MapLink))]
      public virtual System.Type NewLinkClass
      {
        get => this.myNewLinkClass;
        set
        {
          if (!(this.myNewLinkClass != value))
            return;
          this.myNewLinkClass = !(value == (System.Type) null) ? value : throw new ArgumentOutOfRangeException("New Type value for MapView.NewLinkClass must implement IGoLink");
          this.RaisePropertyChangedEvent(nameof (NewLinkClass));
        }
      }

      [Description("The handle color for objects when the view does not have focus.")]
      [Category("Selection")]
      public virtual Color NoFocusSelectionColor
      {
        get => this.myNoFocusSelectionColor;
        set
        {
          if (!(this.myNoFocusSelectionColor != value))
            return;
          this.myNoFocusSelectionColor = value;
          this.RaisePropertyChangedEvent(nameof (NoFocusSelectionColor));
        }
      }

      [Description("The scale at which greeked objects paint something simple.")]
      [Category("Appearance")]
      [DefaultValue(0.24f)]
      public virtual float PaintGreekScale
      {
        get => this.myPaintGreekScale;
        set
        {
          if ((double) this.myPaintGreekScale == (double) value)
            return;
          this.myPaintGreekScale = value;
          this.RaisePropertyChangedEvent(nameof (PaintGreekScale));
        }
      }

      [Description("The scale at which greeked objects paint nothing.")]
      [Category("Appearance")]
      [DefaultValue(0.13f)]
      public virtual float PaintNothingScale
      {
        get => this.myPaintNothingScale;
        set
        {
          if ((double) this.myPaintNothingScale == (double) value)
            return;
          this.myPaintNothingScale = value;
          this.RaisePropertyChangedEvent(nameof (PaintNothingScale));
        }
      }

      [DefaultValue(100f)]
      [Description("The distance at which potential links will snap to valid ports.")]
      [Category("Behavior")]
      public virtual float PortGravity
      {
        get => this.myPortGravity;
        set
        {
          if ((double) this.myPortGravity == (double) value)
            return;
          this.myPortGravity = (double) value > 0.0 ? value : throw new ArgumentOutOfRangeException("New distance value for MapView.PortGravity must be positive");
          this.RaisePropertyChangedEvent(nameof (PortGravity));
        }
      }

      [Description("The handle color for the primary selection.")]
      [Category("Selection")]
      public virtual Color PrimarySelectionColor
      {
        get => this.myPrimarySelectionColor;
        set
        {
          if (!(this.myPrimarySelectionColor != value))
            return;
          this.myPrimarySelectionColor = value;
          this.RaisePropertyChangedEvent(nameof (PrimarySelectionColor));
        }
      }

      [Browsable(false)]
      public virtual SizeF PrintDocumentSize
      {
        get
        {
          RectangleF documentBounds = this.ComputeDocumentBounds();
          SizeF printDocumentSize = MapTool.SubtractPoints(new PointF(documentBounds.X + documentBounds.Width, documentBounds.Y + documentBounds.Height), this.PrintDocumentTopLeft);
          printDocumentSize.Width += Math.Abs(this.ShadowOffset.Width);
          printDocumentSize.Height += Math.Abs(this.ShadowOffset.Height);
          return printDocumentSize;
        }
      }

      [Browsable(false)]
      public virtual PointF PrintDocumentTopLeft
      {
        get
        {
          PointF topLeft = this.Document.TopLeft;
          SizeF shadowOffset = this.ShadowOffset;
          if ((double) shadowOffset.Width < 0.0)
            topLeft.X += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
            topLeft.Y += shadowOffset.Height;
          return topLeft;
        }
      }

      [Category("Appearance")]
      [DefaultValue(0.8f)]
      [Description("The scale at which we should print.")]
      public virtual float PrintScale
      {
        get => this.myPrintScale;
        set
        {
          if ((double) this.myPrintScale == (double) value)
            return;
          this.myPrintScale = (double) value > 0.0 ? value : throw new ArgumentOutOfRangeException("New value for MapView.PrintScale must be positive");
          this.RaisePropertyChangedEvent(nameof (PrintScale));
        }
      }

      [Category("Selection")]
      [DefaultValue(1f)]
      [Description("The width of the pen used to draw the standard resize handle")]
      public virtual float ResizeHandlePenWidth
      {
        get => this.myResizeHandlePenWidth;
        set
        {
          if ((double) this.myResizeHandlePenWidth == (double) value)
            return;
          this.myResizeHandlePenWidth = value;
          this.RaisePropertyChangedEvent(nameof (ResizeHandlePenWidth));
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Selection")]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Description("The default size for new resize handles.")]
      public virtual SizeF ResizeHandleSize
      {
        get => this.myResizeHandleSize;
        set
        {
          if (!(this.myResizeHandleSize != value))
            return;
          this.myResizeHandleSize = value;
          this.RaisePropertyChangedEvent(nameof (ResizeHandleSize));
        }
      }

      [Description("The distance to scroll when scrolling a small amount.")]
      [Category("Behavior")]
      public virtual Size ScrollSmallChange
      {
        get => this.myScrollSmallChange;
        set
        {
          if (!(this.myScrollSmallChange != value))
            return;
          if (value.Width <= 0 || value.Height <= 0)
            throw new ArgumentOutOfRangeException("New Size value for MapView.ScrollSmallChange must have positive dimensions");
          this.myScrollSmallChange = value;
          HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
          if (horizontalScrollBar != null && horizontalScrollBar.SmallChange != this.myScrollSmallChange.Width)
            horizontalScrollBar.SmallChange = this.myScrollSmallChange.Width;
          VScrollBar verticalScrollBar = this.VerticalScrollBar;
          if (verticalScrollBar != null && verticalScrollBar.SmallChange != this.myScrollSmallChange.Height)
            verticalScrollBar.SmallChange = this.myScrollSmallChange.Height;
          this.RaisePropertyChangedEvent(nameof (ScrollSmallChange));
        }
      }

      [Description("The handle color for objects other than the primary selection.")]
      [Category("Selection")]
      public virtual Color SecondarySelectionColor
      {
        get => this.mySecondarySelectionColor;
        set
        {
          if (!(this.mySecondarySelectionColor != value))
            return;
          this.mySecondarySelectionColor = value;
          this.RaisePropertyChangedEvent(nameof (SecondarySelectionColor));
        }
      }

      [Browsable(false)]
      public virtual MapSelection Selection => this.mySelection;

      [Description("Whether the user typing a letter or digit will select the next node starting with that character.")]
      [Category("Selection")]
      [DefaultValue(true)]
      public bool SelectsByFirstChar
      {
        get => this.mySelectsByFirstChar;
        set
        {
          if (this.mySelectsByFirstChar == value)
            return;
          this.mySelectsByFirstChar = value;
          this.RaisePropertyChangedEvent(nameof (SelectsByFirstChar));
        }
      }

      [Category("Shadows")]
      [Description("The color used for drawing drop shadows.")]
      public virtual Color ShadowColor
      {
        get => this.myShadowColor;
        set
        {
          if (!(this.myShadowColor != value))
            return;
          this.myShadowColor = value;
          this.RaisePropertyChangedEvent(nameof (ShadowColor));
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Browsable(false)]
      [Category("Shadows")]
      [Description("The offset distance for drop shadows.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual SizeF ShadowOffset
      {
        get => this.myShadowOffset;
        set
        {
          if (!(this.myShadowOffset != value))
            return;
          this.myShadowOffset = value;
          this.RaisePropertyChangedEvent(nameof (ShadowOffset));
        }
      }

      [Description("The visibility policy for the horizontal scroll bar.")]
      [Category("Appearance")]
      [DefaultValue(2)]
      public virtual MapViewScrollBarVisibility ShowHorizontalScrollBar
      {
        get => this.myShowHorizScroll;
        set
        {
          if (this.myShowHorizScroll == value)
            return;
          this.myShowHorizScroll = value;
          this.LayoutScrollBars(true);
          this.RaisePropertyChangedEvent(nameof (ShowHorizontalScrollBar));
        }
      }

      [Category("Behavior")]
      [Description("Whether any parts of the document at negative coordinates can be seen or scrolled to.")]
      [DefaultValue(true)]
      public virtual bool ShowsNegativeCoordinates
      {
        get => this.myShowsNegativeCoordinates;
        set
        {
          if (this.myShowsNegativeCoordinates == value)
            return;
          this.myShowsNegativeCoordinates = value;
          this.RaisePropertyChangedEvent(nameof (ShowsNegativeCoordinates));
        }
      }

      [DefaultValue(2)]
      [Category("Appearance")]
      [Description("The visibility policy for the vertical scroll bar.")]
      public virtual MapViewScrollBarVisibility ShowVerticalScrollBar
      {
        get => this.myShowVertScroll;
        set
        {
          if (this.myShowVertScroll == value)
            return;
          this.myShowVertScroll = value;
          this.LayoutScrollBars(true);
          this.RaisePropertyChangedEvent(nameof (ShowVerticalScrollBar));
        }
      }

      [DefaultValue(true)]
      [Description("Использовать ли буфер при выводе")]
      [Category("Appearance")]
      public bool UseBuffer
      {
        get => this.useBuffer;
        set
        {
          if (this.useBuffer == value)
            return;
          this.useBuffer = value;
          this.DoubleBuffered = !value;
          this.RaisePropertyChangedEvent(nameof (UseBuffer));
        }
      }

      [DefaultValue(2)]
      [Description("How nicely lines are drawn")]
      [Category("Appearance")]
      public SmoothingMode SmoothingMode
      {
        get => this.mySmoothingMode;
        set
        {
          if (this.mySmoothingMode == value)
            return;
          this.mySmoothingMode = value;
          this.RaisePropertyChangedEvent(nameof (SmoothingMode));
        }
      }

      [Category("Appearance")]
      [DefaultValue(5)]
      [Description("How nicely text is rendered")]
      public TextRenderingHint TextRenderingHint
      {
        get => this.myTextRenderingHint;
        set
        {
          if (this.myTextRenderingHint == value)
            return;
          this.myTextRenderingHint = value;
          this.RaisePropertyChangedEvent(nameof (TextRenderingHint));
        }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public virtual IMapTool Tool
      {
        get => this.myTool;
        set
        {
          if (this.myTool == value)
            return;
          if (this.myTool != null)
            this.myTool.Stop();
          this.myTool = value != null ? value : this.DefaultTool;
          if (this.myTool != null)
            this.myTool.Start();
          this.RaisePropertyChangedEvent(nameof (Tool));
        }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public virtual ToolTip ToolTip
      {
        get => this.myToolTip;
        set
        {
          if (this.myToolTip == value)
            return;
          this.myToolTip = value;
          this.RaisePropertyChangedEvent(nameof (ToolTip));
        }
      }

      public static float Version => 2.2f;

      public static string VersionName
      {
        get => "1.1.1.0";
        set
        {
          MapView.myVersionName = value;
          MapView.myVersionAssembly = Assembly.GetCallingAssembly();
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual VScrollBar VerticalScrollBar
      {
        get => this.myVertScroll;
        set
        {
          VScrollBar vertScroll = this.myVertScroll;
          if (vertScroll == value)
            return;
          if (vertScroll != null)
          {
            vertScroll.Scroll -= this.myVertScrollHandler;
            this.Controls.Remove((Control) vertScroll);
          }
          this.myVertScroll = value;
          if (value != null)
          {
            value.SmallChange = this.ScrollSmallChange.Height;
            this.Controls.Add((Control) value);
            value.Scroll += this.myVertScrollHandler;
          }
          this.LayoutScrollBars(true);
          this.RaisePropertyChangedEvent(nameof (VerticalScrollBar));
        }
      }

      internal class ExternalDragImage : MapImage
      {
        private SizeF myOffset;

        public ExternalDragImage() => this.myOffset = new SizeF(0.0f, 0.0f);

        public override PointF Location
        {
          get => new PointF(this.Left + this.myOffset.Width, this.Top + this.myOffset.Height);
          set
          {
            this.Position = new PointF(value.X - this.myOffset.Width, value.Y - this.myOffset.Height);
          }
        }

        public SizeF Offset
        {
          get => this.myOffset;
          set => this.myOffset = value;
        }
      }

      [Serializable]
      internal sealed class PrintInfo
      {
        internal int CurPage;
        internal RectangleF DocRect;
        internal float HorizScale;
        internal int NumPagesAcross;
        internal int NumPagesDown;
        internal SizeF PrintSize;
        internal float VertScale;
      }
    }
}
