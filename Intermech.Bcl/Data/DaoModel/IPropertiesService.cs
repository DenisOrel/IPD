
// Type: Intermech.Data.DaoModel.IPropertiesService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data.DaoModel
{
    public interface IPropertiesService
    {
      string ReadProperty(string name, string defaultValue);

      void WriteProperty(string name, string value);
    }
}
