
// Type: Intermech.Interfaces.IIconReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Drawing;


namespace Intermech.Interfaces
{
    /// <summary>Чтение икон (из registry) для типов файлов</summary>
    public interface IIconReader
    {
      Icon GetIconByFileExt(string ext);
    }
}
