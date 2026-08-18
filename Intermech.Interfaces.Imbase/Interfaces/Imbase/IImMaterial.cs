// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImMaterial
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[ComVisible(true)]
[Guid("D13BB0D5-C3AF-489C-9599-3EFDB478A24E")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IImMaterial
{
  void SetCurrentSystem(int lSystemFlag);

  void SetWorkMode(int lWorkMode);

  void SetInitData(string bsTypesizeImKey, string bsMaterialImKey, int lProfileFolderLevel);

  void SetInitData2(string bsImKey);

  void RunViewer(out string pbsSelectedObjDefinition, out int plIsImKey);

  void GetDescriptionByKey(string bsImKey, out string pbsDescription);

  void GetImbaseMenuItems(string bsImKey, int lFolder, ref string pbsMenuItems);

  void ExecuteCommand(string bsImKey, int lCommand);

  void ClearBase();

  void CanWorkWithBase(out int plValidBase);

  void GetMaterialKeyByTypesize(string bsImKey, out string pbsMaterialImKey);

  void RunViewer2(out string pbsSelectedObjImKey, out string pbsSelectedObjDescription);
}
