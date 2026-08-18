// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.UPCA
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  UPC-A encoding
///  Written by: Brad Barnhill
/// </summary>
internal class UPCA : BarcodeCommon, IBarcode
{
  private string[] UPC_CodeA = new string[10]
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
  private string[] UPC_CodeB = new string[10]
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
  private string _Country_Assigning_Manufacturer_Code = "N/A";
  private Hashtable CountryCodes = new Hashtable();

  public UPCA(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the UPC-A algorithm.</summary>
  private string Encode_UPCA()
  {
    if (this.Raw_Data.Length != 11 && this.Raw_Data.Length != 12)
      this.Error("EUPCA-1: Data length invalid. (Length must be 11 or 12)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EUPCA-2: Numeric Data Only");
    this.CheckDigit();
    string[] upcCodeA1 = this.UPC_CodeA;
    char ch = this.Raw_Data[0];
    int index1 = int.Parse(ch.ToString());
    string str1 = "101" + upcCodeA1[index1];
    for (int index2 = 0; index2 < 5; ++index2)
    {
      string str2 = str1;
      string[] upcCodeA2 = this.UPC_CodeA;
      ch = this.Raw_Data[index2 + 1];
      int index3 = int.Parse(ch.ToString());
      string str3 = upcCodeA2[index3];
      str1 = str2 + str3;
    }
    string str4 = str1 + "01010";
    int num = 0;
    while (num < 5)
    {
      string str5 = str4;
      string[] upcCodeB = this.UPC_CodeB;
      ch = this.Raw_Data[num++ + 6];
      int index4 = int.Parse(ch.ToString());
      string str6 = upcCodeB[index4];
      str4 = str5 + str6;
    }
    string str7 = str4;
    string[] upcCodeB1 = this.UPC_CodeB;
    ch = this.Raw_Data[this.Raw_Data.Length - 1];
    int index5 = int.Parse(ch.ToString());
    string str8 = upcCodeB1[index5];
    string str9 = str7 + str8 + "101";
    this.init_CountryCodes();
    string key = "0" + this.Raw_Data.Substring(0, 1);
    try
    {
      this._Country_Assigning_Manufacturer_Code = this.CountryCodes[(object) key].ToString();
    }
    catch
    {
      this.Error("EUPCA-3: Country assigning manufacturer code not found.");
    }
    finally
    {
      this.CountryCodes.Clear();
    }
    return str9;
  }

  private void init_CountryCodes()
  {
    this.CountryCodes.Clear();
    this.CountryCodes.Add((object) "00", (object) "US / CANADA");
    this.CountryCodes.Add((object) "01", (object) "US / CANADA");
    this.CountryCodes.Add((object) "02", (object) "US / CANADA");
    this.CountryCodes.Add((object) "03", (object) "US / CANADA");
    this.CountryCodes.Add((object) "04", (object) "US / CANADA");
    this.CountryCodes.Add((object) "05", (object) "US / CANADA");
    this.CountryCodes.Add((object) "06", (object) "US / CANADA");
    this.CountryCodes.Add((object) "07", (object) "US / CANADA");
    this.CountryCodes.Add((object) "08", (object) "US / CANADA");
    this.CountryCodes.Add((object) "09", (object) "US / CANADA");
    this.CountryCodes.Add((object) "10", (object) "US / CANADA");
    this.CountryCodes.Add((object) "11", (object) "US / CANADA");
    this.CountryCodes.Add((object) "12", (object) "US / CANADA");
    this.CountryCodes.Add((object) "13", (object) "US / CANADA");
    this.CountryCodes.Add((object) "20", (object) "IN STORE");
    this.CountryCodes.Add((object) "21", (object) "IN STORE");
    this.CountryCodes.Add((object) "22", (object) "IN STORE");
    this.CountryCodes.Add((object) "23", (object) "IN STORE");
    this.CountryCodes.Add((object) "24", (object) "IN STORE");
    this.CountryCodes.Add((object) "25", (object) "IN STORE");
    this.CountryCodes.Add((object) "26", (object) "IN STORE");
    this.CountryCodes.Add((object) "27", (object) "IN STORE");
    this.CountryCodes.Add((object) "28", (object) "IN STORE");
    this.CountryCodes.Add((object) "29", (object) "IN STORE");
    this.CountryCodes.Add((object) "30", (object) "FRANCE");
    this.CountryCodes.Add((object) "31", (object) "FRANCE");
    this.CountryCodes.Add((object) "32", (object) "FRANCE");
    this.CountryCodes.Add((object) "33", (object) "FRANCE");
    this.CountryCodes.Add((object) "34", (object) "FRANCE");
    this.CountryCodes.Add((object) "35", (object) "FRANCE");
    this.CountryCodes.Add((object) "36", (object) "FRANCE");
    this.CountryCodes.Add((object) "37", (object) "FRANCE");
    this.CountryCodes.Add((object) "40", (object) "GERMANY");
    this.CountryCodes.Add((object) "41", (object) "GERMANY");
    this.CountryCodes.Add((object) "42", (object) "GERMANY");
    this.CountryCodes.Add((object) "43", (object) "GERMANY");
    this.CountryCodes.Add((object) "44", (object) "GERMANY");
    this.CountryCodes.Add((object) "45", (object) "JAPAN");
    this.CountryCodes.Add((object) "46", (object) "RUSSIAN FEDERATION");
    this.CountryCodes.Add((object) "49", (object) "JAPAN (JAN-13)");
    this.CountryCodes.Add((object) "50", (object) "UNITED KINGDOM");
    this.CountryCodes.Add((object) "54", (object) "BELGIUM / LUXEMBOURG");
    this.CountryCodes.Add((object) "57", (object) "DENMARK");
    this.CountryCodes.Add((object) "64", (object) "FINLAND");
    this.CountryCodes.Add((object) "70", (object) "NORWAY");
    this.CountryCodes.Add((object) "73", (object) "SWEDEN");
    this.CountryCodes.Add((object) "76", (object) "SWITZERLAND");
    this.CountryCodes.Add((object) "80", (object) "ITALY");
    this.CountryCodes.Add((object) "81", (object) "ITALY");
    this.CountryCodes.Add((object) "82", (object) "ITALY");
    this.CountryCodes.Add((object) "83", (object) "ITALY");
    this.CountryCodes.Add((object) "84", (object) "SPAIN");
    this.CountryCodes.Add((object) "87", (object) "NETHERLANDS");
    this.CountryCodes.Add((object) "90", (object) "AUSTRIA");
    this.CountryCodes.Add((object) "91", (object) "AUSTRIA");
    this.CountryCodes.Add((object) "93", (object) "AUSTRALIA");
    this.CountryCodes.Add((object) "94", (object) "NEW ZEALAND");
    this.CountryCodes.Add((object) "99", (object) "COUPONS");
    this.CountryCodes.Add((object) "471", (object) "TAIWAN");
    this.CountryCodes.Add((object) "474", (object) "ESTONIA");
    this.CountryCodes.Add((object) "475", (object) "LATVIA");
    this.CountryCodes.Add((object) "477", (object) "LITHUANIA");
    this.CountryCodes.Add((object) "479", (object) "SRI LANKA");
    this.CountryCodes.Add((object) "480", (object) "PHILIPPINES");
    this.CountryCodes.Add((object) "482", (object) "UKRAINE");
    this.CountryCodes.Add((object) "484", (object) "MOLDOVA");
    this.CountryCodes.Add((object) "485", (object) "ARMENIA");
    this.CountryCodes.Add((object) "486", (object) "GEORGIA");
    this.CountryCodes.Add((object) "487", (object) "KAZAKHSTAN");
    this.CountryCodes.Add((object) "489", (object) "HONG KONG");
    this.CountryCodes.Add((object) "520", (object) "GREECE");
    this.CountryCodes.Add((object) "528", (object) "LEBANON");
    this.CountryCodes.Add((object) "529", (object) "CYPRUS");
    this.CountryCodes.Add((object) "531", (object) "MACEDONIA");
    this.CountryCodes.Add((object) "535", (object) "MALTA");
    this.CountryCodes.Add((object) "539", (object) "IRELAND");
    this.CountryCodes.Add((object) "560", (object) "PORTUGAL");
    this.CountryCodes.Add((object) "569", (object) "ICELAND");
    this.CountryCodes.Add((object) "590", (object) "POLAND");
    this.CountryCodes.Add((object) "594", (object) "ROMANIA");
    this.CountryCodes.Add((object) "599", (object) "HUNGARY");
    this.CountryCodes.Add((object) "600", (object) "SOUTH AFRICA");
    this.CountryCodes.Add((object) "601", (object) "SOUTH AFRICA");
    this.CountryCodes.Add((object) "609", (object) "MAURITIUS");
    this.CountryCodes.Add((object) "611", (object) "MOROCCO");
    this.CountryCodes.Add((object) "613", (object) "ALGERIA");
    this.CountryCodes.Add((object) "619", (object) "TUNISIA");
    this.CountryCodes.Add((object) "622", (object) "EGYPT");
    this.CountryCodes.Add((object) "625", (object) "JORDAN");
    this.CountryCodes.Add((object) "626", (object) "IRAN");
    this.CountryCodes.Add((object) "690", (object) "CHINA");
    this.CountryCodes.Add((object) "691", (object) "CHINA");
    this.CountryCodes.Add((object) "692", (object) "CHINA");
    this.CountryCodes.Add((object) "729", (object) "ISRAEL");
    this.CountryCodes.Add((object) "740", (object) "GUATEMALA");
    this.CountryCodes.Add((object) "741", (object) "EL SALVADOR");
    this.CountryCodes.Add((object) "742", (object) "HONDURAS");
    this.CountryCodes.Add((object) "743", (object) "NICARAGUA");
    this.CountryCodes.Add((object) "744", (object) "COSTA RICA");
    this.CountryCodes.Add((object) "746", (object) "DOMINICAN REPUBLIC");
    this.CountryCodes.Add((object) "750", (object) "MEXICO");
    this.CountryCodes.Add((object) "759", (object) "VENEZUELA");
    this.CountryCodes.Add((object) "770", (object) "COLOMBIA");
    this.CountryCodes.Add((object) "773", (object) "URUGUAY");
    this.CountryCodes.Add((object) "775", (object) "PERU");
    this.CountryCodes.Add((object) "777", (object) "BOLIVIA");
    this.CountryCodes.Add((object) "779", (object) "ARGENTINA");
    this.CountryCodes.Add((object) "780", (object) "CHILE");
    this.CountryCodes.Add((object) "784", (object) "PARAGUAY");
    this.CountryCodes.Add((object) "785", (object) "PERU");
    this.CountryCodes.Add((object) "786", (object) "ECUADOR");
    this.CountryCodes.Add((object) "789", (object) "BRAZIL");
    this.CountryCodes.Add((object) "850", (object) "CUBA");
    this.CountryCodes.Add((object) "858", (object) "SLOVAKIA");
    this.CountryCodes.Add((object) "859", (object) "CZECH REPUBLIC");
    this.CountryCodes.Add((object) "860", (object) "YUGLOSLAVIA");
    this.CountryCodes.Add((object) "869", (object) "TURKEY");
    this.CountryCodes.Add((object) "880", (object) "SOUTH KOREA");
    this.CountryCodes.Add((object) "885", (object) "THAILAND");
    this.CountryCodes.Add((object) "888", (object) "SINGAPORE");
    this.CountryCodes.Add((object) "890", (object) "INDIA");
    this.CountryCodes.Add((object) "893", (object) "VIETNAM");
    this.CountryCodes.Add((object) "899", (object) "INDONESIA");
    this.CountryCodes.Add((object) "955", (object) "MALAYSIA");
    this.CountryCodes.Add((object) "977", (object) "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
    this.CountryCodes.Add((object) "978", (object) "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
    this.CountryCodes.Add((object) "979", (object) "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
    this.CountryCodes.Add((object) "980", (object) "REFUND RECEIPTS");
    this.CountryCodes.Add((object) "981", (object) "COMMON CURRENCY COUPONS");
    this.CountryCodes.Add((object) "982", (object) "COMMON CURRENCY COUPONS");
  }

  private void CheckDigit()
  {
    try
    {
      string str = this.Raw_Data.Substring(0, 11);
      int num1 = 0;
      int num2 = 0;
      for (int startIndex = 0; startIndex < str.Length; ++startIndex)
      {
        if (startIndex % 2 == 0)
          num2 += int.Parse(str.Substring(startIndex, 1)) * 3;
        else
          num1 += int.Parse(str.Substring(startIndex, 1));
      }
      int num3 = 10 - (num1 + num2) % 10;
      if (num3 == 10)
        num3 = 0;
      this.Raw_Data = str + num3.ToString()[0].ToString();
    }
    catch
    {
      this.Error("EUPCA-4: Error calculating check digit.");
    }
  }

  public string Encoded_Value => this.Encode_UPCA();
}
