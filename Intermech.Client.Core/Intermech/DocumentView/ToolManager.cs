
// Type: Intermech.DocumentView.ToolManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// The tool, normally the default tool for a view, used to handle input and
/// decide if any other tools would be appropriate as the view's current tool.
/// </summary>
[Serializable]
public class ToolManager : AbstractTool
{
  [NonSerialized]
  private bool _started;

  /// <summary>The standard tool constructor.</summary>
  /// <param name="v"></param>
  public ToolManager(IView v)
    : base(v)
  {
    this._started = false;
  }

  /// <summary>
  /// Provide default behavior, when not running some other tool.
  /// </summary>
  /// <remarks>
  /// By default this handles:
  /// <list type="bullet">
  /// <item>Delete: the Delete key deletes the current selection</item>
  /// <item>Select All: Ctrl-A selects all selectable document objects</item>
  /// <item>Copy, Cut, Paste: The Ctrl-C, Ctrl-X, and Ctrl-V keys do the standard clipboard operations</item>
  /// <item>Edit: the F2 key starts in-place editing of the current node's text label</item>
  /// <item>PageDown, PageUp: The PageDown and PageUp keys scroll vertically; Shift-PageDown and Shift-PageUp
  /// scroll horizontally</item>
  /// <item>Home, End: the Home and End keys scroll to the left side and right sides of the document;
  /// Ctrl-Home and Ctrl-End scroll to the top-left and bottom-right corners of the document, respectively</item>
  /// <item>Undo, Redo: Ctrl-Z and Ctrl-Y perform undo and redo</item>
  /// <item>Escape: the Escape key cancels the current input operation</item>
  /// <item>letters and digits: selects the next node whose text starts with that character</item>
  /// </list>
  /// </remarks>
  public override void DoKeyDown()
  {
    InputEventArgs lastInput = this.LastInput;
    bool control = lastInput.Control;
    Keys key = lastInput.Key;
    if (key == Keys.Delete)
      this.View.EditDelete();
    else if (control && key == Keys.A)
      this.View.SelectAll();
    else if (control && key == Keys.C)
      this.View.EditCopy();
    else if (control && key == Keys.X)
      this.View.EditCut();
    else if (control && key == Keys.V)
    {
      this.View.EditPaste();
    }
    else
    {
      switch (key)
      {
        case Keys.Prior:
          if (lastInput.Shift)
          {
            this.View.ScrollPage(-1f, 0.0f);
            break;
          }
          this.View.ScrollPage(0.0f, -1f);
          break;
        case Keys.Next:
          if (lastInput.Shift)
          {
            this.View.ScrollPage(1f, 0.0f);
            break;
          }
          this.View.ScrollPage(0.0f, 1f);
          break;
        case Keys.End:
          RectangleF documentBounds1 = this.View.ComputeDocumentBounds();
          SizeF docExtentSize = this.View.DocExtentSize;
          PointF pointF = !control ? new PointF(documentBounds1.X + documentBounds1.Width - docExtentSize.Width, this.View.DocPosition.Y) : new PointF(documentBounds1.X + documentBounds1.Width - docExtentSize.Width, documentBounds1.Y + documentBounds1.Height - docExtentSize.Height);
          this.View.DocPosition = new PointF(Math.Max(0.0f, pointF.X), Math.Max(0.0f, pointF.Y));
          break;
        case Keys.Home:
          RectangleF documentBounds2 = this.View.ComputeDocumentBounds();
          if (control)
          {
            this.View.DocPosition = new PointF(documentBounds2.X, documentBounds2.Y);
            break;
          }
          this.View.DocPosition = new PointF(documentBounds2.X, this.View.DocPosition.Y);
          break;
        case Keys.F2:
          this.View.EditEdit();
          break;
        default:
          if (control && key == Keys.Z)
          {
            this.View.Undo();
            break;
          }
          if (control && key == Keys.Y)
          {
            this.View.Redo();
            break;
          }
          if (key == Keys.Escape)
          {
            if (this.View.CanSelectObjects())
              this.Selection.Clear();
            base.DoKeyDown();
            break;
          }
          bool flag = false;
          if (!control && !lastInput.Alt && this.View.SelectsByFirstChar)
          {
            string str = TypeDescriptor.GetConverter(typeof (Keys)).ConvertToString((ITypeDescriptorContext) null, CultureInfo.CurrentCulture, (object) lastInput.Key);
            char minValue = char.MinValue;
            if (str.Length == 1)
              minValue = str[0];
            else if (str.Length == 2 && str[0] == 'D')
              minValue = str[1];
            if (char.IsLetterOrDigit(minValue))
              flag = this.View.SelectNextNode(minValue);
          }
          if (flag)
            break;
          base.DoKeyDown();
          break;
      }
    }
  }

