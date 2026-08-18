// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PartGuidAllocator
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class PartGuidAllocator
{
  public Guid Allocate(PartData partData, List<RowData> rows)
  {
    byte[] sign = this.CalculateSign(partData);
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData row = rows[index];
      if (row.PartGuid != Guid.Empty && !this.IsSignValid(row.PartGuid, sign))
        row.PartGuid = Guid.Empty;
    }
    Guid guid = Guid.Empty;
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData row = rows[index];
      if (row.PartGuid != Guid.Empty)
      {
        guid = row.PartGuid;
        break;
      }
    }
    if (guid == Guid.Empty)
      guid = this.Allocate(sign);
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData row = rows[index];
      if (row.PartGuid == Guid.Empty)
        row.PartGuid = guid;
    }
    return guid;
  }

  public Guid Allocate(PartData partData) => this.Allocate(this.CalculateSign(partData));

  private Guid Allocate(byte[] sign)
  {
    byte[] numArray = new byte[16 /*0x10*/];
    this.WriteLongToBytes(DateTime.UtcNow.ToBinary(), numArray, 0);
    sign.CopyTo((Array) numArray, 8);
    return new Guid(numArray);
  }

  private bool IsSignValid(Guid partGuid, byte[] actualSign)
  {
    byte[] byteArray = partGuid.ToByteArray();
    for (int index = 0; index < 8; ++index)
    {
      if ((int) byteArray[8 + index] != (int) actualSign[index])
        return false;
    }
    return true;
  }

  private byte[] CalculateSign(PartData partData)
  {
    return new Crc64().ComputeHash(this.TextToBytes(PartKey.Calculate(partData.SectionCode, partData.TaggingMode, partData.OriginalTag, partData.OKP, partData.Name)));
  }

  private byte[] TextToBytes(string text)
  {
    byte[] bytes = new byte[text.Length * 4];
    for (int index = 0; index < text.Length; ++index)
      this.WriteCharCodeToBytes(char.ConvertToUtf32(text, index), bytes, index * 4);
    return bytes;
  }

  private void WriteLongToBytes(long value, byte[] bytes, int index)
  {
    for (int index1 = 0; index1 < 8; ++index1)
    {
      byte num = (byte) ((ulong) value & (ulong) byte.MaxValue);
      bytes[index + index1] = num;
      value >>= 8;
    }
  }

  private void WriteCharCodeToBytes(int code, byte[] bytes, int index)
  {
    for (int index1 = 0; index1 < 4; ++index1)
    {
      byte num = (byte) (code & (int) byte.MaxValue);
      bytes[index + index1] = num;
      code >>= 8;
    }
  }
}
