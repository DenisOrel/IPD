
// Type: Intermech.Interfaces.SelectionService.SelectionXMLWrapper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>
    /// Служебный класс в котором реализована работа с XML и базой (запись и чтение BLOB)
    /// для условий выборки
    /// </summary>
    public class SelectionXMLWrapper
    {
      /// <summary>
      /// Флаг отображающий принадлежность какому сервису (клиентскому или серверному SelectionXMLWrapper принадлежит)
      /// </summary>
      protected bool IsClientPartWrapper;
      /// <summary>
      /// Кэш условий выборки для классификаторов, используется только на клиентской   стороне
      /// т.к. эти условия сохраняются только для клиента на период сеанса работы
      /// </summary>
      private static Dictionary<long, MemoryStream> _classifConditions;
      private readonly int defaultBlobBlockSize = 262144 /*0x040000*/;
      /// <summary>
      /// Флаг, определяющий показывать ли вложенные папки классификатора
      /// </summary>
      public bool ShowInternalFolders;
      /// <summary>
      /// GUID атрибута в котором хранятся параметры выборки (т.е. условия выборки)
      /// </summary>
      protected static Guid parmGuid = new Guid("cad0069b-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Константы для именования узлов и атрибутов XML-документа в котором хранится
      /// информация о условиях выборки и параметрах этих условий
      /// </summary>
      protected const string _elnmSelectionParameters = "SelectionParameters";
      protected const string _elnmCondition = "Condition";
      protected const string _attrConditionText = "ConditionText";
      protected const string _elnmAttributeSource = "AttributeSource";
      protected const string _elnmAttributeGUID = "AttributeGUID";
      protected const string _elnmRelationalOperators = "RelationalOperators";
      protected const string _attrValueType = "ValueType";
      protected const string _elnmValue1 = "Value1";
      protected const string _elnmValue2 = "Value2";
      protected const string _elnmValueItem = "ValueItem";
      protected const string _elnmValueObjGuid = "ValueObjGuid";
      protected const string _elnmValueAttrGuid = "ValueAttrGuid";
      protected const string _elnmLogicalOperator = "LogicalOperator";
      protected const string _elnmGroupID = "GroupID";
      protected const string _elnmSQL = "SQL";
      protected const string _elnmCaseSensitive = "CaseSensitive";
      protected const string _elementContent = "Content";
      protected const string _elnmTypeID = "TypeID";
      protected const string _elnmChildNodes = "ChildNodes";
      protected const string _elnmHandSelection = "HandSelection";
      protected const string _elnmHandSelectionSwitch = "HandSelectionEnabled";
      protected const string _elnmHandSelectionItem = "HSI";
      protected const string _elnmNestedConditions = "NestedConditions";

      /// <summary>Конструктор</summary>
      public SelectionXMLWrapper(bool isClientPart) => this.IsClientPartWrapper = isClientPart;

      /// <summary>Добавление к XML-узлу нового текстового элемента</summary>
      /// <param name="parentNode">XML-узел к которому будет добавлен текстовый элемент</param>
      /// <param name="elementName">наименование нового текстового элемента</param>
      /// <param name="elementText">текстовое значение нового элемента</param>
      protected void AddTextElement(XmlNode parentNode, string elementName, string elementText)
      {
        XmlNode element = (XmlNode) parentNode.OwnerDocument.CreateElement(elementName);
        XmlNode textNode = (XmlNode) parentNode.OwnerDocument.CreateTextNode(elementText);
        element.AppendChild(textNode);
        parentNode.AppendChild(element);
      }

      /// <summary>Преобразование параметра условия выборки в XML-узел</summary>
      /// <param name="parentNode">XML-узел к которому будет добавлен узел со значением</param>
      /// <param name="elementName">имя параметра условия выборки</param>
      /// <param name="elementValue">значение параметра условия выборки</param>
      protected void AddValueElement(XmlNode parentNode, string elementName, object elementValue)
      {
        XmlDocument ownerDocument = parentNode.OwnerDocument;
        XmlNode element = (XmlNode) ownerDocument.CreateElement(elementName);
        XmlAttribute attribute = ownerDocument.CreateAttribute("ValueType");
        if (elementValue != null)
          attribute.Value = elementValue.GetType().FullName;
        else
          attribute.Value = "";
        element.Attributes.Append(attribute);
        if (elementValue != null && elementValue is IList)
        {
          for (int index = 0; index < ((ICollection) elementValue).Count; ++index)
            this.AddValueElement(element, "ValueItem", ((IList) elementValue)[index]);
        }
        else if (elementValue != null && elementValue is InputObjectAttribute)
        {
          this.AddValueElement(element, "ValueObjGuid", (object) ((InputObjectAttribute) elementValue).ObjectGUID);
          this.AddValueElement(element, "ValueAttrGuid", (object) ((InputObjectAttribute) elementValue).AttributeGUID);
        }
        else
        {
          string text = (string) null;
          if (elementValue != null)
          {
            TypeConverter converter = TypeDescriptor.GetConverter(elementValue.GetType());
            if (converter != null && converter.CanConvertTo(typeof (string)))
              text = converter.ConvertToInvariantString(elementValue);
          }
          if (text == null)
            text = Convert.ToString(elementValue, (IFormatProvider) CultureInfo.InvariantCulture);
          XmlText textNode = ownerDocument.CreateTextNode(text);
          element.AppendChild((XmlNode) textNode);
        }
        parentNode.AppendChild(element);
      }

      /// <summary>
      /// Получение значения параметра условия выборки из XML-узла
      /// </summary>
      /// <param name="xmlNode">XML-узел в котором хранится значение параметра</param>
      /// <returns>значения параметра условия выборки</returns>
      protected object LoadValueElement(XmlNode xmlNode)
      {
        if (xmlNode == null)
          return (object) null;
        object obj1 = (object) null;
        string typeName = xmlNode.Attributes["ValueType"] != null ? xmlNode.Attributes["ValueType"].Value : "";
        if (typeName != "")
        {
          Type type1 = Type.GetType(typeName);
          if (type1.IsArray || type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (List<>))
          {
            List<object> objectList = new List<object>();
            Type type2 = (Type) null;
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
              if (childNode.Name == "ValueItem")
              {
                object obj2 = this.LoadValueElement(childNode);
                if (obj2 != null)
                {
                  objectList.Add(obj2);
                  type2 = obj2.GetType();
                }
              }
            }
            obj1 = !(type2 == (Type) null) ? (!(type2 == typeof (long)) ? (!(type2 == typeof (int)) ? (object) objectList.ToArray() : (object) objectList.ConvertAll<int>((Converter<object, int>) (x => (int) x)).ToArray()) : (object) objectList.ConvertAll<long>((Converter<object, long>) (x => (long) x)).ToArray()) : (object) objectList.ToArray();
          }
          else if (type1 == typeof (InputObjectAttribute))
          {
            InputObjectAttribute inputObjectAttribute = new InputObjectAttribute();
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
              object obj3 = this.LoadValueElement(childNode);
              if (obj3 != null && obj3.GetType() == typeof (Guid))
              {
                switch (childNode.Name)
                {
                  case "ValueObjGuid":
                    inputObjectAttribute.ObjectGUID = (Guid) obj3;
                    continue;
                  case "ValueAttrGuid":
                    inputObjectAttribute.AttributeGUID = (Guid) obj3;
                    continue;
                  default:
                    continue;
                }
              }
            }
            obj1 = (object) inputObjectAttribute;
          }
          else if (type1 == typeof (MeasuredValue))
            obj1 = (object) MeasureHelper.ConvertToMeasuredValue(xmlNode.InnerText);
          else if (xmlNode.InnerText != "")
          {
            TypeConverter converter = TypeDescriptor.GetConverter(Type.GetType(typeName));
            if (converter != null && converter.CanConvertFrom(xmlNode.InnerText.GetType()))
              obj1 = converter.ConvertFromInvariantString(xmlNode.InnerText);
          }
        }
        return obj1;
      }

      /// <summary>Запись параметров выборки в базу</summary>
      /// <param name="session"></param>
      /// <param name="dbObject">идентификатор объекта (идентификатор выборки)</param>
      /// <param name="memoryStream">поток, который нужно записать в базу</param>
      /// <returns>Если запись прошла успешно - возвращается true</returns>
      private bool SaveToBase(IUserSession session, IDBObject dbObject, MemoryStream memoryStream)
      {
        bool flag = false;
        if (dbObject == null || memoryStream == null)
          return false;
        if (this.IsClientPartWrapper && MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")).Contains(dbObject.ObjectType))
        {
          if (SelectionXMLWrapper._classifConditions == null)
            SelectionXMLWrapper._classifConditions = new Dictionary<long, MemoryStream>(1);
          if (!SelectionXMLWrapper._classifConditions.ContainsKey(dbObject.ObjectID))
            SelectionXMLWrapper._classifConditions.Add(dbObject.ObjectID, memoryStream);
          else
            SelectionXMLWrapper._classifConditions[dbObject.ObjectID] = memoryStream;
          return true;
        }
        IDBAttributeType attributeType = session.GetAttributeType(SelectionXMLWrapper.parmGuid);
        IDBAttribute dbAttribute = dbObject.GetAttributeByGuid(SelectionXMLWrapper.parmGuid) ?? dbObject.Attributes.AddAttribute(attributeType.AttributeID, false);
        if (dbAttribute is IBlobWriter)
        {
          IBlobWriter blobWriter = dbAttribute as IBlobWriter;
          BlobInformation blobInfo = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "", ArcMethods.NotPacked, "Selection Structure XML");
          long length = memoryStream.Length;
          if (blobWriter.OpenBlob(blobInfo, false))
          {
            try
            {
              memoryStream.Position = 0L;
              int defaultBlobBlockSize = this.defaultBlobBlockSize;
              long num = 0;
              while (num < length)
              {
                int count = length - num > (long) defaultBlobBlockSize ? defaultBlobBlockSize : (int) (length - num);
                byte[] numArray = new byte[count];
                num += (long) memoryStream.Read(numArray, 0, count);
                blobWriter.WriteDataBlock(numArray);
              }
              flag = true;
            }
            catch (Exception ex)
            {
              blobWriter.CancelWrite();
              throw ex;
            }
          }
        }
        return flag;
      }

      /// <summary>Чтение параметров выборки из базы</summary>
      /// <param name="session"></param>
      /// <param name="objectID">идентификатор объекта (идентификатор выборки)</param>
      /// <returns>Если чтение прошло успешно - возвращается прочитанный поток, иначе возвращается null</returns>
      private MemoryStream LoadFromBase(IUserSession session, long objectID, out bool oldVersion)
      {
        oldVersion = true;
        if (objectID == 0L)
          return (MemoryStream) null;
        if (this.IsClientPartWrapper && MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")).Contains(session.GetObjectInfo(objectID).ObjectTypeID))
          return SelectionXMLWrapper._classifConditions == null || !SelectionXMLWrapper._classifConditions.ContainsKey(objectID) ? (MemoryStream) null : SelectionXMLWrapper._classifConditions[objectID];
        IDBAttribute attributeByGuid = session.GetObject(objectID).GetAttributeByGuid(SelectionXMLWrapper.parmGuid);
        if (attributeByGuid == null || attributeByGuid.IsNull)
          return (MemoryStream) null;
        MemoryStream aDestStream = new MemoryStream();
        new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(session);
        return aDestStream;
      }

      /// <summary>
      /// Загрузка из базы для указанной выборки ее условий представленных в формате XML-документа
      /// </summary>
      /// <param name="session"></param>
      /// <param name="objectID">идентификатор объекта (идентификатор выборки)</param>
      /// <returns>возвращается XML-документ</returns>
      protected XmlDocument LoadXML(IUserSession session, long objectID)
      {
        bool oldVersion;
        MemoryStream inStream = this.LoadFromBase(session, objectID, out oldVersion);
        XmlDocument xmlDocument = new XmlDocument();
        if (inStream != null && inStream.Length > 0L)
        {
          inStream.Position = 0L;
          if (!oldVersion)
            xmlDocument.PreserveWhitespace = true;
          xmlDocument.Load((Stream) inStream);
        }
        return xmlDocument;
      }

      /// <summary>
      /// Сохранение в базе для указанной выборки ее условий представленных в формате XML-документа
      /// </summary>
      /// <param name="obj">идентификатор объекта (идентификатор выборки)</param>
      /// <param name="xmlDocument">XML-документ который нужно сохранить условия выборки</param>
      /// <returns>если сохранение прошло успешно - возвращается true, иначе - false</returns>
      protected bool SaveXML(IUserSession session, IDBObject obj, XmlDocument xmlDocument)
      {
        MemoryStream memoryStream = new MemoryStream();
        if (xmlDocument != null)
        {
          xmlDocument.PreserveWhitespace = true;
          xmlDocument.Save((Stream) memoryStream);
        }
        return this.SaveToBase(session, obj, memoryStream);
      }
    }
}
