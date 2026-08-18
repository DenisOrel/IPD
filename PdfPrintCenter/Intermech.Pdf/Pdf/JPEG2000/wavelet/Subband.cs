// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.wavelet.Subband
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.image;
using System;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.wavelet;

public abstract class Subband
{
  public int anGainExp;
  public int h;
  public bool isNode;
  public int level;
  public int nomCBlkH;
  public int nomCBlkW;
  internal JPXImageCoordinates numCb;
  public int orientation;
  public int resLvl;
  public int sbandIdx;
  public int ulcx;
  public int ulcy;
  public int ulx;
  public int uly;
  public int w;
  public const int WT_ORIENT_HH = 3;
  public const int WT_ORIENT_HL = 1;
  public const int WT_ORIENT_LH = 2;
  public const int WT_ORIENT_LL = 0;

  public Subband()
  {
  }

  internal Subband(
    int w,
    int h,
    int ulcx,
    int ulcy,
    int lvls,
    WaveletFilter[] hfilters,
    WaveletFilter[] vfilters)
  {
    this.w = w;
    this.h = h;
    this.ulcx = ulcx;
    this.ulcy = ulcy;
    this.resLvl = lvls;
    Subband subband = this;
    for (int index1 = 0; index1 < lvls; ++index1)
    {
      int index2 = subband.resLvl <= hfilters.Length ? subband.resLvl - 1 : hfilters.Length - 1;
      int index3 = subband.resLvl <= vfilters.Length ? subband.resLvl - 1 : vfilters.Length - 1;
      subband = subband.split(hfilters[index2], vfilters[index3]);
    }
  }

  public virtual Subband getSubband(int x, int y)
  {
    if (x < this.ulx || y < this.uly || x >= this.ulx + this.w || y >= this.uly + this.h)
      throw new ArgumentException();
    Subband subband;
    Subband hh;
    for (subband = this; subband.isNode; subband = x >= hh.ulx ? (y >= hh.uly ? subband.HH : subband.HL) : (y >= hh.uly ? subband.LH : subband.LL))
      hh = subband.HH;
    return subband;
  }

  public virtual Subband getSubbandByIdx(int rl, int sbi)
  {
    Subband subbandByIdx = this;
    if (rl > subbandByIdx.resLvl || rl < 0)
      throw new ArgumentException("Resolution level index out of range");
    if (rl != subbandByIdx.resLvl || sbi != subbandByIdx.sbandIdx)
    {
      if (subbandByIdx.sbandIdx != 0)
        subbandByIdx = subbandByIdx.Parent;
      while (subbandByIdx.resLvl > rl)
        subbandByIdx = subbandByIdx.LL;
      while (subbandByIdx.resLvl < rl)
        subbandByIdx = subbandByIdx.Parent;
      switch (sbi)
      {
        case 0:
          return subbandByIdx;
        case 1:
          return subbandByIdx.HL;
        case 2:
          return subbandByIdx.LH;
        case 3:
          return subbandByIdx.HH;
      }
    }
    return subbandByIdx;
  }

