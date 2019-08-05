using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Controller
{
    public static class HexMapMath
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
            result.width = ((HEXWIDTH / 4 * 3) * dataSize.x) + (HEXWIDTH / 4);
            result.height = (HEXHEIGHT * dataSize.y) + (HEXHEIGHT / 2);

            return result;
        }

        public static (int x, int y) GetCharacterStarterPosition((int w, int h) mapSize, int order, int team)
        {
            (int x, int y) position;

            switch (team)
            {
                case 0:
                    position.x = mapSize.w / 8 * 3;
                    position.y = mapSize.h / 8 * 3 + order;
                    break;
                case 1:
                    position.x = mapSize.w / 8 * 5;
                    position.y = mapSize.h / 8 * 3 + order;
                    break;
                default:
                    throw new Exception("Сражаться могут лишь 2 команды");
            }

            return position;
        }
    }

}
