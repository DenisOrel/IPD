// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.ICopyNodeID
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>интерфейс для передачи информации о копии документа</summary>
public interface ICopyNodeID
{
  /// <summary>версия копии документа</summary>
  long CopyObjectID { get; }

  /// <summary>шаг жц, на котором находится копия</summary>
  int LСStepID { get; }

  /// <summary>версия документа, для которого создана копия</summary>
  long DocObjectID { get; }

  /// <summary>id документа, для которого создана копия</summary>
  long DocID { get; }

  /// <summary>Заголовок копии</summary>
  string СopyCaption { get; }

  /// <summary>абонент, которому выслана копия</summary>
  long SubscriberID { get; }
}
