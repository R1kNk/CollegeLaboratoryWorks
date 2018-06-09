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

namespace BezieLine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isStartClicked;
        private bool isEndClick;
        private bool isFirstClicked;
        private bool isSecondClick;
        private Point endPoint;
        private Point startPoint;
        private Point firstPoint;
        private Point secondPoint;
        public MainWindow()
        {
            InitializeComponent();
            //
            startPoint = new Point(StartPointButton.Margin.Left, StartPointButton.Margin.Top);
            endPoint =  new Point(EndPointButton.Margin.Left, EndPointButton.Margin.Top);

            EndPointButton.Margin = new Thickness(endPoint.X, endPoint.Y, 0, 0);
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = startPoint,
                IsClosed = false
            };
            Vector vector = endPoint - pathFigure.StartPoint;
            firstPoint = new Point(pathFigure.StartPoint.X + vector.X / 2, pathFigure.StartPoint.Y);
            secondPoint = new Point(pathFigure.StartPoint.X + vector.X / 1.5, pathFigure.StartPoint.Y + vector.Y / 0.95);
            //

            //
            //Кривая Безье
            
            BezierSegment curve = new BezierSegment(firstPoint, secondPoint, endPoint, true);

            PathGeometry path = new PathGeometry();
            path.Figures.Add(pathFigure);
            pathFigure.Segments.Add(curve);
            pathMain.Data = path;

            //

        }



        private Point Center()
        {
            return new Point(window.Width / 2, window.Height / 2);
        } 
        private void DrawLine(object sender, MouseEventArgs e)
        {

            if (isEndClick)
            {
                 endPoint = e.GetPosition(this);
                EndPointButton.Margin = new Thickness(endPoint.X, endPoint.Y, 0, 0);
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = startPoint,
                    IsClosed = false
                };
                //

                //
                //Кривая Безье
                Vector vector = endPoint - pathFigure.StartPoint;
                Point point1 = new Point(pathFigure.StartPoint.X + vector.X / 2, pathFigure.StartPoint.Y);
                Point point2 = new Point(pathFigure.StartPoint.X + vector.X / 1.5, pathFigure.StartPoint.Y + vector.Y / 0.95);
                BezierSegment curve = new BezierSegment(point1, point2, endPoint, true);

                //hokus pokus notice kek
                PathGeometry path = new PathGeometry();
                path.Figures.Add(pathFigure);
                pathFigure.Segments.Add(curve);
                pathMain.Data = path;
            }
            else if (isStartClicked)
            {
                 startPoint = e.GetPosition(this);
                StartPointButton.Margin = new Thickness(startPoint.X, startPoint.Y, 0, 0);
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = endPoint,
                    IsClosed = false
                };
                //

                //
                //Кривая Безье
                Vector vector = startPoint - pathFigure.StartPoint;
                Point point1 = new Point(pathFigure.StartPoint.X + vector.X / 2, pathFigure.StartPoint.Y);
                Point point2 = new Point(pathFigure.StartPoint.X + vector.X / 1.5, pathFigure.StartPoint.Y + vector.Y / 0.95);
                BezierSegment curve = new BezierSegment(point1, point2, startPoint, true);

                PathGeometry path = new PathGeometry();
                path.Figures.Add(pathFigure);
                pathFigure.Segments.Add(curve);
                pathMain.Data = path;
            }
            else if (isFirstClicked)
            {
                firstPoint = e.GetPosition(this);
                firstPointButton.Margin = new Thickness(firstPoint.X, firstPoint.Y, 0, 0);
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = startPoint,
                    IsClosed = false
                };
                //

                //
                //Кривая Безье
                Vector vector = endPoint - pathFigure.StartPoint;
               
                BezierSegment curve = new BezierSegment(firstPoint, secondPoint, endPoint, true);

                //hokus pokus notice kek
                PathGeometry path = new PathGeometry();
                path.Figures.Add(pathFigure);
                pathFigure.Segments.Add(curve);
                pathMain.Data = path;
            }
            else if (isSecondClick)
            {

                secondPoint = e.GetPosition(this);
                secondPointButton.Margin = new Thickness(secondPoint.X, secondPoint.Y, 0, 0);
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = startPoint,
                    IsClosed = false
                };
                //

                //
                //Кривая Безье
                Vector vector = endPoint - pathFigure.StartPoint;

                BezierSegment curve = new BezierSegment(firstPoint, secondPoint, endPoint, true);

                //hokus pokus notice kek
                PathGeometry path = new PathGeometry();
                path.Figures.Add(pathFigure);
                pathFigure.Segments.Add(curve);
                pathMain.Data = path;
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            MessageBox.Show("Координата x=" + p.X.ToString() + " y=" + p.Y.ToString());
        }

        private void StartPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isEndClick&&!isFirstClicked&&!isSecondClick) isStartClicked = !isStartClicked;
        }

        private void EndPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isStartClicked&&!isFirstClicked &&!isSecondClick) isEndClick = !isEndClick;
        }

        private void firstPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isStartClicked && !isEndClick && !isSecondClick) isFirstClicked = !isFirstClicked;

        }

        private void secondPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isStartClicked && !isEndClick && !isFirstClicked) isSecondClick = !isSecondClick;

        }
    }
}
