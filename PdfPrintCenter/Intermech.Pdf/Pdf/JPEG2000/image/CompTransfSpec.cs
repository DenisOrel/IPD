// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.CompTransfSpec
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image;

internal class CompTransfSpec(int nt, int nc, byte type) : ModuleSpec(nt, nc, type)
{
  public virtual bool CompTransfUsed
  {
    get
    {
      if ((int) this.def != 0)
        return true;
      if (this.tileDef != null)
      {
        for (int index = this.nTiles - 1; index >= 0; --index)
        {
          if (this.tileDef[index] != null && (int) this.tileDef[index] != 0)
            return true;
        }
      }
      return false;
    }
  }
}
