// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.VintaSoftScanner.MainForm
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Vintasoft.Twain;

#nullable disable
namespace Intermech.TwainScanner.VintaSoftScanner;

public class MainForm : Form
{
  /// <summary>TWAIN device manager.</summary>
  private DeviceManager _deviceManager;
  /// <summary>Scan session count.</summary>
  private int _sessionCount;
  /// <summary>Acquired image count.</summary>
  private int _imageCount;
  private string fileExtension = "";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox pictureBox1;

  public MainForm()
  {
    this.InitializeComponent();
    this._deviceManager = new DeviceManager((Form) this);
  }

  /// <summary>Application form is shown.</summary>
  private void MainForm_Shown(object sender, EventArgs e)
  {
    if (!this.OpenDeviceManager())
      this.Close();
    this.AcquireImage();
  }

  public string FileExtension
  {
    get => this.fileExtension;
    set => this.fileExtension = value;
  }

  /// <summary>Open TWAIN device manager.</summary>
  private bool OpenDeviceManager()
  {
    if (!this._deviceManager.IsTwainAvailable)
    {
      this._deviceManager.IsTwain2Compatible = false;
      if (!this._deviceManager.IsTwainAvailable)
      {
        int num = (int) MessageBox.Show("Не найден менеджер Twain");
        return false;
      }
    }
    this._deviceManager.Open();
    if (((ReadOnlyCollectionBase) this._deviceManager.Devices).Count != 0)
      return true;
    int num1 = (int) MessageBox.Show("Сканеры не обнаружены");
    return false;
  }

  /// <summary>Событие передачи данных от сканера</summary>
  public event EventHandler OnTransferImage;

  /// <summary>Событие завершения сканирования</summary>
  public event EventHandler OnEndScaning;

  /// <summary>Acquire image.</summary>
  private void AcquireImage()
  {
    try
    {
      this._sessionCount = 0;
      this._imageCount = 0;
      string str = ScanerSelectForm.Execute(this._deviceManager);
      if (str != null)
      {
        Device device = this._deviceManager.Devices.Find(str);
        device.Open();
        device.ShowUI = true;
        device.DisableAfterAcquire = false;
        device.ImageAcquired += new EventHandler<ImageAcquiredEventArgs>(this.device_ImageAcquired);
        device.ScanCompleted += new EventHandler(this.device_ScanCompleted);
        device.UserInterfaceClosed += new EventHandler(this.device_UserInterfaceClosed);
        device.ScanCanceled += new EventHandler(this.device_ScanCanceled);
        device.ScanFailed += new EventHandler<ScanFailedEventArgs>(this.device_ScanFailed);
        device.Acquire();
      }
      else
        this.Close();
    }
    catch (TwainException ex)
    {
      int num = (int) MessageBox.Show(((Exception) ex).Message);
    }
  }

  /// <summary>Image is acquired.</summary>
  private void device_ImageAcquired(object sender, ImageAcquiredEventArgs e)
  {
    ++this._imageCount;
    if (this.pictureBox1.Image != null)
    {
      this.pictureBox1.Image.Dispose();
      this.pictureBox1.Image = (Image) null;
    }
    this.pictureBox1.Image = (Image) e.Image.GetAsBitmap(true);
    MemoryStream memoryStream = new MemoryStream();
    ImageFormat format = ImageFormat.Bmp;
    switch (this.FileExtension.ToUpper())
    {
      case ".TIFF":
      case ".TIF":
        format = ImageFormat.Tiff;
        break;
      case ".JPEG":
      case ".JPG":
        format = ImageFormat.Jpeg;
        break;
      case ".PNG":
        format = ImageFormat.Png;
        break;
      case ".GIF":
        format = ImageFormat.Gif;
        break;
    }
    this.pictureBox1.Image.Save((Stream) memoryStream, format);
    if (this.OnTransferImage != null)
      this.OnTransferImage((object) memoryStream.GetBuffer(), EventArgs.Empty);
    e.Image.Dispose();
  }

  /// <summary>Scan is completed.</summary>
  private void device_ScanCompleted(object sender, EventArgs e)
  {
    ++this._sessionCount;
    Device device = (Device) sender;
    if (device.ShowUI)
      return;
    int num = (int) MessageBox.Show($"Сканирование завершено. Изображений отсканированно: {this._imageCount}");
    this.CloseDevice(device, true);
  }

  /// <summary>Scan is canceled.</summary>
  private void device_ScanCanceled(object sender, EventArgs e)
  {
    Device device = (Device) sender;
    int num = (int) MessageBox.Show("Отмена сканирования");
    this.CloseDevice(device, true);
  }

  /// <summary>Scan is failed.</summary>
  private void device_ScanFailed(object sender, ScanFailedEventArgs e)
  {
    Device device = (Device) sender;
    int num = (int) MessageBox.Show(e.ErrorString);
    this.CloseDevice(device, false);
    this.AcquireImage();
  }

  /// <summary>User interface of device is closed.</summary>
  private void device_UserInterfaceClosed(object sender, EventArgs e)
  {
    Device device = (Device) sender;
    int num = (int) MessageBox.Show($"Сканирование завершено. Изображений отсканированно: {this._imageCount}");
    this.CloseDevice(device, true);
  }

  /// <summary>Unsubscribe from device events and close the device.</summary>
  private void CloseDevice(Device device, bool close)
  {
    if (device == null)
      return;
    device.ImageAcquired -= new EventHandler<ImageAcquiredEventArgs>(this.device_ImageAcquired);
    device.ScanCompleted -= new EventHandler(this.device_ScanCompleted);
    device.UserInterfaceClosed -= new EventHandler(this.device_UserInterfaceClosed);
    device.ScanCanceled -= new EventHandler(this.device_ScanCanceled);
    device.ScanFailed -= new EventHandler<ScanFailedEventArgs>(this.device_ScanFailed);
    if (device.State != null)
      device.Close();
    if (!close)
      return;
    this.Close();
  }

  protected override void OnClosed(EventArgs e) => base.OnClosed(e);

  /// <summary>Application form is closing.</summary>
  private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    ((Component) this._deviceManager).Dispose();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.pictureBox1 = new PictureBox();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
    this.pictureBox1.Location = new Point(7, 12);
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.Size = new Size(440, 523);
    this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
    this.pictureBox1.TabIndex = 6;
    this.pictureBox1.TabStop = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(456, 545);
    this.Controls.Add((Control) this.pictureBox1);
    this.Name = nameof (MainForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Предварительный просмотр";
    this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
    this.Shown += new EventHandler(this.MainForm_Shown);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
  }
}
