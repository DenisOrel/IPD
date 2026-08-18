
// Type: Intermech.Controls.Grid.ChangedType
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace Intermech.Controls.Grid;

/// <summary>Change events that are filtered up out of the control</summary>
public enum ChangedType
{
  /// <summary>Invalidation Fired</summary>
  GeneralInvalidate,
  /// <summary>Sub Item Changed</summary>
  SubItemChanged,
  /// <summary>Sub Item Collection Changed</summary>
  SubItemCollectionChanged,
  /// <summary>Item Changed</summary>
  ItemChanged,
  /// <summary>Item Collection Changed</summary>
  ItemCollectionChanged,
  ItemCollectionAdded,
  ItemCollectionRemoved,
  /// <summary>Column changed</summary>
  ColumnChanged,
  /// <summary>Column Collection Changed</summary>
  ColumnCollectionChanged,
  /// <summary>Focus Changed</summary>
  FocusedChanged,
  /// <summary>A different item is now selected</summary>
  SelectionChanged,
  /// <summary>Column state has changed</summary>
  ColumnStateChanged,
}
