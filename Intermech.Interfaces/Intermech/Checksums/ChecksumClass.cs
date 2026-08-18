
// Type: Intermech.Checksums.ChecksumClass
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech.Checksums
{
    [Serializable]
    public class ChecksumClass
    {
      private ChecksumAlgorithm checksumAlgorithm;
      private object value;

      /// <summary>Тип контрольной суммы</summary>
      public ChecksumAlgorithm ChecksumAlgorithm => this.checksumAlgorithm;

      /// <summary>Значение контрольной суммы</summary>
      public object Value => this.value;

      public ChecksumClass(ChecksumAlgorithm checksumAlgorithm, object checksumValue)
      {
        this.checksumAlgorithm = checksumAlgorithm;
        this.value = checksumValue;
      }

      private string GetChecksumString()
      {
        if (this.checksumAlgorithm == ChecksumAlgorithm.Crc32)
          return Convert.ToString(Convert.ToInt64(this.value), 16 /*0x10*/).ToUpper();
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < ((byte[]) this.value).Length; ++index)
          stringBuilder.Append(((byte[]) this.value)[index].ToString("x2"));
        return stringBuilder.ToString().ToUpper();
      }

      /// <summary>Строковое представление значения контрольной суммы</summary>
      /// <returns></returns>
      public override string ToString() => this.GetChecksumString();
    }
}
