// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.AsyncPipeServer
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.IO.Pipes;
using System.Text;

#nullable disable
namespace Intermech.TwainScanner;

public class AsyncPipeServer
{
  private string _pipeName;

  public event DelegateMessage PipeMessage;

  public void Listen(string PipeName)
  {
    try
    {
      this._pipeName = PipeName;
      NamedPipeServerStream state = new NamedPipeServerStream(PipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
      state.BeginWaitForConnection(new AsyncCallback(this.WaitForConnectionCallBack), (object) state);
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
      byte[] numArray = new byte[(int) byte.MaxValue];
      asyncState.Read(numArray, 0, (int) byte.MaxValue);
      Encoding.UTF8.GetString(numArray, 0, numArray.Length);
      this.PipeMessage(numArray);
      asyncState.Close();
      NamedPipeServerStream state = new NamedPipeServerStream(this._pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
      state.BeginWaitForConnection(new AsyncCallback(this.WaitForConnectionCallBack), (object) state);
    }
    catch
    {
    }
  }
}
