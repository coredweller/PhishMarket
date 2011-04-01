namespace PhishPond.Repository.LinqToSql
{
    using System.IO;

    public interface ILogWriter
    {
        TextWriter Get();
    }
}
