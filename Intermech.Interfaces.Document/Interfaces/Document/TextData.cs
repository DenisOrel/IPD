// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TextData
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Элемент отображения данных.
/// Может отображать как собственные данные, так и данные документа.</summary>
[Serializable]
public class TextData : RectangleElement, INodeWithReference
{
  public const char NonBreakSpace = '\u000E';
  public const char UnicodeNonBreakSpace = ' ';
  public const char NonBreakDash = '\u0017';
  public const char ParagraphChar = '\u0015';
  /// <summary>Формат символов по умолчанию</summary>
  public static CharFormat DefaultCharFormat = new CharFormat();
  /// <summary>Формат абзаца по умолчанию</summary>
  public static ParagraphFormat DefaultParagraphFormat = new ParagraphFormat();
  [NonSerialized]
  private TextChanged_EventHandler textChanged;
  [NonSerialized]
  private TextValidating_EventHandler textValidating;
  [NonSerialized]
  private TextReadOnly_EventHandler textReadOnly;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  protected bool duplicateTextForAllPages;
  protected bool repeatTextAsHeader;
  /// <summary>Заменять спецсимволы AVS: '~' на неразрывный пробел и '?' на разрыв строки</summary>
  protected bool replaceOldAVSSpecChars;
  protected string textFormat;
  protected bool useTextFormatForRefs;
  private CharFormat charFormat;
  protected ParagraphFormat paragraphFormat;
  protected string text;
  protected ReferenceBase referenceToTextSource;
  protected TextOrientation orientation;
  /// <summary>Имя атрибута в который вносятся данные о изменении текста пользователем</summary>
  public const string CellAttr_ChangedByUser = "ChangedByUser";

  [System.ComponentModel.ReadOnly(false)]
  public override string NodeTypeCaption
  {
    get => base.NodeTypeCaption;
    set
    {
    }
  }

  public override bool ReadOnlyNow
  {
    get
    {
      TextReadOnly_EventArgs e = new TextReadOnly_EventArgs();
      e.ReadOnly = base.ReadOnlyNow;
      if (this.referenceToTextSource is ITextSource referenceToTextSource)
        e.ReadOnly |= referenceToTextSource.ReadOnly;
      this.OnTextReadOnly(e);
      return e.ReadOnly;
    }
  }

  /// <summary>Форматирование только для чтения.
  /// Запрет на форматирование</summary>
  [Browsable(false)]
  public virtual bool ReadOnlyFormating
  {
    get
    {
      if (!this.ReadOnlyNow)
        return false;
      return !this.AllowFormatingForReadOnlyText || this.IsCellWithDinamicHeaderGroupedText;
    }
  }

  /// <summary>
  /// Ячейка выводит переменный текст для разных состояний записи при включенной динамической группировке
  /// </summary>
  internal bool IsCellWithDinamicHeaderGroupedText
  {
    get
    {
      return this.OwnerDocument != null && this.OwnerDocument.DynamicGroupHeaderIsEnabled && this.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource && referenceToTextSource.ReferenceBaseType == BaseReferenceNodeType.ntSelectedNode && referenceToTextSource.AttributeName == "GroupHeaderCellText" && referenceToTextSource.NodeLink is RectangleElement nodeLink && nodeLink.HasGroupHeaderText;
    }
  }

  /// <summary>Разрешать форматирование для ReadOnly ячеек</summary>
  [Browsable(false)]
  public virtual bool AllowFormatingForReadOnlyText
  {
    [DebuggerStepThrough] get
    {
      return this.OwnerDocument != null && this.OwnerDocument.AllowFormatingForReadOnlyText;
    }
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (this.TemplateId == null)
      return;
    if (properties[(object) "Orientation"] is CustomPropertyDescriptor property1)
      property1.SetIsReadOnly(true);
    if (!(properties[(object) "Text"] is CustomPropertyDescriptor property2))
      return;
    property2.SetIsReadOnly(this.ReadOnlyNow);
  }

  /// <summary>Возможен вызов дополнительного редактора для элемента</summary>
  public override bool CanCallEditor
  {
    get
    {
      return this.CallExternalEditor != null && (this.CanCallExternalEditor == null || this.CanCallExternalEditor((DocumentTreeNode) this)) || base.CanCallEditor;
    }
  }

