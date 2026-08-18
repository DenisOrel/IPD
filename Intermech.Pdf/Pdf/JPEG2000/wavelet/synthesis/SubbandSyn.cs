// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.synthesis.SubbandSyn
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.wavelet.synthesis
{
    public class SubbandSyn : Subband
    {
      public SynWTFilter hFilter;
      public int magbits;
      private SubbandSyn parent;
      private SubbandSyn subb_HH;
      private SubbandSyn subb_HL;
      private SubbandSyn subb_LH;
      private SubbandSyn subb_LL;
      public SynWTFilter vFilter;

      public SubbandSyn()
      {
      }

      internal SubbandSyn(
        int w,
        int h,
        int ulcx,
        int ulcy,
        int lvls,
        WaveletFilter[] hfilters,
        WaveletFilter[] vfilters)
        : base(w, h, ulcx, ulcy, lvls, hfilters, vfilters)
      {
      }

      internal override Subband split(WaveletFilter hfilter, WaveletFilter vfilter)
      {
        this.isNode = !this.isNode ? true : throw new ArgumentException();
        this.hFilter = (SynWTFilter) hfilter;
        this.vFilter = (SynWTFilter) vfilter;
        this.subb_LL = new SubbandSyn();
        this.subb_LH = new SubbandSyn();
        this.subb_HL = new SubbandSyn();
        this.subb_HH = new SubbandSyn();
        this.subb_LL.parent = this;
        this.subb_HL.parent = this;
        this.subb_LH.parent = this;
        this.subb_HH.parent = this;
        this.initChilds();
        return (Subband) this.subb_LL;
      }

      public override Subband HH => (Subband) this.subb_HH;

      public override Subband HL => (Subband) this.subb_HL;

      internal override WaveletFilter HorWFilter => (WaveletFilter) this.hFilter;

      public override Subband LH => (Subband) this.subb_LH;

      public override Subband LL => (Subband) this.subb_LL;

      public override Subband Parent => (Subband) this.parent;

      internal override WaveletFilter VerWFilter => (WaveletFilter) this.hFilter;
    }
}
