<i>🌟 PayCore .NET Core Bootcamp - Bitirme Projesi</i>

<hr />
<h2>🧐 Proje Hakkında</h2>
<ul>
    <li>Farklı kullanıcıların kayıt olabildiği, sisteme giriş yaparak ürün, kategori ekleyebildiği, başka kullanıcıların ürünlerine teklif verebildiği, ürünleri için aldığı teklifleri kabul edip reddedebildiği, ürünlerin satışını yapabildiği ve daha bir çok detaylı kontrollerden geçen bir API yönetim sistemidir.</li>
    <li>.NET 6 ile geliştirilmiş bir ASP.NET Web API projesidir.</li>
    <li>Farklı kütüphanelere ve generic yapılara yer verilmiştir.</li>
    <li>Sınıflara ve barındırdığı özelliklere dair açıklamalar her dosyanın içerisinde yorum satırlarında detaylı olarak belirtilmiştir.</li>
    <li>Katmanlı bir yapı izlenerek oluşturulmuştur. (Tek proje yapısı tercih edilmiştir.)</li>
    <li>Sistemin büyük çoğunluğu asenkron çalışmaktadır.</li>
    <li><a href="https://www.postgresql.org" target="_blank">PostgreSQL</a> veri tabanı kullanılmıştır.</li>
    <li><a href="https://nhibernate.info" target="_blank">NHibernate</a> ORM aracından yararlanılmıştır.</li>
    <li>Yetkilendirme işlemlerinin ve token bazlı giriş sisteminin entegrasyonu <a href="https://jwt.io">JSON Web Token (JWT)</a> ile gerçekleştirilmiştir.</li>
    <li>Veritabanında kullanıcı şifresi saklama ve saklı şifreyi çözme işlemlerine yer verilmiştir.</li>
    <li>Gerekli validasyon işlemleri <a href="https://fluentvalidation.net">FluentValidation</a> kütüphanesi kullanılarak gerçekleştirilmiştir.</li>
    <li>Veri transferiyle daha güvenli biçimde veritabanında ekleme ve güncelleme işlemlerinin sağlanması DTO'lar ile birlikte <a href="https://docs.automapper.org/en/stable/">AutoMapper</a> kütüphanesi aracılığıyla yapılmıştır.</li>
    <li>Log tutma işlemleri <a href="https://serilog.net">Serilog</a> kütüphanesi aracılığıyla yapılmıştır.</li>
    <li>E-Posta gönderme sisteminin entegrasyonu <a href="https://www.nuget.org/packages/MailKit/">MailKit</a> kütüphanesi aracılığıyla yapılmıştır.</li>
    <li>E-Posta gönderme işlemi SMTP protokolü kullanılarak <a href="https://www.hangfire.io">Hangfire</a> servisi ile background worker olarak gerçekleştirilmiştir.</li>
    <li>Veritabanına ait script kodlarına <a href="" target="_blank">bu linkten</a> erişilebilir. (VPN gerekebilir.)</li>
</ul>

