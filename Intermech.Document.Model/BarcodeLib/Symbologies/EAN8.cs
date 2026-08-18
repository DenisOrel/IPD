// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.EAN8
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  EAN-8 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class EAN8 : BarcodeCommon, IBarcode
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
  private string[] EAN_CodeC = new string[10]
  {
    "1110010",
    "1100110",
    "1101100",
    "1000010",
    "1011100",
    "1001110",
    "1010000",
    "1000100",
    "1001000",
    "1110100"
  };

  public EAN8(string input)
  {
    this.Raw_Data = input;
    this.CheckDigit();
  }

  /// <summary>Encode the raw data using the EAN-8 algorithm.</summary>
  private string Encode_EAN8()
  {
    if (this.Raw_Data.Length != 8 && this.Raw_Data.Length != 7)
      this.Error("EEAN8-1: Invalid data length. (7 or 8 numbers only)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EEAN8-2: Numeric only.");
    string str1 = "101";
    for (int index = 0; index < this.Raw_Data.Length / 2; ++index)
      str1 += this.EAN_CodeA[int.Parse(this.Raw_Data[index].ToString())];
    string str2 = str1 + "01010";
    for (int index = this.Raw_Data.Length / 2; index < this.Raw_Data.Length; ++index)
      str2 += this.EAN_CodeC[int.Parse(this.Raw_Data[index].ToString())];
    return str2 + "101";
  }

  private void CheckDigit()
  {
    if (this.Raw_Data.Length != 7)
      return;
    int num1 = 0;
    int num2 = 0;
    for (int startIndex = 0; startIndex <= 6; startIndex += 2)
      num2 += int.Parse(this.Raw_Data.Substring(startIndex, 1)) * 3;
    for (int startIndex = 1; startIndex <= 5; startIndex += 2)
      num1 += int.Parse(this.Raw_Data.Substring(startIndex, 1));
    int num3 = 10 - (num1 + num2) % 10;
    if (num3 == 10)
      num3 = 0;
    this.Raw_Data += num3.ToString();
  }

  public string Encoded_Value => this.Encode_EAN8();
}
