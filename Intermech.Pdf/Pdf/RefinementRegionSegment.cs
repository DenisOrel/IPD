// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.RefinementRegionSegment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class RefinementRegionSegment : JBIG2BaseSegment
    {
      private bool m_inlineImage;
      private int m_noOfReferedToSegments;
      private int[] m_referedToSegments;

      public RefinementRegionSegment(
        JBIG2StreamDecoder streamDecoder,
        bool inlineImage,
        int[] referedToSegments,
        int noOfReferedToSegments)
        : base(streamDecoder)
      {
        this.m_inlineImage = inlineImage;
        this.m_referedToSegments = referedToSegments;
        this.m_noOfReferedToSegments = noOfReferedToSegments;
      }
    }
}
