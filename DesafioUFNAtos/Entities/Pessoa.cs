using DesafioUFNAtos.Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioUFNAtos.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }

        public Pessoa()
        {} 

        public Pessoa(string nome, string telefone, string cidade, string rg, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            Cidade = cidade;
            Rg = rg;
            Cpf = cpf;
        }

        public bool GravarPessoa()
        {
            Contexto bd = new();

            SqlConnection cn = bd.AbrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = $"insert into Pessoa values(@Nome, @Telefone, @Cidade, @Rg, @Cpf)";
            command.Parameters.Add("@Nome", SqlDbType.VarChar);
            command.Parameters.Add("@Telefone", SqlDbType.VarChar);
            command.Parameters.Add("@Cidade", SqlDbType.VarChar);
            command.Parameters.Add("@Rg", SqlDbType.VarChar);
            command.Parameters.Add("@Cpf", SqlDbType.VarChar);
            command.Parameters[0].Value = Nome;
            command.Parameters[1].Value = Telefone;
            command.Parameters[2].Value = Cidade;
            command.Parameters[3].Value = Rg;
            command.Parameters[4].Value = Cpf;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                bd.FecharConexao();
            }
        }

        public Pessoa ConsultaPessoa(string nome)
        {
            Contexto bd = new();
            try
            {
                SqlConnection cn = bd.AbrirConexao();
                SqlCommand command = new("select * from Pessoa", cn);

                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(1) == nome)
                    {
                        Id = rdr.GetInt32(0);
                        Nome = rdr.GetString(1);
                        Telefone = rdr.GetString(2);
                        Cidade = rdr.GetString(3);
                        Rg = rdr.GetString(4);
                        Cpf = rdr.GetString(5);
                        return this;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                bd.FecharConexao();
            }
        }
    }
}
