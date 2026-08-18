// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AttributeSortSchema
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Условие выдёргивания подстроки из некоторого параметра, а так же его сравнения с другими такими же "выдергиваниями" для сортировки записей</summary>
[Serializable]
public class AttributeSortSchema : IWriteReadXml, ICloneable, IComparer
{
  public Guid attributeGuid = Guid.Empty;
  private int _attributeID = -1;
  private Guid _schemeGuid = Guid.NewGuid();
  private string _attributeName = string.Empty;
  private FieldSource attrSrc = FieldSource.Object;
  private SubstringStartFinishType substringStartType = SubstringStartFinishType.FinishStart;
  private string startSubstring = ".";
  private int startPosition = 1;
  private SubstringStartFinishType substringEndType = SubstringStartFinishType.FinishStart;
  private string endSubstring = ".";
  private int endPosition = 1;
  private CompareType compareType = CompareType.Number;
  private SortOrder sortOrder = SortOrder.Ascending;
  private EmptyOrder emptyOrder = EmptyOrder.ToBegin;
  private string _fieldName;

  /// <summary>Конструктор</summary>
  public AttributeSortSchema()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  public AttributeSortSchema(AvsRowAttributeInfo attrInfo)
  {
    this._attributeID = attrInfo.AttributeId;
    this.attributeGuid = attrInfo.AttributeGuid;
    this._attributeName = attrInfo.Name;
    this.attrSrc = attrInfo.AttrSrc;
  }

  /// <summary>Guid, уникально идентифицирующий данное правило сортировки</summary>
  public Guid SchemeGuid
  {
    [DebuggerStepThrough] get => this._schemeGuid;
    set => this._schemeGuid = value;
  }

  /// <summary>Идентификатор атрибута, значение которого сортируется</summary>
  public int AttributeID
  {
    [DebuggerStepThrough] get => this._attributeID;
  }

  /// <summary>Установить данные из SpecRowAttributeInfo</summary>
  /// <param name="info"></param>
  public void SetInfo(AvsRowAttributeInfo info)
  {
    this._attributeID = info.AttributeId;
    this.attributeGuid = info.AttributeGuid;
    this._attributeName = info.Name;
    this.attrSrc = info.AttrSrc;
  }

  /// <summary>Назначить идентификатор атрибута</summary>
  /// <param name="iUserSession">Пользовательская сессия</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  public void SetAttributeID(IUserSession iUserSession, int attributeID)
  {
    this._attributeID = attributeID;
    IDBAttributeType attributeType = iUserSession.GetAttributeType(attributeID);
    if (attributeType != null)
    {
      this._attributeName = attributeType.Name;
      this.attributeGuid = attributeType.PropertiesStructure.AttributeGuid;
    }
    else
    {
      this._attributeName = string.Empty;
      this.attributeGuid = Guid.Empty;
    }
  }

  /// <summary>Источник данных поля записи AVS</summary>
  public FieldSource AttrSrc
  {
    [DebuggerStepThrough] get => this.attrSrc;
    set => this.attrSrc = value;
  }

  /// <summary>Атрибут связи</summary>
  public bool IsRelation
  {
    [DebuggerStepThrough] get => this.attrSrc == FieldSource.Relation;
  }

  /// <summary>Атрибут объекта</summary>
  public bool IsObject
  {
    [DebuggerStepThrough] get => this.attrSrc == FieldSource.Object;
  }

  /// <summary>Поле записи в документе</summary>
  public bool IsDocField
  {
    [DebuggerStepThrough] get => this.attrSrc == FieldSource.DocumentRowField;
  }

  /// <summary>Тип начала подстроки</summary>
  public SubstringStartFinishType SubstringStartType
  {
    [DebuggerStepThrough] get => this.substringStartType;
    set => this.substringStartType = value;
  }

  /// <summary>Символ, с которого начинается подстрока</summary>
  public string StartSubstring
  {
    [DebuggerStepThrough] get => this.startSubstring;
    set => this.startSubstring = value;
  }

