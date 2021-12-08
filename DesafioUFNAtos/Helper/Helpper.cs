using DesafioUFNAtos.Entities;

namespace DesafioUFNAtos.Helper
{
    public class Helpper
    {
        public string Path { get; set; }

        public Helpper(string path)
        {
            Path = path;
        }

        private List<string> LerArquivo()
        {
            string linha;
            List<string> linhas = new();
            using (StreamReader sr = File.OpenText(Path))
            {
                while ((linha = sr.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }
            return linhas;
        }

        public string ImportarEntidades()
        {
            int countA = 0, countP = 0;
            List<Pessoa> pessoas = new();
            var linhas = LerArquivo();

            foreach (var dado in linhas)
            {
                string[] novaEntidade = dado.Split("-");
                if (novaEntidade[0] == "Z")
                {
                    Pessoa p = new(novaEntidade[1], novaEntidade[2], novaEntidade[3], novaEntidade[4], novaEntidade[5]);

                    if (p.ConsultaPessoa(p.Nome) == null)
                    {
                        p.GravarPessoa();
                        pessoas.Add(p);
                        countP++;
                    }
                }
                else if (novaEntidade[0] == "Y")
                {
                    if(pessoas.Count != 0)
                    {
                        Pessoa p = pessoas.Last();
                        p = p.ConsultaPessoa(p.Nome);
                        Aluno a = new(int.Parse(novaEntidade[1]), novaEntidade[2], novaEntidade[3], p.Id);
                        a.GravarAluno();
                        countA++;
                    }
                }
            }

            if (countP != 0)
                return $"Foram registrados {countP} pessoas, sendo elas, {countA} alunos!";
            
            else if (countA != 0)
                return $"Todos os dados de pessoas já estavam cadastrados, porém, foram adicionados {countA} alunos!";
            
            else
                return $"Todos dados informados já estavam cadastrados!";
        }
    }
}
