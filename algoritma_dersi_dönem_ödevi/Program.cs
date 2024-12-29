using System;

class Program
{
    static void Main(string[] args)
    {
        int ogrenciSayisi = 0;

        while (true)
        {
            try
            {
                Console.Write("Kaç öğrenci kaydetmek istiyorsunuz: ");
                ogrenciSayisi = int.Parse(Console.ReadLine());
                if (ogrenciSayisi < 1)
                    Console.WriteLine("Lütfen 1 veya daha büyük bir sayı giriniz.");
                else
                    break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Hatalı giriş yaptınız! Lütfen geçerli bir tam sayı giriniz.");
            }
        }

        string[,] ogrenciler = new string[ogrenciSayisi, 7];

        for (int i = 0; i < ogrenciSayisi; i++)
        {
            Console.WriteLine($"\n{i + 1}. Öğrencinin bilgilerini giriniz:");

            while (true)
            {
                try
                {
                    Console.Write("Numara (11 haneli): ");
                    string number = Console.ReadLine();
                    if (number.Length != 11 || !long.TryParse(number, out _))
                        throw new FormatException();
                    ogrenciler[i, 0] = number;
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Hatalı giriş yaptınız! Öğrenci numarası 11 haneli bir sayı olmalıdır.");
                }
            }

            Console.Write("Ad: ");
            ogrenciler[i, 1] = Console.ReadLine();

            Console.Write("Soyad: ");
            ogrenciler[i, 2] = Console.ReadLine();

            int vize = GetValidatedGrade("Vize Notu");
            ogrenciler[i, 3] = vize.ToString();

            int final = GetValidatedGrade("Final Notu");
            ogrenciler[i, 4] = final.ToString();

            int ortalama = (int)(vize * 0.4 + final * 0.6);
            ogrenciler[i, 5] = ortalama.ToString();
            ogrenciler[i, 6] = GetLetterGrade(ortalama);
        }

        Console.WriteLine("\n{0,-15}{1,-15}{2,-15}{3,-10}{4,-10}{5,-15}{6,-10}",
            "Numara", "Ad", "Soyad", "Vize", "Final", "Ortalama", "Harf Notu");

        for (int i = 0; i < ogrenciSayisi; i++)
        {
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-10}{4,-10}{5,-15}{6,-10}",
                ogrenciler[i, 0], ogrenciler[i, 1], ogrenciler[i, 2],
                ogrenciler[i, 3], ogrenciler[i, 4], ogrenciler[i, 5], ogrenciler[i, 6]);
        }

        double sınıfOrtalaması = 0;
        int EnYüksekNot = int.MinValue;
        int enDüşükNot = int.MaxValue;

        for (int i = 0; i < ogrenciSayisi; i++)
        {
            int ortalama = int.Parse(ogrenciler[i, 5]);
            sınıfOrtalaması += ortalama;
            if (ortalama > EnYüksekNot) EnYüksekNot = ortalama;
            if (ortalama < enDüşükNot) enDüşükNot = ortalama;
        }
        sınıfOrtalaması /= ogrenciSayisi;

        Console.WriteLine($"\nSınıf Ortalaması: {sınıfOrtalaması:F2}");
        Console.WriteLine($"En Yüksek Not: {EnYüksekNot}");
        Console.WriteLine($"En Düşük Not: {enDüşükNot}");
    }

    static int GetValidatedGrade(string prompt)
    {
        int grade = 0;
        while (true)
        {
            try
            {
                Console.Write($"{prompt} (0-100): ");
                grade = int.Parse(Console.ReadLine());
                if (grade < 0 || grade > 100)
                    Console.WriteLine("Hatalı giriş yaptınız! Lütfen 0-100 arasında bir değer giriniz.");
                else
                    break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Hatalı giriş yaptınız! Lütfen geçerli bir tam sayı giriniz.");
            }
        }
        return grade;
    }

    static string GetLetterGrade(int ortalama)
    {
        if (ortalama >= 85) return "AA";
        else if (ortalama >= 75) return "BA";
        else if (ortalama >= 65) return "BB";
        else if (ortalama >= 50) return "CB";
        else if (ortalama >= 40) return "CC";
        else if (ortalama >= 30) return "DC";
        else if (ortalama >= 20) return "DD";
        else if (ortalama >= 10) return "FD";
        else return "FF";
    }
}