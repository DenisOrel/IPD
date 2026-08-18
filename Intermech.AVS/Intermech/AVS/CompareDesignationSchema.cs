// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CompareDesignationSchema
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary> Схема определения "похожих" обозначений </summary>
public class CompareDesignationSchema : ICloneable, IWriteReadXml
{
  private SkipLinesSchema _skipLinesSchema;
  private SpecifNumberingFull _specifNumberingFull;
  private bool _changed;
  private CompareDesignationSubStr[] _subStrs = new CompareDesignationSubStr[0];

  public CompareDesignationSchema(SpecifNumberingFull specifNumberingFull)
  {
    this.SpecifNumberingFull = specifNumberingFull;
  }

  public CompareDesignationSchema(SkipLinesSchema skipLinesSchema)
  {
    this.SkipLinesSchema = skipLinesSchema;
  }

  /// <summary> Полная схема нумерации позиций в спецификации </summary>
  public SpecifNumberingFull SpecifNumberingFull
  {
    get => this._specifNumberingFull;
    set => this._specifNumberingFull = value;
  }

  /// <summary> Схема пропуска строк в спецификации </summary>
  public SkipLinesSchema SkipLinesSchema
  {
    get => this._skipLinesSchema;
    set => this._skipLinesSchema = value;
  }

  /// <summary> Массив правил для выдирания подстроки </summary>
  public CompareDesignationSubStr[] SubStrs => this._subStrs;

