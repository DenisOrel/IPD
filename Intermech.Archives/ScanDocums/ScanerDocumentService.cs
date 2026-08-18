// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.ScanerDocumentService
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces.Client;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.Archives.ScanDocums;

/// <summary>Служба сканирования графических документов</summary>
[ComVisible(true)]
public class ScanerDocumentService : IScanerDocumentService
{
  private object tbdObject;
  /// <summary>Если служба проинициализирована</summary>
  private bool isInit;

  public void OnEndScaningMethod()
  {
    // ISSUE: reference to a compiler-generated field
    if (ScanerDocumentService.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ScanerDocumentService.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Done", (IEnumerable<Type>) null, typeof (ScanerDocumentService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ScanerDocumentService.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) ScanerDocumentService.\u003C\u003Eo__1.\u003C\u003Ep__0, this.tbdObject);
    this.tbdObject = (object) null;
    if (this.OnEndScaning == null)
      return;
    this.OnEndScaning((object) null, EventArgs.Empty);
  }

  public void OnImageTransferMethod(byte[] image)
  {
    if (this.OnImageTransfer == null)
      return;
    this.OnImageTransfer((object) image, EventArgs.Empty);
  }

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
    if (true)
    {
      Process[] processesByName = Process.GetProcessesByName("Intermech.TwainScanner");
      try
      {
        for (int index = processesByName.Length - 1; index >= 0; --index)
          processesByName[index].Kill();
      }
      catch
      {
      }
      string str = AppDomain.CurrentDomain.BaseDirectory + "\\Intermech.TwainScanner.exe";
      Process process = Process.Start(new ProcessStartInfo()
      {
        FileName = str,
        Arguments = "r"
      });
      new PipeServer(new Func<byte[], byte[]>(this.server_PipeMessage)).ListenAsync("Intermech.Archives.ScanDocums.Client");
      PipeClient pipeClient = new PipeClient();
      fileExt = "F" + fileExt;
      byte[] bytes = Encoding.UTF8.GetBytes(fileExt);
      pipeClient.Send(bytes, "Intermech.TwainScanner", 10000);
      try
      {
        process.Kill();
        if (this.OnEndScaning == null)
          return;
        this.OnEndScaning((object) null, EventArgs.Empty);
      }
      catch
      {
      }
    }
    else
    {
      ScanerDocumetPreviewDialog.Problems problm = ScanerDocumetPreviewDialog.Problems.Scan;
      if (!this.isInit)
      {
        this.Init();
        problm = ScanerDocumetPreviewDialog.Problems.SelectDeviceAndScan;
      }
      using (ScanerDocumetPreviewDialog documetPreviewDialog = new ScanerDocumetPreviewDialog(problm, fileExt))
      {
        if (documetPreviewDialog.isNullOnTransferImage())
          documetPreviewDialog.OnTransferImage += new EventHandler(this.dlg_OnTransferImage);
        if (documetPreviewDialog.isOnEndScaning())
          documetPreviewDialog.OnEndScaning += new EventHandler(this.dlg_OnEndScaning);
        int num = (int) documetPreviewDialog.ShowDialog();
      }
    }
  }

  /// <summary>Сообщение с сервера с сканированным изображением</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  private byte[] server_PipeMessage(byte[] data)
  {
    if (this.OnImageTransfer != null)
      this.OnImageTransfer((object) data, EventArgs.Empty);
    return new byte[1];
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
