
// Type: Intermech.Interfaces.MaterialHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Статические методы для работы с типом объектов "Материал"
    /// </summary>
    public class MaterialHelper
    {
      /// <summary>Разделитель в строке</summary>
      public const string Separator = "&^";
      /// <summary>Префикс значения ключа Imbase</summary>
      public const string ImbaseKeyPrefix = "i6";
      /// <summary>Длина ключа Imbase</summary>
      public const int ImbaseKeyLength = 20;
      /// <summary>Количество символов в ключе Imbase для кода каталога</summary>
      private const int _catalogImbaseKeyLength = 6;

      /// <summary>
      /// Разбиваем строку, если есть разделитель на 2 части - 1ая часть: наименование материала, 2ая часть : старый ключ IMBASE или Guid
      /// </summary>
      /// <param name="strMaterial">Входная строка</param>
      /// <param name="materialName">Наименование материала</param>
      /// <param name="secondPart">Вторая часть пришедшего параметра</param>
      public static void TrimMaterialNameString(
        string strMaterial,
        ref string materialName,
        ref string secondPart)
      {
        int length = strMaterial.IndexOf("&^");
        if (length >= 0)
        {
          materialName = strMaterial.Substring(0, length);
          secondPart = strMaterial.Substring(length + "&^".Length, strMaterial.Length - (length + "&^".Length));
        }
        if (!(secondPart != string.Empty) || secondPart.IndexOf("i6") == 0 && secondPart.Length == 20 || GuidHelper.IsGuid(secondPart))
          return;
        secondPart = string.Empty;
      }

      /// <summary>Режимы поиска материала в базе</summary>
      private enum SearchMode
      {
        MaterialNameOnly,
        ImbaseKey,
        Guid,
      }
    }
}
