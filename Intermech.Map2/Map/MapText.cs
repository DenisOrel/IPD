// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapText
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Security;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapText : MapObject
    {
      public const int ChangedText = 1501;
      public const int ChangedFamilyName = 1502;
      public const int ChangedFontSize = 1503;
      public const int ChangedWrapping = 1520;
      public const int ChangedWrappingWidth = 1521;
      public const int ChangedGdiCharSet = 1522;
      public const int ChangedEditorStyle = 1523;
      public const int ChangedMinimum = 1524;
      public const int ChangedMaximum = 1525;
      public const int ChangedDropDownList = 1526;
      public const int ChangedChoices = 1527;
      public const int ChangedRightToLeft = 1528;
      public const int ChangedRightToLeftFromView = 1529;
      public const int ChangedBordered = 1530;
      public const int ChangedStringTrimming = 1531;
      public const int ChangedAlignment = 1504;
      public const int ChangedTextColor = 1505;
      public const int ChangedBackgroundColor = 1506;
      public const int ChangedTransparentBackground = 1507;
      public const int ChangedBold = 1508;
      public const int ChangedItalic = 1509;
      public const int ChangedUnderline = 1510;
      public const int ChangedStrikeThrough = 1511;
      public const int ChangedMultiline = 1512;
      public const int ChangedBackgroundOpaqueWhenSelected = 1515;
      public const int ChangedClipping = 1516;
      public const int ChangedAutoResizes = 1518;
      private const int flagTransparentBackground = 1;
      private const int flagBold = 2;
      private const int flagItalic = 4;
      private const int flagUnderline = 8;
      private const int flagStrikeThrough = 16 /*0x10*/;
      private const int flagMultiline = 32 /*0x20*/;
      private const int flagWrapping = 64 /*0x40*/;
      private const int flagClipping = 128 /*0x80*/;
      private const int flagAutoResizes = 256 /*0x0100*/;
      private const int flagDropDownList = 2048 /*0x0800*/;
      private const int flagBordered = 1048576 /*0x100000*/;
      private const byte DEFAULT_CHARSET = 1;
      private const int flagBackgroundOpaqueWhenSelected = 512 /*0x0200*/;
      private const int flagRightToLeft = 268435456 /*0x10000000*/;
      private const int flagRightToLeftFromView = 536870912 /*0x20000000*/;
      private const int flagUpdating = 1073741824 /*0x40000000*/;
      private const int maskEditorStyle = 61440 /*0xF000*/;
      private const int maskGdiCharSet = 16711680 /*0xFF0000*/;
      private const int maskStringTrimming = 251658240 /*0x0F000000*/;
      private int myAlignment;
      private Color myBackgroundColor;
      private ArrayList myChoices;
      private static string myDefaultFontName;
      private static float myDefaultFontSize;
      private float myMinimumFontSize;
      [NonSerialized]
      protected MapControl myEditor;
      protected static Bitmap myEmptyBitmap;
      private static readonly ArrayList myEmptyChoices;
      private string myFamilyName;
      [NonSerialized]
      private Font myFont;
      private float myFontSize;
      private int myInternalTextFlags;
      private static Font myLastFont;
      private int myMaximum;
      private int myMinimum;
      private static readonly char[] myNewlineArray = new char[2]
      {
        '\r',
        '\n'
      };
      [NonSerialized]
      protected int myNumLines;
      private string myString;
      [NonSerialized]
      private StringFormat myStringFormat;
      private Color myTextColor;
      private float myWrappingWidth;

      static MapText()
      {
        MapText.myDefaultFontName = "Microsoft Sans Serif";
        MapText.myDefaultFontSize = 10f;
        MapText.myLastFont = (Font) null;
        MapText.myEmptyChoices = ArrayList.FixedSize(new ArrayList());
        MapText.myEmptyBitmap = new Bitmap(10, 10);
      }

      public override void Dispose()
      {
        this.onCreateControl = (MapControl.CreateControlEdit) null;
        base.Dispose();
      }

      public MapText()
      {
        this.myMinimumFontSize = 10f;
        this.myString = "";
        this.myFamilyName = MapText.myDefaultFontName;
        this.myFontSize = MapText.myDefaultFontSize;
        this.myAlignment = 2;
        this.myTextColor = Color.Black;
        this.myBackgroundColor = Color.White;
        this.myInternalTextFlags = 537919745;
        this.myWrappingWidth = 150f;
        this.myMinimum = 0;
        this.myMaximum = 100;
        this.myChoices = MapText.myEmptyChoices;
        this.myStringFormat = (StringFormat) null;
        this.myFont = (Font) null;
        this.myEditor = (MapControl) null;
        this.myNumLines = 1;
        this.myEditor = (MapControl) null;
        this.ToolTipText = (string) null;
        this.InternalFlags &= -257;
        this.InternalFlags &= -17;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        sel.RemoveHandles((MapObject) this);
        if (this.BackgroundOpaqueWhenSelected)
        {
          bool skipsUndoManager = this.SkipsUndoManager;
          this.SkipsUndoManager = true;
          this.TransparentBackground = false;
          this.SkipsUndoManager = skipsUndoManager;
        }
        else
          base.AddSelectionHandles(sel, selectedObj);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1501:
            this.Text = (string) e.GetValue(undo);
            break;
          case 1502:
            this.FamilyName = (string) e.GetValue(undo);
            break;
          case 1503:
            this.FontSize = e.GetFloat(undo);
            break;
          case 1504:
            this.Alignment = e.GetInt(undo);
            break;
          case 1505:
            this.TextColor = (Color) e.GetValue(undo);
            break;
          case 1506:
            this.BackgroundColor = (Color) e.GetValue(undo);
            break;
          case 1507:
            this.TransparentBackground = (bool) e.GetValue(undo);
            break;
          case 1508:
            this.Bold = (bool) e.GetValue(undo);
            break;
          case 1509:
            this.Italic = (bool) e.GetValue(undo);
            break;
          case 1510:
            this.Underline = (bool) e.GetValue(undo);
            break;
          case 1511:
            this.StrikeThrough = (bool) e.GetValue(undo);
            break;
          case 1512:
            this.Multiline = (bool) e.GetValue(undo);
            break;
          case 1515:
            this.BackgroundOpaqueWhenSelected = (bool) e.GetValue(undo);
            break;
          case 1516:
            this.Clipping = (bool) e.GetValue(undo);
            break;
          case 1518:
            this.AutoResizes = (bool) e.GetValue(undo);
            break;
          case 1520:
            this.Wrapping = (bool) e.GetValue(undo);
            break;
          case 1521:
            this.WrappingWidth = e.GetFloat(undo);
            break;
          case 1522:
            this.GdiCharSet = e.GetInt(undo);
            break;
          case 1523:
            this.EditorStyle = (MapTextEditorStyle) e.GetInt(undo);
            break;
          case 1524:
            this.Minimum = e.GetInt(undo);
            break;
          case 1525:
            this.Maximum = e.GetInt(undo);
            break;
          case 1526:
            this.DropDownList = (bool) e.GetValue(undo);
            break;
          case 1527:
            this.Choices = (ArrayList) e.GetValue(undo);
            break;
          case 1528:
            this.RightToLeft = (bool) e.GetValue(undo);
            break;
          case 1530:
            this.Bordered = (bool) e.GetValue(undo);
            break;
          case 1531:
            this.StringTrimming = (StringTrimming) e.GetInt(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public virtual string ComputeEdit(string oldtext, string newtext) => newtext;

      protected virtual float computeHeight(Graphics g, Font font, float maxw)
      {
        string str1 = this.Text;
        float lineHeight = this.getLineHeight(font);
        if (str1.Length == 0)
        {
          this.myNumLines = 1;
          return lineHeight;
        }
        if (!this.Multiline)
        {
          int firstLineBreak = this.FindFirstLineBreak(str1, 0);
          if (firstLineBreak >= 0)
            str1 = str1.Substring(0, firstLineBreak);
        }
        StringFormat stringFormat = this.getStringFormat((MapView) null);
        float height = 0.0f;
        this.myNumLines = 0;
        int num1 = 0;
        int nextline = 0;
        bool flag = false;
        while (!flag)
        {
          int num2 = this.FindFirstLineBreak(str1, num1, ref nextline);
          if (num2 == -1)
          {
            num2 = str1.Length;
            flag = true;
          }
          if (num1 <= num2)
          {
            string str2 = str1.Substring(num1, num2 - num1);
            if (str2.Length > 0)
            {
              if (this.Wrapping)
              {
                SizeF area = new SizeF(maxw, 1E+09f);
                int lines = 0;
                SizeF sizeF = this.measureString(str2, g, font, stringFormat, area, out lines);
                height += sizeF.Height;
                this.myNumLines += lines;
              }
              else
              {
                height += lineHeight;
                ++this.myNumLines;
              }
            }
            else
            {
              height += lineHeight;
              ++this.myNumLines;
            }
          }
          num1 = nextline;
        }
        return height;
      }

      protected virtual float computeWidth(Graphics g, Font font)
      {
        string str = this.Text;
        if (str.Length == 0)
          return 0.0f;
        StringFormat genericTypographic = StringFormat.GenericTypographic;
        if (this.Multiline)
        {
          float width = 0.0f;
          int num1 = 0;
          bool flag = false;
          int nextline = 0;
          while (!flag)
          {
            int num2 = this.FindFirstLineBreak(str, num1, ref nextline);
            if (num2 == -1)
            {
              num2 = str.Length;
              flag = true;
            }
            float stringWidth = this.getStringWidth(str.Substring(num1, num2 - num1), g, font, genericTypographic);
            if (this.Wrapping && (double) stringWidth > (double) this.WrappingWidth)
              return this.WrappingWidth;
            if ((double) stringWidth > (double) width)
              width = stringWidth;
            num1 = nextline;
          }
          return width;
        }
        int firstLineBreak = this.FindFirstLineBreak(str, 0);
        if (firstLineBreak >= 0)
          str = str.Substring(0, firstLineBreak);
        float stringWidth1 = this.getStringWidth(str, g, font, genericTypographic);
        return this.Wrapping && (double) stringWidth1 > (double) this.WrappingWidth ? this.WrappingWidth : stringWidth1;
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapText mapText = (MapText) base.CopyObject(env);
        if (mapText != null)
          mapText.myEditor = (MapControl) null;
        return (MapObject) mapText;
      }

      public string ToolTipText { get; set; }

      public override string GetToolTip(MapView view) => this.ToolTipText;

      /// <summary>Событие создание Control для редактирования из указанного типа</summary>
      public event MapControl.CreateControlEdit onCreateControl;

      public override MapControl CreateEditor(MapView view)
      {
        MapControl editor = new MapControl();
        if (this.onCreateControl != null)
          editor.onCreateControl += this.onCreateControl;
        if (this.EditorStyle == MapTextEditorStyle.NumericUpDown)
        {
          editor.ControlType = typeof (MapText.NumericUpDownControl);
          RectangleF bounds = this.Bounds;
          bounds.X -= 2f;
          bounds.Y -= 2f;
          bounds.Width += 36f;
          bounds.Height += 8f;
          editor.Bounds = bounds;
          return editor;
        }
        if (this.EditorStyle == MapTextEditorStyle.ComboBox)
        {
          editor.ControlType = typeof (MapText.ComboBoxControl);
          RectangleF bounds = this.Bounds;
          bounds.X -= 2f;
          bounds.Y -= 2f;
          bounds.Width += 4f;
          bounds.Height += 4f;
          if (view != null)
          {
            StringFormat stringFormat = this.getStringFormat(view);
            float val1 = bounds.Width * view.DocScale;
            Graphics graphics = view.CreateGraphics();
            Font font1 = this.Font;
            float size = font1.Size * view.DocScale;
            Font font2 = this.makeFont(font1.Name, size, font1.Style);
            if (graphics != null)
            {
              foreach (string choice in this.Choices)
                val1 = Math.Max(val1, this.getStringWidth(choice, graphics, font2, stringFormat));
              graphics.Dispose();
            }
            font2.Dispose();
            float num = val1 + 30f;
            bounds.Width = num / view.DocScale;
          }
          editor.Bounds = bounds;
          return editor;
        }
        editor.ControlType = typeof (MapText.TextBoxControl);
        RectangleF bounds1 = this.Bounds;
        bounds1.X -= 2f;
        bounds1.Y -= 2f;
        bounds1.Width += 4f;
        bounds1.Height += 4f;
        if (this.Multiline || this.Wrapping)
          bounds1.Height += this.getLineHeight(this.Font) * 2f;
        if (!this.Wrapping)
        {
          switch (this.Alignment)
          {
            case 1:
              bounds1.X -= 15f;
              bounds1.Width += 30f;
              editor.Bounds = bounds1;
              return editor;
            case 2:
            case 3:
            case 16 /*0x10*/:
              if (this.isRightToLeft(view))
                bounds1.X -= 30f;
              bounds1.Width += 30f;
              if ((double) bounds1.Width < (double) bounds1.Height)
                bounds1.Width = 3f * bounds1.Height;
              editor.Bounds = bounds1;
              return editor;
            case 4:
            case 8:
              if (!this.isRightToLeft(view))
                bounds1.X -= 30f;
              bounds1.Width += 30f;
              editor.Bounds = bounds1;
              return editor;
            case 32 /*0x20*/:
              bounds1.X -= 15f;
              bounds1.Width += 30f;
              editor.Bounds = bounds1;
              return editor;
            case 64 /*0x40*/:
              if (!this.isRightToLeft(view))
                bounds1.X -= 30f;
              bounds1.Width += 30f;
              editor.Bounds = bounds1;
              return editor;
            case 128 /*0x80*/:
              bounds1.X -= 15f;
              bounds1.Width += 30f;
              editor.Bounds = bounds1;
              return editor;
            default:
              if (this.isRightToLeft(view))
                bounds1.X -= 30f;
              bounds1.Width += 30f;
              editor.Bounds = bounds1;
              return editor;
          }
        }
        else
        {
          switch (this.Alignment)
          {
            case 1:
              bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width / 2.0 - (double) this.WrappingWidth / 2.0 - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
            case 2:
            case 3:
            case 16 /*0x10*/:
              if (this.isRightToLeft(view))
                bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width - (double) this.WrappingWidth - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
            case 4:
            case 8:
              if (!this.isRightToLeft(view))
                bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width - (double) this.WrappingWidth - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
            case 32 /*0x20*/:
              bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width / 2.0 - (double) this.WrappingWidth / 2.0 - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
            case 64 /*0x40*/:
              if (!this.isRightToLeft(view))
                bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width - (double) this.WrappingWidth - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
            case 128 /*0x80*/:
              bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width / 2.0 - (double) this.WrappingWidth / 2.0 - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
            default:
              if (this.isRightToLeft(view))
                bounds1.X = (float) ((double) bounds1.X + (double) bounds1.Width - (double) this.WrappingWidth - 2.0);
              bounds1.Width = Math.Max(this.WrappingWidth + 4f, bounds1.Width);
              editor.Bounds = bounds1;
              return editor;
          }
        }
      }

      public override void DoBeginEdit(MapView view)
      {
        if (view == null)
          return;
        if (this.Editor != null)
          return;
        try
        {
          view.StartTransaction();
          this.RemoveSelectionHandles(view.Selection);
          this.myEditor = this.CreateEditor(view);
          this.Editor.EditedObject = (MapObject) this;
          view.EditControl = this.Editor;
          this.Editor.GetControl(view)?.Focus();
        }
        catch (SecurityException ex)
        {
          MapObject.Trace("MapText DoBeginEdit: " + ex.ToString());
          view.EditControl = (MapControl) null;
          this.myEditor = (MapControl) null;
          view.AbortTransaction();
        }
      }

      public virtual void DoEdit(MapView view, string oldtext, string newtext)
      {
        this.Text = this.ComputeEdit(oldtext, newtext);
      }

      public override void DoEndEdit(MapView view)
      {
        if (this.Editor == null)
          return;
        this.Editor.EditedObject = (MapObject) null;
        if (view != null)
          view.EditControl = (MapControl) null;
        this.myEditor = (MapControl) null;
        if (view == null)
          return;
        view.RaiseObjectEdited((MapObject) this);
        view.FinishTransaction("Text Edit");
      }

      protected virtual void drawString(
        string str,
        Graphics g,
        MapView view,
        Font font,
        Brush br,
        RectangleF rect,
        StringFormat fmt)
      {
        g.DrawString(str, font, br, rect, fmt);
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if ((double) shadowOffset.Width < 0.0)
          {
            rect.X += shadowOffset.Width;
            rect.Width -= shadowOffset.Width;
          }
          else
            rect.Width += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
          {
            rect.Y += shadowOffset.Height;
            rect.Height -= shadowOffset.Height;
          }
          else
            rect.Height += shadowOffset.Height;
        }
        MapObject.InflateRect(ref rect, Math.Max(rect.Height / 3f, 2f), 1f);
        return rect;
      }

      public int FindFirstLineBreak(string str, int start)
      {
        int nextline = 0;
        return this.FindFirstLineBreak(str, start, ref nextline);
      }

      protected int FindFirstLineBreak(string str, int start, ref int nextline)
      {
        int index = str.IndexOfAny(MapText.myNewlineArray, start);
        if (index >= 0)
        {
          if (str[index] == '\r' && index + 1 < str.Length && str[index + 1] == '\n')
          {
            nextline = index + 2;
            return index;
          }
          nextline = index + 1;
        }
        return index;
      }

      protected Font findLargestFont(Graphics g, RectangleF rect)
      {
        string name = this.Font.Name;
        FontStyle style = this.Font.Style;
        float size1;
        Font font;
        for (size1 = 10f; this.fitsInBox(g, font = this.makeFont(name, size1, style), rect); ++size1)
          font.Dispose();
        font.Dispose();
        Font largestFont;
        for (float size2 = size1 - 0.1f; !this.fitsInBox(g, largestFont = this.makeFont(name, size2, style), rect) && (double) size2 > 1.0; size2 -= 0.1f)
          largestFont.Dispose();
        return largestFont;
      }

      private bool fitsInBox(Graphics g, Font font, RectangleF rect)
      {
        float width = this.computeWidth(g, font);
        if ((double) rect.Width < (double) width)
          return false;
        float height = this.computeHeight(g, font, rect.Width);
        return (double) rect.Height >= (double) height;
      }

      protected float getLineHeight(Font font) => font.GetHeight();

      protected StringFormat getStringFormat(MapView view)
      {
        if (this.myStringFormat == null)
        {
          this.myStringFormat = new StringFormat(StringFormat.GenericTypographic);
          this.myStringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
        }
        this.myStringFormat.Trimming = this.StringTrimming;
        if (this.StringTrimming == StringTrimming.None)
          this.myStringFormat.FormatFlags &= ~StringFormatFlags.LineLimit;
        else
          this.myStringFormat.FormatFlags |= StringFormatFlags.LineLimit;
        switch (this.Alignment)
        {
          case 1:
            this.myStringFormat.Alignment = StringAlignment.Center;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
          case 2:
          case 3:
          case 16 /*0x10*/:
            this.myStringFormat.Alignment = StringAlignment.Near;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
          case 4:
          case 8:
            this.myStringFormat.Alignment = StringAlignment.Far;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
          case 32 /*0x20*/:
            this.myStringFormat.Alignment = StringAlignment.Center;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
          case 64 /*0x40*/:
            this.myStringFormat.Alignment = StringAlignment.Far;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
          case 128 /*0x80*/:
            this.myStringFormat.Alignment = StringAlignment.Center;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
          default:
            this.myStringFormat.Alignment = StringAlignment.Near;
            if (this.isRightToLeft(view))
              this.myStringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            else
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
            if (this.Wrapping)
              this.myStringFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
            else
              this.myStringFormat.FormatFlags |= StringFormatFlags.NoWrap;
            return this.myStringFormat;
        }
      }

      protected virtual float getStringWidth(string str, Graphics g, Font font, StringFormat fmt)
      {
        PointF origin = new PointF();
        return g.MeasureString(str, font, origin, fmt).Width;
      }

      public bool isRightToLeft(MapView view)
      {
        return this.RightToLeftFromView && view != null ? view.RightToLeft == System.Windows.Forms.RightToLeft.Yes : this.RightToLeft;
      }

      public virtual Font makeFont(string name, float size, FontStyle style)
      {
        byte gdiCharSet = (byte) this.GdiCharSet;
        return new Font(name, size, style, GraphicsUnit.Point, gdiCharSet);
      }

      protected virtual SizeF measureString(
        string str,
        Graphics g,
        Font font,
        StringFormat fmt,
        SizeF area,
        out int lines)
      {
        int charactersFitted = 0;
        return g.MeasureString(str, font, area, fmt, out charactersFitted, out lines);
      }

      protected override void OnBoundsChanged(RectangleF old)
      {
        base.OnBoundsChanged(old);
        SizeF size = this.Size;
        if ((double) old.Width == (double) size.Width && (double) old.Height == (double) size.Height)
          return;
        this.UpdateScale();
      }

      protected override void OnLayerChanged(MapLayer oldlayer, MapLayer newlayer, MapObject mainObj)
      {
        base.OnLayerChanged(oldlayer, newlayer, mainObj);
        if (oldlayer == null && newlayer != null)
          this.UpdateSize();
        if (this.Editor == null)
          return;
        MapView view = this.Editor.View;
        if (view == null)
          return;
        this.DoEndEdit(view);
      }

      public override bool OnSingleClick(MapInputEventArgs evt, MapView view)
      {
        if (!this.CanEdit() || !view.CanEditObjects() || evt.Shift || evt.Control)
          return false;
        this.DoBeginEdit(view);
        return true;
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.PaintGreek(g, view))
          return;
        RectangleF bounds = this.Bounds;
        if (!this.TransparentBackground)
        {
          if (this.Shadowed)
          {
            SizeF shadowOffset = this.GetShadowOffset(view);
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawRectangle(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
          Color backgroundColor = this.BackgroundColor;
          Brush brush = backgroundColor == Color.White ? MapShape.Brushes_White : (Brush) new SolidBrush(this.BackgroundColor);
          MapShape.DrawRectangle(g, view, (Pen) null, brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
          if (backgroundColor != Color.White)
            brush.Dispose();
        }
        string text = this.Text;
        if (this.Shadowed && this.TransparentBackground)
        {
          RectangleF rect = bounds;
          SizeF shadowOffset = this.GetShadowOffset(view);
          rect.X += shadowOffset.Width;
          rect.Y += shadowOffset.Height;
          if (this.Bordered)
          {
            Pen shadowPen = this.GetShadowPen(view, 1f);
            MapShape.DrawRectangle(g, view, shadowPen, (Brush) null, rect.X - 1f, rect.Y, rect.Width + 2f, rect.Height);
          }
          if (text.Length > 0)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            this.paintText(text, g, view, rect, shadowBrush);
          }
        }
        if (this.Bordered)
        {
          using (Pen pen = new Pen(Color.Silver))
            MapShape.DrawRectangle(g, view, pen, (Brush) null, bounds.X - 1f, bounds.Y, bounds.Width + 2f, bounds.Height);
        }
        Color textColor = this.TextColor;
        if (text.Length <= 0)
          return;
        Brush textbrush = textColor == Color.Black ? MapShape.Brushes_Black : (Brush) new SolidBrush(this.TextColor);
        this.paintText(text, g, view, bounds, textbrush);
        if (!(textColor != Color.Black))
          return;
        textbrush.Dispose();
      }

      public virtual bool PaintGreek(Graphics g, MapView view)
      {
        float docScale = view.DocScale;
        float paintNothingScale = view.PaintNothingScale;
        float paintGreekScale = view.PaintGreekScale;
        if (view.IsPrinting)
        {
          paintNothingScale /= 4f;
          paintGreekScale /= 4f;
        }
        float num1 = this.FontSize / 10f;
        float num2 = paintNothingScale / num1;
        float num3 = paintGreekScale / num1;
        if ((double) docScale > (double) num2)
        {
          if ((double) docScale > (double) num3)
            return false;
          RectangleF bounds = this.Bounds;
          using (Pen pen = new Pen(this.TextColor))
          {
            int lineCount = this.LineCount;
            float y = bounds.Y;
            float num4 = bounds.Height / (float) (lineCount + 1);
            for (int index = 0; index < lineCount; ++index)
            {
              y += num4;
              MapShape.DrawLine(g, view, pen, bounds.X, y, bounds.X + bounds.Width, y);
            }
          }
        }
        return true;
      }

      protected virtual void paintText(
        string str,
        Graphics g,
        MapView view,
        RectangleF rect,
        Brush textbrush)
      {
        if (str.Length == 0)
          return;
        Font font1 = this.Font;
        if (font1 == null)
          return;
        Font font2 = (Font) null;
        float lineHeight = this.getLineHeight(font1);
        bool clipping = this.Clipping;
        Region region1 = (Region) null;
        Region region2 = (Region) null;
        if (clipping)
        {
          region1 = g.Clip;
          region2 = new Region(rect);
          g.Clip = region2;
        }
        if (!this.Multiline)
        {
          int firstLineBreak = this.FindFirstLineBreak(str, 0);
          if (firstLineBreak >= 0)
            str = str.Substring(0, firstLineBreak);
        }
        StringFormat stringFormat = this.getStringFormat(view);
        if (view.IsPrinting)
        {
          font2 = this.findLargestFont(g, this.Bounds);
          font1 = font2;
        }
        float num1 = 0.0f;
        int num2 = 0;
        int nextline = -1;
        bool flag = false;
        while (!flag)
        {
          int num3 = this.FindFirstLineBreak(str, num2, ref nextline);
          if (num3 == -1)
          {
            num3 = str.Length;
            flag = true;
          }
          if (num2 <= num3)
          {
            string str1 = str.Substring(num2, num3 - num2);
            if (str1.Length > 0)
            {
              RectangleF rect1 = new RectangleF(rect.X, rect.Y + num1, rect.Width, rect.Height - num1);
              this.drawString(str1, g, view, font1, textbrush, rect1, stringFormat);
              if (this.Wrapping)
              {
                int lines = 0;
                SizeF sizeF = this.measureString(str1, g, font1, stringFormat, new SizeF(rect1.Width, rect1.Height), out lines);
                num1 += sizeF.Height;
              }
              else
                num1 += lineHeight;
            }
            else
              num1 += lineHeight;
          }
          num2 = nextline;
        }
        font2?.Dispose();
        if (clipping && region1 != null)
          g.Clip = region1;
        region2?.Dispose();
      }

      protected virtual void recalcBoundingRect()
      {
        lock (MapText.myEmptyBitmap)
        {
          using (Graphics g = Graphics.FromImage((Image) MapText.myEmptyBitmap))
          {
            g.PageUnit = GraphicsUnit.Pixel;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            float num = this.computeWidth(g, this.Font);
            if ((double) num < (double) this.myMinimumFontSize)
              num = this.myMinimumFontSize;
            float height = this.computeHeight(g, this.Font, num);
            if ((double) num == (double) this.Width && (double) height == (double) this.Height)
              return;
            this.SetSizeKeepingLocation(new SizeF(num, height));
          }
        }
      }

      public override void RemoveSelectionHandles(MapSelection sel)
      {
        if (this.BackgroundOpaqueWhenSelected)
        {
          bool skipsUndoManager = this.SkipsUndoManager;
          this.SkipsUndoManager = true;
          this.TransparentBackground = true;
          this.SkipsUndoManager = skipsUndoManager;
        }
        base.RemoveSelectionHandles(sel);
      }

      protected virtual void rescaleFont()
      {
        lock (MapText.myEmptyBitmap)
        {
          using (Graphics g = Graphics.FromImage((Image) MapText.myEmptyBitmap))
          {
            g.PageUnit = GraphicsUnit.Pixel;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            using (Font largestFont = this.findLargestFont(g, this.Bounds))
              this.FontSize = largestFont.Size;
          }
        }
      }

      private void ResetFont()
      {
        if (this.myFont == null)
          return;
        this.myFont = (Font) null;
      }

      public override void SetSizeKeepingLocation(SizeF s)
      {
        this.Bounds = this.SetRectangleSpotLocation(this.Bounds with
        {
          Width = s.Width,
          Height = s.Height
        }, this.Alignment, this.Location);
      }

      private Font shareFont(string name, float size, FontStyle style)
      {
        lock (typeof (MapText))
        {
          if (MapText.myLastFont != null && MapText.myLastFont.Name == name && (double) MapText.myLastFont.Size == (double) size && MapText.myLastFont.Style == style)
            return MapText.myLastFont;
          MapText.myLastFont = this.makeFont(name, size, style);
          return MapText.myLastFont;
        }
      }

      internal void UpdateScale()
      {
        if (!this.AutoRescales || this.Initializing || (this.InternalTextFlags & 1073741824 /*0x40000000*/) != 0)
          return;
        this.InternalTextFlags |= 1073741824 /*0x40000000*/;
        this.rescaleFont();
        this.InternalTextFlags &= -1073741825 /*0xBFFFFFFF*/;
      }

      internal void UpdateSize(bool force = false)
      {
        if (this.Initializing || (this.InternalTextFlags & 1073741824 /*0x40000000*/) != 0 || !force && !this.AutoResizes)
          return;
        this.InternalTextFlags |= 1073741824 /*0x40000000*/;
        this.recalcBoundingRect();
        this.InternalTextFlags &= -1073741825 /*0xBFFFFFFF*/;
      }

      private void UpdateSizeOrScale()
      {
        if (this.AutoResizes)
        {
          this.UpdateSize();
        }
        else
        {
          if (!this.AutoRescales)
            return;
          this.UpdateScale();
        }
      }

      [Category("Appearance")]
      [Description("The text alignment.")]
      [DefaultValue(2)]
      public virtual int Alignment
      {
        get => this.myAlignment;
        set
        {
          int alignment = this.myAlignment;
          if (alignment == value)
            return;
          this.myAlignment = value;
          this.Changed(1504, alignment, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [DefaultValue(true)]
      [Description("Whether the bounds are recalculated when the text changes.")]
      [Category("Behavior")]
      public virtual bool AutoResizes
      {
        get => (this.myInternalTextFlags & 256 /*0x0100*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 256 /*0x0100*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 256 /*0x0100*/;
          else
            this.myInternalTextFlags &= -257;
          this.Changed(1518, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The background color for this text object.")]
      [Category("Appearance")]
      public virtual Color BackgroundColor
      {
        get => this.myBackgroundColor;
        set
        {
          Color backgroundColor = this.myBackgroundColor;
          if (!(backgroundColor != value))
            return;
          this.myBackgroundColor = value;
          this.Changed(1506, 0, (object) backgroundColor, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether the text background is displayed when selected, and transparent when not selected")]
      [DefaultValue(false)]
      [Category("Behavior")]
      public virtual bool BackgroundOpaqueWhenSelected
      {
        get => (this.myInternalTextFlags & 512 /*0x0200*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 512 /*0x0200*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 512 /*0x0200*/;
          else
            this.myInternalTextFlags &= -513;
          this.Changed(1515, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(false)]
      [Description("Whether the font is bold.")]
      [Category("Appearance")]
      public virtual bool Bold
      {
        get => (this.myInternalTextFlags & 2) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 2) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 2;
          else
            this.myInternalTextFlags &= -3;
          this.ResetFont();
          this.Changed(1508, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Category("Appearance")]
      [DefaultValue(false)]
      [Description("Whether a simple border using the TextColor is drawn around the text.")]
      public virtual bool Bordered
      {
        get => (this.InternalFlags & 1048576 /*0x100000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1048576 /*0x100000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1048576 /*0x100000*/;
          else
            this.InternalFlags &= -1048577;
          this.Changed(1530, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The list of items presented in a drop-down list when editing")]
      [Category("Behavior")]
      public ArrayList Choices
      {
        get => this.myChoices == null ? MapText.myEmptyChoices : this.myChoices;
        set
        {
          ArrayList oldVal = this.myChoices != null ? this.myChoices : MapText.myEmptyChoices;
          ArrayList newVal = value ?? MapText.myEmptyChoices;
          if (oldVal == newVal)
            return;
          this.myChoices = newVal;
          this.Changed(1527, 0, (object) oldVal, MapObject.NullRect, 0, (object) newVal, MapObject.NullRect);
        }
      }

      [Description("Whether the text drawing is clipped by the bounds.")]
      [DefaultValue(false)]
      [Category("Appearance")]
      public virtual bool Clipping
      {
        get => (this.myInternalTextFlags & 128 /*0x80*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 128 /*0x80*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 128 /*0x80*/;
          else
            this.myInternalTextFlags &= -129;
          this.Changed(1516, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The initial font face name for newly constructed MapText objects.")]
      public static string DefaultFontFamilyName
      {
        get => MapText.myDefaultFontName;
        set
        {
          if (value == null)
            return;
          MapText.myDefaultFontName = value;
        }
      }

      [Description("The initial font size for newly constructed MapText objects.")]
      public static float DefaultFontSize
      {
        get => MapText.myDefaultFontSize;
        set
        {
          if ((double) value <= 0.0)
            return;
          MapText.myDefaultFontSize = value;
        }
      }

      [Description("The Minimum font size  is limited.")]
      public float MinimumFontSize
      {
        get => this.myMinimumFontSize;
        set
        {
          if ((double) value <= 0.0)
            return;
          this.myMinimumFontSize = value;
        }
      }

      [DefaultValue(false)]
      [Description("Whether the user is limited to values that are in the predefined list of Items.")]
      [Category("Behavior")]
      public bool DropDownList
      {
        get => (this.myInternalTextFlags & 2048 /*0x0800*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 2048 /*0x0800*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 2048 /*0x0800*/;
          else
            this.myInternalTextFlags &= -2049;
          this.Changed(1526, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override MapControl Editor => this.myEditor;

      [DefaultValue(0)]
      [Category("Behavior")]
      [Description("The kind of Control used when editing")]
      public MapTextEditorStyle EditorStyle
      {
        get => (MapTextEditorStyle) ((this.myInternalTextFlags & 61440 /*0xF000*/) >> 12);
        set
        {
          MapTextEditorStyle oldI = (MapTextEditorStyle) ((this.myInternalTextFlags & 61440 /*0xF000*/) >> 12);
          if (oldI == value)
            return;
          this.myInternalTextFlags = this.myInternalTextFlags & -61441 | (int) value << 12;
          this.Changed(1523, (int) oldI, (object) null, MapObject.NullRect, (int) value, (object) null, MapObject.NullRect);
        }
      }

      [Description("The font family face name.")]
      [Category("Appearance")]
      public virtual string FamilyName
      {
        get => this.myFamilyName;
        set
        {
          string newVal = value ?? MapText.DefaultFontFamilyName;
          string familyName = this.myFamilyName;
          if (!(familyName != newVal))
            return;
          this.myFamilyName = newVal;
          this.ResetFont();
          this.Changed(1502, 0, (object) familyName, MapObject.NullRect, 0, (object) newVal, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Browsable(false)]
      public Font Font
      {
        get
        {
          if (this.myFont == null)
          {
            FontStyle style = FontStyle.Regular;
            if (this.Bold)
              style |= FontStyle.Bold;
            if (this.Italic)
              style |= FontStyle.Italic;
            if (this.Underline)
              style |= FontStyle.Underline;
            if (this.StrikeThrough)
              style |= FontStyle.Strikeout;
            this.myFont = this.shareFont(this.FamilyName, this.FontSize, style);
          }
          return this.myFont;
        }
        set
        {
          if (value == null)
            return;
          this.Initializing = true;
          this.FamilyName = value.Name;
          this.FontSize = value.Size;
          this.Bold = (value.Style & FontStyle.Bold) != 0;
          this.Italic = (value.Style & FontStyle.Italic) != 0;
          this.Underline = (value.Style & FontStyle.Underline) != 0;
          this.StrikeThrough = (value.Style & FontStyle.Strikeout) != 0;
          this.GdiCharSet = (int) value.GdiCharSet;
          this.myFont = value;
          this.Initializing = false;
          this.UpdateSizeOrScale();
        }
      }

      [Category("Appearance")]
      [Description("The text font size, in points")]
      public virtual float FontSize
      {
        get => this.myFontSize;
        set
        {
          float fontSize = this.myFontSize;
          if ((double) value <= 0.0 || (double) fontSize == (double) value)
            return;
          this.myFontSize = value;
          this.ResetFont();
          this.Changed(1503, 0, (object) null, MapObject.MakeRect(fontSize), 0, (object) null, MapObject.MakeRect(value));
          this.UpdateSize();
        }
      }

      [Description("The GDI character set.")]
      [DefaultValue(1)]
      [Category("Appearance")]
      public virtual int GdiCharSet
      {
        get => (this.myInternalTextFlags & 16711680 /*0xFF0000*/) >> 16 /*0x10*/;
        set
        {
          int oldI = (this.myInternalTextFlags & 16711680 /*0xFF0000*/) >> 16 /*0x10*/;
          int newI = value & (int) byte.MaxValue;
          if (oldI == newI)
            return;
          this.myInternalTextFlags = this.myInternalTextFlags & -16711681 | newI << 16 /*0x10*/;
          this.ResetFont();
          this.Changed(1522, oldI, (object) null, MapObject.NullRect, newI, (object) null, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      private int InternalTextFlags
      {
        get => this.myInternalTextFlags;
        set => this.myInternalTextFlags = value;
      }

      [DefaultValue(false)]
      [Category("Appearance")]
      [Description("Whether the font is italic.")]
      public virtual bool Italic
      {
        get => (this.myInternalTextFlags & 4) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 4) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 4;
          else
            this.myInternalTextFlags &= -5;
          this.ResetFont();
          this.Changed(1509, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Category("Appearance")]
      [Description("How many lines of text are being displayed")]
      public virtual int LineCount => this.myNumLines;

      public override PointF Location
      {
        get => this.GetSpotLocation(this.Alignment);
        set => this.SetSpotLocation(this.Alignment, value);
      }

      [Description("The maximum value that the user can choose")]
      [Category("Behavior")]
      [DefaultValue(100)]
      public int Maximum
      {
        get => this.myMaximum;
        set
        {
          int maximum = this.myMaximum;
          if (maximum == value)
            return;
          this.myMaximum = value;
          this.Changed(1525, maximum, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [Description("The minimum value that the user can choose")]
      [DefaultValue(0)]
      public int Minimum
      {
        get => this.myMinimum;
        set
        {
          int minimum = this.myMinimum;
          if (minimum == value)
            return;
          this.myMinimum = value;
          this.Changed(1524, minimum, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("Whether the text will be displayed as multiple lines of text.")]
      [DefaultValue(false)]
      [Category("Appearance")]
      public virtual bool Multiline
      {
        get => (this.myInternalTextFlags & 32 /*0x20*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 32 /*0x20*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 32 /*0x20*/;
          else
            this.myInternalTextFlags &= -33;
          this.Changed(1512, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Description("Whether to draw text from right to left, when RightToLeftFromView is false")]
      [DefaultValue(false)]
      [Category("Appearance")]
      public virtual bool RightToLeft
      {
        get => (this.myInternalTextFlags & 268435456 /*0x10000000*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 268435456 /*0x10000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 268435456 /*0x10000000*/;
          else
            this.myInternalTextFlags &= -268435457 /*0xEFFFFFFF*/;
          this.Changed(1528, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Appearance")]
      [Description("Whether the view's RightToLeft property takes precedence over this text object's RightToLeft property")]
      public virtual bool RightToLeftFromView
      {
        get => (this.myInternalTextFlags & 536870912 /*0x20000000*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 536870912 /*0x20000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 536870912 /*0x20000000*/;
          else
            this.myInternalTextFlags &= -536870913 /*0xDFFFFFFF*/;
          this.Changed(1529, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(false)]
      [Description("Whether the font style includes a strike-through.")]
      [Category("Appearance")]
      public virtual bool StrikeThrough
      {
        get => (this.myInternalTextFlags & 16 /*0x10*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 16 /*0x10*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 16 /*0x10*/;
          else
            this.myInternalTextFlags &= -17;
          this.ResetFont();
          this.Changed(1511, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Description("How to trim text that does not fit.")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public virtual StringTrimming StringTrimming
      {
        get => (StringTrimming) ((this.myInternalTextFlags & 251658240 /*0x0F000000*/) >> 24);
        set
        {
          int oldI = (this.myInternalTextFlags & 251658240 /*0x0F000000*/) >> 24;
          int newI = (int) (value & (StringTrimming) 15);
          if (oldI == newI)
            return;
          this.myInternalTextFlags = this.myInternalTextFlags & -251658241 | newI << 24;
          this.ResetFont();
          this.Changed(1531, oldI, (object) null, MapObject.NullRect, newI, (object) null, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Description("The string that this text object displays.")]
      [DefaultValue("")]
      [Category("Appearance")]
      public virtual string Text
      {
        get => this.myString;
        set
        {
          bool flag = !string.IsNullOrEmpty(this.myString);
          string newVal = value ?? "";
          string oldVal = this.myString;
          if (!(oldVal != newVal))
            return;
          this.myString = newVal;
          this.Changed(1501, 0, (object) oldVal, MapObject.NullRect, 0, (object) newVal, MapObject.NullRect);
          if (flag)
            this.UpdateSize(true);
          else
            this.UpdateSizeOrScale();
        }
      }

      [Description("The color of the text.")]
      [Category("Appearance")]
      public virtual Color TextColor
      {
        get => this.myTextColor;
        set
        {
          Color textColor = this.myTextColor;
          if (!(textColor != value))
            return;
          this.myTextColor = value;
          this.Changed(1505, 0, (object) textColor, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [DefaultValue(true)]
      [Description("Whether the text is painted alone, or if the background is painted first.")]
      public virtual bool TransparentBackground
      {
        get => (this.myInternalTextFlags & 1) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 1) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 1;
          else
            this.myInternalTextFlags &= -2;
          this.Changed(1507, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether the font style includes an underline.")]
      [DefaultValue(false)]
      [Category("Appearance")]
      public virtual bool Underline
      {
        get => (this.myInternalTextFlags & 8) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 8) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 8;
          else
            this.myInternalTextFlags &= -9;
          this.ResetFont();
          this.Changed(1510, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [Category("Appearance")]
      [DefaultValue(false)]
      [Description("Whether the text is wrapped.")]
      public virtual bool Wrapping
      {
        get => (this.myInternalTextFlags & 64 /*0x40*/) != 0;
        set
        {
          bool oldVal = (this.myInternalTextFlags & 64 /*0x40*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.myInternalTextFlags |= 64 /*0x40*/;
          else
            this.myInternalTextFlags &= -65;
          this.Changed(1520, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSizeOrScale();
        }
      }

      [DefaultValue(150)]
      [Description("The width at which wrapping occurs, if Wrapping is true.")]
      [Category("Appearance")]
      public virtual float WrappingWidth
      {
        get => this.myWrappingWidth;
        set
        {
          float wrappingWidth = this.myWrappingWidth;
          if ((double) value <= 0.0 || (double) wrappingWidth == (double) value)
            return;
          this.myWrappingWidth = value;
          this.Changed(1521, 0, (object) null, MapObject.MakeRect(wrappingWidth), 0, (object) null, MapObject.MakeRect(value));
          this.UpdateSizeOrScale();
        }
      }

      internal sealed class ComboBoxControl : ComboBox, IMapControlObject
      {
        private MapControl myMapControl;
        private MapView myMapView;

        public ComboBoxControl()
        {
          this.myMapControl = (MapControl) null;
          this.myMapView = (MapView) null;
        }

        private void AcceptText()
        {
          MapControl mapControl = this.MapControl;
          if (mapControl == null)
            return;
          if (mapControl.EditedObject is MapText editedObject)
            editedObject.DoEdit(this.MapView, editedObject.Text, this.Text);
          mapControl.DoEndEdit(this.MapView);
        }

        private bool HandleKey(Keys key)
        {
          switch (key)
          {
            case Keys.Tab:
            case Keys.Return:
              this.AcceptText();
              this.MapView.InitFocus();
              return true;
            case Keys.Escape:
              this.MapControl?.DoEndEdit(this.MapView);
              this.MapView.InitFocus();
              return true;
            default:
              return false;
          }
        }

        protected override void OnLeave(EventArgs evt)
        {
          this.AcceptText();
          base.OnLeave(evt);
        }

        protected override bool ProcessDialogKey(Keys key)
        {
          return this.HandleKey(key) || base.ProcessDialogKey(key);
        }

        public MapControl MapControl
        {
          get => this.myMapControl;
          set
          {
            if (this.myMapControl == value)
              return;
            this.myMapControl = value;
            if (value == null || !(value.EditedObject is MapText editedObject))
              return;
            this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
            Font font = editedObject.Font;
            float size = font.Size;
            if (this.MapView != null)
              size *= this.MapView.DocScale;
            this.Font = editedObject.makeFont(font.Name, size, font.Style);
            foreach (object choice in editedObject.Choices)
              this.Items.Add(choice);
            if (!editedObject.Multiline)
            {
              int length = editedObject.Text.IndexOf("\r\n");
              if (length >= 0)
                this.Text = editedObject.Text.Substring(0, length);
              else
                this.Text = editedObject.Text;
            }
            else
              this.Text = editedObject.Text;
            if (editedObject.DropDownList)
              this.DropDownStyle = ComboBoxStyle.DropDownList;
            else
              this.DropDownStyle = ComboBoxStyle.DropDown;
          }
        }

        public MapView MapView
        {
          get => this.myMapView;
          set => this.myMapView = value;
        }
      }

      internal sealed class NumericUpDownControl : NumericUpDown, IMapControlObject
      {
        private MapControl myMapControl;
        private MapView myMapView;

        public NumericUpDownControl()
        {
          this.myMapControl = (MapControl) null;
          this.myMapView = (MapView) null;
        }

        private void AcceptText()
        {
          MapControl mapControl = this.MapControl;
          if (mapControl == null)
            return;
          if (mapControl.EditedObject is MapText editedObject)
            editedObject.DoEdit(this.MapView, editedObject.Text, this.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
          mapControl.DoEndEdit(this.MapView);
        }

        private bool HandleKey(Keys key)
        {
          switch (key)
          {
            case Keys.Tab:
            case Keys.Return:
              MapControl mapControl = this.MapControl;
              if (mapControl != null)
              {
                if (mapControl.EditedObject is MapText editedObject)
                  editedObject.DoEdit(this.MapView, editedObject.Text, this.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
                mapControl.DoEndEdit(this.MapView);
              }
              this.MapView.InitFocus();
              return true;
            case Keys.Escape:
              this.MapControl?.DoEndEdit(this.MapView);
              this.MapView.InitFocus();
              return true;
            default:
              return false;
          }
        }

        protected override void OnLeave(EventArgs evt)
        {
          this.AcceptText();
          base.OnLeave(evt);
        }

        protected override bool ProcessDialogKey(Keys key)
        {
          return this.HandleKey(key) || base.ProcessDialogKey(key);
        }

        public MapControl MapControl
        {
          get => this.myMapControl;
          set
          {
            if (this.myMapControl == value)
              return;
            this.myMapControl = value;
            if (value == null || !(value.EditedObject is MapText editedObject))
              return;
            this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
            Font font = editedObject.Font;
            float size = font.Size;
            if (this.MapView != null)
              size *= this.MapView.DocScale;
            this.Font = editedObject.makeFont(font.Name, size, font.Style);
            this.Minimum = (Decimal) editedObject.Minimum;
            this.Maximum = (Decimal) editedObject.Maximum;
            try
            {
              this.Value = Decimal.Parse(editedObject.Text, (IFormatProvider) CultureInfo.CurrentCulture);
            }
            catch (FormatException ex)
            {
              this.Value = this.Minimum;
            }
            catch (OverflowException ex)
            {
              this.Value = this.Minimum;
            }
          }
        }

        public MapView MapView
        {
          get => this.myMapView;
          set => this.myMapView = value;
        }
      }

      internal sealed class TextBoxControl : TextBox, IMapControlObject
      {
        private MapControl myMapControl;
        private MapView myMapView;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
          if (disposing)
          {
            this.myMapControl = (MapControl) null;
            this.myMapView = (MapView) null;
            this.TextChanged -= new EventHandler(this.TextBoxControl_TextChanged);
          }
          base.Dispose(disposing);
        }

        public TextBoxControl()
        {
          this.myMapControl = (MapControl) null;
          this.myMapView = (MapView) null;
          this.TextChanged += new EventHandler(this.TextBoxControl_TextChanged);
        }

        private void TextBoxControl_TextChanged(object sender, EventArgs e)
        {
          if (this.MapControl == null || this.MapView == null)
            return;
          if (this.MapControl.EditedObject is MapText && this.AcceptsReturn)
          {
            int length = this.Text.LastIndexOf("\r\n\r\n");
            if (length != -1 && length == this.Text.Length - 4)
            {
              this.Text = this.Text.Substring(0, length);
              this.AcceptText();
              this.MapView.InitFocus();
              return;
            }
          }
          System.Drawing.Size size = TextRenderer.MeasureText(this.Text + ".", this.Font);
          System.Drawing.Size view = this.MapView.ConvertDocToView(this.MapControl.Size);
          this.MapControl.Size = this.MapView.ConvertViewToDoc(new System.Drawing.Size(Math.Max(size.Width, view.Width), Math.Max(size.Height, view.Height)));
        }

        private void AcceptText()
        {
          MapControl mapControl = this.MapControl;
          if (mapControl == null)
            return;
          if (mapControl.EditedObject is MapText editedObject)
            editedObject.DoEdit(this.MapView, editedObject.Text, this.Text);
          mapControl.DoEndEdit(this.MapView);
        }

        private bool HandleKey(Keys key)
        {
          switch (key)
          {
            case Keys.Tab:
            case Keys.Return:
              if (key == Keys.Return && this.AcceptsReturn)
              {
                int length = this.Text.LastIndexOf("\r\n\r\n");
                if (length != this.Text.Length - 4)
                  return false;
                if (length != -1)
                  this.Text = this.Text.Substring(0, length);
              }
              this.AcceptText();
              this.MapView.InitFocus();
              return true;
            case Keys.Escape:
              MapControl mapControl = this.MapControl;
              MapView mapView = this.MapView;
              mapControl?.DoEndEdit(this.MapView);
              mapView.InitFocus();
              return true;
            default:
              return false;
          }
        }

        protected override void OnLeave(EventArgs evt)
        {
          this.AcceptText();
          base.OnLeave(evt);
        }

        protected override bool ProcessDialogKey(Keys key)
        {
          return this.HandleKey(key) || base.ProcessDialogKey(key);
        }

        public MapControl MapControl
        {
          get => this.myMapControl;
          set
          {
            if (this.myMapControl == value)
              return;
            this.myMapControl = value;
            if (value == null || !(value.EditedObject is MapText editedObject))
              return;
            if (!editedObject.Multiline)
            {
              int firstLineBreak = editedObject.FindFirstLineBreak(editedObject.Text, 0);
              if (firstLineBreak >= 0)
                this.Text = editedObject.Text.Substring(0, firstLineBreak);
              else
                this.Text = editedObject.Text;
            }
            else
              this.Text = editedObject.Text;
            switch (editedObject.Alignment)
            {
              case 1:
                this.TextAlign = HorizontalAlignment.Center;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font1 = editedObject.Font;
                float size1 = font1.Size;
                if (this.MapView != null)
                  size1 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font1.Name, size1, font1.Style);
                break;
              case 2:
              case 3:
              case 16 /*0x10*/:
                this.TextAlign = HorizontalAlignment.Left;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font2 = editedObject.Font;
                float size2 = font2.Size;
                if (this.MapView != null)
                  size2 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font2.Name, size2, font2.Style);
                break;
              case 4:
              case 8:
                this.TextAlign = HorizontalAlignment.Right;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font3 = editedObject.Font;
                float size3 = font3.Size;
                if (this.MapView != null)
                  size3 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font3.Name, size3, font3.Style);
                break;
              case 32 /*0x20*/:
                this.TextAlign = HorizontalAlignment.Center;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font4 = editedObject.Font;
                float size4 = font4.Size;
                if (this.MapView != null)
                  size4 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font4.Name, size4, font4.Style);
                break;
              case 64 /*0x40*/:
                this.TextAlign = HorizontalAlignment.Right;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font5 = editedObject.Font;
                float size5 = font5.Size;
                if (this.MapView != null)
                  size5 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font5.Name, size5, font5.Style);
                break;
              case 128 /*0x80*/:
                this.TextAlign = HorizontalAlignment.Center;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font6 = editedObject.Font;
                float size6 = font6.Size;
                if (this.MapView != null)
                  size6 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font6.Name, size6, font6.Style);
                break;
              default:
                this.TextAlign = HorizontalAlignment.Left;
                this.Multiline = editedObject.Multiline || editedObject.Wrapping;
                this.AcceptsReturn = editedObject.Multiline;
                this.WordWrap = editedObject.Wrapping;
                this.RightToLeft = editedObject.isRightToLeft(this.MapView) ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No;
                Font font7 = editedObject.Font;
                float size7 = font7.Size;
                if (this.MapView != null)
                  size7 *= this.MapView.DocScale;
                this.Font = editedObject.makeFont(font7.Name, size7, font7.Style);
                break;
            }
          }
        }

        public MapView MapView
        {
          get => this.myMapView;
          set => this.myMapView = value;
        }
      }
    }
}
