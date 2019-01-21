using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuditoriaFront
{
    class ConectarSQL
    {
        private string auditSQL = @"
if not exists (select * from sysobjects where name='ARef_Integrity' and xtype='U')CREATE TABLE  ARef_Integrity (
    id int IDENTITY(1,1) PRIMARY KEY,
	TableName		varchar(50)	NOT NULL,
    ForeignKeyName	varchar(50)	NOT NULL,
	FK_Value	varchar(50)	NOT NULL,
    TableNameReference		varchar(50)	NOT NULL,
    Details		varchar(50)	NOT NULL
) 
--SCRIPTTTTTTTTTTTTTTTTTTTTTTTTT

Declare @ForeignKeyName nvarchar(128),
		@TableName nvarchar(128),
		@FKColumnName nvarchar(128),
		@ReferenceTableName nvarchar(128),
		@ReferenceColumnName nvarchar(128),
		@sql nvarchar(4000)

Declare ForeignKeys cursor GLOBAL
FOR SELECT fk.name,
       OBJECT_NAME(fk.parent_object_id),
       COL_NAME(fc.parent_object_id, fc.parent_column_id),
       OBJECT_NAME(fk.referenced_object_id),
       COL_NAME(fc.referenced_object_id, fc.referenced_column_id)
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fc ON fk.OBJECT_ID = fc.constraint_object_id


Open ForeignKeys
FETCH NEXT FROM ForeignKeys INTO @ForeignKeyName, @TableName, @FKColumnName, @ReferenceTableName, @ReferenceColumnName
while(@@fetch_status= 0)
begin
--

SET @SQL = N'
Declare @FK_Value varchar(128)
Declare orphanElements cursor local static
FOR SELECT['+@FKColumnName+'] FROM['+@TableName+'] WHERE['+@FKColumnName+'] NOT IN(SELECT['+@ReferenceColumnName+'] FROM ['+@ReferenceTableName+'])


Open orphanElements
--fetch ForeignKeys into @FK_Value
FETCH NEXT FROM orphanElements INTO @FK_Value
while(@@fetch_status=0)
begin
--

insert ARef_Integrity values('''+ @TableName + ''','''+@ForeignKeyName+''', @FK_Value,'''+@ReferenceTableName+''','' error'')


FETCH NEXT FROM orphanElements INTO @FK_Value
--
end
close orphanElements
'

EXEC sp_executesql @SQL
    
--
FETCH NEXT FROM ForeignKeys INTO @ForeignKeyName, @TableName, @FKColumnName, @ReferenceTableName, @ReferenceColumnName
--
end
close ForeignKeys
deallocate ForeignKeys

select* from ARef_Integrity ";



        private string no_pk_tables_sql = @"SELECT SCHEMA_NAME(t.schema_id) AS schema_name  
    ,t.name AS table_name  
FROM sys.tables t   
WHERE object_id NOT IN   
   (  
    SELECT parent_object_id   
    FROM sys.key_constraints   
    WHERE type_desc = 'PRIMARY_KEY_CONSTRAINT' -- or type = 'PK'  
    );  ";

        private string relation_tables_sql = @"select *
							from sys.foreign_keys";





        public string get_no_pk_tables(string base_de_datos, string raw_sql)
        {
            return ejecutarComando(base_de_datos, no_pk_tables_sql);
        }

        public string get_relation_tables(string base_de_datos, string raw_sql)
        {
            return ejecutarComando(base_de_datos, relation_tables_sql);
            MessageBox.Show(relation_tables_sql);
        }
        private string getConnectionString(string bdd_name)
        {
            string data_source = "DESKTOP-0KBEA3Q"; // nombre del servidor 

            string atributos = "Integrated Security=True"; //atributos adicionales

            string output = "Data Source = " + data_source + ";";
            output += "Initial Catalog = " + bdd_name + ";";
            output += atributos + ";";

            return output;
        }

        public string auditarbase(string base_de_datos, string raw_sql)
        {
            Console.WriteLine("alooooo"+raw_sql+base_de_datos);
            return ejecutarComando(base_de_datos, auditSQL);
        }

        public void anomaliaDeDatos(string base_de_datos)
        {
            const string comillas = "\"";
            string caracter = "\\";
            string consulta = @" EXEC xp_cmdshell 'bcp " + comillas + "SELECT * FROM " + base_de_datos + ".dbo.Resultados" + comillas + " queryout " + comillas + "C:" + caracter + "resultadosIntegridadDatos.txt" + comillas + " -T -c '";
            string procediminetoAlmacendo = @"create procedure IntegridadDatos as 
                begin 
                drop table Resultados 
                create table Resultados(Tabla varchar(50) not null, Llave varchar(100) not null, 
                Valor_que_no_cumple varchar(50) not null) 
                insert into Resultados exec('dbcc checkconstraints with all_constraints')
                EXEC master.dbo.sp_configure 'show advance options',1 
                RECONFIGURE 
                EXEC master.dbo.sp_configure 'xp_cmdshell',1 
                RECONFIGURE" +
                consulta +
                " end";

            string connection = getConnectionString(base_de_datos);
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand comando;
            SqlCommand comando2;

            string ejecutarProcedure = @"EXEC IntegridadDatos";

            try
            {
                conn.Open();
                comando = new SqlCommand(procediminetoAlmacendo, conn);
            
                SqlDataReader reader = comando.ExecuteReader();
                reader.Close();
               
                comando.Dispose();
            
                conn.Close();

                MessageBox.Show("Procedimiento Generado...OK");
            
                conn.Open();
                comando2 = new SqlCommand(ejecutarProcedure, conn);
                SqlDataReader reader2 = comando2.ExecuteReader();
                reader2.Close();

                comando2.Dispose();

                conn.Close();

            }
            catch (Exception err)
            {

                //MessageBox.Show("No se pudo conectar\n" + err.ToString());

                MessageBox.Show("Se genero archivo de reporte en C:");

            }


        }

        public void integridadDeOperaciones(string base_de_datos)
        {
            const string comillas = "\"";
            string caracter = "\\";
            string consulta = @" EXEC xp_cmdshell 'bcp " + comillas + "SELECT * FROM " + base_de_datos + ".dbo.Resultados" + comillas + " queryout " + comillas + "C:" + caracter + "resultadosIntegridadDatos.txt" + comillas + " -T -c '";
            string procediminetoAlmacendo = @"create procedure IntegridadOperaciones as 
            begin
	        drop table OperacionesTriggers;
	        create table OperacionesTriggers 
	        (
		        nombre_Trigger varchar(50) not null, 
		        esquema_tabla varchar(50) not null, 
		        tabla_nombre varchar(50) not null,  
		        isupdate integer not null, 
		        isdelete integer not null, 
		        isinsert integer not null, 
		        isafter integer not null, 
		        isinsteadof integer not null, 
		        disabled_type integer not null
	        );
	        insert into OperacionesTriggers  
	        SELECT  o.name, s.name ,OBJECT_NAME(o.parent_obj) ,OBJECTPROPERTY(o.id, 'ExecIsUpdateTrigger') ,OBJECTPROPERTY(o.id, 'ExecIsDeleteTrigger') ,OBJECTPROPERTY(o.id, 'ExecIsInsertTrigger') ,OBJECTPROPERTY(o.id, 'ExecIsAfterTrigger') ,OBJECTPROPERTY(o.id, 'ExecIsInsteadOfTrigger') ,OBJECTPROPERTY(o.id, 'ExecIsTriggerDisabled')
	        FROM sysobjects AS o

	        INNER JOIN sysobjects AS o2 ON o.parent_obj = o2.id

	        INNER JOIN sysusers AS s ON o2.uid = s.uid

	        WHERE o.type = 'TR';

	        EXEC master.dbo.sp_configure 'show advanced options', 1
	        RECONFIGURE
	        EXEC master.dbo.sp_configure 'xp_cmdshell', 1
	        RECONFIGURE" + consulta +
            "end";

            string connection = getConnectionString(base_de_datos);
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand comando;
            SqlCommand comando2;

            string ejecutarProcedure = @"EXEC IntegridadOperaciones";

            try
            {
                conn.Open();
                comando = new SqlCommand(procediminetoAlmacendo, conn);

                SqlDataReader reader = comando.ExecuteReader();
                reader.Close();

                comando.Dispose();

                conn.Close();

                MessageBox.Show("Procedimiento Generado...OK");

                conn.Open();
                comando2 = new SqlCommand(ejecutarProcedure, conn);
                SqlDataReader reader2 = comando2.ExecuteReader();
                reader2.Close();

                comando2.Dispose();

                conn.Close();

            }
            catch (Exception err)
            {

                //MessageBox.Show("No se pudo conectar\n" + err.ToString());

                MessageBox.Show("Se genero archivo de reporte en C:");

            }
        }

        

        public List<string> getIR(String base_de_datos, string raw_sql)
        {

            return setParsing(base_de_datos, raw_sql);
        }

        public int getFK(String base_de_datos, string raw_sql)
        {
            string connection = getConnectionString(base_de_datos);
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand comando;
            SqlDataReader sql_data_reader;
            try
            {
                conn.Open();
                comando = new SqlCommand(raw_sql, conn);
                sql_data_reader = comando.ExecuteReader();
                int aux = 0;
                while (sql_data_reader.Read())
                {
                    for (int x = 0; x < sql_data_reader.FieldCount; x++)
                    {
                        aux += Int32.Parse(sql_data_reader.GetValue(x) + "");
                    }

                }

                sql_data_reader.Close();
                comando.Dispose();
                conn.Close();
                return aux;
            }
            catch (Exception err)
            {

                MessageBox.Show("No se pudo conectar\n" + err.ToString());
                return 0;
            }

        }




        public List<string> getColName(String base_de_datos, string table_name)
        {
            string TN = @"select COLUMN_NAME
from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table_name + "'";
            return setParsing(base_de_datos, TN);

        }

        public string chequeoBase(string base_de_datos, string raw_sql)
        {
            string dbcc_sql = "DBCC CHECKDB ([" + base_de_datos + "]) with tableresults";
            return ejecutarComando(base_de_datos, dbcc_sql);
        }

        public string ejecutarComando(string base_de_datos, string raw_sql)
        {
            string connection = getConnectionString(base_de_datos);
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand comando;
            SqlDataReader sql_data_reader;
            try
            {
                conn.Open();
                comando = new SqlCommand(raw_sql, conn);
                sql_data_reader = comando.ExecuteReader();
                string aux = "";
                while (sql_data_reader.Read())
                {
                    for (int x = 0; x < sql_data_reader.FieldCount; x++)
                    {
                        aux += sql_data_reader.GetValue(x) + "  ,  ";
                    }
                    aux += "\n";
                }

                sql_data_reader.Close();
                comando.Dispose();
                conn.Close();
                return aux;
            }
            catch (Exception err)
            {

                MessageBox.Show("No se pudo conectar\n" + err.ToString());
                return null;
            }
        }
        public List<string> setParsing(string base_de_datos, string raw_sql)
        {
            string connection = getConnectionString(base_de_datos);
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand comando;
            SqlDataReader sql_data_reader;
            try
            {
                conn.Open();
                comando = new SqlCommand(raw_sql, conn);
                sql_data_reader = comando.ExecuteReader();
                string aux = "";
                List<string> lista = new List<string>();

                while (sql_data_reader.Read())
                {
                    for (int x = 0; x < sql_data_reader.FieldCount; x++)
                    {
                        lista.Add(sql_data_reader.GetValue(x) + "");


                    }


                }

                sql_data_reader.Close();
                comando.Dispose();
                conn.Close();
                return lista;
            }
            catch (Exception err)
            {

                MessageBox.Show("No se pudo conectar\n" + err.ToString());
                return null;
            }
        }

        public int getMatch(String base_de_datos, string table_name, string col_name)
        {
            string query = @"select count(*)
							from INFORMATION_SCHEMA.COLUMNS
							WHERE TABLE_NAME = '" + table_name + "' and COLUMN_NAME = '" + col_name + "'";
            string connection = getConnectionString(base_de_datos);
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand comando;
            SqlDataReader sql_data_reader;
            try
            {
                conn.Open();
                comando = new SqlCommand(query, conn);
                sql_data_reader = comando.ExecuteReader();
                int aux = 0;
                while (sql_data_reader.Read())
                {
                    for (int x = 0; x < sql_data_reader.FieldCount; x++)
                    {
                        aux += Int32.Parse(sql_data_reader.GetValue(x) + "");
                    }

                }

                sql_data_reader.Close();
                comando.Dispose();
                conn.Close();
                return aux;
            }
            catch (Exception err)
            {

                MessageBox.Show("No se pudo conectar\n" + err.ToString());
                return 0;
            }
        }
    }
}
