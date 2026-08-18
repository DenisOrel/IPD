
// Type: Intermech.Extensions.EnumHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Extensions
{
    /// <summary>Расширение системного класса Enum для упрощения обработки флагов</summary>
    public static class EnumHelper
    {
      /// <summary>Коллекция всех возможным флагов типа enum
      /// Если includeZero == false, то результата исключается 0,
      /// У перечислений помеченных атрибутом Flags возвращаются только индивидуальные значения, комбинации флагов отфильтровываются</summary>
      /// <param name="includeZero">(Optional) Включать ли в результат флаг со значением 0 (по-умолчанию false)</param>
      /// <returns>Коллекция всех возможных значений перечисления</returns>
      [NotNull]
      [MustUseReturnValue]
      public static IReadOnlyList<TEnum> PossibleValues<TEnum>(bool includeZero = false) where TEnum : struct, Enum
      {
        Type type = typeof (TEnum);
        TypeCode typeCode = Convert.GetTypeCode((object) default (TEnum));
        TEnum[] values = (TEnum[]) Enum.GetValues(type);
        if (values.Length == 0)
          return (IReadOnlyList<TEnum>) Array.Empty<TEnum>();
        List<TEnum> source = new List<TEnum>(values.Length);
        bool flag1 = type.HasAttribute<FlagsAttribute>();
        if (!flag1 & includeZero)
        {
          source.AddRange(((IEnumerable<TEnum>) values).Distinct());
          return (IReadOnlyList<TEnum>) source;
        }
        bool flag2 = false;
        foreach (TEnum @enum in values)
        {
          ulong num = FlagsExtensions.ToULong(@enum, typeCode);
          if (num > 0UL | includeZero)
          {
            source.Add(@enum);
            if (flag1 && !flag2)
              flag2 = (num & num - 1UL) > 0UL;
          }
        }
        if (source.Count == 0)
          return (IReadOnlyList<TEnum>) Array.Empty<TEnum>();
        if (!flag1 || !flag2)
          return (IReadOnlyList<TEnum>) source.Distinct().ToList();
        List<TEnum> enumList = new List<TEnum>(source.Count);
        HashSet<TEnum> enumSet = new HashSet<TEnum>();
        foreach (TEnum @enum in source)
        {
          TEnum value = @enum;
          if (enumSet.Add(value) && !source.Any((Func<TEnum, bool>) (compareVal => value.HasNewFlags(compareVal) && value.HasFlag((Enum) compareVal))))
            enumList.Add(value);
        }
        return (IReadOnlyList<TEnum>) enumList;
      }
    }
}
