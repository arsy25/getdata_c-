using System;
using System.IO.Ports;

class Program
{
    static void Main(string[] args)
    {
        string portName = "COM6";  // Ganti dengan nama port serial yang digunakan oleh Arduino Anda
        int baudRate = 115200;

        SerialPort serialPort = new SerialPort(portName, baudRate);

        try
        {
            serialPort.Open();

            if (serialPort.IsOpen)
            {
                Console.WriteLine("Port serial terbuka.");

                while (true)
                {
                    try
                    {
                        string data = serialPort.ReadLine().Trim(); // Baca dan hapus spasi ekstra
                        string[] splitData = data.Split(':'); // Pisahkan berdasarkan karakter ':'

                        if (splitData.Length == 2)
{
    string label = splitData[0].Trim();
    string valueStr = splitData[1].Trim();

    switch (label)
    {
        case "RPM":
            if (int.TryParse(valueStr, out int rpm))
            {
                Console.WriteLine(rpm);
            }
            else
            {
                Console.WriteLine($"Gagal mengonversi RPM: {valueStr}");
            }
            break;
        case "Speed (km/h)":
            if (float.TryParse(valueStr, out float speedFloat))
            {
                int speed = (int)speedFloat;
                Console.WriteLine(speed);
            }
            else
            {
                Console.WriteLine($"Gagal mengonversi Speed: {valueStr}");
            }
            break;
        case "Handlebar Position (%)":
            if (int.TryParse(valueStr, out int handlebarPosition))
            {
                Console.WriteLine(handlebarPosition);
            }
            else
            {
                Console.WriteLine($"Gagal mengonversi Handlebar Position: {valueStr}");
            }
            break;
        default:
            Console.WriteLine($"Data tidak valid: {data}");
            break;
    }
}


                    }
                    catch (TimeoutException) { }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
        }
        finally
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}
