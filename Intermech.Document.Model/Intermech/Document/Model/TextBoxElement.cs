// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TextBoxElement
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

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
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Элемент редактирования текста.
/// Поддерживает форматированный текст (на данный момент не полностью).</summary>
[Serializable]
public class TextBoxElement : TextData, IPageElementWithInterface
{
  public static readonly float MaxTextHeight = 10000f;
  /// <summary>Имя типа элемента</summary>
  public static string ElementTypeName = LocalizationHolder.rm.GetString("Document.Model_511");
  [NonSerialized]
  private CancelEventHandler inplaceEditorActivating;
  [NonSerialized]
  private EventHandler inplaceEditorActivated;
  [NonSerialized]
  private CancelEventHandler inplaceEditorDeactivating;
  [NonSerialized]
  private EventHandler inplaceEditorDeactivated;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private AutoSizeDirection autoSize;
  private bool fontAutoSize;
  protected float textHeight;
  protected float textWidth;
  [NonSerialized]
  private RtfInSiteEditorWrapper textBox;
  private string rtf;
  private int protectedFirstCharCount;
  private int protectedEndCharCount;
  [NonSerialized]
  private PageElementUI pageUI;
  /// <summary>Первая позиция в тексте с которой отображает следующий элемент в цепочке</summary>
  [ExternalLink]
  internal int nextCellCharPos = -1;

  /// <summary>Инициализировать поля</summary>
  protected override void InitFields() => base.InitFields();

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  public TextBoxElement(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
  }

  public TextBoxElement(RectangleElement source)
  {
    IDictionary links = (IDictionary) new HybridDictionary();
    base.CopyFields((DocumentTreeNode) source, false, true, true, false, true, links);
    this.OnDeserialization((object) this);
    this.RestoreLinks(true, false, true, links);
  }

  /// <summary>Конструктор</summary>
  public TextBoxElement()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public TextBoxElement(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new TextBoxElement(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new TextBoxElement();

  static TextBoxElement() => TextBoxElement.InitReadFieldDict();

  /// <summary>Цвет переднего плана</summary>
  public override Color ForeColor
  {
    get => base.ForeColor;
    set
    {
      if (!(this.ForeColor != value))
        return;
      base.ForeColor = value;
      if (this.textBox != null)
        this.textBox.FormatEditorText();
      else
        this.InvalidateUI(true);
    }
  }

  /// <summary>Цвет фона</summary>
  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      if (!(this.BackColor != value))
        return;
      base.BackColor = value;
      if (this.textBox != null)
        this.textBox.FormatEditorText();
      else
        this.InvalidateUI(true);
    }
  }

  /// <summary>Отформатированный текст в формате RTF</summary>
  [Category("Debug")]
  public virtual string Rtf
  {
    [DebuggerStepThrough] get
    {
      if (!(this.FindFirstCell() is TextBoxElement textBoxElement))
        textBoxElement = this;
      if (textBoxElement.text != null && textBoxElement.referenceToTextSource != null && textBoxElement.Text != textBoxElement.text)
      {
        textBoxElement.text = (string) null;
        textBoxElement.rtf = (string) null;
      }
      return textBoxElement.rtf;
    }
    set => this.SetRtfText(value, true, true);
  }

  public void SetRtfText(string value, bool updateLayout, bool updateUI)
  {
    this.rtf = value;
    if (!updateLayout)
      return;
    this.UpdateLayout(updateUI);
  }

  /// <summary>Количество заблокированных для редактирования первых символов.
  /// Назначается внешними программами, например AVS. В XML не сохраняется.</summary>
  [Browsable(false)]
  public virtual int ProtectedFirstCharCount
  {
    [DebuggerStepThrough] get
    {
      int startCharIndex = this.StartCharIndex;
      if (startCharIndex == -1)
        return this.protectedFirstCharCount;
      if (!(this.FindFirstCell() is TextBoxElement textBoxElement))
        textBoxElement = this;
      return textBoxElement.NormalizedProtectedFirstCharCount - startCharIndex;
    }
  }

  [Browsable(false)]
  public int NormalizedProtectedFirstCharCount
  {
    get => this.protectedFirstCharCount <= 0 ? 0 : this.protectedFirstCharCount;
  }

  public void AssignProtectedZone(int protectedFirstCharCountValue, int protectedEndCharCountValue)
  {
    this.AssignProtectedFirstCharCount(protectedFirstCharCountValue);
    this.AssignProtectedEndCharCount(protectedEndCharCountValue);
  }

  /// <summary>Установить значение ProtectedFirstCharCount</summary>
  public virtual void AssignProtectedFirstCharCount(int value)
  {
    if (value == this.protectedFirstCharCount)
      return;
    this.protectedFirstCharCount = value;
    if (this.textBox == null)
      return;
    this.textBox.SetProtectedFirstCharCount(value);
  }

  /// <summary>Количество заблокированных для редактирования последних символов.
  /// Назначается внешними программами, например AVS. В XML не сохраняется.</summary>
  [Browsable(false)]
  public virtual int ProtectedEndCharCount
  {
    [DebuggerStepThrough] get
    {
      int startCharIndex = this.StartCharIndex;
      if (startCharIndex == -1)
        return this.protectedEndCharCount;
      if (!(this.FindFirstCell() is TextBoxElement textBoxElement))
        textBoxElement = this;
      return textBoxElement.protectedEndCharCount - startCharIndex;
    }
  }

  /// <summary>Установить значение ProtectedEndCharCount</summary>
  public virtual void AssignProtectedEndCharCount(int value)
  {
    if (value == this.protectedEndCharCount)
      return;
    this.protectedEndCharCount = value;
    if (this.textBox == null)
      return;
    this.textBox.SetProtectedEndCharCount(value);
  }

  /// <summary>В ячейке ничего не отображается</summary>
  public override bool IsEmptyText
  {
    get
    {
      if (this.prevCell == null)
        return string.IsNullOrEmpty(this.Text);
      if (this.StartCharIndex != -1)
        return false;
      return !this.repeatTextAsHeader || string.IsNullOrEmpty(this.Text);
    }
  }

  public override string Text
  {
    get => base.Text;
    set
    {
      this.rtf = (string) null;
      if (this.InPlaceEditorActive && this.textBox != null)
      {
        this.textBox.Invalidate();
        if (this.textBox.EditorActive)
          this.textBox.EditorText = value;
      }
      base.Text = value;
    }
  }

  /// <summary>Получить полный текст цепочки с учётом редактируемого в ячейке</summary>
  /// <param name="planeText">Текст без форматирования</param>
  /// <param name="rtfText">Текст в формате RTF</param>
  /// <param name="onlyRtfIfExist">Получать только RTF, если он есть</param>
  public void GetActualText(out string planeText, out string rtfText, bool onlyRtfIfExist)
  {
    planeText = (string) null;
    rtfText = (string) null;
    if (!(this.FindFirstCell() is TextBoxElement textBoxElement1))
      textBoxElement1 = this;
    TextBoxElement textBoxElement2 = (TextBoxElement) null;
    TextBoxElement textBoxElement3 = textBoxElement1;
    while (textBoxElement3 != null && textBoxElement2 == null)
    {
      if (textBoxElement3.InPlaceEditorActive)
        textBoxElement2 = textBoxElement3;
      else
        textBoxElement3 = textBoxElement3.nextCell as TextBoxElement;
    }
    if (textBoxElement2 != null)
    {
      textBoxElement2.textBox.GetActualText(out planeText, out rtfText, onlyRtfIfExist);
    }
    else
    {
      if (!textBoxElement1.IsEmptyText)
        rtfText = textBoxElement1.Rtf;
      if (rtfText != null && onlyRtfIfExist)
        return;
      planeText = textBoxElement1.GetText();
    }
  }

