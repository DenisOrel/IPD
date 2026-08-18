// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Imbase.IImbaseTechObjInfoService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.Imbase;

#nullable disable
namespace Intermech.Interfaces.TechCard.Imbase;

/// <summary>
/// Интерфейс для получения информации Imbase для ТЕХНОЛОГИЧЕСКИХ объектов
/// (а не по объектам справочников / каталогов как у IImbaseObjInfoService)
/// </summary>
public interface IImbaseTechObjInfoService : IImbaseObjInfoService
{
}
