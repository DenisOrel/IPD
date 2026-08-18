// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.OurStreamReader
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.IO;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class OurStreamReader : StreamReader
{
  private char[] buf;
  private int BufCount;
  private int BufIdx;
  private int BufMax;
  private bool eof;

  internal OurStreamReader(string FileName)
    : base(FileName)
  {
    this.BufIdx = this.BufCount = 0;
    this.BufMax = 4096 /*0x1000*/;
    this.buf = new char[this.BufMax + 1];
    this.eof = false;
  }

  internal int ReadLine(char[] line, int MaxChars)
  {
    int index = 0;
    while (index < MaxChars)
    {
      if (this.BufIdx >= this.BufCount)
      {
        if (!this.eof)
        {
          this.BufCount = this.Read(this.buf, 0, this.BufMax);
          if (this.BufCount < this.BufMax)
            this.eof = true;
          this.BufIdx = 0;
          if (this.BufCount == 0)
            break;
        }
        else
          break;
      }
      if (this.buf[this.BufIdx] != '\r')
      {
        line[index] = this.buf[this.BufIdx];
        ++index;
      }
      ++this.BufIdx;
      if (index > 0 && line[index - 1] == '\n')
        break;
    }
    line[index] = char.MinValue;
    return index;
  }
}
