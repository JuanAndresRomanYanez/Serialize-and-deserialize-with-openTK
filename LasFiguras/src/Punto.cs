using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasFiguras
{
    class Punto
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Punto()
        {
            this.x = 0f;
            this.y = 0f;
            this.z = 0f;
        }

        public Punto(Punto p1)
        {
            this.x = p1.x;
            this.y = p1.y;
            this.z = p1.z;
        }
        public Punto(float x1, float y1, float z1)
        {
            this.x = x1;
            this.y = y1;
            this.z = z1;
        }

        public void MoverPunto(Punto p)
        {
            this.x = this.x + p.x;
            this.y = this.y + p.y;
            this.z = this.z + p.z;
        }
    }
}
