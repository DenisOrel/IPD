
// Type: Intermech.Client.Core.Show.Net.ShowNew.ExternFile.FileTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowDll;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.ExternFile;

/// <summary>список внешних файлов</summary>
[DebuggerDisplay("Count = {_dictionary.Count}")]
internal class FileTable
{
  /// <summary> </summary>
  private ExternFileFunction _findFun;
  /// <summary>колекция файлов(имя файла,данные файла)</summary>
  private SortedDictionary<string, byte[]> _dictionary = new SortedDictionary<string, byte[]>();
  /// <summary>колекция файлов(оригинальное имя файла,имя файла)</summary>
  private SortedDictionary<string, string> _dictName = new SortedDictionary<string, string>();
  /// <summary>имя и данные базового файла</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private FileData _basefile;

  /// <summary>создать таблицу файлов</summary>
  /// <param name="basefile">имя и данные базового файла</param>
  /// <param name="findFun">делегат для подстановки файла</param>
  internal FileTable(FileData basefile, ExternFileFunction findFun)
  {
    this._basefile = basefile != null ? basefile : throw new ArgumentNullException(nameof (basefile));
    this._findFun = findFun;
  }

  /// <summary>имя и данные базового файла</summary>
  internal FileData Basefile => this._basefile;

  /// <summary>проверка : есть ли оригинальное имя файла</summary>
  /// <param name="key">оригинальное имя файла</param>
  /// <returns>true - есть оригинальное имя</returns>
  internal bool ContainsKey(string key) => this._dictName.ContainsKey(key);

  internal void CloseBase() => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Close();

  /// <summary>открыть базовый файл с помощью DLL</summary>
  /// <param name="defaultWeight">толщина линии по умолчанию</param>
  /// <returns>код завершения</returns>
  internal DwgOpenException.ReturnType OpenBase(float defaultWeight)
  {
    FindFileDelegate fun = (FindFileDelegate) null;
    if (Intermech.Client.Core.Show.Net.ShowDll.ShowDll.VersionNetShowDLL != 0)
    {
      if (this._findFun != null)
        fun = new FindFileDelegate(this.FindFileLocal);
      DwgOpenException.ReturnType returnType = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_Dwg_Net(this._basefile, defaultWeight, ref fun);
      return returnType == DwgOpenException.ReturnType.exOk ? returnType : throw new DwgOpenException("Error Open", returnType);
    }
    DwgOpenException.ReturnType returnType1 = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_Dwg_Data(this._basefile);
    return returnType1 == DwgOpenException.ReturnType.exOk ? returnType1 : throw new DwgOpenException("Error Open", returnType1);
  }

  /// <summary>связать файл с данными в памяти </summary>
  /// <param name="fileName">путь и имя файла</param>
  /// <param name="lenbytes">длинна файла</param>
  /// <param name="bytes">содержимое файла</param>
  /// <returns>null если файл НЕ найден; иначе уточнёное имя файла</returns>
  private string FindFileLocal(string fileName, out int lenbytes, out byte[] bytes)
  {
    bytes = (byte[]) null;
    lenbytes = 0;
    string fileName1 = fileName;
    if (!this._dictName.ContainsKey(fileName))
    {
      byte[] numArray = this._findFun(ref fileName1);
      this._dictName.Add(fileName, fileName1);
      if (!this._dictionary.ContainsKey(fileName1))
        this._dictionary.Add(fileName1, numArray);
    }
    fileName1 = this._dictName[fileName];
    lenbytes = (bytes = this._dictionary[fileName1]) == null ? 0 : bytes.Length;
    return fileName1;
  }

  /// <summary>добавить пару (оригинальное имя файла,данные файла)</summary>
  /// <param name="key">оригинальное имя файла</param>
  /// <param name="value">данные файла</param>
  internal void Add(string key, byte[] value) => this._dictionary.Add(key, value);

  /// <summary>получить по оригинальному имени файла  данные</summary>
  /// <param name="key">оригинальное имя файла</param>
  /// <returns>данные файла; если файл ненайден - null</returns>
  internal byte[] this[string key] => this._dictionary[key];
}
