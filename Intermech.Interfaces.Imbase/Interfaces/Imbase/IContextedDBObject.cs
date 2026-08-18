// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IContextedDBObject
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Интерфейс поддерживает для объектов IMBASE свойство ContextID - объект,
/// в контексте которого создается объект. Используется для объектов типа ImbaseTableRecord
/// </summary>
public interface IContextedDBObject
{
  /// <summary>Идентификатор еонтекстного объекта</summary>
  long ContextId { get; set; }
}
