namespace Yaf.Repository.LinqToSql
{
    using System.IO;

    public interface ILogWriter
    {
        TextWriter Get();
    }
}
