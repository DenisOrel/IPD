
// Type: Intermech.Client.Core.FormDesigner.Controls.ExpertSystem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Класс для упрощенного использования экспертной системы.
/// </summary>
internal class ExpertSystem
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static bool IsExpertSystemExists()
  {
    return ServiceUtils.GetService<IExpertUser>((object) ApplicationServices.Container, false) != null;
  }

  /// <summary>Рассчитать значение атрибута.</summary>
  /// <param name="info">Информация об элементе, которому принадлежит атрибут</param>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="desForm">Форма</param>
  public static void Calculate(IElementInfo info, Guid attrGuid, DesForm desForm)
  {
    ExpertSystem.ExecuteCalculate(info.ElementIdentifier, attrGuid, desForm, true);
  }

  /// <summary>Пересчитать значение атрибута.</summary>
  /// <param name="id">Идентификатор элемента, которому принадлежит атрибут</param>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="desForm">Форма</param>
  public static void ReCalculate(long id, Guid attrGuid, DesForm desForm)
  {
    ExpertSystem.ExecuteCalculate(id, attrGuid, desForm, false);
  }

  /// <summary>Запуск расчета.</summary>
  /// <param name="id">Идентификатор элемента, которому принадлежит атрибут</param>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="desForm">Форма</param>
  /// <param name="isCalc">Рассчет/пересчет</param>
  private static void ExecuteCalculate(long id, Guid attrGuid, DesForm desForm, bool isCalc)
  {
    if (desForm == null)
      return;
    IExpertUser service = ServiceUtils.GetService<IExpertUser>((object) ApplicationServices.Container, true);
    using (IExpertTask expertTask = service.GetExpertTask())
    {
      int attributeId = MetaDataHelper.GetAttributeID((object) attrGuid);
      IElementInfo info = desForm.Info;
      IElementInfo relationInfo = desForm.RelationInfo;
      long baseID = info.ElementIdentifier;
      long moreID = 0;
      IEnumerable<AttributeValues> attributeValues = ExpertSystem.GetAttributeValues(desForm, baseID);
      IEnumerable<AttributeValues> attributeValueses = (IEnumerable<AttributeValues>) null;
      if (relationInfo != null)
      {
        moreID = relationInfo.ElementIdentifier;
        attributeValueses = ExpertSystem.GetAttributeValues(desForm, moreID);
      }
      int num = -1;
      Action<IExpertTask, int, long, int> action;
      if (isCalc)
      {
        action = new Action<IExpertTask, int, long, int>(ExpertSystem.Calc);
        if (id == baseID)
        {
          ExpertSystem.ClearCalcAttrValue(attributeId, attributeValues);
          num = desForm.ElementTypeID;
        }
        else
        {
          ExpertSystem.ClearCalcAttrValue(attributeId, attributeValueses);
          num = desForm.RelationTypeID;
        }
      }
      else
        action = new Action<IExpertTask, int, long, int>(ExpertSystem.ReCalc);
      Dictionary<CalcAttrPair, CalculatedAttr> dictionary1 = ExpertSystem.GetCalcParams(attributeValues, baseID).Union<KeyValuePair<CalcAttrPair, CalculatedAttr>>((IEnumerable<KeyValuePair<CalcAttrPair, CalculatedAttr>>) ExpertSystem.GetCalcParams(attributeValueses, moreID)).ToDictionary<KeyValuePair<CalcAttrPair, CalculatedAttr>, CalcAttrPair, CalculatedAttr>((Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, CalcAttrPair>) (x => x.Key), (Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, CalculatedAttr>) (x => x.Value));
      expertTask.SetCalcParms(dictionary1);
      action(expertTask, attributeId, id, num);
      if (service.ShowTraceWindow)
        expertTask.ShowTraceDialog();
      Dictionary<CalcAttrPair, CalculatedAttr> modifiedParms = expertTask.GetModifiedParms();
      if (modifiedParms == null || modifiedParms.Count <= 0)
        return;
      Dictionary<int, CalculatedAttr> dictionary2 = modifiedParms.Where<KeyValuePair<CalcAttrPair, CalculatedAttr>>((Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, bool>) (x => x.Key.objID == baseID)).ToDictionary<KeyValuePair<CalcAttrPair, CalculatedAttr>, int, CalculatedAttr>((Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, int>) (x => x.Key.attrTypeID), (Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, CalculatedAttr>) (x => x.Value));
      Dictionary<int, CalculatedAttr> computedValues = (Dictionary<int, CalculatedAttr>) null;
      if (moreID != 0L)
        computedValues = modifiedParms.Where<KeyValuePair<CalcAttrPair, CalculatedAttr>>((Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, bool>) (x => x.Key.objID == moreID)).ToDictionary<KeyValuePair<CalcAttrPair, CalculatedAttr>, int, CalculatedAttr>((Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, int>) (x => x.Key.attrTypeID), (Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, CalculatedAttr>) (x => x.Value));
      if (!modifiedParms.FirstOrDefault<KeyValuePair<CalcAttrPair, CalculatedAttr>>((Func<KeyValuePair<CalcAttrPair, CalculatedAttr>, bool>) (x => x.Key.objID != baseID && x.Key.objID != moreID && x.Key.objID != -1L)).Equals((object) new KeyValuePair<CalcAttrPair, CalculatedAttr>()))
        ExpertSystem.ShowApplyForm(expertTask, info, relationInfo);
      List<AttributeValues> ofAttributeValues1 = ExpertSystem.ToListOfAttributeValues(attributeValues, dictionary2);
      List<AttributeValues> ofAttributeValues2 = ExpertSystem.ToListOfAttributeValues(attributeValueses, computedValues);
      desForm.AttributeChanging((IEnumerable<AttributeValues>) ofAttributeValues1, (IEnumerable<AttributeValues>) ofAttributeValues2);
    }
  }

  /// <summary>Расчет.</summary>
  /// <param name="expTask">Задача ЭС</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="elementID">Идентификатор элемента, которому принадлежит атрибут</param>
  /// <param name="elementType">Тип элемента, которому принадлежит атрибут</param>
  private static void Calc(IExpertTask expTask, int attrID, long elementID, int elementType = -1)
  {
    object obj = (object) null;
    int num = (int) expTask.Calculate(elementType, attrID, elementID, out obj);
  }

  /// <summary>Пересчет.</summary>
  /// <param name="expTask">Задача ЭС</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="elementID">Идентификатор элемента, которому принадлежит атрибут</param>
  /// <param name="elementType">Тип элемента, которому принадлежит атрибут</param>
  private static void ReCalc(IExpertTask expTask, int attrID, long elementID, int elementType = -1)
  {
    expTask.Recalculate(attrID, elementID);
  }

  /// <summary>Получение атрибутов объекта/связи.</summary>
  /// <param name="desForm">Форма</param>
  /// <param name="ID">Идентификатор объекта/связи</param>
  /// <returns>Список значений атрибутов</returns>
  private static IEnumerable<AttributeValues> GetAttributeValues(DesForm desForm, long ID)
  {
    IEnumerable<AttributeValues> attributeValues = (IEnumerable<AttributeValues>) desForm.GetAttributeValuesFromControls(ID);
    IEnumerable<AttributeValues> additionalValues = (IEnumerable<AttributeValues>) desForm.GetAdditionalValues(ID);
    if (attributeValues.Count<AttributeValues>() == 0)
      attributeValues = additionalValues;
    else if (additionalValues.Count<AttributeValues>() > 0)
      attributeValues = attributeValues.Union<AttributeValues>(additionalValues);
    return attributeValues;
  }

  /// <summary>Очистить значение указанного атрибута.</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="values">Значения атрибутов</param>
  private static void ClearCalcAttrValue(int attrID, IEnumerable<AttributeValues> values)
  {
    if (values == null)
      return;
    AttributeValues attributeValues = values.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
    if (attributeValues == null)
      return;
    attributeValues.Values = new object[1]
    {
      (object) DBNull.Value
    };
  }

  /// <summary>Подготовить список параметров для рассчета.</summary>
  /// <param name="AVs">Список значений атрибутов</param>
  /// <param name="ID">Идентификатор объекта/связи</param>
  /// <returns>Список параметров</returns>
  public static Dictionary<CalcAttrPair, CalculatedAttr> GetCalcParams(
    IEnumerable<AttributeValues> AVs,
    long ID)
  {
    Dictionary<CalcAttrPair, CalculatedAttr> calcParams = new Dictionary<CalcAttrPair, CalculatedAttr>();
    if (AVs != null)
    {
      foreach (AttributeValues av in AVs)
      {
        int attributeId = av.AttributeID;
        object[] values = av.Values;
        object Val = (object) null;
        if (values.Length > 1)
        {
          for (int index = 0; index < values.Length; ++index)
          {
            if (values[index] == DBNull.Value)
              values[index] = (object) null;
          }
          Val = (object) values;
        }
        else if (values.Length != 0)
        {
          object obj = values[0];
          Val = obj == DBNull.Value ? (object) null : obj;
        }
        CalcAttrPair calcAttrPair = new CalcAttrPair(ID, attributeId);
        CalculatedAttr calculatedAttr = new CalculatedAttr(calcAttrPair, Val);
        calcParams.Add(calcAttrPair, calculatedAttr);
      }
    }
    return calcParams;
  }

  /// <summary>
  /// Показать форму для сохранения изменений в объектах не связанных с указанной формой.
  /// </summary>
  /// <param name="expTask">Задача ЭС</param>
  /// <param name="baseEI">Информация об основном объекте/связи</param>
  /// <param name="moreEI">Информация о дополнительной связи</param>
  private static void ShowApplyForm(IExpertTask expTask, IElementInfo baseEI, IElementInfo moreEI)
  {
    long excludeObjId = 0;
    long elementIdentifier;
    if (baseEI.ElementKind == AttributableElements.Object)
    {
      excludeObjId = baseEI.ElementIdentifier;
      elementIdentifier = moreEI != null ? moreEI.ElementIdentifier : 0L;
    }
    else
      elementIdentifier = baseEI.ElementIdentifier;
    expTask.ShowApplyForm(excludeObjId, elementIdentifier);
  }

  /// <summary>
  /// Преобразование расчитанных значений в список значений атрибутов.
  /// </summary>
  /// <param name="AVs">Список значений атрибутов, полученных у формы</param>
  /// <param name="computedValues">Измененные значения при рассчете</param>
  /// <returns>Список измененных значений атрибутов</returns>
  private static List<AttributeValues> ToListOfAttributeValues(
    IEnumerable<AttributeValues> AVs,
    Dictionary<int, CalculatedAttr> computedValues)
  {
    List<AttributeValues> ofAttributeValues = (List<AttributeValues>) null;
    if (computedValues != null && computedValues.Count > 0)
    {
      ofAttributeValues = new List<AttributeValues>(computedValues.Count);
      IEnumerable<AttributeValues> source = AVs ?? (IEnumerable<AttributeValues>) new List<AttributeValues>(0);
      foreach (KeyValuePair<int, CalculatedAttr> computedValue in computedValues)
      {
        int attrID = computedValue.Key;
        object obj1 = computedValue.Value.Value;
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
        object[] objArray1;
        object[] objArray2;
        object obj2;
        switch (obj1)
        {
          case ICollection c:
            objArray1 = new ArrayList(c).ToArray();
            objArray2 = objArray1;
            goto label_8;
          case ExpertValue expertValue:
            obj2 = expertValue.Value;
            break;
          default:
            obj2 = obj1;
            break;
        }
        object obj3 = obj2;
        string str1 = Convert.ToString(obj3);
        objArray2 = new object[1]{ (object) str1 };
        objArray1 = new object[1]
        {
          attributeType.FieldType == FieldTypes.ftString ? (object) str1 : obj3
        };
label_8:
        if (attributeType.FieldType == FieldTypes.ftObjectLink)
        {
          string str2 = LocalizationHolder.rm.GetString("LinkedControl.AttributeDescriptionText");
          IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
          for (int index = 0; index < objArray1.Length; ++index)
          {
            object obj4 = objArray1[index];
            long result = 0;
            if (long.TryParse(Convert.ToString(obj4), out result))
            {
              QuickObjectInfo objectInfo = service.GetObjectInfo(result);
              objArray2[index] = !string.IsNullOrEmpty(objectInfo.Caption) ? (object) objectInfo.Caption : (object) $"{str2} №{result.ToString()}";
            }
            else
              objArray2[index] = (object) DBNull.Value;
          }
        }
        AttributeValues attributeValues1 = source.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
        if (attributeValues1 == null)
          attributeValues1 = new AttributeValues(attrID)
          {
            AttributeType = attributeType.FieldType
          };
        AttributeValues attributeValues2 = attributeValues1;
        attributeValues2.Descriptions = objArray2;
        attributeValues2.Values = objArray1;
        ofAttributeValues.Add(attributeValues2);
      }
    }
    return ofAttributeValues;
  }
}
