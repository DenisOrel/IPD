// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ProductInfo
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.AVS;

/// <summary>Информация об исполнении специфицируемого изделия</summary>
[Serializable]
public class ProductInfo : ISerializable
{
  /// <summary>Идентификатор версии объекта</summary>
  public long Id = -1;
  /// <summary>Идентификатор объекта</summary>
  public long F_ID = -1;
  /// <summary>Тип объекта</summary>
  public int ObjectType = -1;
  /// <summary>Глобальный идентификатор версии объекта</summary>
  public Guid Guid = Guid.Empty;
  /// <summary>Идентификатор группового изделия</summary>
  public Guid ArticleGroupID = Guid.Empty;
  /// <summary>Идентификатор пользователя взявшего на изменение</summary>
  internal long CheckoutBy = -1;
  /// <summary>Обозначение</summary>
  private string designation;
  /// <summary>Номер исполнения, для граф Количество, полученный из атрибута объекта</summary>
  private string number;
  /// <summary>Сгенерированный номер исполнения, для заполнения заголовка граф Количество</summary>
  public string generatedNumber;
  /// <summary>Наименование</summary>
  public string Name;
  /// <summary>Литера</summary>
  public string Litera;
  /// <summary>Код исполнения</summary>
  public string ProductKod;
  /// <summary>Код ОКП</summary>
  public string ProductOKPCode;
  /// <summary>Дата модификации объекта в БД</summary>
  public DateTime ModifyDate;
  /// <summary>Ссылка на объект-прототип.
  /// <remarks>
  /// Если "Родительская версия" и "Ссылка на прототип" заполнены, то этот объект создавался как версия
  /// а атрибут "Ссылка на прототип" просто скопировался
  /// При создании по прототипу Родительская версия НЕ копируется и атрибут должен быть пустым.
  /// </remarks>
  ///  </summary>
  public long PrototypeId = -1;
  /// <summary>Родительская версия исполнения
  /// <remarks>
  /// Если "Родительская версия" и "Ссылка на прототип" заполнены, то этот объект создавался как версия
  /// а атрибут "Ссылка на прототип" просто скопировался
  /// При создании по прототипу Родительская версия НЕ копируется и атрибут должен быть пустым.
  /// </remarks>
  ///  </summary>
  public long ParentVersionId = -1;
  /// <summary>Идентификатор версии Спецификации, взятый со связи с документом.
  /// Используется только для проверки правильности связей</summary>
  [NonSerialized]
  internal long DocumentId = -1;
  /// <summary>Атрибуты исполнений для вывода в документе</summary>
  internal Dictionary<int, string> AdditionalAttributes = new Dictionary<int, string>();
  /// <summary>Внутренняя переменная для вспомогательных ссылок</summary>
  internal object Tag;

  /// <summary>Конструктор</summary>
  internal ProductInfo()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="guid">Глобальный идентификатор версии исполнения</param>
  /// <param name="id">Идентификатор версии исполнения</param>
  /// <param name="name">Наименование исполнения</param>
  /// <param name="designation">Наименование исполнения</param>
  public ProductInfo(Guid guid, long id, string name, string designation = null)
  {
    this.Guid = guid;
    this.Id = id;
    this.Name = name;
    this.Designation = designation;
  }

  /// <summary>Конструктор</summary>
  /// <param name="productObj">Объект БД исполнения</param>
  /// <param name="attrList">Список атрибутов исполнения, которые требуется загрузить</param>
  /// <param name="documentDesignationSuffix">Суффикс обозначения документа</param>
  public ProductInfo(IDBObject productObj, List<int> attrList = null, string documentDesignationSuffix = null)
  {
    this.Id = productObj.ObjectID;
    this.Guid = productObj.ObjectGUID;
    this.UpdateInfo(productObj, attrList, documentDesignationSuffix);
  }

