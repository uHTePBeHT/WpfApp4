using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
    public class Turtle : LiveCreature
    {
        // Создаем события для сигнализации о действиях черепахи
        public event EventHandler<string> Moved;
        public event EventHandler<string> Stood;

        public Turtle(double movementSpeed, double maxSpeed) : base(movementSpeed, maxSpeed)
        {
        }

        public override string Move()
        {
            MovementSpeed = Math.Min(MovementSpeed + 0.5, MaxSpeed); // Увеличиваем скорость перемещения
            Moved?.Invoke(this, $"Черепаха ускорилась до {MovementSpeed}.");
            return $"Черепаха ускорилась до {MovementSpeed}.";
        }

        public override string Stand()
        {
            MovementSpeed = Math.Max(MovementSpeed - 0.5, 0); // Уменьшаем скорость перемещения

            if (MovementSpeed > 0)
            {
                Stood?.Invoke(this, $"Черепаха замедлилась до {MovementSpeed}.");
                return $"Черепаха замедлилась до {MovementSpeed}.";
            }
            else
            {
                Stood?.Invoke(this, "Черепаха остановилась.");
                return "Черепаха остановилась.";
            }
        }
    }
}
