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

        public void MapMatrix(string bin, int[,] arr)
        {
            int x = 1, y = 5;

            for (int i = 0; i <= arr.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= arr.GetUpperBound(1); j++)
                {
                    if (i == 0 )
                    {
                        arr[i,j] = 1;
                    } else if (j == arr.GetUpperBound(1))
                    {
                        arr[i, j] = 1;
                    } else if (j == 0 && i % 2 == 0)
                    {
                        arr[i, j] = 1;
                    } else if (i == arr.GetUpperBound(0) && j % 2 == 0)
                    {
                        arr[i, j] = 1;
                    } else arr[i, j] = 0;
                }
            }

            int bit = 8;
            int CurY = 0, curX = 0;
            for (int i = 0; i < bin.Length; i++)
            {
                    
                switch (bit)
                {
                    case 8:
                        arr[y, x] = Convert.ToInt32(bin[i]);
                        curX = x;
                        CurY = y;
                        y--;
                        if(y <= 0){
                            x += 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 7:
                        arr[y, x] = Convert.ToInt32(bin[i]);
                        x--;
                        if(y <= 0){
                            x += 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 6:
                        arr[y, x] = Convert.ToInt32(bin[i]);
                        y = CurY;
                        x++;
                        if(x <= 0){
                            x = arr.GetUpperBound(1) - 1;
                            y -= 2;
                            if(y <= 0){
                                y = arr.GetUpperBound(0) - 1;
                            }
                        }
                        break;

                    case 5:
                        arr[y, x] = Convert.ToInt32(bin[i]);
                        y--;
                        if(y <= 0){
                            x += 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 4:
                        arr[y, x] = Convert.ToInt32(bin[i]);
                        y--;
                        if(y <= 0){
                            x += 2;
                            y = arr.GetUpperBound(0) - 1;
                        }            
                        break;

                    case 3:

                        break;

                    case 2:

                        break;

                    case 1:

                        break;

            }

        }


        private void Go_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Text = Data.Text;
                // 
                int r = 0;
                // size
                int f = 0;


                //count size
                if (Text.Length <= 3)
                {

                    r = 3 - Text.Length;
                    f = 10;
                }
                else if (Text.Length <= 5)
                {

                    r = 5 - Text.Length;
                    f = 12;
                }
                else if (Text.Length <= 8)
                {

                    r = 8 - Text.Length;
                    f = 14;
                }
                else if (Text.Length <= 12)
                {

                    r = 12 - Text.Length;
                    f = 16;
                }
                else if (Text.Length <= 18)
                {

                    r = 16 - Text.Length;
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

                int[,] arr = new int[f,f];

                string res = "";

                MapMatrix(binary, arr);

                for (int i = 0; i < f; i++)
                {
                    for (int z = 0; z < f; z++)
                    {
                        if (z != f - 1)
                        {
                            res += arr[z, i] + " ";
                        }
                        else res += arr[z, i] + Environment.NewLine; 
                    }
                }

                MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
