
// Type: Intermech.Interfaces.VersionsRule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.Search.Utilities;
using Intermech.Search.VersionSelectionRules.AddingToDropdownList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, хранящий правило подбора версий и набор критериев
    /// </summary>
    [Serializable]
    public sealed class VersionsRule : IAssignable, ICloneable, IEnumerator, IEnumerable
    {
      /// <summary>
      /// Отображать в составах версии объектов, которые были некорректно подобраны по конкретизации
      /// </summary>
      public static bool ShowInvalidConcreteVersions;
      /// <summary>
      /// Осуществлять ли запись поля ActualDate правила подбора версий в базу данных
      /// </summary>
      public static bool WriteRuleDateTime = true;
      /// <summary>
      /// Имя колонки, в которой будет размещаться статус строки - отфильтрована или нет
      /// </summary>
      public static string RowStatusColumnName = "7573339D9E944EB2A4DB0EB75BF1FB09";
      /// <summary>Заголовок системного правила "Все версии объектов"</summary>
      public static readonly string captionAllVersions = LocalizationHolder.rm.GetString("Interfaces_614");
      /// <summary>
      /// Заголовок системного правила "Все версии объектов с учётом конкретизации"
      /// </summary>
      public static readonly string captionAllConcreteVersions = LocalizationHolder.rm.GetString("Interfaces_615");
      /// <summary>
      /// Заголовок системного правила "Последние версии объектов"
      /// </summary>
      public static readonly string captionLatestVersions = LocalizationHolder.rm.GetString("Interfaces_616");
      /// <summary>Заголовок системного правила "Подбор базовых версий"</summary>
      public static readonly string captionBaseVersions = LocalizationHolder.rm.GetString("Interfaces_617");
      /// <summary>
      /// Заголовок системного правила "Последовательное проведение изменений"
      /// </summary>
      public static readonly string captionSequentialModifications = LocalizationHolder.rm.GetString("Interfaces_618");
      /// <summary>ID текущего объекта с правилом</summary>
      public long RuleObjectID;
      /// <summary>Заголовок текущего объекта с правилом</summary>
      public string RuleObjectCaption;
      /// <summary>Guid текущего объекта с правилом</summary>
      public string RuleObjectGuid;
      /// <summary>
      /// Тип текущего объекта с правилом (Общее / Персональное)
      /// </summary>
      public int RuleObjectType;
      /// <summary>Дата последней модификации объекта</summary>
      public DateTime RuleObjectModified;
      /// <summary>Тип правила подбора версий</summary>
      private VersionsRuleType FCurrentRuleType;
      /// <summary>Внутренняя позиция для работы c IEnumerator</summary>
      private int _pos = -1;
      /// <summary>
      /// Коллекция критериев подбора версий (экземпляры VersionsRuleCriterion)
      /// </summary>
      private List<VersionsRuleCriterion> FCriterions = new List<VersionsRuleCriterion>(2);
      /// <summary>
      /// Дата для получения состава. Если равна DateTime.MinValue, то не используется.
      /// </summary>
      private DateTime FActualDate;
      /// <summary>
      /// Дата для получения состава. Если равна DateTime.MinValue, то не используется.
      /// </summary>
      [NonSerialized]
      private DateTime FActualDateBeforeSave;
      /// <summary>
      /// Используется для запрета использования правила при выполнении команд Редактировать, Открыть, Открыть с помощью...
      /// </summary>
      private bool _usingProhibited = true;
      /// <summary>
      /// Коллекция значений атрибутов, которые используются в правиле подбора версий.
      /// Коллекция - пары значений [ID атрибута]=[MyAttributeElement] со значением этого атрибута.
      /// Список корректируется автоматически, но его значение надо обновлять вызовом метода LoadRuleAttributes.
      /// </summary>
      [NonSerialized]
      private Dictionary<int, MyAttributeElement> FRuleAttributes = new Dictionary<int, MyAttributeElement>(2);
      /// <summary>Сугубо для внутреннего применения</summary>
      [NonSerialized]
      private CompareFunctionsHelper CFHelper = new CompareFunctionsHelper();
      private bool _ignoreSoftConcretization;
      private bool _addToDropDownList;
      private const string IgnoreSoftConcretizationXMLAttributeName = "IgnoreSoftConcretization";

      /// <summary>
      /// true - правило для редактирования составов, false - обычное правило
      /// </summary>
      public bool EditingRule
      {
        [DebuggerStepThrough] get => !this._usingProhibited;
        set
        {
          this._usingProhibited = !value;
          if (!this.EditingRule)
            return;
          this._ignoreSoftConcretization = false;
        }
      }

      public bool IgnoreSoftConcretization
      {
        get => this._ignoreSoftConcretization;
        set => this._ignoreSoftConcretization = value;
      }

      /// <summary>
      /// Используется для запрета использования правила при выполнении команд Редактировать, Открыть, Открыть с помощью...
      /// (true - обычное правило, false - правило для редактирования составов объектов)
      /// </summary>
      public bool UsingProhibited
      {
        [DebuggerStepThrough] get => this._usingProhibited;
        set => this._usingProhibited = value;
      }

      /// <summary>Является ли данное правило правилом по умолчанию</summary>
      public bool IsDefault
      {
        get
        {
          return !ObjectHelper.IsUnknownObjectVersionID(this.RuleObjectID) && (long) ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.Versions_DefaultVersionRule) == this.RuleObjectID;
        }
        set
        {
          ServiceLocator.Get<IConfigurationOptionRepository>().AddOrUpdate(ConfigurationOptionKeys.Versions_DefaultVersionRule, (object) this.RuleObjectID);
        }
      }

      /// <summary>Тип правила подбора версий</summary>
      public VersionsRuleType CurrentRuleType
      {
        [DebuggerStepThrough] get => this.FCurrentRuleType;
        set => this.FCurrentRuleType = value;
      }

      /// <summary>Список всех критериев подбора</summary>
      public List<VersionsRuleCriterion> Criterions
      {
        [DebuggerStepThrough] get => this.FCriterions;
      }

      /// <summary>Критерий подбора с указанным индексом</summary>
      public VersionsRuleCriterion this[int index]
      {
        get
        {
          return index >= this.FCriterions.Count || index < 0 ? (VersionsRuleCriterion) null : this.FCriterions[index];
        }
      }

      /// <summary>
      /// Дата для получения состава. Если равна DateTime.MinValue, то не используется.
      /// </summary>
      public DateTime ActualDate
      {
        [DebuggerStepThrough] get => this.FActualDate;
        set => this.FActualDate = value;
      }

      public bool AddToDropDownList
      {
        get => this._addToDropDownList;
        set => this._addToDropDownList = value;
      }

      /// <summary>
      /// Создать пустой класс, инициализировать внутренние поля
      /// </summary>
      public VersionsRule()
      {
        if (this.FRuleAttributes == null)
          this.FRuleAttributes = new Dictionary<int, MyAttributeElement>(2);
        this.CFHelper = new CompareFunctionsHelper();
        this.Clear();
      }

      /// <summary>
      /// Создать пустой класс, заполнить его содержимым из указанного объекта-источника
      /// </summary>
      /// <param name="source"></param>
      public VersionsRule(object source)
        : this()
      {
        this.Assign(source);
      }

      /// <summary>
      /// Создать пустой класс, инициализировать внутренние поля
      /// </summary>
      /// <param name="actualDate">Дата для получения состава. Если равна DateTime.MinValue, то не используется.</param>
      public VersionsRule(DateTime actualDate)
        : this()
      {
        this.FActualDate = actualDate;
      }

      /// <summary>
      /// Создать пустой класс, загрузить данные из документа XML
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Doc"></param>
      public VersionsRule(IUserSession session, XmlDocument Doc)
        : this()
      {
        this.LoadFromXMLDoc(session, Doc);
      }

      /// <summary>
      /// Создать пустой класс, загрузить данные из указанного потока (внутри потока - документ XML)
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Source">Исходные данные (весь поток целиком - документ XML)</param>
      public VersionsRule(IUserSession session, Stream Source)
        : this()
      {
        this.LoadFromStream(session, Source);
      }

      /// <summary>
      /// Загрузить правило подбора версий из указанного XML-документа
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Doc">Документ XML, содержащий правило подбора версий</param>
      /// <returns>true, если загрузка прошла успешно</returns>
      public bool LoadFromXMLDoc(IUserSession session, XmlDocument Doc)
      {
        this.Clear();
        if (Doc == null)
          return false;
        XmlNode documentElement = (XmlNode) Doc.DocumentElement;
        if (documentElement == null || documentElement.Name != "Intermech.NET")
          return false;
        XmlNode namedItem1 = documentElement.Attributes.GetNamedItem("rule_type");
        this.FCurrentRuleType = VersionsRuleType.vrtStandardRule;
        if (namedItem1 != null && namedItem1.InnerText.Length > 0)
        {
          int result;
          if (int.TryParse(namedItem1.InnerText, out result))
          {
            try
            {
              this.FCurrentRuleType = (VersionsRuleType) result;
            }
            catch
            {
              this.FCurrentRuleType = VersionsRuleType.vrtStandardRule;
            }
          }
        }
        documentElement.Attributes.GetNamedItem("default_rule");
        XmlNode namedItem2 = documentElement.Attributes.GetNamedItem("actualDate");
        if (namedItem2 != null && namedItem2.InnerText.Length > 0)
        {
          DateTime result;
          this.FActualDate = !DateTime.TryParse(namedItem2.InnerText, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? DateTime.MinValue : result;
        }
        if (!VersionsRule.WriteRuleDateTime)
          this.FActualDate = DateTime.MinValue;
        XmlNode namedItem3 = documentElement.Attributes.GetNamedItem("guid");
        if (namedItem3 != null && namedItem3.InnerText.Length > 0)
          this.RuleObjectGuid = namedItem3.InnerText;
        XmlNode namedItem4 = documentElement.Attributes.GetNamedItem("IgnoreSoftConcretization");
        if (namedItem4 != null && !string.IsNullOrEmpty(namedItem4.Value))
          this.IgnoreSoftConcretization = Convert.ToBoolean(namedItem4.Value);
        if (documentElement.ChildNodes.Count == 0)
          return false;
        for (int i = 0; i < documentElement.ChildNodes.Count; ++i)
        {
          XmlNode childNode = documentElement.ChildNodes[i];
          if (!(childNode.Name != "criterion"))
          {
            VersionsRuleCriterion Criterion = new VersionsRuleCriterion();
            Criterion.LoadXML(session, childNode);
            if (Criterion.MainAttribute.Attribute.AttrType != FieldTypes.ftUnknown)
              this.Add(Criterion);
          }
        }
        this.SyncAttributes();
        return true;
      }

      /// <summary>
      /// Сохранить правило подбора версий в указанный XML-документ
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Doc">Документ XML, в который надо сохранить правило</param>
      /// <returns>true, если сохранение прошло успешно</returns>
      public bool SaveToXMLDoc(IUserSession session, XmlDocument Doc)
      {
        if (Doc == null)
          return false;
        this.Valid(session);
        this.SyncAttributes();
        XmlNode documentElement = (XmlNode) Doc.DocumentElement;
        if (documentElement == null || documentElement.Name != "Intermech.NET")
          return false;
        documentElement.RemoveAll();
        XmlAttribute attribute1 = Doc.CreateAttribute("rule_type");
        attribute1.InnerText = ((int) this.CurrentRuleType).ToString();
        documentElement.Attributes.Append(attribute1);
        if (VersionsRule.WriteRuleDateTime)
        {
          DateTime date = this.FActualDate.Date;
          XmlAttribute attribute2 = Doc.CreateAttribute("actualDate");
          attribute2.InnerText = date.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
          documentElement.Attributes.Append(attribute2);
        }
        XmlAttribute attribute3 = Doc.CreateAttribute("guid");
        if (this.RuleObjectGuid.Equals((object) Guid.Empty))
          this.RuleObjectGuid = Guid.NewGuid().ToString();
        attribute3.InnerText = this.RuleObjectGuid.ToString();
        documentElement.Attributes.Append(attribute3);
        XmlAttribute attribute4 = Doc.CreateAttribute("IgnoreSoftConcretization");
        attribute4.Value = this.IgnoreSoftConcretization.ToString();
        documentElement.Attributes.Append(attribute4);
        if (this.Criterions.Count > 0)
        {
          for (int index = 0; index < this.Criterions.Count; ++index)
            this.Criterions[index].SaveXML(session, Doc, documentElement);
        }
        return true;
      }

      /// <summary>Загрузить правила в документ XML из потока</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Source">Исходные данные (весь поток целиком)</param>
      public void LoadFromStream(IUserSession session, Stream Source)
      {
        this.Clear();
        if (Source == null || Source.Length == 0L)
          return;
        XmlDocument Doc = new XmlDocument();
        Source.Position = 0L;
        try
        {
          Doc.Load(Source);
          this.LoadFromXMLDoc(session, Doc);
        }
        catch
        {
          this.Clear();
        }
      }

      /// <summary>Записать правила из документа XML в поток</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="Dest">Поток, в который надо сохранить XML-документ</param>
      /// <returns>true, если сохранение прошло успешно</returns>
      public bool SaveToStream(IUserSession session, Stream Dest)
      {
        if (Dest == null)
          return false;
        XmlDocument Doc = new XmlDocument();
        Dest.Position = 0L;
        Dest.SetLength(0L);
        Doc.LoadXml("<?xml version=\"1.0\" ?>\n<Intermech.NET />\n");
        bool stream = this.SaveToXMLDoc(session, Doc);
        try
        {
          Doc.CreateXmlDeclaration("1.0", "UTF-8", (string) null);
          Doc.Save(Dest);
        }
        catch
        {
          stream = false;
        }
        return stream;
      }

      /// <summary>Загрузить данные из указанного объекта</summary>
      /// <param name="session">Сессия</param>
      /// <param name="ARuleObjectID">ID версии объекта, из которой будет вытягиваться информация правила</param>
      public void LoadFromObject(IUserSession session, long ARuleObjectID)
      {
        this.Clear();
        if (ARuleObjectID == 0L)
          return;
        this.RuleObjectID = ARuleObjectID;
        if (session == null)
          return;
        IDBObject RuleObject = session.GetObject(this.RuleObjectID, false);
        this.LoadFromObject(session, RuleObject);
      }

      /// <summary>Сохранить данные в указанный объект</summary>
      /// <param name="session">Сессия</param>
      /// <param name="ARuleObjectID">ID версии объекта, в которую будут записаны данные</param>
      /// <returns>true, если сохранение прошло успешно</returns>
      public bool SaveToObject(IUserSession session, long ARuleObjectID)
      {
        if (ARuleObjectID == 0L || session == null)
          return false;
        this.RuleObjectID = ARuleObjectID;
        IDBObject RuleObject = session.GetObject(this.RuleObjectID, false);
        return this.SaveToObject(session, RuleObject);
      }

      /// <summary>Загрузить данные из указанного объекта</summary>
      /// <param name="session">Сессия</param>
      /// <param name="RuleObject">Объект, из которой будет вытягиваться информация правила</param>
      public void LoadFromObject(IUserSession session, IDBObject RuleObject)
      {
        this.Clear();
        if (RuleObject == null || session == null)
          return;
        if (RuleObject.ObjectGUID == Guid.Parse("cad0069c-306c-11d8-b4e9-00304f19f545"))
        {
          this.ConvertToLatestVersionsRule(session);
        }
        else
        {
          IDBAttribute attributeByGuid1;
          IDBAttribute attributeByGuid2;
          try
          {
            attributeByGuid1 = RuleObject.GetAttributeByGuid(new Guid("cad001d2-306c-11d8-b4e9-00304f19f545"));
            attributeByGuid2 = RuleObject.GetAttributeByGuid(new Guid("cad00820-306c-11d8-b4e9-00304f19f545"));
          }
          catch
          {
            return;
          }
          this._usingProhibited = attributeByGuid2 == null || (this._usingProhibited = attributeByGuid2.AsBoolean);
          if (attributeByGuid1 != null)
          {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
              if (attributeByGuid1 is IDBShortBlobAttribute shortBlobAttribute)
              {
                ShortBlobValue blobValue = shortBlobAttribute.GetBlobValue();
                if (blobValue.RealFileSize > 0L && blobValue.Value != null)
                {
                  memoryStream.Write(blobValue.Value, 0, blobValue.Value.Length);
                  memoryStream.Position = 0L;
                  if (blobValue.ArcMethod == ArcMethods.ZLibPacked)
                  {
                    MemoryStream outStream = new MemoryStream();
                    ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
                    memoryStream.Close();
                    memoryStream = outStream;
                  }
                  this.LoadFromStream(session, (Stream) memoryStream);
                }
              }
              int StandardCriterions;
              int AdvancedCriterions;
              this.CriterionsCount(out StandardCriterions, out AdvancedCriterions);
              if (StandardCriterions == 1 && AdvancedCriterions == 1)
              {
                VersionsRuleCriterion criterion = this.Criterions[0];
                if (criterion.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545" && criterion.ComparableValues.Count == 1 && criterion.CompareFunction == "EQUALS")
                {
                  int num = criterion.ComparableValues[0].ValueType == "CONST" ? 1 : 0;
                }
              }
              this.RuleObjectID = RuleObject.ObjectID;
              this.RuleObjectGuid = RuleObject.ObjectGUID.ToString();
              this.RuleObjectType = RuleObject.ObjectType;
              this.RuleObjectCaption = RuleObject.Caption;
              this.RuleObjectModified = RuleObject.CreateDate;
            }
            finally
            {
              this.SyncAttributes();
              memoryStream.Close();
            }
          }
        }
        IDBAttribute attributeById = RuleObject.GetAttributeByID(AddingToDropdownListConstants.AddToDropdownListAttributeTypeID);
        if (attributeById == null)
          return;
        this._addToDropDownList = attributeById.AsBoolean;
      }

      /// <summary>Сохранить данные в указанный объект</summary>
      /// <param name="session">Сессия</param>
      /// <param name="RuleObject">Объект, в который будут записаны данные</param>
      /// <returns>true, если сохранение прошло успешно</returns>
      public bool SaveToObject(IUserSession session, IDBObject RuleObject)
      {
        if (RuleObject == null || session == null)
          return false;
        IDBAttribute attributeByGuid1;
        IDBAttribute attributeByGuid2;
        try
        {
          attributeByGuid1 = RuleObject.GetAttributeByGuid(new Guid("cad001d2-306c-11d8-b4e9-00304f19f545"));
          attributeByGuid2 = RuleObject.GetAttributeByGuid(new Guid("cad00820-306c-11d8-b4e9-00304f19f545"));
        }
        catch
        {
          return false;
        }
        if (attributeByGuid2 != null)
          attributeByGuid2.AsBoolean = this._usingProhibited;
        if (attributeByGuid1 != null)
        {
          MemoryStream Dest = new MemoryStream();
          try
          {
            this.SaveToStream(session, (Stream) Dest);
            Dest.Position = 0L;
            IBlobWriter blobWriter = (IBlobWriter) attributeByGuid1;
            if (blobWriter != null)
            {
              byte[] array = Dest.ToArray();
              long length = Dest.Length;
              BlobInformation blobInfo = new BlobInformation(length, length, DateTime.Now, "VersionRules.xml", ArcMethods.NotPacked, string.Empty);
              blobWriter.OpenBlob(blobInfo, false);
              blobWriter.WriteDataBlock(array);
            }
          }
          finally
          {
            this.SyncAttributes();
            Dest.Close();
          }
          this.RuleObjectID = RuleObject.ObjectID;
          this.RuleObjectGuid = RuleObject.ObjectGUID.ToString();
          this.RuleObjectType = RuleObject.ObjectType;
          this.RuleObjectCaption = RuleObject.Caption;
          this.RuleObjectModified = RuleObject.CreateDate;
        }
        IDBAttribute attributeById = RuleObject.GetAttributeByID(AddingToDropdownListConstants.AddToDropdownListAttributeTypeID);
        if (attributeById != null)
          attributeById.Value = (object) this._addToDropDownList;
        return true;
      }

      /// <summary>Загрузить данные из указанного объекта</summary>
      /// <param name="session">Сессия</param>
      /// <param name="RuleAttribute">Атрибут, из которой будет вытягиваться информация правила</param>
      public void LoadFromAttribute(IUserSession session, IDBAttribute RuleAttribute)
      {
        this.Clear();
        if (RuleAttribute == null || session == null)
          return;
        MemoryStream Source = new MemoryStream();
        try
        {
          IBlobReader blobReader = (IBlobReader) RuleAttribute;
          if (blobReader != null && blobReader.OpenBlob(0).RealFileSize > 0L)
          {
            byte[] buffer = blobReader.ReadDataBlock(0);
            if (buffer != null)
            {
              Source.Write(buffer, 0, buffer.Length);
              Source.Position = 0L;
              this.LoadFromStream(session, (Stream) Source);
            }
          }
          int StandardCriterions;
          int AdvancedCriterions;
          this.CriterionsCount(out StandardCriterions, out AdvancedCriterions);
          if (StandardCriterions != 1 || AdvancedCriterions != 1)
            return;
          VersionsRuleCriterion criterion = this.Criterions[0];
          if (!(criterion.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545") || criterion.ComparableValues.Count != 1 || !(criterion.CompareFunction == "EQUALS"))
            return;
          int num = criterion.ComparableValues[0].ValueType == "CONST" ? 1 : 0;
        }
        finally
        {
          this.SyncAttributes();
          Source.Close();
        }
      }

      /// <summary>Сохранить данные в указанный объект</summary>
      /// <param name="session">Сессия</param>
      /// <param name="RuleAttribute">Атрибут типа ShortBLOB, в который будут записаны данные</param>
      /// <returns>true, если сохранение прошло успешно</returns>
      public bool SaveToAttribute(IUserSession session, IDBAttribute RuleAttribute)
      {
        if (RuleAttribute == null || session == null)
          return false;
        MemoryStream Dest = new MemoryStream();
        try
        {
          this.SaveToStream(session, (Stream) Dest);
          Dest.Position = 0L;
          IBlobWriter blobWriter = (IBlobWriter) RuleAttribute;
          if (blobWriter != null)
          {
            byte[] array = Dest.ToArray();
            long length = Dest.Length;
            BlobInformation blobInfo = new BlobInformation(length, length, DateTime.Now, "VersionRules.xml", ArcMethods.NotPacked, string.Empty);
            blobWriter.OpenBlob(blobInfo, false);
            blobWriter.WriteDataBlock(array);
          }
        }
        finally
        {
          this.SyncAttributes();
          Dest.Close();
        }
        return true;
      }

      /// <summary>
      /// Загрузить правила в документ XML из строки. В строке - XML-документ с правилом
      /// </summary>
      /// <param name="session">Сессия, из которой вытягивается информация</param>
      /// <param name="XML">Исходные данные</param>
      public void LoadFromXML(IUserSession session, string XML)
      {
        this.Clear();
        if (XML.Length == 0)
          return;
        XmlDocument Doc = new XmlDocument();
        if (Doc == null)
          return;
        try
        {
          Doc.LoadXml(XML);
          this.LoadFromXMLDoc(session, Doc);
        }
        catch
        {
          this.Clear();
        }
      }

      /// <summary>
      /// Записать правила из документа XML в строку с текстом XML
      /// </summary>
      /// <param name="session">Сессия, из которой вытягивается информация</param>
      /// <returns>В строке - XML-документ с правилом</returns>
      public string SaveToXML(IUserSession session)
      {
        XmlDocument Doc = new XmlDocument();
        if (Doc == null)
          return "<?xml version=\"1.0\" ?>\n<Intermech.NET />\n";
        try
        {
          Doc.LoadXml("<?xml version=\"1.0\" ?>\n<Intermech.NET />\n");
          Doc.CreateXmlDeclaration("1.0", "UTF-8", (string) null);
          this.SaveToXMLDoc(session, Doc);
        }
        catch
        {
          return "<?xml version=\"1.0\" ?>\n<Intermech.NET />\n";
        }
        string xml = Doc.InnerXml;
        if (xml.Length == 0)
          xml = "<?xml version=\"1.0\" ?>\n<Intermech.NET />\n";
        return xml;
      }

      /// <summary>Добавить новый критерий подбора в список критериев</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="AttrGUID">GUID атрибута, по которому будет проводиться подбор версий</param>
      /// <param name="CompareFunction">Функция сравнения (значение класса CompareFunctionsHelper)</param>
      /// <param name="BoolFunction">Логическая функция для сравнения со следующим критерием подбора</param>
      /// <param name="CompareTypes">Список видов значений для сравнения (константы класса CompareTypesHelper)</param>
      /// <param name="CompareValues">Список значений для сравнения.
      /// Тип данных зависит от типа основного атрибута.
      /// Если CompareTo = CompareTypesHelper.ctAttribute, то в CompareTo содержится GUID второго атрибута</param>
      public VersionsRuleCriterion Add(
        IUserSession session,
        string AttrGUID,
        string CompareFunction,
        string BoolFunction,
        ArrayList CompareTypes,
        ArrayList CompareValues)
      {
        lock (this)
        {
          VersionsRuleCriterion versionsRuleCriterion = new VersionsRuleCriterion(session, AttrGUID, CompareFunction, BoolFunction, CompareTypes, CompareValues);
          this.FCriterions.Add(versionsRuleCriterion);
          this.SyncAttributes();
          return versionsRuleCriterion;
        }
      }

      /// <summary>Добавить готовый критерий подбора в список критериев</summary>
      /// <param name="Criterion">Созданный и инициализированный критерий подбора</param>
      /// <returns>Индекс добавленного критерия в списке критериев подбора</returns>
      public int Add(VersionsRuleCriterion Criterion)
      {
        if (Criterion == null)
          return -1;
        lock (this)
        {
          this.FCriterions.Add(Criterion);
          int num = this.FCriterions.IndexOf(Criterion);
          this.SyncAttributes();
          return num;
        }
      }

      /// <summary>Добавить новый критерий подбора в список критериев</summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <param name="index">Индекс в списке для вставки</param>
      /// <param name="AttrGUID">GUID атрибута, по которому будет проводиться подбор версий</param>
      /// <param name="CompareFunction">Функция сравнения (значение класса CompareFunctionsHelper)</param>
      /// <param name="BoolFunction">Логическая функция для  сравнения со следующим критерием подбора</param>
      /// <param name="CompareTypes">Список видов значений для сравнения (константы класса CompareTypesHelper)</param>
      /// <param name="CompareValues">Список значений для сравнения.
      /// Тип данных зависит от типа основного атрибута.
      /// Если CompareTo = CompareTypesHelper.ctAttribute, то в CompareTo содержится GUID второго атрибута</param>
      public VersionsRuleCriterion Insert(
        IUserSession session,
        int index,
        string AttrGUID,
        string CompareFunction,
        string BoolFunction,
        ArrayList CompareTypes,
        ArrayList CompareValues)
      {
        lock (this)
        {
          VersionsRuleCriterion versionsRuleCriterion = new VersionsRuleCriterion(session, AttrGUID, CompareFunction, BoolFunction, CompareTypes, CompareValues);
          this.FCriterions.Insert(index, versionsRuleCriterion);
          this.SyncAttributes();
          return versionsRuleCriterion;
        }
      }

      /// <summary>
      /// Вставить готовый критерий подбора в список критериев в указанную позицию
      /// </summary>
      /// <param name="index">Индекс в списке для вставки</param>
      /// <param name="Criterion">Созданный и инициализированный критерий подбора</param>
      /// <returns>Индекс добавленного критерия в списке критериев подбора</returns>
      public void Insert(int index, VersionsRuleCriterion Criterion)
      {
        if (Criterion == null)
          return;
        this.FCriterions.Insert(index, Criterion);
        this.SyncAttributes();
      }

      /// <summary>Удалить из списка указанный критерий</summary>
      /// <param name="Criterion">Удаляемый критерий</param>
      /// <returns>true, если удаление успешно</returns>
      public bool Remove(VersionsRuleCriterion Criterion)
      {
        if (Criterion == null || this.FCriterions.IndexOf(Criterion) < 0)
          return false;
        lock (this)
        {
          this.FCriterions.Remove(Criterion);
          this.SyncAttributes();
          return true;
        }
      }

      /// <summary>Удалить из списка критерий с указанным индексом</summary>
      /// <param name="index">Индекс критерия подбора</param>
      /// <returns>true, если удаление успешно</returns>
      public bool RemoveAt(int index)
      {
        if (index >= this.FCriterions.Count || index < 0)
          return false;
        lock (this)
        {
          this.FCriterions.RemoveAt(index);
          this.SyncAttributes();
          return true;
        }
      }

      /// <summary>Обменять местами два критерия подбора</summary>
      /// <param name="Index1">Индекс первого критерия</param>
      /// <param name="Index2">Индекс второго критерия</param>
      /// <returns>true, если обмен удачен</returns>
      public bool Exchange(int Index1, int Index2)
      {
        int count = this.FCriterions.Count;
        if (count == 0 || Index1 < 0 || Index2 < 0 || Index1 >= count || Index2 >= count)
          return false;
        lock (this)
        {
          VersionsRuleCriterion fcriterion = this.FCriterions[Index2];
          this.FCriterions[Index2] = this.FCriterions[Index1];
          this.FCriterions[Index1] = fcriterion;
        }
        return true;
      }

      /// <summary>
      /// Выполнить полную проверку правила, его критериев и их значений для сравнения
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      /// <returns></returns>
      public bool Valid(IUserSession session)
      {
        if (session == null)
          return false;
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        bool flag = true;
        int num1 = 0;
        int num2 = 0;
        lock (this)
        {
          if (this.FCriterions == null)
            this.FCriterions = new List<VersionsRuleCriterion>();
          int count = this.FCriterions.Count;
          for (int index = 0; index < count; ++index)
          {
            VersionsRuleCriterion fcriterion = this.FCriterions[index];
            if (fcriterion != null && fcriterion != null)
            {
              flag = flag && fcriterion.Valid();
              if (this.CFHelper.IsAggregate(fcriterion.CompareFunction))
                ++num2;
              else
                ++num1;
            }
          }
          if (num1 < 1)
          {
            for (int index = 0; index < 1 - num1; ++index)
              this.Insert(session, 0, "cad0002c-306c-11d8-b4e9-00304f19f545", this.CFHelper.DefaultFunction, CompareOperatorsHelper.ctDefaultFunction, (ArrayList) null, (ArrayList) null);
          }
          if (num2 < 1)
          {
            for (int index = 0; index < 1 - num2; ++index)
              this.Insert(session, this.FCriterions.Count, "cad0002c-306c-11d8-b4e9-00304f19f545", this.CFHelper.DefaultAggFunction, CompareOperatorsHelper.ctDefaultFunction, (ArrayList) null, (ArrayList) null);
          }
          int num3 = 0;
          int Index1 = -1;
          List<VersionsRuleCriterion> versionsRuleCriterionList = new List<VersionsRuleCriterion>();
          for (int index = 0; index < count; ++index)
          {
            VersionsRuleCriterion fcriterion = this.FCriterions[index];
            if (fcriterion != null && fcriterion != null)
            {
              if (this.CFHelper.IsAggregate(fcriterion.CompareFunction))
              {
                ++num3;
                if (num3 == 1)
                  Index1 = index;
              }
              if (num3 > 1)
                versionsRuleCriterionList.Add(fcriterion);
            }
          }
          if (num3 > 0 && Index1 >= 0)
            this.Exchange(Index1, count - 1);
          if (versionsRuleCriterionList.Count > 0)
          {
            for (int index = 0; index < versionsRuleCriterionList.Count; ++index)
              this.Remove(versionsRuleCriterionList[index]);
          }
        }
        this.SyncAttributes();
        return flag;
      }

      /// <summary>
      /// Подсчитать и вернуть количество обычных и расширенных критериев подбора
      /// </summary>
      /// <param name="StandardCriterions"></param>
      /// <param name="AdvancedCriterions"></param>
      public void CriterionsCount(out int StandardCriterions, out int AdvancedCriterions)
      {
        StandardCriterions = 0;
        AdvancedCriterions = 0;
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        lock (this)
        {
          int count = this.FCriterions.Count;
          for (int index = 0; index < count; ++index)
          {
            VersionsRuleCriterion fcriterion = this.FCriterions[index];
            if (fcriterion != null)
            {
              if (this.CFHelper.IsAggregate(fcriterion.CompareFunction))
                ++AdvancedCriterions;
              else
                ++StandardCriterions;
            }
          }
        }
      }

      /// <summary>
      /// Проверяет, есть ли в критериях правила подбора версий хотя бы одно значение для сравненения
      /// с типом данных "Переменная" (значение, которое укажет пользователь)
      /// </summary>
      /// <returns>true, если найдена хотя бы одна переменная</returns>
      public bool HasVariableValues()
      {
        int count = this.Criterions.Count;
        if (count == 0)
          return false;
        lock (this)
        {
          for (int index = 0; index < count; ++index)
          {
            VersionsRuleCriterion criterion = this.Criterions[index];
            if (criterion != null && criterion.HasVariableValues())
              return true;
          }
        }
        return false;
      }

      /// <summary>
      /// Проверить, подходит ли указанное правило для хранения значений переменных текущего правила
      /// Сверяет полное соответствие всех критериев подбора версий у текущего правила и указанного.
      /// </summary>
      /// <param name="Value">Экземпляр правила подбора версий, в котором предположительно хранятся варианты значений для сравнения</param>
      /// <returns></returns>
      public bool IsCompatible(VersionsRule Value)
      {
        if (Value == null || Value.Criterions.Count != this.Criterions.Count || Value.RuleObjectID != this.RuleObjectID || Value.RuleObjectGuid != this.RuleObjectGuid || Value.RuleObjectType != this.RuleObjectType)
          return false;
        if (this.Criterions.Count > 0)
        {
          for (int index = 0; index < this.Criterions.Count; ++index)
          {
            VersionsRuleCriterion criterion1 = this.Criterions[index];
            VersionsRuleCriterion criterion2 = Value.Criterions[index];
            if (criterion1 == null || !criterion1.IsCompatible(criterion2))
              return false;
          }
        }
        return true;
      }

      /// <summary>Проверить, является ли указанное правило пустым</summary>
      /// <returns>true, если в правиле не заданы критерии подбора версий</returns>
      public bool Empty() => this.FCriterions == null || this.FCriterions.Count == 0;

      /// <summary>
      /// Конвертировать все значения с типом "Переменная" в тип "Константа"
      /// </summary>
      /// <param name="session">Сессия, из которой будет вытягиваться информация</param>
      public void ConvertVarsToConsts(IUserSession session)
      {
        this.SyncAttributes();
        int count = this.Criterions.Count;
        if (count == 0)
          return;
        lock (this)
        {
          for (int index = 0; index < count; ++index)
            this.Criterions[index]?.ConvertVarsToConsts(session);
        }
      }

      /// <summary>
      /// Вернуть значение атрибута для его отображения на экране	в "читабельном виде"
      /// </summary>
      /// <param name="Mode">Что должно входить в результат:
      /// 0 - только заголовок объекта,
      /// 1 - только названия критериев подбора,
      /// 2 - переменные значения для сравнения</param>
      /// <returns>Значение атрибута в виде строки для отображения</returns>
      public object GetDisplayValue(int Mode)
      {
        string ruleObjectCaption = this.RuleObjectCaption;
        if (Mode == 0)
          return (object) ruleObjectCaption;
        if (this.FCriterions.Count == 0 || Mode < 2)
          return (object) ruleObjectCaption;
        StringBuilder stringBuilder = new StringBuilder(this.FCriterions.Count);
        lock (this)
        {
          for (int index = 0; index < this.FCriterions.Count; ++index)
          {
            VersionsRuleCriterion fcriterion = this.FCriterions[index];
            if (Mode == 2)
              stringBuilder.Append(fcriterion.GetDisplayValue(2));
            else
              stringBuilder.Append($"[{fcriterion.MainAttribute.Attribute.AttrName}] ");
          }
        }
        return (object) stringBuilder.ToString().Trim();
      }

      /// <summary>Преобразование объекта в пользовательское правило</summary>
      /// <param name="session">Сессия</param>
      public void ConvertToStandardRule(IUserSession session)
      {
        this.Valid(session);
        this.FCurrentRuleType = VersionsRuleType.vrtStandardRule;
      }

      /// <summary>
      /// Преобразование объекта в системное правило [один основной критерий сравнения][один дополнительный критерий]
      /// </summary>
      /// <param name="Caption">Заголовок правила</param>
      /// <param name="AttrGuid">Guid атрибута основного критерия сравнения</param>
      /// <param name="AttrValue">Значение для сравнения для этого атрибута</param>
      /// <param name="session">Сессия</param>
      public void ConvertToSystemRule(
        IUserSession session,
        string Caption,
        string AttrGuid,
        object AttrValue)
      {
        if (session == null)
          return;
        this.Clear();
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        lock (this)
        {
          VersionsRuleCriterion versionsRuleCriterion = this.Insert(session, 0, AttrGuid, "EQUALS", CompareOperatorsHelper.ctDefaultFunction, (ArrayList) null, (ArrayList) null);
          versionsRuleCriterion.ComparableValues.Clear();
          if (AttrValue != null)
            versionsRuleCriterion.Add(session, "CONST", AttrValue.ToString());
          this.Insert(session, this.FCriterions.Count, "cad0002c-306c-11d8-b4e9-00304f19f545", this.CFHelper.DefaultRuleAggFunction, CompareOperatorsHelper.ctDefaultFunction, (ArrayList) null, (ArrayList) null);
          this.FCurrentRuleType = VersionsRuleType.vrtSystemRule;
          this.RuleObjectCaption = Caption;
          this.SyncAttributes();
        }
      }

      /// <summary>
      /// Проверить, корректно ли правило для подбора составов на указанную дату
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <returns>true - правило является корректным</returns>
      public bool IsCorrectActualDateRule(IUserSession session)
      {
        this.Valid(session);
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        int num1 = -4;
        int num2 = -9;
        bool flag = false;
        lock (this)
        {
          for (int index = 0; index < this.FCriterions.Count; ++index)
          {
            VersionsRuleCriterion criterion = this.Criterions[index];
            if (criterion != null)
            {
              flag = !this.CFHelper.IsAggregate(criterion.CompareFunction) && (criterion.MainAttribute.Attribute.AttrID == num1 || criterion.MainAttribute.Attribute.AttrID == num2);
              if (flag)
                break;
            }
          }
        }
        return flag;
      }

      /// <summary>
      /// Преобразовать правило в правило для подбора составов на указанную дату
      /// </summary>
      /// <param name="session">Сессия</param>
      public void ConvertToActualDateRule(IUserSession session)
      {
        this.Valid(session);
        this.FCurrentRuleType = VersionsRuleType.vrtStandardRule;
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        int num1 = -4;
        int num2 = -9;
        lock (this)
        {
          bool flag = false;
          for (int index = 0; index < this.FCriterions.Count; ++index)
          {
            VersionsRuleCriterion criterion = this.Criterions[index];
            if (criterion != null)
            {
              flag = !this.CFHelper.IsAggregate(criterion.CompareFunction) && (criterion.MainAttribute.Attribute.AttrID == num1 || criterion.MainAttribute.Attribute.AttrID == num2);
              if (flag)
                break;
            }
          }
          if (!flag)
            this.FCriterions.Insert(0, new VersionsRuleCriterion(session, "cad00030-306c-11d8-b4e9-00304f19f545", "EQUALS", CompareOperatorsHelper.ctDefaultFunction, new ArrayList()
            {
              (object) "VARIABLE"
            }, new ArrayList() { (object) 0 }));
          this.FCurrentRuleType = VersionsRuleType.vrtStandardRule;
          this.SyncAttributes();
        }
      }

      /// <summary>
      /// Преобразование объекта в правило "Все версии объектов"
      /// </summary>
      /// <param name="session">Сессия</param>
      public void ConvertToAllVersionsRule(IUserSession session)
      {
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        this.Clear();
        lock (this)
        {
          this.EditingRule = false;
          this.Insert(session, 0, "cad0002c-306c-11d8-b4e9-00304f19f545", "NOTNULL", "NOP", (ArrayList) null, (ArrayList) null);
          this.FCurrentRuleType = VersionsRuleType.vrtAllVersionsRule;
          this.RuleObjectCaption = VersionsRule.captionAllVersions;
          this.RuleObjectGuid = "cad001e3-306c-11d8-b4e9-00304f19f545";
          this.SyncAttributes();
        }
      }

      /// <summary>
      /// Преобразование объекта в правило "Все версии объектов с учётом конкретизации"
      /// </summary>
      /// <param name="session">Сессия</param>
      public void ConvertToAllConcreteVersionsRule(IUserSession session)
      {
        this.ConvertToAllVersionsRule(session);
        lock (this)
        {
          this.FCurrentRuleType = VersionsRuleType.vrtSystemRule;
          this.RuleObjectCaption = VersionsRule.captionAllConcreteVersions;
          this.RuleObjectGuid = "cad005ac-306c-11d8-b4e9-00304f19f5455";
          this.SyncAttributes();
        }
      }

      /// <summary>
      /// Преобразование объекта в правило "Последние версии объектов"
      /// </summary>
      /// <param name="session">Сессия</param>
      public void ConvertToLatestVersionsRule(IUserSession session)
      {
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        this.Clear();
        lock (this)
        {
          this.EditingRule = false;
          this.Insert(session, 0, "cad0002c-306c-11d8-b4e9-00304f19f545", "NOTNULL", "NOP", (ArrayList) null, (ArrayList) null);
          this.Insert(session, 1, "cad0002c-306c-11d8-b4e9-00304f19f545", "MAX", "NOP", (ArrayList) null, (ArrayList) null);
          IDBObject dbObject = session.GetObject(new Guid("cad0069c-306c-11d8-b4e9-00304f19f545"), false);
          this.RuleObjectID = dbObject != null ? dbObject.ObjectID : 0L;
          this.FCurrentRuleType = VersionsRuleType.vrtLatestVersionsRule;
          this.RuleObjectCaption = VersionsRule.captionLatestVersions;
          this.RuleObjectGuid = "cad0069c-306c-11d8-b4e9-00304f19f545";
          this.SyncAttributes();
        }
      }

      /// <summary>
      /// Преобразование объекта в правило "Подбор базовых версий"
      /// </summary>
      /// <param name="session">Сессия</param>
      public void ConvertToBaseVersions(IUserSession session)
      {
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        this.Clear();
        lock (this)
        {
          this.EditingRule = true;
          this.Insert(session, 0, "cad014d3-306c-11d8-b4e9-00304f19f545", "EQUALS", "OR", new ArrayList()
          {
            (object) "CONST"
          }, new ArrayList() { (object) 1 });
          this.Insert(session, 1, "cad00029-306c-11d8-b4e9-00304f19f545", "MAX", "NOP", (ArrayList) null, (ArrayList) null);
          IDBObject dbObject = session.GetObject(new Guid("cad00601-306c-11d8-b4e9-00304f19f545"), false);
          this.RuleObjectID = dbObject != null ? dbObject.ObjectID : 0L;
          this.FCurrentRuleType = VersionsRuleType.vrtSystemRule;
          this.RuleObjectCaption = VersionsRule.captionBaseVersions;
          this.RuleObjectGuid = "cad00601-306c-11d8-b4e9-00304f19f545";
          this.SyncAttributes();
        }
      }

      /// <summary>
      /// Преобразование объекта в правило "Последовательное проведение изменений"
      /// </summary>
      /// <param name="session">Сессия</param>
      public void ConvertToSequentialModifications(IUserSession session)
      {
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        this.Clear();
        lock (this)
        {
          this.EditingRule = true;
          ArrayList CompareTypes = new ArrayList();
          CompareTypes.Add((object) "CONST");
          ArrayList CompareValues = new ArrayList();
          CompareValues.Add((object) new Guid("cad00013-306c-11d8-b4e9-00304f19f545"));
          this.Add(session, "cad00030-306c-11d8-b4e9-00304f19f545", "EQUALS", "OR", CompareTypes, CompareValues);
          CompareValues.Clear();
          CompareValues.Add((object) new Guid("cad003be-306c-11d8-b4e9-00304f19f545"));
          this.Add(session, "cad00030-306c-11d8-b4e9-00304f19f545", "EQUALS", "NOP", CompareTypes, CompareValues);
          this.Add(session, "cad014d3-306c-11d8-b4e9-00304f19f545", "BASEVERSION", "NOP", (ArrayList) null, (ArrayList) null);
          IDBObject dbObject = session.GetObject(new Guid("cad00602-306c-11d8-b4e9-00304f19f545"), false);
          this.RuleObjectID = dbObject != null ? dbObject.ObjectID : 0L;
          this.FCurrentRuleType = VersionsRuleType.vrtSystemRule;
          this.RuleObjectCaption = VersionsRule.captionSequentialModifications;
          this.RuleObjectGuid = "cad00602-306c-11d8-b4e9-00304f19f545";
          this.SyncAttributes();
        }
      }

      /// <summary>Очистить все внутренние поля экземпляру класса</summary>
      public void Clear()
      {
        this.FCriterions.Clear();
        this.FActualDate = DateTime.MinValue;
        this.RuleObjectCaption = "";
        this.RuleObjectID = 0L;
        this.RuleObjectGuid = "";
        this.RuleObjectType = 0;
        this.RuleObjectModified = DateTime.MinValue;
        this.SyncAttributes();
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is VersionsRule versionsRule))
          return;
        lock (this)
        {
          this.FCurrentRuleType = versionsRule.CurrentRuleType;
          this.RuleObjectCaption = versionsRule.RuleObjectCaption;
          this.RuleObjectGuid = versionsRule.RuleObjectGuid;
          this.RuleObjectID = versionsRule.RuleObjectID;
          this.RuleObjectModified = versionsRule.RuleObjectModified;
          this.RuleObjectType = versionsRule.RuleObjectType;
          this.ActualDate = versionsRule.ActualDate;
          this.UsingProhibited = versionsRule.UsingProhibited;
          this.FCriterions.Clear();
          int count = versionsRule.FCriterions.Count;
          if (count > 0)
          {
            lock (this)
            {
              lock (versionsRule)
              {
                for (int index = 0; index < count; ++index)
                {
                  VersionsRuleCriterion fcriterion = versionsRule.FCriterions[index];
                  if (fcriterion != null)
                    this.FCriterions.Add(fcriterion.Clone() as VersionsRuleCriterion);
                }
              }
            }
          }
        }
        this.SyncAttributes();
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone() => (object) new VersionsRule((object) this);

      /// <summary>Вернуть ссылку на интерфейс IEnumerator</summary>
      /// <returns>Ссылка на интерфейс IEnumerator</returns>
      public IEnumerator GetEnumerator() => (IEnumerator) this;

      /// <summary>Сбросить указатель позиции в списке на начало списка</summary>
      public void Reset() => this._pos = 0;

      /// <summary>Вернуть текущий элемент в списке объектов</summary>
      public object Current
      {
        get
        {
          lock (this)
          {
            if (this.FCriterions.Count > 0)
            {
              if (this._pos >= 0)
              {
                if (this._pos < this.FCriterions.Count)
                  return (object) this.FCriterions[this._pos];
              }
            }
          }
          return (object) null;
        }
      }

      /// <summary>Перейти к следующему элементу в списке объектов</summary>
      /// <returns></returns>
      public bool MoveNext()
      {
        lock (this)
        {
          if (this._pos >= this.FCriterions.Count - 1 || this.FCriterions.Count <= 0)
            return false;
          ++this._pos;
          return true;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию списка значений атрибутов с теми атрибутами, которые есть в списке критериев подбора
      /// </summary>
      public void SyncAttributes()
      {
        lock (this)
        {
          if (this.FRuleAttributes == null)
            this.FRuleAttributes = new Dictionary<int, MyAttributeElement>(2);
          this.FRuleAttributes.Clear();
          int count1 = this.Criterions.Count;
          if (count1 == 0)
            return;
          for (int index1 = 0; index1 < count1; ++index1)
          {
            VersionsRuleCriterion criterion = this.Criterions[index1];
            if (criterion != null)
            {
              if ((this.FRuleAttributes.ContainsKey(criterion.MainAttribute.Attribute.AttrID) ? this.FRuleAttributes[criterion.MainAttribute.Attribute.AttrID] : (MyAttributeElement) null) == null)
              {
                MyAttributeElement attributeElement = new MyAttributeElement(criterion.MainAttribute.Attribute.AttrID, criterion.MainAttribute.Attribute.AttrGUID, (object) null, criterion.MainAttribute.Attribute.AttrName, (object) null);
                this.FRuleAttributes.Add(criterion.MainAttribute.Attribute.AttrID, attributeElement);
              }
              int count2 = criterion.ComparableValues.Count;
              if (count2 != 0)
              {
                for (int index2 = 0; index2 < count2; ++index2)
                {
                  ComparableValue comparableValue = criterion.ComparableValues[index2];
                  if (comparableValue != null && !(comparableValue.ValueType != "ATTRIBUTE") && comparableValue.Attribute.AttrID != 0 && (this.FRuleAttributes.ContainsKey(comparableValue.Attribute.AttrID) ? this.FRuleAttributes[comparableValue.Attribute.AttrID] : (MyAttributeElement) null) == null)
                  {
                    MyAttributeElement attributeElement = new MyAttributeElement(comparableValue.Attribute.AttrID, comparableValue.Attribute.AttrGUID, (object) null, comparableValue.Attribute.AttrName, (object) null);
                    this.FRuleAttributes.Add(comparableValue.Attribute.AttrID, attributeElement);
                  }
                }
              }
            }
          }
        }
      }

      /// <summary>
      /// Метод возвращает значение в виде объекта на основании исходных данных CompValue.Attribute.AttrType
      /// </summary>
      /// <param name="VersionElement">Фильтруемая версия объекта</param>
      /// <param name="value">Значение для сравнения, которое надо обработать. Оно может содержать константу, переменную и значение атрибута</param>
      /// <param name="dataType">Тип данных значения для сравнения</param>
      /// <returns>Значение для сравнения в виде объекта. Тип данных хранится в поле CompValue.Attribute.AttrType.</returns>
      private object GetComparableValue(_Object @object, ComparableValue value)
      {
        if (value.ValueType != "ATTRIBUTE")
          return value.Value;
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(value.Attribute.AttrGUID));
        return @object.Attributes.GetAttributeValue(attributeTypeId);
      }

      /// <summary>
      /// Метод возвращает значение в виде объекта на основании исходных данных CompValue.Attribute.AttrType
      /// </summary>
      /// <param name="VersionElement">Фильтруемая версия объекта</param>
      /// <param name="CompValue">Значение для сравнения, которое надо обработать. Оно может содержать константу, переменную и значение атрибута</param>
      /// <param name="CompValueType">Тип данных значения для сравнения</param>
      /// <returns>Значение для сравнения в виде объекта. Тип данных хранится в поле CompValue.Attribute.AttrType.</returns>
      private object GetComparableValue(
        ref MyVersionElement VersionElement,
        ref ComparableValue CompValue,
        out FieldTypes CompValueType)
      {
        CompValueType = FieldTypes.ftUnknown;
        if (CompValue == null || CompValue.ValueType == "ATTRIBUTE" && (VersionElement == null || VersionElement.Tag == null))
          return (object) null;
        CompValueType = CompValue.Attribute.AttrType;
        if (CompValue.ValueType != "ATTRIBUTE")
          return CompValue.Value;
        if (CompValue.Attribute.AttrID == 0)
          return (object) null;
        string columnName = Convert.ToString(CompValue.Attribute.AttrGUID);
        DataRow tag = VersionElement.Tag as DataRow;
        if (tag.Table.Columns.IndexOf(columnName) < 0)
          return (object) null;
        try
        {
          return tag[columnName];
        }
        catch
        {
          return (object) null;
        }
      }

      /// <summary>Записать в протокол запись</summary>
      /// <param name="log">Протокол</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="state">Статус подобранной версии</param>
      /// <returns>Запись из протокола или null</returns>
      private FiltrateVersionsLogEntry WriteToLog(
        FiltrateVersionsLog log,
        int relTypeID,
        long prjLinkID,
        long objectID,
        ObjectFiltrationState state)
      {
        if (log == null || relTypeID == -1 || prjLinkID == 0L || objectID == 0L)
          return (FiltrateVersionsLogEntry) null;
        FiltrateVersionsLogEntry entry = log[relTypeID, prjLinkID, objectID] ?? new FiltrateVersionsLogEntry();
        entry.PrjLinkID = prjLinkID;
        entry.ObjectID = objectID;
        entry.State = state;
        log.Add(relTypeID, entry);
        return entry;
      }

      /// <summary>Записать в протокол запись</summary>
      /// <param name="log">Протокол</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="state">Статус подобранной версии</param>
      /// <param name="weight">"Вес", с которым подобралась или была отбракована указанная версия</param>
      /// <returns>Запись из протокола или null</returns>
      private FiltrateVersionsLogEntry WriteToLog(
        FiltrateVersionsLog log,
        int relTypeID,
        long prjLinkID,
        long objectID,
        ObjectFiltrationState state,
        int weight)
      {
        if (log == null || relTypeID == -1 || prjLinkID == 0L || objectID == 0L)
          return (FiltrateVersionsLogEntry) null;
        FiltrateVersionsLogEntry entry = log[relTypeID, prjLinkID, objectID] ?? new FiltrateVersionsLogEntry();
        entry.PrjLinkID = prjLinkID;
        entry.ObjectID = objectID;
        entry.State = state;
        entry.Weight = weight;
        log.Add(relTypeID, entry);
        return entry;
      }

      /// <summary>Записать в протокол запись</summary>
      /// <param name="log">Протокол</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="state">Статус подобранной версии</param>
      /// <param name="weight">"Вес", с которым подобралась или была отбракована указанная версия</param>
      /// <param name="mainAttribute">Идентификатор атрибута, по значению которого была подобрана данная версия по
      /// основным критериям подбора версий.
      /// Значение Intermech.Consts.UnknownAttributeId означает, что версия не была
      /// подобрана по основным критериям подбора версий</param>
      /// <param name="criterion">Номер основного критерия, по которому была подобрана данная версия.
      /// Значение -1 означает, что версия не была подобрана по основным критериям
      /// подбора версий</param>
      /// <returns>Запись из протокола или null</returns>
      private FiltrateVersionsLogEntry WriteToLog(
        FiltrateVersionsLog log,
        int relTypeID,
        long prjLinkID,
        long objectID,
        ObjectFiltrationState state,
        int weight,
        int mainAttribute,
        int criterion)
      {
        if (log == null || relTypeID == -1 || prjLinkID == 0L || objectID == 0L)
          return (FiltrateVersionsLogEntry) null;
        FiltrateVersionsLogEntry entry = log[relTypeID, prjLinkID, objectID] ?? new FiltrateVersionsLogEntry();
        entry.PrjLinkID = prjLinkID;
        entry.ObjectID = objectID;
        entry.State = state;
        entry.Weight = weight;
        entry.MainAttribute = mainAttribute;
        entry.Criterion = criterion;
        log.Add(relTypeID, entry);
        return entry;
      }

      /// <summary>
      /// Проверить соответствие объекта основным критериям подбора
      /// </summary>
      /// <param name="userSession">Сессия</param>
      /// <param name="relationObject">Объект</param>
      /// <returns>Результат проверки</returns>
      public bool CheckVersionByCriterions(IUserSession userSession, _Object @object)
      {
        if (userSession == null)
          throw new ArgumentNullException(nameof (userSession));
        if (@object == null)
          throw new ArgumentNullException("@object");
        if (this.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
          return true;
        IMSObjectType objectType = MetaDataHelper.GetObjectType(@object.TypeID);
        if (objectType != null && objectType.VersionsMode == ObjectVersionModes.SingleVersion)
          return true;
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        int num = 0;
        bool flag1 = true;
        string str = "NOP";
        if (this.FCriterions == null)
          return false;
        VersionsRuleCriterion[] array;
        lock (this.FCriterions)
        {
          if (this.FCriterions == null)
            return false;
          array = this.FCriterions.ToArray();
        }
        foreach (VersionsRuleCriterion versionsRuleCriterion in array)
        {
          if (versionsRuleCriterion != null && !this.CFHelper.IsAggregate(versionsRuleCriterion.CompareFunction) && versionsRuleCriterion.MainAttribute.Attribute.AttrType != FieldTypes.ftUnknown)
          {
            ++num;
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(versionsRuleCriterion.MainAttribute.Attribute.AttrGUID));
            object attributeValue = @object.Attributes.GetAttributeValue(attributeTypeId);
            if (this.RuleObjectGuid == "cad00601-306c-11d8-b4e9-00304f19f545")
              attributeValue = (object) attributeValue.ToString();
            object obj1 = attributeValue;
            FieldTypes type1_1 = versionsRuleCriterion.MainAttribute.Attribute.AttrType;
            if (this.RuleObjectGuid == "cad00601-306c-11d8-b4e9-00304f19f545")
              type1_1 = FieldTypes.ftString;
            FieldTypes type1_2 = type1_1;
            bool flag2 = false;
            if (versionsRuleCriterion.MainAttribute.Attribute.AttrGUID == "cad0002b-306c-11d8-b4e9-00304f19f545")
            {
              int lifecycleStepId = @object.LifecycleStepID;
              if (lifecycleStepId != -1)
              {
                IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(lifecycleStepId);
                if (lcStep != null)
                {
                  attributeValue = (object) lcStep.Guid.ToString();
                  type1_1 = FieldTypes.ftString;
                  flag2 = true;
                }
              }
            }
            else if (versionsRuleCriterion.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545")
            {
              int lifecycleLevelId = @object.LifecycleLevelID;
              if (lifecycleLevelId != 0)
              {
                IMSLifeCycleLevel lcLevel = MetaDataHelper.GetLCLevel(lifecycleLevelId);
                if (lcLevel != null)
                {
                  attributeValue = (object) lcLevel.Guid.ToString();
                  type1_1 = FieldTypes.ftString;
                  flag2 = true;
                }
              }
            }
            object obj2 = (object) null;
            object obj3 = (object) null;
            FieldTypes type2_1 = FieldTypes.ftUnknown;
            FieldTypes type2_2 = FieldTypes.ftUnknown;
            bool flag3 = false;
            int count = versionsRuleCriterion.ComparableValues.Count;
            if (count >= versionsRuleCriterion.CFunc.MinComparableValues(versionsRuleCriterion.CompareFunction) && count <= versionsRuleCriterion.CFunc.MaxComparableValues(versionsRuleCriterion.CompareFunction))
            {
              switch (versionsRuleCriterion.CompareFunction)
              {
                case "CONTAINS":
                  ComparableValue comparableValue1 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType1 = comparableValue1.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue1);
                  flag3 = CompareFunctionsHelper.ObjValues_CONTAINS(ref attributeValue, ref obj2);
                  if (!flag3 & flag2)
                  {
                    flag3 = CompareFunctionsHelper.ObjValues_CONTAINS(ref obj1, ref obj2);
                    break;
                  }
                  break;
                case "EQUALS":
                  ComparableValue comparableValue2 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType2 = comparableValue2.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue2);
                  flag3 = CompareFunctionsHelper.ObjValues_EQUALS(ref attributeValue, ref obj2, type1_1, attrType2);
                  if (!flag3 & flag2)
                  {
                    flag3 = CompareFunctionsHelper.ObjValues_EQUALS(ref obj1, ref obj2, type1_2, attrType2);
                    break;
                  }
                  break;
                case "EQUALS_GREATER":
                  ComparableValue comparableValue3 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType3 = comparableValue3.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue3);
                  CompareResult compareResult1 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj2, type1_1, attrType3);
                  if (((compareResult1 == CompareResult.More ? 0 : (compareResult1 != 0 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
                    compareResult1 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj2, type1_2, attrType3);
                  flag3 = compareResult1 == CompareResult.More || compareResult1 == CompareResult.Equal;
                  break;
                case "EQUALS_LESS":
                  ComparableValue comparableValue4 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType4 = comparableValue4.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue4);
                  CompareResult compareResult2 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj2, type1_1, attrType4);
                  if (((compareResult2 == CompareResult.Less ? 0 : (compareResult2 != 0 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
                    compareResult2 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj2, type1_2, attrType4);
                  flag3 = compareResult2 == CompareResult.Less || compareResult2 == CompareResult.Equal;
                  break;
                case "GREATER":
                  ComparableValue comparableValue5 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType5 = comparableValue5.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue5);
                  CompareResult compareResult3 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj2, type1_1, attrType5);
                  if (compareResult3 != CompareResult.More & flag2)
                    compareResult3 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj2, type1_2, attrType5);
                  flag3 = compareResult3 == CompareResult.More;
                  break;
                case "IN_BOUNDS":
                  ComparableValue comparableValue6 = versionsRuleCriterion.ComparableValues[0];
                  obj2 = this.GetComparableValue(@object, comparableValue6);
                  ComparableValue comparableValue7 = versionsRuleCriterion.ComparableValues[1];
                  obj3 = this.GetComparableValue(@object, comparableValue7);
                  CompareResult compareResult4 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj2, type1_1, type2_1);
                  CompareResult compareResult5 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj3, type1_1, type2_2);
                  if (((compareResult4 != CompareResult.More ? 1 : (compareResult5 != CompareResult.Less ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
                  {
                    compareResult4 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj2, type1_2, type2_1);
                    compareResult5 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_2, type2_2);
                  }
                  flag3 = compareResult4 == CompareResult.More && compareResult5 == CompareResult.Less;
                  break;
                case "IN_BOUNDS_INC":
                  ComparableValue comparableValue8 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType6 = comparableValue8.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue8);
                  ComparableValue comparableValue9 = versionsRuleCriterion.ComparableValues[1];
                  FieldTypes attrType7 = comparableValue9.Attribute.AttrType;
                  obj3 = this.GetComparableValue(@object, comparableValue9);
                  CompareResult compareResult6 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj2, type1_1, attrType6);
                  CompareResult compareResult7 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj3, type1_1, attrType7);
                  if (((compareResult6 == CompareResult.More || compareResult6 == CompareResult.Equal ? (compareResult7 == CompareResult.Less ? 0 : (compareResult7 != 0 ? 1 : 0)) : 1) & (flag2 ? 1 : 0)) != 0)
                  {
                    compareResult6 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj2, type1_2, attrType6);
                    compareResult7 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_2, attrType7);
                  }
                  flag3 = (compareResult6 == CompareResult.More || compareResult6 == CompareResult.Equal) && (compareResult7 == CompareResult.Less || compareResult7 == CompareResult.Equal);
                  break;
                case "IN_LIST":
                  for (int index = 0; index < versionsRuleCriterion.ComparableValues.Count; ++index)
                  {
                    ComparableValue comparableValue10 = versionsRuleCriterion.ComparableValues[index];
                    FieldTypes attrType8 = comparableValue10.Attribute.AttrType;
                    obj2 = this.GetComparableValue(@object, comparableValue10);
                    flag3 = CompareFunctionsHelper.ObjValues_EQUALS(ref attributeValue, ref obj2, type1_1, attrType8);
                    if (!flag3 & flag2)
                      flag3 = CompareFunctionsHelper.ObjValues_EQUALS(ref obj1, ref obj2, type1_2, attrType8);
                    if (flag3)
                      break;
                  }
                  break;
                case "LESS":
                  ComparableValue comparableValue11 = versionsRuleCriterion.ComparableValues[0];
                  FieldTypes attrType9 = comparableValue11.Attribute.AttrType;
                  obj2 = this.GetComparableValue(@object, comparableValue11);
                  CompareResult compareResult8 = CompareFunctionsHelper.ObjValues_COMPARE(ref attributeValue, ref obj2, type1_1, attrType9);
                  if (compareResult8 != CompareResult.Less & flag2)
                    compareResult8 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj2, type1_2, attrType9);
                  flag3 = compareResult8 == CompareResult.Less;
                  break;
                case "NOTNULL":
                  flag3 = attributeValue != DBNull.Value;
                  break;
                default:
                  flag3 = false;
                  break;
              }
            }
            else
              flag3 = false;
            if (versionsRuleCriterion.Negation)
              flag3 = !flag3;
            if (num == 1)
            {
              flag1 = flag3;
            }
            else
            {
              switch (str)
              {
                case "OR":
                  flag1 = flag3;
                  break;
                case "AND":
                  flag1 &= flag3;
                  break;
                default:
                  flag1 = flag3;
                  break;
              }
            }
            str = versionsRuleCriterion.BoolFunction;
            if (flag1 && versionsRuleCriterion.BoolFunction == "OR")
              break;
          }
        }
        return flag1;
      }

      /// <summary>
      /// Проверить соответствие указанной версии объекта основным критериям подбора версий
      /// </summary>
      /// <param name="session">Сессия, с которой будет работать фильтрация</param>
      /// <param name="VersionElement">Данные по фильтруемой версии объекта</param>
      public void CheckVersionByCriterions(IUserSession session, ref MyVersionElement VersionElement)
      {
        if (VersionElement == null || VersionElement.ID == 0L || VersionElement.Tag == null || session == null)
          return;
        if (this.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
        {
          VersionElement.State = ObjectFiltrationState.fsCorresponding;
        }
        else
        {
          DataRow tag = VersionElement.Tag as DataRow;
          DataTable table = tag.Table;
          FiltrateVersionsLog extendedProperty = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
          string columnName1 = "cad0002e-306c-11d8-b4e9-00304f19f545";
          string columnName2 = "cad00036-306c-11d8-b4e9-00304f19f545";
          int result1;
          if (!int.TryParse(Convert.ToString(tag[columnName1]), out result1))
            result1 = 0;
          IMSObjectType imsObjectType = (IMSObjectType) null;
          try
          {
            if (result1 != 0)
              imsObjectType = MetaDataHelper.GetObjectType(result1);
          }
          catch
          {
            imsObjectType = (IMSObjectType) null;
          }
          VersionElement.Weigth = 0;
          int result2 = -1;
          int columnIndex = tag.Table.Columns.IndexOf(columnName2);
          if (columnIndex >= 0 && !int.TryParse(Convert.ToString(tag[columnIndex]), out result2))
            result2 = -1;
          if (imsObjectType != null && imsObjectType.VersionsMode == ObjectVersionModes.SingleVersion)
          {
            VersionElement.State = ObjectFiltrationState.fsNonVersionable;
            this.WriteToLog(extendedProperty, result2, VersionElement.PrjLinkID, VersionElement.ID, VersionElement.State, VersionElement.Weigth, 0, 0);
          }
          else
          {
            if (this.CFHelper == null)
              this.CFHelper = new CompareFunctionsHelper();
            int num = 0;
            int mainAttribute = 0;
            int criterion1 = -1;
            for (int index1 = 0; index1 < this.Criterions.Count - 1; ++index1)
            {
              VersionsRuleCriterion criterion2 = this.Criterions[index1];
              if (criterion2 != null && !this.CFHelper.IsAggregate(criterion2.CompareFunction) && criterion2.MainAttribute.Attribute.AttrType != FieldTypes.ftUnknown)
              {
                mainAttribute = criterion2.MainAttribute.Attribute.AttrID;
                criterion1 = num;
                ++num;
                string columnName3 = Convert.ToString(criterion2.MainAttribute.Attribute.AttrGUID);
                object obj1 = tag[columnName3];
                if (this.RuleObjectGuid == "cad00601-306c-11d8-b4e9-00304f19f545")
                  obj1 = (object) obj1.ToString();
                object obj2 = obj1;
                FieldTypes type1_1 = criterion2.MainAttribute.Attribute.AttrType;
                if (this.RuleObjectGuid == "cad00601-306c-11d8-b4e9-00304f19f545")
                  type1_1 = FieldTypes.ftString;
                FieldTypes type1_2 = type1_1;
                bool flag1 = false;
                if (criterion2.MainAttribute.Attribute.AttrGUID == "cad0002b-306c-11d8-b4e9-00304f19f545")
                {
                  int int32Value = DataSetProcessor.GetInt32Value(obj1, -1);
                  if (int32Value != -1)
                  {
                    IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(int32Value);
                    if (lcStep != null)
                    {
                      obj1 = (object) lcStep.Guid.ToString();
                      type1_1 = FieldTypes.ftString;
                      flag1 = true;
                    }
                  }
                }
                if (criterion2.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545")
                {
                  int int32Value = DataSetProcessor.GetInt32Value(obj1, 0);
                  if (int32Value != 0)
                  {
                    IMSLifeCycleLevel lcLevel = MetaDataHelper.GetLCLevel(int32Value);
                    if (lcLevel != null)
                    {
                      obj1 = (object) lcLevel.Guid.ToString();
                      type1_1 = FieldTypes.ftString;
                      flag1 = true;
                    }
                  }
                }
                object obj3 = (object) null;
                FieldTypes CompValueType1 = FieldTypes.ftUnknown;
                FieldTypes CompValueType2 = FieldTypes.ftUnknown;
                bool flag2 = false;
                int count = criterion2.ComparableValues.Count;
                if (count >= criterion2.CFunc.MinComparableValues(criterion2.CompareFunction) && count <= criterion2.CFunc.MaxComparableValues(criterion2.CompareFunction))
                {
                  switch (criterion2.CompareFunction)
                  {
                    case "CONTAINS":
                      ComparableValue comparableValue1 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue1, out CompValueType1);
                      flag2 = CompareFunctionsHelper.ObjValues_CONTAINS(ref obj1, ref obj3);
                      if (!flag2 & flag1)
                      {
                        flag2 = CompareFunctionsHelper.ObjValues_CONTAINS(ref obj2, ref obj3);
                        break;
                      }
                      break;
                    case "EQUALS":
                      ComparableValue comparableValue2 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue2, out CompValueType1);
                      flag2 = CompareFunctionsHelper.ObjValues_EQUALS(ref obj1, ref obj3, type1_1, CompValueType1);
                      if (!flag2 & flag1)
                      {
                        flag2 = CompareFunctionsHelper.ObjValues_EQUALS(ref obj2, ref obj3, type1_2, CompValueType1);
                        break;
                      }
                      break;
                    case "EQUALS_GREATER":
                      ComparableValue comparableValue3 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue3, out CompValueType1);
                      CompareResult compareResult1 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_1, CompValueType1);
                      if (((compareResult1 == CompareResult.More ? 0 : (compareResult1 != 0 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
                        compareResult1 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref obj3, type1_2, CompValueType1);
                      flag2 = compareResult1 == CompareResult.More || compareResult1 == CompareResult.Equal;
                      break;
                    case "EQUALS_LESS":
                      ComparableValue comparableValue4 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue4, out CompValueType1);
                      CompareResult compareResult2 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_1, CompValueType1);
                      if (((compareResult2 == CompareResult.Less ? 0 : (compareResult2 != 0 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
                        compareResult2 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref obj3, type1_2, CompValueType1);
                      flag2 = compareResult2 == CompareResult.Less || compareResult2 == CompareResult.Equal;
                      break;
                    case "GREATER":
                      ComparableValue comparableValue5 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue5, out CompValueType1);
                      CompareResult compareResult3 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_1, CompValueType1);
                      if (compareResult3 != CompareResult.More & flag1)
                        compareResult3 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref obj3, type1_2, CompValueType1);
                      flag2 = compareResult3 == CompareResult.More;
                      break;
                    case "IN_BOUNDS":
                      ComparableValue comparableValue6 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue6, out CompValueType1);
                      ComparableValue comparableValue7 = criterion2.ComparableValues[1];
                      object comparableValue8 = this.GetComparableValue(ref VersionElement, ref comparableValue7, out CompValueType2);
                      CompareResult compareResult4 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_1, CompValueType1);
                      CompareResult compareResult5 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref comparableValue8, type1_1, CompValueType2);
                      if (((compareResult4 != CompareResult.More ? 1 : (compareResult5 != CompareResult.Less ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
                      {
                        compareResult4 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref obj3, type1_2, CompValueType1);
                        compareResult5 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref comparableValue8, type1_2, CompValueType2);
                      }
                      flag2 = compareResult4 == CompareResult.More && compareResult5 == CompareResult.Less;
                      break;
                    case "IN_BOUNDS_INC":
                      ComparableValue comparableValue9 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue9, out CompValueType1);
                      ComparableValue comparableValue10 = criterion2.ComparableValues[1];
                      object comparableValue11 = this.GetComparableValue(ref VersionElement, ref comparableValue10, out CompValueType2);
                      CompareResult compareResult6 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_1, CompValueType1);
                      CompareResult compareResult7 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref comparableValue11, type1_1, CompValueType2);
                      if (((compareResult6 == CompareResult.More || compareResult6 == CompareResult.Equal ? (compareResult7 == CompareResult.Less ? 0 : (compareResult7 != 0 ? 1 : 0)) : 1) & (flag1 ? 1 : 0)) != 0)
                      {
                        compareResult6 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref obj3, type1_2, CompValueType1);
                        compareResult7 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref comparableValue11, type1_2, CompValueType2);
                      }
                      flag2 = (compareResult6 == CompareResult.More || compareResult6 == CompareResult.Equal) && (compareResult7 == CompareResult.Less || compareResult7 == CompareResult.Equal);
                      break;
                    case "IN_LIST":
                      for (int index2 = 0; index2 < criterion2.ComparableValues.Count; ++index2)
                      {
                        ComparableValue comparableValue12 = criterion2.ComparableValues[index2];
                        obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue12, out CompValueType1);
                        flag2 = CompareFunctionsHelper.ObjValues_EQUALS(ref obj1, ref obj3, type1_1, CompValueType1);
                        if (!flag2 & flag1)
                          flag2 = CompareFunctionsHelper.ObjValues_EQUALS(ref obj2, ref obj3, type1_2, CompValueType1);
                        if (flag2)
                          break;
                      }
                      break;
                    case "LESS":
                      ComparableValue comparableValue13 = criterion2.ComparableValues[0];
                      obj3 = this.GetComparableValue(ref VersionElement, ref comparableValue13, out CompValueType1);
                      CompareResult compareResult8 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj1, ref obj3, type1_1, CompValueType1);
                      if (compareResult8 != CompareResult.Less & flag1)
                        compareResult8 = CompareFunctionsHelper.ObjValues_COMPARE(ref obj2, ref obj3, type1_2, CompValueType1);
                      flag2 = compareResult8 == CompareResult.Less;
                      break;
                    case "NOTNULL":
                      flag2 = obj1 != DBNull.Value;
                      break;
                    default:
                      flag2 = false;
                      break;
                  }
                }
                else
                  flag2 = false;
                if (criterion2.Negation)
                  flag2 = !flag2;
                if (num == 1)
                  VersionElement.BoolState = flag2;
                else if (VersionElement.BoolOp == "OR")
                  VersionElement.BoolState = flag2;
                else if (VersionElement.BoolOp == "AND")
                  VersionElement.BoolState &= flag2;
                else
                  VersionElement.BoolState = flag2;
                VersionElement.BoolOp = criterion2.BoolFunction;
                if (!VersionElement.BoolState || !(criterion2.BoolFunction == "OR"))
                  ++VersionElement.Weigth;
                else
                  break;
              }
            }
            VersionElement.State = !VersionElement.BoolState ? ObjectFiltrationState.fsFiltrationStopped : ObjectFiltrationState.fsCorresponding;
            this.WriteToLog(extendedProperty, VersionElement.RelTypeID, VersionElement.PrjLinkID, VersionElement.ID, VersionElement.State, VersionElement.Weigth, mainAttribute, criterion1);
          }
        }
      }

      /// <summary>
      /// Удалить из отсортированного списка версий версии, "вес" которых отличается от "веса" первой версии в списке
      /// </summary>
      /// <param name="Versions">Отсортированный по "весу" список версий</param>
      private static void VersionsWeigthCut(List<MyVersionElement> Versions)
      {
        if (Versions == null || Versions.Count <= 1)
          return;
        int weigth = Versions[0].Weigth;
        for (int index = Versions.Count - 1; index >= 1; --index)
        {
          MyVersionElement version = Versions[index];
          if (version.Weigth > weigth)
            Versions.Remove(version);
        }
      }

      /// <summary>
      /// Выполнить выбор одной из версий в списке по первому дополнительному критерию подбора правила
      /// </summary>
      /// <param name="objects">Список версий объекта типа MyVersionElement, по которому надо провести дополнительную фильтрацию</param>
      /// <returns>MyVersionElement наиболее подходящей версии или null</returns>
      public _Object SelectVersionAdv(IEnumerable<_Object> objects)
      {
        if (objects == null)
          throw new ArgumentNullException(nameof (objects));
        VersionsRuleCriterion advancedCriterion = this.GetAdvancedCriterion();
        if (advancedCriterion == null)
          return (_Object) null;
        int attributeTypeID = MetaDataHelper.GetAttributeTypeID(Guid.Parse(advancedCriterion.MainAttribute.Attribute.AttrGUID));
        if (advancedCriterion.CompareFunction == "BASEVERSION")
          return objects.Where<_Object>((System.Func<_Object, bool>) (o => o.IsBaseVersion)).FirstOrDefault<_Object>();
        if (advancedCriterion.CompareFunction == "MAX")
          return objects.OrderBy<_Object, object>((System.Func<_Object, object>) (o => o.Attributes.GetAttributeValue(attributeTypeID))).LastOrDefault<_Object>();
        if (advancedCriterion.CompareFunction == "MIN")
          return objects.OrderBy<_Object, object>((System.Func<_Object, object>) (o => o.Attributes.GetAttributeValue(attributeTypeID))).FirstOrDefault<_Object>();
        throw new NotSupportedException();
      }

      /// <summary>
      /// Выполнить выбор одной из версий в списке по первому дополнительному критерию подбора правила
      /// </summary>
      /// <param name="Versions">Список версий объекта типа MyVersionElement, по которому надо провести дополнительную фильтрацию</param>
      /// <param name="attrID">Идентификатор атрибута дополнительного критерия</param>
      /// <param name="criterion">Номер этого критерия</param>
      /// <returns>MyVersionElement наиболее подходящей версии или null</returns>
      public MyVersionElement SelectVersionAdv(
        ref List<MyVersionElement> Versions,
        out int attrID,
        out int criterion)
      {
        attrID = 0;
        criterion = -1;
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        if (Versions == null || Versions.Count == 0 || this.Criterions == null || this.Criterions.Count == 0)
          return (MyVersionElement) null;
        Versions.Sort((IComparer<MyVersionElement>) new VersionsRule.VersionsWeigthSort());
        VersionsRule.VersionsWeigthCut(Versions);
        if (Versions.Count == 1 && Versions[0].State == ObjectFiltrationState.fsCorresponding)
        {
          Versions[0].State = ObjectFiltrationState.fsCorrespondingSingle;
          return Versions[0];
        }
        VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
        MyVersionElement myVersionElement1 = (MyVersionElement) null;
        MyVersionElement myVersionElement2 = (MyVersionElement) null;
        string columnName1 = "cad014d3-306c-11d8-b4e9-00304f19f545";
        object obj1 = (object) string.Empty;
        object obj2 = (object) string.Empty;
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        for (int index = 0; index < this.Criterions.Count; ++index)
        {
          VersionsRuleCriterion criterion1 = this.Criterions[index];
          if (criterion1 != null && this.CFHelper.IsAggregate(criterion1.CompareFunction))
          {
            versionsRuleCriterion = criterion1;
            attrID = versionsRuleCriterion.MainAttribute.Attribute.AttrID;
            criterion = index;
            break;
          }
        }
        if (versionsRuleCriterion == null || versionsRuleCriterion.MainAttribute.Attribute.AttrID == 0)
          return (MyVersionElement) null;
        string columnName2 = Convert.ToString(versionsRuleCriterion.MainAttribute.Attribute.AttrGUID);
        bool flag = versionsRuleCriterion.CompareFunction == "BASEVERSION";
        for (int index = 0; index < Versions.Count; ++index)
        {
          MyVersionElement myVersionElement3 = Versions[index];
          DataRow tag = myVersionElement3.Tag as DataRow;
          object obj3 = tag[columnName2];
          if (flag)
          {
            if ((DataSetProcessor.GetInt64Value(tag, columnName1, 0L) & 1L) == 1L)
            {
              myVersionElement3.State = ObjectFiltrationState.fsCorrespondingSingle;
              return myVersionElement3;
            }
          }
          else if (index == 0)
          {
            myVersionElement1 = myVersionElement3;
            myVersionElement2 = myVersionElement1;
            obj1 = obj3;
            obj2 = obj3;
          }
          else
          {
            if (CompareFunctionsHelper.ObjValues_COMPARE(ref obj3, ref obj1, versionsRuleCriterion.MainAttribute.Attribute.AttrType, versionsRuleCriterion.MainAttribute.Attribute.AttrType) == CompareResult.Less)
            {
              myVersionElement1 = myVersionElement3;
              obj1 = obj3;
            }
            if (CompareFunctionsHelper.ObjValues_COMPARE(ref obj3, ref obj2, versionsRuleCriterion.MainAttribute.Attribute.AttrType, versionsRuleCriterion.MainAttribute.Attribute.AttrType) == CompareResult.More)
            {
              myVersionElement2 = myVersionElement3;
              obj2 = obj3;
            }
          }
        }
        return versionsRuleCriterion.CompareFunction == "MIN" ? myVersionElement1 : myVersionElement2;
      }

      /// <summary>
      /// Вернуть коллекцию описателей столбцов для запроса в базу у коллекции связей.
      /// Будет возвращено как минимум пять описателей - значения атрибутов
      /// "F_PRJLINK_ID", "F_ID", "F_OBJECT_ID", "F_VERSION_ID", "F_OBJECT_TYPE".
      /// Если указан ID атрибута "Идентификатор версии в составе", то добавить и его столбец
      /// </summary>
      /// <param name="CompositionVersionAttrID">ID атрибута "Идентификатор версии в составе". 0 означает отсутствие такого атрибута</param>
      /// <param name="pars">Параметры запроса в базу данных</param>
      /// <returns>Коллекция описателей столбцов для всех атрибутов, которые встречаются в правиле подбора версий</returns>
      public List<ColumnDescriptor> GetRuleAttrsColumns(
        int CompositionVersionAttrID,
        DBRecordSetParams pars)
      {
        this.SyncAttributes();
        List<ColumnDescriptor> ruleAttrsColumns = new List<ColumnDescriptor>(0);
        int num1 = 10;
        if (CompositionVersionAttrID != 0)
        {
          int num2 = num1 + 1;
        }
        lock (this)
        {
          if (this.FRuleAttributes.Count == 0)
          {
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            if (CompositionVersionAttrID != 0)
              ruleAttrsColumns.Add(new ColumnDescriptor((object) CompositionVersionAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            ruleAttrsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
            return ruleAttrsColumns;
          }
          IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.FRuleAttributes.GetEnumerator();
          enumerator.Reset();
          while (enumerator.MoveNext())
          {
            if (enumerator.Value is MyAttributeElement attributeElement && attributeElement.ID != 0 && attributeElement.ID != -20 && attributeElement.ID != -3 && attributeElement.ID != -2 && attributeElement.ID != -5 && attributeElement.ID != -7 && attributeElement.ID != -23 && attributeElement.ID != -21 && attributeElement.ID != -22 && attributeElement.ID != -16 && attributeElement.ID != -15 && attributeElement.ID != CompositionVersionAttrID)
            {
              ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) attributeElement.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
              ruleAttrsColumns.Add(columnDescriptor);
            }
          }
        }
        int index1 = 0;
        ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index1, columnDescriptor1);
        int index2 = index1 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index2, columnDescriptor1);
        int index3 = index2 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index3, columnDescriptor1);
        int index4 = index3 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index4, columnDescriptor1);
        int index5 = index4 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index5, columnDescriptor1);
        int index6 = index5 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index6, columnDescriptor1);
        int index7 = index6 + 1;
        if (CompositionVersionAttrID != 0)
        {
          columnDescriptor1 = new ColumnDescriptor((object) CompositionVersionAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
          ruleAttrsColumns.Insert(index7, columnDescriptor1);
          ++index7;
        }
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index7, columnDescriptor1);
        int index8 = index7 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index8, columnDescriptor1);
        int index9 = index8 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index9, columnDescriptor1);
        int index10 = index9 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        ruleAttrsColumns.Insert(index10, columnDescriptor1);
        int num3 = index10 + 1;
        return ruleAttrsColumns;
      }

      /// <summary>
      /// Вернуть коллекцию описателей столбцов для запроса в базу у коллекции объектов.
      /// Будет возвращено как минимум четыре описателя - значения атрибутов
      /// "F_ID", "F_OBJECT_ID", "F_VERSION_ID", "F_OBJECT_TYPE".
      /// Если указан ID атрибута "Идентификатор версии в составе", то добавить и его столбец
      /// </summary>
      /// <param name="CompositionVersionAttrID">ID атрибута "Идентификатор версии в составе". 0 означает отсутствие такого атрибута</param>
      /// <returns>Коллекция описателей столбцов для всех атрибутов, которые встречаются в правиле подбора версий</returns>
      public ColumnDescriptor[] GetRuleAttrsColumns4Obj(int CompositionVersionAttrID)
      {
        this.SyncAttributes();
        ArrayList arrayList = (ArrayList) null;
        int length = 6;
        if (CompositionVersionAttrID != 0)
          ++length;
        lock (this)
        {
          int count = this.FRuleAttributes.Count;
          if (count == 0)
          {
            int index1 = 0;
            ColumnDescriptor[] attrsColumns4Obj = new ColumnDescriptor[length];
            attrsColumns4Obj[index1] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
            int index2 = index1 + 1;
            attrsColumns4Obj[index2] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
            int index3 = index2 + 1;
            attrsColumns4Obj[index3] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
            int index4 = index3 + 1;
            attrsColumns4Obj[index4] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
            int index5 = index4 + 1;
            if (CompositionVersionAttrID != 0)
            {
              attrsColumns4Obj[index5] = new ColumnDescriptor((object) CompositionVersionAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
              ++index5;
            }
            attrsColumns4Obj[index5] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
            int index6 = index5 + 1;
            attrsColumns4Obj[index6] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
            int num = index6 + 1;
            return attrsColumns4Obj;
          }
          arrayList = new ArrayList(count);
          IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.FRuleAttributes.GetEnumerator();
          enumerator.Reset();
          while (enumerator.MoveNext())
          {
            if (enumerator.Value is MyAttributeElement attributeElement && attributeElement.ID != 0 && attributeElement.ID != -3 && attributeElement.ID != -2 && attributeElement.ID != -5 && attributeElement.ID != -7 && attributeElement.ID != -16 && attributeElement.ID != -15 && attributeElement.ID != CompositionVersionAttrID)
            {
              ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) attributeElement.ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
              arrayList.Add((object) columnDescriptor);
            }
          }
        }
        int index7 = 0;
        ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        arrayList.Insert(index7, (object) columnDescriptor1);
        int index8 = index7 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        arrayList.Insert(index8, (object) columnDescriptor1);
        int index9 = index8 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        arrayList.Insert(index9, (object) columnDescriptor1);
        int index10 = index9 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        arrayList.Insert(index10, (object) columnDescriptor1);
        int index11 = index10 + 1;
        if (CompositionVersionAttrID != 0)
        {
          columnDescriptor1 = new ColumnDescriptor((object) CompositionVersionAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
          arrayList.Insert(index11, (object) columnDescriptor1);
          ++index11;
        }
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        arrayList.Insert(index11, (object) columnDescriptor1);
        int index12 = index11 + 1;
        columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0);
        arrayList.Insert(index12, (object) columnDescriptor1);
        int num1 = index12 + 1;
        ColumnDescriptor[] attrsColumns4Obj1 = new ColumnDescriptor[arrayList.Count];
        arrayList.CopyTo((Array) attrsColumns4Obj1);
        return attrsColumns4Obj1;
      }

      /// <summary>
      /// Скопировать указанную строку с данными fromRow в указанную таблицу toTable, при необходимости подтвердить изменения
      /// </summary>
      /// <param name="fromRow">Исходная строка с данными</param>
      /// <param name="toTable">Таблица-назначение</param>
      /// <param name="acceptChanges">true, если необходимо подтвердить изменения в таблице</param>
      private static void CopyRow(DataRow fromRow, DataTable toTable, bool acceptChanges)
      {
        if (fromRow == null || toTable == null)
          return;
        DataRow row;
        if (fromRow.Table != null)
        {
          row = toTable.NewRow();
          for (int index = 0; index < toTable.Columns.Count; ++index)
            row[toTable.Columns[index].ColumnName] = fromRow[toTable.Columns[index].ColumnName];
        }
        else
          row = fromRow;
        lock (toTable)
        {
          toTable.Rows.Add(row);
          if (!acceptChanges)
            return;
          toTable.AcceptChanges();
        }
      }

      public bool CheckCriterions() => this.Criterions.Count >= 2;

      /// <summary>
      /// Проверить, требуется ли конкретизация для данной версии
      /// </summary>
      /// <param name="CompositionAttr">Атрибут "Идентификатор версии в составе" и его обязательность</param>
      /// <param name="version">Проверяемая версия объекта</param>
      /// <param name="objVersions">Все версии указанного объекта</param>
      /// <returns>true - для версии задано какое-то значение конкретизации</returns>
      private bool MustBeConcreteVersion(
        Tuple<long, RequiredModes> CompositionAttr,
        MyVersionElement version,
        Dictionary<long, MyVersionElement> objVersions)
      {
        if (version == null || CompositionAttr == null || CompositionAttr.Item1 == 0L || CompositionAttr.Item1 == -1L)
          return false;
        DataTable table1 = version.Tag is DataRow tag1 ? tag1.Table : (DataTable) null;
        if (CompositionAttr.Item2 != RequiredModes.Manual && table1.ExtendedProperties.ContainsKey((object) "Part_Object_ID"))
        {
          long int64 = Convert.ToInt64(table1.ExtendedProperties[(object) "Part_Object_ID"]);
          return int64 != -1L && Math.Abs(int64) > 0L;
        }
        bool flag1 = CompositionAttr.Item2 == RequiredModes.Manual;
        foreach (KeyValuePair<long, MyVersionElement> objVersion in objVersions)
        {
          if (flag1)
          {
            if (objVersion.Value.Tag is DataRow tag2)
            {
              DataTable table2 = tag2.Table;
            }
            bool flag2 = tag2["cad001c2-306c-11d8-b4e9-00304f19f545"] != null && tag2["cad001c2-306c-11d8-b4e9-00304f19f545"] != DBNull.Value;
            flag1 = flag1 && !flag2;
          }
          else
            break;
        }
        return !flag1 || tag1["cad001c2-306c-11d8-b4e9-00304f19f545"] != null && tag1["cad001c2-306c-11d8-b4e9-00304f19f545"] != DBNull.Value;
      }

      /// <summary>
      /// Определить статус версии объекта.
      /// Основное требование к таблице ObjVersions - её столбцы получены по описателям,
      /// возвращённым методом GetRuleAttrsColumns
      /// </summary>
      /// <param name="session">Сессия, которая будет использоваться при фильтрации</param>
      /// <param name="objectVersion">Проверяемая версия объекта</param>
      /// <param name="fID">Идентификатор проверяемого объекта</param>
      /// <param name="ObjVersions">Таблица, в которой хранится матрица значений атрибутов всех версий проверяемого объекта.
      /// Столбцы этой таблицы должны быть получены по описателям, возвращённым методом GetRuleAttrsColumns</param>
      /// <returns>Вернёт значение, указывающее, насколько полно соответствует правилу указанная версия объекта</returns>
      public ObjectFiltrationState GetObjectVersionState(
        IUserSession session,
        long objectVersion,
        long fID,
        ref DataTable ObjVersions)
      {
        ObjectFiltrationState objectVersionState1 = ObjectFiltrationState.fsInvalidRule;
        if (this.Criterions.Count < 2)
          return objectVersionState1;
        ObjectFiltrationState objectVersionState2 = ObjectFiltrationState.fsVersionNotFound;
        if (ObjVersions == null || ObjVersions.Rows.Count == 0)
          return objectVersionState2;
        string columnName1 = "cad00029-306c-11d8-b4e9-00304f19f545";
        string columnName2 = "cad00033-306c-11d8-b4e9-00304f19f545";
        string columnName3 = "cad00036-306c-11d8-b4e9-00304f19f545";
        string columnName4 = "cad014d3-306c-11d8-b4e9-00304f19f545";
        int num1 = ObjVersions.Columns.IndexOf(columnName2);
        int num2 = ObjVersions.Columns.IndexOf(columnName3);
        ObjectFiltrationState AState = ObjectFiltrationState.fsCorresponding;
        bool flag1 = this.GetAdvancedCriterion().CompareFunction == "BASEVERSION";
        long editingContextId = this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" ? session.EditingContextID : 0L;
        long userId = session.UserID;
        EditingContextsObjectContainer editingContextsObject = (this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" ? session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService : (IDBEditingContextsService) null)?.GetEditingContextsObject((object) session, editingContextId, false, true);
        bool flag2 = editingContextId != 0L && editingContextsObject != null;
        if (flag2)
        {
          long objectVersion1 = editingContextsObject.GetObjectVersion(fID);
          if (objectVersion1 != 0L && objectVersion1 != objectVersion)
            return ObjectFiltrationState.fsVersionConflictsWithContext;
        }
        int count = ObjVersions.Rows.Count;
        List<MyVersionElement> myVersionElementList1 = new List<MyVersionElement>();
        List<MyVersionElement> myVersionElementList2 = new List<MyVersionElement>();
        List<MyVersionElement> myVersionElementList3 = new List<MyVersionElement>();
        for (int index = 0; index < count; ++index)
        {
          MyVersionElement myVersionElement = new MyVersionElement(Convert.ToInt64(ObjVersions.Rows[index][columnName1]), 0, AState, true, "NOP", DataSetProcessor.GetInt64Value(ObjVersions.Rows[index], columnName4, 0L) == 1L, (object) ObjVersions.Rows[index], num1 >= 0 ? Convert.ToInt64(ObjVersions.Rows[index][columnName2]) : 0L, num2 >= 0 ? Convert.ToInt32(ObjVersions.Rows[index][columnName3]) : -1);
          myVersionElementList1.Add(myVersionElement);
          int num3 = myVersionElement.IsBase ? 1 : 0;
        }
        lock (this)
        {
          for (int index = 0; index < myVersionElementList1.Count; ++index)
          {
            MyVersionElement VersionElement = myVersionElementList1[index];
            if (this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" & flag2)
            {
              List<long> versionContextId = editingContextsObject.GetVersionContextID(VersionElement.ID);
              if (versionContextId != null && versionContextId.Contains(Math.Abs(editingContextId)))
              {
                ObjectFiltrationState objectFiltrationState = ObjectFiltrationState.fsVersionFromMainContext;
                VersionElement.State = objectFiltrationState;
                if (VersionElement.ID == objectVersion)
                  return VersionElement.State;
              }
              if (versionContextId != null && versionContextId.Count > 0)
              {
                ObjectFiltrationState objectFiltrationState = ObjectFiltrationState.fsVersionFromLinkedContext;
                VersionElement.State = objectFiltrationState;
                if (VersionElement.ID == objectVersion)
                  return VersionElement.State;
              }
            }
            this.CheckVersionByCriterions(session, ref VersionElement);
            if (VersionElement.State == ObjectFiltrationState.fsCorresponding)
              myVersionElementList2.Add(VersionElement);
            else
              myVersionElementList3.Add(VersionElement);
          }
          if (myVersionElementList2.Count == 1)
          {
            MyVersionElement myVersionElement = myVersionElementList2[0];
            if (myVersionElement.State != ObjectFiltrationState.fsVersionFromMainContext && myVersionElement.State != ObjectFiltrationState.fsVersionFromLinkedContext)
              myVersionElement.State = ObjectFiltrationState.fsCorrespondingSingle;
            if (myVersionElement.ID == objectVersion)
              return myVersionElement.State;
          }
          if (myVersionElementList2.Count > 0)
          {
            for (int index = 0; index < myVersionElementList2.Count; ++index)
            {
              if (myVersionElementList2[index].ID == objectVersion)
                return flag1 && myVersionElementList2[index].IsBase ? ObjectFiltrationState.fsCorrespondingSingle : ObjectFiltrationState.fsCorresponding;
            }
          }
          if (myVersionElementList3.Count > 0)
          {
            for (int index = 0; index < myVersionElementList3.Count; ++index)
            {
              if (myVersionElementList3[index].ID == objectVersion)
                return flag1 && myVersionElementList2[index].IsBase ? ObjectFiltrationState.fsVariance : ObjectFiltrationState.fsFiltrationStopped;
            }
          }
        }
        return ObjectFiltrationState.fsVersionNotFound;
      }

      /// <summary>
      /// Выполнить фильтрацию версий одного объекта на основании данных в указанной таблице.
      /// Основное требование к таблице ObjVersions - её столбцы получены по описателям,
      /// возвращённым методом GetRuleAttrsColumns
      /// </summary>
      /// <param name="session">Сессия, которая будет использоваться при фильтрации</param>
      /// <param name="CompositionAttr">ID атрибута "Конкретизация версии в составе" и его обязательность</param>
      /// <param name="ObjVersions">Таблица, в которой хранится матрица значений атрибутов всех версий проверяемого объекта.
      /// Столбцы этой таблицы должны быть получены по описателям, возвращённым методом GetRuleAttrsColumns</param>
      /// <param name="State">В этом параметре будет возвращено значение, указывающее, насколько полно соответствует правилу указанная версия объекта</param>
      /// <param name="services">Контейнер сервисов</param>
      /// <returns>Вернёт F_OBJECT_ID наиболее подходящей версии объекта, соответствующего текущему правилу подбора версий. Если вернёт 0, ни одна версия не найдена. См. State для определения причины возврата такого результата.</returns>
      public long FiltrateVersions(
        IUserSession session,
        Tuple<long, RequiredModes> CompositionAttr,
        ref DataTable ObjVersions,
        out ObjectFiltrationState State,
        IServiceProvider services)
      {
        State = ObjectFiltrationState.fsInvalidRule;
        if (this.Criterions.Count < 2)
          return 0;
        State = ObjectFiltrationState.fsVersionNotFound;
        if (ObjVersions == null || ObjVersions.Rows.Count == 0)
          return 0;
        FiltrateVersionsLog extendedProperty = ObjVersions.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? ObjVersions.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
        string columnName1 = "cad00029-306c-11d8-b4e9-00304f19f545";
        string columnName2 = "cad00033-306c-11d8-b4e9-00304f19f545";
        string columnName3 = "cad00036-306c-11d8-b4e9-00304f19f545";
        string columnName4 = "cad014d3-306c-11d8-b4e9-00304f19f545";
        int num1 = ObjVersions.Columns.IndexOf(columnName2);
        int num2 = ObjVersions.Columns.IndexOf(columnName3);
        State = ObjectFiltrationState.fsCorresponding;
        bool flag1 = this.GetAdvancedCriterion().CompareFunction == "BASEVERSION";
        IElementStatusesService service = services != null ? services.GetService(typeof (IElementStatusesService)) as IElementStatusesService : (IElementStatusesService) null;
        int num3 = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref ObjVersions) < 0 ? 0 : (service != null ? 1 : 0);
        long editingContextId = this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" ? session.EditingContextID : 0L;
        long userId = session.UserID;
        EditingContextsObjectContainer editingContextsObject = (this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" ? session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService : (IDBEditingContextsService) null)?.GetEditingContextsObject((object) session, editingContextId, false, true);
        bool flag2 = editingContextId != 0L && editingContextsObject != null;
        MyVersionElement myVersionElement1 = (MyVersionElement) null;
        int count = ObjVersions.Rows.Count;
        List<MyVersionElement> myVersionElementList = new List<MyVersionElement>();
        List<MyVersionElement> Versions1 = new List<MyVersionElement>();
        List<MyVersionElement> Versions2 = new List<MyVersionElement>();
        for (int index = 0; index < count; ++index)
        {
          MyVersionElement myVersionElement2 = new MyVersionElement(Convert.ToInt64(ObjVersions.Rows[index][columnName1]), 0, State, true, "NOP", DataSetProcessor.GetInt64Value(ObjVersions.Rows[index], columnName4, 0L) == 1L, (object) ObjVersions.Rows[index], num1 >= 0 ? Convert.ToInt64(ObjVersions.Rows[index][columnName2]) : 0L, num2 >= 0 ? Convert.ToInt32(ObjVersions.Rows[index][columnName3]) : -1);
          myVersionElementList.Add(myVersionElement2);
          if (myVersionElement2.IsBase)
            myVersionElement1 = myVersionElement2;
        }
        lock (this)
        {
          for (int index = 0; index < myVersionElementList.Count; ++index)
          {
            MyVersionElement VersionElement = myVersionElementList[index];
            if (this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" & flag2)
            {
              List<long> versionContextId = editingContextsObject.GetVersionContextID(VersionElement.ID);
              if (versionContextId != null && versionContextId.Contains(Math.Abs(editingContextId)))
              {
                State = ObjectFiltrationState.fsVersionFromMainContext;
                VersionElement.State = State;
                if (VersionElement.Tag is DataRow tag)
                {
                  DataTable table = tag.Table;
                  extendedProperty = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
                }
                this.WriteToLog(extendedProperty, VersionElement.RelTypeID, VersionElement.PrjLinkID, VersionElement.ID, State, VersionElement.Weigth, 0, 0);
                return VersionElement.ID;
              }
              if (versionContextId != null && versionContextId.Count > 0)
              {
                State = ObjectFiltrationState.fsVersionFromLinkedContext;
                VersionElement.State = State;
                if (VersionElement.Tag is DataRow tag)
                {
                  DataTable table = tag.Table;
                  extendedProperty = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
                }
                this.WriteToLog(extendedProperty, VersionElement.RelTypeID, VersionElement.PrjLinkID, VersionElement.ID, State, VersionElement.Weigth, 0, 0);
                return VersionElement.ID;
              }
            }
            this.CheckVersionByCriterions(session, ref VersionElement);
            if (VersionElement.State == ObjectFiltrationState.fsCorresponding)
              Versions1.Add(VersionElement);
            else
              Versions2.Add(VersionElement);
          }
          if (Versions1.Count == 1)
          {
            MyVersionElement myVersionElement3 = Versions1[0];
            if (myVersionElement3.State != ObjectFiltrationState.fsVersionFromMainContext && myVersionElement3.State != ObjectFiltrationState.fsVersionFromLinkedContext)
              myVersionElement3.State = ObjectFiltrationState.fsCorrespondingSingle;
            if (myVersionElement3.Tag is DataRow tag)
            {
              DataTable table = tag.Table;
              extendedProperty = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
            }
            this.WriteToLog(extendedProperty, myVersionElement3.RelTypeID, myVersionElement3.PrjLinkID, myVersionElement3.ID, myVersionElement3.State, myVersionElement3.Weigth);
            return myVersionElement3.ID;
          }
          int attrID = 0;
          int criterion = -1;
          if (Versions1.Count > 0)
          {
            MyVersionElement myVersionElement4 = this.SelectVersionAdv(ref Versions1, out attrID, out criterion);
            if (myVersionElement4 != null)
            {
              if (myVersionElement4.State != ObjectFiltrationState.fsCorrespondingSingle)
                myVersionElement4.State = ObjectFiltrationState.fsCorresponding;
              if (myVersionElement4.Tag is DataRow tag)
              {
                DataTable table = tag.Table;
                extendedProperty = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
              }
              if (attrID != 0)
                this.WriteToLog(extendedProperty, myVersionElement4.RelTypeID, myVersionElement4.PrjLinkID, myVersionElement4.ID, myVersionElement4.State, myVersionElement4.Weigth, attrID, -1);
              else
                this.WriteToLog(extendedProperty, myVersionElement4.RelTypeID, myVersionElement4.PrjLinkID, myVersionElement4.ID, myVersionElement4.State, myVersionElement4.Weigth);
              return myVersionElement4.ID;
            }
            if (flag1 && Versions1.Count > 0)
            {
              if (this.EditingRule || myVersionElement1 == null)
                throw new AmbiguousVersionsException(string.Format(LocalizationHolder.rm.GetString("Interfaces_619") + LocalizationHolder.rm.GetString("Interfaces_620") + LocalizationHolder.rm.GetString("Interfaces_621") + LocalizationHolder.rm.GetString("Interfaces_622"), (object) Versions1[0].ID.ToString()));
              myVersionElement1.State = ObjectFiltrationState.fsVariance;
              return myVersionElement1.ID;
            }
          }
          if (Versions2.Count > 0)
          {
            MyVersionElement myVersionElement5 = this.SelectVersionAdv(ref Versions2, out attrID, out criterion);
            if (myVersionElement5 != null)
            {
              State = ObjectFiltrationState.fsVariance;
              if (myVersionElement5.Tag is DataRow tag)
              {
                DataTable table = tag.Table;
                extendedProperty = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
              }
              this.WriteToLog(extendedProperty, myVersionElement5.RelTypeID, myVersionElement5.PrjLinkID, myVersionElement5.ID, myVersionElement5.State, myVersionElement5.Weigth, attrID, -1);
              return myVersionElement5.ID;
            }
            if (flag1)
            {
              if (Versions2.Count > 0)
              {
                if (this.EditingRule || myVersionElement1 == null)
                  throw new AmbiguousVersionsException(string.Format(LocalizationHolder.rm.GetString("Interfaces_619") + LocalizationHolder.rm.GetString("Interfaces_620") + LocalizationHolder.rm.GetString("Interfaces_621") + LocalizationHolder.rm.GetString("Interfaces_622"), (object) Versions2[0].ToString()));
                myVersionElement1.State = ObjectFiltrationState.fsVariance;
                return myVersionElement1.ID;
              }
            }
          }
        }
        State = ObjectFiltrationState.fsVersionNotFound;
        return 0;
      }

      /// <summary>
      /// Выполнить фильтрацию версий одного объекта на основании данных в указанной коллекции.
      /// </summary>
      /// <param name="session">Сессия, с которой будет работать фильтрация</param>
      /// <param name="CompositionAttr">ID атрибута "Конкретизация версии в составе" и его обязательность</param>
      /// <param name="ObjVersions">Коллекция, в которой хранится список версий объектов типа MyVersionElement.</param>
      /// <param name="services">Контейнер сервисов</param>
      /// <returns>Вернёт MyVersionElement наиболее подходящей версии объекта, соответствующего текущему правилу подбора версий. Если вернёт null, ни одна версия не найдена</returns>
      public MyVersionElement FiltrateVersions(
        IUserSession session,
        Tuple<long, RequiredModes> CompositionAttr,
        ref Dictionary<long, MyVersionElement> ObjVersions,
        IServiceProvider services)
      {
        if (ObjVersions == null || ObjVersions.Count == 0 || session == null)
          return (MyVersionElement) null;
        if (this.Criterions.Count < 2)
          return (MyVersionElement) null;
        int count = ObjVersions.Count;
        List<MyVersionElement> Versions1 = new List<MyVersionElement>();
        List<MyVersionElement> Versions2 = new List<MyVersionElement>();
        FiltrateVersionsLog log = (FiltrateVersionsLog) null;
        VersionsRuleCriterion advancedCriterion = this.GetAdvancedCriterion();
        int columnIndex1 = -2;
        SeriesDateSettings seriesDateSettings = (SeriesDateSettings) null;
        bool flag1 = advancedCriterion.CompareFunction == "BASEVERSION";
        IElementStatusesService service = services != null ? services.GetService(typeof (IElementStatusesService)) as IElementStatusesService : (IElementStatusesService) null;
        int columnIndex2 = -2;
        bool flag2 = false;
        long editingContextId = this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" ? session.EditingContextID : 0L;
        long userId = session.UserID;
        IDBEditingContextsService customService = this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" ? session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService : (IDBEditingContextsService) null;
        EditingContextsObjectContainer editingContextsObject = customService == null || editingContextId == 0L ? (EditingContextsObjectContainer) null : customService.GetEditingContextsObject((object) session, editingContextId, false, true);
        bool flag3 = editingContextId != 0L && editingContextsObject != null;
        Dictionary<long, bool> dictionary = new Dictionary<long, bool>();
        lock (this)
        {
          if (CompositionAttr != null && CompositionAttr.Item1 != 0L && CompositionAttr.Item1 != -1L)
          {
            long num1 = -1;
            foreach (KeyValuePair<long, MyVersionElement> keyValuePair in ObjVersions)
            {
              MyVersionElement myVersionElement = keyValuePair.Value;
              DataTable table = myVersionElement.Tag is DataRow tag ? tag.Table : (DataTable) null;
              log = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
              if (table.ExtendedProperties.ContainsKey((object) "Part_Object_ID"))
                num1 = Convert.ToInt64(table.ExtendedProperties[(object) "Part_Object_ID"]);
              long num2 = num1 == -1L || Math.Abs(num1) <= 0L ? Math.Abs(myVersionElement.ID) : Math.Abs(num1);
              if (tag["cad001c2-306c-11d8-b4e9-00304f19f545"] != null && tag["cad001c2-306c-11d8-b4e9-00304f19f545"] != DBNull.Value)
              {
                if (num2 == Math.Abs(Convert.ToInt64(tag["cad001c2-306c-11d8-b4e9-00304f19f545"])))
                {
                  myVersionElement.State = ObjectFiltrationState.fsCompositeVersion;
                  return myVersionElement;
                }
                dictionary[myVersionElement.ID] = true;
              }
            }
          }
          MyVersionElement version1 = (MyVersionElement) null;
          foreach (KeyValuePair<long, MyVersionElement> keyValuePair in ObjVersions)
          {
            MyVersionElement VersionElement = keyValuePair.Value;
            if (!dictionary.ContainsKey(VersionElement.ID) || dictionary.Count >= ObjVersions.Count)
            {
              if (VersionElement.IsBase)
                version1 = VersionElement;
              if (seriesDateSettings != null && seriesDateSettings.Enabled)
              {
                VersionElement.State = seriesDateSettings.CheckApplicabilities(session, DataSetProcessor.GetStringValue(VersionElement.Tag as DataRow, columnIndex1, string.Empty), VersionElement.ID);
                if (flag2)
                  service.SetElementStatuses16("{14BE37A7-84F7-44CB-97AA-15A713C703E0}", (VersionElement.Tag as DataRow)[columnIndex2] as byte[], Convert.ToInt16((object) VersionElement.State));
                if (VersionElement.State == ObjectFiltrationState.fsVersionBySeries || VersionElement.State == ObjectFiltrationState.fsVersionByDate)
                {
                  VersionElement.State = ObjectFiltrationState.fsNotRequired;
                  if (VersionElement.Tag is DataRow tag)
                  {
                    DataTable table = tag.Table;
                    log = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
                  }
                  this.WriteToLog(log, VersionElement.RelTypeID, VersionElement.PrjLinkID, VersionElement.ID, VersionElement.State, VersionElement.Weigth, 0, 0);
                  return VersionElement;
                }
              }
              if (this.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" & flag3)
              {
                List<long> versionContextId = editingContextsObject.GetVersionContextID(VersionElement.ID);
                if (versionContextId != null && versionContextId.Contains(Math.Abs(editingContextId)))
                {
                  VersionElement.State = !this.MustBeConcreteVersion(CompositionAttr, VersionElement, ObjVersions) ? ObjectFiltrationState.fsVersionFromMainContext : (dictionary.Count < ObjVersions.Count ? ObjectFiltrationState.fsVersionFromMainContext : ObjectFiltrationState.fsCompositeVersionNotFound);
                  if (VersionElement.Tag is DataRow tag)
                  {
                    DataTable table = tag.Table;
                    log = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
                  }
                  this.WriteToLog(log, VersionElement.RelTypeID, VersionElement.PrjLinkID, VersionElement.ID, VersionElement.State, VersionElement.Weigth, 0, 0);
                  return VersionElement;
                }
                if (versionContextId != null && versionContextId.Count > 0)
                {
                  VersionElement.State = !this.MustBeConcreteVersion(CompositionAttr, VersionElement, ObjVersions) ? ObjectFiltrationState.fsVersionFromLinkedContext : (dictionary.Count < ObjVersions.Count ? ObjectFiltrationState.fsVersionFromLinkedContext : ObjectFiltrationState.fsCompositeVersionNotFound);
                  if (VersionElement.Tag is DataRow tag)
                  {
                    DataTable table = tag.Table;
                    log = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
                  }
                  this.WriteToLog(log, VersionElement.RelTypeID, VersionElement.PrjLinkID, VersionElement.ID, VersionElement.State, VersionElement.Weigth, 0, 0);
                  return VersionElement;
                }
              }
              this.CheckVersionByCriterions(session, ref VersionElement);
              if (VersionElement.State == ObjectFiltrationState.fsCorresponding || VersionElement.State == ObjectFiltrationState.fsNotRequired || VersionElement.State == ObjectFiltrationState.fsNonVersionable || VersionElement.State == ObjectFiltrationState.fsCompositeVersion)
                Versions1.Add(VersionElement);
              else
                Versions2.Add(VersionElement);
            }
          }
          if (Versions1.Count == 1)
          {
            MyVersionElement version2 = Versions1[0];
            if (version2.State != ObjectFiltrationState.fsNotRequired && version2.State != ObjectFiltrationState.fsNonVersionable && version2.State != ObjectFiltrationState.fsCompositeVersion)
              version2.State = ObjectFiltrationState.fsCorrespondingSingle;
            version2.State = !this.MustBeConcreteVersion(CompositionAttr, version2, ObjVersions) ? version2.State : (dictionary.Count <= 0 || dictionary.Count >= ObjVersions.Count ? ObjectFiltrationState.fsCompositeVersionNotFound : version2.State);
            return version2;
          }
          int attrID = 0;
          int criterion = -1;
          if (Versions1.Count > 0)
          {
            MyVersionElement version3 = this.SelectVersionAdv(ref Versions1, out attrID, out criterion);
            if (version3 != null)
            {
              if (version3.Tag is DataRow tag)
              {
                DataTable table = tag.Table;
                log = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
              }
              if (version3.State != ObjectFiltrationState.fsCorrespondingSingle)
                version3.State = ObjectFiltrationState.fsCorresponding;
              version3.State = !this.MustBeConcreteVersion(CompositionAttr, version3, ObjVersions) ? version3.State : (dictionary.Count <= 0 || dictionary.Count >= ObjVersions.Count ? ObjectFiltrationState.fsCompositeVersionNotFound : version3.State);
              if (attrID != 0)
                this.WriteToLog(log, version3.RelTypeID, version3.PrjLinkID, version3.ID, version3.State, version3.Weigth, attrID, -1);
              else
                this.WriteToLog(log, version3.RelTypeID, version3.PrjLinkID, version3.ID, version3.State, version3.Weigth);
              return version3;
            }
            if (flag1 && Versions1.Count > 0)
            {
              if (this.EditingRule || version1 == null)
                throw new AmbiguousVersionsException(string.Format(LocalizationHolder.rm.GetString("Interfaces_619") + LocalizationHolder.rm.GetString("Interfaces_620") + LocalizationHolder.rm.GetString("Interfaces_621") + LocalizationHolder.rm.GetString("Interfaces_622"), (object) Versions1[0].ToString()));
              version1.State = !this.MustBeConcreteVersion(CompositionAttr, version1, ObjVersions) ? ObjectFiltrationState.fsVariance : (dictionary.Count < ObjVersions.Count ? ObjectFiltrationState.fsVariance : ObjectFiltrationState.fsCompositeVersionNotFound);
              return version1;
            }
          }
          if (Versions2.Count > 0)
          {
            MyVersionElement version4 = this.SelectVersionAdv(ref Versions2, out attrID, out criterion);
            if (version4 != null)
            {
              version4.State = !this.MustBeConcreteVersion(CompositionAttr, version4, ObjVersions) ? ObjectFiltrationState.fsVariance : (dictionary.Count <= 0 || dictionary.Count >= ObjVersions.Count ? ObjectFiltrationState.fsCompositeVersionNotFound : ObjectFiltrationState.fsVariance);
              if (version4.Tag is DataRow tag)
              {
                DataTable table = tag.Table;
                log = table.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? table.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
              }
              this.WriteToLog(log, version4.RelTypeID, version4.PrjLinkID, version4.ID, version4.State, version4.Weigth, attrID, -1);
              return version4;
            }
            if (flag1)
            {
              if (Versions2.Count > 0)
              {
                if (this.EditingRule || version1 == null)
                  throw new AmbiguousVersionsException(string.Format(LocalizationHolder.rm.GetString("Interfaces_619") + LocalizationHolder.rm.GetString("Interfaces_620") + LocalizationHolder.rm.GetString("Interfaces_621") + LocalizationHolder.rm.GetString("Interfaces_622"), (object) Versions2[0].ToString()));
                version1.State = !this.MustBeConcreteVersion(CompositionAttr, version1, ObjVersions) ? ObjectFiltrationState.fsVariance : (dictionary.Count <= 0 || dictionary.Count >= ObjVersions.Count ? ObjectFiltrationState.fsCompositeVersionNotFound : ObjectFiltrationState.fsVariance);
                return version1;
              }
            }
          }
        }
        return (MyVersionElement) null;
      }

      /// <summary>
      /// Выполнить фильтрацию версий всех объектов на основании данных в указанной таблице.
      /// Основное требование к таблице ObjVersions - её столбцы получены по описателям,
      /// возвращённым методом GetRuleAttrsColumns
      /// </summary>
      /// <param name="session">Сессия, с которой будет работать фильтрация</param>
      /// <param name="RelationType">Тип связи, для которой выполняется фильтрация</param>
      /// <param name="ObjVersions">Таблица, в которой хранится матрица значений атрибутов всех версий проверяемого объекта.
      /// Столбцы этой таблицы должны быть получены по описателям, возвращённым методом GetRuleAttrsColumns</param>
      /// <param name="FilteredRows">Коллекция DataRow наиболее подходящих версий объектов, соответствующих текущему правилу подбора версий.</param>
      /// <param name="FiltrationResultColumnIdx">Индекс столбца, в котором хранятся результаты фильтрации версий</param>
      /// <param name="services">Контейнер сервисов</param>
      /// <returns>true, если фильтрация была проведена успешно, false, если были какие-то ошибки</returns>
      public bool FiltrateAllVersions(
        IUserSession session,
        int RelationType,
        ref DataTable ObjVersions,
        out List<DataRow> FilteredRows,
        int FiltrationResultColumnIdx,
        IServiceProvider services)
      {
        FilteredRows = new List<DataRow>();
        if (ObjVersions == null || ObjVersions.Rows.Count == 0 || session == null)
          return false;
        IDBAttributeType4 dbAttributeType4 = (IDBAttributeType4) null;
        long num = 0;
        RequiredModes requiredModes = RequiredModes.Auto;
        if (RelationType != -1)
        {
          IDBRelationType dbRelationType;
          try
          {
            dbRelationType = session.GetRelationType(RelationType);
          }
          catch
          {
            dbRelationType = (IDBRelationType) null;
          }
          if (dbRelationType != null)
          {
            try
            {
              dbAttributeType4 = dbRelationType.Attributes.GetAttributeByID(session.IdentHelper.CompositionVersionID);
            }
            catch
            {
              dbAttributeType4 = (IDBAttributeType4) null;
            }
          }
        }
        if (dbAttributeType4 != null)
        {
          num = (long) dbAttributeType4.AttributeID;
          requiredModes = dbAttributeType4.Required;
        }
        int count = ObjVersions.Rows.Count;
        if (this.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule && num == 0L)
        {
          if (FilteredRows.Capacity - FilteredRows.Count < count)
            FilteredRows.Capacity = FilteredRows.Count + count;
          for (int index = 0; index < count; ++index)
            FilteredRows.Add(ObjVersions.Rows[index]);
          return true;
        }
        if (this.Criterions.Count < 2)
          return false;
        string columnName1 = "cad0002a-306c-11d8-b4e9-00304f19f545";
        string columnName2 = "cad00034-306c-11d8-b4e9-00304f19f545";
        string columnName3 = "cad00035-306c-11d8-b4e9-00304f19f545";
        string columnName4 = "cad00033-306c-11d8-b4e9-00304f19f545";
        string columnName5 = "cad00029-306c-11d8-b4e9-00304f19f545";
        string columnName6 = "cad00036-306c-11d8-b4e9-00304f19f545";
        string columnName7 = "cad014d3-306c-11d8-b4e9-00304f19f545";
        bool flag1 = FiltrationResultColumnIdx >= 0;
        int statusesColumnIndex = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref ObjVersions);
        bool flag2 = statusesColumnIndex >= 0;
        Dictionary<long, Dictionary<long, Dictionary<long, MyVersionElement>>> dictionary1 = new Dictionary<long, Dictionary<long, Dictionary<long, MyVersionElement>>>(0);
        int columnIndex1 = ObjVersions.Columns.IndexOf(columnName1);
        ObjVersions.Columns.IndexOf(columnName2);
        ObjVersions.Columns.IndexOf(columnName3);
        int columnIndex2 = ObjVersions.Columns.IndexOf(VersionsRule.RowStatusColumnName);
        int columnIndex3 = ObjVersions.Columns.IndexOf(columnName5);
        int columnIndex4 = ObjVersions.Columns.IndexOf(columnName7);
        int columnIndex5 = ObjVersions.Columns.IndexOf(columnName4);
        int columnIndex6 = ObjVersions.Columns.IndexOf(columnName6);
        bool flag3 = columnIndex5 < 0;
        long key1 = 0;
        if (flag3)
          dictionary1.Add(key1, new Dictionary<long, Dictionary<long, MyVersionElement>>());
        Dictionary<long, Dictionary<long, MyVersionElement>> dictionary2 = (Dictionary<long, Dictionary<long, MyVersionElement>>) null;
        Dictionary<long, MyVersionElement> ObjVersions1 = (Dictionary<long, MyVersionElement>) null;
        for (int index = 0; index < count; ++index)
        {
          long int64_1 = Convert.ToInt64(ObjVersions.Rows[index][columnIndex1]);
          long int64_2 = Convert.ToInt64(ObjVersions.Rows[index][columnIndex3]);
          long key2 = !flag3 ? Convert.ToInt64(ObjVersions.Rows[index][columnIndex5]) : key1;
          if (!dictionary1.TryGetValue(key2, out dictionary2))
            dictionary2 = (Dictionary<long, Dictionary<long, MyVersionElement>>) null;
          if (dictionary2 == null && !flag3)
          {
            dictionary2 = new Dictionary<long, Dictionary<long, MyVersionElement>>();
            dictionary1.Add(key2, dictionary2);
          }
          if (!dictionary2.TryGetValue(int64_1, out ObjVersions1))
          {
            ObjVersions1 = new Dictionary<long, MyVersionElement>();
            dictionary2.Add(int64_1, ObjVersions1);
          }
          MyVersionElement myVersionElement = new MyVersionElement(int64_2, 0, ObjectFiltrationState.fsCorresponding, true, "NOP", DataSetProcessor.GetInt64Value(ObjVersions.Rows[index], columnIndex4, 0L) == 1L, (object) ObjVersions.Rows[index], columnIndex5 >= 0 ? key2 : 0L, columnIndex6 >= 0 ? Convert.ToInt32(ObjVersions.Rows[index][columnIndex6]) : 0);
          ObjVersions1.Add(int64_2, myVersionElement);
        }
        IElementStatusesService service = services.GetService(typeof (IElementStatusesService)) as IElementStatusesService;
        lock (this)
        {
          FiltrateVersionsLog extendedProperty = ObjVersions.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? ObjVersions.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
          foreach (KeyValuePair<long, Dictionary<long, Dictionary<long, MyVersionElement>>> keyValuePair1 in dictionary1)
          {
            dictionary2 = keyValuePair1.Value;
            if (dictionary2 != null && dictionary2.Count != 0)
            {
              foreach (KeyValuePair<long, Dictionary<long, MyVersionElement>> keyValuePair2 in dictionary2)
              {
                ObjVersions1 = keyValuePair2.Value;
                if (ObjVersions1 != null && ObjVersions1.Count != 0)
                {
                  MyVersionElement myVersionElement = this.FiltrateVersions(session, new Tuple<long, RequiredModes>(num, requiredModes), ref ObjVersions1, services);
                  if (myVersionElement != null)
                  {
                    if (num != 0L && num != 0L && myVersionElement.State != ObjectFiltrationState.fsCompositeVersion && this.RuleObjectGuid == "cad005ac-306c-11d8-b4e9-00304f19f5455")
                    {
                      foreach (KeyValuePair<long, MyVersionElement> keyValuePair3 in ObjVersions1)
                      {
                        DataRow tag = keyValuePair3.Value.Tag as DataRow;
                        if (columnIndex2 >= 0)
                          tag[columnIndex2] = (object) true;
                        FilteredRows.Add(tag);
                      }
                    }
                    else if (myVersionElement.State != ObjectFiltrationState.fsCompositeVersionNotFound || VersionsRule.ShowInvalidConcreteVersions)
                    {
                      DataRow tag = myVersionElement.Tag as DataRow;
                      if (columnIndex2 >= 0)
                        tag[columnIndex2] = (object) true;
                      FilteredRows.Add(tag);
                      this.WriteToLog(extendedProperty, myVersionElement.RelTypeID, myVersionElement.PrjLinkID, myVersionElement.ID, myVersionElement.State, myVersionElement.Weigth);
                      if (flag1 && myVersionElement.Tag is DataRow)
                        (myVersionElement.Tag as DataRow)[FiltrationResultColumnIdx] = (object) Convert.ToInt32((object) myVersionElement.State);
                      if (flag2 && myVersionElement.Tag is DataRow && service != null)
                      {
                        short int16 = Convert.ToInt16((object) myVersionElement.State);
                        service.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", (myVersionElement.Tag as DataRow)[statusesColumnIndex] as byte[], int16);
                      }
                    }
                  }
                }
              }
            }
          }
        }
        return true;
      }

      /// <summary>
      /// Выполнить фильтрацию версий всех родительских объектов на основании данных в указанной таблице применяемостей.
      /// Основное требование к таблице ObjVersions - её столбцы получены по описателям,
      /// возвращённым методом GetRuleAttrsColumns
      /// </summary>
      /// <param name="session">Сессия, с которой будет работать фильтрация</param>
      /// <param name="RelationType">Тип связи, для которой выполняется фильтрация</param>
      /// <param name="ObjVersions">Таблица, в которой хранится матрица значений атрибутов всех версий проверяемого объекта.
      /// Столбцы этой таблицы должны быть получены по описателям, возвращённым методом GetRuleAttrsColumns</param>
      /// <param name="FilteredRows">Коллекция DataRow наиболее подходящих версий объектов, соответствующих текущему правилу подбора версий.</param>
      /// <param name="FiltrationResultColumnIdx">Индекс столбца, в котором хранятся результаты фильтрации версий</param>
      /// <param name="services">Контейнер сервисов</param>
      /// <returns>true, если фильтрация была проведена успешно, false, если были какие-то ошибки</returns>
      public bool FiltrateAllParentVersions(
        IUserSession session,
        int RelationType,
        ref DataTable ObjVersions,
        out List<DataRow> FilteredRows,
        int FiltrationResultColumnIdx,
        IServiceProvider services)
      {
        FilteredRows = new List<DataRow>();
        if (ObjVersions == null || ObjVersions.Rows.Count == 0 || session == null)
          return false;
        IDBAttributeType4 dbAttributeType4 = (IDBAttributeType4) null;
        long num1 = 0;
        RequiredModes requiredModes = RequiredModes.Auto;
        if (RelationType != -1)
        {
          IDBRelationType dbRelationType;
          try
          {
            dbRelationType = session.GetRelationType(RelationType);
          }
          catch
          {
            dbRelationType = (IDBRelationType) null;
          }
          if (dbRelationType != null)
          {
            try
            {
              dbAttributeType4 = dbRelationType.Attributes.GetAttributeByID(session.IdentHelper.CompositionVersionID);
            }
            catch
            {
              dbAttributeType4 = (IDBAttributeType4) null;
            }
          }
        }
        if (dbAttributeType4 != null)
        {
          num1 = (long) dbAttributeType4.AttributeID;
          requiredModes = dbAttributeType4.Required;
        }
        int count = ObjVersions.Rows.Count;
        if (this.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule && num1 == 0L)
        {
          for (int index = 0; index < count; ++index)
            FilteredRows.Add(ObjVersions.Rows[index]);
          return true;
        }
        if (this.Criterions.Count < 2)
          return false;
        string columnName1 = "cad0002a-306c-11d8-b4e9-00304f19f545";
        string columnName2 = "cad00034-306c-11d8-b4e9-00304f19f545";
        string columnName3 = "cad00035-306c-11d8-b4e9-00304f19f545";
        string columnName4 = "cad00033-306c-11d8-b4e9-00304f19f545";
        string columnName5 = "cad00036-306c-11d8-b4e9-00304f19f545";
        string columnName6 = "cad014d3-306c-11d8-b4e9-00304f19f545";
        bool flag1 = FiltrationResultColumnIdx >= 0;
        int statusesColumnIndex = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref ObjVersions);
        bool flag2 = statusesColumnIndex >= 0;
        Dictionary<long, Dictionary<long, MyVersionElement>> dictionary1 = new Dictionary<long, Dictionary<long, MyVersionElement>>(0);
        ObjVersions.Columns.IndexOf(columnName1);
        ObjVersions.Columns.IndexOf(columnName2);
        ObjVersions.Columns.IndexOf(columnName3);
        int num2 = ObjVersions.Columns.IndexOf(columnName4);
        int num3 = ObjVersions.Columns.IndexOf(columnName5);
        int columnIndex = ObjVersions.Columns.IndexOf(VersionsRule.RowStatusColumnName);
        Dictionary<long, List<DataRow>> dictionary2 = new Dictionary<long, List<DataRow>>();
        for (int index = 0; index < count; ++index)
        {
          long int64_1 = Convert.ToInt64(ObjVersions.Rows[index][columnName1]);
          long int64_2 = Convert.ToInt64(ObjVersions.Rows[index][columnName2]);
          Convert.ToInt64(ObjVersions.Rows[index][columnName3]);
          Dictionary<long, MyVersionElement> dictionary3 = dictionary1.ContainsKey(int64_1) ? dictionary1[int64_1] : (Dictionary<long, MyVersionElement>) null;
          if (dictionary3 == null)
          {
            dictionary3 = new Dictionary<long, MyVersionElement>();
            dictionary1.Add(int64_1, dictionary3);
          }
          if (!dictionary2.ContainsKey(int64_2))
            dictionary2.Add(int64_2, new List<DataRow>());
          dictionary2[int64_2].Add(ObjVersions.Rows[index]);
          if (!dictionary3.ContainsKey(int64_2))
            dictionary3.Add(int64_2, new MyVersionElement(int64_2, 0, ObjectFiltrationState.fsCorresponding, true, "NOP", DataSetProcessor.GetInt64Value(ObjVersions.Rows[index], columnName6, 0L) == 1L, (object) ObjVersions.Rows[index], num2 >= 0 ? Convert.ToInt64(ObjVersions.Rows[index][columnName4]) : 0L, num3 >= 0 ? Convert.ToInt32(ObjVersions.Rows[index][columnName5]) : 0));
        }
        IElementStatusesService service = services.GetService(typeof (IElementStatusesService)) as IElementStatusesService;
        lock (this)
        {
          FiltrateVersionsLog extendedProperty = ObjVersions.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) ? ObjVersions.ExtendedProperties[(object) FiltrateVersionsLog.Key] as FiltrateVersionsLog : (FiltrateVersionsLog) null;
          foreach (KeyValuePair<long, Dictionary<long, MyVersionElement>> keyValuePair1 in dictionary1)
          {
            Dictionary<long, MyVersionElement> ObjVersions1 = keyValuePair1.Value;
            if (ObjVersions1 != null && ObjVersions1.Count != 0)
            {
              MyVersionElement myVersionElement = this.FiltrateVersions(session, new Tuple<long, RequiredModes>(num1, requiredModes), ref ObjVersions1, services);
              if (myVersionElement != null)
              {
                if (num1 != 0L && num1 != 0L && myVersionElement.State != ObjectFiltrationState.fsCompositeVersion && this.RuleObjectGuid == "cad005ac-306c-11d8-b4e9-00304f19f5455")
                {
                  foreach (KeyValuePair<long, MyVersionElement> keyValuePair2 in ObjVersions1)
                  {
                    List<DataRow> dataRowList = dictionary2[keyValuePair2.Value.ID];
                    for (int index = 0; index < dataRowList.Count; ++index)
                    {
                      if (columnIndex >= 0)
                        dataRowList[index][columnIndex] = (object) true;
                      FilteredRows.Add(dataRowList[index]);
                    }
                  }
                }
                else if (myVersionElement.State != ObjectFiltrationState.fsCompositeVersionNotFound || VersionsRule.ShowInvalidConcreteVersions)
                {
                  List<DataRow> dataRowList = dictionary2[myVersionElement.ID];
                  for (int index = 0; index < dataRowList.Count; ++index)
                  {
                    if (columnIndex >= 0)
                      dataRowList[index][columnIndex] = (object) true;
                    FilteredRows.Add(dataRowList[index]);
                    if (flag1)
                      dataRowList[index][FiltrationResultColumnIdx] = (object) Convert.ToInt32((object) myVersionElement.State);
                    if (flag2 && service != null)
                    {
                      short int16 = Convert.ToInt16((object) myVersionElement.State);
                      service.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", dataRowList[index][statusesColumnIndex] as byte[], int16);
                    }
                  }
                  this.WriteToLog(extendedProperty, myVersionElement.RelTypeID, myVersionElement.PrjLinkID, myVersionElement.ID, myVersionElement.State, myVersionElement.Weigth);
                }
              }
            }
          }
        }
        return true;
      }

      /// <summary>
      /// Выполнить фильтрацию версий всех объектов на основании данных в указанной таблице.
      /// Основное требование к таблице ObjVersions - её столбцы получены по описателям,
      /// возвращённым методом GetRuleAttrsColumns
      /// </summary>
      /// <param name="session">Сессия, с которой будет работать фильтрация</param>
      /// <param name="RelationType">Тип связи, для которой выполняется фильтрация</param>
      /// <param name="ObjVersions">Таблица, в которой хранится матрица значений атрибутов всех версий проверяемого объекта.
      /// Столбцы этой таблицы должны быть получены по описателям, возвращённым методом GetRuleAttrsColumns.
      /// Данные в этой таблице будут модифицированы в процессе фильтрации</param>
      /// <param name="FiltrationResultColumnIdx">Индекс столбца, в котором хранятся результаты настройки фильтрации версий</param>
      /// <param name="services">Контейнер сервисов</param>
      /// <returns>true, если фильтрация была проведена успешно, false, если были какие-то ошибки</returns>
      public bool FiltrateAllVersions(
        IUserSession session,
        int RelationType,
        ref DataTable ObjVersions,
        int FiltrationResultColumnIdx,
        IServiceProvider services)
      {
        List<DataRow> FilteredRows;
        bool flag;
        switch (ObjVersions.ExtendedProperties.Contains((object) "SelectFunction") ? (SelectFunction) ObjVersions.ExtendedProperties[(object) "SelectFunction"] : SelectFunction.EntersIn)
        {
          case SelectFunction.EntersIn:
          case SelectFunction.EntersInVersion:
            flag = this.FiltrateAllParentVersions(session, RelationType, ref ObjVersions, out FilteredRows, FiltrationResultColumnIdx, services);
            break;
          default:
            flag = this.FiltrateAllVersions(session, RelationType, ref ObjVersions, out FilteredRows, FiltrationResultColumnIdx, services);
            break;
        }
        if (!flag || FilteredRows == null || ObjVersions.Rows.Count == 0)
          return flag;
        lock (ObjVersions)
        {
          int columnIndex = ObjVersions.Columns.IndexOf(VersionsRule.RowStatusColumnName);
          for (int index = ObjVersions.Rows.Count - 1; index >= 0; --index)
          {
            DataRow row = ObjVersions.Rows[index];
            object obj = (object) null;
            if (columnIndex >= 0)
              obj = row[columnIndex];
            if (obj != null && obj != DBNull.Value)
            {
              if (Convert.ToBoolean(obj))
                continue;
            }
            else if (FilteredRows.IndexOf(row) >= 0)
            {
              FilteredRows.Remove(row);
              continue;
            }
            ObjVersions.Rows.Remove(row);
          }
        }
        return true;
      }

      /// <summary>
      /// Метод отыскивает первый критерий, содержащий указанный атрибут в виде
      /// константы или значения пользователя, и возвращает его значение. В противном
      /// случае будет возвращено значение null
      /// </summary>
      /// <param name="attrID">Идентификатор атрибута</param>
      /// <returns>Значение атрибута или null</returns>
      public object GetAttributeValue(int attrID)
      {
        lock (this)
        {
          for (int index1 = 0; index1 < this.FCriterions.Count; ++index1)
          {
            VersionsRuleCriterion fcriterion = this.FCriterions[index1];
            if (!this.CFHelper.IsAggregate(fcriterion.CompareFunction) && fcriterion.MainAttribute.Attribute.AttrID == attrID && fcriterion.ComparableValues.Count != 0)
            {
              for (int index2 = 0; index2 < fcriterion.ComparableValues.Count; ++index2)
              {
                ComparableValue comparableValue = fcriterion.ComparableValues[index2];
                if (!(comparableValue.ValueType == "ATTRIBUTE") && !fcriterion.Negation && (fcriterion.CompareFunction == "EQUALS" || fcriterion.CompareFunction == "EQUALS_GREATER" || fcriterion.CompareFunction == "EQUALS_LESS" || fcriterion.CompareFunction == "IN_LIST"))
                  return comparableValue.Value;
              }
            }
          }
        }
        return (object) null;
      }

      /// <summary>
      /// Метод отыскивает первый критерий, содержащий указанный атрибут в виде
      /// константы или значения пользователя, и возвращает его значения (в критерии
      /// может быть несколько значений для сравнения для одного и того же атрибута).
      /// </summary>
      /// <param name="attrID">Идентификатор атрибута</param>
      /// <param name="ignoreDuplicates">Игнорировать дубликаты значений в списке</param>
      /// <returns>Список значений атрибута</returns>
      public List<object> GetAttributeValue(int attrID, bool ignoreDuplicates)
      {
        List<object> attributeValue = new List<object>();
        if (this.FRuleAttributes == null)
          this.FRuleAttributes = new Dictionary<int, MyAttributeElement>(2);
        this.CFHelper = new CompareFunctionsHelper();
        lock (this)
        {
          for (int index1 = 0; index1 < this.FCriterions.Count; ++index1)
          {
            VersionsRuleCriterion fcriterion = this.FCriterions[index1];
            if (!this.CFHelper.IsAggregate(fcriterion.CompareFunction) && fcriterion.MainAttribute.Attribute.AttrID == attrID && fcriterion.ComparableValues.Count != 0)
            {
              for (int index2 = 0; index2 < fcriterion.ComparableValues.Count; ++index2)
              {
                ComparableValue comparableValue = fcriterion.ComparableValues[index2];
                if (!(comparableValue.ValueType == "ATTRIBUTE") && !attributeValue.Contains(comparableValue.Value) | ignoreDuplicates && !fcriterion.Negation && (fcriterion.CompareFunction == "EQUALS" || fcriterion.CompareFunction == "EQUALS_GREATER" || fcriterion.CompareFunction == "EQUALS_LESS" || fcriterion.CompareFunction == "IN_LIST"))
                  attributeValue.Add(comparableValue.Value);
              }
            }
          }
        }
        return attributeValue;
      }

      /// <summary>
      /// Извлечь из правила список основных критериев подбора версий объектов
      /// </summary>
      /// <returns>Список основных критериев подбора версий объектов</returns>
      public List<VersionsRuleCriterion> GetMainCriterions()
      {
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        List<VersionsRuleCriterion> mainCriterions = new List<VersionsRuleCriterion>(this.Criterions.Count);
        lock (this)
        {
          for (int index = 0; index < this.Criterions.Count; ++index)
          {
            VersionsRuleCriterion criterion = this.Criterions[index];
            if (criterion != null && !this.CFHelper.IsAggregate(criterion.CompareFunction))
              mainCriterions.Add(criterion);
          }
        }
        return mainCriterions;
      }

      /// <summary>
      /// Получить у правила дополнительный критерий подбора версий объектов
      /// </summary>
      /// <returns>Дополнительный критерий подбора версий объектов</returns>
      public VersionsRuleCriterion GetAdvancedCriterion()
      {
        if (this.CFHelper == null)
          this.CFHelper = new CompareFunctionsHelper();
        lock (this)
        {
          if (this.Criterions.Count == 0)
            return (VersionsRuleCriterion) null;
          if (this.CFHelper.IsAggregate(this.Criterions[this.Criterions.Count - 1].CompareFunction))
            return this.Criterions[this.Criterions.Count - 1];
          for (int index = 0; index < this.Criterions.Count; ++index)
          {
            VersionsRuleCriterion criterion = this.Criterions[index];
            if (criterion != null && this.CFHelper.IsAggregate(criterion.CompareFunction))
              return criterion;
          }
        }
        return (VersionsRuleCriterion) null;
      }

      /// <summary>Сохранить на время значение ActualDate</summary>
      public void PushActualDate() => this.FActualDateBeforeSave = this.FActualDate;

      /// <summary>Восстановить значение ActualDate</summary>
      public void PopActualDate() => this.FActualDate = this.FActualDateBeforeSave;

      /// <summary>
      /// Класс для сортировки списка версий объектов по их "весу"
      /// </summary>
      private class VersionsWeigthSort : IComparer<MyVersionElement>
      {
        /// <summary>Сравнить две версии объекта по их "весу"</summary>
        /// <param name="x">Первая версия</param>
        /// <param name="y">Вторая версия</param>
        /// <returns>-1, 0, 1</returns>
        public int Compare(MyVersionElement x, MyVersionElement y)
        {
          return x == null || y == null ? 0 : x.Weigth.CompareTo(y.Weigth);
        }
      }
    }
}
