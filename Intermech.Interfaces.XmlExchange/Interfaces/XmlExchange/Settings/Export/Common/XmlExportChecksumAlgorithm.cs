// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Common.XmlExportChecksumAlgorithm
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Common;

public enum XmlExportChecksumAlgorithm
{
  /// <summary>CRC32 в 16-м формате</summary>
  [CustomDescription("Checksums_AlgorithmTypeCRC32")] Crc32,
  /// <summary>MD5 в 16-м формате</summary>
  [CustomDescription("Checksums_AlgorithmTypeMD5")] Md5,
  /// <summary>CRC32 в 10-м формате</summary>
  Crc32Dec,
}
