
// Type: Intermech.Client.Core.Show.Net.ShowNew.ExternFile.FileData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.IO;


namespace Intermech.Client.Core.Show.Net.ShowNew.ExternFile;

/// <summary> класс для хранения данных файла</summary>
[DebuggerDisplay("{_nameInfo.Name} Original={_nameInfo.OriginalPath}")]
internal class FileData
{
  /// <summary>содержимое файла ; иначе null</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private byte[] _bytes;
  /// <summary> оригинальный путь и имя файла; иначе string.Empty</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string _originalPath = string.Empty;

  /// <summary> оригинальный путь и имя файла; иначе string.Empty</summary>
  internal string OriginalPath => this._originalPath;

  /// <summary>содержимое файла ; иначе null</summary>
  internal byte[] InFile => this._bytes;

  /// <summary>содержит файл</summary>
  /// <param name="fileName">имя файла</param>
  /// <param name="bytes">содержимое файла; null если надо читать файл</param>
  /// <exception cref="T:System.ArgumentNullException">если не удалось получить содержимое файла </exception>
  internal FileData(string fileName, byte[] bytes)
  {
    this._bytes = bytes;
    if (fileName != null)
    {
      this._originalPath = fileName;
      if (this._bytes == null)
      {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
          this._bytes = new byte[(int) fileStream.Length];
          fileStream.Read(this._bytes, 0, this._bytes.Length);
          fileStream.Close();
        }
      }
    }
    if (this._bytes == null)
      throw new ArgumentNullException(nameof (bytes));
  }
}
