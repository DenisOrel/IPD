// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PrimitiveLoader
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Загрузчик-конвертер примитивов из бланка (шаблона)</summary>
[Serializable]
public class PrimitiveLoader
{
  /// <summary>Загрузчик</summary>
  public BinaryReader Reader;
  /// <summary>Сигнатура примитива</summary>
  public static string PrimSign = "PRIM";
  /// <summary></summary>
  public static int MagicSign = 1431655765 /*0x55555555*/;
  /// <summary>Сигнатура "Рабочей области"</summary>
  public static int WorkSign = 30583 /*0x7777*/;
  /// <summary>workspace with strings</summary>
  public static int WorkSignStrings = 34952 /*0x8888*/;
  /// <summary>Последняя версия формата файла</summary>
  public int Version = 274;
  /// <summary>Версия загружаемого файла</summary>
  public int LoadingVersion;
  /// <summary>Имя загружаемого файла</summary>
  public string LoadingFile;
  internal int CurrentPrimitiveSize;
  internal long CurrentPrimitiveStartPosition;
  /// <summary>Массив загружаемых примитивов</summary>
  public static Type[] RegArray = new Type[11]
  {
    typeof (PrimitiveBase),
    typeof (AutoText),
    typeof (TextField),
    typeof (PolyLinePrimitive),
    typeof (TablePrimitive),
    typeof (PictPrimitive),
    typeof (ContainerPrimitive),
    typeof (Area),
    typeof (BlankList),
    typeof (OlePrimitive),
    typeof (UserPrimitive)
  };

  /// <summary>Загрузить файл</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public void LoadFile(string fileName)
  {
    this.LoadingFile = fileName;
    this.Load((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read), (string) null);
  }

  /// <summary>Загрузить из потока</summary>
  /// <param name="stream">Поток данных</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public void Load(Stream stream, string preReadedHeaderSignature)
  {
    Stream stream1;
    if (stream.CanSeek)
    {
      stream1 = stream;
    }
    else
    {
      stream1 = (Stream) new ImChunkedStream();
      stream.CopyTo(stream1);
      stream1.Position = 0L;
    }
    this.Reader = new BinaryReader(stream1, Encoding.GetEncoding(1251));
    try
    {
      this.Load(preReadedHeaderSignature);
    }
    finally
    {
      this.Reader.Close();
    }
  }

  /// <summary>Загрузить из потока</summary>
  /// <param name="reader">Загрузчик BinaryReader</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public void LoadFromStream(BinaryReader reader, string preReadedHeaderSignature)
  {
    this.Reader = reader;
    this.Load(preReadedHeaderSignature);
  }

  /// <summary>Загрузить</summary>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public virtual void Load(string preReadedHeaderSignature)
  {
  }

  /// <summary>Прочитать сигнатуру</summary>
  /// <param name="reader">Загрузчик</param>
  /// <returns>Сигнатуру</returns>
  public static string ReadSign(BinaryReader reader)
  {
    char[] buffer = new char[4];
    reader.Read(buffer, 0, 4);
    return new string(buffer);
  }

  /// <summary>Загрузить примитив</summary>
  /// <param name="owner">Группа владелец примитива</param>
  /// <returns>Примитив</returns>
  public PrimitiveBase ReadPrimitive(GroupPrimitive owner)
  {
    char[] buffer = new char[4];
    this.Reader.Read(buffer, 0, 4);
    if (new string(buffer) != PrimitiveLoader.PrimSign)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_151"));
    byte index = this.Reader.ReadByte();
    int currentPrimitiveSize = this.CurrentPrimitiveSize;
    this.CurrentPrimitiveSize = this.Reader.ReadInt32();
    long primitiveStartPosition = this.CurrentPrimitiveStartPosition;
    this.CurrentPrimitiveStartPosition = this.Reader.BaseStream.Position;
    PrimitiveBase primitiveBase = (PrimitiveBase) null;
    try
    {
      if ((int) index < PrimitiveLoader.RegArray.Length)
        primitiveBase = (PrimitiveBase) Activator.CreateInstance(PrimitiveLoader.RegArray[(int) index], (object) owner);
      else
        primitiveBase = (PrimitiveBase) Activator.CreateInstance(PrimitiveLoader.RegArray[0], (object) owner);
      primitiveBase.Load(this);
    }
    finally
    {
      PrimitiveLoader.GotoEndDataBlock(this.CurrentPrimitiveStartPosition, (long) this.CurrentPrimitiveSize, this.Reader);
      this.CurrentPrimitiveSize = currentPrimitiveSize;
      this.CurrentPrimitiveStartPosition = primitiveStartPosition;
    }
    return primitiveBase;
  }

