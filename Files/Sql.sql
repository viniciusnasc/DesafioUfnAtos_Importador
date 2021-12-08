create table Pessoa(
        Id int not null identity primary key,
        Nome varchar(50) not null,
        Telefone varchar(20),
		Cidade varchar(20),
        Rg varchar(20),
		Cpf varchar(20)
      )
	  
	  create table Aluno(
	  Matricula int not null primary key,
	  CodCurso varchar(20) not null,
	  NomeCurso varchar(30) not null,
	  Pessoa int not null,

	  Constraint Fk_Pessoa Foreign key(Pessoa)
	  references Pessoa(Id)
	  )

	  drop table Pessoa
	  drop table Aluno
	  select * from Pessoa
	  select * from Aluno

	  select Pessoa.Nome, Aluno.NomeCurso 
	  From Aluno
	  Inner Join Pessoa on Aluno.Pessoa = Pessoa.Id