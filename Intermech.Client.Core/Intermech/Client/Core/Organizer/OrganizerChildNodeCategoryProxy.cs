
// Type: Intermech.Client.Core.Organizer.OrganizerChildNodeCategoryProxy
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Класс, в котором хранится значение ссылки на объект, а также его название.
/// </summary>
internal class OrganizerChildNodeCategoryProxy
{
  /// <summary>Идентификатор типа узла.</summary>
  internal int ID = -1;
  /// <summary>Заголовок объекта.</summary>
  internal string Name = string.Empty;

  /// <summary>Конструктор.</summary>
  /// <param name="id">Идентификатор версии объекта</param>
  public OrganizerChildNodeCategoryProxy(int id)
  {
    this.ID = id;
    if (id == -1)
      return;
    if (ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service)
    {
      Dictionary<int, string> nodesCaption = service.NodesCaption;
      if (nodesCaption != null)
      {
        foreach (KeyValuePair<int, string> keyValuePair in nodesCaption)
        {
          if (id == keyValuePair.Key)
          {
            this.Name = keyValuePair.Value;
            break;
          }
        }
      }
    }
    if (id != MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545"))
      return;
    this.Name = LocalizationHolder.rm.GetString("Organaizer_TaskCaption");
  }

  /// <summary>Конструктор.</summary>
  /// <param name="id">Идентификатор версии объекта</param>
  /// <param name="name"></param>
  public OrganizerChildNodeCategoryProxy(int id, string name)
  {
    this.ID = id;
    this.Name = name;
  }

  /// <summary>Получить значение класса в виде строки.</summary>
  /// <returns>Значение класса в виде строки</returns>
  public override string ToString() => this.Name;
}
