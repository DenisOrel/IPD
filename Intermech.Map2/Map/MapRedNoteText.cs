// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRedNoteText
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapRedNoteText : MapText
    {
      private readonly object _dictionaryLock = new object();
      private bool _isUpdateDictionary;
      /// <summary>коллекция: формула--&gt; изображение формулы </summary>
      private Dictionary<string, Image> _dictImage = new Dictionary<string, Image>();
      /// <summary>коллекция: формула--&gt;  реальный размер изображения</summary>
      private Dictionary<string, SizeF> _dictImageSize = new Dictionary<string, SizeF>();

      /// <summary>Событие создание коллекции формула -- изображение формулы</summary>
      public static event MapRedNoteText.CreateFormulaImages OnCreateFormulaImages;

      /// <summary>очистка графики формул</summary>
      private void ClearImages()
      {
        lock (this._dictionaryLock)
        {
          foreach (KeyValuePair<string, Image> keyValuePair in this._dictImage)
            keyValuePair.Value.Dispose();
          this._dictImage.Clear();
          this._dictImageSize.Clear();
          this._isUpdateDictionary = false;
        }
      }

      /// <summary>получить из коллекции элемент</summary>
      /// <param name="formula">изображение формулы</param>
      /// <returns>изображение формулы , реальный размер изображения</returns>
      private MapRedNoteText.ImageData GetImageData(string formula)
      {
        if (!this._isUpdateDictionary)
        {
          this.ClearImages();
          if (MapRedNoteText.OnCreateFormulaImages != null)
          {
            MapRedNoteText.Fragment.Split(this.Text, (Func<string, MapRedNoteText.ImageData>) (key =>
            {
              this._dictImage[key] = (Image) null;
              return (MapRedNoteText.ImageData) null;
            }));
            if (this._dictImage.Count != 0)
            {
              int num = MapRedNoteText.OnCreateFormulaImages(this.Font, this.TextColor, this.BackgroundColor, ref this._dictImage, ref this._dictImageSize) ? 1 : 0;
            }
          }
          this._isUpdateDictionary = true;
        }
        return new MapRedNoteText.ImageData()
        {
          Image = this._dictImage[formula],
          TotalSize = this._dictImageSize[formula]
        };
      }

      public override void Dispose()
      {
        this.ClearImages();
        base.Dispose();
      }

      public override string Text
      {
        get => base.Text;
        set
        {
          this._isUpdateDictionary = false;
          base.Text = value;
        }
      }

      public override Color TextColor
      {
        get => base.TextColor;
        set
        {
          this._isUpdateDictionary = false;
          base.TextColor = value;
        }
      }

      public override float FontSize
      {
        get => base.FontSize;
        set
        {
          this._isUpdateDictionary = false;
          base.FontSize = value;
        }
      }

      public override string FamilyName
      {
        get => base.FamilyName;
        set
        {
          this._isUpdateDictionary = false;
          base.FamilyName = value;
        }
      }

      public override Color BackgroundColor
      {
        get => base.BackgroundColor;
        set
        {
          this._isUpdateDictionary = false;
          base.BackgroundColor = value;
        }
      }

      public bool UseMillimeters { get; internal set; } = true;

      private void Draw(
        MapRedNoteText.Fragment item,
        Graphics g,
        MapView view,
        Font font,
        Brush br,
        RectangleF curRect,
        StringFormat fmt)
      {
        if (item.FontName != "")
        {
          using (Font font1 = this.makeFont(item.FontName, font.Size, font.Style))
            g.DrawString(item.Text, font1, br, curRect, fmt);
        }
        else if (item.Image != null)
        {
          SizeF size = this.UseMillimeters ? item.TotalSize : (this.View != null ? new SizeF(item.TotalSize.Width * this.View.PixelsPerMM, item.TotalSize.Height * this.View.PixelsPerMM) : new SizeF((SizeF) item.Image.Size));
          RectangleF rect = new RectangleF(curRect.Location, size);
          g.DrawImage(item.Image, rect);
        }
        else
          g.DrawString(item.Text, font, br, curRect, fmt);
      }

      private SizeF GetSize(
        MapRedNoteText.Fragment item,
        Graphics g,
        Font font,
        StringFormat fmt,
        SizeF area,
        out int lines)
      {
        lines = 1;
        int charactersFitted;
        if (item.FontName != "")
        {
          using (Font font1 = this.makeFont(item.FontName, font.Size, font.Style))
          {
            SizeF size = g.MeasureString(item.Text, font1, area, fmt, out charactersFitted, out lines);
            if (lines == 1)
              size.Height = Math.Max(size.Height, this.getLineHeight(font1));
            return size;
          }
        }
        if (item.Image != null)
        {
          if (this.UseMillimeters)
            return item.TotalSize;
          return this.View == null ? new SizeF((SizeF) item.Image.Size) : new SizeF(item.TotalSize.Width * this.View.PixelsPerMM, item.TotalSize.Height * this.View.PixelsPerMM);
        }
        SizeF size1 = g.MeasureString(item.Text, font, area, fmt, out charactersFitted, out lines);
        if (lines == 1)
          size1.Height = Math.Max(size1.Height, this.getLineHeight(font));
        return size1;
      }

      protected override float getStringWidth(string str, Graphics g, Font font, StringFormat fmt)
      {
        SizeF empty = SizeF.Empty;
        MapRedNoteText.Fragment[] array = MapRedNoteText.Fragment.Split(str, new Func<string, MapRedNoteText.ImageData>(this.GetImageData)).ToArray();
        float stringWidth = 0.0f;
        foreach (MapRedNoteText.Fragment fragment in array)
        {
          SizeF size = this.GetSize(fragment, g, font, fmt, empty, out int _);
          stringWidth += size.Width;
        }
        return stringWidth;
      }

      private void DrawString(
        string str,
        Graphics g,
        MapView view,
        Font font,
        Brush br,
        RectangleF rect,
        StringFormat fmt,
        float height)
      {
        MapRedNoteText.Fragment[] array = MapRedNoteText.Fragment.Split(str, new Func<string, MapRedNoteText.ImageData>(this.GetImageData)).ToArray();
        SizeF empty = SizeF.Empty;
        RectangleF rectangleF = rect;
        float num = this.getLineHeight(font);
        int lines;
        foreach (MapRedNoteText.Fragment fragment in array)
          num = Math.Max(this.GetSize(fragment, g, font, fmt, empty, out lines).Height, num);
        foreach (MapRedNoteText.Fragment fragment in array)
        {
          SizeF size = this.GetSize(fragment, g, font, fmt, empty, out lines);
          RectangleF curRect = new RectangleF(rectangleF.X, rectangleF.Y + (float) (((double) num - (double) size.Height) / 2.0), rectangleF.Width, num);
          this.Draw(fragment, g, view, font, br, curRect, fmt);
          float width = size.Width;
          rectangleF.X += width;
          rectangleF.Width -= width;
        }
      }

      protected override float computeWidth(Graphics g, Font font)
      {
        string text = this.Text;
        StringFormat genericTypographic = StringFormat.GenericTypographic;
        string[] separator = new string[2]{ "\r\n", "\n" };
        string[] strArray = text.Split(separator, StringSplitOptions.None);
        if (!this.Multiline)
          strArray = new string[1]{ strArray[0] };
        float width = 0.0f;
        foreach (string str in strArray)
        {
          float stringWidth = this.getStringWidth(str, g, font, genericTypographic);
          if (this.Wrapping && (double) stringWidth > (double) this.WrappingWidth)
            return this.WrappingWidth;
          if ((double) stringWidth > (double) width)
            width = stringWidth;
        }
        return width;
      }

      protected float getStringHeight(
        string str,
        Graphics g,
        Font font,
        StringFormat fmt,
        SizeF area,
        out int lines)
      {
        MapRedNoteText.Fragment[] array = MapRedNoteText.Fragment.Split(str, new Func<string, MapRedNoteText.ImageData>(this.GetImageData)).ToArray();
        int num = 1;
        float val2 = this.getLineHeight(font);
        foreach (MapRedNoteText.Fragment fragment in array)
          val2 = Math.Max(this.GetSize(fragment, g, font, fmt, area, out lines).Height, val2);
        lines = num;
        return val2;
      }

      protected override float computeHeight(Graphics g, Font font, float maxw)
      {
        string text = this.Text;
        StringFormat stringFormat = this.getStringFormat((MapView) null);
        string[] separator = new string[2]{ "\r\n", "\n" };
        string[] strArray = text.Split(separator, StringSplitOptions.None);
        if (!this.Multiline)
          strArray = new string[1]{ strArray[0] };
        SizeF area = new SizeF(maxw, 1E+09f);
        this.myNumLines = 0;
        float height = 0.0f;
        foreach (string str in strArray)
        {
          int lines;
          float stringHeight = this.getStringHeight(str, g, font, stringFormat, area, out lines);
          height += stringHeight;
          this.myNumLines += lines;
        }
        return height;
      }

      protected override void paintText(
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
        bool clipping = this.Clipping;
        Region region1 = (Region) null;
        Region region2 = (Region) null;
        if (clipping)
        {
          region1 = g.Clip;
          region2 = new Region(rect);
          g.Clip = region2;
        }
        StringFormat stringFormat = this.getStringFormat(view);
        double lineHeight = (double) this.getLineHeight(font1);
        Font font2 = (Font) null;
        if (view.IsPrinting)
        {
          font2 = this.findLargestFont(g, this.Bounds);
          font1 = font2;
        }
        string[] strArray = str.Split(new string[2]
        {
          "\r\n",
          "\n"
        }, StringSplitOptions.None);
        if (!this.Multiline)
          strArray = new string[1]{ strArray[0] };
        float num = 0.0f;
        foreach (string str1 in strArray)
        {
          RectangleF rect1 = new RectangleF(rect.X, rect.Y + num, rect.Width, rect.Height - num);
          float stringHeight = this.getStringHeight(str1, g, font1, stringFormat, rect1.Size, out int _);
          if (str1.Length > 0)
            this.DrawString(str1, g, view, font1, textbrush, rect1, stringFormat, stringHeight);
          num += stringHeight;
        }
        font2?.Dispose();
        if (clipping && region1 != null)
          g.Clip = region1;
        region2?.Dispose();
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
          Control control = this.Editor.GetControl(view);
          if (control == null)
            return;
          control.Update();
          control.Focus();
        }
        catch (SecurityException ex)
        {
          MapObject.Trace("MapText DoBeginEdit: " + (object) ex);
          view.EditControl = (MapControl) null;
          this.myEditor = (MapControl) null;
          view.AbortTransaction();
        }
      }

      public class ImageData
      {
        internal Image Image { get; set; }

        internal SizeF TotalSize { get; set; }

        internal ImageData()
        {
          this.Image = (Image) null;
          this.TotalSize = SizeF.Empty;
        }
      }

      [DebuggerDisplay("[{ImageData != null ? \"*\" : FontName}] = '{Text}'")]
      internal class Fragment
      {
        /// <summary>текст фрагмента</summary>
        internal string Text { get; set; }

        /// <summary>имя уникального фонта</summary>
        internal string FontName { get; set; }

        /// <summary>графика формулы</summary>
        internal Image Image { get; set; }

        internal SizeF TotalSize { get; set; }

        internal Fragment()
        {
          this.Text = "";
          this.FontName = "";
          this.Image = (Image) null;
          this.TotalSize = SizeF.Empty;
        }

        public override string ToString()
        {
          if (string.IsNullOrEmpty(this.FontName))
            return this.Text;
          return $"<text font='{this.FontName}' >{this.Text}</text>";
        }

        private static bool GetSymbol(string text, int nPos, out int startIndex, out int finishIndex)
        {
          startIndex = finishIndex = -1;
          if (string.IsNullOrEmpty(text))
            return false;
          int num1 = text.LastIndexOf("<<", nPos, nPos + 1, StringComparison.Ordinal);
          if (num1 <= -1 || text.LastIndexOf(">>", nPos, nPos, StringComparison.Ordinal) >= num1)
            return false;
          int num2 = text.IndexOf(">>", nPos, StringComparison.Ordinal);
          if (num2 <= -1)
            return false;
          int num3 = text.IndexOf("<<", nPos, StringComparison.Ordinal);
          if (num3 != -1 && num3 <= num2)
            return false;
          int num4 = num2 + 2;
          startIndex = num1;
          finishIndex = num4;
          return true;
        }

        internal static List<MapRedNoteText.Fragment> SplitText(string value)
        {
          List<MapRedNoteText.Fragment> fragmentList = new List<MapRedNoteText.Fragment>();
          if (string.IsNullOrEmpty(value))
            return fragmentList;
          string input = value.Replace("\r\n", "\n");
          while (input.Length != 0)
          {
            Match match1 = Regex.Match(input, "(?:<text)(\\b[^>]*)>(.*?)(?:</text>)");
            string str1 = match1.Success ? input.Substring(0, match1.Index) : input;
            input = input.Remove(0, str1.Length + match1.Length);
            string str2 = str1;
            while (str2.Length != 0)
            {
              int length = str2.IndexOf("\n", StringComparison.Ordinal);
              switch (length)
              {
                case -1:
                  fragmentList.Add(new MapRedNoteText.Fragment()
                  {
                    Text = str2
                  });
                  str2 = "";
                  continue;
                case 0:
                  fragmentList.Add(new MapRedNoteText.Fragment()
                  {
                    Text = "\r\n"
                  });
                  str2 = str2.Remove(0, length + 1);
                  continue;
                default:
                  fragmentList.Add(new MapRedNoteText.Fragment()
                  {
                    Text = str2.Substring(0, length)
                  });
                  goto case 0;
              }
            }
            if (match1.Success && match1.Groups[2].Length != 0)
            {
              Match match2 = Regex.Match(match1.Groups[1].Value, ".+?font[ ]*=[ ]*['\\\"\"](?<font>.+?)['\\\"\"]");
              string str3 = match2.Success ? match2.Groups["font"].Value : "";
              fragmentList.Add(new MapRedNoteText.Fragment()
              {
                FontName = str3,
                Text = match1.Groups[2].Value
              });
            }
          }
          return fragmentList;
        }

        internal static List<MapRedNoteText.Fragment> Split(
          string value,
          Func<string, MapRedNoteText.ImageData> imageFunc)
        {
          List<MapRedNoteText.Fragment> fragmentList = new List<MapRedNoteText.Fragment>();
          if (string.IsNullOrEmpty(value))
            return fragmentList;
          int startIndex = -1;
          int finishIndex = -1;
          string text = value.Replace("\r\n", "\n");
          while (text.Length != 0)
          {
            int nPos = 0;
            while (nPos < text.Length && !MapRedNoteText.Fragment.GetSymbol(text, nPos, out startIndex, out finishIndex))
              ++nPos;
            string str1 = startIndex > -1 ? text.Substring(0, startIndex) : text;
            text = text.Remove(0, str1.Length);
            fragmentList.AddRange((IEnumerable<MapRedNoteText.Fragment>) MapRedNoteText.Fragment.SplitText(str1));
            if (startIndex > -1)
            {
              string str2 = text.Substring(0, finishIndex - startIndex);
              text = text.Remove(0, str2.Length);
              MapRedNoteText.ImageData imageData = imageFunc(str2);
              if (imageData != null)
                fragmentList.Add(new MapRedNoteText.Fragment()
                {
                  Image = imageData.Image,
                  TotalSize = imageData.TotalSize,
                  Text = str2
                });
            }
          }
          return fragmentList;
        }
      }

      /// <summary>получить коллекцию формула -- изображение формулы</summary>
      /// <param name="font">используемый фонт</param>
      /// <param name="textColor">цвет формулы</param>
      /// <param name="backgroundColor">цвет подложки</param>
      /// <param name="dictImage">коллекция формула--&gt; изображение формулы </param>
      /// <param name="dictImageSize">коллекция формула--&gt; реальный размер изображения</param>
      /// <returns>есть ли формулы</returns>
      public delegate bool CreateFormulaImages(
        Font font,
        Color textColor,
        Color backgroundColor,
        ref Dictionary<string, Image> dictImage,
        ref Dictionary<string, SizeF> dictImageSize);
    }
}
