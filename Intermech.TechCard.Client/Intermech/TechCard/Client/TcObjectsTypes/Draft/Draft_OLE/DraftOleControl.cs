// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE.DraftOleControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Controls.OleContainer;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;

/// <summary>Контрол для отображения OLE эскизов</summary>
internal class DraftOleControl : UserControl
{
  /// <summary>Режим отображения OLE</summary>
  private DraftOleControl.OleDisplayMode _displayMode;
  /// <summary>
  /// 
  /// </summary>
  private PictureBoxSizeMode _sizeMode;
  /// <summary>OLE Контейнер</summary>
  private ImOleContainer _oleContainer;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox pictBox;

  /// <summary>Инициализация контролов</summary>
  private void InitializeCustomControls()
  {
    if (this._oleContainer == null)
      return;
    this._oleContainer.Parent = (Control) this;
    this._oleContainer.ActivationGesture = ActivationGesture.DoubleClick;
    this._oleContainer.ShowMenus = false;
    this._oleContainer.ShowToolbars = false;
    this._oleContainer.SizeMode = DocumentSizeMode.Zoom;
    this._oleContainer.Name = "contrs_oleContr";
    this._oleContainer.CreateControl();
    ((ISupportInitialize) this._oleContainer).BeginInit();
    this._oleContainer.BringToFront();
    this._oleContainer.Dock = DockStyle.Fill;
    ((ISupportInitialize) this._oleContainer).EndInit();
    this._oleContainer.DocumentModified += new EventHandler(this.OleDataModified);
  }

  /// <summary>
  /// 
  /// </summary>
  private void DisposeCustomControls()
  {
    if (this._oleContainer == null)
      return;
    this._oleContainer.DocumentModified -= new EventHandler(this.OleDataModified);
    this._oleContainer.Parent = (Control) null;
    this._oleContainer.SourceData = (Stream) null;
    this._oleContainer.Dispose();
    this._oleContainer = (ImOleContainer) null;
  }

  /// <summary>Изменение режима отображения</summary>
  /// <param name="value"></param>
  private void UpdateSizeMode(PictureBoxSizeMode value)
  {
    switch (this._displayMode)
    {
      case DraftOleControl.OleDisplayMode.Image:
        this.pictBox.SizeMode = value;
        break;
      case DraftOleControl.OleDisplayMode.OLE:
        if (this._oleContainer == null)
          break;
        if (value != PictureBoxSizeMode.StretchImage)
        {
          if (value == PictureBoxSizeMode.Zoom)
          {
            this._oleContainer.SizeMode = DocumentSizeMode.Zoom;
            break;
          }
          this._oleContainer.SizeMode = DocumentSizeMode.Clip;
          break;
        }
        this._oleContainer.SizeMode = DocumentSizeMode.Stretch;
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="displayMode"></param>
  private void UpdateDisplayMode(DraftOleControl.OleDisplayMode displayMode)
  {
    if (this._oleContainer == null)
      return;
    if (displayMode == DraftOleControl.OleDisplayMode.Image)
      this.pictBox.BringToFront();
    else
      this._oleContainer.BringToFront();
    this.UpdateSizeMode(this._sizeMode);
  }

  /// <summary>Обновление содержимого контрола</summary>
  private void UpdateOleStream(Stream value)
  {
    if (value == null || value.Length == 0L)
    {
      if (this._displayMode != DraftOleControl.OleDisplayMode.Unknown)
        return;
      this._displayMode = DraftOleControl.OleDisplayMode.OLE;
    }
    else
    {
      long position = value.Position;
      try
      {
        Image image = (Image) null;
        try
        {
          value.Position = 0L;
          image = Image.FromStream(value);
        }
        catch
        {
        }
        if (image == null)
        {
          value.Position = 0L;
          Stream stream = (Stream) null;
          try
          {
            stream = OleHelper.ExtractOleData(value);
          }
          catch (Exception ex)
          {
            IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
            if (service != null)
            {
              string category = LocalizationHolder.rm.GetString("TechCard.Client_424");
              string text = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_80"), (object) ex.Message);
              service.WriteString(category, text);
              service.WriteString(category, ex.StackTrace);
              service.Activate(category);
            }
          }
          if (stream != null)
          {
            stream.Position = 0L;
            try
            {
              image = Image.FromStream(stream);
            }
            catch
            {
            }
          }
        }
        if (image != null)
        {
          this.pictBox.Image = image;
          this._displayMode = DraftOleControl.OleDisplayMode.Image;
        }
        else
          this._displayMode = DraftOleControl.OleDisplayMode.OLE;
      }
      catch
      {
        this._displayMode = DraftOleControl.OleDisplayMode.OLE;
      }
      finally
      {
        value.Position = position;
        this.UpdateDisplayMode(this._displayMode);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OleDataModified(object sender, EventArgs e)
  {
    if (this.ReadOnly)
      return;
    this.UpdateOleStream(this.OleStream);
  }

  /// <summary>Конструктор</summary>
  public DraftOleControl()
  {
    if (!this.DesignMode)
      this._oleContainer = new ImOleContainer();
    this.InitializeCustomControls();
    this.InitializeComponent();
    this.pictBox.Visible = true;
    if (this._oleContainer == null)
      return;
    this._oleContainer.Visible = true;
  }

  /// <summary>Создание нового объекта</summary>
  /// <returns></returns>
  public bool CallInsertDlg() => this._oleContainer != null && this._oleContainer.CallInsertDlg();

  /// <summary>
  /// 
  /// </summary>
  public void OpenEditor()
  {
    if (this.ReadOnly)
      return;
    this._oleContainer?.Activate();
  }

  /// <summary>Режим только чтение</summary>
  public bool ReadOnly { get; set; }

  /// <summary>Режим отображения</summary>
  public DraftOleControl.OleDisplayMode DisplayMode => this._displayMode;

  /// <summary>Size Mode</summary>
  public PictureBoxSizeMode SizeMode
  {
    get => this._sizeMode;
    set
    {
      if (this._sizeMode == value)
        return;
      this.UpdateSizeMode(value);
      this._sizeMode = value;
    }
  }

  /// <summary>Stream OLE объекта</summary>
  public Stream OleStream
  {
    get => this._oleContainer?.SourceData;
    set
    {
      if (this._oleContainer == null)
        return;
      this._oleContainer.SourceData = value;
      this._oleContainer.Update();
      this.UpdateOleStream(value);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.DisposeCustomControls();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DraftOleControl));
    this.pictBox = new PictureBox();
    ((ISupportInitialize) this.pictBox).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pictBox, "pictBox");
    this.pictBox.Name = "pictBox";
    this.pictBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pictBox);
    this.Name = nameof (DraftOleControl);
    ((ISupportInitialize) this.pictBox).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Режим отображения OLE объекта</summary>
  public enum OleDisplayMode
  {
    /// <summary>Режим не определен</summary>
    Unknown,
    /// <summary>Изображение</summary>
    Image,
    /// <summary>OLE объект</summary>
    OLE,
  }
}
