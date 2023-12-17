using System;
using System.Collections.Generic;

public enum VehicleType
{
    Car = 1,
    Moto = 2
}

public class Vehicle
{
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public VehicleType Type { get; set; }

    public Vehicle(string licensePlate, string brand, VehicleType type)
    {
        LicensePlate = licensePlate;
        Brand = brand;
        Type = type;
    }

    public string GetVehicleTypeDescription()
    {
        return Type == VehicleType.Car ? "Carro" : "Moto";
    }
}

public class ParkingLot
{
    private List<Vehicle> vehicles;
    private decimal parkingRate;
    private decimal hourlyRate;

    public ParkingLot(decimal parkingRate, decimal hourlyRate)
    {
        this.parkingRate = parkingRate;
        this.hourlyRate = hourlyRate;
        vehicles = new List<Vehicle>();
    }

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public decimal RemoveVehicle(string licensePlate)
    {
        Vehicle vehicleToRemove = vehicles.Find(v => v.LicensePlate == licensePlate);

        if (vehicleToRemove != null)
        {
            Console.Write($"Digite por quantas horas o veículo {licensePlate} ficou estacionado: ");
            int parkedHours = Convert.ToInt32(Console.ReadLine());

            vehicles.Remove(vehicleToRemove);

            // Simulação de cálculo do valor cobrado (pode ser ajustado conforme a lógica real do estacionamento)
            decimal amountCharged = CalculateParkingFee(parkedHours);
            return amountCharged;
        }
        else
        {
            return -1; // Indica que o veículo não foi encontrado
        }
    }

    public void ListVehicles()
    {
        Console.WriteLine("Veículos estacionados:");

        if (vehicles.Count > 0)
        {
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Placa: {vehicle.LicensePlate}, Marca: {vehicle.Brand}, Tipo: {vehicle.GetVehicleTypeDescription()}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum veículo no estacionamento.");
        }
    }

    private decimal CalculateParkingFee(int parkedHours)
    {
        // Simulação de cálculo do valor cobrado (pode ser ajustado conforme a lógica real do estacionamento)
        return parkingRate + hourlyRate * parkedHours;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Digite o valor do estacionamento: ");
        decimal parkingRate = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Digite o valor por hora: ");
        decimal hourlyRate = Convert.ToDecimal(Console.ReadLine());

        ParkingLot parkingLot = new ParkingLot(parkingRate, hourlyRate);

        while (true)
        {
            Console.WriteLine("1. Adicionar veículo");
            Console.WriteLine("2. Remover veículo");
            Console.WriteLine("3. Listar veículos");
            Console.WriteLine("4. Sair");

            Console.Write("Escolha uma opção: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddVehicle(parkingLot);
                    break;

                case "2":
                    RemoveVehicle(parkingLot);
                    break;

                case "3":
                    parkingLot.ListVehicles();
                    break;

                case "4":
                    Console.WriteLine("Saindo do programa...");
                    return;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void AddVehicle(ParkingLot parkingLot)
    {
        Console.WriteLine("Escolha o tipo de veículo:");
        Console.WriteLine("1. Carro");
        Console.WriteLine("2. Moto");

        Console.Write("Digite a opção: ");
        string vehicleTypeOption = Console.ReadLine();

        if (vehicleTypeOption == "1" || vehicleTypeOption == "2")
        {
            VehicleType vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), vehicleTypeOption);
            Console.Write("Digite a placa do veículo: ");
            string licensePlate = Console.ReadLine();
            Console.Write("Digite a marca do veículo: ");
            string brand = Console.ReadLine();

            Vehicle newVehicle = new Vehicle(licensePlate, brand, vehicleType);
            parkingLot.AddVehicle(newVehicle);
            Console.WriteLine("Veículo adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Opção inválida para o tipo de veículo.");
        }
    }

    static void RemoveVehicle(ParkingLot parkingLot)
    {
        Console.Write("Digite a placa do veículo a ser removido: ");
        string plateToRemove = Console.ReadLine();

        decimal amountCharged = parkingLot.RemoveVehicle(plateToRemove);
        if (amountCharged >= 0)
        {
            Console.WriteLine($"Veículo removido. Valor cobrado: {amountCharged:C}");
        }
        else
        {
            Console.WriteLine("Veículo não encontrado no estacionamento.");
        }
    }
}
