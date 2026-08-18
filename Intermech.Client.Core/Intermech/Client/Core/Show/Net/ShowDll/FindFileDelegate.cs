
// Type: Intermech.Client.Core.Show.Net.ShowDll.FindFileDelegate
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary>связать файл с данными в памяти</summary>
/// <param name="fileName">путь и имя файла</param>
/// <param name="lenbytes">длинна файла</param>
/// <param name="bytes">содержимое файла</param>
/// <returns>null если файл НЕ найден; иначе уточнёное имя файла</returns>
public delegate string FindFileDelegate(string fileName, out int lenbytes, out byte[] bytes);
