
// Type: Intermech.Interfaces.SelectionService.SelectionWrapper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>
    /// Класс для работы с условиями выборок в формате масива из элементов ConditionStructure
    /// </summary>
    /// <summary>Конструктор</summary>
    /// <param name="isClientPart">признак, показывающий на клиенте или на сервере
    /// создается данный экземпляр Wrapperа. Если экземпляр создается на сервере, то надо
    /// установить false, если на клиенте - true</param>
    public class SelectionWrapper(bool isClientPart) : SelectionXMLWrapper(isClientPart)
    {
      /// <summary>
      /// Функция для получения условия выборки в текстовом виде (точее для получения заголовка условия)
      /// </summary>
      /// <param name="conStr">Структура с параметрами условия выборки</param>
      /// <returns>Текстовое прелсиавление условия выборки</returns>
      public static string GenerateConditionCaption(IUserSession session, ConditionStructure conStr)
      {
        string str = "";
        string val1 = "";
        string val2 = "";
        string val1a = "";
        bool flag = true;
        object attribute = conStr.Attribute;
        if (attribute != null && !attribute.Equals((object) 0))
        {
          IDBAttributeType attrType = (IDBAttributeType) null;
          switch (attribute)
          {
            case Guid guid when !guid.Equals(Guid.Empty):
              attrType = session.GetAttributeType((Guid) attribute, false);
              break;
            case string _ when (string) attribute != "":
              attrType = session.GetAttributeType((string) attribute, false);
              break;
            case int num when num != 0:
              attrType = session.GetAttributeType((int) attribute, false);
              break;
          }
          string name;
          if (attrType != null)
          {
            name = attrType.Name;
            val1 = SelectionParameter.ConvertToString(session, conStr.Value, attrType);
            val2 = SelectionParameter.ConvertToString(session, conStr.Value2, attrType);
          }
          else
          {
            name = Convert.ToString(attribute);
            flag = false;
          }
          str = SelectionWrapper.addAttributeName(name);
        }
        else if (SelectionParameter.IsInRelationOpr(conStr.RelationalOperator))
        {
          val1 = conStr.RelationalOperator == RelationalOperators.EntersIn || conStr.RelationalOperator == RelationalOperators.ConsistFrom || conStr.RelationalOperator == RelationalOperators.ExistsInVersionContext ? SelectionParameter.GetObjectLinkTextValue(session, conStr.Value) : SelectionParameter.GetObjectTypeLinkTextValue(session, conStr.Value);
          if (conStr.TypeID != null)
            val1a = $"{val1a}{LocalizationHolder.rm.GetString("Interfaces_116")}{SelectionParameter.GetRelationTypeTextValue(session, conStr.TypeID)}\")";
        }
        else if (conStr.RelationalOperator == RelationalOperators.ObjectTypeFilter)
          val1 = SelectionParameter.GetObjectTypeLinkTextValue(session, conStr.Value);
        return flag ? str + SelectionWrapper.FormingValueString(conStr.RelationalOperator, val1, val1a, val2, string.Empty) : LocalizationHolder.rm.GetString("Interfaces_737") + str;
      }

      private static string addAttributeName(string attributeName) => $"\"{attributeName}\" ";

      public static string FormingValueString(
        RelationalOperators op,
        string val1,
        string val1a,
        string val2,
        string attributeName)
      {
        if (SelectionParameter.IsLinkRelationOpr(op))
          return $"{RelationalOperatorsHelper.GetCaption(op)} в \"{attributeName}\" у объектов типа \"{val1}\" ";
        string str = attributeName != string.Empty ? SelectionWrapper.addAttributeName(attributeName) : string.Empty;
        return op == RelationalOperators.Between || op == RelationalOperators.NotBetween ? str + RelationalOperatorsHelper.GetCaption(op).ToLower() + string.Format(LocalizationHolder.rm.GetString("Interfaces_574"), (object) val1, (object) val2) : str + RelationalOperatorsHelper.GetCaption(op).ToLower() + (val1 != "" ? $" \"{val1}\"{val1a}" : "") + (val2 != "" ? $"{LocalizationHolder.rm.GetString("Interfaces_117")}\"{val2}\"" : "");
      }

      /// <summary>
      /// Преобразование атрибута к строковому представлению глобального идентификатора GUID
      /// </summary>
      /// <param name="attributeObject">объект атрибута</param>
      /// <returns>Строковое представление глобального идентификатора GUID</returns>
      private string GetAttributeGuidString(IUserSession session, object attributeObject)
      {
        string attributeGuidString = "";
        if (attributeObject != null)
        {
          if (attributeObject is Guid guid && !guid.Equals(Guid.Empty))
            attributeGuidString = Convert.ToString((object) (Guid) attributeObject);
          else if (session != null)
          {
            IDBAttributeType dbAttributeType = (IDBAttributeType) null;
            if (attributeObject is string)
              dbAttributeType = session.GetAttributeType(Convert.ToString(attributeObject));
            else if (attributeObject is int && Convert.ToInt32(attributeObject) != 0)
              dbAttributeType = session.GetAttributeType(Convert.ToInt32(attributeObject));
            if (dbAttributeType != null)
            {
              guid = (dbAttributeType as IDBGuid).GUID;
              attributeGuidString = guid.ToString();
            }
          }
        }
        return attributeGuidString;
      }

      /// <summary>
      /// Преобразование условия выборки ConditionStructure в XML-узел
      /// </summary>
      /// <param name="session"></param>
      /// <param name="cs">условие выборки которое надо преобразовать</param>
      /// <param name="xmlDocument">XML-документ в котором производится преобразование</param>
      /// <returns>XML-узел в который преобразовано условие</returns>
      protected XmlNode PackCondition(
        IUserSession session,
        ConditionStructure cs,
        XmlDocument xmlDocument)
      {
        if (xmlDocument == null)
          return (XmlNode) null;
        XmlNode element = (XmlNode) xmlDocument.CreateElement("Condition");
        this.AddTextElement(element, "AttributeSource", Convert.ToString((int) cs.AttributeSource));
        this.AddTextElement(element, "AttributeGUID", this.GetAttributeGuidString(session, cs.Attribute));
        this.AddTextElement(element, "RelationalOperators", Convert.ToString((int) cs.RelationalOperator));
        this.AddValueElement(element, "Value1", cs.Value);
        this.AddValueElement(element, "Value2", cs.Value2);
        this.AddTextElement(element, "LogicalOperator", Convert.ToString((int) cs.LogicalOperator));
        this.AddTextElement(element, "GroupID", Convert.ToString(cs.GroupID));
        this.AddTextElement(element, "SQL", cs.SQL);
        this.AddTextElement(element, "CaseSensitive", cs.CaseSensitive ? "1" : "0");
        this.AddTextElement(element, "Content", Convert.ToString((int) cs.Content));
        this.AddValueElement(element, "TypeID", cs.TypeID);
        if (cs.NestedConditions != null && cs.NestedConditions.Length != 0)
          element.AppendChild(this.PackConditionTree(session, xmlDocument, (XmlNode) xmlDocument.CreateElement("NestedConditions"), cs.NestedConditions));
        return element;
      }

      /// <summary>
      /// Преобразование XML-узла в условие выборки ConditionStructure
      /// </summary>
      /// <param name="xmlNode">XML-узел, который надо преобразовать</param>
      /// <returns>Условие выборки</returns>
      protected ConditionStructure UnpackCondition(XmlNode xmlNode)
      {
        ConditionStructure conditionStructure = new ConditionStructure((string) null, RelationalOperators.None, (object) null, (object) null, LogicalOperators.NONE, 0, false);
        if (xmlNode != null)
        {
          foreach (XmlNode childNode in xmlNode.ChildNodes)
          {
            switch (childNode.Name)
            {
              case "AttributeGUID":
                conditionStructure.Attribute = childNode.InnerText == "" ? (object) null : (object) new Guid(childNode.InnerText);
                continue;
              case "AttributeSource":
                conditionStructure.AttributeSource = childNode.InnerText == "" ? AttributeSourceTypes.Auto : (AttributeSourceTypes) Convert.ToInt32(childNode.InnerText);
                continue;
              case "CaseSensitive":
                conditionStructure.CaseSensitive = !(childNode.InnerText == "") && !(childNode.InnerText == "0");
                continue;
              case "Content":
                conditionStructure.Content = childNode.InnerText == "" ? ColumnContents.Text : (ColumnContents) Convert.ToInt32(childNode.InnerText);
                continue;
              case "GroupID":
                conditionStructure.GroupID = childNode.InnerText == "" ? 0 : Convert.ToInt32(childNode.InnerText);
                continue;
              case "LogicalOperator":
                conditionStructure.LogicalOperator = childNode.InnerText == "" ? LogicalOperators.NONE : (LogicalOperators) Convert.ToInt32(childNode.InnerText);
                continue;
              case "NestedConditions":
                conditionStructure.NestedConditions = this.UnpackConditionTree(childNode);
                continue;
              case "RelationalOperators":
                conditionStructure.RelationalOperator = childNode.InnerText == "" ? RelationalOperators.None : (RelationalOperators) Convert.ToInt32(childNode.InnerText);
                continue;
              case "SQL":
                conditionStructure.SQL = childNode.InnerText;
                continue;
              case "TypeID":
                conditionStructure.TypeID = this.LoadValueElement(childNode);
                continue;
              case "Value1":
                conditionStructure.Value = this.LoadValueElement(childNode);
                continue;
              case "Value2":
                conditionStructure.Value2 = this.LoadValueElement(childNode);
                continue;
              default:
                continue;
            }
          }
        }
        return conditionStructure;
      }

      /// <summary>
      /// Преобразование списка условий выборки в древовидную XML-структуру
      /// </summary>
      /// <param name="xmlDocument">XML-документ в который производится преобразование</param>
      /// <param name="conditionStructures">список условий - массив ConditionStructure</param>
      /// <returns>Корневой узел древовидной XML-структуры условий выборки</returns>
      private XmlNode PackConditionTree(
        IUserSession session,
        XmlDocument xmlDocument,
        XmlNode rootNode,
        ConditionStructure[] conditionStructures)
      {
        if (conditionStructures.Length == 0)
          return rootNode;
        for (int index = 0; index < conditionStructures.Length; ++index)
        {
          XmlNode newChild = this.PackCondition(session, conditionStructures[index], xmlDocument);
          XmlAttribute attribute = xmlDocument.CreateAttribute("ConditionText");
          attribute.Value = SelectionWrapper.GenerateConditionCaption(session, conditionStructures[index]);
          newChild.Attributes.Append(attribute);
          if (conditionStructures[index].GroupID > 0)
          {
            int groupId = conditionStructures[index].GroupID;
            List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
            while (groupId > 0)
            {
              ++index;
              groupId += conditionStructures[index].GroupID;
              conditionStructureList.Add(conditionStructures[index]);
            }
            XmlNode element = (XmlNode) xmlDocument.CreateElement("ChildNodes");
            newChild.AppendChild(this.PackConditionTree(session, xmlDocument, element, conditionStructureList.ToArray()));
          }
          rootNode.AppendChild(newChild);
        }
        return rootNode;
      }

      /// <summary>
      /// Преобразование древовидной XML-структуры в список условий выборки
      /// </summary>
      /// <param name="xmlNode">верхний узел древовидной XML-структуры</param>
      /// <returns>список условий - массив ConditionStructure</returns>
      protected ConditionStructure[] UnpackConditionTree(XmlNode xmlNode)
      {
        ArrayList arrayList = new ArrayList();
        foreach (XmlNode childNode1 in xmlNode.ChildNodes)
        {
          if (childNode1.Name == "Condition")
          {
            arrayList.Add((object) this.UnpackCondition(childNode1));
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
            {
              if (childNode2.Name == "ChildNodes")
              {
                foreach (ConditionStructure conditionStructure in this.UnpackConditionTree(childNode2))
                  arrayList.Add((object) conditionStructure);
              }
            }
          }
        }
        ConditionStructure[] conditionStructureArray = new ConditionStructure[arrayList.Count];
        for (int index = 0; index < arrayList.Count; ++index)
          conditionStructureArray[index] = (ConditionStructure) arrayList[index];
        return conditionStructureArray;
      }

      protected List<ConditionStructure> GetConditionStructures(XmlDocument xmlDocument)
      {
        List<ConditionStructure> conditionStructures = new List<ConditionStructure>();
        XmlNode documentElement = (XmlNode) xmlDocument.DocumentElement;
        if (documentElement != null)
        {
          foreach (XmlNode childNode in documentElement.ChildNodes)
          {
            if (childNode.Name == "ChildNodes")
            {
              foreach (ConditionStructure conditionStructure in this.UnpackConditionTree(childNode))
                conditionStructures.Add(conditionStructure);
            }
          }
        }
        return conditionStructures;
      }

      public ConditionStructure[] LoadConditionStructures(
        IUserSession userSession,
        long objectID,
        XmlDocument xmlDocument)
      {
        List<ConditionStructure> conditionStructures = this.GetConditionStructures(xmlDocument);
        IDBObject dbObject = userSession.GetObject(objectID);
        IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad00155-306c-11d8-b4e9-00304f19f545"));
        bool flag1 = attributeByGuid1 != null && !attributeByGuid1.IsNull && Convert.ToBoolean(attributeByGuid1.Value);
        bool flag2 = dbObject.ObjectType == userSession.GetObjectType(new Guid("cad00150-306c-11d8-b4e9-00304f19f545")).ObjectType || dbObject.ObjectType == userSession.GetObjectType(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545")).ObjectType || dbObject.ObjectType == userSession.GetObjectType(new Guid("cad0014f-306c-11d8-b4e9-00304f19f545")).ObjectType;
        if (flag1 | flag2)
        {
          ConditionStructure conditionStructure = new ConditionStructure((string) null, RelationalOperators.InSelection, (object) objectID, LogicalOperators.AND, 0, flag2 && this.ShowInternalFolders);
          conditionStructure.TypeID = (object) dbObject.ObjectType;
          if (flag1)
          {
            IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cadd99b3-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid2 != null && attributeByGuid2.AsBoolean)
              conditionStructure.Value2 = (object) true;
          }
          conditionStructures.Add(conditionStructure);
        }
        IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(new Guid("cadd9971-306c-11d8-b4e9-00304f19f545"), false);
        if (attributeByGuid3 != null && attributeByGuid3.AsBoolean)
        {
          ConditionStructure conditionStructure = new ConditionStructure(0, RelationalOperators.LocalObjectTypes, (object) null, LogicalOperators.NONE, 0, false);
          conditionStructures.Add(conditionStructure);
        }
        ConditionStructure[] array = conditionStructures.ToArray();
        this.CorrectLogicalOperators(ref array);
        return array;
      }

      private void CorrectLogicalOperators(ref ConditionStructure[] condList)
      {
        if (condList.Length == 0)
          return;
        for (int index = 0; index < condList.Length; ++index)
        {
          ConditionStructure conditionStructure = condList[index];
          if (conditionStructure.NestedConditions != null && conditionStructure.NestedConditions.Length != 0)
            this.CorrectLogicalOperators(ref conditionStructure.NestedConditions);
          if (index > 0)
            condList[index - 1].LogicalOperator = conditionStructure.LogicalOperator;
        }
        condList[condList.Length - 1].LogicalOperator = LogicalOperators.NONE;
      }

      /// <summary>
      /// Загрузка из базы для указанной выборки массива условий ConditionStructure
      /// </summary>
      /// <param name="userSession">интерфейс сессии в которой производится загрузка</param>
      /// <param name="objectID">идентификатор объекта (идентификатор выборки)</param>
      /// <returns>список условий - массив ConditionStructure</returns>
      public ConditionStructure[] LoadConditionStructures(IUserSession session, long objectID)
      {
        return this.LoadConditionStructures(session, objectID, this.LoadXML(session, objectID));
      }

      /// <summary>
      /// Сохранение в базе для указанной выборки массива условий ConditionStructure
      /// </summary>
      /// <param name="session">интерфейс сессии в которой производится сохранение</param>
      /// <param name="objectID">идентификатор объекта (идентификатор выборки)</param>
      /// <param name="conditionStructures">список условий - массив ConditionStructure</param>
      /// <returns>если сохранение прошло успешно - возвращается true, иначе - false</returns>
      public bool SaveConditionStructures(
        IUserSession session,
        long objectID,
        ConditionStructure[] conditionStructures)
      {
        return this.SaveXML(session, session.GetObject(objectID), this.SaveToXML(session, conditionStructures));
      }

      public XmlDocument SaveToXML(IUserSession session, ConditionStructure[] conditionStructures)
      {
        return this.SaveToXML(session, conditionStructures, true);
      }

      /// <summary>Сохранение условий ConditionStructure в  XML</summary>
      /// <param name="uSession">интерфейс сессии в которой производится сохранение</param>
      /// <param name="conditionStructures">список условий - массив ConditionStructure</param>
      /// <returns></returns>
      public XmlDocument SaveToXML(
        IUserSession session,
        ConditionStructure[] conditionStructures,
        bool correct)
      {
        if (correct && conditionStructures.Length != 0)
        {
          for (int index = conditionStructures.Length - 1; index > 0; --index)
            conditionStructures[index].LogicalOperator = conditionStructures[index - 1].LogicalOperator;
          conditionStructures[0].LogicalOperator = LogicalOperators.AND;
        }
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.AppendChild((XmlNode) xmlDocument.CreateXmlDeclaration("1.0", (string) null, (string) null));
        XmlNode element = (XmlNode) xmlDocument.CreateElement("SelectionParameters");
        xmlDocument.AppendChild(element);
        element.AppendChild(this.PackConditionTree(session, xmlDocument, (XmlNode) xmlDocument.CreateElement("ChildNodes"), conditionStructures));
        return xmlDocument;
      }
    }
}
