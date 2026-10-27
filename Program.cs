using System.Text;
using System.Linq;

namespace VehicleCreationSystem;

class Program
{
    public static void Main(string[] args)
    {
        Print.Introduction();
        VehicleManager manager = new VehicleManager();
        while (true)
        {
            Print.Menu();
            string input = Console.ReadLine()?.Trim() ?? "";

            switch (input)
            {
                case "1":
                    manager.CreateVehicle("car", ChooseFactory());
                    break;

                case "2":
                    manager.CreateVehicle("mot", ChooseFactory());
                    break;

                case "3":
                    manager.ShowAllVehicles();
                    break;

                case "4":
                    manager.StartAllVehicles();
                    break;

                case "5":
                    manager.StopAllVehicles();
                    break;
                case "6":
                    manager.ClearAllVehicles();
                    break;
                case "7":
                    Print.Message("Введите Id транспортного средства");
                    int id;
                    if (!int.TryParse(Console.ReadLine()?.Trim(), out id))
                    {
                        Print.Message("Некорректный ID.");
                        break;
                    }
                    IVehicle? vehicle = manager.FindVechicle(id);
                    if (vehicle == null)
                    {
                        Print.Message($"Транспортное средство c Id = {id} не найдено");
                    }
                    else
                    {
                        if (vehicle is Car car)
                        {
                            Print.CarMenu();
                            string inp = Console.ReadLine()?.Trim() ?? "";
                            switch (inp)
                            {
                                case "1":
                                    manager.StartVehicles(vehicle);
                                    break;
                                case "2":
                                    manager.StopVehicles(vehicle);
                                    break;
                                case "3":
                                    manager.OpenTrunk(vehicle);
                                    break;
                                case "4":
                                    manager.CloseTrunk(vehicle);
                                    break;
                            }
                        }
                        else if (vehicle is Motocycle moto)
                        {
                            Print.MotoMenu();
                            string inp = Console.ReadLine()?.Trim() ?? "";
                            switch (inp)
                            {
                                case "1":
                                    manager.StartVehicles(vehicle);
                                    break;
                                case "2":
                                    manager.StopVehicles(vehicle);
                                    break;
                                case "3":
                                    manager.DoWheelie(vehicle);
                                    break;
                                default: Print.Message("Ошибка ввода."); break;
                            }
                        }
                    }
                    break;
                case "8":
                    Print.Message("Программа успешно завершена!");
                    return;

                default:
                    Print.Message("Ошибка ввода. Команда не распознана");
                    break;
            }
        }
    }
    static IVehicleFactory ChooseFactory()
    {
        while (true)
        {
            Print.LittleMenu();
            switch (Console.ReadLine()?.Trim())
            {
                case "1": return new ElectricVehicleFactory();
                case "2": return new GasVehicleFactory();
                default: Print.Message("Ошибка ввода."); break;
            }
        }
    }
}
// == Вспомогательные классы ==
class Print
{
    public static void Introduction()
    {
        Console.WriteLine(
            "Работу выполнили: Бедарева Софья и Сторожева Анна\n" +
            "Группа: 9ИС-435\n" +
            "Практическая работа №2: Реализация порождающего паттерна «Абстрактная фабрика» в C#\n\n"
        );
    }
    public static void LittleMenu()
    {
        Console.WriteLine(
                "Выберите тип двигателя:\n" +
                "1 - Электрический\n" +
                "2 - Бензиновый\n"
            );
    }
    public static void Menu()
    {
        Console.WriteLine(
            "Выберите действие\n" +
            "1 - Создать автомобиль\n" +
            "2 - Создать мотоцикл\n" +
            "3 - Показать все транспортные средства\n" +
            "4 - Запустить все транспортные средства\n" +
            "5 - Остановить все транспортные средства\n" +
            "6 - Очистить список\n" +
            "7 - Действия с конкретным транспортным средством\n" +
            "8 - Выход из программы\n"
        );
    }

    public static void CarMenu()
    {
        Console.WriteLine(
            "Выберите действие:\n" +
            "1 - Запустить транспортное средство\n" +
            "2 - Остановить транспортное средство\n" +
            "3 - Открыть багажник\n" +
            "4 - Закрыть багажник\n"
        );
    }

    public static void MotoMenu()
    {
        Console.WriteLine(
            "Выберите действие:\n" +
            "1 - Запустить транспортное средство\n" +
            "2 - Остановить транспортное средство\n" +
            "3 - Сделать трюк\n"
        );
    }
    public static void Message(string info)
    {
        Console.WriteLine(info);
    }
}

