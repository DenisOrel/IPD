// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.SignsDataItemModel
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.Workflow;

[XmlRoot(ElementName = "SignsDataItemModel")]
[Serializable]
public class SignsDataItemModel : INotifyPropertyChanged
{
  private bool _personalSigns;

  public SignsDataItemModel() => this.Nodes = new BindingList<SignsDataItem>();

  [XmlArray(ElementName = "Nodes")]
  public BindingList<SignsDataItem> Nodes { get; private set; }

  public bool Contains(Guid objectTypeGuid)
  {
    foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
    {
      if (node.ObjectType.Equals(objectTypeGuid))
        return true;
    }
    return false;
  }

  public bool Contains(int objectTypeID)
  {
    foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
    {
      if (node.ObjectTypeID.Equals(objectTypeID))
        return true;
    }
    return false;
  }

  /// <summary>
  /// Получить из коллекции элемент который соответствует запрашевоему типу, либо элемент чей тип является родителем для запрашиваемого
  /// </summary>
  /// <param name="objectTypeID">Искомый тип объекта</param>
  /// <returns></returns>
  public SignsDataItem GetSignsDataItem(int objectTypeID)
  {
    foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
    {
      if (node.ObjectTypeID.Equals(objectTypeID))
        return node;
    }
    foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(objectTypeID, node.ObjectTypeID))
        return node;
    }
    return (SignsDataItem) null;
  }

  public SignsDataItem this[Guid objectTypeGuid]
  {
    get
    {
      foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
      {
        if (node.ObjectType.Equals(objectTypeGuid))
          return node;
      }
      return (SignsDataItem) null;
    }
  }

  public SignsDataItem this[int objectTypeID]
  {
    get
    {
      foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
      {
        if (node.ObjectTypeID.Equals(objectTypeID))
          return node;
      }
      return (SignsDataItem) null;
    }
  }

  /// <summary>Требовать персональную подпись исполнителя</summary>
  /// 
  ///             по совещанию с Димой решено эту настройку сделать одну на всё, как и было ранее
  public bool PersonalSigns
  {
    get => this._personalSigns;
    set
    {
      this._personalSigns = value;
      this.OnPropertyChanged(nameof (PersonalSigns));
    }
  }

  public bool HasInvalidTypes
  {
    get
    {
      foreach (SignsDataItem node in (Collection<SignsDataItem>) this.Nodes)
      {
        if (node.ObjectTypeID < 0)
          return true;
      }
      return false;
    }
  }

  /// <summary>
  /// Для обновления элемента управления который отвечает за показ данной опции нужно подписаться на данное событие
  /// </summary>
  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
