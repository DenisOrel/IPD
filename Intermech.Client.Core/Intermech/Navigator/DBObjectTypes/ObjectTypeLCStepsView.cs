
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeLCStepsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Закладка, отображающая список шагов ЖЦ для указанного типа объекта
/// </summary>
internal sealed class ObjectTypeLCStepsView : ChildrenView
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;

  /// <summary>Название закладки</summary>
  public override string Caption
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Client.Core_1367"), (object) Helper.GetObjectTypeName(this._nodeID.TypeID));
    }
  }

  /// <summary>Содержимое закладки</summary>
  public override ContentType ViewContentType
  {
    [DebuggerStepThrough] get => ContentType.NonFolders;
  }

  /// <summary>Категория</summary>
  protected override int StateStreamCategoryID => Intermech.Navigator.Consts.CategoryLifeCycleStepNode;

  /// <summary>Название потока, в котором будут сохранены настройки</summary>
  public override string StateStreamPrefix => "ObjectTypeLCSTepsView";

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (ObjectTypeLCStepsView._imageIndex >= 0)
        return ObjectTypeLCStepsView._imageIndex;
      ObjectTypeLCStepsView._imageIndex = ServicesManager.GetService<INamedImageList>().ImageIndex("imgLifeSteps");
      return ObjectTypeLCStepsView._imageIndex;
    }
  }
}
