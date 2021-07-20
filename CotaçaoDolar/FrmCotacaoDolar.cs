using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Globalization;

namespace CotaçaoDolar
{
    public partial class FrmCotacaoDolar : Form
    {
        public FrmCotacaoDolar()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String strUrl = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=5ecc0b0a";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(strUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        Market market = JsonConvert.DeserializeObject<Market>(result);

                        lblBuy.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Buy);
                        lblSell.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Sell);
                        lblVar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P}", market.Currency.Variation / 100);
                    }
                    else
                    {
                        lblBuy.Text = "-";
                        lblSell.Text = "-";
                        lblVar.Text = "-";
                    }
                }
                catch (Exception ex)
                {
                    lblBuy.Text = "-";
                    lblSell.Text = "-";
                    lblVar.Text = "-";

                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}