
// Type: Intermech.Client.Core.FormDesigner.Controls.IMLabel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Класс создан для того, чтобы в дизайнере форм лэйба обрамлялась пунктирной линией.
/// </summary>
[Designer(typeof (IMLabelControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public sealed class IMLabel : Label, IImageFromLibrary
{
  /// <summary>
  /// Ссылка на изображение, хранящееся в библиотеке изображений
  /// </summary>
  private Guid _imgFromLibGuid = Guid.Empty;
  /// <summary>ID объекта "библиотечное изображение"</summary>
  private long _imgFromLibraryID;
  private string _imgFromLibraryName = string.Empty;
  private Image _img;

  /// <summary>Цвет фона элемента управления.</summary>
  public new Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>Изображение, отображаемое в элементе управления.</summary>
  public new Image Image
  {
    get => base.Image;
    set
    {
      this.ClearImgFromLibraryData();
      base.Image = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>По просьбе Е. Лицкевич</remarks>
  [Browsable(false)]
  protected override Padding DefaultPadding => new Padding(0, 0, 0, 0);

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  public new Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new int TabIndex { get; private set; }

  /// <summary>Конструктор.</summary>
  public IMLabel()
  {
    this.Name = string.Empty;
    this.TabIndex = -1;
  }

  /// <summary>
  /// Ссылка на изображение, хранящееся в библиотеке изображений.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(typeof (Guid), "00000000-0000-0000-0000-000000000000")]
  public Guid ImageFromLibrary
  {
    get => this._imgFromLibGuid;
    set
    {
      if (!(this._imgFromLibGuid != value))
        return;
      if (base.Image != null)
      {
        base.Image.Dispose();
        base.Image = (Image) null;
      }
      this._imgFromLibraryID = value == Guid.Empty ? 0L : this._imgFromLibraryID;
      if (this._img != null)
      {
        this._img.Dispose();
        this._img = (Image) null;
      }
      this._img = this.GetImageFromLibrary(value, ref this._imgFromLibraryID, ref this._imgFromLibraryName);
      if (this._img != null)
        this._imgFromLibGuid = value;
      else
        this.ClearImgFromLibraryData();
      this.Invalidate();
    }
  }

  /// <summary>Наименование изображения.</summary>
  [Browsable(false)]
  public string ImageFromLibraryName => this._imgFromLibraryName;

  /// <summary>ID объекта "библиотечное изображение".</summary>
  [Browsable(false)]
  public long ImageFromLibraryID => this._imgFromLibraryID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._img != null)
      {
        this._img.Dispose();
        this._img = (Image) null;
      }
      if (this.BackgroundImage != null)
      {
        this.BackgroundImage.Dispose();
        this.BackgroundImage = (Image) null;
      }
      if (base.Image != null)
      {
        base.Image.Dispose();
        base.Image = (Image) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>Перерисовка изображения.</summary>
  /// <param name="pevent"></param>
  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    base.OnPaintBackground(pevent);
    if (this._img == null)
      return;
    this.DrawImage(pevent.Graphics, this._img, this.ClientRectangle, this.ImageAlign);
  }

  /// <summary>
  /// Очистить данные, которые относятся к изображению подгружаемому из библиотеки.
  /// </summary>
  private void ClearImgFromLibraryData()
  {
    this._imgFromLibraryName = string.Empty;
    this._imgFromLibraryID = 0L;
    this._imgFromLibGuid = Guid.Empty;
    if (this._img == null)
      return;
    this._img.Dispose();
    this._img = (Image) null;
  }

  /// <summary>Необходимость сериализации свойства BackColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeBackColor()
  {
    return this.Parent != null && !this.Parent.BackColor.Equals((object) this.BackColor);
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }

  /// <summary>Необходимость сериализации свойства ForeColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeForeColor()
  {
    return this.Parent != null && !this.Parent.ForeColor.Equals((object) this.ForeColor);
  }
}
