// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.RtfInSiteEditorWrapper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model.PdfGenerator;
using Intermech.Document.Model.TypographicFont;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Класс-оболочка для редактора текста по месту TERN</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец редактора</param>
public class RtfInSiteEditorWrapper(TextData owner) : InSiteEditorWrapper(owner), IDisposable
{
  private RectangleF ownerBounds = RectangleElement.EmptyRectangleF;
  private RectangleF clientBounds = RectangleElement.EmptyRectangleF;
  private Rectangle winOwnerBounds = Rectangle.Empty;
  private Rectangle winClientBounds = Rectangle.Empty;
  [NonSerialized]
  private Image paintBuffer;
  private List<int> MaterialList;
  [NonSerialized]
  private int suspendLostFocusHandler;
  [NonSerialized]
  private bool editor_ModifiedHanlder_IsSuspended;
  private static float EditorTopMargin = 0.529166f;
  private static ImRtfEditor ternBufferForActualText = (ImRtfEditor) null;
  /// <summary>Язык по умолчанию</summary>
  protected const int DefLang = 1049;
  /// <summary>CharSet для шрифта по умолчанию</summary>
  protected const byte DefCharSet = 204;
  /// <summary>Разделитель имени в формуле</summary>
  public const char NameDivider = ':';
  /// <summary>Максимальная ширина текста в RTFEditor [дюймы]</summary>
  private const float MaxPageWidthInch = 40f;
  /// <summary>Максимальная ширина текста в RTFEditor [мм]</summary>
  private const float MaxTextWidth = 1016f;
  /// <summary>Требуется валидация вне зависимости от изменения текста</summary>
  public bool NeedValidate;
  /// <summary>Очередь враперов закэшировавших изображение в метафайле</summary>
  private static Queue<RtfInSiteEditorWrapper> paintBufferCache = new Queue<RtfInSiteEditorWrapper>(1200);
  /// <summary>Размер дополнительного поля снизу для текста, чтобы не отсекалась последняя строка подходящая вплотную к границе</summary>
  public float AdditionalBottomForText = 0.54f;

  /// <summary>Начать печать</summary>
  internal static void BeginPrint(ImRtfEditor ternPrintBuffer)
  {
    ternPrintBuffer?.TerSetPrintPreview(true);
  }

  /// <summary>Закончить печать</summary>
  internal static void EndPrint(ImRtfEditor ternPrintBuffer)
  {
    ternPrintBuffer?.TerSetPrintPreview(false);
  }

  /// <summary>Очищаем кеш от враперов</summary>
  /// <param name="doc">Документ враперы которого надо вычистить</param>
  internal static void ClearPaintCache(ImDocument doc)
  {
    Queue<RtfInSiteEditorWrapper> siteEditorWrapperQueue = new Queue<RtfInSiteEditorWrapper>(RtfInSiteEditorWrapper.paintBufferCache.Count);
    Queue<RtfInSiteEditorWrapper>.Enumerator enumerator = RtfInSiteEditorWrapper.paintBufferCache.GetEnumerator();
    while (enumerator.MoveNext())
    {
      RtfInSiteEditorWrapper current = enumerator.Current;
      if (current.Owner != null && current.Owner.OwnerDocument != null && current.Owner.OwnerDocument != doc)
        siteEditorWrapperQueue.Enqueue(current);
    }
    RtfInSiteEditorWrapper.paintBufferCache = siteEditorWrapperQueue;
  }

  /// <summary>Буфер изображения на экране</summary>
  protected virtual Image PaintBuffer
  {
    [DebuggerStepThrough] get => this.paintBuffer;
    set
    {
      if (this.paintBuffer == value)
        return;
      if (this.paintBuffer != null)
      {
        this.paintBuffer.Dispose();
      }
      else
      {
        if (RtfInSiteEditorWrapper.paintBufferCache.Count >= 500)
          RtfInSiteEditorWrapper.paintBufferCache.Dequeue().PaintBuffer = (Image) null;
        RtfInSiteEditorWrapper.paintBufferCache.Enqueue(this);
      }
      this.paintBuffer = value;
    }
  }

  private bool HasComplexDesignation
  {
    get
    {
      bool flag = false;
      string attributeValue = this.owner.GetAttributeValue(DocumentTreeNode.AttributeName_ComplexDesignation, false);
      if (attributeValue != null)
      {
        bool result = false;
        if (bool.TryParse(attributeValue, out result))
          flag = result;
      }
      return flag && this.owner.Text.Contains("|");
    }
  }

  /// <summary>В элементе имеются формулы</summary>
  private bool HasFormulas
  {
    get => this.Owner.CheckFlags((byte) 128 /*0x80*/) || this.HasComplexDesignation;
    set => this.Owner.SetFlags((byte) 128 /*0x80*/, value);
  }

  /// <summary>Создать вспомогательный редактор для отрисовки текста</summary>
  /// <returns></returns>
  public static ImRtfEditor CreateTernPaintBuffer()
  {
    ImRtfEditor tern = RtfInSiteEditorWrapper.CreateTern(new RectangleF(0.0f, 0.0f, 200f, 200f), new Rectangle(0, 0, 200, 200));
    tern.TerSetFlags(false, 134217728 /*0x08000000*/);
    tern.Name = "ternPaintBuffer";
    tern.draw.ResetBufBM();
    tern.TerSetFlags3(true, 262144 /*0x040000*/);
    tern.UseWindow = false;
    return tern;
  }

  public static ImRtfEditor CreateTernEditorBuffer()
  {
    ImRtfEditor tern = RtfInSiteEditorWrapper.CreateTern(new RectangleF(0.0f, 0.0f, 20f, 5f), new Rectangle(0, 0, 74, 17));
    tern.Name = "ternEditorBuffer";
    tern.TerSetFlags3(false, 262144 /*0x040000*/);
    tern.TerSetFlags(true, 128 /*0x80*/);
    tern.TerSetFlags3(true, 16 /*0x10*/);
    tern.TerSetFlags4(true, 1);
    tern.TerSetFlags5(true, 268435456 /*0x10000000*/);
    tern.TerSetFlags4(true, 256 /*0x0100*/);
    tern.ShowHyperlinkCursor = true;
    return tern;
  }

  public static ImRtfEditor CreateTernEditorBufferNotSpellCheck()
  {
    ImRtfEditor ternEditorBuffer = RtfInSiteEditorWrapper.CreateTernEditorBuffer();
    ternEditorBuffer.TerSetFlags4(false, 256 /*0x0100*/);
    return ternEditorBuffer;
  }

  /// <summary>Создать вспомогательный редактор для печати текста</summary>
  /// <returns></returns>
  internal static ImRtfEditor CreateTernPrintBuffer()
  {
    ImRtfEditor tern = RtfInSiteEditorWrapper.CreateTern(new RectangleF(0.0f, 0.0f, 200f, 200f), new Rectangle(0, 0, 200, 200));
    tern.TerSetFlags3(true, 262144 /*0x040000*/);
    tern.FullRenderMode = false;
    tern.UseWindow = false;
    tern.Name = "ternPrintBuffer";
    return tern;
  }

  /// <summary>Создать вспомогательный редактор для разбивки текста</summary>
  /// <returns></returns>
  internal static ImRtfEditor CreateTernDistributeBuffer()
  {
    ImRtfEditor tern = RtfInSiteEditorWrapper.CreateTern(new RectangleF(0.0f, 0.0f, 200f, 200f), new Rectangle(0, 0, 200, 200));
    tern.TerSetFlags(false, 134217728 /*0x08000000*/);
    tern.draw.ResetBufBM();
    tern.IsBackGroundEditor = true;
    tern.TerSetFlags3(true, 262144 /*0x040000*/);
    tern.FullRenderMode = false;
    tern.UseWindow = false;
    tern.Name = "ternDistributeBuffer";
    return tern;
  }

  /// <summary>Создать вспомогательный редактор для локальных нужд</summary>
  /// <returns></returns>
  internal static ImRtfEditor CreateTernBuffer(string name)
  {
    ImRtfEditor tern = RtfInSiteEditorWrapper.CreateTern(new RectangleF(0.0f, 0.0f, 200f, 200f), new Rectangle(0, 0, 200, 200));
    tern.TerSetFlags(false, 134217728 /*0x08000000*/);
    tern.draw.ResetBufBM();
    tern.IsBackGroundEditor = true;
    tern.TerSetFlags3(true, 262144 /*0x040000*/);
    tern.FullRenderMode = false;
    tern.UseWindow = false;
    tern.Name = name;
    tern.ShowHyperlinkCursor = true;
    return tern;
  }

