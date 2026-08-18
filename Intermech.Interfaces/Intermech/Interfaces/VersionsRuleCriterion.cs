
// Type: Intermech.Interfaces.VersionsRuleCriterion
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>Класс, инкапсулирующий в себя критерий подбора версий</summary>
    [Serializable]
    public sealed class VersionsRuleCriterion : ICloneable
    {
      /// <summary>Сугубо для внутреннего применения</summary>
      private CompareFunctionsHelper FCFunc = new CompareFunctionsHelper();
      /// <summary>Сугубо для внутреннего применения</summary>
      private CompareTypesHelper FCTypes = new CompareTypesHelper();
      /// <summary>Сугубо для внутреннего применения</summary>
      private CompareOperatorsHelper FCOperators = new CompareOperatorsHelper();
      /// <summary>
      /// Применять ли логическое отрицание к результатам работы функции сравнения
      /// </summary>
      private bool FNegation;
      /// <summary>Функция сравнения</summary>
      private string FCompareFunction = "";
      /// <summary>Логическая функция. По умолчанию - "ИЛИ"</summary>
      private string FBoolFunction = CompareOperatorsHelper.ctDefaultFunction;
      /// <summary>
      /// Исходный атрибут, по которому проводится подбор версий
      /// </summary>
      private ComparableValue FMainAttribute = new ComparableValue();
      /// <summary>
      ///  Список значений для сравнения (список ComparableValue)
      /// </summary>
      private List<ComparableValue> FComparableValues = new List<ComparableValue>();

      /// <summary>Хелпер по работе со списком функций сравнения</summary>
      public CompareFunctionsHelper CFunc
      {
        [DebuggerStepThrough] get => this.FCFunc;
      }

      /// <summary>
      /// Применять ли логическое отрицание к результатам работы функции сравнения
      /// </summary>
      public bool Negation
      {
        [DebuggerStepThrough] get => this.FNegation;
        set
        {
          this.FNegation = false;
          if (value)
          {
            if (!this.FCFunc.CanBeNegative(this.FCompareFunction))
              return;
            this.FNegation = value;
          }
          else
            this.FNegation = value;
        }
      }

      /// <summary>Функция сравнения</summary>
      public string CompareFunction
      {
        [DebuggerStepThrough] get => this.FCompareFunction;
      }

      /// <summary>Логическая функция</summary>
      public string BoolFunction
      {
        [DebuggerStepThrough] get => this.FBoolFunction;
        set
        {
          if (this.FBoolFunction == value || !this.FCOperators.IsMember(value))
            return;
          this.FBoolFunction = value;
        }
      }

      /// <summary>
      /// Исходный атрибут, по которому проводится подбор версий
      /// </summary>
      public ComparableValue MainAttribute
      {
        [DebuggerStepThrough] get => this.FMainAttribute;
      }

      /// <summary>Значения для сравнения (список ComparableValue)</summary>
      public ComparableValue this[int index]
      {
        get
        {
          return index >= this.FComparableValues.Count || index < 0 ? (ComparableValue) null : this.FComparableValues[index];
        }
      }

      /// <summary>
      /// Список значений для сравнения (список ComparableValue)
      /// </summary>
      public List<ComparableValue> ComparableValues
      {
        [DebuggerStepThrough] get => this.FComparableValues;
      }

      /// <summary>
      /// Создать неинициализированный экземпляр класса VersionsRuleCriterion
      /// </summary>
      public VersionsRuleCriterion()
      {
      }

      /// <summary>
      /// Создать экземпляр класса VersionsRuleCriterion, настроенный на атрибут AttrGUID
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="AAttrGUID">GUID атрибута, по которому будет проводиться подбор версий</param>
      /// <param name="ACompareFunction">Функция сравнения (значение класса CompareFunctionsHelper)</param>
      /// <param name="ABoolFunction">Логическая функция для сравнения со следующим критерием подбора</param>
      /// <param name="ACompareTypes">Список видов значений для сравнения (константы класса CompareTypesHelper)</param>
      /// <param name="ACompareValues">Список значений для сравнения.
      /// Тип данных зависит от типа основного атрибута.
      /// Если CompareTo = CompareTypesHelper.ctAttribute, то в CompareTo содержится GUID второго атрибута</param>
      public VersionsRuleCriterion(
        IUserSession session,
        string AAttrGUID,
        string ACompareFunction,
        string ABoolFunction,
        ArrayList ACompareTypes,
        ArrayList ACompareValues)
      {
        this.MainAttribute.Criterion = this;
        this.MainAttribute.SetValueType(session, "ATTRIBUTE", (object) AAttrGUID);
        this.SetCompareFunction(session, ACompareFunction);
        this.BoolFunction = ABoolFunction;
        if (ACompareTypes == null || ACompareTypes.Count <= 0 || ACompareValues == null || ACompareValues.Count <= 0 || ACompareTypes.Count != ACompareValues.Count)
          return;
        this.FComparableValues.Clear();
        for (int index = 0; index < ACompareTypes.Count; ++index)
          this.FComparableValues.Add(new ComparableValue(session, ACompareTypes[index].ToString(), (object) ACompareValues[index].ToString(), this));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Value"></param>
      /// <returns></returns>
      public bool SetCompareFunction(IUserSession session, string Value)
      {
        if (!this.FCFunc.IsMember(Value))
          return false;
        int count1 = this.FComparableValues.Count;
        int num1 = this.FCFunc.MinComparableValues(Value);
        int num2 = this.FCFunc.MaxComparableValues(Value);
        if (num2 <= 0)
          this.FComparableValues.Clear();
        if (num1 == num2 && count1 > num2 && count1 > 0 && num2 > 0)
        {
          for (int index = count1 - 1; index >= num2; --index)
            this.FComparableValues.RemoveAt(index);
        }
        int count2 = this.FComparableValues.Count;
        if (count2 < num1)
        {
          for (int index = 0; index < num1 - count2; ++index)
            this.FComparableValues.Add(new ComparableValue(session, "CONST", (object) cvConsts.cvConst, this));
        }
        this.FCompareFunction = Value;
        this.Negation = this.FNegation;
        return true;
      }

      /// <summary>
      /// Добавить новое значение для сравнения в список.
      /// Если добавлять нельзя, вернёт -1, иначе индекс вновь добавленного значения
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="ValueType">Тип сравниваемой величины (константа класса CompareTypesHelper)</param>
      /// <param name="Value">Сравниваемое значение (если ValueType = CompareTypesHelper.ctAttribute, то Value = GUID атрибута)</param>
      /// <returns>-1 если нельзя добавлять значения, иначе индекс вновь добавленного значения</returns>
      public int Add(IUserSession session, string ValueType, string Value)
      {
        if (this.CompareFunction == "" || this.FCFunc.MaxComparableValues(this.CompareFunction) <= this.ComparableValues.Count)
          return -1;
        ComparableValue comparableValue = new ComparableValue(session, ValueType, (object) Value, this);
        this.ComparableValues.Add(comparableValue);
        return this.ComparableValues.IndexOf(comparableValue);
      }

      /// <summary>Удалить из списка указанное значение для сравнения</summary>
      /// <param name="obj">Удаляемое значение для сравнения</param>
      /// <returns>true, если значение успешно удалено</returns>
      public bool Remove(ComparableValue obj)
      {
        if (obj == null || this.ComparableValues.IndexOf(obj) < 0 || this.CompareFunction == "" || this.FCFunc.MinComparableValues(this.CompareFunction) >= this.ComparableValues.Count)
          return false;
        this.ComparableValues.Remove(obj);
        return true;
      }

      /// <summary>
      /// Удалить из списка значение для сравнения с указанным индексом
      /// </summary>
      /// <param name="index">Номер удаляемого значения для сравнения</param>
      /// <returns>true, если значение успешно удалено</returns>
      public bool RemoveAt(int index)
      {
        if (index < 0 || index >= this.ComparableValues.Count || this.CompareFunction == "" || this.FCFunc.MinComparableValues(this.CompareFunction) >= this.ComparableValues.Count)
          return false;
        this.ComparableValues.RemoveAt(index);
        return true;
      }

      /// <summary>
      /// Проверяет, можно ли добавлять значение(я) для сравнения данному критерию
      /// </summary>
      /// <returns>true, если значение(я) можно добавлять</returns>
      public bool CanAddValue()
      {
        return this.ComparableValues.Count < this.FCFunc.MaxComparableValues(this.CompareFunction);
      }

      /// <summary>
      /// Метод корректирует типы данных у значений для сравнения при смене типа главного атрибута
      /// </summary>
      public void CorrectValuesType()
      {
        int count = this.ComparableValues.Count;
        if (count <= 0)
          return;
        FieldTypes attrType = this.MainAttribute.AttrType;
        bool isAttrList = this.MainAttribute.Attribute.IsAttrList;
        for (int index = 0; index < count; ++index)
        {
          ComparableValue comparableValue = this.ComparableValues[index];
          if (comparableValue.ValueType != "ATTRIBUTE")
          {
            comparableValue.AttrType = attrType;
            comparableValue.Attribute.AttrType = attrType;
            comparableValue.Attribute.IsAttrList = isAttrList;
          }
        }
      }

      /// <summary>
      /// Проверяет, можно ли у данного критерия удалять значение(я) для сравнения
      /// </summary>
      /// <returns>true, если значение(я) можно удалять</returns>
      public bool CanDeleteValue()
      {
        int num1 = this.FCFunc.MinComparableValues(this.CompareFunction);
        int num2 = this.FCFunc.MaxComparableValues(this.CompareFunction);
        int count = this.ComparableValues.Count;
        return num1 != num2 && count > num1;
      }

      /// <summary>Является ли данный критерий валидным по всем полям</summary>
      /// <returns>true, если критерий полностью валиден по всем полям</returns>
      public bool Valid()
      {
        bool flag1 = this.MainAttribute.Valid();
        bool flag2 = this.FCFunc.IsMember(this.FCompareFunction);
        bool flag3 = true;
        for (int index = 0; index < this.FComparableValues.Count; ++index)
        {
          ComparableValue fcomparableValue = this.FComparableValues[index];
          if (fcomparableValue != null)
            flag3 = flag3 && fcomparableValue.Valid();
        }
        int num1 = this.FCFunc.MinComparableValues(this.CompareFunction);
        int num2 = this.FCFunc.MaxComparableValues(this.CompareFunction);
        int count = this.ComparableValues.Count;
        bool flag4 = num1 == num2 && count == num1 || count >= num1 && count <= num2;
        return flag1 & flag2 & flag3 & flag4;
      }

      /// <summary>
      /// Проверяет, есть ли в критерии хотя бы одно значение для сравненения
      /// с типом данных "Переменная" (значение, которое укажет пользователь)
      /// </summary>
      /// <returns>true, если найдена хотя бы одна переменная</returns>
      public bool HasVariableValues()
      {
        int count = this.ComparableValues.Count;
        for (int index = 0; index < count; ++index)
        {
          if (this.ComparableValues[index].ValueType == "VARIABLE")
            return true;
        }
        return false;
      }

      /// <summary>Очистить все внутренние поля экземпляру класса</summary>
      public void Clear()
      {
        this.FNegation = false;
        this.FCompareFunction = "";
        this.FMainAttribute.Clear();
        this.FComparableValues.Clear();
        this.FBoolFunction = CompareOperatorsHelper.ctDefaultFunction;
      }

      /// <summary>Загрузить критерий из указанного узла XML-документа</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Node">Узел документа XML, содержащий тег criterion со всеми параметрами</param>
      /// <returns>true, если загруженный критерий валиден</returns>
      public bool LoadXML(IUserSession session, XmlNode Node)
      {
        this.Clear();
        if (Node == null || Node.Name != "criterion")
          return false;
        XmlNode namedItem1 = Node.Attributes.GetNamedItem("function");
        if (namedItem1 == null || !this.FCFunc.IsMember(namedItem1.InnerText))
          return false;
        XmlNode namedItem2 = Node.Attributes.GetNamedItem("guid");
        if (namedItem2 == null)
          return false;
        XmlNode namedItem3 = Node.Attributes.GetNamedItem("not");
        XmlNode namedItem4 = Node.Attributes.GetNamedItem("bool");
        this.MainAttribute.SetValueType(session, "ATTRIBUTE", (object) namedItem2.InnerText);
        this.SetCompareFunction(session, namedItem1.InnerText);
        this.Negation = namedItem3 != null && namedItem3.InnerText == "1";
        if (namedItem4 != null)
          this.BoolFunction = namedItem4.InnerText;
        int count1 = this.ComparableValues.Count;
        int count2 = Node.ChildNodes.Count;
        if (this.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545" || this.MainAttribute.Attribute.AttrGUID == "cad0002b-306c-11d8-b4e9-00304f19f545" || MyAttributeHelper.IsUserGuidType(this.MainAttribute.Attribute.AttrGUID))
          this.MainAttribute.Attribute.AttrType = FieldTypes.ftString;
        if (count2 > count1 && count2 > 0)
        {
          int num = count2 - count1;
          if (num > 0)
          {
            for (int index = 0; index < num; ++index)
            {
              ComparableValue comparableValue = new ComparableValue();
              comparableValue.Criterion = this;
              if (comparableValue.ValueType != "ATTRIBUTE")
                comparableValue.Attribute.Assign(this.MainAttribute.Attribute);
              this.ComparableValues.Add(comparableValue);
            }
          }
        }
        int count3 = this.ComparableValues.Count;
        int num1 = 0;
        if (count3 > 0 && count2 > 0)
        {
          for (; count3 > 0 && count2 > 0; --count2)
          {
            XmlNode childNode = Node.ChildNodes[num1];
            if (childNode != null)
            {
              ComparableValue comparableValue = this.ComparableValues[num1];
              comparableValue.LoadXML(session, childNode);
              if (comparableValue.ValueType != "ATTRIBUTE")
                comparableValue.Attribute.Assign(this.MainAttribute.Attribute);
              ++num1;
            }
            --count3;
          }
        }
        this.CorrectValuesType();
        return this.Valid();
      }

      /// <summary>
      /// Сохранить значения экземпляра класса в узел Node документа XML
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="Node">Существующий узел документа XML, в который надо выгрузить данные объекта</param>
      /// <returns>true, если выгрузка данных в узел Node прошла успешно</returns>
      public bool SaveXML(IUserSession session, XmlNode Node)
      {
        if (session == null || Node == null || Node.Name != "criterion")
          return false;
        XmlDocument ownerDocument = Node.OwnerDocument;
        if (ownerDocument == null)
          return false;
        Node.RemoveAll();
        XmlAttribute attribute1 = ownerDocument.CreateAttribute("function");
        attribute1.InnerText = this.CompareFunction;
        Node.Attributes.Append(attribute1);
        XmlAttribute attribute2 = ownerDocument.CreateAttribute("guid");
        attribute2.InnerText = this.MainAttribute.Value.ToString();
        Node.Attributes.Append(attribute2);
        if (this.Negation)
        {
          XmlAttribute attribute3 = ownerDocument.CreateAttribute("not");
          attribute3.InnerText = "1";
          Node.Attributes.Append(attribute3);
        }
        XmlAttribute attribute4 = ownerDocument.CreateAttribute("bool");
        attribute4.InnerText = this.BoolFunction;
        Node.Attributes.Append(attribute4);
        this.CorrectValuesType();
        if (this.ComparableValues.Count > 0)
        {
          for (int index = 0; index < this.ComparableValues.Count; ++index)
            this.ComparableValues[index].SaveXML(session, ownerDocument, Node);
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
        XmlElement element = Doc.CreateElement("criterion");
        xmlNode.AppendChild((XmlNode) element);
        this.SaveXML(session, (XmlNode) element);
        return (XmlNode) element;
      }

      /// <summary>
      /// Конвертировать все значения с типом "Переменная" в тип "Константа"
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      public void ConvertVarsToConsts(IUserSession session)
      {
        int count = this.ComparableValues.Count;
        for (int index = 0; index < count; ++index)
        {
          ComparableValue comparableValue = this.ComparableValues[index];
          if (comparableValue.ValueType == "VARIABLE")
            comparableValue.SetType(session, "CONST");
        }
      }

      /// <summary>
      /// Вернуть значение атрибута для его отображения на экране	в "читабельном виде"
      /// </summary>
      /// <param name="Mode">Что должно входить в результат:
      /// 1 - только название критерия подбора,
      /// 2 - переменные значения для сравнения</param>
      /// <returns>Значение атрибута в виде строки для отображения</returns>
      public object GetDisplayValue(int Mode)
      {
        string displayValue1 = $"[{this.MainAttribute.Attribute.AttrName}] ";
        if (Mode == 1)
          return (object) displayValue1;
        if (this.FComparableValues.Count <= 0)
          return (object) string.Empty;
        StringBuilder stringBuilder = new StringBuilder(this.FComparableValues.Count);
        lock (this)
        {
          for (int index = 0; index < this.FComparableValues.Count; ++index)
          {
            ComparableValue fcomparableValue = this.FComparableValues[index];
            if (Mode == 2 && fcomparableValue.ValueType == "VARIABLE")
              stringBuilder.Append($"\"{fcomparableValue.GetDisplayValue(1)}\" ");
          }
        }
        string str = stringBuilder.ToString().Trim();
        string displayValue2;
        if (str.Length <= 0)
          displayValue2 = "";
        else
          displayValue2 = $"[{this.MainAttribute.Attribute.AttrName} {str}] ";
        return (object) displayValue2;
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        VersionsRuleCriterion versionsRuleCriterion = new VersionsRuleCriterion()
        {
          FNegation = this.FNegation,
          FCompareFunction = this.FCompareFunction,
          FBoolFunction = this.FBoolFunction,
          FMainAttribute = (ComparableValue) this.FMainAttribute.Clone()
        };
        versionsRuleCriterion.FMainAttribute.Criterion = versionsRuleCriterion;
        versionsRuleCriterion.FComparableValues.Clear();
        int count = this.FComparableValues.Count;
        for (int index = 0; index < count; ++index)
        {
          ComparableValue fcomparableValue = this.FComparableValues[index];
          fcomparableValue.Criterion = versionsRuleCriterion;
          versionsRuleCriterion.FComparableValues.Add(fcomparableValue.Clone() as ComparableValue);
        }
        return (object) versionsRuleCriterion;
      }

      /// <summary>
      /// Выполнить полную проверку совместимости текущего критерия подбора с указанным
      /// </summary>
      /// <param name="Value">Критерий для проверки</param>
      /// <returns>true, если критерии полностью совместимы</returns>
      public bool IsCompatible(VersionsRuleCriterion Value)
      {
        if (Value == null || this.ComparableValues.Count != Value.ComparableValues.Count || this.CompareFunction != Value.CompareFunction || this.BoolFunction != Value.BoolFunction || this.Negation != Value.Negation || !this.MainAttribute.IsCompatible(Value.MainAttribute))
          return false;
        if (this.ComparableValues.Count > 0)
        {
          for (int index = 0; index < this.ComparableValues.Count; ++index)
          {
            ComparableValue comparableValue1 = this.ComparableValues[index];
            ComparableValue comparableValue2 = Value.ComparableValues[index];
            if (!(comparableValue1 != null & comparableValue1.IsCompatible(comparableValue2)))
              return false;
          }
        }
        return true;
      }
    }
}
