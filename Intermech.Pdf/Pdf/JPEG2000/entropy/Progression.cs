// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.entropy.Progression
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.entropy
{
    internal class Progression
    {
      public int ce;
      public int cs;
      public int lye;
      public int re;
      public int rs;
      public int type;

      public Progression(int type, int cs, int ce, int rs, int re, int lye)
      {
        this.type = type;
        this.cs = cs;
        this.ce = ce;
        this.rs = rs;
        this.re = re;
        this.lye = lye;
      }

      public override string ToString()
      {
        string str = "type= ";
        switch (this.type)
        {
          case 0:
            str += "layer, ";
            break;
          case 1:
            str += "res, ";
            break;
          case 2:
            str += "res-pos, ";
            break;
          case 3:
            str += "pos-comp, ";
            break;
          case 4:
            str += "pos-comp, ";
            break;
        }
        return $"{$"{str}comp.: {(object) this.cs}-{(object) this.ce}, "}res.: {(object) this.rs}-{(object) this.re}, layer: up to {(object) this.lye}";
      }
    }
}
