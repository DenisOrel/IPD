// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.JBIG2.ZLib.Inflate
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Compression.JBIG2.ZLib;

internal sealed class Inflate
{
  private const int BAD = 13;
  internal InfBlocks blocks;
  private const int BLOCKS = 7;
  private const int CHECK1 = 11;
  private const int CHECK2 = 10;
  private const int CHECK3 = 9;
  private const int CHECK4 = 8;
  private const int DICT0 = 6;
  private const int DICT1 = 5;
  private const int DICT2 = 4;
  private const int DICT3 = 3;
  private const int DICT4 = 2;
  private const int DONE = 12;
  private const int FLAG = 1;
  private static readonly byte[] mark = new byte[4]
  {
    (byte) 0,
    (byte) 0,
    (byte) SupportClass.Identity((long) byte.MaxValue),
    (byte) SupportClass.Identity((long) byte.MaxValue)
  };
  internal int marker;
  private const int MAX_WBITS = 15;
  internal int method;
  private const int METHOD = 0;
  internal int mode;
  internal long need;
  internal int nowrap;
  private const int PRESET_DICT = 32 /*0x20*/;
  internal long[] was = new long[1];
  internal int wbits;
  private const int Z_BUF_ERROR = -5;
  private const int Z_DATA_ERROR = -3;
  private const int Z_DEFLATED = 8;
  private const int Z_ERRNO = -1;
  internal const int Z_FINISH = 4;
  internal const int Z_FULL_FLUSH = 3;
  private const int Z_MEM_ERROR = -4;
  private const int Z_NEED_DICT = 2;
  internal const int Z_NO_FLUSH = 0;
  private const int Z_OK = 0;
  internal const int Z_PARTIAL_FLUSH = 1;
  private const int Z_STREAM_END = 1;
  private const int Z_STREAM_ERROR = -2;
  internal const int Z_SYNC_FLUSH = 2;
  private const int Z_VERSION_ERROR = -6;

  internal int inflate(ZStream z, int f)
  {
    if (z == null || z.istate == null || z.next_in == null)
      return -2;
    f = f == 4 ? -5 : 0;
    int r = -5;
    while (true)
    {
      switch (z.istate.mode)
      {
        case 0:
          if (z.avail_in != 0)
          {
            r = f;
            --z.avail_in;
            ++z.total_in;
            Inflate istate = z.istate;
            byte[] nextIn = z.next_in;
            int index = z.next_in_index++;
            int num1;
            int num2 = num1 = (int) nextIn[index];
            istate.method = num1;
            if ((num2 & 15) != 8)
            {
              z.istate.mode = 13;
              z.msg = "unknown compression method";
              z.istate.marker = 5;
              continue;
            }
            if ((z.istate.method >> 4) + 8 > z.istate.wbits)
            {
              z.istate.mode = 13;
              z.msg = "invalid window size";
              z.istate.marker = 5;
              continue;
            }
            z.istate.mode = 1;
            goto case 1;
          }
          goto label_10;
        case 1:
          if (z.avail_in != 0)
          {
            r = f;
            --z.avail_in;
            ++z.total_in;
            int num = (int) z.next_in[z.next_in_index++] & (int) byte.MaxValue;
            if (((z.istate.method << 8) + num) % 31 /*0x1F*/ != 0)
            {
              z.istate.mode = 13;
              z.msg = "incorrect header check";
              z.istate.marker = 5;
              continue;
            }
            if ((num & 32 /*0x20*/) == 0)
            {
              z.istate.mode = 7;
              continue;
            }
            goto label_29;
          }
          goto label_24;
        case 2:
          goto label_30;
        case 3:
          goto label_33;
        case 4:
          goto label_36;
        case 5:
          goto label_39;
        case 6:
          goto label_11;
        case 7:
          r = z.istate.blocks.proc(z, r);
          if (r != -3)
          {
            if (r == 0)
              r = f;
            if (r == 1)
            {
              r = f;
              z.istate.blocks.reset(z, z.istate.was);
              if (z.istate.nowrap != 0)
              {
                z.istate.mode = 12;
                continue;
              }
              z.istate.mode = 8;
              goto case 8;
            }
            goto label_16;
          }
          z.istate.mode = 13;
          z.istate.marker = 0;
          continue;
        case 8:
          if (z.avail_in != 0)
          {
            r = f;
            --z.avail_in;
            ++z.total_in;
            z.istate.need = (long) (((int) z.next_in[z.next_in_index++] & (int) byte.MaxValue) << 24 & -16777216 /*0xFF000000*/);
            z.istate.mode = 9;
            goto case 9;
          }
          goto label_43;
        case 9:
          if (z.avail_in != 0)
          {
            r = f;
            --z.avail_in;
            ++z.total_in;
            z.istate.need += (long) (((int) z.next_in[z.next_in_index++] & (int) byte.MaxValue) << 16 /*0x10*/) & 16711680L /*0xFF0000*/;
            z.istate.mode = 10;
            goto case 10;
          }
          goto label_46;
        case 10:
          if (z.avail_in != 0)
          {
            r = f;
            --z.avail_in;
            ++z.total_in;
            z.istate.need += (long) (((int) z.next_in[z.next_in_index++] & (int) byte.MaxValue) << 8) & 65280L;
            z.istate.mode = 11;
            goto case 11;
          }
          goto label_49;
        case 11:
          if (z.avail_in != 0)
          {
            r = f;
            --z.avail_in;
            ++z.total_in;
            z.istate.need += (long) z.next_in[z.next_in_index++] & (long) byte.MaxValue;
            if ((int) z.istate.was[0] != (int) z.istate.need)
            {
              z.istate.mode = 13;
              z.msg = "incorrect data check";
              z.istate.marker = 5;
              continue;
            }
            goto label_55;
          }
          goto label_52;
        case 12:
          goto label_56;
        case 13:
          goto label_21;
        default:
          goto label_22;
      }
    }
label_10:
    return r;
label_11:
    z.istate.mode = 13;
    z.msg = "need dictionary";
    z.istate.marker = 0;
    return -2;
label_16:
    return r;
label_21:
    return -3;
label_22:
    return -2;
label_24:
    return r;
label_29:
    z.istate.mode = 2;
label_30:
    if (z.avail_in == 0)
      return r;
    r = f;
    --z.avail_in;
    ++z.total_in;
    z.istate.need = (long) (((int) z.next_in[z.next_in_index++] & (int) byte.MaxValue) << 24 & -16777216 /*0xFF000000*/);
    z.istate.mode = 3;
label_33:
    if (z.avail_in == 0)
      return r;
    r = f;
    --z.avail_in;
    ++z.total_in;
    z.istate.need += (long) (((int) z.next_in[z.next_in_index++] & (int) byte.MaxValue) << 16 /*0x10*/) & 16711680L /*0xFF0000*/;
    z.istate.mode = 4;
label_36:
    if (z.avail_in == 0)
      return r;
    r = f;
    --z.avail_in;
    ++z.total_in;
    z.istate.need += (long) (((int) z.next_in[z.next_in_index++] & (int) byte.MaxValue) << 8) & 65280L;
    z.istate.mode = 5;
label_39:
    if (z.avail_in == 0)
      return r;
    --z.avail_in;
    ++z.total_in;
    z.istate.need += (long) z.next_in[z.next_in_index++] & (long) byte.MaxValue;
    z.adler = z.istate.need;
    z.istate.mode = 6;
    return 2;
label_43:
    return r;
label_46:
    return r;
label_49:
    return r;
label_52:
    return r;
label_55:
    z.istate.mode = 12;
label_56:
    return 1;
  }