  /// <summary>Загрузить прямоугольный примитив</summary>
  /// <returns>Примитив</returns>
  public Rectangle ReadRect()
  {
    int left = this.Reader.ReadInt32();
    int num1 = this.Reader.ReadInt32();
    int num2 = this.Reader.ReadInt32();
    int num3 = this.Reader.ReadInt32();
    int top = num1;
    int right = num2;
    int bottom = num3;
    return Rectangle.FromLTRB(left, top, right, bottom);
  }

  /// <summary>Прочитать строку</summary>
  /// <returns>Строку</returns>
  public string ReadString() => PrimitiveLoader.ReadString(this.Reader);

  /// <summary>Прочитать строку</summary>
  /// <param name="reader">BinaryReader</param>
  /// <returns>Строку</returns>
  public static string ReadString(BinaryReader reader)
  {
    byte count = reader.ReadByte();
    char[] buffer = new char[(int) count];
    reader.Read(buffer, 0, (int) count);
    return new string(buffer);
  }

  /// <summary>Прочитать длинную строку</summary>
  /// <returns>Строку</returns>
  public string ReadStringLong() => PrimitiveLoader.ReadStringLong(this.Reader);

  /// <summary>Зачитать длинную строку</summary>
  /// <param name="reader">BinaryReader</param>
  /// <returns>Строку</returns>
  public static string ReadStringLong(BinaryReader reader)
  {
    int count = reader.ReadInt32();
    char[] buffer = new char[count];
    reader.Read(buffer, 0, count);
    return new string(buffer);
  }

  /// <summary>Загрузить список целых чисел</summary>
  /// <param name="sl">Контейнер для целых чисел</param>
  /// <param name="reader">BinaryReader</param>
  public static void LoadIntList(List<int> sl, BinaryReader reader)
  {
    int num1 = reader.ReadInt32();
    for (int index = 0; index < num1; ++index)
    {
      int num2 = reader.ReadInt32();
      sl.Add(num2);
    }
  }

  /// <summary>Загрузить список строк</summary>
  /// <param name="sl">Контейнер для строк</param>
  /// <param name="reader">BinaryReader</param>
  /// <param name="loadingVersion">Версия файла</param>
  public static void LoadStringList(StringCollection sl, BinaryReader reader, int loadingVersion)
  {
    int num = reader.ReadInt32();
    for (int index = 0; index < num; ++index)
    {
      string str = loadingVersion < 122 ? PrimitiveLoader.ReadString(reader) : PrimitiveLoader.ReadStringLong(reader);
      sl.Add(str);
    }
  }

  /// <summary>Прочитать цвет в формате Delphi</summary>
  /// <param name="reader">BinaryReader</param>
  /// <returns>Цвет</returns>
  public static Color ReadDelphiColor(BinaryReader reader)
  {
    int red = (int) reader.ReadByte();
    int num1 = (int) reader.ReadByte();
    int num2 = (int) reader.ReadByte();
    if (reader.ReadByte() != (byte) 0)
      throw new Exception("Invalid Color");
    int green = num1;
    int blue = num2;
    return Color.FromArgb(red, green, blue);
  }

  public static void GotoEndDataBlock(
    long startPosition,
    long dataBlockLength,
    BinaryReader binReader)
  {
    int int32 = Convert.ToInt32(startPosition + dataBlockLength - binReader.BaseStream.Position);
    if (int32 == 0)
      return;
    byte[] buffer = int32 >= 0 ? new byte[int32] : throw new Exception("Document format error");
    binReader.Read(buffer, 0, int32);
  }

  internal long CurrentPrimitiveEndPosition
  {
    get => this.CurrentPrimitiveStartPosition + (long) this.CurrentPrimitiveSize;
  }

  internal bool CurrentPrimitiveIsLoaded
  {
    get => this.Reader.BaseStream.Position >= this.CurrentPrimitiveEndPosition;
  }
}
