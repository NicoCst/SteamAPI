using ToolBox.Services;

namespace DAL.Entities;

public class Game : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Version { get; set; }
    public int UserDevId { get; set; }
}