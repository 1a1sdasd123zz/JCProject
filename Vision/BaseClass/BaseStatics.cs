using DevExpress.XtraGrid.Views.Tile;
//using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace YC_Vision
{
    public class BaseStatics
    {
        static int _count = 12;
        //public Chart _chart = new Chart();
        public  Series _seriesOk = new Series();
        public Series _seriesNg = new Series();
        private DataPoint[] _pointsOk = new DataPoint[_count];
        private DataPoint[] _pointsNg = new DataPoint[_count];
        Time time;

        public BaseStatics(int time) 
        {
            _seriesOk.XValueType = ChartValueType.String;
            _seriesOk.IsXValueIndexed = true;
            _seriesNg.XValueType = ChartValueType.String;
            _seriesNg.IsXValueIndexed = true;

            int t1 = time + 1;
            int t2 = time + 12 + 1;
            for (int i = 0; i < _pointsOk.Length; i++)
            {
                string val = t1.ToString() + ":00";
                _pointsOk[i] = new DataPoint();
                _pointsOk[i].SetValueXY(val, 100);
                _pointsOk[i].Color = Color.Green;
                _seriesOk.Points.Add(_pointsOk[i]);

                _pointsNg[i] = new DataPoint();
                _pointsNg[i].SetValueXY(val, 100);
                _pointsNg[i].Color = Color.Red;
                _seriesNg.Points.Add(_pointsNg[i]);
                t1++;
                t2++;
            }
            //_chart.Series.Add(_seriesOk);
            //_chart.Series.Add(_seriesNg);

        }
        private void Init()
        {
            
        }
    }

    public class myChart
    {
        public Chart _chart = new Chart();
        public mySeries _seriesOk;
        public mySeries _seriesNg;
        public myChart()
        {
            Title title = new Title();
            _chart.Titles.Add(title);
            myDataPoint[] datapointOks = new myDataPoint[12];
            myDataPoint[] datapointNgs = new myDataPoint[12];
            int t1 = 8;//白班时间
            int t2 = 21;//夜班时间
            for (int i =0; i< 12;i++)
            {
                string text = (t1++).ToString() + ":00";
                datapointOks[i] = new myDataPoint(text,0,Color.Green);
                if(t2++ == 24)
                {
                    t2 = 0;
                }
                string text2 = (t2).ToString() + ":00";
                datapointNgs[i] = new myDataPoint(text2, 0, Color.Red); 
            }
            _seriesOk = new mySeries(datapointOks, ChartValueType.String);
            _seriesNg = new mySeries(datapointOks, ChartValueType.String);
            _chart.Series.Add(_seriesOk._series);
            _chart.Series.Add(_seriesNg._series);
            
        }
    }

    public class mySeries
    {
        public Series _series = new Series();
        public mySeries(myDataPoint point, ChartValueType type = ChartValueType.Auto)
        {
            _series.XValueType = type;
            _series.IsXValueIndexed = true;
            _series.Points.Add(point._point);
        }
        public mySeries(myDataPoint[] point, ChartValueType type = ChartValueType.Auto)
        {
            _series.XValueType = type;
            _series.IsXValueIndexed = true;
            foreach (var item in point)
            {
                _series.Points.Add(item._point);
            }
        }
    }
    public class myDataPoint
    {
        public DataPoint _point = new DataPoint();
        public myDataPoint(object text,object val,Color color)
        {
            _point.SetValueXY(text, val);
            _point.Color = color;
            _point.IsVisibleInLegend = true;
        }
    }
}
