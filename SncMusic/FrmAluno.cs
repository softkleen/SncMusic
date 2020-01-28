﻿using System;
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
    public partial class FrmAluno : Form
    {
        public FrmAluno()
        {
            InitializeComponent();
           
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string sexo;
            if (rdbMasculino.Checked) sexo = "M";
            else sexo = "F";// resolve o sexo
            mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            Aluno aluno = new Aluno(txtNome.Text, mskCPF.Text, sexo, txtEmail.Text, mskTelefone.Text);
            aluno.Inserir();
            MessageBox.Show("Aluno Gravado com sucesso!");
            LimparControles();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // se o text do botão for igual a "..."
            if (btnBuscar.Text == "...")
            {
                //alterar o texto do botão para "Buscar"
                btnBuscar.Text = "Buscar";
                //tornar o txtid Enable true
                txtId.Enabled = true;
                //tornar o txtid readonly false
                txtId.ReadOnly = false;
                //colocar o foco (cursor) no txtid e limpe
                txtId.Focus();
                txtId.Clear();
            }
            //senão
            else
            {
                //se txtid for diferente de vazio então consulte o aluno
                if (txtId.Text != string.Empty)
                {
                    Aluno aluno = new Aluno();
                    aluno.ConsultarPorId(Convert.ToInt32(txtId.Text));
                    txtEmail.Text = aluno.Email;
                    mskCPF.Text = aluno.Cpf;
                    mskTelefone.Text = aluno.Telefone;
                    txtNome.Text = aluno.Nome;
                    if (aluno.Sexo == "M")
                        rdbMasculino.Checked = true;
                    else
                        rdbFeminino.Checked = true;

                    //altere o texto do botão para "..."
                    btnBuscar.Text = "...";
                    //tornar o txtid Enable false
                    txtId.Enabled = false;
                    //tornar o txtid readonly true
                    txtId.ReadOnly = true;
                }

                
            }

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (txtId.Text != string.Empty)
            {
                btnAlterar.Enabled = true;
            }
            else
            {
                btnAlterar.Enabled = false;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string sexo;
            if (rdbMasculino.Checked) sexo = "M";
            else sexo = "F";// resolve o sexo
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            Aluno aluno = new Aluno();
            if (aluno.Alterar(new Aluno(Convert.ToInt32(txtId.Text), txtNome.Text, sexo, mskTelefone.Text)))
            {
                MessageBox.Show("Dados do aluno alterados com sucesso!");
                LimparControles();
            }
            else
                MessageBox.Show("Falha ao alterar dados do aluno!");
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show(
                "Deseja realmente excluir o aluno?",
                "Excluir Aluno...",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (msg == DialogResult.Yes && txtId.Text != string.Empty)
            {
                var comm = Banco.Abrir();
                comm.CommandText = "delete from tb_aluno where id_aluno = " + txtId.Text;
                comm.ExecuteNonQuery();
                MessageBox.Show("Aluno excluído com sucesso!");
                LimparControles();
            }
           
        }
        private void LimparControles()
        {
            txtId.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            mskCPF.Clear();
            mskTelefone.Clear();
            rdbFeminino.Checked = false;
            rdbMasculino.Checked = false;
            txtNome.Focus();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Aluno aluno = new Aluno();
            var lista = aluno.ListarTodos();
            foreach (var item in lista)
            {
                listBox1.Items.Add(item.Id +" - "+ item.Nome);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length>1) 
            {
                listBox1.Items.Clear();
                Aluno aluno = new Aluno();
                var dr = aluno.ListarTodos(textBox1.Text);
                while (dr.Read())
                {
                    listBox1.Items.Add(dr.GetString(1));
                }
            }
        }
    }
}
