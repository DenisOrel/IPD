
// Type: Intermech.Checksums.ChecksumAlgorithm
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Checksums
{
    /// <summary>Тип контрольной суммы</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Checksums_AlgorithmType")]
    [Category("Misc")]
    public enum ChecksumAlgorithm
    {
      /// <summary>CRC32</summary>
      [CustomDescription("Checksums_AlgorithmTypeCRC32")] Crc32,
      /// <summary>MD5</summary>
      [CustomDescription("Checksums_AlgorithmTypeMD5")] Md5,
      /// <summary>ГОСТ 34.11-2012 256 бит</summary>
      [CustomDescription("Checksums_AlgorithmTypeGOST3411_2012_256")] Gost3411_2012_256,
      /// <summary>ГОСТ 34.11-2012 512 бит</summary>
      [CustomDescription("Checksums_AlgorithmTypeGOST3411_2012_512")] Gost3411_2012_512,
    }
}