  /// <summary>Позиция определяющая начало подстроки</summary>
  public int StartPosition
  {
    [DebuggerStepThrough] get => this.startPosition;
    set => this.startPosition = value;
  }

  /// <summary>Тип конца подстроки</summary>
  public SubstringStartFinishType SubstringEndType
  {
    [DebuggerStepThrough] get => this.substringEndType;
    set => this.substringEndType = value;
  }

  /// <summary>Символ, которым заканчивается подстрока</summary>
  public string EndSubstring
  {
    [DebuggerStepThrough] get => this.endSubstring;
    set => this.endSubstring = value;
  }

  /// <summary>Позиция определяющая конец подстроки</summary>
  public int EndPosition
  {
    [DebuggerStepThrough] get => this.endPosition;
    set => this.endPosition = value;
  }

  /// <summary>Тип сравнения Текст/Число</summary>
  public CompareType CompareType
  {
    [DebuggerStepThrough] get => this.compareType;
    set => this.compareType = value;
  }

  /// <summary>Направление сортировки По возрастанию/По убыванию</summary>
  public SortOrder SortOrder
  {
    [DebuggerStepThrough] get => this.sortOrder;
    set => this.sortOrder = value;
  }

  /// <summary>Расположение пустых строк</summary>
  public EmptyOrder EmptyOrder
  {
    [DebuggerStepThrough] get => this.emptyOrder;
    set => this.emptyOrder = value;
  }

  /// <summary>Наименование атрибута</summary>
  public string AttributeName
  {
    [DebuggerStepThrough] get => this._attributeName;
  }

  /// <summary>Наименование и источник атрибута</summary>
  public string AttributeNameAndSource
  {
    [DebuggerStepThrough] get
    {
      return $"{this._attributeName} {(this.attrSrc == FieldSource.Object ? "(объект)" : (this.attrSrc == FieldSource.Relation ? "(связь)" : "(графа)"))}";
    }
  }

  /// <summary>Где начинать вырезание подстроки для сравнения </summary>
  public string FromStr
  {
    [DebuggerStepThrough] get
    {
      switch (this.SubstringStartType)
      {
        case SubstringStartFinishType.FinishStart:
          return "начала параметра";
        case SubstringStartFinishType.FromNPosition:
          return $"буквы номер {this.StartPosition.ToString()}";
        case SubstringStartFinishType.FromNFoundSubstring:
          return $"символа '{this.StartSubstring}' номер {this.StartPosition.ToString()}";
        case SubstringStartFinishType.FromEndFoundNSubstring:
          return $"символа '{this.StartSubstring}' номер {this.StartPosition.ToString()} (с конца строки)";
        default:
          return "???";
      }
    }
  }

  /// <summary>Где завершать вырезание подстроки для сравнения </summary>
  public string ToStr
  {
    [DebuggerStepThrough] get
    {
      switch (this.SubstringEndType)
      {
        case SubstringStartFinishType.FinishStart:
          return "конца параметра";
        case SubstringStartFinishType.FromNPosition:
          return $"количества символов {this.EndPosition.ToString()}";
        case SubstringStartFinishType.FromNFoundSubstring:
          return $"символа '{this.EndSubstring}' номер {this.EndPosition.ToString()}";
        case SubstringStartFinishType.FromEndFoundNSubstring:
          return $"символа '{this.EndSubstring}' номер {this.EndPosition.ToString()} (с конца строки)";
        default:
          return "???";
      }
    }
  }

  /// <summary>Направление сортировки</summary>
  public string SortOrderStr
  {
    [DebuggerStepThrough] get
    {
      switch (this.SortOrder)
      {
        case SortOrder.Ascending:
          return "По возрастанию";
        case SortOrder.Descending:
          return "По убыванию";
        default:
          return "???";
      }
    }
  }

