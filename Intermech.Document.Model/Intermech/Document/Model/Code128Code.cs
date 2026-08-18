// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Code128Code
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.Model;

/// <summary>
/// Static tools for determining codes for individual characters in the content
/// </summary>
public static class Code128Code
{
  private const int cSHIFT = 98;
  private const int cCODEA = 101;
  private const int cCODEB = 100;
  private const int cSTARTA = 103;
  private const int cSTARTB = 104;
  private const int cSTOP = 106;

  /// <summary>
  /// Get the Code128 code value(s) to represent an ASCII character, with
  /// optional look-ahead for length optimization
  /// </summary>
  /// <param name="CharAscii">The ASCII value of the character to translate</param>
  /// <param name="LookAheadAscii">The next character in sequence (or -1 if none)</param>
  /// <param name="CurrCodeSet">The current codeset, that the returned codes need to follow;
  /// if the returned codes change that, then this value will be changed to reflect it</param>
  /// <returns>An array of integers representing the codes that need to be output to produce the
  /// given character</returns>
  public static int[] CodesForChar(int CharAscii, int LookAheadAscii, ref CodeSet CurrCodeSet)
  {
    int num = -1;
    if (!Code128Code.CharCompatibleWithCodeset(CharAscii, CurrCodeSet))
    {
      if (LookAheadAscii != -1 && !Code128Code.CharCompatibleWithCodeset(LookAheadAscii, CurrCodeSet))
      {
        switch (CurrCodeSet)
        {
          case CodeSet.CodeA:
            num = 100;
            CurrCodeSet = CodeSet.CodeB;
            break;
          case CodeSet.CodeB:
            num = 101;
            CurrCodeSet = CodeSet.CodeA;
            break;
        }
      }
      else
        num = 98;
    }
    int[] numArray;
    if (num != -1)
      numArray = new int[2]
      {
        num,
        Code128Code.CodeValueForChar(CharAscii)
      };
    else
      numArray = new int[1]
      {
        Code128Code.CodeValueForChar(CharAscii)
      };
    return numArray;
  }

  /// <summary>
  /// Tells us which codesets a given character value is allowed in
  /// </summary>
  /// <param name="CharAscii">ASCII value of character to look at</param>
  /// <returns>Which codeset(s) can be used to represent this character</returns>
  public static Code128Code.CodeSetAllowed CodesetAllowedForChar(int CharAscii)
  {
    if (CharAscii >= 32 /*0x20*/ && CharAscii <= 95)
      return Code128Code.CodeSetAllowed.CodeAorB;
    return CharAscii >= 32 /*0x20*/ ? Code128Code.CodeSetAllowed.CodeB : Code128Code.CodeSetAllowed.CodeA;
  }

  /// <summary>
  /// Determine if a character can be represented in a given codeset
  /// </summary>
  /// <param name="CharAscii">character to check for</param>
  /// <param name="currcs">codeset context to test</param>
  /// <returns>true if the codeset contains a representation for the ASCII character</returns>
  public static bool CharCompatibleWithCodeset(int CharAscii, CodeSet currcs)
  {
    Code128Code.CodeSetAllowed codeSetAllowed = Code128Code.CodesetAllowedForChar(CharAscii);
    if (codeSetAllowed == Code128Code.CodeSetAllowed.CodeAorB || codeSetAllowed == Code128Code.CodeSetAllowed.CodeA && currcs == CodeSet.CodeA)
      return true;
    return codeSetAllowed == Code128Code.CodeSetAllowed.CodeB && currcs == CodeSet.CodeB;
  }

  /// <summary>
  /// Gets the integer code128 code value for a character (assuming the appropriate code set)
  /// </summary>
  /// <param name="CharAscii">character to convert</param>
  /// <returns>code128 symbol value for the character</returns>
  public static int CodeValueForChar(int CharAscii)
  {
    return CharAscii < 32 /*0x20*/ ? CharAscii + 64 /*0x40*/ : CharAscii - 32 /*0x20*/;
  }

  /// <summary>
  /// Return the appropriate START code depending on the codeset we want to be in
  /// </summary>
  /// <param name="cs">The codeset you want to start in</param>
  /// <returns>The code128 code to start a barcode in that codeset</returns>
  public static int StartCodeForCodeSet(CodeSet cs) => cs != CodeSet.CodeA ? 104 : 103;

  /// <summary>Return the Code128 stop code</summary>
  /// <returns>the stop code</returns>
  public static int StopCode() => 106;

  /// <summary>
  /// Indicates which code sets can represent a character -- CodeA, CodeB, or either
  /// </summary>
  public enum CodeSetAllowed
  {
    CodeA,
    CodeB,
    CodeAorB,
  }
}
