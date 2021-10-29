using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using System.Windows.Input;
using Color = System.Windows.Media.Color;
using System.Windows.Media.Effects;
using System.Linq;

namespace Lingo_animations
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawTextblocks_10_C(Word_canvas_10_C);
            DrawTextblocks_10_R(Word_canvas_10_R);
        }

        int Word_canvas = 100;
        public int size = 100;
        public Label[] L_10_C = new Label[10];
        public Border[] B_10_C = new Border[10];
        public Label[] L_10_R = new Label[10];
        public Border[] B_10_R = new Border[10];
        public List<string> correct_word_10 = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public List<string> random_word_10 = new List<string>();
        public int score_10 = 70;
        public bool pause = false;

        public static DropShadowEffect drop = new DropShadowEffect
        {
            Color = new Color { A = 0, R = 0, G = 0, B = 0 },
            Direction = 315,
            ShadowDepth = 5,
            Opacity = 1,
            BlurRadius = 5,
            RenderingBias = RenderingBias.Quality,
        };

        public void DrawTextblocks_10_C(UniformGrid Word_canvas_10_C)
        {
            for (int j = 0; j < 10; j++)
            { 
                RadialGradientBrush Red = new RadialGradientBrush()
                {

                };
                GradientStop stop1 = new GradientStop(Colors.Maroon, 1.0);
                GradientStop stop2 = new GradientStop(Colors.Red, 0.251);
                Red.GradientStops.Add(stop1);
                Red.GradientStops.Add(stop2);

                Border Background_10 = new Border
                {
                    Background = Red,
                    Width = size,
                    Height = size,
                    Margin = new Thickness(0, 0, 0, 0),
                    BorderBrush = new SolidColorBrush(Colors.DarkGray),
                    BorderThickness = new Thickness(6),
                };

                Label Label_10 = new Label
                {
                    Height = size,
                    Effect = drop,
                    Width = size,
                    FontSize = 75,
                    Background = new SolidColorBrush(Colors.Transparent),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(-5, -20, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                };

                B_10_C[j] = Background_10;
                Background_10.Child = Label_10;
                Word_canvas_10_C.Children.Add(Background_10);
                Canvas.SetLeft(Label_10, j * Word_canvas);
                Canvas.SetTop(Label_10, j * Word_canvas);
                L_10_C[j] = Label_10;

            }
        }

        public void DrawTextblocks_10_R(UniformGrid Word_canvas_10_R)
        {
            for (int j = 0; j < 10; j++)
            {
                RadialGradientBrush Yellow_block = new RadialGradientBrush
                {
                    GradientOrigin = new Point(0.5, 0.5),
                    Center = new Point(0.5, 0.5),
                    RadiusX = 0.45,
                    RadiusY = 0.45
                };
                Yellow_block.GradientStops.Add(new GradientStop(Colors.Goldenrod, 1.0));
                Yellow_block.GradientStops.Add(new GradientStop(Colors.DarkBlue, 1.0));

                Border Background_10 = new Border
                {
                    Background = Yellow_block,
                    Width = size,
                    Height = size,
                    Margin = new Thickness(0,0,0,0),
                    BorderBrush = new SolidColorBrush(Colors.DarkGray),
                    BorderThickness = new Thickness(6),
                };

                Label Label_10 = new Label
                {
                    Height = size,
                    Effect = drop,
                    Width = size,
                    FontSize = 75,
                    Background = new SolidColorBrush(Colors.Transparent),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(-5, -20, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                };

                B_10_R[j] = Background_10;
                Background_10.Child = Label_10;
                Word_canvas_10_R.Children.Add(Background_10);
                Canvas.SetLeft(Label_10, j * 118);
                Canvas.SetTop(Label_10, j * 118);
                L_10_R[j] = Label_10;
            }
        }

        private async void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.T)
            {
                Random random = new Random();
                random_word_10 = correct_word_10.OrderBy(x => random.Next()).ToList();

                for (int i = 0; i < correct_word_10.Count; i++)
                {
                    L_10_C[i].Content = correct_word_10[i].ToString();
                    L_10_R[i].Content = random_word_10[i].ToString();
                }

                List<int> numbers = new List<int>(10);
                L_score_10.Content = score_10.ToString();

                SoundPlayer Lingos = new SoundPlayer { Stream = Properties.Resources.Start_woord };
                Lingos.Play();

                Word_canvas_10_C.Visibility = Visibility.Visible;
                _10_score.Visibility = Visibility.Visible;

                Word_canvas_10_C.RenderTransform = new TranslateTransform(0, 118);
                _10_score.RenderTransform = new TranslateTransform(0, 118);

                DoubleAnimation Show_10 = new DoubleAnimation(500, 0, new Duration(TimeSpan.FromMilliseconds(250)))
                {
                    AutoReverse = false,
                    RepeatBehavior = new RepeatBehavior(1)
                };

                Storyboard.SetTarget(Show_10, Word_canvas_10_C);
                Storyboard.SetTargetProperty(Show_10, new PropertyPath("RenderTransform.Y"));

                DoubleAnimation show_score = new DoubleAnimation(400, 0, new Duration(TimeSpan.FromMilliseconds(250)))
                {
                    AutoReverse = false,
                    RepeatBehavior = new RepeatBehavior(1)
                };

                Storyboard.SetTarget(show_score, _10_score);
                Storyboard.SetTargetProperty(show_score, new PropertyPath("RenderTransform.Y"));

                Storyboard sb = new Storyboard();
                sb.Children.Add(Show_10);
                sb.Children.Add(show_score);
                sb.Begin();
                await Task.Delay(1450);

                while (numbers.Count < 3)
                {
                    int rnd = new Random().Next(0, 10);
                    while (!numbers.Contains(rnd))
                    {
                        SoundPlayer Bonus = new SoundPlayer { Stream = Properties.Resources.Bonus_letter };
                        Bonus.Play();
                        Word_canvas_10_C.Visibility = Visibility.Visible;

                        B_10_R[rnd].RenderTransform = new TranslateTransform(0, 118);
                        B_10_C[rnd].RenderTransform = new TranslateTransform(0, 118);

                        DoubleAnimation show_in = new DoubleAnimation(0, size, new Duration(TimeSpan.FromMilliseconds(600)))
                        {
                            AutoReverse = false,
                            RepeatBehavior = new RepeatBehavior(1)
                        };

                        Storyboard.SetTarget(show_in, B_10_R[rnd]);
                        Storyboard.SetTargetProperty(show_in, new PropertyPath("RenderTransform.Y"));

                        DoubleAnimation show_out = new DoubleAnimation(0, size, new Duration(TimeSpan.FromMilliseconds(600)))
                        {
                            AutoReverse = false,
                            RepeatBehavior = new RepeatBehavior(1)
                        };

                        Storyboard.SetTarget(show_out, B_10_C[rnd]);
                        Storyboard.SetTargetProperty(show_out, new PropertyPath("RenderTransform.Y"));

                        var sb0 = new Storyboard();
                        sb0.Children.Add(show_in);
                        sb0.Children.Add(show_out);
                        sb0.Begin();
                        numbers.Add(rnd);
                        await Task.Delay(500);
                    }
                }

                await Task.Delay(2000);

                while (numbers.Count < 8 && pause == false)
                {
                    int rnd = new Random().Next(0, 10);

                    while (!numbers.Contains(rnd))
                    {
                        score_10 = score_10 - 10;
                        L_score_10.Content = score_10.ToString();

                        SoundPlayer Bonus = new SoundPlayer { Stream = Properties.Resources.Bonus_letter };
                        Bonus.Play();

                        B_10_C[rnd].RenderTransform = new TranslateTransform(0, 118);
                        B_10_R[rnd].RenderTransform = new TranslateTransform(0, 118);

                        DoubleAnimation show_1 = new DoubleAnimation(0, size, new Duration(TimeSpan.FromMilliseconds(600)))
                        {
                            AutoReverse = false,
                            RepeatBehavior = new RepeatBehavior(1)
                        };

                        Storyboard.SetTarget(show_1, B_10_C[rnd]);
                        Storyboard.SetTargetProperty(show_1, new PropertyPath("RenderTransform.Y"));

                        DoubleAnimation show_2 = new DoubleAnimation(0, size, new Duration(TimeSpan.FromMilliseconds(600)))
                        {
                            AutoReverse = false,
                            RepeatBehavior = new RepeatBehavior(1)
                        };

                        Storyboard.SetTarget(show_2, B_10_R[rnd]);
                        Storyboard.SetTargetProperty(show_2, new PropertyPath("RenderTransform.Y"));

                        Storyboard sb2 = new Storyboard();
                        sb2.Children.Add(show_1);
                        sb2.Children.Add(show_2);
                        sb2.Begin();
                        numbers.Add(rnd);
                        await Task.Delay(2200);

                        if (numbers.Count == 8)
                        {
                            SoundPlayer New_line = new SoundPlayer { Stream = Properties.Resources.New_line };
                            New_line.Play();

                            score_10 = 0;
                            L_score_10.Content = score_10.ToString();

                            for (int i = 0; i < 10; i++)
                            {
                                await Task.Delay(100);
                                RadialGradientBrush Red = new RadialGradientBrush()
                                {

                                };
                                GradientStop stop_1 = new GradientStop(Colors.Maroon, 1.0);
                                GradientStop stop_2 = new GradientStop(Colors.Red, 0.251);
                                Red.GradientStops.Add(stop_1);
                                Red.GradientStops.Add(stop_2);

                                B_10_R[i].Background = Red;
                                L_10_R[i].Content = correct_word_10[i];
                            }
                        }
                    }
                }
            }

            else if (e.Key == Key.F8)
            {
                if (pause == false)
                {
                    R_score_A.Stroke = new SolidColorBrush(Colors.Goldenrod);
                    R_score_B.Stroke = new SolidColorBrush(Colors.Goldenrod);
                    pause = true;
                }

                else if (pause)
                {
                    R_score_A.Stroke = new SolidColorBrush(Colors.Silver);
                    R_score_B.Stroke = new SolidColorBrush(Colors.Silver);
                    pause = false;
                }
            }

            else if (e.Key == Key.F9 && pause == true && score_10 != 0)
            {
                SoundPlayer New_line = new SoundPlayer { Stream = Properties.Resources.New_line };
                New_line.Play();

                for (int i = 0; i < 10; i ++)
                {
                    await Task.Delay(100);
                    RadialGradientBrush Red = new RadialGradientBrush()
                    {

                    };
                    GradientStop stop_1 = new GradientStop(Colors.Maroon, 1.0);
                    GradientStop stop_2 = new GradientStop(Colors.Red, 0.251);
                    Red.GradientStops.Add(stop_1);
                    Red.GradientStops.Add(stop_2);

                    B_10_R[i].Background = Red;
                    L_10_R[i].Content = correct_word_10[i];
                }

                SoundPlayer Lingo = new SoundPlayer { Stream = Properties.Resources.Lingo };
                Lingo.Play();

                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(25);
                    int dur = 80;

                    RadialGradientBrush Red_block = new RadialGradientBrush
                    {

                    };
                    GradientStop stop1 = new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("OrangeRed"), 1.0);
                    GradientStop stop2 = new GradientStop(Colors.Red, 0.251);
                    Red_block.GradientStops.Add(stop1);
                    Red_block.GradientStops.Add(stop2);

                    ColorAnimation ca1 = new ColorAnimation(Colors.Maroon, Colors.White, new Duration(TimeSpan.FromMilliseconds(dur)))
                    {
                        RepeatBehavior = new RepeatBehavior(2.0),
                        AutoReverse = true
                    };

                    ColorAnimation ca2 = new ColorAnimation(Colors.Red, Colors.White, new Duration(TimeSpan.FromMilliseconds(dur)))
                    {
                        RepeatBehavior = new RepeatBehavior(2.0),
                        AutoReverse = true
                    };

                    Red_block.GradientStops[0].BeginAnimation(GradientStop.ColorProperty, ca1);
                    Red_block.GradientStops[1].BeginAnimation(GradientStop.ColorProperty, ca2);
                    B_10_C[9 - i].Background = Red_block;
                    B_10_R[9 - i].Background = Red_block;
                    B_10_C[i].Background = Red_block;
                    B_10_R[i].Background = Red_block;
                    _10_score_A.Content = L_score_10.Content;
                    _10_score_B.Content = L_score_10.Content;
                }
            }

        } // gele letters nog even checken + 8 remove
    }
}
