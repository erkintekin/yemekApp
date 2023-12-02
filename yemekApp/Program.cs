using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using static yemekApp.Program;

namespace yemekApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestaurantSystem restaurantSystem = new RestaurantSystem();

            Console.WriteLine("\n***********\tHoşgeldiniz\t*************\n");

            while (true)
            {

                Console.WriteLine("\n********   Menüden seçim yapınız   **********\n");

                Console.WriteLine("\t*******\t1. Yemek ekle\t*********\n");
                Console.WriteLine("\t*******\t2. Menüyü göster  ********\n");
                Console.WriteLine("\t*******\t3. Sipariş ver\t********\n");
                Console.WriteLine("\t*******\t4. Çıkış\t********\n");
                int menuSecim = int.Parse(Console.ReadLine());

                switch (menuSecim)
                {
                    case 1:
                        Console.Clear();
                        YemekCounter(restaurantSystem);
                        break;

                    case 2:
                        Console.WriteLine("***********\tMenülerimiz\t**********\n");
                        restaurantSystem.MenuyuGoster();
                        break;

                    case 3:
                        restaurantSystem.SiparisAl();
                        break;
                    case 4:
                        Environment.Exit(0);
                         break;
                    default:
                        Console.WriteLine("Lütfen doğru değer giriniz!");
                        break;
                }

            }

        }

        public static void YemekCounter(RestaurantSystem restaurantSystem)
        {
            Console.WriteLine("\tKaç adet yemek ekleyeceksiniz?");
            Console.Write("\nAdet: ");
            int sayi = int.Parse(Console.ReadLine());

            for (int i = 0; i < sayi; i++)
            {
                Console.WriteLine($"\n\t{i + 1}. yemeği giriniz\n");
                Console.Write("Yemek Adı: ");
                string yemekAdi = Console.ReadLine();


                Console.WriteLine("\nFiyatını ekleyiniz\n");
                Console.Write("Fiyat: ");
                int fiyatGiris = int.Parse(Console.ReadLine());

                Yemek yemek1 = new Yemek(yemekAdi, fiyatGiris);

                restaurantSystem.YemekEkle(yemek1);
                yemek1.YemekBilgisiYazdir();

            }
        }

    }


    public class Yemek
    {
        public string YemekAdi { get; set; }

        public int Fiyat { get; set; }


        public Yemek(string yemek, int fiyat)
        {
            YemekAdi = yemek;
            Fiyat = fiyat;

        }

        public virtual void YemekBilgisiYazdir()
        {
            Console.WriteLine($"\nYemek: {YemekAdi}, Fiyatı {Fiyat}\n");
        }

    }

    public class anaYemek : Yemek
    {
        public anaYemek(string yemek, int fiyat) : base(yemek, fiyat)
        {

        }

        public override void YemekBilgisiYazdir()
        {

            Console.WriteLine($"Seçtiğiniz ana yemek {YemekAdi}, Fiyatı {Fiyat}");
        }
    }


    public class Tatlı : Yemek
    {
        public Tatlı(string yemek, int fiyat) : base(yemek, fiyat)
        {

        }

        public override void YemekBilgisiYazdir()
        {

            Console.WriteLine($"Seçtiğiniz tatlı {YemekAdi}, Fiyatı {Fiyat}");
        }
    }

    public class Icecek : Yemek
    {
        public Icecek(string yemek, int fiyat) : base(yemek, fiyat)
        {

        }

        public override void YemekBilgisiYazdir()
        {

            Console.WriteLine($"Seçtiğiniz içecek {YemekAdi}, Fiyatı {Fiyat}");
        }
    }

    public class RestaurantSystem
    {
        List<Yemek> YemekListesi = new List<Yemek>();
        List<Yemek> Sepet = new List<Yemek>();

        public void YemekEkle(Yemek yemek)
        {
            YemekListesi.Add(yemek);
        }


        public void MenuyuGoster()  // YemekBilgisiYazdir'ı triggerlasın
        {
            foreach (var item in YemekListesi)
            {
                item.YemekBilgisiYazdir();
            }
        }

        public void SiparisAl()
        {
            MenuyuGoster();

            Console.WriteLine("Kaç adet sipariş vereceksiniz?\n");
            Console.Write("Sipariş adedi:");
            int secim = int.Parse(Console.ReadLine());

            Console.WriteLine("\nSipariş adı giriniz:");
            string siparisAdi = Console.ReadLine();

            for (int i = 0; i < secim; i++)
            {
                Console.WriteLine($"\n{i + 1}. vermek istediğiniz ürünü girin\n");
                string y1 = Console.ReadLine();

                Console.WriteLine($"{i + 1}. vermek istediğiniz fiyatını girin\n");
                int fiyat = int.Parse(Console.ReadLine());

                Yemek yemekSecim = new Yemek(y1, fiyat);

                if (YemekListesi.Any(y => y.YemekAdi == yemekSecim.YemekAdi) && YemekListesi.Any(y => y.Fiyat == yemekSecim.Fiyat))
                {
                    Sepet.Add(yemekSecim);
                }
                else
                {
                    Console.WriteLine("Aradığınız yemek bulunamadı veya fiyatı yanlış girdiniz, tekrar kontrol ediniz!");
                }

                int toplamFiyat = Sepet.Sum(y => y.Fiyat);

                if (Sepet.Count > 0)
                {
                    foreach (var item in Sepet)
                    {
                        Console.WriteLine($"Sepetinizdeki ürünler: {item.YemekAdi}, {item.Fiyat} TL\n");
                    }
                }
                Console.WriteLine($"\nSipariş adı:{siparisAdi}\nToplam Fiyat: {toplamFiyat} TL");



            }

        }
    }


}
