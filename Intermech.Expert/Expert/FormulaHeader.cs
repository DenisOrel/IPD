// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.FormulaHeader
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.IO;

#nullable disable
namespace Intermech.Expert;

/// <summary>Header for serializing TempFormula</summary>
public class FormulaHeader
{
  public short HeaderLen = 6;
  public int Version = ExpertConsts.FormulaVersion;

  public FormulaHeader(int Version) => this.Version = Version;

  public FormulaHeader(BinaryReader br)
  {
    this.HeaderLen = br.ReadInt16();
    this.Version = br.ReadInt32();
    br.BaseStream.Position = (long) this.HeaderLen;
  }

  public static void Write(BinaryWriter bw)
  {
    bw.Write((short) 6);
    bw.Write(ExpertConsts.FormulaVersion);
  }

  public static void SaveFormula(BinaryWriter bw, TempFormula tf)
  {
    FormulaHeader.Write(bw);
    tf.Save(bw);
  }

  public static TempFormula LoadFormula(BinaryReader br)
  {
    FormulaHeader formulaHeader = new FormulaHeader(br);
    TempFormula tempFormula = new TempFormula(true);
    tempFormula.Load(br, formulaHeader.Version);
    tempFormula.Cond = tempFormula.resType == DataType.Boolean;
    return tempFormula;
  }
}
