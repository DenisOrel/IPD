// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.InSiteEditorWrapper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Базовый класс-оболочка для редактора текста по месту</summary>
[TypeConverter(typeof (LocalizedExpandableObjectConverter))]
[Serializable]
public abstract class InSiteEditorWrapper
{
  protected Control editorControl;
  /// <summary>Родитель</summary>
  internal TextData owner;

  /// <summary>Установить шрифт по умолчанию</summary>
  /// <param name="font">Шрифт</param>
  /// <param name="textColor">Цвет текста</param>
  public virtual void SetDefaultEditorFont(Font font, Color textColor)
  {
    if (this.EditorControl == null)
      return;
    this.EditorControl.Font = font;
  }

  /// <summary>Установить шрифт по умолчанию</summary>
  public virtual void SetDefaultCharFormat()
  {
  }

  /// <summary>Установить цвет фона по умолчанию</summary>
  /// <param name="backColor">Цвет фона</param>
  public virtual void SetDefaultEditorBackColor(Color backColor)
  {
    if (this.EditorControl == null)
      return;
    this.EditorControl.BackColor = backColor;
  }

  /// <summary>Установить режим ReadOnly для активного редактора</summary>
  /// <param name="value"></param>
  public virtual void SetReadOnly(bool value)
  {
  }

  public virtual bool OwnerDocumentIsReadOnly(Control editor)
  {
    return editor != null && editor.Parent is PageControl parent && parent.DocumentControl.ReadOnly;
  }

  /// <summary>Установить выравнивание текста по умолчанию</summary>
  public virtual void SetDefaultTextAlignment()
  {
  }

  /// <summary>Установить формат параграфа по умолчанию</summary>
  public virtual void SetDefaultParagraphFormat()
  {
  }

  /// <summary>Форматировать текст в активном редакторе</summary>
  public virtual void FormatEditorText()
  {
  }

  /// <summary>Редактор активен</summary>
  public virtual bool EditorActive
  {
    [DebuggerStepThrough] get => this.editorControl != null && this.editorControl.Parent != null;
  }

