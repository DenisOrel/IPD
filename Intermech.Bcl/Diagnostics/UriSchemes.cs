
// Type: Intermech.Diagnostics.UriSchemes
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Diagnostics
{
    public static class UriSchemes
    {
      private const string Const_None = "None";
      private const string Const_Any = "Any";
      [NotNull]
      public static readonly IReadOnlyDictionary<UriScheme, string> Value2Name = (IReadOnlyDictionary<UriScheme, string>) new Dictionary<UriScheme, string>()
      {
        [UriScheme.None] = "None",
        [UriScheme.Any] = "Any",
        [UriScheme.File] = Uri.UriSchemeFile,
        [UriScheme.Ftp] = Uri.UriSchemeFtp,
        [UriScheme.Gopher] = Uri.UriSchemeGopher,
        [UriScheme.Http] = Uri.UriSchemeHttp,
        [UriScheme.Https] = Uri.UriSchemeHttps,
        [UriScheme.Mailto] = Uri.UriSchemeMailto,
        [UriScheme.News] = Uri.UriSchemeNews,
        [UriScheme.Nntp] = Uri.UriSchemeNntp,
        [UriScheme.NetTcp] = Uri.UriSchemeNetTcp,
        [UriScheme.NetPipe] = Uri.UriSchemeNetPipe
      };
      [NotNull]
      public static readonly IReadOnlyDictionary<string, UriScheme> Name2Value = (IReadOnlyDictionary<string, UriScheme>) new Dictionary<string, UriScheme>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)
      {
        ["None"] = UriScheme.None,
        ["Any"] = UriScheme.Any,
        [Uri.UriSchemeFile] = UriScheme.File,
        [Uri.UriSchemeFtp] = UriScheme.Ftp,
        [Uri.UriSchemeGopher] = UriScheme.Gopher,
        [Uri.UriSchemeHttp] = UriScheme.Http,
        [Uri.UriSchemeHttps] = UriScheme.Https,
        [Uri.UriSchemeMailto] = UriScheme.Mailto,
        [Uri.UriSchemeNews] = UriScheme.News,
        [Uri.UriSchemeNntp] = UriScheme.Nntp,
        [Uri.UriSchemeNetTcp] = UriScheme.NetTcp,
        [Uri.UriSchemeNetPipe] = UriScheme.NetPipe
      };
    }
}
