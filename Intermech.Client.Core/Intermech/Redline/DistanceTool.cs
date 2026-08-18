
// Type: Intermech.Redline.DistanceTool
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Map;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Redline;

/// <summary>линейка</summary>
[Serializable]
public class DistanceTool : MapTool
{
  /// <summary>высота текста </summary>
  private readonly float _fontSize = 10f;
  private Redliner _redliner;
  private MapText _text;
  private MapRedPencil _stroke;
  private float _scale;

  internal DistanceTool(Redliner redliner)
    : base(redliner.View)
  {
    this._redliner = redliner;
  }

  /// <summary>сменить курсор перед работой</summary>
  public override void Start()
  {
    this.View.Cursor = Cursors.Cross;
    this.View.InitFocus();
    this._text = new MapText();
    this._text.Multiline = true;
    this._text.AutoRescales = false;
    this.View.Layers.Default.Add((MapObject) this._text);
    this._text.TextColor = this._redliner.PenColor;
  }

  /// <summary>уничтожить создаваемый объект и востановить курсор</summary>
  public override void Stop()
  {
    if (this._text != null)
    {
      this._text.Remove();
      this._text = (MapText) null;
    }
    if (this._stroke != null)
    {
      this._stroke.Remove();
      this._stroke = (MapRedPencil) null;
    }
    this.View.Cursor = this.View.DefaultCursor;
  }

  /// <summary>действия когда клавиша мыши нажата</summary>
  public override void DoMouseDown()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return;
    if (this._stroke == null)
    {
      this._stroke = new MapRedPencil();
      this._stroke.Pen = new Pen(this._redliner.PenColorAlpha, 0.0f);
      this._stroke.Pen.DashStyle = DashStyle.Solid;
      this.View.Layers.Default.Add((MapObject) this._stroke);
      this._stroke.AddPoint(this.LastInput.DocPoint);
    }
    this._stroke.AddPoint(this.LastInput.DocPoint);
  }

  /// <summary>действия когда мышь двигают</summary>
  public override void DoMouseMove() => this.ChangeData(this.LastInput.DocPoint);

  private void ChangeData(PointF DocPoint)
  {
    float scale = this._scale;
    this._scale = this.View.DocScale;
    if (this._stroke != null)
    {
      if ((double) scale == (double) this._scale && !(this._stroke.GetLastPoint() != DocPoint))
        return;
      this._stroke.SetPoint(this._stroke.PointsCount - 1, DocPoint);
      this.SetDistance(DocPoint, this._stroke.CopyPointsArray());
    }
    else
    {
      if ((double) scale == (double) this._scale && !(this._text.Location != DocPoint))
        return;
      this._text.Location = DocPoint;
      this._text.FontSize = this._fontSize / this._scale;
      this._text.TextColor = this._redliner.PenColor;
      this._text.Text = $" x={DocPoint.X:0.00} y={DocPoint.Y:0.00}";
      this._text.Alignment = 0;
    }
  }

  private void SetDistance(PointF DocPoint, PointF[] Points)
  {
    this._text.Location = DocPoint;
    this._text.FontSize = this._fontSize / this.View.DocScale;
    this._text.TextColor = this._redliner.PenColor;
    double num1 = 0.0;
    double x = 0.0;
    double num2 = 0.0;
    double num3 = 0.0;
    double num4 = 0.0;
    for (int index = 1; index < Points.Length; ++index)
    {
      x = (double) Points[index].X - (double) Points[index - 1].X;
      num2 = (double) Points[index].Y - (double) Points[index - 1].Y;
      num1 += Math.Sqrt(x * x + num2 * num2);
      num3 = num4;
      num4 = Math.Atan2(-num2, x);
    }
    this._text.Alignment = x >= 0.0 || -num2 <= 0.0 ? 0 : 16 /*0x10*/;
    double num5 = num4 - num3 + Math.PI;
    string str1 = $" x={DocPoint.X:0.00} y={DocPoint.Y:0.00}";
    string.Format(LocalizationHolder.rm.GetString("Client.Core_1558"), (object) x, (object) -num2);
    string str2 = string.Format(LocalizationHolder.rm.GetString("Client.Core_1559"), (object) Math.Sqrt(x * x + num2 * num2));
    string str3 = string.Format(LocalizationHolder.rm.GetString("Client.Core_1560"), (object) num1);
    string str4 = string.Format(LocalizationHolder.rm.GetString("Client.Core_1561"), (object) (Math.Atan2(-num2, x) * (180.0 / Math.PI)));
    string str5 = string.Format(LocalizationHolder.rm.GetString("Client.Core_1562"), (object) Math.Abs(Math.Atan2(Math.Sin(num5), Math.Cos(num5)) * (180.0 / Math.PI)));
    if (Points.Length != 2)
      this._text.Text = $"{str2}{str5}\r\n{str1}\r\n{str3}\r\n";
    else
      this._text.Text = $"{str2}{str4}\r\n{str1}\r\n";
  }

  /// <summary>действия когда клавиша мыши отпущена</summary>
  public override void DoMouseUp()
  {
    int buttons = (int) this.LastInput.Buttons;
  }

  /// <summary>действия когда клавиша клавиатуры нажата</summary>
  public override void DoKeyDown()
  {
    if (this.LastInput.Key == Keys.Escape)
    {
      if (this._stroke != null)
      {
        this._stroke.Remove();
        this._stroke = (MapRedPencil) null;
        this.ChangeData(this.LastInput.DocPoint);
        return;
      }
      this.DoCancelMouse();
      this._redliner.OnChanged();
    }
    if (this.LastInput.Control)
    {
      if (this._stroke == null)
        return;
      int pointsCount = this._stroke.PointsCount;
      if (pointsCount <= 2)
        return;
      this._stroke.RemovePoint(pointsCount - 1);
      this._stroke.SetPoint(pointsCount - 2, this.LastInput.DocPoint);
      this.ChangeData(this.LastInput.DocPoint);
    }
    else
      base.DoKeyDown();
  }

  public override void DoMouseWheel()
  {
    this.View.DoWheel(this.LastInput);
    this.ChangeData(this.LastInput.DocPoint);
  }
}
