// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Interleaved2of5
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Interleaved 2 of 5 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Interleaved2of5 : BarcodeCommon, IBarcode
{
  private string[] I25_Code = new string[10]
  {
    "NNWWN",
    "WNNNW",
    "NWNNW",
    "WWNNN",
    "NNWNW",
    "WNWNN",
    "NWWNN",
    "NNNWW",
    "WNNWN",
    "NWNWN"
  };

  public Interleaved2of5(string input) => this.Raw_Data = input;

  /// <summary>
  /// Encode the raw data using the Interleaved 2 of 5 algorithm.
  /// </summary>
  private string Encode_Interleaved2of5()
  {
    if (this.Raw_Data.Length % 2 != 0)
      this.Error("EI25-1: Data length invalid.");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EI25-2: Numeric Data Only");
    string str1 = "1010";
    for (int index1 = 0; index1 < this.Raw_Data.Length; index1 += 2)
    {
      bool flag = true;
      string[] i25Code1 = this.I25_Code;
      char ch1 = this.Raw_Data[index1];
      int index2 = int.Parse(ch1.ToString());
      string str2 = i25Code1[index2];
      string[] i25Code2 = this.I25_Code;
      ch1 = this.Raw_Data[index1 + 1];
      int index3 = int.Parse(ch1.ToString());
      string str3 = i25Code2[index3];
      string str4 = "";
      while (str2.Length > 0)
      {
        string str5 = str4;
        ch1 = str2[0];
        string str6 = ch1.ToString();
        ch1 = str3[0];
        string str7 = ch1.ToString();
        str4 = str5 + str6 + str7;
        str2 = str2.Substring(1);
        str3 = str3.Substring(1);
      }
      foreach (char ch2 in str4)
      {
        str1 = !flag ? (ch2 != 'N' ? str1 + "00" : str1 + "0") : (ch2 != 'N' ? str1 + "11" : str1 + "1");
        flag = !flag;
      }
    }
    return str1 + "1101";
  }

  public string Encoded_Value => this.Encode_Interleaved2of5();
}
