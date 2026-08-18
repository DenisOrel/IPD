
// Type: Intermech.Client.Core.FormDesigner.Controls.IMPreviewBox
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

[RefreshProperties(RefreshProperties.All)]
[CanAlwaysEnabled]
public class IMPreviewBox : 
  PictureBox,
  IAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  IExtendedParent4Control,
  IParent4Control
{
  /// <summary>Guid атрибута Превью</summary>
  public Guid PreviewAttrGuid = SystemGUIDs.attributePreview;
  public Guid ModifiedAttrGuid = new Guid("cad0013a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Замена Guid'а атрибута</summary>
  private AttributeInfo _attrInfo;
  /// <summary>ID объекта "библиотечное изображение"</summary>
  private Image _img;
  /// <summary>
  /// Сюда сохраняем картинку, когда контрол связан с атрибутом, и у атрибута задано значение
  /// </summary>
  /// <remarks>На тот случай, если перейдем на закладку "Свойства", удалим значение атрибута, потом вернемся назад - нужно, чтобы перегрузилась картинка</remarks>
  private Image _bckgImg;
  private long cachedId = -1;
  private AttributableElements cachedAttributableElements;
  private DateTime cachedModifiedDate = DateTime.MinValue;

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
  public IMPreviewBox()
  {
    this.Name = string.Empty;
    this.CanAddAttribute = false;
    this.TabIndex = -1;
    this.TabStop = false;
    this.ContextMenuStrip = this.GetContextMenu();
  }

  /// <summary>
  /// Глобальные идентификаторы атрибута и типа объекта/связи.
  /// </summary>
  [Browsable(false)]
  public AttributeInfo AttributeInfo
  {
    get
    {
      if (this._attrInfo == null)
        this._attrInfo = new AttributeInfo(this.PreviewAttrGuid, Guid.Empty);
      return this._attrInfo;
    }
    set
    {
    }
  }

  /// <summary>
  /// Возможность добавления атрибута в случае если он отсутствует у объекта.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public bool CanAddAttribute
  {
    get => false;
    set
    {
    }
  }

  /// <summary>Значение атрибута.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AttributeValues Values
  {
    get => (AttributeValues) null;
    set
    {
      bool flag1 = true;
      if (value != null && value.Values != null && value.Values.Length != 0)
      {
        Image image = (Image) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          bool flag2 = false;
          IDBAttributable dbAttributable = (IDBAttributable) null;
          if (this.cachedAttributableElements == this.ParentInfo.ElementKind && this.cachedId == this.ParentInfo.ElementIdentifier)
          {
            if (this.cachedAttributableElements == AttributableElements.Object)
            {
              dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(this.ParentInfo.ElementIdentifier, false);
              IDBAttribute attributeByGuid = (dbAttributable as IDBObject).GetAttributeByGuid(this.ModifiedAttrGuid);
              DateTime dateTime = this.cachedModifiedDate;
              if (attributeByGuid != null)
                dateTime = attributeByGuid.AsDateTime;
              if (this.cachedModifiedDate != dateTime)
              {
                this.cachedModifiedDate = dateTime;
                flag2 = true;
              }
            }
          }
          else
          {
            if (this.ParentInfo.ElementKind == AttributableElements.Object)
              dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(this.ParentInfo.ElementIdentifier, false);
            else if (this.ParentInfo.ElementKind == AttributableElements.Relation)
              dbAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(this.ParentInfo.ElementIdentifier, false);
            if (this.ParentInfo.ElementKind == AttributableElements.Object)
              this.cachedModifiedDate = (dbAttributable as IDBObject).ModifyDate;
            flag2 = true;
          }
          this.cachedId = this.ParentInfo.ElementIdentifier;
          this.cachedAttributableElements = this.ParentInfo.ElementKind;
          if (flag2 && dbAttributable != null)
          {
            IDBAttribute attributeByGuid = dbAttributable.GetAttributeByGuid(this.PreviewAttrGuid, false);
            if (attributeByGuid != null)
            {
              MemoryStream aDestStream = new MemoryStream();
              new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
              try
              {
                aDestStream.Position = 0L;
                image = Image.FromStream((Stream) aDestStream, true, true);
              }
              catch
              {
                aDestStream.Dispose();
                image = (Image) null;
                flag1 = false;
              }
            }
          }
          else
            flag1 = false;
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
          flag1 = false;
          this.Invalidate();
        }
      }
      if (!flag1)
        return;
      if (this._bckgImg != null)
      {
        this.BackgroundImage = this._bckgImg.Clone() as Image;
        this._bckgImg.Dispose();
        this._bckgImg = (Image) null;
      }
      else
        this.ClearImg();
    }
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

  /// <summary>Изменение данных.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Modified
  {
    get => false;
    set
    {
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
      Rectangle rect = ((IImageFromLibrary) null).CalcBackgroundImageRectangle(this.ClientRectangle, this._img.Size, this.BackgroundImageLayout);
      pevent.Graphics.DrawImage(this._img, rect);
    }
  }

  /// <summary>
  /// Очистить данные, которые относятся к изображению подгружаемому из библиотеки.
  /// </summary>
  private void ClearImgFromLibraryData() => this.ClearImg();

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

  /// <summary>Получить контекстное меню</summary>
  /// <returns></returns>
  private ContextMenuStrip GetContextMenu()
  {
    ContextMenuStrip contextMenu = new ContextMenuStrip();
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    int index = service.ImageIndex("imgView");
    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(LocalizationHolder.rm.GetString("Client.Core_378"));
    toolStripMenuItem.Click += (EventHandler) ((sender, e) => this.ShowImageEvent(sender, e, (object) (this._img ?? this.BackgroundImage)));
    toolStripMenuItem.Image = index != -1 ? service.ImageList.Images[index] : (Image) null;
    contextMenu.Items.Add((ToolStripItem) toolStripMenuItem);
    return contextMenu;
  }

  /// <summary>Обрабочик события Показать изображение</summary>
  /// <param name="sender"></param>
  /// <param name="eventArgs"></param>
  /// <param name="item"></param>
  private void ShowImageEvent(object sender, EventArgs eventArgs, object item)
  {
    FullImageView.ShowImage(item);
  }

  /// <summary>Необходимость сериализации свойства BackColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeBackColor()
  {
    return this.Parent != null && !this.Parent.BackColor.Equals((object) this.BackColor);
  }
}
