
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Закладка, отображающая список схем жизненных циклов</summary>
public class LifeCycleSchemesView : ChildrenView
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;

  /// <summary>Название закладки</summary>
  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_1337");

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (LifeCycleSchemesView._imageIndex >= 0)
        return LifeCycleSchemesView._imageIndex;
      LifeCycleSchemesView._imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgLifeStepSchemes");
      return LifeCycleSchemesView._imageIndex;
    }
  }
}
