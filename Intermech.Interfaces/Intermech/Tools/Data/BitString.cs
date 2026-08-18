
// Type: Intermech.Tools.Data.BitString
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;


namespace Intermech.Tools.Data
{
    public class BitString
    {
      private const char OneValue = '1';
      private const char ZeroValue = '0';
      private string value;

      public BitString() => this.value = string.Empty;

      public BitString(string value)
      {
        this.value = value != null ? value : throw new ArgumentNullException(nameof (value));
      }

      public bool Read(int bitIndex)
      {
        return this.value.Length >= bitIndex + 1 && this.value[bitIndex] != '0';
      }

      public void Write(int bitIndex, bool bitValue)
      {
        int totalWidth = bitIndex + 1;
        if (this.value.Length < totalWidth)
        {
          if (!bitValue)
            return;
          this.value = this.value.PadRight(totalWidth, '0');
        }
        char ch = bitValue ? '1' : '0';
        if ((int) this.value[bitIndex] == (int) ch)
          return;
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(this.value.Length))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(this.value);
          stringBuilder[bitIndex] = bitValue ? '1' : '0';
          this.value = stringBuilder.ToString();
        }
      }

      public string Value => this.value;

      public override string ToString() => this.value;
    }
}
