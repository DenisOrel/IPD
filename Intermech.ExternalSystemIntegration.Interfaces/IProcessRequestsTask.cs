// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IProcessRequestsTask
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

public interface IProcessRequestsTask
{
  /// <summary>Начать выполнение задачи</summary>
  /// <param name="taskID"></param>
  /// <returns></returns>
  bool ProcessRequestsQueue();
}
