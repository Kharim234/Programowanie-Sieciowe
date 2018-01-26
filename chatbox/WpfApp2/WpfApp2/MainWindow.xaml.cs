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
using System.Net.Sockets;
using System.ComponentModel;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int portNum;// = 1;
        static string hostName;// = "localhost";
        TcpClient client;// = new TcpClient(hostName, portNum);
        NetworkStream ns;
        private BackgroundWorker Worker;

        public MainWindow()
        {
            InitializeComponent();
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;


        }
     

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow.Items.Clear();
            portNum = int.Parse(PortServer.Text);
            hostName = IPserver.Text;

               // StartedConnection = true;
               /*
                BackgroundWorker Worker = new BackgroundWorker();
                Worker.WorkerReportsProgress = true;
                Worker.WorkerSupportsCancellation = true;
                Worker.DoWork += Worker_DoWork;
                Worker.ProgressChanged += Worker_ProgressChanged;
                Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                */
                //Worker.RunWorkerAsync(10000);
               


            Worker.RunWorkerAsync();

            /*

            byte[] bytes = new byte[1024];

                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                bytesRead = ns.Read(bytes, 0, bytes.Length);
                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                client.Close();

            */

        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int progress = 0;
            bool connection_open = true;
            try
            {
            client = new TcpClient(hostName, portNum);
            ns = client.GetStream();
                (sender as BackgroundWorker).ReportProgress(progress, "Connected");

                byte[] bytes = new byte[1024];
                int bytesRead;
            string EncodedMessage;

           // int max = (int)e.Argument;
           // int result = 0;
            while (connection_open)
            {
                if (ns.DataAvailable)
                {
                    try
                    {
                        bytesRead = ns.Read(bytes, 0, bytes.Length);
                        EncodedMessage = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                        if (EncodedMessage == "Close Connection")
                            connection_open = false;
                        else
                            EncodedMessage = "Server(Ktos): "  + EncodedMessage;

                        (sender as BackgroundWorker).ReportProgress(progress, EncodedMessage);
                    }
                    catch (Exception ex)
                    {
                        (sender as BackgroundWorker).ReportProgress(progress, ex.ToString());

                    }
                }
                if (Worker.CancellationPending)
                {
                    e.Cancel = true;
                    connection_open = false;
                }

            }
            }
            catch (Exception ex)
            {
                (sender as BackgroundWorker).ReportProgress(progress, ex.ToString());
            }
            //e.Result = result;
        }
        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // pbCalculationProgress.Value = e.ProgressPercentage;
            //  if (e.UserState != null)
            ChatWindow.Items.Add( e.UserState);
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
            ChatWindow.Items.Add("Disconnecting...");
                //string MessageToSend = Message.Text;
                //ChatWindow.Items.Add("Client(JA): " + MessageToSend);
                byte[] MessageConverted = Encoding.ASCII.GetBytes("Close Connection");
                try
                {
                    ns.Write(MessageConverted, 0, MessageConverted.Length);

                }
                catch (Exception ex)
                {
                    ChatWindow.Items.Add(ex.ToString());
                }
            }

            ns.Close();
            ChatWindow.Items.Add("NetworkStream Closed");
            client.Close();
            ChatWindow.Items.Add("Client Closed");
           // MessageBox.Show("Numbers between 0 and 10000 divisible by 7: " + e.Result);
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            string MessageToSend = Message.Text;
            if(! (MessageToSend == ""))
            {
                Message.Text = "";
                ChatWindow.Items.Add("Client(JA): " + MessageToSend);
                byte[] MessageConverted = Encoding.ASCII.GetBytes(MessageToSend);
                try
                {
                    ns.Write(MessageConverted, 0, MessageConverted.Length);

                }
                catch (Exception ex)
                {
                    ChatWindow.Items.Add(ex.ToString());
                }
            }
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Worker.CancelAsync();
        }
    }
}
