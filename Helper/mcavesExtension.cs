using McavesExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mcavesServices
{
    public static class mcavesExtension
    {
        public static Int32 ToInt32(this object o)
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch
            {
                return 0;
            }
        }
        public static Int32? ToDBFk(this int o)
        {
            if (o > 0)
            {
                return o;
            }
            else
            {
                return null;
            }
        }

        public static string ToText(this object o)
        {
            try
            {
                return o != null ? o.ToString() : "";
            }
            catch
            {
                return "";
            }
        }

        public static string ToJson(this object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch
            {
                return "";
            }
        }

        public static DataTable ToTable(this object o)
        {
            try
            {
                return JsonConvert.DeserializeObject<DataTable>(o.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static List<dynamic> DtTableToList(this DataTable dataTable)
        {
            try
            {
                List<dynamic> list = new List<dynamic>();

                foreach (DataRow row in dataTable.Rows)
                {
                    var obj = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;

                    foreach (DataColumn col in dataTable.Columns)
                    {
                        obj[col.ColumnName] = row[col.ColumnName];
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

       

        public static Boolean ToBoolean(this object o)
        {
            try
            {
                return Convert.ToBoolean(o);
            }
            catch
            {
                return false;
            }
        }

        public static Decimal ToDecimal(this object o)
        {
            try
            {
                return Convert.ToDecimal(o);
            }
            catch
            {
                return 0.0M;
            }
        }
        public static string ToDecimal(this object o, string forma)
        {
            try
            {
                return string.Format(forma, o.ToDecimal());
            }
            catch
            {
                return "0.00";
            }
        }
        public static DateTime? ToDateTime(this object o)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch
            {
                return null;
            }
        }
        public static DateTime ToSqlDateTime(this object o)
        {
            try
            {
                if (o == null) throw new Exception();
                return Convert.ToDateTime(o);
            }
            catch
            {
                return new DateTime(1753, 2, 2);
            }
        }

        public static string ToFormattedDate(this object o)
        {
            try
            {
                return Convert.ToDateTime(o).ToString("yyyy/MM/dd");
            }
            catch
            {
                return "";
            }
        }

        public static DateTime? ToEnglishDate(this object o)
        {
            try
            {
                var chunks = o.ToText().Split('-', '/', '.');
                if (chunks.Length != 3)
                {
                    return null;
                }
                int year = chunks[0].ToInt32();
                int month = chunks[1].ToInt32();
                int day = chunks[2].ToInt32();
                return NepDateConverter.NepToEng(year, month, day);
            }
            catch
            {
                return null;
            }
        }

        public static string ToNepaliDate(this DateTime? dt)
        {
            try
            {
                if (dt == null)
                {
                    return string.Empty;
                }
                else
                {
                    DateTime dtt = Convert.ToDateTime(dt);
                    NepDate nepDate = NepDateConverter.EngToNep(dtt.Year, dtt.Month, dtt.Day);
                    return string.Format("{0}/{1}/{2}", nepDate.Year, nepDate.Month, nepDate.Day);
                }
            }
            catch
            {
                return null;
            }
        }

        public static NepDate ToNepaliDateObj(this DateTime dt)
        {
            try
            {
                NepDate nepDate = NepDateConverter.EngToNep(dt.Year, dt.Month, dt.Day);
                return nepDate;
            }
            catch
            {
                return null;
            }
        }
    }
}