  /// <summary> Признак того, что схема была отредактирована </summary>
  public bool Changed
  {
    get => this._changed;
    set => this._changed = value;
  }

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get
    {
      if (this._skipLinesSchema == null && this._specifNumberingFull == null || this.SpecifNumberingFull != null && this.SpecifNumberingFull.ReadOnly)
        return true;
      return this._skipLinesSchema != null && this._skipLinesSchema.ReadOnly;
    }
  }

  /// <summary> Загрузка схемы по-умолчанию </summary>
  public void LoadDefaultSchema()
  {
    if (this._specifNumberingFull == null && this._skipLinesSchema == null)
      return;
    if (this._specifNumberingFull != null && this._specifNumberingFull.ParentLevel == null || this._skipLinesSchema != null && this._skipLinesSchema.Parent == null)
    {
      this._subStrs = new CompareDesignationSubStr[1]
      {
        new CompareDesignationSubStr(this)
        {
          StartFindWhat = CompareDesignationSubStr.FindWhat.StartEndString,
          FinishFindWhat = CompareDesignationSubStr.FindWhat.AnySymbolNumber,
          FinishNumber = 12
        }
      };
    }
    else
    {
      CompareDesignationSchema designationSchema;
      int? length;
      if (this._specifNumberingFull != null && this._specifNumberingFull.ParentLevel != null)
      {
        designationSchema = this._specifNumberingFull.ParentLevel.CompareDesignationSchema;
        length = designationSchema?.SubStrs?.Length;
        this._subStrs = new CompareDesignationSubStr[length ?? 0];
      }
      else
      {
        if (this._skipLinesSchema == null || this._skipLinesSchema.Parent == null)
          return;
        designationSchema = this._skipLinesSchema.Parent.CompareDesignationSchema;
        length = designationSchema?.SubStrs?.Length;
        this._subStrs = new CompareDesignationSubStr[length ?? 0];
      }
      int index = 0;
      while (true)
      {
        int num1 = index;
        length = designationSchema?.SubStrs?.Length;
        int num2 = length ?? 0;
        if (num1 < num2)
        {
          CompareDesignationSubStr designationSubStr = new CompareDesignationSubStr(this);
          designationSubStr.CopyParamsFrom(designationSchema.SubStrs[index]);
          this._subStrs.SetValue((object) designationSubStr, index);
          ++index;
        }
        else
          break;
      }
    }
    this._changed = this._specifNumberingFull != null && this._specifNumberingFull.ParentLevel == null || this._skipLinesSchema != null && this._skipLinesSchema.Parent == null;
  }

  /// <summary> Добавить в схему новое, пустое правило выдирания подстроки </summary>
  /// <returns> созданое правило </returns>
  public CompareDesignationSubStr AddEmptyStr()
  {
    CompareDesignationSubStr newItem = new CompareDesignationSubStr(this);
    this._subStrs = (CompareDesignationSubStr[]) ArrayEditHelper.AddItemToArray((Array) this._subStrs, (object) newItem);
    return newItem;
  }

  /// <summary> Добавить в схему новое, пустое правило выдирания подстроки </summary>
  /// <returns> созданое правило </returns>
  public CompareDesignationSubStr Add(CompareDesignationSubStr compareDesignationSubStr)
  {
    compareDesignationSubStr.CompareDesignationSchema = this;
    this._subStrs = (CompareDesignationSubStr[]) ArrayEditHelper.AddItemToArray((Array) this._subStrs, (object) compareDesignationSubStr);
    return compareDesignationSubStr;
  }

  /// <summary> Очистка состояния схемы, сброс к значениям по умолчанию </summary>
  public void Clear()
  {
    this._changed = false;
    this.LoadDefaultSchema();
  }

  /// <summary> Удалить из списка правило </summary>
  public void Remove(CompareDesignationSubStr compareDesignationSubStr)
  {
    this._subStrs = (CompareDesignationSubStr[]) ArrayEditHelper.RemoveItemAt((Array) this._subStrs, Array.IndexOf<CompareDesignationSubStr>(this._subStrs, compareDesignationSubStr));
  }

  /// <summary> Сравление двух обозначнений </summary>
  /// <param name="designation1"> Обозначение 1 </param>
  /// <param name="designation2"> Обозначение 2 </param>
  /// <returns> true, если обозначения похожи </returns>
  public bool IsDesiagnationsAreSame(string designation1, string designation2)
  {
    if (designation1 == null && designation2 == null)
      return true;
    if (designation1 == null || designation2 == null)
      return false;
    string str1 = string.Empty;
    string str2 = string.Empty;
    if (this.SubStrs.Length != 0)
    {
      foreach (CompareDesignationSubStr subStr in this.SubStrs)
      {
        str1 += subStr.GetDesignationSubStr(designation1);
        str2 += subStr.GetDesignationSubStr(designation2);
      }
    }
    else
    {
      str1 = designation1;
      str2 = designation2;
    }
    return str1 == str2;
  }

  object ICloneable.Clone() => (object) this.Clone();

  public CompareDesignationSchema Clone()
  {
    CompareDesignationSchema designationSchema;
    if (this._skipLinesSchema != null)
    {
      designationSchema = new CompareDesignationSchema(this._skipLinesSchema);
    }
    else
    {
      if (this._specifNumberingFull == null)
        return (CompareDesignationSchema) null;
      designationSchema = new CompareDesignationSchema(this._specifNumberingFull);
    }
    designationSchema.CopyParamsFrom(this);
    return designationSchema;
  }

  /// <summary> Копировать параметры из другой схемы </summary>
  /// <returns> Копия схемы </returns>
  public void CopyParamsFrom(CompareDesignationSchema copy)
  {
    this._subStrs = new CompareDesignationSubStr[copy.SubStrs.Length];
    for (int index = 0; index < copy.SubStrs.Length; ++index)
    {
      CompareDesignationSubStr designationSubStr = new CompareDesignationSubStr(this);
      designationSubStr.CopyParamsFrom(copy.SubStrs[index]);
      this._subStrs.SetValue((object) designationSubStr, index);
    }
    this._changed = copy.Changed;
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  bool IWriteReadXml.ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (!(readArgs.Reader.LocalName == "CompareDesignationSubStrArray2"))
      return false;
    this._subStrs = (CompareDesignationSubStr[]) WriteReadXmlHelper.ReadArrayFromXml(typeof (CompareDesignationSubStr), readArgs);
    foreach (CompareDesignationSubStr subStr in this._subStrs)
      subStr.CompareDesignationSchema = this;
    this._changed = this._subStrs.Length != 0;
    return true;
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  void IWriteReadXml.WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (!this.Changed)
      return;
    xw.WriteStartElement(elementName);
    try
    {
      WriteReadXmlHelper.WriteArrayToXml("CompareDesignationSubStrArray2", (IList) this._subStrs, "CompareDesignationSubStr", xw, objectRefId);
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  void IWriteReadXml.ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    if (this._subStrs.Length != 0)
      return;
    this.LoadDefaultSchema();
  }
}
