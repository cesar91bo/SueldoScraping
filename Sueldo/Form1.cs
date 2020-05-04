using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Sueldo
{
    public partial class Sueldo : Form
    {
        public Sueldo()
        {
            InitializeComponent();
            button1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<string> bloqueSueldos = new List<string>();
            List<string> sueldos = new List<string>();
            List<int> sueldoInt = new List<int>();
            string pesos;
            string pesosSub = "";
            int acu = 0;
            int cont = 0;
            int palabraPesoFin = 0;
            int palabraPesoInicio = 0;

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load("https://www.cuantogano.com/sueldos/it-programacion.html");

            //HtmlNode Body = htmlDocument.DocumentNode.CssSelect("body").First();
            //string sBody = Body.InnerHtml;

            //HtmlWeb dolarWeb = new HtmlWeb();
            //HtmlDocument htmlDocument2 = dolarWeb.Load("http://www.dolarhoy.com/");

            //var dolar2 = 

            foreach (var node in htmlDocument.DocumentNode.CssSelect(".sueldo"))
            {
                bloqueSueldos.Add(node.InnerText);


                pesos = node.InnerText.Replace("\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tRubro: IT - Programación\r\n\t\t\t\t\t\t\tSueldo:", "");
                pesos = pesos.Replace("\r\n\t\t\t\t\t\t\t", "");

                palabraPesoFin = pesos.IndexOf("pesos") + "pesos".Length;
                palabraPesoInicio = pesos.IndexOf("pesos");

                if (pesos.Contains("$"))
                {
                    pesos = pesos.Substring(0, palabraPesoFin);
                    pesosSub = pesos.Substring(2, palabraPesoInicio - 2);
                }
                else
                {
                    pesos = "0";
                }

                sueldos.Add(pesos);

                if (Convert.ToInt32(pesosSub) < 120000 && (Convert.ToInt32(pesosSub) > 25000))
                {
                    sueldoInt.Add(Convert.ToInt32(pesosSub));
                }

            }



            foreach (var item in sueldoInt)
            {
                cont++;

                acu = acu + item;
            }

            int[] ints = sueldoInt.Select(x => int.Parse(x.ToString())).ToArray();
            Array.Sort(ints);

            double media = (ints[0] + ints[cont - 1]) / 2;

            double promedio = acu / cont;

            double dolar = 66.64;


            textBox1.Text = "$ " + promedio.ToString();
            textBox2.Text = "$ " + media.ToString();
            textBox3.Text = "U$ " + (media / dolar).ToString();
            textBox4.Text = "U$ " + (promedio / dolar).ToString();


            textBox5.Text = "U$ " + (ints[0] / dolar);
            textBox6.Text = "$ " + ints[0].ToString();
            textBox7.Text = "U$ " + (ints[cont - 1] / dolar);
            textBox8.Text = "$ " + ints[cont-1].ToString();
        }

    }
}
