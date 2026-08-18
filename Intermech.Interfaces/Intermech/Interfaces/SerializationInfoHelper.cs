
// Type: Intermech.Interfaces.SerializationInfoHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>Утилиты для работы с SerializationInfo</summary>
    public class SerializationInfoHelper
    {
      /// <summary>
      /// Получение информации по типам сериализованных параметров
      /// </summary>
      /// <param name="info"></param>
      /// <returns></returns>
      public static Dictionary<string, Type> GetParamsType(SerializationInfo info)
      {
        Dictionary<string, Type> paramsType = new Dictionary<string, Type>();
        if (info == null)
          return (Dictionary<string, Type>) null;
        foreach (SerializationEntry serializationEntry in info)
          paramsType.Add(serializationEntry.Name, serializationEntry.ObjectType);
        return paramsType;
      }

      /// <summary>Получение информации сериализованных параметров</summary>
      /// <param name="info"></param>
      /// <returns></returns>
      public static Dictionary<string, object> GetParamsValue(SerializationInfo info)
      {
        Dictionary<string, object> paramsValue = new Dictionary<string, object>();
        if (info == null)
          return (Dictionary<string, object>) null;
        foreach (SerializationEntry serializationEntry in info)
          paramsValue.Add(serializationEntry.Name, serializationEntry.Value);
        return paramsValue;
      }
    }
}
