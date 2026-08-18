
// Type: Intermech.Checksums.ChecksumAlgorithmPropertyClass
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Checksums
{
    public class ChecksumAlgorithmPropertyClass
    {
      private ChecksumAlgorithm checksumAlgorithm;

      public ChecksumAlgorithm ChecksumAlgorithm
      {
        get => this.checksumAlgorithm;
        set => this.checksumAlgorithm = value;
      }

      public ChecksumAlgorithmPropertyClass(ChecksumAlgorithm checksumAlgorithm)
      {
        this.checksumAlgorithm = checksumAlgorithm;
      }

      public override string ToString() => EnumTypeHelper.GetCaption((Enum) this.checksumAlgorithm);
    }
}
