
// Type: Intermech.Navigator.DBObjects.ProjectObjectID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс, в котором хранится идентификатор версии объекта с проектом
/// </summary>
public class ProjectObjectID
{
  /// <summary>Идентификатор проекта</summary>
  public long ProjectID;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="projectID">Идентификатор проекта</param>
  public ProjectObjectID(long projectID) => this.ProjectID = projectID;
}
