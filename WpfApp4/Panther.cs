using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
    public class Panther : LiveCreature
    {
        private bool _climbedTree;

        // Создаем события для сигнализации о действиях пантеры
        public event EventHandler<string> Moved;
        public event EventHandler<string> Stood;
        public event EventHandler<string> Roared;
        public event EventHandler<string> ClimbedTree;

        public Panther(double movementSpeed, double maxSpeed) : base(movementSpeed, maxSpeed)
        {
        }

        public override string Move() // изменение типа возвращаемого значения
        {
            _climbedTree = false; // При движении сбрасываем состояние залезания на дерево
            MovementSpeed = Math.Min(MovementSpeed + 5, MaxSpeed); // Увеличиваем скорость перемещения
            return $"Пантера ускорилась до {MovementSpeed}."; // возвращаем строку с сообщением
        }

        public override string Stand() // изменение типа возвращаемого значения
        {
            MovementSpeed = Math.Max(MovementSpeed - 5, 0); // Уменьшаем скорость перемещения

            if (MovementSpeed > 0)
            {
                return $"Пантера замедлилась до {MovementSpeed}."; // возвращаем строку с сообщением
            }
            else
            {
                return "Пантера остановилась."; // возвращаем строку с сообщением
            }
        }

        public string Roar() // изменение типа возвращаемого значения
        {
            return "Пантера рычит: Ррррррр!"; // возвращаем строку с сообщением
        }

        public string ClimbTree() // изменение типа возвращаемого значения
        {
            _climbedTree = true;

            MovementSpeed = 0;

            return "Пантера залезла на дерево."; // возвращаем строку с сообщением
        }
    }

}
