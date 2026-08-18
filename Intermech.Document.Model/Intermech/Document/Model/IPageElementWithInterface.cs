// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.IPageElementWithInterface
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Интерфейс для элементов страницы с пользовательским интерфейсом</summary>
public interface IPageElementWithInterface
{
  /// <summary>Контейнер для управления размерами и положением прямоугольного
  /// элемента управления</summary>
  PageElementUI PageUI { get; set; }

  /// <summary>Создать объекты интерфейса пользователя</summary>
  void CreateUI();

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  void DestroyUI();

  /// <summary>Объекты интерфейса пользователя нужны</summary>
  bool NeedUI { get; }

  /// <summary>Добавить и связать объекты интерфейса пользователя</summary>
  /// <param name="child">Дочерний узел</param>
  void AddChildUI(DocumentTreeNode child, bool createUI);

  /// <summary>Редактор на месте</summary>
  bool IsInPlaceEditor { get; }

  /// <summary>Можно активировать редактирование по месту</summary>
  bool CanActivateInPlaceEditor { get; }

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="pageUI">Элемент управления в контексте которого должен быть редактор</param>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  void ActivateInPlaceEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs);

  /// <summary>Событие перед активацией редактора по месту</summary>
  event CancelEventHandler InplaceEditorActivating;

  /// <summary>Событие после активации редактора по месту</summary>
  event EventHandler InplaceEditorActivated;

  /// <summary>Деактивировать радактор на месте</summary>
  void DeactivateInPlaceEditor();

  /// <summary>Событие перед деактивацией редактора по месту</summary>
  event CancelEventHandler InplaceEditorDeactivating;

  /// <summary>Событие после деактивации редактора по месту</summary>
  event EventHandler InplaceEditorDeactivated;

  /// <summary>Редактор для редактирования по месту активен</summary>
  bool InPlaceEditorActive { get; }

  /// <summary>Контрол редактора по месту</summary>
  Control InPlaceEditorControl { get; }
}
