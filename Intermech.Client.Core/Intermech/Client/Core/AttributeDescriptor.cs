
// Type: Intermech.Client.Core.AttributeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors.Controls;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> Дескриптор атрибута в списке выбора атрибутов </summary>
public class AttributeDescriptor : IComparable
{
  private int _attributeID = -1;
  private string _attributeName = string.Empty;
  private bool _isRelationAttribute;
  private CheckedListBoxItem _checkedListBoxItem;
  private bool _movedToTop;

  /// <summary> Создать дескриптор атрибута в списке выбора атрибутов </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attributeName"> Наименование атрибута </param>
  /// <param name="isRelationAttribute"> Признак того, что атрибут принадлежит связи, а не объекту </param>
  public AttributeDescriptor(int attributeID, string attributeName, bool isRelationAttribute)
  {
    this._attributeID = attributeID;
    this._attributeName = !(attributeName == string.Empty) ? attributeName : DBHelper.GetAttributeName(attributeID);
    this._isRelationAttribute = isRelationAttribute;
  }

  /// <summary> Создать дескриптор атрибута в списке выбора атрибутов </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="isRelationAttribute"> Признак того, что атрибут принадлежит связи, а не объекту </param>
  public AttributeDescriptor(int attributeID, bool isRelationAttribute)
    : this(attributeID, string.Empty, isRelationAttribute)
  {
  }

  /// <summary> Идентификатор атрибута </summary>
  public int AttributeID => this._attributeID;

  /// <summary> Наименование атрибута </summary>
  public string AttributeName => this._attributeName;

  /// <summary> Признак того, что атрибут принадлежит связи, а не объекту </summary>
  public bool IsRelationAttribute
  {
    get => this._isRelationAttribute;
    set => this._isRelationAttribute = value;
  }

  /// <summary> Ссылка на визуальную строку в CheckedListBox-е, ассоциированую c данным атрибутом </summary>
  public CheckedListBoxItem CheckedListBoxItem
  {
    get => this._checkedListBoxItem;
    set
    {
      if (value.Value != this)
        return;
      this._checkedListBoxItem = value;
    }
  }

  /// <summary> Признак того, что данный атрибут отмечен как выбранный </summary>
  public bool Checked
  {
    get
    {
      return this._checkedListBoxItem != null && this._checkedListBoxItem.CheckState == CheckState.Checked;
    }
    set
    {
      if (this._checkedListBoxItem == null || this._checkedListBoxItem.CheckState == (value ? CheckState.Checked : CheckState.Unchecked))
        return;
      this._checkedListBoxItem.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
    }
  }

  /// <summary> Признак того, что атрибут перемещён в начало списка </summary>
  public bool MovedToTopInSortOrder
  {
    get => this._movedToTop;
    set => this._movedToTop = value;
  }

  public override string ToString() => this._attributeName;

  public int CompareTo(object obj)
  {
    if (obj == null || !(obj is AttributeDescriptor) || this.MovedToTopInSortOrder && !((AttributeDescriptor) obj).MovedToTopInSortOrder)
      return -1;
    return !this.MovedToTopInSortOrder && ((AttributeDescriptor) obj).MovedToTopInSortOrder ? 1 : this.AttributeName.CompareTo(((AttributeDescriptor) obj).AttributeName);
  }
}
