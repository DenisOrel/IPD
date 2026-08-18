// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ENHMETAHEADER
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.Model;

[StructLayout(LayoutKind.Sequential)]
public class ENHMETAHEADER
{
  public int iType;
  public int nSize;
  public int rclBounds_left;
  public int rclBounds_top;
  public int rclBounds_right;
  public int rclBounds_bottom;
  public int rclFrame_left;
  public int rclFrame_top;
  public int rclFrame_right;
  public int rclFrame_bottom;
  public int dSignature;
  public int nVersion;
  public int nBytes;
  public int nRecords;
  public short nHandles;
  public short sReserved;
  public int nDescription;
  public int offDescription;
  public int nPalEntries;
  public int szlDevice_cx;
  public int szlDevice_cy;
  public int szlMillimeters_cx;
  public int szlMillimeters_cy;
  public int cbPixelFormat;
  public int offPixelFormat;
  public int bOpenGL;
  public int szlMicrometers_cx;
  public int szlMicrometers_cy;

  public void ReadFromStream(Stream stream)
  {
    BinaryReader binaryReader = new BinaryReader(stream);
    this.iType = binaryReader.ReadInt32();
    this.nSize = binaryReader.ReadInt32();
    this.rclBounds_left = binaryReader.ReadInt32();
    this.rclBounds_top = binaryReader.ReadInt32();
    this.rclBounds_right = binaryReader.ReadInt32();
    this.rclBounds_bottom = binaryReader.ReadInt32();
    this.rclFrame_left = binaryReader.ReadInt32();
    this.rclFrame_top = binaryReader.ReadInt32();
    this.rclFrame_right = binaryReader.ReadInt32();
    this.rclFrame_bottom = binaryReader.ReadInt32();
    this.dSignature = binaryReader.ReadInt32();
    this.nVersion = binaryReader.ReadInt32();
    this.nBytes = binaryReader.ReadInt32();
    this.nRecords = binaryReader.ReadInt32();
    this.nHandles = binaryReader.ReadInt16();
    this.sReserved = binaryReader.ReadInt16();
    this.nDescription = binaryReader.ReadInt32();
    this.offDescription = binaryReader.ReadInt32();
    this.nPalEntries = binaryReader.ReadInt32();
    this.szlDevice_cx = binaryReader.ReadInt32();
    this.szlDevice_cy = binaryReader.ReadInt32();
    this.szlMillimeters_cx = binaryReader.ReadInt32();
    this.szlMillimeters_cy = binaryReader.ReadInt32();
    if (this.nSize > 88)
    {
      this.cbPixelFormat = binaryReader.ReadInt32();
      this.offPixelFormat = binaryReader.ReadInt32();
      this.bOpenGL = binaryReader.ReadInt32();
      if (this.nSize > 100)
      {
        this.szlMicrometers_cx = binaryReader.ReadInt32();
        this.szlMicrometers_cy = binaryReader.ReadInt32();
      }
      else
        this.szlMicrometers_cx = this.szlMicrometers_cy = 0;
    }
    else
    {
      this.cbPixelFormat = this.offPixelFormat = this.bOpenGL = 0;
      this.szlMicrometers_cx = this.szlMicrometers_cy = 0;
    }
  }

  public void WriteToStream(Stream stream)
  {
    BinaryWriter binaryWriter = new BinaryWriter(stream);
    binaryWriter.Write(this.iType);
    binaryWriter.Write(this.nSize);
    binaryWriter.Write(this.rclBounds_left);
    binaryWriter.Write(this.rclBounds_top);
    binaryWriter.Write(this.rclBounds_right);
    binaryWriter.Write(this.rclBounds_bottom);
    binaryWriter.Write(this.rclFrame_left);
    binaryWriter.Write(this.rclFrame_top);
    binaryWriter.Write(this.rclFrame_right);
    binaryWriter.Write(this.rclFrame_bottom);
    binaryWriter.Write(this.dSignature);
    binaryWriter.Write(this.nVersion);
    binaryWriter.Write(this.nBytes);
    binaryWriter.Write(this.nRecords);
    binaryWriter.Write(this.nHandles);
    binaryWriter.Write(this.sReserved);
    binaryWriter.Write(this.nDescription);
    binaryWriter.Write(this.offDescription);
    binaryWriter.Write(this.nPalEntries);
    binaryWriter.Write(this.szlDevice_cx);
    binaryWriter.Write(this.szlDevice_cy);
    binaryWriter.Write(this.szlMillimeters_cx);
    binaryWriter.Write(this.szlMillimeters_cy);
    if (this.nSize <= 88)
      return;
    binaryWriter.Write(this.cbPixelFormat);
    binaryWriter.Write(this.offPixelFormat);
    binaryWriter.Write(this.bOpenGL);
    if (this.nSize <= 100)
      return;
    binaryWriter.Write(this.szlMicrometers_cx);
    binaryWriter.Write(this.szlMicrometers_cy);
  }

  public ENHMETAHEADER() => this.nSize = 0;

  public ENHMETAHEADER(Stream metaFileStream) => this.ReadFromStream(metaFileStream);
}