  /// <summary>Сравнивать как строки или как числа </summary>
  public string CompareTypeStr
  {
    [DebuggerStepThrough] get
    {
      switch (this.CompareType)
      {
        case CompareType.Text:
          return "Символьное";
        case CompareType.Number:
          return "Числовое";
        default:
          return "???";
      }
    }
  }

  /// <summary>Куда помещать пустые значения при сортировке </summary>
  public string EmptyValueStr
  {
    [DebuggerStepThrough] get
    {
      switch (this.EmptyOrder)
      {
        case EmptyOrder.ToBegin:
          return "В начало";
        case EmptyOrder.ToEnd:
          return "В конец";
        default:
          return "???";
      }
    }
  }

  /// <summary>Имя поля в таблицах базы данных</summary>
  public string FieldName
  {
    [DebuggerStepThrough] get => this._fieldName;
    set => this._fieldName = value;
  }

  /// <summary>Извлечь подстроку для сортировки</summary>
  /// <param name="str">Оригинальная строка</param>
  /// <returns>Подстрока</returns>
  public string ExtractSubstring(string str)
  {
    string substring = (string) null;
    try
    {
      if (str == null || str == "")
        return (string) null;
      int startIndex1 = -1;
      int length = 0;
      switch (this.substringStartType)
      {
        case SubstringStartFinishType.Unknow:
          return (string) null;
        case SubstringStartFinishType.FinishStart:
          startIndex1 = 0;
          break;
        case SubstringStartFinishType.FromNPosition:
          startIndex1 = this.startPosition - 1;
          break;
        case SubstringStartFinishType.FromNFoundSubstring:
          int startIndex2 = 0;
          int num1 = 0;
          int num2;
          while (true)
          {
            num2 = str.IndexOf(this.startSubstring, startIndex2);
            if (num2 >= 0)
            {
              ++num1;
              if (num1 != this.startPosition)
                startIndex2 = num2 + this.startSubstring.Length;
              else
                break;
            }
            else
              goto label_17;
          }
          startIndex1 = num2 + this.startSubstring.Length;
          break;
        case SubstringStartFinishType.FromEndFoundNSubstring:
          int lastIndex1 = str.Length - 1;
          int num3 = 0;
          int num4;
          while (true)
          {
            num4 = this.IndexOfFromEnd(str, this.startSubstring, lastIndex1);
            if (num4 >= 0)
            {
              ++num3;
              if (num3 != this.startPosition)
                lastIndex1 = num4 - 1;
              else
                break;
            }
            else
              goto label_17;
          }
          startIndex1 = num4 + this.startSubstring.Length;
          break;
      }
label_17:
      switch (this.substringEndType)
      {
        case SubstringStartFinishType.Unknow:
          return (string) null;
        case SubstringStartFinishType.FinishStart:
          length = str.Length - startIndex1;
          break;
        case SubstringStartFinishType.FromNPosition:
          length = this.endPosition;
          break;
        case SubstringStartFinishType.FromNFoundSubstring:
          int startIndex3 = 0;
          int num5 = 0;
          int num6;
          while (true)
          {
            num6 = str.IndexOf(this.endSubstring, startIndex3);
            if (num6 >= 0)
            {
              ++num5;
              if (num5 != this.endPosition)
                startIndex3 = num6 + this.endSubstring.Length;
              else
                break;
            }
            else
              goto label_31;
          }
          length = num6 - startIndex1;
          break;
        case SubstringStartFinishType.FromEndFoundNSubstring:
          int lastIndex2 = str.Length - 1;
          int num7 = 0;
          int num8;
          while (true)
          {
            num8 = this.IndexOfFromEnd(str, this.endSubstring, lastIndex2);
            if (num8 >= 0)
            {
              ++num7;
              if (num7 != this.endPosition)
                lastIndex2 = num8 - 1;
              else
                break;
            }
            else
              goto label_31;
          }
          length = num8 - startIndex1;
          break;
      }
label_31:
      if (startIndex1 >= 0)
      {
        if (length > 0)
        {
          if (startIndex1 + length <= str.Length)
            substring = str.Substring(startIndex1, length);
        }
      }
    }
    catch (Exception ex)
    {
    }
    return substring;
  }

