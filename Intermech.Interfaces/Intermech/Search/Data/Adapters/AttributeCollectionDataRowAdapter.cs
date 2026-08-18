
// Type: Intermech.Search.Data.Adapters.AttributeCollectionDataRowAdapter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Search.Data.Adapters
{
    /// <summary>
    /// Адаптер DataRow, позволяющий работать с ним как с коллекцией атрибутов
    /// </summary>
    public sealed class AttributeCollectionDataRowAdapter : AttributeCollectionBase
    {
      private readonly IAttributeValueConverter _attributeValueConverter;

      /// <summary>Конструктор</summary>
      /// <param name="dataRow">Строка с результатами запроса</param>
      /// <param name="params">Адаптер параметров запроса</param>
      /// <param name="attributeValueConverter"></param>
      /// <exception cref="T:System.ArgumentNullException">
      /// dataRow
      /// or
      /// @params
      /// </exception>
      public AttributeCollectionDataRowAdapter(
        DataRow dataRow,
        IRecordSetParamsAdapter @params,
        IAttributeValueConverter attributeValueConverter)
      {
        this.DataRow = dataRow ?? throw new ArgumentNullException(nameof (dataRow));
        this.Params = @params ?? throw new ArgumentNullException(nameof (@params));
        this._attributeValueConverter = attributeValueConverter ?? throw new ArgumentNullException(nameof (attributeValueConverter));
      }

      /// <summary>Строка с результатами запроса</summary>
      public DataRow DataRow { get; private set; }

      /// <summary>Адаптер параметров запроса</summary>
      public IRecordSetParamsAdapter Params { get; private set; }

      public override void Add(_Attribute attribute) => throw new NotImplementedException();

      public override void AddRange(IEnumerable<_Attribute> attributes)
      {
        throw new NotImplementedException();
      }

      /// <summary>Проверить наличие атрибута в коллекции</summary>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <returns></returns>
      public override bool HasAttribute(int attributeTypeID)
      {
        return this.GetColumnIndex(attributeTypeID) > -1;
      }

      public override _Attribute GetAttribute(int attributeTypeID)
      {
        throw new NotImplementedException();
      }

      public override IEnumerator<_Attribute> GetEnumerator() => throw new NotImplementedException();

      /// <summary>Получить значение атрибута</summary>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <returns></returns>
      /// <exception cref="T:System.ArgumentException"></exception>
      /// <exception cref="T:Intermech.Search.AttributeNotFoundException"></exception>
      public override object GetAttributeValue(int attributeTypeID)
      {
        int columnIndex = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? this.GetColumnIndex(attributeTypeID) : throw new ArgumentException();
        return columnIndex == -1 ? (object) null : this._attributeValueConverter.Convert(this.DataRow[columnIndex], attributeTypeID);
      }

      /// <summary>Установить значение атрибута</summary>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <param name="value">Значение атрибута</param>
      /// <exception cref="T:System.NotImplementedException"></exception>
      public override void SetAttributeValue(int attributeTypeID, object value)
      {
        throw new NotImplementedException();
      }

      private int GetColumnIndex(int attributeTypeID) => this.Params.GetColumnIndex(attributeTypeID);
    }
}
