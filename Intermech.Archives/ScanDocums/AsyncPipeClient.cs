// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.AsyncPipeClient
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.IO.Pipes;

#nullable disable
namespace Intermech.Archives.ScanDocums;

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
