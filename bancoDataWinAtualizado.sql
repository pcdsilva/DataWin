CREATE TABLE professores (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nome VARCHAR(50) NOT NULL,
  cargo VARCHAR(150),
  titulacao  VARCHAR(150),
  telefone INTEGER,
  email VARCHAR(50) NOT NULL
);

CREATE TABLE alunos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nome VARCHAR(50) NOT NULL,
  telefone CHAR(14) NOT NULL,
  email VARCHAR(50) NOT NULL,
  endereco VARCHAR(200) NOT NULL,
  data_nascimento DATE NULL,
  cpf VARCHAR(11) NOT NULL
);

CREATE TABLE cursos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nome VARCHAR(50) NOT NULL,
  requisito VARCHAR(255) NULL,
  carga_horaria SMALLINT  NULL,
  preco DECIMAL
);

CREATE TABLE turmas (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nome VARCHAR(50) NOT NULL,
  professores_id INT NOT NULL,
  cursos_id INTEGER  NOT NULL,
  carga_horaria SMALLINT  NULL,
  data_inicio DATE NULL,
  data_final DATE NULL,
  INDEX turmas_FKIndex1(cursos_id),
  INDEX turmas_FKIndex2(professores_id),
  FOREIGN KEY(cursos_id)
    REFERENCES cursos(id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(professores_id)
    REFERENCES professores(id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);

CREATE TABLE matriculas (
  id INT IDENTITY(1,1) PRIMARY KEY,
  turmas_id INTEGER  NOT NULL,
  alunos_id INT NOT NULL,
  data_matricula DATE NULL,
  valor_matricula DECIMAL,
  INDEX matriculas_FKIndex3(turmas_id),
  INDEX matriculas_FKIndex1(alunos_id),
  FOREIGN KEY(turmas_id)
    REFERENCES turmas(id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(alunos_id)
    REFERENCES alunos(id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);

CREATE TABLE notas(
	id INT IDENTITY(1,1) PRIMARY KEY,
	turmas_id INTEGER  NOT NULL,
	alunos_id INT NOT NULL,
	valor_nota DECIMAL,
	INDEX notas_FKIndex3(turmas_id),
	INDEX notas_FKIndex1(alunos_id),
	FOREIGN KEY(turmas_id)
    REFERENCES turmas(id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
	FOREIGN KEY(alunos_id)
    REFERENCES alunos(id)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
	);
 

 

 