  /// <summary>
  /// Search <see cref="P:Intermech.Map.MapView.MouseDownTools" /> for the first tool that we can start;
  /// if we find one, we start it by making it the view's current <see cref="P:Intermech.Map.MapView.Tool" />.
  /// </summary>
  /// <remarks>
  /// This sets the <see cref="P:Intermech.Map.MapToolManager.Started" /> property to true if we did not find a startable
  /// tool, so that later searches for tools in the <see cref="M:Intermech.Map.MapToolManager.DoMouseMove" /> and
  /// <see cref="M:Intermech.Map.MapToolManager.DoMouseUp" /> methods can proceed.
  /// </remarks>
  public override void DoMouseDown()
  {
    foreach (object mouseDownTool in (IEnumerable) this.View.MouseDownTools)
    {
      if (mouseDownTool is ITool tool && tool.CanStart())
      {
        this.View.Tool = tool;
        return;
      }
    }
    this.Started = true;
  }

  /// <summary>
  /// When there are no other tools running, a mouse hover just invokes
  /// <see cref="M:Intermech.Map.MapView.DoHover(Intermech.Map.MapInputEventArgs)" />, which in turn raises <see cref="E:Intermech.Map.MapView.ObjectHover" />
  /// and <see cref="E:Intermech.Map.MapView.BackgroundHover" /> events.
  /// </summary>
  public override void DoMouseHover() => this.View.DoHover(this.LastInput);

  /// <summary>
  /// Search <see cref="P:Intermech.Map.MapView.MouseMoveTools" /> for the first tool that we can start;
  /// if we find one, we start it by making it the view's current <see cref="P:Intermech.Map.MapView.Tool" />.
  /// </summary>
  /// <remarks>
  /// This implementation does not do the search when <see cref="P:Intermech.Map.MapToolManager.Started" /> is false,
  /// presumably because of a mouse motion without a mouse down in this view.
  /// However, this method always calls <see cref="M:Intermech.Map.MapView.DoMouseOver(Intermech.Map.MapInputEventArgs)" /> when it does
  /// not find a startable tool, so that normal mouse-over behavior for tooltips and
  /// cursors happens when no other tool is running.
  /// </remarks>
  public override void DoMouseMove()
  {
    if (this.Started)
    {
      foreach (object mouseMoveTool in (IEnumerable) this.View.MouseMoveTools)
      {
        if (mouseMoveTool is ITool tool && tool.CanStart())
        {
          this.View.Tool = tool;
          return;
        }
      }
    }
    this.View.DoMouseOver(this.LastInput);
  }

  /// <summary>
  /// Search <see cref="P:Intermech.Map.MapView.MouseUpTools" /> for the first tool that we can start;
  /// if we find one, we start it by making it the view's current <see cref="P:Intermech.Map.MapView.Tool" />.
  /// </summary>
  public override void DoMouseUp()
  {
    if (!this.Started)
      return;
    foreach (object mouseUpTool in (IEnumerable) this.View.MouseUpTools)
    {
      if (mouseUpTool is ITool tool && tool.CanStart())
      {
        this.View.Tool = tool;
        break;
      }
    }
  }

  /// <summary>
  /// When there are no other tools running, a mouse wheel event scrolls or zooms
  /// the view by calling <see cref="M:Intermech.Map.MapView.DoWheel(Intermech.Map.MapInputEventArgs)" />.
  /// </summary>
  public override void DoMouseWheel() => this.View.DoWheel(this.LastInput);

  /// <summary>
  /// Set the <see cref="P:Intermech.Map.MapToolManager.Started" /> property to false.
  /// </summary>
  public override void Stop() => this.Started = false;

  /// <summary>
  /// Gets or sets whether we have performed a mouse down as part of a mouse down-move-up gesture.
  /// </summary>
  public bool Started
  {
    get => this._started;
    set => this._started = value;
  }
}
