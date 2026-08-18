// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.TimeStampServer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;
using System.Net;
using System.Text;


namespace Syncfusion.Pdf.Security
{
    public class TimeStampServer
    {
      private string m_password;
      private Uri m_server;
      private int m_timeOut;
      private string m_username;

      public TimeStampServer(Uri server)
      {
        this.m_server = !(server == (Uri) null) ? server : throw new ArgumentNullException("Sever");
      }

      public TimeStampServer(Uri server, string username, string password)
        : this(server)
      {
        this.m_username = username;
        this.m_password = password;
      }

      public TimeStampServer(Uri server, string username, string password, int timeOut)
        : this(server, username, password)
      {
        this.m_timeOut = timeOut;
      }

      internal byte[] GetTimeStampResponse(byte[] request)
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(this.m_server.ToString());
        httpWebRequest.ContentLength = (long) request.Length;
        httpWebRequest.ContentType = "application/timestamp-query";
        httpWebRequest.Method = "POST";
        if (!string.IsNullOrEmpty(this.m_username))
        {
          string base64String = Convert.ToBase64String(Encoding.Default.GetBytes($"{this.m_username}:{this.m_password}"));
          httpWebRequest.Headers["Authorization"] = "Basic " + base64String;
        }
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(request, 0, request.Length);
        requestStream.Close();
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        Stream stream = response.StatusCode == HttpStatusCode.OK ? response.GetResponseStream() : throw new Exception("Server returned unexpected response code : " + response.StatusCode.ToString());
        MemoryStream memoryStream = new MemoryStream();
        byte[] buffer = new byte[1024 /*0x0400*/];
        int count;
        while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
          memoryStream.Write(buffer, 0, count);
        stream.Close();
        response.Close();
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        string contentEncoding = response.ContentEncoding;
        if (!string.IsNullOrEmpty(response.ContentEncoding) && response.ContentEncoding.Equals("base64", StringComparison.InvariantCultureIgnoreCase))
          bytes = Convert.FromBase64String(Encoding.ASCII.GetString(bytes));
        return bytes;
      }

      public string Password
      {
        get => this.m_password;
        set => this.m_password = value;
      }

      public Uri Server
      {
        get => this.m_server;
        set
        {
          this.m_server = !(value == (Uri) null) ? value : throw new ArgumentNullException(nameof (Server));
        }
      }

      public int TimeOut
      {
        get => this.m_timeOut;
        set => this.m_timeOut = value;
      }

      public string UserName
      {
        get => this.m_username;
        set => this.m_username = value;
      }
    }
}
