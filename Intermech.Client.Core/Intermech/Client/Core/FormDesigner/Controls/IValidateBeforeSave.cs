
// Type: Intermech.Client.Core.FormDesigner.Controls.IValidateBeforeSave
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Интерфейс наследуют контролы, у которых перед сохранением данных необходимо выполнить некоторые действия.
/// В частности создавался по просьбе О.Лембиевского для его контрола, в котором перед сохранением необходимо проверять правильность заполнения.
/// </summary>
public interface IValidateBeforeSave
{
  /// <summary>
  /// 
  /// </summary>
  void Validate();
}
