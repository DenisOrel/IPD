// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.RecordNew
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.AVS.Victor;

/// <summary>  Любая запись </summary>
public class RecordNew
{
  private Stream _stream;
  public char _recordType_Avs6;
  public List<RecordNewField> _listFields = new List<RecordNewField>();
  public List<RecordNew> _listR2;
  public long _articleID = -1;
  public const int ArticleDesignationFieldID = 7;

  /// <summary> Обозначение в записи </summary>
  /// <returns></returns>
  public string Desigation() => this.FieldByType((byte) 4)._fieldText_Avs6;

  /// <summary> Наименование в записи </summary>
  /// <returns></returns>
  public string Name() => this.FieldByType((byte) 5)._fieldText_Avs6;

  /// <summary> Поле по индексу </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public RecordNewField FieldByIndex(int index)
  {
    return index < 0 || index >= this._listFields.Count ? (RecordNewField) null : this._listFields[index];
  }

  /// <summary> Текст поля по индексу </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public string FieldSByIndex(int index)
  {
    if (index < 0 || index >= this._listFields.Count)
      return "";
    RecordNewField recordNewField = this.FieldByIndex(index);
    return recordNewField == null ? "" : recordNewField._fieldText_Avs6;
  }

  /// <summary> Поле по типу </summary>
  /// <param name="fieldType"></param>
  /// <returns></returns>
  public RecordNewField FieldByType(byte fieldType)
  {
    RecordNewField recordNewField = (RecordNewField) null;
    for (int index = 0; index < this._listFields.Count; ++index)
    {
      RecordNewField listField = this._listFields[index];
      if ((int) listField._fieldType_Avs6 == (int) fieldType)
      {
        recordNewField = listField;
        break;
      }
    }
    return recordNewField;
  }

  /// <summary> Текст поля по типу </summary>
  /// <param name="fieldType"></param>
  /// <returns></returns>
  public string FieldSByType(byte fieldType)
  {
    RecordNewField recordNewField = this.FieldByType(fieldType);
    return recordNewField == null ? "" : recordNewField._fieldText_Avs6;
  }

  /// <summary> Индекс поля по его типу </summary>
  /// <param name="fieldType"></param>
  /// <returns></returns>
  public int FieldIndexByType(byte fieldType)
  {
    for (int index = 0; index < this._listFields.Count; ++index)
    {
      if ((int) this._listFields[index]._fieldType_Avs6 == (int) fieldType)
        return index;
    }
    return -1;
  }

  /// <summary> Поле по его имени </summary>
  /// <param name="fieldType"></param>
  /// <returns></returns>
  public RecordNewField FieldByName(string name, AVS6_From_Avs6Main.TypeListFields typeListFields)
  {
    if (name == "")
      return (RecordNewField) null;
    byte fieldType = AVS6_From_Avs6Main.FieldTypeByName(typeListFields, name);
    return fieldType == (byte) 0 ? (RecordNewField) null : this.FieldByType(fieldType);
  }

  /// <summary> Текст поля по его имени </summary>
  /// <param name="name"></param>
  /// <param name="typeListFields"></param>
  /// <param name="aVS6_Fields"></param>
  /// <returns></returns>
  public string FieldSByName(string name, AVS6_From_Avs6Main.TypeListFields typeListFields)
  {
    if (name == "")
      return (string) null;
    return this.FieldByName(name, typeListFields)?._fieldText_Avs6;
  }

  public RecordNew()
  {
  }

  public RecordNew(Stream stream1) => this._stream = stream1;

  /// <summary> Чтение записи из stream </summary>
  /// <param name="br"></param>
  /// <param name="stream1"></param>
  /// <returns></returns>
  public bool Read(BinaryReader br, Stream stream1)
  {
    if (br == null || stream1 == null)
      return false;
    this._stream = stream1;
    if (br.ReadChar() != '#')
      return false;
    this._recordType_Avs6 = Convert.ToChar(br.ReadByte());
    int count = (int) br.ReadByte();
    int num1 = (int) br.ReadInt16();
    int num2 = (int) br.ReadInt16();
    int[] intArray = this.ConvertBytesArrayToIntArray(br.ReadBytes(count * 2));
    byte[] numArray = br.ReadBytes(count);
    long position = this._stream.Position;
    int index1 = 0;
    foreach (int num3 in numArray)
    {
      int num4 = intArray[index1];
      byte num5 = numArray[index1];
      int valueSize = index1 < intArray.Length - 1 ? intArray[index1 + 1] - num4 : -1;
      long dataOffset = position + (long) num4;
      string str = this.GetFieldValueStr(br, dataOffset, valueSize).Trim();
      if (!string.IsNullOrEmpty(str))
        this._listFields.Add(new RecordNewField()
        {
          _fieldType_Avs6 = num5,
          _fieldText_Avs6 = str
        });
      ++index1;
    }
    this._stream.Seek(position + (long) num1, SeekOrigin.Begin);
    if (num2 > 0)
    {
      this._listR2 = new List<RecordNew>();
      for (int index2 = 0; index2 < num2; ++index2)
      {
        RecordNew recordNew = new RecordNew(this._stream);
        if (!recordNew.Read(br, this._stream))
          throw new ArgumentNullException("Ошибка чтения записи файла AVS6");
        this._listR2.Add(recordNew);
      }
    }
    return true;
  }

  /// <summary> Выдает содержимое отдельного поля по его смещению (ПРИ ЧТЕНИИ ИЗ stream) </summary>
  /// <param name="br"></param>
  /// <param name="dataOffset"></param>
  /// <param name="valueSize"></param>
  /// <returns></returns>
  private string GetFieldValueStr(BinaryReader br, long dataOffset, int valueSize)
  {
    int count = 0;
    br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);
    for (int index = 0; index < 70000 && br.ReadByte() != (byte) 0; ++index)
      ++count;
    if (count == 0 || count >= 70000)
      return string.Empty;
    br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);
    byte[] numArray = br.ReadBytes(count);
    Encoding encoding = Encoding.GetEncoding(1251);
    Encoding unicode = Encoding.Unicode;
    Encoding dstEncoding = unicode;
    byte[] bytes1 = numArray;
    byte[] bytes2 = Encoding.Convert(encoding, dstEncoding, bytes1);
    char[] chars = new char[unicode.GetCharCount(bytes2, 0, bytes2.Length)];
    unicode.GetChars(bytes2, 0, bytes2.Length, chars, 0);
    return new string(chars);
  }

  private int[] ConvertBytesArrayToIntArray(byte[] byteArray)
  {
    int[] intArray = new int[byteArray.Length / 2];
    for (int index = 0; index < intArray.Length; ++index)
      intArray[index] = (int) byteArray[index * 2] + ((int) byteArray[index * 2 + 1] << 8);
    return intArray;
  }
}
