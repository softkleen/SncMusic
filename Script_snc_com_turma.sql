-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema dbsncmusic01
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema dbsncmusic01
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `dbsncmusic01` DEFAULT CHARACTER SET utf8 ;
USE `dbsncmusic01` ;

-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_aluno`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_aluno` (
  `id_aluno` INT(11) NOT NULL AUTO_INCREMENT,
  `nome_aluno` VARCHAR(50) NOT NULL,
  `cpf_aluno` VARCHAR(11) NOT NULL,
  `sexo_aluno` VARCHAR(10) NOT NULL,
  `email_aluno` VARCHAR(60) NOT NULL,
  `telefone_aluno` VARCHAR(14) NOT NULL,
  `data_cadastro_aluno` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`id_aluno`),
  UNIQUE INDEX `cpf` (`cpf_aluno` ASC) ,
  UNIQUE INDEX `email_UNIQUE` (`email_aluno` ASC))
ENGINE = InnoDB
AUTO_INCREMENT = 11
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_curso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_curso` (
  `id_curso` INT(11) NOT NULL AUTO_INCREMENT,
  `nome_curso` VARCHAR(45) NOT NULL,
  `carga_horaria_curso` INT(11) NOT NULL,
  `valor_curso` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`id_curso`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_endereco`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_endereco` (
  `cep` CHAR(8) NOT NULL,
  `logradouro` VARCHAR(100) NOT NULL,
  `numero` VARCHAR(20) NOT NULL,
  `complemento` VARCHAR(60) NULL DEFAULT NULL,
  `bairro` VARCHAR(45) NULL DEFAULT NULL,
  `cidade` VARCHAR(45) NULL DEFAULT NULL,
  `estado` VARCHAR(45) NULL DEFAULT NULL,
  `sigla_estado` CHAR(2) NULL DEFAULT NULL,
  `aluno_id_aluno` INT(11) NOT NULL,
  INDEX `fk_tb_endereco_tb_aluno1_idx` (`aluno_id_aluno` ASC) ,
  CONSTRAINT `fk_tb_endereco_tb_aluno1`
    FOREIGN KEY (`aluno_id_aluno`)
    REFERENCES `dbsncmusic01`.`tb_aluno` (`id_aluno`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_turma`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_turma` (
  `id_turma` INT NOT NULL AUTO_INCREMENT,
  `id_curso_turma` INT NOT NULL,
  `sigla_turma` VARCHAR(45) NOT NULL,
  `data_inicio` DATETIME NOT NULL,
  `data_termino` DATETIME NOT NULL,
  `hora` TIMESTAMP NOT NULL,
  `duracao` DECIMAL(10,2) NOT NULL,
  `dia_semana` VARCHAR(45) NOT NULL,
  `situacao` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_turma`),
  INDEX `fk_curso_turma_idx` (`id_curso_turma` ASC) ,
  CONSTRAINT `fk_curso_turma`
    FOREIGN KEY (`id_curso_turma`)
    REFERENCES `dbsncmusic01`.`tb_curso` (`id_curso`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_usuario` (
  `id_usuario` INT(11) NOT NULL AUTO_INCREMENT,
  `nome_usuario` VARCHAR(45) NOT NULL,
  `email_usuario` VARCHAR(45) NOT NULL,
  `senha_usuario` VARCHAR(45) NOT NULL,
  `situacao_usuario` CHAR(1) NOT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE INDEX `email_usuario_UNIQUE` (`email_usuario` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_matricula`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_matricula` (
  `id_matricula` INT(11) NOT NULL AUTO_INCREMENT,
  `id_aluno_mat` INT(11) NOT NULL,
  `id_turma` INT(11) NOT NULL,
  `situacao` CHAR(1) NOT NULL,
  `valor_curso_matricula` DECIMAL(10,2) NOT NULL,
  `data_matricula` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  `usuario_id_usuario` INT(11) NOT NULL,
  PRIMARY KEY (`id_matricula`),
  INDEX `fk_saiqueesuatafarel_aluno_mat_idx` (`id_aluno_mat` ASC) ,
  INDEX `fk_tb_matricula_tb_usuario1_idx` (`usuario_id_usuario` ASC),
  INDEX `fk_turma_mat_4564_idx` (`id_turma` ASC) ,
  CONSTRAINT `fk_turma_mat_4564`
    FOREIGN KEY (`id_turma`)
    REFERENCES `dbsncmusic01`.`tb_turma` (`id_turma`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_saiqueesuatafarel_aluno_mat`
    FOREIGN KEY (`id_aluno_mat`)
    REFERENCES `dbsncmusic01`.`tb_aluno` (`id_aluno`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tb_matricula_tb_usuario1`
    FOREIGN KEY (`usuario_id_usuario`)
    REFERENCES `dbsncmusic01`.`tb_usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_professor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_professor` (
  `id_professor` INT(10) NOT NULL AUTO_INCREMENT,
  `nome_professor` VARCHAR(50) NOT NULL,
  `cpf_professor` VARCHAR(11) NOT NULL,
  `email_professor` VARCHAR(60) NOT NULL,
  `telefone_professor` VARCHAR(9) NOT NULL,
  `data_cadastro_professor` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`id_professor`),
  UNIQUE INDEX `cpf` (`cpf_professor` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_professor_curso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_professor_curso` (
  `professor_id_professor` INT(10) NOT NULL,
  `curso_id_curso` INT(11) NOT NULL,
  `data_associacao` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  INDEX `fk_tb_professor_has_tb_curso_tb_curso1_idx` (`curso_id_curso` ASC) ,
  INDEX `fk_tb_professor_has_tb_curso_tb_professor1_idx` (`professor_id_professor` ASC),
  CONSTRAINT `fk_tb_professor_has_tb_curso_tb_curso1`
    FOREIGN KEY (`curso_id_curso`)
    REFERENCES `dbsncmusic01`.`tb_curso` (`id_curso`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tb_professor_has_tb_curso_tb_professor1`
    FOREIGN KEY (`professor_id_professor`)
    REFERENCES `dbsncmusic01`.`tb_professor` (`id_professor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `dbsncmusic01`.`tb_prontuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbsncmusic01`.`tb_prontuario` (
  `id_prontuario` INT NOT NULL AUTO_INCREMENT,
  `id_matricula` INT NOT NULL,
  `historico` TEXT NULL,
  PRIMARY KEY (`id_prontuario`),
  INDEX `fk_mat_pront_idx` (`id_matricula` ASC) ,
  CONSTRAINT `fk_mat_pront`
    FOREIGN KEY (`id_matricula`)
    REFERENCES `dbsncmusic01`.`tb_matricula` (`id_matricula`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `dbsncmusic01` ;

-- -----------------------------------------------------
-- procedure sp_altera_curso
-- -----------------------------------------------------

DELIMITER $$
USE `dbsncmusic01`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_altera_curso`(
spid int(11),spnome varchar(45),spcargahoraria int(11), spvalor decimal(10,2)
)
update tb_curso set nome_curso = spnome,
carga_horaria_curso = spcargahoraria, 
valor_curso = spvalor
where id_curso = spid$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_delete_curso
-- -----------------------------------------------------

DELIMITER $$
USE `dbsncmusic01`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_delete_curso`(spid int(11))
begin
delete from tb_curso where id_curso = spid;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insere_aluno
-- -----------------------------------------------------

DELIMITER $$
USE `dbsncmusic01`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insere_aluno`(
spnome varchar(50),
spcpf varchar(11),
spsexo varchar(10),
spemail varchar(60),
sptelefone varchar(14)
)
begin
INSERT INTO tb_aluno
VALUES
(0,spnome,spcpf,spsexo,spemail,sptelefone,default);
select @@identity as id_aluno;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insere_curso
-- -----------------------------------------------------

DELIMITER $$
USE `dbsncmusic01`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insere_curso`( 
 spnome varchar(45),
 spcargahoraria int(11),
 spvalor decimal(10,2))
begin
insert `tb_curso` values (0,spnome,spcargahoraria,spvalor);
select @@identity as id_curso;
end$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
