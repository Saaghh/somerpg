using System;
using System.Collections.Generic;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace somerpg_uwp
{
    public class Water : TileLayer, IDrawableObject
    {
        protected Dictionary<ResourceKey, bool> triangles = new Dictionary<ResourceKey, bool>();
        
        protected Water()
        {
            AddTriangles();
        }

        protected Water(bool isFull) : this()
        {
            foreach (var item in triangles.Keys)
            {
                triangles[item] = isFull;
            }
        }

        protected Water(ResourceKey start, int length) : this()
        {
            if (length > 1 && length < 5)
            {
                int startInt = 0;
                switch (start)
                {
                    case ResourceKey.TriangleTL:
                        startInt = 0;
                        break;
                    case ResourceKey.TriangleT:
                        startInt = 1;
                        break;
                    case ResourceKey.TriangleTR:
                        startInt = 2;
                        break;
                    case ResourceKey.TriangleBR:
                        startInt = 3;
                        break;
                    case ResourceKey.TriangleB:
                        startInt = 4;
                        break;
                    case ResourceKey.TriangleBL:
                        startInt = 5;
                        break;
                    default:
                        throw new ArgumentException("Wrong starter position");
                }

                for (int i = startInt; i < length + startInt; i++)
                {
                    int j = i;
                    if (j > 5)
                    {
                        j -= 6;
                    }

                    FillTriangle(j);
                }
            }
            else
            {
                throw new ArgumentException("Wrong length. Must be between 1 and 5");
            }
        }

        public void Draw(CanvasAnimatedDrawEventArgs args, int x, int y)
        {
            foreach (ResourceKey item in triangles.Keys)
            {
                if (triangles[item])
                {
                    if (args != null)
                    {
                        args.DrawingSession.FillGeometry(MainPage.DrawingResources[item] as CanvasGeometry, x, y, MainPage.DrawingResources[ResourceKey.BlueBrush] as CanvasSolidColorBrush);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }
        private void FillTriangle(ResourceKey position)
        {
            if (triangles.ContainsKey(position))
            {
                triangles[position] = true;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private void FillTriangle(int position)
        {
            switch (position)
            {
                case 0:
                    FillTriangle(ResourceKey.TriangleTL);
                    break;
                case 1:
                    FillTriangle(ResourceKey.TriangleT);
                    break;
                case 2:
                    FillTriangle(ResourceKey.TriangleTR);
                    break;
                case 3:
                    FillTriangle(ResourceKey.TriangleBR);
                    break;
                case 4:
                    FillTriangle(ResourceKey.TriangleB);
                    break;
                case 5:
                    FillTriangle(ResourceKey.TriangleBL);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        private void AddTriangles()
        {
            triangles.Add(ResourceKey.TriangleTL, false);
            triangles.Add(ResourceKey.TriangleT, false);
            triangles.Add(ResourceKey.TriangleTR, false);
            triangles.Add(ResourceKey.TriangleBR, false);
            triangles.Add(ResourceKey.TriangleB, false);
            triangles.Add(ResourceKey.TriangleBL, false);
        }

        public static Water EmptyWater
        {
            get
            {
                return new Water();
            }
        }
        public static Water FullWater
        {
            get
            {
                return new Water(true);
            }
        }
    }
}