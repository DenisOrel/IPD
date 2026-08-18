// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumFormat
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Forums;

/// <summary>Для каких объектов отображать форум</summary>
public enum ForumFormat
{
  /// <summary>
  /// Содержимое обсуждения
  /// (закладка открыта для самого объекта Обсуждение)
  /// </summary>
  None = -1, // 0xFFFFFFFF
  /// <summary>Для версии</summary>
  Version = 0,
  /// <summary>Для всех версий объекта</summary>
  Object = 1,
  /// <summary>Видимый состав. На один уровень</summary>
  VisibleComposition = 2,
  /// <summary>Видимый состав. На все уровни</summary>
  FullVisibleComposition = 3,
  /// <summary>
  /// Изменения
  /// (объекты, имеющие одинаковый и не нулевой номер группы изменений)
  /// </summary>
  Changes = 4,
}
