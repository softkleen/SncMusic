using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SncMusic
{
    public partial class FrmCurso : Form
    {
        public FrmCurso()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Curso curso = new Curso();
            curso.Nome = txtNome.Text;
            curso.CargaHoraria = Convert.ToInt32(txtCargaHoraria.Text);
            curso.Valor = Convert.ToDouble(txtValor.Text);
            curso.Inserir();
            if (curso.Id > 0)
            {
                txtId.Text = curso.Id.ToString();
                MessageBox.Show("Curso cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Falha ao cadastrar curso!");
            }
            



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregado && cmbCurso.DataSource!=null)
            {
                var idcurso = cmbCurso.SelectedValue.ToString();
                Professor professor = new Professor();
                cmbProfessor.DataSource = professor.ListarPorIdNaoAssociado(idcurso);
                cmbProfessor.DisplayMember = "nome_professor";
                cmbProfessor.ValueMember = "id_professor";

            }

        }
        bool carregado = false;
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            Curso curso = new Curso();
            var dr = curso.ListarTodos();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cmbCurso.DataSource = dt;
            cmbCurso.DisplayMember = "nome_curso";
            cmbCurso.ValueMember = "id_curso";
            carregado = true;

        }

        private void BtnAssociar_Click(object sender, EventArgs e)
        {
            Curso curso = new Curso();
            if (curso.AssociarProfessor((int)cmbCurso.SelectedValue, (int)cmbProfessor.SelectedValue))
            MessageBox.Show("Professor "+cmbProfessor.Text+" associado ao curso "+cmbCurso.Text+" com sucesso");
            else
                MessageBox.Show("Falha ao associar professor ao curso!");

            cmbCurso.DataSource=null;
            cmbProfessor.DataSource=null;
        }
    }
}
