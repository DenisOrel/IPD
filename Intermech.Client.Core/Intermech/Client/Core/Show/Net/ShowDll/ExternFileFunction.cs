
// Type: Intermech.Client.Core.Show.Net.ShowDll.ExternFileFunction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary>связать файл с данными в памяти</summary>
/// <param name="fileName">[in]путь и имя файла; [out]уточнёное имя файла связанное с данными в памяти</param>
/// <returns>данные в памяти для указанного файла; null - файл не найден</returns>
public delegate byte[] ExternFileFunction(ref string fileName);
