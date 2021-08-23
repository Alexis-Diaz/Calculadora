using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class frmCalculadora : Form
    {
        public frmCalculadora()
        {
            InitializeComponent();
        }

        #region Numeros 0-9
        private void btnCero_Click(object sender, EventArgs e)
        {
            HayValores("0");
        }

        private void btnUno_Click(object sender, EventArgs e)
        {
            HayValores("1");
        }

        private void btnDos_Click(object sender, EventArgs e)
        {
            HayValores("2");
        }

        private void btnTres_Click(object sender, EventArgs e)
        {
            HayValores("3");
        }

        private void btnCuatro_Click(object sender, EventArgs e)
        {
            HayValores("4");
        }

        private void btnCinco_Click(object sender, EventArgs e)
        {
            HayValores("5");
        }

        private void btnSeis_Click(object sender, EventArgs e)
        {
            HayValores("6");
        }

        private void btnSiete_Click(object sender, EventArgs e)
        {
            HayValores("7");
        }

        private void btnOcho_Click(object sender, EventArgs e)
        {
            HayValores("8");
        }

        private void btnNueve_Click(object sender, EventArgs e)
        {
            HayValores("9");
        }
        #endregion

        #region Operadores Matematicos
        private void btnSuma_Click(object sender, EventArgs e)
        {
            OperacionMatematica("+");
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            OperacionMatematica("-");
        }
        private void btnMultiplicacion_Click(object sender, EventArgs e)
        {
            OperacionMatematica("*");
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            OperacionMatematica("/");
        }

        private void btnRaiz_Click(object sender, EventArgs e)
        {
            OperacionMatematica("sqr");
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            OperacionMatematica("%");
        }
        #endregion

        private void btnPunto_Click(object sender, EventArgs e)
        {
            HayValores(",");
        }
        private void btnAC_Click(object sender, EventArgs e)
        {
            ResetearValores();
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            string signo = txtSigno.Text;
            decimal numeroA = 0, numeroB = 0, resultado = 0;

            if (!string.IsNullOrEmpty(txtResultado.Text))
                numeroA = Convert.ToDecimal(txtResultado.Text);

            if (!string.IsNullOrEmpty(txtPantalla.Text) && txtPantalla.Text != "-")
            {
                numeroB = Convert.ToDecimal(txtPantalla.Text);
            }

            switch (signo) 
            {
                case "+":
                    resultado = numeroA + numeroB;
                    break;
                case "-":
                    resultado = numeroA - numeroB;
                    break;
                case "*":
                    resultado = numeroA * numeroB;
                    break;
                case "/":
                    if (numeroB == 0)
                    {
                        resultado = numeroA;
                    }
                    else
                    {
                        resultado = numeroA / numeroB;
                    }
                    break;
                case "%":
                    resultado = numeroA % numeroB;
                    break;
                default:
                    resultado = numeroB;
                    break;
            }
            txtResultado.Text = Convert.ToString(resultado);
            txtPantalla.Text = Convert.ToString(resultado);
            txtSigno.Text = "=";
        }
        private void ResetearValores()
        {
            txtResultado.Clear();
            txtPantalla.Clear();
            txtSigno.Clear();
        }
        private void HayValores(string valor)
        {
            string valores = txtPantalla.Text;
            if (string.IsNullOrEmpty(valores))
            {
                if (valor == ",")
                {
                    valores = "0,";
                }
                else
                {
                    valores = valor;
                }
            }
            else
            {
                if (txtSigno.Text == "=" || txtSigno.Text == "sqr")
                {
                    txtSigno.Clear();
                    txtPantalla.Clear();
                    txtResultado.Clear();
                    valores = valor;
                }
                else
                {
                    if (valores != "0")
                    {
                        if (valor != ",")
                            valores += valor;
                    }
                    else
                    {
                        if (valor == ",")
                        {
                            valores += valor;
                        }
                        else
                        {
                            valores = valor;
                        }
                    }

                    if (valor == ",")
                    {
                        var coincidencia = valores.IndexOf(',');
                        if (coincidencia == -1)
                        {
                            valores += valor;
                        }
                    }
                }
            }

            txtPantalla.Text = valores;
        }

        private void RealizarOperacion(ref decimal? contador, string operacionActual="", string operacionAnterior = "")
        {
            if (operacionAnterior != "")
            {
                switch (operacionAnterior)
                {
                    case "+":
                        contador += Convert.ToDecimal(txtPantalla.Text);
                        break;
                    case "-":
                        contador -= Convert.ToDecimal(txtPantalla.Text);

                        break;
                    case "*":
                        contador *= Convert.ToDecimal(txtPantalla.Text);

                        break;
                    case "/":
                        break;
                    case "sqr":
                        decimal valor = Convert.ToDecimal(txtPantalla.Text);
                        contador = (decimal)Math.Sqrt((double)valor);
                        break;
                    case "%":
                        contador %= Convert.ToDecimal(txtPantalla.Text);
                        break;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtResultado.Text) && (txtSigno.Text == "=" || txtSigno.Text == "sqr"))
                {
                    if(operacionActual!="sqr")
                        txtPantalla.Clear();
                }

                if (contador.Equals(null) && operacionActual != "sqr")
                {
                    contador = Convert.ToDecimal(txtPantalla.Text);
                    txtPantalla.Clear();
                }
                else
                {
                    switch (operacionActual)
                    {
                        case "+":
                            if (txtSigno.Text != "+" && txtSigno.Text != "sqr")
                            {
                                operacionAnterior = txtSigno.Text;
                                RealizarOperacion(ref contador, operacionActual, operacionAnterior);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtPantalla.Text))
                                    contador += Convert.ToDecimal(txtPantalla.Text);
                            }
                            break;
                        case "-":
                            if (txtSigno.Text != "-" && txtSigno.Text != "sqr")
                            {
                                operacionAnterior = txtSigno.Text;
                                RealizarOperacion(ref contador, operacionActual, operacionAnterior);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtPantalla.Text))
                                    contador -= Convert.ToDecimal(txtPantalla.Text);
                            }
                            break;
                        case "*":
                            if (txtSigno.Text != "-" && txtSigno.Text != "sqr")
                            {
                                operacionAnterior = txtSigno.Text;
                                RealizarOperacion(ref contador, operacionActual, operacionAnterior);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtPantalla.Text))
                                    contador *= Convert.ToDecimal(txtPantalla.Text);
                            }
                            break;
                        case "/":
                            if (txtSigno.Text != "-" && txtSigno.Text != "sqr")
                            {
                                operacionAnterior = txtSigno.Text;
                                RealizarOperacion(ref contador, operacionActual, operacionAnterior);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtPantalla.Text))
                                    contador /= Convert.ToDecimal(txtPantalla.Text);
                            }
                            break;
                        case "sqr":
                            decimal valor = Convert.ToDecimal(txtPantalla.Text);
                            contador = valor < 0 ? 0 : (decimal)Math.Sqrt((double)valor); 
                            txtPantalla.Text = Convert.ToString(contador);
                            break;
                        case "%":
                            if (txtSigno.Text != "%" && txtSigno.Text != "sqr")
                            {
                                operacionAnterior = txtSigno.Text;
                                RealizarOperacion(ref contador, operacionActual, operacionAnterior);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtPantalla.Text))
                                    contador %= Convert.ToDecimal(txtPantalla.Text);
                            }
                            break;
                    }
                }
            }
            txtResultado.Text = Convert.ToString(contador);
            txtSigno.Text = operacionActual;
            if (operacionActual != "sqr")
            {
                txtPantalla.Clear();
            }
        }

        private void OperacionMatematica(string tipoOperacion)
        {
            decimal? contador = null;
            if (!string.IsNullOrEmpty(txtResultado.Text))
            {
                contador = Convert.ToDecimal(txtResultado.Text);
            }
            if (!string.IsNullOrEmpty(txtPantalla.Text) && txtPantalla.Text != "-")
            {
                RealizarOperacion(ref contador, tipoOperacion);
            }
            else
            {
                if(tipoOperacion == "-" && txtPantalla.Text == "")
                {
                    HayValores(tipoOperacion);
                }
                else
                {
                    txtSigno.Text = tipoOperacion != "sqr" ? tipoOperacion : txtSigno.Text;
                }
            }
            
        }
    }
}
