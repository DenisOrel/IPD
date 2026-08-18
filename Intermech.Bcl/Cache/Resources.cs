
// Type: Intermech.Cache.Resources
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;


namespace Intermech.Cache
{
    /// <summary>Хранит текстовые ресурсы, подлежащие локализации.</summary>
    internal sealed class Resources
    {
      /// <summary>
      /// Возвращает локализованную текстовую строку по ее имени.
      /// </summary>
      /// <param name="name">Имя текстовой строки</param>
      /// <returns></returns>
      public static string GetString(string name)
      {
        switch (name)
        {
          case "E_AbsoluteDateTime":
            return LocalizationHolder.rm.GetString("Cache_1");
          case "E_CacheLocationIsNotExists":
            return LocalizationHolder.rm.GetString("Cache_13");
          case "E_CacheLocationIsNull":
            return LocalizationHolder.rm.GetString("Cache_12");
          case "E_DataIsNull":
            return LocalizationHolder.rm.GetString("Cache_6");
          case "E_DataIsTooLarge":
            return LocalizationHolder.rm.GetString("Cache_7");
          case "E_ExpirationIsNull":
            return LocalizationHolder.rm.GetString("Cache_8");
          case "E_HeadCannotBeNull":
            return LocalizationHolder.rm.GetString("Cache_10");
          case "E_KeyIsNull":
            return LocalizationHolder.rm.GetString("Cache_5");
          case "E_PolicyIsNull":
            return LocalizationHolder.rm.GetString("Cache_4");
          case "E_SlidingDuration":
            return LocalizationHolder.rm.GetString("Cache_2");
          case "E_StorageIsNull":
            return LocalizationHolder.rm.GetString("Cache_3");
          case "E_StoreTotalSpace":
            return LocalizationHolder.rm.GetString("Cache_9");
          case "E_TailCannotBeNull":
            return LocalizationHolder.rm.GetString("Cache_11");
          default:
            return name;
        }
      }
    }
}
