// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerConsts
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

#nullable disable
namespace Intermech.FormDesigner;

internal class FormDesignerConsts
{
  /// <summary>Модуль настроек для редактора форм</summary>
  public const string ModuleFormDesigner = "CLIENT";
  /// <summary>Секция настроек для редактора форм</summary>
  public const string SectionFormDesigner = "FORMDESIGNER";
  /// <summary>
  /// Сохранять индексы TabPage у TabControl форм при сохранении форм
  /// </summary>
  public const string ParamSaveTabPageIndices = "SAVETABPAGEINDICES";
  /// <summary>
  /// Значение по умолчанию для параметра сохранения индексов TabPage у TabControl форм при сохранении форм
  /// </summary>
  public const bool DefaultParamSaveTabPageIndices = true;
}
