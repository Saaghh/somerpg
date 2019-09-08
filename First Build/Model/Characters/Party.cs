using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public class Party : IEnumerable<Character>
    {
        private Character[] characters;

        public int Count => characters.Length;

        public bool IsAlive
        {
            get
            {
                bool isAlive = false;
                for (int i = 0; i < Count; i++)
                {
                    if (characters[i].IsAlive) { isAlive = true; }
                }

                return isAlive;
            }
        }

        public Party()
        {
            characters = new Character[1];
            for (int i = 0; i < Count; i++)
            {
                characters[i] = Character.Test;
            }
        }

        public Character this[int x]
        {
            get
            {
                return characters[x];
            }
            set
            {
                characters[x] = value;
            }
        }

        public IEnumerator<Character> GetEnumerator()
        {
            return ((IEnumerable<Character>)characters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Character>)characters).GetEnumerator();
        }
    }
}
