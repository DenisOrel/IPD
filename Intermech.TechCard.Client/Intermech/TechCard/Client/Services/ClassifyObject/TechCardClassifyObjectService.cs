// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.TechCardClassifyObjectService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Kernel.Search;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>Служба классификации объектов TechCard</summary>
internal class TechCardClassifyObjectService : ITechCardClassifyObjectService
{
  /// <summary>Элемент шаблона для генерации номера объекта</summary>
  public const string ObjectNumberTemplateItem = "<%obj_no%>";

  /// <summary>Проверка параметров классификации</summary>
  /// <param name="classifyParams"></param>
  /// <returns></returns>
  private bool ValidateParams(TechCardClassifyObjectParams classifyParams)
  {
    return !ObjInfoItem.IsEmpty((ITypedInfoItem) classifyParams.ClassifyObjectItem) || classifyParams.ClassifyObjectItem.ObjTypeID != -1;
  }

  /// <summary>Расчет шаблона атрибута через ЭC</summary>
  /// <param name="session"></param>
  /// <param name="classifyParams"></param>
  /// <returns></returns>
  private string CalculateAttributeTemplate(
    IUserSession session,
    TechCardClassifyObjectAttributeParams classifyParams)
  {
    IExpertUser service = ServiceUtils.GetService<IExpertUser>((object) ApplicationServices.Container, false);
    if (service == null)
      return string.Empty;
    using (IExpertTask expertTask = service.GetExpertTask())
    {
      List<long> longList = new List<long>();
      longList.Add(classifyParams.ClassifyObjectItem.ObjectID);
      if (classifyParams.ExtraContextObjInfoItems != null)
        longList.AddRange(classifyParams.ExtraContextObjInfoItems.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => item.ObjectID)));
      IEnumerable<AttributeValues> attributeValues = classifyParams.AttributeValues;
      if ((attributeValues != null ? (attributeValues.Any<AttributeValues>() ? 1 : 0) : 0) != 0)
      {
        Dictionary<CalcAttrPair, CalculatedAttr> parms = new Dictionary<CalcAttrPair, CalculatedAttr>();
        foreach (AttributeValues attributeValue in classifyParams.AttributeValues)
        {
          CalcAttrPair calcAttrPair = new CalcAttrPair(classifyParams.ClassifyObjectItem.ObjectID, classifyParams.ClassifyObjectItem.ObjTypeID, attributeValue.AttributeID);
          parms[calcAttrPair] = new CalculatedAttr(calcAttrPair, attributeValue.Value, AttrState.SetByUser);
        }
        expertTask.SetCalcParms(parms);
      }
      object obj;
      int num = (int) expertTask.Calculate(classifyParams.ClassifyObjectItem.ObjTypeID, classifyParams.AttributeId, classifyParams.ContextObjectItem.ObjectID, longList.ToArray(), out obj);
      if (service.ShowTraceWindow)
        ExpertUser.rur.Execute(expertTask.GetTraceInfo(), true);
      if (num == 1)
        return Convert.ToString(obj);
    }
    return string.Empty;
  }

  /// <summary>Классификация объекта для указанного атрибута</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="classifyParams">Параметры классификации</param>
  /// <param name="classifyStrategy">Стратегия для классификации</param>
  /// <param name="attributeValue"> Значение классифицируемого атрибута</param>
  public bool ClassifyObjectAttribute(
    [NotNull] IUserSession session,
    [NotNull] TechCardClassifyObjectAttributeParams classifyParams,
    ITechCardClassifyObjectStrategy classifyStrategy,
    out string attributeValue)
  {
    attributeValue = string.Empty;
    if (!this.ValidateParams((TechCardClassifyObjectParams) classifyParams) || MetaDataHelper.GetAttribute4ObjectType(classifyParams.ClassifyObjectItem.ObjTypeID, classifyParams.AttributeId) == null)
      return false;
    string format = (string) null;
    if (classifyParams.UseExpertService)
      format = this.CalculateAttributeTemplate(session, classifyParams);
    if (classifyStrategy != null && string.IsNullOrEmpty(format))
      format = classifyStrategy.GetClassifyTemplate(session, (TechCardClassifyObjectParams) classifyParams);
    if (string.IsNullOrEmpty(format))
      return false;
    int length = format.IndexOf("<%obj_no%>", StringComparison.InvariantCultureIgnoreCase);
    if (length < 0)
    {
      attributeValue = format;
      return true;
    }
    string conditionValue = format.Substring(0, length);
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.Append(conditionValue);
      stringBuilder.Append("{0}");
      int startIndex = length + "<%obj_no%>".Length;
      if (startIndex < format.Length)
        stringBuilder.Append(format.Substring(startIndex, format.Length - startIndex));
      format = stringBuilder.ToString();
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(classifyParams.ClassifyObjectItem.ObjTypeID);
    if (objectCollection == null)
      return false;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(classifyParams.AttributeId, RelationalOperators.StartString, (object) conditionValue, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) classifyParams.AttributeId, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    });
    DataTable dataTable = objectCollection.Select(paramSet);
    int num = 1;
    if (dataTable != null && dataTable.Rows.Count != 0)
    {
      num = 0;
      string str = MetaDataHelper.GetAttributeTypeGuid(classifyParams.AttributeId).ToString();
      string filterExpression;
      do
      {
        ++num;
        filterExpression = $"[{str}]={DataSetProcessor.QString(string.Format(format, (object) num))}";
      }
      while (dataTable.Select(filterExpression).Length != 0);
    }
    attributeValue = string.Format(format, (object) num);
    return true;
  }

  /// <summary>Получения пост фикса типа объекта по его Id</summary>
  /// <param name="objectTypeId"></param>
  /// <returns></returns>
  internal static string GetObjectTypePostfix(int objectTypeId)
  {
    return MetaDataHelper.GetObjectType(objectTypeId)?.ShortName ?? string.Empty;
  }
}
