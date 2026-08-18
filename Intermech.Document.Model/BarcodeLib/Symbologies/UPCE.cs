// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.UPCE
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  UPC-E encoding
///  Written by: Brad Barnhill
/// </summary>
internal class UPCE : BarcodeCommon, IBarcode
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
  private string[] EAN_Pattern = new string[10]
  {
    "aaaaaa",
    "aababb",
    "aabbab",
    "aabbba",
    "abaabb",
    "abbaab",
    "abbbaa",
    "ababab",
    "ababba",
    "abbaba"
  };
  private string[] UPCE_Code_0 = new string[10]
  {
    "bbbaaa",
    "bbabaa",
    "bbaaba",
    "bbaaab",
    "babbaa",
    "baabba",
    "baaabb",
    "bababa",
    "babaab",
    "baabab"
  };
  private string[] UPCE_Code_1 = new string[10]
  {
    "aaabbb",
    "aababb",
    "aabbab",
    "aabbba",
    "abaabb",
    "abbaab",
    "abbbaa",
    "ababab",
    "ababba",
    "abbaba"
  };

  /// <summary>Encodes a UPC-E symbol.</summary>
  /// <param name="input">Data to encode.</param>
  public UPCE(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the UPC-E algorithm.</summary>
  private string Encode_UPCE()
  {
    if (this.Raw_Data.Length != 6 && this.Raw_Data.Length != 8 && this.Raw_Data.Length != 12)
      this.Error("EUPCE-1: Invalid data length. (8 or 12 numbers only)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EUPCE-2: Numeric only.");
    char ch1 = this.Raw_Data[this.Raw_Data.Length - 1];
    int index1 = int.Parse(ch1.ToString());
    ch1 = this.Raw_Data[0];
    int num1 = int.Parse(ch1.ToString());
    if (this.Raw_Data.Length == 12)
    {
      string str1 = "";
      string str2 = this.Raw_Data.Substring(1, 5);
      string s = this.Raw_Data.Substring(6, 5);
      if (num1 != 0 && num1 != 1)
        this.Error("EUPCE-3: Invalid Number System (only 0 & 1 are valid)");
      if (str2.EndsWith("000") || str2.EndsWith("100") || str2.EndsWith("200") && int.Parse(s) <= 999)
      {
        string str3 = str1 + str2.Substring(0, 2) + s.Substring(2, 3);
        ch1 = str2[2];
        string str4 = ch1.ToString();
        str1 = str3 + str4;
      }
      else if (str2.EndsWith("00") && int.Parse(s) <= 99)
        str1 = str1 + str2.Substring(0, 3) + s.Substring(3, 2) + "3";
      else if (str2.EndsWith("0") && int.Parse(s) <= 9)
      {
        string str5 = str1 + str2.Substring(0, 4);
        ch1 = s[4];
        string str6 = ch1.ToString();
        str1 = str5 + str6 + "4";
      }
      else if (!str2.EndsWith("0") && int.Parse(s) <= 9 && int.Parse(s) >= 5)
      {
        string str7 = str1 + str2;
        ch1 = s[4];
        string str8 = ch1.ToString();
        str1 = str7 + str8;
      }
      else
        this.Error("EUPCE-4: Illegal UPC-A entered for conversion.  Unable to convert.");
      this.Raw_Data = str1;
    }
    string str9 = num1 != 0 ? this.UPCE_Code_1[index1] : this.UPCE_Code_0[index1];
    string str10 = "101";
    int num2 = 0;
    foreach (char ch2 in str9)
    {
      ch1 = this.Raw_Data[num2++];
      int index2 = int.Parse(ch1.ToString());
      switch (ch2)
      {
        case 'a':
          str10 += this.EAN_CodeA[index2];
          break;
        case 'b':
          str10 += this.EAN_CodeB[index2];
          break;
      }
    }
    return str10 + "01010" + "1";
  }

  public string Encoded_Value => this.Encode_UPCE();
}
