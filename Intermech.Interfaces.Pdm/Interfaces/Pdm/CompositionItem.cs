// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionItem
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Позиция состава</summary>
public sealed class CompositionItem : List<CompositionItem>
{
  /// <summary>Флаги, отображающие статус позиции</summary>
  public CompositionItemFlags CompositionItemFlag { get; set; }

  /// <summary>
  /// Атрибуты позиции (ид.атрибута, его принадлежность (объект/связь), значение)
  /// </summary>
  public List<CompositionItemAttribute> Attributes { get; private set; }

  /// <summary>Ссылка на родительскую позицию (для рутовых == null)</summary>
  public CompositionItem Parent { get; set; }

  /// <summary>Индекс в коллекции позиций состава на текущем уровне</summary>
  public int LevelIndex { get; set; }

  /// <summary>Пустая позиция</summary>
  public bool Empty { get; private set; }

  /// <summary>Флаг того, что позиции в составе прошли сравнение</summary>
  public bool Handled { get; set; }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Позиция парент</param>
  public CompositionItem(CompositionItem parent)
    : this(parent, false, -1)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Позиция парент</param>
  /// <param name="empty">Флаг пустой позиции</param>
  /// <param name="levelIndex">Положение на текущем уровне состава</param>
  public CompositionItem(CompositionItem parent, bool empty, int levelIndex)
  {
    this.Parent = parent;
    this.Empty = empty;
    this.LevelIndex = levelIndex;
    if (empty)
      return;
    this.CompositionItemFlag = CompositionItemFlags.Equal;
    this.Attributes = new List<CompositionItemAttribute>();
  }

