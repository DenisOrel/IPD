namespace Intermech.Map
{
    public interface IMapGraphPart
    {
      MapObject MapObject { get; set; }

      int UserFlags { get; set; }

      object UserObject { get; set; }
    }
}
