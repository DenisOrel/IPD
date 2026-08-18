namespace Intermech.Map
{
    public interface IMapHandle
    {
      MapObject MapObject { get; }

      MapObject HandledObject { get; }

      int HandleID { get; set; }

      MapObject SelectedObject { get; set; }
    }
}
