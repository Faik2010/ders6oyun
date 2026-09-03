#nullable disable

using System;
using System.Collections.Generic;

class Program
{
    static Player oyuncu;
    static List<Monster> canavarlar = new List<Monster>();

    static void Main()
    {
        // ============================================================
        // CANAVAR AVI
        // Bu projede:
        // Class, Object, Property, List, Method,
        // if/else, switch, while, for, iç içe for,
        // Random ve TryParse konularını birlikte kullanıyoruz.
        // ============================================================

        OyunuBaslat();

        bool oyunDevam = true;

        while (oyunDevam)
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("             CANAVAR AVI");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Oyuncu : " + oyuncu.Name);
            Console.WriteLine("Can    : " + oyuncu.Health);
            Console.WriteLine("Level  : " + oyuncu.Level);
            Console.WriteLine("Skor   : " + oyuncu.Score);
            Console.WriteLine();
            Console.WriteLine("1 - Haritayı Göster");
            Console.WriteLine("2 - Canavarlarla Savaş");
            Console.WriteLine("3 - Oyuncu Bilgileri");
            Console.WriteLine("4 - Canavarları Göster");
            Console.WriteLine("5 - Oyundan Çık");
            Console.WriteLine();

            Console.Write("Seçimin: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    HaritayiGoster();
                    break;

                case "2":
                    SavasMenusu();
                    break;

                case "3":
                    OyuncuBilgileriniGoster();
                    break;

                case "4":
                    CanavarlariGoster();
                    break;

                case "5":
                    oyunDevam = false;
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("Geçersiz seçim!");
                    Bekle();
                    break;
            }

            if (oyuncu.Health <= 0)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              OYUN BİTTİ");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("Canavarlar seni yendi!");
                Console.WriteLine("Toplam skorun: " + oyuncu.Score);

                oyunDevam = false;
                Bekle();
            }

            if (TumCanavarlarOldu() && oyuncu.Health > 0)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("           TEBRİKLER!");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("Tüm canavarları yendin!");
                Console.WriteLine("Toplam skorun: " + oyuncu.Score);
                Console.WriteLine("Level'in: " + oyuncu.Level);

