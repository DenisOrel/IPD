// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.AsyncPipeClient
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.IO.Pipes;

#nullable disable
namespace Intermech.TwainScanner;

public class AsyncPipeClient
{
  public void Send(byte[] buffer, string PipeName, int TimeOut = 1000)
  {
    try
    {
      NamedPipeClientStream state = new NamedPipeClientStream(".", PipeName, PipeDirection.Out, PipeOptions.Asynchronous);
      state.Connect(TimeOut);
      state.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(this.AsyncSend), (object) state);
    }
    catch (TimeoutException ex)
    {
    }
  }

  private void AsyncSend(IAsyncResult iar)
  {
    try
    {
      NamedPipeClientStream asyncState = (NamedPipeClientStream) iar.AsyncState;
      asyncState.EndWrite(iar);
      asyncState.Flush();
      asyncState.Close();
      asyncState.Dispose();
    }
    catch (Exception ex)
    {
    }
  }
}
