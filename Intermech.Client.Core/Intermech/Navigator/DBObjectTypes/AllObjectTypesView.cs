
// Type: Intermech.Navigator.DBObjectTypes.AllObjectTypesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>Закладка, отображающая список всех типов объектов</summary>
public class AllObjectTypesView : ChildrenView
{
  public AllObjectTypesView() => this._editingModeButtonItem.Visible = true;

  /// <summary>Название закладки</summary>
  public override string Caption
  {
    [DebuggerStepThrough] get => AllObjectTypesDescriptor.Caption;
  }

  /// <summary>Содержимое закладки</summary>
  public override ContentType ViewContentType
  {
    [DebuggerStepThrough] get => ContentType.Folders;
  }

  /// <summary>Название потока, в котором будут сохранены настройки</summary>
  public override string StateStreamPrefix => nameof (AllObjectTypesView);
}
