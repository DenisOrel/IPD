// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Pharmacode
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Pharmacode encoding
///  Written by: Brad Barnhill
/// </summary>
internal class Pharmacode : BarcodeCommon, IBarcode
{
  private string _thinBar = "1";
  private string _gap = "00";
  private string _thickBar = "111";

  /// <summary>Encodes with Pharmacode.</summary>
  /// <param name="input">Data to encode.</param>
  public Pharmacode(string input)
  {
    this.Raw_Data = input;
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
    {
      this.Error("EPHARM-1: Data contains invalid  characters (non-numeric).");
    }
    else
    {
      if (this.Raw_Data.Length <= 6)
        return;
      this.Error("EPHARM-2: Data too long (invalid data input length).");
    }
  }

  /// <summary>Encode the raw data using the Pharmacode algorithm.</summary>
  private string Encode_Pharmacode()
  {
    int result;
    if (!int.TryParse(this.Raw_Data, out result))
      this.Error("EPHARM-3: Input is unparseable.");
    else if (result < 3 || result > 131070)
      this.Error("EPHARM-4: Data contains invalid  characters (invalid numeric range).");
    int num1 = 0;
    for (int y = 15; y >= 0; --y)
    {
      if ((int) Math.Pow(2.0, (double) y) < result / 2)
      {
        num1 = y;
        break;
      }
    }
    double num2 = Math.Pow(2.0, (double) (num1 + 1)) - 2.0;
    string[] strArray = new string[num1 + 1];
    int num3 = 0;
    for (int y = num1; y >= 0; --y)
    {
      double num4 = Math.Pow(2.0, (double) y);
      if ((double) result - num2 > num4)
      {
        strArray[num3++] = this._thickBar;
        num2 += num4;
      }
      else
        strArray[num3++] = this._thinBar;
    }
    string empty = string.Empty;
    foreach (string str in strArray)
    {
      if (empty != string.Empty)
        empty += this._gap;
      empty += str;
    }
    return empty;
  }

  public string Encoded_Value => this.Encode_Pharmacode();
}
