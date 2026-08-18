// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecifNumbering
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, хранящий информацию о порядке нумерации в спецификации </summary>
public class SpecifNumbering : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  protected SpecifNumbering _ParentLevel;
  public bool? _IzdelieSameNumbers;
  public int _StartNumber;
  public int _BetweenDifferentDesignations;
  public int _BetweenSameDesignations;
  public int _BetweenIspolns;
  public int _BeforeNewPart;
  public int _BeforeNewRazdel;
  public int _BeforeVariableData;
  public int _BeforeNewIspoln;
  public int _BeforeNewObjType;

  /// <summary> Ссылка на вышестоящий уровень настроек </summary>
  public SpecifNumbering ParentLevel
  {
    get => this._ParentLevel;
    set => this._ParentLevel = value;
  }

  /// <summary> Начать нумерацию с номера </summary>
  public int StartNumber
  {
    get
    {
      if (this._StartNumber != 0)
        return this._StartNumber;
      return this._ParentLevel == null ? 0 : this._ParentLevel.StartNumber;
    }
    set => this._StartNumber = value;
  }

  /// <summary> Одинаковые номера изделия в различных исполнениях </summary>
  public bool IzdelieSameNumbers
  {
    get
    {
      if (this._IzdelieSameNumbers.HasValue)
        return this._IzdelieSameNumbers.Value;
      return this._ParentLevel == null || this._ParentLevel.IzdelieSameNumbers;
    }
    set
    {
      this._IzdelieSameNumbers = new bool?(value);
      if (this._ParentLevel != null)
      {
        int num1 = value ? 1 : 0;
        bool? izdelieSameNumbers = this._ParentLevel._IzdelieSameNumbers;
        int num2 = izdelieSameNumbers.GetValueOrDefault() ? 1 : 0;
        if (num1 == num2 & izdelieSameNumbers.HasValue)
        {
          this._IzdelieSameNumbers = new bool?();
          return;
        }
      }
      this._IzdelieSameNumbers = new bool?(value);
    }
  }

  /// <summary> Шаг позиций между записями: При различных обозначениях </summary>
  public int BetweenDifferentDesignations
  {
    get
    {
      if (this._BetweenDifferentDesignations != 0)
        return this._BetweenDifferentDesignations;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BetweenDifferentDesignations;
    }
    set => this._BetweenDifferentDesignations = value;
  }

  /// <summary> Шаг позиций между записями: При похожих обозначениях </summary>
  public int BetweenSameDesignations
  {
    get
    {
      if (this._BetweenSameDesignations != 0)
        return this._BetweenSameDesignations;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BetweenSameDesignations;
    }
    set => this._BetweenSameDesignations = value;
  }

  /// <summary> Шаг позиций между записями: Между исполнениями детали </summary>
  public int BetweenIspolns
  {
    get
    {
      if (this._BetweenIspolns != 0)
        return this._BetweenIspolns;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BetweenIspolns;
    }
    set => this._BetweenIspolns = value;
  }

  /// <summary> Шаг позиций между записями: Перед новой частью </summary>
  public int BeforeNewPart
  {
    get
    {
      if (this._BeforeNewPart != 0)
        return this._BeforeNewPart;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BeforeNewPart;
    }
    set => this._BeforeNewPart = value;
  }

  /// <summary> Шаг позиций между записями: Перед новым разделом </summary>
  public int BeforeNewRazdel
  {
    get
    {
      if (this._BeforeNewRazdel != 0)
        return this._BeforeNewRazdel;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BeforeNewRazdel;
    }
    set => this._BeforeNewRazdel = value;
  }

  /// <summary> Шаг позиций между записями: Перед переменными данными </summary>
  public int BeforeVariableData
  {
    get
    {
      if (this._BeforeVariableData != 0)
        return this._BeforeVariableData;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BeforeVariableData;
    }
    set => this._BeforeVariableData = value;
  }

  /// <summary> Шаг позиций между записями: Перед новым исполнением </summary>
  public int BeforeNewIspoln
  {
    get
    {
      if (this._BeforeNewIspoln != 0)
        return this._BeforeNewIspoln;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BeforeNewIspoln;
    }
    set => this._BeforeNewIspoln = value;
  }

  /// <summary> Шаг позиций между записями: Перед новым классом объекта </summary>
  public int BeforeNewObjType
  {
    get
    {
      if (this._BeforeNewObjType != 0)
        return this._BeforeNewObjType;
      return this._ParentLevel == null ? 0 : this._ParentLevel.BeforeNewObjType;
    }
    set => this._BeforeNewObjType = value;
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "BeforeNewIspoln":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_1 = Convert.ToInt32(readArgs.Reader.Value);
        this.BeforeNewIspoln = this._ParentLevel == null ? int32_1 : (this._ParentLevel.BeforeNewIspoln == int32_1 || this._ParentLevel.BeforeNewIspoln == 0 ? 0 : int32_1);
        return true;
      case "BeforeNewObjType":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_2 = Convert.ToInt32(readArgs.Reader.Value);
        this.BeforeNewObjType = this._ParentLevel == null ? int32_2 : (this._ParentLevel.BeforeNewObjType == int32_2 || this._ParentLevel.BeforeNewObjType == 0 ? 0 : int32_2);
        return true;
      case "BeforeNewPart":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_3 = Convert.ToInt32(readArgs.Reader.Value);
        this.BeforeNewPart = this._ParentLevel == null ? int32_3 : (this._ParentLevel.BeforeNewPart == int32_3 || this._ParentLevel.BeforeNewPart == 0 ? 0 : int32_3);
        return true;
      case "BeforeNewRazdel":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_4 = Convert.ToInt32(readArgs.Reader.Value);
        this.BeforeNewRazdel = this._ParentLevel == null ? int32_4 : (this._ParentLevel.BeforeNewRazdel == int32_4 || this._ParentLevel.BeforeNewRazdel == 0 ? 0 : int32_4);
        return true;
      case "BeforeVariableData":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_5 = Convert.ToInt32(readArgs.Reader.Value);
        this.BeforeVariableData = this._ParentLevel == null ? int32_5 : (this._ParentLevel.BeforeVariableData == int32_5 || this._ParentLevel.BeforeVariableData == 0 ? 0 : int32_5);
        return true;
      case "BetweenDifferentDesignations":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_6 = Convert.ToInt32(readArgs.Reader.Value);
        this.BetweenDifferentDesignations = this._ParentLevel == null ? int32_6 : (this._ParentLevel.BetweenDifferentDesignations == int32_6 || this._ParentLevel.BetweenDifferentDesignations == 0 ? 0 : int32_6);
        return true;
      case "BetweenIspolns":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_7 = Convert.ToInt32(readArgs.Reader.Value);
        this.BetweenIspolns = this._ParentLevel == null ? int32_7 : (this._ParentLevel.BetweenIspolns == int32_7 || this._ParentLevel.BetweenIspolns == 0 ? 0 : int32_7);
        return true;
      case "BetweenSameDesignations":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_8 = Convert.ToInt32(readArgs.Reader.Value);
        this.BetweenSameDesignations = this._ParentLevel == null ? int32_8 : (this._ParentLevel.BetweenSameDesignations == int32_8 || this._ParentLevel.BetweenSameDesignations == 0 ? 0 : int32_8);
        return true;
      case "IzdelieSameNumbers":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        bool boolean = Convert.ToBoolean(readArgs.Reader.Value);
        this._IzdelieSameNumbers = this._ParentLevel == null || this._ParentLevel.IzdelieSameNumbers != boolean ? new bool?(boolean) : new bool?();
        return true;
      case "StartNumber":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        int int32_9 = Convert.ToInt32(readArgs.Reader.Value);
        this.StartNumber = this._ParentLevel == null ? int32_9 : (this._ParentLevel.StartNumber == int32_9 || this._ParentLevel.StartNumber == 0 ? 0 : int32_9);
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
    if (elementName != this.GetType().ToString())
      xw.WriteStartElement(elementName);
    try
    {
      int num;
      if ((this.ParentLevel == null || this.ParentLevel.StartNumber != this.StartNumber) && this.StartNumber != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.StartNumber;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("StartNumber", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BetweenDifferentDesignations != this.BetweenDifferentDesignations) && this.BetweenDifferentDesignations != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenDifferentDesignations;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenDifferentDesignations", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BetweenSameDesignations != this.BetweenSameDesignations) && this.BetweenSameDesignations != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenSameDesignations;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenSameDesignations", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BetweenIspolns != this.BetweenIspolns) && this.BetweenIspolns != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenIspolns;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenIspolns", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BeforeNewPart != this.BeforeNewPart) && this.BeforeNewPart != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeNewPart;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeNewPart", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BeforeNewRazdel != this.BeforeNewRazdel) && this.BeforeNewRazdel != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeNewRazdel;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeNewRazdel", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BeforeVariableData != this.BeforeVariableData) && this.BeforeVariableData != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeVariableData;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeVariableData", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BeforeNewIspoln != this.BeforeNewIspoln) && this.BeforeNewIspoln != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeNewIspoln;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeNewIspoln", str);
      }
      if ((this.ParentLevel == null || this.ParentLevel.BeforeNewObjType != this.BeforeNewObjType) && this.BeforeNewObjType != 0)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeNewObjType;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeNewObjType", str);
      }
      if (!this._IzdelieSameNumbers.HasValue)
        return;
      xw.WriteAttributeString("IzdelieSameNumbers", this._IzdelieSameNumbers.ToString());
    }
    finally
    {
      if (elementName != this.GetType().ToString())
        xw.WriteEndElement();
    }
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public SpecifNumbering Clone()
  {
    SpecifNumbering instance = (SpecifNumbering) Activator.CreateInstance(this.GetType());
    instance.CopyParamsFrom(this);
    return instance;
  }

  /// <summary>Копировать параметры из другой схемы</summary>
  /// <returns>Копия схемы</returns>
  public void CopyParamsFrom(SpecifNumbering copy)
  {
    this.StartNumber = copy.StartNumber;
    this.BetweenDifferentDesignations = copy.BetweenDifferentDesignations;
    this.BetweenSameDesignations = copy.BetweenSameDesignations;
    this.BetweenIspolns = copy.BetweenIspolns;
    this.BeforeNewPart = copy.BeforeNewPart;
    this.BeforeNewRazdel = copy.BeforeNewRazdel;
    this.BeforeVariableData = copy.BeforeVariableData;
    this.BeforeNewIspoln = copy.BeforeNewIspoln;
    this.BeforeNewObjType = copy.BeforeNewObjType;
    this._IzdelieSameNumbers = copy._IzdelieSameNumbers;
  }

  /// <summary>
  /// Загрузка параметров по-умолчанию для корня дерева настроек
  /// </summary>
  protected void LoadRootParams()
  {
    this._StartNumber = 1;
    this._BetweenDifferentDesignations = 1;
    this._BetweenSameDesignations = 1;
    this._BetweenIspolns = 1;
    this._BeforeNewPart = 1;
    this._BeforeNewRazdel = 1;
    this._BeforeVariableData = 1;
    this._BeforeNewIspoln = 1;
    this._BeforeNewObjType = 1;
    this._IzdelieSameNumbers = new bool?(true);
  }

  /// <summary> Сбросить настройки к значениям по умолчанию </summary>
  public virtual void Clear()
  {
    if (this._ParentLevel == null)
    {
      this.LoadRootParams();
    }
    else
    {
      this._StartNumber = 0;
      this._BetweenDifferentDesignations = 0;
      this._BetweenSameDesignations = 0;
      this._BetweenIspolns = 0;
      this._BeforeNewPart = 0;
      this._BeforeNewRazdel = 0;
      this._BeforeVariableData = 0;
      this._BeforeNewIspoln = 0;
      this._BeforeNewObjType = 0;
      this._IzdelieSameNumbers = new bool?();
    }
  }
}
