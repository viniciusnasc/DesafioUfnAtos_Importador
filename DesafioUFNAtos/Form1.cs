using DesafioUFNAtos.Banco;
using DesafioUFNAtos.Entities;
using DesafioUFNAtos.Helper;
using System.Data;

namespace DesafioUFNAtos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // C:\Users\Vini\Desktop\desafio1.txt
            try
            {
                openFileDialog1.ShowDialog();
                Helpper helper = new(openFileDialog1.FileName);
                MessageBox.Show(helper.ImportarEntidades());
                MostrarAlunosCurso();
            }
            catch
            {
                MessageBox.Show("Arquivo não encontrado!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MostrarAlunosCurso();
        }

        private void MostrarAlunosCurso()
        {
            Contexto banco = new();
            string sql = "select Pessoa.Nome, Aluno.NomeCurso From Aluno Inner Join Pessoa on Aluno.Pessoa = Pessoa.Id";
            DataTable dt = new();
            dt = banco.ExecutarConsultaGenerica(sql);
            dataGridView1.DataSource = dt;
        }
    }
}