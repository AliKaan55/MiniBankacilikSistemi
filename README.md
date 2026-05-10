🏦 N-Tier Banking System Simulation
Bu proje, N-Tier (Katmanlı) Mimari prensipleri kullanılarak geliştirilmiş, temel bankacılık operasyonlarını yöneten bir backend simülasyonudur. Proje; veri güvenliğini sağlamak amacıyla Entity Framework Core ve Database Transaction yönetimini temel alır.

 Özellikler
Kullanıcı Yönetimi: Kullanıcı kaydı ve güvenli giriş (Login) sistemi.

Hesap Yönetimi: Kullanıcıya özel hesap (TL/USD/EUR) açma ve yönetme.

Para Hareketleri: Para yatırma, para çekme ve hesaplar arası transfer.

İşlem Geçmişi: Yapılan tüm finansal hareketlerin (Deposit, Transfer vb.) tarihçesini görüntüleme.

Veri Bütünlüğü: Para transferi sırasında oluşabilecek hatalara karşı Transaction (Rollback/Commit) yönetimi.

🛠 Kullanılan Teknolojiler
Dil: C# (.NET 8.0+)

ORM: Entity Framework Core

Veritabanı: MS SQL Server

Mimari: N-Tier Architecture (EL, DAL, BL, UI)

🏗 Proje Yapısı (N-Tier)
Proje 4 temel katmandan oluşmaktadır:

Entities (EL): Veritabanı tablolarını temsil eden sınıflar (User, Account, Transaction).

DataAccess (DAL): DbContext yapılandırması ve veritabanı bağlantı yönetimi.

Business (BL): İş mantığının (Validation, Kurallar) ve servislerin bulunduğu katman.

UI (Console): Kullanıcı ile etkileşime geçilen sunum katmanı.
