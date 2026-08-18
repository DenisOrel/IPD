// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SubstituteObject
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Объект-заменитель в составе</summary>
public class SubstituteObject : IComparable
{
  /// <summary>Идентификатор связи "F_PRJLINK_ID"</summary>
  private long _prjLinkID;
  /// <summary>
  /// Идентификатор конкретной версии родительского объекта "F_OBJECT_ID"
  /// </summary>
  private long _projID;
  /// <summary>
  /// Идентификатор конкретной версии дочернего объекта "F_OBJECT_ID"
  /// </summary>
  private long _partID;
  /// <summary>Тип связи</summary>
  private int _relationType = -1;

  /// <summary>Идентификатор связи между объектами "F_PRJLINK_ID"</summary>
  public long PrjLinkID => this._prjLinkID;

  /// <summary>
  /// Идентификатор конкретной версии родительского объекта "F_OBJECT_ID"
  /// </summary>
  public long ProjID => this._projID;

  /// <summary>
  /// Идентификатор конкретной версии дочернего объекта "F_OBJECT_ID"
  /// </summary>
  public long PartID => this._partID;

  /// <summary>Тип связи</summary>
  public int RelationType => this._relationType;

  /// <summary>Создать заполненный экземпляр</summary>
  /// <param name="prjLinkID">ID связи</param>
  /// <param name="projID">ID версии родительского объекта</param>
  /// <param name="partID">ID версии дочернего объекта</param>
  /// <param name="relationType">ID типа связи</param>
  public SubstituteObject(long prjLinkID, long projID, long partID, int relationType)
  {
    this._prjLinkID = prjLinkID;
    this._projID = projID;
    this._partID = partID;
    this._relationType = relationType;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>false, если не равен указанному объекту</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is SubstituteObject substituteObject))
      return base.Equals(obj);
    return this.PrjLinkID == substituteObject.PrjLinkID && this.ProjID == substituteObject.ProjID && this.PartID == substituteObject.PartID && this.RelationType == substituteObject.RelationType;
  }

  /// <summary>Вернуть хэш</summary>
  /// <returns>Хэш</returns>
  public override int GetHashCode()
  {
    long num1 = this.PrjLinkID;
    int num2 = num1.GetHashCode() << 24;
    num1 = this.ProjID;
    int num3 = num1.GetHashCode() << 16 /*0x10*/;
    int num4 = num2 ^ num3;
    num1 = this.PartID;
    int num5 = num1.GetHashCode() << 8;
    return num4 ^ num5 ^ this.RelationType.GetHashCode();
  }

  /// <summary>Выполнить сравнение двух объектов</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1 - меньше, 0 - равен, 1 - больше</returns>
  public int CompareTo(object obj)
  {
    if (!(obj is SubstituteObject substituteObject))
      return 1;
    return this.Equals((object) substituteObject) ? 0 : Math.Abs(this.PartID).CompareTo(Math.Abs(substituteObject.PartID));
  }
}
