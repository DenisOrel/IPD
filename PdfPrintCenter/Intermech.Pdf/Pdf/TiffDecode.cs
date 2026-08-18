// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TiffDecode
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf;

internal class TiffDecode
{
  internal const short BigEndian = 19789;
  internal const int BigEndianVersion = 43;
  internal List<TiffDirectoryEntry> directoryEntries = new List<TiffDirectoryEntry>();
  internal const short LittleEndian = 18761;
  internal const int LittleEndianVersion = 42;
  internal TiffDirectoryEntry m_directory = new TiffDirectoryEntry();
  internal MemoryStream m_stream = new MemoryStream();
  internal TiffHeader m_tiffHeader;
  internal const short MdiLittleEndian = 20549;

  internal void SetField(int count, int offset, TiffTag tag, TiffType type)
  {
    this.directoryEntries.Add(new TiffDirectoryEntry()
    {
      DirectoryCount = count,
      DirectoryOffset = (uint) offset,
      DirectoryTag = tag,
      DirectoryType = type
    });
  }

  internal void WriteDirEntry(List<TiffDirectoryEntry> entries)
  {
    int count = entries.Count;
    this.WriteShort((short) count);
    for (int index = 0; index < count; ++index)
    {
      this.WriteShort((short) entries[index].DirectoryTag);
      this.WriteShort((short) entries[index].DirectoryType);
      this.WriteInt(entries[index].DirectoryCount);
      this.WriteInt((int) entries[index].DirectoryOffset);
    }
    this.WriteInt(0);
  }

  internal void WriteHeader(TiffHeader header)
  {
    this.WriteShort(header.m_byteOrder);
    this.WriteShort(header.m_version);
    this.WriteInt((int) header.m_dirOffset);
  }

  private void WriteInt(int value)
  {
    this.m_stream.Write(new byte[4]
    {
      (byte) value,
      (byte) (value >> 8),
      (byte) (value >> 16 /*0x10*/),
      (byte) (value >> 24)
    }, 0, 4);
  }

  private void WriteShort(short value)
  {
    this.m_stream.Write(new byte[2]
    {
      (byte) value,
      (byte) ((uint) value >> 8)
    }, 0, 2);
  }
}
