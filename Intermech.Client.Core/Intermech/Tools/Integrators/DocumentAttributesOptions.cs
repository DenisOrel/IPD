
// Type: Intermech.Tools.Integrators.DocumentAttributesOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using System;


namespace Intermech.Tools.Integrators;

public static class DocumentAttributesOptions
{
  public static DecodeAttributesOptions GetDecodeOptions(int documentType)
  {
    DecodeAttributesOptions decodeOptions = new DecodeAttributesOptions();
    if (documentType != -1)
      decodeOptions.Properties[(StringKey) "DocumentType"] = (object) documentType;
    return decodeOptions;
  }

  public static EncodeAttributesOptions GetEncodeOptions(int documentType)
  {
    return new EncodeAttributesOptions()
    {
      ReportErrorsOnly = true,
      Properties = {
        [(StringKey) "DocumentType"] = (object) documentType
      }
    };
  }

  public static int TryGetDocumentTypeFromOptions(IAttributeCodecOptions options)
  {
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    object obj;
    return options.Properties.TryGetValue((StringKey) "DocumentType", out obj) && obj is int num ? num : -1;
  }
}
