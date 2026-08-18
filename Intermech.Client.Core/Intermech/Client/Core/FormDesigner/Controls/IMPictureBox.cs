
// Type: Intermech.Client.Core.FormDesigner.Controls.IMPictureBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Thumbnail;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[RefreshProperties(RefreshProperties.All)]
[CanAlwaysEnabled]
public class IMPictureBox : 
  PictureBox,
  IAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  IExtendedParent4Control,
  IParent4Control,
  IImageFromLibrary,
  ILockModify
{
  /// <summary>Замена Guid'а атрибута</summary>
  private AttributeInfo _attrInfo;
  /// <summary>Метод выбора изображений</summary>
  private PictureSelectMode _picSelectMode;
  /// <summary>
  /// Ссылка на изображение, хранящееся в библиотеке изображений
  /// </summary>
  private Guid _imgFromLibGuid = Guid.Empty;
  /// <summary>ID объекта "библиотечное изображение"</summary>
  private long _imgFromLibraryID;
  private string _imgFromLibraryName = string.Empty;
  private Image _img;
  /// <summary>
  /// Сюда сохраняем картинку, когда контрол связан с атрибутом, и у атрибута задано значение
  /// </summary>
  /// <remarks>На тот случай, если перейдем на закладку "Свойства", удалим значение атрибута, потом вернемся назад - нужно, чтобы перегрузилась картинка</remarks>
  private Image _bckgImg;
  /// <summary>
  /// Класс, содержащий изображение, замененное в процессе редактирования в режиме PictureSelectMode.UserRuntime
  /// null для неинициализированного, System.DBNull - для очищенного изображения
  /// </summary>
  private PictureBoxImageData _userDefinedImg;
  private OpenFileDialog fdOpen;
  private ToolStripMenuItem menuItemAdd;
  private ToolStripMenuItem menuItemClear;
  /// <summary>кэш для зачитанного блоба</summary>
  private PictureBoxImageData blobCacheData;
  /// <summary>
  /// Класс, содержащий идентификатор(ы) атрибута + его значение(я)
  /// </summary>
  protected AttributeValues _attrValues;
  /// <summary>Наличие изменений в контроле</summary>
  protected bool _modified;
  protected bool _enabled = true;
  /// <summary>
  /// Флаг, о рассылке уведомления об изменениии значений в контроле
  /// </summary>
  protected bool _needNotify;

  /// <summary>Цвет фона элемента управления.</summary>
  public new Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>
  /// Фоновое изображение, выводимое на элементе управления.
  /// </summary>
  public override Image BackgroundImage
  {
    get => base.BackgroundImage;
    set
    {
      this.ClearImgFromLibraryData();
      base.BackgroundImage = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new int TabIndex { get; private set; }

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new bool TabStop { get; private set; }

  /// <summary>Конструктор.</summary>
  public IMPictureBox()
  {
    this.Name = string.Empty;
    this.CanAddAttribute = false;
    this.TabIndex = -1;
    this.TabStop = false;
    this.ContextMenuStrip = this.GetContextMenu();
  }

  [Browsable(false)]
  public PictureSelectMode PictureSelectMode
  {
    get => this._picSelectMode;
    set => this._picSelectMode = value;
  }

  /// <summary>
  /// Глобальные идентификаторы атрибута и типа объекта/связи.
  /// </summary>
  [Browsable(false)]
  public AttributeInfo AttributeInfo
  {
    get => this._attrInfo;
    set
    {
      this._attrInfo = value == null || !MetaDataHelper.ExistsAttributeType(value.AttributeGuid) ? (AttributeInfo) null : value;
    }
  }

  /// <summary>
  /// Возможность добавления атрибута в случае если он отсутствует у объекта.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public bool CanAddAttribute
  {
    get => this.PictureSelectMode == PictureSelectMode.UserRuntime;
    set
    {
    }
  }

  /// <summary>Значение атрибута.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AttributeValues Values
  {
    get
    {
      if (this.PictureSelectMode == PictureSelectMode.Fixed)
        return (AttributeValues) null;
      if (this._attrValues != null)
        this._attrValues.Values = this.GetValues;
      return this._attrValues;
    }
    set
    {
      bool flag = true;
      if (this.PictureSelectMode == PictureSelectMode.UserRuntime)
      {
        this._attrValues = value;
        AttributeOptions options = AttributeOptions.None;
        if (this._attrInfo != null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            options = this.GetAttributeOptions(MetaDataHelper.GetAttributeID((object) this._attrInfo.AttributeGuid), sessionKeeper.Session);
        }
        if (value != null && value.Values != null && value.Values.Length != 0)
        {
          Image image = (Image) null;
          try
          {
            image = this.GetImageFromAttribute(this.DesForm.Info, value, ref this._userDefinedImg, ref this.blobCacheData);
          }
          catch (Exception ex)
          {
          }
          if (image != null)
          {
            if (this.BackgroundImage != null)
            {
              if (this._bckgImg != null)
              {
                this._bckgImg.Dispose();
                this._bckgImg = (Image) null;
              }
              this._bckgImg = this.BackgroundImage.Clone() as Image;
            }
            this.ClearImg();
            this.ClearBckgImg();
            this._img = image;
            flag = false;
            this.Invalidate();
          }
        }
        this._enabled = this.IsEnabled(value, options);
      }
      if (flag && value != null && value.Values != null && value.Values.Length != 0)
      {
        long result = 0;
        if (long.TryParse(Convert.ToString(value.Values[0]), out result))
        {
          Image imageFromLibrary = this.GetImageFromLibrary(Guid.Empty, ref result, ref this._imgFromLibraryName);
          if (imageFromLibrary != null)
          {
            if (this.BackgroundImage != null)
            {
              if (this._bckgImg != null)
              {
                this._bckgImg.Dispose();
                this._bckgImg = (Image) null;
              }
              this._bckgImg = this.BackgroundImage.Clone() as Image;
            }
            this.ClearImg();
            this.ClearBckgImg();
            this._img = imageFromLibrary;
            flag = false;
            this.Invalidate();
          }
        }
      }
      if (!flag)
        return;
      if (this._bckgImg != null)
      {
        this.BackgroundImage = this._bckgImg.Clone() as Image;
        this._bckgImg.Dispose();
        this._bckgImg = (Image) null;
      }
      else
      {
        this.ClearImg();
        Guid imgFromLibGuid = this._imgFromLibGuid;
        this._imgFromLibGuid = Guid.Empty;
        this.ImageFromLibrary = imgFromLibGuid;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual bool ValueIsEmpty
  {
    get
    {
      return this._attrValues == null || this._attrValues.Values == null || this._attrValues.Values.Length == 0 || this._attrValues.Values[0] == null || this._attrValues.Values[0] == DBNull.Value;
    }
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected virtual object[] GetValues
  {
    get
    {
      BlobValue blobValue = (BlobValue) null;
      if (this._userDefinedImg != null)
      {
        byte[] data = this._userDefinedImg.Buffer != null ? (byte[]) this._userDefinedImg.Buffer.Clone() : (byte[]) null;
        blobValue = new BlobValue(new BlobInformation((long) data.Length, (long) data.Length, this._userDefinedImg.FileDate, this._userDefinedImg.FileName, ArcMethods.NotPacked, string.Empty), data);
      }
      return new object[1]{ (object) blobValue };
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnLeaveControl(EventArgs e)
  {
    if (!this._needNotify || this.DesForm == null || this._attrValues == null)
      return;
    this.DesForm.AttributeChanging(this._attrValues.AttributeID, this._attrValues.Values, this.GetValues, this.ParentPoint == AttributeDestinationPoint.Default);
    this._needNotify = false;
  }

  /// <summary>Установка допустимых значений.</summary>
  /// <param name="data"></param>
  /// <param name="possibleValueFieldName"></param>
  /// <param name="descriptionFieldName"></param>
  public void SetPossibleValues(
    DataTable data,
    string possibleValueFieldName,
    string descriptionFieldName)
  {
  }

  /// <summary>Устанавливает родительскую форму.</summary>
  [Browsable(false)]
  public DesForm DesForm { private get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool LockModify { get; set; }

  /// <summary>Проверка возможности редактирования атрибута.</summary>
  /// <param name="av">Значение атрибута</param>
  /// <param name="options">Опции</param>
  /// <returns>Результат проверки</returns>
  private bool IsEnabled(AttributeValues av, AttributeOptions options)
  {
    bool flag = av != null && !av.ReadOnly;
    if (flag)
      flag = (options & AttributeOptions.DisableManualEdit) != AttributeOptions.DisableManualEdit;
    return flag;
  }

  /// <summary>
  /// Устанавливает и возвращает произошло ли изменение данных.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool Modified
  {
    get => this._modified;
    set
    {
      if (this._picSelectMode == PictureSelectMode.Fixed || this.LockModify || this._attrValues == null)
        return;
      this._modified = value;
      this._needNotify = true;
      if (!this._modified)
        return;
      this.OnModified();
    }
  }

  /// <summary>Событие, возникающее при изменении данных.</summary>
  public event EventHandler ModifiedEvent;

  /// <summary>
  /// 
  /// </summary>
  private void OnModified()
  {
    if (this.ModifiedEvent == null)
      return;
    this.ModifiedEvent((object) this, EventArgs.Empty);
  }

  /// <summary>Идентификатор типа объекта/связи.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ParentTypeID { get; set; }

  /// <summary>Устанавливает родителя для атрибута.</summary>
  [Browsable(false)]
  public IElementInfo ParentInfo { get; set; }

  /// <summary>Для чего нужен контрол.</summary>
  [Browsable(false)]
  [DefaultValue(AttributeDestinationPoint.Default)]
  public AttributeDestinationPoint ParentPoint { get; set; }

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
      this.ClearBckgImg();
      this._imgFromLibraryID = value == Guid.Empty ? 0L : this._imgFromLibraryID;
      this.ClearImg();
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
      this.DesForm = (DesForm) null;
      this.ClearImg();
      this.ClearBckgImg();
      this.ClearUserDefinedImg();
      if (this._bckgImg != null)
      {
        this._bckgImg.Dispose();
        this._bckgImg = (Image) null;
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
    if (this.BackgroundImageLayout == ImageLayout.Tile)
    {
      using (TextureBrush textureBrush = new TextureBrush(this._img, WrapMode.Tile))
        pevent.Graphics.FillRectangle((Brush) textureBrush, this.ClientRectangle);
    }
    else
    {
      Rectangle rect = this.CalcBackgroundImageRectangle(this.ClientRectangle, this._img.Size, this.BackgroundImageLayout);
      pevent.Graphics.DrawImage(this._img, rect);
    }
  }

  /// <summary>
  /// Очистить данные, которые относятся к изображению подгружаемому из библиотеки.
  /// </summary>
  private void ClearImgFromLibraryData()
  {
    this._imgFromLibraryName = string.Empty;
    this._imgFromLibraryID = 0L;
    this._imgFromLibGuid = Guid.Empty;
    this.ClearImg();
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearImg()
  {
    if (this._img == null)
      return;
    this._img.Dispose();
    this._img = (Image) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearBckgImg()
  {
    if (base.BackgroundImage == null)
      return;
    base.BackgroundImage.Dispose();
    base.BackgroundImage = (Image) null;
  }

  private void ClearUserDefinedImg()
  {
    if (this._userDefinedImg == null)
      return;
    this._userDefinedImg = (PictureBoxImageData) null;
  }

  /// <summary>Получить контекстное меню</summary>
  /// <returns></returns>
  private ContextMenuStrip GetContextMenu()
  {
    ContextMenuStrip contextMenu = new ContextMenuStrip();
    contextMenu.Opening += new CancelEventHandler(this.ContextMenu_Opening);
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    int index1 = service.ImageIndex("imgAdd");
    ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(LocalizationHolder.rm.GetString("Client.Core_Upload378"));
    toolStripMenuItem1.Click += (EventHandler) ((sender, e) => this.AddImageEvent(sender, e));
    toolStripMenuItem1.Image = index1 != -1 ? service.ImageList.Images[index1] : (Image) null;
    contextMenu.Items.Add((ToolStripItem) toolStripMenuItem1);
    this.menuItemAdd = toolStripMenuItem1;
    int index2 = service.ImageIndex("imgClear");
    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(LocalizationHolder.rm.GetString("Client.Core_Clear378"));
    toolStripMenuItem2.Click += (EventHandler) ((sender, e) => this.ClearImageEvent(sender, e));
    toolStripMenuItem2.Image = index2 != -1 ? service.ImageList.Images[index2] : (Image) null;
    contextMenu.Items.Add((ToolStripItem) toolStripMenuItem2);
    this.menuItemClear = toolStripMenuItem2;
    int index3 = service.ImageIndex("imgView");
    ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(LocalizationHolder.rm.GetString("Client.Core_378"));
    toolStripMenuItem3.Click += (EventHandler) ((sender, e) => this.ShowImageEvent(sender, e, (object) (this._img ?? this.BackgroundImage)));
    toolStripMenuItem3.Image = index3 != -1 ? service.ImageList.Images[index3] : (Image) null;
    contextMenu.Items.Add((ToolStripItem) toolStripMenuItem3);
    return contextMenu;
  }

  private void ContextMenu_Opening(object sender, CancelEventArgs e)
  {
    this.CheckAccessibilityButtons();
  }

  protected virtual void CheckAccessibilityButtons()
  {
    this.menuItemAdd.Visible = this.PictureSelectMode == PictureSelectMode.UserRuntime;
    this.menuItemAdd.Enabled = this._enabled;
    this.menuItemClear.Visible = this.PictureSelectMode == PictureSelectMode.UserRuntime;
    this.menuItemClear.Enabled = this._enabled;
  }

  /// <summary>Обрабочик события Показать изображение</summary>
  /// <param name="sender"></param>
  /// <param name="eventArgs"></param>
  /// <param name="item"></param>
  private void ShowImageEvent(object sender, EventArgs eventArgs, object item)
  {
    FullImageView.ShowImage(item);
  }

  private OpenFileDialog OpenFileDlgCheckInit()
  {
    if (this.fdOpen == null)
    {
      this.fdOpen = new OpenFileDialog();
      this.fdOpen.Filter = "Файлы изображений (*.bmp;*.gif;*.tif;*.tiff;*.png;*.jpg;*.jpeg;*.exif;*.ico;*.emf;*.wmf)|*.bmp;*.gif;*.tif;*.tiff;*.png;*.jpg;*.jpeg;*.exif;*.ico;*.emf;*.wmf|Все файлы (*.*)|*.*";
    }
    return this.fdOpen;
  }

  private void AddImageEvent(object sender, EventArgs eventArgs)
  {
    this.OpenFileDlgCheckInit();
    if (this.fdOpen.ShowDialog() != DialogResult.OK)
      return;
    this.ClearUserDefinedImg();
    DateTime lastWriteTime = File.GetLastWriteTime(this.fdOpen.FileName);
    using (FileStream fileStream = new FileStream(this.fdOpen.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
      this._userDefinedImg = new PictureBoxImageData((Stream) fileStream, 0L, Path.GetFileName(this.fdOpen.FileName), lastWriteTime);
    this.Values = new AttributeValues(MetaDataHelper.GetAttributeID((object) this.AttributeInfo.AttributeGuid), FieldTypes.ftFile, MultiValueModes.SingleValue, this.GetValues)
    {
      AttributeGuid = this.AttributeInfo.AttributeGuid
    };
    this.Modified = true;
    this.Invalidate();
  }

  private void ClearImageEvent(object sender, EventArgs eventArgs)
  {
    this.ClearUserDefinedImg();
    this.Values = new AttributeValues(MetaDataHelper.GetAttributeID((object) this.AttributeInfo.AttributeGuid))
    {
      AttributeGuid = this.AttributeInfo.AttributeGuid,
      Values = this.GetValues
    };
    this.Modified = true;
    this.Invalidate();
  }

  /// <summary>Необходимость сериализации свойства BackColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeBackColor()
  {
    return this.Parent != null && !this.Parent.BackColor.Equals((object) this.BackColor);
  }
}
