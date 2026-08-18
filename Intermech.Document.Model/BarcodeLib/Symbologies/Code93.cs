// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Code93
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Data;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Code 93 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Code93 : BarcodeCommon, IBarcode
{
  private DataTable C93_Code = new DataTable(nameof (C93_Code));

  /// <summary>Encodes with Code93.</summary>
  /// <param name="input">Data to encode.</param>
  public Code93(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the Code 93 algorithm.</summary>
  private string Encode_Code93()
  {
    this.init_Code93();
    string str1 = this.Add_CheckDigits(this.Raw_Data);
    string str2 = this.C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();
    foreach (char ch in str1)
    {
      try
      {
        str2 += this.C93_Code.Select($"Character = '{ch.ToString()}'")[0]["Encoding"].ToString();
      }
      catch
      {
        this.Error("EC93-1: Invalid data.");
      }
    }
    string str3 = str2 + this.C93_Code.Select("Character = '*'")[0]["Encoding"].ToString() + "1";
    this.C93_Code.Clear();
    return str3;
  }

  private void init_Code93()
  {
    this.C93_Code.Rows.Clear();
    this.C93_Code.Columns.Clear();
    this.C93_Code.Columns.Add("Value");
    this.C93_Code.Columns.Add("Character");
    this.C93_Code.Columns.Add("Encoding");
    this.C93_Code.Rows.Add((object) "0", (object) "0", (object) "100010100");
    this.C93_Code.Rows.Add((object) "1", (object) "1", (object) "101001000");
    this.C93_Code.Rows.Add((object) "2", (object) "2", (object) "101000100");
    this.C93_Code.Rows.Add((object) "3", (object) "3", (object) "101000010");
    this.C93_Code.Rows.Add((object) "4", (object) "4", (object) "100101000");
    this.C93_Code.Rows.Add((object) "5", (object) "5", (object) "100100100");
    this.C93_Code.Rows.Add((object) "6", (object) "6", (object) "100100010");
    this.C93_Code.Rows.Add((object) "7", (object) "7", (object) "101010000");
    this.C93_Code.Rows.Add((object) "8", (object) "8", (object) "100010010");
    this.C93_Code.Rows.Add((object) "9", (object) "9", (object) "100001010");
    this.C93_Code.Rows.Add((object) "10", (object) "A", (object) "110101000");
    this.C93_Code.Rows.Add((object) "11", (object) "B", (object) "110100100");
    this.C93_Code.Rows.Add((object) "12", (object) "C", (object) "110100010");
    this.C93_Code.Rows.Add((object) "13", (object) "D", (object) "110010100");
    this.C93_Code.Rows.Add((object) "14", (object) "E", (object) "110010010");
    this.C93_Code.Rows.Add((object) "15", (object) "F", (object) "110001010");
    this.C93_Code.Rows.Add((object) "16", (object) "G", (object) "101101000");
    this.C93_Code.Rows.Add((object) "17", (object) "H", (object) "101100100");
    this.C93_Code.Rows.Add((object) "18", (object) "I", (object) "101100010");
    this.C93_Code.Rows.Add((object) "19", (object) "J", (object) "100110100");
    this.C93_Code.Rows.Add((object) "20", (object) "K", (object) "100011010");
    this.C93_Code.Rows.Add((object) "21", (object) "L", (object) "101011000");
    this.C93_Code.Rows.Add((object) "22", (object) "M", (object) "101001100");
    this.C93_Code.Rows.Add((object) "23", (object) "N", (object) "101000110");
    this.C93_Code.Rows.Add((object) "24", (object) "O", (object) "100101100");
    this.C93_Code.Rows.Add((object) "25", (object) "P", (object) "100010110");
    this.C93_Code.Rows.Add((object) "26", (object) "Q", (object) "110110100");
    this.C93_Code.Rows.Add((object) "27", (object) "R", (object) "110110010");
    this.C93_Code.Rows.Add((object) "28", (object) "S", (object) "110101100");
    this.C93_Code.Rows.Add((object) "29", (object) "T", (object) "110100110");
    this.C93_Code.Rows.Add((object) "30", (object) "U", (object) "110010110");
    this.C93_Code.Rows.Add((object) "31", (object) "V", (object) "110011010");
    this.C93_Code.Rows.Add((object) "32", (object) "W", (object) "101101100");
    this.C93_Code.Rows.Add((object) "33", (object) "X", (object) "101100110");
    this.C93_Code.Rows.Add((object) "34", (object) "Y", (object) "100110110");
    this.C93_Code.Rows.Add((object) "35", (object) "Z", (object) "100111010");
    this.C93_Code.Rows.Add((object) "36", (object) "-", (object) "100101110");
    this.C93_Code.Rows.Add((object) "37", (object) ".", (object) "111010100");
    this.C93_Code.Rows.Add((object) "38", (object) " ", (object) "111010010");
    this.C93_Code.Rows.Add((object) "39", (object) "$", (object) "111001010");
    this.C93_Code.Rows.Add((object) "40", (object) "/", (object) "101101110");
    this.C93_Code.Rows.Add((object) "41", (object) "+", (object) "101110110");
    this.C93_Code.Rows.Add((object) "42", (object) "%", (object) "110101110");
    this.C93_Code.Rows.Add((object) "43", (object) "(", (object) "100100110");
    this.C93_Code.Rows.Add((object) "44", (object) ")", (object) "111011010");
    this.C93_Code.Rows.Add((object) "45", (object) "#", (object) "111010110");
    this.C93_Code.Rows.Add((object) "46", (object) "@", (object) "100110010");
    this.C93_Code.Rows.Add((object) "-", (object) "*", (object) "101011110");
  }

  private string Add_CheckDigits(string input)
  {
    int[] numArray1 = new int[input.Length];
    int num1 = 1;
    for (int index = input.Length - 1; index >= 0; --index)
    {
      if (num1 > 20)
        num1 = 1;
      numArray1[index] = num1;
      ++num1;
    }
    int[] numArray2 = new int[input.Length + 1];
    int num2 = 1;
    for (int length = input.Length; length >= 0; --length)
    {
      if (num2 > 15)
        num2 = 1;
      numArray2[length] = num2;
      ++num2;
    }
    int num3 = 0;
    char ch;
    for (int index = 0; index < input.Length; ++index)
    {
      int num4 = num3;
      int num5 = numArray1[index];
      DataTable c93Code = this.C93_Code;
      ch = input[index];
      string filterExpression = $"Character = '{ch.ToString()}'";
      int num6 = int.Parse(c93Code.Select(filterExpression)[0]["Value"].ToString());
      int num7 = num5 * num6;
      num3 = num4 + num7;
    }
    int num8 = num3 % 47;
    input += this.C93_Code.Select($"Value = '{num8.ToString()}'")[0]["Character"].ToString();
    int num9 = 0;
    for (int index = 0; index < input.Length; ++index)
    {
      int num10 = num9;
      int num11 = numArray2[index];
      DataTable c93Code = this.C93_Code;
      ch = input[index];
      string filterExpression = $"Character = '{ch.ToString()}'";
      int num12 = int.Parse(c93Code.Select(filterExpression)[0]["Value"].ToString());
      int num13 = num11 * num12;
      num9 = num10 + num13;
    }
    int num14 = num9 % 47;
    input += this.C93_Code.Select($"Value = '{num14.ToString()}'")[0]["Character"].ToString();
    return input;
  }

  public string Encoded_Value => this.Encode_Code93();
}
