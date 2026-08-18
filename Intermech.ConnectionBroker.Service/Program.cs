// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.Service.Program
// Assembly: Intermech.ConnectionBroker.Service, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D4CD0278-1F75-45CE-84EB-6440D3E7C8F8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Service.exe

using Intermech.ApplicationModel;

#nullable disable
namespace Intermech.ConnectionBroker.Service;

internal sealed class Program(string[] aruments) : 
  ServiceApplicationBase<IntermechConnectionBrokerService>(aruments)
{
  private static void Main(string[] args) => new Program(args).Run();
}
