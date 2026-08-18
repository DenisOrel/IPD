
// Type: Intermech.Interfaces.PosDesignationHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Методы для позиционного обозначения</summary>
    public static class PosDesignationHelper
    {
      /// <summary>Суммировать позиционные обозначения</summary>
      /// <param name="posDesignations">Список позиционных обозначений</param>
      /// <returns></returns>
      public static string Summ(List<string> posDesignations)
      {
        List<PosDesignationRecord> posDesignations1 = new List<PosDesignationRecord>();
        for (int index = 0; index < posDesignations.Count; ++index)
        {
          if (!string.IsNullOrEmpty(posDesignations[index]))
            posDesignations1.AddRange((IEnumerable<PosDesignationRecord>) PosDesignationRecord.ParsePositionalDesignation(posDesignations[index]));
        }
        return PosDesignationHelper.Summ(posDesignations1);
      }

      /// <summary>Суммировать позиционные обозначения</summary>
      /// <param name="posDesignations">Список позиционных обозначений</param>
      public static string Summ(
        List<PosDesignationRecord> posDesignations,
        string rangeSplitter = "-",
        string functionalGroupSplitter = "-")
      {
        posDesignations.Sort();
        string str1 = "";
        int index1;
        for (int index2 = 0; index2 < posDesignations.Count; index2 = index1 + 1)
        {
          index1 = posDesignations.Count - 1;
          string str2 = posDesignations[index2].DesignationBase.Trim();
          for (int index3 = index2 + 1; index3 < posDesignations.Count; ++index3)
          {
            long num = posDesignations[index3].NotNullNumber - posDesignations[index3 - 1].NotNullNumber;
            if (str2 != posDesignations[index3].DesignationBase.Trim() || posDesignations[index2].FunctionalGroup != posDesignations[index3].FunctionalGroup || posDesignations[index2].AdditionalSymbol != posDesignations[index3].AdditionalSymbol || num > 1L)
            {
              index1 = index3 - 1;
              break;
            }
          }
          if (str1 != "")
            str1 += ", ";
          str1 += PosDesignationRecord.ConvertToString(posDesignations[index2], posDesignations[index1], rangeSplitter, functionalGroupSplitter: functionalGroupSplitter);
        }
        return str1;
      }
    }
}
