
// Type: Intermech.Interfaces.MyAttributeMetadata
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения метаданных атрибута</summary>
    [Serializable]
    public sealed class MyAttributeMetadata : ICloneable
    {
      /// <summary>ID атрибута</summary>
      private int FAttrID;
      /// <summary>GUID атрибута</summary>
      private string FAttrGUID = "";
      /// <summary>
      /// Тип данных в атрибуте (тип будет расшифрован для ftSystem)
      /// </summary>
      private FieldTypes FAttrType;
      /// <summary>Название атрибута</summary>
      private string FAttrName = "";
      /// <summary>
      /// true, если FAttrType был равен до расшифровки ftSystem
      /// </summary>
      private bool FIsAttrSystem;
      /// <summary>Является ли атрибут списком допустимых значений</summary>
      private bool FIsAttrList;
      /// <summary>
      /// Список допустимых значений атрибута, если он является списковым.
      /// Допустимые значения хранятся в виде списка элементов MyElement
      /// </summary>
      private ArrayList FAttrPossibleValues = new ArrayList();

      /// <summary>ID атрибута</summary>
      public int AttrID => this.FAttrID;

      /// <summary>GUID атрибута</summary>
      public string AttrGUID => this.FAttrGUID;

      /// <summary>
      /// Тип данных в атрибуте (тип будет расшифрован для ftSystem)
      /// </summary>
      public FieldTypes AttrType
      {
        get => this.FAttrType;
        set => this.FAttrType = value;
      }

      /// <summary>Название атрибута</summary>
      public string AttrName => this.FAttrName;

      /// <summary>true, если AttrType был равен до расшифровки ftSystem</summary>
      public bool IsAttrSystem => this.FIsAttrSystem;

      /// <summary>Является ли атрибут списком допустимых значений</summary>
      public bool IsAttrList
      {
        get => this.FIsAttrList;
        set => this.FIsAttrList = value;
      }

      /// <summary>
      /// Список допустимых значений атрибута, если он является списковым.
      /// Допустимые значения хранятся в виде списка элементов MyElement
      /// </summary>
      public ArrayList AttrPossibleValues => this.FAttrPossibleValues;

      /// <summary>Создать пустой неинициализированный экземпляр класса</summary>
      public MyAttributeMetadata()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его поля по GUID атрибута
      /// </summary>
      /// <param name="GUID">GUID атрибута</param>
      public MyAttributeMetadata(string GUID) => this.SetByGUID(GUID);

      /// <summary>
      /// Создать экземпляр класса, заполнить его поля по ID атрибута
      /// </summary>
      /// <param name="ID">ID атрибута</param>
      public MyAttributeMetadata(int ID) => this.SetByID(ID);

      /// <summary>
      /// Создать экземпляр класса, заполнить его поля по описанию атрибута
      /// </summary>
      /// <param name="attr"></param>
      public MyAttributeMetadata(IDBAttributeType attr)
      {
        this.Clear();
        if (attr == null)
          return;
        this.FAttrID = attr.AttributeID;
        this.FAttrName = attr.Name;
        this.FAttrType = attr.AttributeType;
        this.FIsAttrList = attr.MultipleValued == MultiValueModes.SingleValueFromList;
        if (this.IsAttrList)
        {
          DataTable possibleValues = attr.GetPossibleValues();
          if (possibleValues != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
            {
              string caption = row["F_DESCRIPTION"].ToString();
              if (caption.Length <= 0)
                caption = row[attr.ValueFieldName].ToString();
              this.FAttrPossibleValues.Add((object) new MyElement(row[attr.ValueFieldName], caption, (object) null));
            }
          }
        }
        this.FIsAttrSystem = this.FAttrType == FieldTypes.ftSystem;
        if (this.FAttrType != FieldTypes.ftSystem)
          return;
        this.FAttrType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) this.FAttrID);
      }

      /// <summary>Очистить все поля класса</summary>
      public void Clear()
      {
        this.FAttrID = 0;
        this.FAttrGUID = "";
        this.FAttrType = FieldTypes.ftUnknown;
        this.FAttrName = "";
        this.FIsAttrSystem = false;
        this.FIsAttrList = false;
        this.FAttrPossibleValues.Clear();
      }

      /// <summary>Заполнить поля класса по указанному GUID атрибута</summary>
      /// <param name="GUID">GUID атрибута</param>
      /// <returns>true, если атрибут найден в базе</returns>
      public bool SetByGUID(string GUID)
      {
        this.Clear();
        this.FAttrGUID = GUID;
        return MyAttributeHelper.GetAttrInfo(GUID, ref this.FAttrName, ref this.FAttrID, ref this.FAttrType, ref this.FIsAttrSystem, ref this.FIsAttrList, ref this.FAttrPossibleValues);
      }

      /// <summary>Заполнить поля класса по указанному ID атрибута</summary>
      /// <param name="ID">ID атрибута</param>
      /// <returns>true, если атрибут найден в базе</returns>
      public bool SetByID(int ID)
      {
        this.Clear();
        this.FAttrID = ID;
        return MyAttributeHelper.GetAttrInfo(ID, ref this.FAttrName, ref this.FAttrGUID, ref this.FAttrType, ref this.FIsAttrSystem, ref this.FIsAttrList, ref this.FAttrPossibleValues);
      }

      public override string ToString()
      {
        if (this.FAttrName != "")
          return this.FAttrName;
        return this.FAttrGUID != "" ? this.FAttrGUID : this.FAttrID.ToString();
      }

      /// <summary>Добавить новое допустимое значение в список</summary>
      /// <param name="Value">Допустимое значение</param>
      /// <param name="Caption">Его текстовое представление</param>
      /// <param name="Tag">Пользовательские данные</param>
      /// <returns></returns>
      public int AddPossibleValue(object Value, string Caption, object Tag)
      {
        return this.AttrPossibleValues.Add((object) new MyElement(Value, Caption, Tag));
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        MyAttributeMetadata attributeMetadata = new MyAttributeMetadata();
        attributeMetadata.FAttrID = this.FAttrID;
        attributeMetadata.FAttrGUID = this.FAttrGUID;
        attributeMetadata.FAttrType = this.FAttrType;
        attributeMetadata.FAttrName = this.FAttrName;
        attributeMetadata.FIsAttrSystem = this.FIsAttrSystem;
        attributeMetadata.FIsAttrList = this.IsAttrList;
        attributeMetadata.FAttrPossibleValues.Clear();
        int count = this.AttrPossibleValues.Count;
        if (count > 0)
        {
          for (int index = 0; index < count; ++index)
          {
            MyElement attrPossibleValue = (MyElement) this.AttrPossibleValues[index];
            attributeMetadata.FAttrPossibleValues.Add(attrPossibleValue.Clone());
          }
        }
        return (object) attributeMetadata;
      }

      /// <summary>
      /// Выполнить проверку полной совместимости текущих методанных с указанными.
      /// </summary>
      /// <param name="Value">Проверяемые метаданные</param>
      /// <returns>true, если выполняется полная совместимость</returns>
      public bool IsCompatible(MyAttributeMetadata Value)
      {
        return Value != null && Value.AttrType == this.AttrType && Value.AttrGUID == this.AttrGUID && Value.AttrID == this.AttrID;
      }

      /// <summary>
      /// Скопировать из указанного описания атрибута свойства в текущий экземпляр класса
      /// </summary>
      /// <param name="source">Описание-источник</param>
      public void Assign(MyAttributeMetadata source)
      {
        this.Clear();
        if (source == null)
          return;
        this.FAttrID = source.FAttrID;
        this.FAttrGUID = source.FAttrGUID;
        this.FAttrType = source.FAttrType;
        this.FAttrName = source.FAttrName;
        this.FIsAttrSystem = source.FIsAttrSystem;
        this.FIsAttrList = source.IsAttrList;
        this.FAttrPossibleValues.Clear();
        int count = source.AttrPossibleValues.Count;
        if (count <= 0)
          return;
        for (int index = 0; index < count; ++index)
          this.FAttrPossibleValues.Add(((MyElement) source.AttrPossibleValues[index]).Clone());
      }
    }
}