  /// <summary>Возвращает текст в открытом редакторе, если редактор не активен возвращает null</summary>
  public string GetActiveEditorText()
  {
    string activeEditorText = (string) null;
    if (this.textBox != null && this.textBox.EditorActive)
      activeEditorText = this.textBox.EditorText;
    return activeEditorText;
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="value">Текст</param>
  /// <param name="fromOriginalText">Значение устанавливается от оригинальной ячейки дубликату текста</param>
  /// <param name="updateActiveEditor">Обновить текстовый редактор, если он активен</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то проверка стартует внутри</param>
  protected override void AssignText(
    string value,
    bool fromOriginalText,
    bool updateActiveEditor,
    bool saveUndo,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (!(value != this.Text))
      return;
    this.rtf = (string) null;
    base.AssignText(value, fromOriginalText, updateActiveEditor, saveUndo, updateUI, updateLayout, callChain);
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="plainText">Текст без форматирования</param>
  /// <param name="rtfText">RTF текст</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignText(
    string plainText,
    string rtfText,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    this.AssignText(plainText, rtfText, true, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="plainText">Текст без форматирования</param>
  /// <param name="rtfText">RTF текст</param>
  /// <param name="updateActiveEditor">Обновить текстовый редактор, если он активен</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignText(
    string plainText,
    string rtfText,
    bool updateActiveEditor,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    this.AssignText(plainText, rtfText, false, updateActiveEditor, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Присвоить новый текст</summary>
  /// <param name="plainText">Текст без форматирования</param>
  /// <param name="rtfText">RTF текст</param>
  /// <param name="fromOriginalText">Значение устанавливается от оригинальной ячейки дубликату текста</param>
  /// <param name="updateActiveEditor">Обновить текстовый редактор, если он активен</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignText(
    string plainText,
    string rtfText,
    bool fromOriginalText,
    bool updateActiveEditor,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (!fromOriginalText && this.duplicateTextForAllPages && (this.rtf != rtfText || plainText != this.Text))
    {
      if (this.GetFirstTextByTemplate() is TextBoxElement firstTextByTemplate && firstTextByTemplate != this)
      {
        firstTextByTemplate.AssignText(plainText, rtfText, false, updateActiveEditor, saveUndo, updateUI, updateLayout);
        return;
      }
      List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
      if (this.OwnerDocument != null && this.Template != null)
        this.OwnerDocument.FindNodesFromTemplate(this.Template, foundNodes);
      for (int index = 0; index < foundNodes.Count; ++index)
      {
        if (foundNodes[index] != this && foundNodes[index] is TextBoxElement textBoxElement)
        {
          for (PageData pageData = textBoxElement.Page; pageData != null; pageData = pageData.PrevPage)
          {
            if (pageData == this.page)
              textBoxElement.AssignText(plainText, rtfText, true, updateActiveEditor, saveUndo, false, false);
          }
        }
      }
    }
    if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null && (plainText != this.Text || this.rtf != rtfText))
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_579"));
    try
    {
      if (plainText != this.Text)
      {
        if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, "Rtf", (object) this.Rtf, (object) rtfText);
        this.rtf = (string) null;
        this.AssignText(plainText, fromOriginalText, updateActiveEditor, saveUndo, false, false);
        if (this.Text != plainText)
          rtfText = (string) null;
        this.rtf = rtfText;
        if (!updateLayout || !this.needUpdateLayoutFlag)
          return;
        this.UpdateLayout(updateUI);
      }
      else
      {
        if (!(this.rtf != rtfText))
          return;
        if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, "Rtf", (object) this.Rtf, (object) rtfText);
        this.rtf = rtfText;
        this.OnTextChanged(new TextChanged_EventArgs(plainText, plainText, false, updateActiveEditor, false, updateUI, updateLayout));
        if (this.OwnerDocument != null)
          this.OwnerDocument.Modified = true;
        if (!updateUI)
          return;
        this.RefreshUI();
      }
    }
    finally
    {
      if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null && (plainText != this.Text || this.rtf != rtfText))
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Установить новое значение свойства DuplicateTextForAllPages</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetDuplicateTextForAllPages(bool value, bool updateUI, bool updateLayout)
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
  protected override bool UpdateDuplicatedText(bool updateUI, bool updateLayout)
  {
    if (this.duplicateTextForAllPages)
    {
      TextData firstTextByTemplate = this.GetFirstTextByTemplate();
      if (firstTextByTemplate != null && firstTextByTemplate != this)
      {
        if (firstTextByTemplate is TextBoxElement textBoxElement)
          this.AssignText(textBoxElement.Text, textBoxElement.rtf, true, true, false, updateUI, updateLayout);
        else
          this.AssignText(firstTextByTemplate.Text, (string) null, true, true, false, updateUI, updateLayout);
        return true;
      }
    }
    return false;
  }

  /// <summary>Ссылка на источник текста</summary>
  [Editor(typeof (ReferenceToTextSourceUIEditor), typeof (UITypeEditor))]
  public override ReferenceBase ReferenceToTextSource
  {
    get => base.ReferenceToTextSource;
    set => base.ReferenceToTextSource = value;
  }

  /// <summary>Установить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="value">Значение флага</param>
  /// <param name="setInPrevCell">Установить флаг и для предыдущих ячеек</param>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetNeedUpdateLayoutFlag(
    bool value,
    bool setInPrevCell,
    bool updateUI,
    bool updateLayout)
  {
    if (!(updateLayout & value) && this.needUpdateLayoutFlag == value)
      return;
    if (value && this.needUpdateLayoutFlag != value && this.textBox != null)
      this.textBox.Invalidate();
    TableData parentCell = this.ParentCell;
    if (setInPrevCell & value && this.prevCell != null)
      this.prevCell.SetNeedUpdateLayoutFlag(value, setInPrevCell, false, false);
    if (value && parentCell != null)
    {
      this.needUpdateLayoutFlag = value;
      parentCell.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout);
    }
    else if (value && this.autoSize != AutoSizeDirection.None && this.page != null)
    {
      this.needUpdateLayoutFlag = value;
      this.page.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout);
    }
    else
      base.SetNeedUpdateLayoutFlag(value, setInPrevCell, updateUI, updateLayout);
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (this.TextBox != null && this.InPlaceEditorActive && this.TextBox.EditorControl != null)
      this.TextBox.EditorControl.Refresh();
    if (this.SuspendedRefreshUIFlag || this.page == null)
      return;
    if (this.pageUI != null)
      this.RefreshUI(this.pageUI.Bounds);
    else
      this.page.RefreshUI();
  }

  /// <summary>Можно активировать редактирование по месту</summary>
  public override bool CanActivateInPlaceEditor
  {
    get
    {
      if (!base.CanActivateInPlaceEditor || this.IsEmptyText && (this.ReadOnlyNow || this.prevCell != null))
        return false;
      return !this.repeatTextAsHeader || this.prevCell == null || this.StartCharIndex != -1;
    }
  }

  /// <summary>Событие вызываемое изменением текста в TextBox (в контроле редактора RTF)</summary>
  private void TextBox_TextChanged(object sender, EventArgs e)
  {
    if (this.OwnerDocument != null && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo("Изменение текста");
    try
    {
      if (this.textBox == null || !this.textBox.EditorActive)
        return;
      string planeText = (string) null;
      string rtfText = (string) null;
      this.textBox.GetActualText(out planeText, out rtfText, false);
      if (rtfText == null)
        this.textBox.CheckCharFormat();
      if (!(this.FindFirstCell() is TextBoxElement textBoxElement))
        textBoxElement = this;
      textBoxElement.AssignText(planeText, rtfText, false, true, false, true, true);
    }
    finally
    {
      if (this.OwnerDocument != null && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Вызывает событие Текст изменен</summary>
  /// <param name="e">Данные события</param>
  public override void OnTextChanged(TextChanged_EventArgs e)
  {
    if (e.ClearRTF)
      this.rtf = (string) null;
    if (this.textBox != null)
    {
      this.textBox.Invalidate();
      if (e.UpdateActiveEditor && this.textBox.EditorActive)
      {
        if (this.rtf != null)
          this.textBox.EditorRtf = this.rtf;
        else
          this.textBox.EditorText = this.GetText();
      }
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
    }
    if (this.autoSize != AutoSizeDirection.None)
      this.SetNeedUpdateLayoutFlag(true, true, e.UpdateUI, e.UpdateLayout);
    base.OnTextChanged(e);
  }

  /// <summary>Возможен вызов дополнительного редактора для элемента</summary>
  public override bool CanCallEditor => base.CanCallEditor;

  /// <summary>Вызвать дополнительный редактор для элемента</summary>
  public override void CallEditor() => base.CallEditor();

  /// <summary>Обновить формулы в текстовых полях</summary>
  protected override void UpdateFormulasInTextBox()
  {
    if (this.IsEmptyText || this.textBox == null)
      return;
    this.textBox.Invalidate();
    this.SetNeedUpdateLayoutFlag(true, true, false, false);
  }

  /// <summary>Скопировать текст и форматирование в другой элемент</summary>
  /// <param name="destination">Элемент приёмник</param>
  public override void CopyTextAndFormatTo(TextData destination)
  {
    if (destination == null)
      throw new ArgumentNullException(nameof (destination));
    base.CopyTextAndFormatTo(destination);
    if (!(destination is TextBoxElement textBoxElement))
      return;
    textBoxElement.rtf = this.rtf;
  }

  /// <summary>Автоматически подбирать размер</summary>
  [CustomDisplayName("Attribute.Document.Model_143")]
  [CustomDescription("Attribute.Document.Model_144")]
  [CustomCategory("Attribute.Document.Model_145")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool AutoSizeHeight
  {
    [DebuggerStepThrough] get => this.autoSize == AutoSizeDirection.Height;
    set => this.AssignAutoSizeHeight(value, true, true, true);
  }

  /// <summary>Автоматически подбирать размер</summary>
  [Browsable(false)]
  private AutoSizeDirection AutoSize
  {
    [DebuggerStepThrough] get => this.autoSize;
    set => this.AssignAutoSize(value, true, true, true);
  }

  /// <summary>Назначить значение свойству AutoSizeHeight</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Устанавливать перекрывающий флаг</param>
  public void AssignAutoSizeHeight(
    bool value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.AutoSizeHeight == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "AutoSizeHeight", (object) this.AutoSizeHeight, (object) value);
    this.BeginChanges(false);
    this.autoSize = !value ? AutoSizeDirection.None : AutoSizeDirection.Height;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.AutoSize;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.EndChanges(false);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Автоматически подбирать размер</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_243")]
  [CustomDescription("Attribute.Document.Model_244")]
  [CustomCategory("Attribute.Document.Model_245")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(true)]
  public override bool AutoSizeWidth
  {
    [DebuggerStepThrough] get => this.autoSize == AutoSizeDirection.Width;
    set
    {
      if (value && this.autoSize != AutoSizeDirection.Width)
        this.AssignFontAutoSize(false, true, true, true);
      this.AssignAutoSizeWidth(value, true, true, true);
    }
  }

  /// <summary>Вписывать текст в размеры полей</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_248")]
  [CustomDescription("Attribute.Document.Model_249")]
  [CustomCategory("Attribute.Document.Model_245")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(true)]
  public override bool FontAutoSize
  {
    [DebuggerStepThrough] get => this.fontAutoSize;
    set
    {
      if (value == this.fontAutoSize)
        return;
      if (value && this.autoSize == AutoSizeDirection.Width)
        this.AssignAutoSizeWidth(false, true, true, true);
      this.AssignFontAutoSize(value, true, true, true);
    }
  }

  /// <summary>Задать новое значение свойству AutoSizeWidth</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Перекрывать наследование параметра по шаблону</param>
  public void AssignAutoSizeWidth(
    bool value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.AutoSizeWidth == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "AutoSizeWidth", (object) this.AutoSizeWidth, (object) value);
    this.autoSize = !value ? AutoSizeDirection.None : AutoSizeDirection.Width;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.AutoSize;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Задать новое значение свойству FontAutoSize</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Перекрывать наследование параметра по шаблону</param>
  public void AssignFontAutoSize(
    bool value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.FontAutoSize == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "FontAutoSize", (object) this.FontAutoSize, (object) value);
    this.fontAutoSize = value;
    if (setOverrideFlag)
      this.overrideFlags3 |= OverrideFlags3.UseFontAutoSize;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Задать новое значение свойству AutoSize</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Перекрывать наследование параметра по шаблону</param>
  public override void AssignAutoSize(
    AutoSizeDirection value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.autoSize == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "AutoSize", (object) this.AutoSize, (object) value);
    this.autoSize = value;
    if (this.fontAutoSize && value == AutoSizeDirection.Width)
    {
      this.fontAutoSize = false;
      if (setOverrideFlag)
        this.overrideFlags3 ^= OverrideFlags3.UseFontAutoSize;
    }
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.AutoSize;
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Получить максимальную высоту</summary>
  /// <returns>Максимальную высоту</returns>
  protected float GetMaximumHeight()
  {
    if ((double) this.maxHeight != 0.0)
      return this.maxHeight;
    return this.Page != null ? this.Page.Size.Height - this.Location.Y : float.MaxValue;
  }

  /// <summary>Высота содержимого ячейки</summary>
  public override float ContentHeight => Math.Max(this.textHeight, this.MinHeight);

  /// <summary>Для внутреннего использования. Необходимо обновить минимальный размер</summary>
  public override bool NeedUpdateMinHeight
  {
    get
    {
      return this.autoSize == AutoSizeDirection.Height && (double) this.textHeight == 0.0 && !DocumentTreeNode.IsEmptyString(this.Text);
    }
  }

  /// <summary>Максимальная высота</summary>
  [RefreshProperties(RefreshProperties.All)]
  public override float MaxHeight
  {
    [DebuggerStepThrough] get => this.maxHeight;
    set => this.AssignMaxHeight(value, true, true, true);
  }

  /// <summary>Задать новое значение свойству MaxHeight</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="setOverrideFlag">Перекрывать наследование параметра по шаблону</param>
  public override void AssignMaxHeight(
    float value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if ((double) this.MaxHeight == (double) value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "MaxHeight", (object) this.MaxHeight, (object) value);
    this.maxHeight = value;
    if (setOverrideFlag)
      this.SetOverrideFlags(OverrideFlags.MaxHeight);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Минимальная ширина ячейки</summary>
  public override float MinWidth
  {
    get
    {
      return this.autoSize == AutoSizeDirection.Width && (double) this.textWidth > (double) this.minWidth ? this.textWidth : this.minWidth;
    }
    set => base.MinWidth = value;
  }

  /// <summary>Для внутреннего использования. Необходимо обновить минимальный размер</summary>
  public override bool NeedUpdateMinWidth
  {
    get
    {
      return this.autoSize == AutoSizeDirection.Width && (double) this.textWidth == 0.0 && !DocumentTreeNode.IsEmptyString(this.Text);
    }
  }

  /// <summary>Вызывает разбивку по страницам</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void Distribute(DistributeContext context, bool updateUI)
  {
    context.NewSize = this.Size;
    SizeF size = this.Size;
    if (this.autoSize == AutoSizeDirection.Height)
      size.Height = (double) this.maxHeight == 0.0 ? TextBoxElement.MaxTextHeight : this.maxHeight;
    else if (this.autoSize == AutoSizeDirection.Width)
      size.Width = 1000f;
    context.MaxSize = size;
    TableData topLevelTable = this.TopLevelTable;
    context.CanDistributeTopTable = topLevelTable != null && topLevelTable.CanVerticalDistribute();
    this.DistributeCell(context);
    base.Distribute(context, updateUI);
  }

  /// <summary>Обновить представление данных</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void UpdateLayout(bool updateUI)
  {
    if (this.IsVirtualNode || this.SuspendedUpdateLayoutFlag)
      return;
    if (this.needUpdateLayoutFlag)
    {
      TableData parentCell = this.ParentCell;
      if (parentCell != null)
        parentCell.UpdateLayout(updateUI);
      else if (this.page != null && (this.autoSize != AutoSizeDirection.None || this.fontAutoSize || this.HorzAlign != ElementHorizontalAlign.None))
      {
        this.page.UpdateLayout(updateUI);
      }
      else
      {
        if (this.autoSize != AutoSizeDirection.None || this.fontAutoSize)
          this.Distribute(new DistributeContext(), updateUI);
        base.UpdateLayout(false);
        if (!updateUI)
          return;
        if (this.needUpdateUIGeometry)
          this.UpdateUIGeometry(true);
        else
          this.RefreshUI();
      }
    }
    else
    {
      if (!updateUI)
        return;
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
  }

  /// <summary>Можно ли распределять данные по страницам</summary>
  /// <returns></returns>
  public override bool CanSplitData()
  {
    return this.orientation == TextOrientation.Normal && !this.duplicateTextForAllPages;
  }

  /// <summary>Поток пустой</summary>
  public override bool AllFlowsIsEmpty()
  {
    return this.prevCell != null && (this.IsEmptyText || this.repeatTextAsHeader && this.StartCharIndex == -1);
  }

  /// <summary>Распределить данные по ячейке представления</summary>
  /// <param name="context">Контекст разбивки</param>
  public override void DistributeCell(DistributeContext context)
  {
    context.VertDistributed = DistributeResult.All;
    context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
    context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
    context.TryNotBreak |= this.TryNotBreak;
    bool flag = context.IsFixedSizeRow_NN && (double) context.RowSize_NN != 0.0;
    TableData parentCell = this.ParentCell;
    if (this.autoSize == AutoSizeDirection.None)
    {
      this.textHeight = 0.0f;
      if (this.IsTableCell)
      {
        if ((double) this.minHeight > 0.0)
          context.NewSize.Height = this.minHeight;
        if (flag)
          context.NewSize.Height = this.RoundForFixedSizeRow(context.NewSize.Height, context.RowSize_NN, this.minHeight);
      }
      this.AssignBounds(this.Location, context.NewSize, false, false, false);
      SizeF size = this.Size;
      if (this.CanSplitData())
        this.DistributeText(context, false, false);
      else
        this.nextCellCharPos = -1;
      if (this.nextCellCharPos != -1 && this.CanSplitData())
        context.VertDistributed = DistributeResult.Part;
      else if ((double) size.Height > (double) context.MaxSize.Height || (double) size.Width > (double) context.MaxSize.Width)
        context.VertDistributed = DistributeResult.None;
    }
    else if (this.autoSize == AutoSizeDirection.Height)
    {
      this.textWidth = 0.0f;
      SizeF size;
      if (!this.InPlaceEditorActive && this.IsEmptyText)
      {
        this.nextCellCharPos = -1;
        this.textHeight = 0.0f;
        if ((double) this.minHeight > 0.0 && (double) context.NewSize.Height < (double) this.minHeight)
          context.NewSize.Height = this.minHeight;
        RectangleF properBounds = this.ProperBounds;
        if (flag)
          properBounds.Height = context.RowSize_NN;
        if ((double) this.minHeight > 0.0 && (double) properBounds.Height < (double) this.minHeight)
          properBounds.Height = this.minHeight;
        properBounds.Width = context.NewSize.Width;
        properBounds.Height = context.NewSize.Height;
        this.AssignProperBounds(properBounds.Location, properBounds.Size, false, false, false);
        size = this.Size;
      }
      else
      {
        context.NewSize.Height = this.CanSplitData() ? context.MaxSize.Height : TextBoxElement.MaxTextHeight;
        if ((double) context.NewSize.Height > (double) TextBoxElement.MaxTextHeight)
          context.NewSize.Height = TextBoxElement.MaxTextHeight;
        this.DistributeText(context, false, false);
        RectangleF properBounds = this.ProperBounds;
        size = properBounds.Size;
        float rowSize = size.Height;
        if ((double) this.minHeight > 0.0 && (double) rowSize < (double) this.minHeight)
          rowSize = this.minHeight;
        if (flag)
        {
          size.Height = rowSize;
          rowSize = this.RoundForFixedSizeRow(rowSize, context.RowSize_NN, this.minHeight);
        }
        if ((double) this.minHeight > 0.0 && (double) rowSize < (double) this.minHeight)
          rowSize = this.minHeight;
        this.textHeight = rowSize;
        if ((double) properBounds.Height != (double) rowSize)
        {
          properBounds.Height = rowSize;
          size.Height = rowSize;
          this.AssignProperBounds(properBounds.Location, properBounds.Size, false, false, false);
        }
        if ((double) this.properBounds.Height != (double) context.RowSize_NN && (double) context.RowSize_NN != 0.0)
        {
          this.SetOverrideFlags(OverrideFlags.Height);
          this.SetOverrideFlags2(OverrideFlags2.RowHeight);
          if (parentCell != null)
          {
            parentCell.SetOverrideFlags(OverrideFlags.Height);
            parentCell.SetOverrideFlags2(OverrideFlags2.RowHeight);
          }
        }
      }
      if (VisualNode.MoreWithMiscalculation(size.Height, context.MaxSize.Height) || VisualNode.MoreWithMiscalculation(size.Width, context.MaxSize.Width))
        context.VertDistributed = DistributeResult.None;
      else if (this.nextCellCharPos != -1 && this.CanSplitData())
        context.VertDistributed = DistributeResult.Part;
    }
    else if (this.autoSize == AutoSizeDirection.Width)
    {
      this.textHeight = 0.0f;
      if (this.IsTableCell)
      {
        if ((double) this.minHeight > 0.0)
          context.NewSize.Height = this.minHeight;
        if (flag)
          context.NewSize.Height = this.RoundForFixedSizeRow(context.NewSize.Height, context.RowSize_NN, this.minHeight);
      }
      SizeF size;
      if (!this.InPlaceEditorActive && DocumentTreeNode.IsEmptyString(this.Text))
      {
        this.textWidth = 0.0f;
        RectangleF properBounds = this.ProperBounds with
        {
          Width = Math.Max(this.minWidth, RectangleElement.MinimalSize.Width)
        };
        this.AssignProperBounds(properBounds.Location, properBounds.Size, false, false, false);
        size = this.Size;
      }
      else
      {
        context.NewSize.Width = (double) context.MaxSize.Width >= (double) TextBoxElement.MaxTextHeight ? TextBoxElement.MaxTextHeight : context.MaxSize.Width * 2f;
        if ((double) context.NewSize.Width > (double) TextBoxElement.MaxTextHeight)
          context.NewSize.Width = TextBoxElement.MaxTextHeight;
        this.DistributeText(context, false, false);
        RectangleF properBounds = this.ProperBounds;
        size = properBounds.Size;
        float num1 = size.Width;
        float num2 = Math.Max(this.minWidth, RectangleElement.MinimalSize.Width);
        if ((double) num1 < (double) num2)
          num1 = num2;
        this.textWidth = num1;
        if ((double) properBounds.Width != (double) num1)
        {
          properBounds.Width = num1;
          size.Width = num1;
          this.AssignProperBounds(properBounds, false, false, false);
        }
      }
      if (VisualNode.MoreWithMiscalculation(size.Height, context.MaxSize.Height) || VisualNode.MoreWithMiscalculation(size.Width, context.MaxSize.Width))
        context.VertDistributed = DistributeResult.None;
      else if (this.nextCellCharPos != -1 && this.CanSplitData())
        context.VertDistributed = DistributeResult.Part;
    }
    if (context.FirstDataOnPage && context.VertDistributed == DistributeResult.None)
      context.VertDistributed = DistributeResult.All;
    this.AssignNeedUpdateLayoutFlag(context.DistributeResultIsNeedUpdateLayout);
  }

  /// <summary>Разбить текст</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void DistributeText(DistributeContext context, bool updateUI, bool updateLayout)
  {
    try
    {
      if (this.autoSize == AutoSizeDirection.None)
      {
        this.nextCellCharPos = -1;
      }
      else
      {
        if (this.textBox == null)
          this.CreateInSiteEditorWrapper((DrawContext) null, this.properBounds);
        SizeF sizeF1 = this.CalcClientSize(this.CalcProperSize(context.NewSize));
        int nextCellCharPos = -1;
        SizeF sizeF2 = this.textBox.Distribute(this.textBox.GetDistributeBuffer(), this.autoSize, sizeF1, out nextCellCharPos);
        if (this.StartCharIndex == -1 && this.prevCell != null)
          nextCellCharPos = -1;
        if (this.nextCellCharPos != nextCellCharPos)
        {
          this.nextCellCharPos = nextCellCharPos;
          if (this.nextCell != null)
            this.nextCell.ResetTextBoxPaintCache();
        }
        if (this.nextCellCharPos == -1 && this.nextCell != null)
        {
          for (TextBoxElement nextCell = this.nextCell as TextBoxElement; nextCell != null; nextCell = nextCell.nextCell as TextBoxElement)
            nextCell.nextCellCharPos = -1;
        }
        if (this.autoSize == AutoSizeDirection.Height)
          sizeF1.Height = sizeF2.Height;
        else if (this.autoSize == AutoSizeDirection.Width)
          sizeF1.Width = sizeF2.Width;
        this.AssignProperBounds(this.ProperLocation, UnitsConverter.RoundSize(this.CalcSizeFromClientSize(sizeF1), 5), false, updateUI, updateLayout);
      }
    }
    catch (Exception ex)
    {
      ImDocumentData.ShowException(ex, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      LogManager.AddLine(ex.Message + Environment.NewLine + ex.StackTrace, true);
    }
  }

  /// <summary>Только для внутреннего использования. Получить минимальный неделимый размер для разбивки</summary>
  /// <note>Используется для определения свободного пространства в только что созданной для переноса таблице</note>
  public override float GetMinimalSizeForDistribute(DistributeContext context)
  {
    float rowSize = 0.0f;
    bool flag = context.IsFixedSizeRow_NN && (double) context.RowSize_NN != 0.0;
    TableData parentCell = this.ParentCell;
    if (!this.AutoSizeHeight)
    {
      if ((double) this.minHeight > 0.0)
        rowSize = this.minHeight;
      if (flag)
      {
        if ((double) context.RowSize_NN > (double) context.MaxSize.Height)
          return context.RowSize_NN;
        rowSize = this.RoundForFixedSizeRow(rowSize, context.RowSize_NN, this.minHeight);
      }
    }
    else if (!this.InPlaceEditorActive && DocumentTreeNode.IsEmptyString(this.Text) || this.CanSplitData())
    {
      if ((double) this.minHeight > 0.0)
        rowSize = this.minHeight;
      this.setBounds(this.CalcBoundsFromProper(this.ProperBounds));
      if (flag && (double) context.RowSize_NN > (double) rowSize)
        rowSize = context.RowSize_NN;
    }
    else
    {
      if (flag && (double) context.RowSize_NN > (double) context.MaxSize.Height)
        return context.RowSize_NN;
      if (!flag && (double) this.minHeight > (double) context.MaxSize.Height)
        return this.minHeight;
      if (this.needUpdateLayoutFlag)
      {
        SizeF newSize = context.NewSize;
        context.NewSize = new SizeF(this.ProperSize.Width, 2.0 * (double) context.MaxSize.Height < (double) TextBoxElement.MaxTextHeight ? 2f * context.MaxSize.Height : TextBoxElement.MaxTextHeight);
        this.DistributeText(context, false, false);
        context.NewSize = newSize;
        this.needUpdateLayoutFlag = true;
      }
      rowSize = this.Size.Height;
      if ((double) this.minHeight > 0.0 && (double) rowSize < (double) this.minHeight)
        rowSize = this.minHeight;
      if (flag)
        rowSize = this.RoundForFixedSizeRow(rowSize, context.RowSize_NN, this.minHeight);
      if ((double) this.minHeight > 0.0 && (double) rowSize < (double) this.minHeight)
        rowSize = this.minHeight;
    }
    return rowSize;
  }

  /// <summary>Текст в поле может разбиваться по страницам</summary>
  protected override bool IsDistributedText => true;

  /// <summary>Отмерить текст</summary>
  /// <param name="text">Распределяемый текст</param>
  /// <param name="isRTF">Текст в формате RTF</param>
  /// <param name="firstTextPos">Начало разбитого текста для этой ячейки</param>
  /// <param name="maxSize">Максимальный размер текста в ячейке</param>
  /// <param name="nextCellCharPos">Возвращает позицию с которой стартует текст в следующей ячейке</param>
  /// <returns></returns>
  public virtual SizeF MeasureText(
    string text,
    bool isRTF,
    int firstTextPos,
    SizeF maxSize,
    out int nextCellCharPos)
  {
    if (this.textBox == null)
      this.CreateInSiteEditorWrapper((DrawContext) null, this.properBounds);
    SizeF maxSize1 = this.CalcClientSize(this.CalcProperSize(maxSize));
    maxSize = this.CalcSizeFromClientSize(this.textBox.Distribute(this.autoSize, text, isRTF, firstTextPos, maxSize1, out nextCellCharPos));
    return maxSize;
  }

  /// <summary>Установить значение textWidth</summary>
  /// <param name="textWidth">Значение</param>
  internal void AssignTextWidth(float textWidth) => this.textWidth = textWidth;

  /// <summary>Позиция с которой начинается распределённый текст в этой ячейке</summary>
  [Category("Debug")]
  public int StartCharIndex
  {
    [DebuggerStepThrough] get
    {
      int startCharIndex = -1;
      if (this.prevCell is TextBoxElement prevCell)
        startCharIndex = prevCell.nextCellCharPos;
      return startCharIndex;
    }
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(DocumentTreeNode child) => false;

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(System.Type type) => false;

  /// <summary>Герерирует событие Removed</summary>
  protected override void OnRemoved(Removed_EventArgs e)
  {
    if (this.textBox != null && !e.RemovedByShift)
      this.textBox.DeactivateEditor();
    base.OnRemoved(e);
  }

  protected override void OnBranchRemoved(Removed_EventArgs e)
  {
    if (this.InPlaceEditorActive)
      this.DeactivateInPlaceEditor();
    base.OnBranchRemoved(e);
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

  /// <summary>Пользователь не может редактировать данные элемента</summary>
  public override bool ReadOnly
  {
    get
    {
      return this.referenceToTextSource is ReferenceToDBObjectBase referenceToTextSource && !referenceToTextSource.PassiveLink && !string.IsNullOrWhiteSpace(this.textFormat) && this.useTextFormatForRefs && !string.IsNullOrWhiteSpace(this.GetText()) || base.ReadOnly;
    }
    set
    {
      if (this.ReadOnly == value)
        return;
      base.ReadOnly = value;
      if (this.OwnerDocument is ImDocument ownerDocument)
        ownerDocument.UpdateFormatCommands();
      if (this.textBox == null || !this.InPlaceEditorActive)
        return;
      this.textBox.SetReadOnly(this.ReadOnlyNow);
    }
  }

  /// <summary>Отображать фокус элемента</summary>
  public override bool ShowFocused
  {
    get => this.pageUI != null ? this.pageUI.IsActiveElement : base.ShowFocused;
  }

  /// <summary>Показывать на экране, что узел выбран</summary>
  public override bool ShowSelected
  {
    get => this.pageUI != null ? this.pageUI.IsSelected : base.ShowSelected;
  }

  /// <summary>Установить значение свойства CharFormat</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetCharFormat(CharFormat value, bool updateUI, bool updateLayout)
  {
    if (this.CharFormat == value)
      return;
    if (this.textBox != null)
      this.textBox.Invalidate();
    base.SetCharFormat(value, updateUI, updateLayout);
    if (this.InPlaceEditorActive)
    {
      RtfInSiteEditorWrapper textBox = this.textBox;
    }
    ImDocument ownerDocument = this.OwnerDocument as ImDocument;
    if (!updateUI || ownerDocument == null || ownerDocument.DocumentControl == null)
      return;
    ownerDocument.DocumentControl.UpdateFormatCommands();
  }

  /// <summary>Назначить значение Orientation</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetOrientation(TextOrientation value, bool updateUI, bool updateLayout)
  {
    if (this.Orientation == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Orientation", (object) this.Orientation, (object) value);
    if (this.textBox != null)
      this.textBox.Invalidate();
    int num = this.autoSize == AutoSizeDirection.None || this.orientation != TextOrientation.Normal && this.orientation != TextOrientation.UpsideDown || value != TextOrientation.DownTop && value != TextOrientation.TopDown ? (value == TextOrientation.Normal || value == TextOrientation.UpsideDown ? (this.orientation == TextOrientation.DownTop ? 1 : (this.orientation == TextOrientation.TopDown ? 1 : 0)) : 0) : 1;
    this.orientation = value;
    if (num != 0)
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    if (((num == 0 ? 1 : (!updateLayout ? 1 : 0)) & (updateUI ? 1 : 0)) != 0)
    {
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
    this.OnChanged(new Changed_EventArgs());
    if (this.InPlaceEditorActive && this.textBox != null)
      this.textBox.SetTextOrientation(this.textBox.Editor, this.Orientation, true);
    ImDocument ownerDocument = this.OwnerDocument as ImDocument;
    if (!updateUI || ownerDocument == null || ownerDocument.DocumentControl == null)
      return;
    ownerDocument.DocumentControl.UpdateFormatCommands();
  }

  /// <summary>Установить значение свойства ParagraphFormat</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="forceOverrideDefault">Перебивать настройку если она была взята по умолчанию и не хранится в элементе</param>
  public override void SetParagraphFormat(
    ParagraphFormat value,
    bool updateUI,
    bool updateLayout,
    bool forceOverrideDefault = false)
  {
    ParagraphFormat paragraphFormat = forceOverrideDefault ? this.paragraphFormat : this.ParagraphFormat;
    if (paragraphFormat == value || paragraphFormat != null && paragraphFormat.Equals(value))
      return;
    if (this.textBox != null)
      this.textBox.Invalidate();
    base.SetParagraphFormat(value, false, false, forceOverrideDefault);
    if (this.InPlaceEditorActive && this.textBox != null)
      this.textBox.SetDefaultParagraphFormat();
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    ImDocument ownerDocument = this.OwnerDocument as ImDocument;
    if (updateUI && ownerDocument != null && ownerDocument.DocumentControl != null)
    {
      ownerDocument.DocumentControl.NeedUpdateToolbar = false;
      ownerDocument.DocumentControl.UpdateFormatCommands();
    }
    if (!updateLayout & updateUI)
    {
      if (this.needUpdateUIGeometry)
        this.UpdateUIGeometry(true);
      else
        this.RefreshUI();
    }
    if (ownerDocument == null || ownerDocument.DocumentControl == null)
      return;
    ownerDocument.DocumentControl.NeedUpdateToolbar = true;
  }

  /// <summary>Назначить новое значение DefaultRowSize</summary>
  /// <param name="value">Значение</param>
  /// <param name="recursive">Рекурсивно назначить дочерним элементам</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetDefaultRowSize(
    float value,
    bool recursive,
    bool setOverrideFlag,
    bool updateUI,
    bool updateLayout)
  {
    if ((double) this.DefaultRowSize == (double) value)
      return;
    base.SetDefaultRowSize(value, recursive, setOverrideFlag, updateUI, updateLayout);
    if (!this.InPlaceEditorActive || this.textBox == null || !this.IsFixedSizeRows)
      return;
    float defaultRowSize = this.DefaultRowSize;
    if ((double) defaultRowSize > 0.0)
      this.textBox.SetAllRowSize(defaultRowSize, true);
    else
      this.textBox.SetDefaultParagraphFormat();
  }

  /// <summary>Контейнер для управления размерами и положением прямоугольного
  /// элемента управления</summary>
  [Browsable(false)]
  [Category("Debug")]
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

  /// <summary>Создать элемент типа LabelElement, перенести туда все данные,
  /// и заменить этот элемент на новый</summary>
  public virtual void ConvertToLabel()
  {
    LabelElement child = new LabelElement((RectangleElement) this);
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

  /// <summary>Наименование типа</summary>
  [TypeConverter(typeof (NodeTypeCaptionConverter))]
  [System.ComponentModel.ReadOnly(false)]
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => TextBoxElement.ElementTypeName;
    set
    {
      DocumentMenuHelper.ConvertToElement(new DocumentTreeNode[1]
      {
        (DocumentTreeNode) this
      }, value);
    }
  }

  /// <summary>Получить область для вывода текста</summary>
  /// <param name="bounds">Границы элемента</param>
  /// <returns>Область вывода текста</returns>
  protected RectangleF GetTextLayoutArea(RectangleF bounds)
  {
    bounds.X += this.BorderWidth;
    bounds.Width -= 2f * this.BorderWidth;
    bounds.Y += this.BorderWidth;
    bounds.Height -= 2f * this.BorderWidth;
    return bounds;
  }

  /// <summary>Область вывода текста</summary>
  [Browsable(false)]
  public RectangleF TextLayoutArea
  {
    [DebuggerStepThrough] get => this.GetTextLayoutArea(this.Bounds);
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

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="pageUI">Элемент управления в контексте которого должен быть редактор</param>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  public void ActivateInPlaceEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
    if (!this.CanActivateInPlaceEditor)
      return;
    if (this.inplaceEditorActivating != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      if (this.OwnerDocument is ImDocument)
        (this.OwnerDocument as ImDocument).OnInplaceEditorActivating((object) this, cancelEventArgs);
      this.inplaceEditorActivating((object) this, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    if (this.TextBox == null)
    {
      this.CreateInSiteEditorWrapper((DrawContext) null, this.properBounds);
      this.needUpdateUIGeometry = true;
    }
    this.UpdateUIGeometry(false);
    this.textBox.ActivateEditor(pageUI, mouseEventArgs);
    this.ActivateInPlaceEditor();
    if (this.OwnerDocument is ImDocument)
      (this.OwnerDocument as ImDocument).OnInplaceEditorActivated((object) this, new EventArgs());
    if (this.inplaceEditorActivated == null)
      return;
    this.inplaceEditorActivated((object) this, new EventArgs());
  }

  /// <summary>Деактивировать редактор на месте</summary>
  public override void DeactivateInPlaceEditor()
  {
    if (!this.InPlaceEditorActive)
      return;
    if (this.inplaceEditorDeactivating != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      if (this.OwnerDocument is ImDocument)
        (this.OwnerDocument as ImDocument).OnInplaceEditorDeactivating((object) this, cancelEventArgs);
      this.inplaceEditorDeactivating((object) this, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    if (this.textBox != null)
      this.textBox.DeactivateEditor();
    base.DeactivateInPlaceEditor();
    if (this.OwnerDocument is ImDocument)
      (this.OwnerDocument as ImDocument).OnInplaceEditorDeactivated((object) this, new EventArgs());
    if (this.inplaceEditorDeactivated == null)
      return;
    this.inplaceEditorDeactivated((object) this, new EventArgs());
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

  /// <summary>Поле редактирования</summary>
  [Browsable(false)]
  [Category("Debug")]
  public RtfInSiteEditorWrapper TextBox
  {
    [DebuggerStepThrough] get => this.textBox;
    set
    {
      if (this.textBox == value)
        return;
      if (this.textBox != null)
      {
        this.textBox.DeactivateEditor();
        this.textBox.TextChanged -= new EventHandler(this.TextBox_TextChanged);
        this.textBox.SetOwner((TextData) null);
        this.textBox.Invalidate();
      }
      this.textBox = value;
      if (this.textBox == null)
        return;
      this.textBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    }
  }

  /// <summary>Создать врапер редактора по месту</summary>
  internal virtual void CreateInSiteEditorWrapper(DrawContext context, RectangleF propBounds)
  {
    this.TextBox = new RtfInSiteEditorWrapper((TextData) this);
    float scale = 1f;
    Matrix matrix = (Matrix) null;
    PointF dpi = new PointF(96f, 96f);
    MarginsF margins;
    float fixedRowSize;
    if (context != null)
    {
      scale = context.Scale;
      matrix = context.TransformMatrix.Matrix;
      dpi = context.DisplayDPI;
      margins = context.Margins;
      fixedRowSize = context.RowSize_NN;
    }
    else
    {
      if (this.Page is Intermech.Document.Model.Page page && page.PageUI != null)
      {
        matrix = page.PageUI.TransformMatrix.Matrix;
        dpi = page.PageUI.DispayDpi;
        if (page.PageControl != null)
          scale = page.PageControl.PageScale;
      }
      margins = this.Margins;
      fixedRowSize = this.DefaultRowSize;
    }
    if (matrix == null)
      return;
    Rectangle pixel = UnitsConverter.ConvertWorldToPixel(this.ClientBounds, matrix, dpi);
    Rectangle winClientBounds = pixel;
    ++winClientBounds.Width;
    ++winClientBounds.Height;
    this.textBox.SetBounds(propBounds, this.ClientBounds, margins, fixedRowSize, pixel, winClientBounds, this.orientation, scale, dpi, false);
  }

  /// <summary>Контрол редактора по месту</summary>
  [Browsable(false)]
  public Control InPlaceEditorControl
  {
    [DebuggerStepThrough] get => this.textBox != null ? this.textBox.EditorControl : (Control) null;
  }

  public override bool IsInPlaceEditor
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Обновить экранные координаты</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    bool flag = false;
    if (this.pageUI == null && this.needUI)
    {
      this.CreateUI();
      flag = true;
    }
    if (!this.needUpdateUIGeometry || this.pageUI == null || this.page == null)
      return;
    int num = this.SuspendedRefreshUIFlag ? 1 : 0;
    if (num == 0)
      this.SuspendRefreshUI();
    this.InvalidateUI(this.pageUI.Bounds);
    if (this.needUpdateUIGeometry && !flag)
      this.pageUI.UpdateGeometry();
    base.UpdateUIGeometry(false);
    if (this.textBox != null)
    {
      PageControl pageControl = this.PageUI.PageControl;
      Intermech.Document.Model.Page page = this.page as Intermech.Document.Model.Page;
      if (pageControl != null && page?.PageUI != null)
      {
        Rectangle pixel = page.PageUI.ConvertWorldToPixel(this.ClientBounds);
        Rectangle winClientBounds = pixel;
        ++winClientBounds.Width;
        ++winClientBounds.Height;
        Control control = (Control) pageControl;
        if (this.textBox.EditorControl != null)
          this.textBox.EditorControl.Parent = control;
        winClientBounds.Location = page.PagePointToControl(control, winClientBounds.Location);
        this.textBox.SetBounds(this.Bounds, this.ClientBounds, this.Margins, this.DefaultRowSize, pixel, winClientBounds, this.orientation, pageControl.PageScale, pageControl.DisplayDpi, true);
        if (this.textBox.EditorControl != null)
          this.textBox.EditorControl.Visible = this.InPlaceEditorActive;
      }
    }
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

  /// <summary>Сбросить кэш изображения в TextBoxElement</summary>
  public override void ResetTextBoxPaintCache()
  {
    if (this.textBox == null)
      return;
    this.textBox.Invalidate();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(bool force)
  {
    if (!force && this.SuspendedRefreshUIFlag)
      return;
    if (this.pageUI != null)
    {
      if (this.page != null)
        this.page.InvalidateUI(this.pageUI.Bounds);
      this.pageUI.InvalidateUI();
    }
    if (this.textBox == null)
      return;
    this.textBox.Invalidate();
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
    if (!force && this.SuspendedRefreshUIFlag)
      return;
    if (this.page != null)
      this.page.InvalidateUI(clipRectangle);
    if (this.pageUI == null)
      return;
    this.pageUI.InvalidateUI();
  }

  /// <summary>Создать соответствующий элемент управления. Должен быть перекрыт</summary>
  public override void CreateUI()
  {
    if (!this.IsVirtualNode && this.needUI && this.pageUI == null)
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
    this.TextBox = (RtfInSiteEditorWrapper) null;
    base.DestroyUI();
  }

  public override Rectangle GetPixelBounds(DrawContext context)
  {
    return this.pageUI != null ? this.pageUI.Bounds : base.GetPixelBounds(context);
  }

  public override void Dispose()
  {
    base.Dispose();
    if (this.TextBox == null)
      return;
    this.TextBox.Dispose();
    this.TextBox = (RtfInSiteEditorWrapper) null;
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
    RectangleF rectangleF = this.ProperBounds;
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea)
      rectangleF = this.Bounds;
    bool flag1 = parentCell != null && ((double) this.SkipCellsBefore >= 1.0 || (double) this.SkipCellsAfter >= 1.0);
    if (!(!flag1 ? rectangleF : this.Bounds).IntersectsWith(context.ClipRectangle))
      return;
    bool? isSelected = context.IsSelected;
    bool? isFocused = context.IsFocused;
    RectangleElement template = context.Template;
    float? rowSize = context.RowSize;
    bool? isFixedSizeRow = context.IsFixedSizeRow;
    RectangleBorder borders = context.Borders;
    context.Borders = (RectangleBorder) null;
    if (parentCell == null)
      context.Margins = this.Margins;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    GraphicsState gstate1 = context.Graphics.Save();
    try
    {
      if (context.IsPaint && (!context.IsSelected.HasValue || !context.IsSelected.Value))
        context.IsSelected = new bool?(this.ShowSelected);
      bool flag2 = false;
      if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.HasValue)
        context.IsFocused = parentCell == null || !parentCell.IsColumn ? new bool?(flag2 = this.ShowFocused) : new bool?(false);
      context.Template = this.Template as RectangleElement;
      context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
      context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
      if (context.IsSkipedSpace && (parentCell == null || !parentCell.IsFixedStructureArea))
        rectangleF.Height = context.SkipedSpaceSize;
      if (context.Layer == 0)
      {
        this.DrawBackground(context, rectangleF);
        if (!context.WithoutData)
        {
          GraphicsState gstate2 = context.Graphics.Save();
          if (context.IsPaint)
            context.Graphics.Transform = context.TransformMatrix.Matrix;
          if (this.textBox == null && !this.IsEmptyText)
            this.CreateInSiteEditorWrapper(context, rectangleF);
          if (this.textBox != null)
          {
            context.Graphics.SetClip(rectangleF);
            if (this.textBox.EditorControl == null || !context.IsPaint)
              this.textBox.Draw(context);
            else if (!flag2 && (!this.textBox.EditorControl.Visible || this.textBox.Owner.NeedUpdateFormulas))
            {
              context.DrawInCurrentEditor = true;
              this.textBox.Draw(context);
              context.DrawInCurrentEditor = false;
            }
          }
          context.Graphics.Restore(gstate2);
        }
      }
      RowColParams gridRow = (RowColParams) null;
      if (gridRows != null && rowIndex >= 0 && rowIndex < gridRows.Count)
        gridRow = gridRows[rowIndex];
      RowColParams gridCol = (RowColParams) null;
      if (gridCols != null && colIndex >= 0 && colIndex < gridCols.Count)
        gridCol = gridCols[colIndex];
      if (this.drawEllipse)
        this.DrawEllipseBounds(context, rectangleF, gridCol, gridRow, findGridParams);
      else
        this.DrawFrame(context, rectangleF, gridCol, gridRow, findGridParams);
      if (!(!context.IsSkipedSpace & flag1))
        return;
      this.DrawSkipedSpace(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
    }
    finally
    {
      context.Graphics.PageUnit = pageUnit;
      context.Template = template;
      context.RowSize = rowSize;
      context.IsFixedSizeRow = isFixedSizeRow;
      context.IsSelected = isSelected;
      context.IsFocused = isFocused;
      context.MaterialList = (List<int>) null;
      context.Borders = borders;
      if (parentCell == null)
        context.Margins = (MarginsF) null;
      context.Graphics.Restore(gstate1);
    }
  }

  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (!ImDocumentData.ShowDebugInfo)
    {
      this.RemoveProperty(properties, "Rtf");
      this.RemoveProperty(properties, "StartCharIndex");
    }
    if (this.HasTemplate())
    {
      string[] strArray = new string[3]
      {
        "AutoSizeWidth",
        "AutoSizeHeight",
        "FontAutoSize"
      };
      foreach (string key in strArray)
      {
        if (properties[(object) key] is CustomPropertyDescriptor property)
          property.SetIsReadOnly(true);
      }
    }
    if (!this.IsTemplate && this.ReadOnly)
      (properties[(object) "ReferenceToTextSource"] as CustomPropertyDescriptor).SetIsReadOnly(true);
    if (!(this.OwnerDocument is ImDocument ownerDocument) || ownerDocument.DocumentControl == null || !ownerDocument.DocumentControl.ReadOnly)
      return;
    CustomPropertyDescriptor.SetReadOnlyProperties(properties);
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
      case TextBoxElement textBoxElement:
        bool flag1 = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
        if (!flag1)
          this.SuspendUpdateGeometryRefreshUI();
        bool flag2 = !updateLayout || this.SuspendedUpdateLayoutFlag;
        if (!flag2)
          this.SuspendUpdateLayout();
        try
        {
          base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
          if (textBoxElement.rtf != this.rtf && this.Text == textBoxElement.Text)
            this.rtf = textBoxElement.rtf;
          if (this.textBox != null)
            this.textBox.Invalidate();
          if ((this.overrideFlags & OverrideFlags.AutoSize) == OverrideFlags.None)
            this.autoSize = textBoxElement.autoSize;
          if ((this.overrideFlags3 & OverrideFlags3.UseFontAutoSize) != OverrideFlags3.None)
            break;
          this.fontAutoSize = textBoxElement.fontAutoSize;
          break;
        }
        finally
        {
          if (!flag2)
            this.ResumeUpdateLayout(updateUI, updateLayout);
          if (!flag1)
          {
            updateUI = ((updateUI ? 1 : 0) & (flag2 ? 0 : (!updateLayout ? 1 : 0))) != 0;
            this.ResumeUpdateRefreshUI(updateUI, updateUI);
          }
        }
      case LabelElement labelElement:
        int index = this.Index;
        DocumentTreeNode parent = this.Parent;
        if (parent == null)
          break;
        LabelElement child = (LabelElement) labelElement.CloneFromTemplate(true, true);
        child.Id = this.Id;
        child.Name = this.Name;
        child.AssignClonedByTemplateWithParent(this.ClonedByTemplateWithParent);
        if (this.ReferenceToTextSource != null)
          child.AssignReferenceToTextSource(this.ReferenceToTextSource.Clone(), false, false, false);
        else
          child.AssignReferenceToTextSource((ReferenceBase) null, false, false, false);
        child.AssignText(this.Text, false, true, true, false, false);
        child.setBounds(this.bounds);
        child.setProperBounds(this.properBounds);
        parent.RemoveChildNodeAt(index, false, false);
        parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
        break;
      default:
        throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) template.GetDefautCaption(), (object) this.GetDefautCaption()));
    }
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.Template != null;
    if ((flag || this.autoSize != AutoSizeDirection.None) && ((this.overrideFlags & OverrideFlags.AutoSize) != OverrideFlags.None || !flag))
      xw.WriteAttributeString("autoSize", ((int) this.autoSize).ToString());
    if ((double) this.textHeight != 0.0 && this.AutoSizeHeight && !DocumentTreeNode.IsEmptyString(this.Text))
      xw.WriteAttributeString("txtH", this.textHeight.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((double) this.textWidth != 0.0 && this.AutoSizeWidth && !DocumentTreeNode.IsEmptyString(this.Text))
      xw.WriteAttributeString("txtW", this.textWidth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.nextCellCharPos != -1 && this.AutoSizeHeight && !DocumentTreeNode.IsEmptyString(this.Text))
      xw.WriteAttributeString("nextCellCharPos", this.nextCellCharPos.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((!flag || (this.overrideFlags3 & OverrideFlags3.UseFontAutoSize) == OverrideFlags3.None) && flag)
      return;
    xw.WriteAttributeString("fontAutoSize", this.fontAutoSize ? "1" : "0");
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    if (this.rtf == null || this.IsEmptyText)
      return;
    xw.WriteElementString("FormatedText", this.rtf);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (TextBoxElement.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      TextBoxElement.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    if (base.ReadFieldFromXml(readArgs))
      return true;
    switch (readArgs.Reader.LocalName)
    {
      case "FormatedText":
        if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
          readArgs.Reader.Read();
        this.rtf = readArgs.Reader.Value;
        return true;
      case "autoHeight":
      case "autoSizeHeight":
        TextBoxElement.ReadAutoSizeHeight((DocumentTreeNode) this, readArgs);
        return true;
      case "autoSize":
        TextBoxElement.ReadAutoSize((DocumentTreeNode) this, readArgs);
        return true;
      case "nextCellCharPos":
        TextBoxElement.ReadNextCellCharPos((DocumentTreeNode) this, readArgs);
        return true;
      case "txtH":
        TextBoxElement.ReadTextHeight((DocumentTreeNode) this, readArgs);
        return true;
      case "txtW":
        TextBoxElement.ReadTextWidth((DocumentTreeNode) this, readArgs);
        return true;
      default:
        return false;
    }
  }

  private static void InitReadFieldDict()
  {
    TextBoxElement.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) TextData.ReadFieldsDict);
    TextBoxElement.ReadFieldsDict.Add("FormatedText", new ReadFieldFromXmlDelegate(TextBoxElement.ReadFormatedText));
    TextBoxElement.ReadFieldsDict.Add("autoSize", new ReadFieldFromXmlDelegate(TextBoxElement.ReadAutoSize));
    TextBoxElement.ReadFieldsDict.Add("autoSizeHeight", new ReadFieldFromXmlDelegate(TextBoxElement.ReadAutoSizeHeight));
    TextBoxElement.ReadFieldsDict.Add("autoHeight", new ReadFieldFromXmlDelegate(TextBoxElement.ReadAutoSizeHeight));
    TextBoxElement.ReadFieldsDict.Add("txtH", new ReadFieldFromXmlDelegate(TextBoxElement.ReadTextHeight));
    TextBoxElement.ReadFieldsDict.Add("txtW", new ReadFieldFromXmlDelegate(TextBoxElement.ReadTextWidth));
    TextBoxElement.ReadFieldsDict.Add("nextCellCharPos", new ReadFieldFromXmlDelegate(TextBoxElement.ReadNextCellCharPos));
    TextBoxElement.ReadFieldsDict.Add("fontAutoSize", new ReadFieldFromXmlDelegate(TextBoxElement.ReadFontAutoSize));
  }

  private static void ReadFormatedText(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
      readArgs.Reader.Read();
    ((TextBoxElement) docNode).rtf = readArgs.Reader.Value;
  }

  private static void ReadAutoSizeHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 21)
      ((TextBoxElement) docNode).autoSize = bool.Parse(readArgs.Reader.Value) ? AutoSizeDirection.Height : AutoSizeDirection.None;
    else
      ((TextBoxElement) docNode).autoSize = readArgs.Reader.Value == "1" ? AutoSizeDirection.Height : AutoSizeDirection.None;
    docNode.overrideFlags |= OverrideFlags.AutoSize;
  }

  private static void ReadAutoSize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextBoxElement) docNode).autoSize = (AutoSizeDirection) Convert.ToInt32(readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.AutoSize;
  }

  private static void ReadFontAutoSize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextBoxElement) docNode).fontAutoSize = readArgs.Reader.Value.Equals("1", StringComparison.Ordinal);
    docNode.overrideFlags3 |= OverrideFlags3.UseFontAutoSize;
  }

  private static void ReadTextHeight(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextBoxElement) docNode).textHeight = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadTextWidth(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextBoxElement) docNode).textWidth = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadNextCellCharPos(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((TextBoxElement) docNode).nextCellCharPos = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
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
    if (!(src is TextBoxElement textBoxElement))
      return;
    this.autoSize = textBoxElement.autoSize;
    this.textHeight = textBoxElement.textHeight;
    this.textWidth = textBoxElement.textWidth;
    this.fontAutoSize = textBoxElement.fontAutoSize;
    if (copyData)
    {
      this.rtf = textBoxElement.rtf;
      if (this.rtf != null && this.referenceToTextSource != null)
        this.text = textBoxElement.Text;
      this.protectedFirstCharCount = textBoxElement.protectedFirstCharCount;
      this.protectedEndCharCount = textBoxElement.protectedEndCharCount;
      if (!(!templateClone & externalLink))
        return;
      this.nextCellCharPos = textBoxElement.nextCellCharPos;
    }
    else
      this.rtf = (string) null;
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected TextBoxElement(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback</summary>
  public override void OnDeserialization(object sender) => base.OnDeserialization(sender);
}
