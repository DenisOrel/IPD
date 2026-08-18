// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Standard2of5
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Standard 2 of 5 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Standard2of5 : BarcodeCommon, IBarcode
{
  private string[] S25_Code = new string[10]
  {
    "11101010101110",
    "10111010101110",
    "11101110101010",
    "10101110101110",
    "11101011101010",
    "10111011101010",
    "10101011101110",
    "10101110111010",
    "11101010111010",
    "10111010111010"
  };

  public Standard2of5(string input) => this.Raw_Data = input;

  /// <summary>
  /// Encode the raw data using the Standard 2 of 5 algorithm.
  /// </summary>
  private string Encode_Standard2of5()
  {
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("ES25-1: Numeric Data Only");
    string str = "11011010";
    foreach (char ch in this.Raw_Data)
      str += this.S25_Code[int.Parse(ch.ToString())];
    return str + "1101011";
  }

  public string Encoded_Value => this.Encode_Standard2of5();
}
