﻿using Northwoods.GoXam;
using Northwoods.GoXam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GoWPFDataTemplateIssue
{
    public class MoLinkData : GraphLinksModelLinkData<string, string>
    {
        #region Constructors

        public MoLinkData()
        {
            Id = Guid.NewGuid();
            LabelNode = Id.ToString();

            InitializeDashArrays();
            GenerateLinkVisual();
        }

        #endregion

        #region Properties

        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        private bool _isSelected = false;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (_isSelected != value) { bool old = _isSelected; _isSelected = value; RaisePropertyChanged("IsSelected", old, value); } }
        }

        #region Link visual

        private List<DoubleCollection> _dashArrays;

        public List<DoubleCollection> DashArrays
        {
            get { return _dashArrays; }
            set { if (_dashArrays != value) { List<DoubleCollection> old = _dashArrays; _dashArrays = value; RaisePropertyChanged("DashArrays", old, value); } }
        }

        private string _backColor = "Black";

        public string BackColor
        {
            get { return _backColor; }
            set { if (_backColor != value) { string old = _backColor; _backColor = value; RaisePropertyChanged("BackColor", old, value); } }
        }

        private string _foreColor = "Black";

        public string ForeColor
        {
            get { return _foreColor; }
            set { if (_foreColor != value) { string old = _foreColor; _foreColor = value; RaisePropertyChanged("ForeColor", old, value); } }
        }


        private Arrowhead _fromArrow = Arrowhead.None;

        public Arrowhead FromArrow
        {
            get { return _fromArrow; }
            set { if (_fromArrow != value) { Arrowhead old = _fromArrow; _fromArrow = value; RaisePropertyChanged("FromArrow", old, value); } }
        }

        private double _fromArrowScale = 1;

        public double FromArrowScale
        {
            get { return _fromArrowScale; }
            set { if (_fromArrowScale != value) { double old = _fromArrowScale; _fromArrowScale = value; RaisePropertyChanged("FromArrowScale", old, value); } }
        }

        private Arrowhead _toArrow = Arrowhead.Triangle;

        public Arrowhead ToArrow
        {
            get { return _toArrow; }
            set { if (_toArrow != value) { Arrowhead old = _toArrow; _toArrow = value; RaisePropertyChanged("ToArrow", old, value); } }
        }

        private double _toArrowScale = 1;

        public double ToArrowScale
        {
            get { return _toArrowScale; }
            set { if (_toArrowScale != value) { double old = _toArrowScale; _toArrowScale = value; RaisePropertyChanged("ToArrowScale", old, value); } }
        }

        private double _thickness = 1;

        public double Thickness
        {
            get { return _thickness; }
            set { if (_thickness != value) { double old = _thickness; _thickness = value; RaisePropertyChanged("Thickness", old, value); } }
        }

        private DoubleCollection _dashArray = null; // Solid line

        public DoubleCollection DashArray
        {
            get { return _dashArray; }
            set { if (_dashArray != value) { DoubleCollection old = _dashArray; _dashArray = value; RaisePropertyChanged("DashArray", old, value); } }
        }

        #endregion

        #endregion

        #region Methods

        private void InitializeDashArrays()
        {
            DashArrays = new List<DoubleCollection>();
            DashArrays.Add(null); // Solid line
            DashArrays.Add(new DoubleCollection() { 1, 1 });
            DashArrays.Add(new DoubleCollection() { 1, 3 });
            DashArrays.Add(new DoubleCollection() { 3, 1 });
            DashArrays.Add(new DoubleCollection() { .25, 1 });
            DashArrays.Add(new DoubleCollection() { 3, 1, 1, 1, 1, 1 });
            DashArrays.Add(new DoubleCollection() { 5, 5, 1, 5 });
            DashArrays.Add(new DoubleCollection() { 1, 2, 4 });
            DashArrays.Add(new DoubleCollection() { 4, 2, 4, 1 });
        }

        private string RandomBrushString()
        {
            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = properties[random].GetValue(null, null) as Brush;

            return result is not null ? result.ToString() : Brushes.DeepPink.ToString();
        }

        public void GenerateLinkVisual()
        {
            BackColor = RandomBrushString();
            ForeColor = Brushes.Black.ToString();

            FromArrow = (Arrowhead)new Random().Next(Enum.GetNames(typeof(Arrowhead)).Length);
            double minFromArrowSacle = 1;
            double maxFromArrowSacle = 2;
            FromArrowScale = (new Random().NextDouble() * (maxFromArrowSacle - minFromArrowSacle) + minFromArrowSacle);

            ToArrow = (Arrowhead)new Random().Next(Enum.GetNames(typeof(Arrowhead)).Length);
            double minToArrowSacle = 1;
            double maxToArrowSacle = 2;
            ToArrowScale = (new Random().NextDouble() * (maxToArrowSacle - minToArrowSacle) + minToArrowSacle);

            double minThickness = 1;
            double maxThickness = 3;
            Thickness = (new Random().NextDouble() * (maxThickness - minThickness) + minThickness);

            if (DashArrays is not null)
            {
                DashArray = DashArrays[new Random().Next(DashArrays.Count)];
            }
        }

        #endregion
    }
}
