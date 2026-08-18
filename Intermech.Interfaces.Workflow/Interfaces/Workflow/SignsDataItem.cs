// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.SignsDataItem
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Workflow;

[Serializable]
public class SignsDataItem
{
  private Guid _objectType;
  private BindingList<SignsGroup> _groups;

  public string ObjectTypeName => MetaDataHelper.GetObjectTypeName(this.ObjectType);

  /// <summary>GUID типа настраиваемого объекта</summary>
  public Guid ObjectType
  {
    get => this._objectType;
    set => this._objectType = value;
  }

  /// <summary>ID типа настраиваемого объекта</summary>
  public int ObjectTypeID => MetaDataHelper.GetObjectTypeID(this.ObjectType);

  /// <summary>Подпись в любой графе</summary>
  public bool SignAnyGraph
  {
    get
    {
      if (this.Groups.Count == 0)
        return true;
      return this.Groups.Count == 1 && this.Groups[0].SignAnyGraph;
    }
  }

  public BindingList<SignsGroup> Groups
  {
    get => this._groups;
    set => this._groups = value;
  }

  /// <summary>Получаем группу по её идентификатору</summary>
  /// <param name="groupID">идентификатор требуемой группы</param>
  /// <returns>null если группа не найдена или группу</returns>
  public SignsGroup this[int groupID]
  {
    get
    {
      foreach (SignsGroup group in (Collection<SignsGroup>) this.Groups)
      {
        if (group.GroupID == groupID)
          return group;
      }
      return (SignsGroup) null;
    }
  }

  public SignsDataItem(Guid objectType)
  {
    this._objectType = objectType;
    this._groups = new BindingList<SignsGroup>();
  }

  public SignsDataItem()
  {
  }

  /// <summary>
  /// Установить данного родителя всем дочерним, данный метод используется после восстановления из XML
  /// </summary>
  public void SetChild()
  {
    foreach (SignsGroup group in (Collection<SignsGroup>) this.Groups)
      group.Parent = this;
  }
}
