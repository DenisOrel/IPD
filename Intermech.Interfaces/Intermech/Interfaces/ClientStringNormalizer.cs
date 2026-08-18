
// Type: Intermech.Interfaces.ClientStringNormalizer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс предназначена для получения УПРОЩЕННОЙ нормализованной строки на клиенте. НЕ ПРЕДНАЗНАЧЕН ДЛЯ СОЗДАНИЯ НОРМАЛИЗОВАННЫХ СТРОК.
    /// </summary>
    public class ClientStringNormalizer
    {
      public static string RusLettersUpper = "ЕТОРАНКХСВМ";
      public static string LatLettersUpper = "ETOPAHKXCBM";

      public static string GetIndexedString(string str_to_index)
      {
        StringBuilder stringBuilder = new StringBuilder(str_to_index.ToUpper());
        stringBuilder.Replace(" ", string.Empty);
        if (stringBuilder.Length > 0)
        {
          for (int index = 0; index < ClientStringNormalizer.RusLettersUpper.Length; ++index)
            stringBuilder.Replace(ClientStringNormalizer.RusLettersUpper[index], ClientStringNormalizer.LatLettersUpper[index]);
        }
        return stringBuilder.ToString();
      }
    }
}
