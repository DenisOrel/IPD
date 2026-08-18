
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemeStepsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;


namespace Intermech.Navigator.LifeCycle;

/// <summary>
/// Закладка, отображающая список шагов жизненных циклов, принадлежащих схеме жизненного цикла
/// </summary>
public class LifeCycleSchemeStepsView : ChildrenView
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;

  /// <summary>Название закладки</summary>
  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_1338");

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (LifeCycleSchemeStepsView._imageIndex >= 0)
        return LifeCycleSchemeStepsView._imageIndex;
      LifeCycleSchemeStepsView._imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgLifeSteps");
      return LifeCycleSchemeStepsView._imageIndex;
    }
  }
}
