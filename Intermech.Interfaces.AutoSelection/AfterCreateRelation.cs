// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AfterCreateRelation
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>Событие после создания связи</summary>
/// <param name="sender">объект-отправитель сообщения</param>
/// <param name="e">Аргументы события</param>
public delegate void AfterCreateRelation(object sender, RelationEventArgs e);
