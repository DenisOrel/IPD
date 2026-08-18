// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.NamedPipesServer
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using Intermech.Archives.ScanDocums;
using System;
using System.Text;

#nullable disable
namespace Intermech.TwainScanner;

/// <summary>Сервер который обращается к сканеру</summary>
internal class NamedPipesServer
{
  private static NamedPipesServer instance;
  private ScanerDocumentService scanerService;

  internal static NamedPipesServer Instance
  {
    get
    {
      if (NamedPipesServer.instance == null)
        NamedPipesServer.instance = new NamedPipesServer();
      return NamedPipesServer.instance;
    }
    set => NamedPipesServer.instance = value;
  }

  public void Init()
  {
    PipeServer pipeServer = new PipeServer(new Func<byte[], byte[]>(this.GetObjectData));
    LogManager.AddLine("Intermech.TwainScanner start");
    pipeServer.Listen("Intermech.TwainScanner");
  }

  public byte[] GetObjectData(byte[] data)
  {
    LogManager.AddLine(nameof (GetObjectData));
    string fileExt = Encoding.UTF8.GetString(data).Remove(0, 1);
    LogManager.AddLine("fileExt =" + fileExt);
    if (this.scanerService == null)
    {
      this.scanerService = new ScanerDocumentService();
      this.scanerService.OnEndScaning += new EventHandler(this.scanerService_OnEndScaning);
      this.scanerService.OnImageTransfer += new EventHandler(this.scanerService_OnImageTransfer);
    }
    this.scanerService.AcquireDoc(fileExt);
    LogManager.AddLine("AcquireDoc end =" + fileExt);
    return new byte[1];
  }

  private void scanerService_OnImageTransfer(object sender, EventArgs e)
  {
    if (!(sender is byte[] sendBytes))
      return;
    new PipeClient().Send(sendBytes, "Intermech.Archives.ScanDocums.Client");
  }

  private void scanerService_OnEndScaning(object sender, EventArgs e)
  {
  }
}
