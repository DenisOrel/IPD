
// Type: Intermech.Kernel.Search.DBDataParam
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Класс для передачи в поисковые запросы имени параметра и его значения
    /// </summary>
    [Serializable]
    public class DBDataParam
    {
      /// <summary>Имя параметра</summary>
      public string ParamName { get; private set; }

      /// <summary>Значение параметра</summary>
      public object ParamValue { get; private set; }

      public DBDataParam(string param_name, object param_value)
      {
        this.ParamName = param_name;
        this.ParamValue = param_value;
      }

      /// <summary>Создает экземпляр параметра</summary>
      /// <param name="param_name">Имя</param>
      /// <param name="param_value">Значение</param>
      /// <returns></returns>
      public static DBDataParam Parameter(string param_name, object param_value)
      {
        return new DBDataParam(param_name, param_value);
      }
    }
}
