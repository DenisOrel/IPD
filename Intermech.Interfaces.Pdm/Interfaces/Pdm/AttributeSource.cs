// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.AttributeSource
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Класс для хранения описания атрибута, его источника</summary>
[Serializable]
public sealed class AttributeSource
{
  /// <summary>Идентификатор типа атрибута</summary>
  public int ID;
  /// <summary>GUID типа атрибута</summary>
  public Guid GUID = Guid.Empty;
  /// <summary>Источник атрибута</summary>
  public AttributeSourceTypes Source;

  /// <summary>Создать пустой экземпляр класса</summary>
  public AttributeSource()
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="AnID">Идентификатор типа атрибута</param>
  /// <param name="AGuid">GUID атрибута</param>
  /// <param name="ASource">Источник атрибута</param>
  public AttributeSource(int AnID, Guid AGuid, AttributeSourceTypes ASource)
  {
    this.ID = AnID;
    this.GUID = AGuid;
    this.Source = ASource;
  }

  /// <summary>Перекрытый метод для возвращения заголовка</summary>
  /// <returns></returns>
  public override string ToString()
  {
    string str = this.ID.ToString();
    switch (this.Source)
    {
      case AttributeSourceTypes.Auto:
        return str + ".auto";
      case AttributeSourceTypes.Object:
        return str + ".object";
      case AttributeSourceTypes.Relation:
        return str + ".relation";
      case AttributeSourceTypes.Events:
        return str + ".events";
      case AttributeSourceTypes.History:
        return str + ".history";
      case AttributeSourceTypes.FileStorage:
        return str + ".filestorage";
      default:
        return str + ".other";
    }
  }

  /// <summary>Сравнить экземпляр объекта с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (!(obj is AttributeSource attributeSource))
      return base.Equals(obj);
    return this.ID == attributeSource.ID && this.GUID == attributeSource.GUID && this.Source == attributeSource.Source;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => (int) this.Source << 30 ^ this.ID;

  /// <summary>Сделать клон объекта</summary>
  /// <returns>Вернёт 100% копию объекта</returns>
  public object Clone() => (object) new AttributeSource(this.ID, this.GUID, this.Source);
}
