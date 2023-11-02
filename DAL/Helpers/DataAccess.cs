using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL.Helpers
{
    public class DataAccess
    {
        public static string ErrorMessage { get; set; }
        public delegate void Transaction();
        private const int Intentos = 3;
        private static Dictionary<Type, SqlDbType> typeMap;

        public static AppSettings appSettings = new AppSettings();
        public static char Session { get; private set; }

        /// <summary>
        /// Wrapper para agregar ejecuciones de comandos en transacciones de IsolatioLevel.Serializable
        /// </summary>
        /// <param name="transaction">Recibe como parámetros las operaciones a realizarse durante la transacción</param>
        public static bool TransactionWrapper(Transaction transaction)
        {
            var options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.Serializable,
                Timeout = TimeSpan.FromSeconds(600)
            };
            //   for (int x = 0; x < Intentos; x++)
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
                    {
                        transaction();

                        scope.Complete();
                    }
                    return true;
                    //    x = Intentos;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    //Exception loggedException = new Exception(string.Format("Intentos de fallidos de transacción {0}", x), exception);
                    //Logging.Logs.LogException(loggedException);
                    //   if (x == Intentos - 1)
                    //throw;
                    return false;

                }
            }
        }
        /// <summary>
        /// Agrega un parámetro a un comando SQL
        /// </summary>
        /// <param name="cmd">SqlCommand Comando SQL</param>
        /// <param name="nombre">Nombre del parámetro</param>
        /// <param name="tipo">Tipo de parámetro SQL</param>
        /// <param name="valor">Valor para asignar al parámetro</param>
        /// <returns>Parámetro inicializado y asignado al comando SQL</returns>
        public static SqlParameter ParameterAdd(SqlCommand cmd, string nombre, SqlDbType tipo, object valor)
        {
            SqlParameter para = new SqlParameter();
            para = cmd.CreateParameter();
            para.ParameterName = nombre;
            para.Value = valor;
            para.SqlDbType = tipo;
            cmd.Parameters.Add(para);
            return para;
        }
        /// <summary>
        /// Crea un comando SQL con base en su nombre y una conexión
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado o query a ejecutar</param>
        /// <param name="cnx">Nombre del objeto de tipo SqlConnection</param>
        /// <returns>Devuelve comando SQL</returns>
        public static SqlCommand CrearSQLComando(string nombreProcedimiento, SqlConnection cnx)
        {
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = nombreProcedimiento;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
        /// <summary>
        /// Crea un comando SQL con base en su nombre y una conexión
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado o query a ejecutar</param>
        /// <param name="cnx">Nombre del objeto de tipo SqlConnection</param>
        /// <returns>Devuelve comando SQL</returns>
        public static SqlCommand CrearSQLComandoQ(string nombreProcedimiento, SqlConnection cnx)
        {
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = nombreProcedimiento;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        /// <summary>
        /// Crea un comando SQL con base en su nombre y una conexión
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado o query a ejecutar</param>
        /// <param name="cnx">Nombre del objeto de tipo SqlConnection</param>
        /// <returns>Devuelve comando SQL</returns>
        public static SqlCommand CrearSQLComando(string nombreProcedimiento)
        {
            //SqlConnection cnx = new SqlConnection(appSettings.ConnectionString);
            SqlConnection cnx = new SqlConnection(appSettings.getTheRightConnection());
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = nombreProcedimiento;
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }
        public static SqlConnection GetConnection(string procedure)
        {
            SqlConnection cnx = new SqlConnection(appSettings.getTheRightConnection());
            return cnx;
        }

        /// <summary>
        /// Crea un comando SQL con base en su nombre y una conexión
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado o query a ejecutar</param>
        /// <param name="cnx">Nombre del objeto de tipo SqlConnection</param>
        /// <returns>Devuelve comando SQL</returns>
        public static SqlCommand CrearSQLComandoQ(string nombreProcedimiento)
        {
            SqlConnection cnx = new SqlConnection(appSettings.ConnectionString);
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandText = nombreProcedimiento;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        /// <summary>
        /// Ejecuta un comando SQL utilizando TransactionScope y devuelve la cantidad de registros afectados
        /// </summary>
        /// <param name="command">Comando de SQL a ejecutar</param>
        /// <returns>Devuelve un entero indicando la cantidad de registros afectados</returns>
        public static int EjecutarSQLNonQueryCTrans(SqlCommand command)
        {
            int renglonesAfectados = -1;
            TransactionWrapper(() =>
            {
                try
                {
                    command.Connection.Open();
                    renglonesAfectados = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }
            });
            return renglonesAfectados;
        }
        /// <summary>
        /// Ejecuta un comando SQL utilizando TransactionScope y devuelve un DataTable con los resultados
        /// </summary>
        /// <param name="command">Comando de SQL a ejecutar</param>
        /// <returns>Dervuelve un Datatable</returns>
        public static DataTable EjecutarSQLSelectCTrans(SqlCommand command)
        {
            DataTable tabla = new DataTable();
            TransactionWrapper(() =>
            {
                try
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    tabla.Load(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }
            });
            return tabla;
        }
        /// <summary>
        /// Ejecutar un comando SQL utilizando TransactionScope y regresa el Scalar del registro afectado
        /// </summary>
        /// <param name="command">Comando SQL a ejecutar</param>
        /// <returns>Devuelve el scalar del registro afectado</returns>
        public static object EjecutarSQLScalarCTrans(SqlCommand command)
        {
            object valorObtenido = "";
            TransactionWrapper(() =>
            {
                try
                {
                    command.Connection.Open();
                    valorObtenido = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }
            });
            return valorObtenido;
        }
        /// <summary>
        /// Ejecuta un comando SQL y devuelve la cantidad de registros afectados
        /// </summary>
        /// <param name="command">Comando de SQL a ejecutar</param>
        /// <returns>Devuelve un entero indicando la cantidad de registros afectados</returns>
        public static int EjecutarSQLNonQuery(SqlCommand command)
        {
            int renglonesAfectados = -1;
            try
            {
                command.Connection.Open();
                renglonesAfectados = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return renglonesAfectados;
        }

        public static async Task<int> EjecutarSQLNonQueryAsync(SqlCommand command)
        {
            int renglonesAfectados = -1;
            try
            {
                command.Connection.Open();
                renglonesAfectados = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return renglonesAfectados;
        }
        /// <summary>
        /// Ejecuta un comando SQL y devuelve un DataTable con los resultados
        /// </summary>
        /// <param name="command">Comando de SQL a ejecutar</param>
        /// <returns>Dervuelve un Datatable</returns>
        public static DataTable EjecutarSQLSelect(SqlCommand command)
        {
            DataTable tabla = new DataTable();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                tabla.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return tabla;
        }
        /// <summary>
        /// Ejecutar un comando SQL y regresa el Scalar del registro afectado
        /// </summary>
        /// <param name="command">Comando SQL a ejecutar</param>
        /// <returns>Devuelve el scalar del registro afectado</returns>
        public static object EjecutarSQLScalar(SqlCommand command)
        {
            object valorObtenido = "";
            try
            {
                command.Connection.Open();
                valorObtenido = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return valorObtenido;
        }

        public static List<SqlParameter> ClassToSqlParameters<T>(T data)
        {
            LoadDictionary();
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));

            List<SqlParameter> ListParam = new List<SqlParameter>();
            object[] values = new object[props.Count];


            for (int i = 0; i < values.Length; i++)
            {
                SqlParameter para = new SqlParameter();
                try
                {
                    para.ParameterName = "@" + props[i].Name;
                    para.Value = props[i].GetValue(data);
                    para.SqlDbType = GetDbType(props[i].PropertyType);
                    ListParam.Add(para);
                }
                catch (Exception e)
                {

                }
            }
            return ListParam;
        }

        public static void LoadDictionary()
        {
            typeMap = new Dictionary<Type, SqlDbType>();

            typeMap[typeof(string)] = SqlDbType.NVarChar;
            typeMap[typeof(char[])] = SqlDbType.NVarChar;
            typeMap[typeof(byte)] = SqlDbType.TinyInt;
            typeMap[typeof(short)] = SqlDbType.SmallInt;
            typeMap[typeof(int)] = SqlDbType.Int;
            typeMap[typeof(long)] = SqlDbType.BigInt;
            typeMap[typeof(byte[])] = SqlDbType.Image;
            typeMap[typeof(bool)] = SqlDbType.Bit;
            typeMap[typeof(DateTime)] = SqlDbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset;
            typeMap[typeof(decimal)] = SqlDbType.Money;
            typeMap[typeof(float)] = SqlDbType.Real;
            typeMap[typeof(double)] = SqlDbType.Decimal;
            typeMap[typeof(TimeSpan)] = SqlDbType.Time;
            /* ... and so on ... */
        }
        public static SqlDbType GetDbType(Type giveType)
        {
            if (typeMap.ContainsKey(giveType))
            {
                return typeMap[giveType];
            }

            throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
        }
        public static SqlDbType GetDbType<T>()
        {
            return GetDbType(typeof(T));
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }
        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
        /// <summary>
        /// Ejecuta un comando SQL y devuelve un JSON con los resultados
        /// </summary>
        /// <param name="command">Comando de SQL a ejecutar</param>
        /// <returns>Dervuelve un Datatable</returns>
        public static StringBuilder EjecutarSQLSelectJSON(SqlCommand command)
        {
            var jsonResult = new StringBuilder();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    jsonResult.Append("[]");
                }
                else
                {
                    while (reader.Read())
                    {
                        jsonResult.Append(reader.GetValue(0).ToString());
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return jsonResult;
        }
    }
}
