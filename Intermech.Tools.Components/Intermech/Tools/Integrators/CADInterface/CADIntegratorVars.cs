// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADIntegratorVars
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public static class CADIntegratorVars
{
  /// <summary>
  /// Переключатель, позволяющий активировать режим импорта файлов, при котором не выполняется импорт
  /// связанных документов по ассоциативным (вспомогательным) связям. Связи такого типа, как правило,
  /// не влияют на работоспособность импортируемого документа.
  /// </summary>
  public static readonly DynamicVariable<bool> DontImportAssociativeDependencies = new DynamicVariable<bool>("CADIntegratorVars.DontImportAssociativeDependencies", false);
}
