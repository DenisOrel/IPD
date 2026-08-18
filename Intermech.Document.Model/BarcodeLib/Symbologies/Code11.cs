// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Code11
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Code 11 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Code11 : BarcodeCommon, IBarcode
{
  private string[] C11_Code = new string[12]
  {
    "101011",
    "1101011",
    "1001011",
    "1100101",
    "1011011",
    "1101101",
    "1001101",
    "1010011",
    "1101001",
    "110101",
    "101101",
    "1011001"
  };

  public Code11(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the Code 11 algorithm.</summary>
  private string Encode_Code11()
  {
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data.Replace("-", "")))
      this.Error("EC11-1: Numeric data and '-' Only");
    int num1 = 1;
    int num2 = 0;
    string rawData = this.Raw_Data;
    char ch1;
    for (int index = this.Raw_Data.Length - 1; index >= 0; --index)
    {
      if (num1 == 10)
        num1 = 1;
      if (this.Raw_Data[index] != '-')
      {
        int num3 = num2;
        ch1 = this.Raw_Data[index];
        int num4 = int.Parse(ch1.ToString()) * num1++;
        num2 = num3 + num4;
      }
      else
        num2 += 10 * num1++;
    }
    int num5 = num2 % 11;
    string str1 = rawData + num5.ToString();
    if (this.Raw_Data.Length >= 10)
    {
      int num6 = 1;
      int num7 = 0;
      for (int index = str1.Length - 1; index >= 0; --index)
      {
        if (num6 == 9)
          num6 = 1;
        if (str1[index] != '-')
        {
          int num8 = num7;
          ch1 = str1[index];
          int num9 = int.Parse(ch1.ToString()) * num6++;
          num7 = num8 + num9;
        }
        else
          num7 += 10 * num6++;
      }
      int num10 = num7 % 11;
      str1 += num10.ToString();
    }
    string str2 = "0";
    string str3 = this.C11_Code[11] + str2;
    foreach (char ch2 in str1)
    {
      int index = ch2 == '-' ? 10 : int.Parse(ch2.ToString());
      str3 = str3 + this.C11_Code[index] + str2;
    }
    return str3 + this.C11_Code[11];
  }

  public string Encoded_Value => this.Encode_Code11();
}
