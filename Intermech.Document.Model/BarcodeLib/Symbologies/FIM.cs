// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.FIM
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  FIM encoding
///  Written by: Brad Barnhill
/// </summary>
internal class FIM : BarcodeCommon, IBarcode
{
  private string[] FIM_Codes = new string[4]
  {
    "110010011",
    "101101101",
    "110101011",
    "111010111"
  };

  public FIM(string input)
  {
    input = input.Trim();
    switch (input)
    {
      case "A":
      case "a":
        this.Raw_Data = this.FIM_Codes[0];
        break;
      case "B":
      case "b":
        this.Raw_Data = this.FIM_Codes[1];
        break;
      case "C":
      case "c":
        this.Raw_Data = this.FIM_Codes[2];
        break;
      case "D":
      case "d":
        this.Raw_Data = this.FIM_Codes[3];
        break;
      default:
        this.Error("EFIM-1: Could not determine encoding type. (Only pass in A, B, C, or D)");
        break;
    }
  }

  public string Encode_FIM()
  {
    string str = "";
    foreach (char ch in this.RawData)
      str = $"{str}{ch.ToString()}0";
    return str.Substring(0, str.Length - 1);
  }

  public string Encoded_Value => this.Encode_FIM();

  public enum FIMTypes
  {
    FIM_A,
    FIM_B,
    FIM_C,
    FIM_D,
  }
}
