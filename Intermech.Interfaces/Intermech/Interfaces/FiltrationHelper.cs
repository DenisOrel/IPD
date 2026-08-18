
// Type: Intermech.Interfaces.FiltrationHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Specialized;


namespace Intermech.Interfaces
{
    /// <summary>Вспомогательный статический класс для плагинов PDM</summary>
    public static class FiltrationHelper
    {
      /// <summary>Отключить указанные фильтрации состава плагинами</summary>
      /// <param name="filtrationSettings">Дополнительные настройки фильтрации или null</param>
      /// <param name="keys">Список ключей, которые блокируют какие-то фильтрации</param>
      public static void BlockPluginFiltrations(
        HybridDictionary filtrationSettings,
        params object[] keys)
      {
        if (filtrationSettings == null || keys == null || keys.Length == 0)
          return;
        for (int index = 0; index < keys.Length; ++index)
          filtrationSettings[keys[index]] = (object) true;
      }

      /// <summary>Отключить все возможные фильтрации состава плагинами</summary>
      /// <param name="filtrationSettings">Дополнительные настройки фильтрации или null</param>
      public static void BlockPluginFiltrations(HybridDictionary filtrationSettings)
      {
        if (filtrationSettings == null)
          return;
        filtrationSettings[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true;
        filtrationSettings[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
        filtrationSettings[(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"] = (object) true;
        filtrationSettings[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true;
      }

      /// <summary>Отключить все возможные фильтрации состава плагинами</summary>
      /// <param name="paramsSet">Параметры запроса в базу данных</param>
      /// <param name="filtrationSettings">Дополнительные настройки фильтрации или null</param>
      public static void BlockPluginFiltrations(
        ref DBRecordSetParams paramsSet,
        HybridDictionary filtrationSettings)
      {
        paramsSet.Tags = filtrationSettings != null ? filtrationSettings : paramsSet.Tags ?? new HybridDictionary(4, true);
        paramsSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true;
        paramsSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
        paramsSet.Tags[(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"] = (object) true;
        paramsSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true;
      }

      /// <summary>Разрешить работу конфигуратора составов</summary>
      /// <param name="filtrationSettings">Дополнительные настройки фильтрации или null</param>
      public static void UnlockConfigurator(HybridDictionary filtrationSettings)
      {
        if (filtrationSettings == null)
          return;
        filtrationSettings[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
      }

      /// <summary>Разрешить работу конфигуратора составов</summary>
      /// <param name="paramsSet">Параметры запроса в базу данных</param>
      /// <param name="filtrationSettings">Дополнительные настройки фильтрации или null</param>
      public static void UnlockConfigurator(
        ref DBRecordSetParams paramsSet,
        HybridDictionary filtrationSettings)
      {
        paramsSet.Tags = filtrationSettings != null ? filtrationSettings : paramsSet.Tags ?? new HybridDictionary(1, true);
        paramsSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
      }
    }
}
