// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.decoder.DecodeHelper
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.entropy;
using Syncfusion.Pdf.JPEG2000.image;
using Syncfusion.Pdf.JPEG2000.quantization;
using Syncfusion.Pdf.JPEG2000.roi;
using Syncfusion.Pdf.JPEG2000.wavelet.synthesis;
using System;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.decoder;

internal class DecodeHelper
{
  public CBlkSizeSpec cblks;
  public CompTransfSpec cts;
  public IntegerSpec dls;
  public ModuleSpec ecopts;
  public ModuleSpec ephs;
  public ModuleSpec ers;
  internal GuardBitsSpec gbs;
  public ModuleSpec iccs;
  public IntegerSpec nls;
  public ModuleSpec pcs;
  public IntegerSpec pos;
  public ModuleSpec pphs;
  public PrecinctSizeSpec pss;
  public QuantStepSizeSpec qsss;
  public QuantTypeSpec qts;
  public MaxShiftSpec rois;
  public ModuleSpec sops;
  public SynWTFilterSpec wfs;

  public DecodeHelper(int nt, int nc)
  {
    this.qts = new QuantTypeSpec(nt, nc, (byte) 2);
    this.qsss = new QuantStepSizeSpec(nt, nc, (byte) 2);
    this.gbs = new GuardBitsSpec(nt, nc, (byte) 2);
    this.wfs = new SynWTFilterSpec(nt, nc, (byte) 2);
    this.dls = new IntegerSpec(nt, nc, (byte) 2);
    this.cts = new CompTransfSpec(nt, nc, (byte) 2);
    this.ecopts = new ModuleSpec(nt, nc, (byte) 2);
    this.ers = new ModuleSpec(nt, nc, (byte) 2);
    this.cblks = new CBlkSizeSpec(nt, nc, (byte) 2);
    this.pss = new PrecinctSizeSpec(nt, nc, (byte) 2, this.dls);
    this.nls = new IntegerSpec(nt, nc, (byte) 1);
    this.pos = new IntegerSpec(nt, nc, (byte) 1);
    this.pcs = new ModuleSpec(nt, nc, (byte) 1);
    this.sops = new ModuleSpec(nt, nc, (byte) 1);
    this.ephs = new ModuleSpec(nt, nc, (byte) 1);
    this.pphs = new ModuleSpec(nt, nc, (byte) 1);
    this.iccs = new ModuleSpec(nt, nc, (byte) 1);
    this.pphs.setDefault((object) false);
  }

  public virtual object Clone() => (object) null;

  public virtual DecodeHelper Copy
  {
    get
    {
      DecodeHelper copy = (DecodeHelper) null;
      try
      {
        copy = (DecodeHelper) this.Clone();
      }
      catch (Exception ex)
      {
      }
      copy.qts = (QuantTypeSpec) this.qts.Copy;
      copy.qsss = (QuantStepSizeSpec) this.qsss.Copy;
      copy.gbs = (GuardBitsSpec) this.gbs.Copy;
      copy.wfs = (SynWTFilterSpec) this.wfs.Copy;
      copy.dls = (IntegerSpec) this.dls.Copy;
      copy.cts = (CompTransfSpec) this.cts.Copy;
      if (this.rois != null)
        copy.rois = (MaxShiftSpec) this.rois.Copy;
      return copy;
    }
  }
}
