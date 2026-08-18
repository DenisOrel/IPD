// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.entropy.CodedCBlk
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.entropy;

public class CodedCBlk
{
  public byte[] data;
  public int m;
  public int n;
  public int skipMSBP;

  public CodedCBlk()
  {
  }

  public CodedCBlk(int m, int n, int skipMSBP, byte[] data)
  {
    this.m = m;
    this.n = n;
    this.skipMSBP = skipMSBP;
    this.data = data;
  }

  public override string ToString()
  {
    return $"m={(object) this.m}, n={(object) this.n}, skipMSBP={(object) this.skipMSBP}, data.length={(this.data != null ? (object) string.Concat((object) this.data.Length) : (object) "(null)")}";
  }
}
