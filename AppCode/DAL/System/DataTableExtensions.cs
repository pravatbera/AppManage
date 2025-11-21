using System.Data;
using System.Reflection;

namespace AppManage.AppCode.DAL.System
{
    public static class DataTableExtensions
    {
        private static T GetItem<T>(DataRow dr, bool isColCaseSensitive = true)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (isColCaseSensitive)
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            if (dr[column.ColumnName] == DBNull.Value)
                                pro.SetValue(obj, null, null);
                            else
                                pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if (string.Equals(pro.Name, column.ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            if (dr[column.ColumnName] == DBNull.Value)
                                pro.SetValue(obj, null, null);
                            else
                                pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        else
                            continue;
                    }
                }
            }
            return obj;
        }

        private static T GetItem<T>(DataRow dr, List<ColMap> colMap, bool isColCaseSensitive)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            colMap.ForEach(t =>
            {
                var dataColName = (t == null ? string.Empty : t.ColSource) ?? string.Empty;
                var modColName = (t == null ? string.Empty : t.ColTergate) ?? string.Empty;

                PropertyInfo pro = isColCaseSensitive ? temp.GetProperties().FirstOrDefault(pr => pr.Name == modColName) : temp.GetProperties().FirstOrDefault(pr => pr.Name.Equals(modColName, StringComparison.OrdinalIgnoreCase));
                var dataColVal = dr[dataColName];
                if (pro != null)
                {
                    if (dataColVal == DBNull.Value)
                        pro.SetValue(obj, null, null);
                    else
                        pro.SetValue(obj, dataColVal, null);
                }
            });
            return obj;
        }
        public static List<T> Convert<T>(this DataTable dt, bool isColCaseSensitive = true)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row, isColCaseSensitive);
                data.Add(item);
            }
            return data;
        }
        public static List<T> Convert<T>(this EnumerableRowCollection rows, bool isColCaseSensitive = true)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in rows)
            {
                T item = GetItem<T>(row, isColCaseSensitive);
                data.Add(item);
            }
            return data;
        }
        public static List<T> Convert<T>(this IEnumerable<DataRow> rows, bool isColCaseSensitive = true)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in rows)
            {
                T item = GetItem<T>(row, isColCaseSensitive);
                data.Add(item);
            }
            return data;
        }
        public static List<T> Convert<T>(this DataTable dt, List<ColMap> colMaps, bool isColCaseSensitive = false)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                if (colMaps != null && colMaps.Count > 0)
                {
                    T item = GetItem<T>(row, colMaps, isColCaseSensitive);
                    data.Add(item);
                }
                else
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            return data;
        }
        [Obsolete("Not Tested", true)]
        public static List<T> Convert<T>(this DataTable dt, List<ColTypeMap> colMaps)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public class ColMap
        {
            /// <summary>
            /// Datatable Column Name
            /// </summary>
            public string ColSource { get; set; }
            /// <summary>
            /// Model's Column Name
            /// </summary>
            public string ColTergate { get; set; }
        }
        public class ColTypeMap : ColMap
        {
            public Type Type { get; set; }
        }
    }
}
