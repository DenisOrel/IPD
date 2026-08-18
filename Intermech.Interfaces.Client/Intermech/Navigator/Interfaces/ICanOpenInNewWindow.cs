// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ICanOpenInNewWindow
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Вот такой вот загадочый интерфейс нужно просить у ISelectedItems, вместо того,
/// чтобы просить IDescriptor (при создании которого идут обращения в базу данных)
/// </summary>
public interface ICanOpenInNewWindow
{
}
