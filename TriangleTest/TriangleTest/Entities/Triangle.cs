using System.Drawing;

namespace TriangleTest.Entities
{
    class Triangle
    {
        private readonly Vector4[] _originData;
        private readonly Vector4[] _data;

        public Vector4 this[int index] => _data[index];

        public Triangle(Vector4 a, Vector4 b, Vector4 c)
        {
            _originData = new[] {a, b, c};
            _data = new[] {a, b, c};
            var test = GetNormal();
        }

        public void Mul(Matrix4X4 matrix4X4)
        {
            for (var i = 0; i < 3; i++)
            {
                _data[i] = _originData[i].Mul(matrix4X4);
            }
        }

        public Vector4 GetNormal()
        {
            var u = _originData[1] - _originData[0];
            var v = _originData[2] - _originData[0];
            return u*v;
        }
    }
}
