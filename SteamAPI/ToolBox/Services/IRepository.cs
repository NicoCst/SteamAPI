namespace ToolBox.Services;

public interface IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
{
    IEnumerable<TEntity> GetAll();
    
    TEntity? GetById(TKey id);
    
    TEntity? Create(TEntity entity);
    
    bool Update(TEntity entity);

    bool Delete(TEntity entity);
}