  public static RectangleF RotateTextBounds(
    RectangleF globalRec,
    TextOrientation txtAngle,
    out Matrix vMatrix,
    ImGraphics g = null)
  {
    RectangleF rectangleF = globalRec;
    vMatrix = new Matrix();
    switch (txtAngle)
    {
      case TextOrientation.DownTop:
        rectangleF = new RectangleF(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        vMatrix.Translate(-rectangleF.X, -rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Rotate(-90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Translate(rectangleF.X, rectangleF.Y + rectangleF.Width, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case TextOrientation.UpsideDown:
        vMatrix.Translate(-rectangleF.X, -rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Rotate(180f, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Translate(rectangleF.Right, rectangleF.Bottom, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case TextOrientation.TopDown:
        rectangleF = new RectangleF(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        vMatrix.Translate(-rectangleF.X, -rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Rotate(90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Translate(rectangleF.X + rectangleF.Height, rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
    }
    return rectangleF;
  }

  public static RectangleF RotateTextBounds(
    RectangleF globalRec,
    TextOrientation txtAngle,
    ImGraphics g)
  {
    RectangleF rectangleF = globalRec;
    switch (txtAngle)
    {
      case TextOrientation.Normal:
        g.TranslateTransform(0.5f, 0.0f);
        break;
      case TextOrientation.DownTop:
        rectangleF = new RectangleF(0.0f, 0.0f, globalRec.Height, globalRec.Width);
        g.TranslateTransform(globalRec.X, (float) ((double) globalRec.Y + (double) globalRec.Height - 0.5));
        g.RotateTransform(-90f);
        break;
      case TextOrientation.UpsideDown:
        rectangleF = new RectangleF(0.0f, 0.0f, globalRec.Width, globalRec.Height);
        g.TranslateTransform((float) ((double) globalRec.X + (double) globalRec.Width - 0.5), globalRec.Y + globalRec.Height);
        g.RotateTransform(180f);
        break;
      case TextOrientation.TopDown:
        rectangleF = new RectangleF(0.0f, 0.0f, globalRec.Height, globalRec.Width);
        g.TranslateTransform(globalRec.X + globalRec.Width, globalRec.Y + 0.5f);
        g.RotateTransform(90f);
        break;
    }
    return rectangleF;
  }

  public static RectangleF RotateTextBounds2(
    RectangleF globalRec,
    TextOrientation txtAngle,
    out Matrix vMatrix)
  {
    RectangleF rectangleF = globalRec;
    vMatrix = new Matrix();
    switch (txtAngle)
    {
      case TextOrientation.DownTop:
        rectangleF = new RectangleF(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        vMatrix.Rotate(-90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case TextOrientation.UpsideDown:
        vMatrix.Rotate(180f, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case TextOrientation.TopDown:
        rectangleF = new RectangleF(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        vMatrix.Rotate(90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
    }
    return rectangleF;
  }

  public static RectangleF RotateTextBounds(RectangleF globalRec, TextOrientation txtAngle)
  {
    RectangleF rectangleF = globalRec;
    switch (txtAngle)
    {
      case TextOrientation.DownTop:
        rectangleF = new RectangleF(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        break;
      case TextOrientation.TopDown:
        rectangleF = new RectangleF(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        break;
    }
    return rectangleF;
  }

  public static Rectangle RotateTextBounds(
    Rectangle globalRec,
    TextOrientation txtAngle,
    out Matrix vMatrix)
  {
    Rectangle rectangle = globalRec;
    vMatrix = new Matrix();
    switch (txtAngle)
    {
      case TextOrientation.DownTop:
        rectangle = new Rectangle(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        vMatrix.Translate((float) -rectangle.X, (float) -rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Rotate(-90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Translate((float) rectangle.X, (float) (rectangle.Y + rectangle.Width), System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case TextOrientation.UpsideDown:
        vMatrix.Translate((float) -rectangle.X, (float) -rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Rotate(180f, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Translate((float) rectangle.Right, (float) rectangle.Bottom, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case TextOrientation.TopDown:
        rectangle = new Rectangle(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        vMatrix.Translate((float) -rectangle.X, (float) -rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Rotate(90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        vMatrix.Translate((float) (rectangle.X + rectangle.Height), (float) rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
    }
    return rectangle;
  }

  public static Rectangle RotateTextBounds(Rectangle globalRec, TextOrientation txtAngle)
  {
    Rectangle rectangle = globalRec;
    switch (txtAngle)
    {
      case TextOrientation.DownTop:
        rectangle = new Rectangle(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        break;
      case TextOrientation.TopDown:
        rectangle = new Rectangle(globalRec.X, globalRec.Y, globalRec.Height, globalRec.Width);
        break;
    }
    return rectangle;
  }

  /// <summary>Используется свой CharFormat, а не из шаблона</summary>
  protected bool CharFormatOverrided
  {
    [DebuggerStepThrough] get => (this.overrideFlags & OverrideFlags.CharFormat) != 0;
    set
    {
      if (value)
        this.overrideFlags |= OverrideFlags.CharFormat;
      else
        this.overrideFlags &= ~OverrideFlags.CharFormat;
    }
  }

  /// <summary>Формат символов</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_481")]
  [CustomDescription("Attribute.Interfaces.Document_482")]
  [CustomCategory("Attribute.Interfaces.Document_483")]
  [RefreshProperties(RefreshProperties.All)]
  public virtual CharFormat CharFormat
  {
    [DebuggerStepThrough] get
    {
      CharFormat charFormat = this.charFormat;
      if (charFormat == null)
      {
        TextData template = (TextData) this.Template;
        if (template != null)
          charFormat = template.CharFormat;
        if (charFormat == null)
        {
          ImDocumentData ownerDocument = this.OwnerDocument;
          charFormat = ownerDocument == null || ownerDocument.IsFormulaLib ? TextData.DefaultCharFormat : ownerDocument.DefaultCharFormat;
        }
      }
      return charFormat;
    }
    set => this.SetCharFormat(value, true, true);
  }

  /// <summary>Инициализировать CharFormat ячейки</summary>
  public void InitCharFormat(CharFormat charFormat)
  {
    if (this.charFormat != null || this.Template != null)
      return;
    if (charFormat == null)
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      charFormat = ownerDocument == null ? TextData.DefaultCharFormat : ownerDocument.DefaultCharFormat;
    }
    if (charFormat == null)
      return;
    charFormat = charFormat.Clone();
    this.charFormat = charFormat;
  }

  /// <summary>Установить значение свойства CharFormat</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetCharFormat(CharFormat value, bool updateUI, bool updateLayout)
  {
    if (this.CharFormat == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "CharFormat", (object) this.CharFormat, (object) value);
    this.charFormat = value;
    this.overrideFlags |= OverrideFlags.CharFormat;
    this.SetNeedUpdateLayoutFlag(true, true, true, updateLayout);
    if (!updateLayout & updateUI)
    {
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Ориентация текста</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_484")]
  [CustomDescription("Attribute.Interfaces.Document_485")]
  [CustomCategory("Attribute.Interfaces.Document_486")]
  [RefreshProperties(RefreshProperties.All)]
  public virtual TextOrientation Orientation
  {
    [DebuggerStepThrough] get => this.orientation;
    set => this.SetOrientation(value, true, true);
  }

  /// <summary>Назначить значение Orientation</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetOrientation(TextOrientation value, bool updateUI, bool updateLayout)
  {
    if (this.Orientation == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Orientation", (object) this.Orientation, (object) value);
    this.orientation = value;
    if (updateUI)
    {
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Используется свой ParagraphFormat, а не из шаблона</summary>
  protected bool ParagraphFormatOverrided
  {
    [DebuggerStepThrough] get => (this.overrideFlags & OverrideFlags.ParagraphFormat) != 0;
    set
    {
      if (value)
        this.overrideFlags |= OverrideFlags.ParagraphFormat;
      else
        this.overrideFlags &= ~OverrideFlags.ParagraphFormat;
    }
  }

  /// <summary>Форматирование абзаца</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_487")]
  [CustomDescription("Attribute.Interfaces.Document_488")]
  [CustomCategory("Attribute.Interfaces.Document_489")]
  [RefreshProperties(RefreshProperties.All)]
  public virtual ParagraphFormat ParagraphFormat
  {
    get
    {
      ParagraphFormat paragraphFormat = this.paragraphFormat;
      if (paragraphFormat == null)
      {
        TextData template = (TextData) this.Template;
        if (template != null)
          paragraphFormat = template.ParagraphFormat;
        if (paragraphFormat == null)
        {
          ImDocumentData ownerDocument = this.OwnerDocument;
          paragraphFormat = ownerDocument == null || ownerDocument.IsFormulaLib ? TextData.DefaultParagraphFormat : ownerDocument.DefaultParagraphFormat;
        }
      }
      return paragraphFormat;
    }
    set => this.SetParagraphFormat(value, true, true);
  }

  /// <summary>Установить значение свойства ParagraphFormat</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="forceOverrideDefault">Перебивать настройку если она была взята по умолчанию и не хранится в элементе</param>
  public virtual void SetParagraphFormat(
    ParagraphFormat value,
    bool updateUI,
    bool updateLayout,
    bool forceOverrideDefault = false)
  {
    ParagraphFormat paragraphFormat = forceOverrideDefault ? this.paragraphFormat : this.ParagraphFormat;
    if (paragraphFormat == value || paragraphFormat != null && paragraphFormat.Equals(value))
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ParagraphFormat", (object) this.ParagraphFormat, (object) value);
    this.paragraphFormat = value;
    this.overrideFlags |= OverrideFlags.ParagraphFormat;
    this.SetNeedUpdateLayoutFlag(true, true, false, updateLayout);
    if (!updateLayout & updateUI)
    {
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Заменять материалы по списку ключевых слов или спецсимволам.
  /// После '/S' - числитель, после '/' - знаменатель
  /// После '^' - верхний индекс, после '/' - нижний
  /// '\' - заменяется на '/'</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_565")]
  [CustomDescription("Attribute.Interfaces.Document_566")]
  [CustomCategory("Attribute.Interfaces.Document_489")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ReplaceAVSMaterial
  {
    get => this.CheckFlags((byte) 32 /*0x20*/);
    set => this.SetReplaceAVSMaterial(value, true, true);
  }

  /// <summary>Задать новое значение свойству ReplaceAVSMaterial с вызовом обработчиков.
  /// Заменять материалы по списку ключевых слов или спецсимволам.
  /// После '/S' - числитель, после '/' - знаменатель
  /// После '^' - верхний индекс, после '/' - нижний
  /// '\' - заменяется на '/'</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void SetReplaceAVSMaterial(bool value, bool updateUI, bool updateLayout)
  {
    if (this.ReplaceAVSMaterial == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ReplaceAVSSpecSybmol", (object) this.ReplaceAVSMaterial, (object) value);
    this.AssignReplaceAVSMaterial(value, true);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Задать новое значение свойству ReplaceAVSMaterial.
  /// Заменять материалы по списку ключевых слов или спецсимволам.
  /// После '/S' - числитель, после '/' - знаменатель
  /// После '^' - верхний индекс, после '/' - нижний
  /// '\' - заменяется на '/'</summary>
  /// <param name="value">Значение</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignReplaceAVSMaterial(bool value, bool setOverrideFlag)
  {
    this.SetFlags((byte) 32 /*0x20*/, value);
    if (!setOverrideFlag)
      return;
    this.SetOverrideFlags3(OverrideFlags3.ReplaceAVSMaterial);
  }

  /// <summary>Заменять спецсимволы AVS: '~' на неразрывный пробел и '?' на разрыв строки</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_583")]
  [CustomDescription("Attribute.Interfaces.Document_584")]
  [CustomCategory("Attribute.Interfaces.Document_489")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ReplaceOldAVSSpecChars
  {
    get => this.replaceOldAVSSpecChars;
    set => this.SetReplaceOldAVSSpecChars(value, true, true);
  }

  /// <summary>Задать новое значение свойству ReplaceOldAVSSpecChars с вызовом обработчиков.
  /// Заменять спецсимволы AVS: '~' на неразрывный пробел и '?' на разрыв строки</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void SetReplaceOldAVSSpecChars(bool value, bool updateUI, bool updateLayout)
  {
    if (this.replaceOldAVSSpecChars == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ReplaceOldAVSSpecChars", (object) this.replaceOldAVSSpecChars, (object) value);
    this.replaceOldAVSSpecChars = value;
    this.SetOverrideFlags3(OverrideFlags3.ReplaceOldAVSSpecChars);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Задать новое значение свойству ReplaceOldAVSSpecChars.
  /// Заменять спецсимволы AVS: '~' на неразрывный пробел и '?' на разрыв строки</summary>
  /// <param name="value">Значение</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignReplaceOldAVSSpecChars(bool value, bool setOverrideFlag)
  {
    this.replaceOldAVSSpecChars = value;
    if (!setOverrideFlag)
      return;
    this.overrideFlags3 |= OverrideFlags3.ReplaceOldAVSSpecChars;
  }

  public static int CharCountInEditor(string text)
  {
    return string.IsNullOrEmpty(text) ? 0 : text.Length + text.Where<char>((Func<char, bool>) (c => c == '\u0015')).Count<char>();
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  public TextData(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
  }

  /// <summary>Конструктор</summary>
  public TextData()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public TextData(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new TextData();

  static TextData() => TextData.InitReadFieldDict();

  /// <summary>В ячейке ничего не отображается</summary>
  [Browsable(false)]
  public virtual bool IsEmptyText
  {
    [DebuggerStepThrough] get
    {
      string text = this.Text;
      return text == null || text == "";
    }
  }

  /// <summary>Дублировать текст на всех страницах</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_551")]
  [CustomDescription("Attribute.Interfaces.Document_552")]
  [CustomCategory("Attribute.Interfaces.Document_472")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool DuplicateTextForAllPages
  {
    [DebuggerStepThrough] get => this.duplicateTextForAllPages;
    set => this.SetDuplicateTextForAllPages(value, true, true);
  }

  /// <summary>Установить новое значение свойства DuplicateTextForAllPages</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetDuplicateTextForAllPages(bool value, bool updateUI, bool updateLayout)
  {
    if (this.duplicateTextForAllPages == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "DuplicateTextForAllPages", (object) this.duplicateTextForAllPages, (object) value);
    this.duplicateTextForAllPages = value;
    if (this.duplicateTextForAllPages)
      this.UpdateDuplicatedText(updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Обновить дублированный текст</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Возвращает true, если был получен дублированный текст. false, если это оригинал</returns>
  protected virtual bool UpdateDuplicatedText(bool updateUI, bool updateLayout)
  {
    if (this.duplicateTextForAllPages)
    {
      TextData firstTextByTemplate = this.GetFirstTextByTemplate();
      if (firstTextByTemplate != null && firstTextByTemplate != this)
      {
        this.AssignText(firstTextByTemplate.Text, true, true, false, updateUI, updateLayout);
        return true;
      }
    }
    return false;
  }

  /// <summary>Повторять как заголовок на следующей странице</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_573")]
  [CustomDescription("Attribute.Interfaces.Document_574")]
  [CustomCategory("Attribute.Interfaces.Document_472")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool RepeatTextAsHeader
  {
    [DebuggerStepThrough] get => this.repeatTextAsHeader;
    set => this.SetRepeatTextAsHeader(value, true, true);
  }

  /// <summary>Установить новое значение свойства RepeatTextAsHeader</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetRepeatTextAsHeader(bool value, bool updateUI, bool updateLayout)
  {
    if (this.repeatTextAsHeader == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "RepeatTextAsHeader", (object) this.repeatTextAsHeader, (object) value);
    this.repeatTextAsHeader = value;
    if (this.repeatTextAsHeader)
      this.UpdateDuplicatedText(updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Получить ячейку с оригиналом дублированного текста</summary>
  /// <returns></returns>
  protected TextData GetFirstTextByTemplate()
  {
    TextData firstTextByTemplate = (TextData) null;
    if (!this.IsTemplate && this.HasTemplate())
      firstTextByTemplate = this.FindNearestNodeFromTemplate(this.Template, true, true) as TextData;
    return firstTextByTemplate;
  }

  /// <summary>Строка формата вывода текста</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_490")]
  [CustomDescription("Attribute.Interfaces.Document_491")]
  [CustomCategory("Attribute.Interfaces.Document_492")]
  public string TextFormat
  {
    [DebuggerStepThrough] get
    {
      if (this.textFormat != null)
        return this.textFormat;
      return this.Template is TextData template ? template.TextFormat : (string) null;
    }
    set => this.AssignTextFormat(value, true);
  }

  /// <summary>Назначить значение TextFormat</summary>
  /// <param name="value"></param>
  /// <param name="updateUI"></param>
  public void AssignTextFormat(string value, bool updateUI)
  {
    if (!(this.textFormat != value))
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "TextFormat", (object) this.TextFormat, (object) value);
    this.textFormat = value;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.OnChanged(new Changed_EventArgs());
    this.RefreshUI();
  }

  /// <summary>Использовать форматирование текста в источниках данных</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_613")]
  [CustomDescription("Attribute.Interfaces.Document_614")]
  [CustomCategory("Attribute.Interfaces.Document_492")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool UseTextFormatForRefs
  {
    [DebuggerStepThrough] get => this.useTextFormatForRefs;
    set => this.AssignUseTextFormatForRefs(value, true);
  }

  /// <summary>Назначить значение UseTextFormatForRefs</summary>
  /// <param name="value"></param>
  /// <param name="updateUI"></param>
  public void AssignUseTextFormatForRefs(bool value, bool updateUI)
  {
    if (this.useTextFormatForRefs == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "TextFormat", (object) this.TextFormat, (object) value);
    this.useTextFormatForRefs = value;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.OnChanged(new Changed_EventArgs());
    this.RefreshUI();
  }

  /// <summary>Текст в поле может разбиваться по страницам</summary>
  protected virtual bool IsDistributedText => false;

  /// <summary>Получить текст</summary>
  /// <remarks>Получает текст по следующим правилам:
  /// Ссылка на текст имеет наибольший приоритет, затем поле text, затем шаблон</remarks>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то проверка стартует внутри</param>
  /// <returns>Возвращает текст для этого элемента. Вместо null возвращает "".</returns>
  public virtual string GetText(List<DocumentTreeNode> callChain = null)
  {
    string text = this.text;
    TextData textData = (TextData) null;
    if (this.repeatTextAsHeader || this.IsDistributedText)
      textData = this.FindFirstCell() as TextData;
    if (textData == null)
      textData = this;
    if (this.duplicateTextForAllPages && this.OwnerDocument != null && !this.IsTemplate && this.HasTemplate() && this.OwnerDocument.FindFirstNodeFromTemplate(this.Template) is TextData nodeFromTemplate)
      textData = nodeFromTemplate;
    ITextSource referenceToTextSource1 = textData.referenceToTextSource as ITextSource;
    bool flag = this.IsNotLoopTextLink(referenceToTextSource1, callChain);
    if (!flag && LogManager.CreateLog)
      LogManager.AddLine($"Циклическая ссылка в элементе '{this.GetDefautCaption()}'");
    if (referenceToTextSource1 != null & flag)
    {
      if (textData.referenceToTextSource is ITextSourceWithCallChain referenceToTextSource2)
      {
        if (callChain == null)
          callChain = new List<DocumentTreeNode>(2);
        callChain.Add((DocumentTreeNode) this);
        text = referenceToTextSource2.GetAcyclicText(callChain);
      }
      else
        text = referenceToTextSource1.Text;
    }
    else if (textData.text != null)
    {
      text = textData.text;
    }
    else
    {
      TextData template = (TextData) textData.Template;
      if (template != null)
        text = template.GetText();
    }
    if (text == null)
      text = "";
    return text;
  }

  /// <summary>Текст</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_493")]
  [CustomDescription("Attribute.Interfaces.Document_494")]
  [CustomCategory("Attribute.Interfaces.Document_495")]
  [RefreshProperties(RefreshProperties.All)]
  public virtual string Text
  {
    get => this.GetText();
    set
    {
      this.AssignText(value, false, true, true, true, true);
      if (!this.ContainsAttribute("ChangedByUser"))
        return;
      this.SetAttributeValue("ChangedByUser", "Changed");
    }
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="value">Текст</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignText(string value, bool saveUndo, bool updateUI, bool updateLayout)
  {
    this.AssignText(value, false, true, saveUndo, updateUI, updateLayout, (List<DocumentTreeNode>) null);
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="value">Текст</param>
  /// <param name="fromOriginalText">Значение устанавливается от оригинальной ячейки дубликату текста</param>
  /// <param name="updateActiveEditor">Обновить текстовый редактор, если он активен</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignText(
    string value,
    bool fromOriginalText,
    bool updateActiveEditor,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    this.AssignText(value, fromOriginalText, updateActiveEditor, saveUndo, updateUI, updateLayout, (List<DocumentTreeNode>) null);
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="value">Текст</param>
  /// <param name="fromOriginalText">Значение устанавливается от оригинальной ячейки дубликату текста</param>
  /// <param name="updateActiveEditor">Обновить текстовый редактор, если он активен</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то проверка стартует внутри</param>
  protected virtual void AssignText(
    string value,
    bool fromOriginalText,
    bool updateActiveEditor,
    bool saveUndo,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (this.repeatTextAsHeader && this.prevCell != null)
    {
      if (!(this.FindFirstCell() is TextData firstCell))
        return;
      firstCell.AssignText(value, fromOriginalText, updateActiveEditor, saveUndo, updateUI, updateLayout);
    }
    else
    {
      string text = this.Text;
      if (!(text != value))
        return;
      if (!fromOriginalText && this.duplicateTextForAllPages)
      {
        TextData firstTextByTemplate = this.GetFirstTextByTemplate();
        if (firstTextByTemplate != null && firstTextByTemplate != this)
        {
          firstTextByTemplate.AssignText(value, false, updateActiveEditor, saveUndo, updateUI, updateLayout);
          return;
        }
        List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
        this.FindNodesFromTemplate(this.Template, foundNodes);
        for (int index = 0; index < foundNodes.Count; ++index)
        {
          if (foundNodes[index] != this && foundNodes[index] is TextData textData)
            textData.AssignText(value, true, updateActiveEditor, saveUndo, false, false);
        }
      }
      TextValidating_EventArgs e = new TextValidating_EventArgs(value);
      this.OnTextValidating(e);
      if (e.Cancel)
        return;
      if (this.referenceToTextSource is ITextSource referenceToTextSource2 && this.IsNotLoopTextLink(referenceToTextSource2, callChain))
      {
        if (this.referenceToTextSource is ITextSourceWithCallChain referenceToTextSource1)
        {
          if (callChain == null)
            callChain = new List<DocumentTreeNode>(2);
          callChain.Add((DocumentTreeNode) this);
          referenceToTextSource1.SetText(e.Text, saveUndo, updateUI, updateLayout, callChain);
          if (referenceToTextSource2 is ReferenceToNodeAttributeBase nodeAttributeBase && nodeAttributeBase.AttributeName == DocumentTreeNode.AttributeName_Text)
            this.text = e.Text;
        }
        else
          referenceToTextSource2.SetText(e.Text, saveUndo, updateUI, updateLayout);
      }
      else
      {
        if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, "Text", (object) this.Text, (object) value);
        this.text = e.Text;
        this.GetText();
        this.OnTextChanged(new TextChanged_EventArgs(text, e.Text, true, updateActiveEditor, false, updateUI, updateLayout));
        if (updateUI)
          this.RefreshUI();
      }
      if (this.OwnerDocument == null)
        return;
      this.OwnerDocument.Modified = true;
    }
  }

  private bool IsNotLoopTextLink(ITextSource textSource, List<DocumentTreeNode> callChain)
  {
    if (!(textSource is ReferenceToNodeAttributeBase nodeAttributeBase) || nodeAttributeBase.AttributeName != DocumentTreeNode.AttributeName_Text)
      return true;
    if (nodeAttributeBase.NodeLink == this)
      return false;
    return callChain == null || !callChain.Contains((DocumentTreeNode) this);
  }

  /// <summary>Обновить ссылки на узлы</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateNodeLinks(
    bool recursive,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    bool flag = false;
    if (this.duplicateTextForAllPages)
      flag = this.UpdateDuplicatedText(updateUI, updateLayout);
    if (!flag && this.referenceToTextSource != null && (this.referenceToTextSource is ReferenceToNode || this.referenceToTextSource.IsDependOnDocument))
    {
      string text = this.Text;
      this.referenceToTextSource.UpdateLink(updateUI, updateLayout);
      if (text != this.Text)
      {
        ReferenceToDBObjectBase referenceToTextSource = this.referenceToTextSource as ReferenceToDBObjectBase;
        this.OnTextChanged(new TextChanged_EventArgs(text, this.Text, true, true, referenceToTextSource != null && !referenceToTextSource.PassiveLink, updateUI, updateLayout));
      }
    }
    base.UpdateNodeLinks(recursive, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Обновить ссылки на узлы обновляемые при печати</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdatePrintLinks(
    bool recursive,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (this.referenceToTextSource != null && this.referenceToTextSource.IsDependOnPrint)
      this.referenceToTextSource.UpdateLink(updateUI, updateLayout);
    base.UpdatePrintLinks(recursive, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Обновить ссылки на атрибуты</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateNodeAttributeLinks(bool recursive, bool updateUI, bool updateLayout)
  {
    if (this.duplicateTextForAllPages)
      this.text = this.GetText();
    if (this.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource)
      referenceToTextSource.UpdateLink(updateUI, updateLayout);
    base.UpdateNodeAttributeLinks(recursive, updateUI, updateLayout);
  }

  /// <summary>Обновить идентификаторы в ссылках на данные по установленным связям с данными</summary>
  internal override void UpdateDataIdCacheLinks()
  {
    base.UpdateDataIdCacheLinks();
    if (this.referenceToTextSource == null)
      return;
    this.referenceToTextSource.UpdateLink(false, false);
  }

  /// <summary>Для внутреннего использования. Скопировать текст и форматирование в другой элемент. Не производит обновлений</summary>
  /// <param name="destination">Элемент приёмник</param>
  public virtual void CopyTextAndFormatTo(TextData destination)
  {
    if (destination == null)
      throw new ArgumentNullException(nameof (destination));
    if (this.referenceToTextSource != null)
    {
      destination.referenceToTextSource = this.referenceToTextSource.Clone();
      destination.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) destination);
      destination.SetOverrideFlags2(OverrideFlags2.Reference);
    }
    else
      destination.AssignReferenceToTextSource((ReferenceBase) null, true, false, false);
    destination.text = this.text;
    if ((this.overrideFlags & OverrideFlags.ParagraphFormat) != OverrideFlags.None && this.paragraphFormat != null)
      destination.SetParagraphFormat(this.paragraphFormat.Clone(), false, false);
    if ((this.overrideFlags & OverrideFlags.CharFormat) != OverrideFlags.None && this.charFormat != null)
      destination.SetCharFormat(this.charFormat.Clone(), false, false);
    if ((this.overrideFlags & OverrideFlags.SkipBefore) != OverrideFlags.None && (this.overrideFlags2 & OverrideFlags2.SkipBeforeForPlugin) == OverrideFlags2.None)
    {
      destination.overrideFlags |= OverrideFlags.SkipBefore;
      destination.overrideFlags2 &= ~OverrideFlags2.SkipBeforeForPlugin;
      destination.skipCellsBefore = this.skipCellsBefore;
    }
    if ((this.overrideFlags & OverrideFlags.SkipAfter) != OverrideFlags.None && (this.overrideFlags2 & OverrideFlags2.SkipAfterForPlugin) == OverrideFlags2.None)
    {
      destination.overrideFlags |= OverrideFlags.SkipAfter;
      destination.overrideFlags2 &= ~OverrideFlags2.SkipAfterForPlugin;
      destination.skipCellsAfter = this.skipCellsAfter;
    }
    if ((this.overrideFlags3 & OverrideFlags3.IgnoreSkipOuterCells) != OverrideFlags3.None)
    {
      destination.overrideFlags3 |= OverrideFlags3.IgnoreSkipOuterCells;
      destination.ignoreSkipOuterCells = this.ignoreSkipOuterCells;
    }
    if ((this.overrideFlags3 & OverrideFlags3.NonSkipBeforeAtStartPage) != OverrideFlags3.None)
      destination.AssignNonSkipBeforeAtStartPage(this.NonSkipBeforeAtStartPage, true);
    HybridDictionary hybridDictionary = new HybridDictionary();
    this.GetAttributes((IDictionary) hybridDictionary, false);
    destination.AddAdditionalAttributes((IDictionary) hybridDictionary);
    destination.SetNeedUpdateLayoutFlag(true, false, false, false);
  }

  private void TextSource_Removed(DocumentTreeNode node)
  {
  }

  /// <summary>Событие Текст изменен</summary>
  public event TextChanged_EventHandler TextChanged
  {
    add => this.textChanged += value;
    remove => this.textChanged -= value;
  }

  /// <summary>Вызывает событие Текст изменен</summary>
  /// <param name="e">Данные события</param>
  public virtual void OnTextChanged(TextChanged_EventArgs e)
  {
    if (this.OwnerDocument != null)
      this.OwnerDocument.OnTextChanged((object) this, e);
    if (this.textChanged != null)
      this.textChanged((object) this, e);
    if (e.UpdateUI)
      this.RefreshUI();
    if (this.ConnectionList != null)
    {
      int index = 0;
      for (int count = this.ConnectionList.Count; index < count; ++index)
      {
        if (this.ConnectionList[index] is ReferenceToTemplate connection && connection.OwnerNode is TextData ownerNode && ownerNode.text == null)
          ownerNode.OnTextChanged(e);
      }
    }
    this.SetPropertiesChangedFlag(true, true, false, e.UpdateUI, e.UpdateLayout);
    if (!e.UpdateUI)
      return;
    this.OnChanged(new Changed_EventArgs(e.SaveModificationDate));
  }

  /// <summary>Событие Текст изменен</summary>
  public event TextValidating_EventHandler TextValidating
  {
    add => this.textValidating += value;
    remove => this.textValidating -= value;
  }

  /// <summary>Вызывает событие Текст изменен</summary>
  /// <param name="e">Данные события</param>
  public virtual void OnTextValidating(TextValidating_EventArgs e)
  {
    if (this.OwnerDocument != null)
      this.OwnerDocument.OnTextValidating((object) this, e);
    if (this.textValidating == null)
      return;
    this.textValidating((object) this, e);
  }

  /// <summary>Событие TextReadOnly</summary>
  public event TextReadOnly_EventHandler TextReadOnly
  {
    add => this.textReadOnly += value;
    remove => this.textReadOnly -= value;
  }

  /// <summary>Вызывает событие TextReadOnly</summary>
  /// <param name="e">Данные события</param>
  public virtual void OnTextReadOnly(TextReadOnly_EventArgs e)
  {
    if (this.textReadOnly == null)
      return;
    this.textReadOnly((object) this, e);
  }

  /// <summary>Преобразовать в ячейку-шапку рекурсивно. Удаляет ячейки данных</summary>
  public override void ConvertToHeader(bool removeData)
  {
    if (this.TableCellType != CellType.DataCell)
      return;
    if (this.nodes != null)
    {
      int index = 0;
      for (int count = this.nodes.Count; index < count; ++index)
      {
        if (this.nodes[index] is RectangleElement node)
          node.ConvertToHeader(false);
      }
    }
    this.TableCellType = CellType.Header;
  }

  /// <summary>Свойство для интерфейса INodeWithReference</summary>
  ReferenceBase INodeWithReference.Reference
  {
    [DebuggerStepThrough] get => this.referenceToTextSource;
  }

  /// <summary>Ссылка на источник текста</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_496")]
  [CustomDescription("Attribute.Interfaces.Document_497")]
  [CustomCategory("Attribute.Interfaces.Document_498")]
  public virtual ReferenceBase ReferenceToTextSource
  {
    [DebuggerStepThrough] get => this.referenceToTextSource;
    set => this.AssignReferenceToTextSource(value, true, true, true);
  }

  /// <summary>Назначить новое значение ссылке на источник данных</summary>
  /// <param name="value">Новая ссылка</param>
  /// <param name="setOverrideFlag">Устанавливать флаг перекрытия шаблона</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку документа</param>
  public virtual void AssignReferenceToTextSource(
    ReferenceBase value,
    bool setOverrideFlag,
    bool updateUI,
    bool updateLayout)
  {
    if (this.referenceToTextSource == value)
      return;
    string text = this.Text;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ReferenceToTextSource", (object) this.ReferenceToTextSource, (object) value);
    bool flag1 = false;
    bool flag2 = value != null && value.IsDependOnDocument;
    if (this.referenceToTextSource != null)
    {
      flag1 = this.referenceToTextSource.IsDependOnDocument;
      this.referenceToTextSource.DisconnectLink();
      this.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) null);
      if (this.page != null && flag1 != flag2)
        this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
    this.referenceToTextSource = value;
    if (this.referenceToTextSource != null)
    {
      this.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) this);
      this.referenceToTextSource.UpdateLink(true, true);
      if (((this.page == null ? 0 : (flag1 != flag2 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
        this.page.DocumentChanged += new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
    else
      this.text = text;
    if (setOverrideFlag)
      this.SetOverrideFlags2(OverrideFlags2.Reference);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    if (text != this.Text)
    {
      this.OnTextChanged(new TextChanged_EventArgs(text, this.Text, true, true, false, updateUI, updateLayout));
    }
    else
    {
      if (!updateUI)
        return;
      this.OnChanged(new Changed_EventArgs(false));
    }
  }

  /// <summary>Содержит ли объект виртуальный атрибут с указанным именем</summary>
  /// <param name="attributeName">Имя виртуального атрибута</param>
  /// <returns>Возвращает true, если объект содержит виртуальный атрибут
  /// с указанным именем</returns>
  internal override bool ContainsVirtualAttribute(string attributeName)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    return attributeName == DocumentTreeNode.AttributeName_Text || base.ContainsVirtualAttribute(attributeName);
  }

  /// <summary>Получить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="notNull">Вернуть пустую строку вместо значения null</param>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то работает без проверок</param>
  /// <returns>Результат выполнения</returns>
  protected override GetVirtualAttributeResult GetVirtualAttributeValue(
    string attributeName,
    bool notNull,
    List<DocumentTreeNode> callChain = null)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    return attributeName == DocumentTreeNode.AttributeName_Text ? new GetVirtualAttributeResult(true, this.GetText(callChain)) : base.GetVirtualAttributeValue(attributeName, notNull, callChain);
  }

  /// <summary>Установить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  /// <returns>Результат выполнения</returns>
  protected override SetVirtualAttributeResult SetVirtualAttributeValue(
    string attributeName,
    string attributeValue,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    if (!(attributeName == DocumentTreeNode.AttributeName_Text))
      return base.SetVirtualAttributeValue(attributeName, attributeValue, updateUI, updateLayout, callChain);
    this.AssignText(attributeValue, false, true, true, updateUI, updateLayout, callChain);
    return new SetVirtualAttributeResult(true, false);
  }

  /// <summary>Получить список всех имен атрибутов</summary>
  /// <param name="forSaveOnly">Добавлять в список только те атрибуты, которые должны сохраниться в XML или копироваться при копировании через буфер</param>
  /// <returns>Список всех имен атрибутов</returns>
  protected override void GetVirtualAttributeNames(
    StringCollection attributeNames,
    bool forSaveOnly = false)
  {
    if (attributeNames == null)
      throw new ArgumentNullException(nameof (attributeNames));
    attributeNames.Add(DocumentTreeNode.AttributeName_Text);
    base.GetVirtualAttributeNames(attributeNames, forSaveOnly);
  }

  /// <summary>Элемент не содержит данных
  /// <remarks>
  /// Если emptyCellIsData - true, то ячейка считается пустой только когда является продолжением и ничего не содержит
  /// (а значит её можно удалить), а одиночная пустая ячейка считается содержимым для таблицы
  /// Если emptyCellIsData - false, то она считается пустой когда не содержит данных либо внутренние ячейки пусты
  /// </remarks>
  /// </summary>
  /// <param name="emptyCellIsData">Допустимы пустые ячейки</param>
  /// <param name="checkNextTable">Проверять следующую ячейку</param>
  /// <returns></returns>
  public override bool IsEmptyData(bool emptyCellIsData, bool checkNextCell = true)
  {
    if (this.repeatTextAsHeader)
      return true;
    if (!this.IsEmptyText || !checkNextCell || this.nextCell == null)
      return false;
    this.nextCell.IsEmptyData(emptyCellIsData);
    return false;
  }

  /// <summary>Поток пустой</summary>
  /// <returns></returns>
  public override bool AllFlowsIsEmpty() => this.prevCell != null && this.IsEmptyData(false, true);

  /// <summary>Удалить лишний перевод строки в последней строке</summary>
  /// <param name="planeText">Текст</param>
  /// <param name="removeAvsReservedSymbol">Удалять знак '?'</param>
  /// <returns>Подчищенный текст</returns>
  public static string DeleteLastEndLine(string planeText, bool removeAvsReservedSymbol)
  {
    if (string.IsNullOrEmpty(planeText))
      return planeText;
    int index = planeText.Length - 1;
    while (index >= 0 && (planeText[index] == '\r' || planeText[index] == '\n' || planeText[index] == '\u0015' || planeText[index] == '?' && removeAvsReservedSymbol))
      --index;
    int startIndex = index + 1;
    return startIndex < planeText.Length ? planeText.Remove(startIndex) : planeText;
  }

  protected override bool IsAllowableLocalDataLink() => false;

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
    if (template == null || !(template is TextData textData))
      return;
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      this.orientation = textData.orientation;
      this.duplicateTextForAllPages = textData.duplicateTextForAllPages;
      this.repeatTextAsHeader = textData.repeatTextAsHeader;
      if ((this.overrideFlags3 & OverrideFlags3.ReplaceOldAVSSpecChars) == OverrideFlags3.None)
        this.replaceOldAVSSpecChars = textData.replaceOldAVSSpecChars;
      if ((this.overrideFlags3 & OverrideFlags3.ReplaceAVSMaterial) == OverrideFlags3.None)
        this.AssignReplaceAVSMaterial(textData.ReplaceAVSMaterial, false);
      if ((this.overrideFlags2 & OverrideFlags2.Reference) == OverrideFlags2.None)
      {
        if (textData.referenceToTextSource != null)
        {
          if (this.referenceToTextSource != null && this.referenceToTextSource.GetType().IsEquivalentTo(textData.referenceToTextSource.GetType()))
          {
            this.referenceToTextSource.CopyData(textData.referenceToTextSource, !this.referenceToTextSource.NeedSaveTextValueToCache);
          }
          else
          {
            ReferenceBase referenceBase = textData.referenceToTextSource.Clone();
            if (this.referenceToTextSource != null && this.referenceToTextSource.NeedSaveTextValueToCache)
            {
              string text = this.Text;
              if (referenceBase is ITextSource textSource)
                textSource.SetText(text, true, false, false);
            }
            this.referenceToTextSource = referenceBase;
            this.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) this);
            if (textData.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource1 && referenceToTextSource1.ReferenceBaseType == BaseReferenceNodeType.ntSelectedNode && this.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource2)
              referenceToTextSource2.SetReference((DocumentTreeNode) null);
            if (this.page != null)
            {
              if (this.referenceToTextSource.IsDependOnDocument)
                this.page.DocumentChanged += new DocumentChanged_EventHandler(this.Page_DocumentChanged);
              else
                this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
            }
          }
        }
        else
          this.AssignReferenceToTextSource((ReferenceBase) null, false, false, false);
      }
      if ((this.overrideFlags & OverrideFlags.TextFormat) == OverrideFlags.None)
        this.textFormat = textData.textFormat;
      if ((this.overrideFlags3 & OverrideFlags3.UseTextFormatInRef) == OverrideFlags3.None)
        this.useTextFormatForRefs = textData.useTextFormatForRefs;
      if ((this.overrideFlags & OverrideFlags.CharFormat) == OverrideFlags.None)
        this.charFormat = textData.charFormat;
      if ((this.overrideFlags & OverrideFlags.ParagraphFormat) == OverrideFlags.None)
        this.paragraphFormat = textData.paragraphFormat;
      base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is TextData;
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.HasTemplate();
    if (this.orientation != TextOrientation.Normal)
      xw.WriteAttributeString("textAngle", this.orientation.ToString());
    if (this.duplicateTextForAllPages && !flag)
      xw.WriteAttributeString("duplicateText", "1");
    if (this.repeatTextAsHeader && !flag)
      xw.WriteAttributeString("repeatAsHeader", "1");
    if (this.replaceOldAVSSpecChars | flag && (!flag || (this.overrideFlags3 & OverrideFlags3.ReplaceOldAVSSpecChars) != OverrideFlags3.None))
      xw.WriteAttributeString("oldAVSChars", this.replaceOldAVSSpecChars ? "1" : "0");
    if (!(this.ReplaceAVSMaterial | flag) || flag && (this.overrideFlags3 & OverrideFlags3.ReplaceAVSMaterial) == OverrideFlags3.None)
      return;
    xw.WriteAttributeString("avsSymbol", this.ReplaceAVSMaterial ? "1" : "0");
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    bool flag = this.Template != null;
    if (this.referenceToTextSource != null)
    {
      if (!flag || (this.overrideFlags2 & OverrideFlags2.Reference) != OverrideFlags2.None)
        this.referenceToTextSource.WriteToXml("Reference", xw, objectRefId);
      else if (this.referenceToTextSource.NeedSaveTextValueToFile && this.referenceToTextSource is ITextSource referenceToTextSource)
        xw.WriteElementString("RefText", referenceToTextSource.Text);
    }
    if (this.text != null && this.prevCell == null)
      xw.WriteElementString("Text", this.text);
    if (this.charFormat != null && (!flag || this.CharFormatOverrided))
      this.charFormat.WriteToXml("Font", xw, objectRefId);
    if (this.paragraphFormat != null && (!flag || this.ParagraphFormatOverrided))
      this.paragraphFormat.WriteToXml("ParFmt", xw, objectRefId);
    if (this.textFormat != null && ((this.overrideFlags & OverrideFlags.TextFormat) != OverrideFlags.None || this.Template == null))
      xw.WriteElementString("TextFormat", this.textFormat);
    if (!this.useTextFormatForRefs || (this.overrideFlags3 & OverrideFlags3.UseTextFormatInRef) == OverrideFlags3.None && this.Template != null)
      return;
    xw.WriteElementString("UseTextFormatForRefs", "1");
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (TextData.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      TextData.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    switch (readArgs.Reader.LocalName)
    {
      case "CharFormat":
        TextData.ReadCharFormat((DocumentTreeNode) this, readArgs);
        return true;
      case "Font":
        TextData.ReadFont((DocumentTreeNode) this, readArgs);
        return true;
      case "ParFmt":
      case "ParagraphFormat":
        TextData.ReadParagraphFormat((DocumentTreeNode) this, readArgs);
        return true;
      case "PlaneTextFormat":
        PlaneTextFormat planeTextFormat = new PlaneTextFormat();
        planeTextFormat.ReadFromXml(readArgs);
        this.paragraphFormat = new ParagraphFormat(planeTextFormat);
        return true;
      case "RefText":
        TextData.ReadRefText((DocumentTreeNode) this, readArgs);
        return true;
      case "Reference":
        TextData.ReadReference((DocumentTreeNode) this, readArgs);
        return true;
      case "Text":
        TextData.ReadText((DocumentTreeNode) this, readArgs);
        return true;
      case "TextFormat":
        TextData.ReadTextFormat((DocumentTreeNode) this, readArgs);
        return true;
      case "avsSymbol":
      case "replaceAVSSpecSymbol":
        TextData.ReadReplaceAVSSpecSymbol((DocumentTreeNode) this, readArgs);
        return true;
      case "duplicateText":
        TextData.ReadDuplicateTextForAllPages((DocumentTreeNode) this, readArgs);
        return true;
      case "oldAVSChars":
      case "replaceOldAVSSpecChars":
        TextData.ReadReplaceOldAVSSpecChars((DocumentTreeNode) this, readArgs);
        return true;
      case "repeatAsHeader":
        TextData.ReadRepeatTextAsHeader((DocumentTreeNode) this, readArgs);
        return true;
      case "textAngle":
        TextData.ReadTextAngle((DocumentTreeNode) this, readArgs);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  private static void InitReadFieldDict()
  {
    TextData.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) RectangleElement.ReadFieldsDict);
    TextData.ReadFieldsDict.Add("Text", new ReadFieldFromXmlDelegate(TextData.ReadText));
    TextData.ReadFieldsDict.Add("RefText", new ReadFieldFromXmlDelegate(TextData.ReadRefText));
    TextData.ReadFieldsDict.Add("Font", new ReadFieldFromXmlDelegate(TextData.ReadFont));
    TextData.ReadFieldsDict.Add("CharFormat", new ReadFieldFromXmlDelegate(TextData.ReadCharFormat));
    TextData.ReadFieldsDict.Add("ParFmt", new ReadFieldFromXmlDelegate(TextData.ReadParagraphFormat));
    TextData.ReadFieldsDict.Add("ParagraphFormat", new ReadFieldFromXmlDelegate(TextData.ReadParagraphFormat));
    TextData.ReadFieldsDict.Add("Reference", new ReadFieldFromXmlDelegate(TextData.ReadReference));
    TextData.ReadFieldsDict.Add("textAngle", new ReadFieldFromXmlDelegate(TextData.ReadTextAngle));
    TextData.ReadFieldsDict.Add("replaceOldAVSSpecChars", new ReadFieldFromXmlDelegate(TextData.ReadReplaceOldAVSSpecChars));
    TextData.ReadFieldsDict.Add("oldAVSChars", new ReadFieldFromXmlDelegate(TextData.ReadReplaceOldAVSSpecChars));
    TextData.ReadFieldsDict.Add("replaceAVSSpecSymbol", new ReadFieldFromXmlDelegate(TextData.ReadReplaceAVSSpecSymbol));
    TextData.ReadFieldsDict.Add("avsSymbol", new ReadFieldFromXmlDelegate(TextData.ReadReplaceAVSSpecSymbol));
    TextData.ReadFieldsDict.Add("duplicateText", new ReadFieldFromXmlDelegate(TextData.ReadDuplicateTextForAllPages));
    TextData.ReadFieldsDict.Add("repeatAsHeader", new ReadFieldFromXmlDelegate(TextData.ReadRepeatTextAsHeader));
    TextData.ReadFieldsDict.Add("TextFormat", new ReadFieldFromXmlDelegate(TextData.ReadTextFormat));
    TextData.ReadFieldsDict.Add("UseTextFormatForRefs", new ReadFieldFromXmlDelegate(TextData.ReadUseTextFormatForRefs));
  }

  private static void ReadTextFormat(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Reader.IsEmptyElement)
      return;
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    ((TextData) docNode).textFormat = readArgs.Reader.Value;
    docNode.overrideFlags |= OverrideFlags.TextFormat;
  }

  private static void ReadUseTextFormatForRefs(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Reader.IsEmptyElement)
      return;
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    ((TextData) docNode).useTextFormatForRefs = readArgs.Reader.Value == "1";
    docNode.overrideFlags3 |= OverrideFlags3.UseTextFormatInRef;
  }

  private static void ReadTextAngle(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextData) docNode).orientation = (TextOrientation) Enum.Parse(typeof (TextOrientation), readArgs.Reader.Value);
  }

  private static void ReadReplaceOldAVSSpecChars(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextData) docNode).replaceOldAVSSpecChars = readArgs.Reader.Value == "1";
    ((TextData) docNode).overrideFlags3 |= OverrideFlags3.ReplaceOldAVSSpecChars;
  }

  private static void ReadReplaceAVSSpecSymbol(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextData) docNode).AssignReplaceAVSMaterial(readArgs.Reader.Value == "1", true);
  }

  private static void ReadDuplicateTextForAllPages(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextData) docNode).duplicateTextForAllPages = readArgs.Reader.Value == "1";
  }

  private static void ReadRepeatTextAsHeader(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextData) docNode).repeatTextAsHeader = readArgs.Reader.Value == "1";
  }

  private static void ReadReference(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TextData textData = (TextData) docNode;
    textData.referenceToTextSource = ReferenceBase.LoadFromXml(readArgs);
    textData.overrideFlags2 |= OverrideFlags2.Reference;
    if (textData.page != null && textData.referenceToTextSource.IsDependOnDocument)
      textData.page.DocumentChanged += new DocumentChanged_EventHandler(textData.Page_DocumentChanged);
    if (readArgs.Version < 15 && textData.referenceToTextSource is UnknownReferenceToObject && textData.referenceToTextSource.UnknownXmlAttributes != null)
    {
      List<StringKeyValue> unknownXmlAttributes = textData.referenceToTextSource.UnknownXmlAttributes;
      for (int index = 0; index < unknownXmlAttributes.Count; ++index)
      {
        if (unknownXmlAttributes[index].Key == "ReferenceToTextData")
        {
          textData.referenceToTextSource = (ReferenceBase) null;
          break;
        }
      }
    }
    if (textData.referenceToTextSource == null)
      return;
    textData.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) textData);
    if (readArgs.Version >= 26 || !(textData.referenceToTextSource is ReferenceToDBObjectBase referenceToTextSource) || referenceToTextSource.ReferenceType != RefToDBObjectType.rtUseParentDocumentObjectLink)
      return;
    referenceToTextSource.PassiveLink = false;
  }

  private static void ReadParagraphFormat(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TextData textData = (TextData) docNode;
    textData.overrideFlags |= OverrideFlags.ParagraphFormat;
    textData.paragraphFormat = new ParagraphFormat();
    textData.paragraphFormat.ReadFromXml(readArgs);
  }

  private static void ReadCharFormat(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TextData textData = (TextData) docNode;
    textData.overrideFlags |= OverrideFlags.CharFormat;
    textData.charFormat = new CharFormat();
    textData.charFormat.ReadFromXml(readArgs);
  }

  private static void ReadFont(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TextData textData = (TextData) docNode;
    textData.overrideFlags |= OverrideFlags.CharFormat;
    if (readArgs.Version < 13)
    {
      Font font = FontXmlWrapper.ReadFromXml(readArgs);
      textData.charFormat = new CharFormat(font);
    }
    else
    {
      textData.charFormat = new CharFormat();
      textData.charFormat.ReadFromXml(readArgs);
    }
  }

  private static void ReadText(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TextData textData = (TextData) docNode;
    textData.text = "";
    if (!readArgs.Reader.IsEmptyElement)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      if (readArgs.Reader.NodeType == XmlNodeType.Text || readArgs.Reader.NodeType == XmlNodeType.Whitespace)
        textData.text = readArgs.Reader.Value;
    }
    if (textData.text != null)
      return;
    textData.text = "";
  }

  private static void ReadRefText(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    TextData textData = (TextData) docNode;
    ITextSource textSource = (ITextSource) null;
    if (textData.referenceToTextSource == null)
    {
      if (textData.Template is TextData template && template.referenceToTextSource != null)
      {
        textData.AssignReferenceToTextSource(template.referenceToTextSource.Clone(), false, false, false);
        textSource = textData.referenceToTextSource as ITextSource;
      }
      else
        textData.AssignReferenceToTextSource((ReferenceBase) null, false, false, false);
    }
    string str = "";
    if (!readArgs.Reader.IsEmptyElement)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      if (readArgs.Reader.NodeType == XmlNodeType.Text || readArgs.Reader.NodeType == XmlNodeType.Whitespace)
      {
        if (textSource != null)
          str = readArgs.Reader.Value;
        else
          textData.AssignText(readArgs.Reader.Value, false, true, false, false, false);
      }
    }
    if (textSource == null || textSource.Text != null)
      return;
    textSource.AssignText(str);
  }

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
    if (!(src is TextData textData))
      return;
    if (templateClone)
    {
      this.charFormat = textData.charFormat;
      this.paragraphFormat = textData.paragraphFormat;
    }
    else
    {
      if (textData.charFormat != null)
        this.charFormat = textData.charFormat.Clone();
      if (textData.paragraphFormat != null)
        this.paragraphFormat = textData.paragraphFormat.Clone();
    }
    this.text = !(!templateClone & copyData) ? (string) null : textData.text;
    this.orientation = textData.orientation;
    this.duplicateTextForAllPages = textData.duplicateTextForAllPages;
    this.repeatTextAsHeader = textData.repeatTextAsHeader;
    this.textFormat = textData.textFormat;
    this.useTextFormatForRefs = textData.useTextFormatForRefs;
    this.replaceOldAVSSpecChars = textData.replaceOldAVSSpecChars;
    this.AssignReplaceAVSMaterial(textData.ReplaceAVSMaterial, false);
    if (textData.referenceToTextSource != null & copyData)
    {
      this.referenceToTextSource = textData.referenceToTextSource.Clone();
      this.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) this);
      if (templateClone && textData.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource1 && referenceToTextSource1.ReferenceBaseType == BaseReferenceNodeType.ntSelectedNode && this.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource2)
        referenceToTextSource2.SetReference((DocumentTreeNode) null);
      if (this.page == null)
        return;
      if (this.referenceToTextSource.IsDependOnDocument)
        this.page.DocumentChanged += new DocumentChanged_EventHandler(this.Page_DocumentChanged);
      else
        this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
    else
    {
      if (this.referenceToTextSource == null)
        return;
      this.referenceToTextSource.DisconnectLink();
      this.referenceToTextSource = (ReferenceBase) null;
      if (this.page == null)
        return;
      this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected TextData(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback</summary>
  public override void OnDeserialization(object sender)
  {
    base.OnDeserialization(sender);
    if (this.referenceToTextSource == null)
      return;
    this.referenceToTextSource.AssignOwnerNode((DocumentTreeNode) this);
    if (!(this.referenceToTextSource is ReferenceToNode referenceToTextSource))
      return;
    referenceToTextSource.UpdateLink(false, false);
  }

  /// <summary>Восстановить сохраненные ссылки</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  public override void RestoreLinks(
    bool copyChildren,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    if (this.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource1 && referenceToTextSource1.ReferenceBaseType == BaseReferenceNodeType.ntSelectedNode && referenceToTextSource1.NodeLink != null)
    {
      TextData link1 = (TextData) links[(object) this];
      if (link1 != null)
      {
        DocumentTreeNode link2 = (DocumentTreeNode) links[(object) referenceToTextSource1.NodeLink];
        if (link1.referenceToTextSource is ReferenceToNodeAttributeBase referenceToTextSource)
          referenceToTextSource.SetReference(link2);
      }
    }
    base.RestoreLinks(copyChildren, templateClone, externalLink, links);
  }

  /// <summary>Метод вызывается при удалении ветки, в которой находится этот узел</summary>
  protected override void OnBranchRemoved(Removed_EventArgs e)
  {
    if (!this.IsVirtualNode && !e.RemovedByShift && this.referenceToTextSource != null)
      this.referenceToTextSource.DisconnectLink();
    base.OnBranchRemoved(e);
  }

  /// <summary>Присвоить значение свойству Page</summary>
  /// <param name="value">Новое значение Page</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void AssignPage(PageData value, bool updateUI, bool updateLayout)
  {
    if (this.page == value)
      return;
    ImDocumentData imDocumentData = (ImDocumentData) null;
    if (this.page != null)
    {
      imDocumentData = this.page.OwnerDocument;
      if (this.referenceToTextSource != null && this.referenceToTextSource.IsDependOnDocument)
        this.page.DocumentChanged -= new DocumentChanged_EventHandler(this.Page_DocumentChanged);
    }
    base.AssignPage(value, updateUI, updateLayout);
    if (this.isVirtualNode || this.referenceToTextSource == null)
      return;
    if (this.referenceToTextSource.IsDependOnPage || this.referenceToTextSource.IsDependOnDocument && this.page != null && imDocumentData != this.page.OwnerDocument)
      this.referenceToTextSource.UpdateLink(updateUI, updateLayout);
    if (this.page == null || !this.referenceToTextSource.IsDependOnDocument)
      return;
    this.page.DocumentChanged += new DocumentChanged_EventHandler(this.Page_DocumentChanged);
  }

  /// <summary>Обработчик события DocumentChanged</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Page_DocumentChanged(object sender, DocumentChanged_EventArgs e)
  {
    if (!this.referenceToTextSource.IsDependOnDocument)
      return;
    this.referenceToTextSource.UpdateLink(false, false);
  }
}