<hr />
<h2>✔ Proje Yapım Süreci & Çalışma Biçimi</h2>
<ul>
    <li>Çalıştırılmadan önce bu yazının okunmasını öneririm. Verilen çalışmanın büyük çoğunluğu oldukça basitti. Bu yüzden daha detaylı ve kontrollü bir sistem oluşturmayı tercih ettim. Bu yazımda tüm detaylarından bahsediyor olacağım.</li>
    <li>NHibernate ORM aracı sisteme dahil edildi. Veritabanı oluşturuldu. PostgreSQL konfigürasyonu uzantı metod aracılığıyla entegre edildi.</li>
    <li>Core katmanı dahil edildi. Yer alacak diğer katmanlara ait ortak işlevlerin, generic yapıların, uzantı metodların, özel API istek ve dönüş tiplerinin ve tüm sistemin ihtiyaç duyabileceği diğer yapılar burada oluşturuldu.</li>
    <li>Entity katmanı dahil edildi. Gerekli tüm entity nesnelerin classları oluşturuldu.</li>
    <li>DTO katmanı dahil edildi. Entity ve DTOlar arasında gerçekleşecek transferler AutoMapper aracılığıyla yapılmaktadır. Gerekli tüm DTO nesneleri oluşturuldu.</li>
    <li>Data Access katmanı dahil edildi. Entity nesnelerine ait session nesnelerinin class ve interfaceleri oluşturuldu. Core katmanında data access için ortak olarak belirlenmiş görevler dışındaki diğer işlemler bu session nesnelerinde yer alabilmektedir.</li>
    <li>Business katmanı dahil edildi. Entity nesnelerine ait servis nesnelerinin class ve interfaceleri oluşturuldu. Core katmanında business için ortak olarak belirlenmiş görevler dışındaki diğer işlemler bu servis nesnelerinde yer alabilmektedir.</li>
    <li>API tarafında kullanılacak olan action metodların requestlerinde yer alacak tüm nesnelerin validasyon kuralları Fluent Validation aracılığıyla belirlendi.</li>
    <li>Sistem mesajlarının tek çatı altında tutulması için bir nesne oluşturuldu.</li>
    <li>API tarafında response dönüşlerindeki yönetimin daha sağlıklı olabilmesi için gerekli bir enum oluşturuldu.</li>
    <li>Hangfire background worker sisteme dahil edildi.</li>
    <li>Statik servisler olan hashing-verifying ile Hangfire'da background worker olarak çalışacak e-posta gönderme servisleri eklendi.</li>
    <li>Kişiselleştirilmiş JSON DateTime formatter eklendi. JSON çıktılarında tarih değerlerinin daha düzgün bir formatta gösterilmesi sağlandı.</li>
    <li>JSON Web Token (JWT) sisteme dahil edildi. Sisteme giriş yapan kullanıcıların e-mail bilgisinin doğruluğu kontrol edildi. Giriş yapan kullanıcının şifresi veritabanında saklanmış şekilde yer alan şifreden çözülerek kontrol edildi.</li>
    <li>Kullanıcıların bu kontrollerden geçtikten sonra başarıyla sisteme bir erişim tokeni alarak giriş yapması sağlandı.</li>
    <li>Kayıt olma işleminde sistemde yer alan bir kullanıcı adı veya e-posta adresi belirtiliyor ise bu işlem gerekli hata bilgisi gösterilerek iptal edilir.</li>
    <li>Kayıt olma işleminde kullanıcının belirlediği parola veritabanında şifrelenerek saklanır ve varsayılan kullanıcı rolü "user" olarak belirlenir.</li>
    <li>Başarıyla kayıt olan kullanıcıya background service olarak bilgilendirme e-postası gönderilir.</li>
    <li>Sistem tarafından gönderilecek tüm e-postalar hata olması durumunda 4 kere daha gönderilmeye çalışılır. Son denemede de hata olması durumunda e-posta gönderme işlemi iptal edilir. Varsayılan deneme sayısı 5 olarak ayarlanmıştır ve değiştirilebilir.</li>
    <li>E-posta gönderme işlemi devam ederken kayıt işlemi kaldığı yerden devam eder ve e-posta gönderme işlemi kayıt olma işleminin hızını bozmaz.</li>
    <li>Kullanıcı silme işleminde eğer kullanıcıya ait sistemde yer alan ürün veya teklifler varsa işlem iptal edilir.</li>
    <li>Kategori ekleme işleminde aynı kategori tekrar eklenemez. Ancak mevcut kategori güncellenebilir.</li>
    <li>Kategori silme işleminde eğer silinmek istenen kategoriye ait eklenmiş ürünler varsa bu işlem iptal edilir. İşlemin gerçekleşebilmesi için o kategoriye ait ürünün olmaması beklenir.</li>
    <li>Sistemde yer alan tüm kategoriler için CRUD işlemlerinin yapılmasına olanak tanıyan CategoryController eklenir.</li>
    <li>Tüm kategorileri listeleme ve ID'ye göre kategori arama dışındaki action metodlar authorize olmayı gerektirir.</li>
    <li>Ürün ekleme işleminde eklenen ürünün giriş yapan kullanıcıya ait bir ürün olduğu belirlenir.</li>
    <li>Ürün silme işleminde öncelikle silinecek ürüne ait teklifler silinir ve sonrasında ürün silinir.</li>
    <li>Teklif yapma işleminde yapılan teklifin giriş yapan kullanıcıya ait olduğu ve teklif yapılan kullanıcının teklifin yapılacak olduğu ürünün sahibi olduğu belirlenir.</li>
    <li>Aynı kullanıcı aynı ürüne tekrar teklif yapamaz. Ancak mevcut teklifini güncelleyebilir.</li>
    <li>Güncellenecek teklif bulunmadıysa işlem iptal edilir.</li>
    <li>Teklif yapılmak istenen ürün eğer teklif yapılmaya kapalıysa işlem iptal edilir.</li>
    <li>Teklifin başarıyla yapılma durumunda teklifi yapan ve teklifin yapıldığı kişilere bilgilendirme e-postası gönderilir.</li>
    <li>Kullanıcı işlemlerinde farklı kullanıcılar tüm kullanıcıların hesaplarını veya tek bir hesabı görüntüleyebilir.</li>
    <li>ProductsController'a ekstra 2 action metod eklenir. Giriş yapan kullanıcının eklediği ürünleri listeleme ve kategori numarasına göre ürünleri listeleme. Kategori numarasına göre ürün listeleme authorize olmayı gerektirmez.</li>
    <li>ProductsController'da yer alan Update ve Delete action metodları kullanıcıların yalnızca kendi eklediği ürünleri güncelleyebilmeleri ve silebilmeleri için Core katmanında yer alan CoreController'dan override edilmiştir.</li>
    <li>ProductsController'a BuyProduct isimli ürün satın alma işlemi eklenir.</li>
    <li>Satın alınan ürünün satılmış olma durumuna ait "IsSold" değeri true olarak ve teklif verilebilme durumuna ait "IsOfferable" değeri false olarak belirlenir.</li>
    <li>Eğer satın alınacak ürün bulunamadıysa işlem iptal edilir. Yanlış bir ürün numarası gönderim durumunda işlem sona erdirilir.</li>
    <li>Eğer satın alınacak ürün zaten satılmış bir ürünse işlem iptal edilir ve satın alma sonlandırılır.</li>
    <li>Ürün başarılı bir şekilde sistemde aksaklık olmadan satın alındıysa satın alan kullanıcıya bilgilendirme e-postası gönderilir.</li>
    <li>Aynı zamanda ürünün sahibinede ürününün satıldığına dair detaylı bir bilgilendirme e-postası gönderilir. Hangi ürünün satıldığı ve hangi kullanıcının satın aldığı bilgileri yer alır.</li>
    <li>CoreController'dan gelen GetAll ve GetById action metodları OffersController'da override edilerek bu controller'da NonAction olarak işaretlenir. Böylelikle kullanıcıların sistemde yer alan tüm teklifleri listelemesi ve numaraya göre teklif araması engellenmiş olunur.</li>
    <li>Kullanıcıların başka kullanıcıların ürünlerine yaptığı teklifleri listelemesi eklenmiştir.</li>
    <li>Kullanıcıların kendi ürünlerine başka kullanıcılar tarafından aldığı tekliflerin listelenmesi eklenmiştir.</li>
    <li>Aynı şekilde teklif yapıldıktan sonra farklı hata durumlarında dönüş mesajlarının daha açıklayıcı olması için Add action metodu CoreController'dan override edilmiştir.</li>
    <li>OffersController'da yer alan Update ve Delete action metodları kullanıcıların yalnızca kendi yaptığı teklifleri güncelleyebilmeleri ve silebilmeleri için Core katmanında yer alan CoreController'dan override edilmiştir.</li>
    <li>OffersController'da kullanıcıların ürünleri için aldığı teklifleri kabul veya reddedebilme işlemi eklenmiştir.</li>
    <li>Belirlenen teklifin yapıldığı kişi bilgisi, giriş yapan kullanıcıya ait değilse işlem iptal edilir. Action metodda yer alan duruma göre teklifin kabul veya red süreci başlar.</li>
    <li>Teklifin reddedilmesi durumunda işlem sona erdirilir. Ürün satışı yapılmaz ve teklifi yapan kullanıcıya detaylı bir bilgilendirme e-postası gönderilir. Teklifin kabul edilmesi durumunda ise ürün satış işlemi başlar.</li>
    <li>Satış başarılı şekilde yapıldıysa satılan ürünün satılmış olma durumu 'true' olarak ve teklif verilebilir olma durumu 'false' tekrar teklif verilemeyecek şekilde güncellenir.</li>
    <li>Teklifin kabul edilmesiyle ve satışın başarılı şekilde sonuçlanmasıyla ürüne teklif gönderen kullanıcıya detaylı bir bilgilendirme e-postası gönderilir.</li>
    <li>Zaten teklifi kabul edilmiş ürünlerin teklif değerlendirmesi tekrar yapılamaz. Ancak her ihtimale karşın bunun da kontrolü yapılır ve bu senaryoda satış daha önceden yapıldığı için iptal edilir.</li>
    <li>Serilog sisteme dahil edildi ve tüm action metodlarda log tutma işlemlerine yer verildi.</li>
