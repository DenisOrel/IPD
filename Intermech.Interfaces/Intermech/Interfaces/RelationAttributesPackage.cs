
// Type: Intermech.Interfaces.RelationAttributesPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, предназначенный для пакетной передачи атрибутов связи между клиентом и сервером
    /// </summary>
    [Serializable]
    public class RelationAttributesPackage : ICloneable
    {
      /// <summary>
      /// Список идентификаторов атрибутов, которые передаются для коллекции связей
      /// </summary>
      private List<int> _attributes = new List<int>();
      /// <summary>
      /// Список идентификаторов атрибутов, которые надо записывать в базу данных.
      /// Если список пустой или null, то в базу будут записаны все атрибуты
      /// </summary>
      private List<int> _writeableAttributes;
      /// <summary>
      /// Список значений атрибутов для коллекции связей.
      /// [(Int64)Идентификатор связи] = [(object[])Список значений атрибутов связи].
      /// Размерность массива значений атрибутов равна количеству идентификаторов атрибутов
      /// в свойстве _attributes.
      /// </summary>
      private Dictionary<long, object[]> _values = new Dictionary<long, object[]>();

      /// <summary>
      /// Список идентификаторов атрибутов, которые передаются для коллекции связей
      /// </summary>
      public virtual List<int> Attributes
      {
        get => this._attributes;
        set
        {
          if (this._attributes == value)
            return;
          this._attributes = value != null ? value : new List<int>();
          this._values.Clear();
        }
      }

      /// <summary>
      /// Список значений атрибутов для коллекции связей.
      /// [(Int64)Идентификатор связи] = [(object[])Список значений атрибутов связи].
      /// Размерность массива значений атрибутов равна количеству идентификаторов атрибутов
      /// в свойстве _attributes.
      /// </summary>
      public virtual Dictionary<long, object[]> Values => this._values;

      /// <summary>
      /// Список идентификаторов атрибутов, которые надо записывать в базу данных.
      /// Если список пустой или null, то в базу будут записаны все атрибуты
      /// </summary>
      public List<int> WriteableAttributes
      {
        get => this._writeableAttributes;
        set => this._writeableAttributes = value;
      }

      /// <summary>Значение указанного атрибута для указанной связи</summary>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <returns>null, если значение атрибута не найдено</returns>
      public virtual object this[long prjLinkID, int attributeID]
      {
        get
        {
          if (!this._values.ContainsKey(prjLinkID) || !this._attributes.Contains(attributeID))
            return (object) null;
          int index = this._attributes.IndexOf(attributeID);
          return this._values[prjLinkID]?[index];
        }
        set
        {
          if (!this._attributes.Contains(attributeID))
            return;
          if (!this._values.ContainsKey(prjLinkID))
          {
            object[] objArray = new object[this._attributes.Count];
            this._values.Add(prjLinkID, objArray);
          }
          int index = this._attributes.IndexOf(attributeID);
          if (index >= this._values[prjLinkID].Length)
          {
            object[] array = this._values[prjLinkID];
            Array.Resize<object>(ref array, this._attributes.Count);
            this._values[prjLinkID] = array;
          }
          this._values[prjLinkID][index] = value != DBNull.Value ? value : (object) null;
        }
      }

      /// <summary>Список значений атрибутов указанной связи</summary>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <returns>Список значений атрибутов указанной связи или null</returns>
      public virtual object[] this[long prjLinkID]
      {
        get => !this._values.ContainsKey(prjLinkID) ? (object[]) null : this._values[prjLinkID];
        set
        {
          if (value == null || value.Length != this._attributes.Count)
            return;
          if (!this._values.ContainsKey(prjLinkID))
            this._values.Add(prjLinkID, value);
          else
            this._values[prjLinkID] = value;
        }
      }

      /// <summary>
      /// Список значений атрибутов указанной связи (копия или ссылка на оригинальный массив)
      /// </summary>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="cloneValues">true - вернуть копию списка значений, иначе - ссылку на оригинальный список</param>
      /// <returns>Список значений атрибутов указанной связи или null</returns>
      public virtual object[] this[long prjLinkID, bool cloneValues]
      {
        get
        {
          if (!this._values.ContainsKey(prjLinkID))
            return (object[]) null;
          object[] objArray1 = this._values[prjLinkID];
          if (objArray1 == null || !cloneValues)
            return objArray1;
          object[] objArray2 = new object[objArray1.Length];
          for (int index = 0; index < objArray2.Length; ++index)
            objArray2[index] = objArray1[index];
          return objArray2;
        }
      }

      /// <summary>Создать пустой экземпляр класса</summary>
      public RelationAttributesPackage()
      {
      }

      /// <summary>
      /// Создать экземпляр класса с заданным списком идентификаторов атрибутов
      /// </summary>
      /// <param name="attributes">Список идентификаторов атрибутов</param>
      public RelationAttributesPackage(List<int> attributes)
      {
        this._attributes = attributes != null ? attributes : this._attributes;
      }

      /// <summary>
      /// Создать экземпляр класса с заданным списком идентификаторов атрибутов
      /// </summary>
      /// <param name="attributes">Список идентификаторов атрибутов</param>
      /// <param name="writeableAttributes">Список идентификаторов атрибутов, которые можно будет записывать в базу данных.
      /// Пустой список или null означают то, что все атрибуты из attributes можно будет записывать в базу данных.</param>
      public RelationAttributesPackage(List<int> attributes, List<int> writeableAttributes)
      {
        this._attributes = attributes != null ? attributes : this._attributes;
        this._writeableAttributes = writeableAttributes;
      }

      /// <summary>Задать значение атрибута указанным связям</summary>
      /// <param name="relations">Список связей</param>
      /// <param name="attrID">Идентификатор атрибута</param>
      /// <param name="value">Значение атрибута</param>
      public virtual void SetRelationsAttrValue(List<long> relations, int attrID, object value)
      {
        if (relations == null)
          return;
        for (int index = 0; index < relations.Count; ++index)
          this[relations[index], attrID] = value;
      }

      /// <summary>Выполнить объединение данных с указанным пакетом</summary>
      /// <param name="source">Пакет атрибутов связей, с которым выполняется объединение</param>
      /// <param name="excludedAttrs">Список исключаемых идентификаторов объектов</param>
      public virtual void MergePackages(RelationAttributesPackage source, params int[] excludedAttrs)
      {
        if (source == null)
          return;
        for (int index = 0; index < source.Attributes.Count; ++index)
        {
          if (!this._attributes.Contains(source.Attributes[index]))
            this._attributes.Add(source.Attributes[index]);
        }
        if (source.WriteableAttributes != null)
        {
          for (int index = 0; index < source.WriteableAttributes.Count; ++index)
          {
            if (!this._writeableAttributes.Contains(source.WriteableAttributes[index]))
              this._writeableAttributes.Add(source.WriteableAttributes[index]);
          }
        }
        List<int> intList = new List<int>();
        if (excludedAttrs != null)
        {
          for (int index = 0; index < excludedAttrs.Length; ++index)
            intList.Add(excludedAttrs[index]);
        }
        foreach (KeyValuePair<long, object[]> keyValuePair in source.Values)
        {
          for (int index = 0; index < source.Attributes.Count; ++index)
          {
            if (!intList.Contains(source.Attributes[index]))
              this[keyValuePair.Key, source.Attributes[index]] = keyValuePair.Value[index];
          }
        }
      }

      /// <summary>Удалить информацию об указанной связи из пакета</summary>
      /// <param name="prjLinkId">Идентификатор удаляемой связи</param>
      public virtual void Remove(long prjLinkId)
      {
        if (!this._values.ContainsKey(prjLinkId))
          return;
        this._values.Remove(prjLinkId);
      }

      /// <summary>Удалить информацию об указанных связях из пакета</summary>
      /// <param name="prjLinkIds">Идентификаторы удаляемых связей</param>
      public virtual void Remove(List<long> prjLinkIds)
      {
        if (prjLinkIds == null)
          return;
        for (int index = 0; index < prjLinkIds.Count; ++index)
        {
          if (this._values.ContainsKey(prjLinkIds[index]))
            this._values.Remove(prjLinkIds[index]);
        }
      }

      /// <summary>Создать 100% копию текущего экземпляра класса</summary>
      /// <returns>100% копия текущего экземпляра класса</returns>
      public object Clone()
      {
        RelationAttributesPackage attributesPackage = new RelationAttributesPackage();
        for (int index = 0; index < this._attributes.Count; ++index)
          attributesPackage._attributes.Add(this._attributes[index]);
        foreach (KeyValuePair<long, object[]> keyValuePair in this._values)
        {
          object[] objArray = new object[attributesPackage._attributes.Count];
          for (int index = 0; index < attributesPackage._attributes.Count; ++index)
            objArray[index] = keyValuePair.Value[index];
          attributesPackage._values.Add(keyValuePair.Key, objArray);
        }
        return (object) attributesPackage;
      }
    }
}
