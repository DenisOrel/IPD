// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.SynWTFilterSpec
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis
{
    internal class SynWTFilterSpec(int nt, int nc, byte type) : ModuleSpec(nt, nc, type)
    {
      public virtual SynWTFilter[] getHFilters(int t, int c)
      {
        return ((SynWTFilter[][]) this.getSpec(t, c))[0];
      }

      public virtual SynWTFilter[] getVFilters(int t, int c)
      {
        return ((SynWTFilter[][]) this.getSpec(t, c))[1];
      }

      public virtual int getWTDataType(int t, int c)
      {
        return ((SynWTFilter[][]) this.getSpec(t, c))[0][0].DataType;
      }

      public virtual bool isReversible(int t, int c)
      {
        SynWTFilter[] hfilters = this.getHFilters(t, c);
        SynWTFilter[] vfilters = this.getVFilters(t, c);
        for (int index = hfilters.Length - 1; index >= 0; --index)
        {
          if (!hfilters[index].Reversible || !vfilters[index].Reversible)
            return false;
        }
        return true;
      }

      public override string ToString()
      {
        string str1 = $"nTiles={(object) this.nTiles}\nnComp={(object) this.nComp}\n\n";
        for (int t = 0; t < this.nTiles; ++t)
        {
          for (int c = 0; c < this.nComp; ++c)
          {
            SynWTFilter[][] spec = (SynWTFilter[][]) this.getSpec(t, c);
            string str2 = $"{str1}(t:{(object) t},c:{(object) c})\n\tH:";
            for (int index = 0; index < spec[0].Length; ++index)
              str2 = $"{str2} {(object) spec[0][index]}";
            string str3 = str2 + "\n\tV:";
            for (int index = 0; index < spec[1].Length; ++index)
              str3 = $"{str3} {(object) spec[1][index]}";
            str1 = str3 + "\n";
          }
        }
        return str1;
      }
    }
}
