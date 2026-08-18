// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.PipeClient
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.IO;
using System.IO.Pipes;

#nullable disable
namespace Intermech.TwainScanner;

public class PipeClient
{
  public byte[] Send(byte[] sendBytes, string PipeName, int TimeOut = 0)
  {
    try
    {
      NamedPipeClientStream ioStream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
      if (TimeOut != 0)
        ioStream.Connect();
      else
        ioStream.Connect(TimeOut);
      new StreamBytes((Stream) ioStream).WriteString(sendBytes);
      byte[] numArray = new StreamBytes((Stream) ioStream).ReadString();
      ioStream.Close();
      return numArray;
    }
    catch (Exception ex)
    {
      throw;
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
