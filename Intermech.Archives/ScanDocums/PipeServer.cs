// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.PipeServer
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Text;

#nullable disable
namespace Intermech.Archives.ScanDocums;

public class PipeServer
{
  private string _pipeName;
  private Func<byte[], byte[]> func;
  private bool close;
  private NamedPipeServerStream pipeServer;

  public PipeServer(Func<byte[], byte[]> func) => this.func = func;

  public void ListenAsync(string PipeName)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += new DoWorkEventHandler(this.bw_DoWork);
    backgroundWorker.WorkerSupportsCancellation = true;
    backgroundWorker.RunWorkerAsync((object) PipeName);
  }

  public void Close()
  {
    this.close = true;
    this.pipeServer.Close();
    this.pipeServer = (NamedPipeServerStream) null;
  }

  private void bw_DoWork(object sender, DoWorkEventArgs e) => this.Listen((string) e.Argument);

  public void Listen(string PipeName)
  {
    try
    {
      this._pipeName = PipeName;
      this.pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
      while (!this.close)
      {
        this.pipeServer.WaitForConnection();
        new StreamBytes((Stream) this.pipeServer).WriteString(this.func(new StreamBytes((Stream) this.pipeServer).ReadString()));
        this.pipeServer.Close();
        this.pipeServer = (NamedPipeServerStream) null;
        this.pipeServer = new NamedPipeServerStream(this._pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
      }
    }
    catch (Exception ex)
    {
    }
  }

  private void WaitForConnectionCallBack(IAsyncResult iar)
  {
    try
    {
      NamedPipeServerStream asyncState = (NamedPipeServerStream) iar.AsyncState;
      asyncState.EndWaitForConnection(iar);
      byte[] numArray = new byte[255000];
      asyncState.Read(numArray, 0, 255000);
      Encoding.UTF8.GetString(numArray, 0, numArray.Length);
      asyncState.Close();
      NamedPipeServerStream state = new NamedPipeServerStream(this._pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
      state.BeginWaitForConnection(new AsyncCallback(this.WaitForConnectionCallBack), (object) state);
    }
    catch
    {
    }
  }
}
