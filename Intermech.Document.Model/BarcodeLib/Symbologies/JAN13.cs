// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.JAN13
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  JAN-13 encoding
///  Written by: Brad Barnhill
/// </summary>
internal class JAN13 : BarcodeCommon, IBarcode
{
  public JAN13(string input) => this.Raw_Data = input;

  /// <summary>Encode the raw data using the JAN-13 algorithm.</summary>
  private string Encode_JAN13()
  {
    if (!this.Raw_Data.StartsWith("49"))
      this.Error("EJAN13-1: Invalid Country Code for JAN13 (49 required)");
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EJAN13-2: Numeric Data Only");
    return new EAN13(this.Raw_Data).Encoded_Value;
  }

  public string Encoded_Value => this.Encode_JAN13();
}