  /// <summary>Индекс подстроки отсчитывая от конца строки</summary>
  /// <param name="str">Строка</param>
  /// <param name="subStr">Подстрока</param>
  /// <param name="lastIndex">Индекс от которого начинать поиск</param>
  private int IndexOfFromEnd(string str, string subStr, int lastIndex)
  {
    if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(subStr))
      return -1;
    int index1 = subStr.Length - 1;
    if (lastIndex >= str.Length)
      lastIndex = str.Length - 1;
    int num = -1;
    for (int index2 = lastIndex; index2 >= 0; --index2)
    {
      if ((int) str[index2] == (int) subStr[index1])
      {
        --index2;
        int index3;
        for (index3 = index1 - 1; index2 >= 0 && index3 >= 0 && (int) str[index2] == (int) subStr[index3]; --index2)
          --index3;
        if (index3 == -1)
        {
          num = index2 + 1;
          break;
        }
      }
    }
    return num;
  }

  /// <summary>Получить информацию об атрибуте</summary>
  /// <returns></returns>
  public AvsRowAttributeInfo GetAttrInfo()
  {
    return new AvsRowAttributeInfo(this.attrSrc, this.attributeGuid, this._attributeID, this._attributeName);
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "SchemeGuid":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this._schemeGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "attrName":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this._attributeName = readArgs.Reader.Value;
        return true;
      case "attrSrc":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.attrSrc = (FieldSource) Enum.Parse(typeof (FieldSource), readArgs.Reader.Value);
        return true;
      case "attributeGuid":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.attributeGuid = new Guid(readArgs.Reader.Value);
        this._attributeID = -1;
        this._attributeName = string.Empty;
        if (readArgs.IUserSession != null)
        {
          IDBAttributeType attributeType = readArgs.IUserSession.GetAttributeType(this.attributeGuid, false);
          if (attributeType != null)
          {
            this._attributeID = attributeType.AttributeID;
            this._attributeName = attributeType.Name;
          }
        }
        return true;
      case "compareType":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.compareType = (CompareType) Enum.Parse(typeof (CompareType), readArgs.Reader.Value);
        return true;
      case "emptyOrder":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.emptyOrder = (EmptyOrder) Enum.Parse(typeof (EmptyOrder), readArgs.Reader.Value);
        return true;
      case "endPosition":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.endPosition = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "endSubstring":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.endSubstring = readArgs.Reader.Value;
        return true;
      case "isRelation":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.attrSrc = !Convert.ToBoolean(readArgs.Reader.Value) ? FieldSource.Object : FieldSource.Relation;
        return true;
      case "sortOrder":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.sortOrder = (SortOrder) Enum.Parse(typeof (SortOrder), readArgs.Reader.Value);
        return true;
      case "startPosition":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.startPosition = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "startSubstring":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.startSubstring = readArgs.Reader.Value;
        return true;
      case "substringEndType":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.substringEndType = (SubstringStartFinishType) Enum.Parse(typeof (SubstringStartFinishType), readArgs.Reader.Value);
        return true;
      case "substringStartType":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.substringStartType = (SubstringStartFinishType) Enum.Parse(typeof (SubstringStartFinishType), readArgs.Reader.Value);
        return true;
      default:
        return false;
    }
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("attributeGuid", this.attributeGuid.ToString());
    xw.WriteAttributeString("attrName", this._attributeName);
    xw.WriteAttributeString("isRelation", this.IsRelation.ToString());
    xw.WriteAttributeString("attrSrc", this.attrSrc.ToString());
    xw.WriteAttributeString("substringStartType", this.substringStartType.ToString());
    xw.WriteAttributeString("startSubstring", this.startSubstring.ToString());
    xw.WriteAttributeString("startPosition", this.startPosition.ToString());
    xw.WriteAttributeString("substringEndType", this.substringEndType.ToString());
    xw.WriteAttributeString("endSubstring", this.endSubstring.ToString());
    xw.WriteAttributeString("endPosition", this.endPosition.ToString());
    xw.WriteAttributeString("compareType", this.compareType.ToString());
    xw.WriteAttributeString("sortOrder", this.sortOrder.ToString());
    xw.WriteAttributeString("emptyOrder", this.emptyOrder.ToString());
    xw.WriteAttributeString("SchemeGuid", this._schemeGuid.ToString());
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Клонировать. Реализация метода для интерфейса ICloneable</summary>
  /// <returns></returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Клонировать</summary>
  /// <returns></returns>
  public virtual AttributeSortSchema Clone()
  {
    AttributeSortSchema instance = (AttributeSortSchema) Activator.CreateInstance(this.GetType());
    instance.attributeGuid = this.attributeGuid;
    instance._attributeID = this._attributeID;
    instance._attributeName = this._attributeName;
    instance.attrSrc = this.attrSrc;
    instance.substringStartType = this.substringStartType;
    instance.startSubstring = this.startSubstring;
    instance.startPosition = this.startPosition;
    instance.substringEndType = this.substringEndType;
    instance.endSubstring = this.endSubstring;
    instance.endPosition = this.endPosition;
    instance.compareType = this.compareType;
    instance.sortOrder = this.sortOrder;
    instance.emptyOrder = this.emptyOrder;
    instance.SchemeGuid = this.SchemeGuid;
    return instance;
  }

  /// <summary>Сравнить 2 строки согласно настройкам</summary>
  /// <param name="x">Строка x</param>
  /// <param name="y">Строка y</param>
  /// <returns>Результат сравнения.
  /// Меньше 0, значит x меньше y,
  /// Равно 0, значит x равен y,
  /// Больше 0, значит x больше y</returns>
  int IComparer.Compare(object x, object y)
  {
    return x == y ? 0 : this.Compare(x?.ToString(), y?.ToString());
  }

  /// <summary>Сравнить</summary>
  /// <param name="strX">Строка X</param>
  /// <param name="strY">Строка Y</param>
  /// <returns>Результат сравнения.
  /// Меньше 0, значит x меньше y,
  /// Равно 0, значит x равен y,
  /// Больше 0, значит x больше y</returns>
  public int Compare(string strX, string strY)
  {
    if ((object) strX == (object) strY)
      return 0;
    string substring1 = this.ExtractSubstring(strX);
    string substring2 = this.ExtractSubstring(strY);
    bool flag = this.emptyOrder == EmptyOrder.ToBegin;
    int num = substring1 == null || substring2 == null ? (substring1 != null || substring2 != null ? (substring1 != null ? (flag ? 1 : -1) : (flag ? -1 : 1)) : (strX == null || strY == null ? (strX != null || strY != null ? (strX != null ? (flag ? 1 : -1) : (flag ? -1 : 1)) : 0) : AttributeSortSchema.StringCompare(strX, strY, this.compareType == CompareType.Number, this.AttributeID))) : AttributeSortSchema.StringCompare(substring1, substring2, this.compareType == CompareType.Number, this.AttributeID);
    if (this.sortOrder == SortOrder.Descending)
      num = -num;
    return num;
  }

  /// <summary>Сравнить текстовые строки</summary>
  /// <param name="strX">Строка 1</param>
  /// <param name="strY">Строка 2</param>
  /// <param name="numberCompare">Сравнивать числа по значению, а не как текст</param>
  /// <returns>Меньше ноля - strX меньше чем strY;
  /// Ноль strX равен strY;
  /// Больше ноля - strX больше чем strY.
  /// </returns>
  public static int StringCompare(string strX, string strY, bool numberCompare)
  {
    return AttributeSortSchema.StringCompare(strX, strY, numberCompare, -1);
  }

  /// <summary>Сравнить текстовые строки</summary>
  /// <param name="strX">Строка 1</param>
  /// <param name="strY">Строка 2</param>
  /// <param name="numberCompare">Сравнивать числа по значению, а не как текст</param>
  /// <returns>Меньше ноля - strX меньше чем strY;
  /// Ноль strX равен strY;
  /// Больше ноля - strX больше чем strY.
  /// </returns>
  public static int StringCompare(string strX, string strY, bool numberCompare, int attributeId)
  {
    if (!numberCompare)
      return string.Compare(strX, strY);
    if (strX == "")
      strX = (string) null;
    if (strY == "")
      strY = (string) null;
    if (strX == strY)
      return 0;
    if (strX != null && strY == null)
      return 1;
    if (strX == null && strY != null)
      return -1;
    ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.DECIMAL | ParserOptions.THOUSANDS | ParserOptions.SCIENTIFIC | ParserOptions.PERCENT | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
    NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
    int startIndex1 = 0;
    int startIndex2 = 0;
    ParsedNumberData number1;
    ParsedNumberData number2;
    string strA;
    string strB;
    int num1;
    int num2;
    int num3;
    while (true)
    {
      number1 = new ParsedNumberData();
      int numberBegin1;
      int numberLength1;
      int num4 = NumberParserAdvanced.ParseNumber(strX, startIndex1, options, number1, currentInfo, out numberBegin1, out numberLength1) ? 1 : 0;
      number2 = new ParsedNumberData();
      int numberBegin2;
      int numberLength2;
      int num5 = NumberParserAdvanced.ParseNumber(strY, startIndex2, options, number2, currentInfo, out numberBegin2, out numberLength2) ? 1 : 0;
      if ((num4 & num5) != 0)
      {
        if (numberBegin1 != 0 || numberBegin2 <= 0)
        {
          if (numberBegin2 != 0 || numberBegin1 <= 0)
          {
            int length = Math.Min(Math.Min(Math.Max(numberBegin1 - startIndex1, numberBegin2 - startIndex2), strX.Length - startIndex1), strY.Length - startIndex2);
            strA = strX.Substring(startIndex1, length);
            strB = strY.Substring(startIndex2, length);
            num1 = string.Compare(strA, strB);
            if (num1 == 0)
            {
              double num6;
              NumberParserAdvanced.NumberToDouble(number1, out num6);
              double num7;
              NumberParserAdvanced.NumberToDouble(number2, out num7);
              num2 = num6.CompareTo(num7);
              if (num2 == 0)
              {
                num3 = string.Compare(strX.Substring(startIndex1, numberBegin1 - startIndex1 + numberLength1), strY.Substring(startIndex2, numberBegin2 - startIndex2 + numberLength2));
                if (num3 == 0)
                {
                  startIndex1 = numberBegin1 + numberLength1;
                  startIndex2 = numberBegin2 + numberLength2;
                }
                else
                  goto label_26;
              }
              else
                goto label_24;
            }
            else
              goto label_19;
          }
          else
            goto label_17;
        }
        else
          break;
      }
      else
        goto label_28;
    }
    return -1;
label_17:
    return 1;
label_19:
    if (attributeId == AvsIDCache.Attr_Designation && (strA.EndsWith("-") || strB.EndsWith("-")) && strA.Length > 0 && strB.Length > 0 && !(strA.Remove(strA.Length - 1) != strB.Remove(strA.Length - 1)))
    {
      double num8 = 0.0;
      double num9 = 0.0;
      if (!strA.EndsWith("-") ? NumberParserAdvanced.NumberToDouble(number2, out num9) : NumberParserAdvanced.NumberToDouble(number1, out num8))
        return num8.CompareTo(num9);
    }
    return num1;
label_24:
    return num2;
label_26:
    return num3;
label_28:
    return string.Compare(strX.Substring(startIndex1), strY.Substring(startIndex2));
  }
}