  /// <summary>Обновить информацию об исполнении</summary>
  /// <param name="attrList">Список атрибутов исполнения, которые требуется загрузить</param>
  /// <param name="documentDesignationSuffix">Суффикс обозначения документа</param>
  public void UpdateInfo(List<int> attrList, string documentDesignationSuffix)
  {
    this.Designation = (string) null;
    this.Name = (string) null;
    this.Litera = (string) null;
    if (this.Id != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.UpdateInfo(sessionKeeper.Session.GetObject(this.Id), attrList, documentDesignationSuffix);
    }
    else
    {
      if (!(this.Guid != Guid.Empty))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject productObj = sessionKeeper.Session.GetObject(this.Guid, false);
        if (productObj == null)
          return;
        this.UpdateInfo(productObj, attrList, documentDesignationSuffix);
      }
    }
  }

  /// <summary>Создать список описателей колонок для запроса в БД</summary>
  /// <param name="attrList">Список дополнительных атрибутов</param>
  /// <param name="includeStdColumns">Добавлять стандартные атрибуты</param>
  /// <param name="includeVersionInRelation">Включена загрузка конкретизации версии на связи</param>
  /// <returns></returns>
  public static List<ColumnDescriptor> CreateColumnDescriptors(
    List<int> attrList,
    bool includeStdColumns,
    bool includeVersionInRelation)
  {
    List<ColumnDescriptor> columnDescriptors = new List<ColumnDescriptor>();
    columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_ArticleGroupID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_ObjectPrototype, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PARENT_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0));
    if (includeStdColumns)
    {
      columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_Name, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_ProductCode, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_Litera, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_ProductConventionalName, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_OKPCode, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_CHKOUT_BY, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0));
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_ContentModifyDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    }
    if (includeVersionInRelation)
      columnDescriptors.Add(new ColumnDescriptor((object) AvsIDCache.Attr_VersionInRelation, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    if (attrList != null && attrList.Count > 0)
    {
      Dictionary<int, ColumnDescriptor> dictionary = new Dictionary<int, ColumnDescriptor>(attrList.Count + columnDescriptors.Count);
      for (int index = 0; index < columnDescriptors.Count; ++index)
        dictionary.Add(Convert.ToInt32(columnDescriptors[index].AttributeID), columnDescriptors[index]);
      for (int index = 0; index < attrList.Count; ++index)
      {
        if (!dictionary.ContainsKey(attrList[index]))
        {
          columnDescriptors.Add(new ColumnDescriptor((object) attrList[index], AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
          dictionary.Add(attrList[index], columnDescriptors[columnDescriptors.Count - 1]);
        }
      }
    }
    return columnDescriptors;
  }

  /// <summary>Зачитать данные исполнений из результата запроса</summary>
  /// <param name="products">Таблица с атрибутами исполнений. Результат запроса в БД</param>
  /// <param name="columnDescriptors">Список атрибутов в таблице исполнений</param>
  /// <param name="includeStdColumns">Загружен стандартный набор атрибутов</param>
  /// <param name="includeVersionInRelation">Включена загрузка конкретизации версии на связи</param>
  internal static List<ProductInfo> ReadProductsInfo(
    DataTable products,
    List<ColumnDescriptor> columnDescriptors,
    bool includeStdColumns,
    bool includeVersionInRelation)
  {
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    foreach (DataRow row in (InternalDataCollectionBase) products.Rows)
    {
      ProductInfo productInfo1 = new ProductInfo();
      int num1 = 0;
      ProductInfo productInfo2 = productInfo1;
      DataRow dataRow1 = row;
      int columnIndex1 = num1;
      int num2 = columnIndex1 + 1;
      long int64_1 = Convert.ToInt64(dataRow1[columnIndex1]);
      productInfo2.Id = int64_1;
      ProductInfo productInfo3 = productInfo1;
      DataRow dataRow2 = row;
      int columnIndex2 = num2;
      int num3 = columnIndex2 + 1;
      long int64_2 = Convert.ToInt64(dataRow2[columnIndex2]);
      productInfo3.F_ID = int64_2;
      ProductInfo productInfo4 = productInfo1;
      DataRow dataRow3 = row;
      int columnIndex3 = num3;
      int columnIndex4 = columnIndex3 + 1;
      int int32_1 = Convert.ToInt32(dataRow3[columnIndex3]);
      productInfo4.ObjectType = int32_1;
      if (row[columnIndex4] != DBNull.Value)
        productInfo1.ArticleGroupID = new Guid(Convert.ToString(row[columnIndex4]));
      int columnIndex5 = columnIndex4 + 1;
      if (row[columnIndex5] != DBNull.Value)
        productInfo1.PrototypeId = Convert.ToInt64(row[columnIndex5]);
      int columnIndex6 = columnIndex5 + 1;
      if (row[columnIndex6] != DBNull.Value)
        productInfo1.ParentVersionId = Convert.ToInt64(row[columnIndex6]);
      int num4 = columnIndex6 + 1;
      if (includeStdColumns)
      {
        ProductInfo productInfo5 = productInfo1;
        DataRow dataRow4 = row;
        int columnIndex7 = num4;
        int num5 = columnIndex7 + 1;
        Guid guid = new Guid(Convert.ToString(dataRow4[columnIndex7]));
        productInfo5.Guid = guid;
        ProductInfo productInfo6 = productInfo1;
        DataRow dataRow5 = row;
        int columnIndex8 = num5;
        int num6 = columnIndex8 + 1;
        string str1 = Convert.ToString(dataRow5[columnIndex8]);
        productInfo6.Designation = str1;
        ProductInfo productInfo7 = productInfo1;
        DataRow dataRow6 = row;
        int columnIndex9 = num6;
        int num7 = columnIndex9 + 1;
        string str2 = Convert.ToString(dataRow6[columnIndex9]);
        productInfo7.Name = str2;
        ProductInfo productInfo8 = productInfo1;
        DataRow dataRow7 = row;
        int columnIndex10 = num7;
        int num8 = columnIndex10 + 1;
        string str3 = Convert.ToString(dataRow7[columnIndex10]);
        productInfo8.number = str3;
        ProductInfo productInfo9 = productInfo1;
        DataRow dataRow8 = row;
        int columnIndex11 = num8;
        int num9 = columnIndex11 + 1;
        string str4 = Convert.ToString(dataRow8[columnIndex11]);
        productInfo9.Litera = str4;
        ProductInfo productInfo10 = productInfo1;
        DataRow dataRow9 = row;
        int columnIndex12 = num9;
        int num10 = columnIndex12 + 1;
        string str5 = Convert.ToString(dataRow9[columnIndex12]);
        productInfo10.ProductKod = str5;
        ProductInfo productInfo11 = productInfo1;
        DataRow dataRow10 = row;
        int columnIndex13 = num10;
        int num11 = columnIndex13 + 1;
        string str6 = Convert.ToString(dataRow10[columnIndex13]);
        productInfo11.ProductOKPCode = str6;
        ProductInfo productInfo12 = productInfo1;
        DataRow dataRow11 = row;
        int columnIndex14 = num11;
        int columnIndex15 = columnIndex14 + 1;
        long int64_3 = Convert.ToInt64(dataRow11[columnIndex14]);
        productInfo12.CheckoutBy = int64_3;
        if (row[columnIndex15] != null && row[columnIndex15] != DBNull.Value)
        {
          productInfo1.ModifyDate = Convert.ToDateTime(row[columnIndex15]);
          productInfo1.ModifyDate = new DateTime(productInfo1.ModifyDate.Year, productInfo1.ModifyDate.Month, productInfo1.ModifyDate.Day, productInfo1.ModifyDate.Hour, productInfo1.ModifyDate.Minute, 0);
        }
        num4 = columnIndex15 + 1;
      }
      if (includeVersionInRelation)
        productInfo1.DocumentId = AvsIDCache.ConvertDbValueToInt64(row[num4++]);
      for (int index = num4; index < columnDescriptors.Count; ++index)
      {
        int int32_2 = Convert.ToInt32(columnDescriptors[index].AttributeID);
        if (productInfo1.AdditionalAttributes.ContainsKey(int32_2))
          productInfo1.AdditionalAttributes[int32_2] = Convert.ToString(row[index]);
        else
          productInfo1.AdditionalAttributes.Add(int32_2, Convert.ToString(row[index]));
      }
      productInfoList.Add(productInfo1);
    }
    if (productInfoList.Count > 1)
    {
      List<int> intList = new List<int>(productInfoList.Count);
      int num = -1;
      bool flag = false;
      for (int index = 0; index < productInfoList.Count; ++index)
      {
        if (index == 0 || intList.IndexOf(productInfoList[index].ObjectType) != -1)
        {
          intList.Add(productInfoList[index].ObjectType);
          if (num == -1)
          {
            num = productInfoList[index].ObjectType;
            flag = MetaDataHelper.IsObjectTypeChildOf(num, AvsIDCache.ObjType_Product) || MetaDataHelper.IsObjectTypeChildOf(num, AvsIDCache.ObjType_Orders);
          }
          else if (!flag && (MetaDataHelper.IsObjectTypeChildOf(productInfoList[index].ObjectType, AvsIDCache.ObjType_Product) || MetaDataHelper.IsObjectTypeChildOf(num, AvsIDCache.ObjType_Orders)))
          {
            num = productInfoList[index].ObjectType;
            flag = true;
          }
        }
      }
      for (int index = productInfoList.Count - 1; index > 0; --index)
      {
        if (!MetaDataHelper.IsObjectTypeChildOf(productInfoList[index].ObjectType, num))
          productInfoList.RemoveAt(index);
      }
    }
    return productInfoList;
  }

  /// <summary>Обновить информацию об исполнении</summary>
  /// <param name="productObj">Объект БД исполнения</param>
  /// <param name="attrList">Список атрибутов исполнения, которые требуется загрузить</param>
  /// <param name="documentDesignationSuffix">Суффикс обозначения документа</param>
  public void UpdateInfo(
    IDBObject productObj,
    List<int> attrList,
    string documentDesignationSuffix)
  {
    this.Designation = (string) null;
    this.Name = (string) null;
    this.Litera = (string) null;
    this.PrototypeId = -1L;
    AttributeValues[] attributesValues = productObj.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
    this.ParentVersionId = productObj.ParentVersionID;
    for (int index = 0; index < attributesValues.Length; ++index)
    {
      if (attributesValues[index].AttributeID == -2)
        this.Id = Convert.ToInt64(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == -12)
        this.Guid = new Guid(Convert.ToString(attributesValues[index].Values[0]));
      else if (attributesValues[index].AttributeID == -3)
        this.F_ID = Convert.ToInt64(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == -7)
        this.ObjectType = Convert.ToInt32(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_ArticleGroupID)
        this.ArticleGroupID = AvsIDCache.ConvertToGuid(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == -6)
        this.CheckoutBy = Convert.ToInt64(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_ObjectPrototype)
        this.PrototypeId = attributesValues[index].Values[0] != DBNull.Value ? Convert.ToInt64(attributesValues[index].Values[0]) : -1L;
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_Designation)
      {
        this.Designation = Convert.ToString(attributesValues[index].Values[0]);
        if (!string.IsNullOrEmpty(documentDesignationSuffix) && MetaDataHelper.IsObjectTypeChildOf(productObj.ObjectType, AvsIDCache.ObjType_Document))
          this.Designation = AVSDocument.FindProductDesignation(this.Designation, documentDesignationSuffix);
      }
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_ProductCode)
        this.number = Convert.ToString(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_Name)
        this.Name = Convert.ToString(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_Litera)
        this.Litera = Convert.ToString(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_ProductConventionalName)
        this.ProductKod = Convert.ToString(attributesValues[index].Values[0]);
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_ContentModifyDate)
      {
        this.ModifyDate = Convert.ToDateTime(attributesValues[index].Values[0]);
        this.ModifyDate = new DateTime(this.ModifyDate.Year, this.ModifyDate.Month, this.ModifyDate.Day, this.ModifyDate.Hour, this.ModifyDate.Minute, 0);
      }
      else if (attributesValues[index].AttributeID == AvsIDCache.Attr_OKPCode)
        this.ProductOKPCode = Convert.ToString(attributesValues[index].Values[0]);
      else if (attrList != null && attrList.Contains(attributesValues[index].AttributeID))
      {
        if (this.AdditionalAttributes.ContainsKey(attributesValues[index].AttributeID))
          this.AdditionalAttributes[attributesValues[index].AttributeID] = Convert.ToString(attributesValues[index].Values[0]);
        else
          this.AdditionalAttributes.Add(attributesValues[index].AttributeID, Convert.ToString(attributesValues[index].Values[0]));
      }
    }
  }

  /// <summary>Обновить информацию об исполнении</summary>
  /// <param name="productObj">Объект БД исполнения</param>
  /// <param name="attrList">Список атрибутов исполнения, которые требуется загрузить</param>
  /// <param name="documentDesignationSuffix">Суффикс обозначения документа</param>
  public void UpdateInfo(ProductInfo src)
  {
    this.Id = src.Id;
    this.F_ID = src.F_ID;
    this.ObjectType = src.ObjectType;
    this.Guid = src.Guid;
    this.ArticleGroupID = src.ArticleGroupID;
    this.CheckoutBy = src.CheckoutBy;
    this.designation = src.designation;
    this.number = src.number;
    this.generatedNumber = src.generatedNumber;
    this.Name = src.Name;
    this.Litera = src.Litera;
    this.ProductKod = src.ProductKod;
    this.ProductOKPCode = src.ProductOKPCode;
    this.ModifyDate = src.ModifyDate;
    this.PrototypeId = src.PrototypeId;
    this.ParentVersionId = src.ParentVersionId;
    this.DocumentId = src.DocumentId;
    this.AdditionalAttributes = src.AdditionalAttributes != null ? new Dictionary<int, string>((IDictionary<int, string>) src.AdditionalAttributes) : (Dictionary<int, string>) null;
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  public string GetAttributeValue(int attributeID)
  {
    switch (attributeID)
    {
      case -7:
        return this.ObjectType.ToString();
      case -3:
        return this.F_ID.ToString();
      case -2:
        return this.Id.ToString();
      default:
        if (attributeID == AvsIDCache.Attr_ArticleGroupID)
          return this.ArticleGroupID.ToString();
        if (attributeID == -6)
          return this.CheckoutBy.ToString();
        if (attributeID == -12)
          return this.Guid.ToString();
        if (attributeID == AvsIDCache.Attr_Designation)
          return this.Designation;
        if (attributeID == AvsIDCache.Attr_Name)
          return this.Name;
        if (attributeID == AvsIDCache.Attr_Litera)
          return this.Litera;
        if (attributeID == AvsIDCache.Attr_ProductConventionalName)
          return this.ProductKod;
        if (attributeID == AvsIDCache.Attr_OKPCode)
          return this.ProductOKPCode;
        if (attributeID == AvsIDCache.Attr_ProductCode)
          return this.number;
        if (attributeID == AvsIDCache.Attr_ContentModifyDate)
          return this.ModifyDate.ToLongDateString();
        return this.AdditionalAttributes.ContainsKey(attributeID) ? this.AdditionalAttributes[attributeID] : (string) null;
    }
  }

  /// <summary>Есть ли атрибут у исполнения</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  public bool HasAttribute(int attributeID)
  {
    return attributeID == -2 || attributeID == -3 || attributeID == -7 || attributeID == -6 || attributeID == -12 || attributeID == AvsIDCache.Attr_Designation || attributeID == AvsIDCache.Attr_Name || attributeID == AvsIDCache.Attr_Litera || attributeID == AvsIDCache.Attr_ProductConventionalName || attributeID == AvsIDCache.Attr_OKPCode || attributeID == AvsIDCache.Attr_ProductCode || attributeID == AvsIDCache.Attr_ContentModifyDate || this.AdditionalAttributes.ContainsKey(attributeID);
  }

  /// <summary>Назначить новое значение атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="value">Значение</param>
  /// <param name="saveToDB">Сохранить значение атрибута в БД</param>
  public void SetAttributeValue(int attributeID, object value, bool saveToDB)
  {
    if (attributeID == -1)
      throw new ArgumentException("Недопустимое значение параметра: \"-1\"", nameof (attributeID));
    string g = (string) null;
    if (value != null)
      g = value.ToString();
    switch (attributeID)
    {
      case -7:
        this.ObjectType = value == null ? -1 : Convert.ToInt32(value);
        break;
      case -3:
        this.F_ID = value == null ? -1L : Convert.ToInt64(value);
        break;
      case -2:
        this.Id = value == null ? -1L : Convert.ToInt64(value);
        break;
      default:
        if (attributeID == AvsIDCache.Attr_ArticleGroupID)
        {
          this.ArticleGroupID = AvsIDCache.ConvertToGuid(value);
          break;
        }
        switch (attributeID)
        {
          case -12:
            this.Guid = value != null ? new Guid(g) : Guid.Empty;
            break;
          case -6:
            this.CheckoutBy = value == null ? -1L : Convert.ToInt64(value);
            break;
          default:
            if (attributeID == AvsIDCache.Attr_Designation)
            {
              this.Designation = g;
              break;
            }
            if (attributeID == AvsIDCache.Attr_Name)
            {
              this.Name = g;
              break;
            }
            if (attributeID == AvsIDCache.Attr_Litera)
            {
              this.Litera = g;
              break;
            }
            if (attributeID == AvsIDCache.Attr_ProductConventionalName)
            {
              this.ProductKod = g;
              break;
            }
            if (attributeID == AvsIDCache.Attr_OKPCode)
            {
              this.ProductOKPCode = g;
              break;
            }
            if (attributeID == AvsIDCache.Attr_ProductCode)
            {
              this.number = g;
              break;
            }
            if (attributeID == AvsIDCache.Attr_ContentModifyDate)
            {
              this.ModifyDate = Convert.ToDateTime(value);
              this.ModifyDate = new DateTime(this.ModifyDate.Year, this.ModifyDate.Month, this.ModifyDate.Day, this.ModifyDate.Hour, this.ModifyDate.Minute, 0);
              break;
            }
            if (this.AdditionalAttributes.ContainsKey(attributeID))
            {
              this.AdditionalAttributes[attributeID] = g;
              break;
            }
            this.AdditionalAttributes.Add(attributeID, g);
            break;
        }
        break;
    }
    if (!(this.Id != -1L & saveToDB))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(this.Id).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(attributeID, value)
      });
  }

  /// <summary>Получить номер исполнения</summary>
  /// <param name="prevNumber">Предыдущий номер</param>
  /// <param name="currentNumber">Возвращает текущий номер, если он числовой, иначе -1</param>
  /// <param name="specificationDesignation">Обозначение групповой СП</param>
  /// <param name="useSameProductDesignations">Использовать одинаковые обозначения исполнений с номером исполнения в конце</param>
  /// <returns></returns>
  public string GetNumber(
    int prevNumber,
    out int currentNumber,
    string specificationDesignation,
    bool useSameProductDesignations)
  {
    string str = this.GetNumber(specificationDesignation, useSameProductDesignations);
    currentNumber = -1;
    if (str != null && str != "" && !NumberParserAdvanced.TryParseInt32FromAnyText(str, out currentNumber))
      currentNumber = -1;
    if (useSameProductDesignations && (str == null || str == "") && this.Designation == specificationDesignation)
    {
      currentNumber = 0;
      return "-";
    }
    if (prevNumber != -1)
    {
      if (currentNumber == -1)
        currentNumber = prevNumber + 1;
      if (str == null || str == "")
        str = useSameProductDesignations ? currentNumber.ToString("d2") : currentNumber.ToString();
    }
    else if (useSameProductDesignations)
    {
      if (currentNumber == -1)
        currentNumber = 0;
      if (str == null || str == "")
        str = "-";
    }
    else
    {
      if (currentNumber == -1)
        currentNumber = 1;
      if (str == null || str == "")
        str = "1";
    }
    return str;
  }

  /// <summary>Получить номер исполнения</summary>
  /// <param name="specificationDesignation">Обозначение групповой СП</param>
  /// <param name="useSameProductDesignations">Использовать одинаковые обозначения исполнений с номером исполнения в конце</param>
  /// <returns></returns>
  public string GetNumber(string specificationDesignation, bool useSameProductDesignations)
  {
    if (!string.IsNullOrEmpty(this.number) || !useSameProductDesignations)
      return this.number;
    if (!string.IsNullOrEmpty(this.Designation) && !string.IsNullOrEmpty(specificationDesignation) && this.Designation.IndexOf(specificationDesignation, StringComparison.CurrentCulture) == 0)
    {
      int num = this.Designation.LastIndexOf('-');
      if (num != -1 && num + 1 < this.Designation.Length && num >= specificationDesignation.Length)
        return this.Designation.Substring(num + 1);
    }
    return this.number ?? "";
  }

  /// <summary>Одинаковые исполнения</summary>
  /// <param name="product">Исполнение</param>
  /// <returns></returns>
  public bool IsEqualProducts(ProductInfo product)
  {
    if (product == null)
      return false;
    if (product == this)
      return true;
    if (product.Guid != Guid.Empty && this.Guid != Guid.Empty)
      return product.Guid == this.Guid;
    return product.Id != -1L && this.Id != -1L ? product.Id == this.Id : product.Designation == this.Designation;
  }

  /// <summary>Назначить новое значение Номера исполнения</summary>
  /// <param name="value">Номер исполнения</param>
  /// <param name="saveToDB">Сохранить в атрибут исполнения</param>
  public void SetNumber(string value, bool saveToDB)
  {
    if (!(this.number != value))
      return;
    this.number = value;
    if (!(this.Id != -1L & saveToDB))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(this.Id).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(AvsIDCache.Attr_ProductCode, (object) this.number)
      });
  }

  /// <summary>Сохранение в строку</summary>
  /// <returns>Строка</returns>
  public string Serialize()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
      return Convert.ToBase64String(serializationStream.GetBuffer());
    }
  }

  /// <summary>Восстановление из строки</summary>
  /// <param name="source">Исходная строка</param>
  /// <returns>Объект ProductInfo</returns>
  public static ProductInfo Deserialize(string source)
  {
    if (source == string.Empty)
      return (ProductInfo) null;
    using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(source)))
      return new BinaryFormatter().Deserialize((Stream) serializationStream) as ProductInfo;
  }

  /// <summary>Перечислить номера исполнений в текстовом виде</summary>
  public static string EnumerateNumbersToText(
    IEnumerable<ProductInfo> products,
    out bool isSimpleRange)
  {
    isSimpleRange = true;
    return products == null ? "" : ProductInfo.EnumerateNumbersToText((IList<string>) products.Select<ProductInfo, string>((System.Func<ProductInfo, string>) (p => p.Number)).ToArray<string>(), out isSimpleRange);
  }

  /// <summary>Перечислить номера исполнений в текстовом виде</summary>
  public static string EnumerateNumbersToText(IList<string> numbers, out bool isSimpleRange)
  {
    string text = "";
    isSimpleRange = numbers.Count <= 2;
    int index1;
    for (int index2 = 0; index2 < numbers.Count; index2 = index1 + 1)
    {
      index1 = numbers.Count - 1;
      string number = numbers[index2];
      int? firstNumberInt = new int?();
      int? lastNumberInt = new int?();
      int result;
      ref int local = ref result;
      if (int.TryParse(number, out local))
        firstNumberInt = new int?(result);
      int? nullable1 = firstNumberInt;
      for (int index3 = index2 + 1; index3 < numbers.Count; ++index3)
      {
        int? nullable2 = new int?();
        if (int.TryParse(numbers[index3], out result))
          nullable2 = new int?(result);
        if (!nullable1.HasValue || !nullable2.HasValue || nullable2.Value - nullable1.Value > 1)
        {
          index1 = index3 - 1;
          lastNumberInt = nullable1;
          break;
        }
        if (index3 == numbers.Count - 1)
        {
          index1 = index3;
          lastNumberInt = nullable2;
        }
        nullable1 = nullable2;
      }
      isSimpleRange = ((isSimpleRange ? 1 : 0) | (!firstNumberInt.HasValue || !lastNumberInt.HasValue || index2 != 0 ? 0 : (index1 == numbers.Count - 1 ? 1 : 0))) != 0;
      if (text != "")
        text += ", ";
      text += ProductInfo.NumbersRangeListToString(numbers[index2], firstNumberInt, numbers[index1], lastNumberInt);
    }
    return text;
  }

  private static string NumbersRangeListToString(
    string firstNumberStr,
    int? firstNumberInt,
    string lastNumberStr,
    int? lastNumberInt,
    string rangeSplitter = "...",
    string oneStepSplitter = ", ")
  {
    string str1;
    if (lastNumberStr.IsEmpty() || firstNumberStr == lastNumberStr)
      str1 = firstNumberStr;
    else if (firstNumberStr.IsEmpty())
    {
      str1 = lastNumberStr;
    }
    else
    {
      string str2 = oneStepSplitter;
      if (firstNumberInt.HasValue && lastNumberInt.HasValue)
      {
        int? nullable1 = lastNumberInt;
        int? nullable2 = firstNumberInt;
        int? nullable3 = nullable1.HasValue & nullable2.HasValue ? new int?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new int?();
        int num = 1;
        if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
          str2 = rangeSplitter;
      }
      str1 = firstNumberStr + str2 + lastNumberStr;
    }
    return str1;
  }

  /// <summary>Преобразовать в строку</summary>
  /// <returns>Возвращает заголовок исполнения</returns>
  public override string ToString() => $"{this.Designation} ({this.Name})";

  /// <summary>Это раздел общих данных</summary>
  public bool IsCommonData
  {
    [DebuggerStepThrough] get => this.Guid == AVSDocument.ChapterCommonDataGuid;
  }

  /// <summary>Это раздел переменных данных</summary>
  public bool IsVariableData
  {
    [DebuggerStepThrough] get
    {
      return this.Guid == AVSDocument.ChapterVariableDataGuid || this.Guid == AVSDocument.ChapterVariableDataVGuid;
    }
  }

  /// <summary>Это дополнительная часть</summary>
  public bool IsAdditionalChapter
  {
    [DebuggerStepThrough] get => this.Guid == AVSDocument.AdditionalChapterGuid;
  }

  /// <summary>Обозначение</summary>
  public string Designation
  {
    get => this.designation;
    set
    {
      if (!(this.designation != value))
        return;
      this.designation = value;
    }
  }

  /// <summary>Номер исполнения, для граф Количество, полученный из атрибута объекта</summary>
  public string Number
  {
    get => this.number;
    set => this.number = value;
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Designation", (object) this.Designation);
    info.AddValue("generatedNumber", (object) this.generatedNumber);
    info.AddValue("Guid", (object) this.Guid);
    info.AddValue("Id", this.Id);
    info.AddValue("F_ID", this.F_ID);
    info.AddValue("ObjectType", this.ObjectType);
    info.AddValue("ArticleGroupID", (object) this.ArticleGroupID);
    info.AddValue("CheckoutBy", this.CheckoutBy);
    info.AddValue("Litera", (object) this.Litera);
    info.AddValue("Name", (object) this.Name);
    info.AddValue("number", (object) this.number);
    info.AddValue("ProductKod", (object) this.ProductKod);
    info.AddValue("ProductOKPCode", (object) this.ProductOKPCode);
    info.AddValue("ModifyDate", this.ModifyDate);
    info.AddValue("AddCount", this.AdditionalAttributes.Count);
    int num = 0;
    foreach (KeyValuePair<int, string> additionalAttribute in this.AdditionalAttributes)
    {
      info.AddValue("AddKey" + (object) num, additionalAttribute.Key);
      info.AddValue("AddValue" + (object) num, (object) additionalAttribute.Value);
      ++num;
    }
  }

  private ProductInfo(SerializationInfo info, StreamingContext context)
  {
    this.Designation = info.GetString(nameof (Designation));
    this.generatedNumber = info.GetString(nameof (generatedNumber));
    this.Guid = (Guid) info.GetValue(nameof (Guid), typeof (Guid));
    this.Id = info.GetInt64(nameof (Id));
    if (info.MemberCount > 11)
    {
      this.F_ID = info.GetInt64(nameof (F_ID));
      this.ObjectType = info.GetInt32(nameof (ObjectType));
      this.CheckoutBy = info.GetInt64(nameof (CheckoutBy));
    }
    this.Litera = info.GetString(nameof (Litera));
    this.Name = info.GetString(nameof (Name));
    this.number = info.GetString(nameof (number));
    this.ProductKod = info.GetString(nameof (ProductKod));
    this.ProductOKPCode = info.GetString(nameof (ProductOKPCode));
    try
    {
      if (info.MemberCount > 10)
        this.ModifyDate = info.GetDateTime(nameof (ModifyDate));
    }
    catch
    {
    }
    int int32 = info.GetInt32("AddCount");
    for (int index = 0; index < int32; ++index)
      this.AdditionalAttributes[info.GetInt32("AddKey" + index.ToString())] = info.GetString("AddValue" + index.ToString());
  }
}
