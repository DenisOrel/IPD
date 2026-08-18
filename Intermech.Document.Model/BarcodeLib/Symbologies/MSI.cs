// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.MSI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace BarcodeLib.Symbologies;

internal class MSI : BarcodeCommon, IBarcode
{
  /// <summary>
  ///  MSI encoding
  ///  Written by: Brad Barnhill
  /// </summary>
  private string[] MSI_Code = new string[10]
  {
    "100100100100",
    "100100100110",
    "100100110100",
    "100100110110",
    "100110100100",
    "100110100110",
    "100110110100",
    "100110110110",
    "110100100100",
    "110100100110"
  };
  private TYPE Encoded_Type;

  public MSI(string input, TYPE EncodedType)
  {
    this.Encoded_Type = EncodedType;
    this.Raw_Data = input;
  }

  /// <summary>Encode the raw data using the MSI algorithm.</summary>
  private string Encode_MSI()
  {
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EMSI-1: Numeric Data Only");
    string rawData = this.Raw_Data;
    char ch1;
    if (this.Encoded_Type == TYPE.MSI_Mod10 || this.Encoded_Type == TYPE.MSI_2Mod10)
    {
      string s = "";
      string str1 = "";
      for (int index = rawData.Length - 1; index >= 0; index -= 2)
      {
        ch1 = rawData[index];
        s = ch1.ToString() + s;
        if (index - 1 >= 0)
        {
          ch1 = rawData[index - 1];
          str1 = ch1.ToString() + str1;
        }
      }
      string str2 = Convert.ToString(int.Parse(s) * 2);
      int num1 = 0;
      int num2 = 0;
      foreach (char ch2 in str1)
        num1 += int.Parse(ch2.ToString());
      foreach (char ch3 in str2)
        num2 += int.Parse(ch3.ToString());
      int num3 = 10 - (num2 + num1) % 10;
      rawData += num3.ToString();
    }
    if (this.Encoded_Type == TYPE.MSI_Mod11 || this.Encoded_Type == TYPE.MSI_Mod11_Mod10)
    {
      int num4 = 0;
      int num5 = 2;
      for (int index = rawData.Length - 1; index >= 0; --index)
      {
        if (num5 > 7)
          num5 = 2;
        int num6 = num4;
        ch1 = rawData[index];
        int num7 = int.Parse(ch1.ToString()) * num5++;
        num4 = num6 + num7;
      }
      int num8 = 11 - num4 % 11;
      rawData += num8.ToString();
    }
    if (this.Encoded_Type == TYPE.MSI_2Mod10 || this.Encoded_Type == TYPE.MSI_Mod11_Mod10)
    {
      string s = "";
      string str3 = "";
      for (int index = rawData.Length - 1; index >= 0; index -= 2)
      {
        ch1 = rawData[index];
        s = ch1.ToString() + s;
        if (index - 1 >= 0)
        {
          ch1 = rawData[index - 1];
          str3 = ch1.ToString() + str3;
        }
      }
      string str4 = Convert.ToString(int.Parse(s) * 2);
      int num9 = 0;
      int num10 = 0;
      foreach (char ch4 in str3)
        num9 += int.Parse(ch4.ToString());
      foreach (char ch5 in str4)
        num10 += int.Parse(ch5.ToString());
      int num11 = 10 - (num10 + num9) % 10;
      rawData += num11.ToString();
    }
    string str = "110";
    foreach (char ch6 in rawData)
      str += this.MSI_Code[int.Parse(ch6.ToString())];
    return str + "1001";
  }

  public string Encoded_Value => this.Encode_MSI();
}
