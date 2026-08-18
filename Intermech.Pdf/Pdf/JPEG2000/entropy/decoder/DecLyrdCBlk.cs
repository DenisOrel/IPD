// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.entropy.decoder.DecLyrdCBlk
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.entropy.decoder
{
    public class DecLyrdCBlk : CodedCBlk
    {
      public int dl;
      public int ftpIdx;
      public int h;
      public int nl;
      public int nTrunc;
      public bool prog;
      public int[] tsLengths;
      public int ulx;
      public int uly;
      public int w;

      public override string ToString()
      {
        string str1 = $"Coded code-block ({(object) this.m},{(object) this.n}): {(object) this.skipMSBP} MSB skipped, {(object) this.dl} bytes, {(object) this.nTrunc} truncation points, {(object) this.nl} layers, progressive={(object) this.prog}, ulx={(object) this.ulx}, uly={(object) this.uly}, w={(object) this.w}, h={(object) this.h}, ftpIdx={(object) this.ftpIdx}";
        if (this.tsLengths == null)
          return str1;
        string str2 = str1 + " {";
        for (int index = 0; index < this.tsLengths.Length; ++index)
          str2 = $"{str2} {(object) this.tsLengths[index]}";
        return str2 + " }";
      }
    }
}
