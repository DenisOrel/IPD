// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Code128Content
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections;
using System.Text;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>
/// Represent the set of code values to be output into barcode form
/// </summary>
public class Code128Content
{
  private int[] mCodeList;

  /// <summary>Create content based on a string of ASCII data</summary>
  /// <param name="AsciiData">the string that should be represented</param>
  public Code128Content(string AsciiData) => this.mCodeList = this.StringToCode128(AsciiData);

  /// <summary>
  /// Provides the Code128 code values representing the object's string
  /// </summary>
  public int[] Codes => this.mCodeList;

  /// <summary>
  /// Transform the string into integers representing the Code128 codes
  /// necessary to represent it
  /// </summary>
  /// <param name="AsciiData">String to be encoded</param>
  /// <returns>Code128 representation</returns>
  private int[] StringToCode128(string AsciiData)
  {
    byte[] bytes = Encoding.ASCII.GetBytes(AsciiData);
    CodeSet bestStartSet = this.GetBestStartSet(bytes.Length != 0 ? Code128Code.CodesetAllowedForChar((int) bytes[0]) : Code128Code.CodeSetAllowed.CodeAorB, bytes.Length > 1 ? Code128Code.CodesetAllowedForChar((int) bytes[1]) : Code128Code.CodeSetAllowed.CodeAorB);
    ArrayList arrayList = new ArrayList(bytes.Length + 3);
    arrayList.Add((object) Code128Code.StartCodeForCodeSet(bestStartSet));
    for (int index = 0; index < bytes.Length; ++index)
    {
      int CharAscii = (int) bytes[index];
      int LookAheadAscii = bytes.Length > index + 1 ? (int) bytes[index + 1] : -1;
      arrayList.AddRange((ICollection) Code128Code.CodesForChar(CharAscii, LookAheadAscii, ref bestStartSet));
    }
    int num = (int) arrayList[0];
    for (int index = 1; index < arrayList.Count; ++index)
      num += index * (int) arrayList[index];
    arrayList.Add((object) (num % 103));
    arrayList.Add((object) Code128Code.StopCode());
    return arrayList.ToArray(typeof (int)) as int[];
  }

  /// <summary>
  /// Determines the best starting code set based on the the first two
  /// characters of the string to be encoded
  /// </summary>
  /// <param name="csa1">First character of input string</param>
  /// <param name="csa2">Second character of input string</param>
  /// <returns>The codeset determined to be best to start with</returns>
  private CodeSet GetBestStartSet(Code128Code.CodeSetAllowed csa1, Code128Code.CodeSetAllowed csa2)
  {
    return 0 + (csa1 == Code128Code.CodeSetAllowed.CodeA ? 1 : 0) + (csa1 == Code128Code.CodeSetAllowed.CodeB ? -1 : 0) + (csa2 == Code128Code.CodeSetAllowed.CodeA ? 1 : 0) + (csa2 == Code128Code.CodeSetAllowed.CodeB ? -1 : 0) <= 0 ? CodeSet.CodeB : CodeSet.CodeA;
  }
}
