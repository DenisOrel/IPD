// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.IADIntegratorAPI
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

[ComVisible(true)]
[Guid("AD4B69BC-43EA-4CFD-892E-10E06908CA26")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IADIntegratorAPI
{
  void CreateElementList(string projectFile);

  void CreateSpecification(string projectFile);

  void ImportProject(string projectFile);

  void SaveChanges(string projectFile);

  void ExtendedSave(string projectFile);

  void ViewDocumentProperties(string projectFile);

  int ErrorCode { get; }

  string ErrorMessage { get; }
}
