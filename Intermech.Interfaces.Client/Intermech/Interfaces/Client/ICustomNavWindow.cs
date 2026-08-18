// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICustomNavWindow
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, который должен поддерживаться контекстом редактирования (дескриптором или быть в наборе сервисов), функции которого должы
/// возвращать тип окна - потомка NavWindow. Таким образом OpenNewWindow (открыть в новом окне будет создавать окна этого типа)
/// </summary>
public interface ICustomNavWindow
{
  Type GetNavWindowType(IServiceProvider context, IDescriptor rootDescriptor);
}
