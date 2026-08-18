// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PageInformationSegment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class PageInformationSegment : JBIG2Segment
    {
      private BitOperation m_bitOperation;
      private JBIG2Image m_pageBitmap;
      private int m_pageBitmapHeight;
      private int m_pageBitmapWidth;
      private PageInformationFlags m_pageInformationFlags;
      private int m_pageStriping;
      private int m_xResolution;
      private int m_yResolution;

      internal PageInformationSegment(JBIG2StreamDecoder streamDecoder)
        : base(streamDecoder)
      {
        this.m_bitOperation = new BitOperation();
        this.m_pageInformationFlags = new PageInformationFlags();
      }

      public override void readSegment()
      {
        short[] numArray1 = new short[4];
        this.m_decoder.ReadByte(numArray1);
        this.m_pageBitmapWidth = this.m_bitOperation.GetInt32(numArray1);
        short[] numArray2 = new short[4];
        this.m_decoder.ReadByte(numArray2);
        this.m_pageBitmapHeight = this.m_bitOperation.GetInt32(numArray2);
        short[] numArray3 = new short[4];
        this.m_decoder.ReadByte(numArray3);
        this.m_xResolution = this.m_bitOperation.GetInt32(numArray3);
        short[] numArray4 = new short[4];
        this.m_decoder.ReadByte(numArray4);
        this.m_yResolution = this.m_bitOperation.GetInt32(numArray4);
        this.m_pageInformationFlags.setFlags((int) this.m_decoder.ReadByte());
        short[] numArray5 = new short[2];
        this.m_decoder.ReadByte(numArray5);
        this.m_pageStriping = this.m_bitOperation.GetInt16(numArray5);
        int flagValue = this.m_pageInformationFlags.GetFlagValue("DEFAULT_PIXEL_VALUE");
        this.m_pageBitmap = new JBIG2Image(this.m_pageBitmapWidth, this.m_pageBitmapHeight != -1 ? this.m_pageBitmapHeight : this.m_pageStriping & (int) short.MaxValue, this.m_arithmeticDecoder, this.m_huffmanDecoder, this.m_mmrDecoder);
        this.m_pageBitmap.Clear(flagValue);
      }

      internal JBIG2Image pageBitmap => this.m_pageBitmap;

      internal int pageBitmapHeight => this.m_pageBitmapHeight;

      internal PageInformationFlags pageInformationFlags => this.m_pageInformationFlags;
    }
}
