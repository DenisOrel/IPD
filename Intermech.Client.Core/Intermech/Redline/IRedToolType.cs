
// Type: Intermech.Redline.IRedToolType
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Redline;

/// <summary>Типы  пометок</summary>
public enum IRedToolType
{
  /// <summary>нет пометок</summary>
  tNone,
  /// <summary>растояние</summary>
  [RedCommand("RedDistance"), RedTool(typeof (DistanceTool))] Distance,
  /// <summary>элипс</summary>
  [RedCommand("RedEllipse"), RedTool(typeof (RedLineEllipseTool))] tEllipse,
  /// <summary>элипс с заливкой</summary>
  [RedCommand("RedEllipseFill"), RedTool(typeof (RedLineEllipseFillTool))] tEllipseFill,
  /// <summary>окружность</summary>
  [RedCommand("RedCircle"), RedTool(typeof (RedLineCircleTool))] tCircle,
  /// <summary>окружность с заливкой</summary>
  [RedCommand("RedCircleFill"), RedTool(typeof (RedLineCircleFillTool))] tCircleFill,
  /// <summary>прямоугольник</summary>
  [RedCommand("RedRectangle"), RedTool(typeof (RedLineRectangleTool))] tRectangle,
  /// <summary>прямоугольник с заливкой</summary>
  [RedCommand("RedRectangleFill"), RedTool(typeof (RedLineRectangleFillTool))] tRectangleFill,
  /// <summary>линия</summary>
  [RedCommand("RedLine"), RedTool(typeof (RedLineStrokeTool))] tStroke,
  /// <summary>карандаш</summary>
  [RedCommand("RedPencil"), RedTool(typeof (RedLinePencilTool))] tPencil,
  /// <summary>коментарий</summary>
  [RedCommand("RedNote"), RedTool(typeof (RedLineNoteTool))] tNote,
}
