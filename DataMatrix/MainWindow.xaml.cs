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
                // dataMatrix width and height
                int hs = 0;
                int ws = 0;
                // 
                int r = 0;
                //
                int f = 0;


                //count size
                if (Text.Length <= 3)
                {
                    hs = 10;
                    ws = 10;
                    r = 3 - Text.Length;
                    f = 3;
                }
                else if (Text.Length <= 5)
                {
                    hs = 12;
                    ws = 12;
                    r = 5 - Text.Length;
                    f = 5;
                }
                else if (Text.Length <= 8)
                {
                    hs = 14;
                    ws = 14;
                    r = 8 - Text.Length;
                    f = 8;
                }
                else if (Text.Length <= 12)
                {
                    hs = 16;
                    ws = 16;
                    r = 12 - Text.Length;
                    f = 12;
                }
                else if (Text.Length <= 18)
                {
                    hs = 18;
                    ws = 18;
                    r = 18 - Text.Length;
                    f = 18;
                }



                int[] c = new int[Text.Length + r];
                int j = 0;

                for (int i = 0; i < Text.Length; i++)
                {
                    try {
                        try
                        {
                            int d = Convert.ToInt32(Text[i]);
                            int s = Convert.ToInt32(Text[i + 1]);

                            string k = d.ToString() + s.ToString();
                            c[j] = Convert.ToInt32(k) + 130;
                            j++;
                        }
                        catch { }
                        int z = Convert.ToInt32(Text[i]);
                        c[j] = z + 130;
                        j++;
                    }
                    catch
                    {
                        c[j] = Text[i] + 1;
                        j++;
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
                byte[] sisya = new byte[c.Length];

                for (int i = 0; i < c.Length; i++)
                {
                    binary  += Convert.ToString(c[i], 2);
                    sisya[i] = Convert.ToByte(binary);
                }

                DM.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(sisya);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
