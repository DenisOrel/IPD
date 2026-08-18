// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ChangeInfo
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Информация об изменениях в структуре комплекта</summary>
[Serializable]
public class ChangeInfo : IComparable<ChangeInfo>, IEquatable<ChangeInfo>, IComparable
{
  /// <summary>Идентификатор объекта или связи</summary>
  protected long _id = -1;
  /// <summary>Идентификатор типа объекта или связи</summary>
  protected int _typeID = -1;
  /// <summary>Тип операции</summary>
  protected DocOperType _operType;
  /// <summary>Тип элемента</summary>
  protected AttributableElements _elemType;

  /// <summary>Конструктор</summary>
  /// <param name="id"></param>
  /// <param name="typeID"></param>
  /// <param name="operType"></param>
  public ChangeInfo(long id, int typeID, DocOperType operType)
  {
    this._id = id;
    this._typeID = typeID;
    this._operType = operType;
  }

  /// <summary>Идентификатор объекта или связи</summary>
  public virtual long ID => this._id;

  /// <summary>Идентификатор типа объекта или связи</summary>
  public virtual int TypeID => this._typeID;

  /// <summary>Тип операции</summary>
  public virtual DocOperType OperType => this._operType;

  /// <summary>Тип элемента</summary>
  public virtual AttributableElements ElemType => this._elemType;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this._id.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public int CompareTo(ChangeInfo other)
  {
    if (other == null)
      return -1;
    int num1 = this.ElemType.CompareTo((object) other.ElemType);
    if (num1 != 0)
      return num1;
    int num2 = this.ID.CompareTo(other.ID);
    if (num2 != 0)
      return num2;
    return this.OperType.CompareTo((object) other.OperType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(ChangeInfo other) => this.CompareTo(other).Equals(0);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public int CompareTo(object obj) => this.CompareTo(obj as ChangeInfo);
}
