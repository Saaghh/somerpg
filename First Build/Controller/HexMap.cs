using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Controller
{
    public static class HexMap
    {
        public const int HEXWIDTH = 200;
        public const int HEXHEIGHT = 160;

        public static (int x, int y) GetHexCoordinate(int x, int y)
        {
            int resultX, resultY;
            if (x % 2 != 1) //если четный столбец
            {
                resultX = (HEXWIDTH / 4 * 3) * x;
                resultY = (HEXHEIGHT / 2) + (HEXHEIGHT * y);
            }
            else
            {
                resultX = (HEXWIDTH / 4 * 3) * x;
                resultY = HEXHEIGHT * y;
            }

            var result = (resultX, resultY);

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSize">Размер карты в тайлах</param>
        /// <returns>Размер карты в пикселях</returns>
        public static (int width, int height) GetMapPixelSize((int x, int y) dataSize)
        {
            (int width, int height) result;
            result.width = (150 * dataSize.x) + 50;
            result.height = (200 * dataSize.y);

            return result;
        }
    }

}
