using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
    public class Dog : LiveCreature
    {

        public event EventHandler<string> Moved;
        public event EventHandler<string> Stood;
        public event EventHandler<string> Barked;

        public Dog(double movementSpeed, double maxSpeed) : base(movementSpeed, maxSpeed)
        {
        }

        public override void Move()
        {
            MovementSpeed = Math.Min(MovementSpeed + 4, MaxSpeed); // Увеличиваем скорость перемещения
            Moved?.Invoke(this, $"Собака ускорилась до {MovementSpeed}.");
        }

        public override void Stand()
        {
            MovementSpeed = Math.Max(MovementSpeed - 4, 0); // Уменьшаем скорость перемещения

            if (MovementSpeed > 0)
            {
                Stood?.Invoke(this, $"Собака замедлилась до {MovementSpeed}.");
            }
            else
            {
                Stood?.Invoke(this, "Собака остановилась.");
            }
        }

        public void Bark()
        {
            Barked?.Invoke(this, "Собака лает: Гав! Гав!");
        }
    }
}