  internal virtual void initChilds()
  {
    Subband ll = this.LL;
    Subband hl = this.HL;
    Subband lh = this.LH;
    Subband hh = this.HH;
    ll.level = this.level + 1;
    ll.ulcx = this.ulcx + 1 >> 1;
    ll.ulcy = this.ulcy + 1 >> 1;
    ll.ulx = this.ulx;
    ll.uly = this.uly;
    ll.w = (this.ulcx + this.w + 1 >> 1) - ll.ulcx;
    ll.h = (this.ulcy + this.h + 1 >> 1) - ll.ulcy;
    ll.resLvl = this.orientation == 0 ? this.resLvl - 1 : this.resLvl;
    ll.anGainExp = this.anGainExp;
    ll.sbandIdx = this.sbandIdx << 2;
    hl.orientation = 1;
    hl.level = ll.level;
    hl.ulcx = this.ulcx >> 1;
    hl.ulcy = ll.ulcy;
    hl.ulx = this.ulx + ll.w;
    hl.uly = this.uly;
    hl.w = (this.ulcx + this.w >> 1) - hl.ulcx;
    hl.h = ll.h;
    hl.resLvl = this.resLvl;
    hl.anGainExp = this.anGainExp + 1;
    hl.sbandIdx = (this.sbandIdx << 2) + 1;
    lh.orientation = 2;
    lh.level = ll.level;
    lh.ulcx = ll.ulcx;
    lh.ulcy = this.ulcy >> 1;
    lh.ulx = this.ulx;
    lh.uly = this.uly + ll.h;
    lh.w = ll.w;
    lh.h = (this.ulcy + this.h >> 1) - lh.ulcy;
    lh.resLvl = this.resLvl;
    lh.anGainExp = this.anGainExp + 1;
    lh.sbandIdx = (this.sbandIdx << 2) + 2;
    hh.orientation = 3;
    hh.level = ll.level;
    hh.ulcx = hl.ulcx;
    hh.ulcy = lh.ulcy;
    hh.ulx = hl.ulx;
    hh.uly = lh.uly;
    hh.w = hl.w;
    hh.h = lh.h;
    hh.resLvl = this.resLvl;
    hh.anGainExp = this.anGainExp + 2;
    hh.sbandIdx = (this.sbandIdx << 2) + 3;
  }

  public virtual Subband nextSubband()
  {
    if (this.isNode)
      throw new ArgumentException();
    switch (this.orientation)
    {
      case 0:
        Subband parent1 = this.Parent;
        return parent1 != null && parent1.resLvl == this.resLvl ? parent1.HL : (Subband) null;
      case 1:
        return this.Parent.LH;
      case 2:
        return this.Parent.HH;
      case 3:
        Subband subband1 = this;
        while (true)
        {
          switch (subband1.orientation)
          {
            case 0:
              goto label_10;
            case 1:
              goto label_13;
            case 2:
              goto label_14;
            case 3:
              subband1 = subband1.Parent;
              continue;
            default:
              goto label_16;
          }
        }
label_10:
        Subband parent2 = subband1.Parent;
        if (parent2 == null || parent2.resLvl != this.resLvl)
          return (Subband) null;
        Subband subband2 = parent2.HL;
        goto label_16;
label_13:
        subband2 = subband1.Parent.LH;
        goto label_16;
label_14:
        subband2 = subband1.Parent.HH;
label_16:
        throw new ArgumentException();
      default:
        throw new ArgumentException();
    }
  }

  internal abstract Subband split(WaveletFilter hfilter, WaveletFilter vfilter);

  public override string ToString()
  {
    return $"w={(object) this.w},h={(object) this.h},ulx={(object) this.ulx},uly={(object) this.uly},ulcx={(object) this.ulcx},ulcy={(object) this.ulcy},idx={(object) this.sbandIdx},orient={(object) this.orientation},node={(object) this.isNode},level={(object) this.level},resLvl={(object) this.resLvl},nomCBlkW={(object) this.nomCBlkW},nomCBlkH={(object) this.nomCBlkH},numCb={(object) this.numCb}";
  }

  public abstract Subband HH { get; }

  public abstract Subband HL { get; }

  internal abstract WaveletFilter HorWFilter { get; }

  public abstract Subband LH { get; }

  public abstract Subband LL { get; }

  public virtual Subband NextResLevel
  {
    get
    {
      if (this.level == 0)
        return (Subband) null;
      Subband subband = this;
      do
      {
        subband = subband.Parent;
        if (subband == null)
          return (Subband) null;
      }
      while (subband.resLvl == this.resLvl);
      Subband nextResLevel = subband.HL;
      while (nextResLevel.isNode)
        nextResLevel = nextResLevel.LL;
      return nextResLevel;
    }
  }

  public abstract Subband Parent { get; }

  internal abstract WaveletFilter VerWFilter { get; }
}
