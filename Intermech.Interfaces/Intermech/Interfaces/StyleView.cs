
// Type: Intermech.Interfaces.StyleView
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public enum StyleView
    {
      /// <summary>Файлы открывающиеся через MapObject</summary>
      Native,
      /// <summary>
      /// Файлы типа txt, reg, cmd, bat
      /// которые не открываются через PreviewHandler
      /// </summary>
      Internal,
      /// <summary>ActiveX</summary>
      ActiveX,
      /// <summary>В новом процессе</summary>
      Shell,
      /// <summary>В новом процессе по умолчанию</summary>
      Default,
      /// <summary>В новом процессе по командной линии</summary>
      CommandLine,
      /// <summary>Preview handler</summary>
      PreView,
      /// <summary>ExtractImage</summary>
      ExtractImage,
      /// <summary>
      /// Извлекает превьею непосредственно из файла, посредством специального сервиса IPS
      /// </summary>
      InternalExtractView,
      /// <summary>Thumbnail</summary>
      PrevThumbnail,
      /// <summary>Неизвестный</summary>
      Unknown,
    }
}
