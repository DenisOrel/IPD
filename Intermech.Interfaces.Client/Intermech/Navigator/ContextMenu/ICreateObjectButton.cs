// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.ICreateObjectButton
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;
using System.Drawing;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Интерфейс, управляющий кнопкой "Создать ..." на панели главной формы
/// </summary>
public interface ICreateObjectButton
{
  /// <summary>
  /// Назначить кнопке "Создать ..." значок, соответствующий указанному типу объектов
  /// </summary>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="MRUItem">Описание</param>
  void BtnNewObjTypeIcon(int objTypeID, IMRUItem MRUItem);

  /// <summary>Индекс значка кнопки "Создать ..."</summary>
  int BtnNewImageIndex { get; set; }

  /// <summary>Изображение (Image) кнопки "Создать ..."</summary>
  Image BtnNewImage { get; set; }

  /// <summary>Значок (Icon) кнопки "Создать ..."</summary>
  Icon BtnNewIcon { get; set; }

  /// <summary>
  /// Сбросить значок на стандартный, удалить ссылку на MRUItem из тега кнопки
  /// </summary>
  void ResetIcon();
}
