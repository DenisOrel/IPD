// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IFormDesignerEditorHook
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс для внесения изменений в дизайнер форм</summary>
public interface IFormDesignerEditorHook
{
  /// <summary>Можно ли вызывать изменения</summary>
  bool CanExecuteSelector { get; }

  /// <summary>Вызов отображения формы выбора атрибутов</summary>
  /// <param name="value">исходное значение AttributeInfo типа атрибута</param>
  /// <param name="pd">информация о редактируемом свойстве</param>
  /// <param name="context">объект в контексте которого произошел вызов изменения</param>
  /// <returns>Если пользователь выбрал и нажал "OK"</returns>
  bool ExecuteSelector(object context, PropertyDescriptor pd, ref object value);
}
