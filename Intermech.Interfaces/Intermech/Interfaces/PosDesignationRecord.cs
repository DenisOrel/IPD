
// Type: Intermech.Interfaces.PosDesignationRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Вспомогательный класс для суммирования позиционного обозначения</summary>
    public class PosDesignationRecord : IComparable
    {
      /// <summary>Позиционное обозначение</summary>
      public string Designation;
      /// <summary>Буквенная часть позиционного обозначения</summary>
      public string DesignationBase;
      /// <summary>Число позиционного обозначения</summary>
      public long? Number;
      public string FunctionalGroup;
      public string AdditionalSymbol;

      /// <summary>Число позиционного обозначения. Вместо null возвращает 0.</summary>
      public long NotNullNumber => this.Number.HasValue ? this.Number.Value : 0L;

      public string GetFullPosDesignation(string functionalGroupSplitter = "-")
      {
        string str = "";
        if (!string.IsNullOrEmpty(this.FunctionalGroup))
          str = this.FunctionalGroup + functionalGroupSplitter;
        return str + $"{this.DesignationBase}{this.Number}{this.AdditionalSymbol}";
      }

      public PosDesignationRecord(
        string designation,
        string baseDesignation,
        long? number,
        string functionalGroup,
        string additionalSymbol)
      {
        this.Designation = designation;
        this.DesignationBase = baseDesignation;
        this.Number = number;
        this.FunctionalGroup = functionalGroup;
        this.AdditionalSymbol = additionalSymbol;
      }

      public PosDesignationRecord(PosDesignationRecord basePosDesignationRec, long? number)
        : this(basePosDesignationRec.Designation, basePosDesignationRec.DesignationBase, number, basePosDesignationRec.FunctionalGroup, basePosDesignationRec.AdditionalSymbol)
      {
        this.Designation = this.DesignationBase + number.ToString();
      }

      public PosDesignationRecord(
        string posDesignation,
        string functionalGroup,
        string additionalSymbol)
      {
        this.Designation = posDesignation;
        if (this.Designation != null)
          this.Designation = this.Designation.Trim();
        this.FunctionalGroup = functionalGroup;
        this.AdditionalSymbol = additionalSymbol;
        long number;
        string textBeforeNumber;
        string textAfterNumber;
        if (NumberParserAdvanced.ParseUnsignedInteger(this.Designation, out number, out textBeforeNumber, out textAfterNumber) && string.IsNullOrEmpty(textAfterNumber))
        {
          this.DesignationBase = textBeforeNumber;
          this.Number = new long?(number);
        }
        else
        {
          this.DesignationBase = this.Designation;
          this.Number = new long?();
        }
      }

      public bool IsValid => !string.IsNullOrEmpty(this.DesignationBase) && this.Number.HasValue;

      public override string ToString() => this.GetFullPosDesignation();

      private static int MakeSeparatorList(
        string text,
        IList<string> separators,
        ref int[] sepList,
        ref int[] lengthList)
      {
        int index1 = 0;
        int length1 = sepList.Length;
        for (int index2 = 0; index2 < text.Length && index1 < length1; ++index2)
        {
          for (int index3 = 0; index3 < separators.Count; ++index3)
          {
            string separator = separators[index3];
            if (!string.IsNullOrEmpty(separator))
            {
              int length2 = separator.Length;
              if ((int) text[index2] == (int) separator[0] && length2 <= text.Length - index2 && (length2 == 1 || string.CompareOrdinal(text, index2, separator, 0, length2) == 0))
              {
                sepList[index1] = index2;
                lengthList[index1] = length2;
                ++index1;
                index2 += length2 - 1;
                break;
              }
            }
          }
        }
        return index1;
      }

      /// <summary>Разобрать запись со списком позиционных обозначений в сокращённой форме на отдельные позиционные обозначения</summary>
      /// <param name="posDesignation">Сокращённый список позиционных обозначений</param>
      /// <param name="functionalGroup">Функциональная группа</param>
      /// <param name="additionalSymbol">Дополнительный символ, который добавляется к поз. обозначению подборных элементов</param>
      /// <param name="rangeSplitter">Разделитель диапазона значений</param>
      /// <returns></returns>
      public static List<PosDesignationRecord> ParsePositionalDesignation(
        string posDesignation,
        string functionalGroup = null,
        string additionalSymbol = null,
        string rangeSplitter = "-")
      {
        List<PosDesignationRecord> positionalDesignation = new List<PosDesignationRecord>();
        posDesignation = posDesignation.Replace("*", "");
        if (!string.IsNullOrEmpty(additionalSymbol))
          posDesignation = posDesignation.Replace(additionalSymbol, "");
        List<string> separators = new List<string>(4)
        {
          "-",
          "...",
          ".."
        };
        if (!string.IsNullOrEmpty(rangeSplitter) && !separators.Contains(rangeSplitter))
          separators.Add(rangeSplitter);
        string str1 = posDesignation;
        char[] chArray = new char[2]{ ',', ';' };
        foreach (string str2 in str1.Split(chArray))
        {
          string str3 = str2.Trim();
          if (!string.IsNullOrEmpty(str3))
          {
            int[] sepList = new int[str3.Length];
            int[] lengthList = new int[str3.Length];
            int separatorsCount = PosDesignationRecord.MakeSeparatorList(str3, (IList<string>) separators, ref sepList, ref lengthList);
            if (separatorsCount > 0)
            {
              PosDesignationRecord posDBefore1 = (PosDesignationRecord) null;
              int designationRecord = PosDesignationRecord.FindPosDesignationRecord(str3, 0, 0, sepList, lengthList, separatorsCount, functionalGroup, additionalSymbol, out posDBefore1);
              if (posDBefore1 != null && designationRecord == separatorsCount)
              {
                positionalDesignation.Add(posDBefore1);
              }
              else
              {
                PosDesignationRecord posDBefore2 = (PosDesignationRecord) null;
                if (designationRecord != separatorsCount)
                {
                  int startTextIndex = sepList[designationRecord] + lengthList[designationRecord];
                  PosDesignationRecord.FindPosDesignationRecord(str3, startTextIndex, designationRecord + 1, sepList, lengthList, separatorsCount, functionalGroup, additionalSymbol, out posDBefore2);
                }
                if (posDBefore1 != null && posDBefore1.IsValid && posDBefore2 != null && posDBefore2.IsValid)
                {
                  positionalDesignation.Add(posDBefore1);
                  for (long index = posDBefore1.NotNullNumber + 1L; index < posDBefore2.NotNullNumber; ++index)
                    positionalDesignation.Add(new PosDesignationRecord(posDBefore1, new long?(index)));
                  positionalDesignation.Add(posDBefore2);
                }
                else
                  positionalDesignation.Add(new PosDesignationRecord(str3, functionalGroup, additionalSymbol));
              }
            }
            else
              positionalDesignation.Add(new PosDesignationRecord(str3, functionalGroup, additionalSymbol));
          }
        }
        return positionalDesignation;
      }

      private static int FindPosDesignationRecord(
        string designationByComma,
        int startTextIndex,
        int startSeparator,
        int[] separatorPositions,
        int[] separatorLength,
        int separatorsCount,
        string functionalGroup,
        string additionalSymbol,
        out PosDesignationRecord posDBefore)
      {
        for (int designationRecord = startSeparator; designationRecord < separatorsCount; ++designationRecord)
        {
          if (separatorPositions[designationRecord] - startTextIndex > 0)
          {
            string posDesignation = designationByComma.Substring(startTextIndex, separatorPositions[designationRecord] - startTextIndex);
            posDBefore = new PosDesignationRecord(posDesignation, functionalGroup, additionalSymbol);
            if (posDBefore.IsValid)
              return designationRecord;
          }
        }
        posDBefore = startTextIndex >= designationByComma.Length - 1 ? (PosDesignationRecord) null : new PosDesignationRecord(designationByComma.Substring(startTextIndex), functionalGroup, additionalSymbol);
        return separatorsCount;
      }

      public static string ConvertToString(
        PosDesignationRecord firstPosDesignation,
        PosDesignationRecord lastPosDesignation,
        string rangeSplitter = "-",
        string oneStepSplitter = ", ",
        string functionalGroupSplitter = "-")
      {
        string str;
        if (lastPosDesignation == null || firstPosDesignation == lastPosDesignation || firstPosDesignation.Designation == lastPosDesignation.Designation)
        {
          str = firstPosDesignation.GetFullPosDesignation(functionalGroupSplitter);
        }
        else
        {
          if (lastPosDesignation.NotNullNumber - firstPosDesignation.NotNullNumber == 1L)
            rangeSplitter = oneStepSplitter;
          str = firstPosDesignation.GetFullPosDesignation(functionalGroupSplitter) + rangeSplitter + lastPosDesignation.GetFullPosDesignation(functionalGroupSplitter);
        }
        return str;
      }

      public int CompareTo(object obj)
      {
        PosDesignationRecord designationRecord = obj as PosDesignationRecord;
        if (designationRecord == this)
          return 0;
        if (designationRecord == null)
          return 1;
        int num1 = this.CompareNullableString(this.FunctionalGroup, designationRecord.FunctionalGroup);
        if (num1 != 0)
          return num1;
        int num2 = this.CompareNullableString(this.DesignationBase.Trim(), designationRecord.DesignationBase.Trim());
        return num2 != 0 ? num2 : this.CompareNullableInt(this.Number, designationRecord.Number);
      }

      private int CompareNullableString(string str1, string str2)
      {
        if (str1 == str2)
          return 0;
        if (str1 == null)
          return -1;
        return str2 == null ? 1 : string.Compare(str1, str2);
      }

      private int CompareNullableInt(long? num1, long? num2)
      {
        long? nullable1 = num1;
        long? nullable2 = num2;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          return 0;
        if (!num1.HasValue)
          return -1;
        return !num2.HasValue ? 1 : num1.Value.CompareTo(num2.Value);
      }
    }
}
