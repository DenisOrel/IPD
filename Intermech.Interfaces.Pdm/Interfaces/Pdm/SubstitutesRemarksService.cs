// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SubstitutesRemarksService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using Intermech.Search.Pdm.Substitutes;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Служба, помогающая выполнить генерацию примечания для связей, участвующих в допустимых заменах
/// </summary>
public sealed class SubstitutesRemarksService : LongLifeObject, ISubstitutesRemarksService
{
  /// <summary>
  /// Рассчитать примечания для связей, участвующих в допустимых заменах
  /// </summary>
  /// <param name="substsSettings">Настройки допустимых замен</param>
  /// <param name="relAttrs">Набор связей и значения атрибутов, требуемых для расчёта примечаний:
  /// - Количество (связь),
  /// - Обозначение (объект),
  /// - Наименование (объект),
  /// - Позиция (количество),
  /// - Конструкторский основной вариант (связь),
  /// - Номер группы заменителей (связь),
  /// - Номер заменителя в группе (связь)</param>
  /// <returns>Словарь, содержащий идентификаторы связей и соответствующие им значения расшифровок допустимых замен</returns>
  public Dictionary<long, string> CalcSubstituteRemarks(
    ISubstitutesSettings substsSettings,
    RelationAttributesPackage relAttrs)
  {
    Dictionary<long, string> dictionary = new Dictionary<long, string>();
    if (substsSettings == null || relAttrs == null || relAttrs.Values.Count == 0 || relAttrs.Attributes.Count < 7)
      return dictionary;
    int num1 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrQuantity);
    int num2 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrDesignation);
    int num3 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrName);
    int num4 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrPosition);
    relAttrs.Attributes.IndexOf(SubstituteObjects.attrDesignActualVariant);
    int index1 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrSubstituteGroupNo);
    int index2 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrSubstituteInGroup);
    if (num1 == -1 || num2 == -1 || num3 == -1 || num4 == -1 || index1 == -1 || index2 == -1)
      return dictionary;
    SubstituteObjects substituteObjects = new SubstituteObjects();
    foreach (KeyValuePair<long, object[]> keyValuePair in relAttrs.Values)
    {
      long int64Value1 = DataSetProcessor.GetInt64Value(keyValuePair.Value[index1], 0L);
      if (int64Value1 != 0L)
      {
        long int64Value2 = DataSetProcessor.GetInt64Value(keyValuePair.Value[index2], 0L);
        substituteObjects.AddRelation(int64Value1, int64Value2, keyValuePair.Key);
      }
    }
    substituteObjects.RelationAttributes = relAttrs;
    List<long> groups = substituteObjects.Groups;
    for (int index3 = 0; index3 < groups.Count; ++index3)
    {
      foreach (KeyValuePair<long, string> substituteGroupRemark in this.InternalCalcSubstituteGroupRemarks(substsSettings, substituteObjects, groups[index3]))
        dictionary[substituteGroupRemark.Key] = substituteGroupRemark.Value;
    }
    return dictionary;
  }

  /// <summary>
  /// Рассчитать примечания для связей, участвующих в допустимых заменах
  /// </summary>
  /// <param name="substsSettings">Настройки допустимых замен</param>
  /// <param name="substs">Допустимые замены в составе</param>
  ///         /// <returns>Словарь, содержащий идентификаторы связей и соответствующие им значения расшифровок допустимых замен</returns>
  public Dictionary<long, string> CalcSubstituteRemarks(
    ISubstitutesSettings substsSettings,
    SubstituteObjects substs)
  {
    Dictionary<long, string> dictionary = new Dictionary<long, string>();
    RelationAttributesPackage relationAttributes = substs?.RelationAttributes;
    if (relationAttributes == null || substsSettings == null || substs == null || substs.Count == 0)
      return dictionary;
    int num1 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrQuantity);
    int num2 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrDesignation);
    int num3 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrName);
    int num4 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrPosition);
    relationAttributes.Attributes.IndexOf(SubstituteObjects.attrDesignActualVariant);
    int num5 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrSubstituteGroupNo);
    int num6 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrSubstituteInGroup);
    if (num1 == -1 || num2 == -1 || num3 == -1 || num4 == -1 || num5 == -1 || num6 == -1)
      return dictionary;
    List<long> groups = substs.Groups;
    for (int index = 0; index < groups.Count; ++index)
    {
      foreach (KeyValuePair<long, string> substituteGroupRemark in this.InternalCalcSubstituteGroupRemarks(substsSettings, substs, groups[index]))
        dictionary[substituteGroupRemark.Key] = substituteGroupRemark.Value;
    }
    return dictionary;
  }

  /// <summary>
  /// Проверить, принадлежит ли указанная связь к основному конструкторскому варианту
  /// (либо её атрибут "Конструкторский основной вариант" равен "1", либо,
  /// если этого атрибута нет, substNo = 0)
  /// </summary>
  /// <param name="values">Значения атрибутов связи</param>
  /// <param name="attributes">Список атрибутов связи</param>
  /// <param name="substNo">Номер заменителя в группе</param>
  /// <returns>true, если связь принадлежит к основному конструкторскому варианту</returns>
  private bool IsActualVariant(object[] values, List<int> attributes, int substNo)
  {
    int index = attributes.IndexOf(SubstituteObjects.attrDesignActualVariant);
    if (index < 0)
      return substNo == 0;
    object obj = values[index];
    long result;
    return obj == null || !long.TryParse(obj.ToString(), out result) ? substNo == 0 : result == 1L;
  }

  /// <summary>Сделать первый символ в строке заглавным</summary>
  /// <param name="value">Строка</param>
  /// <returns>Строка с заглавным первым символом</returns>
  private string FirstLetterUpper(string value)
  {
    if (string.IsNullOrEmpty(value))
      return value;
    int length = value.Length;
    return length <= 1 ? value[0].ToString().ToUpper() : value[0].ToString().ToUpper() + value.Substring(1, length - 1);
  }

  /// <summary>
  /// Вернуть строку "поз. ... кол. ... ..." или "обозначение ... кол. ...", если нет позиции
  /// </summary>
  /// <param name="relAttrs">Коллекция значений атрибутов связей</param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="substsSettings">Настройки допустимых замен</param>
  /// <returns>Строка "поз. ... кол. ... ..." или "обозначение ... кол. ...", если нет позиции</returns>
  private string RelPositionQuantity(
    RelationAttributesPackage relAttrs,
    long prjLinkID,
    ISubstitutesSettings substsSettings)
  {
    int index1 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrQuantity);
    int index2 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrDesignation);
    int index3 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrName);
    int index4 = relAttrs.Attributes.IndexOf(SubstituteObjects.attrPosition);
    if (index1 == -1 || index2 == -1 || index3 == -1 || index4 == -1)
      return string.Empty;
    object[] relAttr = relAttrs[prjLinkID];
    string str1 = this.RelPosition(LocalizationHolder.rm.GetString("Interfaces.Pdm_1"), relAttr[index4], string.Empty, !string.IsNullOrEmpty(relAttr[index2] as string) ? relAttr[index2].ToString() : (relAttr[index3] != null ? relAttr[index3].ToString() : string.Empty));
    string str2 = this.RelQuantity(substsSettings, substsSettings.QuantityInBrackets ? LocalizationHolder.rm.GetString("Interfaces.Pdm_2") : LocalizationHolder.rm.GetString("Interfaces.Pdm_3"), relAttr[index1], substsSettings.QuantityInBrackets ? ")" : string.Empty);
    return !(str2 != string.Empty) || !substsSettings.QuantityInSubstitutes ? str1 : $"{str1} {str2}";
  }

  /// <summary>
  /// Вернуть строку с количеством, префиксом и суффиксом, либо пустую строку, если количество не задано
  /// </summary>
  /// <param name="substsSettings">Настройки допустимых замен</param>
  /// <param name="preffix">Строка будет добавлена в начало результирующей строки с количеством</param>
  /// <param name="value">Значение атрибута "Количество"</param>
  /// <param name="suffix">Строка будет добавлена в конец результирующей строки с количеством</param>
  /// <returns>Строка с количеством, префиксом и суффиксом, либо пустая строка, если количество не задано</returns>
  private string RelQuantity(
    ISubstitutesSettings substsSettings,
    string preffix,
    object value,
    string suffix)
  {
    if (value == null || value == DBNull.Value || substsSettings == null)
      return string.Empty;
    string str = MeasureHelper.ConvertToMeasuredValue(value.ToString()).Caption;
    if (str.IndexOf(LocalizationHolder.rm.GetString("Interfaces.Pdm_4")) == str.Length - 3)
      str += ".";
    if (str != string.Empty && substsSettings.NonbreakingSpace)
      str = str.Replace(LocalizationHolder.rm.GetString("Interfaces.Pdm_4"), Convert.ToChar(160 /*0xA0*/).ToString() + LocalizationHolder.rm.GetString("Interfaces.Pdm_4a"));
    return !(str != string.Empty) ? string.Empty : preffix + str + suffix;
  }

  /// <summary>
  /// Вернуть строку с позицией, префиксом и суффиксом, либо значение по умолчанию, если позиция не задана
  /// </summary>
  /// <param name="preffix">Строка будет добавлена в начало результирующей строки с позицией</param>
  /// <param name="value">Значение атрибута "Позиция"</param>
  /// <param name="suffix">Строка будет добавлена в конец результирующей строки с позицией</param>
  /// <param name="defValue">Значение по умолчанию</param>
  /// <returns>Строка с позицией, префиксом и суффиксом, либо значение по умолчанию, если позиция не задана</returns>
  private string RelPosition(string preffix, object value, string suffix, string defValue)
  {
    if (value == null || value == DBNull.Value)
      return defValue;
    string str = value.ToString();
    return !(str != string.Empty) ? defValue : preffix + str + suffix;
  }

  private Dictionary<long, string> InternalCalcSubstituteGroupRemarks(
    ISubstitutesSettings substitutesSettings,
    SubstituteObjects substituteObjects,
    long substituteGroupNumber)
  {
    substituteObjects.SortPositions();
    Dictionary<long, string> dictionary = new Dictionary<long, string>();
    RelationAttributesPackage relationAttributes = substituteObjects.RelationAttributes;
    if (substitutesSettings == null || substituteObjects.Count == 0 || substituteGroupNumber <= 0L || relationAttributes == null || relationAttributes.Values.Count == 0)
      return dictionary;
    int index1 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrQuantity);
    int num1 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrDesignation);
    int num2 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrName);
    int num3 = relationAttributes.Attributes.IndexOf(SubstituteObjects.attrPosition);
    relationAttributes.Attributes.IndexOf(SubstituteObjects.attrDesignActualVariant);
    if (index1 == -1 || num1 == -1 || num2 == -1 || num3 == -1)
      return dictionary;
    List<List<long>> substituteObject = substituteObjects[substituteGroupNumber];
    List<long> relations1 = new List<long>();
    substituteObjects.GatherRelations(substituteGroupNumber, ref relations1);
    if (substituteObject == null || substituteObject.Count < 2 || relations1.Count == 0 || relations1.Count > relationAttributes.Values.Count)
      return dictionary;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index2 = 0; index2 < substituteObject.Count; ++index2)
    {
      int num4 = index2;
      List<long> relations2 = substituteObject[num4];
      List<long> longList1 = !relationAttributes.Attributes.Contains(SubstitutesConstants.PositionNumberAttributeTypeID) ? this.CloneRelationsList(relations2, relationAttributes, SubstituteObjects.attrPosition) : this.CloneRelationsList(relations2, relationAttributes, SubstitutesConstants.PositionNumberAttributeTypeID);
      bool flag1 = substituteObjects.HasRelationsDesignerActualVariant(substituteGroupNumber);
      int count1 = longList1.Count;
      for (int index3 = 0; index3 < count1; ++index3)
      {
        long num5 = longList1[index3];
        object[] values = relationAttributes[num5];
        stringBuilder.Length = 0;
        string str1 = this.RelQuantity(substitutesSettings, string.Empty, values[index1], string.Empty);
        stringBuilder.Append(str1);
        string str2 = this.GetPositionDesignation(relationAttributes, num5);
        if (!substitutesSettings.IncludePositionalDesignationInNote)
          str2 = string.Empty;
        if (!string.IsNullOrEmpty(str2))
          stringBuilder.AppendFormat(" {0}, ", (object) str2);
        else if (!string.IsNullOrEmpty(str1))
          stringBuilder.Append(", ");
        int num6 = substituteObjects.IsAuxiliaryPosition(num5) ? 1 : 0;
        bool flag2 = substituteObjects.IsEqualPosition(num5);
        if (num6 != 0)
        {
          long objectId = substituteObjects.GetObjectID(num5);
          if (!ObjectHelper.IsUnknownObjectID(objectId))
          {
            string relationPositionNumber = substituteObjects.GetRelationPositionNumber(num5);
            long[] positionRelationIds = substituteObjects.GetAuxPositionRelationIds(substituteGroupNumber, relationPositionNumber, objectId);
            stringBuilder.Append(this.UsePlaceholders ? "[Substitute]" : substitutesSettings.Substitute);
            foreach (long relationID in positionRelationIds)
            {
              long[] array = ((IEnumerable<long>) substituteObjects.GetRelationIdsInSubstituteWithRelation(substituteGroupNumber, relationID)).Where<long>((Func<long, bool>) (o => !substituteObjects.IsAuxiliaryPosition(o))).ToArray<long>();
              foreach (long prjLinkID in array)
              {
                string str3 = this.RelPositionQuantity(relationAttributes, prjLinkID, substitutesSettings);
                stringBuilder.Append(" ");
                stringBuilder.Append(str3);
                if (prjLinkID != ((IEnumerable<long>) array).Last<long>())
                  stringBuilder.Append(", ");
              }
              if (array.Length != 0 && relationID != ((IEnumerable<long>) positionRelationIds).Last<long>())
                stringBuilder.Append(", или");
            }
            string str4 = stringBuilder.ToString();
            if (str4.Length > Consts.MaxStringSize)
              str4 = str4.Substring(0, Consts.MaxStringSize);
            dictionary[num5] = str4;
            continue;
          }
        }
        string empty = string.Empty;
        bool flag3 = this.IsActualVariant(values, relationAttributes.Attributes, num4) || !flag1 && num4 == 0;
        if (flag3 | flag2)
        {
          string str5 = this.UsePlaceholders ? "[ActualSubstitute]" : substitutesSettings.ActualSubstitute;
          if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
            str5 = this.FirstLetterUpper(str5);
          stringBuilder.Append(str5);
          stringBuilder.Append(" ");
          if (count1 > 1)
          {
            string str6 = this.UsePlaceholders ? "[ActualSubstitute2]" : substitutesSettings.ActualSubstitute2;
            stringBuilder.Append(str6);
            stringBuilder.Append(" ");
          }
        }
        else if (count1 > 1)
        {
          string str7 = this.UsePlaceholders ? "[Substitute]" : substitutesSettings.Substitute;
          if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
            str7 = this.FirstLetterUpper(str7);
          stringBuilder.Append(str7);
          stringBuilder.Append(" ");
        }
        int num7 = 1;
        for (int index4 = 0; index4 < count1; ++index4)
        {
          long prjLinkID = longList1[index4];
          if (prjLinkID != num5)
          {
            string str8 = this.RelPositionQuantity(relationAttributes, prjLinkID, substitutesSettings);
            stringBuilder.Append(str8);
            ++num7;
            if (index4 < count1 - 1 && num7 < count1)
              stringBuilder.Append(", ");
          }
        }
        int num8 = 0;
        for (int index5 = 0; index5 < substituteObject.Count; ++index5)
        {
          if (index5 != num4)
          {
            ++num8;
            string str9 = num8 <= 1 || substituteObject.Count <= 2 || num8 >= substituteObject.Count ? " " : substitutesSettings.PositionsSeparator;
            if (stringBuilder.ToString().Length > 0 || str9 != " ")
              stringBuilder.Append(str9);
            stringBuilder.Append(flag3 | flag2 ? (this.UsePlaceholders ? "[ActualSubstitute3]" : substitutesSettings.ActualSubstitute3) : (this.UsePlaceholders ? "[Substitute3]" : substitutesSettings.Substitute3));
            stringBuilder.Append(" ");
            List<long> longList2 = substituteObject[index5];
            int count2 = longList2.Count;
            int num9 = 0;
            for (int index6 = 0; index6 < count2; ++index6)
            {
              long prjLinkID = longList2[index6];
              string str10 = this.RelPositionQuantity(relationAttributes, prjLinkID, substitutesSettings);
              stringBuilder.Append(str10);
              ++num9;
              if (count2 > 1 && index6 == 0)
              {
                stringBuilder.Append(" ");
                if (flag3 | flag2)
                  stringBuilder.Append(this.UsePlaceholders ? "[ActualSubstitute2]" : substitutesSettings.ActualSubstitute2);
                else
                  stringBuilder.Append(this.UsePlaceholders ? "[Substitute2]" : substitutesSettings.Substitute2);
                stringBuilder.Append(" ");
              }
              if (index6 > 0 && index6 < count2 - 1 && num9 < count2)
                stringBuilder.Append(", ");
            }
          }
        }
        string str11 = stringBuilder.ToString();
        if (str11.Length > Consts.MaxStringSize)
          str11 = str11.Substring(0, Consts.MaxStringSize);
        dictionary[num5] = str11.Replace("  ", " ");
      }
    }
    return dictionary;
  }

  /// <summary>Создать отсортированную копию списка связей</summary>
  /// <param name="relations">Список связей</param>
  /// <param name="relAttrs">Список атрибутов связей</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>Отсортированная копия списка связей</returns>
  private List<long> CloneRelationsList(
    List<long> relations,
    RelationAttributesPackage relAttrs,
    int attrID)
  {
    List<long> longList = new List<long>();
    if (relations == null || relAttrs == null || attrID == -1)
      return longList;
    for (int index = 0; index < relations.Count; ++index)
      longList.Add(relations[index]);
    longList.Sort((IComparer<long>) new RelationsComparerByAttr(relAttrs, attrID));
    return longList;
  }

  private string GetPositionDesignation(
    RelationAttributesPackage relationAttributesPackage,
    long relationID)
  {
    int index = relationAttributesPackage.Attributes.IndexOf(SubstitutesConstants.PositionDesignationAttributeTypeID);
    object[] objArray = relationAttributesPackage[relationID];
    return index >= 0 && objArray != null && index < objArray.Length ? objArray[index] as string : (string) null;
  }

  public bool UsePlaceholders { get; set; }
}
