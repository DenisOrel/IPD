
// Type: Intermech.Interfaces.ClientEventLogHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    public class ClientEventLogHelper
    {
      /// <summary>
      /// Возвращает наименование действия actType для категории categoryType и объекта
      /// categoryID в хэше actionNames. Если для categoryID&gt;0 наименование не найдено, то идет поиск для
      /// categoryID=0. Если для categoryType&gt;0 наименование не найдено, то идет поиск
      /// для categoryType=0 и categoryID=0.
      /// </summary>
      public static string GetActionName(CategoryValue ct, Hashtable actionNames)
      {
        object obj = actionNames[(object) ct];
        if (obj == null)
        {
          if (ct.CategoryID > 0L)
          {
            ct.CategoryID = 0L;
            obj = actionNames[(object) ct];
          }
          if (obj == null)
          {
            ct.CategoryType = 0;
            obj = (object) ActionTypeHelper.GetCaption(ct.ActionID);
          }
        }
        if (obj == null)
          obj = (object) "";
        return Convert.ToString(obj);
      }
    }
}
