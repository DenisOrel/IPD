// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Code39
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Code 39 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Code39 : BarcodeCommon, IBarcode
{
  private Hashtable C39_Code = new Hashtable();
  private Hashtable ExtC39_Translation = new Hashtable();
  private bool _AllowExtended;
  private bool _EnableChecksum;

  /// <summary>Encodes with Code39.</summary>
  /// <param name="input">Data to encode.</param>
  public Code39(string input) => this.Raw_Data = input;

  /// <summary>Encodes with Code39.</summary>
  /// <param name="input">Data to encode.</param>
  /// <param name="AllowExtended">Allow Extended Code 39 (Full Ascii mode).</param>
  public Code39(string input, bool AllowExtended)
  {
    this.Raw_Data = input;
    this._AllowExtended = AllowExtended;
  }

  /// <summary>Encodes with Code39.</summary>
  /// <param name="input">Data to encode.</param>
  /// <param name="AllowExtended">Allow Extended Code 39 (Full Ascii mode).</param>
  /// <param name="EnableChecksum">Whether to calculate the Mod 43 checksum and encode it into the barcode</param>
  public Code39(string input, bool AllowExtended, bool EnableChecksum)
  {
    this.Raw_Data = input;
    this._AllowExtended = AllowExtended;
    this._EnableChecksum = EnableChecksum;
  }

  /// <summary>Encode the raw data using the Code 39 algorithm.</summary>
  private string Encode_Code39()
  {
    this.init_Code39();
    this.init_ExtendedCode39();
    string strNoAstr = this.Raw_Data.Replace("*", "");
    string FormattedData = $"*{strNoAstr}{(this._EnableChecksum ? this.getChecksumChar(strNoAstr).ToString() : string.Empty)}*";
    if (this._AllowExtended)
      this.InsertExtendedCharsIfNeeded(ref FormattedData);
    string str1 = "";
    foreach (char key in FormattedData)
    {
      try
      {
        str1 += this.C39_Code[(object) key].ToString();
        str1 += "0";
      }
      catch
      {
        if (this._AllowExtended)
          this.Error("EC39-1: Invalid data.");
        else
          this.Error("EC39-1: Invalid data. (Try using Extended Code39)");
      }
    }
    string str2 = str1.Substring(0, str1.Length - 1);
    this.C39_Code.Clear();
    return str2;
  }

  private void init_Code39()
  {
    this.C39_Code.Clear();
    this.C39_Code.Add((object) '0', (object) "101001101101");
    this.C39_Code.Add((object) '1', (object) "110100101011");
    this.C39_Code.Add((object) '2', (object) "101100101011");
    this.C39_Code.Add((object) '3', (object) "110110010101");
    this.C39_Code.Add((object) '4', (object) "101001101011");
    this.C39_Code.Add((object) '5', (object) "110100110101");
    this.C39_Code.Add((object) '6', (object) "101100110101");
    this.C39_Code.Add((object) '7', (object) "101001011011");
    this.C39_Code.Add((object) '8', (object) "110100101101");
    this.C39_Code.Add((object) '9', (object) "101100101101");
    this.C39_Code.Add((object) 'A', (object) "110101001011");
    this.C39_Code.Add((object) 'B', (object) "101101001011");
    this.C39_Code.Add((object) 'C', (object) "110110100101");
    this.C39_Code.Add((object) 'D', (object) "101011001011");
    this.C39_Code.Add((object) 'E', (object) "110101100101");
    this.C39_Code.Add((object) 'F', (object) "101101100101");
    this.C39_Code.Add((object) 'G', (object) "101010011011");
    this.C39_Code.Add((object) 'H', (object) "110101001101");
    this.C39_Code.Add((object) 'I', (object) "101101001101");
    this.C39_Code.Add((object) 'J', (object) "101011001101");
    this.C39_Code.Add((object) 'K', (object) "110101010011");
    this.C39_Code.Add((object) 'L', (object) "101101010011");
    this.C39_Code.Add((object) 'M', (object) "110110101001");
    this.C39_Code.Add((object) 'N', (object) "101011010011");
    this.C39_Code.Add((object) 'O', (object) "110101101001");
    this.C39_Code.Add((object) 'P', (object) "101101101001");
    this.C39_Code.Add((object) 'Q', (object) "101010110011");
    this.C39_Code.Add((object) 'R', (object) "110101011001");
    this.C39_Code.Add((object) 'S', (object) "101101011001");
    this.C39_Code.Add((object) 'T', (object) "101011011001");
    this.C39_Code.Add((object) 'U', (object) "110010101011");
    this.C39_Code.Add((object) 'V', (object) "100110101011");
    this.C39_Code.Add((object) 'W', (object) "110011010101");
    this.C39_Code.Add((object) 'X', (object) "100101101011");
    this.C39_Code.Add((object) 'Y', (object) "110010110101");
    this.C39_Code.Add((object) 'Z', (object) "100110110101");
    this.C39_Code.Add((object) '-', (object) "100101011011");
    this.C39_Code.Add((object) '.', (object) "110010101101");
    this.C39_Code.Add((object) ' ', (object) "100110101101");
    this.C39_Code.Add((object) '$', (object) "100100100101");
    this.C39_Code.Add((object) '/', (object) "100100101001");
    this.C39_Code.Add((object) '+', (object) "100101001001");
    this.C39_Code.Add((object) '%', (object) "101001001001");
    this.C39_Code.Add((object) '*', (object) "100101101101");
  }

  private void init_ExtendedCode39()
  {
    this.ExtC39_Translation.Clear();
    this.ExtC39_Translation.Add((object) Convert.ToChar(0).ToString(), (object) "%U");
    this.ExtC39_Translation.Add((object) Convert.ToChar(1).ToString(), (object) "$A");
    this.ExtC39_Translation.Add((object) Convert.ToChar(2).ToString(), (object) "$B");
    this.ExtC39_Translation.Add((object) Convert.ToChar(3).ToString(), (object) "$C");
    this.ExtC39_Translation.Add((object) Convert.ToChar(4).ToString(), (object) "$D");
    this.ExtC39_Translation.Add((object) Convert.ToChar(5).ToString(), (object) "$E");
    this.ExtC39_Translation.Add((object) Convert.ToChar(6).ToString(), (object) "$F");
    this.ExtC39_Translation.Add((object) Convert.ToChar(7).ToString(), (object) "$G");
    this.ExtC39_Translation.Add((object) Convert.ToChar(8).ToString(), (object) "$H");
    this.ExtC39_Translation.Add((object) Convert.ToChar(9).ToString(), (object) "$I");
    this.ExtC39_Translation.Add((object) Convert.ToChar(10).ToString(), (object) "$J");
    this.ExtC39_Translation.Add((object) Convert.ToChar(11).ToString(), (object) "$K");
    this.ExtC39_Translation.Add((object) Convert.ToChar(12).ToString(), (object) "$L");
    this.ExtC39_Translation.Add((object) Convert.ToChar(13).ToString(), (object) "$M");
    this.ExtC39_Translation.Add((object) Convert.ToChar(14).ToString(), (object) "$N");
    this.ExtC39_Translation.Add((object) Convert.ToChar(15).ToString(), (object) "$O");
    this.ExtC39_Translation.Add((object) Convert.ToChar(16 /*0x10*/).ToString(), (object) "$P");
    this.ExtC39_Translation.Add((object) Convert.ToChar(17).ToString(), (object) "$Q");
    this.ExtC39_Translation.Add((object) Convert.ToChar(18).ToString(), (object) "$R");
    this.ExtC39_Translation.Add((object) Convert.ToChar(19).ToString(), (object) "$S");
    this.ExtC39_Translation.Add((object) Convert.ToChar(20).ToString(), (object) "$T");
    this.ExtC39_Translation.Add((object) Convert.ToChar(21).ToString(), (object) "$U");
    this.ExtC39_Translation.Add((object) Convert.ToChar(22).ToString(), (object) "$V");
    this.ExtC39_Translation.Add((object) Convert.ToChar(23).ToString(), (object) "$W");
    this.ExtC39_Translation.Add((object) Convert.ToChar(24).ToString(), (object) "$X");
    this.ExtC39_Translation.Add((object) Convert.ToChar(25).ToString(), (object) "$Y");
    this.ExtC39_Translation.Add((object) Convert.ToChar(26).ToString(), (object) "$Z");
    this.ExtC39_Translation.Add((object) Convert.ToChar(27).ToString(), (object) "%A");
    this.ExtC39_Translation.Add((object) Convert.ToChar(28).ToString(), (object) "%B");
    this.ExtC39_Translation.Add((object) Convert.ToChar(29).ToString(), (object) "%C");
    this.ExtC39_Translation.Add((object) Convert.ToChar(30).ToString(), (object) "%D");
    this.ExtC39_Translation.Add((object) Convert.ToChar(31 /*0x1F*/).ToString(), (object) "%E");
    this.ExtC39_Translation.Add((object) "!", (object) "/A");
    this.ExtC39_Translation.Add((object) "\"", (object) "/B");
    this.ExtC39_Translation.Add((object) "#", (object) "/C");
    this.ExtC39_Translation.Add((object) "$", (object) "/D");
    this.ExtC39_Translation.Add((object) "%", (object) "/E");
    this.ExtC39_Translation.Add((object) "&", (object) "/F");
    this.ExtC39_Translation.Add((object) "'", (object) "/G");
    this.ExtC39_Translation.Add((object) "(", (object) "/H");
    this.ExtC39_Translation.Add((object) ")", (object) "/I");
    this.ExtC39_Translation.Add((object) "*", (object) "/J");
    this.ExtC39_Translation.Add((object) "+", (object) "/K");
    this.ExtC39_Translation.Add((object) ",", (object) "/L");
    this.ExtC39_Translation.Add((object) "/", (object) "/O");
    this.ExtC39_Translation.Add((object) ":", (object) "/Z");
    this.ExtC39_Translation.Add((object) ";", (object) "%F");
    this.ExtC39_Translation.Add((object) "<", (object) "%G");
    this.ExtC39_Translation.Add((object) "=", (object) "%H");
    this.ExtC39_Translation.Add((object) ">", (object) "%I");
    this.ExtC39_Translation.Add((object) "?", (object) "%J");
    this.ExtC39_Translation.Add((object) "[", (object) "%K");
    this.ExtC39_Translation.Add((object) "\\", (object) "%L");
    this.ExtC39_Translation.Add((object) "]", (object) "%M");
    this.ExtC39_Translation.Add((object) "^", (object) "%N");
    this.ExtC39_Translation.Add((object) "_", (object) "%O");
    this.ExtC39_Translation.Add((object) "{", (object) "%P");
    this.ExtC39_Translation.Add((object) "|", (object) "%Q");
    this.ExtC39_Translation.Add((object) "}", (object) "%R");
    this.ExtC39_Translation.Add((object) "~", (object) "%S");
    this.ExtC39_Translation.Add((object) "`", (object) "%W");
    this.ExtC39_Translation.Add((object) "@", (object) "%V");
    this.ExtC39_Translation.Add((object) "a", (object) "+A");
    this.ExtC39_Translation.Add((object) "b", (object) "+B");
    this.ExtC39_Translation.Add((object) "c", (object) "+C");
    this.ExtC39_Translation.Add((object) "d", (object) "+D");
    this.ExtC39_Translation.Add((object) "e", (object) "+E");
    this.ExtC39_Translation.Add((object) "f", (object) "+F");
    this.ExtC39_Translation.Add((object) "g", (object) "+G");
    this.ExtC39_Translation.Add((object) "h", (object) "+H");
    this.ExtC39_Translation.Add((object) "i", (object) "+I");
    this.ExtC39_Translation.Add((object) "j", (object) "+J");
    this.ExtC39_Translation.Add((object) "k", (object) "+K");
    this.ExtC39_Translation.Add((object) "l", (object) "+L");
    this.ExtC39_Translation.Add((object) "m", (object) "+M");
    this.ExtC39_Translation.Add((object) "n", (object) "+N");
    this.ExtC39_Translation.Add((object) "o", (object) "+O");
    this.ExtC39_Translation.Add((object) "p", (object) "+P");
    this.ExtC39_Translation.Add((object) "q", (object) "+Q");
    this.ExtC39_Translation.Add((object) "r", (object) "+R");
    this.ExtC39_Translation.Add((object) "s", (object) "+S");
    this.ExtC39_Translation.Add((object) "t", (object) "+T");
    this.ExtC39_Translation.Add((object) "u", (object) "+U");
    this.ExtC39_Translation.Add((object) "v", (object) "+V");
    this.ExtC39_Translation.Add((object) "w", (object) "+W");
    this.ExtC39_Translation.Add((object) "x", (object) "+X");
    this.ExtC39_Translation.Add((object) "y", (object) "+Y");
    this.ExtC39_Translation.Add((object) "z", (object) "+Z");
    this.ExtC39_Translation.Add((object) Convert.ToChar((int) sbyte.MaxValue).ToString(), (object) "%T");
  }

  private void InsertExtendedCharsIfNeeded(ref string FormattedData)
  {
    string str = "";
    foreach (char key in FormattedData)
    {
      try
      {
        this.C39_Code[(object) key].ToString();
        str += key.ToString();
      }
      catch
      {
        object obj = this.ExtC39_Translation[(object) key.ToString()];
        str += obj.ToString();
      }
    }
    FormattedData = str;
  }

  private char getChecksumChar(string strNoAstr)
  {
    string str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";
    this.InsertExtendedCharsIfNeeded(ref strNoAstr);
    int num = 0;
    for (int index = 0; index < strNoAstr.Length; ++index)
      num += str.IndexOf(strNoAstr[index].ToString());
    return str[num % 43];
  }

  public string Encoded_Value => this.Encode_Code39();
}
