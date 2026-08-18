// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPRelationRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Интерфейс, ссылающийся на описание связи</summary>
public interface IMRPRelationRef : IMRPGuidItem, IMRPTypedItem, IMRPUpdateableItemRef, IMRPContext
{
  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  bool IsNewRelation { get; }

  /// <summary>Идентификатор версии родительского объекта</summary>
  long ProjectID { get; }

  /// <summary>Идентификатор связи</summary>
  long PrjLinkID { get; }
}