                oyunDevam = false;
                Bekle();
            }
        }

        Console.Clear();
        Console.WriteLine("Oyundan çıktın. Görüşürüz!");
    }

    static void OyunuBaslat()
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("          CANAVAR AVI'NA HOŞ GELDİN!");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.Write("Oyuncu adın: ");
        string isim = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isim))
        {
            isim = "Kahraman";
        }

        // Object oluşturuyoruz.
        // Player class'ından "oyuncu" adında bir nesne oluşturduk.
        oyuncu = new Player();

        // Object'in property'lerine değer veriyoruz.
        oyuncu.Name = isim;
        oyuncu.Health = 100;
        oyuncu.AttackPower = 20;
        oyuncu.Level = 1;
        oyuncu.Score = 0;

        // List kullanarak birden fazla Monster object'i tutuyoruz.
        canavarlar.Add(new Monster("Goblin", 40, 8, 50));
        canavarlar.Add(new Monster("Ork", 60, 12, 100));
        canavarlar.Add(new Monster("İskelet", 50, 10, 80));
        canavarlar.Add(new Monster("Troll", 80, 15, 150));
        canavarlar.Add(new Monster("Ejderha", 120, 20, 300));

        Console.WriteLine();
        Console.WriteLine("Hoş geldin " + oyuncu.Name + "!");
        Console.WriteLine();
        Console.WriteLine("Amacın bütün canavarları yenmek.");
        Console.WriteLine("Başarılar!");

        Bekle();
    }

    static void HaritayiGoster()
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("                HARİTA");
        Console.WriteLine("========================================");
        Console.WriteLine();

        // İç içe for kullanıyoruz.
        // Dış for satırları, iç for ise sütunları oluşturuyor.
        for (int satir = 0; satir < 5; satir++)
        {
            for (int sutun = 0; sutun < 5; sutun++)
            {
                string sembol = "[ ]";

                // Oyuncunun bulunduğu konum
                if (satir == 2 && sutun == 2)
                {
                    sembol = "[P]";
                }

                // Bazı hücrelere canavar yerleştiriyoruz.
                if (satir == 0 && sutun == 4 && !canavarlar[0].Defeated)
                {
                    sembol = "[G]";
                }

                if (satir == 1 && sutun == 1 && !canavarlar[1].Defeated)
                {
                    sembol = "[O]";
                }

                if (satir == 3 && sutun == 3 && !canavarlar[2].Defeated)
                {
                    sembol = "[I]";
                }

                if (satir == 4 && sutun == 0 && !canavarlar[3].Defeated)
                {
                    sembol = "[T]";
                }

                if (satir == 0 && sutun == 0 && !canavarlar[4].Defeated)
                {
                    sembol = "[E]";
                }

                Console.Write(sembol);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("[P] = Oyuncu");
        Console.WriteLine("[G] = Goblin");
        Console.WriteLine("[O] = Ork");
        Console.WriteLine("[I] = İskelet");
        Console.WriteLine("[T] = Troll");
        Console.WriteLine("[E] = Ejderha");

        Bekle();
    }

    static void SavasMenusu()
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("             CANAVARLAR");
        Console.WriteLine("========================================");
        Console.WriteLine();

        bool canavarVar = false;

        // List içerisindeki bütün canavarları dolaşıyoruz.
        for (int i = 0; i < canavarlar.Count; i++)
        {
            Monster canavar = canavarlar[i];

            if (!canavar.Defeated)
            {
                canavarVar = true;

                Console.WriteLine(
                    (i + 1) + " - " +
                    canavar.Name +
                    " | HP: " +
                    canavar.Health +
                    " | Saldırı: " +
                    canavar.AttackPower
                );
            }
        }

        if (!canavarVar)
        {
            Console.WriteLine("Yenecek canavar kalmadı.");
            Bekle();
            return;
        }

        Console.WriteLine();
        Console.Write("Savaşmak istediğin canavarın numarası: ");

        string input = Console.ReadLine();

        int secim;

        // Kullanıcının gerçekten sayı girip girmediğini kontrol ediyoruz.
        bool sayiMi = int.TryParse(input, out secim);

        if (!sayiMi)
        {
            Console.WriteLine();
            Console.WriteLine("Lütfen sayı gir.");
            Bekle();
            return;
        }

        if (secim < 1 || secim > canavarlar.Count)
        {
            Console.WriteLine();
            Console.WriteLine("Geçersiz canavar numarası.");
            Bekle();
            return;
        }

        Monster secilenCanavar = canavarlar[secim - 1];

        if (secilenCanavar.Defeated)
        {
            Console.WriteLine();
            Console.WriteLine("Bu canavar zaten yenildi.");
            Bekle();
            return;
        }

        Savas(secilenCanavar);
    }

    static void Savas(Monster canavar)
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("               SAVAŞ!");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.WriteLine(oyuncu.Name + " VS " + canavar.Name);
        Console.WriteLine();

        Random random = new Random();

        while (oyuncu.Health > 0 && canavar.Health > 0)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(oyuncu.Name + " HP: " + oyuncu.Health);
            Console.WriteLine(canavar.Name + " HP: " + canavar.Health);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            Console.WriteLine("1 - Saldır");
            Console.WriteLine("2 - Kaç");
            Console.Write("Seçimin: ");

            string secim = Console.ReadLine();

            if (secim == "1")
            {
                // Random kullanarak saldırının biraz değişmesini sağlıyoruz.
                int oyuncuHasari = random.Next(
                    oyuncu.AttackPower - 5,
                    oyuncu.AttackPower + 6
                );

                if (oyuncuHasari < 1)
                {
                    oyuncuHasari = 1;
                }

                canavar.Health -= oyuncuHasari;

                Console.WriteLine();
                Console.WriteLine(
                    oyuncu.Name +
                    " saldırdı ve " +
                    oyuncuHasari +
                    " hasar verdi!"
                );

                if (canavar.Health <= 0)
                {
                    canavar.Health = 0;
                    canavar.Defeated = true;

                    oyuncu.Score += canavar.Score;

                    // Her savaş kazanıldığında level kontrolü yapıyoruz.
                    oyuncu.Level++;

                    Console.WriteLine();
                    Console.WriteLine("🎉 " + canavar.Name + " yenildi!");
                    Console.WriteLine("+" + canavar.Score + " skor kazandın!");
                    Console.WriteLine("Level'in " + oyuncu.Level + " oldu!");

                    Bekle();
                    return;
                }

                // Canavar hala yaşıyorsa karşı saldırı yapıyor.
                int canavarHasari = random.Next(
                    canavar.AttackPower - 3,
                    canavar.AttackPower + 4
                );

                if (canavarHasari < 1)
                {
                    canavarHasari = 1;
                }

                oyuncu.Health -= canavarHasari;

                if (oyuncu.Health < 0)
                {
                    oyuncu.Health = 0;
                }

                Console.WriteLine(
                    canavar.Name +
                    " karşı saldırı yaptı ve " +
                    canavarHasari +
                    " hasar verdi!"
                );

                Console.WriteLine();
            }
            else if (secim == "2")
            {
                Console.WriteLine();
                Console.WriteLine("Savaştan kaçtın!");

                Bekle();
                return;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Geçersiz seçim!");
            }
        }

        Bekle();
    }

    static void OyuncuBilgileriniGoster()
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("          OYUNCU BİLGİLERİ");
        Console.WriteLine("========================================");
        Console.WriteLine();

        // Object'in property'lerini okuyoruz.
        Console.WriteLine("İsim          : " + oyuncu.Name);
        Console.WriteLine("Can           : " + oyuncu.Health);
        Console.WriteLine("Saldırı Gücü  : " + oyuncu.AttackPower);
        Console.WriteLine("Level         : " + oyuncu.Level);
        Console.WriteLine("Skor          : " + oyuncu.Score);

        Console.WriteLine();

        Bekle();
    }

    static void CanavarlariGoster()
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("             CANAVAR LİSTESİ");
        Console.WriteLine("========================================");
        Console.WriteLine();

        // List içerisinde bulunan her Monster object'ini dolaşıyoruz.
        foreach (Monster canavar in canavarlar)
        {
            string durum;

            if (canavar.Defeated)
            {
                durum = "YENİLDİ";
            }
            else
            {
                durum = "HAYATTA";
            }

            Console.WriteLine(
                canavar.Name +
                " | HP: " +
                canavar.Health +
                " | Saldırı: " +
                canavar.AttackPower +
                " | Skor: " +
                canavar.Score +
                " | Durum: " +
                durum
            );
        }

        Console.WriteLine();

        Bekle();
    }

    static bool TumCanavarlarOldu()
    {
        // Bütün canavarları kontrol ediyoruz.
        for (int i = 0; i < canavarlar.Count; i++)
        {
            if (!canavarlar[i].Defeated)
            {
                return false;
            }
        }

        return true;
    }

    static void Bekle()
    {
        Console.WriteLine();
        Console.WriteLine("Devam etmek için bir tuşa bas...");
        Console.ReadKey();
    }
}


// ============================================================
// PLAYER CLASS
// ============================================================

// Class kendi oluşturduğumuz bir veri tipidir.
// Oyuncuya ait bilgileri tek bir yerde topluyoruz.
class Player
{
    // Property'ler object'in özelliklerini temsil eder.
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public int Level { get; set; }
    public int Score { get; set; }
}


// ============================================================
// MONSTER CLASS
// ============================================================

// Aynı şekilde canavarların özelliklerini de
// kendi class'ımız içerisinde topluyoruz.
class Monster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public int Score { get; set; }
    public bool Defeated { get; set; }

    // Bu constructor sayesinde Monster oluştururken
    // bilgileri tek seferde verebiliyoruz.
    public Monster(string name, int health, int attackPower, int score)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        Score = score;
        Defeated = false;
    }
}