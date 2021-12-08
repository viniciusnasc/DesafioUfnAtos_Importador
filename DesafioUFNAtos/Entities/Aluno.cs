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
    public class Aluno : Pessoa
    {
        public int Matricula { get; set; }
        public string CodCurso { get; set; }
        public string NomeCurso { get; set; }
        public int Pessoa { get; set; }

        public Aluno(int matricula, string codCurso, string nomeCurso, int pessoa)
        {
            Matricula = matricula;
            CodCurso = codCurso;
            NomeCurso = nomeCurso;
            Pessoa = pessoa;
        }

        public bool GravarAluno()
        {
            Contexto bd = new();

            SqlConnection cn = bd.AbrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = $"insert into Aluno values(@Matricula, @CodCurso, @NomeCurso, @Pessoa)";
            command.Parameters.Add("@Matricula", SqlDbType.Int);
            command.Parameters.Add("@CodCurso", SqlDbType.VarChar);
            command.Parameters.Add("@NomeCurso", SqlDbType.VarChar);
            command.Parameters.Add("@Pessoa", SqlDbType.Int);
            command.Parameters[0].Value = Matricula;
            command.Parameters[1].Value = CodCurso;
            command.Parameters[2].Value = NomeCurso;
            command.Parameters[3].Value = Pessoa;

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
    }
}
