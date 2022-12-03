using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Util
{
    public static class Utils
    {
        public static string MenuAtivo { get; set; }
        public static bool IsTrimNullOrEmpty(this string texto) => texto.Trim().IsNullOrEmpty();
        public static bool IsNullOrEmpty(this string texto) => string.IsNullOrEmpty(texto);
        public static bool IsNullOrEmpty(this object obj) => obj == null;
        public static bool IsNullOrEmpty(this List<object> list) => list == null || list.Count == 0;
        public static bool IsNullOrEmpty(this IList<object> list) => list == null || list.Count == 0;
        public static void CarregarUserControl(this Panel panel, UserControl userControl, bool limpar) => CarregarUserControl(panel, userControl, limpar, true);
        public static void CarregarUserControl(this Panel panel, UserControl userControl) => CarregarUserControl(panel, userControl, true, true);
        public static void CarregarUserControl(this Panel panel, UserControl userControl, bool limpar, bool controleMenu)
        {
            if (controleMenu && MenuAtivo == userControl.Name)
                return;

            if (limpar)
                panel.Controls.Clear();

            MenuAtivo = userControl.Name;
            userControl.Dock = DockStyle.Fill;
            panel.Controls.Add(userControl);
        }
        public static void LimparTextoComponentes(this UserControl userControl, Control.ControlCollection controls = null)
        {
            if(userControl.Controls.Count > 0)
            {
                Control.ControlCollection controles = null;

                if (controls.IsNullOrEmpty())
                {
                    controles = userControl.Controls;
                }
                else
                {
                    controles = controls;
                }

                foreach (var control in controles)
                {
                    if(control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }

                    if(control is MaskedTextBox)
                    {
                        (control as MaskedTextBox).Clear();
                    }

                    if(control is ComboBox && (control as ComboBox).Items.Count > 0)
                    {
                        (control as ComboBox).SelectedIndex = 0;
                    }

                    if(control is DataGridView)
                    {
                        (control as DataGridView).Rows.Clear();
                    }

                    if(control is RadioButton)
                    {
                        (control as RadioButton).Checked = false;
                    }

                    if (control is GroupBox)
                    {
                        LimparTextoComponentes(userControl, (control as GroupBox).Controls);
                    }
                }
            }
        }
        public static void AtivarDesativarComponentes(this UserControl userControl, bool valor, Control.ControlCollection controls = null)
        {
            if (userControl.Controls.Count > 0)
            {
                Control.ControlCollection controles = null;

                if (controls.IsNullOrEmpty())
                {
                    controles = userControl.Controls;
                }
                else
                {
                    controles = controls;
                }

                foreach (var control in controles)
                {
                    if (control is Button && !(control as Button).Name.ToLower().Contains("adicionar"))
                        continue;

                    if (control is TextBox)
                    {
                        (control as TextBox).Enabled = valor;
                    }

                    if (control is MaskedTextBox)
                    {
                        (control as MaskedTextBox).Enabled = valor;
                    }

                    if (control is ComboBox)
                    {
                        (control as ComboBox).Enabled = valor;
                    }

                    if (control is DataGridView)
                    {
                        (control as DataGridView).Enabled = valor;
                    }

                    if (control is RadioButton)
                    {
                        (control as RadioButton).Enabled = valor;
                    }

                    if (control is Button)
                    {
                        (control as Button).Enabled = valor;
                    }

                    if(control is GroupBox)
                    {
                        AtivarDesativarComponentes(userControl, valor, (control as GroupBox).Controls);
                    }
                }
            }
        }
        public static SecureString Preencher(this SecureString secureString, byte[] array) => Preencher(secureString, array, false);
        public static SecureString Preencher(this SecureString secureString, byte[] array, bool permitirAlteracaoPosterior)
        {
            if (secureString.IsNullOrEmpty())
                secureString = new SecureString();

            if (!array.IsNullOrEmpty())
                for (int i = 0; i < array.Length; i++)
                {
                    secureString.AppendChar(Convert.ToChar(array[i]));
                }

            if (!permitirAlteracaoPosterior)
                secureString.MakeReadOnly();

            return secureString;
        }

        public static string ObterTexto(this SecureString secureString)
        {
            if (secureString.IsNullOrEmpty())
                return String.Empty;

            return Marshal.PtrToStringUni(Marshal.SecureStringToBSTR(secureString));
        }

        public static string CriarHashSha3(string texto)
        {
            return Hash.Salt(Hash.SHA3(texto));
        }

        public static bool CompararHashSha3(string texto, string textoBanco)
        {
            var hash = CriarHashSha3(texto);
            return hash == textoBanco;
        }
    }

    public enum Situacao
    {
        Desativado = 0,
        Ativo = 1
    }
    public enum TipoComandoSQL
    {
        Atualizar,
        Inserir,
        Excluir,
        InserirOuAtualizar,
        TextoSQL
    }
    public enum TipoLog
    {
        Geral,
        BancoDados,
        Erro
    }

    public enum TipoPessoa
    {
        Fisica = 1,
        Juridica,
        Funcionario,
        Fornecedor
    }

    public enum TipoEndereco
    {
        Cobranca,
        Entrega
    }
}