</ul>

<hr />
<h2>💻 Proje Yapısı</h2>
<ul>
    <li>Core
        <ul>
            <li>Entity
                <ul>
                    <li>Abstract
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Entity/Abstract/ICoreEntity.cs" target="_blank"><b>ICoreEntity.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Concrete
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Entity/Concrete/CoreEntity.cs" target="_blank"><b>CoreEntity.cs</b></a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>Dto
                <ul>
                    <li>Abstract
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Dto/Abstract/ICoreDto.cs" target="_blank"><b>ICoreDto.cs</b></a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>Data Access
                <ul>
                    <li>Abstract
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/DataAccess/Abstract/ICoreSession.cs" target="_blank"><b>ICoreSession.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/DataAccess/Abstract/IHelperSession.cs" target="_blank"><b>IHelperSession.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Concrete
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/DataAccess/Concrete/CoreSession.cs" target="_blank"><b>CoreSession.cs</b></a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>Business
                <ul>
                    <li>Abstract
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Business/Abstract/ICoreService.cs" target="_blank"><b>ICoreService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Business/Abstract/IService.cs" target="_blank"><b>IService.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Concrete
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Business/Concrete/CoreService.cs" target="_blank"><b>CoreService.cs</b></a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>Web API
                <ul>
                    <li>Abstract
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Abstract/ICoreController.cs" target="_blank"><b>ICoreController.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Concrete
                        <ul>
                            <li>Controllers
                                <ul>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Controllers/CoreController.cs" target="_blank"><b>CoreController.cs</b></a></li>
                                </ul>
                            </li>
                            <li>Extensions
                                <ul>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Extensions/CustomAuthExtension.cs" target="_blank"><b>CustomAuthExtension.cs</b></a></li>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Extensions/CustomSwaggerExtension.cs" target="_blank"><b>CustomSwaggerExtension.cs</b></a></li>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Extensions/NHibernateExtension.cs" target="_blank"><b>NHibernateExtension.cs</b></a></li>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Extensions/ServiceExtension.cs" target="_blank"><b>ServiceExtension.cs</b></a></li>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Extensions/SwaggerFileOperationFilterExtension.cs" target="_blank"><b>SwaggerFileOperationFilterExtension.cs</b></a></li>
                                </ul>
                            </li>
                            <li>Jwt
                                <ul>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Jwt/JwtConfig.cs" target="_blank"><b>JwtConfig.cs</b></a></li>
                                </ul>
                            </li>
                            <li>Requests
                                <ul>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Requests/TokenRequest.cs" target="_blank"><b>TokenRequest.cs</b></a></li>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Requests/BuyProductRequest.cs" target="_blank"><b>BuyProductRequest.cs</b></a></li>
                                </ul>
                            </li>
                            <li>Responses
                                <ul>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Responses/CoreResponse.cs" target="_blank"><b>CoreResponse.cs</b></a></li>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Responses/TokenResponse.cs" target="_blank"><b>TokenResponse.cs</b></a></li>
                                </ul>
                            </li>
                            <li>Utilities
                                <ul>
                                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/WebAPI/Concrete/Utilities/DateTimeConverter.cs" target="_blank"><b>DateTimeConverter.cs</b></a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>Enums
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Enums/Enums.cs" target="_blank"><b>Enums.cs</b></a></li>
                </ul>
            </li>
            <li>Constants
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Core/Constants/SystemMessage.cs" target="_blank"><b>SystemMessage.cs</b></a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li>Entity
        <ul>
            <li>Concrete
                <ul>
                    <li>Entities
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Entities/Account.cs" target="_blank"><b>Account.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Entities/Category.cs" target="_blank"><b>Category.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Entities/Offer.cs" target="_blank"><b>Offer.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Entities/Product.cs" target="_blank"><b>Product.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Mappings
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Mappings/AccountMap.cs" target="_blank"><b>AccountMap.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Mappings/CategoryMap.cs" target="_blank"><b>CategoryMap.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Mappings/OfferMap.cs" target="_blank"><b>OfferMap.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Entity/Concrete/Mappings/ProductMap.cs" target="_blank"><b>ProductMap.cs</b></a></li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ul>
    </li>
    <li>Dto
        <ul>
            <li>Concrete
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Dto/Concrete/AccountDto.cs" target="_blank"><b>AccountDto.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Dto/Concrete/CategoryDto.cs" target="_blank"><b>CategoryDto.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Dto/Concrete/OfferDto.cs" target="_blank"><b>OfferDto.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Dto/Concrete/ProductDto.cs" target="_blank"><b>ProductDto.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Dto/Concrete/EmailDto.cs" target="_blank"><b>EmailDto.cs</b></a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li>Data Access
        <ul>
            <li>Abstract
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Abstract/IAccountSession.cs" target="_blank"><b>IAccountSession.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Abstract/ICategorySession.cs" target="_blank"><b>ICategorySession.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Abstract/IOfferSession.cs" target="_blank"><b>IOfferSession.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Abstract/IProductSession.cs" target="_blank"><b>IProductSession.cs</b></a></li>
                </ul>
            </li>
            <li>Concrete
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Concrete/AccountSession.cs" target="_blank"><b>AccountSession.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Concrete/CategorySession.cs" target="_blank"><b>CategorySession.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Concrete/OfferSession.cs" target="_blank"><b>OfferSession.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/DataAccess/Concrete/ProductSession.cs" target="_blank"><b>ProductSession.cs</b></a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li>Business
        <ul>
            <li>Abstract
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Abstract/IAccountService.cs" target="_blank"><b>IAccountService.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Abstract/ICategoryService.cs" target="_blank"><b>ICategoryService.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Abstract/IOfferService.cs" target="_blank"><b>IOfferService.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Abstract/IProductService.cs" target="_blank"><b>IProductService.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Abstract/ITokenService.cs" target="_blank"><b>ITokenService.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Abstract/IEmailService.cs" target="_blank"><b>IEmailService.cs</b></a></li>
                </ul>
            </li>
            <li>Concrete
                <ul>
                    <li>Mapping Profiles
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/MappingProfiles/MappingProfile.cs" target="_blank"><b>MappingProfile.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Services
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/Services/AccountService.cs" target="_blank"><b>AccountService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/Services/CategoryService.cs" target="_blank"><b>CategoryService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/Services/OfferService.cs" target="_blank"><b>OfferService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/Services/ProductService.cs" target="_blank"><b>ProductService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/Services/TokenService.cs" target="_blank"><b>TokenService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/Services/EmailService.cs" target="_blank"><b>EmailService.cs</b></a> (Bu servis yerine EmailBackgroundService.cs kullanılmaktadır. İncelemek isteyenlere örnek olması açısından kaldırılmadı.)</li>
                        </ul>
                    </li>
                    <li>Static Services
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/StaticServices/EncryptionService.cs" target="_blank"><b>EncryptionService.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/StaticServices/EmailBackgroundService.cs" target="_blank"><b>EmailBackgroundService.cs</b></a></li>
                        </ul>
                    </li>
                    <li>Validation Rules
                        <ul>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/ValidationRules/AccountValidator.cs" target="_blank"><b>AccountValidator.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/ValidationRules/CategoryValidator.cs" target="_blank"><b>CategoryValidator.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/ValidationRules/LoginValidator.cs" target="_blank"><b>LoginValidator.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/ValidationRules/OfferValidator.cs" target="_blank"><b>OfferValidator.cs</b></a></li>
                            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Business/Concrete/ValidationRules/ProductValidator.cs" target="_blank"><b>ProductValidator.cs</b></a></li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ul>
    </li>
    <li>Web API
        <ul>
            <li>Controllers
                <ul>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/WebAPI/Controllers/AccountsController.cs" target="_blank"><b>AccountsController.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/WebAPI/Controllers/AuthController.cs" target="_blank"><b>AuthController.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/WebAPI/Controllers/CategoriesController.cs" target="_blank"><b>CategoriesController.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/WebAPI/Controllers/OffersController.cs" target="_blank"><b>OffersController.cs</b></a></li>
                    <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/WebAPI/Controllers/ProductsController.cs" target="_blank"><b>ProductsController.cs</b></a></li>
                </ul>
            </li>
            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Program.cs" target="_blank"><b>Program.cs</b></a></li>
            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/Startup.cs" target="_blank"><b>Startup.cs</b></a></li>
            <li><a href="https://github.com/TheGreamer/PayCore-Final-Work/blob/master/PayCoreFinalWork.WebAPI/appsettings.json" target="_blank"><b>appsettings.json</b></a></li>
        </ul>
    </li>
</ul>

<hr />
<h2><b>📒 Veritabanı Tablo ve Kolon Şeması</b></h2>
<ul>
    <li>
        <h3>🌟 Kategoriler Tablosu</h3>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/mcvhjll.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <h3>🌟 Kullanıcılar Tablosu</h3>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/2o1fst0.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <h3>🌟 Ürünler Tablosu</h3>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/sdudge6.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <h3>🌟 Teklifler Tablosu</h3>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/2cbo1wp.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
</ul>

<hr />
<h2><b>👨‍💻 Swagger Action Görünümleri</b></h2>
<ul>
    <li>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/9g7bf73.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/sw1j9hl.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/tuk16zv.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/n686kw0.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
    <li>
        <p dir="auto">
            <a target="_blank" rel="noopener noreferrer" href="">
                <img src="https://i.hizliresim.com/qholz54.png" alt="Swagger" style="max-width: 100%;">
            </a>
        </p>
    </li>
</ul>