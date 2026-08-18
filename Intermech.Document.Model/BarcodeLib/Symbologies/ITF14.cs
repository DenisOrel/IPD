// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.ITF14
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  ITF-14 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class ITF14 : BarcodeCommon, IBarcode
{
  private string[] ITF14_Code = new string[10]
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

  public ITF14(string input)
  {
    this.Raw_Data = input;
    this.CheckDigit();
  }

  /// <summary>Encode the raw data using the ITF-14 algorithm.</summary>
  private string Encode_ITF14()
  {
    if (this.Raw_Data.Length > 14 || this.Raw_Data.Length < 13)
      this.Error("EITF14-1: Data length invalid. (Length must be 13 or 14)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EITF14-2: Numeric data only.");
    string str1 = "1010";
    for (int index1 = 0; index1 < this.Raw_Data.Length; index1 += 2)
    {
      bool flag = true;
      string[] itF14Code1 = this.ITF14_Code;
      char ch1 = this.Raw_Data[index1];
      int index2 = int.Parse(ch1.ToString());
      string str2 = itF14Code1[index2];
      string[] itF14Code2 = this.ITF14_Code;
      ch1 = this.Raw_Data[index1 + 1];
      int index3 = int.Parse(ch1.ToString());
      string str3 = itF14Code2[index3];
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

  private void CheckDigit()
  {
    if (this.Raw_Data.Length != 13)
      return;
    int num1 = 0;
    for (int startIndex = 0; startIndex <= this.Raw_Data.Length - 1; ++startIndex)
    {
      int num2 = int.Parse(this.Raw_Data.Substring(startIndex, 1));
      num1 += num2 * (startIndex == 0 || startIndex % 2 == 0 ? 3 : 1);
    }
    int num3 = 10 - num1 % 10;
    if (num3 == 10)
      num3 = 0;
    this.Raw_Data += num3.ToString();
  }

  public string Encoded_Value => this.Encode_ITF14();
}