  /// <summary>Конвертировать выравнивание по горизонтали в константы ImRtfEditor</summary>
  /// <param name="horzAlignment">Выравнивание по горизонтали</param>
  /// <returns>Константа ImRtfEditor</returns>
  protected int ConvertHorzAlignmentToTernConst(HorzAlignment? horzAlignment)
  {
    int ternConst = 1024 /*0x0400*/;
    if (horzAlignment.HasValue)
    {
      HorzAlignment? nullable = horzAlignment;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case HorzAlignment.Left:
            ternConst = 1024 /*0x0400*/;
            break;
          case HorzAlignment.Center:
            ternConst = 1;
            break;
          case HorzAlignment.Right:
            ternConst = 2;
            break;
          case HorzAlignment.Justify:
            ternConst = 2048 /*0x0800*/;
            break;
        }
      }
    }
    return ternConst;
  }

  /// <summary>Конвертировать выравнивание по вертикали в константы ImRtfEditor</summary>
  /// <param name="vertAlignment">Выравнивание по вертикали</param>
  /// <returns>Константа ImRtfEditor</returns>
  protected int ConvertVertAlignmentToTernConst(VertAlignment? vertAlignment)
  {
    int ternConst = 0;
    if (vertAlignment.HasValue)
    {
      VertAlignment? nullable = vertAlignment;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case VertAlignment.Top:
            ternConst = 0;
            break;
          case VertAlignment.Center:
            ternConst = 128 /*0x80*/;
            break;
          case VertAlignment.Bottom:
            ternConst = 256 /*0x0100*/;
            break;
        }
      }
    }
    return ternConst;
  }

  protected int ConvertVertAlignmentToPictAlign(PictAlignmentInText? vertAlignment)
  {
    int pictAlign = 0;
    if (vertAlignment.HasValue)
    {
      PictAlignmentInText? nullable = vertAlignment;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case PictAlignmentInText.Bottom:
            pictAlign = 0;
            break;
          case PictAlignmentInText.Center:
            pictAlign = 1;
            break;
          case PictAlignmentInText.Top:
            pictAlign = 2;
            break;
          case PictAlignmentInText.CustomBaseLine:
            pictAlign = 0;
            break;
        }
      }
    }
    return pictAlign;
  }

  /// <summary>Преобразовать стиль шрифта в константы ImRtfEditor</summary>
  /// <param name="style">Стиль шрифта</param>
  /// <returns>Константа ImRtfEditor соответсвующая стилю шрифта</returns>
  private int TernFontStyle(FontStyle style)
  {
    int num = 0;
    if ((style & FontStyle.Bold) != FontStyle.Regular)
      num |= 2;
    if ((style & FontStyle.Italic) != FontStyle.Regular)
      num |= 4;
    if ((style & FontStyle.Strikeout) != FontStyle.Regular)
      num |= 8;
    if ((style & FontStyle.Underline) != FontStyle.Regular)
      num |= 1;
    return num;
  }

  /// <summary>Получить размер дополнительного поля снизу для текста, в зависимости от настройки фиксированной высоты строки.
  /// Небходим чтобы не отсекалась последняя строка подходящая вплотную к границе ячейки
  /// </summary>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <returns></returns>
  public float GetAdditionalTextHeihgt(float fixedRowSize)
  {
    return (double) fixedRowSize == 0.0 ? this.AdditionalBottomForText : 0.25f * fixedRowSize;
  }

  /// <summary>Установить границы редактора</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="bounds">Границы в контрола</param>
  /// <param name="pageSize">Размер страницы</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="zoom">Масштаб в процентах</param>
  /// <param name="repaint">Обновить изображение</param>
  /// <param name="repage">Вызвать разбивку текста в редакторе после настройки</param>
  protected void SetEditorBounds(
    ImRtfEditor editor,
    Rectangle bounds,
    SizeF pageSize,
    float fixedRowSize,
    TextOrientation orientation,
    int zoom,
    bool repaint,
    bool repage)
  {
    if (editor == null)
      throw new ArgumentNullException(nameof (editor));
    bounds.Height += 2;
    if (editor.UseWindow)
      editor.Bounds = bounds;
    else
      editor.clientSizeBuffer = new Size?(bounds.Size);
    if (editor.ZoomPercent != zoom)
      editor.TerSetZoom(zoom);
    float mm = 0.0f;
    if (orientation == TextOrientation.DownTop || orientation == TextOrientation.TopDown)
    {
      pageSize = new SizeF(pageSize.Height, pageSize.Width);
    }
    else
    {
      mm = this.GetAdditionalTextHeihgt(fixedRowSize);
      pageSize.Height += mm;
      VertAlignment? vertAlignment1 = this.owner.ParagraphFormat.VertAlignment;
      VertAlignment vertAlignment2 = VertAlignment.Bottom;
      if (!(vertAlignment1.GetValueOrDefault() == vertAlignment2 & vertAlignment1.HasValue))
        mm = 0.0f;
    }
    SizeF inch = UnitsConverter.MmToInch(pageSize);
    editor.sec.TerSetSectPageSize(-1, inch.Width, inch.Height, false);
    editor.TerSetMarginEx(-1, 0, 0, 0, UnitsConverter.MmToTwips(mm), 0, 0, false);
    if (!(!repaint & repage))
      return;
    editor.TerRepaginate(false);
  }

  /// <summary>Форматировать текст в активном редакторе</summary>
  public override void FormatEditorText()
  {
    if (this.owner == null || this.Editor == null)
      return;
    this.FormatEditorText(this.Editor, this.owner.ParagraphFormat, this.owner.Orientation, this.owner.CharFormat, this.owner.GetBackColor(), this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, true, (DrawContextWithUI) null);
  }

  /// <summary>Установить в редакторе формат символов по умолчанию</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="charFormat">Формат символов</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="bColor">Цвет фона</param>
  /// <param name="isRTF">Содержит RTF</param>
  /// <param name="forceApplyCharStyle">Принудительно применять все настройки к стилю</param>
  protected void SetDefaultCharFormat(
    ImRtfEditor editor,
    CharFormat charFormat,
    TextOrientation orientation,
    Color bColor,
    bool isRTF,
    bool forceApplyCharStyle,
    DrawContextWithUI context)
  {
    if (editor == null)
      return;
    int styleId = editor.par.TerGetStyleId("Default Paragraph Font");
    if (isRTF)
      editor.par.EditStyle(true, styleId, "Default Paragraph Font", false, 1, false, false);
    Color? nullable = charFormat.TextColor;
    Color foreColor;
    if (nullable.HasValue)
    {
      nullable = charFormat.TextColor;
      foreColor = nullable.Value;
    }
    else
      foreColor = this.owner.GetForeColor();
    float? fontSize = charFormat.FontSize;
    float num;
    if (fontSize.HasValue)
    {
      fontSize = charFormat.FontSize;
      num = fontSize.Value;
    }
    else
    {
      fontSize = TextData.DefaultCharFormat.FontSize;
      num = fontSize.Value;
    }
    nullable = charFormat.UnderlineColor;
    Color clrAuto;
    if (!nullable.HasValue)
    {
      clrAuto = tc.CLR_AUTO;
    }
    else
    {
      nullable = charFormat.UnderlineColor;
      clrAuto = nullable.Value;
    }
    Color NewULineColor = clrAuto;
    nullable = charFormat.TextBkColor;
    Color NewTextBkColor;
    if (nullable.HasValue)
    {
      nullable = charFormat.TextBkColor;
      NewTextBkColor = nullable.Value;
    }
    else
      NewTextBkColor = bColor;
    int NewTextAngle = 0;
    switch (orientation)
    {
      case TextOrientation.DownTop:
        NewTextAngle = 90;
        break;
      case TextOrientation.TopDown:
        NewTextAngle = 270;
        break;
    }
    int font3 = editor.fnt.TerCreateFont3(-1, true, charFormat.FontFamily, -Convert.ToInt32(num * 20f), (int) charFormat.CharStyle, foreColor, NewTextBkColor, NewULineColor, 0, 0, styleId, 0, 0, 204, 1049, NewTextAngle);
    editor.SetStyleParamsFromFont(0, font3);
    editor.SetStyleParamsFromFont(styleId, font3);
    if (font3 != 0)
      editor.ReplaceDefaultFont(font3);
    editor.InputFontId = 0;
    if (!isRTF)
      return;
    editor.par.EditStyle(false, styleId, "Default Paragraph Font", false, 1, false, false, forceApplyCharStyle);
  }

  /// <summary>Получить CharFormat из редактора для всего выделенного текста</summary>
  /// <param name="editor"></param>
  /// <returns></returns>
  internal CharFormat GetDefaultCharFormat(ImRtfEditor editor)
  {
    CharFormat defaultCharFormat = new CharFormat();
    int curFont = editor.TerGetCurFont(0, 0);
    string TypeFace;
    int PointSize;
    int style;
    editor.GetFontInfo(curFont, out TypeFace, out PointSize, out style);
    defaultCharFormat.FontFamily = TypeFace;
    defaultCharFormat.CharStyle = (CharStyle) style;
    defaultCharFormat.FontSize = new float?((float) PointSize);
    Color TextColor;
    Color ulineColor;
    editor.TerGetTextColor(curFont, out TextColor, out Color _, out ulineColor);
    defaultCharFormat.TextBkColor = new Color?();
    defaultCharFormat.TextColor = new Color?(TextColor);
    defaultCharFormat.UnderlineColor = new Color?(ulineColor);
    return defaultCharFormat;
  }

  /// <summary>Обновляем CharFormat для элемента из редактора</summary>
  internal void CheckCharFormat()
  {
    if (!this.EditorActive || !this.owner.InPlaceEditorActive || !(this.owner is TextBoxElement owner))
      return;
    CharFormat defaultCharFormat = this.GetDefaultCharFormat(this.Editor);
    CharFormat charFormat1 = owner.CharFormat.Clone();
    float? fontSize1 = defaultCharFormat.FontSize;
    float? fontSize2 = charFormat1.FontSize;
    if (!((double) fontSize1.GetValueOrDefault() == (double) fontSize2.GetValueOrDefault() & fontSize1.HasValue == fontSize2.HasValue))
      charFormat1.FontSize = defaultCharFormat.FontSize;
    if (defaultCharFormat.CharStyle != charFormat1.CharStyle)
      charFormat1.CharStyle = defaultCharFormat.CharStyle;
    if (defaultCharFormat.FontFamily != charFormat1.FontFamily)
      charFormat1.FontFamily = defaultCharFormat.FontFamily;
    Color? nullable1 = defaultCharFormat.TextColor;
    Color foreColor = owner.GetForeColor();
    if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != foreColor ? 1 : 0) : 0) : 1) != 0)
    {
      charFormat1.TextColor = defaultCharFormat.TextColor;
    }
    else
    {
      CharFormat charFormat2 = charFormat1;
      nullable1 = new Color?();
      Color? nullable2 = nullable1;
      charFormat2.TextColor = nullable2;
    }
    nullable1 = defaultCharFormat.UnderlineColor;
    Color clrAuto = tc.CLR_AUTO;
    if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != clrAuto ? 1 : 0) : 0) : 1) != 0)
      charFormat1.FontSize = defaultCharFormat.FontSize;
    owner.SetCharFormat(charFormat1, false, false);
  }

  /// <summary>Установить позицию текста</summary>
  /// <param name="position">Позиция, -1 в конец документа</param>
  /// <param name="repaint">Перерисовать</param>
  public void SetTextPosition(int position, bool repaint)
  {
    if (this.Editor == null)
      return;
    this.Editor.SetTerCursorPos(position, -1, repaint);
  }

  /// <summary>Вставить гиперссылку в текущую позицию</summary>
  /// <param name="text">Текст</param>
  /// <param name="code">Код</param>
  /// <param name="repaint">Перерисовать</param>
  public void InsertHyperLink(string text, string code, bool repaint)
  {
    if (this.Editor == null)
      return;
    code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
    this.Editor.TerInsertHyperlink(text, code, 0, repaint);
  }

  /// <summary>Удалить гиперссылку в текущей позиции</summary>
  /// <param name="repaint">Перерисовать</param>
  public void DeleteHyperLink(bool repaint)
  {
    if (this.Editor == null)
      return;
    this.Editor.TerDeleteHypertext(-1, -1, repaint);
  }

  /// <summary>Установить направление текста в редакторе</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="orientation">Направление</param>
  /// <param name="keepText">Сохранять текст. Если false, то текст теряется</param>
  /// <returns></returns>
  internal int SetTextOrientation(ImRtfEditor editor, TextOrientation orientation, bool keepText)
  {
    if (editor == null || !keepText && orientation.IsHorizontalText())
      return -1;
    editor.TextOrientation = (int) orientation;
    int FrameNo = -1;
    if (keepText)
    {
      SelectionBlock selectionBlock = editor.GetSelectionBlock();
      string shortRtf = editor.GetShortRtf();
      int firstTextPos = -1;
      if (this.owner is TextBoxElement owner)
        firstTextPos = owner.StartCharIndex;
      this.SetupEditor(editor, shortRtf, true, firstTextPos, this.owner.ParagraphFormat, orientation, this.owner.CharFormat, this.owner.GetBackColor(), this.clientBounds, editor.Bounds, this.owner.Margins, (float) editor.ZoomPercent / 100f, this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, false, out List<int> _, (DrawContextWithUI) null);
      editor.RestoreSelection(selectionBlock, false);
    }
    else
    {
      editor.RotatedFrame = true;
      ParagraphFormat paragraphFormat = this.owner.ParagraphFormat;
      int align = 0;
      if (paragraphFormat != null)
      {
        VertAlignment? vertAlignment = paragraphFormat.VertAlignment;
        if (vertAlignment.HasValue)
        {
          vertAlignment = paragraphFormat.VertAlignment;
          align = this.ConvertVertAlignmentToTernConst(new VertAlignment?(vertAlignment.Value));
        }
      }
      if (orientation.IsHorizontalText())
        editor.TerSetSectAlign(-1, align, false);
      else
        editor.TerSetSectAlign(-1, 0, false);
      editor.VertAlignment = align;
      FrameNo = editor.TerInsertParaFrame(0, 0, Convert.ToInt32(editor.TerSect[0].PprHeight * 1440f), Convert.ToInt32(editor.TerSect[0].PprWidth * 1440f), false, true);
      editor.TerPosFrame(FrameNo, 0, false);
      int direction = orientation == TextOrientation.DownTop ? 2 : 1;
      editor.TerRotateFrameText(false, -FrameNo, direction, false);
    }
    return FrameNo;
  }

  /// <summary>Установить форматирование параграфа по умолчанию</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="textFormat">Формат</param>
  /// <param name="orientation">Направление текста</param>
  /// <param name="fixedRowSize">Фиксированный размер строки, 0 если нефиксирован</param>
  protected void SetDefaultParagraphFormat(
    ImRtfEditor editor,
    ParagraphFormat textFormat,
    TextOrientation orientation,
    float fixedRowSize)
  {
    if (editor == null)
      return;
    SelectionBlock selectionBlock = editor.GetSelectionBlock();
    editor.par.EditStyle(true, 0, "Normal", false, 2, true, false);
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    int num7 = 0;
    int num8 = 0;
    int num9 = 0;
    VertAlignment? vertAlignment;
    if (textFormat != null)
    {
      if (textFormat.HorzAlignment.HasValue)
        num1 = this.ConvertHorzAlignmentToTernConst(new HorzAlignment?(textFormat.HorzAlignment.Value));
      if (textFormat.VertAlignment.HasValue)
      {
        int num10 = num1;
        vertAlignment = textFormat.VertAlignment;
        int ternConst = this.ConvertVertAlignmentToTernConst(new VertAlignment?(vertAlignment.Value));
        num1 = num10 | ternConst;
      }
      if (textFormat.KeepTogether.HasValue && textFormat.KeepTogether.Value)
        num1 |= 16384 /*0x4000*/;
      if (textFormat.KeepWithNext.HasValue && textFormat.KeepWithNext.Value)
        num1 |= 32768 /*0x8000*/;
      if (textFormat.DisableFloatLines.HasValue && textFormat.DisableFloatLines.Value)
        num2 |= 32 /*0x20*/;
      if (textFormat.DisableWordWrap.HasValue && textFormat.DisableWordWrap.Value)
        num2 |= 16 /*0x10*/;
      if (textFormat.IdentFirstLine.HasValue)
        num3 = UnitsConverter.MmToTwips(textFormat.IdentFirstLine.Value * 10f);
      if (textFormat.IdentLeft.HasValue)
        num4 = UnitsConverter.MmToTwips(textFormat.IdentLeft.Value * 10f);
      if (textFormat.IdentRight.HasValue)
        num5 = UnitsConverter.MmToTwips(textFormat.IdentRight.Value * 10f);
      if ((double) fixedRowSize == 0.0)
      {
        if (textFormat.IntervalAfter.HasValue)
          num6 = Convert.ToInt32(textFormat.IntervalAfter.Value * 20f);
        if (textFormat.IntervalBefore.HasValue)
          num7 = Convert.ToInt32(textFormat.IntervalBefore.Value * 20f);
        if (textFormat.LineSpacingMethod.HasValue)
        {
          LineSpacingMethod? lineSpacingMethod = textFormat.LineSpacingMethod;
          if (lineSpacingMethod.HasValue)
          {
            switch (lineSpacingMethod.GetValueOrDefault())
            {
              case LineSpacingMethod.InPercents:
                num9 = !textFormat.SpaceBetweenLines.HasValue ? 0 : Convert.ToInt32(textFormat.SpaceBetweenLines.Value - 100f);
                break;
              case LineSpacingMethod.Ratio_1:
                num9 = 0;
                break;
              case LineSpacingMethod.Ratio_1_5:
                num9 = 50;
                break;
              case LineSpacingMethod.Ratio_2:
                num9 = 100;
                break;
              case LineSpacingMethod.AtLeast:
                num8 = !textFormat.SpaceBetweenLines.HasValue ? 0 : Convert.ToInt32(textFormat.SpaceBetweenLines.Value * 20f);
                break;
              case LineSpacingMethod.Exact:
                num8 = !textFormat.SpaceBetweenLines.HasValue ? 0 : -Convert.ToInt32(textFormat.SpaceBetweenLines.Value * 20f);
                break;
              case LineSpacingMethod.ExactMM:
                num8 = !textFormat.SpaceBetweenLines.HasValue ? 0 : -(int) Math.Truncate((double) textFormat.SpaceBetweenLines.Value * 56.692913055419922);
                break;
              case LineSpacingMethod.Ratio:
                num9 = !textFormat.SpaceBetweenLines.HasValue ? 0 : Convert.ToInt32((float) ((double) textFormat.SpaceBetweenLines.Value * 100.0 - 100.0));
                break;
            }
          }
        }
      }
      else
        num8 = -(int) Math.Truncate((double) fixedRowSize * 56.692913055419922);
    }
    else if ((double) fixedRowSize != 0.0)
      num8 = -(int) Math.Truncate((double) fixedRowSize * 56.692913055419922);
    if (num1 == 0)
      num1 = 1024 /*0x0400*/;
    editor.StyleId[0].ParaFlags = num1;
    editor.StyleId[0].pflags = num2;
    editor.StyleId[0].FirstIndentTwips = num3;
    editor.StyleId[0].LeftIndentTwips = num4;
    editor.StyleId[0].RightIndentTwips = num5;
    editor.StyleId[0].SpaceBefore = num7;
    editor.StyleId[0].SpaceAfter = num6;
    editor.StyleId[0].SpaceBetween = num8;
    editor.StyleId[0].LineSpacing = num9;
    editor.par.EditStyle(false, 0, "Normal", false, 2, true, false, false);
    int align = 0;
    vertAlignment = textFormat.VertAlignment;
    if (vertAlignment.HasValue)
    {
      vertAlignment = textFormat.VertAlignment;
      align = this.ConvertVertAlignmentToTernConst(new VertAlignment?(vertAlignment.Value));
    }
    if (this.Owner.Orientation.IsHorizontalText() && textFormat != null)
    {
      vertAlignment = textFormat.VertAlignment;
      if (vertAlignment.HasValue)
      {
        if ((double) fixedRowSize != 0.0)
        {
          vertAlignment = textFormat.VertAlignment;
          if (vertAlignment.Value == VertAlignment.Center)
            goto label_49;
        }
        editor.TerSetSectAlign(-1, align, false);
        goto label_50;
      }
    }
label_49:
    editor.TerSetSectAlign(-1, 0, false);
label_50:
    editor.VertAlignment = align;
    editor.RestoreSelection(selectionBlock, false);
  }

  /// <summary>Преобразовать ориентацию в угол поворота</summary>
  /// <param name="orientation">Ориентация</param>
  /// <returns>Угол поворота</returns>
  private static int OrientationToAngle(TextOrientation orientation)
  {
    switch (orientation)
    {
      case TextOrientation.Normal:
        return 0;
      case TextOrientation.DownTop:
        return -90;
      case TextOrientation.UpsideDown:
        return 180;
      case TextOrientation.TopDown:
        return 90;
      default:
        return 0;
    }
  }

  /// <summary>Форматировать текст в редакторе</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="paragraphFormat">Формат параграфа</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="charFormat">Формат символов</param>
  /// <param name="backColor">Цвет фона текста</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="isRTF">Содержит RTF</param>
  /// <param name="forceApplyCharStyle">Принудительно применять все настройки к стилю</param>
  protected void FormatEditorText(
    ImRtfEditor editor,
    ParagraphFormat paragraphFormat,
    TextOrientation orientation,
    CharFormat charFormat,
    Color backColor,
    float fixedRowSize,
    bool isRTF,
    DrawContextWithUI context,
    bool forceApplyCharStyle = true)
  {
    if (editor == null)
      return;
    this.SetDefaultCharFormat(editor, charFormat, orientation, backColor, isRTF, forceApplyCharStyle, context);
    editor.TerSetPageBkColor(backColor);
    this.SetDefaultParagraphFormat(editor, paragraphFormat, orientation, fixedRowSize);
  }

  /// <summary>Получить фрагмент текста</summary>
  /// <param name="text">Текст</param>
  /// <param name="firstTextPos">Первая позиция текста</param>
  /// <param name="calc_N_as_RN">Считать \r как два символа \r\n. Необходимо для синхронизации подстёта с редактором</param>
  /// <returns></returns>
  internal string GetTextFragment(string text, int firstTextPos, bool calc_N_as_RN = true)
  {
    if (firstTextPos == -1)
      return text;
    string textFragment = (string) null;
    if (!string.IsNullOrEmpty(text))
    {
      int startIndex = firstTextPos;
      if (calc_N_as_RN)
      {
        for (int index = 1; index < startIndex; ++index)
        {
          if (index < text.Length && text[index] == '\n' && text[index - 1] != '\r' && (index + 1 >= text.Length || text[index + 1] != '\r'))
            --startIndex;
        }
      }
      textFragment = startIndex >= text.Length ? "" : text.Substring(startIndex);
    }
    else if (text != null)
      textFragment = "";
    return textFragment;
  }

  /// <summary>Установить текст в редакторе</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="text">Текст</param>
  /// <param name="isRTF">Текст задан в формате RTF</param>
  /// <param name="firstTxtPos">Начала текста отображаемого в редакторе. Для распределённого по страницам текста</param>
  /// <param name="paragraphFormat">Формат параграфа</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="charFormat">Формат символов</param>
  /// <param name="backColor">Цвет фона текста</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="materialList">Список позиций формул в тексте</param>
  private void SetEditorText(
    ImRtfEditor editor,
    string text,
    bool isRTF,
    int firstTxtPos,
    ParagraphFormat paragraphFormat,
    TextOrientation orientation,
    CharFormat charFormat,
    Color backColor,
    float fixedRowSize,
    out List<int> materialList,
    DrawContextWithUI context)
  {
    materialList = (List<int>) null;
    if (editor == null)
      return;
    try
    {
      editor.blk.TerDeleteAll(false);
      bool flag = this.owner.Orientation == TextOrientation.DownTop || this.owner.Orientation == TextOrientation.TopDown;
      editor.ignoreRtfFrameSize = flag;
      int num = editor.SuspendWordWrap() ? 1 : 0;
      this.FormatEditorText(editor, paragraphFormat, orientation, charFormat, backColor, fixedRowSize, isRTF, context);
      editor.SetTerCursorPos(0, 0, false);
      editor.TerOpFlags2 |= 1024 /*0x0400*/;
      editor.RotatedFrame = false;
      editor.DistributedTextStartPos = -1;
      this.InsertTextIntoEditor(editor, text, isRTF, firstTxtPos, orientation, context);
      if (this.owner.FontAutoSize && !string.IsNullOrWhiteSpace(text))
        this.AdjustFontSizeToFitInCell(editor);
      if (isRTF)
      {
        this.FormatEditorText(editor, paragraphFormat, orientation, charFormat, backColor, fixedRowSize, isRTF, context, false);
        editor.page.Repaginate(false, false, 0, false);
      }
      if (isRTF && firstTxtPos > 0)
        RtfInSiteEditorWrapper.RemoveLeadingTextFromEditor(editor, firstTxtPos);
      bool replaceSpecChar = false;
      string nonbreakingText = (string) null;
      if (this.Owner != null)
      {
        replaceSpecChar = this.Owner.ReplaceOldAVSSpecChars;
        nonbreakingText = this.Owner.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false);
      }
      List<int> avsMaterialPos;
      this.ReplaceSpecSymbolAndFormulas(editor, replaceSpecChar, true, this.Owner != null && this.Owner.ReplaceAVSMaterial, nonbreakingText, out avsMaterialPos);
      editor.SetTerCursorPos(0, 0, false);
      if (num == 0)
        editor.ResumeWordWrap();
      editor.page.Repaginate(false, false, 0, false);
      materialList = RtfInSiteEditorWrapper.ConvertPosToLines(editor, avsMaterialPos);
    }
    finally
    {
      editor.ignoreRtfFrameSize = false;
    }
  }

  private void AdjustFontSizeToFitInCell(ImRtfEditor editor)
  {
    lock (EmptySyncRoot.Value)
    {
      if (!(this.owner is TextBoxElement owner) || owner.IsEmptyText)
        return;
      bool isRTF = owner.Rtf != null;
      TextOrientation orientation = owner.Orientation;
      CharFormat charFormat = owner.CharFormat.Clone();
      Color backColor = owner.GetBackColor();
      float? fontSize = ((this.owner.TemplateRoot?.FindNode(this.owner.TemplateId) is TextData node ? node.CharFormat : (CharFormat) null) ?? TextData.DefaultCharFormat).FontSize;
      float num1 = (float) ((double) charFormat.FontSize ?? (double) TextData.DefaultCharFormat.FontSize.Value);
      float val1 = this.CalcFontSizeAdjustmentRatio(editor);
      if ((double) val1 >= 1.0)
      {
        float mm = UnitsConverter.PixelsToMm((int) editor.TerGr.MeasureString("A", charFormat.GetFont()).Height, editor.TerGr.DpiX);
        float val2 = (float) Math.Round((this.owner.Orientation.IsHorizontalText() ? (double) this.owner.ProperBounds.Height - (double) owner.Margins.Top - (double) owner.Margins.Bottom : (double) this.owner.ProperBounds.Width - (double) owner.Margins.Left - (double) owner.Margins.Right) / (double) mm, 1);
        val1 = Math.Min(val1, val2);
      }
      bool flag = fontSize.HasValue && (double) num1 > (double) fontSize.Value;
      if (!((double) Math.Abs(val1 - 1f) > 9.9999997473787516E-06 | flag))
        return;
      float num2 = RtfInSiteEditorWrapper.StepRound(Math.Max(num1 * val1, 6f), 0.25f);
      if ((double) val1 > 1.0 && fontSize.HasValue)
      {
        if ((double) num2 <= (double) fontSize.Value)
          return;
        num2 = fontSize.Value;
      }
      if (flag)
        num2 = fontSize.Value;
      charFormat.FontSize = new float?(num2);
      this.SetDefaultCharFormat(editor, charFormat, orientation, backColor, isRTF, false, (DrawContextWithUI) null);
    }
  }

  private float CalcFontSizeAdjustmentRatio(ImRtfEditor editor)
  {
    float num1 = 1f;
    int mm = (int) UnitsConverter.TwipsToMm((float) editor.GetTextWidth(-1));
    RectangleF rectangleF = this.owner.ProperBounds;
    if (!this.owner.Orientation.IsHorizontalText())
      rectangleF = new RectangleF(rectangleF.X, rectangleF.Y, rectangleF.Height, rectangleF.Width);
    TextBoxElement owner = this.owner as TextBoxElement;
    float num2 = this.owner.Orientation.IsHorizontalText() ? owner.Margins.Left + owner.Margins.Right : owner.Margins.Top + owner.Margins.Bottom;
    float num3 = (float) mm + num2;
    float num4 = num3 - rectangleF.Width;
    if ((double) Math.Abs(num4) > 9.9999997473787516E-06 && mm != 0)
    {
      float num5 = rectangleF.Width / num3;
      if ((double) num4 < 0.0 && (double) num5 < 1.1)
        num5 = 1f;
      num1 = RtfInSiteEditorWrapper.StepRound(num5, 0.1f);
    }
    return num1;
  }

  private static float StepRound(float value, float step)
  {
    int num1 = (double) value < 0.0 ? -1 : 1;
    value *= (float) num1;
    float num2 = (float) Math.Floor((double) value);
    float num3 = value - num2;
    if ((double) num3 <= 9.9999997473787516E-06)
      return num2;
    for (float num4 = step; (double) num4 <= 1.0; num4 += step)
    {
      if ((double) num3 < (double) num4)
      {
        num3 = num4 - step;
        break;
      }
    }
    return (num2 + num3) * (float) num1;
  }

  private bool InsertTextIntoEditor(
    ImRtfEditor editor,
    string text,
    bool isRTF,
    int firstTxtPos,
    TextOrientation orientation,
    DrawContextWithUI context)
  {
    bool flag;
    if (isRTF)
    {
      if (orientation.IsHorizontalText())
      {
        flag = editor.InsertRtfBuf(text, 0, 0, false);
      }
      else
      {
        int FrameNo = this.SetTextOrientation(editor, orientation, false);
        flag = editor.InsertRtfBuf(text, 0, 0, false);
        editor.SelectTerTextLines(editor.TotalLines - 1, editor.TotalLines, false);
        editor.TerDeleteBlock(false);
        editor.DeselectTerText(false);
        editor.TerPosFrame(FrameNo, 0, false);
      }
    }
    else
    {
      if (firstTxtPos > 0)
      {
        text = this.GetTextFragment(text, firstTxtPos);
        editor.DistributedTextStartPos = firstTxtPos;
      }
      if (!orientation.IsHorizontalText())
        this.SetTextOrientation(editor, orientation, false);
      editor.blk.InitFirstParaChar();
      flag = editor.blk.InsertTerText(text, false);
    }
    return flag;
  }

  private static void RemoveLeadingTextFromEditor(ImRtfEditor editor, int firstTxtPos)
  {
    int row = 0;
    int col = 0;
    editor.page.Repaginate(false, false, 0, false);
    editor.pos.TerAbsToRowCol(firstTxtPos, out row, out col, false);
    editor.SelectTerText(0, 0, row, col, false);
    editor.TerDeleteBlock(false);
    editor.DeselectTerText(false);
    editor.DistributedTextStartPos = firstTxtPos;
  }

  /// <summary>Установить текст в редакторе</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="text">Текст</param>
  /// <param name="isRTF">Текст задан в формате RTF</param>
  /// <param name="firstTxtPos">Начала текста отображаемого в редакторе. Для распределённого по страницам текста</param>
  private void SetEditorText(ImRtfEditor editor, string text, bool isRTF, int firstTxtPos)
  {
    if (editor == null)
      return;
    editor.blk.TerDeleteAll(false);
    editor.RotatedFrame = false;
    TextOrientation orientation = this.owner.Orientation;
    editor.DistributedTextStartPos = -1;
    this.InsertTextIntoEditor(editor, text, isRTF, firstTxtPos, orientation, (DrawContextWithUI) null);
    if (orientation.IsHorizontalText())
    {
      editor.DeselectTerText(false);
      editor.SetTerCursorPos(0, 0, false);
    }
    if (!isRTF || firstTxtPos <= 0)
      return;
    RtfInSiteEditorWrapper.RemoveLeadingTextFromEditor(editor, firstTxtPos);
  }

  /// <summary>Обновить формат текста в редакторе если он активен</summary>
  public override void UpdateActiveEditorFormat()
  {
    ImRtfEditor editor = this.Editor;
    if (editor == null || this.owner == null)
      return;
    float fixedRowSize = 0.0f;
    if (this.owner.IsFixedSizeRows)
      fixedRowSize = this.owner.DefaultRowSize;
    this.FormatEditorText(editor, this.owner.ParagraphFormat, this.owner.Orientation, this.owner.CharFormat, this.owner.GetBackColor(), fixedRowSize, true, (DrawContextWithUI) null);
  }

  /// <summary>Установить фиксированную высоту для всех строк в активном редакторе</summary>
  /// <param name="rowSize">Высота строки</param>
  /// <param name="repaint">Перерисовывать редактор</param>
  internal void SetAllRowSize(float rowSize, bool repaint)
  {
    this.SetAllRowSize(this.Editor, rowSize, repaint);
  }

  /// <summary>Установить фиксированную высоту для всех строк в редакторе</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="rowSize">Высота строки</param>
  /// <param name="repaint">Перерисовывать редактор</param>
  private void SetAllRowSize(ImRtfEditor editor, float rowSize, bool repaint)
  {
    if (editor == null)
      return;
    SelectionBlock selectionBlock = editor.GetSelectionBlock();
    editor.SelectAll(false);
    editor.TerSetParaSpacing(0, 0, -UnitsConverter.MmToTwips(rowSize), false);
    editor.RestoreSelection(selectionBlock, repaint);
  }

  /// <summary>Создать и инициализировать экземпляр редактора ImRtfEditor</summary>
  /// <param name="bounds">Границы в мм</param>
  /// <param name="winBounds">Границы в писелах</param>
  /// <returns>Экземпляр редактора ImRtfEditor</returns>
  public static ImRtfEditor CreateTern(RectangleF bounds, Rectangle winBounds)
  {
    ImRtfEditor tern = new ImRtfEditor();
    tern.PaintEnabled = false;
    tern.TernKey = "3XP41-MR5T2-76CH3";
    tern.TerSetFlags5(true, 1073741824 /*0x40000000*/);
    tern.TerSetFlags(true, 1048576 /*0x100000*/);
    tern.TerSetFlags(true, 32 /*0x20*/);
    tern.TerSetFlags3(true, 128 /*0x80*/);
    tern.TerSetFlags(true, 512 /*0x0200*/);
    tern.TerSetFlags4(true, 4096 /*0x1000*/);
    tern.TerSetFlags2(true, 32 /*0x20*/);
    tern.TerSetFlags4(true, 1);
    tern.BorderMargin = false;
    tern.FittedView = false;
    tern.HorzScrollBar = false;
    tern.VertScrollBar = false;
    tern.PageMode = true;
    tern.PrintViewMode = true;
    if (!tern.IsHandleCreated)
      tern.TerCreateControl();
    tern.TerSetMarginEx(-1, 0, 0, 0, 0, 0, 0, false);
    tern.DefLang = 1049;
    tern.DefInpLang = InputLanguage.FromCulture(new CultureInfo(1049));
    tern.ReqLang = 1049;
    tern.ReqCharSet = (byte) 204;
    tern.PaintEnabled = false;
    tern.Bounds = winBounds;
    PaperSize size = new PaperSize("custom", UnitsConverter.MmToHundredthsOfInch(bounds.Width), UnitsConverter.MmToHundredthsOfInch(bounds.Height));
    tern.TerSetPaper(size, true, false);
    tern.WordWrap = true;
    tern.PaintEnabled = true;
    tern.TerSetModify(false);
    tern.TerEnableSpeedKey(600, false);
    tern.TerEnableSpeedKey(601, false);
    tern.TerEnableSpeedKey(715, false);
    tern.TerEnableSpeedKey(716, false);
    tern.TerEnableSpeedKey(612, false);
    tern.TerEnableSpeedKey(613, false);
    tern.TerEnableSpeedKey(615, false);
    tern.TerEnableSpeedKey(616, false);
    tern.TerEnableSpeedKey(617, false);
    tern.TerEnableSpeedKey(619, false);
    tern.TerEnableSpeedKey(622, false);
    tern.TerEnableSpeedKey(632, false);
    tern.TerEnableSpeedKey(623, false);
    tern.TerEnableSpeedKey(624, false);
    tern.TerEnableSpeedKey(633, false);
    tern.TerEnableSpeedKey(634, false);
    tern.TerEnableSpeedKey(635, false);
    tern.TerEnableSpeedKey(636, false);
    tern.TerEnableSpeedKey(637, false);
    tern.TerEnableSpeedKey(741, false);
    tern.TerEnableSpeedKey(640, false);
    tern.TerEnableSpeedKey(641, false);
    tern.TerEnableSpeedKey(642, false);
    tern.TerEnableSpeedKey(643, false);
    tern.TerEnableSpeedKey(645, false);
    tern.TerEnableSpeedKey(646, false);
    tern.TerEnableSpeedKey(647, false);
    tern.TerEnableSpeedKey(687, false);
    tern.TerEnableSpeedKey(742, false);
    tern.TerEnableSpeedKey(689, false);
    tern.TerEnableSpeedKey(654, false);
    tern.TerEnableSpeedKey(655, false);
    tern.TerEnableSpeedKey(662, false);
    tern.TerEnableSpeedKey(729, true);
    tern.TerEnableSpeedKey(748, true);
    tern.TerEnableSpeedKey(730, false);
    tern.TerEnableSpeedKey(731, false);
    tern.TerEnableSpeedKey(732, false);
    tern.TerEnableSpeedKey(670, false);
    tern.TerEnableSpeedKey(804, false);
    tern.TerEnableSpeedKey(805, false);
    return tern;
  }

  /// <summary>Назначить контрол редатора</summary>
  /// <param name="value">Значение</param>
  protected override void AssignEditorControl(Control value)
  {
    if (this.EditorControl == value)
      return;
    this.PaintBuffer = (Image) null;
    this.MaterialList = (List<int>) null;
    base.AssignEditorControl(value);
  }

  /// <summary>Редактор ImRtfEditor</summary>
  internal ImRtfEditor Editor
  {
    [DebuggerStepThrough] get => this.EditorControl as ImRtfEditor;
    set
    {
      ImRtfEditor editor = this.Editor;
      if (editor == value)
        return;
      this.PaintBuffer = (Image) null;
      this.MaterialList = (List<int>) null;
      IPageElementWithInterface owner = this.owner as IPageElementWithInterface;
      if (editor != null)
      {
        editor.PostPaint -= new ImRtfEditor.EventPostPaint(this.Editor_PostPaint);
        editor.Modified -= new ImRtfEditor.EventModified(this.Editor_Modified);
        editor.Validating -= new CancelEventHandler(((InSiteEditorWrapper) this).Editor_Validating);
        editor.LostFocus -= new EventHandler(this.Editor_LostFocus);
        if (owner != null && owner.PageUI != null)
        {
          PageElementUI pageUi = owner.PageUI;
          editor.PreprocessClick -= new PreprocessEventHandler(pageUi.PreprocessControlClick);
          editor.PreprocessDoubleClick -= new PreprocessEventHandler(pageUi.PreprocessControlDoubleClick);
          editor.PreprocessEnter -= new PreprocessEventHandler(pageUi.PreprocessControlEnter);
          editor.PreprocessLeave -= new PreprocessEventHandler(pageUi.PreprocessControlLeave);
          editor.PreprocessMouseDown -= new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseDown);
          editor.PreprocessMouseEnter -= new PreprocessEventHandler(pageUi.PreprocessControlMouseEnter);
          editor.PreprocessMouseLeave -= new PreprocessEventHandler(pageUi.PreprocessControlMouseLeave);
          editor.PreprocessMouseMove -= new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseMove);
          editor.PreprocessMouseUp -= new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseUp);
          editor.PreprocessMouseWheel -= new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseWheel);
          editor.PreprocessKeyDown -= new PreprocessKeyEventHandler(pageUi.PreprocessControlKeyDown);
          editor.PreprocessKeyPress -= new PreprocessKeyPressEventHandler(pageUi.PreprocessControlKeyPress);
          editor.PreprocessKeyUp -= new PreprocessKeyEventHandler(pageUi.PreprocessControlKeyUp);
          editor.UpdateToolbar -= new ImRtfEditor.EventUpdateToolbar(this.Editor_UpdateToolbar);
          editor.CursorPosChanged -= new EventHandler(this.Editor_CursorPosChanged);
          editor.Hypertext -= new ImRtfEditor.EventHypertext(this.Value_Hypertext);
        }
      }
      if (value != null)
      {
        value.TerSetModify(false);
        value.PostPaint += new ImRtfEditor.EventPostPaint(this.Editor_PostPaint);
        value.Modified += new ImRtfEditor.EventModified(this.Editor_Modified);
        value.LostFocus += new EventHandler(this.Editor_LostFocus);
        value.CausesValidation = true;
        value.Validating += new CancelEventHandler(((InSiteEditorWrapper) this).Editor_Validating);
        if (owner != null && owner.PageUI != null)
        {
          PageElementUI pageUi = owner.PageUI;
          value.PreprocessClick += new PreprocessEventHandler(pageUi.PreprocessControlClick);
          value.PreprocessDoubleClick += new PreprocessEventHandler(pageUi.PreprocessControlDoubleClick);
          value.PreprocessEnter += new PreprocessEventHandler(pageUi.PreprocessControlEnter);
          value.PreprocessLeave += new PreprocessEventHandler(pageUi.PreprocessControlLeave);
          value.PreprocessMouseDown += new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseDown);
          value.PreprocessMouseEnter += new PreprocessEventHandler(pageUi.PreprocessControlMouseEnter);
          value.PreprocessMouseLeave += new PreprocessEventHandler(pageUi.PreprocessControlMouseLeave);
          value.PreprocessMouseMove += new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseMove);
          value.PreprocessMouseUp += new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseUp);
          value.PreprocessMouseWheel += new PreprocessMouseEventHandler(pageUi.PreprocessControlMouseWheel);
          value.PreprocessKeyDown += new PreprocessKeyEventHandler(pageUi.PreprocessControlKeyDown);
          value.PreprocessKeyPress += new PreprocessKeyPressEventHandler(pageUi.PreprocessControlKeyPress);
          value.PreprocessKeyUp += new PreprocessKeyEventHandler(pageUi.PreprocessControlKeyUp);
          value.UpdateToolbar += new ImRtfEditor.EventUpdateToolbar(this.Editor_UpdateToolbar);
          value.UndoSaved += new EventHandler(this.Value_UndoSaved);
          value.CursorPosChanged += new EventHandler(this.Editor_CursorPosChanged);
          value.Hypertext += new ImRtfEditor.EventHypertext(this.Value_Hypertext);
        }
      }
      this.AssignEditorControl((Control) value);
    }
  }

  private void Value_Hypertext(object Sender, ref tc.StrHyperlink link)
  {
    string code1 = link.code;
    string code2;
    try
    {
      code2 = Encoding.UTF8.GetString(Convert.FromBase64String(code1));
    }
    catch
    {
      code2 = link.code;
    }
    this.GetDocumentControl()?.OnHyperLinkActivated(new HyperLinkActivated_EventArgs((DocumentTreeNode) this.Owner, code2, link.RightClick));
  }

  private void Value_UndoSaved(object sender, EventArgs e)
  {
  }

  /// <summary>Обработчик события изменения позиции курсора в редакторе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Editor_CursorPosChanged(object sender, EventArgs e) => this.ScrollToViewEditor();

  /// <summary>Изменить положение страницы так, чтобы был виден редактор</summary>
  private void ScrollToViewEditor()
  {
    DocumentControl documentControl = this.GetDocumentControl();
    if (documentControl == null || this.owner == null || this.Editor == null)
      return;
    Point location = this.Editor.Location;
    Point point = this.GetTextCursorCoor();
    point = new Point(point.X + location.X, point.Y + location.Y);
    int curLineHeight = this.GetCurLineHeight();
    documentControl.ScrollToViewRectangle(this.Editor.Bounds, new Point?(point), new int?(curLineHeight), false, false);
  }

  /// <summary>Проверка текста в редакторе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  internal override void Editor_Validating(object sender, CancelEventArgs e)
  {
    ++this.suspendLostFocusHandler;
    try
    {
      TextBoxElement owner = this.owner as TextBoxElement;
      string str = (string) null;
      string rtfText1 = (string) null;
      int num1 = -1;
      bool flag1 = false;
      bool flag2 = false;
      try
      {
        if (!(sender is ImRtfEditor editor))
          editor = this.Editor;
        if (this.owner != null && editor != null && (this.NeedValidate || editor.TerIsModified()))
        {
          if (this.Owner.ReadOnlyNow && this.Owner.ReadOnlyFormating || this.OwnerDocumentIsReadOnly((Control) editor))
            return;
          if (owner != null)
            num1 = editor.DistributedTextStartPos;
          if (owner != null && !owner.IsEmptyText)
            rtfText1 = owner.Rtf;
          int num2 = !editor.CheckPlaneText() ? 1 : 0;
          string planeText1 = editor.PlaneText;
          string planeText2 = (string) null;
          string rtfText2 = (string) null;
          if (num2 != 0)
            this.GetActualText(out planeText2, out rtfText2, true);
          str = this.owner.Text;
          if (num1 > 0)
            str = this.GetTextFragment(str, num1);
          if (!this.TextIsEqual_IgnoreRN(planeText1, str))
            flag1 = true;
          if (flag1 || this.NeedValidate)
          {
            TextValidating_EventArgs e1 = new TextValidating_EventArgs(planeText1);
            this.owner.OnTextValidating(e1);
            e.Cancel = e1.Cancel;
            if (!e1.Cancel && e1.Text != planeText1)
            {
              this.SetEditorText(this.Editor, e1.Text, false, num1);
              this.ReplaceSpecSymbolAndFormulas(this.Editor, this.owner.ReplaceOldAVSSpecChars, true, this.owner.ReplaceAVSMaterial, this.owner.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false), out List<int> _);
              editor.page.Repaginate(false, false, 0, true);
            }
          }
          else
          {
            if (rtfText2 == null)
              rtfText2 = string.Empty;
            if (rtfText1 == null)
              rtfText1 = string.Empty;
            if (rtfText2 != rtfText1)
              flag2 = true;
          }
        }
        if (e.Cancel || !(flag1 | flag2))
          return;
        this.OnTextChaged();
        editor.TerSetModify(false);
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
        owner?.AssignText(str, rtfText1, true, false, false);
        bool isRTF = rtfText1 != null;
        if (isRTF)
          str = rtfText1;
        this.SetEditorText(this.Editor, str, isRTF, num1, this.owner.ParagraphFormat, this.owner.Orientation, this.owner.CharFormat, this.owner.GetBackColor(), this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, out List<int> _, (DrawContextWithUI) null);
        this.Editor?.Refresh();
        e.Cancel = true;
      }
    }
    finally
    {
      --this.suspendLostFocusHandler;
    }
  }

  public override void OnTextChaged()
  {
    base.OnTextChaged();
    if (this.Owner == null || !this.Owner.ContainsAttribute("ChangedByUser"))
      return;
    this.Owner.SetAttributeValue("ChangedByUser", "Changed");
  }

  /// <summary>Сравнить текст игнорируя различия в переносах строки \n и \r\n</summary>
  /// <param name="text1">Первый текст</param>
  /// <param name="text2">Второй текст</param>
  /// <returns></returns>
  internal bool TextIsEqual_IgnoreRN(string text1, string text2)
  {
    if (text1 == null)
      return text2 == null;
    if (text2 == null)
      return false;
    int index1 = 0;
    int index2;
    for (index2 = 0; index1 < text1.Length && index2 < text2.Length; ++index2)
    {
      if ((int) text1[index1] != (int) text2[index2])
      {
        if (index1 > 0 && text1[index1] == '\n' && text1[index1 - 1] != '\r' && index2 < text2.Length - 1 && text2[index2] == '\r' && text2[index2 + 1] == '\n')
        {
          ++index2;
        }
        else
        {
          if (index2 <= 0 || text2[index2] != '\n' || text2[index2 - 1] == '\r' || index1 >= text1.Length - 1 || text1[index1] != '\r' || text1[index1 + 1] != '\n')
            return false;
          ++index1;
        }
      }
      ++index1;
    }
    return index1 == text1.Length && index2 == text2.Length;
  }

  /// <summary>Обработчик потери фокуса в редакторе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Editor_LostFocus(object sender, EventArgs e)
  {
    if (this.suspendLostFocusHandler > 0 || this.Editor == null || this.owner == null || this.OwnerDocumentIsReadOnly(sender as Control))
      return;
    DocumentControl documentControl = this.GetDocumentControl();
    if (documentControl != null)
    {
      documentControl.SaveActiveEditorSelection();
      documentControl.NeedUpdateToolbar = false;
    }
    string str = this.owner.Text;
    int distributedTextStartPos = this.Editor.DistributedTextStartPos;
    if (distributedTextStartPos > 0)
      str = this.GetTextFragment(str, distributedTextStartPos);
    if (!this.TextIsEqual_IgnoreRN(this.EditorText, str))
    {
      this.Editor_Validating(sender, new CancelEventArgs());
    }
    else
    {
      if (!(this.owner is TextBoxElement) || (this.owner as TextBoxElement).PrevCell != null || !((this.owner as TextBoxElement).Rtf != this.EditorRtf))
        return;
      this.Editor_Validating(sender, new CancelEventArgs());
    }
  }

  /// <summary>Получить DocumentControl для владельца обёртки</summary>
  /// <returns></returns>
  private DocumentControl GetDocumentControl()
  {
    ImDocument imDocument = (ImDocument) null;
    if (this.owner != null)
      imDocument = this.owner.OwnerDocument as ImDocument;
    return imDocument?.DocumentControl;
  }

  /// <summary>Обновить панель инструментов</summary>
  /// <param name="Sender"></param>
  internal void Editor_UpdateToolbar(object Sender)
  {
    DocumentControl documentControl = this.GetDocumentControl();
    if (documentControl == null || !documentControl.NeedUpdateToolbar)
      return;
    documentControl.UpdateFormatCommands();
  }

  /// <summary>Преобразовать абсолютную позицию текста в редакторе в номера строк</summary>
  /// <param name="editor">Редактор теста</param>
  /// <param name="posList">Позиции в тексте</param>
  /// <returns></returns>
  internal static List<int> ConvertPosToLines(ImRtfEditor editor, List<int> posList)
  {
    if (posList == null)
      return (List<int>) null;
    if (editor == null)
      throw new ArgumentNullException(nameof (editor));
    List<int> lines = new List<int>(posList.Count);
    for (int index = 0; index < posList.Count; ++index)
    {
      int row;
      editor.TerAbsToRowCol(posList[index], out row, out int _);
      if (!lines.Contains(row))
        lines.Add(row);
    }
    return lines;
  }

  /// <summary>Получить полный текст цепочки с учётом редактируемого в ячейке</summary>
  /// <param name="planeText">Текст без форматирования</param>
  /// <param name="rtfText">Текст в формате RTF</param>
  /// <param name="onlyRtfIfExist">Получать только RTF, если он есть</param>
  internal void GetActualText(out string planeText, out string rtfText, bool onlyRtfIfExist)
  {
    planeText = (string) null;
    rtfText = (string) null;
    if (!this.EditorActive || !this.owner.InPlaceEditorActive || !(this.owner is TextBoxElement owner))
      return;
    int num = -1;
    if (this.Editor != null)
      num = this.Editor.DistributedTextStartPos;
    if (num != -1)
    {
      if (!owner.IsEmptyText)
        rtfText = owner.Rtf;
      if (rtfText == null || !onlyRtfIfExist)
        planeText = owner.GetText();
      bool isRTF = rtfText != null;
      string text = !isRTF ? planeText : rtfText;
      if (RtfInSiteEditorWrapper.ternBufferForActualText == null || RtfInSiteEditorWrapper.ternBufferForActualText.InvokeRequired)
        RtfInSiteEditorWrapper.ternBufferForActualText = RtfInSiteEditorWrapper.CreateTernBuffer("ternBufferForActualText");
      ImRtfEditor bufferForActualText = RtfInSiteEditorWrapper.ternBufferForActualText;
      RectangleF clientBounds = this.clientBounds with
      {
        Height = TextBoxElement.MaxTextHeight
      };
      this.SetupEditor(bufferForActualText, text, isRTF, -1, this.owner.ParagraphFormat, this.owner.Orientation, this.owner.CharFormat, this.owner.GetBackColor(), clientBounds, new Rectangle(0, 0, 200, 200), this.owner.Margins, 1f, this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, false, out List<int> _, (DrawContextWithUI) null);
      bufferForActualText.page.Repaginate(false, false, 0, false);
      int row;
      int col;
      bufferForActualText.pos.TerAbsToRowCol(num, out row, out col, false);
      ImRtfEditor editor = this.Editor;
      string textFragment = this.GetTextFragment(planeText, num);
      string planeText1 = editor.PlaneText;
      if (!string.IsNullOrEmpty(textFragment))
      {
        int LastLine = bufferForActualText.TotalLines - 1;
        int LastCol = bufferForActualText.text[LastLine].len - 1;
        bufferForActualText.SelectTerText(row, col, LastLine, LastCol, false);
        bufferForActualText.TerDeleteBlock(false);
        bufferForActualText.DeselectTerText(false);
      }
      editor.DeselectTerText(false);
      if (editor.CheckPlaneText())
      {
        bufferForActualText.SetTerCursorPos(row, col, false);
        bufferForActualText.blk.InsertTerText(planeText1, false);
      }
      else
      {
        string shortRtf = editor.GetShortRtf();
        bufferForActualText.InsertRtfBuf(shortRtf, row, col, false);
        bufferForActualText.SelectTerTextLines(bufferForActualText.TotalLines - 1, bufferForActualText.TotalLines, false);
        bufferForActualText.TerDeleteBlock(false);
        bufferForActualText.DeselectTerText(false);
      }
      if (bufferForActualText.CheckPlaneText())
      {
        planeText = bufferForActualText.PlaneText;
        rtfText = (string) null;
      }
      else
      {
        rtfText = bufferForActualText.GetShortRtf();
        if (onlyRtfIfExist)
          return;
        planeText = bufferForActualText.PlaneText;
      }
    }
    else
    {
      if (this.owner.PrevCell != null)
        return;
      ImRtfEditor editor = this.Editor;
      if (editor == null)
        return;
      if (editor.CheckPlaneText())
      {
        planeText = editor.PlaneText;
        rtfText = (string) null;
      }
      else
      {
        rtfText = editor.GetShortRtf();
        if (onlyRtfIfExist)
          return;
        planeText = editor.PlaneText;
      }
    }
  }

  /// <summary>Найти элемент в цепочке, соответствующий позиции текста</summary>
  /// <param name="currTB">Ячейка с которой начинать поиск</param>
  /// <param name="textPos">Позиция текста</param>
  /// <returns></returns>
  internal TextBoxElement FindTextBoxForTextPosition(TextBoxElement currTB, int textPos)
  {
    if (currTB.StartCharIndex != -1 && textPos < currTB.StartCharIndex)
    {
      while (currTB != null && textPos < currTB.StartCharIndex)
        currTB = currTB.PrevCell as TextBoxElement;
    }
    else if (currTB.NextCell is TextBoxElement nextCell1 && nextCell1.StartCharIndex != -1 && textPos >= nextCell1.StartCharIndex)
    {
      currTB = nextCell1;
      while (currTB != null && currTB.NextCell is TextBoxElement nextCell && textPos >= nextCell.StartCharIndex && nextCell.StartCharIndex != -1)
        currTB = nextCell;
    }
    return currTB;
  }

  /// <summary>
  /// Вызываем обновление редактора, если нажат backspace в пустом редакторе, чтобы перешел на пред. страницу
  /// </summary>
  internal void EmptyEditorBackspace()
  {
    if (this.Owner.PrevCell == null)
      return;
    int CursCol = 0;
    int CursLine;
    this.Editor.GetTerCursorPos(out CursLine, ref CursCol);
    if (CursCol != 0 || CursLine != 0)
      return;
    this.Editor.OldTextLines = -2;
    this.Editor_Modified((object) null);
  }

  /// <summary>Обработчик события Modified в редакторе</summary>
  /// <param name="Sender">Объект вызвавший событие</param>
  protected virtual void Editor_Modified(object Sender)
  {
    if (this.editor_ModifiedHanlder_IsSuspended)
      return;
    this.editor_ModifiedHanlder_IsSuspended = true;
    try
    {
      ImRtfEditor editor = this.Editor;
      if (editor == null)
        return;
      TextBoxElement owner = this.owner as TextBoxElement;
      TextBoxElement firstCell = this.owner.FindFirstCell() as TextBoxElement;
      if (owner == null || owner.OwnerDocument == null || owner.SuspendedUpdateUIGeometryFlag)
        return;
      SelectionBlock selectionBlock1 = editor.GetSelectionBlock();
      this.PaintBuffer = (Image) null;
      this.MaterialList = (List<int>) null;
      bool isFixedSizeRows = owner.IsFixedSizeRows;
      float lineSize = 0.0f;
      if (isFixedSizeRows)
        lineSize = owner.DefaultRowSize;
      bool flag1 = false;
      TextOrientation orientation = owner.Orientation;
      bool flag2 = orientation.IsHorizontalText();
      if (!flag2)
      {
        if (editor.TotalLines > 0 && editor.text[0].fid == 0)
          this.SetTextOrientation(editor, orientation, editor.PlaneText != "");
        else if (editor.TotalLines > 1 && editor.text[0].fid > 0 && editor.PlaneText == "")
        {
          editor.SelectTerTextLines(1, editor.TotalLines, false);
          editor.TerDeleteBlock(false);
          editor.DeselectTerText(false);
        }
      }
      if (isFixedSizeRows && (double) lineSize > 0.0)
      {
        int paraParam = editor.TerGetParaParam(0, false, 6);
        int SpaceBetween = -(int) Math.Truncate((double) lineSize * 56.692913055419922);
        int num = SpaceBetween;
        if (paraParam != num)
        {
          SelectionBlock selectionBlock2 = editor.GetSelectionBlock();
          editor.TerSetFlags3(true, 262144 /*0x040000*/);
          editor.SelectAll(false);
          editor.TerSetParaSpacing(0, 0, SpaceBetween, false);
          editor.TerRepaginate(false);
          editor.RestoreSelection(selectionBlock2, false);
          editor.TerSetFlags3(false, 262144 /*0x040000*/);
        }
      }
      List<int> avsMaterialPos = (List<int>) null;
      bool flag3 = this.ReplaceSpecSymbolAndFormulas(editor, this.owner.ReplaceOldAVSSpecChars, true, this.owner.ReplaceAVSMaterial, this.owner.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false), out avsMaterialPos);
      this.MaterialList = RtfInSiteEditorWrapper.ConvertPosToLines(editor, avsMaterialPos);
      bool refresh = false;
      int cellCharCount = -1;
      if (owner.AutoSizeHeight)
      {
        if (!flag3)
          selectionBlock1 = editor.GetSelectionBlock();
        owner.SuspendRefreshUI();
        try
        {
          int textHeight = editor.page.TerGetTextHeight();
          int textWidth = editor.GetTextWidth(flag2 ? 0 : -1);
          int totalLines = editor.TotalLines;
          if (flag3)
          {
            editor.TerRepaginate(true);
            flag1 = false;
          }
          else
            editor.TerRepaint(false);
          int num = editor.DistributedTextStartPos;
          if (num == -1)
            num = 0;
          int CursCol = 0;
          int CursLine;
          editor.GetTerCursorPos(out CursLine, ref CursCol);
          int textPos = num + editor.TerRowColToAbs(CursLine, CursCol, false);
          int pageLastLine = editor.GetPageLastLine(0);
          if ((!flag2 || textHeight == editor.OldTextHeight) && (flag2 || textWidth == editor.OldTextWidth) && totalLines == editor.OldTextLines && CursLine <= pageLastLine)
          {
            if (owner.nextCellCharPos != -1)
            {
              if (textPos <= owner.nextCellCharPos)
                goto label_54;
            }
            else
              goto label_54;
          }
          refresh = true;
          RectangleF properBounds = owner.ProperBounds;
          SizeF size = this.CalcCellSizeForText(editor, owner.Margins, owner.IsFixedSizeRows, out cellCharCount);
          if (owner.PrevCell is TextBoxElement prevCell && cellCharCount != -1)
            cellCharCount += prevCell.nextCellCharPos;
          owner.nextCellCharPos = cellCharCount;
          size.Width = properBounds.Width;
          if (isFixedSizeRows && (double) lineSize > 0.0)
            size.Height = owner.RoundForFixedSizeRow(size.Height, lineSize, 0.0f);
          int oldTextLines = editor.OldTextLines;
          editor.OldTextHeight = editor.page.TerGetTextHeight();
          editor.OldTextLines = editor.TotalLines;
          editor.OldTextWidth = editor.GetTextWidth(flag2 ? 0 : -1);
          if (!this.OwnerDocument.IsDistributing)
          {
            if ((double) Math.Abs(size.Height - properBounds.Height) > 9.9999997473787516E-06)
            {
              owner.AssignProperBounds(properBounds.Location, size, true, true, true);
              if (!flag2)
                this.SetTextOrientation(editor, orientation, true);
            }
            else if (totalLines != oldTextLines)
              owner.SetNeedUpdateLayoutFlag(true, true, true, true, true);
          }
          TextBoxElement boxForTextPosition = this.FindTextBoxForTextPosition(firstCell, textPos);
          if (boxForTextPosition != null && boxForTextPosition != owner)
          {
            this.OnTextChaged();
            boxForTextPosition = this.FindTextBoxForTextPosition(firstCell, textPos);
            if (boxForTextPosition.Page != null && boxForTextPosition.Page.IsLockedForLayout)
              boxForTextPosition.Page.WaitForLayout(1000);
            DocumentControl documentControl = (DocumentControl) null;
            if (firstCell.PageUI != null)
              documentControl = firstCell.PageUI.DocumentControl;
            if (documentControl != null)
            {
              documentControl.SetSelection((DocumentTreeNode) boxForTextPosition, true, Point.Empty, true, false);
              if (boxForTextPosition.PageUI != null && !boxForTextPosition.PageUI.IsActiveElement)
              {
                documentControl.SetActiveElement((DocumentTreeNode) null, true, Point.Empty);
                documentControl.SetActiveElement((DocumentTreeNode) boxForTextPosition, true, Point.Empty);
              }
              PageElementUI focusedElement = documentControl.FocusedElement;
              if (boxForTextPosition.TextBox != null)
                editor = boxForTextPosition.TextBox.Editor;
              selectionBlock1.HilightType = 0;
              selectionBlock1.StartPos = 0;
              selectionBlock1.EndPos = 0;
            }
          }
          if (boxForTextPosition != null)
          {
            if (!boxForTextPosition.InPlaceEditorActive)
            {
              this.OnTextChaged();
              IPageElementWithInterface elementWithInterface = (IPageElementWithInterface) boxForTextPosition;
              if (elementWithInterface != null)
              {
                elementWithInterface.ActivateInPlaceEditor(elementWithInterface.PageUI, (MouseEventArgs) null);
                if (boxForTextPosition.TextBox != null)
                  editor = boxForTextPosition.TextBox.Editor;
              }
            }
          }
        }
        finally
        {
          owner.ResumeRefreshUI(refresh);
        }
label_54:
        editor?.RestoreSelection(selectionBlock1, false);
      }
      else if (owner.AutoSizeWidth)
      {
        if (!flag3)
          selectionBlock1 = editor.GetSelectionBlock();
        owner.SuspendRefreshUI();
        try
        {
          float pprWidth = editor.TerSect[0].PprWidth;
          editor.sec.TerSetSectPageSize(-1, 40f, editor.TerSect[0].PprHeight, false);
          editor.TerRepaginate(false);
          int textWidth = editor.GetTextWidth(0);
          int totalLines = editor.TotalLines;
          int num = editor.DistributedTextStartPos;
          if (num == -1)
            num = 0;
          int CursCol = 0;
          int CursLine;
          editor.GetTerCursorPos(out CursLine, ref CursCol);
          int textPos = num + editor.TerRowColToAbs(CursLine, CursCol, false);
          int pageLastLine = editor.GetPageLastLine(0);
          int oldTextWidth = editor.OldTextWidth;
          if (textWidth != oldTextWidth || totalLines != editor.OldTextLines || CursLine > pageLastLine || owner.nextCellCharPos != -1 && textPos > owner.nextCellCharPos)
          {
            refresh = true;
            RectangleF properBounds = owner.ProperBounds;
            SizeF size = this.CalcCellSizeForText(editor, owner.Margins, owner.IsFixedSizeRows, out cellCharCount) with
            {
              Height = properBounds.Height
            };
            int oldTextLines = editor.OldTextLines;
            editor.OldTextHeight = editor.page.TerGetTextHeight();
            editor.OldTextLines = editor.TotalLines;
            editor.OldTextWidth = editor.GetTextWidth(0);
            if ((double) Math.Abs(size.Width - properBounds.Width) > 9.9999997473787516E-06)
            {
              owner.AssignTextWidth(size.Width);
              owner.AssignProperBounds(properBounds.Location, size, true, true, true);
            }
            else if (totalLines != oldTextLines)
              owner.SetNeedUpdateLayoutFlag(true, true, true, true, true);
          }
          else
            editor.sec.TerSetSectPageSize(-1, pprWidth, editor.TerSect[0].PprHeight, false);
          TextBoxElement boxForTextPosition1 = this.FindTextBoxForTextPosition(firstCell, textPos);
          if (boxForTextPosition1 != null)
          {
            if (boxForTextPosition1 != owner)
            {
              this.OnTextChaged();
              TextBoxElement boxForTextPosition2 = this.FindTextBoxForTextPosition(firstCell, textPos);
              DocumentControl documentControl = (DocumentControl) null;
              if (firstCell.PageUI != null)
                documentControl = firstCell.PageUI.DocumentControl;
              if (documentControl != null)
              {
                documentControl.SetSelection((DocumentTreeNode) boxForTextPosition2, true, Point.Empty, true, false);
                if (boxForTextPosition2.PageUI != null && !boxForTextPosition2.PageUI.IsActiveElement)
                {
                  documentControl.SetActiveElement((DocumentTreeNode) null, true, Point.Empty);
                  documentControl.SetActiveElement((DocumentTreeNode) boxForTextPosition2, true, Point.Empty);
                }
                PageElementUI focusedElement = documentControl.FocusedElement;
                if (boxForTextPosition2.TextBox != null)
                  editor = boxForTextPosition2.TextBox.Editor;
                selectionBlock1.HilightType = 0;
                selectionBlock1.StartPos = 0;
                selectionBlock1.EndPos = 0;
              }
            }
          }
        }
        finally
        {
          owner.ResumeRefreshUI(refresh);
        }
        editor.RestoreSelection(selectionBlock1, false);
      }
      else if (owner.CheckFlags((byte) 8))
      {
        if (!flag3)
          selectionBlock1 = editor.GetSelectionBlock();
        owner.SuspendRefreshUI();
        try
        {
          float pprWidth = editor.TerSect[0].PprWidth;
          editor.sec.TerSetSectPageSize(-1, 40f, editor.TerSect[0].PprHeight, false);
          editor.TerRepaginate(false);
          if (editor.GetTextWidth(0) != editor.OldTextWidth)
          {
            refresh = true;
            RectangleF properBounds = owner.ProperBounds;
            SizeF sizeF = this.CalcCellSizeForText(editor, owner.Margins, owner.IsFixedSizeRows, out cellCharCount) with
            {
              Height = properBounds.Height
            };
            if ((double) sizeF.Width - (double) properBounds.Width > 9.9999997473787516E-06)
            {
              editor.sec.TerSetSectPageSize(-1, UnitsConverter.MmToInch(sizeF.Width), editor.TerSect[0].PprHeight, false);
              Control parent = editor.Parent;
              if (owner.Page is Page && (owner.Page as Page).PageUI != null)
                editor.Width = (owner.Page as Page).PageUI.ConvertWorldXToPixel(sizeF.Width) + 5;
              editor.TerRepaginate(false);
            }
            else if (editor.Parent is PageControl parent1)
            {
              Rectangle rectangle = this.CalcPixelTextBounds(this.clientBounds, owner.Margins, parent1);
              RectangleF rectangleF = this.CalcTextBounds(this.clientBounds, owner.Margins);
              ++rectangle.Width;
              editor.sec.TerSetSectPageSize(-1, UnitsConverter.MmToInch(rectangleF.Width), editor.TerSect[0].PprHeight, false);
              editor.Width = rectangle.Width;
              editor.TerRepaginate(false);
            }
            editor.OldTextHeight = editor.page.TerGetTextHeight();
            editor.OldTextLines = editor.TotalLines;
            editor.OldTextWidth = editor.GetTextWidth(0);
          }
          else
            editor.sec.TerSetSectPageSize(-1, pprWidth, editor.TerSect[0].PprHeight, false);
        }
        finally
        {
          owner.ResumeRefreshUI(refresh);
        }
        editor.RestoreSelection(selectionBlock1, false);
      }
      else
      {
        if (!owner.FontAutoSize)
          return;
        if (!flag3)
          selectionBlock1 = editor.GetSelectionBlock();
        owner.SuspendRefreshUI();
        try
        {
          this.AdjustFontSizeToFitInCell(editor);
        }
        finally
        {
          owner.ResumeRefreshUI(refresh);
        }
        editor.RestoreSelection(selectionBlock1, false);
      }
    }
    finally
    {
      this.editor_ModifiedHanlder_IsSuspended = false;
    }
  }

  private static float GetEditorFontHeightMm(ImRtfEditor editor, CharFormat charFormat)
  {
    if (!charFormat.FontSize.HasValue)
      return 0.0f;
    Font font = charFormat.GetFont();
    return UnitsConverter.PixelsToMm((int) editor.TerGr.MeasureString("A", font).Height, editor.TerGr.DpiY);
  }

  /// <summary>Количество заблокированных для редактирования первых символов</summary>
  internal int ProtectedFirstCharCount
  {
    [DebuggerStepThrough] get
    {
      ImRtfEditor editor = this.Editor;
      return editor != null ? editor.ProtectedFirstCharCount : 0;
    }
  }

  /// <summary>Установить значение ProtectedFirstCharCount в редакторе</summary>
  public virtual void SetProtectedFirstCharCount(int value)
  {
    this.SetProtectedFirstCharCount(this.Editor, value);
  }

  /// <summary>Является ли слово целым словом</summary>
  /// <param name="text">текст</param>
  /// <param name="word">слово</param>
  /// <param name="beginIndex">начальный индекс слова</param>
  /// <returns></returns>
  internal bool IsWholeWord(string text, string word, int beginIndex)
  {
    return (beginIndex == 0 || text[beginIndex - 1] == ' ') && (beginIndex + word.Length >= text.Length - 1 || text[beginIndex + word.Length] == ' ');
  }

  /// <summary>Заменить разрывные пробелы и дефис в тексте на неразрывные</summary>
  public virtual string SetSpases(string text)
  {
    string[] strArray = new string[5]
    {
      LocalizationHolder.rm.GetString("Document.Model_639"),
      LocalizationHolder.rm.GetString("Document.Model_640"),
      LocalizationHolder.rm.GetString("Document.Model_641"),
      "DIN",
      "ISO"
    };
    string text1 = text;
    for (int index1 = 0; index1 < strArray.Length; ++index1)
    {
      string word = strArray[index1];
      int beginIndex = -1;
      while ((beginIndex = text1.IndexOf(word, beginIndex + 1)) != -1)
      {
        if (this.IsWholeWord(text1, word, beginIndex))
        {
          int index2 = beginIndex;
          while (index2 < text1.Length && text1[index2] != ' ')
            ++index2;
          int num1 = index2;
          while (index2 < text1.Length && text1[index2] == ' ')
            ++index2;
          while (index2 < text1.Length && text1[index2] != ' ')
            ++index2;
          int num2 = index2;
          StringBuilder stringBuilder = new StringBuilder(text1);
          for (int index3 = num1; index3 < num2; ++index3)
          {
            if (stringBuilder[index3] == ' ')
              stringBuilder[index3] = ' ';
          }
          text1 = stringBuilder.ToString();
        }
      }
    }
    return text1;
  }

  /// <summary>Проверка является ли символ пробелом</summary>
  /// <param name="ch">Символ</param>
  /// <returns></returns>
  internal static bool IsWhiteSpace(char ch)
  {
    return char.IsWhiteSpace(ch) || RtfInSiteEditorWrapper.IsNonBreakSpace(ch);
  }

  /// <summary>Проверка является ли символ неразрывным пробелом</summary>
  /// <param name="ch">Символ</param>
  /// <returns></returns>
  internal static bool IsNonBreakSpace(char ch) => ch == '\u000E' || ch == ' ';

  /// <summary>Проверка является ли символ неразрывным пробелом</summary>
  /// <param name="ch">Символ</param>
  /// <returns></returns>
  internal static bool IsLineBreak(char ch)
  {
    return ch == '\u0015' || ch == '\r' || ch == '\n' || ch == '\u000F';
  }

  internal static bool IsWordSpliter(char ch)
  {
    return RtfInSiteEditorWrapper.IsWhiteSpace(ch) || RtfInSiteEditorWrapper.IsLineBreak(ch) || ch == '/' || ch == '\\';
  }

  /// <summary>Получить размер формулы</summary>
  /// <param name="im">Изображение</param>
  /// <param name="size">Размер</param>
  /// <returns></returns>
  private SizeF GetFormulaSize(Image im, SizeF size)
  {
    if (im == null || im.Height <= 0)
      return size;
    float num = (float) im.Width / (float) im.Height;
    return new SizeF(size.Height * num, size.Height);
  }

  /// <summary>
  /// Заменить текст формулы на ее графическое представление
  /// </summary>
  private void ReplaceIndexOrMaterialFormula(
    ImRtfEditor editor,
    bool isIndexFormula,
    string originalText,
    string argument1,
    string argument2,
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    ref List<int> avsMaterialPos)
  {
    if (isIndexFormula)
      this.ReplaceIndexFormula(editor, originalText, argument1, argument2, startLine, startCol, endLine, endCol);
    else
      this.PutMaterialFormulaAndSavePos(editor, originalText, argument1, argument2, startLine, startCol, endLine, endCol, ref avsMaterialPos);
  }

  /// <summary>
  /// Заменить текст формулы на ее графическое представление
  /// </summary>
  private void ReplaceIndexFormula(
    ImRtfEditor editor,
    string originalText,
    string argument1,
    string argument2,
    int startLine,
    int startCol,
    int endLine,
    int endCol)
  {
    string formulaText = string.Format(LocalizationHolder.rm.GetString("Document.Model_643"), (object) argument1, (object) argument2);
    this.ReplaceFormula(editor, originalText, formulaText, startLine, startCol, endLine, endCol);
  }

  /// <summary>
  /// Вставить изображение формулы в текст, заменив указанный фрагмент, и запомнить номер строки
  /// </summary>
  private void PutMaterialFormulaAndSavePos(
    ImRtfEditor editor,
    string originalText,
    string argument1,
    string argument2,
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    ref List<int> avsMaterialPos)
  {
    this.ReplaceMaterialFormula(editor, originalText, argument1, argument2, startLine, startCol, endLine, endCol);
    this.InsertFormatText(editor, startLine, startCol, ' ');
    int startCol1 = startCol + 1;
    this.InsertFormatText(editor, startLine, startCol1 + 1, ' ');
    RtfInSiteEditorWrapper.RememberMaterialPos(editor, startLine, startCol1, ref avsMaterialPos);
  }

  /// <summary>
  /// Заменить текст формулы "Материал" на графическое изображение
  /// </summary>
  private void ReplaceMaterialFormula(
    ImRtfEditor editor,
    string originalText,
    string argument1,
    string argument2,
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    bool forceIfProtected = false)
  {
    string formulaText = string.Format(LocalizationHolder.rm.GetString("Document.Model_644"), (object) argument1, (object) argument2);
    this.ReplaceFormula(editor, originalText, formulaText, startLine, startCol, endLine, endCol, forceIfProtected: forceIfProtected);
  }

  /// <summary>Запомнить номер строки</summary>
  private static void RememberMaterialPos(
    ImRtfEditor editor,
    int startLine,
    int startCol,
    ref List<int> avsMaterialPos)
  {
    if (avsMaterialPos == null)
      avsMaterialPos = new List<int>();
    int abs = editor.TerRowColToAbs(startLine, startCol, scanAllChars: true);
    if (avsMaterialPos.Contains(abs))
      return;
    avsMaterialPos.Add(abs);
  }

  private void ReplaceFormula(
    ImRtfEditor editor,
    string originalText,
    string formulaText,
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    CharFormat charFormat = null,
    bool forceIfProtected = false)
  {
    FormList formulas = this.owner.OwnerDocument is ImDocument ownerDocument ? ownerDocument.FindFormulas(formulaText) : (FormList) null;
    if (formulas == null || formulas.IsEmptyPages())
      return;
    if (charFormat != null)
      formulas.List[0].SetCharFormatForAllFields(charFormat);
    else
      formulas.List[0].SetCharFormatForAllFields(this.owner.CharFormat);
    int num = editor.CheckTextTag(startLine, startCol, 77) ? 1 : 0;
    Metafile metafile = formulas.GetMetafile();
    SizeF formulaSize = new SizeF(formulas.totalSize.Width, formulas.totalSize.Height);
    if (formulas.List.Any<Formula>((Func<Formula, bool>) (f => f.IsIndexFormula)))
      this.AdjustIndexFormulaRenderingSize(editor, ref formulaSize);
    if (formulas.List.Any<Formula>((Func<Formula, bool>) (f => f.IsMaterialFormula)))
      this.AdjustMaterialFormulaRenderingSize(ref formulaSize);
    editor.SelectTerText(startLine, startCol, endLine, endCol, false);
    editor.TerDeleteBlock(false, forceIfProtected);
    editor.blk.TerPastePicture("", (Image) metafile, 0, this.ConvertVertAlignmentToPictAlign(new PictAlignmentInText?(formulas.AlignInText)), true, UnitsConverter.MmToTwips(formulaSize), UnitsConverter.MmToTwips(formulas.Offset), false, forceIfProtected);
    editor.SetTextTags(startLine, startCol, startLine, startCol, 79, (string) null, originalText, 0);
    if (num == 0)
      return;
    editor.SetTextTags(startLine, startCol, startLine, startCol, 77, (string) null, (string) null, 0);
  }

  private void AdjustIndexFormulaRenderingSize(ImRtfEditor editor, ref SizeF formulaSize)
  {
    float editorFontHeightMm = RtfInSiteEditorWrapper.GetEditorFontHeightMm(editor, this.owner.CharFormat.Clone());
    if ((double) editorFontHeightMm <= 0.0)
      return;
    float num = editorFontHeightMm * Formula.FormulaImageZoomFactor_Index;
    formulaSize.Width *= num / formulaSize.Height;
    formulaSize.Height = num;
  }

  private void AdjustMaterialFormulaRenderingSize(ref SizeF formulaSize)
  {
    float num1 = (float) ((double) this.owner.Size.Width - (double) (this.owner.Margins.Left + this.owner.Margins.Right) - 2.0);
    float width = formulaSize.Width;
    if ((double) width <= (double) num1)
      return;
    float num2 = num1 / width;
    formulaSize.Width = num1;
    formulaSize.Height *= num2;
  }

  private TextPosition FindEndOfComplexDesignationFormula(
    ImRtfEditor editor,
    TextPosition startPosition)
  {
    List<char> symbols = new List<char>()
    {
      '\u0015',
      ' ',
      ','
    };
    TextPosition position = this.FindSymbol(editor, startPosition, symbols);
    if (position.IsEmpty)
    {
      position = new TextPosition(editor.TotalLines - 1, editor.text[editor.TotalLines - 1].len - 1);
    }
    else
    {
      ImRtfEditorTextEnumerator editorTextEnumerator = new ImRtfEditorTextEnumerator(editor, position.Line, position.Column);
      editorTextEnumerator.MoveNext();
      if (editorTextEnumerator.Current == ' ')
      {
        string str = "";
        TextPosition textPosition = new TextPosition();
        while (editorTextEnumerator.MoveNext())
        {
          if (symbols.Contains(editorTextEnumerator.Current))
            textPosition = new TextPosition(editorTextEnumerator.CurLine, editorTextEnumerator.CurColumn);
          else
            str += editorTextEnumerator.Current.ToString();
        }
        if (ImDocumentData.ComplexDesignationSuffixs.Contains(str.Trim()))
          position = textPosition;
      }
      position = ImRtfEditorTextEnumerator.GetPrevPosition(editor, position);
    }
    return position;
  }

  private TextPosition FindEndOfMaterialFormula(ImRtfEditor editor, TextPosition startPosition)
  {
    List<char> symbols = new List<char>()
    {
      '\u0015',
      '\u000E',
      ' '
    };
    TextPosition position = this.FindSymbol(editor, startPosition, symbols);
    position = !position.IsEmpty ? ImRtfEditorTextEnumerator.GetPrevPosition(editor, position) : new TextPosition(editor.TotalLines - 1, editor.text[editor.TotalLines - 1].len - 1);
    return position;
  }

  private TextPosition FindSymbol(
    ImRtfEditor editor,
    TextPosition startPosition,
    List<char> symbols)
  {
    ImRtfEditorTextEnumerator editorTextEnumerator = new ImRtfEditorTextEnumerator(editor, startPosition.Line, startPosition.Column);
    while (editorTextEnumerator.MoveNext())
    {
      if (symbols.Contains(editorTextEnumerator.Current))
        return new TextPosition(editorTextEnumerator.CurLine, editorTextEnumerator.CurColumn);
    }
    return TextPosition.Empty;
  }

  private RtfInSiteEditorWrapper.KeywordWithPosition SearchForNearestKeywordBackward(
    ImRtfEditor editor,
    TextPosition startPosition,
    TextPosition stopPosition,
    List<string> keywords)
  {
    return this.SelectLongestKeyword(this.SelectNearestToEndKeyword(this.SearchForKeywordsBackward(editor, startPosition, stopPosition, keywords)));
  }

  private RtfInSiteEditorWrapper.KeywordWithPosition SearchNearestKeywordBeforeSlash(
    ImRtfEditor editor,
    TextPosition startPosition,
    List<string> keywords,
    out TextPosition slashPosition)
  {
    return this.SelectLongestKeyword(this.SelectNearestToEndKeyword(this.SearchKeywordsBeforeSlash(editor, startPosition, keywords, out slashPosition)));
  }

  private RtfInSiteEditorWrapper.KeywordWithPosition SelectLongestKeyword(
    List<RtfInSiteEditorWrapper.KeywordWithPosition> foundKeywords)
  {
    if (foundKeywords.Count == 0)
      return (RtfInSiteEditorWrapper.KeywordWithPosition) null;
    RtfInSiteEditorWrapper.KeywordWithPosition keywordWithPosition = foundKeywords[0];
    foreach (RtfInSiteEditorWrapper.KeywordWithPosition foundKeyword in foundKeywords)
    {
      if (keywordWithPosition.Keyword.Length < foundKeyword.Keyword.Length)
        keywordWithPosition = foundKeyword;
    }
    return keywordWithPosition;
  }

  private List<RtfInSiteEditorWrapper.KeywordWithPosition> SelectNearestToEndKeyword(
    List<RtfInSiteEditorWrapper.KeywordWithPosition> foundKeywords)
  {
    List<RtfInSiteEditorWrapper.KeywordWithPosition> endKeyword = new List<RtfInSiteEditorWrapper.KeywordWithPosition>();
    if (foundKeywords.Count == 0)
      return endKeyword;
    endKeyword.Add(foundKeywords[0]);
    foreach (RtfInSiteEditorWrapper.KeywordWithPosition foundKeyword in foundKeywords)
    {
      if (!endKeyword.Contains(foundKeyword))
      {
        int num = foundKeyword.Position.End.CompareTo(endKeyword[0].Position.End);
        if (num == 0)
          endKeyword.Add(foundKeyword);
        else if (num > 0)
        {
          endKeyword.Clear();
          endKeyword.Add(foundKeyword);
        }
      }
    }
    return endKeyword;
  }

  private List<RtfInSiteEditorWrapper.KeywordWithPosition> SearchForKeywordsBackward(
    ImRtfEditor editor,
    TextPosition startPosition,
    TextPosition stopPosition,
    List<string> keywords)
  {
    List<RtfInSiteEditorWrapper.CheckingKeyword_Backward> checkingKeywordBackwardList = new List<RtfInSiteEditorWrapper.CheckingKeyword_Backward>(keywords.Count);
    List<RtfInSiteEditorWrapper.KeywordWithPosition> collection = new List<RtfInSiteEditorWrapper.KeywordWithPosition>(keywords.Count);
    List<RtfInSiteEditorWrapper.KeywordWithPosition> keywordWithPositionList = new List<RtfInSiteEditorWrapper.KeywordWithPosition>(keywords.Count);
    for (int line = startPosition.Line; line >= stopPosition.Line; --line)
    {
      int num = line == startPosition.Line ? startPosition.Column : editor.text[line].len - 1;
      int column1 = line == stopPosition.Line ? stopPosition.Column : 0;
      for (int column2 = num; column2 >= column1; --column2)
      {
        char ch = editor.text[line].txt[column2];
        if (RtfInSiteEditorWrapper.IsWordSpliter(ch))
          keywordWithPositionList.AddRange((IEnumerable<RtfInSiteEditorWrapper.KeywordWithPosition>) collection);
        collection.Clear();
        if (ch != '\u0015')
        {
          for (int index = checkingKeywordBackwardList.Count - 1; index >= 0; --index)
          {
            if (!checkingKeywordBackwardList[index].CheckPrevChar(ch))
              checkingKeywordBackwardList.RemoveAt(index);
            else if (checkingKeywordBackwardList[index].IsWholeChecked)
            {
              collection.Add(new RtfInSiteEditorWrapper.KeywordWithPosition(checkingKeywordBackwardList[index], new TextPosition(line, column2)));
              checkingKeywordBackwardList.RemoveAt(index);
            }
          }
          for (int index = 0; index < keywords.Count; ++index)
          {
            if ((int) char.ToUpper(ch) == (int) char.ToUpper(keywords[index][keywords[index].Length - 1]))
              checkingKeywordBackwardList.Add(new RtfInSiteEditorWrapper.CheckingKeyword_Backward(keywords[index], new TextPosition(line, column2)));
          }
        }
        else
          break;
      }
    }
    keywordWithPositionList.AddRange((IEnumerable<RtfInSiteEditorWrapper.KeywordWithPosition>) collection);
    return keywordWithPositionList;
  }

  private EditorTextBlock SearchForKeywordForwardInCurrentParagraph(
    ImRtfEditor editor,
    TextPosition startPosition,
    string keyword)
  {
    int index = 0;
    EditorTextBlock empty = EditorTextBlock.Empty;
    ImRtfEditorTextEnumerator editorTextEnumerator = new ImRtfEditorTextEnumerator(editor, startPosition.Line, startPosition.Column);
    while (editorTextEnumerator.MoveNext() && editorTextEnumerator.Current != '\u0015')
    {
      if (!empty.End.IsEmpty)
      {
        if (!RtfInSiteEditorWrapper.IsWhiteSpace(editorTextEnumerator.Current))
        {
          empty = EditorTextBlock.Empty;
          break;
        }
        break;
      }
      if ((int) char.ToUpper(editorTextEnumerator.Current) == (int) char.ToUpper(keyword[index]))
      {
        if (index == 0)
          empty.Start = new TextPosition(editorTextEnumerator.CurLine, editorTextEnumerator.CurColumn);
        if (index == keyword.Length - 1)
          empty.End = new TextPosition(editorTextEnumerator.CurLine, editorTextEnumerator.CurColumn);
        ++index;
      }
      else
        break;
    }
    return empty;
  }

  public static bool HasMaterialKeyword(string text, List<string> keywords)
  {
    return !string.IsNullOrWhiteSpace(text) && keywords.Any<string>((Func<string, bool>) (k => text.IndexOf(k, StringComparison.InvariantCultureIgnoreCase) != -1));
  }

  private List<RtfInSiteEditorWrapper.KeywordWithPosition> SearchKeywordsBeforeSlash(
    ImRtfEditor editor,
    TextPosition startPosition,
    List<string> keywords,
    out TextPosition slashPosition)
  {
    slashPosition = TextPosition.Empty;
    List<RtfInSiteEditorWrapper.CheckingKeyword_Forward> checkingKeywordForwardList = new List<RtfInSiteEditorWrapper.CheckingKeyword_Forward>(keywords.Count);
    List<RtfInSiteEditorWrapper.KeywordWithPosition> collection = new List<RtfInSiteEditorWrapper.KeywordWithPosition>(keywords.Count);
    List<RtfInSiteEditorWrapper.KeywordWithPosition> keywordWithPositionList = new List<RtfInSiteEditorWrapper.KeywordWithPosition>(keywords.Count);
    ImRtfEditorTextEnumerator editorTextEnumerator = new ImRtfEditorTextEnumerator(editor, startPosition.Line, startPosition.Column);
    while (editorTextEnumerator.MoveNext())
    {
      char current = editorTextEnumerator.Current;
      if (current == '\u0015' || editor.CheckTextTag(editorTextEnumerator.CurLine, editorTextEnumerator.CurColumn, (IList<int>) tc.ReplacedCharTags))
        return new List<RtfInSiteEditorWrapper.KeywordWithPosition>();
      if (RtfInSiteEditorWrapper.IsWordSpliter(current))
        keywordWithPositionList.AddRange((IEnumerable<RtfInSiteEditorWrapper.KeywordWithPosition>) collection);
      collection.Clear();
      if (current == '/')
      {
        slashPosition = editorTextEnumerator.CurrentPosition;
        break;
      }
      for (int index = checkingKeywordForwardList.Count - 1; index >= 0; --index)
      {
        if (!checkingKeywordForwardList[index].IsWholeChecked && !checkingKeywordForwardList[index].CheckNextChar(current))
          checkingKeywordForwardList.RemoveAt(index);
        else if (checkingKeywordForwardList[index].IsWholeChecked)
        {
          collection.Add(new RtfInSiteEditorWrapper.KeywordWithPosition(checkingKeywordForwardList[index], editorTextEnumerator.CurrentPosition));
          checkingKeywordForwardList.RemoveAt(index);
        }
      }
      for (int index = 0; index < keywords.Count; ++index)
      {
        if ((int) char.ToUpper(current) == (int) char.ToUpper(keywords[index][0]))
          checkingKeywordForwardList.Add(new RtfInSiteEditorWrapper.CheckingKeyword_Forward(keywords[index], editorTextEnumerator.CurrentPosition));
      }
    }
    return keywordWithPositionList;
  }

  private string GetTextBlock(ImRtfEditor editor, TextPosition start, TextPosition end)
  {
    StringBuilder stringBuilder = new StringBuilder();
    ImRtfEditorTextEnumerator editorTextEnumerator = new ImRtfEditorTextEnumerator(editor, start, end);
    while (editorTextEnumerator.MoveNext())
      stringBuilder.Append(editorTextEnumerator.Current);
    return stringBuilder.ToString();
  }

  private void InsertFormatText(ImRtfEditor editor, TextPosition position, char formatChar)
  {
    this.InsertFormatText(editor, position.Line, position.Column, formatChar);
  }

  private void InsertFormatText(ImRtfEditor editor, int line, int column, char formatChar)
  {
    editor.CurLine = line;
    editor.CurCol = column;
    editor.blk.InsertTerText(formatChar.ToString(), false, false);
    editor.SetTextTags(line, column, line, column, 79, (string) null, (string) null, 0);
  }

  /// <summary>
  /// Ищем составное обозначение, в нем части разделяются при помощи |
  /// </summary>
  /// <param name="editor"></param>
  /// <param name="startLine"></param>
  /// <param name="startCol"></param>
  /// <param name="avsMaterialPos"></param>
  /// <returns></returns>
  private bool SearchAndReplaceComplexDesignation(
    ImRtfEditor editor,
    ref int startLine,
    ref int startCol,
    ref List<int> avsMaterialPos)
  {
    if (editor.CheckTextTag(startLine, startCol, (IList<int>) tc.ReplacedCharTags))
      return false;
    TextPosition textPosition1 = new TextPosition(startLine, startCol);
    TextPosition textPosition2 = textPosition1;
    TextPosition empty = TextPosition.Empty;
    TextPosition position = TextPosition.Empty;
    ImRtfEditorTextEnumerator editorTextEnumerator = new ImRtfEditorTextEnumerator(editor, textPosition1.Line, textPosition1.Column);
    while (editorTextEnumerator.MoveNext())
    {
      char current = editorTextEnumerator.Current;
      if (current == '\u0015' || editor.CheckTextTag(editorTextEnumerator.CurLine, editorTextEnumerator.CurColumn, (IList<int>) tc.ReplacedCharTags))
        return false;
      if (RtfInSiteEditorWrapper.IsWordSpliter(current) && position.IsEmpty)
      {
        TextPosition currentPosition = editorTextEnumerator.CurrentPosition;
        textPosition2 = ImRtfEditorTextEnumerator.GetNextPosition(editor, currentPosition);
      }
      if (current == '|')
      {
        position = editorTextEnumerator.CurrentPosition;
        break;
      }
    }
    if (position.IsEmpty)
      return false;
    TextPosition textPosition3 = textPosition2;
    TextPosition prevPosition = ImRtfEditorTextEnumerator.GetPrevPosition(editor, position);
    string textBlock1 = this.GetTextBlock(editor, textPosition3, prevPosition);
    char ch = Convert.ToChar(160 /*0xA0*/);
    string newValue1 = ch.ToString();
    string str1 = textBlock1.Replace("_", newValue1);
    TextPosition nextPosition1 = ImRtfEditorTextEnumerator.GetNextPosition(editor, position);
    TextPosition designationFormula = this.FindEndOfComplexDesignationFormula(editor, nextPosition1);
    string textBlock2 = this.GetTextBlock(editor, nextPosition1, designationFormula);
    ch = Convert.ToChar(160 /*0xA0*/);
    string newValue2 = ch.ToString();
    string str2 = textBlock2.Replace("_", newValue2);
    TextPosition textPosition4 = designationFormula;
    TextPosition nextPosition2 = ImRtfEditorTextEnumerator.GetNextPosition(editor, textPosition4);
    int num1 = editor.text[nextPosition2.Line].txt[nextPosition2.Column] == ' ' ? 1 : 0;
    if (num1 != 0)
    {
      textPosition4 = nextPosition2;
      nextPosition2 = ImRtfEditorTextEnumerator.GetNextPosition(editor, textPosition4);
    }
    string textBlock3 = this.GetTextBlock(editor, textPosition3, textPosition4);
    string formulaText = string.Format(LocalizationHolder.rm.GetString("Document.Model_655"), (object) str2, (object) str1);
    CharFormat charFormat = this.Owner.CharFormat.Clone();
    charFormat.Underline = new UnderlineStyle?(UnderlineStyle.None);
    ParagraphFormat paragraphFormat = this.Owner.ParagraphFormat.Clone();
    paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Center);
    this.Owner.SetParagraphFormat(paragraphFormat, false, false);
    if (this.Owner is TextBoxElement)
    {
      TextBoxElement owner = this.Owner as TextBoxElement;
      if ((double) owner.Bounds.Height < 15.0 && !owner.AutoSizeHeight)
      {
        formulaText = string.Format(LocalizationHolder.rm.GetString("Document.Model_654"), (object) str2, (object) str1);
        float? fontSize = charFormat.FontSize;
        float num2 = 8f;
        if ((double) fontSize.GetValueOrDefault() > (double) num2 & fontSize.HasValue)
          charFormat.FontSize = new float?(8f);
      }
    }
    this.ReplaceFormula(editor, textBlock3, formulaText, textPosition3.Line, textPosition3.Column, nextPosition2.Line, nextPosition2.Column, charFormat);
    if (num1 != 0)
    {
      TextPosition nextPosition3 = ImRtfEditorTextEnumerator.GetNextPosition(editor, textPosition3);
      this.InsertFormatText(editor, nextPosition3, ' ');
    }
    RtfInSiteEditorWrapper.RememberMaterialPos(editor, textPosition3.Line, textPosition3.Column, ref avsMaterialPos);
    startLine = designationFormula.Line;
    startCol = designationFormula.Column;
    return true;
  }

  private bool SearchAndReplaceMaterialKeyword(
    ImRtfEditor editor,
    ref int startLine,
    ref int startCol,
    ref List<int> avsMaterialPos)
  {
    if (editor.CheckTextTag(startLine, startCol, (IList<int>) tc.ReplacedCharTags))
      return false;
    TextPosition startPosition = new TextPosition(startLine, startCol);
    List<string> materialKeyWords = this.GetMaterialKeyWords();
    TextPosition slashPosition;
    RtfInSiteEditorWrapper.KeywordWithPosition keywordWithPosition = this.SearchNearestKeywordBeforeSlash(editor, startPosition, materialKeyWords, out slashPosition);
    if (keywordWithPosition == null)
      return false;
    this.GetTextBlock(editor, keywordWithPosition.Start, keywordWithPosition.End);
    TextPosition nextPosition1 = ImRtfEditorTextEnumerator.GetNextPosition(editor, keywordWithPosition.End);
    TextPosition nextPosition2 = ImRtfEditorTextEnumerator.GetNextPosition(editor, nextPosition1);
    TextPosition prevPosition1 = ImRtfEditorTextEnumerator.GetPrevPosition(editor, slashPosition);
    string textBlock1 = this.GetTextBlock(editor, nextPosition2, prevPosition1);
    TextPosition nextPosition3 = ImRtfEditorTextEnumerator.GetNextPosition(editor, slashPosition);
    TextPosition ofMaterialFormula = this.FindEndOfMaterialFormula(editor, nextPosition3);
    string textBlock2 = this.GetTextBlock(editor, nextPosition3, ofMaterialFormula);
    TextPosition textPosition = ofMaterialFormula;
    TextPosition nextPosition4 = ImRtfEditorTextEnumerator.GetNextPosition(editor, textPosition);
    int num = editor.text[nextPosition4.Line].txt[nextPosition4.Column] == ' ' ? 1 : 0;
    if (num != 0)
    {
      textPosition = nextPosition4;
      nextPosition4 = ImRtfEditorTextEnumerator.GetNextPosition(editor, textPosition);
    }
    string textBlock3 = this.GetTextBlock(editor, nextPosition2, textPosition);
    string formulaText = string.Format(LocalizationHolder.rm.GetString("Document.Model_644"), (object) textBlock1, (object) textBlock2);
    this.ReplaceFormula(editor, textBlock3, formulaText, nextPosition2.Line, nextPosition2.Column, nextPosition4.Line, nextPosition4.Column);
    if (num != 0)
    {
      TextPosition nextPosition5 = ImRtfEditorTextEnumerator.GetNextPosition(editor, nextPosition2);
      this.InsertFormatText(editor, nextPosition5, ' ');
    }
    TextPosition prevPosition2 = ImRtfEditorTextEnumerator.GetPrevPosition(editor, keywordWithPosition.Start);
    if (!prevPosition2.IsEmpty && !RtfInSiteEditorWrapper.IsLineBreak(editor.text[prevPosition2.Line].txt[prevPosition2.Column]))
    {
      int abs1 = editor.TerRowColToAbs(nextPosition2.Line, nextPosition2.Column);
      this.InsertFormatText(editor, keywordWithPosition.Start, '\u0015');
      int abs2 = abs1 + 2;
      editor.TerAbsToRowCol(abs2, out nextPosition2.Line, out nextPosition2.Column);
    }
    RtfInSiteEditorWrapper.RememberMaterialPos(editor, nextPosition2.Line, nextPosition2.Column, ref avsMaterialPos);
    startLine = nextPosition2.Line;
    startCol = nextPosition2.Column;
    return true;
  }

  /// <summary>Заменить разрывные пробелы и дефис в тексте на неразрывные и вставить формулы</summary>
  /// <param name="editor">Редактор, в котором заменять символы</param>
  /// <param name="replaceSpecChar">Заменять спецсимволы '~' на неразрывный пробел и '?' на разрыв строки</param>
  /// <param name="replaceFormulas">Заменять формулы в виде 'Имя:аргумент1;аргумент2;...' на символы из библиотеки формул</param>
  /// <param name="replaceAVSMaterial">Заменять материалы по списку ключевых слов или спецсимволам.
  /// После '/S' - числитель, после '/' - знаменатель
  /// После '^' - верхний индекс, после '/' - нижний</param>
  /// <param name="nonbreakingText">Неразрывный текст</param>
  /// <param name="avsMaterialPos">Позиции заменённых материалов</param>
  /// <returns>true, если были новые замены</returns>
  public bool ReplaceSpecSymbolAndFormulas(
    ImRtfEditor editor,
    bool replaceSpecChar,
    bool replaceFormulas,
    bool replaceAVSMaterial,
    string nonbreakingText,
    out List<int> avsMaterialPos)
  {
    this.HasFormulas = false;
    if (editor == null)
      throw new ArgumentNullException(nameof (editor));
    bool flag1 = false;
    avsMaterialPos = (List<int>) null;
    string[] strArray1 = new string[6]
    {
      LocalizationHolder.rm.GetString("Document.Model_639"),
      LocalizationHolder.rm.GetString("Document.Model_640"),
      LocalizationHolder.rm.GetString("Document.Model_641"),
      "DIN",
      "ISO",
      LocalizationHolder.rm.GetString("Document.Model_642")
    };
    int num1 = 0;
    int num2 = 1;
    bool flag2 = true;
    int num3 = 5;
    ImDocument ownerDoc = (ImDocument) null;
    if (this.owner != null)
      ownerDoc = this.owner.OwnerDocument as ImDocument;
    replaceFormulas &= ownerDoc != null;
    replaceAVSMaterial &= ownerDoc != null;
    List<string> stringList = (List<string>) null;
    if (ownerDoc != null)
      stringList = ownerDoc.MaterialKeyWords;
    int num4 = 0;
    int startCol1 = -1;
    int startLine1 = -1;
    int num5 = -1;
    int num6 = -1;
    int num7 = 0;
    int index1 = -1;
    int index2 = -1;
    int num8 = -1;
    int num9 = -1;
    int index3 = -1;
    int num10 = -1;
    int num11 = -1;
    int num12 = -1;
    int num13 = -1;
    char c = char.MinValue;
    char ch = char.MinValue;
    int num14 = 0;
    int num15 = 0;
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = (StringBuilder) null;
    StringBuilder stringBuilder3 = (StringBuilder) null;
    StringBuilder stringBuilder4 = (StringBuilder) null;
    bool flag3 = (editor.TerFlags3 & 262144 /*0x040000*/) != 0;
    editor.TerSetFlags3(true, 262144 /*0x040000*/);
    SelectionBlock selectionBlock = editor.GetSelectionBlock();
    bool isIndexFormula = false;
    if (replaceSpecChar)
    {
      for (int index4 = 0; index4 < editor.TotalLines; ++index4)
      {
        for (int index5 = 0; index5 < editor.text[index4].len; ++index5)
        {
          ch = editor.text[index4].txt[index5];
          switch (ch)
          {
            case '?':
              editor.text[index4].txt[index5] = '\u0015';
              editor.SetTextTags(index4, index5, index4, index5, 79, (string) null, "?", 0);
              break;
            case '~':
              editor.text[index4].txt[index5] = '\u000E';
              editor.SetTextTags(index4, index5, index4, index5, 79, (string) null, "~", 0);
              break;
          }
        }
      }
    }
    for (int startLine2 = 0; startLine2 < editor.TotalLines; ++startLine2)
    {
      for (int startCol2 = 0; startCol2 < editor.text[startLine2].len; ++startCol2)
      {
        if (startCol2 != 0 || startLine2 != 0)
          c = ch;
        ch = editor.text[startLine2].txt[startCol2];
        if (!string.IsNullOrEmpty(nonbreakingText))
        {
          if (index3 != -1 && index3 < nonbreakingText.Length)
          {
            ++index3;
            if (index3 < nonbreakingText.Length)
            {
              if ((int) char.ToUpper(ch) != (int) nonbreakingText[index3])
                index3 = -1;
              else if (index3 == nonbreakingText.Length - 1)
              {
                num13 = startLine2;
                num12 = startCol2;
              }
            }
            else
            {
              for (int index6 = num11; index6 <= num13; ++index6)
              {
                for (int index7 = num10; index6 == num13 && index7 <= num12 || index6 != num13 && index7 < editor.text[index6].len; ++index7)
                {
                  if (editor.text[index6].txt[index7] == ' ')
                  {
                    editor.text[index6].txt[index7] = '\u000E';
                    editor.SetTextTags(index6, index7, index6, index7, 79, (string) null, " ", 0);
                    flag1 = true;
                  }
                  else if (editor.text[index6].txt[index7] == '-')
                  {
                    editor.text[index6].txt[index7] = '\u0017';
                    editor.SetTextTags(index6, index7, index6, index7, 79, (string) null, "-", 0);
                    flag1 = true;
                  }
                }
                num10 = 0;
              }
            }
          }
          else if ((int) char.ToUpper(ch) == (int) nonbreakingText[0])
          {
            index3 = 0;
            num11 = startLine2;
            num10 = startCol2;
          }
        }
        if (this.owner.NeedUpdateFormulas)
        {
          int index8 = (int) editor.text[startLine2]?.tag?[startCol2] ?? 0;
          if (index8 != 0 && editor.CharTag[index8].type == 79 && ch == '\u0018')
          {
            string auxText = editor.CharTag[index8].AuxText;
            if (auxText.StartsWith("<<") && auxText.Length > 4 || auxText.StartsWith("«") && auxText.Length > 2)
            {
              FormList formulas = ownerDoc.FindFormulas(auxText);
              if (!formulas.IsEmptyPages())
                flag1 = this.ReplaceTextByFormulaImage(editor, ownerDoc, formulas, auxText, startLine2, startCol2, startLine2, startCol2, ref avsMaterialPos, true, false);
            }
            if (auxText.ToUpper().StartsWith("\\S"))
            {
              string[] strArray2 = auxText.Split(new string[4]
              {
                "\\S",
                "\\s",
                "/",
                ";"
              }, StringSplitOptions.RemoveEmptyEntries);
              int endLine = startCol2 == editor.text[startLine2].len - 1 ? startLine2 + 1 : startLine2;
              int endCol = startCol2 == editor.text[startLine2].len - 1 ? 0 : startCol2 + 1;
              if (endLine == editor.TotalLines)
              {
                endLine = startLine2;
                endCol = startCol2;
              }
              this.ReplaceMaterialFormula(editor, auxText, strArray2[0], strArray2[1], startLine2, startCol2, endLine, endCol, true);
              flag1 = true;
            }
          }
        }
        if (replaceFormulas && num14 > 0)
        {
          switch (num14)
          {
            case 1:
              if (ch == '<')
              {
                stringBuilder1.Append(ch);
                num14 = 2;
                continue;
              }
              num14 = 0;
              continue;
            case 2:
              switch (ch)
              {
                case '<':
                  stringBuilder1.Length = 1;
                  num14 = 1;
                  startCol1 = startCol2;
                  startLine1 = startLine2;
                  continue;
                case '>':
                  stringBuilder1.Append(ch);
                  num14 = 3;
                  continue;
                default:
                  stringBuilder1.Append(ch);
                  continue;
              }
            case 3:
              if (ch == '>')
              {
                stringBuilder1.Append(ch);
                if (stringBuilder1.Length > 4)
                {
                  FormList formulas = ownerDoc.FindFormulas(stringBuilder1.ToString());
                  if (!formulas.IsEmptyPages())
                  {
                    flag1 = this.ReplaceTextByFormulaImage(editor, ownerDoc, formulas, stringBuilder1.ToString(), startLine1, startCol1, startLine2, startCol2, ref avsMaterialPos, true, false);
                    startLine2 = startLine1;
                    startCol2 = startCol1;
                  }
                }
                num14 = 0;
                stringBuilder1.Length = 0;
                continue;
              }
              num14 = 0;
              continue;
          }
        }
        if (replaceFormulas && ch == '<')
        {
          stringBuilder1.Length = 0;
          stringBuilder1.Append(ch);
          num14 = 1;
          startCol1 = startCol2;
          startLine1 = startLine2;
        }
        else if (replaceFormulas && num15 > 0 && num15 == 1)
        {
          switch (ch)
          {
            case '«':
              stringBuilder1.Length = 1;
              num15 = 1;
              startCol1 = startCol2;
              startLine1 = startLine2;
              continue;
            case '»':
              stringBuilder1.Append(ch);
              if (stringBuilder1.Length > 2)
              {
                FormList formulas = ownerDoc.FindFormulas(stringBuilder1.ToString());
                if (!formulas.IsEmptyPages())
                {
                  flag1 = this.ReplaceTextByFormulaImage(editor, ownerDoc, formulas, stringBuilder1.ToString(), startLine1, startCol1, startLine2, startCol2, ref avsMaterialPos, true, false);
                  startLine2 = startLine1;
                  startCol2 = startCol1;
                }
              }
              num15 = 0;
              stringBuilder1.Length = 0;
              continue;
            default:
              stringBuilder1.Append(ch);
              continue;
          }
        }
        else if (replaceFormulas && ch == '«')
        {
          stringBuilder1.Length = 0;
          stringBuilder1.Append(ch);
          num15 = 1;
          startCol1 = startCol2;
          startLine1 = startLine2;
        }
        else
        {
          if (editor.CheckTextTag(startLine2, startCol2, (IList<int>) tc.ReplacedCharTags))
          {
            string auxText = editor.CharTag[(int) editor.text[startLine2].tag[startCol2]].AuxText;
            if (auxText != null && (auxText.StartsWith("<<") || auxText.StartsWith("«")))
              this.HasFormulas = true;
            if (auxText != null && auxText.ToUpper().IndexOf("\\S") != -1 && auxText.IndexOf("^") == -1 || this.owner.IsFixedSizeRows && (double) UnitsConverter.TwipsToMm((float) editor.text[startLine2].height) > (double) this.owner.DefaultRowSize * 1.25)
              RtfInSiteEditorWrapper.RememberMaterialPos(editor, startLine2, startCol2, ref avsMaterialPos);
            editor.HasTextReplaces = true;
          }
          bool flag4 = false;
          string attributeValue = this.owner.GetAttributeValue(DocumentTreeNode.AttributeName_ComplexDesignation, false);
          if (attributeValue != null)
          {
            bool result = false;
            if (bool.TryParse(attributeValue, out result))
              flag4 = result;
          }
          bool flag5 = false;
          if (flag4 && this.SearchAndReplaceComplexDesignation(editor, ref startLine2, ref startCol2, ref avsMaterialPos))
          {
            flag1 = true;
            flag5 = true;
            this.HasFormulas = true;
          }
          if (replaceAVSMaterial && !flag5)
          {
            if (num4 == 1)
            {
              if (ch == 'S' || ch == 's')
              {
                num4 = 2;
                stringBuilder1.Length = 0;
                stringBuilder1.Append("\\" + ch.ToString());
                if (stringBuilder2 != null)
                  stringBuilder2.Length = 0;
                else
                  stringBuilder2 = new StringBuilder();
                if (stringBuilder3 != null)
                  stringBuilder3.Length = 0;
                else
                  stringBuilder3 = new StringBuilder();
                if (stringBuilder4 != null)
                {
                  stringBuilder4.Length = 0;
                  continue;
                }
                stringBuilder4 = new StringBuilder();
                continue;
              }
              num4 = 0;
            }
            else
            {
              if (num4 > 1 && ch == '\u0015' || num4 == 3 && ch == ';')
              {
                if (ch == '\u0015' && (num4 == 3 || num4 == 4) || num4 == 3 && ch == ';')
                {
                  if (ch == ';')
                  {
                    stringBuilder1.Append(ch);
                    ++startCol2;
                  }
                  this.ReplaceIndexOrMaterialFormula(editor, isIndexFormula, stringBuilder1.ToString(), stringBuilder2?.ToString(), stringBuilder3?.ToString(), startLine1, startCol1, startLine2, startCol2, ref avsMaterialPos);
                  flag1 = true;
                }
                startLine2 = startLine1;
                startCol2 = startCol1;
                startLine1 = -1;
                startCol1 = -1;
                num4 = 0;
                if (stringBuilder2 != null)
                  stringBuilder2.Length = 0;
                if (stringBuilder3 != null)
                  stringBuilder3.Length = 0;
                if (stringBuilder4 != null)
                {
                  stringBuilder4.Length = 0;
                  continue;
                }
                continue;
              }
              switch (num4)
              {
                case 2:
                  stringBuilder1.Append(ch);
                  switch (ch)
                  {
                    case '/':
                      isIndexFormula = false;
                      num4 = 3;
                      continue;
                    case '^':
                      isIndexFormula = true;
                      num4 = 3;
                      continue;
                    default:
                      if (stringBuilder2 != null)
                      {
                        stringBuilder2.Append(ch);
                        continue;
                      }
                      continue;
                  }
                case 3:
                  stringBuilder1.Append(ch);
                  if (ch == ';')
                  {
                    num4 = 4;
                    continue;
                  }
                  if (stringBuilder3 != null)
                  {
                    stringBuilder3.Append(ch);
                    continue;
                  }
                  continue;
                case 4:
                  stringBuilder1.Append(ch);
                  if (stringBuilder4 != null)
                  {
                    stringBuilder4.Append(ch);
                    continue;
                  }
                  continue;
                default:
                  if (ch == '\\')
                  {
                    char minValue = char.MinValue;
                    if (startCol2 < editor.text[startLine2].len - 1)
                      minValue = editor.text[startLine2].txt[startCol2 + 1];
                    else if (startLine2 < editor.TotalLines - 1)
                      minValue = editor.text[startLine2 + 1].txt[0];
                    if (char.ToUpper(minValue) == 'S')
                    {
                      num4 = 1;
                      startLine1 = startLine2;
                      startCol1 = startCol2;
                      continue;
                    }
                    editor.text[startLine2].txt[startCol2] = '/';
                    editor.SetTextTags(startLine2, startCol2, startLine2, startCol2, 79, (string) null, "\\", 0);
                    flag1 = true;
                    ch = '/';
                    if (startCol1 != -1 && startLine1 != -1)
                    {
                      startLine2 = startLine1;
                      startCol2 = startCol1;
                    }
                    stringBuilder1.Length = 0;
                    if (stringBuilder2 != null)
                      stringBuilder2.Length = 0;
                    if (stringBuilder3 != null)
                      stringBuilder3.Length = 0;
                    if (stringBuilder4 != null)
                      stringBuilder4.Length = 0;
                    startLine1 = -1;
                    startCol1 = -1;
                    continue;
                  }
                  if (stringList != null && stringList.Count > 0 && this.SearchAndReplaceMaterialKeyword(editor, ref startLine2, ref startCol2, ref avsMaterialPos))
                  {
                    flag1 = true;
                    continue;
                  }
                  break;
              }
            }
          }
          if (index1 != -1 && index2 < strArray1[index1].Length)
          {
            ++index2;
            if (index2 < strArray1[index1].Length)
            {
              if ((int) char.ToUpper(ch) != (int) strArray1[index1][index2])
              {
                index1 = -1;
                num9 = -1;
              }
            }
            else
            {
              num6 = startLine2;
              num5 = startCol2;
              num7 = 0;
            }
          }
          if (index1 == -1)
          {
            flag2 = true;
            if ((startCol2 == 0 && startLine2 == 0 || c == char.MinValue || !char.IsLetterOrDigit(c)) && char.IsLetter(ch))
            {
              for (int index9 = 0; index9 < strArray1.Length; ++index9)
              {
                if (strArray1[index9] != null && strArray1[index9] != "" && (int) char.ToUpper(ch) == (int) strArray1[index9][0])
                {
                  index1 = index9;
                  index2 = 0;
                  break;
                }
              }
            }
          }
          if (index1 != -1 && index2 >= strArray1[index1].Length && num9 == -1 && !RtfInSiteEditorWrapper.IsWhiteSpace(ch))
          {
            if (index1 == num1 && num7 == 0 && char.ToUpper(ch) == 'Р')
              ++num7;
            else if (index1 == num3 && num7 < 4)
            {
              if (num7 == 0 && char.ToUpper(ch) == 'С')
                ++num7;
              else if (num7 == 1 && char.ToUpper(ch) == 'Э')
                ++num7;
              else if (num7 == 2 && char.ToUpper(ch) == 'В')
                ++num7;
              else if (num7 == 3)
              {
                if (RtfInSiteEditorWrapper.IsWhiteSpace(ch))
                  ++num7;
                else if (char.IsDigit(ch))
                {
                  ++num7;
                  num9 = startLine2;
                  num8 = startCol2;
                }
                else
                {
                  index1 = -1;
                  num7 = -1;
                }
              }
              else
              {
                index1 = -1;
                num7 = -1;
              }
            }
            else if (char.IsDigit(ch))
            {
              num9 = startLine2;
              num8 = startCol2;
            }
            else
            {
              index1 = -1;
              num7 = -1;
            }
          }
          if (index1 != -1 && num9 != -1 && num8 != -1)
          {
            if (num6 != -1 && num5 != -1)
            {
              for (int index10 = num6; index10 <= num9; ++index10)
              {
                for (int index11 = num5; index10 == num9 && index11 < num8 || index10 != num9 && index11 < editor.text[index10].len; ++index11)
                {
                  if (editor.text[index10].txt[index11] == ' ')
                  {
                    editor.text[index10].txt[index11] = '\u000E';
                    editor.SetTextTags(index10, index11, index10, index11, 79, (string) null, " ", 0);
                    flag1 = true;
                  }
                }
                num5 = 0;
              }
              num6 = -1;
              num5 = -1;
            }
            if (char.IsDigit(ch) || ch == '.' || ch == ':' || index1 == num2 && !RtfInSiteEditorWrapper.IsWhiteSpace(ch) && ch != '-')
            {
              c = ch;
            }
            else
            {
              if (ch == '-')
              {
                editor.text[startLine2].txt[startCol2] = '\u0017';
                editor.SetTextTags(startLine2, startCol2, startLine2, startCol2, 79, (string) null, "-", 0);
                flag1 = true;
              }
              if (flag2 && index1 == num2 && RtfInSiteEditorWrapper.IsWhiteSpace(ch))
              {
                c = ch;
                flag2 = false;
                if (editor.text[startLine2].txt[startCol2] == ' ')
                {
                  editor.text[startLine2].txt[startCol2] = '\u000E';
                  editor.SetTextTags(startLine2, startCol2, startLine2, startCol2, 79, (string) null, " ", 0);
                  flag1 = true;
                }
              }
              else
              {
                index1 = -1;
                num6 = -1;
                num5 = -1;
                num8 = -1;
                num9 = -1;
              }
            }
          }
        }
      }
    }
    editor.RestoreSelection(selectionBlock, false);
    if (flag3)
      editor.TerSetFlags3(true, 262144 /*0x040000*/);
    else
      editor.TerSetFlags3(false, 262144 /*0x040000*/);
    this.HasFormulas |= editor.HasImages();
    editor.HasTextReplaces |= flag1;
    return flag1;
  }

  private bool ReplaceTextByFormulaImage(
    ImRtfEditor editor,
    ImDocument ownerDoc,
    FormList formula,
    string originalFormulaText,
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    ref List<int> avsMaterialPos,
    bool forceIfProtected = false,
    bool updateEditor = true)
  {
    int num = editor.CheckTextTag(startLine, startCol, 77) ? 1 : 0;
    editor.SelectTerText(startLine, startCol, endLine, endCol + 1, false);
    editor.TerDeleteBlock(false, forceIfProtected);
    this.InsertTextByFormulaImage(editor, ownerDoc, formula, originalFormulaText, startLine, startCol, ref avsMaterialPos, forceIfProtected, false);
    if (num != 0)
      editor.SetTextTags(startLine, startCol, startLine, startCol, 77, (string) null, (string) null, 0);
    if (updateEditor)
      editor.page.Repaginate(false, false, 0, true);
    return true;
  }

  public bool InsertTextByFormulaImage(
    ImRtfEditor editor,
    ImDocument ownerDoc,
    FormList formula,
    string originalFormulaText,
    int startLine,
    int startCol,
    ref List<int> avsMaterialPos,
    bool forceIfProtected = false,
    bool updateEditor = true)
  {
    if (editor.IsBackGroundEditor)
    {
      if (ownerDoc.TernSpecSymbolsBufferB == null)
        ownerDoc.TernSpecSymbolsBufferB = RtfInSiteEditorWrapper.CreateTernPrintBuffer();
      formula.TernPrintBuffer = ownerDoc.TernSpecSymbolsBufferB;
      if (ownerDoc.TernDistributeSpecSymbolsBufferB == null)
        ownerDoc.TernDistributeSpecSymbolsBufferB = RtfInSiteEditorWrapper.CreateTernDistributeBuffer();
    }
    else
    {
      if (ownerDoc.TernSpecSymbolsBuffer == null)
        ownerDoc.TernSpecSymbolsBuffer = RtfInSiteEditorWrapper.CreateTernPrintBuffer();
      formula.TernPrintBuffer = ownerDoc.TernSpecSymbolsBuffer;
      if (ownerDoc.TernDistributeSpecSymbolsBuffer == null)
        ownerDoc.TernDistributeSpecSymbolsBuffer = RtfInSiteEditorWrapper.CreateTernDistributeBuffer();
    }
    Metafile metafile = formula.GetMetafile(isDoubleStriked: editor.blk.IsDoubleStrikedOut);
    SizeF formulaSize = this.GetFormulaSize((Image) metafile, formula.totalSize);
    if (this.owner.IsFixedSizeRows && (double) formulaSize.Height > (double) this.owner.DefaultRowSize * 1.25)
      RtfInSiteEditorWrapper.RememberMaterialPos(editor, startLine, startCol, ref avsMaterialPos);
    formula.TernPrintBuffer = (ImRtfEditor) null;
    if (formula.List.Any<Formula>((Func<Formula, bool>) (f => f.IsIndexFormula)))
      this.AdjustIndexFormulaRenderingSize(editor, ref formulaSize);
    editor.blk.TerPastePicture("", (Image) metafile, 0, this.ConvertVertAlignmentToPictAlign(new PictAlignmentInText?(formula.AlignInText)), true, UnitsConverter.MmToTwips(formulaSize), UnitsConverter.MmToTwips(formula.Offset), false, forceIfProtected);
    editor.SetTextTags(startLine, startCol, startLine, startCol, 79, (string) null, originalFormulaText, 0);
    string formulaFieldsFormat = formula.GetFormulaFieldsFormat();
    editor.SetTextTags(startLine, startCol, startLine, startCol, 81, (string) null, formulaFieldsFormat, 0);
    if (updateEditor)
      editor.page.Repaginate(false, false, 0, true);
    return true;
  }

  public FormList DecodeFormulaFromEditor()
  {
    if (this.OwnerDocument == null || !this.EditorActive)
      return (FormList) null;
    string stringFromTag1 = this.Editor.ExtractStringFromTag((IList<int>) tc.ReplacedCharTags);
    if (string.IsNullOrEmpty(stringFromTag1) || stringFromTag1.IndexOf("<<") == -1)
      return (FormList) null;
    FormList formulas = this.OwnerDocument is ImDocument ownerDocument ? ownerDocument.FindFormulas(stringFromTag1) : (FormList) null;
    if (formulas != null)
    {
      string stringFromTag2 = this.Editor.ExtractStringFromTag(81);
      if (!string.IsNullOrEmpty(stringFromTag2))
        formulas.ApplyFormulaFieldsFormat(stringFromTag2);
    }
    return formulas;
  }

  /// <summary>Установить значение ProtectedFirstCharCount в редакторе</summary>
  public virtual void SetProtectedFirstCharCount(ImRtfEditor editor, int value)
  {
    if (editor == null)
      return;
    int startLine = 0;
    int startCol = 0;
    if (editor.ProtectedFirstCharCount > 0)
      editor.DeleteTextTags(startLine, startCol, editor.ProtectedFirstCharCount, 77, (string) null, true);
    if (editor.DistributedTextStartPos != -1)
    {
      value -= editor.DistributedTextStartPos;
      if (value < 0)
        value = 0;
    }
    editor.ProtectedFirstCharCount = value;
    editor.ProtectedFirstRealCharCount = value;
    if (value <= 0)
      return;
    int realCharTagCount;
    editor.SetTextTags(startLine, startCol, editor.ProtectedFirstCharCount, 77, (string) null, (string) null, 0, true, out realCharTagCount);
    editor.ProtectedFirstRealCharCount = realCharTagCount;
  }

  /// <summary>Установить значение ProtectedFirstCharCount в редакторе</summary>
  public virtual void SetProtectedEndCharCount(int value)
  {
    this.SetProtectedEndCharCount(this.Editor, value);
  }

  /// <summary>Установить значение ProtectedFirstCharCount в редакторе</summary>
  public virtual void SetProtectedEndCharCount(ImRtfEditor editor, int value)
  {
    if (editor == null)
      return;
    int num = editor.GetTotalChars() - 2;
    int row;
    int col;
    if (editor.ProtectedEndCharCount > 0)
    {
      editor.TerAbsToRowCol(num - editor.ProtectedEndCharCount, out row, out col, false);
      editor.DeleteTextTags(row, col, editor.ProtectedEndCharCount, 77, (string) null, true);
    }
    if (num - value < 0)
      value = num;
    editor.ProtectedEndCharCount = value;
    editor.ProtectedEndRealCharCount = value;
    if (value <= 0)
      return;
    editor.TerAbsToRowCol(num - value, out row, out col, false);
    editor.SetTextTags(row, col, editor.ProtectedEndCharCount, 77, (string) null, (string) null, 0, true, out int _);
  }

  /// <summary>Рисовать для вывода на печать</summary>
  /// <param name="context">Контекст отрисовки</param>
  /// <param name="bounds">Границы элемента</param>
  internal void MergePrint(DrawContextWithUI context, RectangleF bounds)
  {
    if ((double) bounds.Width == 0.0 || (double) bounds.Height == 0.0)
      return;
    if (context.TernPrintBuffer == null)
    {
      context.TernPrintBuffer = RtfInSiteEditorWrapper.CreateTernPrintBuffer();
      if (context.Document != null)
        context.Document.TernPrintBuffer = context.TernPrintBuffer;
    }
    else
      context.TernPrintBuffer.Reset();
    Matrix matrix = context.Graphics.Transform.Clone();
    RectangleF rectangleF1 = this.CalcTextBounds(bounds, context.Margins);
    PointF[] pts = new PointF[2]
    {
      rectangleF1.Location,
      new PointF(rectangleF1.Right, rectangleF1.Bottom)
    };
    context.Graphics.Transform.TransformPoints(pts);
    rectangleF1 = RectangleF.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
    Rectangle rectangle = UnitsConverter.MmToTwips(rectangleF1);
    RectangleF rectangleF2 = this.clientBounds;
    bool isRTF = false;
    string text = (string) null;
    int firstTextPos = -1;
    if (this.owner is TextBoxElement owner)
    {
      text = owner.Rtf;
      firstTextPos = owner.StartCharIndex;
    }
    if (text != null && text != "")
      isRTF = true;
    else
      text = this.owner.Text;
    TextOrientation orientation = this.owner.Orientation;
    if (orientation != TextOrientation.Normal)
    {
      rectangleF1.Location = PointF.Empty;
      Matrix vMatrix;
      rectangleF1 = TextData.RotateTextBounds2(rectangleF1, orientation, out vMatrix);
      rectangleF2 = TextData.RotateTextBounds(rectangleF2, orientation);
      rectangle = TextData.RotateTextBounds(rectangle, orientation);
      if (orientation == TextOrientation.DownTop)
      {
        rectangle.X -= UnitsConverter.MmToTwips(context.Margins.Top + context.Margins.Bottom);
        rectangle.Y = rectangle.Y + rectangle.Width - UnitsConverter.MmToTwips(context.Margins.Left + context.Margins.Right);
      }
      else if (orientation == TextOrientation.TopDown)
      {
        rectangle.X = rectangle.X + rectangle.Height - UnitsConverter.MmToTwips(context.Margins.Top + context.Margins.Bottom);
        rectangle.Y += UnitsConverter.MmToTwips(context.Margins.Left + context.Margins.Right);
      }
      else if (orientation == TextOrientation.UpsideDown)
      {
        rectangle.X = rectangle.X + rectangle.Width - UnitsConverter.MmToTwips(RtfInSiteEditorWrapper.EditorTopMargin);
        rectangle.Y = rectangle.Y + rectangle.Height - UnitsConverter.MmToTwips(RtfInSiteEditorWrapper.EditorTopMargin);
      }
      context.Graphics.MultiplyTransform(vMatrix, System.Drawing.Drawing2D.MatrixOrder.Prepend);
    }
    Rectangle editorBounds = this.CalcPixelTextBounds(rectangleF2, context.Margins, new PointF(context.Graphics.DpiX, context.Graphics.DpiY));
    this.SetupEditor(context.TernPrintBuffer, text, isRTF, firstTextPos, this.owner.ParagraphFormat, TextOrientation.Normal, this.owner.CharFormat, this.owner.GetBackColor(), rectangleF2, editorBounds, this.owner.Margins, 1f, context.IsFixedSizeRow_NN ? context.RowSize_NN : 0.0f, false, out context.MaterialList, context);
    context.TernPrintBuffer.TransformMatrix = context.Graphics.Transform;
    context.TernPrintBuffer.ImPrintPreview(context.Graphics.InternalGraphics, rectangle);
    context.TernPrintBuffer.TransformMatrix = (Matrix) null;
    float[] elements = matrix.Elements;
    COp.XFORM xform = new COp.XFORM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
    IntPtr hdc = context.Graphics.InternalGraphics.GetHdc();
    COp.Win32.SetGraphicsMode(hdc, 2);
    COp.Win32.SetWorldTransform(hdc, ref xform);
    context.Graphics.InternalGraphics.ReleaseHdc(hdc);
    context.Graphics.Transform = matrix;
    context.TernPrintBuffer.TransformMatrix = (Matrix) null;
  }

  /// <summary>Рисовать для вывода на печать используя готовый буфер</summary>
  /// <param name="context">Контекст отрисовки</param>
  /// <param name="bounds">Границы элемента</param>
  internal void MergePrint(ImRtfEditor editor, DrawContextWithUI context, RectangleF bounds)
  {
    if (editor == null)
      return;
    RectangleF rectangleF = this.CalcTextBounds(bounds, context.Margins);
    PointF[] pts = new PointF[2]
    {
      rectangleF.Location,
      new PointF(rectangleF.Right, rectangleF.Bottom)
    };
    context.Graphics.Transform.TransformPoints(pts);
    Rectangle twips = UnitsConverter.MmToTwips(RectangleF.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y));
    editor.ImPrintPreview(context.Graphics.InternalGraphics, twips);
  }

  /// <summary>Проверить буфер отрисовки текста и создать новый, если необходимо</summary>
  /// <param name="context">Контекст отрисовки</param>
  internal void CheckPaintBuffer(DrawContextWithUI context)
  {
    if (context.TernPaintBuffer != null)
      return;
    if (context.Document != null)
      context.TernPaintBuffer = context.Document.TernPaintBuffer;
    if (context.TernPaintBuffer != null)
      return;
    context.TernPaintBuffer = RtfInSiteEditorWrapper.CreateTernPaintBuffer();
    if (context.Document == null)
      return;
    context.Document.TernPaintBuffer = context.TernPaintBuffer;
  }

  /// <summary>Вывести текст на Graphics</summary>
  /// <param name="context">Контекст прорисовки</param>
  /// <param name="bounds">Границы текста</param>
  /// <param name="ternClipBounds">Границы редактора текста</param>
  /// <param name="pixelSize">Размеры в пикселях</param>
  /// <param name="location">Положение текста в пикселях</param>
  /// <param name="useBuffer">Используется буфер изображения</param>
  internal void PrintOnGraphics(
    DrawContextWithUI context,
    RectangleF bounds,
    Rectangle ternBounds,
    Size pixelSize,
    Point location,
    bool useBuffer)
  {
    this.CheckPaintBuffer(context);
    context.TernPaintBuffer.TerEnableRefresh(false);
    RectangleF clientBounds = this.clientBounds;
    PageControl pageControl = context.PageControl;
    float num = 1f;
    if (pageControl != null)
      num = pageControl.PageScale;
    PointF dpi = context.PageControl != null ? context.PageControl.DisplayDpi : new PointF(context.Graphics.DpiX, context.Graphics.DpiY);
    Rectangle editorBounds = this.CalcPixelTextBounds(this.clientBounds, context.Margins, dpi);
    this.SetupEditor(context.TernPaintBuffer, clientBounds, editorBounds, 1f, context.IsFixedSizeRow_NN ? context.RowSize_NN : 0.0f, false, out context.MaterialList, context);
    context.TernPaintBuffer.TerEnableRefresh(true);
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    context.Graphics.PageUnit = GraphicsUnit.Pixel;
    context.TernPaintBuffer.WrapFlag = 0;
    context.HasImages = context.TernPaintBuffer.HasImages();
    if (context.IsPaint && !useBuffer && !context.IsPdf)
    {
      context.TernPaintBuffer.TransformMatrix = new Matrix();
      context.TernPaintBuffer.TransformMatrix.Scale(num, num, System.Drawing.Drawing2D.MatrixOrder.Prepend);
      context.TernPaintBuffer.TransformMatrix.Translate((float) location.X, (float) location.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
    }
    if (this.owner.Orientation == TextOrientation.UpsideDown && !context.IsPdf)
    {
      Matrix vMatrix;
      TextData.RotateTextBounds(new Rectangle(new Point(0, 0), pixelSize), this.owner.Orientation, out vMatrix);
      context.Graphics.MultiplyTransform(vMatrix, System.Drawing.Drawing2D.MatrixOrder.Prepend);
      context.TernPaintBuffer.TransformMatrix = context.Graphics.Transform;
    }
    context.TernPaintBuffer.TextOrientation = (int) this.owner.Orientation;
    context.TernPaintBuffer.PaintOnGraphics(context.Graphics.InternalGraphics, ternBounds);
    if (!useBuffer)
    {
      float[] elements = new Matrix().Elements;
      COp.XFORM xform = new COp.XFORM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
      IntPtr hdc = context.Graphics.InternalGraphics.GetHdc();
      COp.Win32.SetGraphicsMode(hdc, 2);
      COp.Win32.SetWorldTransform(hdc, ref xform);
      context.Graphics.InternalGraphics.ReleaseHdc(hdc);
      context.Graphics.InternalGraphics.ResetTransform();
      context.TernPaintBuffer.TransformMatrix = (Matrix) null;
    }
    context.Graphics.PageUnit = pageUnit;
  }

  /// <summary>Вывести текст на Graphics</summary>
  /// <param name="editor">Редатор текста</param>
  /// <param name="context">Контекст прорисовки</param>
  /// <param name="ternBounds">Границы редактора текста</param>
  /// <param name="pixelSize">Размеры в пикселях</param>
  internal void PrintOnGraphics(
    ImRtfEditor editor,
    DrawContextWithUI context,
    Rectangle ternBounds,
    Size pixelSize)
  {
    if (editor == null)
      return;
    SelectionBlock selectionBlock = editor.GetSelectionBlock();
    GraphicsUnit pageUnit1 = context.Graphics.PageUnit;
    try
    {
      this.CheckPaintBuffer(context);
      context.TernPaintBuffer.TerEnableRefresh(false);
      RectangleF clientBounds = this.clientBounds;
      TextBoxElement owner = this.owner as TextBoxElement;
      int firstTextPos = -1;
      if (owner != null)
        firstTextPos = owner.StartCharIndex;
      Color backColor = this.owner.GetBackColor();
      ParagraphFormat paragraphFormat = this.owner.ParagraphFormat;
      TextOrientation orientation = this.owner.Orientation;
      CharFormat charFormat = this.owner.CharFormat;
      this.SetupEditor(context.TernPaintBuffer, editor.RtfText, true, firstTextPos, paragraphFormat, orientation, charFormat, backColor, clientBounds, ternBounds, context.Margins, 1f, context.IsFixedSizeRow_NN ? context.RowSize_NN : 0.0f, false, out context.MaterialList, context);
      context.TernPaintBuffer.TerEnableRefresh(true);
      GraphicsUnit pageUnit2 = context.Graphics.PageUnit;
      context.Graphics.PageUnit = GraphicsUnit.Pixel;
      context.TernPaintBuffer.WrapFlag = 0;
      if (this.owner.Orientation == TextOrientation.UpsideDown)
      {
        Matrix vMatrix;
        TextData.RotateTextBounds(new Rectangle(new Point(0, 0), pixelSize), this.owner.Orientation, out vMatrix);
        context.Graphics.MultiplyTransform(vMatrix, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        context.TernPaintBuffer.TransformMatrix = context.Graphics.Transform;
      }
      context.TernPaintBuffer.PaintOnGraphics(context.Graphics.InternalGraphics, ternBounds);
      context.TernPaintBuffer.TransformMatrix = (Matrix) null;
      context.Graphics.PageUnit = pageUnit2;
    }
    finally
    {
      editor.RestoreSelection(selectionBlock, false);
      context.Graphics.PageUnit = pageUnit1;
    }
  }

  /// <summary>Редактор пуст</summary>
  protected bool IsEmpty
  {
    [DebuggerStepThrough] get => this.Owner == null || this.Owner.IsEmptyText;
  }

  /// <summary>Обработчик события PostPaint редактора</summary>
  /// <param name="Sender">Вызвавший объект</param>
  /// <param name="gr">Graphics</param>
  protected void Editor_PostPaint(object Sender, Graphics gr)
  {
    if (this.Editor == null || this.owner == null || this.owner.SuspendedRefreshUIFlag)
      return;
    DrawContext context = new DrawContext(new ImGraphics(gr), true, gr.ClipBounds, 0, true, false, new MatrixWrapper(gr.Transform));
    context.IsFocused = new bool?(true);
    context.IsSelected = new bool?(true);
    Rectangle rectangle = this.winOwnerBounds;
    if (this.owner is IPageElementWithInterface owner && owner.PageUI != null)
      rectangle = owner.PageUI.Bounds;
    Rectangle bounds = this.Editor.Bounds;
    rectangle.Location = new Point(rectangle.X - bounds.X, rectangle.Y - bounds.Y);
    if (this.Owner != null)
    {
      VertAlignment? vertAlignment1 = this.Owner.ParagraphFormat.VertAlignment;
      VertAlignment vertAlignment2 = VertAlignment.Center;
      if (vertAlignment1.GetValueOrDefault() == vertAlignment2 & vertAlignment1.HasValue)
        rectangle.Y -= UnitsConverter.MmToPixels(this.AdditionalBottomForText, gr.DpiY);
    }
    context.PixelMode = true;
    context.PixelBounds = rectangle;
    context.Layer = -1;
    if (this.owner.DrawEllipse)
      this.owner.DrawEllipseBounds(context, (RectangleF) rectangle, (RowColParams) null, (RowColParams) null, true);
    else
      this.owner.DrawFrame(context, (RectangleF) rectangle, (RowColParams) null, (RowColParams) null, true);
    context.Layer = 0;
    if (this.owner.DrawEllipse)
      this.owner.DrawEllipseBounds(context, (RectangleF) rectangle, (RowColParams) null, (RowColParams) null, true);
    else
      this.owner.DrawFrame(context, (RectangleF) rectangle, (RowColParams) null, (RowColParams) null, true);
    if (!this.owner.CheckFlags((byte) 8) || rectangle.Width >= bounds.Width)
      return;
    rectangle.Size = bounds.Size;
    --rectangle.Height;
    gr.PageUnit = GraphicsUnit.Pixel;
    using (Pen pen = new Pen(Color.Red, 0.0f))
    {
      pen.DashStyle = DashStyle.Dash;
      gr.DrawRectangle(pen, rectangle);
    }
  }

  /// <summary>Вычислить размеры редактора ImRtfEditor в пикселях с учетом полей</summary>
  /// <param name="winBounds">Размеры области редактирования в пикселях</param>
  /// <returns>Размеры редактора ImRtfEditor в пикселях с учетом полей</returns>
  protected Rectangle CalcTextBounds(Rectangle winBounds, MarginsF margins)
  {
    Rectangle rectangle = winBounds;
    if (this.owner != null && this.owner.Page is Page page && margins != null)
    {
      PointF displayDpi = page.GetDisplayDpi();
      rectangle.X += UnitsConverter.MmToPixels(margins.Left, displayDpi.X);
      rectangle.Y += UnitsConverter.MmToPixels(margins.Top, displayDpi.Y);
      rectangle.Width -= UnitsConverter.MmToPixels(margins.Left + margins.Right, displayDpi.X);
      rectangle.Height -= UnitsConverter.MmToPixels(margins.Top + margins.Bottom, displayDpi.Y);
    }
    return rectangle;
  }

  /// <summary>Вычислить размеры текстовой области в пикселях с учетом полей</summary>
  /// <param name="bounds">Размеры области редактирования в пикселях</param>
  /// <param name="margins">Поля в миллиметрах</param>
  /// <param name="dpi">Разрешение</param>
  protected Rectangle CalcPixelTextBounds(RectangleF bounds, MarginsF margins, PointF dpi)
  {
    RectangleF mm = bounds;
    if (margins != null)
    {
      mm.X += margins.Left;
      mm.Y += margins.Top;
      mm.Width -= margins.Left + margins.Right;
      mm.Height -= margins.Top + margins.Bottom;
    }
    return UnitsConverter.MmToPixels(mm, dpi);
  }

  /// <summary>Вычислить размеры текстовой области в пикселях с учетом полей</summary>
  /// <param name="bounds">Размеры области редактирования в пикселях</param>
  /// <param name="margins">Поля в миллиметрах</param>
  /// <param name="dpi">Разрешение</param>
  protected Rectangle CalcPixelTextBounds(
    RectangleF bounds,
    MarginsF margins,
    PageControl pageControl)
  {
    RectangleF rectangle = bounds;
    rectangle.X += margins.Left;
    rectangle.Y += margins.Top;
    rectangle.Width -= margins.Left + margins.Right;
    rectangle.Height -= margins.Top + margins.Bottom;
    return this.owner != null && this.owner.Page is Page && (this.owner.Page as Page).PageUI != null ? (this.owner.Page as Page).PageUI.ConvertWorldToPixel(rectangle) : Rectangle.Empty;
  }

  /// <summary>Вычислить размеры области редактирования в мм с учетом полей</summary>
  /// <param name="bounds">Размеры ячейки в мм</param>
  /// <returns>Размеры редактора ImRtfEditor в мм с учетом полей</returns>
  protected RectangleF CalcTextBounds(RectangleF bounds, MarginsF margins)
  {
    RectangleF rectangleF = bounds;
    if (margins != null)
    {
      rectangleF.X += margins.Left;
      rectangleF.Y += margins.Top;
      rectangleF.Width -= margins.Left + margins.Right;
      rectangleF.Height -= margins.Top + margins.Bottom;
    }
    return rectangleF;
  }

  /// <summary>Рассчитать размер ячейки на основе размера текста</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="margins">Поля, мм</param>
  /// <param name="cellCharCount">Количество текста в данной ячейке после которого нужно переносить в следующую.
  /// Если -1, то переносить не надо.</param>
  /// <returns>Размер ячейки, мм</returns>
  internal SizeF CalcCellSizeForText(
    ImRtfEditor editor,
    MarginsF margins,
    bool isFixedSizeRow,
    out int cellCharCount)
  {
    SizeF textSize = (SizeF) Size.Empty;
    bool flag = this.owner.Orientation.IsHorizontalText();
    int num = editor.GetTextWidth(flag ? 0 : -1) + 1;
    int firstPageTextHeight = editor.page.TerGetFirstPageTextHeight();
    if (!isFixedSizeRow)
      ++firstPageTextHeight;
    textSize.Width = UnitsConverter.TwipsToMm(flag ? (float) num : (float) firstPageTextHeight);
    textSize.Height = UnitsConverter.TwipsToMm(flag ? (float) firstPageTextHeight : (float) num);
    cellCharCount = -1;
    if (editor.TotalPages > 1)
    {
      int pageFirstLine = editor.TerGetPageFirstLine(1);
      cellCharCount = editor.pos.RowColToAbs(pageFirstLine, 0, false, false);
    }
    textSize = this.CalcCellSizeForTextSize(textSize, margins);
    return textSize;
  }

  /// <summary>Вычислить размеры поля для области редакторования в мм с учетом полей</summary>
  /// <param name="ternSize">Размеры области редактирования в мм</param>
  /// <returns>Размеры редактора ImRtfEditor в мм с учетом полей</returns>
  protected SizeF CalcCellSizeForTextSize(SizeF textSize, MarginsF margins)
  {
    return new SizeF(textSize.Width + margins.Left + margins.Right, textSize.Height + margins.Top + margins.Bottom);
  }

  /// <summary>Распределить текст и получить его размеры</summary>
  /// <param name="direction">Направление изменения размера</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="nextCellCharPos">Возвращается позиция текста для следующей ячейки</param>
  /// <returns>Возвращает размер текста</returns>
  public override SizeF Distribute(
    AutoSizeDirection direction,
    SizeF maxSize,
    out int nextCellCharPos)
  {
    return this.Distribute(this.GetDistributeBuffer(), direction, maxSize, out nextCellCharPos);
  }

  /// <summary>Получить вспомогательный контрол для разбивки</summary>
  /// <returns></returns>
  internal ImRtfEditor GetDistributeBuffer()
  {
    return !this.Owner.IsFormulaLib ? ImDocument.TernDistributeBuffer : ImDocument.TernDistributeBufferInFormula;
  }

  /// <summary>Получить вспомогательный контрол для подгонки размера шрифта</summary>
  /// <returns></returns>
  internal ImRtfEditor GetFontMetricsBuffer()
  {
    ImDocument imDocument = (ImDocument) null;
    if (this.owner != null)
      imDocument = this.owner.OwnerDocument as ImDocument;
    ImRtfEditor fontMetricsBuffer = (ImRtfEditor) null;
    if (imDocument != null)
    {
      if (imDocument.TernFontMetricsBuffer == null)
        imDocument.TernFontMetricsBuffer = RtfInSiteEditorWrapper.CreateTernBuffer("doc.TernFontMetricsBuffer");
      fontMetricsBuffer = imDocument.TernFontMetricsBuffer;
    }
    return fontMetricsBuffer;
  }

  /// <summary>Получить вспомогательный контрол для разбивки</summary>
  /// <returns></returns>
  [DebuggerStepThrough]
  internal ImRtfEditor GetPaintBuffer()
  {
    ImDocument imDocument = (ImDocument) null;
    page = (Page) null;
    if (this.owner != null)
      imDocument = this.owner.OwnerDocument as ImDocument;
    ImRtfEditor paintBuffer = (ImRtfEditor) null;
    if (imDocument != null)
      paintBuffer = imDocument.TernPaintBuffer;
    else if (this.owner != null && this.owner.Page is Page page)
      paintBuffer = page.TernPaintBuffer;
    if (paintBuffer == null)
    {
      paintBuffer = RtfInSiteEditorWrapper.CreateTernPaintBuffer();
      if (imDocument != null)
        imDocument.TernPaintBuffer = paintBuffer;
      else if (page != null)
        page.TernPaintBuffer = paintBuffer;
    }
    return paintBuffer;
  }

  /// <summary>Распределить текст и получить его размеры</summary>
  /// <param name="direction">Направление изменения размера</param>
  /// <param name="text">Текст</param>
  /// <param name="isRTF">Текст в формате RTF</param>
  /// <param name="firstTextPos">Первая позиция текста для этой ячейки</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="nextCellCharPos">Возвращается позиция текста для следующей ячейки</param>
  /// <returns>Возвращает размер текста</returns>
  public virtual SizeF Distribute(
    AutoSizeDirection direction,
    string text,
    bool isRTF,
    int firstTextPos,
    SizeF maxSize,
    out int nextCellCharPos)
  {
    return this.Distribute(this.GetDistributeBuffer(), direction, text, isRTF, firstTextPos, maxSize, out nextCellCharPos);
  }

  /// <summary>Распределить текст и получить его размеры</summary>
  /// <param name="editor">Редактор в котором распределять текст</param>
  /// <param name="direction">Направление изменения размера</param>
  /// <param name="text">Текст</param>
  /// <param name="isRTF">Текст в формате RTF</param>
  /// <param name="firstTextPos">Первая позиция текста для этой ячейки</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="nextCellCharPos">Возвращается позиция текста для следующей ячейки</param>
  /// <returns>Возвращает размер текста</returns>
  internal SizeF Distribute(
    ImRtfEditor editor,
    AutoSizeDirection direction,
    string text,
    bool isRTF,
    int firstTextPos,
    SizeF maxSize,
    out int nextCellCharPos)
  {
    nextCellCharPos = -1;
    SizeF sizeF = SizeF.Empty;
    if (!string.IsNullOrEmpty(text))
    {
      RectangleF clientBounds = this.clientBounds with
      {
        Size = maxSize
      };
      this.SetupEditor(editor, text, isRTF, firstTextPos, this.owner.ParagraphFormat, this.owner.Orientation, this.owner.CharFormat, this.owner.GetBackColor(), clientBounds, new Rectangle(0, 0, 200, 200), this.owner.Margins, 1f, this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, false, out List<int> _, (DrawContextWithUI) null);
      int cellCharCount = -1;
      sizeF = this.CalcCellSizeForText(editor, this.owner.Margins, this.owner.IsFixedSizeRows, out cellCharCount);
      if (cellCharCount != -1)
      {
        if (firstTextPos == -1)
          firstTextPos = 0;
        nextCellCharPos = firstTextPos + cellCharCount;
      }
    }
    return sizeF;
  }

  /// <summary>Распределить текст и получить его размеры</summary>
  /// <param name="editor">Редактор в котором распределять текст</param>
  /// <param name="direction">Направление изменения размера</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="nextCellCharPos">Возвращается позиция текста для следующей ячейки</param>
  /// <returns>Возвращает размер текста</returns>
  internal SizeF Distribute(
    ImRtfEditor editor,
    AutoSizeDirection direction,
    SizeF maxSize,
    out int nextCellCharPos,
    int iteration = 0)
  {
    try
    {
      nextCellCharPos = -1;
      SizeF sizeF = SizeF.Empty;
      if (!this.IsEmpty || this.EditorActive)
      {
        RectangleF clientBounds = this.clientBounds with
        {
          Size = maxSize
        };
        int cellCharCount = -1;
        float defaultRowSize = this.owner.DefaultRowSize;
        if (this.EditorActive)
        {
          editor = this.Editor;
          float pageWidth = 0.0f;
          float pageHeight = 0.0f;
          switch (direction)
          {
            case AutoSizeDirection.Height:
              pageHeight = editor.TerSect[0].PprHeight;
              if (this.owner.CanSplitData())
                editor.sec.TerSetSectPageSize(-1, editor.TerSect[0].PprWidth, UnitsConverter.MmToInch(maxSize.Height + this.GetAdditionalTextHeihgt(defaultRowSize)), false);
              else
                editor.sec.TerSetSectPageSize(-1, editor.TerSect[0].PprWidth, TextBoxElement.MaxTextHeight, false);
              editor.TerRepaginate(false);
              break;
            case AutoSizeDirection.Width:
              pageWidth = editor.TerSect[0].PprWidth;
              editor.sec.TerSetSectPageSize(-1, 40f, editor.TerSect[0].PprHeight, false);
              editor.TerRepaginate(false);
              break;
          }
          sizeF = this.CalcCellSizeForText(editor, this.owner.Margins, this.owner.IsFixedSizeRows, out cellCharCount);
          if (direction == AutoSizeDirection.Width)
            editor.sec.TerSetSectPageSize(-1, pageWidth, editor.TerSect[0].PprHeight, false);
          else if (direction == AutoSizeDirection.Height)
            editor.sec.TerSetSectPageSize(-1, editor.TerSect[0].PprWidth, pageHeight, false);
        }
        else
        {
          switch (direction)
          {
            case AutoSizeDirection.Height:
              clientBounds.Height = !this.owner.CanSplitData() ? TextBoxElement.MaxTextHeight : maxSize.Height;
              break;
            case AutoSizeDirection.Width:
              clientBounds.Width = 1016f;
              break;
          }
          this.SetupEditor(editor, clientBounds, new Rectangle(0, 0, 200, 200), 1f, this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, false, out List<int> _, (DrawContextWithUI) null);
          sizeF = this.CalcCellSizeForText(editor, this.owner.Margins, this.owner.IsFixedSizeRows, out cellCharCount);
        }
        if ((double) sizeF.Height > (double) maxSize.Height && (double) sizeF.Height < (double) maxSize.Height + (double) this.GetAdditionalTextHeihgt(defaultRowSize))
          sizeF.Height = maxSize.Height;
        if (cellCharCount != -1)
        {
          int num = -1;
          if (this.owner is TextBoxElement owner)
            num = owner.StartCharIndex;
          if (num == -1)
            num = 0;
          nextCellCharPos = num + cellCharCount;
        }
      }
      return sizeF;
    }
    catch (Exception ex)
    {
      LogManager.AddLine(ex);
      if (iteration < 3)
      {
        if (!this.Owner.IsFormulaLib)
          ImDocument.ReleaseMainDistributeBuffer();
        else
          ImDocument.ReleaseFormulaDistributeBuffer();
        return this.Distribute(this.GetDistributeBuffer(), direction, maxSize, out nextCellCharPos, ++iteration);
      }
      throw;
    }
  }

  /// <summary>Установить шрифт по умолчанию</summary>
  public override void SetDefaultCharFormat()
  {
    ImRtfEditor editor = this.Editor;
    if (editor == null || this.owner == null)
      return;
    Color backColor = this.owner.GetBackColor();
    CharFormat charFormat = this.owner.CharFormat;
    TextOrientation orientation = this.owner.Orientation;
    this.SetDefaultCharFormat(editor, charFormat, orientation, backColor, true, true, (DrawContextWithUI) null);
    editor.TerRepaint(true);
  }

  /// <summary>Установить шрифт по умолчанию</summary>
  /// <param name="font">Шрифт</param>
  /// <param name="textColor">Цвет текста</param>
  public override void SetDefaultEditorFont(Font font, Color textColor)
  {
    if (this.Editor != null)
    {
      this.Editor.SetTerDefaultFont(font.Name, Convert.ToInt32(font.SizeInPoints), 0, textColor, false);
      this.Editor.TerRewrap();
    }
    this.PaintBuffer = (Image) null;
    this.MaterialList = (List<int>) null;
  }

  /// <summary>Установить цвет фона по умолчанию</summary>
  /// <param name="backColor">Цвет фона</param>
  public override void SetDefaultEditorBackColor(Color backColor)
  {
    if (this.Editor != null)
    {
      this.Editor.EditStyle(true, "Normal", false, 2, false);
      this.Editor.TerSetParaBkColor(false, backColor, false);
      this.Editor.EditStyle(false, "Normal", false, 2, false);
    }
    this.PaintBuffer = (Image) null;
    this.MaterialList = (List<int>) null;
  }

  /// <summary>Установить режим ReadOnly для активного редактора</summary>
  public override void SetReadOnly(bool value)
  {
    if (this.Editor == null)
      return;
    this.Editor.TerSetReadOnly(value);
    this.Editor.TerRepaint(false);
  }

  /// <summary>Установить выравнивание текста по умолчанию</summary>
  public override void SetDefaultTextAlignment()
  {
    if (this.Editor != null)
    {
      ParagraphFormat paragraphFormat = this.Owner.ParagraphFormat;
      int FmtType = 1024 /*0x0400*/;
      if (paragraphFormat != null)
        FmtType = this.ConvertHorzAlignmentToTernConst(paragraphFormat.HorzAlignment);
      SelectionBlock selectionBlock = this.Editor.GetSelectionBlock();
      this.Editor.SelectAll(false);
      this.Editor.SetTerParaFmt(FmtType, true, false);
      this.Editor.RestoreSelection(selectionBlock, false);
      int ternConst = paragraphFormat == null || !paragraphFormat.VertAlignment.HasValue ? 0 : this.ConvertVertAlignmentToTernConst(paragraphFormat.VertAlignment);
      if (this.owner.Orientation.IsHorizontalText())
        this.Editor.TerSetSectAlign(-1, ternConst, false);
      else
        this.Editor.TerSetSectAlign(-1, 0, false);
      this.Editor.VertAlignment = ternConst;
    }
    this.PaintBuffer = (Image) null;
    this.MaterialList = (List<int>) null;
  }

  /// <summary>Установить формат параграфа по умолчанию</summary>
  public override void SetDefaultParagraphFormat()
  {
    ImRtfEditor editor = this.Editor;
    if (editor == null || this.owner == null)
      return;
    ParagraphFormat paragraphFormat = this.owner.ParagraphFormat;
    TextOrientation orientation = this.owner.Orientation;
    this.SetDefaultParagraphFormat(editor, paragraphFormat, orientation, this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f);
    editor.page.Repaginate(false, false, 0, true);
  }

  /// <summary>Получить координаты текстового курсора (каретки).
  /// Координаты относительно редактора.</summary>
  /// <returns></returns>
  public override Point GetTextCursorCoor()
  {
    if (this.EditorActive)
    {
      ImRtfEditor editor = this.Editor;
      if (editor != null)
      {
        int CursCol = 0;
        int CursLine;
        editor.GetTerCursorPos(out CursLine, ref CursCol);
        int pX;
        int pY;
        editor.TerTextPosToPix(1, CursLine, CursCol, out pX, out pY);
        return new Point(pX, pY);
      }
    }
    return Point.Empty;
  }

  /// <summary>Получить высоту строки в пикселях экрана</summary>
  public override int GetCurLineHeight()
  {
    int curLineHeight = 0;
    if (this.EditorActive)
    {
      ImRtfEditor editor = this.Editor;
      if (editor != null)
        curLineHeight = editor.TerScrLineHeight(editor.CurLine);
    }
    return curLineHeight;
  }

  /// <summary>Получить размеры текста</summary>
  /// <returns></returns>
  internal SizeF GetTextMaxSize()
  {
    SizeF size1;
    if (this.owner is TextBoxElement owner && owner.AutoSizeHeight)
    {
      SizeF size2 = owner.Size with
      {
        Height = (double) owner.MaxHeight != 0.0 ? owner.MaxHeight : (owner.Page == null ? 3.40282359E+36f : owner.Page.Size.Height)
      };
      size1 = owner.CalcProperSize(size2);
    }
    else
      size1 = owner.Size;
    return owner.CalcClientSize(size1);
  }

  /// <summary>Активировать редактор текста</summary>
  /// <param name="pageUI">Элемент пользовательского интерфейса</param>
  /// <param name="mouseEventArgs">Аргументы события мыши</param>
  public override void ActivateEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
    this.ActivateEditor(pageUI, mouseEventArgs, true);
  }

  /// <summary>Активировать редактор текста</summary>
  /// <param name="pageUI">Элемент пользовательского интерфейса</param>
  /// <param name="mouseEventArgs">Аргументы события мыши</param>
  /// <param name="show">Сделать редактор видимым и передать ему фокус</param>
  /// <param name="showException">Выдавать ошибку если активация не успешна иначе пересоздать редактор и активировать заново</param>
  internal void ActivateEditor(
    PageElementUI pageUI,
    MouseEventArgs mouseEventArgs,
    bool show,
    bool showException = false)
  {
    ImRtfEditor editor = this.Editor;
    PageControl pageControl = (PageControl) null;
    if (pageUI != null)
      pageControl = pageUI.PageControl;
    if (pageControl == null)
      return;
    DocumentControl documentControl = pageControl.DocumentControl;
    if (editor == null)
    {
      ImRtfEditor imRtfEditor = (ImRtfEditor) null;
      if (documentControl != null)
        imRtfEditor = documentControl.TernEditorBuffer;
      if (imRtfEditor == null)
      {
        imRtfEditor = RtfInSiteEditorWrapper.CreateTernEditorBuffer();
        if (documentControl != null)
          documentControl.TernEditorBuffer = imRtfEditor;
      }
      editor = imRtfEditor;
    }
    editor.TerSetFlags3(true, 262144 /*0x040000*/);
    try
    {
      editor.PaintEnabled = false;
      editor.Visible = false;
      if (editor.Tag is TextData tag && tag != this.Owner && tag.InPlaceEditorActive)
        tag.DeactivateInPlaceEditor();
      editor.Tag = (object) this.Owner;
      Rectangle editorBounds = this.CalcPixelTextBounds(this.clientBounds, this.owner.Margins, pageControl);
      ++editorBounds.Width;
      float pageScale = pageControl.PageScale;
      RectangleF clientBounds = this.clientBounds;
      editor.ProtectedFirstCharCount = 0;
      editor.ProtectedEndCharCount = 0;
      this.PaintBuffer = (Image) null;
      this.MaterialList = (List<int>) null;
      this.SetupEditor(editor, clientBounds, editorBounds, pageScale, this.owner.IsFixedSizeRows ? this.owner.DefaultRowSize : 0.0f, true, out this.MaterialList, (DrawContextWithUI) null);
      if (this.owner is TextBoxElement owner1)
      {
        if (!(owner1.FindFirstCell() is TextBoxElement textBoxElement))
          textBoxElement = owner1;
        this.SetProtectedFirstCharCount(editor, textBoxElement.NormalizedProtectedFirstCharCount);
        this.SetProtectedEndCharCount(editor, textBoxElement.ProtectedEndCharCount);
      }
      bool readOnlyNow = this.owner.ReadOnlyNow;
      if (!readOnlyNow && documentControl != null)
        readOnlyNow = documentControl.ReadOnly;
      editor.TerSetReadOnly(readOnlyNow);
      editor.OldTextHeight = editor.page.TerGetTextHeight();
      editor.OldTextLines = editor.TotalLines;
      editor.OldTextWidth = editor.GetTextWidth(0);
      editor.TerFlushUndo();
      editor.Enabled = true;
      editor.BackColor = this.Owner.GetBackColor();
      editor.PaintEnabled = true;
      if (show)
      {
        editor.Parent = (Control) pageUI.PageControl;
        editor.Visible = true;
        editor.Focus();
      }
      if (mouseEventArgs != null)
      {
        int num1 = mouseEventArgs.Y;
        Point location;
        if (num1 == -1)
        {
          int row = 0;
          TextBoxElement nextCell = this.owner.NextCell as TextBoxElement;
          TextBoxElement owner2 = this.owner as TextBoxElement;
          if (owner2 != null && nextCell != null && nextCell.StartCharIndex != -1)
          {
            int abs = owner2.StartCharIndex == -1 ? nextCell.StartCharIndex - 1 : nextCell.StartCharIndex - owner2.StartCharIndex - 1;
            editor.TerAbsToRowCol(abs, out row, out int _, false);
          }
          else
            row = editor.GetPageLastLine(0);
          int pY;
          editor.TerTextPosToPix(1, row, 0, out int _, out pY);
          location = editor.Location;
          num1 = location.Y + pY;
        }
        Point point;
        ref Point local = ref point;
        int x1 = mouseEventArgs.X;
        location = editor.Location;
        int x2 = location.X;
        int x3 = x1 - x2;
        int num2 = num1;
        location = editor.Location;
        int y1 = location.Y;
        int y2 = num2 - y1;
        local = new Point(x3, y2);
        editor.pos.TerMousePos((point.Y << 16 /*0x10*/) + point.X, false);
        editor.SetTerCursorPos(editor.MouseLine, editor.MouseCol, false);
      }
      else
        editor.SetTerCursorPos(0, 0, false);
      this.Editor = editor;
      editor.TerSetModify(false);
      if (this.owner.CheckFlags((byte) 8))
      {
        SelectionBlock selectionBlock = editor.GetSelectionBlock();
        float pprWidth = editor.TerSect[0].PprWidth;
        editor.sec.TerSetSectPageSize(-1, 40f, editor.TerSect[0].PprHeight, false);
        editor.TerRepaginate(false);
        if (editor.GetTextWidth(0) != editor.OldTextWidth)
        {
          int cellCharCount = -1;
          SizeF sizeF = this.CalcCellSizeForText(editor, this.owner.Margins, this.owner.IsFixedSizeRows, out cellCharCount) with
          {
            Height = clientBounds.Height
          };
          if ((double) sizeF.Width - (double) clientBounds.Width > 9.9999997473787516E-06)
          {
            editor.sec.TerSetSectPageSize(-1, UnitsConverter.MmToInch(sizeF.Width), editor.TerSect[0].PprHeight, false);
            if (this.owner != null && this.owner.Page is Page && (this.owner.Page as Page).PageUI != null)
              editor.Width = (this.owner.Page as Page).PageUI.ConvertWorldXToPixel(sizeF.Width) + 5;
            editor.TerRepaginate(false);
          }
          editor.OldTextHeight = editor.page.TerGetTextHeight();
          editor.OldTextLines = editor.TotalLines;
          editor.OldTextWidth = editor.GetTextWidth(0);
        }
        else
          editor.sec.TerSetSectPageSize(-1, pprWidth, editor.TerSect[0].PprHeight, false);
        editor.RestoreSelection(selectionBlock, false);
      }
      documentControl.NeedUpdateToolbar = true;
    }
    catch
    {
      if (showException)
        throw;
      this.Editor = (ImRtfEditor) null;
      if (documentControl != null)
        documentControl.TernEditorBuffer = (ImRtfEditor) null;
      this.ActivateEditor(pageUI, mouseEventArgs, show, true);
    }
    finally
    {
      editor.TerSetFlags3(false, 262144 /*0x040000*/);
      editor.TerSetModify(false);
    }
  }

  /// <summary>Деактивировать редактор</summary>
  public override void DeactivateEditor()
  {
    if (this.EditorControl != null)
      this.EditorControl.Capture = false;
    base.DeactivateEditor();
    this.Editor = (ImRtfEditor) null;
  }

  /// <summary>Настроить редактор</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="textBounds">Границы текста</param>
  /// <param name="editorBounds">Границы редактора</param>
  /// <param name="scale">Масштаб</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="org0">Обнулять положение текста</param>
  /// <param name="materialList">Список позиций формул в тексте</param>
  internal void SetupEditor(
    ImRtfEditor editor,
    RectangleF textBounds,
    Rectangle editorBounds,
    float scale,
    float fixedRowSize,
    bool org0,
    out List<int> materialList,
    DrawContextWithUI context)
  {
    if (editor == null)
      throw new ArgumentNullException(nameof (editor));
    string planeText = "";
    bool isRTF = false;
    int firstTextPos = -1;
    if (this.owner is TextBoxElement owner)
    {
      if (!this.owner.IsEmptyText)
      {
        string rtfText = (string) null;
        owner.GetActualText(out planeText, out rtfText, false);
        isRTF = rtfText != null;
        if (isRTF)
          planeText = rtfText;
        firstTextPos = owner.StartCharIndex;
      }
    }
    else
      planeText = this.owner.Text;
    this.SetupEditor(editor, planeText, isRTF, firstTextPos, this.owner.ParagraphFormat, this.owner.Orientation, this.owner.CharFormat, this.owner.GetBackColor(), textBounds, editorBounds, this.owner.Margins, scale, fixedRowSize, org0, out materialList, context);
  }

  /// <summary>Настроить редактор</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="text">Текст</param>
  /// <param name="isRTF">Текст задан в формате RTF</param>
  /// <param name="firstTextPos">Первая позиция текста для этой ячейки</param>
  /// <param name="paragraphFormat">Формат параграфа</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="charFormat">Формат символов</param>
  /// <param name="backColor">Цвет фона текста</param>
  /// <param name="textBounds">Границы текста</param>
  /// <param name="editorBounds">Границы редактора</param>
  /// <param name="margins">Поля в миллиметрах</param>
  /// <param name="scale">Масштаб</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  public void SetupEditor(
    ImRtfEditor editor,
    string text,
    bool isRTF,
    int firstTextPos,
    ParagraphFormat paragraphFormat,
    TextOrientation orientation,
    CharFormat charFormat,
    Color backColor,
    RectangleF textBounds,
    Rectangle editorBounds,
    MarginsF margins,
    float scale,
    float fixedRowSize)
  {
    this.SetupEditor(editor, text, isRTF, firstTextPos, paragraphFormat, orientation, charFormat, backColor, textBounds, editorBounds, margins, scale, fixedRowSize, false, out List<int> _, (DrawContextWithUI) null);
  }

  /// <summary>Настроить редактор</summary>
  /// <param name="editor">Редактор</param>
  /// <param name="text">Текст</param>
  /// <param name="isRTF">Текст задан в формате RTF</param>
  /// <param name="firstTextPos">Первая позиция текста для этой ячейки</param>
  /// <param name="paragraphFormat">Формат параграфа</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="charFormat">Формат символов</param>
  /// <param name="backColor">Цвет фона текста</param>
  /// <param name="textBounds">Границы текста</param>
  /// <param name="editorBounds">Границы редактора</param>
  /// <param name="margins">Поля в миллиметрах</param>
  /// <param name="scale">Масштаб</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="org0">Обнулять положение текста</param>
  /// <param name="materialList">Список позиций формул в тексте</param>
  internal void SetupEditor(
    ImRtfEditor editor,
    string text,
    bool isRTF,
    int firstTextPos,
    ParagraphFormat paragraphFormat,
    TextOrientation orientation,
    CharFormat charFormat,
    Color backColor,
    RectangleF textBounds,
    Rectangle editorBounds,
    MarginsF margins,
    float scale,
    float fixedRowSize,
    bool org0,
    out List<int> materialList,
    DrawContextWithUI context)
  {
    bool flag = editor != null ? editor.PaintEnabled : throw new ArgumentNullException(nameof (editor));
    editor.PaintEnabled = false;
    editor.HasTextReplaces = false;
    editor.blk.TerDeleteAll(false);
    RectangleF rectangleF = this.CalcTextBounds(textBounds, margins);
    int int32 = Convert.ToInt32(scale * 100f);
    this.SetEditorBounds(editor, editorBounds, rectangleF.Size, fixedRowSize, orientation, int32, false, false);
    this.SetEditorText(editor, text, isRTF, firstTextPos, paragraphFormat, orientation, charFormat, backColor, fixedRowSize, out materialList, context);
    if (context != null && context.IsDoubleStriked.HasValue)
      editor.blk.IsDoubleStrikedOut = context.IsDoubleStriked.Value;
    editor.PaintEnabled = flag;
  }

  /// <summary>Выделить текст</summary>
  /// <param name="pageUI">Элемент управления в контексте которого активизировать</param>
  /// <param name="selection">Координаты выделения</param>
  public override void SetTextSelection(PageElementUI pageUI, TextSelection selection)
  {
    base.SetTextSelection(pageUI, selection);
    if (this.Editor == null)
      return;
    this.Editor.SelectTerText(selection.Position, -1, selection.EndPosition, -1, true);
  }

  /// <summary>Получить координаты выделения</summary>
  /// <returns>Координаты выделения</returns>
  public override TextSelection GetTextSelection()
  {
    TextSelection textSelection = new TextSelection(0, 0);
    if (this.Editor != null)
    {
      int row;
      int BegCol;
      int EndLine;
      int EndCol;
      if (this.Editor.TerGetSelection(out row, out BegCol, out EndLine, out EndCol))
      {
        textSelection.Position = this.Editor.TerRowColToAbs(row, BegCol);
        textSelection.EndPosition = this.Editor.TerRowColToAbs(EndLine, EndCol);
      }
      else
      {
        BegCol = -1;
        this.Editor.GetTerCursorPos(out row, ref BegCol);
        textSelection.Position = row;
      }
    }
    return textSelection;
  }

  /// <summary>Текст в редакторе</summary>
  public override string EditorText
  {
    [DebuggerStepThrough] get => this.Editor != null ? this.Editor.PlaneText : (string) null;
    set
    {
      if (!(this.EditorText != value))
        return;
      this.PaintBuffer = (Image) null;
      this.MaterialList = (List<int>) null;
      if (this.Editor == null)
        return;
      int firstTxtPos = -1;
      if (this.owner is TextBoxElement owner)
        firstTxtPos = owner.StartCharIndex;
      this.SetEditorText(this.Editor, value, false, firstTxtPos);
      List<int> avsMaterialPos;
      if (this.Owner != null)
        this.ReplaceSpecSymbolAndFormulas(this.Editor, this.owner.ReplaceOldAVSSpecChars, true, this.owner.ReplaceAVSMaterial, this.owner.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false), out avsMaterialPos);
      else
        this.ReplaceSpecSymbolAndFormulas(this.Editor, false, true, false, (string) null, out avsMaterialPos);
      this.Editor.page.Repaginate(false, false, 0, false);
    }
  }

  /// <summary>В редакторе текст без форматирования</summary>
  public override bool EditorTextIsPlain
  {
    get
    {
      ImRtfEditor editor = this.Editor;
      return editor == null || editor.CheckPlaneText();
    }
  }

  /// <summary>Текст в редакторе с форматированием</summary>
  public override string EditorRtf
  {
    get => this.Editor != null ? this.Editor.GetShortRtf() : (string) null;
    set
    {
      if (!(this.EditorRtf != value))
        return;
      this.PaintBuffer = (Image) null;
      this.MaterialList = (List<int>) null;
      if (this.Editor == null)
        return;
      int firstTxtPos = -1;
      if (this.owner is TextBoxElement owner)
        firstTxtPos = owner.StartCharIndex;
      this.SetEditorText(this.Editor, value, true, firstTxtPos);
    }
  }

  /// <summary>Минимальный размер редактора</summary>
  public override SizeF MinSize
  {
    [DebuggerStepThrough] get => new SizeF(3f, 3f);
  }

  /// <summary>Установить границы</summary>
  /// <param name="ownerBounds">Границы владельца в мм</param>
  /// <param name="clientBounds">Границы редактора в мм</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="margins">Поля в миллиметрах</param>
  /// <param name="winOwnerBounds">Границы владельца в пикселах</param>
  /// <param name="winClientBounds">Границы редактора в пикселах</param>
  /// <param name="orientation">Ориентация текста</param>
  /// <param name="scale">Масштаб</param>
  /// <param name="dpi">dpi экрана</param>
  /// <param name="repage">Вызвать внутреннюю переразбивку в редакторе</param>
  public override void SetBounds(
    RectangleF ownerBounds,
    RectangleF clientBounds,
    MarginsF margins,
    float fixedRowSize,
    Rectangle winOwnerBounds,
    Rectangle winClientBounds,
    TextOrientation orientation,
    float scale,
    PointF dpi,
    bool repage)
  {
    SizeF minSize;
    if ((double) clientBounds.Width < (double) this.MinSize.Width)
    {
      ref RectangleF local = ref clientBounds;
      minSize = this.MinSize;
      double width = (double) minSize.Width;
      local.Width = (float) width;
    }
    double height1 = (double) clientBounds.Height;
    minSize = this.MinSize;
    double height2 = (double) minSize.Height;
    if (height1 < height2)
    {
      ref RectangleF local = ref clientBounds;
      minSize = this.MinSize;
      double height3 = (double) minSize.Height;
      local.Height = (float) height3;
    }
    if (this.ownerBounds == ownerBounds && this.clientBounds == clientBounds && this.winOwnerBounds == winOwnerBounds && this.winClientBounds == winClientBounds)
      return;
    if (this.clientBounds.Size != clientBounds.Size || this.winClientBounds.Size != winClientBounds.Size)
    {
      this.PaintBuffer = (Image) null;
      this.MaterialList = (List<int>) null;
    }
    this.ownerBounds = ownerBounds;
    this.clientBounds = clientBounds;
    this.winOwnerBounds = winOwnerBounds;
    this.winClientBounds = winClientBounds;
    if (this.Owner != null)
    {
      VertAlignment? vertAlignment1 = this.Owner.ParagraphFormat.VertAlignment;
      VertAlignment vertAlignment2 = VertAlignment.Center;
      if (vertAlignment1.GetValueOrDefault() == vertAlignment2 & vertAlignment1.HasValue)
      {
        this.clientBounds.Y -= this.AdditionalBottomForText;
        this.winClientBounds.Y -= UnitsConverter.MmToPixels(this.AdditionalBottomForText, dpi.Y);
      }
    }
    ImRtfEditor editor = this.Editor;
    if (editor == null)
      return;
    Rectangle bounds = this.CalcTextBounds(winClientBounds, margins);
    RectangleF rectangleF = this.CalcTextBounds(clientBounds, margins);
    int int32 = Convert.ToInt32(scale * 100f);
    this.SetEditorBounds(editor, bounds, rectangleF.Size, fixedRowSize, orientation, int32, false, repage);
  }

  /// <summary>Обновить изображение</summary>
  public override void Invalidate()
  {
    this.PaintBuffer = (Image) null;
    this.MaterialList = (List<int>) null;
  }

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  private static extern IntPtr GetDC(IntPtr hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

  /// <summary>Обновить ClientBounds - границы ячейки</summary>
  private void UpdateClientBounds()
  {
    if (this.Owner == null)
      return;
    this.clientBounds = this.Owner.ClientBounds;
    if (this.Owner == null)
      return;
    VertAlignment? vertAlignment1 = this.Owner.ParagraphFormat.VertAlignment;
    VertAlignment vertAlignment2 = VertAlignment.Center;
    if (!(vertAlignment1.GetValueOrDefault() == vertAlignment2 & vertAlignment1.HasValue))
      return;
    this.clientBounds.Y -= this.AdditionalBottomForText;
  }

  public bool DoesFontExist(string fontFamilyName, FontStyle fontStyle)
  {
    try
    {
      using (FontFamily fontFamily = new FontFamily(fontFamilyName))
        return fontFamily.IsStyleAvailable(fontStyle);
    }
    catch (ArgumentException ex)
    {
      return false;
    }
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    try
    {
      if ((this.winClientBounds.Width == 0 || this.winClientBounds.Height == 0) && context.IsPaint)
        return;
      this.UpdateClientBounds();
      ImRtfEditor editor = this.Editor;
      if (context.IsPaint && (this.winClientBounds.Width <= 0 || this.winClientBounds.Height <= 0))
        return;
      if (!(context is DrawContextWithUI context1))
      {
        ImDocument document = (ImDocument) null;
        if (this.owner != null)
          document = this.owner.OwnerDocument as ImDocument;
        context1 = new DrawContextWithUI(document, (PageControl) null, context);
        if (!context.IsPaint && document != null && document.TernPrintBuffer != null)
          context1.TernPrintBuffer = document.TernPrintBuffer;
      }
      if (context.IsPaint || context.IsPdf)
      {
        Rectangle rectangle = this.CalcTextBounds(this.winClientBounds, context.Margins);
        Matrix transform = context.Graphics.Transform;
        int num1 = transform.IsIdentity ? 1 : 0;
        Point location = rectangle.Location;
        Size size = this.winClientBounds.Size;
        Size pixelSize = size;
        int num2 = 0;
        if (this.PaintBuffer == null && this.owner.Orientation == TextOrientation.UpsideDown)
        {
          num2 = UnitsConverter.MmToPixels(2f * RtfInSiteEditorWrapper.EditorTopMargin, context.Graphics.DpiY);
          pixelSize = new Size(size.Width - num2, size.Height - num2);
        }
        if (num1 == 0)
        {
          Matrix matrix = transform.Clone();
          matrix.Invert();
          MatrixWrapper matrixWrapper = new MatrixWrapper(matrix);
          location = Point.Round(matrixWrapper.TransformPoint(rectangle.Location));
          Point point = Point.Round(matrixWrapper.TransformPoint(new Point(rectangle.Right, rectangle.Bottom)));
          size = new Size(point.X - location.X, point.Y - location.Y);
          pixelSize = new Size(size.Width - num2, size.Height - num2);
        }
        bool useBuffer = context.IsPdf || this.owner.Orientation != TextOrientation.Normal || context.IsSelected.Value && !context.IsFocused.Value || this.HasFormulas;
        bool flag1 = true;
        if (context.IsPdf && (this.Owner.CharFormat.CharStyle & CharStyle.Italic) != 0)
        {
          Intermech.Document.Model.TypographicFont.TypographicFont typographicFont = this.Owner.CharFormat.GetFont().GetTypographicFont();
          flag1 = typographicFont != null && typographicFont.Italic;
        }
        bool flag2 = this.HasFormulas || context.HasImages || !flag1 || this.owner.Orientation != 0;
        GraphicsUnit pageUnit1 = context.Graphics.PageUnit;
        if (!this.IsEmpty && (this.PaintBuffer == null || context.IsPdf || this.Owner.NeedUpdateFormulas))
        {
          if (useBuffer)
          {
            IntPtr hdc = context.Graphics.InternalGraphics.GetHdc();
            try
            {
              RectangleF rectangleF = new RectangleF(new PointF(0.0f, 0.0f), this.clientBounds.Size);
              RectangleF frameRect1 = new RectangleF(0.0f, 0.0f, this.clientBounds.Width * 10f, this.clientBounds.Height * 10f);
              if (!context.IsPdf)
              {
                this.PaintBuffer = (Image) new Metafile(hdc, frameRect1, MetafileFrameUnit.Millimeter, EmfType.EmfOnly);
              }
              else
              {
                this.PaintBuffer = (Image) new Metafile(hdc, EmfType.EmfOnly);
                MetafileHeader metafileHeader = (this.PaintBuffer as Metafile).GetMetafileHeader();
                PointF pointF1 = new PointF(metafileHeader.DpiX, metafileHeader.DpiY);
                if (flag2 && (double) pointF1.X > 80.0)
                {
                  PointF pointF2 = PointF.Empty;
                  using (Graphics graphics = Graphics.FromImage(this.PaintBuffer))
                    pointF2 = new PointF(graphics.DpiX, graphics.DpiY);
                  RectangleF clientBounds = this.clientBounds;
                  RectangleF frameRect2 = new RectangleF(0.0f, 0.0f, clientBounds.Width * pointF2.X / pointF1.X, clientBounds.Height * pointF2.Y / pointF1.Y);
                  this.PaintBuffer = (Image) new Metafile(hdc, frameRect2, MetafileFrameUnit.Millimeter, EmfType.EmfOnly);
                }
              }
              using (Graphics g = Graphics.FromImage(this.PaintBuffer))
              {
                g.PageUnit = GraphicsUnit.Millimeter;
                RectangleF rect = rectangleF;
                g.SetClip(rect);
                if (context.IsPdf)
                {
                  g.AddMetafileComment(Encoding.Unicode.GetBytes("#Skip#EmfPolygon16"));
                  g.DrawRectangle(new Pen(Color.Red), rect.X, rect.Y, rect.Width, rect.Height);
                }
                ImGraphics graphics = context1.Graphics;
                context1.Graphics = new ImGraphics(g);
                if (context1 != null)
                {
                  bool? isDoubleStriked = context1.IsDoubleStriked;
                  if (isDoubleStriked.HasValue)
                  {
                    CBlk blk = context1.TernPaintBuffer.blk;
                    isDoubleStriked = context1.IsDoubleStriked;
                    int num3 = isDoubleStriked.Value ? 1 : 0;
                    blk.IsDoubleStrikedOut = num3 != 0;
                  }
                }
                try
                {
                  if (context.DrawInCurrentEditor && editor != null)
                    this.PrintOnGraphics(editor, context1, new Rectangle(new Point(0, 0), size), pixelSize);
                  else
                    this.PrintOnGraphics(context1, this.clientBounds, new Rectangle(new Point(0, 0), rectangle.Size), pixelSize, rectangle.Location, useBuffer);
                }
                catch (Exception ex)
                {
                  LogManager.AddLine(ex.Message + Environment.NewLine + ex.StackTrace, true);
                  if (ImDocumentEditorConfig.Instance.ShowDebugInfo)
                    throw;
                }
                finally
                {
                  context1.Graphics = graphics;
                }
                this.MaterialList = context1.MaterialList;
                context.HasImages = context1.HasImages;
              }
              if (this.PaintBuffer != null)
              {
                LogManager.AddLine($"DPI0 ID = {this.Owner.Id}, dpi = {this.PaintBuffer.HorizontalResolution},{this.PaintBuffer.VerticalResolution}");
                this.PaintBuffer = (Image) this.PaintBuffer.Clone();
                LogManager.AddLine($"DPI1 ID = {this.Owner.Id}, dpi = {this.PaintBuffer.HorizontalResolution},{this.PaintBuffer.VerticalResolution}");
              }
            }
            finally
            {
              context.Graphics.InternalGraphics.ReleaseHdc();
            }
          }
          else
          {
            if (context1 != null)
            {
              bool? isDoubleStriked = context1.IsDoubleStriked;
              if (isDoubleStriked.HasValue)
              {
                CBlk blk = context1.TernPaintBuffer.blk;
                isDoubleStriked = context1.IsDoubleStriked;
                int num4 = isDoubleStriked.Value ? 1 : 0;
                blk.IsDoubleStrikedOut = num4 != 0;
              }
            }
            if (context.DrawInCurrentEditor && editor != null)
              this.PrintOnGraphics(editor, context1, new Rectangle(new Point(0, 0), size), pixelSize);
            else
              this.PrintOnGraphics(context1, this.clientBounds, new Rectangle(new Point(0, 0), rectangle.Size), pixelSize, rectangle.Location, useBuffer);
          }
        }
        if (this.PaintBuffer != null)
        {
          if (!context.IsPdf)
          {
            context.Graphics.PageUnit = GraphicsUnit.Pixel;
            Region clip = context.Graphics.Clip;
            Rectangle rect = new Rectangle(location, size);
            context.Graphics.SetClip(rect, CombineMode.Intersect);
            if (!context.IsSelected.Value || context.IsFocused.Value)
            {
              try
              {
                context.Graphics.DrawImage(this.PaintBuffer, (RectangleF) new Rectangle(location.X, location.Y, this.PaintBuffer.Width, this.PaintBuffer.Height));
              }
              catch
              {
                this.PaintBuffer = (Image) null;
                throw;
              }
            }
            else
            {
              GraphicsUnit pageUnit2 = GraphicsUnit.Pixel;
              this.PaintBuffer.GetBounds(ref pageUnit2);
              context.Graphics.DrawImage(this.PaintBuffer, new Rectangle(location.X, location.Y, this.PaintBuffer.Width, this.PaintBuffer.Height), 0, 0, this.PaintBuffer.Width, this.PaintBuffer.Height, GraphicsUnit.Pixel, VisualNode.NegativeImageAttributes);
            }
            context.Graphics.SetClip(clip, CombineMode.Replace);
            context.MaterialList = this.MaterialList;
          }
          else
          {
            GraphicsState gstate = context.Graphics.Save();
            Image image = this.PaintBuffer;
            RectangleF clientBounds1 = this.Owner.ClientBounds;
            if ((double) clientBounds1.Width > 0.0 && (double) clientBounds1.Height > 0.0)
            {
              RectangleF clientBounds2;
              if (flag2)
                image = (Image) (context.Graphics as PdfImGraphics).GetBitmap(this.PaintBuffer, clientBounds1.Size, this.Owner.BackColor);
              else if (this.owner.Orientation == TextOrientation.DownTop || this.owner.Orientation == TextOrientation.TopDown)
              {
                ref RectangleF local = ref clientBounds1;
                clientBounds2 = this.Owner.ClientBounds;
                double width = (double) clientBounds2.Height - 0.0;
                clientBounds2 = this.Owner.ClientBounds;
                double height = (double) clientBounds2.Width - 0.0;
                local = new RectangleF(0.0f, 0.0f, (float) width, (float) height);
              }
              if (!flag2)
              {
                PointF point = new PointF(0.0f, 0.0f);
                if (this.owner.Orientation == TextOrientation.Normal)
                  point = new PointF(this.clientBounds.Location.X + 0.5f, this.clientBounds.Location.Y);
                else if (this.owner.Orientation == TextOrientation.DownTop)
                {
                  context.Graphics.TranslateTransform(this.clientBounds.X, (float) ((double) this.clientBounds.Y + (double) this.clientBounds.Height - 0.5));
                  context.Graphics.RotateTransform(-90f);
                }
                else if (this.owner.Orientation == TextOrientation.TopDown)
                {
                  context.Graphics.TranslateTransform(this.clientBounds.X + this.clientBounds.Width, this.clientBounds.Y + 0.5f);
                  context.Graphics.RotateTransform(90f);
                }
                else if (this.owner.Orientation == TextOrientation.UpsideDown)
                {
                  context.Graphics.TranslateTransform((float) ((double) this.clientBounds.X + (double) this.clientBounds.Width - 0.5), this.clientBounds.Y + this.clientBounds.Height);
                  context.Graphics.RotateTransform(180f);
                }
                context.Graphics.DrawImage(this.PaintBuffer, point);
              }
              else
              {
                RectangleF rect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
                if (this.owner.Orientation == TextOrientation.Normal)
                {
                  ref RectangleF local = ref rect;
                  clientBounds2 = this.Owner.ClientBounds;
                  double x = (double) clientBounds2.X + 0.5;
                  clientBounds2 = this.owner.ClientBounds;
                  double y = (double) clientBounds2.Y;
                  clientBounds2 = this.owner.ClientBounds;
                  double width = (double) clientBounds2.Width;
                  clientBounds2 = this.owner.ClientBounds;
                  double height = (double) clientBounds2.Height;
                  local = new RectangleF((float) x, (float) y, (float) width, (float) height);
                }
                else if (this.owner.Orientation == TextOrientation.DownTop)
                {
                  ref RectangleF local = ref rect;
                  clientBounds2 = this.Owner.ClientBounds;
                  double x = (double) clientBounds2.X;
                  clientBounds2 = this.owner.ClientBounds;
                  double y = (double) clientBounds2.Y - 0.5;
                  clientBounds2 = this.owner.ClientBounds;
                  double width = (double) clientBounds2.Width;
                  clientBounds2 = this.owner.ClientBounds;
                  double height = (double) clientBounds2.Height;
                  local = new RectangleF((float) x, (float) y, (float) width, (float) height);
                }
                else if (this.owner.Orientation == TextOrientation.TopDown)
                {
                  ref RectangleF local = ref rect;
                  clientBounds2 = this.Owner.ClientBounds;
                  double x = (double) clientBounds2.X;
                  clientBounds2 = this.owner.ClientBounds;
                  double y = (double) clientBounds2.Y;
                  clientBounds2 = this.owner.ClientBounds;
                  double width = (double) clientBounds2.Width;
                  clientBounds2 = this.owner.ClientBounds;
                  double height = (double) clientBounds2.Height - 0.5;
                  local = new RectangleF((float) x, (float) y, (float) width, (float) height);
                }
                else if (this.owner.Orientation == TextOrientation.UpsideDown)
                {
                  ref RectangleF local = ref rect;
                  clientBounds2 = this.owner.ClientBounds;
                  double width = (double) clientBounds2.Width;
                  clientBounds2 = this.owner.ClientBounds;
                  double height = (double) clientBounds2.Height;
                  local = new RectangleF(0.0f, 0.0f, (float) width, (float) height);
                  context.Graphics.TranslateTransform((float) ((double) this.clientBounds.X + (double) this.clientBounds.Width - 0.5), this.clientBounds.Y + this.clientBounds.Height);
                  context.Graphics.RotateTransform(180f);
                }
                context.Graphics.DrawImage(image, rect);
              }
            }
            context.Graphics.Restore(gstate);
            this.PaintBuffer = (Image) null;
            context.MaterialList = this.MaterialList;
          }
        }
        context.Graphics.PageUnit = pageUnit1;
      }
      else
      {
        if (this.IsEmpty)
          return;
        if (context.DrawInCurrentEditor && editor != null)
          this.MergePrint(editor, context1, this.clientBounds);
        else
          this.MergePrint(context1, this.clientBounds);
        if (context == context1)
          return;
        context.MaterialList = context1.MaterialList;
      }
    }
    catch (Exception ex)
    {
      LogManager.AddLine(ex.Message + Environment.NewLine + ex.StackTrace, true);
      if (!ImDocumentEditorConfig.Instance.ShowDebugInfo)
        return;
      throw;
    }
  }

  /// <summary>Курсор находится в конце текста</summary>
  public override bool CursorInEndPosition
  {
    get
    {
      if (!this.EditorActive)
        return false;
      ImRtfEditor editor = this.Editor;
      if (editor == null || editor.HilightType != 0)
        return false;
      return this.owner.NextCell == null ? editor.CurLine == editor.TotalLines - 1 && editor.CurCol + 1 >= editor.text[editor.CurLine].len : editor.CurLine >= editor.GetPageLastLine(0) && editor.CurCol + 1 >= editor.text[editor.CurLine].len;
    }
  }

  /// <summary>Курсор находится в начале текста</summary>
  public override bool CursorInFirstPosition
  {
    get
    {
      if (!this.EditorActive)
        return true;
      ImRtfEditor editor = this.Editor;
      if (editor == null)
        return true;
      return editor.HilightType == 0 && editor.CurLine == 0 && editor.CurCol == 0;
    }
  }

  /// <summary>Курсор находится на первой строке</summary>
  public override bool CursorInFirstLine
  {
    get
    {
      if (!this.EditorActive)
        return true;
      ImRtfEditor editor = this.Editor;
      if (editor == null)
        return true;
      return editor.HilightType == 0 && editor.CurLine == 0;
    }
  }

  /// <summary>Курсор находится на последней строке</summary>
  public override bool CursorInLastLine
  {
    get
    {
      if (!this.EditorActive)
        return false;
      ImRtfEditor editor = this.Editor;
      if (editor == null || editor.HilightType != 0)
        return false;
      return this.owner.NextCell == null ? editor.CurLine == editor.TotalLines - 1 : editor.CurLine == editor.GetPageLastLine(0);
    }
  }

  public void Dispose()
  {
    this.editorControl = (Control) null;
    this.PaintBuffer = (Image) null;
    this.owner = (TextData) null;
  }

  private class KeywordWithPosition
  {
    public string Keyword;
    public EditorTextBlock Position;

    public KeywordWithPosition(
      RtfInSiteEditorWrapper.CheckingKeyword_Forward checkedkeyword,
      TextPosition endPosition)
    {
      this.Keyword = checkedkeyword.Keyword;
      this.Position.End = endPosition;
      this.Position.Start = checkedkeyword.Position;
    }

    public KeywordWithPosition(
      RtfInSiteEditorWrapper.CheckingKeyword_Backward checkedkeyword,
      TextPosition firstPosition)
    {
      this.Keyword = checkedkeyword.Keyword;
      this.Position.End = checkedkeyword.Position;
      this.Position.Start = firstPosition;
    }

    public KeywordWithPosition(string keyword, EditorTextBlock position)
    {
      this.Keyword = keyword;
      this.Position = position;
    }

    public TextPosition Start => this.Position.Start;

    public TextPosition End => this.Position.End;
  }

  private abstract class CheckingKeyword
  {
    public string Keyword;
    public TextPosition Position;
    protected int CurCharIndex;

    public abstract bool IsWholeChecked { get; }

    public virtual bool CheckCurrentChar(char checkingChar)
    {
      return (int) char.ToUpper(this.CurChar) == (int) char.ToUpper(checkingChar);
    }

    public char CurChar
    {
      get
      {
        return this.CurCharIndex < 0 || string.IsNullOrEmpty(this.Keyword) || this.CurCharIndex >= this.Keyword.Length ? char.MinValue : this.Keyword[this.CurCharIndex];
      }
    }

    public CheckingKeyword(string keyword, TextPosition position)
    {
      this.Keyword = keyword;
      this.Position = position;
    }
  }

  private class CheckingKeyword_Forward : RtfInSiteEditorWrapper.CheckingKeyword
  {
    public CheckingKeyword_Forward(string keyword, TextPosition startPosition)
      : base(keyword, startPosition)
    {
      this.CurCharIndex = 0;
    }

    public override bool IsWholeChecked => this.CurCharIndex >= this.Keyword.Length - 1;

    public bool CheckNextChar(char checkingChar)
    {
      ++this.CurCharIndex;
      return this.CheckCurrentChar(checkingChar);
    }
  }

  private class CheckingKeyword_Backward : RtfInSiteEditorWrapper.CheckingKeyword
  {
    public CheckingKeyword_Backward(string keyword, TextPosition endPosition)
      : base(keyword, endPosition)
    {
      if (!string.IsNullOrEmpty(this.Keyword))
        this.CurCharIndex = keyword.Length - 1;
      else
        this.CurCharIndex = -1;
    }

    public override bool IsWholeChecked => this.CurCharIndex == 0;

    public bool CheckPrevChar(char checkingChar)
    {
      --this.CurCharIndex;
      return this.CheckCurrentChar(checkingChar);
    }
  }
}