  internal int inflateEnd(ZStream z)
  {
    if (this.blocks != null)
      this.blocks.free(z);
    this.blocks = (InfBlocks) null;
    return 0;
  }

  internal int inflateInit(ZStream z, int w)
  {
    z.msg = (string) null;
    this.blocks = (InfBlocks) null;
    this.nowrap = 0;
    if (w < 0)
    {
      w = -w;
      this.nowrap = 1;
    }
    if (w < 8 || w > 15)
    {
      this.inflateEnd(z);
      return -2;
    }
    this.wbits = w;
    z.istate.blocks = new InfBlocks(z, z.istate.nowrap != 0 ? (object) (Inflate) null : (object) this, 1 << w);
    this.inflateReset(z);
    return 0;
  }

  internal int inflateReset(ZStream z)
  {
    if (z == null || z.istate == null)
      return -2;
    z.total_in = z.total_out = 0L;
    z.msg = (string) null;
    z.istate.mode = z.istate.nowrap != 0 ? 7 : 0;
    z.istate.blocks.reset(z, (long[]) null);
    return 0;
  }

  internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
  {
    int start = 0;
    int n = dictLength;
    if (z == null || z.istate == null || z.istate.mode != 6)
      return -2;
    if (z._adler.adler32(1L, dictionary, 0, dictLength) != z.adler)
      return -3;
    z.adler = z._adler.adler32(0L, (byte[]) null, 0, 0);
    if (n >= 1 << z.istate.wbits)
    {
      n = (1 << z.istate.wbits) - 1;
      start = dictLength - n;
    }
    z.istate.blocks.set_dictionary(dictionary, start, n);
    z.istate.mode = 7;
    return 0;
  }

  internal int inflateSync(ZStream z)
  {
    if (z == null || z.istate == null)
      return -2;
    if (z.istate.mode != 13)
    {
      z.istate.mode = 13;
      z.istate.marker = 0;
    }
    int availIn = z.avail_in;
    if (availIn == 0)
      return -5;
    int nextInIndex = z.next_in_index;
    int index;
    for (index = z.istate.marker; availIn != 0 && index < 4; --availIn)
    {
      if ((int) z.next_in[nextInIndex] == (int) Inflate.mark[index])
        ++index;
      else
        index = z.next_in[nextInIndex] == (byte) 0 ? 4 - index : 0;
      ++nextInIndex;
    }
    z.total_in += (long) (nextInIndex - z.next_in_index);
    z.next_in_index = nextInIndex;
    z.avail_in = availIn;
    z.istate.marker = index;
    if (index != 4)
      return -3;
    long totalIn = z.total_in;
    long totalOut = z.total_out;
    this.inflateReset(z);
    z.total_in = totalIn;
    z.total_out = totalOut;
    z.istate.mode = 7;
    return 0;
  }

  internal int inflateSyncPoint(ZStream z)
  {
    return z != null && z.istate != null && z.istate.blocks != null ? z.istate.blocks.sync_point() : -2;
  }
}
