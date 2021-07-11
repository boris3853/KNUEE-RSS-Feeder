using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace RSS_FEEDER
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            // URI 받아오기
            string uri = uri_text.Text;
            

            // 예외처리
            try {
                XmlReader reader = XmlReader.Create(uri);

                while (reader.Read())
                {
                    if (reader.IsStartElement("title"))
                    {

                        //MessageBox.Show("Success");
                        reader.ReadToFollowing("title");
                        string title = reader.ReadElementContentAsString();


                        //string link = reader.GetAttribute("link");
                        //string date = reader.GetAttribute("dc:date");

                        MyData.GetInstance().Add(new MyData { DataN = "1", DataT = title, DataD = DateTime.Today });
                        MyListView.ItemsSource = MyData.GetInstance();
                    }
                }
            }

            // URI 형식 안맞을 때
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Unknown URI...");
            }

            // 텍스트 박스 비우기
            uri_text.Text = String.Empty;
        }
    }

    public class MyData
    {
        // No Title Date
        public string DataN { get; set; }
        public string DataT { get; set; }
        public DateTime DataD { get; set; }

        private static List<MyData> instance;
        public static List<MyData> GetInstance()
        {
            if (instance == null)
                instance = new List<MyData>();

            return instance;
        }
    }
}
