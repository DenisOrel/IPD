
// Type: Intermech.Diagnostics.UriScheme
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;


namespace Intermech.Diagnostics
{
    public enum UriScheme
    {
      /// <summary>None</summary>
      [Description("None")] None,
      /// <summary>Any</summary>
      [Description("Any")] Any,
      /// <summary>Pointer to a file</summary>
      [Description("Pointer to a file")] File,
      /// <summary>File Transfer Protocol (FTP)</summary>
      [Description("File Transfer Protocol (FTP)")] Ftp,
      /// <summary>Gopher protocol</summary>
      [Description("Gopher protocol")] Gopher,
      /// <summary>Hypertext Transfer Protocol (HTTP)</summary>
      [Description("Hypertext Transfer Protocol (HTTP)")] Http,
      /// <summary>Secure Hypertext Transfer Protocol (HTTPS)</summary>
      [Description("Secure Hypertext Transfer Protocol (HTTPS)")] Https,
      /// <summary>Simple Mail Transport Protocol (SMTP)</summary>
      [Description("Simple Mail Transport Protocol (SMTP)")] Mailto,
      /// <summary>Network News Transport Protocol (NNTP)</summary>
      [Description("Network News Transport Protocol (NNTP)")] News,
      /// <summary>Network News Transport Protocol (NNTP)</summary>
      [Description("Network News Transport Protocol (NNTP)")] Nntp,
      /// <summary>Windows Communication Foundation (WCF)</summary>
      [Description("Windows Communication Foundation (WCF)")] NetTcp,
      /// <summary>Windows Communication Foundation (WCF)</summary>
      [Description("Windows Communication Foundation (WCF)")] NetPipe,
    }
}
