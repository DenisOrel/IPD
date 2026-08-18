
// Type: Intermech.Search.Web.RemoteClientDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Web
{
    public sealed class RemoteClientDescription
    {
      public RemoteClientDescription(
        string publicUserHostAddress,
        string privateUserHostAddress,
        string userAgent)
      {
        if (string.IsNullOrEmpty(publicUserHostAddress))
          throw new ArgumentException();
        if (string.IsNullOrEmpty(userAgent))
          throw new ArgumentException();
        this.PublicUserHostAddress = publicUserHostAddress;
        this.PrivateUserHostAddress = privateUserHostAddress;
        this.UserAgent = userAgent;
      }

      public string PublicUserHostAddress { get; private set; }

      public string PrivateUserHostAddress { get; private set; }

      public string UserAgent { get; private set; }
    }
}
