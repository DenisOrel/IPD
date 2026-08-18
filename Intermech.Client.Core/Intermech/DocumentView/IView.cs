
// Type: Intermech.DocumentView.IView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

public interface IView
{
  event PaintEventHandler Paint;

  event EventHandler Resize;

  event PropertyChangedEventHandler PropertyChanged;

  event EventHandler ViewChanged;

  event EventHandler ViewChanging;

  Point ConvertDocToView(PointF p);

  Rectangle ConvertDocToView(RectangleF r);

  Size ConvertDocToView(SizeF s);

  PointF ConvertViewToDoc(Point p);

  RectangleF ConvertViewToDoc(Rectangle r);

  SizeF ConvertViewToDoc(Size s);

  [Description("The position in the document that this view is displaying.")]
  [Browsable(false)]
  [Category("Appearance")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  PointF DocPosition { get; set; }

  void RaisePropertyChangedEvent(string propname);

  void UpdateView();

  void SetPosAndScale(PointF newPos, float newScale);

  void UpdateScrollBars();

  [Browsable(false)]
  RectangleF DocExtent { get; }

  Rectangle DisplayRectangle { get; }

  void ScrollRectangleToVisible(RectangleF contentRect);

  /// <summary>
  /// Replace one of the "mode-less" tools used by this view.
  /// </summary>
  /// <param name="tooltype">the <c>Type</c> of the tool to be replaced;
  /// this should not be a base class of the actual tool instance type</param>
  /// <param name="newtool">the tool to use instead of the existing one of
  /// <c>Type</c> <paramref name="tooltype" />;
  /// if null, the old tool is only removed</param>
  /// <returns>the tool that was replaced, or null if no such instance was found</returns>
  /// <remarks>
  /// When you want to customize an existing "mode-less" tool, and when setting one of its properties
  /// is insufficient, you may need to define your own subclass of that tool or define
  /// your own tool inheriting from <see cref="T:Intermech.Map.MapTool" />.
  /// In order for the view to use your tool, you'll need to create an instance of
  /// your tool class for the view, and then you can either set <see cref="P:Intermech.Map.MapView.Tool" />
  /// explicitly, or let <see cref="T:Intermech.Map.MapToolManager" /> find your tool in one of the mouse tool
  /// lists, such as <see cref="P:Intermech.Map.MapView.MouseDownTools" />.
  /// For the latter case, you could just add an instance of your tool to one of those lists.
  /// But often you will not want to allow the instance of the original tool class to be used.
  /// This method makes it easy to replace an existing tool with a different one.
  /// This method searches all of the lists of mode-less tools:
  /// <seealso cref="P:Intermech.Map.MapView.MouseDownTools" />, <seealso cref="P:Intermech.Map.MapView.MouseMoveTools" />, <seealso cref="P:Intermech.Map.MapView.MouseUpTools" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.FindMouseTool(System.Type)" />
  /// <example>
  /// You have defined a new subclass of <see cref="T:Intermech.Map.MapToolLinkingNew" />, called <c>CustomLinkTool</c>.
  /// For each view that you want to use of this new tool instead of the standard way
  /// for users to draw new links, call
  /// <c>aView.ReplaceMouseTool(typeof(GoToolLinkingNew), new CustomLinkTool(aView))</c>
  /// </example>
  ITool ReplaceMouseTool(System.Type tooltype, ITool newtool);

  void HandleScroll(object sender, ScrollEventArgs e);

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  HScrollBar HorizontalScrollBar { get; set; }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  VScrollBar VerticalScrollBar { get; set; }

  [DefaultValue(2)]
  [Description("The visibility policy for the vertical scroll bar.")]
  [Category("Appearance")]
  ViewScrollBarVisibility ShowVerticalScrollBar { get; set; }

  [Category("Appearance")]
  [Description("The visibility policy for the horizontal scroll bar.")]
  [DefaultValue(2)]
  ViewScrollBarVisibility ShowHorizontalScrollBar { get; set; }

  void LayoutScrollBars(bool update);

  [Description("The distance to scroll when scrolling a small amount.")]
  [Category("Behavior")]
  Size ScrollSmallChange { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  Control CornerControl { get; set; }

  [Description("The border style for this view.")]
  [DefaultValue(2)]
  [Category("Appearance")]
  BorderStyle BorderStyle { get; set; }

  [Browsable(false)]
  SizeF DocumentSize { get; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  IDocument Document { get; set; }

  [Browsable(false)]
  PointF DocumentTopLeft { get; }

  PointF LimitDocPosition(PointF p);

  float LimitDocScale(float s);

  [Browsable(false)]
  SizeF DocExtentSize { get; }

  [Description("The offset distance for drop shadows.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Category("Shadows")]
  [Browsable(false)]
  SizeF ShadowOffset { get; set; }

  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Whether any parts of the document at negative coordinates can be seen or scrolled to.")]
  bool ShowsNegativeCoordinates { get; set; }

  void DoCancelMouse();

  void DoEndEdit();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  ITool Tool { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  ITool DefaultTool { get; set; }

  void DoMouseDown();

  void ScrollPage(float dx, float dy);

  void DoAutoScroll(Point viewPnt);

  [Browsable(false)]
  InputEventArgs FirstInput { get; }

  [Browsable(false)]
  InputEventArgs LastInput { get; }

  void DoWheel(InputEventArgs evt);

  [Description("The scale at which this view displays its document.")]
  [DefaultValue(1f)]
  [Category("Appearance")]
  float DocScale { get; set; }

  void ScrollLine(float dx, float dy);

  bool DoDoubleClick(InputEventArgs evt);

  IObject PickObject(bool doc, bool view, PointF p, bool selectableOnly);

  [Browsable(false)]
  IList MouseUpTools { get; }

  bool DoMouseOver(InputEventArgs evt);

  [Browsable(false)]
  IList MouseMoveTools { get; }

  PointF OriginDocPosition { get; set; }

  bool DoHover(InputEventArgs evt);

  ISelection Selection { get; }

  bool DoSingleClick(InputEventArgs evt);

  bool StartTransaction();

  bool AbortTransaction();

  bool FinishTransaction(string p);

  void EditDelete();

  void SelectAll();

  void EditCopy();

  void EditCut();

  void EditPaste();

  void EditEdit();

  RectangleF ComputeDocumentBounds();

  void Undo();

  void Redo();

  bool CanSelectObjects();

  bool SelectNextNode(char ch1);

  [Browsable(false)]
  IList MouseDownTools { get; }

  [Description("Whether the user typing a letter or digit will select the next node starting with that character.")]
  [DefaultValue(true)]
  [Category("Selection")]
  bool SelectsByFirstChar { get; set; }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  Cursor Cursor { get; set; }

  void DoAutoPan(Point originPnt, Point viewPnt);

  [Description("How long to wait in the autoscroll margin before performing any autoscrolling.")]
  [DefaultValue(1000)]
  [Category("Behavior")]
  int AutoScrollDelay { get; set; }

  [Description("The appearance style of the grid.")]
  [Category("Grid")]
  [DefaultValue(0)]
  ViewGridStyle GridStyle { get; set; }

  void SelectInRectangle(RectangleF rect);

  void StopAutoScroll();

  void DrawXorBox(Rectangle rectangle, bool p);

  void Invalidate(Rectangle rectangle);

  void Refresh();

  void SetDefaults();

  Cursor GetDefaultCursor();

  void ZoomIn();

  void ZoomOut();

  void ZoomToFit();

  void ZoomToBox(RectangleF docBox);

  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Whether the user can select objects, if visible.")]
  bool AllowSelect { get; set; }

  bool Visible { get; set; }

  void MoveSelection(IObject iObject, SizeF ef2);
}
