
// Type: Intermech.MultiValueModesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    public class MultiValueModesHelper
    {
      public static string GetCaption(MultiValueModes mode) => EnumTypeHelper.GetCaption((Enum) mode);

      public static MultiValueModes GetMultiValueMode(string s)
      {
        return (MultiValueModes) EnumTypeHelper.GetEnumValue(typeof (MultiValueModes), s);
      }

      /// <summary>
      /// показывает, выбирается ли значение атрибута из списка допустимых значений
      /// </summary>
      /// <param name="multiValueMode"></param>
      /// <returns></returns>
      public static bool IsValuedFromList(MultiValueModes multiValueMode)
      {
        return multiValueMode == MultiValueModes.SingleValueFromList || multiValueMode == MultiValueModes.MultiValuesFromList;
      }

      /// <summary>показывает, многозначный ли атрибут</summary>
      /// <param name="multiValueMode"></param>
      /// <returns></returns>
      public static bool IsMultipleValued(MultiValueModes multiValueMode)
      {
        return multiValueMode == MultiValueModes.MultiValues || multiValueMode == MultiValueModes.MultiValuesFromList;
      }
    }
}
