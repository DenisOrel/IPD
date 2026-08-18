// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.ScanerDocumetPreviewDialog
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.ScanDocums;

/// <summary>
/// Драйвер для работы со сканирующими устройствами (web-камеры, сканеры и т.д.)
/// </summary>
public class ScanerDocumetPreviewDialog : Form, IMessageFilter
{
  /// <summary>Число сообщений Not для обработчика сообщений</summary>
  private int NotMessagesCount;
  /// <summary>
  /// Максимально-допустимое число сообщений Not для обработчика сообщений
  /// </summary>
  private int NotMessagesCountMax = 1000;
  /// <summary>Формат  файла</summary>
  private string extImgFormat = string.Empty;
  /// <summary>Драйвер TWAIN</summary>
  private IntermechTwainDriver driver;
  /// <summary>Если фильтор сообщений инициализирован</summary>
  private bool msgfilter;
  /// <summary>Текущая задача</summary>
  private ScanerDocumetPreviewDialog.Problems cupentProblem;
  private Label label1;

  /// <summary>Событие передачи данных от сканера</summary>
  public event EventHandler OnTransferImage;

  /// <summary>Событие завершения сканирования</summary>
  public event EventHandler OnEndScaning;

  /// <summary>является ли нулевым OnEndScaning</summary>
  /// <returns></returns>
  public bool isOnEndScaning() => this.OnEndScaning == null;

  /// <summary>является ли нулевым OnTransferImage</summary>
  /// <returns></returns>
  public bool isNullOnTransferImage() => this.OnTransferImage == null;

  /// <summary>Конструктор. Диалог драйвера</summary>
  /// <param name="problm"></param>
  /// <param name="extFileFormat"></param>
  public ScanerDocumetPreviewDialog(
    ScanerDocumetPreviewDialog.Problems problm,
    string extFileFormat)
  {
    this.InitializeComponent();
    this.Init(problm, extFileFormat);
    this.Load += new EventHandler(this.ScanerDocumetPreviewDialog_Load);
  }

  /// <summary>
  /// Обработчик сообщений сканера (отлавливает сообщения только сканера)
  /// </summary>
  /// <param name="m"></param>
  /// <returns></returns>
  bool IMessageFilter.PreFilterMessage(ref Message m)
  {
    TwainCommand twainCommand = this.driver.PassMessage(ref m);
    if (twainCommand == TwainCommand.Not)
    {
      ++this.NotMessagesCount;
      if (this.NotMessagesCount > this.NotMessagesCountMax)
      {
        this.CloseDialog();
        throw new Exception(ServiceHolder.rm.GetString("Archives_97"));
      }
      return false;
    }
    this.NotMessagesCount = 0;
    try
    {
      switch (twainCommand)
      {
        case TwainCommand.TransferReady:
          ArrayList arrayList = this.driver.TransferPictures();
          for (int index = 0; index < arrayList.Count; ++index)
          {
            try
            {
              IntPtr img = (IntPtr) arrayList[index];
              byte[] imageData = this.driver.GetImageData(img, this.extImgFormat);
              if (imageData != null)
              {
                this.driver.FreeImage(img);
                if (this.OnTransferImage != null)
                  this.OnTransferImage((object) imageData, EventArgs.Empty);
              }
            }
            catch (OutOfMemoryException ex)
            {
              throw new OutOfMemoryException(ServiceHolder.rm.GetString("Archives_93"));
            }
          }
          this.driver.Acquire();
          break;
        case TwainCommand.CloseRequest:
          this.CloseDialog();
          break;
        case TwainCommand.CloseOk:
          this.CloseDialog();
          break;
      }
    }
    catch (Exception ex)
    {
      this.CloseDialog();
      throw new Exception(ServiceHolder.rm.GetString("Archives_94") + ex.Message);
    }
    return true;
  }

  /// <summary>Закрывает текущий диалог</summary>
  private void CloseDialog()
  {
    this.driver.Finish();
    this.EndingScan();
    this.driver.CloseSrc();
    if (this.OnEndScaning != null)
      this.OnEndScaning((object) null, EventArgs.Empty);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>Завершает процесс сканирования</summary>
  private void EndingScan()
  {
    if (!this.msgfilter)
      return;
    Application.RemoveMessageFilter((IMessageFilter) this);
    this.msgfilter = false;
    this.Enabled = true;
    this.Activate();
  }

  private void ScanerDocumetPreviewDialog_Load(object sender, EventArgs e)
  {
    try
    {
      this.Visible = false;
      switch (this.cupentProblem)
      {
        case ScanerDocumetPreviewDialog.Problems.Scan:
          this.Acquire();
          break;
        case ScanerDocumetPreviewDialog.Problems.SelecDevice:
          this.SelectDevice();
          break;
        case ScanerDocumetPreviewDialog.Problems.SelectDeviceAndScan:
          this.SelectDevice();
          this.Acquire();
          break;
      }
    }
    catch (Exception ex)
    {
      this.CloseDialog();
      throw new Exception(ServiceHolder.rm.GetString("Archives_94") + ex.Message);
    }
  }

  /// <summary>Выбрать устройство</summary>
  private void SelectDevice() => this.driver.Select();

  /// <summary>Сканировать</summary>
  private void Acquire()
  {
    if (!this.msgfilter)
    {
      this.Enabled = false;
      this.msgfilter = true;
      Application.AddMessageFilter((IMessageFilter) this);
    }
    this.driver.Acquire();
  }

  /// <summary>Инициализация драйвера</summary>
  /// <param name="problm">Задача</param>
  /// <param name="extFileFormat">формат файла изображения (пример: .png)</param>
  private void Init(ScanerDocumetPreviewDialog.Problems problm, string extFileFormat)
  {
    this.driver = new IntermechTwainDriver();
    this.cupentProblem = problm;
    this.driver.Init(this.Handle);
    this.extImgFormat = extFileFormat;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ScanerDocumetPreviewDialog));
    this.label1 = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoValidate = AutoValidate.EnablePreventFocusChange;
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Name = nameof (ScanerDocumetPreviewDialog);
    this.Opacity = 0.0;
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.TransparencyKey = Color.White;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Задачи</summary>
  public enum Problems
  {
    /// <summary>Сканировать</summary>
    Scan,
    /// <summary>Выбрать устройство</summary>
    SelecDevice,
    /// <summary>Выбрать условие и Сканировать</summary>
    SelectDeviceAndScan,
  }
}
