// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.UPCSupplement5
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  UPC Supplement-5 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class UPCSupplement5 : BarcodeCommon, IBarcode
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
  private string[] UPC_SUPP_5 = new string[10]
  {
    "bbaaa",
    "babaa",
    "baaba",
    "baaab",
    "abbaa",
    "aabba",
    "aaabb",
    "ababa",
    "abaab",
    "aabab"
  };

  public UPCSupplement5(string input) => this.Raw_Data = input;

  /// <summary>
  /// Encode the raw data using the UPC Supplemental 5-digit algorithm.
  /// </summary>
  private string Encode_UPCSupplemental_5()
  {
    if (this.Raw_Data.Length != 5)
      this.Error("EUPC-SUP5-1: Invalid data length. (Length = 5 required)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EUPCA-2: Numeric Data Only");
    int num1 = 0;
    int num2 = 0;
    for (int startIndex = 0; startIndex <= 4; startIndex += 2)
      num2 += int.Parse(this.Raw_Data.Substring(startIndex, 1)) * 3;
    for (int startIndex = 1; startIndex < 4; startIndex += 2)
      num1 += int.Parse(this.Raw_Data.Substring(startIndex, 1)) * 9;
    string str1 = this.UPC_SUPP_5[(num1 + num2) % 10];
    string str2 = "";
    int index = 0;
    foreach (char ch in str1)
    {
      str2 = index != 0 ? str2 + "01" : str2 + "1011";
      switch (ch)
      {
        case 'a':
          str2 += this.EAN_CodeA[int.Parse(this.Raw_Data[index].ToString())];
          break;
        case 'b':
          str2 += this.EAN_CodeB[int.Parse(this.Raw_Data[index].ToString())];
          break;
      }
      ++index;
    }
    return str2;
  }

  public string Encoded_Value => this.Encode_UPCSupplemental_5();
}
