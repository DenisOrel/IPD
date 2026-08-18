// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.RectangularArrays
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class RectangularArrays
{
  internal float[][] ReturnRectangularFloatArray(int Size1, int Size2)
  {
    float[][] numArray = new float[Size1][];
    for (int index = 0; index < Size1; ++index)
      numArray[index] = new float[Size2];
    return numArray;
  }

  internal int[][] ReturnRectangularIntArray(int Size1, int Size2)
  {
    int[][] numArray = new int[Size1][];
    for (int index = 0; index < Size1; ++index)
      numArray[index] = new int[Size2];
    return numArray;
  }

  internal byte[][] ReturnRectangularSbyteArray(int Size1, int Size2)
  {
    byte[][] numArray = new byte[Size1][];
    for (int index = 0; index < Size1; ++index)
      numArray[index] = new byte[Size2];
    return numArray;
  }

  internal short[][] ReturnRectangularShortArray(int Size1, int Size2)
  {
    short[][] numArray = new short[Size1][];
    for (int index = 0; index < Size1; ++index)
      numArray[index] = new short[Size2];
    return numArray;
  }

  internal string[][] ReturnRectangularStringArray(int Size1, int Size2)
  {
    string[][] strArray = new string[Size1][];
    for (int index = 0; index < Size1; ++index)
      strArray[index] = new string[Size2];
    return strArray;
  }
}
