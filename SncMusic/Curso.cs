﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SncMusic
{
    public class Curso
    {
        //atributos
        private int id;
        private string nome;
        private int cargaHoraria;
        private double valor;
      
        //propriedades
        public int Id { get => id; set => id = value; } //encapsulamento
        public string Nome { get => nome; set => nome = value; }
        public int CargaHoraria { get => cargaHoraria; set => cargaHoraria = value; }
        public double Valor { get => valor; set => valor = value; }
        //métodos construtores (new Curso())
        public Curso() 
        {
            valor = 0.0;
        }
        public Curso(int id, string nome, int cargaHoraria, double valor)
        {
            this.id = id;
            this.nome = nome;
            this.cargaHoraria = cargaHoraria;
            this.valor = valor;
        }
        public Curso(string nome, int cargaHoraria, double valor)
        {
            this.nome = nome;
            this.cargaHoraria = cargaHoraria;
            this.valor = valor;
        }
        public Curso(string nome, double valor)
        {
            this.nome = nome;
            this.valor = valor;
        }
        //métodos da classe
        public void Inserir() 
        {
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "insert into tb_curso values(0,@nome, @carga_horaria,@valor)";
                comm.Parameters.Add("@nome", MySqlDbType.VarChar).Value = Nome;
                comm.Parameters.Add("@carga_horaria", MySqlDbType.Int32).Value = CargaHoraria;
                comm.Parameters.Add("@valor", MySqlDbType.Decimal).Value = Valor;
                comm.ExecuteNonQuery();
                comm.CommandText = "select @@identity";
                Id = Convert.ToInt32(comm.ExecuteScalar());
                Banco.Fechar();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool Alterar(Curso curso) 
        {
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "update tb_curso set nome_curso = @nome," +
                    "carga_horaria_curso = @carga" +
                    ", valor_curso = @valor where id_curso = @id";
                comm.Parameters.Add("@id", MySqlDbType.Int32).Value = curso.Id;
                comm.Parameters.Add("@nome", MySqlDbType.VarChar).Value = curso.Nome;
                comm.Parameters.Add("@carga", MySqlDbType.Int32).Value = curso.CargaHoraria;
                comm.Parameters.Add("@valor", MySqlDbType.Decimal).Value = curso.Valor;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void ConsultarPorId(int _id)
        {
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "select * from tb_curso where id  =" + _id;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Id = dr.GetInt32(0);
                    Nome = dr.GetString(1);
                    CargaHoraria = dr.GetInt32(2);
                    Valor = Convert.ToDouble(dr.GetDecimal(3));
                }
            }
            catch (Exception)
            {

            }
        }
        public List<Curso> ConsultarPorNome(string caracteres) 
        {
            List<Curso> lista = new List<Curso>();
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "select * from tb_curso where nome_curso like '%@carac%'";
                comm.Parameters.Add("@carac", MySqlDbType.VarChar).Value = caracteres;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Curso(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetDouble(3)));
                }
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }

        }
        public MySqlDataReader ListarTodos()
        {
            MySqlDataReader dr;
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "select * from tb_curso";
                dr = comm.ExecuteReader();
                return dr;
            }
            catch (Exception)
            {                
                return dr=null;
            }
            
        }
        public bool AssociarProfessor(int _id_curso, int _id_professor)
        {
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "insert into tb_professor_curso values(@prof, @curso, default)";
                comm.Parameters.Add("@prof", MySqlDbType.Int32).Value = _id_professor;
                comm.Parameters.Add("@curso", MySqlDbType.Int32).Value = _id_curso;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Professor> ConsultarAssociacao(int _id_curso)
        {
            List<Professor> lista = new List<Professor>();
            try
            {
                var comm = Banco.Abrir();
                comm.CommandText = "select p.id_professor, p.nome_professor, p.cpf_professor," +
                    " p.email_professor " +
                    "from tb_professor_curso pc inner join tb_professor p " +
                    "on pc.professor_id_professor = p.id_professor " +
                    "where pc.curso_id_curso =  "+ _id_curso;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Professor(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3)));
                }
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }
    }
}
