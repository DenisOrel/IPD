// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InvokeServiceMethod`1
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Делегат анонимного метода с возвращаемым значением, используемый сервисом IInvokeService.
/// </summary>
/// <typeparam name="T">Тип значения, возвращаемого анонимным методом</typeparam>
[Obsolete("Use the Func<T> delegate type instead of this type.", true)]
public delegate T InvokeServiceMethod<T>();
