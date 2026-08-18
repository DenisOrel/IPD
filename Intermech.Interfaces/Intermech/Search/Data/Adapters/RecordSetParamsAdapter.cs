
// Type: Intermech.Search.Data.Adapters.RecordSetParamsAdapter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Adapters
{
    /// <summary>
    /// Адаптер для параметров запроса, позволяющий получать индекс колонки запроса по идентификатору атрибута
    /// </summary>
    public sealed class RecordSetParamsAdapter : IRecordSetParamsAdapter
    {
      private DBRecordSetParams _params;
      private AttributeSourceTypes _attributeSourceType;
      private Dictionary<int, int> _columnIndexDictionaryByAttributeTypeID = new Dictionary<int, int>();

      /// <summary>Конструктор</summary>
      /// <param name="params">Параметры запроса</param>
      /// <param name="attributeSourceType">Тип источника атрибутов</param>
      public RecordSetParamsAdapter(DBRecordSetParams @params, AttributeSourceTypes attributeSourceType = AttributeSourceTypes.Auto)
      {
        this._params = @params.Columns != null ? @params : throw new ArgumentException();
        this._attributeSourceType = attributeSourceType;
        this.Initialize();
      }

      /// <summary>Получить индекс колонки запроса</summary>
      /// <param name="obligatoryObjectAttribute">Системный атрибут</param>
      /// <returns>Индекс колонки запроса</returns>
      public int GetColumnIndex(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return this.GetColumnIndex((int) obligatoryObjectAttribute);
      }

      /// <summary>Получить индекс колонки запроса</summary>
      /// <param name="attributeTypeID">Идентификатор типа аттрибута</param>
      /// <returns>Индекс колонки запроса</returns>
      public int GetColumnIndex(int attributeTypeID)
      {
        int num;
        return !this._columnIndexDictionaryByAttributeTypeID.TryGetValue(attributeTypeID, out num) ? -1 : num;
      }

      private void Initialize()
      {
        int index = 0;
        for (int length = this._params.Columns.Length; index < length; ++index)
        {
          int attributeTypeId = this.GetAttributeTypeID(this._params.Columns[index]);
          AttributeSourceTypes one = AttributeSourceTypes.Auto;
          if (this.IsSystemAttribute(attributeTypeId))
            one = this.GetAttributeSourceType((ObligatoryObjectAttributes) attributeTypeId);
          else if (this._params.ColumnsInfo != null)
            one = this._params.ColumnsInfo[index].AttributeSource;
          if (!this._columnIndexDictionaryByAttributeTypeID.ContainsKey(attributeTypeId) && this.CompareAttributeSourceTypes(one, this._attributeSourceType))
            this._columnIndexDictionaryByAttributeTypeID.Add(attributeTypeId, index);
        }
      }

      private int GetAttributeTypeID(object attributeID)
      {
        switch (attributeID)
        {
          case int attributeTypeId1:
            return attributeTypeId1;
          case ObligatoryObjectAttributes attributeTypeId2:
            return (int) attributeTypeId2;
          case Guid attributeTypeGuid:
            return ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeGuidToAttributeTypeID(attributeTypeGuid);
          case string _:
            return (string) attributeID == ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ELEMENT_STATUSES) ? -77 : ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeNameToAttributeTypeID((string) attributeID);
          default:
            throw new NotSupportedException($"{attributeID} нельзя исльзовать в качесте идентификатора атрибута");
        }
      }

      private bool IsSystemAttribute(int attributeTypeID)
      {
        return ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attributeTypeID);
      }

      private AttributeSourceTypes GetAttributeSourceType(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        return ObligatoryObjectAttributesHelper.GetAttributeSourceType(obligatoryObjectAttribute);
      }

      private bool CompareAttributeSourceTypes(AttributeSourceTypes one, AttributeSourceTypes two)
      {
        return one == AttributeSourceTypes.Auto || one == AttributeSourceTypes.Other || two == AttributeSourceTypes.Auto || two == AttributeSourceTypes.Other || one == two;
      }
    }
}
