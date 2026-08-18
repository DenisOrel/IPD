
// Type: Intermech.Redline.RedLineNoteTool
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Map;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Redline;

[Serializable]
public class RedLineNoteTool : MapTool
{
  private Redliner _redliner;
  private IMapRelative _relative;
  private MapRedNote _comment;

  internal RedLineNoteTool(Redliner redliner)
    : base(redliner.View)
  {
    this._redliner = redliner;
    this._relative = this._redliner.Relative;
  }

  /// <summary>сменить курсор перед работой</summary>
  public override void Start()
  {
    this.View.Cursor = Cursors.Cross;
    this.View.InitFocus();
  }

  /// <summary>уничтожить создаваемый объект и востановить курсор</summary>
  public override void Stop()
  {
    if (this._comment != null)
    {
      this._comment.Remove();
      this._comment = (MapRedNote) null;
    }
    this.View.Cursor = this.View.DefaultCursor;
  }

  /// <summary>действия когда клавиша мыши нажата</summary>
  public override void DoMouseDown()
  {
    if (this.LastInput.Buttons != MouseButtons.Left || this._comment != null)
      return;
    this._comment = new MapRedNote()
    {
      UseMillimeters = !this._redliner.UseUnitsConversion
    };
    this._comment.Pen = new Pen(this._redliner.PenColorAlpha, this._redliner.PenWidthInDrawingUnits);
    this._comment.Pen.DashStyle = DashStyle.Solid;
    this._comment.Brush = !(this._redliner.BrushColor != Color.Empty) ? (Brush) null : (Brush) new SolidBrush(this._redliner.BrushColorAlpha);
    this._comment.TextColor = this._redliner.TextColorAlpha;
    this._comment.FontName = this._redliner.FontName;
    this._comment.FontSize = this._redliner.UseUnitsConversion ? this._redliner.FontSize : this._redliner.FontSize / this.View.PixelsPerMM;
    this._comment.NoteStyle = this._redliner.NoteStyle.GetName<Intermech.Interfaces.IRedNoteStyle>().ToEnum<Intermech.Map.IRedNoteStyle>();
    this._comment.Facet = this._redliner.Facet;
    this._comment.NoteArrow = this._redliner.NoteArrow.GetName<Intermech.Interfaces.IRedArrowStyle>().ToEnum<Intermech.Map.IRedArrowStyle>();
    this._comment.ArrowSize = this._redliner.ArrowSize;
    this._comment.Text = "";
    this._comment.PlaceNote = this.LastInput.DocPoint;
    this._comment.NoteLocation = this.LastInput.DocPoint;
    this.View.Layers.Default.Add((MapObject) this._comment);
  }

  /// <summary>действия когда мышь двигают</summary>
  public override void DoMouseMove()
  {
    if (this._comment == null)
      return;
    this._comment.NoteLocation = this.LastInput.DocPoint;
  }

  /// <summary>действия когда клавиша мыши отпущена</summary>
  public override void DoMouseUp()
  {
    if (this.LastInput.Buttons != MouseButtons.Left || this._comment == null)
      return;
    this._comment.NoteLocation = this.LastInput.DocPoint;
    this.StartTransaction();
    if (this._relative != null)
    {
      this._comment.Relative = this._relative;
      this._comment.RelativeId = this._relative.GetId(this._comment.PlaceNote);
    }
    this._redliner.AddNewObject((MapObject) this._comment);
    this.TransactionResult = "New comment";
    this.StopTransaction();
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.View.EditObject((MapObject) this._comment);
    this._comment = (MapRedNote) null;
    this.TransactionResult = (string) null;
  }

  /// <summary>действия когда клавиша клавиатуры нажата</summary>
  public override void DoKeyDown()
  {
    if (this.LastInput.Key == Keys.Escape)
    {
      this.DoCancelMouse();
      this._redliner.OnChanged();
    }
    else
      base.DoKeyDown();
  }
}
