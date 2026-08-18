// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Telepen
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Telepen encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Telepen : BarcodeCommon, IBarcode
{
  private static Hashtable Telepen_Code = new Hashtable();
  private Telepen.StartStopCode StartCode;
  private Telepen.StartStopCode StopCode = Telepen.StartStopCode.STOP1;
  private int SwitchModeIndex;
  private int iCheckSum;

  /// <summary>Encodes data using the Telepen algorithm.</summary>
  /// <param name="input"></param>
  public Telepen(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the Telepen algorithm.</summary>
  private string Encode_Telepen()
  {
    if (Telepen.Telepen_Code.Count == 0)
      this.Init_Telepen();
    this.iCheckSum = 0;
    this.SetEncodingSequence();
    string output = Telepen.Telepen_Code[(object) this.StartCode].ToString();
    switch (this.StartCode)
    {
      case Telepen.StartStopCode.START2:
        this.EncodeNumeric(this.RawData.Substring(0, this.SwitchModeIndex), ref output);
        if (this.SwitchModeIndex < this.RawData.Length)
        {
          this.EncodeSwitchMode(ref output);
          this.EncodeASCII(this.RawData.Substring(this.SwitchModeIndex), ref output);
          break;
        }
        break;
      case Telepen.StartStopCode.START3:
        this.EncodeASCII(this.RawData.Substring(0, this.SwitchModeIndex), ref output);
        this.EncodeSwitchMode(ref output);
        this.EncodeNumeric(this.RawData.Substring(this.SwitchModeIndex), ref output);
        break;
      default:
        this.EncodeASCII(this.RawData, ref output);
        break;
    }
    output += (string) Telepen.Telepen_Code[(object) this.Calculate_Checksum(this.iCheckSum)];
    output += (string) Telepen.Telepen_Code[(object) this.StopCode];
    return output;
  }

  private void EncodeASCII(string input, ref string output)
  {
    try
    {
      foreach (char key in input)
      {
        output += (string) Telepen.Telepen_Code[(object) key];
        this.iCheckSum += Convert.ToInt32(key);
      }
    }
    catch
    {
      this.Error("ETELEPEN-1: Invalid data when encoding ASCII");
    }
  }

  private void EncodeNumeric(string input, ref string output)
  {
    try
    {
      if (input.Length % 2 > 0)
        this.Error("ETELEPEN-3: Numeric encoding attempted on odd number of characters");
      for (int startIndex = 0; startIndex < input.Length; startIndex += 2)
      {
        output += (string) Telepen.Telepen_Code[(object) Convert.ToChar(int.Parse(input.Substring(startIndex, 2)) + 27)];
        this.iCheckSum += int.Parse(input.Substring(startIndex, 2)) + 27;
      }
    }
    catch
    {
      this.Error("ETELEPEN-2: Numeric encoding failed");
    }
  }

  private void EncodeSwitchMode(ref string output)
  {
    this.iCheckSum += 16 /*0x10*/;
    output += (string) Telepen.Telepen_Code[(object) Convert.ToChar(16 /*0x10*/)];
  }

  private char Calculate_Checksum(int iCheckSum)
  {
    return Convert.ToChar((int) sbyte.MaxValue - iCheckSum % (int) sbyte.MaxValue);
  }

  private void SetEncodingSequence()
  {
    this.StartCode = Telepen.StartStopCode.START1;
    this.StopCode = Telepen.StartStopCode.STOP1;
    this.SwitchModeIndex = this.Raw_Data.Length;
    int num1 = 0;
    string rawData = this.Raw_Data;
    for (int index = 0; index < rawData.Length && char.IsNumber(rawData[index]); ++index)
      ++num1;
    if (num1 == this.Raw_Data.Length)
    {
      this.StartCode = Telepen.StartStopCode.START2;
      this.StopCode = Telepen.StartStopCode.STOP2;
      if (this.Raw_Data.Length % 2 <= 0)
        return;
      this.SwitchModeIndex = this.RawData.Length - 1;
    }
    else
    {
      int num2 = 0;
      for (int index = this.Raw_Data.Length - 1; index >= 0 && char.IsNumber(this.Raw_Data[index]); --index)
        ++num2;
      if (num1 < 4 && num2 < 4)
        return;
      if (num1 > num2)
      {
        this.StartCode = Telepen.StartStopCode.START2;
        this.StopCode = Telepen.StartStopCode.STOP2;
        this.SwitchModeIndex = num1 % 2 == 1 ? num1 - 1 : num1;
      }
      else
      {
        this.StartCode = Telepen.StartStopCode.START3;
        this.StopCode = Telepen.StartStopCode.STOP3;
        this.SwitchModeIndex = num2 % 2 == 1 ? this.Raw_Data.Length - num2 + 1 : this.Raw_Data.Length - num2;
      }
    }
  }

  private void Init_Telepen()
  {
    Telepen.Telepen_Code.Add((object) Convert.ToChar(0), (object) "1110111011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(1), (object) "1011101110111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(2), (object) "1110001110111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(3), (object) "1010111011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(4), (object) "1110101110111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(5), (object) "1011100011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(6), (object) "1000100011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(7), (object) "1010101110111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(8), (object) "1110111000111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(9), (object) "1011101011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(10), (object) "1110001011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(11), (object) "1010111000111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(12), (object) "1110101011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(13), (object) "1010001000111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(14), (object) "1000101000111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(15), (object) "1010101011101110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(16 /*0x10*/), (object) "1110111010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(17), (object) "1011101110001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(18), (object) "1110001110001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(19), (object) "1010111010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(20), (object) "1110101110001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(21), (object) "1011100010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(22), (object) "1000100010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(23), (object) "1010101110001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(24), (object) "1110100010001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(25), (object) "1011101010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(26), (object) "1110001010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(27), (object) "1010100010001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(28), (object) "1110101010111010");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(29), (object) "1010001010001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(30), (object) "1000101010001110");
    Telepen.Telepen_Code.Add((object) Convert.ToChar(31 /*0x1F*/), (object) "1010101010111010");
    Telepen.Telepen_Code.Add((object) ' ', (object) "1110111011100010");
    Telepen.Telepen_Code.Add((object) '!', (object) "1011101110101110");
    Telepen.Telepen_Code.Add((object) '"', (object) "1110001110101110");
    Telepen.Telepen_Code.Add((object) '#', (object) "1010111011100010");
    Telepen.Telepen_Code.Add((object) '$', (object) "1110101110101110");
    Telepen.Telepen_Code.Add((object) '%', (object) "1011100011100010");
    Telepen.Telepen_Code.Add((object) '&', (object) "1000100011100010");
    Telepen.Telepen_Code.Add((object) '\'', (object) "1010101110101110");
    Telepen.Telepen_Code.Add((object) '(', (object) "1110111000101110");
    Telepen.Telepen_Code.Add((object) ')', (object) "1011101011100010");
    Telepen.Telepen_Code.Add((object) '*', (object) "1110001011100010");
    Telepen.Telepen_Code.Add((object) '+', (object) "1010111000101110");
    Telepen.Telepen_Code.Add((object) ',', (object) "1110101011100010");
    Telepen.Telepen_Code.Add((object) '-', (object) "1010001000101110");
    Telepen.Telepen_Code.Add((object) '.', (object) "1000101000101110");
    Telepen.Telepen_Code.Add((object) '/', (object) "1010101011100010");
    Telepen.Telepen_Code.Add((object) '0', (object) "1110111010101110");
    Telepen.Telepen_Code.Add((object) '1', (object) "1011101000100010");
    Telepen.Telepen_Code.Add((object) '2', (object) "1110001000100010");
    Telepen.Telepen_Code.Add((object) '3', (object) "1010111010101110");
    Telepen.Telepen_Code.Add((object) '4', (object) "1110101000100010");
    Telepen.Telepen_Code.Add((object) '5', (object) "1011100010101110");
    Telepen.Telepen_Code.Add((object) '6', (object) "1000100010101110");
    Telepen.Telepen_Code.Add((object) '7', (object) "1010101000100010");
    Telepen.Telepen_Code.Add((object) '8', (object) "1110100010100010");
    Telepen.Telepen_Code.Add((object) '9', (object) "1011101010101110");
    Telepen.Telepen_Code.Add((object) ':', (object) "1110001010101110");
    Telepen.Telepen_Code.Add((object) ';', (object) "1010100010100010");
    Telepen.Telepen_Code.Add((object) '<', (object) "1110101010101110");
    Telepen.Telepen_Code.Add((object) '=', (object) "1010001010100010");
    Telepen.Telepen_Code.Add((object) '>', (object) "1000101010100010");
    Telepen.Telepen_Code.Add((object) '?', (object) "1010101010101110");
    Telepen.Telepen_Code.Add((object) '@', (object) "1110111011101010");
    Telepen.Telepen_Code.Add((object) 'A', (object) "1011101110111000");
    Telepen.Telepen_Code.Add((object) 'B', (object) "1110001110111000");
    Telepen.Telepen_Code.Add((object) 'C', (object) "1010111011101010");
    Telepen.Telepen_Code.Add((object) 'D', (object) "1110101110111000");
    Telepen.Telepen_Code.Add((object) 'E', (object) "1011100011101010");
    Telepen.Telepen_Code.Add((object) 'F', (object) "1000100011101010");
    Telepen.Telepen_Code.Add((object) 'G', (object) "1010101110111000");
    Telepen.Telepen_Code.Add((object) 'H', (object) "1110111000111000");
    Telepen.Telepen_Code.Add((object) 'I', (object) "1011101011101010");
    Telepen.Telepen_Code.Add((object) 'J', (object) "1110001011101010");
    Telepen.Telepen_Code.Add((object) 'K', (object) "1010111000111000");
    Telepen.Telepen_Code.Add((object) 'L', (object) "1110101011101010");
    Telepen.Telepen_Code.Add((object) 'M', (object) "1010001000111000");
    Telepen.Telepen_Code.Add((object) 'N', (object) "1000101000111000");
    Telepen.Telepen_Code.Add((object) 'O', (object) "1010101011101010");
    Telepen.Telepen_Code.Add((object) 'P', (object) "1110111010111000");
    Telepen.Telepen_Code.Add((object) 'Q', (object) "1011101110001010");
    Telepen.Telepen_Code.Add((object) 'R', (object) "1110001110001010");
    Telepen.Telepen_Code.Add((object) 'S', (object) "1010111010111000");
    Telepen.Telepen_Code.Add((object) 'T', (object) "1110101110001010");
    Telepen.Telepen_Code.Add((object) 'U', (object) "1011100010111000");
    Telepen.Telepen_Code.Add((object) 'V', (object) "1000100010111000");
    Telepen.Telepen_Code.Add((object) 'W', (object) "1010101110001010");
    Telepen.Telepen_Code.Add((object) 'X', (object) "1110100010001010");
    Telepen.Telepen_Code.Add((object) 'Y', (object) "1011101010111000");
    Telepen.Telepen_Code.Add((object) 'Z', (object) "1110001010111000");
    Telepen.Telepen_Code.Add((object) '[', (object) "1010100010001010");
    Telepen.Telepen_Code.Add((object) '\\', (object) "1110101010111000");
    Telepen.Telepen_Code.Add((object) ']', (object) "1010001010001010");
    Telepen.Telepen_Code.Add((object) '^', (object) "1000101010001010");
    Telepen.Telepen_Code.Add((object) '_', (object) "1010101010111000");
    Telepen.Telepen_Code.Add((object) '`', (object) "1110111010001000");
    Telepen.Telepen_Code.Add((object) 'a', (object) "1011101110101010");
    Telepen.Telepen_Code.Add((object) 'b', (object) "1110001110101010");
    Telepen.Telepen_Code.Add((object) 'c', (object) "1010111010001000");
    Telepen.Telepen_Code.Add((object) 'd', (object) "1110101110101010");
    Telepen.Telepen_Code.Add((object) 'e', (object) "1011100010001000");
    Telepen.Telepen_Code.Add((object) 'f', (object) "1000100010001000");
    Telepen.Telepen_Code.Add((object) 'g', (object) "1010101110101010");
    Telepen.Telepen_Code.Add((object) 'h', (object) "1110111000101010");
    Telepen.Telepen_Code.Add((object) 'i', (object) "1011101010001000");
    Telepen.Telepen_Code.Add((object) 'j', (object) "1110001010001000");
    Telepen.Telepen_Code.Add((object) 'k', (object) "1010111000101010");
    Telepen.Telepen_Code.Add((object) 'l', (object) "1110101010001000");
    Telepen.Telepen_Code.Add((object) 'm', (object) "1010001000101010");
    Telepen.Telepen_Code.Add((object) 'n', (object) "1000101000101010");
    Telepen.Telepen_Code.Add((object) 'o', (object) "1010101010001000");
    Telepen.Telepen_Code.Add((object) 'p', (object) "1110111010101010");
    Telepen.Telepen_Code.Add((object) 'q', (object) "1011101000101000");
    Telepen.Telepen_Code.Add((object) 'r', (object) "1110001000101000");
    Telepen.Telepen_Code.Add((object) 's', (object) "1010111010101010");
    Telepen.Telepen_Code.Add((object) 't', (object) "1110101000101000");
    Telepen.Telepen_Code.Add((object) 'u', (object) "1011100010101010");
    Telepen.Telepen_Code.Add((object) 'v', (object) "1000100010101010");
    Telepen.Telepen_Code.Add((object) 'w', (object) "1010101000101000");
    Telepen.Telepen_Code.Add((object) 'x', (object) "1110100010101000");
    Telepen.Telepen_Code.Add((object) 'y', (object) "1011101010101010");
    Telepen.Telepen_Code.Add((object) 'z', (object) "1110001010101010");
    Telepen.Telepen_Code.Add((object) '{', (object) "1010100010101000");
    Telepen.Telepen_Code.Add((object) '|', (object) "1110101010101010");
    Telepen.Telepen_Code.Add((object) '}', (object) "1010001010101000");
    Telepen.Telepen_Code.Add((object) '~', (object) "1000101010101000");
    Telepen.Telepen_Code.Add((object) Convert.ToChar((int) sbyte.MaxValue), (object) "1010101010101010");
    Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.START1, (object) "1010101010111000");
    Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.STOP1, (object) "1110001010101010");
    Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.START2, (object) "1010101011101000");
    Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.STOP2, (object) "1110100010101010");
    Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.START3, (object) "1010101110101000");
    Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.STOP3, (object) "1110101000101010");
  }

  public string Encoded_Value => this.Encode_Telepen();

  private enum StartStopCode
  {
    START1,
    STOP1,
    START2,
    STOP2,
    START3,
    STOP3,
  }
}
