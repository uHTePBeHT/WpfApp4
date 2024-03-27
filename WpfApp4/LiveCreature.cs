using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    public abstract class LiveCreature
    {
        private double _movementSpeed; // Скорость перемещения
        private double _maxSpeed;

        public double MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        public double MaxSpeed
        {
            get { return _maxSpeed; }
            set { _maxSpeed = value; }
        }

        public LiveCreature(double movementSpeed, double maxSpeed)
        {
            MovementSpeed = movementSpeed;
            MaxSpeed = maxSpeed;
        }

        public abstract void Move(); // Двигаться
        public abstract void Stand(); // Стоять
    }
}
