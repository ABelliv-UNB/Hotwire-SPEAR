using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hotwire_Transient_GUI.Code
{
    public class SGFilter
    {

        public double[,] Deg2Or3Coeff = new double[,]
        {
            { 0, 0, -21},
            { 0, -2, 14},
            { -3, 3, 39},
            { 12, 6, 54},
            { 17, 7, 59},
            { 12, 6, 54},
            { -3, 3, 39},
            {  0, 2, 14},
            {  0, 0, -21}
        };

        public double[,] Deg4Or5Coeff = new double[,]
            { 
                {   0,  15},
                {   5, -55},
                { -30,  30},
                {  75, 135},
                { 131, 179},
                {  75, 135},
                { -30,  30},
                {   5, -55},
                {   0,  15}
            };

        public static ObservableCollection<Point> filter(List<Point> Points, int Degree, int WindowSize)
        {
            ObservableCollection<Point> filteredPoints = new ObservableCollection<Point>();
            double newY;
            double denominator;

            for(int i = 0; i < Points.Count; i++)
            {
                newY = 0;
                denominator = 17;
                if(i - 2 > 0)
                {
                    newY += -3 * Points[i - 2].wireTemp;
                    denominator -= 3;
                }
                if (i - 1 > 0)
                {
                    newY += 12 * Points[i - 1].wireTemp;
                    denominator += 12;
                }

                newY += 17.0 * Points[i].wireTemp;

                if (i + 1 < Points.Count)
                {
                    newY += 12 * Points[i + 1].wireTemp;
                    denominator += 12;
                }
                if (i + 2 < Points.Count)
                {
                    newY += -3 * Points[i + 2].wireTemp;
                    denominator -= 3;
                }

                newY = newY / denominator;
                filteredPoints.Add(new Point(Points[i].time, newY, i));

            }

            return filteredPoints;

        }

        //private List<Point> filterLowDeg(List<Point> Points, int Degree, int WindowSize)
        //{
        //    List<Point> filteredPoints = new List<Point>();
        //    int i;

        //    for(i = 0; i < Points.Count; i++)
        //    {
        //        if(WindowSize == 5)
        //        {

        //        }





        //    }
        //}

    }
}
