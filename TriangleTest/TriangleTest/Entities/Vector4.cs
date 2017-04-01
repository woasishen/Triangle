using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleTest.Entities
{
    class Vector4
    {
        private readonly float[] _data;
        public float X
        {
            get { return _data[0]; }
            set { _data[0] = value; }
        }

        public float Y
        {
            get { return _data[1]; }
            set { _data[1] = value; }
        }

        public float Z
        {
            get { return _data[2]; }
            set { _data[2] = value; }
        }

        public float W
        {
            get { return _data[3]; }
            set { _data[3] = value; }
        }

        public float this[int index]
        {
            get { return _data[index - 1]; }
            set { _data[index - 1] = value; }
        }

        public Vector4()
        {
            _data = new float[4];
        }
        public Vector4(float x, float y, float z)
        {
            _data = new[] { x, y, z, 1 };
        }

        public PointF PointF => new PointF(this[1] / this[4], -this[2] / this[4]);

        public Vector4 Mul(Matrix4X4 matrix4X4)
        {
            var result = new Vector4();
            for (var i = 1; i < 5; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    result[i] += this[j] * matrix4X4[j][i];
                }
            }
            return result;
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            var reslut = new Vector4();
            for (int i = 1; i < 5; i++)
            {
                reslut[i] = a[i] - b[i];
            }
            return reslut;
        }

        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            var result = new Vector4();
            result.X = a.Y * b.Z - a.Z * b.Y;
            result.Y = a.Z * b.X - a.X * b.Z;
            result.Z = a.X * b.Y - a.Y * b.X;

            return result;
        }

        public override string ToString()
        {
            return $"{{{X}\t,{Y}\t,{Z}\t,{W}}}";
        }
    }
}
