﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace First_Build.Model
{
    public class Character
    {
        public string name = "John";
        public float health = 200f;
        public int weapon = 10;
        public float weaponSpeed = 0.5f;
        public int armor = 1;

        public DependencyProperty Name;

        public Character()
        {
            Name = DependencyProperty.Register("StringToTextBox", typeof(string), typeof(TextBox));
        }

        public void GetHit(int damage)
        {
            health -= weapon - armor;
        }

        public void Attack(Character target)
        {
            target.GetHit(weapon);
        }
    }
}
