using System.Data;

namespace Architected.DataTableCore
{
    public class DataTableColumn
    {
        public string Name { get; }

        public SqlDbType Type { get; }

        public long Size { get; }

        public DataTableColumn(string name, SqlDbType type, long size)
        {
            Name = name;
            Type = type;
            Size = size;
        }

        public DataTableColumn(string name, SqlDbType type)
        {
            Name = name;
            Type = type;
            Size = 0;
        }
    }
}
