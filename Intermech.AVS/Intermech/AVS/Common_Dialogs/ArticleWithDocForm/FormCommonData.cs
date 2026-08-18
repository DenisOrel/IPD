// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.FormCommonData
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Общие данные для закладок</summary>
internal class FormCommonData : IFormCommonData
{
  private Dictionary<string, bool> readOnlyDict = new Dictionary<string, bool>();
  private int relationType = -1;
  private string _designation = string.Empty;
  private string _name = string.Empty;
  private string _okpCode = string.Empty;
  private string _format = string.Empty;
  private MaterialInfo _material = new MaterialInfo(0L, string.Empty);
  private string _size = string.Empty;
  private bool _podbor;
  private string zona;
  private string note;
  private string position;
  private string smotri;
  private string posDesignation;
  private string fullName;
  private long classifierID;

  /// <summary>
  /// Функция дополнительной проверки правильности общих данных
  /// Сделана по наставлению Борисыча, для проверки пустых обозначения и наименования
  /// </summary>
  public void Check()
  {
    if (this._designation == string.Empty && this._name == string.Empty)
      throw new Exception("Не заданы значения для атрибутов \"Обозначение\" и \"Наименование\"");
    if (this._name == string.Empty)
      throw new Exception("Не задано значение для атрибута \"Наименование\"");
  }

  private void OnChanged(CommonDataType type)
  {
    CommonDataChangedDelegate changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new CommonDataChangedEventArgs(type));
  }

  public bool GetReadOnly(string fieldName)
  {
    return this.readOnlyDict.ContainsKey(fieldName) && this.readOnlyDict[fieldName];
  }

  public void SetReadOnly(string fieldName, bool readOnly)
  {
    this.readOnlyDict[fieldName] = readOnly;
  }

  public int RelationType
  {
    get => this.relationType;
    set => this.relationType = value;
  }

  /// <summary>Обозначение</summary>
  public string Designation
  {
    get => this._designation;
    set
    {
      if (!(this._designation != value))
        return;
      this._designation = value;
      this.OnChanged(CommonDataType.Designation);
    }
  }

  /// <summary>Наименование</summary>
  public string Name
  {
    get => this._name;
    set
    {
      if (!(this._name != value))
        return;
      this._name = value;
      this.OnChanged(CommonDataType.Name);
    }
  }

  /// <summary>Код ОКП</summary>
  public string OKPCode
  {
    get => this._okpCode;
    set
    {
      if (!(this._okpCode != value))
        return;
      this._okpCode = value;
      this.OnChanged(CommonDataType.OKPCode);
    }
  }

  /// <summary>Формат</summary>
  public string Format
  {
    get => this._format;
    set
    {
      if (!(this._format != value))
        return;
      this._format = value;
      this.OnChanged(CommonDataType.Format);
    }
  }

  public event CommonDataChangedDelegate Changed;

  /// <summary>Материал</summary>
  public MaterialInfo Material
  {
    get => this._material;
    set
    {
      this._material = value;
      this.OnChanged(CommonDataType.Material);
    }
  }

  /// <summary>Размеры</summary>
  public string Size
  {
    get => this._size;
    set
    {
      this._size = value;
      this.OnChanged(CommonDataType.Size);
    }
  }

  /// <summary>Подбор</summary>
  public bool Podbor
  {
    get => this._podbor;
    set
    {
      this._podbor = value;
      this.OnChanged(CommonDataType.Podbor);
    }
  }

  public string Zona
  {
    get => this.zona;
    set => this.zona = value;
  }

  public string Note
  {
    get => this.note;
    set => this.note = value;
  }

  public string Position
  {
    get => this.position;
    set => this.position = value;
  }

  /// <summary>Смотри</summary>
  public string Smotri
  {
    get => this.smotri;
    set => this.smotri = value;
  }

  public string PosDesignation
  {
    get => this.posDesignation;
    set => this.posDesignation = value;
  }

  public string FullName
  {
    get => this.fullName;
    set => this.fullName = value;
  }

  public long ClassifierID
  {
    get => this.classifierID;
    set => this.classifierID = value;
  }

  public MeasuredValue Count { get; set; }
}
