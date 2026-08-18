// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DeleteIDEvenArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DatabaseConfigurator;

/// <summary>
/// Объявим спец класс - наследника от EventArgs для уведомлении о удалении
/// элемента в категории
/// </summary>
[Serializable]
public class DeleteIDEvenArgs : EventArgs
{
}
