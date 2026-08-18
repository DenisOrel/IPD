// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.StatusEnum
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

/// <summary>Множество - Статус запроса</summary>
/// 
///             0=Ожидание, 1=В работе, 2=Ошибка, 3=Запрос сформирован, 4=Ответ получен, 5=Запрос обработан
public enum StatusEnum
{
  Wait,
  Work,
  Error,
  RequestCreate,
  ResponceRecive,
  Done,
}
