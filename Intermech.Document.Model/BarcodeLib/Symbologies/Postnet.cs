// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Postnet
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Postnet encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Postnet : BarcodeCommon, IBarcode
{
  private string[] POSTNET_Code = new string[10]
  {
    "11000",
    "00011",
    "00101",
    "00110",
    "01001",
    "01010",
    "01100",
    "10001",
    "10010",
    "10100"
  };

  public Postnet(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the PostNet algorithm.</summary>
  private string Encode_Postnet()
  {
    this.Raw_Data = this.Raw_Data.Replace("-", "");
    switch (this.Raw_Data.Length)
    {
      case 5:
      case 6:
      case 9:
      case 11:
        string str = "1";
        int num1 = 0;
        foreach (char ch in this.Raw_Data)
        {
          try
          {
            int int32 = Convert.ToInt32(ch.ToString());
            str += this.POSTNET_Code[int32];
            num1 += int32;
          }
          catch (Exception ex)
          {
            this.Error("EPOSTNET-2: Invalid data. (Numeric only) --> " + ex.Message);
          }
        }
        int num2 = num1 % 10;
        int index = 10 - (num2 == 0 ? 10 : num2);
        return str + this.POSTNET_Code[index] + "1";
      default:
        this.Error("EPOSTNET-2: Invalid data length. (5, 6, 9, or 11 digits only)");
        goto case 5;
    }
  }

  public string Encoded_Value => this.Encode_Postnet();
}
