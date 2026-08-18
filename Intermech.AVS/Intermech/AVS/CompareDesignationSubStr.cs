// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CompareDesignationSubStr
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary> Схема вырезания подстроки </summary>
public class CompareDesignationSubStr : ICloneable, IWriteReadXml
{
  private CompareDesignationSchema _compareDesignationSchema;
  private CompareDesignationSubStr.FindWhat _startFindWhat = CompareDesignationSubStr.FindWhat.Unknow;
  private int _startNumber = 1;
  private char _startSymbol = '.';
  private CompareDesignationSubStr.FindWhat _finishFindWhat = CompareDesignationSubStr.FindWhat.Unknow;
  private int _finishNumber = 1;
  private char _finishSymbol = '.';
  private Guid _strGuid = Guid.Empty;

  public CompareDesignationSubStr()
  {
    this._compareDesignationSchema = (CompareDesignationSchema) null;
    this._strGuid = Guid.NewGuid();
  }

  public CompareDesignationSubStr(CompareDesignationSchema сompareDesignationSchema)
  {
    this._compareDesignationSchema = сompareDesignationSchema;
    this._strGuid = Guid.NewGuid();
  }

  /// <summary> Получение подстроки из обозначения </summary>
  /// <param name="designation"></param>
  /// <returns></returns>
  public string GetDesignationSubStr(string designation)
  {
    if (this._startFindWhat == CompareDesignationSubStr.FindWhat.Unknow || designation == string.Empty)
      return string.Empty;
    int startIndex = -1;
    switch (this._startFindWhat)
    {
      case CompareDesignationSubStr.FindWhat.StartEndString:
        startIndex = 0;
        break;
      case CompareDesignationSubStr.FindWhat.AnySymbolNumber:
        startIndex = this._startNumber > designation.Length ? -1 : this._startNumber - 1;
        break;
      case CompareDesignationSubStr.FindWhat.SymbolNumber:
        char[] chArray1 = new char[1]{ this._startSymbol };
        string[] strArray1 = designation.Split(chArray1);
        if (strArray1.Length > this._startNumber)
        {
          startIndex = 0;
          for (int index = 0; index < this._startNumber; ++index)
            startIndex = startIndex + strArray1[index].Length + 1;
          break;
        }
        break;
      case CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd:
        char[] chArray2 = new char[1]{ this._startSymbol };
        string[] strArray2 = designation.Split(chArray2);
        if (strArray2.Length > this._startNumber)
        {
          int length = designation.Length;
          for (int index = strArray2.Length - 1; index >= strArray2.Length - this._startNumber; --index)
            length -= strArray2[index].Length + 1;
          startIndex = length + 1;
          break;
        }
        break;
    }
    if (startIndex < 0 || startIndex >= designation.Length)
      return string.Empty;
    string str = designation.Substring(startIndex, designation.Length - startIndex);
    int length1 = -1;
    switch (this._finishFindWhat)
    {
      case CompareDesignationSubStr.FindWhat.StartEndString:
        length1 = str.Length;
        break;
      case CompareDesignationSubStr.FindWhat.AnySymbolNumber:
        length1 = this._finishNumber > str.Length ? str.Length : this._finishNumber;
        break;
      case CompareDesignationSubStr.FindWhat.SymbolNumber:
        char[] chArray3 = new char[1]{ this._finishSymbol };
        string[] strArray3 = designation.Split(chArray3);
        if (strArray3.Length > this._finishNumber)
        {
          int num = 0;
          for (int index = 0; index < this._finishNumber; ++index)
            num = num + strArray3[index].Length + 1;
          length1 = num - 1 - startIndex;
          break;
        }
        break;
      case CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd:
        char[] chArray4 = new char[1]{ this._finishSymbol };
        string[] strArray4 = designation.Split(chArray4);
        if (strArray4.Length > this._finishNumber)
        {
          int length2 = designation.Length;
          for (int index = strArray4.Length - 1; index >= strArray4.Length - this._finishNumber; --index)
            length2 -= strArray4[index].Length + 1;
          length1 = length2 - startIndex;
          break;
        }
        break;
    }
    if (length1 < 0)
      return string.Empty;
    return length1 + 1 > str.Length ? str : str.Substring(0, length1);
  }

  /// <summary> Guid правила </summary>
  public Guid StrGuid => this._strGuid;

  /// <summary> Полная вычленения подстрок из обозначения для определения "похожести" обозначений </summary>
  public CompareDesignationSchema CompareDesignationSchema
  {
    get => this._compareDesignationSchema;
    set => this._compareDesignationSchema = value;
  }

  /// <summary> Что искать для определения начала/окончания подстроки </summary>
  public CompareDesignationSubStr.FindWhat StartFindWhat
  {
    get => this._startFindWhat;
    set => this._startFindWhat = value;
  }

  /// <summary> Номер символа или буквы, который надо найти </summary>
  public int StartNumber
  {
    get => this._startNumber;
    set => this._startNumber = value;
  }

