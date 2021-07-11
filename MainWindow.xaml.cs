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
                int numb = 1;

                while (reader.Read())
                {
                    
                    if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == "item")
                    {
                        XmlReader inner = reader.ReadSubtree();
                        inner.ReadToDescendant("title");
                        string _title = reader.ReadString();
                        //MessageBox.Show(_title);

                        inner.ReadToNextSibling("link");
                        string _link = reader.ReadString();
                        //MessageBox.Show(_link);

                        inner.ReadToNextSibling("dc:date");
                        string _date = reader.ReadString();
                        
                        //MessageBox.Show(_date);

                        
                        RSSData.GetInstance().Add(new RSSData { DataN = numb++, DataT = _title, DataL = _link, DataD = _date });
                        DataSet.ItemsSource = RSSData.GetInstance();
                        
                    }
                    DataSet.Items.Refresh();
                }
            }

            // URI 형식 안맞을 때
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Not a Correct URI...");
            }

            // 데이터 입력 후 텍스트 박스 비우기
            uri_text.Text = String.Empty;
        }
    }

    public class RSSData
    {
        // Numbering Title Link Date
        public int DataN { get; set; } 
        public string DataT { get; set; }
        public string DataL { get; set; }
        public string DataD { get; set; }

        private static List<RSSData> instance;
        public static List<RSSData> GetInstance()
        {
            if (instance == null)
                instance = new List<RSSData>();

            return instance;
        }
    }
}
