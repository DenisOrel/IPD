// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.WebMorda
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

using Intermech.Diagnostics;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

#nullable disable
namespace Intermech.ConnectionBroker;

internal sealed class WebMorda
{
  private ConnectionBrokerServer _Broker;
  private HttpListener _Listener;

  public WebMorda(ConnectionBrokerServer broker) => this._Broker = broker;

  private string GetResponseHTTP()
  {
    StringBuilder stringBuilder = new StringBuilder("<html><head><title>IPS Connection Broker</title></head><body><h1>Брокер подключений IPS</h1><h2>Список серверов приложений:</h2><table cellpadding=\"2\" cellspacing=\"0\" width=\"98%\" border=\"1\" bordercolor=\"white\" class=\"infotable\"><tbody>");
    foreach (object serversOutput in this._Broker.GetServersOutputList())
      stringBuilder.AppendFormat("<tr><td>{0}</td></tr>", serversOutput);
    stringBuilder.Append("</tbody></table></BODY></HTML>");
    return stringBuilder.ToString();
  }

  public bool StartListener()
  {
    int num = 19667;
    string s = ConfigurationManager.AppSettings.Get("HttpPort");
    int result;
    if (s != null && s != string.Empty && int.TryParse(s, out result))
      num = result;
    if (!HttpListener.IsSupported)
      return false;
    try
    {
      this._Listener = new HttpListener();
      string str = ConfigurationManager.AppSettings.Get("HttpAddress");
      if (s != null && s != string.Empty)
      {
        this._Listener.Prefixes.Add($"http://{str}:{num}/");
      }
      else
      {
        string hostName = Dns.GetHostName();
        IPAddress[] addressList = Dns.GetHostEntry(hostName).AddressList;
        for (int index = 0; index < addressList.Length; ++index)
        {
          if (addressList[index].AddressFamily == AddressFamily.InterNetwork)
            this._Listener.Prefixes.Add($"http://{addressList[index].ToString()}:{num}/");
        }
        this._Listener.Prefixes.Add($"http://{hostName}:{num}/");
        this._Listener.Prefixes.Add($"http://localhost:{num}/");
        this._Listener.Prefixes.Add($"http://127.0.0.1:{num}/");
      }
      this._Listener.Start();
      this._Broker._EventLog.DefaultLog.Write($"Слушатель HTTP стартован. Порт {num}", EventLogItemType.Information);
      return true;
    }
    catch (Exception ex)
    {
      this._Broker._EventLog.DefaultLog.Write($"Ошибка старта слушателя HTTP (порт {num}): {ex.Message}", EventLogItemType.Error);
      return false;
    }
  }

  public void Listen(object obj)
  {
    try
    {
      while (this._Listener.IsListening)
      {
        HttpListenerResponse response = this._Listener.GetContext().Response;
        response.ContentType = "text/html; charset=UTF-8";
        byte[] bytes = Encoding.UTF8.GetBytes(this.GetResponseHTTP());
        response.ContentLength64 = (long) bytes.Length;
        using (Stream outputStream = response.OutputStream)
          outputStream.Write(bytes, 0, bytes.Length);
      }
    }
    catch (Exception ex)
    {
      this._Broker._EventLog.DefaultLog.Write($"Ошибка слушателя HTTP: {ex.Message}", EventLogItemType.Error);
      this._Broker._EventLog.DefaultLog.Write("Веб-интерфейс остановлен.", EventLogItemType.Information);
    }
    this._Listener.Stop();
    this._Listener.Close();
  }
}