  /// <summary>Активировать редактор текста</summary>
  /// <param name="pageUI">Элемент пользовательского интерфейса</param>
  /// <param name="mouseEventArgs">Аргументы события мыши</param>
  public virtual void ActivateEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
    this.EditorText = this.Owner.GetText();
    if (this.editorControl == null)
      return;
    this.editorControl.Visible = true;
    this.editorControl.Enabled = true;
    this.editorControl.BackColor = this.Owner.GetBackColor();
    this.editorControl.Parent = (Control) pageUI.PageControl;
    this.editorControl.Focus();
  }

  /// <summary>Деактивировать редактор</summary>
  public virtual void DeactivateEditor()
  {
    Control editorControl = this.EditorControl;
    if (editorControl == null)
      return;
    editorControl.Visible = false;
    Control parent = editorControl.Parent;
    if (parent == null)
      return;
    parent.Focus();
    editorControl.Parent = (Control) null;
    editorControl.Visible = false;
  }

  /// <summary>владелец редактора</summary>
  public TextData Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  public void SetOwner(TextData value) => this.owner = value;

  internal ImDocumentData OwnerDocument
  {
    get => this.owner != null ? this.owner.OwnerDocument : (ImDocumentData) null;
  }

  internal List<string> GetMaterialKeyWords()
  {
    ImDocumentData ownerDocument = this.OwnerDocument;
    return ownerDocument != null ? ownerDocument.MaterialKeyWords : new List<string>();
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец редактора</param>
  public InSiteEditorWrapper(TextData owner) => this.owner = owner;

  /// <summary>Минимальный размер редактора</summary>
  public abstract SizeF MinSize { get; }

  /// <summary>контрол редактора</summary>
  public virtual Control EditorControl
  {
    [DebuggerStepThrough] get => this.editorControl;
  }

  /// <summary>Назначить контрол редактора</summary>
  /// <param name="value">Значение</param>
  protected virtual void AssignEditorControl(Control value)
  {
    if (this.editorControl == value)
      return;
    if (this.editorControl != null)
    {
      this.editorControl.GotFocus -= new EventHandler(this.editorControl_GotFocus);
      this.editorControl.Parent = (Control) null;
    }
    this.editorControl = value;
    if (this.editorControl == null)
      return;
    this.editorControl.GotFocus += new EventHandler(this.editorControl_GotFocus);
  }

  private void editorControl_GotFocus(object sender, EventArgs e)
  {
  }

  /// <summary>Событие Текст изменен</summary>
  public event EventHandler TextChanged;

  /// <summary>Вызывает событие TextChaged</summary>
  public virtual void OnTextChaged()
  {
    EventHandler textChanged = this.TextChanged;
    if (textChanged == null)
      return;
    textChanged((object) this, new EventArgs());
  }

  /// <summary>Проверка текста в редакторе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  internal virtual void Editor_Validating(object sender, CancelEventArgs e)
  {
  }

  /// <summary>Обновить формат текста в редакторе если он активен</summary>
  public abstract void UpdateActiveEditorFormat();

  /// <summary>Текст в редакторе</summary>
  public abstract string EditorText { get; set; }

  /// <summary>В редакторе текст без форматирования</summary>
  public abstract bool EditorTextIsPlain { get; }

  /// <summary>Текст в редакторе с форматированием</summary>
  public abstract string EditorRtf { get; set; }

  /// <summary>Установить границы</summary>
  /// <param name="ownerBounds">Границы владельца в мм</param>
  /// <param name="clientBounds">Границы редактора в мм</param>
  /// <param name="margins">Поля в миллиметрах</param>
  /// <param name="fixedRowSize">Фиксированный размер строки</param>
  /// <param name="winOwnerBounds">Границы владельца в пикселах</param>
  /// <param name="winClientBounds">Границы редактора в пикселах</param>
  /// <param name="scale">Масштаб</param>
  /// <param name="dpi">dpi экрана</param>
  /// <param name="repage">Вызвать переразбивку после вызова</param>
  public abstract void SetBounds(
    RectangleF ownerBounds,
    RectangleF clientBounds,
    MarginsF margins,
    float fixedRowSize,
    Rectangle winOwnerBounds,
    Rectangle winClientBounds,
    TextOrientation orientation,
    float scale,
    PointF dpi,
    bool repage);

  /// <summary>Обновить изображение</summary>
  public abstract void Invalidate();

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public abstract void Draw(DrawContext context);

  /// <summary>Распределить текст и получить его размеры</summary>
  /// <param name="direction">Направление изменения размера</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="nextCellCharPos">Возвращается позиция текста для следующей ячейки</param>
  /// <returns>Возвращает размер текста</returns>
  public abstract SizeF Distribute(
    AutoSizeDirection direction,
    SizeF maxSize,
    out int nextCellCharPos);

  /// <summary>Выделить текст</summary>
  /// <param name="pageUI">Элемент управления в контексте которого активизировать</param>
  /// <param name="selection">Координаты выделения</param>
  public virtual void SetTextSelection(PageElementUI pageUI, TextSelection selection)
  {
    if (this.EditorActive)
      return;
    this.ActivateEditor(pageUI, (MouseEventArgs) null);
  }

  /// <summary>Получить координаты выделения</summary>
  /// <returns>Координаты выделения</returns>
  public virtual TextSelection GetTextSelection() => new TextSelection();

  /// <summary>Получить координаты текстового курсора (каретки).
  /// Координаты относительно редактора.</summary>
  /// <returns></returns>
  public abstract Point GetTextCursorCoor();

  /// <summary>Получить высоту строки в пикселях экрана</summary>
  public abstract int GetCurLineHeight();

  /// <summary>Курсор находится в конце текста</summary>
  public abstract bool CursorInEndPosition { get; }

  /// <summary>Курсор находится в начале текста</summary>
  public abstract bool CursorInFirstPosition { get; }

  /// <summary>Курсор находится на последней строке</summary>
  public abstract bool CursorInLastLine { get; }

  /// <summary>Курсор находится на первой строке</summary>
  public abstract bool CursorInFirstLine { get; }
}