  public void AddAttribute(CompositionItemAttribute attribute)
  {
    if (this.Attributes.Exists((Predicate<CompositionItemAttribute>) (_ => _.AttributeID.Equals(attribute.AttributeID))))
      return;
    this.Attributes.Add(attribute);
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="versionID">Номер версии</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="owner">Идентификатор владельца</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  /// <param name="siteID">Код узда</param>
  /// <param name="modificationID">Идентификатор контекста</param>
  /// <param name="level">Уровень продвижения</param>
  /// <param name="checkOut">Идентификатор пользователя, взявшего объект на изменение</param>
  /// <param name="projectID">Идентификатор проекта </param>
  public CompositionItem(
    long objectID,
    long id,
    int objectTypeID,
    long versionID,
    string caption,
    long owner,
    long baseVersion,
    string siteID,
    long modificationID,
    int level,
    long checkOut,
    long projectID)
    : this((CompositionItem) null, objectID, id, objectTypeID, versionID, 0L, -1, Guid.Empty, caption, owner, baseVersion, siteID, modificationID, level, checkOut, projectID)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Парент позиция</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="versionID">Номер версии</param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="relationType">Идентификатор типа связи</param>
  /// <param name="prjGuid">Глобальный идентификатор связи</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="owner">Идентификатор владельца</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  /// <param name="siteID">Код узда</param>
  /// <param name="modificationID">Идентификатор контекста</param>
  /// <param name="level">Уровень продвижения</param>
  /// <param name="checkOut">Идентификатор пользователя, взявшего объект на изменение</param>
  /// <param name="projectID">Идентификатор проекта</param>
  public CompositionItem(
    CompositionItem parent,
    long objectID,
    long id,
    int objectTypeID,
    long versionID,
    long prjLinkID,
    int relationType,
    Guid prjGuid,
    string caption,
    long owner,
    long baseVersion,
    string siteID,
    long modificationID,
    int level,
    long checkOut,
    long projectID)
    : this(parent)
  {
    this.CompositionItemFlag = CompositionItemFlags.Equal;
    this.Attributes.AddRange((IEnumerable<CompositionItemAttribute>) new List<CompositionItemAttribute>()
    {
      new CompositionItemAttribute(-2, AttributeSourceTypes.Object, (object) objectID),
      new CompositionItemAttribute(-3, AttributeSourceTypes.Object, (object) id),
      new CompositionItemAttribute(-7, AttributeSourceTypes.Object, (object) objectTypeID),
      new CompositionItemAttribute(-5, AttributeSourceTypes.Object, (object) versionID),
      new CompositionItemAttribute(-20, AttributeSourceTypes.Relation, (object) prjLinkID),
      new CompositionItemAttribute(-23, AttributeSourceTypes.Relation, (object) relationType),
      new CompositionItemAttribute(-26, AttributeSourceTypes.Relation, (object) prjGuid),
      new CompositionItemAttribute(-50, AttributeSourceTypes.Object, (object) caption),
      new CompositionItemAttribute(-8, AttributeSourceTypes.Object, (object) owner),
      new CompositionItemAttribute(-16, AttributeSourceTypes.Object, (object) baseVersion),
      new CompositionItemAttribute(-17, AttributeSourceTypes.Object, (object) siteID),
      new CompositionItemAttribute(-15, AttributeSourceTypes.Object, (object) modificationID),
      new CompositionItemAttribute(-9, AttributeSourceTypes.Object, (object) level),
      new CompositionItemAttribute(-6, AttributeSourceTypes.Object, (object) checkOut),
      new CompositionItemAttribute(-14, AttributeSourceTypes.Object, (object) projectID)
    });
  }

  /// <summary>Создание пустого экземпляра позиции</summary>
  /// <param name="parent">Парент позиция</param>
  /// <param name="levelIndex">Положение на текущем уровне состава</param>
  /// <returns></returns>
  public static CompositionItem CreateEmpty(CompositionItem parent, int levelIndex)
  {
    return new CompositionItem(parent, true, levelIndex);
  }

  /// <summary>Номер версии</summary>
  public int Version
  {
    get
    {
      return Convert.ToInt32(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -5)).Value);
    }
  }

  /// <summary>Признак базовой версии</summary>
  public int BaseVersion
  {
    get
    {
      return Convert.ToInt32(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -16)).Value);
    }
  }

  /// <summary>Идентификатор проекта</summary>
  public long ProjectID
  {
    get
    {
      return Convert.ToInt64(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -14)).Value);
    }
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    get
    {
      return Convert.ToInt64(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -2)).Value);
    }
  }

  /// <summary>Идентификатор объекта</summary>
  public long ID
  {
    get
    {
      return Convert.ToInt64(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -3)).Value);
    }
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjectTypeID
  {
    get
    {
      return Convert.ToInt32(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -7)).Value);
    }
  }

  /// <summary>Уровень продвижения</summary>
  public int Level
  {
    get
    {
      return Convert.ToInt32(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -9)).Value);
    }
  }

  /// <summary>
  /// Идентификатор пользователя, взявшего объект на изменение
  /// </summary>
  public long CheckOut
  {
    get
    {
      return Convert.ToInt64(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -6)).Value);
    }
  }

  /// <summary>Идентификатор владельца</summary>
  public long Owner
  {
    get
    {
      return Convert.ToInt64(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -8)).Value);
    }
  }

  /// <summary>Идентификатор связи</summary>
  public long PrjLinkID
  {
    get
    {
      return Convert.ToInt64(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -20)).Value);
    }
  }

  /// <summary>Глобальный идентификатор связи</summary>
  public Guid PrjLinkGUID
  {
    get
    {
      return (Guid) this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -26)).Value;
    }
  }

  /// <summary>Идентификатор типа связи</summary>
  public int RelationTypeID
  {
    get
    {
      return Convert.ToInt32(this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -23)).Value);
    }
  }

  /// <summary>Заголовок</summary>
  public string Caption
  {
    get
    {
      return (string) this.Attributes.Find((Predicate<CompositionItemAttribute>) (x => x.AttributeID == -50)).Value;
    }
  }

  /// <summary>Клонирование экземпляра текущей позиции</summary>
  public object Clone()
  {
    CompositionItem compositionItem1 = new CompositionItem((CompositionItem) this.Parent?.Clone())
    {
      CompositionItemFlag = this.CompositionItemFlag,
      LevelIndex = this.LevelIndex,
      Empty = this.Empty
    };
    compositionItem1.Attributes = new List<CompositionItemAttribute>();
    foreach (CompositionItemAttribute attribute in this.Attributes)
      compositionItem1.Attributes.Add((CompositionItemAttribute) attribute.Clone());
    if (this.Count > 0)
    {
      foreach (CompositionItem compositionItem2 in (List<CompositionItem>) this)
        compositionItem1.Add((CompositionItem) compositionItem2.Clone());
    }
    return (object) compositionItem1;
  }
}
