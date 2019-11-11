using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataMatrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CodeMarg(int c, int i) {

            int r = ((149 * i) % 253) + 1;
            c = (r + 129) % 254;

        }


        private void Go_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Text = Data.Text;
                // 
                int r = 0;
                //
                int f = 0;


                //count size
                if (Text.Length <= 3)
                {

                    r = 3 - Text.Length;
                    f = 3;
                }
                else if (Text.Length <= 5)
                {

                    r = 5 - Text.Length;
                    f = 5;
                }
                else if (Text.Length <= 8)
                {

                    r = 8 - Text.Length;
                    f = 8;
                }
                else if (Text.Length <= 12)
                {

                    r = 12 - Text.Length;
                    f = 12;
                }
                else if (Text.Length <= 18)
                {

                    r = 18 - Text.Length;
                    f = 18;
                }



                int[] c = new int[Text.Length + r];
                int j = 0;
                string k = "";
                int p = 0;
                for (int i = 0; i < Text.Length; i++)
                {
                    if (Text[i] >= '0' && Text[i] <= '9')
                    {
                        if (p != 2)
                        {
                            k += Text[i].ToString();
                            p++;
                        }
                    }
                    else
                    {
                        if (k != "")
                        {
                            c[j] = Convert.ToInt32(k) + 130;
                            j++;
                            k = "";
                        }
                        else
                        {
                            c[j] = Text[i] + 1;
                            j++;
                        }
                    }
                }

                //add margin
                if (r > 0)
                {
                    c[j] = 129;
                    for (int i = j + 1; i < c.Length; i++)
                    {
                        CodeMarg(c[i], i);
                    }

                }



                //convert to bitcode
                string binary = "";
              

                for (int i = 0; i < c.Length; i++)
                {
                    binary += Convert.ToString(c[i], 2);
                    
                }

                DM.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(binary);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
