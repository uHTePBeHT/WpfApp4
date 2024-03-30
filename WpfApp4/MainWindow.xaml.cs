using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Panther panther;
        private Dog dog;
        private Turtle turtle;

        public MainWindow()
        {
            InitializeComponent();

            loadAssemblyButton.Click += LoadAssemblyButton_Click;

            classListBox.SelectionChanged += ClassListBox_SelectionChanged;

            executeButton.Click += ExecuteButton_Click;


            // Создание экземпляров животных с определенной скоростью
            panther = new Panther(15, 40);
            dog = new Dog(10, 20);
            turtle = new Turtle(1, 5);

            pantherSpeedTextBlock.Text = $"Current Panther Speed: {panther.MovementSpeed}";
            pantherMaxSpeedTextBlock.Text = $"Max Panther Speed: {panther.MaxSpeed}";

            dogSpeedTextBlock.Text = $"Current Dog Speed: {dog.MovementSpeed}";
            dogMaxSpeedTextBlock.Text = $"Max Dog Speed: {dog.MaxSpeed}";

            turtleSpeedTextBlock.Text = $"Current Turtle Speed: {turtle.MovementSpeed}";
            turtleMaxSpeedTextBlock.Text = $"Max Turtle Speed: {turtle.MaxSpeed}";

            // Подписываемся на события пантеры и обновляем текст в TextBlock
            panther.Moved += (sender, message) => pantherTextBlock.Text = message;
            panther.Stood += (sender, message) => pantherTextBlock.Text = message;
            panther.Roared += (sender, message) => pantherTextBlock.Text = message;
            panther.ClimbedTree += (sender, message) => pantherTextBlock.Text = message;

            // Подписываемся на события собаки и обновляем текст в TextBlock
            dog.Moved += (sender, message) => dogTextBlock.Text = message;
            dog.Stood += (sender, message) => dogTextBlock.Text = message;
            dog.Barked += (sender, message) => dogTextBlock.Text = message;

            // Подписываемся на события черепахи и обновляем текст в TextBlock
            turtle.Moved += (sender, message) => turtleTextBlock.Text = message;
            turtle.Stood += (sender, message) => turtleTextBlock.Text = message;
        }


        //----------------------------------------------------------------

        private void LoadAssemblyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string assemblyPath = assemblyPathTextBox.Text;

                // Загрузка сборки
                var assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);

                // Получение всех типов в сборке
                var types = assembly.GetTypes();

                // Фильтрация типов по реализации интерфейса
                var implementers = types.Where(t => typeof(LiveCreature).IsAssignableFrom(t));

                // Очистка списка классов и обновление списка
                classListBox.Items.Clear();
                foreach (var implementer in implementers)
                {
                    classListBox.Items.Add(implementer.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assembly: " + ex.Message);
            }
        }

        private void ClassListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Очистка старых контролов с параметрами методов
                methodListBox.Items.Clear();

                // Получение имени выбранного типа (класса)
                var selectedTypeName = classListBox.SelectedItem as string;

                if (selectedTypeName != null)
                {
                    // Получение типа (класса) по его имени
                    var selectedType = Type.GetType("WpfApp4." + selectedTypeName); // Пример указания пространства имен вашего приложения
                    if (selectedType != null)
                    {
                        // Получение всех методов выбранного типа
                        var methods = selectedType.GetMethods();

                        // Добавление методов в список для выбора
                        foreach (var method in methods)
                        {
                            methodListBox.Items.Add(method.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading class methods: " + ex.Message);
            }
        }



        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, был ли выбран класс
                if (classListBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a class.");
                    return;
                }

                // Проверяем, был ли выбран метод
                if (methodListBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a method.");
                    return;
                }

                // Получаем имя выбранного типа (класса)
                var selectedTypeName = classListBox.SelectedItem as string;
                if (selectedTypeName != null)
                {
                    // Получение типа (класса) по его имени
                    var selectedType = Type.GetType("WpfApp4." + selectedTypeName); // Пример указания пространства имен вашего приложения

                    // Создаем экземпляр выбранного класса
                    var instance = Activator.CreateInstance(selectedType, new object[] { default(double), default(double) });

                    // Получаем имя выбранного метода
                    var selectedMethodName = methodListBox.SelectedItem as string;

                    // Получаем информацию о выбранном методе
                    var selectedMethod = selectedType.GetMethod(selectedMethodName);

                    // Вызываем выбранный метод
                    var result = selectedMethod.Invoke(instance, null);

                    // Отображаем результат выполнения метода
                    MessageBox.Show("Method executed successfully. Result: " + result.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing method: " + ex.Message);
            }
        }










        //----------------------------------------------------------------

        private void MovePanther_Click(object sender, RoutedEventArgs e)
        {
            panther.Move();
            pantherSpeedTextBlock.Text = $"Current Panther Speed: {panther.MovementSpeed}";
            climbedTreeCheckBox.IsChecked = false;

        }

        private void StandPanther_Click(object sender, RoutedEventArgs e)
        {
            panther.Stand();
            pantherSpeedTextBlock.Text = $"Current Panther Speed: {panther.MovementSpeed}";
        }

        private void RoarPanther_Click(object sender, RoutedEventArgs e)
        {
            panther.Roar();

        }

        private void ClimbTreePanther_Click(object sender, RoutedEventArgs e)
        {
            panther.ClimbTree();
            pantherSpeedTextBlock.Text = $"Current Panther Speed: {panther.MovementSpeed}";
            climbedTreeCheckBox.IsChecked = true;
        }


        //----------------------------------------------------------------

        private void MoveDog_Click(object sender, RoutedEventArgs e)
        {
            dog.Move();
            dogSpeedTextBlock.Text = $"Current Dog Speed: {dog.MovementSpeed}";
        }


        private void StandDog_Click(object sender, RoutedEventArgs e)
        {
            dog.Stand();
            dogSpeedTextBlock.Text = $"Current Dog Speed: {dog.MovementSpeed}";
        }

        private void BarkDog_Click(object sender, RoutedEventArgs e)
        {
            dog.Bark();
        }


        //----------------------------------------------------------------


        private void MoveTurtle_Click(object sender, RoutedEventArgs e)
        {
            turtle.Move();
            turtleSpeedTextBlock.Text = $"Current Turtle Speed: {turtle.MovementSpeed}";
        }

        private void StandTurtle_Click(object sender, RoutedEventArgs e)
        {
            turtle.Stand();
            turtleSpeedTextBlock.Text = $"Current Turtle Speed: {turtle.MovementSpeed}";
        }
    }
}
