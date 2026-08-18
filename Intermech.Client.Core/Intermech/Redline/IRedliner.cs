
// Type: Intermech.Redline.IRedliner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Redline;

/// <summary> Интерфейс для обеспечения работы RedLine </summary>
public interface IRedliner
{
  /// <summary> Данные были изменены </summary>
  event EventHandler Changed;

  /// <summary> настроить на рисование отрезков </summary>
  void DrawLine();

  /// <summary> настроить на рисование эллипса </summary>
  void DrawEllipse();

  /// <summary> настроить на рисование линии движением мыши </summary>
  void DrawPencil();

  /// <summary> настроить на рисование заметки </summary>
  void DrawNote();

  /// <summary>настроить на измерение отрезков </summary>
  void Distance();

  /// <summary> отменить настройки на рисование</summary>
  void CancelDraw();

  /// <summary>откат, если возможно</summary>
  void Undo();

  /// <summary>откат полностью, если возможно</summary>
  void UndoAll();

  /// <summary> отменить откат, если возможно </summary>
  void Redo();

  /// <summary>проверка: можно ли выполнить откат</summary>
  bool CanUndo { get; }

  /// <summary>проверка: можно ли выполнить отмену отката</summary>
  bool CanRedo { get; }

  /// <summary> Изменились ли данные в Redline? </summary>
  bool Dirty { get; }

  /// <summary> настроить на изменение данных в Redline </summary>
  bool Select { get; set; }

  /// <summary> цвет примитивов  Redline </summary>
  Color Color { get; set; }

  DashStyle PropertyStyle { get; set; }

  /// <summary> видовое окно связанное с документом </summary>
  MapView View { get; }

  /// <summary>
  /// Контекст отображаемого объекта. Например страница многостраничного документа, схема чертежа,
  /// блок или что то другое
  /// </summary>
  object Context { get; set; }

  /// <summary> сделать видимым слой пользователя </summary>
  /// <param name="id">ID слоя</param>
  void SetVisibleLayer(object id);

  /// <summary>слой работающего пользователя</summary>
  object CurrentUserLayer { get; }

  /// <summary>Список слоев Redline</summary>
  object[] Layers { get; }

  /// <summary>Сохранить данные </summary>
  /// <returns>Данные</returns>
  string Save();

  /// <summary> Восстановить данные </summary>
  /// <param name="data">Данные</param>
  void Load(string data);
}
