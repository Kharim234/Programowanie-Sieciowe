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
using System.Threading;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Pawn : int { Black = 1, BlackKing = 2, White = 3, WhiteKing = 4, None = 0 };
        enum CellBoardColor : int { White = 0, Black = 1, Selected = 2 };
        enum PlayerColors : int { None = 0, White = 1, Black = 2 };
        static int portNum;// = 1;
        static string hostName;// = "localhost";
        TcpClient client;// = new TcpClient(hostName, portNum);
        NetworkStream ns;
        private BackgroundWorker Worker;
        /*int[,] BoardColor = new int[8, 8] {{1,0,1,0,1,0,1,0},
                                        {0,1,0,1,0,1,0,1},
                                        {1,0,1,0,1,0,1,0},
                                        {0,1,0,1,0,1,0,1},
                                        {1,0,1,0,1,0,1,0},
                                        {0,1,0,1,0,1,0,1},
                                        {1,0,1,0,1,0,1,0},
                                        {0,1,0,1,0,1,0,1},};
        int[,] Pawns = new int[8, 8] {{1,0,1,0,1,0,1,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,1,0,1,0,1,0,1},
                                        {1,0,1,0,1,0,1,0},
                                        {0,1,0,1,0,1,0,1},};
*/
        Game Game1 = new Game();

        public MainWindow()
        {
            InitializeComponent();
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            // Rectangle qrectangle = new Rectangle();
            // rectangle.Fill = new SolidColorBrush(Colors.Aqua);
            // rectangle.Width = 100;
            // rectangle.Height = 50;
//            Game1.InitializeNewGameData();
            DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);

            //DeleteBoard();
            //DrawBoard(Board1.BoardColor);
            /*int count = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button MyControl1 = new Button();
                   // Rectangle rectangle = new Rectangle();
                    MyControl1.Content = count.ToString();
                    //MyControl1.Content = rectangle;
                    MyControl1.Name = "Button" + count.ToString();
                     if(Board1.BoardColor[i,j]==1)
                        MyControl1.Background = Brushes.LightSeaGreen;
                     else
                        MyControl1.Background = Brushes.White;

                    if (Board1.Pawns[i, j] == (int)Pawn.Black)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("C:\\Users\\Cezary\\Desktop\\Image\\BlackPawn.png"));
                        // img.Stretch = Stretch.Fill;
                        // img.Width = 10;
                        // img.StretchDirection = StretchDirection.Both;
                        //Canvas canvas = new Canvas();
                        StackPanel stackPnl = new StackPanel();
                        //stackPnl.Orientation = Orientation.Horizontal;
                        //stackPnl.Margin = new Thickness(10);
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    if (Board1.Pawns[i, j] == (int)Pawn.BlackKing)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("C:\\Users\\Cezary\\Desktop\\Image\\BlackKing.png"));
                        // img.Stretch = Stretch.Fill;
                        // img.Width = 10;
                        // img.StretchDirection = StretchDirection.Both;
                        //Canvas canvas = new Canvas();
                        StackPanel stackPnl = new StackPanel();
                        //stackPnl.Orientation = Orientation.Horizontal;
                        //stackPnl.Margin = new Thickness(10);
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    if (Board1.Pawns[i, j] == (int)Pawn.White)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("C:\\Users\\Cezary\\Desktop\\Image\\WhitePawn.png"));
                        // img.Stretch = Stretch.Fill;
                        // img.Width = 10;
                        // img.StretchDirection = StretchDirection.Both;
                        //Canvas canvas = new Canvas();
                        StackPanel stackPnl = new StackPanel();
                        //stackPnl.Orientation = Orientation.Horizontal;
                        //stackPnl.Margin = new Thickness(10);
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    if (Board1.Pawns[i, j] == (int)Pawn.WhiteKing)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("C:\\Users\\Cezary\\Desktop\\Image\\WhiteKing.png"));
                        // img.Stretch = Stretch.Fill;
                        // img.Width = 10;
                        // img.StretchDirection = StretchDirection.Both;
                        //Canvas canvas = new Canvas();
                        StackPanel stackPnl = new StackPanel();
                        //stackPnl.Orientation = Orientation.Horizontal;
                        //stackPnl.Margin = new Thickness(10);
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    //  if(BoardColor[j,i]==1)
                    //    rectangle.Fill = new SolidColorBrush(Colors.LightSeaGreen);
                    //  else
                    //   rectangle.Fill = new SolidColorBrush(Colors.White);

                    MyControl1.Click += new RoutedEventHandler(Button_Click);

                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                   // Grid.SetColumn(rectangle, j);
                   // Grid.SetRow(rectangle, i);
                    CheckerGrid.Children.Add(MyControl1);
                   // CheckerGrid.Children.Add(rectangle);

                    count++;

                }

            }*/
            //CheckerGrid.Children.Clear();
        }

        private void UpdateInfoBoxes()
        {
            if (Game1.PlayerColor == (int)PlayerColors.White)
            {
                PlayerColorBox.Text = "White Player";

            }

            if (Game1.PlayerColor == (int)PlayerColors.Black)
            {
                PlayerColorBox.Text = "Black Player";

            }

            if (Game1.PlayerRound == (int)PlayerColors.White)
                PlayerRoundBox.Text = "White Player Round";

            if (Game1.PlayerRound == (int)PlayerColors.Black)
                PlayerRoundBox.Text = "Black Player Round";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button _btn = (Button)sender;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);

            if (Game1.GameStarted)
            {
                if (Game1.PlayerColor == Game1.PlayerRound)
                {
                    if (!Game1.PawnSelected)
                    {
                        if (Game1.CheckAndSelectPawn(_row, _column))
                        {
                            DeleteBoard();
                            DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
                            //MessageBox.Show(string.Format("Selected column {0}, row {1}", _column, _row));
                        }

                    }
                    else
                    {
                        if (Game1.CheckAndDeselectPawn(_row, _column))
                        {
                            DeleteBoard();
                            DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
                            if (Game1.CaptureAnotherPawn)
                            {
                                Game1.CaptureAnotherPawn = false;
                                Game1.SetEnemyRound();
                            }

                            // MessageBox.Show(string.Format("Deselected column {0}, row {1} ", _column, _row));
                        }
                        if (!Game1.CaptureAnotherPawn)
                            if (Game1.CheckAndMovePawn(_row, _column))
                            {
                            DeleteBoard();
                            DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
                            // MessageBox.Show(string.Format("Moved Pawn column {0}, row {1} ", _column, _row));
                            Game1.SetEnemyRound();
                            }

                        if (Game1.CheckAndCapturePawn(_row, _column))
                        {

                            if (!Game1.CanCaptureAnotherPawn(_row, _column))
                            {
                                // MessageBox.Show(string.Format("Capture column {0}, row {1} ", _column, _row));
                                Game1.SetEnemyRound();
                            }

                            DeleteBoard();
                            DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
                        }

                    }
                    UpdateInfoBoxes();
                    //                Thread.Sleep(1);
                    SendData(Game1.ConvertRoundAndPawnsToMessage());
                    if (Game1.GameStarted)
                        WinConditionWihoutDisconnect();





                }
            }




        } 

        public void WinConditionWihoutDisconnect()
        {
            int Winner = Game1.WhoWin();
            if (Winner == (int)PlayerColors.Black)
            {
                //Worker.CancelAsync(); //Przerwij polaczenie
                MessageBox.Show("Black Player is winner");
                DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
            }

            if (Winner == (int)PlayerColors.White)
            {
               // Worker.CancelAsync(); //Przerwij polaczenie
                MessageBox.Show("White Player is winner");
                DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
            }
        }

        public void SendData(string MessageToSend)
        {


            if (!(MessageToSend == ""))
            {

               // Messages.Items.Add("Klient(JA): " + MessageToSend);
                byte[] MessageConverted = Encoding.ASCII.GetBytes(MessageToSend);
                try
                {
                    ns.Write(MessageConverted, 0, MessageConverted.Length);

                }
                catch (Exception ex)
                {
                    Messages.Items.Add(ex.ToString());
                }
            }
        }

        public void DeleteBoard()
        {
            CheckerGrid.Children.Clear();
        }
       /* public void DrawBoard(int[,] BoardColor)
        {
            DeleteBoard();
            int count = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button MyControl1 = new Button();
                    MyControl1.Content = count.ToString();
                    MyControl1.Name = "Button" + count.ToString();
                    if (BoardColor[i, j] == 1)
                        MyControl1.Background = Brushes.LightSeaGreen;
                    else
                        MyControl1.Background = Brushes.White;
                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    MyControl1.Click += new RoutedEventHandler(Button_Click);
                    CheckerGrid.Children.Add(MyControl1);
                    count++;

                }

            }
        }*/

        public void DrawBoardandPawns(int[,] Pawns, int[,] BoardColor)
        {

            int count = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button MyControl1 = new Button();
                   // MyControl1.Content = count.ToString();
                    MyControl1.Name = "Button" + count.ToString();
                    if (BoardColor[i, j] == (int)CellBoardColor.Black)
                        MyControl1.Background = Brushes.LightSeaGreen;
                    if (BoardColor[i, j] == (int)CellBoardColor.White)
                        MyControl1.Background = Brushes.White;
                    if (BoardColor[i, j] == (int)CellBoardColor.Selected)
                        MyControl1.Background = Brushes.Yellow;

                    if (Pawns[i, j] == (int)Pawn.Black)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("img\\BlackPawn.png", UriKind.Relative));
                        StackPanel stackPnl = new StackPanel();
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    if (Pawns[i, j] == (int)Pawn.BlackKing)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("img\\BlackKing.png", UriKind.Relative));
                        StackPanel stackPnl = new StackPanel();
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    if (Pawns[i, j] == (int)Pawn.White)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("img\\WhitePawn.png", UriKind.Relative));
                        StackPanel stackPnl = new StackPanel();
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    if (Pawns[i, j] == (int)Pawn.WhiteKing)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri("img\\WhiteKing.png", UriKind.Relative));
                        StackPanel stackPnl = new StackPanel();
                        stackPnl.Children.Add(img);
                        MyControl1.Content = stackPnl;
                    }
                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    MyControl1.Click += new RoutedEventHandler(Button_Click);
                    CheckerGrid.Children.Add(MyControl1);
                    count++;

                }

            }


        }

        public class Game
        {
            public int[,] BoardColor;
            public int[,] OriginalBoardColor;
            public int[,] OriginalPawns;
            public int[,] Pawns;
            public int[,] NoPawns;
            public bool PawnSelected;
            public int PawnSelectedRow;
            public int PawnSelectedColumn;
            public int PlayerColor;
            public int PlayerRound;
            public bool GameStarted;
            public bool CaptureAnotherPawn;


            public Game()
            {
                GameStarted = false;
                CaptureAnotherPawn = false;
                OriginalBoardColor = new int[8, 8] {{1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},
                                                    {1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},
                                                    {1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},
                                                    {1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},};
                BoardColor = new int[8, 8] {{1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},
                                                    {1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},
                                                    {1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},
                                                    {1,0,1,0,1,0,1,0},
                                                    {0,1,0,1,0,1,0,1},};

                OriginalPawns = new int[8, 8]   {{3,0,3,0,3,0,3,0},
                                        {0,3,0,3,0,3,0,3},
                                        {3,0,3,0,3,0,3,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,1,0,1,0,1,0,1},
                                        {1,0,1,0,1,0,1,0},
                                        {0,1,0,1,0,1,0,1},};

                NoPawns = new int[8, 8]  {{0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},};

                Pawns = new int[8, 8]  {{0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0},};

                //  enum Pawn : int { Black = 1, BlackKing = 2, White = 3, WhiteKing = 4, None = 0 };
                PawnSelected = false;




            }
            public void EndGame()
            {
                GameStarted = false;
                PawnSelected = false;
                Array.Copy(NoPawns, 0, Pawns, 0, NoPawns.Length);

                //                PlayerColor = (int)PlayerColors.Black;
                //                PlayerRound = (int)PlayerColors.White;
            }
            public void SetEnemyRound()
            {
                if (PlayerColor == (int)PlayerColors.White)
                {
                    PlayerRound = (int)PlayerColors.Black;
                    //return "Black Player Round";
                }
                else
                {
                    PlayerRound = (int)PlayerColors.White;
                    // return "White Player Round";
                }

            }
            public void ConvertMessageToPawnsAndRound(string Message)
            {
                int[] array = new int[Pawns.Length];
                PlayerRound = Message[0] - '0';

                string PawnsMessage = Message.Substring(1);
                array = PawnsMessage.Select(c => c - '0').ToArray();
                for (int i = 0; i < Pawns.GetLength(0); i++)
                {
                    for (int j = 0; j < Pawns.GetLength(1); j++)
                    {
                        Pawns[i, j] = array[i * Pawns.GetLength(0) + j];
                    }
                }
            }
            public int WhoWin()
            {
                bool IsThereAnyWhitePawn = false;
                bool IsThereAnyBlackPawn = false;
                int result = (int)PlayerColors.None;

                for (int i = 0; i < Pawns.GetLength(0); i++)
                {
                    for (int j = 0; j < Pawns.GetLength(1); j++)
                    {
                        if (Pawns[i, j] == (int)Pawn.Black || Pawns[i, j] == (int)Pawn.BlackKing)
                        {
                            IsThereAnyBlackPawn = true;
                        }
                        if (Pawns[i, j] == (int)Pawn.White || Pawns[i, j] == (int)Pawn.WhiteKing)
                        {
                            IsThereAnyWhitePawn = true;
                        }
                    }
                }

                if (IsThereAnyBlackPawn && !IsThereAnyWhitePawn)
                {
                    result = (int)PlayerColors.Black;
                    Array.Copy(NoPawns, 0, Pawns, 0, NoPawns.Length);
                    GameStarted = false;
                }

                if (!IsThereAnyBlackPawn && IsThereAnyWhitePawn)
                {
                    result = (int)PlayerColors.White;
                    Array.Copy(NoPawns, 0, Pawns, 0, NoPawns.Length);
                    GameStarted = false;
                }

                return result;
            }

            public string ConvertRoundAndPawnsToMessage()
            {
                string PawnString;
                int[] array = new int[Pawns.Length];
                for (int i = 0; i < Pawns.GetLength(0); i++)
                {
                    for (int j = 0; j < Pawns.GetLength(1); j++)
                    {
                        array[i * Pawns.GetLength(0) + j] = Pawns[i, j];
                    }
                }
                PawnString = String.Join("", array.Select(p => p.ToString()).ToArray());
                return PlayerRound.ToString() + PawnString;
            }
            public void InitializeNewGameData()
            {
                GameStarted = true;
                Array.Copy(OriginalPawns, 0, Pawns, 0, OriginalPawns.Length);
                Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                PawnSelected = false;
                PlayerColor = (int)PlayerColors.White;
                PlayerRound = (int)PlayerColors.White;

            }

            public bool CheckAndSelectPawn(int ClickedRow, int ClickedColumn)
            {
                int PawnColor, KingColor;
                if (PlayerColor == (int)PlayerColors.Black)
                {
                    PawnColor = (int)Pawn.Black;
                    KingColor = (int)Pawn.BlackKing;
                }
                else
                {
                    PawnColor = (int)Pawn.White;
                    KingColor = (int)Pawn.WhiteKing;
                }
                if (Pawns[ClickedRow, ClickedColumn] == PawnColor || Pawns[ClickedRow, ClickedColumn] == KingColor)
                {
                    BoardColor[ClickedRow, ClickedColumn] = (int)CellBoardColor.Selected;
                    PawnSelected = true;
                    PawnSelectedRow = ClickedRow;
                    PawnSelectedColumn = ClickedColumn;
                    return true;
                }
                else
                {
                    PawnSelected = false;
                    return false;
                }
            }
            public bool CheckAndDeselectPawn(int ClickedRow, int ClickedColumn)
            {

                if (ClickedRow == PawnSelectedRow && ClickedColumn == PawnSelectedColumn)
                {

                    //  BoardColor[ClickedRow, ClickedColumn] = OriginalBoardColor[ClickedRow, ClickedColumn];
                    Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                    PawnSelected = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool CheckAndMovePawn(int ClickedRow, int ClickedColumn)
            {
                bool CanMove = false;
                int direction;
                int PawnColor;
                if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.Black || Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.White)
                {
                    if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.Black)
                    {
                        direction = -1;
                        PawnColor = (int)Pawn.Black;
                    }
                    else
                    {
                        direction = 1;
                        PawnColor = (int)Pawn.White;
                    }
                    if (((ClickedColumn == (PawnSelectedColumn - 1)) || (ClickedColumn == (PawnSelectedColumn + 1))) && (ClickedRow == (PawnSelectedRow + direction)) && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None))
                    {
                        Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                        Pawns[ClickedRow, ClickedColumn] = PawnColor;
                        TryConvertToKing(ClickedRow, ClickedColumn);
                        PawnSelected = false;
                        Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                        CanMove = true;

                    }
                }
                if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.BlackKing || Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.WhiteKing)
                {
                    if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.BlackKing)
                    {
                        PawnColor = (int)Pawn.BlackKing;
                    }
                    else
                    {
                        PawnColor = (int)Pawn.WhiteKing;
                    }
                    if (((ClickedColumn == (PawnSelectedColumn - 1)) || (ClickedColumn == (PawnSelectedColumn + 1))) &&( (ClickedRow == (PawnSelectedRow + 1)) || (ClickedRow == (PawnSelectedRow - 1))) && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None))
                    {
                        Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                        Pawns[ClickedRow, ClickedColumn] = PawnColor;
                        //TryConvertToKing(ClickedRow, ClickedColumn);
                        PawnSelected = false;
                        Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                        CanMove = true;

                    }
                }
                return CanMove;
            }
            public bool CanCaptureAnotherPawn(int Row, int Column)
            {
                bool CanHit = false;
                int PawnColor;
                int EnemyColor1, EnemyColor2;
                if (Pawns[Row, Column] == (int)Pawn.Black || Pawns[Row, Column] == (int)Pawn.White)
                {
                    if (Pawns[Row, Column] == (int)Pawn.Black)
                    {
                        PawnColor = (int)Pawn.Black;
                        EnemyColor1 = (int)Pawn.White;
                        EnemyColor2 = (int)Pawn.WhiteKing;
                    }
                    else
                    {
                        PawnColor = (int)Pawn.White;
                        EnemyColor1 = (int)Pawn.Black;
                        EnemyColor2 = (int)Pawn.BlackKing;
                    }
                    if (PawnColor == (int)Pawn.Black)
                    {
                        if (Row - 2 >= 0 && Column - 2 >= 0)
                            if (((Pawns[Row - 1, Column - 1] == EnemyColor1) || (Pawns[Row - 1, Column - 1] == EnemyColor2)) && (Pawns[Row - 2, Column - 2] == (int)Pawn.None))
                                CanHit = true;

                        if (Row - 2 >= 0 && Column + 2 < Pawns.GetLength(1))
                            if (((Pawns[Row - 1, Column + 1] == EnemyColor1) || (Pawns[Row - 1, Column + 1] == EnemyColor2)) && (Pawns[Row - 2, Column + 2] == (int)Pawn.None))
                                CanHit = true;
                    }
                    if (PawnColor == (int)Pawn.White)
                    {
                        if (Row + 2 < Pawns.GetLength(0) && Column + 2 < Pawns.GetLength(1))
                            if (((Pawns[Row + 1, Column + 1] == EnemyColor1) || (Pawns[Row + 1, Column + 1] == EnemyColor2)) && (Pawns[Row + 2, Column + 2] == (int)Pawn.None))
                                CanHit = true;

                        if (Row + 2 < Pawns.GetLength(0) && Column - 2 >= 0)
                            if (((Pawns[Row + 1, Column - 1] == EnemyColor1) || (Pawns[Row + 1, Column - 1] == EnemyColor2)) && (Pawns[Row + 2, Column - 2] == (int)Pawn.None))
                                CanHit = true;
                    }




                }
                if (Pawns[Row, Column] == (int)Pawn.BlackKing || Pawns[Row, Column] == (int)Pawn.WhiteKing)
                {
                    if (Pawns[Row, Column] == (int)Pawn.BlackKing)
                    {
                        //  PawnColor = (int)Pawn.Black;
                        EnemyColor1 = (int)Pawn.White;
                        EnemyColor2 = (int)Pawn.WhiteKing;
                    }
                    else
                    {
                        // PawnColor = (int)Pawn.White;
                        EnemyColor1 = (int)Pawn.Black;
                        EnemyColor2 = (int)Pawn.BlackKing;
                    }
                    if (Row - 2 >= 0 && Column - 2 >= 0)
                        if (((Pawns[Row - 1, Column - 1] == EnemyColor1) || (Pawns[Row - 1, Column - 1] == EnemyColor2)) && (Pawns[Row - 2, Column - 2] == (int)Pawn.None))
                            CanHit = true;
                    if (Row + 2 < Pawns.GetLength(0) && Column + 2 < Pawns.GetLength(1))
                        if (((Pawns[Row + 1, Column + 1] == EnemyColor1) || (Pawns[Row + 1, Column + 1] == EnemyColor2)) && (Pawns[Row + 2, Column + 2] == (int)Pawn.None))
                            CanHit = true;

                    if (Row + 2 < Pawns.GetLength(0) && Column - 2 >= 0)
                        if (((Pawns[Row + 1, Column - 1] == EnemyColor1) || (Pawns[Row + 1, Column - 1] == EnemyColor2)) && (Pawns[Row + 2, Column - 2] == (int)Pawn.None))
                            CanHit = true;

                    if (Row - 2 >= 0 && Column + 2 < Pawns.GetLength(1))
                        if (((Pawns[Row - 1, Column + 1] == EnemyColor1) || (Pawns[Row - 1, Column + 1] == EnemyColor2)) && (Pawns[Row - 2, Column + 2] == (int)Pawn.None))
                            CanHit = true;



                }
                CaptureAnotherPawn = CanHit;
                if (CaptureAnotherPawn)
                {
                    PawnSelected = true;
                    PawnSelectedRow = Row;
                    PawnSelectedColumn = Column;
                    BoardColor[PawnSelectedRow, PawnSelectedColumn] = (int)CellBoardColor.Selected;
                }
                return CanHit;
            }


            public void TryConvertToKing(int Row, int Column)
            {

                if (Pawns[Row, Column] == (int)Pawn.Black)
                {
                    if (Row == 0)
                        Pawns[Row, Column] = (int)Pawn.BlackKing;
                }

                if (Pawns[Row, Column] == (int)Pawn.White)
                {
                    if (Row == (Pawns.GetLength(1) - 1))
                        Pawns[Row, Column] = (int)Pawn.WhiteKing;
                }

            }

            public bool CheckAndCapturePawn(int ClickedRow, int ClickedColumn)
            {
                bool CanMove = false;
                int PawnColor;
                int EnemyColor1, EnemyColor2;
                if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.Black || Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.White)
                {
                    if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.Black)
                    {
                        PawnColor = (int)Pawn.Black;
                        EnemyColor1 = (int)Pawn.White;
                        EnemyColor2 = (int)Pawn.WhiteKing;
                    }
                    else
                    {
                        PawnColor = (int)Pawn.White;
                        EnemyColor1 = (int)Pawn.Black;
                        EnemyColor2 = (int)Pawn.BlackKing;
                    }
                    if (PawnColor == (int)Pawn.Black)
                    {
                        if ((ClickedColumn == (PawnSelectedColumn - 2)) && (ClickedRow == (PawnSelectedRow - 2))
                            && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                            && (((Pawns[PawnSelectedRow - 1, PawnSelectedColumn - 1] == EnemyColor1)) || (Pawns[PawnSelectedRow - 1, PawnSelectedColumn - 1] == EnemyColor2)))
                        {
                            Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                            Pawns[PawnSelectedRow - 1, PawnSelectedColumn - 1] = (int)Pawn.None;
                            Pawns[ClickedRow, ClickedColumn] = PawnColor;
                            TryConvertToKing(ClickedRow, ClickedColumn);
                            PawnSelected = false;
                            Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                            CanMove = true;

                        }
                        if ((ClickedColumn == (PawnSelectedColumn + 2)) && (ClickedRow == (PawnSelectedRow - 2))
                            && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                            && (((Pawns[PawnSelectedRow - 1, PawnSelectedColumn + 1] == EnemyColor1)) || (Pawns[PawnSelectedRow - 1, PawnSelectedColumn + 1] == EnemyColor2)))
                        {
                            Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                            Pawns[PawnSelectedRow - 1, PawnSelectedColumn + 1] = (int)Pawn.None;
                            Pawns[ClickedRow, ClickedColumn] = PawnColor;
                            TryConvertToKing(ClickedRow, ClickedColumn);
                            PawnSelected = false;
                            Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                            CanMove = true;

                        }
                    }
                    if (PawnColor == (int)Pawn.White)
                    {
                        if ((ClickedColumn == (PawnSelectedColumn - 2)) && (ClickedRow == (PawnSelectedRow + 2))
                        && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                        && (((Pawns[PawnSelectedRow + 1, PawnSelectedColumn - 1] == EnemyColor1)) || (Pawns[PawnSelectedRow + 1, PawnSelectedColumn - 1] == EnemyColor2)))
                        {
                            Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                            Pawns[PawnSelectedRow + 1, PawnSelectedColumn - 1] = (int)Pawn.None;
                            Pawns[ClickedRow, ClickedColumn] = PawnColor;
                            TryConvertToKing(ClickedRow, ClickedColumn);
                            PawnSelected = false;
                            Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                            CanMove = true;

                        }
                        if ((ClickedColumn == (PawnSelectedColumn + 2)) && (ClickedRow == (PawnSelectedRow + 2))
                            && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                            && (((Pawns[PawnSelectedRow + 1, PawnSelectedColumn + 1] == EnemyColor1)) || (Pawns[PawnSelectedRow + 1, PawnSelectedColumn + 1] == EnemyColor2)))
                        {
                            Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                            Pawns[PawnSelectedRow + 1, PawnSelectedColumn + 1] = (int)Pawn.None;
                            Pawns[ClickedRow, ClickedColumn] = PawnColor;
                            TryConvertToKing(ClickedRow, ClickedColumn);
                            PawnSelected = false;
                            Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                            CanMove = true;

                        }

                    }
                }
                if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.BlackKing || Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.WhiteKing)
                {
                    if (Pawns[PawnSelectedRow, PawnSelectedColumn] == (int)Pawn.BlackKing)
                    {
                        PawnColor = (int)Pawn.BlackKing;
                        EnemyColor1 = (int)Pawn.White;
                        EnemyColor2 = (int)Pawn.WhiteKing;
                    }
                    else
                    {
                        PawnColor = (int)Pawn.WhiteKing;
                        EnemyColor1 = (int)Pawn.Black;
                        EnemyColor2 = (int)Pawn.BlackKing;
                    }
                    if ((ClickedColumn == (PawnSelectedColumn - 2)) && (ClickedRow == (PawnSelectedRow - 2))
                        && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                        && (((Pawns[PawnSelectedRow - 1, PawnSelectedColumn - 1] == EnemyColor1)) || (Pawns[PawnSelectedRow - 1, PawnSelectedColumn - 1] == EnemyColor2)))
                    {
                        Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                        Pawns[PawnSelectedRow - 1, PawnSelectedColumn - 1] = (int)Pawn.None;
                        Pawns[ClickedRow, ClickedColumn] = PawnColor;
                        TryConvertToKing(ClickedRow, ClickedColumn);
                        PawnSelected = false;
                        Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                        CanMove = true;

                    }
                    if ((ClickedColumn == (PawnSelectedColumn + 2)) && (ClickedRow == (PawnSelectedRow - 2))
                        && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                        && (((Pawns[PawnSelectedRow - 1, PawnSelectedColumn + 1] == EnemyColor1)) || (Pawns[PawnSelectedRow - 1, PawnSelectedColumn + 1] == EnemyColor2)))
                    {
                        Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                        Pawns[PawnSelectedRow - 1, PawnSelectedColumn + 1] = (int)Pawn.None;
                        Pawns[ClickedRow, ClickedColumn] = PawnColor;
                        TryConvertToKing(ClickedRow, ClickedColumn);
                        PawnSelected = false;
                        Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                        CanMove = true;

                    }
                    if ((ClickedColumn == (PawnSelectedColumn - 2)) && (ClickedRow == (PawnSelectedRow + 2))
                        && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                        && (((Pawns[PawnSelectedRow + 1, PawnSelectedColumn - 1] == EnemyColor1)) || (Pawns[PawnSelectedRow + 1, PawnSelectedColumn - 1] == EnemyColor2)))
                    {
                        Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                        Pawns[PawnSelectedRow + 1, PawnSelectedColumn - 1] = (int)Pawn.None;
                        Pawns[ClickedRow, ClickedColumn] = PawnColor;
                        TryConvertToKing(ClickedRow, ClickedColumn);
                        PawnSelected = false;
                        Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                        CanMove = true;

                    }
                    if ((ClickedColumn == (PawnSelectedColumn + 2)) && (ClickedRow == (PawnSelectedRow + 2))
                        && (Pawns[ClickedRow, ClickedColumn] == (int)Pawn.None)
                        && (((Pawns[PawnSelectedRow + 1, PawnSelectedColumn + 1] == EnemyColor1)) || (Pawns[PawnSelectedRow + 1, PawnSelectedColumn + 1] == EnemyColor2)))
                    {
                        Pawns[PawnSelectedRow, PawnSelectedColumn] = (int)Pawn.None;
                        Pawns[PawnSelectedRow + 1, PawnSelectedColumn + 1] = (int)Pawn.None;
                        Pawns[ClickedRow, ClickedColumn] = PawnColor;
                        TryConvertToKing(ClickedRow, ClickedColumn);
                        PawnSelected = false;
                        Array.Copy(OriginalBoardColor, 0, BoardColor, 0, OriginalBoardColor.Length);
                        CanMove = true;

                    }

                }
                return CanMove;
            }


        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            Messages.Items.Clear();
            portNum = int.Parse(PortServer.Text);
            hostName = IPserver.Text;
            Worker.RunWorkerAsync();
            UpdateInfoBoxes();

        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Worker.CancelAsync();

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
                            // else
                            //   EncodedMessage = "Server(Ktos): " + EncodedMessage;

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

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

                Messages.Items.Add("Disconnecting...");
                //string MessageToSend = Message.Text;
                //ChatWindow.Items.Add("Client(JA): " + MessageToSend);
                byte[] MessageConverted = Encoding.ASCII.GetBytes("Close Connection");
                try
                {
                    ns.Write(MessageConverted, 0, MessageConverted.Length);

                }
                catch (Exception ex)
                {
                    Messages.Items.Add(ex.ToString());
                }
            }
            else
            {
                if (Game1.GameStarted)
                    WinConditionWihoutDisconnect();

                if (Game1.GameStarted)
                    MessageBox.Show("Player has disconnected");
            }
            Game1.EndGame();
            DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
            if (ns != null)
            {
                ns.Close();
                Messages.Items.Add("NetworkStream Closed");
            }
            if (client != null)
            {
                client.Close();
                Messages.Items.Add("Client Closed");
                // MessageBox.Show("Numbers between 0 and 10000 divisible by 7: " + e.Result);
            }

        }
        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // pbCalculationProgress.Value = e.ProgressPercentage;
            //  if (e.UserState != null)

            string Buffer = (string)(e.UserState);
            if (Buffer.Length != 65)
                Messages.Items.Add(e.UserState);
            if (Buffer == "Connected")
            {
                Game1.InitializeNewGameData();
                DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
            }

            if (Buffer.Length == 65)
            {
                bool ok = true;
                foreach (char c in Buffer)
                    if (!(('0'.Equals(c)) || ('1'.Equals(c)) || ('2'.Equals(c)) || ('3'.Equals(c)) || ('4'.Equals(c))))
                        ok = false;
                if (ok)
                {
                    Game1.ConvertMessageToPawnsAndRound((string)e.UserState);
                    DeleteBoard();
                    DrawBoardandPawns(Game1.Pawns, Game1.BoardColor);
                    if (Game1.GameStarted)
                        WinConditionWihoutDisconnect();
                }
            }
            UpdateInfoBoxes();
        }
    }
}
    

