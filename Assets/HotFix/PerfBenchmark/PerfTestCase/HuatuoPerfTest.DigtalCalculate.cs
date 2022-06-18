﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Huatuo.Perf.Huatuo
{
    // 以下代码来自 https://github.com/nxrighthere/BurstBenchmarks
    // Fibonacci
    [PerfClass("Fibonacci", "Huatuo", "数值计算")]
    public struct FibonacciNET : IBenchmark
    {
        public uint number;
        public uint result;

        public void Clear()
        {
        }

        public void Prepare()
        {
            number = PerfLevel.fibonacciNum;
        }

        public void Run()
        {
            result = Fibonacci(number);
        }

        private uint Fibonacci(uint number)
        {
            if (number <= 1)
                return 1;

            return Fibonacci(number - 1) + Fibonacci(number - 2);
        }
    }

    // Mandelbrot
    [PerfClass("Mandelbrot", "Huatuo", "数值计算")]
    public struct MandelbrotNET : IBenchmark
    {
        public uint width;
        public uint height;
        public uint iterations;
        public float result;

        public void Clear()
        {
        }

        public void Prepare()
        {
            width = PerfLevel.mandelbrotW;
            height = PerfLevel.mandelbrotH;
            iterations = PerfLevel.mandelbrotIterations;
        }

        public void Run()
        {
            result = Mandelbrot(width, height, iterations);
        }

        private float Mandelbrot(uint width, uint height, uint iterations)
        {
            float data = 0.0f;

            for (uint i = 0; i < iterations; i++)
            {
                float
                    left = -2.1f,
                    right = 1.0f,
                    top = -1.3f,
                    bottom = 1.3f,
                    deltaX = (right - left) / width,
                    deltaY = (bottom - top) / height,
                    coordinateX = left;

                for (uint x = 0; x < width; x++)
                {
                    float coordinateY = top;

                    for (uint y = 0; y < height; y++)
                    {
                        float workX = 0;
                        float workY = 0;
                        int counter = 0;

                        while (counter < 255 && Math.Sqrt((workX * workX) + (workY * workY)) < 2.0f)
                        {
                            counter++;

                            float newX = (workX * workX) - (workY * workY) + coordinateX;

                            workY = 2 * workX * workY + coordinateY;
                            workX = newX;
                        }

                        data = workX + workY;
                        coordinateY += deltaY;
                    }

                    coordinateX += deltaX;
                }
            }

            return data;
        }
    }

    // NBody
    struct NBody
    {
        public double x, y, z, vx, vy, vz, mass;
    }

    [PerfClass("NBody", "Huatuo", "数值计算")]
    public unsafe struct NBodyNET : IBenchmark
    {
        public uint advancements;
        public double result;

        public void Prepare()
        {
            advancements = PerfLevel.nbodyAdvancements;
        }

        public void Clear()
        {
        }

        public void Run()
        {
            result = NBody(advancements);
        }

        private double NBody(uint advancements)
        {
            NBody* sun = stackalloc NBody[5];
            NBody* end = sun + 4;

            InitializeBodies(sun, end);
            Energy(sun, end);

            while (--advancements > 0)
            {
                Advance(sun, end, 0.01);
            }

            Energy(sun, end);

            return sun[0].x + sun[0].y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeBodies(NBody* sun, NBody* end)
        {
            const double pi = 3.141592653589793;
            const double solarMass = 4 * pi * pi;
            const double daysPerYear = 365.24;

            unchecked
            {
                sun[1] = new NBody
                { // Jupiter
                    x = 4.84143144246472090e+00,
                    y = -1.16032004402742839e+00,
                    z = -1.03622044471123109e-01,
                    vx = 1.66007664274403694e-03 * daysPerYear,
                    vy = 7.69901118419740425e-03 * daysPerYear,
                    vz = -6.90460016972063023e-05 * daysPerYear,
                    mass = 9.54791938424326609e-04 * solarMass
                };

                sun[2] = new NBody
                { // Saturn
                    x = 8.34336671824457987e+00,
                    y = 4.12479856412430479e+00,
                    z = -4.03523417114321381e-01,
                    vx = -2.76742510726862411e-03 * daysPerYear,
                    vy = 4.99852801234917238e-03 * daysPerYear,
                    vz = 2.30417297573763929e-05 * daysPerYear,
                    mass = 2.85885980666130812e-04 * solarMass
                };

                sun[3] = new NBody
                { // Uranus
                    x = 1.28943695621391310e+01,
                    y = -1.51111514016986312e+01,
                    z = -2.23307578892655734e-01,
                    vx = 2.96460137564761618e-03 * daysPerYear,
                    vy = 2.37847173959480950e-03 * daysPerYear,
                    vz = -2.96589568540237556e-05 * daysPerYear,
                    mass = 4.36624404335156298e-05 * solarMass
                };

                sun[4] = new NBody
                { // Neptune
                    x = 1.53796971148509165e+01,
                    y = -2.59193146099879641e+01,
                    z = 1.79258772950371181e-01,
                    vx = 2.68067772490389322e-03 * daysPerYear,
                    vy = 1.62824170038242295e-03 * daysPerYear,
                    vz = -9.51592254519715870e-05 * daysPerYear,
                    mass = 5.15138902046611451e-05 * solarMass
                };

                double vx = 0, vy = 0, vz = 0;

                for (NBody* planet = sun + 1; planet <= end; ++planet)
                {
                    double mass = planet->mass;

                    vx += planet->vx * mass;
                    vy += planet->vy * mass;
                    vz += planet->vz * mass;
                }

                sun->mass = solarMass;
                sun->vx = vx / -solarMass;
                sun->vy = vy / -solarMass;
                sun->vz = vz / -solarMass;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Energy(NBody* sun, NBody* end)
        {
            unchecked
            {
                double e = 0.0;

                for (NBody* bi = sun; bi <= end; ++bi)
                {
                    double
                        imass = bi->mass,
                        ix = bi->x,
                        iy = bi->y,
                        iz = bi->z,
                        ivx = bi->vx,
                        ivy = bi->vy,
                        ivz = bi->vz;

                    e += 0.5 * imass * (ivx * ivx + ivy * ivy + ivz * ivz);

                    for (NBody* bj = bi + 1; bj <= end; ++bj)
                    {
                        double
                            jmass = bj->mass,
                            dx = ix - bj->x,
                            dy = iy - bj->y,
                            dz = iz - bj->z;

                        e -= imass * jmass / Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetD2(double dx, double dy, double dz)
        {
            double d2 = dx * dx + dy * dy + dz * dz;

            return d2 * Math.Sqrt(d2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Advance(NBody* sun, NBody* end, double distance)
        {
            unchecked
            {
                for (NBody* bi = sun; bi < end; ++bi)
                {
                    double
                        ix = bi->x,
                        iy = bi->y,
                        iz = bi->z,
                        ivx = bi->vx,
                        ivy = bi->vy,
                        ivz = bi->vz,
                        imass = bi->mass;

                    for (NBody* bj = bi + 1; bj <= end; ++bj)
                    {
                        double
                            dx = bj->x - ix,
                            dy = bj->y - iy,
                            dz = bj->z - iz,
                            jmass = bj->mass,
                            mag = distance / GetD2(dx, dy, dz);

                        bj->vx = bj->vx - dx * imass * mag;
                        bj->vy = bj->vy - dy * imass * mag;
                        bj->vz = bj->vz - dz * imass * mag;
                        ivx = ivx + dx * jmass * mag;
                        ivy = ivy + dy * jmass * mag;
                        ivz = ivz + dz * jmass * mag;
                    }

                    bi->vx = ivx;
                    bi->vy = ivy;
                    bi->vz = ivz;
                    bi->x = ix + ivx * distance;
                    bi->y = iy + ivy * distance;
                    bi->z = iz + ivz * distance;
                }

                end->x = end->x + end->vx * distance;
                end->y = end->y + end->vy * distance;
                end->z = end->z + end->vz * distance;
            }
        }
    }

    // Sieve of Eratosthenes
    [PerfClass("SieveOfEratosthenes", "Huatuo", "数值计算")]
    public unsafe struct SieveOfEratosthenesNET : IBenchmark
    {
        public uint iterations;
        public uint result;

        public void Clear()
        {
        }

        public void Prepare()
        {
            iterations = PerfLevel.sieveOfEratosthenesIterations;
        }

        public void Run()
        {
            result = SieveOfEratosthenes(iterations);
        }

        private uint SieveOfEratosthenes(uint iterations)
        {
            const int size = 1024;

            byte* flags = stackalloc byte[size];
            uint a, b, c, prime, count = 0;

            for (a = 1; a <= iterations; a++)
            {
                count = 0;

                for (b = 0; b < size; b++)
                {
                    flags[b] = 1; // True
                }

                for (b = 0; b < size; b++)
                {
                    if (flags[b] == 1)
                    {
                        prime = b + b + 3;
                        c = b + prime;

                        while (c < size)
                        {
                            flags[c] = 0; // False
                            c += prime;
                        }

                        count++;
                    }
                }
            }

            return count;
        }
    }

    // Pixar Raytracer

    struct Vector
    {
        public float x, y, z;
    }

    enum PixarRayHit
    {
        None = 0,
        Letter = 1,
        Wall = 2,
        Sun = 3
    }

    [PerfClass("PixarRaytracer", "Huatuo", "数值计算")]
    public unsafe struct PixarRaytracerNET : IBenchmark
    {
        public uint width;
        public uint height;
        public uint samples;
        public float result;

        public void Prepare()
        {
            width = PerfLevel.pixarRaytracerW;
            height = PerfLevel.pixarRaytracerH;
            samples = PerfLevel.pixarRaytracerSamples;
        }

        public void Clear()
        {
        }

        public void Run()
        {
            result = PixarRaytracer(width, height, samples);
        }

        private uint marsagliaZ, marsagliaW;

        private float PixarRaytracer(uint width, uint height, uint samples)
        {
            marsagliaZ = 666;
            marsagliaW = 999;

            Vector position = new Vector { x = -22.0f, y = 5.0f, z = 25.0f };
            Vector goal = new Vector { x = -3.0f, y = 4.0f, z = 0.0f };

            goal = Add(Inverse(goal), MultiplyFloat(position, -1.0f));

            Vector left = new Vector { x = goal.z, y = 0, z = goal.x };

            left = MultiplyFloat(Inverse(left), 1.0f / width);

            Vector up = Cross(goal, left);
            Vector color = default(Vector);
            Vector adjust = default(Vector);

            for (uint y = height; y > 0; y--)
            {
                for (uint x = width; x > 0; x--)
                {
                    for (uint p = samples; p > 0; p--)
                    {
                        color = Add(color, Trace(position, Add(Inverse(MultiplyFloat(Add(goal, left), x - width / 2 + Random())), MultiplyFloat(up, y - height / 2 + Random()))));
                    }

                    color = MultiplyFloat(color, (1.0f / samples) + 14.0f / 241.0f);
                    adjust = AddFloat(color, 1.0f);
                    color = new Vector
                    {
                        x = color.x / adjust.x,
                        y = color.y / adjust.y,
                        z = color.z / adjust.z
                    };

                    color = MultiplyFloat(color, 255.0f);
                }
            }

            return color.x + color.y + color.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector Multiply(Vector left, Vector right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;

            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector MultiplyFloat(Vector vector, float value)
        {
            vector.x *= value;
            vector.y *= value;
            vector.z *= value;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float Modulus(Vector left, Vector right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float ModulusSelf(Vector vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector Inverse(Vector vector)
        {
            return MultiplyFloat(vector, 1 / (float)Math.Sqrt(ModulusSelf(vector)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector Add(Vector left, Vector right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;

            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector AddFloat(Vector vector, float value)
        {
            vector.x += value;
            vector.y += value;
            vector.z += value;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector Cross(Vector to, Vector from)
        {
            Vector vector = default(Vector);

            vector.x = to.y * from.z - to.z * from.y;
            vector.y = to.z * from.x - to.x * from.z;
            vector.z = to.x * from.y - to.y * from.x;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float Min(float left, float right)
        {
            return left < right ? left : right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float BoxTest(Vector position, Vector lowerLeft, Vector upperRight)
        {
            lowerLeft = MultiplyFloat(Add(position, lowerLeft), -1);
            upperRight = MultiplyFloat(Add(upperRight, position), -1);

            return -Min(Min(Min(lowerLeft.x, upperRight.x), Min(lowerLeft.y, upperRight.y)), Min(lowerLeft.z, upperRight.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float Random()
        {
            marsagliaZ = 36969 * (marsagliaZ & 65535) + (marsagliaZ >> 16);
            marsagliaW = 18000 * (marsagliaW & 65535) + (marsagliaW >> 16);

            return ((marsagliaZ << 16) + marsagliaW) * 2.0f / 10000000000.0f;
        }

        private float Sample(Vector position, int* hitType)
        {
            const int size = 60;

            float distance = 1e9f;
            Vector f = position;
            byte* letters = stackalloc byte[size];

            // P              // I              // X              // A              // R
            letters[0] = 53; letters[12] = 65; letters[24] = 73; letters[32] = 85; letters[44] = 97; letters[56] = 99;
            letters[1] = 79; letters[13] = 79; letters[25] = 79; letters[33] = 79; letters[45] = 79; letters[57] = 87;
            letters[2] = 53; letters[14] = 69; letters[26] = 81; letters[34] = 89; letters[46] = 97; letters[58] = 105;
            letters[3] = 95; letters[15] = 79; letters[27] = 95; letters[35] = 95; letters[47] = 95; letters[59] = 79;
            letters[4] = 53; letters[16] = 67; letters[28] = 73; letters[36] = 89; letters[48] = 97;
            letters[5] = 87; letters[17] = 79; letters[29] = 95; letters[37] = 95; letters[49] = 87;
            letters[6] = 57; letters[18] = 67; letters[30] = 81; letters[38] = 93; letters[50] = 101;
            letters[7] = 87; letters[19] = 95; letters[31] = 79; letters[39] = 79; letters[51] = 87;
            letters[8] = 53; letters[20] = 65; letters[40] = 87; letters[52] = 97;
            letters[9] = 95; letters[21] = 95; letters[41] = 87; letters[53] = 95;
            letters[10] = 57; letters[22] = 69; letters[42] = 91; letters[54] = 101;
            letters[11] = 95; letters[23] = 95; letters[43] = 87; letters[55] = 95;

            f.z = 0.0f;

            for (int i = 0; i < size; i += 4)
            {
                Vector begin = MultiplyFloat(new Vector { x = letters[i] - 79.0f, y = letters[i + 1] - 79.0f, z = 0.0f }, 0.5f);
                Vector e = Add(MultiplyFloat(new Vector { x = letters[i + 2] - 79.0f, y = letters[i + 3] - 79.0f, z = 0.0f }, 0.5f), MultiplyFloat(begin, -1.0f));
                Vector o = MultiplyFloat(Add(f, MultiplyFloat(Add(begin, e), Min(-Min(Modulus(MultiplyFloat(Add(begin, f), -1.0f), e) / ModulusSelf(e), 0.0f), 1.0f))), -1.0f);

                distance = Min(distance, ModulusSelf(o));
            }

            distance = (float)Math.Sqrt(distance);

            Vector* curves = stackalloc Vector[2];

            curves[0] = new Vector { x = -11.0f, y = 6.0f, z = 0.0f };
            curves[1] = new Vector { x = 11.0f, y = 6.0f, z = 0.0f };

            for (int i = 1; i >= 0; i--)
            {
                Vector o = Add(f, MultiplyFloat(curves[i], -1.0f));
                float m = 0.0f;

                if (o.x > 0.0f)
                {
                    m = (float)Math.Abs(Math.Sqrt(ModulusSelf(o)) - 2.0f);
                }
                else
                {
                    if (o.y > 0.0f)
                        o.y += -2.0f;
                    else
                        o.y += 2.0f;

                    o.y += (float)Math.Sqrt(ModulusSelf(o));
                }

                distance = Min(distance, m);
            }

            distance = (float)Math.Pow(Math.Pow(distance, 8.0f) + Math.Pow(position.z, 8.0f), 0.125f) - 0.5f;
            *hitType = (int)PixarRayHit.Letter;

            float roomDistance = Min(-Min(BoxTest(position, new Vector { x = -30.0f, y = -0.5f, z = -30.0f }, new Vector { x = 30.0f, y = 18.0f, z = 30.0f }), BoxTest(position, new Vector { x = -25.0f, y = -17.5f, z = -25.0f }, new Vector { x = 25.0f, y = 20.0f, z = 25.0f })), BoxTest(new Vector { x = Math.Abs(position.x) % 8, y = position.y, z = position.z }, new Vector { x = 1.5f, y = 18.5f, z = -25.0f }, new Vector { x = 6.5f, y = 20.0f, z = 25.0f }));

            if (roomDistance < distance)
            {
                distance = roomDistance;
                *hitType = (int)PixarRayHit.Wall;
            }

            float sun = 19.9f - position.y;

            if (sun < distance)
            {
                distance = sun;
                *hitType = (int)PixarRayHit.Sun;
            }

            return distance;
        }

        private int RayMarching(Vector origin, Vector direction, Vector* hitPosition, Vector* hitNormal)
        {
            int hitType = (int)PixarRayHit.None;
            int noHitCount = 0;
            float distance = 0.0f;

            for (float i = 0; i < 100; i += distance)
            {
                *hitPosition = MultiplyFloat(Add(origin, direction), i);
                distance = Sample(*hitPosition, &hitType);

                if (distance < 0.01f || ++noHitCount > 99)
                {
                    *hitNormal = Inverse(new Vector { x = Sample(Add(*hitPosition, new Vector { x = 0.01f, y = 0.0f, z = 0.0f }), &noHitCount) - distance, y = Sample(Add(*hitPosition, new Vector { x = 0.0f, y = 0.01f, z = 0.0f }), &noHitCount) - distance, z = Sample(Add(*hitPosition, new Vector { x = 0.0f, y = 0.0f, z = 0.01f }), &noHitCount) - distance });

                    return hitType;
                }
            }

            return (int)PixarRayHit.None;
        }

        private Vector Trace(Vector origin, Vector direction)
        {
            Vector
                sampledPosition = new Vector { x = 1.0f, y = 1.0f, z = 1.0f },
                normal = new Vector { x = 1.0f, y = 1.0f, z = 1.0f },
                color = new Vector { x = 1.0f, y = 1.0f, z = 1.0f },
                attenuation = new Vector { x = 1.0f, y = 1.0f, z = 1.0f },
                lightDirection = Inverse(new Vector { x = 0.6f, y = 0.6f, z = 1.0f });

            for (int bounce = 3; bounce > 0; bounce--)
            {
                PixarRayHit hitType = (PixarRayHit)RayMarching(origin, direction, &sampledPosition, &normal);

                switch (hitType)
                {
                    case PixarRayHit.None:
                        break;

                    case PixarRayHit.Letter:
                        {
                            direction = MultiplyFloat(Add(direction, normal), Modulus(normal, direction) * -2.0f);
                            origin = MultiplyFloat(Add(sampledPosition, direction), 0.1f);
                            attenuation = MultiplyFloat(attenuation, 0.2f);

                            break;
                        }

                    case PixarRayHit.Wall:
                        {
                            float
                                incidence = Modulus(normal, lightDirection),
                                p = 6.283185f * Random(),
                                c = Random(),
                                s = (float)Math.Sqrt(1.0f - c),
                                g = normal.z < 0 ? -1.0f : 1.0f,
                                u = -1.0f / (g + normal.z),
                                v = normal.x * normal.y * u;

                            direction = Add(Add(new Vector { x = v, y = g + normal.y * normal.y * u, z = -normal.y * ((float)Math.Cos(p) * s) }, new Vector { x = 1.0f + g * normal.x * normal.x * u, y = g * v, z = -g * normal.x }), MultiplyFloat(normal, (float)Math.Sqrt(c)));
                            origin = MultiplyFloat(Add(sampledPosition, direction), 0.1f);
                            attenuation = MultiplyFloat(attenuation, 0.2f);

                            if (incidence > 0 && RayMarching(MultiplyFloat(Add(sampledPosition, normal), 0.1f), lightDirection, &sampledPosition, &normal) == (int)PixarRayHit.Sun)
                                color = MultiplyFloat(Multiply(Add(color, attenuation), new Vector { x = 500.0f, y = 400.0f, z = 100.0f }), incidence);

                            break;
                        }

                    case PixarRayHit.Sun:
                        {
                            color = Multiply(Add(color, attenuation), new Vector { x = 50.0f, y = 80.0f, z = 100.0f });

                            goto escape;
                        }
                }
            }

        escape:

            return color;
        }
    }

    // Fireflies Flocking
    struct Boid
    {
        public Vector position, velocity, acceleration;
    }

    [PerfClass("FirefliesFlocking", "Huatuo", "数值计算")]
    public unsafe struct FirefliesFlockingNET : IBenchmark
    {
        public uint boids;
        public uint lifetime;
        public float result;

        public void Prepare()
        {
            boids = PerfLevel.firefliesFlockingBoids;
            lifetime = PerfLevel.firefliesFlockingLifeTime;
        }

        public void Clear()
        {
        }
        public void Run()
        {
            result = FirefliesFlocking(boids, lifetime);
        }

        private uint parkMiller;
        private float maxSpeed;
        private float maxForce;
        private float separationDistance;
        private float neighbourDistance;

        private float FirefliesFlocking(uint boids, uint lifetime)
        {
            parkMiller = 666;
            maxSpeed = 1.0f;
            maxForce = 0.03f;
            separationDistance = 15.0f;
            neighbourDistance = 30.0f;

            Boid* fireflies = (Boid*)UnsafeUtils.Malloc((int)(boids * sizeof(Boid)), 16, out void* firefliesPointer);

            for (uint i = 0; i < boids; ++i)
            {
                fireflies[i].position = new Vector { x = Random(), y = Random(), z = Random() };
                fireflies[i].velocity = new Vector { x = Random(), y = Random(), z = Random() };
                fireflies[i].acceleration = new Vector { x = 0.0f, y = 0.0f, z = 0.0f };
            }

            for (uint i = 0; i < lifetime; ++i)
            {
                // Update
                for (uint boid = 0; boid < boids; ++boid)
                {
                    Add(&fireflies[boid].velocity, &fireflies[boid].acceleration);

                    float speed = Length(&fireflies[boid].velocity);

                    if (speed > maxSpeed)
                    {
                        Divide(&fireflies[boid].velocity, speed);
                        Multiply(&fireflies[boid].velocity, maxSpeed);
                    }

                    Add(&fireflies[boid].position, &fireflies[boid].velocity);
                    Multiply(&fireflies[boid].acceleration, maxSpeed);
                }

                // Separation
                for (uint boid = 0; boid < boids; ++boid)
                {
                    Vector separation = default(Vector);
                    int count = 0;

                    for (uint target = 0; target < boids; ++target)
                    {
                        Vector position = fireflies[boid].position;

                        Subtract(&position, &fireflies[target].position);

                        float distance = Length(&position);

                        if (distance > 0.0f && distance < separationDistance)
                        {
                            Normalize(&position);
                            Divide(&position, distance);

                            separation = position;
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        Divide(&separation, (float)count);
                        Normalize(&separation);
                        Multiply(&separation, maxSpeed);
                        Subtract(&separation, &fireflies[boid].velocity);

                        float force = Length(&separation);

                        if (force > maxForce)
                        {
                            Divide(&separation, force);
                            Multiply(&separation, maxForce);
                        }

                        Multiply(&separation, 1.5f);
                        Add(&fireflies[boid].acceleration, &separation);
                    }
                }

                // Cohesion
                for (uint boid = 0; boid < boids; ++boid)
                {
                    Vector cohesion = default(Vector);
                    int count = 0;

                    for (uint target = 0; target < boids; ++target)
                    {
                        Vector position = fireflies[boid].position;

                        Subtract(&position, &fireflies[target].position);

                        float distance = Length(&position);

                        if (distance > 0.0f && distance < neighbourDistance)
                        {
                            cohesion = fireflies[boid].position;
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        Divide(&cohesion, (float)count);
                        Subtract(&cohesion, &fireflies[boid].position);
                        Normalize(&cohesion);
                        Multiply(&cohesion, maxSpeed);
                        Subtract(&cohesion, &fireflies[boid].velocity);

                        float force = Length(&cohesion);

                        if (force > maxForce)
                        {
                            Divide(&cohesion, force);
                            Multiply(&cohesion, maxForce);
                        }

                        Add(&fireflies[boid].acceleration, &cohesion);
                    }
                }
            }

            UnsafeUtils.Free(firefliesPointer);

            return (float)parkMiller;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(Vector* left, Vector* right)
        {
            left->x += right->x;
            left->y += right->y;
            left->z += right->z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Subtract(Vector* left, Vector* right)
        {
            left->x -= right->x;
            left->y -= right->y;
            left->z -= right->z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Divide(Vector* vector, float value)
        {
            vector->x /= value;
            vector->y /= value;
            vector->z /= value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Multiply(Vector* vector, float value)
        {
            vector->x *= value;
            vector->y *= value;
            vector->z *= value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Normalize(Vector* vector)
        {
            float length = (float)Math.Sqrt(vector->x * vector->x + vector->y * vector->y + vector->z * vector->z);

            vector->x /= length;
            vector->y /= length;
            vector->z /= length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float Length(Vector* vector)
        {
            return (float)Math.Sqrt(vector->x * vector->x + vector->y * vector->y + vector->z * vector->z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float Random()
        {
            parkMiller = (uint)(((ulong)parkMiller * 48271u) % 0x7fffffff);

            return parkMiller / 10000000.0f;
        }
    }

    // Polynomials
    [PerfClass("Polynomials", "Huatuo", "数值计算")]
    public unsafe struct PolynomialsNET : IBenchmark
    {
        public uint iterations;
        public float result;

        public void Clear()
        {
        }

        public void Prepare()
        {
            iterations = PerfLevel.polynomialsIterations;
        }

        public void Run()
        {
            result = Polynomials(iterations);
        }

        private float Polynomials(uint iterations)
        {
            const float x = 0.2f;

            float pu = 0.0f;
            float* poly = stackalloc float[100];

            for (uint i = 0; i < iterations; i++)
            {
                float mu = 10.0f;
                float s;
                int j;

                for (j = 0; j < 100; j++)
                {
                    poly[j] = mu = (mu + 2.0f) / 2.0f;
                }

                s = 0.0f;

                for (j = 0; j < 100; j++)
                {
                    s = x * s + poly[j];
                }

                pu += s;
            }

            return pu;
        }
    }

    struct Particle
    {
        public float x, y, z, vx, vy, vz;
    }

    // Particle Kinematics
    [PerfClass("ParticleKinematics", "Huatuo", "数值计算")]
    public unsafe struct ParticleKinematicsNET : IBenchmark
    {
        public uint quantity;
        public uint iterations;
        public float result;

        public void Clear()
        {
        }

        public void Prepare()
        {
            quantity = PerfLevel.particleKinematicsQuantity;
            iterations = PerfLevel.particleKinematicsIterations;
        }

        public void Run()
        {
            result = ParticleKinematics(quantity, iterations);
        }

        private float ParticleKinematics(uint quantity, uint iterations)
        {
            Particle* particles = (Particle*)UnsafeUtils.Malloc((int)(quantity * sizeof(Particle)), 16, out void* particlesPointer);

            for (uint i = 0; i < quantity; ++i)
            {
                particles[i].x = (float)i;
                particles[i].y = (float)(i + 1);
                particles[i].z = (float)(i + 2);
                particles[i].vx = 1.0f;
                particles[i].vy = 2.0f;
                particles[i].vz = 3.0f;
            }

            for (uint a = 0; a < iterations; ++a)
            {
                for (uint b = 0, c = quantity; b < c; ++b)
                {
                    Particle* p = &particles[b];

                    p->x += p->vx;
                    p->y += p->vy;
                    p->z += p->vz;
                }
            }

            Particle particle = new Particle { x = particles[0].x, y = particles[0].y, z = particles[0].z };

            UnsafeUtils.Free(particlesPointer);

            return particle.x + particle.y + particle.z;
        }
    }

    // Arcfour
    [PerfClass("Arcfour", "Huatuo", "数值计算")]
    public unsafe struct ArcfourNET : IBenchmark
    {
        public uint iterations;
        public int result;

        public void Prepare()
        {
            iterations = PerfLevel.arcfourIterations;
        }

        public void Clear()
        {
        }

        public void Run()
        {
            result = Arcfour(iterations);
        }

        private int Arcfour(uint iterations)
        {
            const int keyLength = 5;
            const int streamLength = 10;

            byte* state = (byte*)UnsafeUtils.Malloc(256, 8, out void* statePointer);
            byte* buffer = (byte*)UnsafeUtils.Malloc(64, 8, out void* bufferPointer);
            byte* key = stackalloc byte[5];
            byte* stream = stackalloc byte[10];

            key[0] = 0xDB;
            key[1] = 0xB7;
            key[2] = 0x60;
            key[3] = 0xD4;
            key[4] = 0x56;

            stream[0] = 0xEB;
            stream[1] = 0x9F;
            stream[2] = 0x77;
            stream[3] = 0x81;
            stream[4] = 0xB7;
            stream[5] = 0x34;
            stream[6] = 0xCA;
            stream[7] = 0x72;
            stream[8] = 0xA7;
            stream[9] = 0x19;

            int idx = 0;

            for (uint i = 0; i < iterations; i++)
            {
                idx = KeySetup(state, key, keyLength);
                idx = GenerateStream(state, buffer, streamLength);
            }

            UnsafeUtils.Free(statePointer);
            UnsafeUtils.Free(bufferPointer);

            return idx;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int KeySetup(byte* state, byte* key, int length)
        {
            int i, j;
            byte t;

            for (i = 0; i < 256; ++i)
            {
                state[i] = (byte)i;
            }

            for (i = 0, j = 0; i < 256; ++i)
            {
                j = (j + state[i] + key[i % length]) % 256;
                t = state[i];
                state[i] = state[j];
                state[j] = t;
            }

            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GenerateStream(byte* state, byte* buffer, int length)
        {
            int i, j;
            int idx;
            byte t;

            for (idx = 0, i = 0, j = 0; idx < length; ++idx)
            {
                i = (i + 1) % 256;
                j = (j + state[i]) % 256;
                t = state[i];
                state[i] = state[j];
                state[j] = t;
                buffer[idx] = state[(state[i] + state[j]) % 256];
            }

            return i;
        }
    }

    // Seahash
    [PerfClass("Seahash", "Huatuo", "数值计算")]
    public unsafe struct SeahashNET : IBenchmark
    {
        public uint iterations;
        public ulong result;
        public void Prepare()
        {
            iterations = PerfLevel.seahashIterations;
        }

        public void Clear()
        {
        }
        public void Run()
        {
            result = Seahash(iterations);
        }

        private ulong Seahash(uint iterations)
        {
            const int bufferLength = 1024 * 128;

            byte* buffer = (byte*)UnsafeUtils.Malloc(bufferLength, 8, out void* bufferPointer);

            for (int i = 0; i < bufferLength; i++)
            {
                buffer[i] = (byte)(i % 256);
            }

            ulong hash = 0;

            for (uint i = 0; i < iterations; i++)
            {
                hash = Compute(buffer, bufferLength, 0x16F11FE89B0D677C, 0xB480A793D8E6C86C, 0x6FE2E5AAF078EBC9, 0x14F994A4C5259381);
            }

            UnsafeUtils.Free(bufferPointer);

            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong Read(byte* pointer)
        {
            return *(ulong*)pointer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong Diffuse(ulong value)
        {
            value *= 0x6EED0E9DA4D94A4F;
            value ^= ((value >> 32) >> (int)(value >> 60));
            value *= 0x6EED0E9DA4D94A4F;

            return value;
        }

        private ulong Compute(byte* buffer, ulong length, ulong a, ulong b, ulong c, ulong d)
        {
            const uint blockSize = 32;

            ulong end = length & ~(blockSize - 1);

            for (uint i = 0; i < end; i += blockSize)
            {
                a ^= Read(buffer + i);
                b ^= Read(buffer + i + 8);
                c ^= Read(buffer + i + 16);
                d ^= Read(buffer + i + 24);

                a = Diffuse(a);
                b = Diffuse(b);
                c = Diffuse(c);
                d = Diffuse(d);
            }

            ulong excessive = length - end;
            byte* bufferEnd = buffer + end;

            if (excessive > 0)
            {
                a ^= Read(bufferEnd);

                if (excessive > 8)
                {
                    b ^= Read(bufferEnd);

                    if (excessive > 16)
                    {
                        c ^= Read(bufferEnd);

                        if (excessive > 24)
                        {
                            d ^= Read(bufferEnd);
                            d = Diffuse(d);
                        }

                        c = Diffuse(c);
                    }

                    b = Diffuse(b);
                }

                a = Diffuse(a);
            }

            a ^= b;
            c ^= d;
            a ^= c;
            a ^= length;

            return Diffuse(a);
        }
    }

    // Radix
    [PerfClass("RadixN", "Huatuo", "数值计算")]
    public unsafe struct RadixNET : IBenchmark
    {
        public uint iterations;
        public int result;
        public void Prepare()
        {
            iterations = PerfLevel.radixIterations;
        }

        public void Clear()
        {
        }
        public void Run()
        {
            result = Radix(iterations);
        }

        private uint classicRandom;

        private int Radix(uint iterations)
        {
            classicRandom = 7525;

            const int arrayLength = 128;

            int* array = (int*)UnsafeUtils.Malloc(arrayLength * sizeof(int), 16, out void* arrayPointer);

            for (uint a = 0; a < iterations; a++)
            {
                for (int b = 0; b < arrayLength; b++)
                {
                    array[b] = Random();
                }

                Sort(array, arrayLength);
            }

            int head = array[0];

            UnsafeUtils.Free(arrayPointer);

            return head;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Random()
        {
            classicRandom = (6253729 * classicRandom + 4396403);

            return (int)(classicRandom % 32767);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FindLargest(int* array, int length)
        {
            int i;
            int largest = -1;

            for (i = 0; i < length; i++)
            {
                if (array[i] > largest)
                    largest = array[i];
            }

            return largest;
        }

        private void Sort(int* array, int length)
        {
            int i;
            int* semiSorted = stackalloc int[length];
            int significantDigit = 1;
            int largest = FindLargest(array, length);

            while (largest / significantDigit > 0)
            {
                int* bucket = stackalloc int[10];

                for (i = 0; i < length; i++)
                {
                    bucket[(array[i] / significantDigit) % 10]++;
                }

                for (i = 1; i < 10; i++)
                {
                    bucket[i] += bucket[i - 1];
                }

                for (i = length - 1; i >= 0; i--)
                {
                    semiSorted[--bucket[(array[i] / significantDigit) % 10]] = array[i];
                }

                for (i = 0; i < length; i++)
                {
                    array[i] = semiSorted[i];
                }

                significantDigit *= 10;
            }
        }
    }

}

