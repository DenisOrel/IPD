
// Type: Intermech.Navigator.ObjectTypeNodeFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Класс, позволяющий получить список допустимых или запрещённых типов объектов для узлов деревьев
/// </summary>
public class ObjectTypeNodeFilter : IObjectTypeNodeFilter
{
  /// <summary>Список разрешённых типов объектов</summary>
  private List<int> _enabledObjectTypes = new List<int>(0);
  /// <summary>Список запрещённых типов объектов</summary>
  private List<int> _disabledObjectTypes = new List<int>(0);

  /// <summary>Создать неинициализированный экземпляр класса</summary>
  public ObjectTypeNodeFilter()
  {
  }

  /// <summary>
  /// Создать экземпляр класса со списком разрешённых типов объектов
  /// </summary>
  /// <param name="enabledObjectTypes">Список разрешённых типов объектов</param>
  public ObjectTypeNodeFilter(int[] enabledObjectTypes)
  {
    if (enabledObjectTypes == null)
      return;
    for (int index = 0; index < enabledObjectTypes.Length; ++index)
    {
      if (!this._enabledObjectTypes.Contains(enabledObjectTypes[index]))
        this._enabledObjectTypes.Add(enabledObjectTypes[index]);
    }
  }

  /// <summary>
  /// Создать экземпляр класса со списком разрешённых типов объектов
  /// </summary>
  /// <param name="enabledObjectTypes">Список разрешённых типов объектов</param>
  public ObjectTypeNodeFilter(List<int> enabledObjectTypes)
  {
    if (enabledObjectTypes == null)
      return;
    for (int index = 0; index < enabledObjectTypes.Count; ++index)
    {
      if (!this._enabledObjectTypes.Contains(enabledObjectTypes[index]))
        this._enabledObjectTypes.Add(enabledObjectTypes[index]);
    }
  }

  /// <summary>
  /// Создать экземпляр класса со списками разрешённых и запрещённых типов объектов
  /// </summary>
  /// <param name="enabledObjectTypes">Список разрешённых типов объектов</param>
  /// <param name="disabledObjectTypes">Список запрещённых типов объектов</param>
  public ObjectTypeNodeFilter(int[] enabledObjectTypes, int[] disabledObjectTypes)
  {
    if (enabledObjectTypes != null)
    {
      for (int index = 0; index < enabledObjectTypes.Length; ++index)
      {
        if (!this._enabledObjectTypes.Contains(enabledObjectTypes[index]))
          this._enabledObjectTypes.Add(enabledObjectTypes[index]);
      }
    }
    if (disabledObjectTypes == null)
      return;
    for (int index = 0; index < disabledObjectTypes.Length; ++index)
    {
      if (!this._disabledObjectTypes.Contains(disabledObjectTypes[index]))
        this._disabledObjectTypes.Add(disabledObjectTypes[index]);
    }
  }

  /// <summary>
  /// Создать экземпляр класса со списками разрешённых и запрещённых типов объектов
  /// </summary>
  /// <param name="enabledObjectTypes">Список разрешённых типов объектов</param>
  /// <param name="disabledObjectTypes">Список запрещённых типов объектов</param>
  public ObjectTypeNodeFilter(List<int> enabledObjectTypes, List<int> disabledObjectTypes)
  {
    if (enabledObjectTypes != null)
    {
      for (int index = 0; index < enabledObjectTypes.Count; ++index)
      {
        if (!this._enabledObjectTypes.Contains(enabledObjectTypes[index]))
          this._enabledObjectTypes.Add(enabledObjectTypes[index]);
      }
    }
    if (disabledObjectTypes == null)
      return;
    for (int index = 0; index < disabledObjectTypes.Count; ++index)
    {
      if (!this._disabledObjectTypes.Contains(disabledObjectTypes[index]))
        this._disabledObjectTypes.Add(disabledObjectTypes[index]);
    }
  }

  /// <summary>Список разрешённых типов объектов</summary>
  public List<int> EnabledObjectTypes => this._enabledObjectTypes;

  /// <summary>Список запрещённых типов объектов</summary>
  public List<int> DisabledObjectTypes => this._disabledObjectTypes;
}
