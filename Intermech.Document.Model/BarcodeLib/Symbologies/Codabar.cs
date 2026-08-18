// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Codabar
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Codabar encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Codabar : BarcodeCommon, IBarcode
{
  private Hashtable Codabar_Code = new Hashtable();

  public Codabar(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the Codabar algorithm.</summary>
  private string Encode_Codabar()
  {
    if (this.Raw_Data.Length < 2)
      this.Error("ECODABAR-1: Data format invalid. (Invalid length)");
    string str1 = this.Raw_Data[0].ToString().ToUpper().Trim();
    if (!(str1 == "A") && !(str1 == "B") && !(str1 == "C") && !(str1 == "D"))
      this.Error("ECODABAR-2: Data format invalid. (Invalid START character)");
    string str2 = this.Raw_Data[this.Raw_Data.Trim().Length - 1].ToString().ToUpper().Trim();
    if (!(str2 == "A") && !(str2 == "B") && !(str2 == "C") && !(str2 == "D"))
      this.Error("ECODABAR-3: Data format invalid. (Invalid STOP character)");
    this.init_Codabar();
    string Data = this.Raw_Data;
    foreach (char key in (IEnumerable) this.Codabar_Code.Keys)
    {
      if (!BarcodeCommon.CheckNumericOnly(key.ToString()))
        Data = Data.Replace(key, '1');
    }
    if (!BarcodeCommon.CheckNumericOnly(Data))
      this.Error("ECODABAR-4: Data contains invalid  characters.");
    string str3 = "";
    foreach (char key in this.Raw_Data)
      str3 = str3 + this.Codabar_Code[(object) key].ToString() + "0";
    string str4 = str3.Remove(str3.Length - 1);
    this.Codabar_Code.Clear();
    this.Raw_Data = this.Raw_Data.Trim().Substring(1, this.RawData.Trim().Length - 2);
    return str4;
  }

  private void init_Codabar()
  {
    this.Codabar_Code.Clear();
    this.Codabar_Code.Add((object) '0', (object) "101010011");
    this.Codabar_Code.Add((object) '1', (object) "101011001");
    this.Codabar_Code.Add((object) '2', (object) "101001011");
    this.Codabar_Code.Add((object) '3', (object) "110010101");
    this.Codabar_Code.Add((object) '4', (object) "101101001");
    this.Codabar_Code.Add((object) '5', (object) "110101001");
    this.Codabar_Code.Add((object) '6', (object) "100101011");
    this.Codabar_Code.Add((object) '7', (object) "100101101");
    this.Codabar_Code.Add((object) '8', (object) "100110101");
    this.Codabar_Code.Add((object) '9', (object) "110100101");
    this.Codabar_Code.Add((object) '-', (object) "101001101");
    this.Codabar_Code.Add((object) '$', (object) "101100101");
    this.Codabar_Code.Add((object) ':', (object) "1101011011");
    this.Codabar_Code.Add((object) '/', (object) "1101101011");
    this.Codabar_Code.Add((object) '.', (object) "1101101101");
    this.Codabar_Code.Add((object) '+', (object) "101100110011");
    this.Codabar_Code.Add((object) 'A', (object) "1011001001");
    this.Codabar_Code.Add((object) 'B', (object) "1010010011");
    this.Codabar_Code.Add((object) 'C', (object) "1001001011");
    this.Codabar_Code.Add((object) 'D', (object) "1010011001");
    this.Codabar_Code.Add((object) 'a', (object) "1011001001");
    this.Codabar_Code.Add((object) 'b', (object) "1010010011");
    this.Codabar_Code.Add((object) 'c', (object) "1001001011");
    this.Codabar_Code.Add((object) 'd', (object) "1010011001");
  }

  public string Encoded_Value => this.Encode_Codabar();
}
