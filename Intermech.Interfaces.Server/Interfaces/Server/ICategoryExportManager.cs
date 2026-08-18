// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ICategoryExportManager
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Briefcase;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface ICategoryExportManager
{
  void RegisterCategoryExport(int category, ICategoryExport iCategoryExport);

  void UnregisterCategoryExport(int category, ICategoryExport iCategoryExport);

  ICategoryExport[] GetRegisteredCategoryExport(int category);
}
