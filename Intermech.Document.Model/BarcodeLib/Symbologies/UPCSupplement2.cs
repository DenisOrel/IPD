// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.UPCSupplement2
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  UPC Supplement-2 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class UPCSupplement2 : BarcodeCommon, IBarcode
{
  private string[] EAN_CodeA = new string[10]
  {
    "0001101",
    "0011001",
    "0010011",
    "0111101",
    "0100011",
    "0110001",
    "0101111",
    "0111011",
    "0110111",
    "0001011"
  };
  private string[] EAN_CodeB = new string[10]
  {
    "0100111",
    "0110011",
    "0011011",
    "0100001",
    "0011101",
    "0111001",
    "0000101",
    "0010001",
    "0001001",
    "0010111"
  };
  private string[] UPC_SUPP_2 = new string[4]
  {
    "aa",
    "ab",
    "ba",
    "bb"
  };

  public UPCSupplement2(string input) => this.Raw_Data = input;

  /// <summary>
  /// Encode the raw data using the UPC Supplemental 2-digit algorithm.
  /// </summary>
  private string Encode_UPCSupplemental_2()
  {
    if (this.Raw_Data.Length != 2)
      this.Error("EUPC-SUP2-1: Invalid data length. (Length = 2 required)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EUPC-SUP2-2: Numeric Data Only");
    string str1 = "";
    try
    {
      str1 = this.UPC_SUPP_2[int.Parse(this.Raw_Data.Trim()) % 4];
    }
    catch
    {
      this.Error("EUPC-SUP2-3: Invalid Data. (Numeric only)");
    }
    string str2 = "1011";
    int index = 0;
    foreach (char ch in str1)
    {
      switch (ch)
      {
        case 'a':
          str2 += this.EAN_CodeA[int.Parse(this.Raw_Data[index].ToString())];
          break;
        case 'b':
          str2 += this.EAN_CodeB[int.Parse(this.Raw_Data[index].ToString())];
          break;
      }
      if (index++ == 0)
        str2 += "01";
    }
    return str2;
  }

  public string Encoded_Value => this.Encode_UPCSupplemental_2();
}
