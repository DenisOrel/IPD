
// Type: Intermech.Interfaces.MapiFileDesc
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Interfaces
{
    /// <summary>Информация о приаттаченном объекте.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MapiFileDesc
    {
      /// <summary>зарезервировано</summary>
      public int reserved;
      /// <summary>маска флагов приложения</summary>
      public int flags;
      /// <summary>показывающее положение файлов-приложений в сообщении</summary>
      public int position;
      /// <summary>путь к файлу. Включает в себя имя диска и директории</summary>
      public string path;
      /// <summary>
      /// имя приаттаченного файла приложения (под таким именем его увидит получатель).
      /// Если значение не заданно или установленно как NULL, то используется имя path
      /// </summary>
      public string name;
      /// <summary>указывает тип файла</summary>
      public IntPtr type;
    }
}
