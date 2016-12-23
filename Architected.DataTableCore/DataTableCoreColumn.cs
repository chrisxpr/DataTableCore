using System.Data;

namespace Architected.DataTableCore
{
    public class DataTableCoreColumn
    {
        private string _name;
        private SqlDbType _type;
        private long _size;

        public string Name => _name;
        public SqlDbType Type => _type;
        public long Size => _size;

        public DataTableCoreColumn(string name, SqlDbType type, long size)
        {
            _name = name;
            _type = type;
            _size = size;
        }

        public DataTableCoreColumn(string name, SqlDbType type)
        {
            _name = name;
            _type = type;
            _size = 0;
        }
    }
}
