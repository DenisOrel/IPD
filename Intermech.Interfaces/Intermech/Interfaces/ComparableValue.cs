
// Type: Intermech.Interfaces.ComparableValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, инкапсулирующий в себя сравниваемое значение и его вид
    /// </summary>
    [Serializable]
    public sealed class ComparableValue : ICloneable
    {
      /// <summary>Сугубо для внутреннего применения</summary>
      private CompareTypesHelper FCTypes = new CompareTypesHelper();
      /// <summary>
      /// Вид значения для сравнения (константа класса CompareTypesHelper)
      /// </summary>
      private string FValueType = "";
      /// <summary>
      /// Значение, с которым происходит сравнение. Если FValueType = CompareTypesHelper.ctAttribute, то FValue = GUID атрибута
      /// </summary>
      private object FValue = (object) "";
      /// <summary>Тип данных (для атрибута определяется автоматически)</summary>
      private FieldTypes FAttrType = FieldTypes.ftString;
      /// <summary>Поле с атрибутом</summary>
      private MyAttributeMetadata FAttribute = new MyAttributeMetadata();
      /// <summary>
      /// Критерий подбора, в состав которого входит данное значение для сравнения
      /// </summary>
      public VersionsRuleCriterion Criterion;

      /// <summary>
      /// Вид значения для сравнения (константа класса CompareTypesHelper)
      /// </summary>
      public string ValueType
      {
        [DebuggerStepThrough] get => this.FValueType;
      }

      /// <summary>
      /// Значение, с которым происходит сравнение.
      /// Если ValueType = CompareTypesHelper.ctAttribute, то FValue = GUID атрибута
      /// </summary>
      public object Value
      {
        [DebuggerStepThrough] get => this.FValue;
      }

      /// <summary>
      /// Тип данных
      /// Если ValueType = CompareTypesHelper.ctAttribute, то это значение будет определяться автоматически
      /// </summary>
      public FieldTypes AttrType
      {
        [DebuggerStepThrough] get => this.FAttrType;
        set
        {
          if (!(this.ValueType != "ATTRIBUTE"))
            return;
          this.FAttrType = value;
          this.FAttribute.AttrType = value;
        }
      }

      /// <summary>Поле с атрибутом</summary>
      public MyAttributeMetadata Attribute
      {
        [DebuggerStepThrough] get => this.FAttribute;
        set => this.FAttribute = value != null ? value : this.FAttribute;
      }

      public ComparableValue()
      {
      }

      /// <summary>
      /// Создать экземпляр класса для сравниваемого значения и его величины
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация по атрибуту</param>
      /// <param name="AValueType">Тип сравниваемой величины (константа класса CompareTypesHelper)</param>
      /// <param name="AValue">Сравниваемое значение (Если AValueType = CompareTypesHelper.ctAttribute, то AValue = GUID атрибута)</param>
      /// <param name="ACriterion">Критерий подбора, в состав которого входит данное значение для сравнения</param>
      public ComparableValue(
        IUserSession session,
        string AValueType,
        object AValue,
        VersionsRuleCriterion ACriterion)
      {
        this.Criterion = ACriterion;
        this.SetValueType(session, AValueType, AValue);
      }

      /// <summary>
      /// Выполнить проверку корректности содержимого сравниваемого значения
      /// </summary>
      /// <returns>true - значение корректно</returns>
      public bool Valid()
      {
        if (this.FValueType != "ATTRIBUTE")
          return true;
        return this.FAttribute.AttrName != "" && this.FAttribute.AttrID != 0 && this.FAttribute.AttrType != 0;
      }

      /// <summary>Установить новое значение сравниваемой величины</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация по атрибуту</param>
      /// <param name="AValue">Сравниваемое значение (если AValueType = CompareTypesHelper.ctAttribute, то AValue = GUID атрибута)</param>
      /// <returns>true, если всё корректно</returns>
      public bool SetValue(IUserSession session, object AValue)
      {
        return this.SetValueType(session, this.FValueType, AValue);
      }

      /// <summary>Установить новый тип сравниваемой величины</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация по атрибуту</param>
      /// <param name="AValueType">Тип сравниваемой величины (константа класса CompareTypesHelper)</param>
      /// <returns>true, если всё корректно</returns>
      public bool SetType(IUserSession session, string AValueType)
      {
        return this.SetValueType(session, AValueType, this.FValue);
      }

      /// <summary>
      /// Установить новое значение сравниваемой величины и её типа
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация по атрибуту</param>
      /// <param name="AValueType">Тип сравниваемой величины (константа класса CompareTypesHelper)</param>
      /// <param name="AValue">Сравниваемое значение (если AValueType = CompareTypesHelper.ctAttribute, то AValue = GUID атрибута)</param>
      /// <returns>true, если всё корректно</returns>
      public bool SetValueType(IUserSession session, string AValueType, object AValue)
      {
        string valueType = this.ValueType;
        this.FValueType = "";
        this.FValue = (object) null;
        this.FAttribute.Clear();
        this.FAttribute.AttrType = this.FAttrType;
        MyElement myElement = AValue as MyElement;
        if (!this.FCTypes.IsMember(AValueType))
          return false;
        this.FValueType = AValueType;
        if (this.FValueType == "VARIABLE")
        {
          this.FValue = AValue;
          if (myElement != null)
            this.FValue = myElement.Value;
          if (this.FAttrType == FieldTypes.ftMeasured)
            this.FValue = (object) this.FValue.ToString();
          if (valueType == "ATTRIBUTE")
            this.FValue = (object) "";
          if (!MyAttributeHelper.IsUserIDType(this.Criterion.MainAttribute.Attribute.AttrID))
            return true;
        }
        if (this.FValueType == "CONST" || this.FValueType == "VARIABLE")
        {
          this.FValue = AValue;
          if (myElement != null)
            this.FValue = myElement.Value;
          if (this.FAttrType == FieldTypes.ftMeasured)
            this.FValue = (object) this.FValue.ToString();
          if (valueType == "ATTRIBUTE")
            this.FValue = (object) "";
          string str = this.FValue.ToString();
          if (str.Length > 0)
          {
            if (this.Criterion.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545")
            {
              bool flag = false;
              try
              {
                if (GuidHelper.IsGuid(str))
                  this.FValue = (object) MetaDataHelper.GetLCLevelID(str);
                else
                  flag = true;
              }
              catch
              {
                flag = true;
              }
              if (flag)
              {
                long result = 0;
                if (long.TryParse(str, out result))
                  this.FValue = (object) result;
              }
              this.Criterion.MainAttribute.Attribute.AttrType = FieldTypes.ftInteger;
              return true;
            }
            if (this.Criterion.MainAttribute.Attribute.AttrGUID == "cad0002b-306c-11d8-b4e9-00304f19f545")
            {
              bool flag = false;
              try
              {
                if (GuidHelper.IsGuid(str))
                  this.FValue = (object) MetaDataHelper.GetLCStepID(str);
                else
                  flag = true;
              }
              catch
              {
                flag = true;
              }
              if (flag)
              {
                long result = 0;
                if (long.TryParse(str, out result))
                  this.FValue = (object) result;
              }
              this.Criterion.MainAttribute.Attribute.AttrType = FieldTypes.ftInteger;
              return true;
            }
            if (MyAttributeHelper.IsUserGuidType(this.Criterion.MainAttribute.Attribute.AttrGUID))
            {
              Guid objectGUID = Guid.Empty;
              long result = 0;
              try
              {
                if (GuidHelper.IsGuid(str))
                  objectGUID = new Guid(str);
                else
                  long.TryParse(str, out result);
              }
              catch
              {
              }
              this.Criterion.MainAttribute.Attribute.AttrType = FieldTypes.ftInteger;
              IDBObject dbObject = result == 0L ? session.GetObject(objectGUID, false) : session.GetObject(result, false);
              if (dbObject != null)
              {
                long objectId = dbObject.ObjectID;
                this.FValue = (object) objectId;
                IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545"));
                IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545"));
                if (attributeByGuid1 != null && attributeByGuid2 != null)
                {
                  string asString1 = attributeByGuid1.AsString;
                  string asString2 = attributeByGuid2.AsString;
                  string Caption = asString1;
                  if (asString1.Length <= 0)
                    Caption = asString2;
                  bool flag = false;
                  foreach (MyElement attrPossibleValue in this.Criterion.MainAttribute.Attribute.AttrPossibleValues)
                  {
                    flag = attrPossibleValue.Caption == Caption && attrPossibleValue.Value.ToString() == objectId.ToString();
                    if (flag)
                      break;
                  }
                  if (!flag)
                    this.Criterion.MainAttribute.Attribute.AddPossibleValue((object) objectId, Caption, (object) null);
                }
              }
              else
                this.FValue = (object) "";
            }
          }
          return true;
        }
        this.FValue = AValue;
        if (myElement != null)
          this.FValue = myElement.Value;
        if (this.FValue.ToString().Length <= 0)
          return false;
        int num = this.FAttribute.SetByGUID(this.FValue.ToString()) ? 1 : 0;
        this.FAttrType = this.FAttribute.AttrType;
        return num != 0;
      }

      /// <summary>Сравнить своё значение с указанным значением</summary>
      /// <param name="CompareType">Тип сравниваемого значения</param>
      /// <param name="CompareValue">Сравниваемое значение (если CompareType = CompareTypesHelper.ctAttribute, то CompareValue = GUID атрибута)</param>
      /// <returns>Вернёт -1 если своё значение меньше указанного CompareValue, 0 - если значения идентичны, 1 - если своё значение больше указанного CompareValue</returns>
      public int Compare(string CompareType, object CompareValue) => 0;

      /// <summary>Очистить все внутренние поля экземпляру класса</summary>
      public void Clear()
      {
        this.FValueType = "";
        this.FValue = (object) null;
        this.FAttrType = FieldTypes.ftUnknown;
        this.FAttribute.Clear();
      }

      /// <summary>
      /// Загрузить значения экземпляра класса из узла Node документа XML
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация по атрибуту</param>
      /// <param name="Node">Узел документа XML, из которого надо загрузить данные в объект</param>
      /// <returns>true, если загрузка прошла успешно</returns>
      public bool LoadXML(IUserSession session, XmlNode Node)
      {
        this.Clear();
        if (Node == null || Node.Name != "value")
          return false;
        XmlNode namedItem = Node.Attributes.GetNamedItem("type");
        if (namedItem == null || !this.FCTypes.IsMember(namedItem.InnerText))
          return false;
        object AValue = (object) null;
        if (this.Criterion != null)
        {
          this.FAttrType = this.Criterion.MainAttribute.Attribute.AttrType;
          this.FAttribute.AttrType = this.FAttrType;
          if (namedItem.InnerText == "ATTRIBUTE")
          {
            try
            {
              if (!Node.InnerText.StartsWith("["))
                AValue = (object) new Guid(Node.InnerText);
            }
            catch
            {
              AValue = (object) Node.InnerText;
            }
          }
          else
          {
            if (this.Criterion.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545" || this.Criterion.MainAttribute.Attribute.AttrGUID == "cad0002b-306c-11d8-b4e9-00304f19f545" || MyAttributeHelper.IsUserIDType(this.Criterion.MainAttribute.Attribute.AttrID))
              this.Criterion.MainAttribute.Attribute.AttrType = FieldTypes.ftString;
            switch (this.Criterion.MainAttribute.Attribute.AttrType)
            {
              case FieldTypes.ftInteger:
                long result1;
                long.TryParse(Node.InnerText, out result1);
                AValue = (object) result1;
                break;
              case FieldTypes.ftDouble:
                double result2;
                AValue = double.TryParse(Node.InnerText, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2) || double.TryParse(Node.InnerText, out result2) ? (object) result2 : (object) 0;
                break;
              case FieldTypes.ftDateTime:
                DateTime result3;
                DateTime.TryParse(Node.InnerText, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result3);
                AValue = (object) result3.Date;
                break;
              case FieldTypes.ftBoolean:
                bool result4;
                bool.TryParse(Node.InnerText, out result4);
                AValue = (object) result4;
                break;
              case FieldTypes.ftAutoInc:
                long result5;
                long.TryParse(Node.InnerText, out result5);
                AValue = (object) result5;
                break;
              case FieldTypes.ftGuid:
                try
                {
                  if (!Node.InnerText.StartsWith("["))
                  {
                    AValue = (object) new Guid(Node.InnerText);
                    break;
                  }
                  break;
                }
                catch
                {
                  AValue = (object) Node.InnerText;
                  break;
                }
              default:
                AValue = (object) Node.InnerText;
                break;
            }
          }
        }
        if (AValue == null)
          AValue = (object) Node.InnerText;
        return this.SetValueType(session, namedItem.InnerText, AValue);
      }

      /// <summary>
      /// Сохранить значения экземпляра класса в узел Node документа XML
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="Node">Существующий узел документа XML, в который надо выгрузить данные объекта</param>
      /// <returns>true, если выгрузка данных в узел Node прошла успешно</returns>
      public bool SaveXML(IUserSession session, XmlNode Node)
      {
        if (session == null || Node == null || Node.Name != "value")
          return false;
        XmlDocument ownerDocument = Node.OwnerDocument;
        if (ownerDocument == null)
          return false;
        Node.Attributes.RemoveAll();
        XmlAttribute attribute = ownerDocument.CreateAttribute("type");
        attribute.InnerText = this.ValueType;
        Node.Attributes.Append(attribute);
        string str = this.Value.ToString();
        try
        {
          if (this.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545")
          {
            try
            {
              str = MetaDataHelper.GetLCLevelGuid(Convert.ToInt32(this.Value.ToString())).ToString();
            }
            catch
            {
            }
          }
          if (this.Attribute.AttrGUID == "cad0002b-306c-11d8-b4e9-00304f19f545")
          {
            try
            {
              str = MetaDataHelper.GetLCStepGuid(Convert.ToInt32(this.Value.ToString())).ToString();
            }
            catch
            {
            }
          }
          if (MyAttributeHelper.IsUserIDType(this.Criterion.MainAttribute.Attribute.AttrID))
          {
            try
            {
              IDBObject dbObject = session.GetObject(Convert.ToInt64(this.Value.ToString()), false);
              str = dbObject != null ? dbObject.ObjectGUID.ToString() : Guid.Empty.ToString();
            }
            catch
            {
              str = Guid.Empty.ToString();
            }
          }
          double result1;
          if (this.Attribute.AttrType == FieldTypes.ftDouble && double.TryParse(this.Value.ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
            str = result1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          if (this.Attribute.AttrType == FieldTypes.ftDateTime)
          {
            DateTime result2;
            if (DateTime.TryParse(this.Value.ToString(), out result2))
            {
              result2 = result2.Date;
              str = result2.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
            }
          }
        }
        finally
        {
          Node.InnerText = str;
        }
        return true;
      }

      /// <summary>
      /// Сохранить данные экземпляра класса во вновь созданном узле документа Doc
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="Doc">Документ XML, в котором надо создавать узел с данными</param>
      /// <param name="Parent">Родительский узел в документе, в который надо добавить новый узел</param>
      /// <returns>Узел с данными объекта</returns>
      public XmlNode SaveXML(IUserSession session, XmlDocument Doc, XmlNode Parent)
      {
        if (Doc == null)
          return (XmlNode) null;
        XmlNode xmlNode = Parent ?? (XmlNode) Doc.DocumentElement;
        XmlElement element = Doc.CreateElement("value");
        xmlNode.AppendChild((XmlNode) element);
        this.SaveXML(session, (XmlNode) element);
        return (XmlNode) element;
      }

      /// <summary>
      /// Вернуть значение атрибута для его отображения на экране	в "читабельном виде"
      /// </summary>
      /// <param name="EditorMode">Режим редактирования (0 - полноценный редактор правила подбора версий объектов (admin режим), 1 - заполнение недостающих значений для сравнения в критериях (user режим), 2 - просмотр правила подбора версий объектов (read-only режим))</param>
      /// <returns>Значение атрибута в виде строки для отображения</returns>
      public object GetDisplayValue(int EditorMode)
      {
        if (this.ValueType == "ATTRIBUTE")
        {
          if (this.Value.ToString().Length <= 0)
            return (object) cvConsts.cvAttribute;
          return this.Attribute.AttrName.Length <= 0 ? (object) cvConsts.cvAttribute : (object) this.Attribute.AttrName;
        }
        object displayValue = this.FValue;
        if (this.Attribute.AttrID == -4)
        {
          int result = -1;
          if (this.Value != null && int.TryParse(this.Value.ToString(), out result))
          {
            IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(result);
            if (lcStep != null)
              return (object) $"{lcStep.Name} ({MetaDataHelper.GetLCSchemaName(lcStep.SchemaID)})";
          }
          if (this.Value != null && GuidHelper.IsGuid(this.Value.ToString()))
          {
            IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(new Guid(this.Value.ToString()));
            if (lcStep != null)
              return (object) $"{lcStep.Name} ({MetaDataHelper.GetLCSchemaName(lcStep.SchemaID)})";
          }
        }
        if (this.Attribute.AttrID == -9)
        {
          int result = -1;
          if (this.Value != null && int.TryParse(this.Value.ToString(), out result))
            return (object) MetaDataHelper.GetLCLevelName(result);
          if (this.Value != null && GuidHelper.IsGuid(this.Value.ToString()))
            return (object) MetaDataHelper.GetLCLevelName(new Guid(this.Value.ToString()));
        }
        if (this.AttrType == FieldTypes.ftBoolean)
        {
          try
          {
            displayValue = (object) Convert.ToBoolean(this.Value);
          }
          catch
          {
            displayValue = (object) false;
          }
        }
        if (this.AttrType == FieldTypes.ftDateTime)
        {
          try
          {
            DateTime dateTime = Convert.ToDateTime(this.Value, (IFormatProvider) CultureInfo.InvariantCulture);
            dateTime = dateTime.Date;
            displayValue = (object) dateTime.ToString("dd.MM.yyyy");
          }
          catch
          {
            displayValue = this.Value;
          }
        }
        if (this.AttrType == FieldTypes.ftDouble)
        {
          try
          {
            double result;
            if (!double.TryParse(this.Value.ToString(), out result))
            {
              if (!double.TryParse(this.Value.ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result))
                goto label_29;
            }
            displayValue = (object) result;
          }
          catch
          {
            displayValue = this.Value;
          }
        }
    label_29:
        if (this.Criterion == null || !this.Criterion.MainAttribute.Attribute.IsAttrList)
          return this.Value;
        if (this.Criterion.MainAttribute.Attribute.AttrPossibleValues.Count > 0)
        {
          MyElement myElement = (MyElement) null;
          bool flag = false;
          MyElement attrPossibleValue = (MyElement) this.Criterion.MainAttribute.Attribute.AttrPossibleValues[0];
          for (int index = 0; index < this.Criterion.MainAttribute.Attribute.AttrPossibleValues.Count; ++index)
          {
            myElement = (MyElement) this.Criterion.MainAttribute.Attribute.AttrPossibleValues[index];
            if (myElement != null)
            {
              try
              {
                flag = Convert.ToString(myElement.Value) == Convert.ToString(this.FValue);
              }
              catch
              {
                flag = false;
              }
              if (!flag)
                myElement = (MyElement) null;
              else
                break;
            }
          }
          if (flag)
          {
            if (this.Criterion != null && this.ValueType == cvConsts.cvConst)
            {
              if (this.Criterion.MainAttribute.Attribute.AttrType == FieldTypes.ftBoolean)
              {
                try
                {
                  return (object) Convert.ToBoolean(myElement.Value);
                }
                catch
                {
                  return (object) false;
                }
              }
            }
            return (object) myElement.Caption;
          }
          if (attrPossibleValue != null)
            return (object) attrPossibleValue.Caption;
        }
        return displayValue;
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        return (object) new ComparableValue()
        {
          FValueType = this.FValueType,
          FValue = this.FValue,
          FAttrType = this.FAttrType,
          FAttribute = (MyAttributeMetadata) this.FAttribute.Clone(),
          Criterion = this.Criterion
        };
      }

      /// <summary>
      /// Выполнить проверку совместимости текущего значения для сравнения с указанным
      /// </summary>
      /// <param name="Value">Сравниваемое значение</param>
      /// <returns>true, если указанные значения полностью совместимы по типу данных (и при необходимости - по атрибутам)</returns>
      public bool IsCompatible(ComparableValue Value)
      {
        if (this.ValueType == "CONST")
          return this.ValueType == Value.ValueType && this.AttrType == Value.AttrType;
        if (!(this.ValueType == "VARIABLE"))
          return this.Attribute.IsCompatible(Value.Attribute);
        return this.ValueType == Value.ValueType && this.AttrType == Value.AttrType;
      }
    }
}
