// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.ScanerDocumentService
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using Intermech.TwainScanner;
using Intermech.TwainScanner.VintaSoftScanner;
using System;

#nullable disable
namespace Intermech.Archives.ScanDocums;

/// <summary>Служба сканирования графических документов</summary>
internal class ScanerDocumentService
{
  /// <summary>Если служба проинициализирована</summary>
  private bool isInit;
  private bool vintaSoftScan = true;

  /// <summary>Событие. Получение данных от сканера</summary>
  public event EventHandler OnImageTransfer;

  /// <summary>Событие завершения сканирования</summary>
  public event EventHandler OnEndScaning;

  /// <summary>навен ли OnImageTransfer null</summary>
  /// <returns></returns>
  public bool IsNullOnEndScaning() => this.OnEndScaning == null;

  /// <summary>навен ли OnImageTransfer null</summary>
  /// <returns></returns>
  public bool IsNullOnImageTransfer() => this.OnImageTransfer == null;

  /// <summary>Сканировать</summary>
  /// <param name="fileExt">форамт файла (пример: .png )</param>
  public void AcquireDoc(string fileExt)
  {
    if (!this.vintaSoftScan)
    {
      ScanerDocumetPreviewDialog.Problems problm = ScanerDocumetPreviewDialog.Problems.Scan;
      LogManager.AddLine("isInit =" + this.isInit.ToString());
      if (!this.isInit)
      {
        this.Init();
        problm = ScanerDocumetPreviewDialog.Problems.SelectDeviceAndScan;
      }
      LogManager.AddLine("problem =" + problm.ToString());
      using (ScanerDocumetPreviewDialog documetPreviewDialog = new ScanerDocumetPreviewDialog(problm, fileExt))
      {
        if (documetPreviewDialog.isNullOnTransferImage())
          documetPreviewDialog.OnTransferImage += new EventHandler(this.dlg_OnTransferImage);
        if (documetPreviewDialog.isOnEndScaning())
          documetPreviewDialog.OnEndScaning += new EventHandler(this.dlg_OnEndScaning);
        int num = (int) documetPreviewDialog.ShowDialog();
      }
    }
    else
    {
      using (MainForm mainForm = new MainForm())
      {
        mainForm.FileExtension = fileExt;
        mainForm.OnTransferImage += new EventHandler(this.dlg_OnTransferImage);
        int num = (int) mainForm.ShowDialog();
      }
    }
  }

  /// <summary>Инициализация службы</summary>
  public void Init()
  {
    if (this.isInit)
      return;
    this.isInit = true;
  }

  /// <summary>выбор устройства/драйвера</summary>
  public void SelectDevice()
  {
    using (ScanerDocumetPreviewDialog documetPreviewDialog = new ScanerDocumetPreviewDialog(ScanerDocumetPreviewDialog.Problems.SelecDevice, string.Empty))
    {
      int num = (int) documetPreviewDialog.ShowDialog();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dlg_OnEndScaning(object sender, EventArgs e)
  {
    if (this.OnEndScaning == null)
      return;
    this.OnEndScaning(sender, e);
  }

  private void dlg_OnTransferImage(object sender, EventArgs e)
  {
    if (this.OnImageTransfer == null)
      return;
    this.OnImageTransfer(sender, e);
  }
}
