// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.EAN13
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  EAN-13 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class EAN13 : BarcodeCommon, IBarcode
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
  private Hashtable CountryCodes = new Hashtable();

  public EAN13(string input)
  {
    this.Raw_Data = input;
    this.CheckDigit();
  }

  /// <summary>
  /// Encode the raw data using the EAN-13 algorithm. (Can include the checksum already.  If it doesnt exist in the data then it will calculate it for you.  Accepted data lengths are 12 + 1 checksum or just the 12 data digits)
  /// </summary>
  private string Encode_EAN13()
  {
    if (this.Raw_Data.Length < 12 || this.Raw_Data.Length > 13)
      this.Error("EEAN13-1: Data length invalid. (Length must be 12 or 13)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EEAN13-2: Numeric Data Only");
    string str1 = this.EAN_Pattern[int.Parse(this.Raw_Data[0].ToString())];
    string str2 = "101";
    for (int index = 0; index < 6; ++index)
    {
      if (str1[index] == 'a')
        str2 += this.EAN_CodeA[int.Parse(this.Raw_Data[index + 1].ToString())];
      if (str1[index] == 'b')
        str2 += this.EAN_CodeB[int.Parse(this.Raw_Data[index + 1].ToString())];
    }
    string str3 = str2 + "01010";
    int num = 1;
    while (num <= 5)
      str3 += this.EAN_CodeC[int.Parse(this.Raw_Data[num++ + 6].ToString())];
    int index1 = int.Parse(this.Raw_Data[this.Raw_Data.Length - 1].ToString());
    return str3 + this.EAN_CodeC[index1] + "101";
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
    this.CountryCodes.Add((object) "380", (object) "BULGARIA");
    this.CountryCodes.Add((object) "383", (object) "SLOVENIJA");
    this.CountryCodes.Add((object) "385", (object) "CROATIA");
    this.CountryCodes.Add((object) "387", (object) "BOSNIA-HERZEGOVINA");
    this.CountryCodes.Add((object) "460", (object) "RUSSIA");
    this.CountryCodes.Add((object) "461", (object) "RUSSIA");
    this.CountryCodes.Add((object) "462", (object) "RUSSIA");
    this.CountryCodes.Add((object) "463", (object) "RUSSIA");
    this.CountryCodes.Add((object) "464", (object) "RUSSIA");
    this.CountryCodes.Add((object) "465", (object) "RUSSIA");
    this.CountryCodes.Add((object) "466", (object) "RUSSIA");
    this.CountryCodes.Add((object) "467", (object) "RUSSIA");
    this.CountryCodes.Add((object) "468", (object) "RUSSIA");
    this.CountryCodes.Add((object) "469", (object) "RUSSIA");
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
    this.CountryCodes.Add((object) "615", (object) "NIGERIA");
    this.CountryCodes.Add((object) "616", (object) "KENYA");
    this.CountryCodes.Add((object) "618", (object) "IVORY COAST");
    this.CountryCodes.Add((object) "619", (object) "TUNISIA");
    this.CountryCodes.Add((object) "622", (object) "EGYPT");
    this.CountryCodes.Add((object) "625", (object) "JORDAN");
    this.CountryCodes.Add((object) "626", (object) "IRAN");
    this.CountryCodes.Add((object) "627", (object) "KUWAIT");
    this.CountryCodes.Add((object) "628", (object) "SAUDI ARABIA");
    this.CountryCodes.Add((object) "629", (object) "EMIRATES");
    this.CountryCodes.Add((object) "690", (object) "CHINA");
    this.CountryCodes.Add((object) "691", (object) "CHINA");
    this.CountryCodes.Add((object) "692", (object) "CHINA");
    this.CountryCodes.Add((object) "693", (object) "CHINA");
    this.CountryCodes.Add((object) "694", (object) "CHINA");
    this.CountryCodes.Add((object) "695", (object) "CHINA");
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
    this.CountryCodes.Add((object) "867", (object) "NORTH KOREA");
    this.CountryCodes.Add((object) "869", (object) "TURKEY");
    this.CountryCodes.Add((object) "880", (object) "SOUTH KOREA");
    this.CountryCodes.Add((object) "885", (object) "THAILAND");
    this.CountryCodes.Add((object) "888", (object) "SINGAPORE");
    this.CountryCodes.Add((object) "890", (object) "INDIA");
    this.CountryCodes.Add((object) "893", (object) "VIETNAM");
    this.CountryCodes.Add((object) "899", (object) "INDONESIA");
    this.CountryCodes.Add((object) "955", (object) "MALAYSIA");
    this.CountryCodes.Add((object) "958", (object) "MACAU");
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
      string str = this.Raw_Data.Substring(0, 12);
      int num1 = 0;
      int num2 = 0;
      for (int startIndex = 0; startIndex < str.Length; ++startIndex)
      {
        if (startIndex % 2 == 0)
          num2 += int.Parse(str.Substring(startIndex, 1));
        else
          num1 += int.Parse(str.Substring(startIndex, 1)) * 3;
      }
      int num3 = 10 - (num1 + num2) % 10;
      if (num3 == 10)
        num3 = 0;
      this.Raw_Data = str + num3.ToString()[0].ToString();
    }
    catch
    {
      this.Error("EEAN13-4: Error calculating check digit.");
    }
  }

  public string Encoded_Value => this.Encode_EAN13();
}