class VehicleManager
{
    List<IVehicle> _vahicle = new();
    public void CreateVehicle(string type, IVehicleFactory factory)
    {
        if (type == "car")
        {
            _vahicle.Add(factory.CreateCar());
            Print.Message("Машина успешно создана!\n");
        }
        if (type == "mot")
        {
            _vahicle.Add(factory.CreateMotorcycle());
            Print.Message("Мотоцикл успешно создан!\n");
        }
    }
    public void ShowAllVehicles()
    {
        if (_vahicle.Count == 0)
        {
            Print.Message("Список транспортных средств пуст.\n");
            return;
        }
        foreach (var item in _vahicle)
        {
            Print.Message(item.GetInfo());
            Print.Message("------------------------------------------");
        }
    }
    public void StartAllVehicles()
    {
        if (_vahicle.Count == 0)
        {
            Print.Message("Невозможно запустить транспортные средства. Список транспортных средств пуст.\n");
            return;
        }
        foreach (var item in _vahicle)
        {
            item.Start();
        }
    }
    public void StartVehicles(IVehicle veh)
    {
        veh.Start();
    }
    public void StopAllVehicles()
    {
        if (_vahicle.Count == 0)
        {
            Print.Message("Невозможно остановить транспортные средства. Список транспортных средств пуст.\n");
            return;
        }
        foreach (var item in _vahicle)
        {
            item.Stop();
        }
    } 
    public void StopVehicles(IVehicle veh)
    {
        veh.Stop();
    }    public void ClearAllVehicles()
    {
        _vahicle.Clear();
    }
    public void OpenTrunk(IVehicle veh)
    {
        if (veh is ICar car)
            car.OpenTrunk();
        else
            Print.Message("У этого транспортного средства нет багажника.");
    }

    public void CloseTrunk(IVehicle veh)
    {
        if (veh is ICar car)
            car.CloseTrunk();
        else
            Print.Message("У этого транспортного средства нет багажника.");
    }
    public void DoWheelie(IVehicle veh)
    {
        if (veh is IMotocycle moto)
            moto.DoWheelie();
        else
            Print.Message("Данное транспортное средство не может выполнять трюки");
    }
    public IVehicle? FindVechicle(int id)
    {
        return _vahicle.FirstOrDefault(v => v.Id == id);
    }
}

//  == Интерфейсы ==
public interface IVehicle
{
    int Id { get; }
    void Start();
    void Stop();

    string GetInfo();
}

public interface ICar : IVehicle
{
    void OpenTrunk(); 
    void CloseTrunk();
}

public interface IMotocycle : IVehicle
{
    void DoWheelie(); 
}

// == Абстрактные классы ==
abstract class Vehicle : IVehicle
{
    private static int _nextId = 1;

    public int Id { get; }
    protected bool isRunning;
    public abstract string VehicleName { get; }
    protected Vehicle()
    {
        Id = _nextId++;
    }

    public virtual string GetInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine("------------------------------------------");
        sb.AppendLine($"Транспортное средство: {VehicleName}");
        sb.AppendLine($"ID: {Id}");
        AppendSpecificInfo(sb);
        sb.Append($"Состояние: {(isRunning ? "Едет" : "Стоит")}\n");
        return sb.ToString();
    }
    protected virtual void AppendSpecificInfo(StringBuilder sb) { }
    public virtual void Start()
    {
        if (isRunning)
        {
            Print.Message($"{VehicleName} уже едет");
            return;
        }
        isRunning = true;
        Print.Message($"{VehicleName} успешно запущен");
    }

    public virtual void Stop()
    {
        if (!isRunning)
        {
            Print.Message($"{VehicleName} уже стоит");
            return;
        }
        isRunning = false;
        Print.Message($"{VehicleName} успешно остановлен");
    }
}
abstract class Car : Vehicle, ICar
{
    protected bool trunkIsOpen;
    public override string VehicleName => "Автомобиль";
    protected override void AppendSpecificInfo(StringBuilder sb)
    {
        sb.AppendLine($"Багажник: {(trunkIsOpen ? "Открыт" : "Закрыт")}");
    }

    public void OpenTrunk()
    {
        if (trunkIsOpen)
        {
            Print.Message("Багажник уже открыт");
            return;
        }
        if (isRunning)
        {
            Print.Message($"Нельзя открыть багажник, когда {VehicleName.ToLower()} движется");
            return;
        }
        trunkIsOpen = true;
        Print.Message("Багажник успешно открыт");
    }
    public void CloseTrunk()
    {
        if (!trunkIsOpen)
        {
            Print.Message("Багажник уже закрыт");
            return;
        }
        trunkIsOpen = false;
        Print.Message("Багажник успешно закрыт");
    }
    public override void Start()
    {
        if (trunkIsOpen)
        {
            Print.Message($"Нельзя запустить {VehicleName.ToLower()}, когда багажник открыт");
            return;
        }
        base.Start();
    }
}
abstract class Motocycle : Vehicle, IMotocycle
{
    public override string VehicleName => "Мотоцикл";
    public void DoWheelie()
    {
        if (isRunning)
        {
            Print.Message("Вы успешно сделали трюк.");
        }
        else if (!isRunning)
        {
            Print.Message($"Вы не можете сделать трюк пока {VehicleName.ToLower()} стоит");
        }
    }
}
// == Конкретные реализации ==
class ElectricCar : Car
{
    public override string VehicleName => "Электромобиль";
}

class GasCar : Car
{
    public override string VehicleName => "Автомобиль с бензиновым двигателем";
}

class ElecticMotocycle : Motocycle
{
    public override string VehicleName => "Электромотоцикл";
}

class GasMotocycle : Motocycle
{
    public override string VehicleName => "Мотоцикл на бензиновом двигателе";
}

// == Фабрики ==
public interface IVehicleFactory
{
    ICar CreateCar();
    IMotocycle CreateMotorcycle();
}

public class ElectricVehicleFactory : IVehicleFactory
{
    public ICar CreateCar() => new ElectricCar();

    public IMotocycle CreateMotorcycle() => new ElecticMotocycle();
}

public class GasVehicleFactory : IVehicleFactory
{
    public ICar CreateCar() => new GasCar();

    public IMotocycle CreateMotorcycle() => new GasMotocycle();
}