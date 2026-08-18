// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.ISBN
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  ISBN encoding
///  Written by: Brad Barnhill
/// </summary>
internal class ISBN : BarcodeCommon, IBarcode
{
  public ISBN(string input) => this.Raw_Data = input;

  /// <summary>
  /// Encode the raw data using the Bookland/ISBN algorithm.
  /// </summary>
  private string Encode_ISBN_Bookland()
  {
    if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
      this.Error("EBOOKLANDISBN-1: Numeric Data Only");
    string str = "UNKNOWN";
    if (this.Raw_Data.Length == 10 || this.Raw_Data.Length == 9)
    {
      if (this.Raw_Data.Length == 10)
        this.Raw_Data = this.Raw_Data.Remove(9, 1);
      this.Raw_Data = "978" + this.Raw_Data;
      str = nameof (ISBN);
    }
    else if (this.Raw_Data.Length == 12 && this.Raw_Data.StartsWith("978"))
      str = "BOOKLAND-NOCHECKDIGIT";
    else if (this.Raw_Data.Length == 13 && this.Raw_Data.StartsWith("978"))
    {
      str = "BOOKLAND-CHECKDIGIT";
      this.Raw_Data = this.Raw_Data.Remove(12, 1);
    }
    if (str == "UNKNOWN")
      this.Error("EBOOKLANDISBN-2: Invalid input.  Must start with 978 and be length must be 9, 10, 12, 13 characters.");
    return new EAN13(this.Raw_Data).Encoded_Value;
  }

  public string Encoded_Value => this.Encode_ISBN_Bookland();
}
