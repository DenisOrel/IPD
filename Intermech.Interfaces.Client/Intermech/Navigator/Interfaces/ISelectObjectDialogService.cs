// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISelectObjectDialogService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Служба вызова диалога выбора объектов различных типов</summary>
public interface ISelectObjectDialogService
{
  void Register(int typeID, IDescriptor rootDescriptor);

  IDescriptor GetDescriptor(int typeID);
}
