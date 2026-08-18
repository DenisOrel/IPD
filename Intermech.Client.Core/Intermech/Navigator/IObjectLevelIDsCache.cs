
// Type: Intermech.Navigator.IObjectLevelIDsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Кэш уровней продвижения</summary>
public interface IObjectLevelIDsCache
{
  /// <summary>Получить название уровня продвижения</summary>
  /// <param name="levelID">Идентификатор уровня продвижения</param>
  /// <returns>Название уровня продвижения</returns>
  string GetName(int levelID);

  /// <summary>Получить значок уровня продвижения</summary>
  /// <param name="name">Название уровня продвижения</param>
  /// <returns>Значок уровня продвижения</returns>
  Icon GetIcon(string name);

  /// <summary>
  /// Получить список изображений со значками уровней продвижения
  /// </summary>
  ImageList ImageList { get; }

  /// <summary>Получить описание уровня продвижения по его ID</summary>
  /// <param name="levelID">Идентификатор уровня продвижения</param>
  /// <returns>Описание уровня продвижения по его ID</returns>
  LCLevel GetLCLevel(int levelID);
}
