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
using System.IO;
using System.Windows.Forms;
using System.Drawing;

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
                    if (i == 0)
                    {
                        arr[i, j] = 1;
                    }
                    else if (j == arr.GetUpperBound(1))
                    {
                        arr[i, j] = 1;
                    }
                    else if (j == 0 && i % 2 == 0)
                    {
                        arr[i, j] = 1;
                    }
                    else if (i == arr.GetUpperBound(0) && j % 2 == 0)
                    {
                        arr[i, j] = 1;
                    }
                    else arr[i, j] = 0;
                }
            }

            int bit = 8;
            int CurY = 0, curX = 0;
            string dir = "up";

            for (int i = 0; i < bin.Length; i++)
            {
                switch (bit)
                {
                    case 8:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        curX = x;
                        CurY = y;
                        y--;
                        bit--;
                        if (y <= 0)
                        {
                            x -= 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 7:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        y--;
                        bit--;
                        if (y <= 0)
                        {
                            x -= 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 6:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        y = CurY;
                        x++;
                        bit--;
                        if (x <= 0)
                        {
                            x = arr.GetUpperBound(1) - 1;
                            y -= 2;
                            if (y <= 0)
                            {
                                y = arr.GetUpperBound(0) - 1;
                            }
                        }
                        break;

                    case 5:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        y--;
                        bit--;
                        if (y <= 0)
                        {
                            x -= 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 4:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        y--;
                        bit--;
                        if (y <= 0)
                        {
                            x -= 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 3:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        y = CurY - 1;
                        x++;
                        bit--;
                        if (x <= 0)
                        {
                            x = arr.GetUpperBound(1) - 1;
                            y -= 2;
                            if (y <= 0)
                            {
                                y = arr.GetUpperBound(0) - 1;
                            }
                        }
                        break;

                    case 2:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        y--;
                        bit--;
                        if (y <= 0)
                        {
                            x -= 2;
                            y = arr.GetUpperBound(0) - 1;
                        }
                        break;

                    case 1:
                        arr[y, x] = Convert.ToInt32(bin[i].ToString());
                        
                        if (dir == "up")
                        {
                            if (curX - 2 <= 0)
                            {
                                x = curX + 2;
                                y = CurY - 2;
                                dir = "down";
                            } else if(CurY - 2 >= arr.GetUpperBound(0)) {
                                x = curX + 2;
                                y = CurY - 2;
                                dir = "down";
                            } 
                            else if (curX - 2 <= 0 && CurY + 2 >= arr.GetUpperBound(0))
                            {
                                x = curX + 2;
                                y = CurY - 2;
                                dir = "down";
                            }
                            else if (curX == 0 - 1 && CurY + 2 <= arr.GetUpperBound(0) - 1)
                            {
                                y = CurY + 3;
                                x = curX;
                                dir = "down";
                            }
                            else
                            {
                                x = curX - 2;
                                y = CurY + 2;
                            }
                        }
                        else if (dir == "down")
                        {
                            if (curX - 2 >= arr.GetUpperBound(1) && CurY - 2 <= 0)
                            {
                                x = curX - 2;
                                y = CurY + 2;
                                dir = "up";
                            }
                            else if (curX - 2 >= arr.GetUpperBound(1))
                            {
                                x = curX - 2;
                                y = CurY + 2;
                                dir = "up";
                            }
                            else if (CurY - 2 <= 0)
                            {
                                x = curX - 2;
                                y = CurY + 2;
                                dir = "up";
                            }
                            else
                            {
                                x = curX + 2;
                                y = CurY - 2;
                            }
                        }
                        bit = 8;
                        break;
                        
                }

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



                int[] c = new int[f];
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
                if (f - c.Length != 0)
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

                //byte[] bytes = new byte[f * f];
                //int b = 0;
                //for(int i = 0; i < f; i++){
                //    for (int y = 0; y < f; y++)
                //    {
                //        bytes[b] = Convert.ToByte(arr[i, y].ToString() + arr[i, j + 1].ToString() + arr[i, j + 2].ToString() + arr[i, y + 3].ToString() + arr[i, j + 4].ToString() + arr[i, j + 5].ToString() + arr[i, j + 6].ToString() + arr[i, j + 7].ToString());
                //            b++;
                //            y += 6;
                //    }
                //}


                var box = new PictureBox();
                hospade.Child = box;
                Bitmap img = new Bitmap(128, 128);
                Graphics g = Graphics.FromImage(img);
                System.Drawing.Brush ff = System.Drawing.Brushes.Black;


                int count = 128 / f;
                for (int i = 0; i < f; i++)
                {
                    for (int sa = 0; sa < f; sa++)
                    {
                        if (arr[i, sa] == 1)
                        {
                            g.FillRectangle(ff, i * count, sa * count, count, count);
                        }
                    }

                }

                g.Flush();

                box.Image = img;

                //var stream = new MemoryStream(bytes);
                //var image = new BitmapImage();
                //image.BeginInit();
                //image.StreamSource = stream;
                //image.EndInit();


                //DM.Source = image;

               // MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
           
        }
    }
}
