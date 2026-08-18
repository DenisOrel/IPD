
// Type: Intermech.Client.Core.Forms.LoadCompositionSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Forms;

public class LoadCompositionSettings
{
  public LoadCompositionSettings.LoadTypeType LoadType { get; set; } = LoadCompositionSettings.LoadTypeType.FullLoad;

  public enum LoadTypeType
  {
    /// <summary>Загрузить несколько уровней вниз от текущего уровня</summary>
    LevelsRelative = 1,
    /// <summary>Загрузить до определённого уровня иерархии</summary>
    LevelsAbsolute = 2,
    /// <summary>Загрузить до последнего уровня, который может быть выбран</summary>
    FullLoad = 3,
  }
}
