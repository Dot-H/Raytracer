using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Raytracer.utils
{
    class PerlinNoise
    {
        private static int[] p = new int[512];
        private static int[] permutation = { 151,160,137,91,90,15,
               131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
               190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
               88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
               77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
               102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
               135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
               5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
               223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
               129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
               251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
               49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
               138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
               };

        static PerlinNoise()
        {
            CalculateP();
        }

        private static int _octaves;
        private static int _halfLength = 256;
        
        public static void SetOctaves(int octaves)
        {
            _octaves = octaves;

            var len = (int)Math.Pow(2, octaves);

            permutation = new int[len];

            Reseed();
        }

        private static void CalculateP()
        {
            p = new int[permutation.Length * 2];
            _halfLength = permutation.Length;

            for (int i = 0; i < permutation.Length; i++)
                p[permutation.Length + i] = p[i] = permutation[i];
        }

        public static void Reseed()
        {
            var random = new Random();
            var perm = Enumerable.Range(0, permutation.Length).ToArray();

            for (var i = 0; i < perm.Length; i++)
            {
                var swapIndex = random.Next(perm.Length);

                var t = perm[i];

                perm[i] = perm[swapIndex];

                perm[swapIndex] = t;
            }

            permutation = perm;

            CalculateP();

        }

        public static double Noise(Vector3 position, int octaves, ref double min, ref double max)
        {
            return Noise(position.X, position.Y, position.Z, octaves, ref min, ref max);
        }

        public static double Noise(double x, double y, double z, int octaves, ref double min, ref double max)
        {

            var perlin = 0d;
            var octave = 1;

            for (var i = 0; i < octaves; i++)
            {
                var noise = Noise(x * octave, y * octave, z * octave);

                perlin += noise / octave;

                octave *= 2;
            }

            perlin = Math.Abs((double)Math.Pow(perlin, 2));
            max = Math.Max(perlin, max);
            min = Math.Min(perlin, min);

            //perlin = 1f - 2 * perlin;

            return perlin;
        }

        private static double linear_interpolate(double a, double b, double t)
        {
            return (1d - t) * a + t * b;
        }

        public static double Noise(double x, double y, double z)
        {
            int X = (int)Math.Floor(x) % _halfLength;
            int Y = (int)Math.Floor(y) % _halfLength;
            int Z = (int)Math.Floor(z) % _halfLength;

            if (X < 0)
                X += _halfLength;

            if (Y < 0)
                Y += _halfLength;

            if (Z < 0)
                Z += _halfLength;

            x -= (int)Math.Floor(x);
            y -= (int)Math.Floor(y);
            z -= (int)Math.Floor(z);

            var u = Fade(x);
            var v = Fade(y);
            var w = Fade(z);

            int A = p[X] + Y, AA = p[A] + Z, AB = p[A + 1] + Z,      // HASH COORDINATES OF
                B = p[X + 1] + Y, BA = p[B] + Z, BB = p[B + 1] + Z;      // THE 8 CUBE CORNERS,


            return linear_interpolate(
                    linear_interpolate(
                         linear_interpolate(
                            Grad(p[AA], x, y, z) // AND ADD
                            ,
                            Grad(p[BA], x - 1, y, z) // BLENDED
                            ,
                            u
                            )
                        ,
                        linear_interpolate(
                            Grad(p[AB], x, y - 1, z)  // RESULTS
                            ,
                            Grad(p[BB], x - 1, y - 1, z)
                            ,
                            u
                            )
                        ,
                        v
                    )
                    ,
                    linear_interpolate(
                        linear_interpolate(
                            Grad(p[AA + 1], x, y, z - 1) // CORNERS
                            ,
                            Grad(p[BA + 1], x - 1, y, z - 1) // OF CUBE
                            ,
                            u
                            )
                        ,
                        linear_interpolate(
                            Grad(p[AB + 1], x, y - 1, z - 1)
                            ,
                            Grad(p[BB + 1], x - 1, y - 1, z - 1)
                            ,
                            u
                            )
                        ,
                        v
                    )
                    ,
                    w
                );

        }

        static double Fade(double t) { return t * t * t * (t * (t * 6 - 15) + 10); }

        static double Grad(int hash, double x, double y, double z)
        {
            int h = hash & 15;                      // CONVERT LO 4 BITS OF HASH CODE

            double u = h < 8 ? x : y,                 // INTO 12 GRADIENT DIRECTIONS.
                   v = h < 4 ? y : h == 12 || h == 14 ? x : z;

            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        public NormalizedColor newWorld(double n, double v1, double v2, double v3, Color color1, Color color2, Color color3)
        {
            NormalizedColor c1 = new NormalizedColor(color1);
            NormalizedColor c2 = new NormalizedColor(color2);
            NormalizedColor c3 = new NormalizedColor(color3);
            if (n < v1)
                return c1;
            if (n >= v1 && n < v2)
                return (c1 * ((n - v1) / (v2 - v1))) + (c2 * ((v2 - n) / (v2 - v1)));
            if (n >= v2 && n < v3)
                return (c2 * ((n - v2) / (v3 - v2))) + (c3 * ((v3 - n) / (v3 - v2)));
            else
                return c3;
        }

        public NormalizedColor marbre(double n, Vector3 point, Color color1, Color color2)
        {
            n = 1 - Math.Sqrt(Math.Abs(Math.Sin((2d * 3.141592d * n))));
            NormalizedColor c1 = new NormalizedColor(color1);
            NormalizedColor c2 = new NormalizedColor(color2);
            return c1 * (1d - n) + c2 * n;
        }

        public NormalizedColor bois(double n, double v1, Color color1, Color color2)
        {
            NormalizedColor c1 = new NormalizedColor(color1);
            NormalizedColor c2 = new NormalizedColor(color2);

            n = n % v1;
            if (n > v1 / 2d)
                n = v1 - n;

            //double f = (1d - Math.Cos(Math.PI * n / (v1 / 2d))) / 2d;
            double f = (1d - Math.Sin(Math.PI * n / (v1 / 2d)));
            return new NormalizedColor(c1.R * (1d - f) + c2.R * f,
                                       c1.G * (1d - f) + c2.G * f,
                                       c1.B * (1d - f) + c2.B * f);


        }

    }

}
