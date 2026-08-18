// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ModelMappingException
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Базовый класс для всех ошибок отображения доменной модели в базу данных.
/// </summary>
public class ModelMappingException(string message) : EntityException(message)
{
}
