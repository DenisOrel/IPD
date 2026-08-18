
// Type: Intermech.Client.Core.Show.Net.ShowNew.ExternFile.PathInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.IO;


namespace Intermech.Client.Core.Show.Net.ShowNew.ExternFile;

/// <summary> информация о файле</summary>
[DebuggerDisplay("{Name} Original={OriginalPath}")]
internal class PathInfo
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string _originalPath = string.Empty;

  /// <summary> оригинальный путь и имя файла</summary>
  internal string OriginalPath => this._originalPath;

  /// <summary>Имя файла <c>без</c> расширения</summary>
  internal string NameOnly => Path.GetFileNameWithoutExtension(this._originalPath);

  /// <summary>Имя файла с расширением</summary>
  internal string Name => Path.GetFileName(this._originalPath);

  /// <summary>расширение файла</summary>
  internal string Extension => Path.GetExtension(this._originalPath);

  /// <summary>конструктор</summary>
  /// <param name="fileName">путь и имя файла</param>
  internal PathInfo(string fileName)
  {
    this._originalPath = fileName != null ? fileName : throw new ArgumentNullException(nameof (fileName));
  }
}
