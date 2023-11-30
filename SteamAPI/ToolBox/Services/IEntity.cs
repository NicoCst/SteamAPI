namespace ToolBox.Services;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}