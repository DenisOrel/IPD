// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.IToolsControlPanel
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools;

/// <summary>Интерфейс сервиса окна "Управление инструментами".</summary>
public interface IToolsControlPanel
{
  /// <summary>Добавляет элемент управления в окно.</summary>
  /// <param name="group">Название группы элементов. Если указана пустая строка, то будет использована группа по умолчанию</param>
  /// <param name="item">Добавляемый элемент</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="group" /> не должен быть равен null; параметр <paramref name="item" /> не должен быть равен null</exception>
  void AddItem(string group, Control item);
}
