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

        public override void Move()
        {
            _climbedTree = false; // При движении сбрасываем состояние залезания на дерево
            MovementSpeed = Math.Min(MovementSpeed + 5, MaxSpeed); // Увеличиваем скорость перемещения
            Moved?.Invoke(this, $"Пантера ускорилась до {MovementSpeed}.");
        }

        public override void Stand()
        {
            MovementSpeed = Math.Max(MovementSpeed - 5, 0); // Уменьшаем скорость перемещения

            if (MovementSpeed > 0)
            {
                Stood?.Invoke(this, $"Пантера замедлилась до {MovementSpeed}.");
            }
            else
            {
                Stood?.Invoke(this, "Пантера остановилась.");
            }
        }

        public void Roar()
        {
            Roared?.Invoke(this, "Пантера рычит: Ррррррр!");
        }

        public void ClimbTree()
        {

            _climbedTree = true;

            MovementSpeed = 0;

            ClimbedTree?.Invoke(this, "Пантера залезла на дерево.");

        }
    }
}
