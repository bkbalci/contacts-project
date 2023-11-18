# Contacts Project

Merhaba! Bu proje, ContactService ve ReportService adlı iki servisi içermektedir. ContactService, rehberdeki kullanıcılar ve iletişim bilgileriyle ilgili temel işlevleri yerine getirirken, ReportService ise rehberle ilgili raporların performanslı bir şekilde oluşturulması ve görüntülenmesi işlevlerini sunmaktadır.

## ContactService

Bu servis, PostgreSQL veritabanını kullanarak rehberle ilgili bir dizi işlevi gerçekleştirir.

### İşlevler:

- **Kişi Oluşturma:**
  Rehbere yeni bir kişi ekleyebilirsiniz.

- **Kişi Kaldırma:**
  Rehberden bir kişiyi kaldırabilirsiniz.

- **İletişim Bilgisi Ekleme:**
  Bir kişiye yeni iletişim bilgisi ekleyebilirsiniz.

- **İletişim Bilgisi Kaldırma:**
  Bir kişinin iletişim bilgisini rehberden kaldırabilirsiniz.

- **Kişilerin Listelenmesi:**
  Rehberdeki tüm kişileri listeleyebilirsiniz.

- **Detay Bilgilerin Getirilmesi:**
  Belirli bir kişiyle ilgili detay bilgileri, iletişim bilgileriyle birlikte getirebilirsiniz.


## ReportService

ReportService, rapor oluşturma ve görüntüleme işlevlerini gerçekleştirir.

### İşlevler:

- **Konuma Göre İstatistik Raporu:**
  Rehberdeki kişilerin bulundukları konuma göre istatistiklerini içeren bir rapor talep edebilirsiniz.

- **Oluşturulan Raporların Listelenmesi:**
  Sistem tarafından oluşturulan raporları listeleyebilirsiniz.

- **Rapor Detaylarının Getirilmesi:**
  Sistem tarafından oluşturulan bir raporun detaylı bilgilerini getirebilirsiniz.


## Kullanım

      git clone https://github.com/bkbalci/contacts-project.git
      cd contacts-project
      docker-compose up -d
