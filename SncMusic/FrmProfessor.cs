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
    public partial class FrmProfessor : Form
    {
        public FrmProfessor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            Professor professor = new Professor(txtNome.Text, mskCPF.Text, txtEmail.Text, mskTelefone.Text);
            professor.Inserir();
            MessageBox.Show("Professor Gravado com sucesso!");
            LimparControles();
        }

        private void LimparControles()
        {
            txtId.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            mskCPF.Clear();
            mskTelefone.Clear();
            txtNome.Focus();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            Professor professor = new Professor();
            if (professor.Alterar(new Professor(Convert.ToInt32(txtId.Text), txtNome.Text, mskTelefone.Text)))
            {
                MessageBox.Show("Dados do Professor alterados com sucesso!");
                LimparControles();
            }
            else
                MessageBox.Show("Falha ao alterar dados do professor!");

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
                //se txtid for diferente de vazio então consulte o professor
                if (txtId.Text != string.Empty)
                {
                    Professor professor = new Professor();
                    professor.ConsultarPorId(Convert.ToInt32(txtId.Text));
                    txtEmail.Text = professor.Email;
                    mskCPF.Text = professor.Cpf;
                    mskTelefone.Text = professor.Telefone;
                    txtNome.Text = professor.Nome;


                    //altere o texto do botão para "..."
                    btnBuscar.Text = "...";
                    //tornar o txtid Enable false
                    txtId.Enabled = false;
                    //tornar o txtid readonly true
                    txtId.ReadOnly = true;
                }
            }
        }
    }
}
