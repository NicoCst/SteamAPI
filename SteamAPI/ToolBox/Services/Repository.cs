namespace ToolBox.Services;

public abstract class Repository
{

    protected string ConnectionString;
    public Repository(string connectionString)
    {
        ConnectionString = connectionString;
    }
}