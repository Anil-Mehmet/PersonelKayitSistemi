# Personel Kayıt Sistemi

ASP.NET Core MVC, Entity Framework Core (Code First) ve MSSQL kullanılarak geliştirilmiş bir personel devam takip sistemidir.

## Özellikler

- Barkod okutarak otomatik giriş/çıkış kaydı
- Personel kayıt yönetimi (ekleme, düzenleme, silme, detay görüntüleme)
- Tarih ve barkoda göre filtrelenebilir hareket geçmişi
- İzin talebi oluşturma ve yönetici onay/red mekanizması
- İki farklı kullanıcı rolü:
  - **Muhasebe (Personel girişi):** Personel yönetimi, giriş-çıkış işlemleri, izin talebi oluşturma
  - **Yönetici (Admin girişi):** İzin taleplerini onaylama/reddetme, genel özet paneli

## Kullanılan Teknolojiler

- ASP.NET Core MVC (.NET)
- Entity Framework Core (Code First, Migrations)
- Microsoft SQL Server (LocalDB)
- Bootstrap 5, HTML5, CSS3, JavaScript

## Kurulum

1. Depoyu klonlayın
2. Visual Studio'da açın
3. Package Manager Console üzerinden `Update-Database` komutunu çalıştırın
4. Projeyi çalıştırın (F5)