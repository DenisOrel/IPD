// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureNodeID
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>узел Структура архива</summary>
public class ArchiveStructureNodeID : INodeID
{
  /// <summary>тип атрибута</summary>
  protected int attrTypeID;
  /// <summary>
  /// id архива,
  /// структуру которого показываем
  /// </summary>
  private long arcID;
  /// <summary>нечто про запас.</summary>
  protected object cookie;

  /// <summary>
  /// id архива,
  /// структуру которого показываем
  /// </summary>
  public long ArchiveID
  {
    get => this.arcID;
    set => this.arcID = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="_attrTypeID">id типа выделенного атрибута</param>
  /// <param name="_arcID"> id выделенного архива </param>
  public ArchiveStructureNodeID(int _attrTypeID, long _arcID)
  {
    this.attrTypeID = _attrTypeID;
    this.arcID = _arcID;
  }

  /// <summary>
  /// Идентификатор категории элемента пространства навигации
  /// </summary>
  public int CategoryID => 3;

  /// <summary>собственно тип атрибута</summary>
  public int TypeID => this.attrTypeID;

  /// <summary>нечто про запас.</summary>
  public object Cookie
  {
    get => this.cookie;
    set => this.cookie = value;
  }

  /// <summary>проверим, равен ли один объект другому</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj is ArchiveStructureNodeID archiveStructureNodeId ? this.attrTypeID.Equals(archiveStructureNodeId.attrTypeID) : base.Equals(obj);
  }

  /// <summary>вернуть хэш-код для объекта</summary>
  /// <returns></returns>
  public override int GetHashCode() => this.attrTypeID.GetHashCode();
}
