
// Type: Intermech.Interfaces.Briefcase.PartFile
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Структура в которой храниться инфа от файле портфеля
    /// в объей куче портфеля, которую передает на сервер клиент
    /// </summary>
    [Serializable]
    public struct PartFile(string fileName, long offset, long length)
    {
      /// <summary>Имя файла</summary>
      public string FileName { get; private set; } = fileName;

      /// <summary>Смещение от начала</summary>
      public long Offset { get; private set; } = offset;

      /// <summary>Длина</summary>
      public long Length { get; private set; } = length;
    }
}
