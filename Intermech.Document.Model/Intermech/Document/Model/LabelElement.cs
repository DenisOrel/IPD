// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.LabelElement
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using BarcodeLib;
using Intermech.Document.Model.PdfGenerator;
using Intermech.Document.Model.TypographicFont;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Элемент для вывода простого текста и системных переменных</summary>
[Serializable]
public class LabelElement : TextData, IPageElementWithInterface
{
  private LabelElementType elementType;
  private float barCodeWigth = 1f;
  [NonSerialized]
  private CancelEventHandler inplaceEditorActivating;
  [NonSerialized]
  private EventHandler inplaceEditorActivated;
  [NonSerialized]
  private CancelEventHandler inplaceEditorDeactivating;
  [NonSerialized]
  private EventHandler inplaceEditorDeactivated;
  /// <summary>Имя типа элемента</summary>
  public static string ElementTypeName = LocalizationHolder.rm.GetString("Document.Model_492");
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  [NonSerialized]
  private PageElementUI pageUI;

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  private static extern IntPtr GetDC(IntPtr hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

  [CustomDisplayName("Attribute.Document.Model_285")]
  [CustomDescription("Attribute.Document.Model_286")]
  [CustomCategory("Attribute.Document.Model_287")]
  [RefreshProperties(RefreshProperties.All)]
  public LabelElementType ElementType
  {
    get => this.elementType;
    set
    {
      this.elementType = value;
      if (this.elementType != LabelElementType.Text)
        this.BackColor = Color.White;
      this.RefreshUI();
    }
  }

  [CustomDisplayName("Attribute.Document.Model_288")]
  [CustomDescription("Attribute.Document.Model_289")]
  [CustomCategory("Attribute.Document.Model_290")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public float BarCodeWigth
  {
    get => this.barCodeWigth;
    set
    {
      this.barCodeWigth = value;
      this.RefreshUI();
    }
  }

  /// <summary>Ссылка на источник текста</summary>
  [Editor(typeof (ReferenceToTextSourceUIEditor), typeof (UITypeEditor))]
  public override ReferenceBase ReferenceToTextSource
  {
    get => base.ReferenceToTextSource;
    set => base.ReferenceToTextSource = value;
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(DocumentTreeNode child) => false;

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(System.Type type) => false;

  /// <summary>Обновить ссылки на атрибуты</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  public override void UpdateNodeAttributeLinks(bool recursive, bool updateUI, bool updateLayout)
  {
    if (!(this.ReferenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource))
      return;
    string text = referenceToTextSource.Text;
    referenceToTextSource.UpdateLink(updateUI, updateLayout);
    if (!updateUI || !(text != referenceToTextSource.Text))
      return;
    this.RefreshUI();
  }

  /// <summary>Текст отформатированный согласно TextFormat</summary>
  [CustomDisplayName("Attribute.Document.Model_59")]
  [CustomDescription("Attribute.Document.Model_60")]
  [CustomCategory("Attribute.Document.Model_61")]
  public string FormattedText
  {
    [DebuggerStepThrough] get
    {
      string str = this.GetText() ?? "";
      ReferenceToDBObjectAttributeBase referenceToTextSource = this.referenceToTextSource as ReferenceToDBObjectAttributeBase;
      return !string.IsNullOrWhiteSpace(this.textFormat) && (referenceToTextSource == null || referenceToTextSource.PassiveLink) ? string.Format(this.textFormat, (object) str) : str;
    }
  }

  /// <summary>Текст</summary>
  public override string Text
  {
    [DebuggerStepThrough] get => base.Text;
    set
    {
      if (!(this.Text != value))
        return;
      base.Text = value;
      this.RefreshUI();
    }
  }

  public override void OnTextChanged(TextChanged_EventArgs e)
  {
    if (this.repeatTextAsHeader && this.prevCell == null && this.nextCell != null)
    {
      RectangleElement nextCell = this.nextCell;
      TextChanged_EventArgs e1 = new TextChanged_EventArgs(e.OldText, e.NewText, e.ClearRTF, e.UpdateActiveEditor, e.SaveModificationDate, false, false);
      for (; nextCell != null; nextCell = nextCell.NextCell)
      {
        if (nextCell is TextData textData)
        {
          textData.OnTextChanged(e1);
          if (e.UpdateUI)
            textData.RefreshUI();
        }
      }
    }
    base.OnTextChanged(e);
  }

  public override bool ReadOnly
  {
    get => base.ReadOnly;
    set
    {
      if (this.ReadOnly == value)
        return;
      base.ReadOnly = value;
      if (!(this.OwnerDocument is ImDocument ownerDocument))
        return;
      ownerDocument.UpdateFormatCommands();
    }
  }

  public override Rectangle GetPixelBounds(DrawContext context)
  {
    return this.pageUI != null ? this.pageUI.Bounds : base.GetPixelBounds(context);
  }

  public override bool ShowFocused
  {
    get => this.pageUI != null ? this.pageUI.IsActiveElement : base.ShowFocused;
  }

  public override bool ShowSelected
  {
    get => this.pageUI != null ? this.pageUI.IsSelected : base.ShowSelected;
  }

  /// <summary>Обновить экранные координаты</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    bool flag = false;
    if (this.pageUI == null && this.NeedUI)
    {
      this.CreateUI();
      flag = true;
    }
    if (this.pageUI == null)
      return;
    int num = this.SuspendedRefreshUIFlag ? 1 : 0;
    if (num == 0)
      this.SuspendRefreshUI();
    this.InvalidateUI(this.pageUI.Bounds);
    if (this.needUpdateUIGeometry && !flag)
      this.pageUI.UpdateGeometry();
    base.UpdateUIGeometry(false);
    if (num != 0)
      return;
    this.ResumeRefreshUI(refreshUI);
  }

  /// <summary>Обновить мировые координаты элемента преобразовав экранные координаты</summary>
  public override void UpdateWorldCoor()
  {
    if (this.PageUI == null)
      return;
    int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
    if (num == 0)
      this.SuspendUpdateGeometryRefreshUI();
    this.PageUI.UpdateElementGeometry();
    if (num != 0)
      return;
    this.ResumeUpdateRefreshUI(true, true);
  }

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="pageUI">Элемент управления в контексте которого должен быть редактор</param>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  public void ActivateInPlaceEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
  }

  /// <summary>Событие перед активацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorActivating
  {
    add => this.inplaceEditorActivating += value;
    remove => this.inplaceEditorActivating -= value;
  }

  /// <summary>Событие после активации редактора по месту</summary>
  public event EventHandler InplaceEditorActivated
  {
    add => this.inplaceEditorActivated += value;
    remove => this.inplaceEditorActivated -= value;
  }

  /// <summary>Событие перед деактивацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorDeactivating
  {
    add => this.inplaceEditorDeactivating += value;
    remove => this.inplaceEditorDeactivating -= value;
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event EventHandler InplaceEditorDeactivated
  {
    add => this.inplaceEditorDeactivated += value;
    remove => this.inplaceEditorDeactivated -= value;
  }

  /// <summary>Контрол редактора по месту</summary>
  [Browsable(false)]
  public Control InPlaceEditorControl
  {
    [DebuggerStepThrough] get => (Control) null;
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(bool force)
  {
    if (!force && this.SuspendedRefreshUIFlag || this.pageUI == null)
      return;
    if (this.page != null)
      this.page.InvalidateUI(this.pageUI.Bounds);
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  public override void InvalidateUI(Rectangle clipRectangle)
  {
    this.InvalidateUI(clipRectangle, false);
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(Rectangle clipRectangle, bool force)
  {
    if (this.SuspendedRefreshUIFlag)
      return;
    if (this.page != null)
      this.page.InvalidateUI(clipRectangle);
    if (this.pageUI == null)
      return;
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (this.SuspendedRefreshUIFlag || this.page == null)
      return;
    if (this.pageUI != null)
      this.RefreshUI(this.pageUI.Bounds);
    else
      this.page.RefreshUI();
  }

  /// <summary>Контейнер для управления размерами и положением прямоугольного
  /// элемента управления</summary>
  [Browsable(false)]
  public PageElementUI PageUI
  {
    [DebuggerStepThrough] get => this.pageUI;
    set
    {
      if (this.pageUI == value)
        return;
      int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
      if (num == 0)
        this.SuspendUpdateGeometryRefreshUI();
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) null;
        this.pageUI.Parent = (PageElementUI) null;
      }
      this.pageUI = value;
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) this;
        if (this.Parent is VisualNode parent)
          parent.AddChildUI((DocumentTreeNode) this, false);
      }
      this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (num != 0)
        return;
      this.ResumeUpdateRefreshUI(this.pageUI != null, true);
    }
  }

  /// <summary>Создать соответсвующий элемент управления. Должен быть перекрыт</summary>
  public override void CreateUI()
  {
    if (!this.IsVirtualNode && this.NeedUI && this.PageUI == null)
    {
      if (!(this.parent is Intermech.Document.Model.Page parent2))
      {
        if (!(this.parent is IPageElementWithInterface parent1) || parent1.PageUI == null)
          return;
      }
      else if (parent2.PageUI == null)
        return;
      TableData parentCell = this.ParentCell;
      this.PageUI = parentCell == null || parentCell.IsFixedStructureArea ? (PageElementUI) new RectanglePageElementUI() : (PageElementUI) new TableCellUI();
    }
    base.CreateUI();
  }

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  public override void DestroyUI()
  {
    this.PageUI = (PageElementUI) null;
    base.DestroyUI();
  }

  /// <summary>Можно активировать редактирование по месту</summary>
  public override bool CanActivateInPlaceEditor => false;

  /// <summary>Наименование типа</summary>
  [TypeConverter(typeof (NodeTypeCaptionConverter))]
  [System.ComponentModel.ReadOnly(false)]
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => LabelElement.ElementTypeName;
    set
    {
      DocumentMenuHelper.ConvertToElement(new DocumentTreeNode[1]
      {
        (DocumentTreeNode) this
      }, value);
    }
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (this.OwnerDocument is ImDocument ownerDocument && ownerDocument.DocumentControl != null && ownerDocument.DocumentControl.ReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    if (this.ElementType == LabelElementType.Text)
      properties.Remove((object) "BarCodeWigth");
    if (this.ElementType == LabelElementType.Text)
      return;
    properties.Remove((object) "ParagraphFormat");
    properties.Remove((object) "CharFormat");
    properties.Remove((object) "BackColor");
    properties.Remove((object) "Orientation");
  }

  /// <summary>Область вывода текста с учетом полей</summary>
  [Browsable(false)]
  public RectangleF TextLayoutArea
  {
    [DebuggerStepThrough] get
    {
      RectangleF bounds = this.Bounds;
      bounds.X += this.BorderWidth;
      bounds.Width -= 2f * this.BorderWidth;
      bounds.Y += this.BorderWidth;
      bounds.Height -= 2f * this.BorderWidth;
      return bounds;
    }
  }

  /// <summary>Формат символов</summary>
  public override CharFormat CharFormat
  {
    get => base.CharFormat;
    set
    {
      if (this.CharFormat == value)
        return;
      base.CharFormat = value;
      if (this.OwnerDocument is ImDocument ownerDocument && ownerDocument.DocumentControl != null)
        ownerDocument.DocumentControl.UpdateFormatCommands();
      this.RefreshUI();
    }
  }

  /// <summary>Форматирование параграфа</summary>
  public override ParagraphFormat ParagraphFormat
  {
    get => base.ParagraphFormat;
    set
    {
      ParagraphFormat paragraphFormat = this.ParagraphFormat;
      if (paragraphFormat == value || paragraphFormat != null && paragraphFormat.Equals(value))
        return;
      base.ParagraphFormat = value;
      if (this.OwnerDocument is ImDocument ownerDocument && ownerDocument.DocumentControl != null)
        ownerDocument.DocumentControl.UpdateFormatCommands();
      this.RefreshUI();
    }
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    this.DrawCell(context, (List<RowColParams>) null, -1, (List<RowColParams>) null, -1, true);
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Искать столбец и строк если не заданы</param>
  public override void DrawCell(
    DrawContext context,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag)
      return;
    bool? isSelected = context.IsSelected;
    bool? isFocused = context.IsFocused;
    RectangleElement template = context.Template;
    float? rowSize = context.RowSize;
    bool? isFixedSizeRow = context.IsFixedSizeRow;
    RectangleBorder borders = context.Borders;
    context.Borders = (RectangleBorder) null;
    TableData parentCell = this.ParentCell;
    if (parentCell == null || context.Margins == null)
      context.Margins = this.Margins;
    GraphicsState gstate1 = context.Graphics.Save();
    try
    {
      if (context.IsPaint && (!context.IsSelected.HasValue || !context.IsSelected.Value))
        context.IsSelected = new bool?(this.ShowSelected);
      if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.HasValue)
        context.IsFocused = parentCell == null || !parentCell.IsColumn ? new bool?(this.ShowFocused) : new bool?(false);
      context.Template = this.Template as RectangleElement;
      context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
      context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
      base.DrawCell(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
      if (context.Layer != 0 || context.WithoutData)
        return;
      Matrix transform = context.Graphics.Transform;
      DrawContextWithUI drawContextWithUi = context as DrawContextWithUI;
      PageControl pageControl = (PageControl) null;
      if (drawContextWithUi != null)
        pageControl = drawContextWithUi.PageControl;
      if (pageControl != null && context.IsPaint && this.Page != null)
        context.Graphics.Transform = (this.Page as Intermech.Document.Model.Page).PageUI.TransformMatrix.Matrix;
      GraphicsUnit pageUnit = context.Graphics.PageUnit;
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
      RectangleF rectangleF1 = this.Bounds;
      if (this.ElementType == LabelElementType.Text)
      {
        float num1 = 0.0f;
        float num2 = 0.0f;
        float num3 = 0.0f;
        float num4 = 0.0f;
        ParagraphFormat paragraphFormat1 = this.ParagraphFormat;
        float? nullable;
        int num5;
        if (paragraphFormat1 == null)
        {
          num5 = 0;
        }
        else
        {
          nullable = paragraphFormat1.IdentLeft;
          num5 = nullable.HasValue ? 1 : 0;
        }
        if (num5 != 0)
        {
          nullable = this.ParagraphFormat.IdentLeft;
          num1 = nullable.Value * 10f;
        }
        ParagraphFormat paragraphFormat2 = this.ParagraphFormat;
        int num6;
        if (paragraphFormat2 == null)
        {
          num6 = 0;
        }
        else
        {
          nullable = paragraphFormat2.IdentRight;
          num6 = nullable.HasValue ? 1 : 0;
        }
        if (num6 != 0)
        {
          nullable = this.ParagraphFormat.IdentRight;
          num2 = nullable.Value * 10f;
        }
        ParagraphFormat paragraphFormat3 = this.ParagraphFormat;
        int num7;
        if (paragraphFormat3 == null)
        {
          num7 = 0;
        }
        else
        {
          nullable = paragraphFormat3.IntervalBefore;
          num7 = nullable.HasValue ? 1 : 0;
        }
        if (num7 != 0)
        {
          nullable = this.ParagraphFormat.IntervalBefore;
          num3 = UnitsConverter.PointToMm(nullable.Value);
        }
        ParagraphFormat paragraphFormat4 = this.ParagraphFormat;
        int num8;
        if (paragraphFormat4 == null)
        {
          num8 = 0;
        }
        else
        {
          nullable = paragraphFormat4.IntervalAfter;
          num8 = nullable.HasValue ? 1 : 0;
        }
        if (num8 != 0)
        {
          nullable = this.ParagraphFormat.IntervalAfter;
          num4 = UnitsConverter.PointToMm(nullable.Value);
        }
        rectangleF1 = RectangleF.FromLTRB(rectangleF1.Left + num1, rectangleF1.Top + num3, rectangleF1.Right - num2, rectangleF1.Bottom - num4);
        Matrix vMatrix = new Matrix();
        TextOrientation orientation = this.Orientation;
        GraphicsState gstate2 = (GraphicsState) null;
        if (context.IsPdf)
          gstate2 = context.Graphics.Save();
        if ((double) rectangleF1.Width > 0.0 && (double) rectangleF1.Height > 0.0)
        {
          Font font = this.CharFormat.GetFont();
          Color black = Color.Black;
          if (this.CharFormat.TextColor.HasValue)
            black = this.CharFormat.TextColor.Value;
          StringFormat stringFormat = this.ParagraphFormat?.GetStringFormat() ?? new StringFormat();
          bool flag = false;
          RectangleF rectangleF2;
          if (context.IsPdf)
          {
            if ((this.CharFormat.CharStyle & CharStyle.Italic) != 0 && !string.IsNullOrWhiteSpace(this.FormattedText))
            {
              Intermech.Document.Model.TypographicFont.TypographicFont typographicFont = font.GetTypographicFont();
              flag = (typographicFont != null ? (typographicFont.Italic ? 1 : 0) : 0) == 0;
            }
            rectangleF2 = flag ? TextData.RotateTextBounds(rectangleF1, orientation, out vMatrix) : TextData.RotateTextBounds(rectangleF1, orientation, context.Graphics);
          }
          else
          {
            rectangleF2 = TextData.RotateTextBounds(rectangleF1, orientation, out vMatrix);
            context.Graphics.MultiplyTransform(vMatrix, System.Drawing.Drawing2D.MatrixOrder.Prepend);
          }
          if (flag)
          {
            IntPtr dc = LabelElement.GetDC(IntPtr.Zero);
            try
            {
              RectangleF rectangleF3 = new RectangleF(new PointF(0.0f, 0.0f), rectangleF2.Size);
              RectangleF rectangleF4 = new RectangleF(0.0f, 0.0f, rectangleF2.Width * 10f, rectangleF2.Height * 10f);
              Image image1 = (Image) new Metafile(dc, EmfType.EmfOnly);
              MetafileHeader metafileHeader = (image1 as Metafile).GetMetafileHeader();
              PointF pointF1 = new PointF(metafileHeader.DpiX, metafileHeader.DpiY);
              PointF pointF2 = PointF.Empty;
              using (Graphics graphics = Graphics.FromImage(image1))
                pointF2 = new PointF(graphics.DpiX, graphics.DpiY);
              RectangleF frameRect = new RectangleF(0.0f, 0.0f, rectangleF2.Width * pointF2.X / pointF1.X, rectangleF2.Height * pointF2.Y / pointF1.Y);
              Image image2 = (Image) new Metafile(dc, frameRect, MetafileFrameUnit.Millimeter, EmfType.EmfOnly);
              using (Graphics g1 = Graphics.FromImage(image2))
              {
                g1.PageUnit = GraphicsUnit.Millimeter;
                RectangleF rect = rectangleF3;
                g1.SetClip(rect);
                g1.AddMetafileComment(Encoding.Unicode.GetBytes("#Skip#EmfPolygon16"));
                g1.DrawRectangle(new Pen(Color.Red), rect.X, rect.Y, rect.Width, rect.Height);
                ImGraphics g2 = new ImGraphics(g1);
                Rectangle rectangle = new Rectangle(0, 0, (int) rectangleF2.Size.Width, (int) rectangleF2.Size.Height);
                if (context.IsFixedSizeRow_NN && (double) context.RowSize_NN != 0.0)
                {
                  this.DrawTextWithFixedSizeRow(g2, this.FormattedText, font, (Brush) new SolidBrush(black), (RectangleF) rectangle, stringFormat, context.RowSize_NN);
                }
                else
                {
                  using (SolidBrush solidBrush = new SolidBrush(black))
                    g2.DrawString(this.FormattedText, font, (Brush) solidBrush, (RectangleF) rectangle, stringFormat);
                }
              }
              Image image3 = (Image) image2.Clone();
              Image bitmap = (Image) (context.Graphics as PdfImGraphics).GetBitmap(image3, rectangleF2.Size, this.BackColor);
              Image image4 = LabelElement.RotateBitmap(orientation, bitmap);
              context.Graphics.DrawImage(image4, rectangleF1);
              image3.Dispose();
            }
            finally
            {
              LabelElement.ReleaseDC(IntPtr.Zero, dc);
            }
          }
          else if (context.IsFixedSizeRow_NN && (double) context.RowSize_NN != 0.0)
          {
            if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value)
              this.DrawTextWithFixedSizeRow(context.Graphics, this.FormattedText, font, (Brush) new SolidBrush(VisualNode.InvertColor(black)), rectangleF2, stringFormat, context.RowSize_NN);
            else
              this.DrawTextWithFixedSizeRow(context.Graphics, this.FormattedText, font, (Brush) new SolidBrush(black), rectangleF2, stringFormat, context.RowSize_NN);
          }
          else if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value)
          {
            using (SolidBrush solidBrush = new SolidBrush(VisualNode.InvertColor(black)))
              context.Graphics.DrawString(this.FormattedText, font, (Brush) solidBrush, rectangleF2, stringFormat);
          }
          else
          {
            using (SolidBrush solidBrush = new SolidBrush(black))
              context.Graphics.DrawString(this.FormattedText, font, (Brush) solidBrush, rectangleF2, stringFormat);
          }
        }
        if (context.IsPdf)
          context.Graphics.Restore(gstate2);
      }
      else
      {
        if (this.ElementType == LabelElementType.BarCode_CODE128)
        {
          if (!string.IsNullOrEmpty(this.Text))
          {
            try
            {
              Image image = Code128Rendering.MakeBarcodeImage(this.Text, (int) ((double) this.BarCodeWigth * 10.0), true);
              float width = UnitsConverter.PixelsToMm(image.Width, image.HorizontalResolution) / 10f;
              float height = UnitsConverter.PixelsToMm(image.Height, image.HorizontalResolution) / 10f;
              int x = (int) ((double) rectangleF1.X + ((double) rectangleF1.Width - (double) width) / 2.0);
              int y = (int) ((double) rectangleF1.Y + ((double) rectangleF1.Height - (double) height) / 2.0);
              context.Graphics.DrawImage(image, new RectangleF((float) x, (float) y, width, height));
            }
            catch
            {
            }
          }
        }
        if (this.ElementType == LabelElementType.BarCode_EAN13)
        {
          string StringToEncode = this.Text.TrimStart('-');
          int startIndex = 12;
          if (StringToEncode != null)
          {
            if (StringToEncode.Length > startIndex)
              StringToEncode = StringToEncode.Remove(startIndex, StringToEncode.Length - startIndex);
            if (StringToEncode.Length < startIndex)
            {
              while (StringToEncode.Length != startIndex)
                StringToEncode = "0" + StringToEncode;
            }
          }
          Barcode barcode = new Barcode();
          barcode.Alignment = AlignmentPositions.CENTER;
          try
          {
            barcode.IncludeLabel = true;
            barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
            Image image = barcode.Encode(TYPE.EAN13, StringToEncode, Color.Black, Color.White, (int) (100.0 * (double) this.BarCodeWigth), (int) (50.0 * (double) this.BarCodeWigth));
            float mm1 = UnitsConverter.PixelsToMm(image.Width, image.HorizontalResolution);
            float mm2 = UnitsConverter.PixelsToMm(image.Height, image.HorizontalResolution);
            int x = (int) ((double) rectangleF1.X + ((double) rectangleF1.Width - (double) mm1) / 2.0);
            int y = (int) ((double) rectangleF1.Y + ((double) rectangleF1.Height - (double) mm2) / 2.0);
            context.Graphics.DrawImageUnscaled(image, x, y);
          }
          catch
          {
          }
        }
      }
      context.Graphics.PageUnit = pageUnit;
      if (!context.IsPaint && context.Graphics.Transform == transform)
        return;
      context.Graphics.Transform = transform;
    }
    finally
    {
      context.IsSelected = isSelected;
      context.IsFocused = isFocused;
      context.Template = template;
      context.RowSize = rowSize;
      context.IsFixedSizeRow = isFixedSizeRow;
      context.MaterialList = (List<int>) null;
      context.Borders = borders;
      if (parentCell == null)
        context.Margins = (MarginsF) null;
      context.Graphics.Restore(gstate1);
    }
  }

  /// <summary>Повернуть битмап, если необходимо</summary>
  private static Image RotateBitmap(TextOrientation txtAngle, Image im)
  {
    int num;
    switch (txtAngle)
    {
      case TextOrientation.DownTop:
        num = 3;
        break;
      case TextOrientation.UpsideDown:
        num = 2;
        break;
      case TextOrientation.TopDown:
        num = 1;
        break;
      default:
        num = 0;
        break;
    }
    RotateFlipType rotateFlipType = (RotateFlipType) num;
    if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
    {
      im.RotateFlip(rotateFlipType);
      Image image = im;
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      ImageFormat png = ImageFormat.Png;
      image.Save((Stream) memoryStream2, png);
      im = (Image) new Bitmap((Stream) memoryStream1);
    }
    return im;
  }

  private void DrawTextWithFixedSizeRow(
    ImGraphics g,
    string text,
    Font font,
    Brush brush,
    RectangleF textRec,
    StringFormat stringFormat,
    float rowSize)
  {
    if (string.IsNullOrEmpty(text))
      return;
    if ((double) rowSize == 0.0)
      rowSize = 5f;
    RectangleF layoutRectangle = textRec;
    SizeF size = layoutRectangle.Size with
    {
      Height = UnitsConverter.PointToMm(font.SizeInPoints)
    };
    layoutRectangle.Height = rowSize;
    StringAlignment alignment = stringFormat.Alignment;
    StringAlignment lineAlignment = stringFormat.LineAlignment;
    StringFormatFlags formatFlags = stringFormat.FormatFlags;
    stringFormat.LineAlignment = StringAlignment.Center;
    try
    {
      int startIndex = 0;
      int length = text.Length;
      int charactersFitted = 0;
      int linesFilled = 0;
      switch (lineAlignment)
      {
        case StringAlignment.Center:
          layoutRectangle.Height = (float) int.MaxValue;
          g.MeasureString(text, font, layoutRectangle.Size, stringFormat, out charactersFitted, out linesFilled);
          layoutRectangle.Height = rowSize;
          float num1 = (float) linesFilled * rowSize;
          if ((double) num1 < (double) textRec.Height)
          {
            float num2 = (float) Math.Truncate(((double) textRec.Height - (double) num1) / 2.0 / (double) rowSize) * rowSize;
            layoutRectangle.Y += num2;
            break;
          }
          break;
        case StringAlignment.Far:
          layoutRectangle.Height = (float) int.MaxValue;
          g.MeasureString(text, font, layoutRectangle.Size, stringFormat, out charactersFitted, out linesFilled);
          layoutRectangle.Height = rowSize;
          float num3 = (float) linesFilled * rowSize;
          if ((double) num3 < (double) textRec.Height)
          {
            layoutRectangle.Y = textRec.Bottom - num3;
            break;
          }
          break;
      }
      stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
      for (; (double) layoutRectangle.Y < (double) textRec.Bottom && startIndex < length; layoutRectangle.Y += rowSize)
      {
        stringFormat.Alignment = StringAlignment.Near;
        g.MeasureString(text.Substring(startIndex), font, size, stringFormat, out charactersFitted, out linesFilled);
        stringFormat.Alignment = alignment;
        if (charactersFitted <= 0)
          break;
        g.DrawString(text.Substring(startIndex, charactersFitted), font, brush, layoutRectangle, stringFormat);
        startIndex += charactersFitted;
      }
    }
    finally
    {
      stringFormat.Alignment = alignment;
      stringFormat.LineAlignment = lineAlignment;
      stringFormat.FormatFlags = formatFlags;
    }
  }

  /// <summary>Создать элемент типа ContainerElement, перенести туда все данные,
  /// и заменить этот элемент на новый</summary>
  public virtual void ConvertToContainer()
  {
    ContainerElement child = new ContainerElement((RectangleElement) this);
    DocumentTreeNode parent = this.Parent;
    VisualNode visualNode = parent as VisualNode;
    if (parent == null)
      return;
    int index = this.Index;
    bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
    if (!updateUiGeometryFlag && visualNode != null)
      visualNode.SuspendUpdateGeometryRefreshUI();
    bool updateLayoutFlag = this.SuspendedUpdateLayoutFlag;
    if (!updateLayoutFlag)
      parent.SuspendUpdateLayout();
    try
    {
      parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
      parent.RemoveChildNodeAt(index + 1, false, false);
    }
    finally
    {
      if (!updateLayoutFlag)
        parent.ResumeUpdateLayout(false, true);
      if (!updateUiGeometryFlag && visualNode != null)
        visualNode.ResumeUpdateUIGeometry(true, true);
    }
  }

  /// <summary>Создать элемент типа TextBoxElement, перенести туда все данные,
  /// и заменить этот элемент на новый</summary>
  public virtual void ConvertToTextBox()
  {
    TextBoxElement child = new TextBoxElement((RectangleElement) this);
    DocumentTreeNode parent = this.Parent;
    VisualNode visualNode = parent as VisualNode;
    if (parent == null)
      return;
    int index = this.Index;
    bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
    if (!updateUiGeometryFlag && visualNode != null)
      visualNode.SuspendUpdateGeometryRefreshUI();
    bool updateLayoutFlag = this.SuspendedUpdateLayoutFlag;
    if (!updateLayoutFlag)
      parent.SuspendUpdateLayout();
    try
    {
      parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
      parent.RemoveChildNodeAt(index + 1, false, false);
    }
    finally
    {
      if (!updateLayoutFlag)
        parent.ResumeUpdateLayout(false, true);
      if (!updateUiGeometryFlag && visualNode != null)
        visualNode.ResumeUpdateUIGeometry(true, true);
    }
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Вызов в процессе загрузки</param>
  public override void ApplyTemplateProperties(
    DocumentTreeNode template,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    switch (template)
    {
      case null:
        break;
      case LabelElement _:
        base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
        break;
      case TextBoxElement textBoxElement:
        int index = this.Index;
        DocumentTreeNode parent = this.Parent;
        if (parent == null)
          break;
        TextBoxElement child = (TextBoxElement) textBoxElement.CloneFromTemplate(true, true);
        child.Id = this.Id;
        child.Name = this.Name;
        child.setBounds(this.bounds);
        child.setProperBounds(this.properBounds);
        child.AssignClonedByTemplateWithParent(this.ClonedByTemplateWithParent);
        if (this.ReferenceToTextSource != null)
          child.AssignReferenceToTextSource(this.ReferenceToTextSource.Clone(), false, false, false);
        else
          child.AssignReferenceToTextSource((ReferenceBase) null, false, false, false);
        child.AssignText(this.Text, false, true, true, false, false);
        parent.RemoveChildNodeAt(index, false, false);
        parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
        break;
      default:
        throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) template.GetDefautCaption(), (object) this.GetDefautCaption()));
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="source">Образец</param>
  public LabelElement(RectangleElement source)
  {
    IDictionary links = (IDictionary) new HybridDictionary();
    base.CopyFields((DocumentTreeNode) source, true, true, true, false, true, links);
    this.OnDeserialization((object) this);
    this.RestoreLinks(true, false, true, links);
  }

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new LabelElement(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new LabelElement();

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected LabelElement(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  public LabelElement(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
  }

  /// <summary>Конструктор</summary>
  public LabelElement()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public LabelElement(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Создать пустую ячейку таблицы</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  /// <returns>Ячейка таблицы</returns>
  protected override RectangleElement CreateEmptySingleCell(
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return (RectangleElement) new TextBoxElement(parent, bounds, visible);
  }

  /// <summary>Создать пустую таблицу</summary>
  /// <param name="isColumn">Столбец</param>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  /// <returns>Таблица</returns>
  protected override TableData CreateEmptyTable(
    bool isColumn,
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return (TableData) new TableElement(isColumn, parent, bounds, visible);
  }

  static LabelElement() => LabelElement.InitReadFieldDict();

  /// <summary>Копировать поля из src</summary>
  /// <param name="src">Источник</param>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать данные</param>
  /// <param name="copyDataNodes">Копировать узлы являющиеся ячейками данных для таблиц</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  protected override void CopyFields(
    DocumentTreeNode src,
    bool copyChildren,
    bool copyData,
    bool copyDataNodes,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    base.CopyFields(src, copyChildren, copyData, copyDataNodes, templateClone, externalLink, links);
    if (!(src is LabelElement labelElement))
      return;
    this.elementType = labelElement.elementType;
    this.barCodeWigth = labelElement.barCodeWigth;
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (this.ElementType != LabelElementType.Text)
      xw.WriteAttributeString("labelElementType", ((int) this.ElementType).ToString());
    if ((double) this.barCodeWigth == 1.0)
      return;
    xw.WriteAttributeString("barCodeWigth", this.barCodeWigth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (LabelElement.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      LabelElement.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    switch (readArgs.Reader.LocalName)
    {
      case "labelElementType":
        this.elementType = (LabelElementType) int.Parse(readArgs.Reader.Value);
        return true;
      case "barCodeWigth":
        this.barCodeWigth = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "vertical":
        if (bool.Parse(readArgs.Reader.Value))
          this.orientation = TextOrientation.DownTop;
        return true;
      default:
        if (readArgs.Version < 10 && readArgs.Reader.LocalName == "ReferenceToAttribute")
        {
          this.ReferenceToTextSource = (ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) this);
          this.ReferenceToTextSource.ReadFromXml(readArgs);
          return true;
        }
        return base.ReadFieldFromXml(readArgs);
    }
  }

  private static void InitReadFieldDict()
  {
    LabelElement.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) TextData.ReadFieldsDict);
  }
}
