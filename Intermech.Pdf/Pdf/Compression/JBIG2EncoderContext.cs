// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.JBIG2EncoderContext
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;


namespace Syncfusion.Pdf.Compression
{
    internal class JBIG2EncoderContext
    {
      private const int JBIG2_MAX_CTX = 65536 /*0x010000*/;
      private const int JBIG2_OUTPUTBUFFER_SIZE = 20480 /*0x5000*/;
      private int m_a;
      private byte m_b;
      private int m_bp;
      private int m_c;
      private List<int> m_context;
      private int m_ct;
      private List<int> m_iaidctx;
      private Dictionary<int, List<int>> m_indexContext;
      private byte[] m_outbuf;
      private int m_outbufUsed;
      private Dictionary<int, byte[]> m_outputChunks;

      internal JBIG2EncoderContext()
      {
        this.IndexContext = new Dictionary<int, List<int>>(13);
        for (int key = 0; key < 13; ++key)
          this.IndexContext[key] = new List<int>(512 /*0x0200*/);
        this.Context = new List<int>(65536 /*0x010000*/);
        this.A = 32768 /*0x8000*/;
        this.C = 0;
        this.CT = 12;
        this.BP = -1;
        this.B = (byte) 0;
        this.OutbufUsed = 0;
        this.Outbuf = new byte[20480 /*0x5000*/];
        this.OutputChunks = new Dictionary<int, byte[]>();
        this.Iaidctx = new List<int>();
      }

      internal int A
      {
        get => this.m_a;
        set => this.m_a = value;
      }

      internal byte B
      {
        get => this.m_b;
        set => this.m_b = value;
      }

      internal int BP
      {
        get => this.m_bp;
        set => this.m_bp = value;
      }

      internal int C
      {
        get => this.m_c;
        set => this.m_c = value;
      }

      internal List<int> Context
      {
        get => this.m_context;
        set => this.m_context = value;
      }

      internal int CT
      {
        get => this.m_ct;
        set => this.m_ct = value;
      }

      internal List<int> Iaidctx
      {
        get => this.m_iaidctx;
        set => this.m_iaidctx = value;
      }

      internal Dictionary<int, List<int>> IndexContext
      {
        get => this.m_indexContext;
        set => this.m_indexContext = value;
      }

      internal byte[] Outbuf
      {
        get => this.m_outbuf;
        set => this.m_outbuf = value;
      }

      internal int OutbufUsed
      {
        get => this.m_outbufUsed;
        set => this.m_outbufUsed = value;
      }

      internal Dictionary<int, byte[]> OutputChunks
      {
        get => this.m_outputChunks;
        set => this.m_outputChunks = value;
      }
    }
}
