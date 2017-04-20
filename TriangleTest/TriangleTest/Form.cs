using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TriangleTest.Entities;

namespace TriangleTest
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly Triangle _triangle;

        #region 物体本身变化
        private readonly Matrix4X4 _scale;
        private readonly Matrix4X4 _rota;
        private readonly Matrix4X4 _trans;
        private readonly Matrix4X4 _proj;
        private int _angleX;
        #endregion

        #region 相机变化

        private readonly Matrix4X4 _cRota;
        private readonly Matrix4X4 _cTrans;
        private readonly Matrix4X4 _cProj;
        private int _cAngleX;

        #endregion

        private bool cullBack;

        public Form()
        {
            InitializeComponent();
            _triangle = new Triangle(
                new Vector4(0, 0.5f, 0),
                new Vector4(0.5f, -0.5f, 0),
                new Vector4(-0.5f, -0.5f, 0));
            _scale = new Matrix4X4(true);
            _scale[1][1] = 200;
            _scale[2][2] = 200;
            _scale[3][3] = 200;
            _rota = new Matrix4X4(true);

            _trans = new Matrix4X4(true);
            _trans[4][3] = 200;

            _proj = new Matrix4X4(true);
            _proj[3][4] = 1/200f;
            _proj[4][4] = 0;

            _cRota = new Matrix4X4(true);
            _cTrans = new Matrix4X4(true);
            //_cTrans[4][3] = 500;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            DrawTriangle(e.Graphics);
        }

        private void DrawTriangle(Graphics g)
        {
            g.TranslateTransform(300, 300);
            var points = new[]
            {
                _triangle[0].PointF,
                _triangle[1].PointF,
                _triangle[2].PointF,
                _triangle[0].PointF
            };
            g.DrawLines(new Pen(Color.Red, 2), points);

            if (!cullBack)
            {
                var paths = new GraphicsPath();
                paths.AddLines(points);
                g.FillPath(new SolidBrush(Color.White), paths);
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _angleX += 2;
            _angleX = _angleX % 360;
            var anglePi = _angleX / 180f * Math.PI;
            _rota[1][1] = (float)Math.Cos(anglePi);
            _rota[1][3] = -(float)Math.Sin(anglePi);
            _rota[3][1] = (float)Math.Sin(anglePi);
            _rota[3][3] = (float)Math.Cos(anglePi);


            //_cRota[2][2] = (float)Math.Cos(anglePi);
            //_cRota[2][3] = -(float)Math.Sin(anglePi);
            //_cRota[3][2] = (float)Math.Sin(anglePi);
            //_cRota[3][3] = (float)Math.Cos(anglePi);

            var m = _scale.Mul(_rota).Mul(_trans);
            var tempNormal = _triangle.GetNormal();
            cullBack = tempNormal.Dot(new Vector4(0, 0, -1, 0)) < 0;
            _triangle.Mul(m.Mul(_cRota).Mul(_cTrans).Mul(_proj));



            Invalidate();
        }
    }
}