  /// <summary> Символ, который надо найти </summary>
  public char StartSymbol
  {
    get => this._startSymbol;
    set => this._startSymbol = value;
  }

  /// <summary> Что искать для определения начала/окончания подстроки </summary>
  public CompareDesignationSubStr.FindWhat FinishFindWhat
  {
    get => this._finishFindWhat;
    set => this._finishFindWhat = value;
  }

  /// <summary> Номер символа или буквы, который надо найти </summary>
  public int FinishNumber
  {
    get => this._finishNumber;
    set => this._finishNumber = value;
  }

  /// <summary> Символ, который надо найти </summary>
  public char FinishSymbol
  {
    get => this._finishSymbol;
    set => this._finishSymbol = value;
  }

  /// <summary> Строковое представление правила определения начала подстроки </summary>
  public string StartAsText
  {
    get
    {
      switch (this.StartFindWhat)
      {
        case CompareDesignationSubStr.FindWhat.StartEndString:
          return "начала обозначения";
        case CompareDesignationSubStr.FindWhat.AnySymbolNumber:
          return $"буквы номер {this.StartNumber.ToString()}";
        case CompareDesignationSubStr.FindWhat.SymbolNumber:
          return $"символа '{this.StartSymbol.ToString()}' номер {this.StartNumber.ToString()}";
        case CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd:
          return $"символа '{this.StartSymbol.ToString()}' номер {this.StartNumber.ToString()} (с конца обозначения)";
        default:
          return "???";
      }
    }
  }

  /// <summary> Строковое представления правила определения начала подстроки </summary>
  public string FinishAsText
  {
    get
    {
      switch (this.FinishFindWhat)
      {
        case CompareDesignationSubStr.FindWhat.StartEndString:
          return "окончания обозначения";
        case CompareDesignationSubStr.FindWhat.AnySymbolNumber:
          return $"количества символов = {this.FinishNumber.ToString()}";
        case CompareDesignationSubStr.FindWhat.SymbolNumber:
          return $"символа '{this.FinishSymbol.ToString()}' номер {this.FinishNumber.ToString()}";
        case CompareDesignationSubStr.FindWhat.SymbolNumberFromEnd:
          return $"символа '{this.FinishSymbol.ToString()}' номер {this.FinishNumber.ToString()} (с конца обозначения)";
        default:
          return "???";
      }
    }
  }

  /// <summary> Сделать полную копию схемы </summary>
  /// <returns> Копия схемы </returns>
  public object Clone() => (object) this.CreateClone();

  /// <summary> Сделать полную копию схемы </summary>
  /// <returns> Копия схемы </returns>
  public CompareDesignationSubStr CreateClone()
  {
    CompareDesignationSubStr clone = new CompareDesignationSubStr(this._compareDesignationSchema);
    clone.CopyParamsFrom(this);
    return clone;
  }

  /// <summary> Копировать параметры из другой схемы </summary>
  /// <returns> Копия схемы </returns>
  public virtual void CopyParamsFrom(CompareDesignationSubStr copy)
  {
    this._startFindWhat = copy.StartFindWhat;
    this._startNumber = copy.StartNumber;
    this._startSymbol = copy.StartSymbol;
    this._finishFindWhat = copy.FinishFindWhat;
    this._finishNumber = copy.FinishNumber;
    this._finishSymbol = copy.FinishSymbol;
    this._strGuid = copy.StrGuid;
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "FinishFindWhat":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.FinishFindWhat = (CompareDesignationSubStr.FindWhat) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "FinishNumber":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.FinishNumber = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "FinishSymbol":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.FinishSymbol = Convert.ToChar(readArgs.Reader.Value);
        return true;
      case "GUID":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this._strGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "StartFindWhat":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.StartFindWhat = (CompareDesignationSubStr.FindWhat) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "StartNumber":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.StartNumber = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "StartSymbol":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.StartSymbol = Convert.ToChar(readArgs.Reader.Value);
        return true;
      default:
        return false;
    }
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(this.GetType().Name);
    try
    {
      this.WriteFiledsToXml(elementName, xw, objectRefId);
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  public virtual void WriteFiledsToXml(
    string elementName,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    xw.WriteAttributeString("StartFindWhat", ((int) this.StartFindWhat).ToString());
    xw.WriteAttributeString("StartNumber", this.StartNumber.ToString());
    xw.WriteAttributeString("StartSymbol", this.StartSymbol.ToString());
    xw.WriteAttributeString("FinishFindWhat", ((int) this.FinishFindWhat).ToString());
    xw.WriteAttributeString("FinishNumber", this.FinishNumber.ToString());
    xw.WriteAttributeString("FinishSymbol", this.FinishSymbol.ToString());
    xw.WriteAttributeString("GUID", this._strGuid.ToString());
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary> Что искать для определения начала / окончания подстроки </summary>
  public enum FindWhat
  {
    Unknow = 1,
    StartEndString = 2,
    AnySymbolNumber = 3,
    SymbolNumber = 4,
    SymbolNumberFromEnd = 5,
  }
}
