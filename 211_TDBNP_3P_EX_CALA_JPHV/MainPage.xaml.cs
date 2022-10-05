using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _211_TDBNP_3P_EX_CALA_JPHV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Entry> leaderboardList = new List<Entry>();
        private int size = 10;
        private Uri[,] uri = new Uri[10, 10];
        private Image[,] img = new Image[10,10];
        private Rectangle[,] rect = new Rectangle[10,10];
        private Box[,] boxes = new Box[10,10];
        private int flags;
        private int toWin;
        private bool playing;
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime inicio;
        private TimeSpan pausa = TimeSpan.Zero;
        private DateTime pausaInicio;
        private DispatcherTimer pausaTimer = new DispatcherTimer();
        private bool first = true;

        public MainPage()
        {
            this.InitializeComponent();
            RepeatBehavior rb = new RepeatBehavior();
            rb.Type = RepeatBehaviorType.Forever;
            this.sbRGB.RepeatBehavior = rb;
            this.sbRGB.Begin();
            this.sbMine.RepeatBehavior = rb;
            this.sbMine.Begin();
            this.sbFlag.RepeatBehavior = rb;
            this.sbFlag.Begin();

            fillArrays();
            start();

            inicio = DateTime.Now;

            timer.Tick += OnTimeEvent;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
            first = false;

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (((Sender)e.Parameter).entries != null)
                    leaderboardList = ((Sender)e.Parameter).entries;
                if (((Sender)e.Parameter).name == null)
                {
                    leaderboardList.Sort(delegate (Entry c1, Entry c2) { return c1.time.CompareTo(c2.time); });
                    foreach (var x in leaderboardList)
                        this.lstLeaderboard.Items.Add($"{x.name} - {x.time}");
                    return;
                }
                leaderboardList.Add(new Entry()
                {
                    name = ((Sender)e.Parameter).name,
                    time = ((Sender)e.Parameter).time
                });
                leaderboardList.Sort(delegate (Entry c1, Entry c2) { return c1.time.CompareTo(c2.time); });
                foreach (var x in leaderboardList)
                    this.lstLeaderboard.Items.Add($"{x.name} - {x.time}");
            }
            catch(InvalidCastException jk)
            {

            }
            
        }

        private void OnTimeEvent(Object sender, object e)
        {
            TimeSpan ts = DateTime.Now - inicio - pausa;
            this.txtMinas_Copy.Text = ts.ToString().Substring(3, 8);
        }

        private void start()
        {
            this.playing = true;
            this.flags = Convert.ToInt32(this.sldMinas.Value);
            this.toWin = 100 - flags;
            this.txtMinas.Text = $"{flags}";

            for (int i =0; i < boxes.GetLength(0); i++)
                for (int j = 0; j < boxes.GetLength(1); j++)
                {
                    boxes[i, j] = new Box();
                }
            generateMines();

            generateNumbers();
        }

        private void generateNumbers()
        {
            for (int n = 0; n < this.boxes.GetLength(0); n++)
                for(int m = 0; m < this.boxes.GetLength(1); m++)
                {
                    if(!this.boxes[n, m].mine)
                    {
                        int count = 0;
                        for (int i = n - 1; i <= n + 1; i++)
                            for (int j = m - 1; j <= m + 1; j++)
                                if (i >= 0 && j >= 0 && i < this.size && j < this.size && !(i == n && j == m ))
                                    count += this.boxes[i, j].mine ? 1 : 0;
                        this.boxes[n, m].num = count;
                        this.uri[n, m] = new Uri($"ms-appx:///Assets/img/n{count}.png");
                    }
                }
        }

        private void generateMines()
        {
            Random rnd = new Random();

            for (int n = 0; n < this.sldMinas.Value; n++)
            {
                int i = rnd.Next(this.size);
                int j = rnd.Next(this.size);
                if (!boxes[i, j].mine)
                {
                    boxes[i, j].mine = true;
                    this.uri[i, j] = new Uri("ms-appx:///Assets/img/mina.png");
                }
                else
                    n--;
            }

        }

        private void fillArrays()
        {
            this.img[0, 0] = this.img00;
            this.img[0, 1] = this.img01;
            this.img[0, 2] = this.img02;
            this.img[0, 3] = this.img03;
            this.img[0, 4] = this.img04;
            this.img[0, 5] = this.img05;
            this.img[0, 6] = this.img06;
            this.img[0, 7] = this.img07;
            this.img[0, 8] = this.img08;
            this.img[0, 9] = this.img09;

            this.img[1, 0] = this.img10;
            this.img[1, 1] = this.img11;
            this.img[1, 2] = this.img12;
            this.img[1, 3] = this.img13;
            this.img[1, 4] = this.img14;
            this.img[1, 5] = this.img15;
            this.img[1, 6] = this.img16;
            this.img[1, 7] = this.img17;
            this.img[1, 8] = this.img18;
            this.img[1, 9] = this.img19;

            this.img[2, 0] = this.img20;
            this.img[2, 1] = this.img21;
            this.img[2, 2] = this.img22;
            this.img[2, 3] = this.img23;
            this.img[2, 4] = this.img24;
            this.img[2, 5] = this.img25;
            this.img[2, 6] = this.img26;
            this.img[2, 7] = this.img27;
            this.img[2, 8] = this.img28;
            this.img[2, 9] = this.img29;

            this.img[3, 0] = this.img30;
            this.img[3, 1] = this.img31;
            this.img[3, 2] = this.img32;
            this.img[3, 3] = this.img33;
            this.img[3, 4] = this.img34;
            this.img[3, 5] = this.img35;
            this.img[3, 6] = this.img36;
            this.img[3, 7] = this.img37;
            this.img[3, 8] = this.img38;
            this.img[3, 9] = this.img39;

            this.img[4, 0] = this.img40;
            this.img[4, 1] = this.img41;
            this.img[4, 2] = this.img42;
            this.img[4, 3] = this.img43;
            this.img[4, 4] = this.img44;
            this.img[4, 5] = this.img45;
            this.img[4, 6] = this.img46;
            this.img[4, 7] = this.img47;
            this.img[4, 8] = this.img48;
            this.img[4, 9] = this.img49;

            this.img[5, 0] = this.img50;
            this.img[5, 1] = this.img51;
            this.img[5, 2] = this.img52;
            this.img[5, 3] = this.img53;
            this.img[5, 4] = this.img54;
            this.img[5, 5] = this.img55;
            this.img[5, 6] = this.img56;
            this.img[5, 7] = this.img57;
            this.img[5, 8] = this.img58;
            this.img[5, 9] = this.img59;

            this.img[6, 0] = this.img60;
            this.img[6, 1] = this.img61;
            this.img[6, 2] = this.img62;
            this.img[6, 3] = this.img63;
            this.img[6, 4] = this.img64;
            this.img[6, 5] = this.img65;
            this.img[6, 6] = this.img66;
            this.img[6, 7] = this.img67;
            this.img[6, 8] = this.img68;
            this.img[6, 9] = this.img69;

            this.img[7, 0] = this.img70;
            this.img[7, 1] = this.img71;
            this.img[7, 2] = this.img72;
            this.img[7, 3] = this.img73;
            this.img[7, 4] = this.img74;
            this.img[7, 5] = this.img75;
            this.img[7, 6] = this.img76;
            this.img[7, 7] = this.img77;
            this.img[7, 8] = this.img78;
            this.img[7, 9] = this.img79;

            this.img[8, 0] = this.img80;
            this.img[8, 1] = this.img81;
            this.img[8, 2] = this.img82;
            this.img[8, 3] = this.img83;
            this.img[8, 4] = this.img84;
            this.img[8, 5] = this.img85;
            this.img[8, 6] = this.img86;
            this.img[8, 7] = this.img87;
            this.img[8, 8] = this.img88;
            this.img[8, 9] = this.img89;

            this.img[9, 0] = this.img90;
            this.img[9, 1] = this.img91;
            this.img[9, 2] = this.img92;
            this.img[9, 3] = this.img93;
            this.img[9, 4] = this.img94;
            this.img[9, 5] = this.img95;
            this.img[9, 6] = this.img96;
            this.img[9, 7] = this.img97;
            this.img[9, 8] = this.img98;
            this.img[9, 9] = this.img99;


            this.rect[0, 0] = this.rect00;
            this.rect[0, 1] = this.rect01;
            this.rect[0, 2] = this.rect02;
            this.rect[0, 3] = this.rect03;
            this.rect[0, 4] = this.rect04;
            this.rect[0, 5] = this.rect05;
            this.rect[0, 6] = this.rect06;
            this.rect[0, 7] = this.rect07;
            this.rect[0, 8] = this.rect08;
            this.rect[0, 9] = this.rect09;
                                          
            this.rect[1, 0] = this.rect10;
            this.rect[1, 1] = this.rect11;
            this.rect[1, 2] = this.rect12;
            this.rect[1, 3] = this.rect13;
            this.rect[1, 4] = this.rect14;
            this.rect[1, 5] = this.rect15;
            this.rect[1, 6] = this.rect16;
            this.rect[1, 7] = this.rect17;
            this.rect[1, 8] = this.rect18;
            this.rect[1, 9] = this.rect19;
                                          
            this.rect[2, 0] = this.rect20;
            this.rect[2, 1] = this.rect21;
            this.rect[2, 2] = this.rect22;
            this.rect[2, 3] = this.rect23;
            this.rect[2, 4] = this.rect24;
            this.rect[2, 5] = this.rect25;
            this.rect[2, 6] = this.rect26;
            this.rect[2, 7] = this.rect27;
            this.rect[2, 8] = this.rect28;
            this.rect[2, 9] = this.rect29;
                                          
            this.rect[3, 0] = this.rect30;
            this.rect[3, 1] = this.rect31;
            this.rect[3, 2] = this.rect32;
            this.rect[3, 3] = this.rect33;
            this.rect[3, 4] = this.rect34;
            this.rect[3, 5] = this.rect35;
            this.rect[3, 6] = this.rect36;
            this.rect[3, 7] = this.rect37;
            this.rect[3, 8] = this.rect38;
            this.rect[3, 9] = this.rect39;
                                          
            this.rect[4, 0] = this.rect40;
            this.rect[4, 1] = this.rect41;
            this.rect[4, 2] = this.rect42;
            this.rect[4, 3] = this.rect43;
            this.rect[4, 4] = this.rect44;
            this.rect[4, 5] = this.rect45;
            this.rect[4, 6] = this.rect46;
            this.rect[4, 7] = this.rect47;
            this.rect[4, 8] = this.rect48;
            this.rect[4, 9] = this.rect49;
                                          
            this.rect[5, 0] = this.rect50;
            this.rect[5, 1] = this.rect51;
            this.rect[5, 2] = this.rect52;
            this.rect[5, 3] = this.rect53;
            this.rect[5, 4] = this.rect54;
            this.rect[5, 5] = this.rect55;
            this.rect[5, 6] = this.rect56;
            this.rect[5, 7] = this.rect57;
            this.rect[5, 8] = this.rect58;
            this.rect[5, 9] = this.rect59;
                                          
            this.rect[6, 0] = this.rect60;
            this.rect[6, 1] = this.rect61;
            this.rect[6, 2] = this.rect62;
            this.rect[6, 3] = this.rect63;
            this.rect[6, 4] = this.rect64;
            this.rect[6, 5] = this.rect65;
            this.rect[6, 6] = this.rect66;
            this.rect[6, 7] = this.rect67;
            this.rect[6, 8] = this.rect68;
            this.rect[6, 9] = this.rect69;
                                          
            this.rect[7, 0] = this.rect70;
            this.rect[7, 1] = this.rect71;
            this.rect[7, 2] = this.rect72;
            this.rect[7, 3] = this.rect73;
            this.rect[7, 4] = this.rect74;
            this.rect[7, 5] = this.rect75;
            this.rect[7, 6] = this.rect76;
            this.rect[7, 7] = this.rect77;
            this.rect[7, 8] = this.rect78;
            this.rect[7, 9] = this.rect79;
                                          
            this.rect[8, 0] = this.rect80;
            this.rect[8, 1] = this.rect81;
            this.rect[8, 2] = this.rect82;
            this.rect[8, 3] = this.rect83;
            this.rect[8, 4] = this.rect84;
            this.rect[8, 5] = this.rect85;
            this.rect[8, 6] = this.rect86;
            this.rect[8, 7] = this.rect87;
            this.rect[8, 8] = this.rect88;
            this.rect[8, 9] = this.rect89;
                                          
            this.rect[9, 0] = this.rect90;
            this.rect[9, 1] = this.rect91;
            this.rect[9, 2] = this.rect92;
            this.rect[9, 3] = this.rect93;
            this.rect[9, 4] = this.rect94;
            this.rect[9, 5] = this.rect95;
            this.rect[9, 6] = this.rect96;
            this.rect[9, 7] = this.rect97;
            this.rect[9, 8] = this.rect98;
            this.rect[9, 9] = this.rect99;
        }                                 

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = true;
            
        }

        private void rect_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int i = int.Parse(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Substring(4,1));
            int j = int.Parse(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Substring(5,1));

            if (this.boxes[i, j].flagged || !this.playing) return;

            unlock(i, j);   

        }

        private void unlock(int n, int m)
        {
            if (this.rect[n,m].Visibility == Visibility.Collapsed) return;
            this.rect[n, m].Visibility = Visibility.Collapsed;
            
            if (this.boxes[n, m].mine)
            {
                lose();
                this.img[n, m].Source = new BitmapImage(new Uri("ms-appx:///Assets/img/redMine.png"));
                return;
            }
            this.boxes[n, m].unlocked = true;
            this.toWin--;
            img[n, m].Source = new BitmapImage(uri[n, m]);

            if (this.boxes[n, m].num != 0)
            {
                if (this.toWin <= 0)
                {
                    TimeSpan final = DateTime.Now - inicio - pausa;
                    this.Frame.Navigate(typeof(Win), new Sender()
                    {
                        time = final,
                        entries = leaderboardList
                    });

                }
                return;
            }

            for (int i = n - 1; i <= n + 1; i++)
                for (int j = m - 1; j <= m + 1; j++)
                    if (i >= 0 && j >= 0 && i < this.size && j < this.size && !(i == n && j == m))
                        if (!this.boxes[i,j].flagged && !this.boxes[i, j].unlocked)
                            unlock(i, j);
            if (this.toWin <= 0)
            {
                TimeSpan final = DateTime.Now - inicio - pausa;
                this.Frame.Navigate(typeof(Win), new Sender()
                {
                    time = final,
                    entries = leaderboardList
                });

            }
        }

        private void lose()
        {
            sbCarita.AutoReverse = false;
            this.sbCarita.Begin();
            this.playing = false;
            for (int i = 0; i < this.boxes.GetLength(0); i++)
                for (int j = 0; j < this.boxes.GetLength(1); j++)
                    if (this.boxes[i, j].mine)
                    {
                        this.rect[i, j].Visibility = Visibility.Collapsed;
                        this.img[i, j].Source = new BitmapImage(uri[i,j]);
                    }
                        
        }

        private void rect_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            int i = int.Parse(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Substring(4, 1));
            int j = int.Parse(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Substring(5, 1));


            if (!(this.boxes[i, j].flagged = !this.boxes[i, j].flagged))
            {
                this.rect[i, j].Fill = new ImageBrush()
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/img/closed.png"))
                };
                this.flags++;
            }
            else
            {
                if (this.flags <= 0 || !this.playing) return;
                this.rect[i, j].Fill = new ImageBrush()
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/img/flag.png"))
                };
                this.flags--;
            }


            this.txtMinas.Text = $"{flags}";
        }

        private void btnCarita_Click(object sender, RoutedEventArgs e)
        {
            if (!this.playing)
            {
                this.Frame.Navigate(typeof(MainPage),new Sender()
                {
                    entries = leaderboardList
                });
            }
        }

        private void swtPause_Toggled(object sender, RoutedEventArgs e)
        {
            if (swtPause.IsOn)
            {
                timer.Stop();
                pausaInicio = DateTime.Now;

                
            }
            else
            {
                pausa -= (pausaInicio - DateTime.Now);
                timer.Start();
                pausaTimer.Stop();
            }
        }

        private void sldMinas_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (first) return;
            start();

            for (int i = 0; i < this.rect.GetLength(0); i++)
                for (int j = 0; j < this.rect.GetLength(1); j++)
                    rect[i,j].Visibility = Visibility.Visible;

            inicio = DateTime.Now;

            timer.Tick += OnTimeEvent;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();

            pausa = TimeSpan.Zero;
        }

        private void img_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int n = int.Parse(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Substring(3, 1));
            int m = int.Parse(sender.GetType().GetProperty("Name").GetValue(sender).ToString().Substring(4, 1));


            int count = 0;
            for (int i = n - 1; i <= n + 1; i++)
                for (int j = m - 1; j <= m + 1; j++)
                    if (i >= 0 && j >= 0 && i < this.size && j < this.size && !(i == n && j == m))
                        if (boxes[i, j].flagged)
                            count++;
            if (count == boxes[n, m].num)
                for (int i = n - 1; i <= n + 1; i++)
                    for (int j = m - 1; j <= m + 1; j++)
                        if (i >= 0 && j >= 0 && i < this.size && j < this.size && !(i == n && j == m))
                            if (!boxes[i, j].flagged)
                                unlock(i, j);
        }
    }

}
